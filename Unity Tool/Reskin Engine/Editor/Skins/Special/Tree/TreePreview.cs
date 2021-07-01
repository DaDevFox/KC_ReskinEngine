using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public class TreePreview : SkinPreview<TreeSkin>
    {
        public static string treeModel = $"{environmentRoot}/treetop";
        public static string treeMaterial = $"{environmentRoot}/TreeTop";

        public override GameObject Create()
        {
            return new GameObject("TreePreview", typeof(MeshFilter), typeof(MeshRenderer)); 
        }

        public override void Apply(GameObject obj, TreeSkin skin)
        {
            base.Apply(obj, skin);

            if (skin.baseModel)
                obj.GetComponent<MeshFilter>().mesh = skin.baseModel.GetComponent<MeshFilter>().sharedMesh;
            else
                obj.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>(treeModel);
            if (skin.material)
                obj.GetComponent<MeshRenderer>().material = skin.material;
            else
                obj.GetComponent<MeshRenderer>().material = Resources.Load<Material>(treeMaterial);
        }
    }
}

#endif
