using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//using ReskinEngine.Examples.ExampleMod;

namespace ReskinEngine.API
{
    #region Base

    #region Category Definitions

    #region Base

    /// <summary>
    /// A category with a string id that any skin can be a part of; used for documentation
    /// </summary>
    public abstract class Category
    {
        /// <summary>
        /// ID skins can use to refer to this
        /// </summary>
        public abstract string id { get; }
        /// <summary>
        /// Name shown in documentation; default is id with first letter uppercase
        /// </summary>
        public virtual string name
        {
            get
            {
                if (id.Length == 0)
                    return "";
                else if (id.Length == 1)
                    return char.ToUpper(id[0]).ToString();
                else
                    return char.ToUpper(id[0]) + id.Substring(1);
            }
        }
        /// <summary>
        /// Order of this category in skins list
        /// </summary>
        public virtual int position { get; } = -1;
    }

    #endregion

    #region Skins

    public class EnvironmentCategory : Category
    {
        public override string id => "environment";
        public override int position => 0;
    }

    public class GenericSkinsCategory : Category
    {
        public override string id => "generic";
        public override string name => "Generic Skins";
        public override int position => 1;
    }

    #endregion

    #region Buildings

    public class CastleCategory : Category
    {
        public override string id => "castle";
        public override int position => 3;
    }

    public class TownCategory : Category
    {
        public override string id => "town";
        public override int position => 4;
    }

    public class AdvTownCategory : Category
    {
        public override string id => "advTown";
        public override string name => "Advanced Town";
        public override int position => 5;
    }

    public class FoodCategory : Category
    {
        public override string id => "food";
        public override int position => 6;
    }

    public class IndustryCategory : Category
    {
        public override string id => "industry";
        public override int position => 7;
    }

    public class MaritimeCategory : Category
    {
        public override string id => "maritime";
        public override int position => 8;
    }

    public class CemetaryCategory : Category
    {
        public override string id => "cemetary";
        public override string name => "Cemetaries";
        public override int position => 9;
    }




    #endregion

    #endregion

    #region Attributes

    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class CategoryAttribute : Attribute
    {
        public string category = "none";

        public CategoryAttribute(string category) => this.category = category;
    }


    /// <summary>
    /// Signifies a skin that shouldn't be used
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class HiddenAttribute : Attribute
    {
    }

    /// <summary>
    /// Signifies a Skin that isn't supported on the engine side at the current time
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class NotSupportedAttribute : Attribute
    {

    }

    /// <summary>
    /// Represents a material field that can be changed
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class MaterialAttribute : Attribute
    {
        internal string name;
        internal string description;
        public bool seperator;
        public MaterialAttribute(string description) => this.description = description;
    }

    /// <summary>
    /// Represents a material that cannot be changed
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class PresetMaterialAttribute : Attribute
    {
        public string name;
        public PresetMaterialAttribute(string name) => this.name = name;
    }

    /// <summary>
    /// Signifies a field in a skin that is a model and contains data about its type and description
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class ModelAttribute : Attribute
    {
        public enum Type
        {
            Instance,
            Modular
        }

        public Type type;

        public string name;
        public string description;
        
        internal string presetMatName;
        internal bool seperator;

        public ModelAttribute()
        {
            this.type = Type.Instance;
        }

        public ModelAttribute(Type type)
        {
            this.type = type;
        }
    }

    /// <summary>
    /// Represents a Vector3 field that represents a transform's position
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class AnchorAttribute : Attribute
    {
        public string description;
        public string name;
        internal bool seperator;

        public AnchorAttribute()
        { 
        }

        public AnchorAttribute(string description) => this.description = description;
    }

    /// <summary>
    /// Use this on buildings with one or more job
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class JobsAttribute : Attribute
    {
        public int count = 1;

        public JobsAttribute()
        {

        }

        public JobsAttribute(int count) => this.count = count;
    }

    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class NoteAttribute : Attribute
    {
        public string description;
        public NoteAttribute(string description) => this.description = description;
    }


    /// <summary>
    /// Adds a seperator above this field
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class SeperatorAttribute : Attribute
    {
    }

    #endregion

    #region Skin Definitions

    //Base
    /// <summary>
    /// A Skin object holds fields that pertain to the skin's data, such as models, banners, peasant work positions, etc. 
    /// <para>It does not contain any functionality on its own to actually use this data; that is handled on the Engine side, it's simply a container that sends data to the Engine</para>
    /// </summary>
    [Hidden]
    public abstract class Skin
    {
        public ReskinProfile ReskinProfile { get; internal set; }
        public int Identifier { get; internal set; }
        internal virtual string TypeIdentifier { get; }
        public abstract string Name { get; }

        /// <summary>
        /// Adds GameObject children to the target with specifications per skin as a method of communication between the client API and the Engine
        /// <para>For advanced uses only</para>
        /// </summary>
        /// <param name="target"></param>
        public void Package(Transform target)
        {
            GameObject _base = GameObject.Instantiate(new GameObject(), target);
            _base.name =
                ReskinProfile.CompatabilityIdentifier +
                ":" +
                ReskinProfile.ModName +
                ":" +
                TypeIdentifier +
                ":" +
                Identifier.ToString();

            PackageInternal(target, _base);
        }

        /// <summary>
        /// Package data pertaining to this skin in GameObject form
        /// <para>Will eveuntually be read by a SkinBinder Engine-side</para>
        /// </summary>
        /// <param name="dropoff">dropoff target object</param>
        /// <param name="_base">skin target object</param>
        protected virtual void PackageInternal(Transform dropoff, GameObject _base)
        {

        }

        #region Utils

        /// <summary>
        /// Packages and appends a model to the base object during the packaging phase
        /// </summary>
        /// <param name="_base"></param>
        /// <param name="model"></param>
        /// <param name="name"></param>
        protected void AppendModel(GameObject _base, GameObject model, string name)
        {
            if (model)
                GameObject.Instantiate(model, _base.transform).name = name;
        }

        /// <summary>
        /// Attaches a material to a model and appends a material flag during the packaging phase
        /// </summary>
        /// <param name="_base"></param>
        /// <param name="model"></param>
        /// <param name="modelName"></param>
        /// <param name="material"></param>
        /// <param name="name"></param>
        protected void AppendMaterial(GameObject _base, GameObject model, string modelName, Material material, string name)
        {
            if (model)
            {
                if (material)
                {
                    // set the models material to the material referenced above
                    (_base.transform.Find(modelName).GetComponent<MeshRenderer>() ?? _base.transform.Find(modelName).gameObject.AddComponent<MeshRenderer>()).material = material;
                    // leave a flag in case engine needs to know material was changed
                    (new GameObject($"{name}Flag")).transform.SetParent(_base.transform);
                }
            }

        }

        /// <summary>
        /// Appends a standalone material to a packaging GameObject
        /// </summary>
        /// <param name="_base"></param>
        /// <param name="mat"></param>
        /// <param name="name"></param>
        protected void AppendMaterial(GameObject _base, Material mat, string name)
        {
            GameObject material = new GameObject(name);
            material.transform.SetParent(_base.transform);
            material.AddComponent<MeshFilter>();
            material.AddComponent<MeshRenderer>().material = mat;
        }

        #endregion
    }

    [Hidden]
    public abstract class BuildingSkin : Skin
    {
        public sealed override string Name => FriendlyName;

        /// <summary>
        /// Name of building that shows in build menu; used for documentation
        /// </summary>
        internal virtual string FriendlyName { get; } = "No Name";
        internal virtual string UniqueName { get; } = "No UniqueName";

        internal sealed override string TypeIdentifier => $"building_{UniqueName}";

        /// <summary>
        /// Optional; the positions peasants stand at while working at the building; directly corresponds to number of jobs a building employs
        /// <para>If left null this field will be set to its default value;</para>
        /// </summary>
        public Vector3[] personPositions = new Vector3[0];
        ///<summary>
        /// Paths relative to the building root to all the meshes that will be included in the outline effect when selecting the building (each item in list requires MeshRenderer component)
        /// </summary>
        public string[] outlineMeshes;
        /// <summary>
        /// Paths relative to the building root to all the skinned mesh renderers that will be included in the outline effect when selecting the building (each item in list requires SkinnedMeshRenderer component)
        /// </summary>
        public string[] outlineSkinnedMeshes;


        protected override void PackageInternal(Transform dropoff, GameObject _base)
        {
            base.PackageInternal(dropoff, _base);


            if (personPositions.Length > 0)
            {
                GameObject obj = new GameObject("personPositions");
                obj.transform.SetParent(_base.transform);

                for (int i = 0; i < personPositions.Length; i++)
                {
                    Vector3 personPosition = personPositions[i];
                    GameObject position = new GameObject($"personPosition{i}");
                    position.transform.SetParent(obj.transform);
                    position.transform.position = personPosition;
                }
            }

            if(outlineMeshes != null && outlineMeshes.Length > 0)
            {
                string list = "";
                foreach (string path in outlineMeshes)
                    list += path + ",";
                
                string name = $"outlineMeshes:{list}";

                GameObject obj = new GameObject(name);
                obj.transform.SetParent(_base.transform);
            }

            if (outlineSkinnedMeshes != null && outlineSkinnedMeshes.Length > 0)
            {
                string list = "";
                foreach (string path in outlineSkinnedMeshes)
                    list += path + ",";

                string name = $"outlineSkinnedMeshes:{list}";

                GameObject obj = new GameObject(name);
                obj.transform.SetParent(_base.transform);
            }
        }
    }

    //Generic
    /// <summary>
    /// Don't use as a skin
    /// <para>Type of skin that can be used for most buildings in the game</para>
    /// </summary>
    [Hidden]
    public abstract class GenericBuildingSkin : BuildingSkin
    {
        /// <summary>
        /// The base model to be inserted
        /// </summary>
        [Model(description = "The base model that will replace the building")]
        public GameObject baseModel;

        protected override void PackageInternal(Transform dropoff, GameObject _base)
        {
            //Mod.helper.Log($"packaging generic skin for {UniqueName}");

            base.PackageInternal(dropoff, _base);

            //Mod.helper.Log("2");

            if (baseModel)
                GameObject.Instantiate(baseModel, _base.transform).name = "baseModel";

            //Mod.helper.Log($"packaged generic skin for {UniqueName}");
        }
    }

    #endregion

    #endregion


    //#region Town



    //#endregion

    //#region Advanced Town

    ////Hospital
    //public class HospitalSkin : GenericBuildingSkin
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
