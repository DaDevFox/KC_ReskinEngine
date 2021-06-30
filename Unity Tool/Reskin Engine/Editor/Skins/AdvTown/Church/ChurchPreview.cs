using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{

    #region Town Buildings

    public class ChurchPreview : GenericBuildingPreview<ChurchSkin>
    {
        public override string path => "Reskin Engine/Buildings/Church";
    }

    #endregion
}

#endif