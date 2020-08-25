using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;

namespace ReskinEngine.Editor
{
    public class ReskinEngine
    {
        public static Type[] skinTypes { get; private set; }
        public static Skin[] skins { get; private set; }

        public static void UpdateTypes()
        {
            IEnumerable<Type> skins = 
                from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsSubclassOf(typeof(Skin))
                select t;

            List<Skin> skinInstances = new List<Skin>();

            foreach (Type t in skins)
            {
                Skin s = Activator.CreateInstance(t) as Skin;

                if(s.id != "hidden" && !t.IsAbstract)
                    skinInstances.Add(s);
            }


            ReskinEngine.skinTypes = skins.ToArray();
            ReskinEngine.skins = skinInstances.ToArray();
        }




    }

    [CreateAssetMenu(menuName = "Skin/Keep")]
    [Serializable]
    public class KeepSkin : Skin
    {
        public override string id => "Keep";

        public GameObject keepUpgrade1;
        public GameObject keepUpgrade2;
        public GameObject keepUpgrade3;
        public GameObject keepUpgrade4;
    }

    public class CastleBlockSkin : Skin
    {
        public override string id => "hidden";

        public GameObject Open;
        public GameObject Closed;
        public GameObject Single;
        public GameObject Opposite;
        public GameObject Adjacent;
        public GameObject Threeside;

        public GameObject doorPrefab;
    }


    public class StoneCastleBlockSkin : CastleBlockSkin
    {
        public override string id => "Stone Castle Block";
    }

    public class WoodCastleBlockSkin : CastleBlockSkin
    {
        public override string id => "Wood Castle Block";
    }


}
