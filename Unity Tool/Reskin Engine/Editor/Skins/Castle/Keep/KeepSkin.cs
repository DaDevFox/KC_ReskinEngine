using System;
using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    
    [Serializable]
    public class KeepSkin : BuildingSkin
    {
        public override string UniqueName => "keep";

        [Tooltip("First upgrade of the Keep")]
        public GameObject keepUpgrade1;
        [Tooltip("Second upgrade of the Keep")]
        public GameObject keepUpgrade2;
        [Tooltip("Third upgrade of the Keep")]
        public GameObject keepUpgrade3;
        [Tooltip("Fourth upgrade of the Keep")]
        public GameObject keepUpgrade4;

        public override Vector3 bounds => new Vector3(3f, 1f, 3f);
    }

}

#endif