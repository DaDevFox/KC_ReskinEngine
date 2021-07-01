using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using ReskinEngine.Utils;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public class ReskinEngine
    {
        public static string APIVersionMajor;
        public static string APIVersionMinor;
        public static string ToolVersionMajor;
        public static string ToolVersionMinor;

        public static Type[] skinTypes { get; private set; }
        public static Skin[] skins { get; private set; }

        public static void UpdateTypes()
        {
            IEnumerable<Type> skins = 
                from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsSubclassOf(typeof(Skin))
                select t;

            List<Skin> skinInstances = new List<Skin>();

            foreach (Type type in skins)
            {
                if (type.IsAbstract)
                    continue;

                Skin skin = Activator.CreateInstance(type) as Skin;

                if(skin.typeId != "hidden")
                    skinInstances.Add(skin);
            }


            ReskinEngine.skinTypes = skins.ToArray();
            ReskinEngine.skins = skinInstances.ToArray();
        }




    }

}

#endif