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

        private static string Class()
        {
            return "\tpublic class Mod\n\t{";
        }

        private static string CloseNamespace() => ClosingBrace();

        private static string CloseClass() => $"\t{ClosingBrace()}";

        private static string ClosingBrace() => "}";


        #endregion

        #region Class Body

        public static string ClassVars() => "\t\tpublic static KCModHelper helper;\n";

        public static string OpenMainMethod() => "\t\tpublic void SceneLoaded(KCModHelper helper)\n\t\t{";

        public static string MainMethodInit(Mod mod) => $"\t\t\t// KCModHelper\n\t\t\tMod.helper = helper;\n\n\t\t\t// Setup the ReskinProfile with a name and compatability identifier\n\t\t\tReskinProfile profile = new ReskinProfile(\"{mod.modName}\", \"{mod.compatabilityIdentifier}\");\n\t\t\ttry{{";

        public static string MainMethodConclude() => $"\n\t\t\tprofile.Register();\n\t\t\thelper.Log(\"Init\");";

        public static string CloseMainMethod() => "\n\t\t\t}catch(Exception ex){\n\t\t\t\thelper.Log(ex.ToString());\n\t\t\t}\n\t\t}";

        #region Collections

        public static string WriteCollection(Mod mod, Collection collection) {
            string assetBundleName = Util.AssetBundleName($"{mod.name}_{collection.name}");
            string bundleVarName = $"{collection.name}_bundle";

            string text = $"\t\t\t//{collection.name}\n\t\t\tAssetBundle {bundleVarName} = KCModHelper.LoadAssetBundle(helper.modPath + \"/assetbundle/\", \"{assetBundleName}\");";

            text += "\n\n\n";

            // iterate skins
            foreach (Skin skin in collection.implementedSkins)
            {
                // import assets
                foreach(FieldInfo field in skin.GetType().GetFields())
                {
                    string fieldType = field.FieldType.Name;
                    if (field.GetValue(skin) as UnityEngine.Object != null)
                    {
                        UnityEngine.Object value = field.GetValue(skin) as UnityEngine.Object;
                        text += $"\t\t\t// {skin.name} \n\t\t\t{fieldType} {skin.typeId}_{skin.name}_{field.Name} = {bundleVarName}.LoadAsset<{fieldType}>(\"{AssetDatabase.GetAssetPath(value)}\");";
                    }
                }

                // TODO: protection against naming a skin a type name eg KeepSkin as name of a KeepSkin

                string skinAPIEquivalent = skin.GetType().Name;
                string skinName = Util.SnakeCase(skin.name);

                // create skin
                text += $"\n\t\t\t{skinAPIEquivalent} {skinName} = new {skinAPIEquivalent}();\n";

                // assign assets to fields
                foreach (FieldInfo field in skin.GetType().GetFields())
                {
                    string fieldType = field.FieldType.Name;
                    if (field.GetValue(skin) as UnityEngine.Object != null)
                        text += $"\t\t\t{skinName}.{field.Name} = {skin.typeId}_{skin.name}_{field.Name};\n";
                }


                // BuildingSkin only
                if (skin as BuildingSkin != null)
                {
                    BuildingSkin bSkin = skin as BuildingSkin;

                    #region Person Positions

                    //text += ";LAKDJF;ALKFJ;ASLKFJ;ALFKJAS;LKFJAD";

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

                    text += $"\n\t\t\t{skinName}.personPositions = {personPositions};";

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

                    text += $"\n\t\t\t{skinName}.outlineMeshes = {outlineMeshes};";

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

                    text += $"\n\t\t\t{skinName}.outlineSkinnedMeshes = {outlineSkinnedMeshes};";

                    #endregion

                }

                // register skin
                text += $"\n\t\t\tprofile.Add({skinName});\n\n";

            }


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
                foreach (Skin skin in collection.implementedSkins)
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
                string relativePath = Path.GetFileNameWithoutExtension(sourceFile.name);
                FileStream stream = File.Create($"{outputPath}/API/{relativePath}.cs");
                stream.Write(sourceFile.bytes, 0, sourceFile.bytes.Length);
                stream.Close();

                //AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(sourceFile), $"{outputPath}/API/{relativePath}.cs");
            }
        }

        #endregion
    
        #region Main File

        private static void CreateModFile(Mod mod, string outputPath)
        {
            StreamWriter script = File.CreateText(Path.Combine(outputPath, "Mod") + ".cs");

            #region Heading

            script.WriteLine(Usings());


            script.WriteLine(Namespace(mod));
            script.WriteLine(Class());

            #endregion

            #region Body

            script.WriteLine(ClassVars());
            script.WriteLine(OpenMainMethod());
            script.WriteLine(MainMethodInit(mod));

            foreach (Collection collection in mod.collections)
                script.WriteLine(WriteCollection(mod, collection));


            script.WriteLine(MainMethodConclude());
            script.WriteLine(CloseMainMethod());

            #endregion

            #region Close Heading

            script.WriteLine(CloseClass());
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
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("build"))
            {
                ModBuilder.Build(target as Mod);
            }
        }
    }
}


#endif