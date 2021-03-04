using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinEngine.API
{
    public static class Util
    {
        public static KCModHelper helper { get; private set; }

        static void Preload(KCModHelper helper) => Util.helper = helper;

        public static void Log(object message) => helper.Log(message.ToString());

    }
}
