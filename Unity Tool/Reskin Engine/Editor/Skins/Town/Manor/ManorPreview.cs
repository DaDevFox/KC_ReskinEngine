using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if UNITY_EDITOR


namespace ReskinEngine.Editor
{
    public class ManorPreview : GenericBuildingPreview<ManorSkin>
    {
        public override string path => "Reskin Engine/Buildings/ManorHouse";
    }
}

#endif