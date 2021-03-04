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

            if (skin.baseModel)
            {
                
                GameObject original = obj.transform.Find("Offset/" + originalName) != null ? obj.transform.Find("Offset/" + originalName).gameObject : obj.transform.Find("Offset").GetChild(0).gameObject;
                if (original.name != originalName)
                {
                    original.name = originalName;
                    original.SetActive(false);
                }

                GameObject replaced = obj.transform.Find("Offset/" + replacedName) != null ? obj.transform.Find("Offset/" + originalName).gameObject : new GameObject(replacedName);
                if (replaced.transform.parent != obj.transform.Find("Offset"))
                {
                    replaced.transform.SetParent(obj.transform.Find("Offset"), true);
                    replaced.transform.SetSiblingIndex(1);
                }
                replaced.transform.position = Vector3.zero;


                replaced.ClearChildren();
                GameObject.Instantiate(skin.baseModel, xz(skin.bounds/2f), Quaternion.identity, replaced.transform);
            }
            //else
                //Reset(obj);
        }

        private Vector3 xz(Vector3 original) => new Vector3(original.x, 0f, original.z);
    }
}

#endif