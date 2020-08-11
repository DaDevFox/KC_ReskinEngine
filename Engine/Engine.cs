using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Harmony;
using UnityEngine;

namespace ReskinEngine.Engine
{
    public class Engine
    {
        public static KCModHelper helper;
        public static bool debug = true;


        public static string ReskinWorldLocation { get; } = "ReskinContainer";

        /// <summary>
        /// A dictionary of all the original instances of each type of skin
        /// </summary>
        public static Dictionary<string, SkinBinder> SkinLookup { get; } = new Dictionary<string, SkinBinder>();

        /// <summary>
        /// A dictionary of all registered collections, keyed by collection name. 
        /// </summary>
        internal static Dictionary<string, Collection> CollectionIndex { get; private set; } = new Dictionary<string, Collection>();

        public static List<string> ActiveCollections { get; private set; }

        #region Initialization

        public void Preload(KCModHelper helper)
        {
            Engine.helper = helper;

            var harmony = HarmonyInstance.Create("harmony");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            if (debug)
                Application.logMessageReceived += (condition, stack, type) => 
                {
                    if (type == LogType.Exception)
                        helper.Log($"ex:{condition} => {stack}");
                };

            helper.Log("Preload");
        }

        public void SceneLoaded(KCModHelper helper)
        {
            helper.Log("SceneLoaded");
        }

        #endregion

        #region Utilities

        public static void dLog(object message)
        {
            if(debug)
                helper.Log(message.ToString());
        }


        /// <summary>
        /// Gets the original SkinBinder for a specified type identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static SkinBinder GetOriginalBinder(string identifier)
        {
            if (SkinLookup.ContainsKey(identifier))
                helper.Log("found skin identifier " + identifier);
            
            return SkinLookup.ContainsKey(identifier) ? SkinLookup[identifier] : null;
        }

        public static Collection GetCollection(string name)
        {
            return CollectionIndex.ContainsKey(name) ? CollectionIndex[name] : null;
        }

        public static Dictionary<string, List<SkinBinder>> GetActivePool()
        {
            return CollectionIndex[CollectionIndex.Keys.First()].Binders;
        }

        public static SkinBinder GetRandomBinderFromActive(string identifier)
        {
            if (!GetActivePool().ContainsKey(identifier))
                return null;

            List<SkinBinder> binders = GetActivePool()[identifier];

            return binders[SRand.Range(0, binders.Count)];
        }

        public static SkinBinder GetBinderFromActive(string identifier)
        {
            if (!GetActivePool().ContainsKey(identifier))
                return null;

            List<SkinBinder> binders = GetActivePool()[identifier];

            return binders[0];
        }

        #endregion

        #region Setup

        static void AfterSceneLoaded()
        {
            InitLookup();
            ReadSkins();
            SetupCollections();
            BindAll();

            helper.Log("AfterSceneLoaded complete");
        }

        private static void InitLookup()
        {
            SkinLookup.Clear();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            foreach(Type type in types)
            {
                if (type.GetCustomAttribute<UnregisteredAttribute>() != null)
                    continue;

                if (type.IsSubclassOf(typeof(SkinBinder)) && !type.IsAbstract)
                {
                    SkinBinder s = Activator.CreateInstance(type) as SkinBinder;
                    SkinLookup.Add(s.TypeIdentifier, s);
                }
                
            }
        }

        private static void ReadSkins()
        {
            Transform target = World.inst.transform.Find(ReskinWorldLocation);

            if (!target)
                return;

            DeactivateWorldLocation();

            int childCount = target.childCount;
            for (int i = 0; i < childCount; i++)
            {
                SkinBinder binder = SkinBinder.Unpack(target.GetChild(i).gameObject);

                if (!CollectionIndex.ContainsKey(binder.CollectionName))
                    CollectionIndex.Add(binder.CollectionName, Collection.Create(binder.CollectionName, binder.CompatabilityIdentifier));
                
                CollectionIndex[binder.CollectionName].Add(binder);
            }
        }

        private static void DeactivateWorldLocation()
        {
            World.inst.transform.Find(ReskinWorldLocation).gameObject.SetActive(false);
        }

        private static void SetupCollections()
        {
            Settings.Setup();
        }


        private static void BindAll()
        {
            GetActivePool().Values.Do((binders) => binders[0].Bind());
        }

        #endregion

        [HarmonyPatch(typeof(KCModHelper.ModLoader), "SendSceneLoadSignal")]
        class AfterSceneLoadedPatch
        {
            static void Postfix()
            {
                helper.Log("After SceneLoaded");
                AfterSceneLoaded();
            }
        }


        public static void Exception(Exception ex)
        {
            helper.Log(ex.ToString());
        }

    }
}
