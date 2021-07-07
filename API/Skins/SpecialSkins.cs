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

    [Category("maritime")]
    public class FishingShipSkin : Skin
    {
        public override string Name => "Fishing Ship";
        internal override string TypeIdentifier => "fishingship";


        [Model(ModelAttribute.Type.Modular, description = "Modular model all fishing ships will use")]
        public GameObject baseModel;

        [Material("Material the ship model will use")]
        public Material material;

        protected override void PackageInternal(Transform dropoff, GameObject _base)
        {
            base.PackageInternal(dropoff, _base);

            AppendModel(_base, baseModel, "baseModel");
            AppendMaterial(_base, material, "material");
        }
    }

    [Category("environment")]
    [Version(VersionAttribute.GameVersion.STABLE)]
    public class PeasantSkin : Skin
    {
        [Model(ModelAttribute.Type.Instance)]
        public GameObject head;
        /// <summary>
        /// <para>default value: (0, 0.1410001f, 0)</para>
        /// </summary>
        public Vector3 headPosition = new Vector3(0f, 0.1410001f, 0f);
        public Vector3 headScale = new Vector3(0.04514214f, 0.04514214f, 0.04514214f);


        [Model(ModelAttribute.Type.Instance)]
        public GameObject body;
        /// <summary>
        /// <para>default value: (0, 0.0879999f, 0)</para>
        /// </summary>
        public Vector3 bodyPosition = new Vector3(0, 0.0879999f, 0);
        public Vector3 bodyScale = new Vector3(0.07660437f, 0.07660437f, 0.07660437f);


        [Model(ModelAttribute.Type.Instance)]
        public GameObject legs;
        /// <summary>
        /// Negative values will go through the floor
        /// <para>default value: (0, 0.02899987f, 0)</para>
        /// </summary>
        public Vector3 legsPosition = new Vector3(0, 0.02899987f, 0);
        public Vector3 legsScale = new Vector3(0.05853106f, 0.05853106f, 0.05853106f);

        public override string Name => "peasant";
        internal override string TypeIdentifier => "peasant";

        protected override void PackageInternal(Transform dropoff, GameObject _base)
        {
            base.PackageInternal(dropoff, _base);

            AppendModel(_base, head, "head");
            AppendModel(_base, body, "body");
            AppendModel(_base, legs, "legs");

            AppendTransform(_base, "headTransform", headPosition, Quaternion.identity, headScale);
            AppendTransform(_base, "legsTransform", bodyPosition, Quaternion.identity, bodyScale);
            AppendTransform(_base, "legsTransform", legsPosition, Quaternion.identity, legsScale);
        }
    }

    /// <summary>
    /// ALPHA only skin
    /// </summary>
    [Category("special")]
    [Version(VersionAttribute.GameVersion.ALPHA)]
    public class LiverySkin
    {
        public Texture[] banners;
        public Material[] bannerMaterial;
        public Color[] bannerColor;
        public Color[] mapColor;
        public Material[] uniMaterial;
        public Material[] buildingMaterial;
        public Material[] armyMaterial;
        public Material[] buildUIMaterial;
        public Material[] flagMaterial;
        public Material[] uniMaterialCracked;
    }


}
