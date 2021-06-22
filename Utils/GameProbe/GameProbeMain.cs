//#define STABLE
#define ALPHA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using UnityEngine;

namespace Fox.Utils.Probe
{
#if STABLE

    public class GameProbeMain
    {
        public static KCModHelper helper { get; private set; }

        public void Preload(KCModHelper helper)
        {
            GameProbeMain.helper = helper;

            Application.logMessageReceived += Application_logMessageReceived;


            Log("Preload");
        }

        private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Exception)
                Log($"{condition}\n{stackTrace}");
        }

        public void SceneLoaded()
        {
            var harmony = HarmonyInstance.Create("fox.probes.kc");
            harmony.PatchAll();


            Log("SceneLoaded");
        }

        public static void Log(object message) => helper.Log(message.ToString());

        public static void SaveUnreadableTexture(string path, Texture2D texture)
        {
            RenderTexture tmp = RenderTexture.GetTemporary(
                    texture.width,
                    texture.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

            // Blit the pixels on texture to the RenderTexture
            Graphics.Blit(texture, tmp);

            // Backup the currently set RenderTexture
            RenderTexture previous = RenderTexture.active;

            // Set the current RenderTexture to the temporary one we created
            RenderTexture.active = tmp;

            // Create a new readable Texture2D to copy the pixels to it
            Texture2D myTexture2D = new Texture2D(texture.width, texture.height);

            // Copy the pixels from the RenderTexture to the new Texture
            myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
            myTexture2D.Apply();

            // Reset the active RenderTexture
            RenderTexture.active = previous;

            // Release the temporary RenderTexture
            RenderTexture.ReleaseTemporary(tmp);

            // "myTexture2D" now has the same pixels from "texture" and it's readable.

            World.inst.SaveTexture(path, myTexture2D);
        }

        public static void ProbeTextures()
        {
            Log("Probing Textures");
            Log("Landmasses");

            for (int i = 0; i < World.inst.NumLandMasses; i++)
            {
                Texture2D texture = World.LandMassOwners.data[i].BuildingMaterial.mainTexture as Texture2D;

                SaveUnreadableTexture($"{helper.modPath}/landmass_textures/{i}.png", texture);
            }

            Log("Unis");

            for (int i = 0; i < World.inst.uniMaterial.Length; i++)
            {
                if (World.inst.uniMaterial[i] != null)
                    SaveUnreadableTexture($"{helper.modPath}/unimaterials/{i}.png", World.inst.uniMaterial[i].mainTexture as Texture2D);
            }


            Log("Textures Probed");
        }


        [HarmonyPatch(typeof(World), "Generate")]
        static class GenLandPatch
        {
            static void Postfix()
            {
                ProbeTextures();
            }
        }


    }

#endif

#if ALPHA

    public class GameProbeMain
    {
        public static KCModHelper helper { get; private set; }

        public void Preload(KCModHelper helper)
        {
            GameProbeMain.helper = helper;

            Application.logMessageReceived += Application_logMessageReceived;


            Log("Preload");
        }

        private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Exception)
                Log($"{condition}\n{stackTrace}");
        }

        public void SceneLoaded()
        {
            var harmony = HarmonyInstance.Create("fox.probes.kc");
            harmony.PatchAll();


            Log("SceneLoaded");
        }

        public static void Log(object message) => helper.Log(message.ToString());

        public static void SaveUnreadableTexture(string path, Texture2D texture)
        {
            RenderTexture tmp = RenderTexture.GetTemporary(
                    texture.width,
                    texture.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

            // Blit the pixels on texture to the RenderTexture
            Graphics.Blit(texture, tmp);

            // Backup the currently set RenderTexture
            RenderTexture previous = RenderTexture.active;

            // Set the current RenderTexture to the temporary one we created
            RenderTexture.active = tmp;

            // Create a new readable Texture2D to copy the pixels to it
            Texture2D myTexture2D = new Texture2D(texture.width, texture.height);

            // Copy the pixels from the RenderTexture to the new Texture
            myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
            myTexture2D.Apply();

            // Reset the active RenderTexture
            RenderTexture.active = previous;

            // Release the temporary RenderTexture
            RenderTexture.ReleaseTemporary(tmp);

            // "myTexture2D" now has the same pixels from "texture" and it's readable.

            World.inst.SaveTexture(path, myTexture2D);
        }

        public static void ProbeTextures()
        {
            Log("Probing Textures");

            Log("\tLiveries");

            for (int i = 0; i < World.inst.liverySets.Count; i++)
            {
                Texture2D banners = World.inst.liverySets[i].banners as Texture2D;
                Texture2D bannersMatTexture = World.inst.liverySets[i].bannerMaterial.mainTexture as Texture2D;

                Texture2D unimaterial = World.inst.liverySets[i].uniMaterial.mainTexture as Texture2D;
                Texture2D unimaterialCracked = World.inst.liverySets[i].uniMaterialCracked.mainTexture as Texture2D;

                Texture2D buildingMaterial = World.inst.liverySets[i].buildingMaterial.mainTexture as Texture2D;

                Texture2D armyMaterial = World.inst.liverySets[i].armyMaterial.mainTexture as Texture2D;
                Texture2D buildUIMaterial = World.inst.liverySets[i].buildUIMaterial.mainTexture as Texture2D;
                Texture2D flagMaterial = World.inst.liverySets[i].flagMaterial.mainTexture as Texture2D;




                SaveUnreadableTexture($"{helper.modPath}/liveries/livery_{i}_banners_raw.png", banners);
                SaveUnreadableTexture($"{helper.modPath}/liveries/livery_{i}_banners_fromMat.png", bannersMatTexture);

                SaveUnreadableTexture($"{helper.modPath}/liveries/livery_{i}_unimaterial.png", unimaterial);
                SaveUnreadableTexture($"{helper.modPath}/liveries/livery_{i}_unimaterial_cracked.png", unimaterialCracked);

                SaveUnreadableTexture($"{helper.modPath}/liveries/livery_{i}_buildingMaterial.png", buildingMaterial);
                SaveUnreadableTexture($"{helper.modPath}/liveries/livery_{i}_armyMaterial.png", armyMaterial);
                SaveUnreadableTexture($"{helper.modPath}/liveries/livery_{i}_flagMaterial.png", flagMaterial);

                SaveUnreadableTexture($"{helper.modPath}/liveries/livery_{i}_buildUIMaterial.png", buildUIMaterial);


            }


            Log("Textures Probed");
        }


        [HarmonyPatch(typeof(World), "Generate")]
        static class GenLandPatch
        {
            static void Postfix()
            {
                ProbeTextures();
            }
        }


    }

#endif

}
