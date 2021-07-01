#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Castle Blocks

    public class StoneCastleBlockPreview : CastleBlockPreview<StoneCastleBlockSkin>
    {
        public override string OpenPath => $"{modelsRoot}/1x1x5";
        public override string ClosedPath => $"{modelsRoot}/1x1x5_fulltop";
        public override string SinglePath => $"{modelsRoot}/1x1x5_onesided";
        public override string OppositePath => $"{modelsRoot}/1x1x5_twosided";
        public override string AdjacentPath => $"{modelsRoot}/1x1x5_corner";
        public override string ThreesidePath => $"{modelsRoot}/1x1x5_threesided";
        public override string DoorPath => $"{modelsRoot}/castlestairs_doorway";
    }

    #endregion
}

#endif