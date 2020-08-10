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

            return inst;
        }

        public override void BindToBuildingBase(Building b)
        {
            base.BindToBuildingBase(b);

            Keep keep = b.GetComponent<Keep>();
            Upgradeable upgradeable = keep.GetComponent<Upgradeable>();

            // Upgrades
            if (keepUpgrade1)
            {
                GameObject.Destroy(upgradeable.upgrades[0].model);
                upgradeable.upgrades[0] = new Upgrade()
                {
                    model = GameObject.Instantiate(keepUpgrade1, b.transform)
                };
            }
            if (keepUpgrade2)
            {
                GameObject.Destroy(upgradeable.upgrades[1].model);
                upgradeable.upgrades[1] = new Upgrade()
                {
                    model = GameObject.Instantiate(keepUpgrade2, b.transform)
                };
            }
            if (keepUpgrade3)
            {
                GameObject.Destroy(upgradeable.upgrades[2].model);
                upgradeable.upgrades[2] = new Upgrade()
                {
                    model = GameObject.Instantiate(keepUpgrade3, b.transform)
                };
            }
            if (keepUpgrade4)
            {
                GameObject.Destroy(upgradeable.upgrades[3].model);
                upgradeable.upgrades[3] = new Upgrade()
                {
                    model = GameObject.Instantiate(keepUpgrade4, b.transform)
                };
            }
        }

        public override void BindToBuildingInstance(Building b)
        {
            this.BindToBuildingInstance(b);

            Engine.helper.Log("keep instance bound");
        }

    }

    #endregion



}
