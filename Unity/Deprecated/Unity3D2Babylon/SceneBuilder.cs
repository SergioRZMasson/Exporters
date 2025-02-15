using System;
using System.Collections.Generic;
using System.IO;
using BabylonExport.Entities;
using JsonFx.Json;
using UnityEngine;
using Object = UnityEngine.Object;
using JsonFx.Serialization;
using JsonFx.Serialization.Resolvers;

namespace Unity3D2Babylon
{
    public partial class SceneBuilder
    {
        public string OutputPath { get; private set; }
        public string SceneName { get; private set; }

        readonly Dictionary<string, BabylonMaterial> materialsDictionary;
        readonly Dictionary<string, BabylonMultiMaterial> multiMatDictionary;

        readonly Dictionary<int, string> uniqueGuids;

        readonly BabylonScene babylonScene;
        GameObject[] gameObjects;

        readonly ExportationOptions exportationOptions;

        BabylonTexture sceneReflectionTexture;

        public SceneBuilder(string outputPath, string sceneName, ExportationOptions exportationOptions)
        {
            OutputPath = outputPath;
            SceneName = string.IsNullOrEmpty(sceneName) ? "scene" : sceneName;

            materialsDictionary = new Dictionary<string, BabylonMaterial>();
            multiMatDictionary = new Dictionary<string, BabylonMultiMaterial>();
            uniqueGuids = new Dictionary<int, string>();

            babylonScene = new BabylonScene(OutputPath);

            babylonScene.producer = new BabylonProducer
            {
                file = Path.GetFileName(outputPath),
                version = "Unity3D",
                name = SceneName,
                exporter_version = "0.8"
            };

            this.exportationOptions = exportationOptions;
        }

        public string WriteToBabylonFile()
        {
            babylonScene.Prepare();

            var outputFile = Path.Combine(OutputPath, SceneName + ".babylon");

            var settings = new DataWriterSettings(new DataContractResolverStrategy()) {PrettyPrint = true};

            var jsWriter = new JsonWriter(settings);

            string babylonJSformat = jsWriter.Write(babylonScene);
            using (var sw = new StreamWriter(outputFile))
            {
                sw.Write(babylonJSformat);
                sw.Close();
            }

            return outputFile;
        }

        public void GenerateStatus(List<string> logs)
        {
            var initialLog = new List<string>
            {
                "*Exportation Status:",
                babylonScene.meshes.Length + " mesh(es)",
                babylonScene.lights.Length + " light(s)",
                babylonScene.cameras.Length + " camera(s)",
                babylonScene.materials.Length + " material(s)",
                babylonScene.multiMaterials.Length + " multi-material(s)",
                "",
                "*Log:"
            };

            logs.InsertRange(0, initialLog);
        }

        string GetParentID(Transform transform)
        {
            if (transform.parent == null)
            {
                return null;
            }

            return GetID(transform.parent.gameObject);
        }

        string GetID(GameObject gameObject)
        {
            var key = gameObject.GetInstanceID();

            if (!uniqueGuids.ContainsKey(key))
            {
                uniqueGuids[key] = Guid.NewGuid().ToString();
            }

            return uniqueGuids[key];
        }

        public void ConvertFromUnity()
        {
            ExporterWindow.ReportProgress(0, "Starting Babylon.js exportation process...");

            gameObjects = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];

            if (gameObjects.Length == 0)
            {
                ExporterWindow.ShowMessage("No gameobject! - Please add at least a gameobject to export");
                return;
            }

            var itemsCount = gameObjects.Length;

            var index = 0;
            foreach (var gameObject in gameObjects)
            {
                var progress = ((float)index / itemsCount);
                index++;
                // Static meshes
                var meshFilter = gameObject.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {                    
                    ConvertUnityMeshToBabylon(meshFilter.sharedMesh, meshFilter.transform, gameObject, progress);
                    continue;
                }

                // Skinned meshes
                var skinnedMesh = gameObject.GetComponent<SkinnedMeshRenderer>();
                if (skinnedMesh != null)
                {
                    var babylonMesh = ConvertUnityMeshToBabylon(skinnedMesh.sharedMesh, skinnedMesh.transform, gameObject, progress);
                    var skeleton = ConvertUnitySkeletonToBabylon(skinnedMesh.bones, skinnedMesh.sharedMesh.bindposes, skinnedMesh.transform, gameObject, progress);
                    babylonMesh.skeletonId = skeleton.id;

                    ExportSkeletonAnimation(skinnedMesh, babylonMesh, skeleton);
                    continue;
                }

                // Light
                var light = gameObject.GetComponent<Light>();
                if (light != null)
                {
                    ConvertUnityLightToBabylon(light, progress);
                    continue;
                }

                // Camera
                var camera = gameObject.GetComponent<Camera>();
                if (camera != null)
                {
                    ConvertUnityCameraToBabylon(camera, progress);
                    ConvertUnitySkyboxToBabylon(camera, progress);
                    continue;
                }

                // Empty
                ConvertUnityEmptyObjectToBabylon(gameObject);
            }

            // Materials
            foreach (var mat in materialsDictionary)
            {
                babylonScene.MaterialsList.Add(mat.Value);
            }

            foreach (var multiMat in multiMatDictionary)
            {
                babylonScene.MultiMaterialsList.Add(multiMat.Value);
            }

            // Collisions
            if (exportationOptions.ExportCollisions)
            {
                babylonScene.gravity = exportationOptions.Gravity.ToFloat();
            }
        }

        private static void ExportSkeletonAnimation(SkinnedMeshRenderer skinnedMesh, BabylonMesh babylonMesh, BabylonSkeleton skeleton)
        {
            var animator = skinnedMesh.rootBone.gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                ExportSkeletonAnimationClips(animator, true, skeleton, skinnedMesh.bones, babylonMesh);
            }
            else
            {
                var parent = skinnedMesh.rootBone.parent;
                while (parent != null)
                {
                    animator = parent.gameObject.GetComponent<Animator>();
                    if (animator != null)
                    {
                        ExportSkeletonAnimationClips(animator, true, skeleton, skinnedMesh.bones, babylonMesh);
                        break;
                    }

                    parent = parent.parent;
                }
            }
        }
    }
}
