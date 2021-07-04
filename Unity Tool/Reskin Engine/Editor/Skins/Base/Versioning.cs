using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{

    [Flags]
    public enum GameVersion
    {
        STABLE = 1,
        ALPHA = 2,
        COMMON = 4
    }

    public static class Versioning
    {
        public static GameVersion common { get; } = GameVersion.STABLE | GameVersion.ALPHA | GameVersion.COMMON;
    }

    public static class Settings
    {
        public enum PreferredOptionType{
            List,
            SelectionGrid
        }

        public static GameVersion version = GameVersion.STABLE;

        public static PreferredOptionType preferredOptionType = PreferredOptionType.SelectionGrid;

    }    

    public class SettingsPanel : EditorWindow
    {
        [MenuItem("Reskin Engine/Settings")]
        public static void Open()
        {
            EditorWindow window = EditorWindow.CreateInstance<SettingsPanel>();
            window.Show();
            window.minSize = new Vector2(300, 100);
            window.maxSize = new Vector2(500, 400);

            window.title = "Reskin Engine Settings";
        }

        void OnGUI()
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Settings");

            EditorGUILayout.Separator();

            Settings.version = (GameVersion)EditorGUILayout.EnumPopup("Game Version", Settings.version);
            Settings.preferredOptionType = (Settings.PreferredOptionType)EditorGUILayout.EnumPopup("Preferred Option List Type", Settings.preferredOptionType);

        }


    }
}

#endif