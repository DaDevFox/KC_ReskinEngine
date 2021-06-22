using UnityEngine;
using ReskinEngine.Utils;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public abstract class GenericBuildingPreview<T> : BuildingPreview<T> where T : GenericBuildingSkin
    {
        private static string originalName { get; } = "originalModel";
        private static string replacedName { get; } = "replacedModel";

        public abstract override string path { get; }

        public override void Apply(GameObject obj, T skin)
        {
            base.Apply(obj, skin);

            Transform target = obj.transform.GetChild(0).GetChild(0);
            Collider collider = target.GetComponent<Collider>();
            if(collider)
                GameObject.Destroy(collider);

            if (skin.baseModel)
            {
                target.ClearChildren();
                target.GetComponent<MeshFilter>().mesh = null;
                if (target.transform.Find("baseModel"))
                {
                    GameObject.Destroy(target.transform.Find("baseModel").gameObject);
                }
                GameObject model = GameObject.Instantiate(skin.baseModel, target);
                model.name = "baseModel";
                //if (obj.transform.Find(skin.colliders) && obj.transform.Find(skin.colliders).GetComponent<Collider>() != collider)
                //{
                //    GameObject.DestroyImmediate(collider);
                //}
            }
        }

        private Vector3 xz(Vector3 original) => new Vector3(original.x, 0f, original.z);
    }
}

#endif