using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public class ManorSkin : GenericBuildingSkin
    {
        public override string UniqueName => "manorhouse";
        public override Vector3 bounds => new Vector3(2f, 1f, 2f);
        public override string friendlyName => "Manor House";
    }
}

#endif
