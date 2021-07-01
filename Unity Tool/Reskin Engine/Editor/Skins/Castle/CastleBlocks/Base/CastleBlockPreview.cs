using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Castle Blocks
    #region Base

    public abstract class CastleBlockPreview<T> : BuildingPreview<T> where T : CastleBlockSkin
    {
        public sealed override string path { get; } = $"{buildingsRoot}/WoodCastleBlock";

        private CastleBlockType _selected = CastleBlockType.Open;

        

        public abstract string OpenPath { get; }
        public abstract string ClosedPath { get; }
        public abstract string SinglePath { get; }
        public abstract string OppositePath { get; }
        public abstract string AdjacentPath { get; }
        public abstract string ThreesidePath { get; }
        public abstract string DoorPath { get; }

        public override void Apply(GameObject obj, T skin)
        {
            base.Apply(obj, skin);

            MeshFilter mesh = obj.transform.Find("Offset/1x1x5_wood").GetComponent<MeshFilter>();
            MeshRenderer renderer = obj.transform.Find("Offset/1x1x5_wood").GetComponent<MeshRenderer>();
            Material baseMat;

            Mesh open = skin.Open ? skin.Open.GetComponent<MeshFilter>().sharedMesh : Resources.Load<Mesh>(OpenPath);
            Mesh closed = skin.Closed ? skin.Closed.GetComponent<MeshFilter>().sharedMesh : Resources.Load<Mesh>(ClosedPath);
            Mesh single = skin.Single ? skin.Single.GetComponent<MeshFilter>().sharedMesh : Resources.Load<Mesh>(SinglePath);
            Mesh opposite = skin.Opposite ? skin.Opposite.GetComponent<MeshFilter>().sharedMesh : Resources.Load<Mesh>(OppositePath);
            Mesh adjacent = skin.Adjacent ? skin.Adjacent.GetComponent<MeshFilter>().sharedMesh : Resources.Load<Mesh>(AdjacentPath);
            Mesh threeside = skin.Threeside ? skin.Threeside.GetComponent<MeshFilter>().sharedMesh : Resources.Load<Mesh>(ThreesidePath);
            GameObject door = skin.doorPrefab ? skin.doorPrefab : Resources.Load<GameObject>(DoorPath);

            switch (_selected)
            {
                case CastleBlockType.Open:
                    if (skin.Open || mesh.sharedMesh != open)
                    {
                        mesh.sharedMesh = open;
                    }
                                        
                    break;
                case CastleBlockType.Closed:
                    if (skin.Closed || mesh.sharedMesh != closed)
                        mesh.sharedMesh = closed;
                    break;
                case CastleBlockType.Single:
                    if (skin.Single || mesh.sharedMesh != single)
                        mesh.sharedMesh = single;
                    break;
                case CastleBlockType.Opposite:
                    if (skin.Opposite || mesh.sharedMesh != single)
                        mesh.sharedMesh = opposite;
                    break;
                case CastleBlockType.Adjacent:
                    if (skin.Adjacent || mesh.sharedMesh != adjacent)
                        mesh.sharedMesh = adjacent;
                    break;
                case CastleBlockType.Threeside:
                    if (skin.Threeside || mesh.sharedMesh != threeside)
                        mesh.sharedMesh = threeside;
                    break;
            }
        }

        public override void UI()
        {
            base.UI();

            _selected = (CastleBlockType)EditorGUILayout.IntSlider($"Block Type: {_selected.ToString()}", (int)_selected, 0, (int)CastleBlockType.NumTypes - 1);
        }
    }

    #endregion

    #endregion
}

#endif