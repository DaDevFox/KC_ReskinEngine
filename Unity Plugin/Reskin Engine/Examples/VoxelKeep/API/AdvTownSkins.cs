using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//using ReskinEngine.Examples.ExampleMod;

namespace ReskinEngine.API
{
    [Jobs(4)]
    [Category("advTown")]
    public class ChurchSkin : GenericBuildingSkin
    {
        internal override string UniqueName => "church";
        internal override string FriendlyName => "Church";
    }

    [Jobs(4)]
    [Category("advTown")]
    public class CathedralSkin : GenericBuildingSkin
    {
        internal override string UniqueName => "cathedral";
        internal override string FriendlyName => "Cathedral";
    }
}
