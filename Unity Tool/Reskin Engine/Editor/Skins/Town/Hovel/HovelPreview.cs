#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Houses

    public class HovelPreview : GenericBuildingPreview<HovelSkin>
    {
        public override string path => $"{buildingsRoot}/House";
    }

    #endregion
}

#endif