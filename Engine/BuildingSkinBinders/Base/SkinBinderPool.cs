using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;

namespace ReskinEngine.Engine
{
    public class SkinBinderPool
    {
        public static Dictionary<string, List<SkinBinder>> Pool { get; private set; } = new Dictionary<string, List<SkinBinder>>();

        public List<SkinBinder> Active = new List<SkinBinder>();


        internal static void Bind(List<Collection> from)
        {
            int idx = 0;
            Pool.Clear();


            foreach(Collection collection in from)
            {
                foreach(SkinBinder binder in collection)
                {
                    binder.Identifier = idx;
                    
                    if (!Pool.ContainsKey(binder.CollectionName))
                        Pool.Add(binder.CollectionName, new List<SkinBinder>());

                    Pool[binder.CollectionName].Add(binder);

                    idx++;
                }
            }
        }
        

        internal static Dictionary<string, List<SkinBinder>> GetAllBindersForType(string typeIdentifier)
        {
            Dictionary<string, List<SkinBinder>> all = new Dictionary<string, List<SkinBinder>>();
            
            foreach (string collection in Pool.Keys)
            {
                List<SkinBinder> binders = new List<SkinBinder>();
                foreach(SkinBinder b in Pool[collection])
                    if (b.TypeIdentifier == typeIdentifier)
                        binders.Add(b);

                if (binders.Count > 0)
                    all.Add(collection, binders);
            }

            return all;
        }

        private static string FindBestCompatabilityIdentifierForCollections(List<string> collections)
        {
            string chosenCompatabilityIdentifier = null;
            if (Settings.priorityType == Settings.PriorityType.Absolute)
            {
                Dictionary<string, int> priorities = new Dictionary<string, int>();
                foreach (string collection in collections)
                {
                    if (!priorities.ContainsKey(collection))
                        priorities.Add(collection, 0);

                    priorities[collection] += Settings.CollectionPriority[collection];
                }

                int max = -1;
                string compatabilityMax = null;

                foreach (string compatability in priorities.Keys)
                {
                    int priority = priorities[compatability];
                    if (priority > max)
                    {
                        max = priority;
                        compatabilityMax = compatability;
                    }
                }

                if (compatabilityMax != null)
                    chosenCompatabilityIdentifier = compatabilityMax;
            }
            return chosenCompatabilityIdentifier;
        }





        internal static SkinBinder DetermineBinderForType(string typeIdentifier)
        {
            Dictionary<string, List<SkinBinder>> binders = GetAllBindersForType(typeIdentifier);

            List<string> collectionsByPriority =
                binders.Keys.ToList()
                .OrderBy(c => Settings.CollectionPriority[c]).ToList();

            List<SkinBinder> candidates = new List<SkinBinder>();


            string chosenCompatabilityIdentifier = FindBestCompatabilityIdentifierForCollections(collectionsByPriority);

            if(chosenCompatabilityIdentifier != null)
            {
                foreach(string collection in binders.Keys)
                {
                    Collection current = Engine.GetCollection(collection);

                    if (current.CompatabilityIdentifier == chosenCompatabilityIdentifier)
                        foreach (SkinBinder b in current)
                            candidates.Add(b);
                }
            }

            if (candidates.Count > 0)
                return candidates[SRand.Range(0, candidates.Count - 1)];

            return null;
        }




        //[HarmonyPatch(typeof(World), "PlaceInternal")]
        //class OnPlacePatch
        //{
        //    static void Postfix(Building PendingObj)
        //    {
        //        SkinBinder binder = DetermineBinderForType(PendingObj.UniqueName + "Skin");

        //        if (binder != null)
        //            Engine.helper.Log("found binder for " + PendingObj.UniqueName);
        //    }
        //}



    }
}
