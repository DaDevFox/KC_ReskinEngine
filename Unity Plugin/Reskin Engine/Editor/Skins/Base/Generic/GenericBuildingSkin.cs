using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Generic

    public abstract class GenericBuildingSkin : BuildingSkin
    {
        public abstract override string UniqueName { get; }

        [Tooltip("Model will be instantiated under original building model")]
        public GameObject baseModel;
    }

    #endregion
}

#endif