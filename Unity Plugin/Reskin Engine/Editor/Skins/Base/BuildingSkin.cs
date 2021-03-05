using System;
using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    [Serializable]
    public abstract class BuildingSkin : Skin
    {
        public sealed override string typeId => UniqueName != "hidden" ? $"building_{UniqueName}" : "hidden";
        public override string friendlyName
        {
            get
            {
                if (UniqueName.Length == 0)
                    return "";
                else if (UniqueName.Length == 1)
                    return char.ToUpper(UniqueName[0]).ToString();
                else
                    return char.ToUpper(UniqueName[0]) + UniqueName.Substring(1);
            }
        }

        public virtual string UniqueName { get; }

        public virtual Vector3 bounds { get; } = new Vector3(1f, 0.5f, 1f);

        [Tooltip("All the locations peasants will stand at when working at this building, in order of employee assignments")]
        public Vector3[] personPositions;
        [Tooltip("Paths relative to the building root to all the meshes that will be included in the outline effect when selecting the building (each item in list requires MeshRenderer component)")]
        public string[] outlineMeshes;
        [Tooltip("Paths relative to the building root to all the skinned mesh renderers that will be included in the outline effect when selecting the building (each item in list requires SkinnedMeshRenderer component)")]
        public string[] outlineSkinnedMeshes;
    }



}

#endif