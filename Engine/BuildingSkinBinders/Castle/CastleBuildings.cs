using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

        public override SkinBinder Create(GameObject obj)
        {
            KeepSkinBinder inst = new KeepSkinBinder();

            if (obj.transform.Find("keepUpgrade1"))
                inst.keepUpgrade1 = obj.transform.Find("keepUpgrade1").gameObject;
            if (obj.transform.Find("keepUpgrade2"))
                inst.keepUpgrade2 = obj.transform.Find("keepUpgrade2").gameObject;
            if (obj.transform.Find("keepUpgrade3"))
                inst.keepUpgrade3 = obj.transform.Find("keepUpgrade3").gameObject;
            if (obj.transform.Find("keepUpgrade4"))
                inst.keepUpgrade4 = obj.transform.Find("keepUpgrade4").gameObject;

            ApplyPersonPositions(inst, obj);

            return inst;
        }

        public override void BindToBuildingBase(Building building)
        {
            Engine.dLog("keep bind begun");

            Keep keep = building.GetComponent<Keep>();
            Upgradeable upgradeable = keep.GetComponent<Upgradeable>();

            // Upgrades
            if (keepUpgrade1)
            {
                GameObject.Destroy(upgradeable.upgrades[0].model);
                upgradeable.upgrades[0] = new Upgrade()
                {
                    model = GameObject.Instantiate(keepUpgrade1, building.transform)
                };
            }
            if (keepUpgrade2)
            {
                GameObject.Destroy(upgradeable.upgrades[1].model);
                upgradeable.upgrades[1] = new Upgrade()
                {
                    model = GameObject.Instantiate(keepUpgrade2, building.transform)
                };
            }
            if (keepUpgrade3)
            {
                GameObject.Destroy(upgradeable.upgrades[2].model);
                upgradeable.upgrades[2] = new Upgrade()
                {
                    model = GameObject.Instantiate(keepUpgrade3, building.transform)
                };
            }
            if (keepUpgrade4)
            {
                GameObject.Destroy(upgradeable.upgrades[3].model);
                upgradeable.upgrades[3] = new Upgrade()
                {
                    model = GameObject.Instantiate(keepUpgrade4, building.transform)
                };
            }

            BindPersonPositions(building, this);
        }

        public override void BindToBuildingInstance(Building b)
        {
            this.BindToBuildingBase(b);
        }

    }

    #endregion

    #region Treasure Room

    public class TreasureRoomSkinBinder : BuildingSkinBinder
    {
        public override string UniqueName => "throneroom";

        public GameObject baseModel;

        public override SkinBinder Create(GameObject obj)
        {
            var inst = new TreasureRoomSkinBinder();

            if (obj.transform.Find("baseModel"))
                this.baseModel = obj.transform.Find("baseModel").gameObject;

            ApplyPersonPositions(inst, obj);

            return inst;
        }

        public override void BindToBuildingBase(Building building)
        {
            MeshFilter mesh = building.transform.Find("Offset/throneroom").GetComponent<MeshFilter>();

            if (baseModel)
            {
                mesh.mesh = null;
                GameObject.Instantiate(baseModel, mesh.transform);
            }

            BindPersonPositions(building, this);
        }

        public override void BindToBuildingInstance(Building building)
        {
            this.BindToBuildingBase(building);
        }
    }

    #endregion

    #region Chamber Of War

    public class ChamberOfWarSkinBinder : BuildingSkinBinder
    {
        public override string UniqueName => "chamberofwar";

        public GameObject baseModel;

        public override SkinBinder Create(GameObject obj)
        {
            var inst = new ChamberOfWarSkinBinder();

            if (obj.transform.Find("baseModel"))
                this.baseModel = obj.transform.Find("baseModel").gameObject;

            ApplyPersonPositions(inst, obj);

            return inst;
        }

        public override void BindToBuildingBase(Building building)
        {
            MeshFilter mesh = building.transform.Find("Offset/chamberofwar").GetComponent<MeshFilter>();

            if (baseModel)
            {
                mesh.mesh = null;
                GameObject.Instantiate(baseModel, mesh.transform);
            }

            BindPersonPositions(building, this);
        }

        public override void BindToBuildingInstance(Building building)
        {
            this.BindToBuildingBase(building);
        }
    }

    #endregion


    // Great Hall





    // Barracks

    // Archer School


}
