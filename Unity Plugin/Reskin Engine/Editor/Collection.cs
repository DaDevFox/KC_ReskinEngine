using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    [CreateAssetMenu(menuName = "Skin/Collection")]
    public class Collection : ScriptableObject
    {
        public List<Skin> implementedSkins { get; } = new List<Skin>();


        public Skin Create(string name)
        {
            Type type = ReskinEngine.skins.First(s => s.friendlyName == name).GetType();

            Skin skin = Activator.CreateInstance(type) as Skin;
            implementedSkins.Add(skin);

            return skin;
        }
    }
}

#endif