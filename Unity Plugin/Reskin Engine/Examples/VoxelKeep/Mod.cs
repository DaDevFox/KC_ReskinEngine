using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ReskinEngine.API;

namespace ReskinEngine.Examples.VoxelKeep
{
	public class Mod
	{
		public static KCModHelper helper;

		public void SceneLoaded(KCModHelper helper)
		{
			// KCModHelper
			Mod.helper = helper;

			// Setup the ReskinProfile with a name and compatability identifier
			ReskinProfile profile = new ReskinProfile("KeepExample", "ReskinEngine.Examples");

			//Voxel_Castle
			AssetBundle Voxel_Castle_bundle = KCModHelper.LoadAssetBundle(helper.modPath + "/assetbundle/", "keepexample_voxel_castle");


			// keep 
			GameObject building_keep_keep_keepUpgrade1 = Voxel_Castle_bundle.LoadAsset<GameObject>("Assets/Mod/TPunkoModels/Prefabs/Keeps/Keep-3.prefab");			// keep 
			GameObject building_keep_keep_keepUpgrade2 = Voxel_Castle_bundle.LoadAsset<GameObject>("Assets/Mod/TPunkoModels/Prefabs/Keeps/Keep-3.prefab");			// keep 
			GameObject building_keep_keep_keepUpgrade3 = Voxel_Castle_bundle.LoadAsset<GameObject>("Assets/Mod/TPunkoModels/Prefabs/Keeps/Keep-3.prefab");			// keep 
			GameObject building_keep_keep_keepUpgrade4 = Voxel_Castle_bundle.LoadAsset<GameObject>("Assets/Mod/TPunkoModels/Prefabs/Keeps/Keep-3.prefab");
			KeepSkin keep = new KeepSkin();
			keep.keepUpgrade1 = building_keep_keep_keepUpgrade1;
			keep.keepUpgrade2 = building_keep_keep_keepUpgrade2;
			keep.keepUpgrade3 = building_keep_keep_keepUpgrade3;
			keep.keepUpgrade4 = building_keep_keep_keepUpgrade4;

			keep.personPositions = new Vector3[3] {new Vector3(0f, 0f, 2.64f), new Vector3(0f, 0f, 1.3f), new Vector3(0f, 0f, 0f)};
			profile.Add(keep);



			profile.Register();
			helper.Log("Init");
		}
	}
}
