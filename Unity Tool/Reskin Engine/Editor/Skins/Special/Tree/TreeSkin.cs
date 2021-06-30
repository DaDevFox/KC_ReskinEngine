using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public class TreeSkin : Skin
    {
        public override string typeId => "tree";

        public GameObject baseModel;
        public Material material;
    }
}

#endif