using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;
using ReskinEngine.Utils;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{

    [CustomEditor(typeof(Collection))]
    public class CollectionEditor : UnityEditor.Editor
    {
        #region Import Skin Menu


        private bool showImportSkinMenu = false;

        private Skin importSelectedSkin;

        #endregion

        #region Create Skin Menu

        private bool showCreateSkinMenu = false;

        private int selectedSkinId = 0;
        private string[] skinNameOptions;
        private string skinName = "New Skin";


        #endregion

        #region Skins List

        bool showSkins = true;
        Dictionary<Skin, bool> showSkinIndex = new Dictionary<Skin, bool>();
        bool defaultShowSkinValue = false;
        Skin current = null;

        #endregion

        #region Skin Preview

        private SkinPreview preview;

        #endregion

        public static string infoDocumentPath = "Reskin Engine/Info";

        public static string EditorSceneName { get; } = "ReskinEditor";


        public override void OnInspectorGUI()
        {
            Collection collection = (Collection)target;

            #region Import Skins

            showImportSkinMenu = EditorGUILayout.Foldout(showImportSkinMenu, "Import");

            if (showImportSkinMenu)
            {
                EditorGUILayout.LabelField("Import Skin", new GUIStyle()
                {
                    fontSize = 12,
                    fontStyle = FontStyle.Bold

                });

                importSelectedSkin = (Skin)EditorGUILayout.ObjectField(importSelectedSkin, typeof(Skin));

                if (importSelectedSkin != null && !collection.implementedSkins.Contains(importSelectedSkin))
                    if (GUILayout.Button("Import"))
                        collection.implementedSkins.Add(importSelectedSkin);

                if (importSelectedSkin != null && collection.implementedSkins.Contains(importSelectedSkin))
                    GUILayout.Label("Collection already contains item");
            }

            #endregion

            #region Create Skins

            showCreateSkinMenu = EditorGUILayout.Foldout(showCreateSkinMenu, "Create");
            
            if(showCreateSkinMenu)
            {
                EditorGUILayout.LabelField("Create Skin Menu", new GUIStyle()
                {
                    fontSize = 12,
                    fontStyle = FontStyle.Bold
                    
                });

                //if (GUILayout.Button("Autocreate All"))
                //{
                //    string cachedSkinName = skinName;
                //    int cachedSkinId = selectedSkinId;
                //    for (int i = 0; i < skinNameOptions.Length; i++)
                //    {
                //        selectedSkinId = i;
                //        Skin skin = ReskinEngine.skins.First((s) => s.friendlyName == skinNameOptions[i]);
                //        skinName = skin.friendlyName;

                //        CreateSkin();
                //    }
                //    skinName = cachedSkinName;
                //    selectedSkinId = cachedSkinId;
                //}

                EditorGUILayout.Separator();

                UpdateSkinIdOptions();

                selectedSkinId = EditorGUILayout.Popup("Skin to Create", selectedSkinId, skinNameOptions);

                skinName = GetUnusedSkinName();
                skinName = ValidSkinName(EditorGUILayout.TextField("Skin Name", skinName));

                if (GUILayout.Button("Create"))
                    CreateSkin();

                string description = GetDescription(skinNameOptions[selectedSkinId]);
                GUILayout.Label(description);

                GUILayout.Space(10);
            }

            #endregion

            #region Skins list

            showSkins = EditorGUILayout.Foldout(showSkins, "Skins");

            if (showSkins)
            {
                collection.implementedSkins.RemoveAll((skin) => skin == null);

                if (collection.implementedSkins.Count > 0)
                {
                    EditorGUILayout.LabelField("Implemented Skins");

                    for (int i = 0; i < collection.implementedSkins.Count; i++)
                    {
                        Skin skin = collection.implementedSkins[i];

                        if (skin != null)
                        {
                            if (EditorGUILayout.Foldout(current == skin, $"{skin.name} ({skin.friendlyName})"))
                                current = skin;
                            else if (current == skin)
                                current = null;
                            

                            if (current == skin)
                            {
                                //EditorGUILayout.ObjectField(skin, typeof(Skin), false);

                                collection.implementedSkins[i] = EditorGUILayout.ObjectField(skin, skin.GetType()) as Skin;
                                skin = collection.implementedSkins[i];


                                UnityEditor.Editor editor = CreateEditor(skin);
                                if (editor != null)
                                    editor.OnInspectorGUI();

                                if (preview != null)
                                    preview.UI();

                                if (GUILayout.Button("Delete"))
                                    collection.implementedSkins.RemoveAt(i);

                                
                            }
                        }
                        else
                        {
                            collection.implementedSkins.RemoveAt(i);
                        }
                    }
                }
                else
                    GUILayout.Label("No skins in collection");
            }

            #endregion

            #region Preview

            if (SceneManager.GetActiveScene().name == EditorSceneName)
            {
                GameObject previewObject = SceneManager.GetActiveScene().GetRootGameObjects()[0];

                if (!previewObject)
                    previewObject = new GameObject("Preview");


                if (current)
                {
                    if (preview == null || preview.SkinType != current.GetType())
                        preview = GetPreview(current);

                    // Change preview
                    if (previewObject.name != current.GetType().FullName || previewObject.transform.childCount == 0)
                    {
                        previewObject.name = current.GetType().FullName;
                        previewObject.ClearChildren();

                        if (preview != null)
                        {
                            GameObject created = preview.Create();
                            created.transform.SetParent(previewObject.transform);
                            created.transform.localPosition = Vector3.zero;

                            preview.Apply(created, current);

                            if (created != null)
                            {
                                created.transform.position = Vector3.zero;
                                created.transform.localScale = Vector3.one;
                                created.transform.rotation = Quaternion.identity;
                            }
                        }
                    }


                    // Update preview
                    if (preview != null && previewObject.transform.childCount > 0)
                        preview.Apply(previewObject.transform.GetChild(0).gameObject, current);
                }
                else
                    previewObject.ClearChildren();
            }

            #endregion
        }

        private SkinPreview GetPreview(Skin @for)
        {
            Type[] types = AppDomain.CurrentDomain.FindAllOfType<SkinPreview>();

            foreach(Type type in types)
            {
                if (!type.IsAbstract)
                {
                    SkinPreview binder = Activator.CreateInstance(type) as SkinPreview;
                    if (binder.SkinType == @for.GetType())
                        return binder;
                }
            }
            return null;
        }

        private void UpdateSkinIdOptions()
        {
            List<string> skinIdOptions = new List<string>();

            ReskinEngine.UpdateTypes();

            foreach (Skin s in ReskinEngine.skins)
                skinIdOptions.Add(s.friendlyName);

            skinIdOptions.Sort();
            this.skinNameOptions = skinIdOptions.ToArray();
        }


        private void UpdateShowSkinIndex()
        {
            foreach(Skin skin in ((Collection)target).implementedSkins)
            {
                if(skin)
                    if (!showSkinIndex.ContainsKey(skin))
                        showSkinIndex.Add(skin, defaultShowSkinValue);
            }

            foreach(Skin skin in showSkinIndex.Keys)
            {
                if(skin)
                    if (!((Collection)target).implementedSkins.Contains(skin))
                        showSkinIndex.Remove(skin);
            }
        }

        private string ValidSkinName(string original) => original.Length > 0 ? char.ToLower(original[0]) + original.Substring(1) : original;

        private string GetUnusedSkinName()
        {
            if (skinName.Length > 0) {
                Collection collection = (Collection)target;

                string collectionPath = AssetDatabase.GetAssetPath(collection);
                string dir = Directory.GetParent(collectionPath).FullName;
                string path = Path.Combine(dir, skinName + ".asset");


                path = path.Replace("\\", "/");
                path = path.Replace(Application.dataPath, "Assets");

                if (AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path) != null)
                {
                    int postfix = 1;
                    if(int.TryParse(skinName[skinName.Length - 1].ToString(), out int num))
                    {
                        postfix += num;
                    }
                    return skinName.Substring(0, skinName.Length - (postfix - 1 == 0 ? 0 : ((postfix - 1).ToString().Length))) + postfix.ToString();
                }
                return skinName;
            }
            else
            {
                skinName = "New Skin";
                return GetUnusedSkinName();
            }
        }

        private void CreateSkin()
        {
            Collection collection = (Collection)target;

            string name = skinNameOptions[selectedSkinId];

            Skin created = collection.Create(name);
            created.name = skinName;

            string collectionPath = AssetDatabase.GetAssetPath(collection);
            string dir = Directory.GetParent(collectionPath).FullName;

            

            string finalPath = Path.Combine(dir, created.name) + ".asset";

            finalPath = finalPath.Replace("\\", "/");
            finalPath = finalPath.Replace(Application.dataPath, "Assets");

            AssetDatabase.CreateAsset(created, finalPath);

            showSkinIndex.Add(created, defaultShowSkinValue);
        }

        private string GetDescription(string friendlyName)
        {

            TextAsset document = Resources.Load<TextAsset>(infoDocumentPath);
            string docText = document.text;

            int titleIndex = docText.IndexOf($"-- {friendlyName} --");

            if (titleIndex == -1)
                return "\nError - Item not found\n";

            int startChar = titleIndex;
            
            while(!(docText[startChar] == '\n' && docText[startChar - 2] == '\n') && startChar > 0)
                startChar--;

            int endChar = titleIndex;
            while (endChar < docText.Length - 2 && !(docText[endChar] == '\n' && docText[endChar + 2] == '\n'))
                endChar++;

            return docText.Substring(startChar, endChar - startChar);
        }



    }
}

#endif