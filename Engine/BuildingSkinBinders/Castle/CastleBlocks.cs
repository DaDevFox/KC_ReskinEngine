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

        public override void Read(GameObject _base)
        {
            base.Read(_base);

            if (_base.transform.Find("Open"))
                Open = _base.transform.Find("Open").gameObject;
            if (_base.transform.Find("Closed"))
                Closed = _base.transform.Find("Closed").gameObject;
            if (_base.transform.Find("Single"))
                Single = _base.transform.Find("Single").gameObject;
            if (_base.transform.Find("Opposite"))
                Opposite = _base.transform.Find("Opposite").gameObject;
            if (_base.transform.Find("Adjacent"))
                Adjacent = _base.transform.Find("Adjacent").gameObject;
            if (_base.transform.Find("Threeside"))
                Threeside = _base.transform.Find("Threeside").gameObject;

            if (_base.transform.Find("doorPrefab"))
                doorPrefab = _base.transform.Find("doorPrefab").gameObject;

            ReadMaterial(_base, "material");

            Engine.dLog($"loaded: open {Open.GetComponent<MeshFilter>()}, closed {Closed}, single {Single}, opposite {Opposite}, adjacent {Adjacent}, threeside {Threeside}");
        }

        public override void BindToBuildingBase(Building building)
        {
            CastleBlock block = building.GetComponent<CastleBlock>();

            if (Open && Open.GetComponent<MeshFilter>())
            {
                MeshFilter mesh = block.Open.GetComponent<MeshFilter>();;
                mesh.sharedMesh = Open.GetComponent<MeshFilter>().sharedMesh;
                block.Open.GetComponent<MeshRenderer>().material = material ?? World.GetUniMaterialFor(building.LandMass());
            }
            if (Closed && Closed.GetComponent<MeshFilter>())
            {
                MeshFilter mesh = block.Closed.GetComponent<MeshFilter>(); ;
                mesh.sharedMesh = Closed.GetComponent<MeshFilter>().sharedMesh;
                block.Closed.GetComponent<MeshRenderer>().material = material ?? World.GetUniMaterialFor(building.LandMass());
            }
            if (Single && Single.GetComponent<MeshFilter>())
            {
                MeshFilter mesh = block.Single.GetComponent<MeshFilter>(); ;
                mesh.sharedMesh = Single.GetComponent<MeshFilter>().sharedMesh;
                block.Single.GetComponent<MeshRenderer>().material = material ?? World.GetUniMaterialFor(building.LandMass());
            }
            if (Opposite && Opposite.GetComponent<MeshFilter>())
            {
                MeshFilter mesh = block.Opposite.GetComponent<MeshFilter>(); ;
                mesh.sharedMesh = Opposite.GetComponent<MeshFilter>().sharedMesh;
                block.Opposite.GetComponent<MeshRenderer>().material = material ?? World.GetUniMaterialFor(building.LandMass());
            }
            if (Adjacent && Adjacent.GetComponent<MeshFilter>())
            {
                MeshFilter mesh = block.Adjacent.GetComponent<MeshFilter>(); ;
                mesh.sharedMesh = Adjacent.GetComponent<MeshFilter>().sharedMesh;
                block.Adjacent.GetComponent<MeshRenderer>().material = material ?? World.GetUniMaterialFor(building.LandMass());
            }
            if (Threeside && Threeside.GetComponent<MeshFilter>())
            {
                MeshFilter mesh = block.Threeside.GetComponent<MeshFilter>(); ;
                mesh.sharedMesh = Threeside.GetComponent<MeshFilter>().sharedMesh;
                block.Threeside.GetComponent<MeshRenderer>().material = material ?? World.GetUniMaterialFor(building.LandMass());
            }

            if(doorPrefab)
                block.doorPrefab = doorPrefab;

            block.transform.Find("Offset").GetChild(0).GetComponent<MeshRenderer>().material = World.GetUniMaterialFor(building.LandMass());

            //if (material)
            //    block.transform.Find("Offset").GetChild(0).GetComponent<MeshRenderer>().material = material;
            //else
            //    block.transform.Find("Offset").GetChild(0).GetComponent<MeshRenderer>().material = World.GetUniMaterialFor(building.LandMass());

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
