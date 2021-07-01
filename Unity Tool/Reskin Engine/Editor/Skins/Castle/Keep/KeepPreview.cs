using UnityEngine;
using UnityEditor;
using ReskinEngine.Utils;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Keep

    public class KeepPreview : BuildingPreview<KeepSkin>
    {
        public override string path => $"{buildingsRoot}/Keep";
        private int upgrade;
        private bool refresh = false;

        public override void Apply(GameObject obj, KeepSkin skin)
        {
            base.Apply(obj, skin);

            if (skin.keepUpgrade1)
            {
                GameObject found = obj.transform.Find("Offset/SmallerKeep").gameObject;
                found.transform.localScale = Vector3.one;
                found.GetComponent<MeshFilter>().mesh = null;
                found.ClearChildren();
                if (skin.keepUpgrade1.GetComponent<MeshFilter>())
                    found.GetComponent<MeshCollider>().sharedMesh = skin.keepUpgrade1.GetComponent<MeshFilter>().sharedMesh;
                GameObject.Instantiate(skin.keepUpgrade1, found.transform).name = "inst";
            }
            if (skin.keepUpgrade2)
            {
                GameObject found = obj.transform.Find("Offset/SmallKeep").gameObject;
                found.transform.localScale = Vector3.one;
                found.GetComponent<MeshFilter>().mesh = null;
                found.ClearChildren();
                if (skin.keepUpgrade1.GetComponent<MeshFilter>())
                    found.GetComponent<MeshCollider>().sharedMesh = skin.keepUpgrade1.GetComponent<MeshFilter>().sharedMesh;
                GameObject.Instantiate(skin.keepUpgrade1, found.transform).name = "inst";
            }
            if (skin.keepUpgrade3)
            {
                GameObject found = obj.transform.Find("Offset/Keep").gameObject;
                found.transform.localScale = Vector3.one;
                found.GetComponent<MeshFilter>().mesh = null;
                found.ClearChildren();
                if (skin.keepUpgrade1.GetComponent<MeshFilter>())
                    found.GetComponent<MeshCollider>().sharedMesh = skin.keepUpgrade1.GetComponent<MeshFilter>().sharedMesh;
                GameObject.Instantiate(skin.keepUpgrade1, found.transform).name = "inst";
            }
            if (skin.keepUpgrade4)
            {
                GameObject found = obj.transform.Find("Offset/MediumKeep").gameObject;
                found.transform.localScale = Vector3.one;
                found.GetComponent<MeshFilter>().mesh = null;
                found.ClearChildren();
                if (skin.keepUpgrade1.GetComponent<MeshFilter>())
                    found.GetComponent<MeshCollider>().sharedMesh = skin.keepUpgrade1.GetComponent<MeshFilter>().sharedMesh;
                GameObject.Instantiate(skin.keepUpgrade1, found.transform).name = "inst";
            }

            switch (upgrade)
            {
                case 1:
                    obj.transform.Find("Offset/SmallerKeep").gameObject.SetActive(true);
                    obj.transform.Find("Offset/SmallKeep").gameObject.SetActive(false);
                    obj.transform.Find("Offset/Keep").gameObject.SetActive(false);
                    obj.transform.Find("Offset/MediumKeep").gameObject.SetActive(false);
                    break;
                case 2:
                    obj.transform.Find("Offset/SmallerKeep").gameObject.SetActive(false);
                    obj.transform.Find("Offset/SmallKeep").gameObject.SetActive(true);
                    obj.transform.Find("Offset/Keep").gameObject.SetActive(false);
                    obj.transform.Find("Offset/MediumKeep").gameObject.SetActive(false);
                    break;
                case 3:
                    obj.transform.Find("Offset/SmallerKeep").gameObject.SetActive(false);
                    obj.transform.Find("Offset/SmallKeep").gameObject.SetActive(false);
                    obj.transform.Find("Offset/Keep").gameObject.SetActive(true);
                    obj.transform.Find("Offset/MediumKeep").gameObject.SetActive(false);
                    break;
                case 4:
                    obj.transform.Find("Offset/SmallerKeep").gameObject.SetActive(false);
                    obj.transform.Find("Offset/SmallKeep").gameObject.SetActive(false);
                    obj.transform.Find("Offset/Keep").gameObject.SetActive(false);
                    obj.transform.Find("Offset/MediumKeep").gameObject.SetActive(true);
                    break;
            }

            refresh = false;
        }

        public override void UI()
        {
            base.UI();

            EditorGUILayout.LabelField("Preview Keep Upgrade: ");
            this.upgrade = EditorGUILayout.IntSlider(upgrade, 1, 4);

            refresh = EditorGUILayout.Toggle("Refresh", refresh);
        }
    }

    #endregion
}

#endif