using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Keep

    public class KeepPreview : BuildingPreview<KeepSkin>
    {
        public override string path => "Reskin Engine/Buildings/Keep";
        private int upgrade;
        private bool refresh = false;

        public override void Apply(GameObject obj, KeepSkin skin)
        {
            base.Apply(obj, skin);

            if (skin.keepUpgrade1)
            {
                GameObject.DestroyImmediate(obj.transform.Find("Offset/SmallerKeep").gameObject);
                GameObject.Instantiate(skin.keepUpgrade1, obj.transform.Find("Offset")).name = "SmallerKeep";
            }
            if (skin.keepUpgrade2)
            {
                GameObject.DestroyImmediate(obj.transform.Find("Offset/SmallKeep").gameObject);
                GameObject.Instantiate(skin.keepUpgrade2, obj.transform.Find("Offset")).name = "SmallKeep";
            }
            if (skin.keepUpgrade3)
            {
                GameObject.DestroyImmediate(obj.transform.Find("Offset/Keep").gameObject);
                GameObject.Instantiate(skin.keepUpgrade3, obj.transform.Find("Offset")).name = "Keep";
            }
            if (skin.keepUpgrade4)
            {
                GameObject.DestroyImmediate(obj.transform.Find("Offset/MediumKeep").gameObject);
                GameObject.Instantiate(skin.keepUpgrade4, obj.transform.Find("Offset")).name = "MediumKeep";
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