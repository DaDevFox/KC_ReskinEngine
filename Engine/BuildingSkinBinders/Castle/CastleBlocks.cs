using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BuildingFramework.Reskin.Engine
{
    class CastleBlockSkinBinderBase : BuildingSkinBinder
    {
        public override string UniqueName => "";

        public override SkinBinder Create(GameObject obj)
        {
            throw new NotImplementedException();
        }
    }

    class StoneCastleBlockSkinBinder : CastleBlockSkinBinderBase
    {
        public override string UniqueName => "castleblock";



    }

    class WoodCastleBlockSkinBinder : CastleBlockSkinBinderBase
    {
        public override string UniqueName => "woodcastleblock";



    }

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
    }


}
