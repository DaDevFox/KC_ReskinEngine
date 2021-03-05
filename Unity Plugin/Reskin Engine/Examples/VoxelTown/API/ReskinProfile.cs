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
    /// Class that holds a set of reskins categorized under a single mod. 
    /// </summary>
    public class ReskinProfile
    {
        private static KCModHelper helper;

        /// <summary>
        /// Name of the mod this reskin profile is categorized under
        /// </summary>
        public string ModName { get; private set; }

        /// <summary>
        /// String identifier used to detremine if other mods are part of the same set; usually the mod name or authors name
        /// </summary>
        public string CompatabilityIdentifier { get; private set; }

        /// <summary>
        /// List of other origins this profile is able to be switched out with. 
        /// </summary>
        [Obsolete]
        public string[] Compatability { get; private set; }

        private Dictionary<int, Skin> skins = new Dictionary<int, Skin>();

        public static string ReskinWorldLocation { get; } = "ReskinContainer";

        /// <summary>
        /// Creates a new ReskinProfile with the specified configuration. 
        /// </summary>
        /// <param name="modName">Name of the mod this reskin profile is categorized under</param>
        /// <param name="compatabilityIdentifier">String identifier used to detremine if other mods are part of the same set; usually the mod name</param>
        public ReskinProfile(string modName, string compatabilityIdentifier)
        {
            this.ModName = modName;
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

        private void Preload(KCModHelper helper)
        {
            ReskinProfile.helper = helper;
        }




    }
}
