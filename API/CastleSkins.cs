using UnityEngine;

namespace ReskinEngine.API
{

   

    ////Keep
    public class KeepBuildingSkin : BuildingSkin
    {
        public override string FriendlyName => "Keep"; 
        public override string UniqueName => "keep";

        [Model(description = "The base upgrade for the keep")]
        public GameObject keepUpgrade1;
        [Model(description = "The second upgrade for the keep")]
        public GameObject keepUpgrade2;
        [Model(description = "The third upgrade for the keep")]
        public GameObject keepUpgrade3;
        [Model(description = "The fourth upgrade for the keep")]
        public GameObject keepUpgrade4;

        protected override void PackageInternal(Transform target, GameObject _base)
        {
            base.PackageInternal(target, _base);

            if (keepUpgrade1)
                GameObject.Instantiate(keepUpgrade1, _base.transform).name = "keepUpgrade1";
            if (keepUpgrade2)
                GameObject.Instantiate(keepUpgrade2, _base.transform).name = "keepUpgrade2";
            if (keepUpgrade3)
                GameObject.Instantiate(keepUpgrade3, _base.transform).name = "keepUpgrade3";
            if (keepUpgrade4)
                GameObject.Instantiate(keepUpgrade4, _base.transform).name = "keepUpgrade4";

        }
    }

    #region Castle Blocks

    /// <summary>
    /// Don't use as a skin
    /// </summary>
    [Hidden]
    public class CastleBlockBuildingSkinBase : BuildingSkin
    {
        protected override void PackageInternal(Transform target, GameObject _base)
        {
            base.PackageInternal(target, _base);

            if (Open)
                GameObject.Instantiate(Open, _base.transform).name = "Open";
            if (Closed)
                GameObject.Instantiate(Closed, _base.transform).name = "Closed";
            if (Single)
                GameObject.Instantiate(Single, _base.transform).name = "Single";
            if (Opposite)
                GameObject.Instantiate(Opposite, _base.transform).name = "Opposite";
            if (Adjacent)
                GameObject.Instantiate(Adjacent, _base.transform).name = "Adjacent";
            if (Threeside)
                GameObject.Instantiate(Threeside, _base.transform).name = "Threeside";

            if (doorPrefab)
                GameObject.Instantiate(doorPrefab, _base.transform).name = "doorPrefab";

        }


        /// <summary>
        /// The flat piece without crenelations for a castle block
        /// This is a modular piece. See info.txt for details
        /// </summary>
        [Model(ModelAttribute.Type.Modular, description = "The flat piece without crenelations for a castle block")]
        public GameObject Open;
        /// <summary>
        /// The piece of a castleblock with all crenelations at the top and no connections
        /// This is a modular piece. See info.txt for details
        /// </summary>
        [Model(ModelAttribute.Type.Modular, description = "The piece of a castleblock with all crenelations at the top and no connections")]
        public GameObject Closed;
        /// <summary>
        /// 
        /// This is a modular piece. See info.txt for details
        /// </summary>
        [Model(ModelAttribute.Type.Modular, description = "The piece of a castleblock that only has crenelations on one side")]
        public GameObject Single;
        /// <summary>
        /// The straight piece of a castle block
        /// This is a modular piece. See info.txt for details
        /// </summary>
        [Model(ModelAttribute.Type.Modular, description = "The straight piece of a castle block")]
        public GameObject Opposite;
        /// <summary>
        /// The corner piece for a castle block
        /// This is a modular piece. See info.txt for details
        /// </summary>
        [Model(ModelAttribute.Type.Modular, description = "The corner piece for a castle block")]
        public GameObject Adjacent;
        /// <summary>
        /// The piece of a castleblock with crenelations on 3 sides
        /// This is a modular piece. See info.txt for details
        /// </summary>
        [Model(ModelAttribute.Type.Modular, description = "The piece of a castleblock with crenelations on 3 sides")]
        public GameObject Threeside;

        /// <summary>
        /// The door that appears on a castleblock when it connects to other castleblocks
        /// </summary>
        [Seperator]
        [Model(description = "The door that appears on a castleblock when it connects to other castleblocks")]
        public GameObject doorPrefab;
    }

    //Wood Castle Block
    public class WoodCastleBlockBuildingSkin : CastleBlockBuildingSkinBase
    {
        public override string FriendlyName => "Wooden Castle Block";
        public override string UniqueName => "woodcastleblock";
        public override string TypeIdentifier => base.TypeIdentifier;
    }

    //Stone Castle Block
    public class StoneCastleBlockBuildingSkin : CastleBlockBuildingSkinBase
    {
        public override string FriendlyName => "Stone Castle Block";
        public override string UniqueName => "castleblock";
        public override string TypeIdentifier => base.TypeIdentifier;
    }

    #endregion

    #region Gates

    //  Gate Base
    [Hidden]
    public class GateBuildingSkinBase : BuildingSkin
    {
        [Model(description = "The main model of the gate, excluding the porticulus")]
        public GameObject gate;
        [Model(description = "The part of the gate that moves up and down to show opening/closing")]
        public GameObject porticulus;

        protected override void PackageInternal(Transform target, GameObject _base)
        {
            base.PackageInternal(target, _base);

            if (gate)
                GameObject.Instantiate(gate, _base.transform).name = "gate";
            if (porticulus)
                GameObject.Instantiate(porticulus, _base.transform).name = "porticulus";
        }

    }

    //Wooden Gate
    public class WoodenGateBuildingSkin : GateBuildingSkinBase
    {
        public override string FriendlyName => "Wooden Gate";
        public override string UniqueName => "woodengate";
        public override string TypeIdentifier => base.TypeIdentifier;
    }

    //Stone Gate
    public class StoneGateBuildingSkin : GateBuildingSkinBase
    {
        public override string FriendlyName => "Stone Gate";
        public override string UniqueName => "gate";

        public override string TypeIdentifier => base.TypeIdentifier;

    }

    #endregion

    //Castle Stairs
    public class CastleStairsBuildingSkin : BuildingSkin
    {
        public override string FriendlyName => "Castle Stairs";
        public override string UniqueName => "castlestairs";

        public override string TypeIdentifier => base.TypeIdentifier;


        protected override void PackageInternal(Transform target, GameObject _base)
        {
            base.PackageInternal(target, _base);

            if (stairsFront)
                GameObject.Instantiate(stairsFront, _base.transform).name = "stairsFront";
            if (stairsRight)
                GameObject.Instantiate(stairsRight, _base.transform).name = "stairsRight";
            if (stairsDown)
                GameObject.Instantiate(stairsDown, _base.transform).name = "stairsDown";
            if (stairsLeft)
                GameObject.Instantiate(stairsLeft, _base.transform).name = "stairsLeft";
        }

        [Model(ModelAttribute.Type.Modular, description = "stairs facing +z")]
        public GameObject stairsFront;
        [Model(ModelAttribute.Type.Modular, description = "stairs facing +x")]
        public GameObject stairsRight;
        [Model(ModelAttribute.Type.Modular, description = "stairs facing -z")]
        public GameObject stairsDown;
        [Model(ModelAttribute.Type.Modular, description = "stairs facing -x")]
        public GameObject stairsLeft;
    }

    #region Towers

    //Archer Tower
    [NotSupported]
    public class ArcherTowerBuildingSkin : BuildingSkin
    {
        public override string FriendlyName => "Archer Tower";

        public override string UniqueName => "archer";

        public override string TypeIdentifier => base.TypeIdentifier;

        protected override void PackageInternal(Transform target, GameObject _base)
        {
            base.PackageInternal(target, _base);

            if (baseModel)
                GameObject.Instantiate(baseModel, _base.transform).name = "baseModel";
            if (veteranModel)
                GameObject.Instantiate(veteranModel, _base.transform).name = "veteranModel";
        }


        /// <summary>
        /// The main model of the Archer Tower
        /// </summary>
        [Model(description = "The main model of the Archer Tower")]
        public GameObject baseModel;
        /// <summary>
        /// An embelishment added to the archer tower when it achieves the veteran status
        /// </summary>
        [Model(description = "An embelishment added to the archer tower when it achieves the veteran status")]
        public GameObject veteranModel;
    }

    //Ballista Tower
    [NotSupported]
    public class BallistaTowerBuildingSkin : BuildingSkin
    {
        public override string FriendlyName => "Ballista Tower";
        public override string UniqueName => "ballista";

        public override string TypeIdentifier => base.TypeIdentifier;


        protected override void PackageInternal(Transform target, GameObject _base)
        {
            base.PackageInternal(target, _base);

            if (baseModel)
                GameObject.Instantiate(baseModel, _base.transform).name = "baseModel";
            if (veteranModel)
                GameObject.Instantiate(veteranModel, _base.transform).name = "veteranModel";
            if (topBase)
                GameObject.Instantiate(topBase, _base.transform).name = "topBase";
            if (armR)
                GameObject.Instantiate(armR, _base.transform).name = "armR";
            if (armREnd)
                GameObject.Instantiate(armREnd, _base.transform).name = "armREnd";
            if (armL)
                GameObject.Instantiate(armL, _base.transform).name = "armL";
            if (armLEnd)
                GameObject.Instantiate(armLEnd, _base.transform).name = "armLEnd";
            if (stringR)
                GameObject.Instantiate(stringR, _base.transform).name = "stringR";
            if (stringL)
                GameObject.Instantiate(stringL, _base.transform).name = "stringL";
            if (projectile)
                GameObject.Instantiate(projectile, _base.transform).name = "projectile";
            if (projectileEnd)
                GameObject.Instantiate(projectileEnd, _base.transform).name = "projectileEnd";
            
            if (flag)
                GameObject.Instantiate(flag, _base.transform).name = "flag";
        }

        /// <summary>
        /// An embelishment added to the ballista tower when it achieves the veteran status
        /// </summary>
        [Model(description = "An embelishment added to the archer tower when it achieves the veteran status")]
        public GameObject veteranModel;
        /// <summary>
        /// The main model of the Ballista Tower
        /// </summary>
        [Model(description = "The main model of the Ballista Tower")]
        public GameObject baseModel;
        /// <summary>
        /// The base of the rotational top half of the ballista
        /// </summary>
        [Model(description = "The base of the rotational top half of the ballista")]
        public GameObject topBase;
        /// <summary>
        /// The right side arm used to animate the ballista's firing movement
        /// </summary>
        [Seperator]
        [Model(description = "The right side arm used to animate the ballista's firing movement")]
        public GameObject armR;
        /// <summary>
        /// The right end of the right arm of the ballista; used for anchoring the right side of the string in animation
        /// </summary>
        [Model(description = "The right end of the right arm of the ballista; used for anchoring the right side of the string in animation")]
        public Transform armREnd;
        /// <summary>
        /// The left side arm used to animate the ballista's firing movement
        /// </summary>
        [Seperator]
        [Model(description = "The left side arm used to animate the ballista's firing movement")]
        public GameObject armL;
        /// <summary>
        /// The left end of the left arm of the ballista; used for anchoring the left side of the string in animation
        /// </summary>
        [Model(description = "The left end of the left arm of the ballista; used for anchoring the left side of the string in animation")]
        public Transform armLEnd;
        /// <summary>
        /// The right side of the animated string used to pull back and fire the ballista projectile
        /// </summary>
        [Seperator]
        [Model(description = "The right side of the animated string used to pull back and fire the ballista projectile")]
        public GameObject stringR;
        /// <summary>
        /// The left side of the animated string used to pull back and fire the ballista projectile
        /// </summary>
        [Model(description = "The left side of the animated string used to pull back and fire the ballista projectile")]
        public GameObject stringL;
        /// <summary>
        /// The projectile fired from the ballista
        /// </summary>
        [Seperator]
        [Model(description = "The projectile fired from the ballista")]
        public GameObject projectile;
        /// <summary>
        /// The end of the ballista projectile that's pulled back before firing
        /// </summary>
        [Model(description = "The end of the ballista projectile that's pulled back before firing")]
        public Transform projectileEnd;
        /// <summary>
        /// A decorative flag on the ballista
        /// </summary>
        [Seperator]
        [Model(description = "A decorative flag on the ballista")]
        public GameObject flag;
    }

    #endregion


    //#region Town



    //#endregion

    //#region Advanced Town

    ////Hospital
    //public class HospitalBuildingSkin : GenericBuildingSkin
    //{
    //    public HospitalBuildingSkin()
    //    {
    //        _buildingUniqueName = "hospital";
    //    }
    //}

    //#endregion

    //#region Food

    //public class MarketBuildingSkin : BuildingSkin
    //{
    //    public MarketBuildingSkin()
    //    {
    //        _removalProcedure = Procedures.rp_market;
    //        _replaceProcedure = delegate(Building b) { Procedures.rep_market(b, this);  };

    //        _buildingUniqueName = "market";
    //    }

    //    /// <summary>
    //    /// The gameobject that will replace the base model; will be instantiated at runtime
    //    /// </summary>
    //    public GameObject model;
    //    /// <summary>
    //    /// Optional; creates visual stacks of resources at the specified positions, does not affect actual resource consumption or productions, 
    //    /// but should match the max storage of the building to make it visually accurate. 
    //    /// If left null this field will be set to its default value; 
    //    /// </summary>
    //    public ResourceStacks resourceStacks = null;
    //    /// <summary>
    //    /// Optional; the place resources will be left in most scenarios the resourceStacks are full; 
    //    /// If left null this field will be set to its default value; 
    //    /// </summary>
    //    public Transform resourceDropoff = null;
    //    /// <summary>
    //    /// Optional; the positions peasants stand while working at the building; 
    //    /// If left null this field will be set to its default value; 
    //    /// </summary>
    //    public Transform[] personPositions = null;
    //}




    //#endregion

    //#region Path-Types

    //#region Generic

    //public class PathTypeBuildingSkin : BuildingSkin
    //{
    //    public PathTypeBuildingSkin()
    //    {
    //        _removalProcedure = delegate (Building b) { Procedures.rp_path(b); };
    //        _replaceProcedure = delegate (Building b) { Procedures.rep_path(b, this); };
    //    }

    //    public GameObject Straight;
    //    public GameObject Elbow;
    //    public GameObject Threeway;
    //    public GameObject Fourway;
    //}

    //#endregion

    //#region Roads

    //public class RoadBuildingSkin : PathTypeBuildingSkin
    //{
    //    public RoadBuildingSkin()
    //    {
    //        _buildingUniqueName = "road";
    //    }
    //}

    //public class StoneRoadBuildingSkin : PathTypeBuildingSkin
    //{
    //    public StoneRoadBuildingSkin()
    //    {
    //        _buildingUniqueName = "stoneroad";
    //    }
    //}

    //#endregion

    //#region Bridges

    //public class WoodenBridgeBuildingSkin : PathTypeBuildingSkin
    //{
    //    public WoodenBridgeBuildingSkin()
    //    {
    //        _buildingUniqueName = "bridge";
    //    }
    //}

    //public class StoneBridgeBuildingSkin : PathTypeBuildingSkin
    //{
    //    public StoneBridgeBuildingSkin()
    //    {
    //        _buildingUniqueName = "stonebridge";
    //    }
    //}

    //#endregion

    //#region Garden

    //public class GardenBuildingSkin : BuildingSkin
    //{
    //    public GardenBuildingSkin()
    //    {
    //        _removalProcedure = delegate (Building b) { Procedures.rp_garden(b); };
    //        _replaceProcedure = delegate (Building b) { Procedures.rep_garden(b, this); };

    //        _buildingUniqueName = "garden";
    //    }

    //    public GameObject Straight;
    //    public GameObject Elbow;
    //    public GameObject Threeway;
    //    public GameObject Fourway;
    //    public GameObject Fourway_Special;

    //    public Mesh Straight_flowers;
    //    public Mesh Elbow_flowers;
    //    public Mesh Threeway_flowers;
    //    public Mesh Fourway_flowers;
    //    public Mesh Fourway_Special_flowers;
    //}


    //#endregion

    //#endregion

    //#endregion

}
