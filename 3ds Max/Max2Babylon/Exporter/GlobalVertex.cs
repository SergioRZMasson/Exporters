using Autodesk.Max;
using Utilities;

namespace Max2Babylon
{
    public struct GlobalVertex
    {

        public int BaseIndex { get; set; }
        public int CurrentIndex { get; set; }
        public IPoint3 Position { get; set; }
        public IPoint3 Normal { get; set; }
        public float[] Tangent { get; set; }
        public IPoint2 UV { get; set; }
        public IPoint2 UV2 { get; set; }
        public IPoint2 UV3 { get; set; }
        public IPoint2 UV4 { get; set; }
        public IPoint2 UV5 { get; set; }
        public IPoint2 UV6 { get; set; }
        public IPoint2 UV7 { get; set; }
        public IPoint2 UV8 { get; set; }
        public int BonesIndices { get; set; }
        public IPoint4 Weights { get; set; }
        public int BonesIndicesExtra1 { get; set; }
        public int BonesIndicesExtra2 { get; set; }
        public int BonesIndicesExtra3 { get; set; }
        public IPoint4 WeightsExtra1 { get; set; }
        public IPoint4 WeightsExtra2 { get; set; }
        public IPoint4 WeightsExtra3 { get; set; }
        public float[] Color { get; set; }

        public GlobalVertex(GlobalVertex other)
        {
            this.BaseIndex = other.BaseIndex;
            this.CurrentIndex = other.CurrentIndex;
            this.Position = other.Position != null ? other.Position.Clone() : null;
            this.Normal = other.Normal != null ? other.Normal.Clone() : null;
            this.Tangent = other.Tangent != null ? other.Tangent.Clone2() : null;
            this.UV = other.UV != null ? other.UV.Clone() : null;
            this.UV2 = other.UV2 != null ? other.UV2.Clone() : null;
            this.UV3 = other.UV3 != null ? other.UV3.Clone() : null;
            this.UV4 = other.UV4 != null ? other.UV4.Clone() : null;
            this.UV5 = other.UV5 != null ? other.UV5.Clone() : null;
            this.UV6 = other.UV6 != null ? other.UV6.Clone() : null;
            this.UV7 = other.UV7 != null ? other.UV7.Clone() : null;
            this.UV8 = other.UV8 != null ? other.UV8.Clone() : null;
            this.BonesIndices = other.BonesIndices;
            this.Weights = other.Weights != null ? other.Weights.Clone() : null;
            this.BonesIndicesExtra1 = other.BonesIndicesExtra1;
            this.BonesIndicesExtra2 = other.BonesIndicesExtra2;
            this.BonesIndicesExtra3 = other.BonesIndicesExtra3;
            this.WeightsExtra1 = other.WeightsExtra1 != null ? other.WeightsExtra1.Clone() : null;
            this.WeightsExtra2 = other.WeightsExtra2 != null ? other.WeightsExtra2.Clone() : null;
            this.WeightsExtra3 = other.WeightsExtra3 != null ? other.WeightsExtra3.Clone() : null;
            this.Color = other.Color != null ? other.Color.Clone2() : null;
        }

        public override int GetHashCode()
        {
            //return string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}",
            //                        BaseIndex,
            //                        CurrentIndex,
            //                        Position != null ? Position.ToArray() : null,
            //                        Normal != null ? Normal.ToArray() : null,
            //                        Tangent,
            //                        UV != null ? UV.ToArray() : null,
            //                        UV2 != null ? UV.ToArray() : null,
            //                        BonesIndices,
            //                        Weights != null ? Weights.ToArray() : null,
            //                        BonesIndicesExtra,
            //                        WeightsExtra != null ? WeightsExtra.ToArray() : null,
            //                        Color).GetHashCode();
            
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is GlobalVertex))
            {
                return false;
            }

            var other = (GlobalVertex)obj;

            if (other.BaseIndex != BaseIndex)
            {
                return false;
            }

            if (!other.Position.IsAlmostEqualTo(Position, Tools.Epsilon))
            {
                return false;
            }

            if (!other.Normal.IsAlmostEqualTo(Normal, Tools.Epsilon))
            {
                return false;
            }

            if (UV != null && !other.UV.IsAlmostEqualTo(UV, Tools.Epsilon))
            {
                return false;
            }

            if (UV2 != null && !other.UV2.IsAlmostEqualTo(UV2, Tools.Epsilon))
            {
                return false;
            }

            if (UV3 != null && !other.UV3.IsAlmostEqualTo(UV3, Tools.Epsilon))
            {
                return false;
            }

            if (UV4 != null && !other.UV4.IsAlmostEqualTo(UV4, Tools.Epsilon))
            {
                return false;
            }

            if (UV5 != null && !other.UV5.IsAlmostEqualTo(UV5, Tools.Epsilon))
            {
                return false;
            }

            if (UV6 != null && !other.UV6.IsAlmostEqualTo(UV6, Tools.Epsilon))
            {
                return false;
            }

            if (UV7 != null && !other.UV7.IsAlmostEqualTo(UV7, Tools.Epsilon))
            {
                return false;
            }

            if (UV8 != null && !other.UV8.IsAlmostEqualTo(UV8, Tools.Epsilon))
            {
                return false;
            }

            if (Weights != null && !other.Weights.IsAlmostEqualTo(Weights, Tools.Epsilon))
            {
                return false;
            }

            if (WeightsExtra1 != null && !other.WeightsExtra1.IsAlmostEqualTo(WeightsExtra1, Tools.Epsilon))
            {
                return false;
            }

            if (WeightsExtra2 != null && !other.WeightsExtra2.IsAlmostEqualTo(WeightsExtra2, Tools.Epsilon))
            {
                return false;
            }

            if (WeightsExtra3 != null && !other.WeightsExtra3.IsAlmostEqualTo(WeightsExtra3, Tools.Epsilon))
            {
                return false;
            }

            if (Color != null && !other.Color.IsAlmostEqualTo(Color, Tools.Epsilon))
            {
                return false;
            }

            return other.BonesIndices == BonesIndices;
        }
    }
}
