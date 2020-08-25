using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace ReskinEngine.Editor
{

    [CustomEditor(typeof(Collection))]
    public class CollectionEditor : UnityEditor.Editor
    {
        #region Create Skin Menu

        bool showCreateSkinMenu = false;

        int selectedSkinId = 0;
        string[] skinIdOptions;
        string skinName = "New Skin";


        #endregion

        #region Skins Display

        bool showSkins = true;
        Dictionary<Skin, bool> showSkinIndex = new Dictionary<Skin, bool>();
        bool defaultShowSkinValue = false;

        #endregion


        public override void OnInspectorGUI()
        {
            Collection collection = (Collection)target;



            showCreateSkinMenu = EditorGUILayout.Foldout(showCreateSkinMenu, "Create Skin Menu");
            
            if(showCreateSkinMenu)
            {
                UpdateSkinIdOptions();

                selectedSkinId = EditorGUILayout.Popup("Skin to Create", selectedSkinId, skinIdOptions);
                skinName = EditorGUILayout.TextField("Skin Name", skinName);

                if (GUILayout.Button("Create"))
                    CreateSkin();


                GUILayout.Space(10);
            }



            showSkins = EditorGUILayout.Foldout(showSkins, "Skins");

            if (showSkins)
            {
                UpdateShowSkinIndex();

                collection.implementedSkins.ForEach((s) => { 
                    if (s == null) collection.implementedSkins.Remove(s); 
                });

                if(collection.implementedSkins.Count > 0) 
                {
                    for (int i = 0; i < collection.implementedSkins.Count; i++)
                    {


                        Skin skin = collection.implementedSkins[i];


                        if (skin != null)
                        {
                            showSkinIndex[skin] = EditorGUILayout.Foldout(showSkinIndex[skin], $"{skin.name} ({skin.GetType().Name})");

                            if (showSkinIndex[skin])
                            {
                                //EditorGUILayout.ObjectField(skin, typeof(Skin), false);


                                UnityEditor.Editor editor = UnityEditor.Editor.CreateEditor(skin);
                                if (editor != null)
                                    editor.OnInspectorGUI();

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

        }

        private void UpdateSkinIdOptions()
        {
            List<string> skinIdOptions = new List<string>();

            ReskinEngine.UpdateTypes();

            foreach (Skin s in ReskinEngine.skins)
                skinIdOptions.Add(s.id);

            this.skinIdOptions = skinIdOptions.ToArray();
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

        private void CreateSkin()
        {
            Collection collection = (Collection)target;

            string id = skinIdOptions[selectedSkinId];

            Skin created = collection.Create(id);
            created.name = skinName;

            string collectionPath = AssetDatabase.GetAssetPath(collection);
            string dir = Directory.GetParent(collectionPath).FullName;

            

            string finalPath = Path.Combine(dir, created.name) + ".asset";

            finalPath = finalPath.Replace("\\", "/");
            finalPath = finalPath.Replace(Application.dataPath, "Assets");

            AssetDatabase.CreateAsset(created, finalPath);

            showSkinIndex.Add(created, defaultShowSkinValue);
        }





    }
}
