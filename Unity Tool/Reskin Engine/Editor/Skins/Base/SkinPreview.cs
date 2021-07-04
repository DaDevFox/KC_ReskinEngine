using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using ReskinEngine.Utils;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public abstract class SkinPreview
    {
        public static string KCAssetsRoot { get; } = "Reskin Engine/KCAssets";
        public static string modelsRoot { get; } = $"{KCAssetsRoot}/Models/GameModels (and some prefabs)";
        public static string environmentRoot { get; } = $"{KCAssetsRoot}/Environment";
        public static string spritesRoot { get; } = $"{KCAssetsRoot}/Sprites";

        public abstract Type SkinType { get; }

        /// <summary>
        /// Using the GameObject packaged by the API-side, this method create a new SkinBinder with the fields that correspond to the GameObjects children assigned
        /// </summary>
        public abstract GameObject Create();

        /// <summary>
        /// Applies the skin to a gameobject
        /// </summary>
        public virtual void Apply(GameObject obj, Skin skin)
        {

        }

        /// <summary>
        /// Called during OnInspectorGUI from the editor; use to add additional controls
        /// </summary>
        public virtual void UI()
        {

        }
    }

    public abstract class SkinPreview<T> : SkinPreview where T : Skin
    {
        public override Type SkinType { get; } = typeof(T);


        public override void Apply(GameObject obj, Skin skin)
        {
            base.Apply(obj, skin);
            Apply(obj, skin as T);
        }

        public virtual void Apply(GameObject obj, T skin)
        {
            base.Apply(obj, skin);
        }

        public override void UI()
        {
            base.UI();
        }

        /// <summary>
        /// Avoid this
        /// </summary>
        /// <param name="obj"></param>
        protected void Reset(GameObject obj)
        {
            Transform parent = obj.transform.parent;
            parent.ClearChildren();
            GameObject _new = Create();
            _new.transform.SetParent(parent);
            _new.transform.position = Vector3.zero;
            _new.transform.rotation = Quaternion.identity;
        }
    }

    public abstract class PrefabPreview : SkinPreview
    {
        public abstract override Type SkinType { get; }
        public abstract string path { get; }

        public sealed override GameObject Create()
        {
            return GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(path) ?? Resources.Load<GameObject>(path), null, true);
        }
    }

    public abstract class PrefabPreview<T> : SkinPreview<T> where T : Skin
    {
        public abstract string path { get; }
        
        public sealed override GameObject Create()
        {
            return GameObject.Instantiate(Resources.Load<GameObject>(path));
        }

    }

    public abstract class BuildingPreview<T> : PrefabPreview<T> where T : BuildingSkin
    {
        public static string personPreviewPath = $"{environmentRoot}/Person 1";

        public static string buildingsRoot { get; } = "Reskin Engine/KCAssets/Buildings";

        public abstract override string path { get; }

        public virtual int maxPersonPositions { get; } = 0;
        public Vector3[] personPositions;

        public override void Apply(GameObject obj, T skin)
        {
            base.Apply(obj, skin);

            LineRenderer renderer = obj.GetComponent<LineRenderer>() != null ? obj.GetComponent<LineRenderer>() : obj.AddComponent<LineRenderer>();
            renderer.positionCount = 4;
            renderer.SetPositions(new Vector3[] { Vector3.zero, new Vector3(skin.bounds.x, 0f, 0f), new Vector3(skin.bounds.x, 0f, skin.bounds.z), new Vector3(0f, 0f, skin.bounds.z) });


            renderer.alignment = LineAlignment.View;
            if(renderer.startWidth != 0.1f)
                renderer.startWidth = 0.1f;
            if(renderer.endWidth != 0.1f)
                renderer.endWidth = 0.1f;
            if(renderer.loop != true)
                renderer.loop = true;
            if(renderer.sharedMaterial == null)
                renderer.sharedMaterial = new Material(Shader.Find("Standard"));

            Transform personPositionsContainer = obj.transform.Find("personPositions") ?? new GameObject("personPositions").transform;
            personPositionsContainer.SetParent(obj.transform);
            personPositionsContainer.ClearChildren();

            if(skin.personPositions != null)
                foreach (Vector3 personPosition in skin.personPositions)
                    GameObject.Instantiate(Resources.Load<GameObject>(personPreviewPath), personPosition, Quaternion.identity, personPositionsContainer);
        }

        public override void UI()
        {
            base.UI();
        }

        protected bool CheckModular(GameObject toCheck) => toCheck != null && toCheck.GetComponent<MeshFilter>();

        /// <summary>
        /// Helper function that creates IMGUI for warnings for any modular gameObjects that aren't modular
        /// </summary>
        protected string ModularCheckWarnings(bool warningForNulls, params GameObject[] toCheck)
        {
            string warning = "";
            foreach (GameObject obj in toCheck)
                if (!CheckModular(obj) && obj != null)
                    if(!(obj == null && warningForNulls))
                        warning += $"GameObject {obj.name} not a compatable modular object\n";

            return warning;
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

#endif