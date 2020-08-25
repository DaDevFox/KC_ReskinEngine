using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReskinEngine.Engine
{
    [Unregistered]
    public class PathSkinBinderBase : BuildingSkinBinder
    {
        public override string UniqueName => "road";

        public GameObject straight;
        public GameObject elbow;
        public GameObject intersection3;
        public GameObject intersection4;


        public override SkinBinder Create(GameObject obj)
        {
            var inst = new PathSkinBinderBase();

            if (obj.transform.Find("straight"))
                inst.straight = obj.transform.Find("straight").gameObject;
            if (obj.transform.Find("elbow"))
                inst.elbow = obj.transform.Find("elbow").gameObject;
            if (obj.transform.Find("intersection3"))
                inst.intersection3 = obj.transform.Find("intersection3").gameObject;
            if (obj.transform.Find("intersection4"))
                inst.intersection4 = obj.transform.Find("intersection4").gameObject;

            return inst;
        }

        public override void BindToBuildingBase(Building building)
        {
            Road r = building.GetComponent<Road>();

            if (straight)
                r.Straight.GetComponent<MeshFilter>().mesh = straight.GetComponent<MeshFilter>().mesh;
            if (elbow)
                r.Elbow.GetComponent<MeshFilter>().mesh = elbow.GetComponent<MeshFilter>().mesh;
            if (intersection3)
                r.Intersection3.GetComponent<MeshFilter>().mesh = intersection3.GetComponent<MeshFilter>().mesh;
            if (intersection4)
                r.Intersection4.GetComponent<MeshFilter>().mesh = intersection4.GetComponent<MeshFilter>().mesh;
        }

        public override void BindToBuildingInstance(Building building)
        {
            this.BindToBuildingBase(building);
        }
    }

    public class RoadBuildingSkinBinder : PathSkinBinderBase
    {
        public override string UniqueName => "road";
    }

    public class StoneRoadBuildingSkinBinder : PathSkinBinderBase
    {
        public override string UniqueName => "stoneroad";
    }

    public class BridgeBuildingSkinBinder : PathSkinBinderBase
    {
        public override string UniqueName => "bridge";
    }

    public class StoneBridgeBuildingSkinBinder : PathSkinBinderBase
    {
        public override string UniqueName => "stonebridge";
    }

}
