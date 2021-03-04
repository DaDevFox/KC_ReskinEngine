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

        public override SkinBinder Create(GameObject obj) => base.Create<ChurchSkinBinder>(obj);
    }

    public class CathedralSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "cathedral";
        public override SkinBinder Create(GameObject obj) => base.Create<CathedralSkinBinder>(obj);
    }
}
