using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReskinEngine.Utils
{
    public static class GameObjectExtensions
    {
        public static void ClearChildren(this GameObject obj)
        {
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                GameObject.DestroyImmediate(obj.transform.GetChild(i).gameObject);
            }
        }

        public static void ClearChildren(this Transform obj)
        {
            for (int i = 0; i < obj.childCount; i++)
            {
                GameObject.DestroyImmediate(obj.GetChild(i).gameObject);
            }
        }

    }


}