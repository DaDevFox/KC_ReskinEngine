#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Houses

    public class CottagePreview : GenericBuildingPreview<CottageSkin>
    {
        public override string path => $"{buildingsRoot}/LargeHouse";
    }

    #endregion
}

#endif