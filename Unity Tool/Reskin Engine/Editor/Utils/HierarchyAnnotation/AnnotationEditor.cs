using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Annotation), true)]
public class AnnotationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Space(2);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField((target as Annotation).annotation, new GUIStyle()
        {
            richText = true,

            fontSize = 12,
            wordWrap = true
        });

        EditorGUILayout.Separator();
        GUILayout.Space(2);

        base.OnInspectorGUI();
    }
}

#endif