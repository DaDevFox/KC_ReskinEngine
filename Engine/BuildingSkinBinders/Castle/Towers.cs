using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReskinEngine.Engine
{
    public class ArcherTowerSkinBinder : BuildingSkinBinder
    {
        public override string UniqueName => "archer";

        public GameObject baseModel;
        public GameObject veteranModel;

        public override SkinBinder Create(GameObject obj)
        {
            ArcherTowerSkinBinder inst = new ArcherTowerSkinBinder();

            if (obj.transform.Find("baseModel"))
                inst.baseModel = obj.transform.Find("baseModel").gameObject;
            if (obj.transform.Find("veteranModel"))
                inst.veteranModel = obj.transform.Find("veteranModel").gameObject;

            return inst;
        }

        public override void BindToBuildingBase(Building building)
        {
            GameObject baseModel = building.transform.Find("Offset/1x1x5_archer").gameObject;
            GameObject veteranModel = building.transform.Find("Offset/archertower_veteran").gameObject;

            MeshFilter m_1 = baseModel.GetComponent<MeshFilter>();
            MeshFilter m_2 = veteranModel.GetComponent<MeshFilter>();

            if (this.baseModel)
            {
                m_1.mesh = null;
                GameObject.Instantiate(this.baseModel, baseModel.transform);
            }

            if (this.veteranModel)
            {
                m_2.mesh = null;
                GameObject.Instantiate(this.veteranModel, veteranModel.transform);
            }
        }


    }

    public class BallistaTowerSkinBinder : BuildingSkinBinder
    {
        public override string UniqueName => "ballista";

        
        public GameObject veteranModel;
        public GameObject baseModel;
        public GameObject topBase;

        public GameObject armR;
        public Transform armREnd;
        public GameObject armL;
        public Transform armLEnd;

        public GameObject stringR;
        public GameObject stringL;

        public GameObject projectile;
        public Transform projectileEnd;

        public GameObject flag;

        public override SkinBinder Create(GameObject obj)
        {
            BallistaTowerSkinBinder inst = new BallistaTowerSkinBinder();

            if (obj.transform.Find("veteranModel"))
                inst.veteranModel = obj.transform.Find("veteranModel").gameObject;
            if (obj.transform.Find("baseModel"))
                inst.baseModel = obj.transform.Find("baseModel").gameObject;
            if (obj.transform.Find("topBase"))
                inst.topBase = obj.transform.Find("topBase").gameObject;

            if (obj.transform.Find("armR"))
                inst.armR = obj.transform.Find("armR").gameObject;
            if (obj.transform.Find("armREnd"))
                inst.armREnd = obj.transform.Find("armREnd");

            if (obj.transform.Find("armL"))
                inst.armL = obj.transform.Find("armL").gameObject;
            if (obj.transform.Find("armLEnd"))
                inst.armLEnd = obj.transform.Find("armLEnd");

            if (obj.transform.Find("stringR"))
                inst.stringR = obj.transform.Find("stringR").gameObject;
            if (obj.transform.Find("stringL"))
                inst.stringL = obj.transform.Find("stringL").gameObject;

            if (obj.transform.Find("projectile"))
                inst.projectile = obj.transform.Find("projectile").gameObject;
            if (obj.transform.Find("projectileEnd"))
                inst.projectileEnd = obj.transform.Find("projectileEnd");

            if (obj.transform.Find("flag"))
                inst.flag = obj.transform.Find("flag").gameObject;

            return inst;
        }

        public override void BindToBuildingBase(Building building)
        {
            // Objects
            GameObject baseModel = building.transform.Find("Offset/ballista").gameObject;
            GameObject veteranModel = building.transform.Find("Offset/ballistabase_veteran").gameObject;

            GameObject topBase = building.transform.Find("Offset/ballista/ballistatop").gameObject;

            GameObject armR = building.transform.Find("Offset/ballista/ballistatop/armRContainer/armR").gameObject;
            GameObject armREnd = building.transform.Find("Offset/ballista/ballistatop/armRContainer/armR/armREnd").gameObject;
            GameObject armL = building.transform.Find("Offset/ballista/ballistatop/armLContainer/armL").gameObject;
            GameObject armLEnd = building.transform.Find("Offset/ballista/ballistatop/armLContainer/armL/armLEnd").gameObject;

            GameObject arrow = building.transform.Find("Offset/ballista/ballistatop/arrow").gameObject;
            GameObject arrowEnd = building.transform.Find("Offset/ballista/ballistatop/arrow/arrowEnd").gameObject;

            GameObject stringR = building.transform.Find("Offset/ballista/ballistatop/stringR").gameObject;
            GameObject stringL = building.transform.Find("Offset/ballista/ballistatop/stringL").gameObject;

            GameObject flag = building.transform.Find("Offset/GameObject (1)").gameObject;



            // Meshes
            MeshFilter m_base = baseModel.GetComponent<MeshFilter>();
            MeshFilter m_veteran = veteranModel.GetComponent<MeshFilter>();

            MeshFilter m_top = topBase.GetComponent<MeshFilter>();

            MeshFilter m_armR = armR.GetComponent<MeshFilter>();
            MeshFilter m_armL = armL.GetComponent<MeshFilter>();

            MeshFilter m_arrow = arrow.GetComponent<MeshFilter>();

            MeshFilter m_stringR = stringR.GetComponent<MeshFilter>();
            MeshFilter m_stringL = stringL.GetComponent<MeshFilter>();

            MeshFilter m_flag = flag.GetComponent<MeshFilter>();


            // Bases
            if (this.baseModel)
            {
                m_base.mesh = null;
                GameObject.Instantiate(this.baseModel, baseModel.transform);
            }

            if (this.veteranModel)
            {
                m_veteran.mesh = null;
                GameObject.Instantiate(this.veteranModel, veteranModel.transform);
            }

            if (this.topBase)
            {
                m_top.mesh = null;
                GameObject.Instantiate(this.topBase, topBase.transform);
            }


            // Arms
            if (this.armR)
            {
                m_armR.mesh = null;
                GameObject.Instantiate(this.armR, armR.transform);
            }

            if (this.armL)
            {
                m_armL.mesh = null;
                GameObject.Instantiate(this.armL, armL.transform);
            }

            // Arm Anchors
            if (this.armREnd)
                armREnd.transform.position = this.armREnd.position;
            if (this.armLEnd)
                armLEnd.transform.position = this.armLEnd.position;

            // Projectile
            if (this.projectile)
            {
                m_arrow.mesh = null;
                GameObject.Instantiate(this.projectile, arrow.transform);
            }

            // Projectile Anchor
            if (this.projectileEnd)
                arrowEnd.transform.position = this.projectileEnd.position;


            // Strings
            if(this.stringR)
            {
                m_stringR.mesh = null;
                GameObject.Instantiate(this.stringR, stringR.transform);
            }

            if (this.stringL)
            {
                m_stringL.mesh = null;
                GameObject.Instantiate(this.stringL, stringL.transform);
            }

            // Flag
            if (this.flag)
            {
                m_flag.mesh = null;
                GameObject.Instantiate(this.flag, flag.transform);
            }



        }

        public override void BindToBuildingInstance(Building building)
        {
            this.BindToBuildingBase(building);
        }
    }


}
