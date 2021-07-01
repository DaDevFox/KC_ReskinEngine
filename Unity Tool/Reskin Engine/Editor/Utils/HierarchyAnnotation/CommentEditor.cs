using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Comment))]
public class CommentEditor : Editor{
    public override void OnInspectorGUI()
    {
        GUILayout.Space(2);
        EditorGUILayout.Separator();

        GUILayout.Label((target as Comment).comment, new GUIStyle()
        {
            richText = true,

            fontSize = 14
        });

        EditorGUILayout.Separator();
        GUILayout.Space(2);

        base.OnInspectorGUI();
    }

    public override string GetInfoString()
    {
        return "asdf";
    }

    protected override void OnHeaderGUI()
    {
        base.OnHeaderGUI();

        GUILayout.Label("asdf");
    }

    public override string ToString()
    {
        return "asdf";
    }

    public override GUIContent GetPreviewTitle()
    {
        GUIContent _base = base.GetPreviewTitle();
        _base.text = "asdf";
        return _base;
    }
}

#endif