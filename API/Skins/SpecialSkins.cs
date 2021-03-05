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
        public override string Name => "Tree";
        internal override string TypeIdentifier => "tree";

        [Model(ModelAttribute.Type.Modular, description = "Model of the tree")]
        public GameObject baseModel;

        [Material("Material the tree model will use")]
        public Material material;

        protected override void PackageInternal(Transform target, GameObject _base)
        {
            base.PackageInternal(target, _base);

            AppendModel(_base, baseModel, "baseModel");
            AppendMaterial(_base, material, "material");

        }
    }
}
