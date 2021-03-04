using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinEngine.Engine
{
    public static class Settings
    {
        public enum PriorityType
        {
            Absolute,
            WeightedRandom
        }


        private static int defaultPriority = 1;

        public static Dictionary<string, int> ModPriority { get; set; } = new Dictionary<string, int>();
        public static PriorityType priorityType { get; set; }

        public static void Setup()
        {
            foreach(string mod in Engine.ModIndex.Keys)
            {
                ModPriority.Add(mod, defaultPriority);
            }
        }






    }
}
