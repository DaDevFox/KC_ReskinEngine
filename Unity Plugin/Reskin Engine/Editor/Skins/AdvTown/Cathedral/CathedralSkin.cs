using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public class CathedralSkin : GenericBuildingSkin
    {
        public override string UniqueName => "cathedral";
        public override Vector3 bounds => new Vector3(3f, 1f, 5f);

    }
}

#endif