//#define STABLE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Harmony;
using System.Reflection;

namespace ReskinEngine.Engine
{
    public class TreeSkinBinder : SkinBinder
    {
        public override string TypeIdentifier => "tree";

        public GameObject baseModel;
        public Material material;

        public override void Read(GameObject obj) 
        {
            ReadModel(obj, "baseModel");
            ReadMaterial(obj, "material");
        }

        public override void Bind()
        {
            if(baseModel)
                TreeSystem.inst.treeMesh = baseModel.GetComponent<MeshFilter>().mesh;
            if (material)
                TreeSystem.inst.material = material;
        }
    }

#if STABLE

    /// <summary>
    /// Stable ONLY
    /// </summary>
    public class PeasantSkinBinder : SkinBinder
    {
        public static List<Villager> models = new List<Villager>();

        public override string TypeIdentifier => "peasant";

        public GameObject head;
        /// <summary>
        /// <para>default value: (0, 0.1410001f, 0)</para>
        /// </summary>
        public Vector3 headPosition;
        public Vector3 headScale;


        public GameObject body;
        /// <summary>
        /// <para>default value: (0, 0.0879999f, 0)</para>
        /// </summary>
        public Vector3 bodyPosition;
        public Vector3 bodyScale;


        public GameObject legs;
        /// <summary>
        /// Negative values will go through the floor
        /// <para>default value: (0, 0.02899987f, 0)</para>
        /// </summary>
        public Vector3 legsPosition;
        public Vector3 legsScale;

        public string[] outlineMeshes;



        public override void Read(GameObject obj)
        {
            ReadModels(obj, BindingFlags.Instance | BindingFlags.Public, "head", "body", "legs");

            var headValues = ReadTransformValues(obj, "headTransform");
            var bodyValues = ReadTransformValues(obj, "bodyTransform");
            var legsValues = ReadTransformValues(obj, "legsTransform");

            ReadStringArray(obj, "outlineMeshes");

            if (headValues != null)
            {
                headPosition = headValues.Item1;
                headScale = headValues.Item3;
            }

            if (bodyValues != null)
            {
                bodyPosition = bodyValues.Item1;
                bodyScale = bodyValues.Item3;
            }

            if (legsValues != null)
            {
                legsPosition = legsValues.Item1;
                legsScale = legsValues.Item3;
            }
        }

        public override void Bind()
        {
            base.Bind();

            // Bind to base
            Villager created = GameObject.Instantiate(World.inst.personModel, HiddenCache.transform);
            BindTo(created);
            models.Add(created);
        }

        public void BindTo(Villager villager)
        {
            GameObject head = villager.transform.Find("GameObject").Find("Head").gameObject;
            GameObject body = villager.transform.Find("GameObject").Find("Body").gameObject;
            GameObject legs = villager.transform.Find("GameObject").Find("Legs").gameObject;

            if(this.head)
            {
                head.GetComponent<MeshFilter>().mesh = null;
                GameObject.Instantiate(this.head, head.transform);

                head.transform.localPosition = headPosition;
                head.transform.localScale = headScale;

                villager.head = head.transform.GetChild(0);

                if (outlineMeshes.Length == 0)
                    villager.meshesForOutline.AddRange(head.GetComponentsInChildren<MeshRenderer>());
            }

            if (this.body)
            {
                body.GetComponent<MeshFilter>().mesh = null;
                GameObject.Instantiate(this.body, body.transform);

                body.transform.localPosition = bodyPosition;
                body.transform.localScale = bodyScale;

                villager.body = body.transform.GetChild(0);

                if (outlineMeshes.Length == 0)
                    villager.meshesForOutline.AddRange(body.GetComponentsInChildren<MeshRenderer>());
            }

            if (this.legs)
            {
                legs.GetComponent<MeshFilter>().mesh = null;
                GameObject.Instantiate(this.legs, legs.transform);

                legs.transform.localPosition = legsPosition;
                legs.transform.localScale = legsScale;

                villager.legs = legs.transform.GetChild(0);

                if (outlineMeshes.Length == 0)
                    villager.meshesForOutline.AddRange(legs.GetComponentsInChildren<MeshRenderer>());
            }

            foreach (string path in outlineMeshes)
                if (villager.transform.Find(path) && villager.transform.Find(path).GetComponent<MeshRenderer>())
                    villager.meshesForOutline.Add(villager.transform.Find(path).GetComponent<MeshRenderer>());
        }

        [HarmonyPatch(typeof(PooledObject))]
        [HarmonyPatch("Alloc", typeof(Vector3), typeof(Quaternion))]
        static class VillagerAllocPatch
        {
            static bool Prefix(PooledObject __instance, ref PooledObject __result, Vector3 position, Quaternion rotation)
            {
                Villager vil = __instance as Villager;
                if (vil == null)
                    return true;

                PooledObject pooledObject;
                PooledObject next = typeof(PooledObject).GetField("next", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(__instance) as PooledObject;
                if (next != null)
                {
                    pooledObject = next;
                    typeof(PooledObject).GetField("next", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(__instance, 
                        (typeof(PooledObject).GetField("next", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(pooledObject) as PooledObject));

                    //__instance.next = pooledObject.next;


                    typeof(PooledObject).GetField("next", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(pooledObject, null);

                    //pooledObject.next = null;
                    pooledObject.transform.position = position;
                    pooledObject.transform.rotation = rotation;
                    pooledObject.gameObject.SetActive(true);
                }
                else
                {
                    if(models.Count > 1)
                        pooledObject = UnityEngine.Object.Instantiate<PooledObject>(models[SRand.Range(0, models.Count)]);
                    else if(models.Count == 1)
                        pooledObject = UnityEngine.Object.Instantiate<PooledObject>(models[0]);
                    else
                        pooledObject = UnityEngine.Object.Instantiate<PooledObject>(World.inst.personModel);
                    pooledObject.transform.position = position;
                    if (rotation != Quaternion.identity)
                    {
                        pooledObject.transform.rotation = rotation;
                    }
                    //pooledObject.prefab = __instance;

                    typeof(PooledObject).GetField("prefab", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(pooledObject, __instance);
                }
                pooledObject.Init();
                __result = pooledObject;

                return false;

            }
        }

        /* STABLE:
         * 
         *  On Villager.Init():
         *  - Pick Variation
         *  - Apply head, body, legs and maybe positioning
         * 
         * 
         * 
         * 
         * 
         * ALPHA:
         * 
         *  PATCH SILOS
         *  - Increase silo counts
         *  - Change silo access
         *  
         * 
         * 
         * 
         * 
         * 
         *
         */



    }

#endif

}
