using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ReskinEngine.Engine.Utils;

namespace ReskinEngine.Engine.Utils
{
    public static class GameObjectExtensions
    {
        public static void ClearChildren(this GameObject obj)
        {
            for (int i = 0; i < obj.transform.childCount; i++)
                GameObject.Destroy(obj.transform.GetChild(i));
        }

        public static void ClearChildren(this MonoBehaviour obj)
        {
            for (int i = 0; i < obj.transform.childCount; i++)
                GameObject.Destroy(obj.transform.GetChild(i));
        }
        public static void ClearChildren(this Transform transform)
        {
            for (int i = 0; i < transform.childCount; i++)
                GameObject.Destroy(transform.GetChild(i));
        }
    }

}

namespace ReskinEngine.Engine
{
    #region Keep

    public class KeepSkinBinder : BuildingSkinBinder
    {
        public override string UniqueName => "keep";

        public GameObject keepUpgrade1;
        public GameObject keepUpgrade2;
        public GameObject keepUpgrade3;
        public GameObject keepUpgrade4;

        public GameObject banner1;
        public GameObject banner2;

        public override void Read(GameObject obj)
        {
            base.Read(obj);

            ReadModels(obj, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, "keepUpgrade1", "keepUpgrade2", "keepUpgrade3", "keepUpgrade4");

            ReadPersonPositions(obj);
        }

        public override void BindToBuildingBase(Building building)
        {
            Keep keep = building.GetComponent<Keep>();
            Upgradeable upgradeable = keep.GetComponent<Upgradeable>();

            // Upgrades
            if (keepUpgrade1)
            {
                GameObject obj = building.transform.Find("Offset/SmallerKeep").gameObject;
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<MeshFilter>().mesh = null;
                obj.ClearChildren();
                if (keepUpgrade1.GetComponent<MeshFilter>())
                    obj.GetComponent<MeshCollider>().sharedMesh = keepUpgrade1.GetComponent<MeshFilter>().sharedMesh;
                GameObject.Instantiate(keepUpgrade1, obj.transform).name = "inst";
            }
            if (keepUpgrade2)
            {
                GameObject obj = building.transform.Find("Offset/SmallKeep").gameObject;
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<MeshFilter>().mesh = null;
                obj.ClearChildren();
                if (keepUpgrade1.GetComponent<MeshFilter>())
                    obj.GetComponent<MeshCollider>().sharedMesh = keepUpgrade1.GetComponent<MeshFilter>().sharedMesh;
                GameObject.Instantiate(keepUpgrade1, obj.transform).name = "inst";
            }
            if (keepUpgrade3)
            {
                GameObject obj = building.transform.Find("Offset/Keep").gameObject;
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<MeshFilter>().mesh = null;
                obj.ClearChildren();
                if (keepUpgrade1.GetComponent<MeshFilter>())
                    obj.GetComponent<MeshCollider>().sharedMesh = keepUpgrade1.GetComponent<MeshFilter>().sharedMesh;
                GameObject.Instantiate(keepUpgrade1, obj.transform).name = "inst";
            }
            if (keepUpgrade4)
            {
                GameObject obj = building.transform.Find("Offset/MediumKeep").gameObject;
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<MeshFilter>().mesh = null;
                obj.ClearChildren();
                if (keepUpgrade1.GetComponent<MeshFilter>())
                    obj.GetComponent<MeshCollider>().sharedMesh = keepUpgrade1.GetComponent<MeshFilter>().sharedMesh;
                GameObject.Instantiate(keepUpgrade1, obj.transform).name = "inst";
            }

            base.BindToBuildingBase(building);
        }

        public override void BindToBuildingInstance(Building b)
        {
            this.BindToBuildingBase(b);
        }

    }

    #endregion

    #region Training Buildings



    public class ArcherSchoolSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "archerschool";

        public override void Read(GameObject obj) => base.Read<ArcherSchoolSkinBinder>(obj);
    }

    public class BarracksSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "barracks";

        public override void Read(GameObject obj) => base.Read<BarracksSkinBinder>(obj);
    }

    #endregion

    #region Misc Buildings

    public class TreasureRoomSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "throneroom";

        public override void Read(GameObject obj) => base.Read<TreasureRoomSkinBinder>(obj);

    }


    public class ChamberOfWarSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "chamberofwar";
        public override void Read(GameObject obj) => base.Read<ChamberOfWarSkinBinder>(obj);

    }

    public class GreatHallSkinBinder : GenericBuildingSkinBinder
    {
        public override string UniqueName => "greathall";
        public override void Read(GameObject obj) => base.Read<GreatHallSkinBinder>(obj);

    }


    #endregion
}
