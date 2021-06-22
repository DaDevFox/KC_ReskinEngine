using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using ReskinEngine.Editor.Utils;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public static class Util
    {
        public static string FromCamelCase(string original)
        {
            string _new = "";
            for(int i = 0; i < original.Length; i++)
            {
                if(i != 0)
                {
                    if (char.IsUpper(original[i]))
                        _new += " " + original[i];
                    else
                        _new += original[i];
                }
            }

            return _new;
        }

        public static string SnakeCase(string original)
        {
            return original.Replace(' ', '_');
        }

        public static string AssetBundleName(string original)
        {
            return original.ToLower();
        }
    }

    public class ModBuilder
    {
        #region Constants

        public static string APIFolderLocation { get; } = "Reskin Engine/API";
        private static int indent = 0;
        private static string _t 
        {
            get
            {
                String full = "";
                for (int i = 0; i < indent; i++)
                    full += "\t";
                return full;
            }
        }

        #endregion


        #region Class Frame

        private static string Usings()
        {
            return $"using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Text;\nusing System.Threading.Tasks;\nusing UnityEngine;\nusing ReskinEngine.API;";
        }

        private static string Namespace(Mod mod)
        {
            return $"\nnamespace {mod.@namespace}" + "\n{";
        }

        private static string Class() => $"{_t}public class Mod\n{_t}{{";
        

        private static string CloseNamespace() => ClosingBrace();

        private static string CloseClass() => $"{_t}{ClosingBrace()}";

        private static string ClosingBrace() => "}";


        #endregion

        #region Class Body

        public static string ClassVars() => $"{_t}public static KCModHelper helper;\n";

        public static string OpenMainMethod() => $"{_t}public void SceneLoaded(KCModHelper helper)\n{_t}{{";

        public static string MainMethodInit(Mod mod) => $"{_t}// KCModHelper\n{_t}Mod.helper = helper;\n\n{_t}// Setup the ReskinProfile with a name and compatability identifier\n{_t}ReskinProfile profile = new ReskinProfile(\"{mod.modName}\", \"{mod.compatabilityIdentifier}\");\n";

        public static string OpenTryCatch() => $"\n{_t}try{{\n";

        public static string MainMethodConclude() => $"\n{_t}profile.Register();\n{_t}helper.Log(\"Init\");";


        public static string CloseTryCatch() => $"\n\n{_t}}}catch(Exception ex){{\n{_t}\thelper.Log(ex.ToString());\n{_t}}}";

        public static string CloseMainMethod() => $"\n{_t}}}";

        #region Collections

        public static string WriteCollection(Mod mod, Collection collection) {
            string assetBundleName = Util.AssetBundleName($"{mod.name}_{collection.name}");
            string bundleVarName = $"{collection.name}_bundle";

            string text = $"{_t}#region {collection.name}\n\n{_t}AssetBundle {bundleVarName} = KCModHelper.LoadAssetBundle(helper.modPath + \"/assetbundle/\", \"{assetBundleName}\");";

            text += "\n";

            // iterate skins
            foreach (Skin skin in collection.skins)
            {
                // import assets
                foreach(FieldInfo field in skin.GetType().GetFields())
                {
                    string fieldType = field.FieldType.Name;
                    if (field.GetValue(skin) as UnityEngine.Object != null)
                    {
                        UnityEngine.Object value = field.GetValue(skin) as UnityEngine.Object;
                        text += $"\n{_t}// {skin.name} \n{_t}{fieldType} {skin.typeId}_{skin.name}_{field.Name} = {bundleVarName}.LoadAsset<{fieldType}>(\"{AssetDatabase.GetAssetPath(value)}\");";
                    }
                }

                // TODO: protection against naming a skin a type name eg KeepSkin as name of a KeepSkin

                string skinAPIEquivalent = skin.GetType().Name;
                string skinName = Util.SnakeCase(skin.name);

                // create skin
                text += $"\n{_t}{skinAPIEquivalent} {skinName} = new {skinAPIEquivalent}();\n";

                // assign assets to fields
                foreach (FieldInfo field in skin.GetType().GetFields())
                {
                    string fieldType = field.FieldType.Name;
                    // if it's a UnityEngine.Object, it got packaged into the assetbundle so unpack it from there
                    if (field.GetValue(skin) as UnityEngine.Object != null)
                        text += $"{_t}{skinName}.{field.Name} = {skin.typeId}_{skin.name}_{field.Name};\n";
                    // strings can be directly transfered
                    if (field.GetValue(skin) is String)
                        text += $"{_t}{skinName}.{field.Name} = \"{field.GetValue(skin)}\";\n";
                }


                // BuildingSkin only
                if (skin as BuildingSkin != null)
                {
                    BuildingSkin bSkin = skin as BuildingSkin;

                    #region Person Positions

                    string personPositions = "";
                    if (bSkin.personPositions != null && bSkin.personPositions.Length > 0)
                    {
                        personPositions = $"new Vector3[{bSkin.personPositions.Length}] {{";
                        for (int i = 0; i < bSkin.personPositions.Length; i++)
                        {
                            Vector3 vector = bSkin.personPositions[i];
                            personPositions += $"new Vector3({vector.x}f, {vector.y}f, {vector.z}f)";
                            if (i != bSkin.personPositions.Length - 1)
                                personPositions += ", ";
                        }
                        personPositions += "}";
                    }
                    else
                    {
                        personPositions = "new Vector3[0]";
                    }

                    text += $"\n{_t}{skinName}.personPositions = {personPositions};";

                    #endregion

                    #region Path Lists

                    // Outline Meshes
                    string outlineMeshes = "";
                    if (bSkin.outlineMeshes != null && bSkin.outlineMeshes.Length > 0)
                    {
                        outlineMeshes = $"new string[{bSkin.outlineMeshes.Length}] {{";
                        for(int i = 0; i < bSkin.outlineMeshes.Length; i++)
                        {
                            string path = bSkin.outlineMeshes[i];
                            outlineMeshes += $"\"{path}\"";
                            if (i != bSkin.outlineMeshes.Length - 1)
                                outlineMeshes += ", ";
                        }
                        outlineMeshes += "}";
                    }
                    else
                    {
                        outlineMeshes = "new string[0]";
                    }

                    text += $"\n{_t}{skinName}.outlineMeshes = {outlineMeshes};";

                    // Skinned Outline Meshes
                    string outlineSkinnedMeshes = "";
                    if (bSkin.outlineSkinnedMeshes != null && bSkin.outlineSkinnedMeshes.Length > 0)
                    {
                        outlineSkinnedMeshes = $"new string[{bSkin.outlineSkinnedMeshes.Length}] {{";
                        for (int i = 0; i < bSkin.outlineSkinnedMeshes.Length; i++)
                        {
                            string path = bSkin.outlineSkinnedMeshes[i];
                            outlineSkinnedMeshes += $"\"{path}\"";
                            if (i != bSkin.outlineSkinnedMeshes.Length - 1)
                                outlineSkinnedMeshes += ", ";
                        }
                        outlineSkinnedMeshes += "}";
                    }
                    else
                    {
                        outlineSkinnedMeshes = "new string[0]";
                    }

                    text += $"\n{_t}{skinName}.outlineSkinnedMeshes = {outlineSkinnedMeshes};";

                    // Colliders
                    string colliders = "";
                    if (bSkin.colliders != null && bSkin.colliders.Length > 0)
                    {
                        colliders = $"new string[{bSkin.colliders.Length}] {{";
                        for (int i = 0; i < bSkin.colliders.Length; i++)
                        {
                            string path = bSkin.colliders[i];
                            colliders += $"\"{path}\"";
                            if (i != bSkin.colliders.Length - 1)
                                colliders += ", ";
                        }
                        colliders += "}";
                    }
                    else
                    {
                        colliders = "new string[0]";
                    }

                    text += $"\n{_t}{skinName}.colliders = {colliders};";

                    // Building shader renderers
                    string renderersWithBuildingShader = "";
                    if (bSkin.renderersWithBuildingShader != null && bSkin.renderersWithBuildingShader.Length > 0)
                    {
                        renderersWithBuildingShader = $"new string[{bSkin.renderersWithBuildingShader.Length}] {{";
                        for (int i = 0; i < bSkin.renderersWithBuildingShader.Length; i++)
                        {
                            string path = bSkin.renderersWithBuildingShader[i];
                            renderersWithBuildingShader += $"\"{path}\"";
                            if (i != bSkin.renderersWithBuildingShader.Length - 1)
                                renderersWithBuildingShader += ", ";
                        }
                        renderersWithBuildingShader += "}";
                    }
                    else
                    {
                        renderersWithBuildingShader = "new string[0]";
                    }

                    text += $"\n{_t}{skinName}.renderersWithBuildingShader = {renderersWithBuildingShader};";

                    #endregion

                }

                // register skin
                text += $"\n{_t}profile.Add({skinName});\n";

            }

            text += $"\n{_t}#endregion\n";

            return text;
        }





        #endregion

        #endregion

        [MenuItem("Test/Build Mods from selected")]
        public static void TestBuild()
        {
            var mods = from UnityEngine.Object obj in Selection.objects
                         where obj is Mod
                         select obj as Mod;

            foreach(Mod m in mods)
            {
                Build(m);
            }
        }


        public static void Build(Mod mod)
        {
            string outputPath = mod.outputPath == "default" ? Path.GetDirectoryName(AssetDatabase.GetAssetPath(mod)) : mod.outputPath;
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            CreateAssetBundle(mod, outputPath);
            CreateReskinLibrary(mod, outputPath);
            CreateModFile(mod, outputPath);
        }

        #region Files

        #region Dependencies

        private static void CreateAssetBundle(Mod mod, string outputPath)
        {
            if (!Directory.Exists(outputPath + "/assetbundle"))
                Directory.CreateDirectory(outputPath + "/assetbundle");

            List<AssetBundleBuild> builds = new List<AssetBundleBuild>();

            foreach(Collection collection in mod.collections)
            {
                string collectionBundleName = Util.AssetBundleName($"{mod.name}_{collection.name}");

                // remove previous things tagged with asset bundle
                string[] paths = AssetDatabase.GetAssetPathsFromAssetBundle(collectionBundleName);
                foreach (string path in paths)
                    AssetImporter.GetAtPath(path).assetBundleName = "";

                // add all skin dependencies to asset bundle
                foreach (Skin skin in collection.skins)
                {
                    foreach (FieldInfo field in skin.GetType().GetFields())
                    {
                        if (field.GetValue(skin) as UnityEngine.Object != null)
                        {
                            UnityEngine.Object asset = field.GetValue(skin) as UnityEngine.Object;
                            string path = AssetDatabase.GetAssetPath(asset);
                            AssetImporter importer = AssetImporter.GetAtPath(path);

                            importer.assetBundleName = collectionBundleName;
                        }
                    }
                }

                // queue bundle for building
                builds.Add(new AssetBundleBuild() 
                { 
                    assetBundleName = $"{mod.name}_{collection.name}",
                    assetBundleVariant = "",
                    assetNames = AssetDatabase.GetAssetPathsFromAssetBundle(collectionBundleName)
                });
            }

            // build all asset bundles
            AssetBundles.BuildAssetBundles(builds.ToArray(), outputPath + "/assetbundle/");
        }

        private static void CreateReskinLibrary(Mod mod, string outputPath)
        {
            if(!Directory.Exists(outputPath + "/API"))
                Directory.CreateDirectory(outputPath + "/API");

            TextAsset[] assets = Resources.LoadAll<TextAsset>(APIFolderLocation);
            
            foreach(TextAsset sourceFile in assets)
            {
                string properExtension = ".cs";

                if(sourceFile.name == "version")
                    properExtension = ".txt";

                string relativePath = Path.GetFileNameWithoutExtension(sourceFile.name);
                FileStream stream = File.Create($"{outputPath}/API/{relativePath}{properExtension}");
                stream.Write(sourceFile.bytes, 0, sourceFile.bytes.Length);
                stream.Close();
            }
        }

        #endregion
    
        #region Main File

        private static void CreateModFile(Mod mod, string outputPath)
        {
            StreamWriter script = File.CreateText(Path.Combine(outputPath, "Mod") + ".cs");

            #region Heading

            script.WriteLine(Usings());

            indent = 1;
            script.WriteLine(Namespace(mod));
            script.WriteLine(Class());

            #endregion

            #region Body

            indent = 2;
            script.WriteLine(ClassVars());
            script.WriteLine(OpenMainMethod());

            indent = 3;
            script.WriteLine(OpenTryCatch());

            indent = 4;
            script.WriteLine(MainMethodInit(mod));

            foreach (Collection collection in mod.collections)
                script.WriteLine(WriteCollection(mod, collection));


            script.WriteLine(MainMethodConclude());

            indent = 3;
            script.WriteLine(CloseTryCatch());

            indent = 2;
            script.WriteLine(CloseMainMethod());

            #endregion

            #region Close Heading

            indent = 1;
            script.WriteLine(CloseClass());

            indent = 0;
            script.WriteLine(CloseNamespace());

            #endregion

            script.Close();
        }

        #endregion

        #endregion

    }

    [CustomEditor(typeof(Mod))]
    public class ModEditor : UnityEditor.Editor 
    {
        private static string APIVersionPath { get; } = "Reskin Engine/API/version";
        private static string PluginVersionPath { get; } = "Reskin Engine/version";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Label(GetVersionText(), new GUIStyle() {
                richText = true
            });
            if (GUILayout.Button("build"))
            {
                ModBuilder.Build(target as Mod);
            }
        }

        private string GetVersionText()
        {
            TextAsset APIVersionDoc = Resources.Load<TextAsset>(APIVersionPath);
            string APIVersion = APIVersionDoc.text;


            TextAsset PluginVersionDoc = Resources.Load<TextAsset>(PluginVersionPath);
            string PluginVersion = PluginVersionDoc.text;

            string[] APIVersions = APIVersion.Split('\n');
            string minorVersionAPI = APIVersions[1].Trim().ToLower();
            string majorVersionAPI = APIVersions[0].Trim().ToLower();

            string[] PluginVersions = PluginVersion.Split('\n');
            string minorVersionPlugin = PluginVersions[1].Trim().ToLower();
            string majorVersionPlugin = PluginVersions[0].Trim().ToLower();

            string color = "blue";
            if(majorVersionAPI != majorVersionPlugin)
                color = "red";
            else if (minorVersionAPI != minorVersionPlugin)
                color = "#FF5900";

            string text = $"\nAPI Version: \n<b><color={color}>{APIVersion}</color></b>\nEditor Version: \n<b><color={color}>{PluginVersion}</color></b>";

            return text;
        }
    }
}


#endif