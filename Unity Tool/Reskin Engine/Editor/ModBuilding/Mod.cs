using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    [CreateAssetMenu(menuName = "Skin/Mod")]
    public class Mod : ScriptableObject
    {
        [Tooltip("Name of the mod")]
        public string modName = "MyMod";
        [Tooltip("Mods with the same compatability identifier can work together")]
        public string compatabilityIdentifier = "default";

        public Collection[] collections;
        public string outputPath = "default";

        public string @namespace = "authorName.modName";
    }
}


#endif