using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReskinEngine.Engine
{
    public class ChurchSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "church";

        public override void Read(GameObject obj) => base.Read<ChurchSkinBinder>(obj);
    }

    public class CathedralSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "cathedral";
        public override void Read(GameObject obj) => base.Read<CathedralSkinBinder>(obj);
    }
}
