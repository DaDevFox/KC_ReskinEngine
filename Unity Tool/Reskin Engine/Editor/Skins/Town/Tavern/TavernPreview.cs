using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public class TavernPreview : GenericBuildingPreview<TavernSkin>
    {
        public override string path => $"{buildingsRoot}/Tavern";
    }
}

#endif