using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;

namespace ReskinEngine.Engine
{
    #region Castle Blocks

    // Base
    [Unregistered]
    public class CastleBlockSkinBinderBase : BuildingSkinBinder
    {
        public override string UniqueName => "castleblock";

        public GameObject Open;
        public GameObject Closed;
        public GameObject Single;
        public GameObject Opposite;
        public GameObject Adjacent;
        public GameObject Threeside;

        public GameObject doorPrefab;

        public Material material;

        public override void Read(GameObject obj)
        {
            base.Read(obj);

            if (obj.transform.Find("Open"))
                Open = obj.transform.Find("Open").gameObject;
            if (obj.transform.Find("Closed"))
                Closed = obj.transform.Find("Closed").gameObject;
            if (obj.transform.Find("Single"))
                Single = obj.transform.Find("Single").gameObject;
            if (obj.transform.Find("Opposite"))
                Opposite = obj.transform.Find("Opposite").gameObject;
            if (obj.transform.Find("Adjacent"))
                Adjacent = obj.transform.Find("Adjacent").gameObject;
            if (obj.transform.Find("Threeside"))
                Threeside = obj.transform.Find("Threeside").gameObject;

            if (obj.transform.Find("doorPrefab"))
                doorPrefab = obj.transform.Find("doorPrefab").gameObject;

            ReadMaterial(obj, "material");
        }

        public override void BindToBuildingBase(Building building)
        {
            

            CastleBlock block = building.GetComponent<CastleBlock>();

            if(Open)
                block.Open = Open.transform;
            if(Closed)
                block.Closed = Closed.transform;
            if(Single)
                block.Single = Single.transform;
            if(Opposite)
                block.Opposite = Opposite.transform;
            if(Adjacent)
                block.Adjacent = Adjacent.transform;
            if(Threeside)
                block.Threeside = Threeside.transform;

            if(doorPrefab)
                block.doorPrefab = doorPrefab;

            if (material)
                block.transform.Find("Offset").GetChild(0).GetComponent<MeshRenderer>().material = material;

            base.BindToBuildingBase(building);
        }

        public override void BindToBuildingInstance(Building building)
        {
            this.BindToBuildingBase(building);
        }
    }

    // Stone
    public class StoneCastleBlockSkinBinder : CastleBlockSkinBinderBase
    {
        public override string UniqueName => "castleblock";
    }

    // Wood
    public class WoodCastleBlockSkinBinder : CastleBlockSkinBinderBase
    {
        public override string UniqueName => "woodcastleblock";
    }

    #endregion

    #region Castle Stairs
    
    public class CastleStairsSkinBinder : BuildingSkinBinder
    {
        public override string UniqueName => "castlestairs";

        public GameObject stairsFront;
        public GameObject stairsRight;
        public GameObject stairsDown;
        public GameObject stairsLeft;




        public override void Read(GameObject obj)
        {
            base.Read(obj);

            if (obj.transform.Find("stairsFront"))
                stairsFront = obj.transform.Find("stairsFront").gameObject;
            if (obj.transform.Find("stairsRight"))
                stairsRight = obj.transform.Find("stairsRight").gameObject;
            if (obj.transform.Find("stairsDown"))
                stairsDown = obj.transform.Find("stairsDown").gameObject;
            if (obj.transform.Find("stairsLeft"))
                stairsLeft = obj.transform.Find("stairsLeft").gameObject;

        }

        public override void BindToBuildingBase(Building building)
        {

            CastleStairs stairs = building.GetComponent<CastleStairs>();

            if (stairsRight)
                stairs.east = stairsRight;
            if (stairsLeft)
                stairs.west = stairsLeft;
            if (stairsFront)
                stairs.north = stairsFront;
            if (stairsDown)
                stairs.south = stairsDown;

            base.BindToBuildingBase(building);
        }

    }

    #endregion

    #region Gates

    // Base
    [Unregistered]
    public class GateSkinBinderBase : BuildingSkinBinder
    {
        public override string UniqueName => "gate";

        public GameObject gate;
        public GameObject porticulus;

        public override void Read(GameObject obj)
        {
            base.Read(obj);

            ReadModel(obj, "gate");
            ReadModel(obj, "porticulus");
        }

        public override void BindToBuildingBase(Building building)
        {

            GameObject gateObj = building.transform.Find("Offset/Gate").gameObject;
            GameObject portculusObj = building.transform.Find("Offset/Portculus").gameObject;
            
            if (gate)
            {
                gateObj.GetComponent<MeshFilter>().mesh = null;
                GameObject.Instantiate(gate, gateObj.transform);
            }

            if(porticulus)
            {
                porticulus.GetComponent<MeshFilter>().mesh = null;
                GameObject.Instantiate(porticulus, portculusObj.transform);
            }


            base.BindToBuildingBase(building);
        }

        public override void BindToBuildingInstance(Building building)
        {
            this.BindToBuildingBase(building);
        }
    }

    // Wood
    public class WoodenGateSkinBinder : GateSkinBinderBase
    {
        public override string UniqueName => "woodengate";
    }

    // Stone
    public class StoneGateSkinBinder : GateSkinBinderBase
    {
        public override string UniqueName => "gate";
    }


    #endregion

}
