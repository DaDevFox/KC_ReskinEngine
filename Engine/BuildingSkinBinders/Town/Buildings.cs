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

        public override SkinBinder Create(GameObject obj) => base.Create<HovelSkinBinder>(obj);
    }

    public class CottageSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "largehouse";

        public override SkinBinder Create(GameObject obj) => base.Create<CottageSkinBinder>(obj);
    }

    public class ManorSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "manorhouse";

        public override SkinBinder Create(GameObject obj) => base.Create<ManorSkinBinder>(obj);
    }

    public class WellSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "well";

        public override SkinBinder Create(GameObject obj) => base.Create<WellSkinBinder>(obj);
    }

    public class TavernSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "tavern";

        public override SkinBinder Create(GameObject obj) => base.Create<TavernSkinBinder>(obj);
    }

    public class FireBrigadeSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "firehouse";

        public override SkinBinder Create(GameObject obj) => base.Create<FireBrigadeSkinBinder>(obj);
    }
}
