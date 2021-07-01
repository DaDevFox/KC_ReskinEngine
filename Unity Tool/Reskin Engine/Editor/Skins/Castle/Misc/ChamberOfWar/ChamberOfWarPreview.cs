#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Misc Buildings

    public class ChamberOfWarPreview : GenericBuildingPreview<ChamberOfWarSkin>
    {
        public override string path => $"{buildingsRoot}/ChamberOfWar";
    }

    #endregion
}

#endif