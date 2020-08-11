using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReskinEngine.API
{
    #region Base

    #region Attributes

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
    public class Skin
    {
        public ReskinProfile ReskinProfile { get; internal set; }
        public int Identifier { get; internal set; }
        internal virtual string TypeIdentifier { get; }

        /// <summary>
        /// Adds GameObject children to the target with specifications per skin as a method of communication between the client API and the Engine
        /// <para>For advanced users only</para>
        /// </summary>
        /// <param name="target"></param>
        public void Package(Transform target)
        {
            GameObject _base = GameObject.Instantiate(new GameObject(), target);
            _base.name =
                ReskinProfile.CompatabilityIdentifier +
                ":" +
                ReskinProfile.CollectionName +
                ":" +
                TypeIdentifier +
                ":" +
                Identifier.ToString();

            PackageInternal(target, _base);
        }

        protected virtual void PackageInternal(Transform target, GameObject _base)
        {

        }
    }

    public class BuildingSkin : Skin
    {
        internal virtual string FriendlyName { get; } = "No Name";
        internal virtual string UniqueName { get; } = "No UniqueName";

        internal sealed override string TypeIdentifier => $"building_{UniqueName}";

        /// <summary>
        /// Optional; the positions peasants stand at while working at the building; 
        /// If left null this field will be set to its default value; 
        /// </summary>
        public Transform[] personPositions = null;


    }

    //Generic
    /// <summary>
    /// Don't use as a skin
    /// </summary>
    [Hidden]
    public class GenericBuildingSkin : BuildingSkin
    {
        /// <summary>
        /// The base model to be inserted
        /// </summary>
        [Model(description = "The base model to be inserted")]
        public GameObject model;

        protected override void PackageInternal(Transform target, GameObject _base)
        {
            base.PackageInternal(target, _base);
        }
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

    #endregion

}
