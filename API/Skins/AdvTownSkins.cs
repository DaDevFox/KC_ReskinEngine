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
    public class ChurchBuildingSkin : GenericBuildingSkin
    {
        internal override string UniqueName => "church";
        internal override string FriendlyName => "Church";
    }
}
