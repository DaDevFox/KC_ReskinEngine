using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public class ChurchSkin : GenericBuildingSkin
    {
        public override string UniqueName => "church";
        public override Vector3 bounds => new Vector3(3f, 1f, 2f); 
    }
}

#endif