using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReskinEngine.Engine
{
    public class TreeSkinBinder : SkinBinder
    {
        public override string TypeIdentifier => "tree";

        public GameObject baseModel;
        public Material material;

        public override void Read(GameObject obj) 
        {
            ReadModel(obj, "baseModel");
            ReadMaterial(obj, "material");
        }

        public override void Bind()
        {
            if(baseModel)
                TreeSystem.inst.treeMesh = baseModel.GetComponent<MeshFilter>().mesh;
            if (material)
                TreeSystem.inst.material = material;
        }
    }
}
