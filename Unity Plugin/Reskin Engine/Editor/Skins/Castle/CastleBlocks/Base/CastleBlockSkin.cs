using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Castle Blocks

    #region Base

    public abstract class CastleBlockSkin : BuildingSkin
    {
        public override string UniqueName => "hidden";

        [Tooltip("Modular GameObject; mesh and material used only")]
        public GameObject Open;

        [Tooltip("Modular GameObject; mesh and material used only")]
        public GameObject Closed;

        [Tooltip("Modular GameObject; mesh and material used only")]
        public GameObject Single;

        [Tooltip("Modular GameObject; mesh and material used only")]
        public GameObject Opposite;

        [Tooltip("Modular GameObject; mesh and material used only")]
        public GameObject Adjacent;

        [Tooltip("Modular GameObject; mesh and material used only")]
        public GameObject Threeside;


        public GameObject doorPrefab;
    }

    #endregion

    #endregion
}

#endif