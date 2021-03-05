using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//using ReskinEngine.Examples.ExampleMod;
using System.Reflection;

namespace ReskinEngine.API
{
    /// <summary>
    /// Class that holds a set of building reskins categorized under a single collection. 
    /// </summary>
    public class ReskinProfile
    {
        /// <summary>
        /// Name of the collection this reskin profile is categorized under
        /// </summary>
        public string CollectionName { get; private set; }

        /// <summary>
        /// String identifier used to detremine if other collections are part of the same set; usually the mod name
        /// </summary>
        public string CompatabilityIdentifier { get; private set; }

        /// <summary>
        /// List of other origins this profile is able to be switched out with. 
        /// </summary>
        public string[] Compatability { get; private set; }

        private Dictionary<int, Skin> skins = new Dictionary<int, Skin>();

        public static string ReskinWorldLocation { get; } = "ReskinContainer";

        /// <summary>
        /// Creates a new ReskinProfile with the specified configuration. 
        /// </summary>
        /// <param name="collectionName">Name of the collection this reskin profile is categorized under</param>
        /// <param name="compatabilityIdentifier">String identifier used to detremine if other collections are part of the same set; usually the mod name</param>
        public ReskinProfile(string collectionName, string compatabilityIdentifier)
        {
            this.CollectionName = collectionName;
            this.CompatabilityIdentifier = compatabilityIdentifier;
        }

        public void Add<T>(T skin) where T : Skin 
        {
            if (typeof(T).GetCustomAttribute<NotSupportedAttribute>() != null)
                return;

            skin.Identifier = skins.Keys.Count;
            skin.ReskinProfile = this;
            skins.Add(skins.Keys.Count, skin);
        }

        public void Register()
        {
            Transform target;

            //KCModHelper helper = Mod.helper;

            if (World.inst.transform.Find(ReskinWorldLocation) == null)
            {
                GameObject obj = GameObject.Instantiate(new GameObject(), World.inst.transform);
                obj.name = ReskinWorldLocation;

                target = obj.transform;
            }
            else
                target = World.inst.transform.Find(ReskinWorldLocation);


            foreach (Skin skin in skins.Values)
            {
                //helper.Log(skin.GetType().Name);
                skin.Package(target);
            }


            //helper.Log(InfoFileGenerator.Generate());
        }






    }
}
