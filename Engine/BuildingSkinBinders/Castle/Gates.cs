using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BuildingFramework.Reskin.Engine
{
    public class GateSkinBinderBase : BuildingSkinBinder
    {
        public override string UniqueName => "unregistered";

        public GameObject gate;
        public GameObject porticulus;

        public override SkinBinder Create(GameObject obj)
        {
            GateSkinBinderBase inst = new GateSkinBinderBase();

            if (obj.transform.Find("gate"))
                inst.gate = obj.transform.Find("gate").gameObject;
            if (obj.transform.Find("porticulus"))
                inst.porticulus = obj.transform.Find("porticulus").gameObject;

            return inst;
        }
    }

    public class StoneGateSkinBinder : GateSkinBinderBase
    {
        public override string UniqueName => "gate";
    }

    public class WoodGateSkinBinder : GateSkinBinderBase
    {
        public override string UniqueName => "woodengate";
    }

}
