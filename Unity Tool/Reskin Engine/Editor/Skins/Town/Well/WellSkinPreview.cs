using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if UNITY_EDITOR


namespace ReskinEngine.Editor
{
    public class WellSkinPreview : GenericBuildingPreview<WellSkin>
    {
        public override string path => $"{buildingsRoot}/Well";
    }
}

#endif
