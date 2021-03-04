using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ReskinEngine.Editor.Utils
{
    #if UNITY_EDITOR

    public class AssetBundles : EditorWindow
    {
        public static string assetBundleBuildPath = "Assets/AssetBundles";

        //// Add menu item
        //[MenuItem("KC Toolkit/Build Asset Bundle")]
        //static void Init()
        //{
        //    EditorWindow window = EditorWindow.CreateInstance<AssetBundles>();
        //    window.Show();
        //}

        [MenuItem("KC Toolkit/Build AssetBundles from Selected")]
        private static void BuildBundlesFromSelection()
        {
            // Get all selected *assets*
            var assets = Selection.objects.Where(o => !string.IsNullOrEmpty(AssetDatabase.GetAssetPath(o))).ToArray();

            List<AssetBundleBuild> assetBundleBuilds = new List<AssetBundleBuild>();
            HashSet<string> processedBundles = new HashSet<string>();

            if (assets == null || assets.Length == 0)
            {
                Debug.Log("No AssetBundles found in selection");
                return;
            }

            // Get asset bundle names from selection
            foreach (var o in assets)
            {
                var assetPath = AssetDatabase.GetAssetPath(o);
                var importer = AssetImporter.GetAtPath(assetPath);

                if (importer == null)
                {
                    continue;
                }

                // Get asset bundle name & variant
                var assetBundleName = importer.assetBundleName;
                var assetBundleVariant = importer.assetBundleVariant;
                var assetBundleFullName = string.IsNullOrEmpty(assetBundleVariant) ? assetBundleName : assetBundleName + "." + assetBundleVariant;

                // Only process assetBundleFullName once. No need to add it again.
                if (processedBundles.Contains(assetBundleFullName))
                {
                    continue;
                }

                processedBundles.Add(assetBundleFullName);

                AssetBundleBuild build = new AssetBundleBuild();

                build.assetBundleName = assetBundleName;
                build.assetBundleVariant = assetBundleVariant;
                build.assetNames = AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleFullName);

                assetBundleBuilds.Add(build);
            }

            BuildAssetBundles(assetBundleBuilds.ToArray());
        }


        public static void BuildAssetBundles(AssetBundleBuild[] builds, string path = "")
        {
            string defaultPath = assetBundleBuildPath;

            BuildTarget[] targets = new BuildTarget[] {
            BuildTarget.StandaloneWindows64,
            BuildTarget.StandaloneWindows,
            BuildTarget.StandaloneOSX,
            BuildTarget.StandaloneLinux64 };

            foreach (BuildTarget target in targets)
            {
                var options = BuildAssetBundleOptions.AppendHashToAssetBundleName;

                bool shouldCheckODR = EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS;
#if UNITY_TVOS
            shouldCheckODR |= EditorUserBuildSettings.activeBuildTarget == BuildTarget.tvOS;
#endif
                if (shouldCheckODR)
                {
#if ENABLE_IOS_ON_DEMAND_RESOURCES
                if (PlayerSettings.iOS.useOnDemandResources)
                    options |= BuildAssetBundleOptions.UncompressedAssetBundle;
#endif
#if ENABLE_IOS_APP_SLICING
                options |= BuildAssetBundleOptions.UncompressedAssetBundle;
#endif
                }

                if (builds == null || builds.Length == 0)
                {
                    //@TODO: use append hash... (Make sure pipeline works correctly with it.)
                    BuildPipeline.BuildAssetBundles(defaultPath, options, target);
                }
                else
                {
                    var built = new List<string>();


                    // Choose the output path according to the build target.
                    string outputPath = CreateAssetBundleDirectory(path == "" ? assetBundleBuildPath : path, target);

                    BuildPipeline.BuildAssetBundles(outputPath, builds, options, EditorUserBuildSettings.activeBuildTarget);


                    foreach (AssetBundleBuild build in builds)
                    {
                        string fullName = string.IsNullOrEmpty(build.assetBundleVariant) ? build.assetBundleName : build.assetBundleName + "." + build.assetBundleVariant;
                        if (built.Contains(fullName))
                            continue;

                        built.Add(fullName);
                    }
                }
            }
        }

        public static string CreateAssetBundleDirectory(string path, BuildTarget target)
        {
            //string buildPath = string.IsNullOrEmpty(build.assetBundleVariant) ? build.assetBundleName : Path.Combine(build.assetBundleName, build.assetBundleVariant);


            Dictionary<BuildTarget, string> targetPaths = new Dictionary<BuildTarget, string>()
        {
            { BuildTarget.StandaloneWindows64, "win64" },
            { BuildTarget.StandaloneWindows, "win32" },
            { BuildTarget.StandaloneOSX, "osx" },
            { BuildTarget.StandaloneLinux64, "linux" },
        };
            // Choose the output path according to the build target.
            string outputPath = Path.Combine(path, targetPaths[target]);
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            return outputPath;
        }

        static void BuildMapABs()
        {

            var win32Path = Path.Combine(Application.dataPath, "Workspace/win32");
            if (!Directory.Exists(win32Path))
                Directory.CreateDirectory(win32Path);
            BuildPipeline.BuildAssetBundles(win32Path, BuildAssetBundleOptions.AppendHashToAssetBundleName, BuildTarget.StandaloneWindows);

            var win64Path = Path.Combine(Application.dataPath, "Workspace/win64");
            if (!Directory.Exists(win64Path))
                Directory.CreateDirectory(win64Path);
            BuildPipeline.BuildAssetBundles(win64Path, BuildAssetBundleOptions.AppendHashToAssetBundleName, BuildTarget.StandaloneWindows64);

            var osxPath = Path.Combine(Application.dataPath, "Workspace/osx");
            if (!Directory.Exists(osxPath))
                Directory.CreateDirectory(osxPath);
            BuildPipeline.BuildAssetBundles(osxPath, BuildAssetBundleOptions.AppendHashToAssetBundleName, BuildTarget.StandaloneOSX);

            var linuxPath = Path.Combine(Application.dataPath, "Workspace/linux");
            if (!Directory.Exists(linuxPath))
                Directory.CreateDirectory(linuxPath);
            BuildPipeline.BuildAssetBundles(linuxPath, BuildAssetBundleOptions.AppendHashToAssetBundleName, BuildTarget.StandaloneLinuxUniversal);
        }

        //Example of using a custom asset bundle definition
        static void CustomAssetBundles()
        {
            // Create the array of bundle build details.
            //AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

            //string[] hats = new string[16];
            //hats[0] = "Assets/Workspace/Hats/BrownHat.prefab";
            //hats[1] = "Assets/Workspace/Hats/BrownNewsboyHat.prefab";
            //hats[2] = "Assets/Workspace/Hats/FarmerGreenHat.prefab";
            //hats[3] = "Assets/Workspace/Hats/FarmerPlainHat.prefab";
            //hats[4] = "Assets/Workspace/Hats/GrayNewsboyHat.prefab";
            //hats[5] = "Assets/Workspace/Hats/PurpleLadyHat.prefab";
            //hats[6] = "Assets/Workspace/Hats/TopHat.prefab";
            //hats[7] = "Assets/Workspace/Hats/WhitePinkLadyHat.prefab";

            //hats[8] = "Assets/Workspace/Hats/HatBlack.material";
            //hats[9] = "Assets/Workspace/Hats/HatBrown.material";
            //hats[10] = "Assets/Workspace/Hats/HatGray.material";
            //hats[11] = "Assets/Workspace/Hats/HatGreen.material";
            //hats[12] = "Assets/Workspace/Hats/HatPink.material";
            //hats[13] = "Assets/Workspace/Hats/HatPurple.material";
            //hats[14] = "Assets/Workspace/Hats/HatStraw.material";
            //hats[15] = "Assets/Workspace/Hats/HatWhite.material";

            //buildMap[0].assetNames = hats;

            //buildMap[0].assetBundleName = "hats_windows32";
            //BuildPipeline.BuildAssetBundles("Assets/Workspace", buildMap, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

            //buildMap[0].assetBundleName = "hats_windows64";
            //BuildPipeline.BuildAssetBundles("Assets/Workspace", buildMap, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

            //buildMap[0].assetBundleName = "hats_osx";
            //BuildPipeline.BuildAssetBundles("Assets/Workspace", buildMap, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);

            //buildMap[0].assetBundleName = "hats_linux";
            //BuildPipeline.BuildAssetBundles("Assets/Workspace", buildMap, BuildAssetBundleOptions.None, BuildTarget.StandaloneLinuxUniversal);
        }

        ////string bundleName;
        //Rect buttonRect;
        //void OnGUI()
        //{
        //    {
        //        GUILayout.Label("Asset Bundle Build Window", EditorStyles.boldLabel);
        //        if (GUILayout.Button("Learn about configuring asset bundles", GUILayout.Width(300)))
        //        {
        //            Application.OpenURL(@"https://docs.unity3d.com/2018.2/Documentation/Manual/AssetBundles-Workflow.html");
        //        }
        //        GUILayout.Space(50);

        //        GUILayout.Label("Build asset bundles as defined in the editor:", EditorStyles.wordWrappedLabel);
        //        //bundleName = GUILayout.TextField(bundleName);
        //        if (GUILayout.Button("Build Asset Bundles", GUILayout.Width(300)))
        //        {
        //            BuildMapABs();
        //        }
        //        //GUILayout.Space(40);
        //        //GUILayout.Label("If you want more control over your asset bundles, you can define the function CustomAssetBundles in ToolkitTools/BuildAssetBundle.cs. Take a peak at that function to see how. The button below calls that function:", EditorStyles.wordWrappedLabel);
        //        //if (GUILayout.Button("Build Custom Asset Bundles", GUILayout.Width(300)))
        //        //{
        //        //    CustomAssetBundles();
        //        //}
        //        if (Event.current.type == EventType.Repaint) buttonRect = GUILayoutUtility.GetLastRect();
        //    }
        //}
    }

    #endif

}
