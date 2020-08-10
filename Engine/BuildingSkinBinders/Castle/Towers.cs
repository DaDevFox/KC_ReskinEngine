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
    }

    public class BallistaTowerSkinBinder : BuildingSkinBinder
    {
        public override string UniqueName => "ballista";

        /// <summary>
        /// An embelishment added to the ballista tower when it achieves the veteran status
        /// </summary>
        public GameObject veteranModel;
        /// <summary>
        /// The main model of the Ballista Tower
        /// </summary>
        public GameObject baseModel;
        /// <summary>
        /// The base of the rotational top half of the ballista
        /// </summary>
        public GameObject topBase;
        /// <summary>
        /// The right side arm used to animate the ballista's firing movement
        /// </summary>
        public GameObject armR;
        /// <summary>
        /// The right end of the right arm of the ballista; used for anchoring the right side of the string in animation
        /// </summary>
        public Transform armREnd;
        /// <summary>
        /// The left side arm used to animate the ballista's firing movement
        /// </summary>
        public GameObject armL;
        /// <summary>
        /// The left end of the left arm of the ballista; used for anchoring the left side of the string in animation
        /// </summary>
        public Transform armLEnd;
        /// <summary>
        /// The right side of the animated string used to pull back and fire the ballista projectile
        /// </summary>
        public GameObject stringR;
        /// <summary>
        /// The left side of the animated string used to pull back and fire the ballista projectile
        /// </summary>
        public GameObject stringL;
        /// <summary>
        /// The projectile fired from the ballista
        /// </summary>
        public GameObject projectile;
        /// <summary>
        /// The end of the ballista projectile that's pulled back before firing
        /// </summary>
        public Transform projectileEnd;
        /// <summary>
        /// A decorative flag on the ballista
        /// </summary>
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
    }


}
