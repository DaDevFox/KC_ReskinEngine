#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Castle Blocks

    public class StoneCastleBlockPreview : CastleBlockPreview<StoneCastleBlockSkin>
    {
        public override string OpenPath => "Reskin Engine/Models/GameModels (and some prefabs)/1x1x5";
        public override string ClosedPath => "Reskin Engine/Models/GameModels (and some prefabs)/1x1x5_fulltop";
        public override string SinglePath => "Reskin Engine/Models/GameModels (and some prefabs)/1x1x5_onesided";
        public override string OppositePath => "Reskin Engine/Models/GameModels (and some prefabs)/1x1x5_twosided";
        public override string AdjacentPath => "Reskin Engine/Models/GameModels (and some prefabs)/1x1x5_corner";
        public override string ThreesidePath => "Reskin Engine/Models/GameModels (and some prefabs)/1x1x5_threesided";
        public override string DoorPath => "Reskin Engine/Models/GameModels (and some prefabs)/castlestairs_doorway";
    }

    #endregion
}

#endif