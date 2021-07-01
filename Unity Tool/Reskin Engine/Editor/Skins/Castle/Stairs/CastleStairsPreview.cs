using UnityEngine;
using UnityEditor;
using ReskinEngine.Utils;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Castle Stairs

    public class CastleStairsPreview : BuildingPreview<CastleStairsSkin>
    {
        public enum Direction
        {
            Front, // +z
            Right, // +x
            Down,  // -z
            Left   // -x
        }

        public Direction direction;

        public override string path => $"{buildingsRoot}/CastleStairs";

        public override void Apply(GameObject obj, CastleStairsSkin skin)
        {
            base.Apply(obj, skin);

            GameObject stairs1 = obj.transform.Find("Offset/stairs1").gameObject;
            GameObject stairs2 = obj.transform.Find("Offset/stairs2").gameObject;
            GameObject stairs3 = obj.transform.Find("Offset/stairs3").gameObject;
            GameObject stairs4 = obj.transform.Find("Offset/stairs4").gameObject;

            if (skin.stairsFront && skin.stairsFront.GetComponent<MeshFilter>())
                stairs1.GetComponent<MeshFilter>().mesh = skin.stairsFront.GetComponent<MeshFilter>().mesh;
            if (skin.stairsRight && skin.stairsRight.GetComponent<MeshFilter>())
                stairs2.GetComponent<MeshFilter>().mesh = skin.stairsRight.GetComponent<MeshFilter>().mesh;
            if (skin.stairsDown && skin.stairsDown.GetComponent<MeshFilter>())
                stairs3.GetComponent<MeshFilter>().mesh = skin.stairsDown.GetComponent<MeshFilter>().mesh;
            if (skin.stairsLeft && skin.stairsLeft.GetComponent<MeshFilter>())
                stairs4.GetComponent<MeshFilter>().mesh = skin.stairsLeft.GetComponent<MeshFilter>().mesh;

            Vector3 dir = Vector3.zero;

            switch (direction)
            {
                case Direction.Front:
                    stairs1.SetActive(true);
                    stairs2.SetActive(false);
                    stairs3.SetActive(false);
                    stairs4.SetActive(false);
                    dir = new Vector3(0f, 0f, 1f);
                    break;
                case Direction.Right:
                    stairs1.SetActive(false);
                    stairs2.SetActive(true);
                    stairs3.SetActive(false);
                    stairs4.SetActive(false);
                    dir = new Vector3(1f, 0f, 0f);
                    break;
                case Direction.Down:
                    stairs1.SetActive(false);
                    stairs2.SetActive(false);
                    stairs3.SetActive(true);
                    stairs4.SetActive(false);
                    dir = new Vector3(0f, 0f, -1f);
                    break;
                case Direction.Left:
                    stairs1.SetActive(false);
                    stairs2.SetActive(false);
                    stairs3.SetActive(false);
                    stairs4.SetActive(true);
                    dir = new Vector3(-1f, 0f, 0f);
                    break;
            }

            GameObject container = obj.transform.Find("markerContainer") != null ? obj.transform.Find("markerContainer").gameObject : new GameObject("markerContainer");
            if (!container.transform.parent == obj)
                container.transform.SetParent(obj.transform);

            container.transform.position = Vector3.one / 2f;
            container.ClearChildren();
            GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            marker.transform.SetParent(container.transform, true);
            marker.transform.localPosition = dir;
            marker.transform.localScale = Vector3.one / 2f;

            string warnings = ModularCheckWarnings(false, skin.stairsFront, skin.stairsRight, skin.stairsDown, skin.stairsLeft);
            if(warnings != "")
                Debug.Log(warnings);
        }

        public override void UI()
        {
            base.UI();

            direction = (Direction)EditorGUILayout.IntSlider(direction.ToString(), (int)direction, 0, 3);


        }
    }

    #endregion
}

#endif