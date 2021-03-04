using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Castle Blocks


    #endregion

    #region Gates

    #region Base

    public abstract class GateSkin : BuildingSkin
    {
        public override string UniqueName => "hidden";

        [Tooltip("The main model of the gate, excluding the porticulus")]
        public GameObject gate;
        [Tooltip("The part of the gate that moves up and down to show opening/closing")]
        public GameObject porticulus;
    }

    #endregion

    #endregion
}

#endif