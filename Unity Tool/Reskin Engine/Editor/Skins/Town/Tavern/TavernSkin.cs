using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public class TavernSkin : GenericBuildingSkin
    {
        public override string UniqueName => "tavern";
        public override Vector3 bounds => new Vector3(2f, 1f, 1f); 
    }
}

#endif