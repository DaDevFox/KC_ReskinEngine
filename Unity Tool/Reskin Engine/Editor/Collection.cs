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
    [CreateAssetMenu(menuName = "Reskin Engine/Collection")]
    public class Collection : ScriptableObject
    {
        public Skin[] skins = new Skin[0];

        public void Add(Skin skin){
            Array.Resize(ref skins, skins.Length + 1);
            skins[skins.Length - 1] = skin;
        }

        public void Remove(Skin skin){
            Remove(Find(skin));
        }

        public void Remove(int index){
            int i1 = index;
            int i2 = skins.Length - 1;
            Skin toRemove = skins[i1];

            skins[i1] = skins[i2];
            skins[i2] = toRemove;

            Array.Resize(ref skins, skins.Length - 1);
        }

        public int Find(Skin skin){
            for (int i = 0; i < skins.Length; i++)
                if(skins[i] == skin)
                    return i;
            return -1;
        }

        public int Find(string skinId)
        {
            for (int i = 0; i < skins.Length; i++)
            {
                if (skins[i].typeId == skinId)
                    return i;
            }
            return -1;
        }

        public bool Contains(Skin skin) => Find(skin) != -1;

        public bool Contains(string skinId) => Find(skinId) != -1;

        public static Type GetType(string name) => ReskinEngine.skins.First(s => s.friendlyName == name).GetType();

        public Skin Create(string name)
        {
            Type type = ReskinEngine.skins.First(s => s.friendlyName == name).GetType();

            Skin skin = Activator.CreateInstance(type) as Skin;
            Add(skin);

            return skin;
        }
    }
}

#endif