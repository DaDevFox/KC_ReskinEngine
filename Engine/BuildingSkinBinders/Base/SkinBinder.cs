using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Harmony;

namespace ReskinEngine.Engine
{
    public abstract class SkinBinder
    {
        /// <summary>
        /// The index of the skin within its collection
        /// </summary>
        public int Identifier { get; internal set; }

        /// <summary>
        /// Compatability identifier used to detremine which skins can be used with this one
        /// </summary>
        public string CompatabilityIdentifier { get; private set; }
        /// <summary>
        /// Name of the collection the skin is included in
        /// </summary>
        public string CollectionName { get; private set; }

        /// <summary>
        /// String that the engine uses to identify this binder
        /// </summary>
        public virtual string TypeIdentifier { get; }

        /// <summary>
        /// Creates a SkinBinder from the GameObject based on the information provided by the GameObject's name and returns it. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static SkinBinder Unpack(GameObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            string[] info = obj.name.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

            string compatabilityIdentifier = info[0];
            string collection = info[1];
            string skinType = info[2];
            int skinIdentifier = int.Parse(info[3]);


            SkinBinder original = Engine.GetOriginalBinder(skinType);

            if(original == null)
            {
                Engine.dLog($"No skin binder found for type identifier {skinType}");
                return null;
            }


            SkinBinder instance = original.Create(obj);

            
            instance.CompatabilityIdentifier = compatabilityIdentifier;
            instance.CollectionName = collection;
            instance.Identifier = skinIdentifier;

            return instance;
        }

        /// <summary>
        /// Using the GameObject packaged by the API-side, this method create a new SkinBinder with the fields that correspond to the GameObjects children assigned
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public abstract SkinBinder Create(GameObject obj);


        /// <summary>
        /// Use to bind this skin using any data recieved from the data GameObject
        /// </summary>
        public virtual void Bind()
        {

        }
    }

    /// <summary>
    /// An implementation of SkinBinder designed specifically for buildings and supports skin variation on building placement
    /// <para>Inherit from this to create a skin for buildings that can have variations on placement</para>
    /// </summary>
    public abstract class BuildingSkinBinder : SkinBinder
    {
        /// <summary>
        /// Do not override for BuildingSkinBindres
        /// </summary>
        public sealed override string TypeIdentifier => $"building_{UniqueName}";

        public abstract string UniqueName { get; }

        public Vector3[] peoplePositions;


        /// <summary>
        /// Do not override for BuildingSkinBinders
        /// </summary>
        public sealed override void Bind()
        {
            if (UniqueName != "unregistered")
            {
                Engine.helper.Log(UniqueName);
                BindToBuildingBase(GameState.inst.GetPlaceableByUniqueName(UniqueName));
            }
        }

        /// <summary>
        /// Use this to bind the skin to the base building that will be duplicated every time a building is placed with the given unique name
        /// </summary>
        /// <param name="building"></param>
        public virtual void BindToBuildingBase(Building building)
        {

        }

        /// <summary>
        /// Use this to bind the skin to a single building after it has been placed
        /// </summary>
        /// <param name="building"></param>
        public virtual void BindToBuildingInstance(Building building)
        {

        }



        /// <summary>
        /// Helper function that applies peoplePositions from a GameObject to any BuildingSkinBinder; use in Create()
        /// <para>Only neccessary for buildings with workers</para>
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="_base"></param>
        protected void ApplyPersonPositions(BuildingSkinBinder binder, GameObject _base)
        {
            Transform container = _base.transform.Find("personPositions");
            if (container)
            {
                List<Vector3> positions = new List<Vector3>();
                for (int i = 0; i < container.transform.childCount; i++)
                    positions.Add(container.GetChild(i).transform.position);

                binder.peoplePositions = positions.ToArray();
            }
        }

        /// <summary>
        /// Helper function that binds personPositions from a BuildingSkinBinder to a building, preserving their original transforms but changing the positions
        /// </summary>
        /// <param name="building"></param>
        /// <param name="binder"></param>
        protected void BindPersonPositions(Building building, BuildingSkinBinder binder)
        {
            for (int i = 0; i < building.personPositions.Length; i++)
            {
                if (binder.peoplePositions.Length > i)
                {
                    building.personPositions[i].localPosition = binder.peoplePositions[i];
                }
            }
        }


        [HarmonyPatch(typeof(World), "PlaceInternal")]
        static class OnPlacePatch
        {
            static void Postfix(Building PendingObj)
            {
                SkinBinder s = Engine.GetRandomBinderFromActive("building_" + PendingObj.UniqueName);
                if(s as BuildingSkinBinder != null)
                {
                    BuildingSkinBinder binder = s as BuildingSkinBinder;
                    binder.BindToBuildingInstance(PendingObj);

                    Engine.helper.Log($"binding building skin [{binder.TypeIdentifier}:{binder.Identifier}] to {binder.UniqueName}");
                }
            }
        }

    }

    [Unregistered]
    public class GenericBuildingSkinBinder : BuildingSkinBinder
    {
        public override string UniqueName => "";

        public GameObject baseModel;

        public override SkinBinder Create(GameObject obj)
        {
            var inst = new GenericBuildingSkinBinder();

            if (obj.transform.Find("baseModel"))
                inst.baseModel = obj.transform.Find("baseModel").gameObject;

            ApplyPersonPositions(inst, obj);

            return inst;
        }

        public override void BindToBuildingBase(Building building)
        {
            Engine.helper.Log((building == null).ToString());

            Transform target = building.transform.GetChild(0).GetChild(0);

            foreach (Transform t in building.transform)
                Engine.helper.Log(t.ToString());

            if (!target)
            {
                Engine.helper.Log("GenericBuildingSkinBinder bound to building that doesn't follow generic building architechture; aborting");
                return;
            }


            if (baseModel)
            {
                target.GetComponent<MeshFilter>().mesh = null;
                GameObject.Instantiate(baseModel, target);
            }

            //BindPersonPositions(building, this);
        }

        public override void BindToBuildingInstance(Building building)
        {
            this.BindToBuildingBase(building);
        }
    }



    //    public class KeepBuildingSkin : BuildingSkin
    //    {
    //        public KeepBuildingSkin()
    //        {
    //            _removalProcedure = delegate (Building b) { Procedures.rp_keep(b); };
    //            _replaceProcedure = delegate (Building b) { Procedures.rep_keep(b, this); };

    //            _buildingUniqueName = "keep";
    //        }

    //        public GameObject keepUpgrade1;
    //        public GameObject keepUpgrade2;
    //        public GameObject keepUpgrade3;
    //        public GameObject keepUpgrade4;

    //        public GameObject banner1;
    //        public GameObject banner2;

    //        protected override void PackageInternal(Transform target, GameObject _base)
    //        {
    //            base.PackageInternal(target, _base);

    //            if (keepUpgrade1)
    //                GameObject.Instantiate(keepUpgrade1, _base.transform).name = "keepUpgrade1";
    //            if (keepUpgrade2)
    //                GameObject.Instantiate(keepUpgrade2, _base.transform).name = "keepUpgrade2";
    //            if (keepUpgrade3)
    //                GameObject.Instantiate(keepUpgrade3, _base.transform).name = "keepUpgrade3";
    //            if (keepUpgrade4)
    //                GameObject.Instantiate(keepUpgrade4, _base.transform).name = "keepUpgrade4";

    //            if (banner1)
    //                GameObject.Instantiate(banner1, _base.transform).name = "banner1";
    //            if (banner2)
    //                GameObject.Instantiate(banner2, _base.transform).name = "banner2";
    //        }
    //    }

    //    #region Castle Blocks

    //    //Castle Block Base
    //    public class CastleBlockBuildingSkin : BuildingSkin
    //    {
    //        public CastleBlockBuildingSkin()
    //        {
    //            _removalProcedure = delegate (Building b) { Procedures.rp_castleblock(b); };
    //            _replaceProcedure = delegate (Building b) { Procedures.rep_castleblock(b, this); };
    //        }

    //        /// <summary>
    //        /// The flat piece without crenelations for a castle block
    //        /// This is a modular piece. See info.txt for details
    //        /// </summary>
    //        public GameObject Open;
    //        /// <summary>
    //        /// The piece of a castleblock with all crenelations at the top and no connections
    //        /// This is a modular piece. See info.txt for details
    //        /// </summary>
    //        public GameObject Closed;
    //        /// <summary>
    //        /// The piece of a castleblock that only has crenelations on one side
    //        /// This is a modular piece. See info.txt for details
    //        /// </summary>
    //        public GameObject Single;
    //        /// <summary>
    //        /// The straight piece of a castle block
    //        /// This is a modular piece. See info.txt for details
    //        /// </summary>
    //        public GameObject Opposite;
    //        /// <summary>
    //        /// The corner piece for a castle block
    //        /// This is a modular piece. See info.txt for details
    //        /// </summary>
    //        public GameObject Adjacent;
    //        /// <summary>
    //        /// The piece of a castleblock with crenelations on 3 sides
    //        /// This is a modular piece. See info.txt for details
    //        /// </summary>
    //        public GameObject Threeside;

    //        /// <summary>
    //        /// The door that appears on a castleblock when it connects to other castleblocks
    //        /// </summary>
    //        public GameObject doorPrefab;
    //    }

    //    //Wood Castle Block
    //    public class WoodCastleBlockBuildingSkin : CastleBlockBuildingSkin
    //    {
    //        public WoodCastleBlockBuildingSkin()
    //        {
    //            _buildingUniqueName = "woodcastleblock";
    //        }

    //    }

    //    //Stone Castle Block
    //    public class StoneCastleBlockBuildingSkin : CastleBlockBuildingSkin
    //    {
    //        public StoneCastleBlockBuildingSkin()
    //        {
    //            _buildingUniqueName = "castleblock";
    //        }
    //    }

    //    #endregion

    //    #region Gates

    //    //  Gate Base
    //    public class GateBuildingSkin : BuildingSkin
    //    {
    //        public GateBuildingSkin()
    //        {
    //            _removalProcedure = delegate (Building b) { Procedures.rp_gate(b); };
    //            _replaceProcedure = delegate (Building b) { Procedures.rep_gate(b, this); };
    //        }

    //        public GameObject gate;
    //        public GameObject porticulus;
    //    }

    //    //Wooden Gate
    //    public class WoodenGateBuildingSkin : GateBuildingSkin
    //    {
    //        public WoodenGateBuildingSkin()
    //        {
    //            _buildingUniqueName = "woodengate";
    //        }

    //    }

    //    //Stone Gate
    //    public class StoneGateBuildingSkin : GateBuildingSkin
    //    {
    //        public StoneGateBuildingSkin()
    //        {
    //            _buildingUniqueName = "gate";
    //        }

    //    }

    //    #endregion

    //    //Castle Stairs
    //    public class CastleStairsBuildingSkin : BuildingSkin
    //    {
    //        public CastleStairsBuildingSkin()
    //        {
    //            _removalProcedure = delegate (Building b) { Procedures.rp_castlestairs(b); };
    //            _replaceProcedure = delegate (Building b) { Procedures.rep_castlestairs(b, this); };

    //            _buildingUniqueName = "castlestairs";
    //        }

    //        public GameObject stairsFront;
    //        public GameObject stairsRight;
    //        public GameObject stairsDown;
    //        public GameObject stairsLeft;
    //    }

    //    //Archer Tower
    //    public class ArcherTowerBuildingSkin : BuildingSkin
    //    {
    //        public ArcherTowerBuildingSkin()
    //        {
    //            _removalProcedure = delegate (Building b) { Procedures.rp_archer(b); };
    //            _replaceProcedure = delegate (Building b) { Procedures.rep_archer(b, this); };

    //            _buildingUniqueName = "archer";
    //        }


    //        /// <summary>
    //        /// The main model of the Archer Tower
    //        /// </summary>
    //        public GameObject baseModel;
    //        /// <summary>
    //        /// An embelishment added to the archer tower when it achieves the veteran status
    //        /// </summary>
    //        public GameObject veteranModel;
    //    }

    //    //Ballista Tower
    //    public class BallistaTowerBuildingSkin : BuildingSkin
    //    {
    //        public BallistaTowerBuildingSkin()
    //        {
    //            _removalProcedure = delegate (Building b) { Procedures.rp_ballista(b); };
    //            _replaceProcedure = delegate (Building b) { Procedures.rep_ballista(b, this); };

    //            _buildingUniqueName = "ballista";
    //        }

    //        /// <summary>
    //        /// An embelishment added to the ballista tower when it achieves the veteran status
    //        /// </summary>
    //        public GameObject veteranModel;
    //        /// <summary>
    //        /// The main model of the Ballista Tower
    //        /// </summary>
    //        public GameObject baseModel;
    //        /// <summary>
    //        /// The base of the rotational top half of the ballista
    //        /// </summary>
    //        public GameObject topBase;
    //        /// <summary>
    //        /// The right side arm used to animate the ballista's firing movement
    //        /// </summary>
    //        public GameObject armR;
    //        /// <summary>
    //        /// The right end of the right arm of the ballista; used for anchoring the right side of the string in animation
    //        /// </summary>
    //        public Transform armREnd;
    //        /// <summary>
    //        /// The left side arm used to animate the ballista's firing movement
    //        /// </summary>
    //        public GameObject armL;
    //        /// <summary>
    //        /// The lef end of the left arm of the ballista; used for anchoring the left side of the string in animation
    //        /// </summary>
    //        public Transform armLEnd;
    //        /// <summary>
    //        /// The right side of the animated string used to pull back and fire the ballista projectile
    //        /// </summary>
    //        public GameObject stringR;
    //        /// <summary>
    //        /// The left side of the animated string used to pull back and fire the ballista projectile
    //        /// </summary>
    //        public GameObject stringL;
    //        /// <summary>
    //        /// The projectile fired from the ballista
    //        /// </summary>
    //        public GameObject projectile;
    //        public Transform projectileEnd;
    //        /// <summary>
    //        /// A decorative flag on the ballista
    //        /// </summary>
    //        public GameObject flag;
    //    }



    //#endregion

    //    #region Town



    //    #endregion

    //    #region Advanced Town

    //    //Hospital
    //    public class HospitalBuildingSkin : GenericBuildingSkin
    //    {
    //        public HospitalBuildingSkin()
    //        {
    //            _buildingUniqueName = "hospital";
    //        }
    //    }

    //    #endregion

    //    #region Food

    //    public class MarketBuildingSkin : BuildingSkin
    //    {
    //        public MarketBuildingSkin()
    //        {
    //            _removalProcedure = Procedures.rp_market;
    //            _replaceProcedure = delegate (Building b) { Procedures.rep_market(b, this); };

    //            _buildingUniqueName = "market";
    //        }

    //        /// <summary>
    //        /// The gameobject that will replace the base model; will be instantiated at runtime
    //        /// </summary>
    //        public GameObject model;
    //        /// <summary>
    //        /// Optional; creates visual stacks of resources at the specified positions, does not affect actual resource consumption or productions, 
    //        /// but should match the max storage of the building to make it visually accurate. 
    //        /// If left null this field will be set to its default value; 
    //        /// </summary>
    //        public ResourceStacks resourceStacks = null;
    //        /// <summary>
    //        /// Optional; the place resources will be left in most scenarios the resourceStacks are full; 
    //        /// If left null this field will be set to its default value; 
    //        /// </summary>
    //        public Transform resourceDropoff = null;
    //        /// <summary>
    //        /// Optional; the positions peasants stand while working at the building; 
    //        /// If left null this field will be set to its default value; 
    //        /// </summary>
    //        public Transform[] personPositions = null;
    //    }




    //    #endregion

    //    #region Path-Types

    //    #region Generic

    //    public class PathTypeBuildingSkin : BuildingSkin
    //    {
    //        public PathTypeBuildingSkin()
    //        {
    //            _removalProcedure = delegate (Building b) { Procedures.rp_path(b); };
    //            _replaceProcedure = delegate (Building b) { Procedures.rep_path(b, this); };
    //        }

    //        public GameObject Straight;
    //        public GameObject Elbow;
    //        public GameObject Threeway;
    //        public GameObject Fourway;
    //    }

    //    #endregion

    //    #region Roads

    //    public class RoadBuildingSkin : PathTypeBuildingSkin
    //    {
    //        public RoadBuildingSkin()
    //        {
    //            _buildingUniqueName = "road";
    //        }
    //    }

    //    public class StoneRoadBuildingSkin : PathTypeBuildingSkin
    //    {
    //        public StoneRoadBuildingSkin()
    //        {
    //            _buildingUniqueName = "stoneroad";
    //        }
    //    }

    //    #endregion

    //    #region Bridges

    //    public class WoodenBridgeBuildingSkin : PathTypeBuildingSkin
    //    {
    //        public WoodenBridgeBuildingSkin()
    //        {
    //            _buildingUniqueName = "bridge";
    //        }
    //    }

    //    public class StoneBridgeBuildingSkin : PathTypeBuildingSkin
    //    {
    //        public StoneBridgeBuildingSkin()
    //        {
    //            _buildingUniqueName = "stonebridge";
    //        }
    //    }

    //    #endregion

    //    #region Garden

    //    public class GardenBuildingSkin : BuildingSkin
    //    {
    //        public GardenBuildingSkin()
    //        {
    //            _removalProcedure = delegate (Building b) { Procedures.rp_garden(b); };
    //            _replaceProcedure = delegate (Building b) { Procedures.rep_garden(b, this); };

    //            _buildingUniqueName = "garden";
    //        }

    //        public GameObject Straight;
    //        public GameObject Elbow;
    //        public GameObject Threeway;
    //        public GameObject Fourway;
    //        public GameObject Fourway_Special;

    //        public Mesh Straight_flowers;
    //        public Mesh Elbow_flowers;
    //        public Mesh Threeway_flowers;
    //        public Mesh Fourway_flowers;
    //        public Mesh Fourway_Special_flowers;
    //    }


    //    #endregion

    //    #endregion

    //#endregion

}