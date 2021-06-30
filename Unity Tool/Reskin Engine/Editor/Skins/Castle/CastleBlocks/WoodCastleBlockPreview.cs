#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Castle Blocks
    public class WoodCastleBlockPreview : CastleBlockPreview<WoodCastleBlockSkin>
    {
        public override string OpenPath => "Assets/Game/KCAssets/Models/GameModels (and some prefabs)/1x1x5_wood.fbx";
        public override string ClosedPath => "Assets/Game/KCAssets/Models/GameModels (and some prefabs)/1x1x5_fulltop_wood.fbx";
        public override string SinglePath => "Assets/Game/KCAssets/Models/GameModels (and some prefabs)/1x1x5_onesided_wood.fbx";
        public override string OppositePath => "Assets/Game/KCAssets/Models/GameModels (and some prefabs)/1x1x5_twosided_wood.fbx";
        public override string AdjacentPath => "Assets/Game/KCAssets/Models/GameModels (and some prefabs)/1x1x5_corner_wood.fbx";
        public override string ThreesidePath => "Assets/Game/KCAssets/Models/GameModels (and some prefabs)/1x1x5_threesided_wood.fbx";
        public override string DoorPath => "Assets/Game/KCAssets/Models/GameModels (and some prefabs)/castlestairs_doorway_wooden.fbx";
    }

    #endregion
}

#endif