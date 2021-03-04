using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Misc Buildings

    public class ChamberOfWarSkin : GenericBuildingSkin
    {
        public override string UniqueName => "chamberofwar";
        public override string friendlyName => "Chamber Of War";

        public override Vector3 bounds => new Vector3(3f, 1f, 1f);
    }

    #endregion
}

#endif