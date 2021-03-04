using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Gates

    #endregion

    #region Castle Stairs

    public class CastleStairsSkin : BuildingSkin
    {
        public override string UniqueName => "castlestairs";
        public override string friendlyName => "Castle Stairs";

        [Tooltip("stairs facing +z")]
        public GameObject stairsFront;
        [Tooltip("stairs facing +x")]
        public GameObject stairsRight;
        [Tooltip("stairs facing -z")]
        public GameObject stairsDown;
        [Tooltip("stairs facing -x")]
        public GameObject stairsLeft;


        public GameObject doorPrefab;
    }

    #endregion
}

#endif