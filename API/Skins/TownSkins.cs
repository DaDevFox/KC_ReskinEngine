using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReskinEngine.API
{
    [Category("town")]
    [Hidden]
    public class PathSkinBase : BuildingSkin
    {
        [Model(ModelAttribute.Type.Modular, description = "The straight segment ")]
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

    public class RoadSkin : PathSkinBase
    {
        internal override string FriendlyName => "Road";
        internal override string UniqueName => "road";
    }

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
}
