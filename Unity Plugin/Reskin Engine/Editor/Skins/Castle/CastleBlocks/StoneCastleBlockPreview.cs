#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Castle Blocks

    public class StoneCastleBlockPreview : CastleBlockPreview<StoneCastleBlockSkin>
    {
        public override string OpenPath => "Assets/Game/KCAssets/Models/GameModels (and some prefabs)/1x1x5.fbx";
        public override string ClosedPath => "Assets/Game/KCAssets/Models/GameModels (and some prefabs)/1x1x5_fulltop.fbx";
        public override string SinglePath => "Assets/Game/KCAssets/Models/GameModels (and some prefabs)/1x1x5_onesided.fbx";
        public override string OppositePath => "Assets/Game/KCAssets/Models/GameModels (and some prefabs)/1x1x5_twosided.fbx";
        public override string AdjacentPath => "Assets/Game/KCAssets/Models/GameModels (and some prefabs)/1x1x5_corner.fbx";
        public override string ThreesidePath => "Assets/Game/KCAssets/Models/GameModels (and some prefabs)/1x1x5_threesided.fbx";
        public override string DoorPath => "Assets/Game/KCAssets/Models/GameModels (and some prefabs)/castlestairs_doorway.fbx";
    }

    #endregion
}

#endif