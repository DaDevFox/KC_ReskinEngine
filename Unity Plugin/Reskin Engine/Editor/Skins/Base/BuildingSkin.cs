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

        public Vector3[] personPositions;
    }



}

#endif