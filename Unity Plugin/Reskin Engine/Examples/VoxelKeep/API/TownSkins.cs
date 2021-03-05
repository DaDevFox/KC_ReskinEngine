using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReskinEngine.API
{
    #region Paths

    [Category("generic")]
    [Hidden]
    public class PathSkinBase : BuildingSkin
    {
        [Model(ModelAttribute.Type.Modular, description = "The straight segment")]
        public GameObject straight;

        [Model(ModelAttribute.Type.Modular, description = "The elbow segment")]
        public GameObject elbow;

        [Model(ModelAttribute.Type.Modular, description = "The three way intersection segment")]
        public GameObject intersection3;

        [Model(ModelAttribute.Type.Modular, description = "The four way intersection segment")]
        public GameObject intersection4;

        protected override void PackageInternal(Transform target, GameObject _base)
        {
            base.PackageInternal(target, _base);

            if (straight)
                GameObject.Instantiate(straight, _base.transform).name = "straight";
            if (elbow)
                GameObject.Instantiate(elbow, _base.transform).name = "elbow";
            if (intersection3)
                GameObject.Instantiate(intersection3, _base.transform).name = "intersection3";
            if (intersection4)
                GameObject.Instantiate(intersection4, _base.transform).name = "intersection4";
        }
    }

    [Category("town")]
    public class RoadSkin : PathSkinBase
    {
        internal override string FriendlyName => "Road";
        internal override string UniqueName => "road";
    }

    [Category("town")]
    public class StoneRoadSkin : PathSkinBase
    {
        internal override string FriendlyName => "Stone Road";
        internal override string UniqueName => "stoneroad";
    }

    [Category("maritime")]
    public class BridgeSkin : PathSkinBase
    {
        internal override string FriendlyName => "Bridge";
        internal override string UniqueName => "bridge";
    }

    [Category("maritime")]
    public class StoneBridgeSkin : PathSkinBase
    {
        internal override string FriendlyName => "Stone Bridge";
        internal override string UniqueName => "stonebridge";
    }

    #endregion

    #region Homes

    [Category("town")]
    [Jobs(1)]
    public class HovelSkin : GenericBuildingSkin
    {
        internal override string UniqueName => "smallhouse";
        internal override string FriendlyName => "Hovel";
    }

    [Category("town")]
    [Jobs(2)]
    public class CottageSkin : GenericBuildingSkin
    {
        internal override string UniqueName => "largehouse";
        internal override string FriendlyName => "Cottage";
    }


    [Category("town")]
    [Jobs(4)]
    public class ManorHouseSkin : GenericBuildingSkin
    {
        internal override string UniqueName => "manorhouse";
        internal override string FriendlyName => "Manor House";
    }


    #endregion

    [Category("town")]
    public class WellSkin : GenericBuildingSkin
    {
        internal override string UniqueName => "well";
        internal override string FriendlyName => "Well";
    }

    [Category("town")]
    [Jobs(1)]
    public class TownSquareSkin : BuildingSkin
    {
        internal override string UniqueName => "townsquare";
        internal override string FriendlyName => "Town Square";

        [Model(description = "Base town square floor and flag pole")]
        public GameObject baseModel;
        [Model(description = "GameObject whose children will be the items turned on and off for festivals")]
        public GameObject festivalContainer;
        [Model(description = "GameObject that is turned on or off for halloween (yes it exists off-season)")]
        public GameObject halloweenContainer;
        
        [Seperator]
        [Model(description = "Flag on flagpole")]
        public GameObject flag;

        [Anchor("Position of the flag")]
        public Vector3 flagPosition;

        protected override void PackageInternal(Transform target, GameObject _base)
        {
            base.PackageInternal(target, _base);

            if (baseModel)
                GameObject.Instantiate(baseModel, target).name = "baseModel";
            if (festivalContainer)
                GameObject.Instantiate(festivalContainer, target).name = "festivalContainer";
            if (halloweenContainer)
                GameObject.Instantiate(halloweenContainer, target).name = "halloweenContainer";
            if (flag)
                GameObject.Instantiate(flag, target).name = "flag";

            Transform positionObj = new GameObject("flagPosition").transform;
            positionObj.SetParent(target);
            positionObj.localPosition = flagPosition;

        }
    }

    [Category("town")]
    [Jobs(4)]
    public class TavernSkin : GenericBuildingSkin
    {
        internal override string UniqueName => "tavern";
        internal override string FriendlyName => "Tavern";
    }

    [Category("town")]
    [Jobs(5)]
    public class FireBrigadeSkin : GenericBuildingSkin
    {
        internal override string UniqueName => "firehouse";
        internal override string FriendlyName => "Fire Brigade";
    }
}
