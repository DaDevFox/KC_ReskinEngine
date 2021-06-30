using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Houses

    public class CottageSkin : GenericBuildingSkin
    {
        public override string UniqueName => "largehouse";
        public override string friendlyName => "Cottage";
        public override Vector3 bounds => new Vector3(2f, 1f, 1f);
    }

    #endregion
}

#endif