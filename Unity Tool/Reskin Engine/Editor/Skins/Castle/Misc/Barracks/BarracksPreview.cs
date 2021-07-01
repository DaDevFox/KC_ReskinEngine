#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public class BarracksPreview : GenericBuildingPreview<BarracksSkin>
    {
        public override string path => $"{buildingsRoot}/Barracks";
    }

}

#endif