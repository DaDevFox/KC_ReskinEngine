using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Gates

    #region Base

    public abstract class GatePreview<T> : BuildingPreview<T> where T : GateSkin
    {
        public override string path { get; } = "Reskin Engine/Buildings/WoodCastleBlock";

        public GameObject defaultGateMesh;
        public GameObject defaultPorticulusMesh;

        private bool open = true;

        public override void Apply(GameObject obj, T skin)
        {
            base.Apply(obj, skin);


            GameObject gateObj = obj.transform.Find("Offset/Gate").gameObject;
            GameObject porticulusObj = obj.transform.Find("Offset/Portcullis").gameObject;

            if (skin.gate)
                gateObj.GetComponent<MeshFilter>().mesh = skin.gate.GetComponent<MeshFilter>().mesh;

            if (skin.porticulus)
                porticulusObj.GetComponent<MeshFilter>().mesh = skin.porticulus.GetComponent<MeshFilter>().mesh;

            if (open)
                porticulusObj.transform.localPosition = new Vector3(0f, 1.1f, 0f);
            else
                porticulusObj.transform.localPosition = new Vector3(0f, 0.5f, 0f);
        }

        public override void UI()
        {
            base.UI();

            open = EditorGUILayout.Toggle("Open", open);
        }
    }

    #endregion

    #endregion
}

#endif