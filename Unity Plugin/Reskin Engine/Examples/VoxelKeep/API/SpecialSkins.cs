using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReskinEngine.API
{
    [Category("environment")]
    public class TreeSkin : Skin
    {
        public override string Name => "tree";

        [Model(ModelAttribute.Type.Modular, description = "Model of the tree")]
        public GameObject baseModel;

        [Material("Material the tree model will use")]
        public Material material;

        protected override void PackageInternal(Transform target, GameObject _base)
        {
            base.PackageInternal(target, _base);

            if (baseModel) 
            {
                GameObject.Instantiate(baseModel, _base.transform).name = "baseModel";
                if (material)
                {
                    (_base.transform.Find("baseModel").GetComponent<MeshRenderer>() ?? _base.transform.Find("baseModel").gameObject.AddComponent<MeshRenderer>()).material = material;
                    (new GameObject("materialFlag")).transform.SetParent(_base.transform);
                }
            }
        }
    }
}
