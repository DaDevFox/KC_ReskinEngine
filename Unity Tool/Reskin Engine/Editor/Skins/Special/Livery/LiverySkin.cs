using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public class LiverySkin : Skin
    {
        public override string typeId => "livery";

        public override GameVersion version => GameVersion.ALPHA;
        public override bool supportsVariations => false;

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

#endif