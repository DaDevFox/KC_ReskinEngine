#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Misc Buildings

    public class TreasureRoomPreview : GenericBuildingPreview<TreasureRoomSkin>
    {
        public override string path => $"{buildingsRoot}/ThroneRoom";
    }

    #endregion
}

#endif