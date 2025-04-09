using Maya2Babylon;

namespace MayaBabylon
{
    public struct GlobalVertex
    {
        public int BaseIndex { get; set; }
        public int CurrentIndex { get; set; }
        public float[] Position { get; set; } // Vec3
        public float[] Normal { get; set; } // Vec3
        public float[] Tangent { get; set; } // Vec3
        public float[] UV { get; set; } // Vec2
        public float[] UV2 { get; set; } // Vec2
        public float[] UV3 { get; set; } // Vec2
        public float[] UV4 { get; set; } // Vec2
        public float[] UV5 { get; set; } // Vec2
        public float[] UV6 { get; set; } // Vec2
        public float[] UV7 { get; set; } // Vec2
        public float[] UV8 { get; set; } // Vec2
        public int BonesIndices { get; set; }
        public float[] Weights { get; set; } // Vec4
        public int BonesIndicesExtra1 { get; set; }
        public int BonesIndicesExtra2 { get; set; }
        public int BonesIndicesExtra3 { get; set; }
        public float[] WeightsExtra1 { get; set; } // Vec4
        public float[] WeightsExtra2 { get; set; } // Vec4
        public float[] WeightsExtra3 { get; set; } // Vec4
        public float[] Color { get; set; } // Vec4

        public GlobalVertex(GlobalVertex other)
        {
            this.BaseIndex = other.BaseIndex;
            this.CurrentIndex = other.CurrentIndex;
            this.Position = other.Position;
            this.Normal = other.Normal;
            this.Tangent = other.Tangent;
            this.UV = other.UV;
            this.UV2 = other.UV2;
            this.UV3 = other.UV3;
            this.UV4 = other.UV4;
            this.UV5 = other.UV5;
            this.UV6 = other.UV6;
            this.UV7 = other.UV7;
            this.UV8 = other.UV8;
            this.BonesIndices = other.BonesIndices;
            this.Weights = other.Weights;
            this.BonesIndicesExtra1 = other.BonesIndicesExtra1;
            this.BonesIndicesExtra2 = other.BonesIndicesExtra2;
            this.BonesIndicesExtra3 = other.BonesIndicesExtra3;
            this.WeightsExtra1 = other.WeightsExtra1;
            this.WeightsExtra2 = other.WeightsExtra2;
            this.WeightsExtra3 = other.WeightsExtra3;
            this.Color = other.Color;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is GlobalVertex other)
            {
                // Note the logic located into the Tools.IsAlmostEqualTo Extension which is also check for null parameters.
                return 
                    other.BaseIndex == BaseIndex &&
                    other.Position.IsAlmostEqualTo(Position, Tools.Epsilon) &&
                    other.Normal.IsAlmostEqualTo(Normal, Tools.Epsilon) &
                    other.UV.IsAlmostEqualTo(UV, Tools.Epsilon) &&
                    other.UV2.IsAlmostEqualTo(UV2, Tools.Epsilon) &&
                    other.UV3.IsAlmostEqualTo(UV3, Tools.Epsilon) &&
                    other.UV4.IsAlmostEqualTo(UV4, Tools.Epsilon) &&
                    other.UV5.IsAlmostEqualTo(UV5, Tools.Epsilon) &&
                    other.UV6.IsAlmostEqualTo(UV6, Tools.Epsilon) &&
                    other.UV7.IsAlmostEqualTo(UV7, Tools.Epsilon) &&
                    other.UV8.IsAlmostEqualTo(UV8, Tools.Epsilon) &&
                    other.Weights.IsAlmostEqualTo(Weights, Tools.Epsilon) &&
                    other.WeightsExtra1.IsAlmostEqualTo(WeightsExtra1, Tools.Epsilon) &&
                    other.WeightsExtra2.IsAlmostEqualTo(WeightsExtra2, Tools.Epsilon) &&
                    other.WeightsExtra3.IsAlmostEqualTo(WeightsExtra3, Tools.Epsilon) &&
                    other.Color.IsAlmostEqualTo(Color, Tools.Epsilon) && 
                    other.BonesIndices == BonesIndices;
            }
            return false;
        }
    }
}
