// #define DEV_MODE

using System;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR

using UnityEditor;

#endif

namespace ReskinEngine.Editor.Utils
{
    public class APIFolderSync
    {
        private static TextAsset versionFile = null;

        public static bool devMode { get; set; } = false;


#if UNITY_EDITOR

#if DEV_MODE

        [MenuItem("Reskin Engine/Parse API Folder to Text")]
        public static void ParseAPIFolderToText(){
            versionFile = Resources.Load<TextAsset>(Path.Combine(ModBuilder.APIFolderLocation, "version"));
            string versionFilePath = Path.Combine(Path.GetDirectoryName(Application.dataPath), AssetDatabase.GetAssetPath(versionFile));
            string directoryPath = Path.GetDirectoryName(versionFilePath);

            string[] files = Directory.GetFiles(directoryPath, "*.cs", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                Debug.Log($"replacing [{file}]");
                //test
                File.Copy(file, file + ".txt");
                File.Delete(file);
            }
        }

        [MenuItem("Reskin Engine/Pull API Folder")]
        public static void PullAPIFolder(){

        }


#endif

#endif




    }
}