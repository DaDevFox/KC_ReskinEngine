using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public abstract class Skin : ScriptableObject
    {
        public abstract string typeId { get; }
        public virtual string friendlyName
        {
            get
            {
                if (typeId.Length == 0)
                    return "";
                else if (typeId.Length == 1)
                    return char.ToUpper(typeId[0]).ToString();
                else
                    return char.ToUpper(typeId[0]) + typeId.Substring(1);
            }
        }

        public virtual SkinPreview preview { get; }
    }



}

#endif