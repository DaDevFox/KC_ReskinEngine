using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReskinEngine.Engine
{
    public class HovelSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "smallhouse";

        public override void Read(GameObject obj) => base.Read<HovelSkinBinder>(obj);
    }

    public class CottageSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "largehouse";

        public override void Read(GameObject obj) => base.Read<CottageSkinBinder>(obj);
    }

    public class ManorSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "manorhouse";

        public override void Read(GameObject obj) => base.Read<ManorSkinBinder>(obj);
    }

    public class WellSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "well";

        public override void Read(GameObject obj) => base.Read<WellSkinBinder>(obj);
    }

    public class TavernSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "tavern";

        public override void Read(GameObject obj) => base.Read<TavernSkinBinder>(obj);
    }

    public class FireBrigadeSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "firehouse";

        public override void Read(GameObject obj) => base.Read<FireBrigadeSkinBinder>(obj);
    }
}
