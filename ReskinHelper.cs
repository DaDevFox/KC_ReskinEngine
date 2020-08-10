//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;
//using Harmony;
//using System.Reflection;

//namespace BuildingFramework
//{
//    class ModelHelper : MonoBehaviour
//    {
//        private static List<BuildingSkin> deferredReskins = new List<BuildingSkin>();

//        public static KCModHelper helper;
//        public void Preload(KCModHelper helper)
//        {
//            ModelHelper.helper = helper;

//            var harmony = HarmonyInstance.Create("harmony");
//            harmony.PatchAll(Assembly.GetExecutingAssembly());
//        }

//        public static void ReskinBuildingBase(BuildingSkin skin)
//        {
//            deferredReskins.Add(skin);
//        }

//        #region Removal/Replace Procedure

//        public static void ExecuteRemovalProcedure(BuildingSkin modelInfo)
//        {
//            if (modelInfo.removalProcedure != null)
//            {
//                modelInfo.removalProcedure.Invoke(GameState.inst.GetPlaceableByUniqueName(modelInfo.buildingUniqueName));
//            }
//            else
//            {
//                helper.Log("no base model removal procedure found for building " + modelInfo.buildingUniqueName);
//            }
//        }

//        public static void ExecuteReplaceProcedure(BuildingSkin modelInfo)
//        {
//            if (modelInfo.replaceProcedure != null)
//            {
//                modelInfo.replaceProcedure.Invoke(GameState.inst.GetPlaceableByUniqueName(modelInfo.buildingUniqueName));
//            }
//            else
//            {
//                helper.Log("no base model replace procedure found for building " + modelInfo.buildingUniqueName);
//            }
//        }

//        #endregion



//        [HarmonyPatch(typeof(GameState))]
//        [HarmonyPatch("Start")]
//        static class InitPatch
//        {
//            static void Postfix()
//            {
//                foreach (BuildingSkin modelInfo in deferredReskins)
//                {
//                    try
//                    {
//                        ExecuteRemovalProcedure(modelInfo);
//                        ExecuteReplaceProcedure(modelInfo);
//                    }
//                    catch (Exception ex)
//                    {
//                        helper.Log(ex.Message + "\n" + ex.StackTrace);
//                    }
//                }
//            }
//        }

//    }




//}
