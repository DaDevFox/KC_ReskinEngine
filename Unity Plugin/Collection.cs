using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace ReskinEngine.Editor
{
    [CreateAssetMenu(menuName = "Skin/Collection")]
    public class Collection : ScriptableObject
    {
        public List<Skin> implementedSkins = new List<Skin>();


        public Skin Create(string id)
        {
            Type type = ReskinEngine.skins.First(s => s.id == id).GetType();

            Skin skin = Activator.CreateInstance(type) as Skin;
            implementedSkins.Add(skin);

            return skin;
        }









    }


    public abstract class Skin : ScriptableObject
    {
        public Skin()
        {

        }

        public abstract string id { get; }
    }



}
