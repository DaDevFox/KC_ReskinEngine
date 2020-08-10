using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReskinEngine.Engine
{
    #region Castle Blocks

    // Base
    public class CastleBlockSkinBinderBase : BuildingSkinBinder
    {
        public override string UniqueName => "";

        public GameObject Open;
        public GameObject Closed;
        public GameObject Single;
        public GameObject Opposite;
        public GameObject Adjacent;
        public GameObject Threeside;

        public GameObject doorPrefab;

        public override SkinBinder Create(GameObject obj)
        {
            var inst = new CastleBlockSkinBinderBase();

            if (obj.transform.Find("Open"))
                inst.Open = obj.transform.Find("Open").gameObject;
            if (obj.transform.Find("Closed"))
                inst.Closed = obj.transform.Find("Closed").gameObject;
            if (obj.transform.Find("Single"))
                inst.Single = obj.transform.Find("Single").gameObject;
            if (obj.transform.Find("Opposite"))
                inst.Opposite = obj.transform.Find("Opposite").gameObject;
            if (obj.transform.Find("Adjacent"))
                inst.Adjacent = obj.transform.Find("Adjacent").gameObject;
            if (obj.transform.Find("Threeside"))
                inst.Threeside = obj.transform.Find("Threeside").gameObject;

            if (obj.transform.Find("doorPrefab"))
                inst.doorPrefab = obj.transform.Find("doorPrefab").gameObject;

            return inst;
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

    class CastleStairsSkinBinder : BuildingSkinBinder
    {
        public override string UniqueName => "castlestairs";

        public GameObject stairsFront;
        public GameObject stairsRight;
        public GameObject stairsDown;
        public GameObject stairsLeft;




        public override SkinBinder Create(GameObject obj)
        {
            CastleStairsSkinBinder inst = new CastleStairsSkinBinder();

            if (obj.transform.Find("stairsFront"))
                inst.stairsFront = obj.transform.Find("stairsFront").gameObject;
            if (obj.transform.Find("stairsRight"))
                inst.stairsRight = obj.transform.Find("stairsRight").gameObject;
            if (obj.transform.Find("stairsDown"))
                inst.stairsDown = obj.transform.Find("stairsDown").gameObject;
            if (obj.transform.Find("stairsLeft"))
                inst.stairsLeft = obj.transform.Find("stairsLeft").gameObject;

            return inst;
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
        }

    }

    #endregion

    #region Gates

    // Base
    public class GateSkinBinderBase : BuildingSkinBinder
    {
        public override string UniqueName => "";

        public GameObject _gate;
        public GameObject _porticulus;

        public override SkinBinder Create(GameObject obj)
        {
            var inst = new GateSkinBinderBase();

            if (obj.transform.Find("gate"))
                inst._gate = obj.transform.Find("gate").gameObject;

            if (obj.transform.Find("porticulus"))
                inst._porticulus = obj.transform.Find("porticulus").gameObject;

            return inst;
        }

        public override void BindToBuildingBase(Building building)
        {
            GameObject gateObj = building.transform.Find("Offset/Gate").gameObject;
            GameObject portculusObj = building.transform.Find("Offset/Portculus").gameObject;
            
            if (_gate)
            {
                gateObj.GetComponent<MeshFilter>().mesh = null;
                GameObject.Instantiate(_gate, gateObj.transform);
            }

            if(_porticulus)
            {
                porticulus.GetComponent<MeshFilter>().mesh = null;
                GameObject.Instantiate(_porticulus, portculusObj.transform);
            }
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
