
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;
//using BuildingFramework.Reskin.API;

//namespace BuildingFramework
//{
//    class Procedures
//    {


//        #region Generic

//        /// <summary>
//        /// Generic removal procedure; Works for most buildings
//        /// </summary>
//        internal static void rp_generic(Building b)
//        {
//            GameObject model = b.gameObject.transform.Find("Offset").GetChild(0).gameObject;
//            model.GetComponent<MeshFilter>().mesh = null;

//            foreach(Transform position in b.personPositions)
//            {
//                GameObject.Destroy(position.gameObject);
//            }

//            b.personPositions = null;
//        }

//        internal static void rep_generic(Building b, GenericBuildingSkin skin)
//        {
//            GameObject model = b.gameObject.transform.Find("Offset").GetChild(0).gameObject;
//            GameObject obj = GameObject.Instantiate(skin.model);
//            obj.transform.SetParent(model.transform);

//            if(skin.personPositions != null)
//                b.personPositions = skin.personPositions;
//        }

//        #endregion

//        #region Keep

//        internal static void rp_keep(Building b)
//        {
//            GameObject keepUpgrade1 = b.transform.Find("Offset/SmallerKeep").gameObject;
//            GameObject keepUpgrade2 = b.transform.Find("Offset/SmallKeep").gameObject;
//            GameObject keepUpgrade3 = b.transform.Find("Offset/Keep").gameObject;
//            GameObject keepUpgrade4 = b.transform.Find("Offset/MediumKeep").gameObject;

//            MeshFilter m_1 = keepUpgrade1.GetComponent<MeshFilter>();
//            MeshFilter m_2 = keepUpgrade2.GetComponent<MeshFilter>();
//            MeshFilter m_3 = keepUpgrade3.GetComponent<MeshFilter>();
//            MeshFilter m_4 = keepUpgrade4.GetComponent<MeshFilter>();

//            m_1.mesh = null;
//            m_2.mesh = null;
//            m_3.mesh = null;
//            m_4.mesh = null;
//        }

//        internal static void rep_keep(Building b, KeepBuildingSkin skin)
//        {
//            GameObject keepUpgrade1 = b.transform.Find("Offset/SmallerKeep").gameObject;
//            GameObject keepUpgrade2 = b.transform.Find("Offset/SmallKeep").gameObject;
//            GameObject keepUpgrade3 = b.transform.Find("Offset/Keep").gameObject;
//            GameObject keepUpgrade4 = b.transform.Find("Offset/MediumKeep").gameObject;

//            GameObject.Instantiate(skin.keepUpgrade1, keepUpgrade1.transform);
//            GameObject.Instantiate(skin.keepUpgrade2, keepUpgrade2.transform);
//            GameObject.Instantiate(skin.keepUpgrade3, keepUpgrade3.transform);
//            GameObject.Instantiate(skin.keepUpgrade4, keepUpgrade4.transform);
//        }

//        #endregion

//        #region Castle Block

//        internal static void rp_castleblock(Building b)
//        {
//            CastleBlock cb = b.gameObject.GetComponent<CastleBlock>();
//            cb.Adjacent.GetComponent<MeshFilter>().mesh = null;
//            cb.Single.GetComponent<MeshFilter>().mesh = null;
//            cb.Opposite.GetComponent<MeshFilter>().mesh = null;
//            cb.Threeside.GetComponent<MeshFilter>().mesh = null;
//            cb.doorPrefab = null;
//            cb.Closed.GetComponent<MeshFilter>().mesh = null;
//            cb.Open.GetComponent<MeshFilter>().mesh = null;
//        }

//        internal static void rep_castleblock(Building b, CastleBlockBuildingSkin skin)
//        {
//            CastleBlock cb = b.gameObject.GetComponent<CastleBlock>();
//            if (skin.Adjacent != null)
//                cb.Adjacent = skin.Adjacent.transform;
//            if (skin.Single != null)
//                cb.Single = skin.Single.transform;
//            if (skin.Opposite != null)
//                cb.Opposite = skin.Opposite.transform;
//            if (skin.Threeside != null)
//                cb.Threeside = skin.Threeside.transform;
//            if (skin.doorPrefab != null)
//                cb.doorPrefab = skin.doorPrefab;
//            if (skin.Closed != null)
//                cb.Closed = skin.Closed.transform;
//            if (skin.Open != null)
//                cb.Open = skin.Open.transform;
//        }

//        #endregion

//        #region Gate

//        internal static void rp_gate(Building b)
//        {
//            GameObject gate = b.transform.Find("Offset/Gate").gameObject;
//            GameObject portculus = b.transform.Find("Offset/Portculus").gameObject;

//            gate.GetComponent<MeshFilter>().mesh = null;
//            portculus.GetComponent<MeshFilter>().mesh = null;
//        }

//        internal static void rep_gate(Building b, GateBuildingSkin skin)
//        {
//            GameObject gate = b.transform.Find("Offset/Gate").gameObject;
//            GameObject portculus = b.transform.Find("Offset/Portculus").gameObject;

//            GameObject.Instantiate(skin.gate, gate.transform);
//            GameObject.Instantiate(skin.porticulus, portculus.transform);
//        }


//        #endregion

//        #region Stairs

//        internal static void rp_castlestairs(Building b)
//        {
//            GameObject stairs1 = b.transform.Find("Offset/SmallerKeep").gameObject;
//            GameObject stairs2 = b.transform.Find("Offset/SmallKeep").gameObject;
//            GameObject stairs3 = b.transform.Find("Offset/Keep").gameObject;
//            GameObject stairs4 = b.transform.Find("Offset/MediumKeep").gameObject;

//            MeshFilter m_1 = stairs1.GetComponent<MeshFilter>();
//            MeshFilter m_2 = stairs2.GetComponent<MeshFilter>();
//            MeshFilter m_3 = stairs3.GetComponent<MeshFilter>();
//            MeshFilter m_4 = stairs4.GetComponent<MeshFilter>();

//            m_1.mesh = null;
//            m_2.mesh = null;
//            m_3.mesh = null;
//            m_4.mesh = null;
//        }

//        internal static void rep_castlestairs(Building b, CastleStairsBuildingSkin skin)
//        {
//            GameObject stairs1 = b.transform.Find("Offset/SmallerKeep").gameObject;
//            GameObject stairs2 = b.transform.Find("Offset/SmallKeep").gameObject;
//            GameObject stairs3 = b.transform.Find("Offset/Keep").gameObject;
//            GameObject stairs4 = b.transform.Find("Offset/MediumKeep").gameObject;

//            GameObject.Instantiate(skin.stairsFront, stairs1.transform);
//            GameObject.Instantiate(skin.stairsRight, stairs2.transform);
//            GameObject.Instantiate(skin.stairsDown, stairs3.transform);
//            GameObject.Instantiate(skin.stairsLeft, stairs4.transform);
//        }

//        #endregion

//        #region Archer Tower

//        internal static void rp_archer(Building b)
//        {
//            GameObject baseModel = b.transform.Find("Offset/1x1x5_archer").gameObject;
//            GameObject veteran = b.transform.Find("Offset/archertower_veteran").gameObject;

//            MeshFilter m_1 = baseModel.GetComponent<MeshFilter>();
//            MeshFilter m_2 = veteran.GetComponent<MeshFilter>();

//            m_1.mesh = null;
//            m_2.mesh = null;
//        }

//        internal static void rep_archer(Building b, ArcherTowerBuildingSkin skin)
//        {
//            GameObject baseModel = b.transform.Find("Offset/1x1x5_archer").gameObject;
//            GameObject veteran = b.transform.Find("Offset/archertower_veteran").gameObject;

//            GameObject.Instantiate(skin.baseModel, baseModel.transform);
//            GameObject.Instantiate(skin.veteranModel, veteran.transform);
//        }

//        #endregion

//        #region Ballista Tower

//        internal static void rp_ballista(Building b)
//        {
//            //GameObjects
//            GameObject baseModel = b.transform.Find("Offset/ballista").gameObject;
//            GameObject veteranModel = b.transform.Find("Offset/ballistabase_veteran").gameObject;

//            GameObject topBase = b.transform.Find("Offset/ballista/ballistatop").gameObject;

//            GameObject armR = b.transform.Find("Offset/ballista/ballistatop/armRContainer/armR").gameObject;
//            GameObject armREnd = b.transform.Find("Offset/ballista/ballistatop/armRContainer/armR/armREnd").gameObject;
//            GameObject armL = b.transform.Find("Offset/ballista/ballistatop/armLContainer/armL").gameObject;
//            GameObject armLEnd = b.transform.Find("Offset/ballista/ballistatop/armLContainer/armL/armLEnd").gameObject;

//            GameObject arrow = b.transform.Find("Offset/ballista/ballistatop/arrow").gameObject;
//            GameObject arrowEnd = b.transform.Find("Offset/ballista/ballistatop/arrow/arrowEnd").gameObject;

//            GameObject stringR = b.transform.Find("Offset/ballista/ballistatop/stringR").gameObject;
//            GameObject stringL = b.transform.Find("Offset/ballista/ballistatop/stringL").gameObject;

//            GameObject flag = b.transform.Find("Offset/GameObject (1)").gameObject;



//            //Meshes
//            MeshFilter m_1 = baseModel.GetComponent<MeshFilter>();
//            MeshFilter m_2 = veteranModel.GetComponent<MeshFilter>();

//            MeshFilter m_3 = topBase.GetComponent<MeshFilter>();

//            MeshFilter m_4 = armR.GetComponent<MeshFilter>();
//            MeshFilter m_5 = armL.GetComponent<MeshFilter>();

//            MeshFilter m_6 = arrow.GetComponent<MeshFilter>();

//            MeshFilter m_7 = stringR.GetComponent<MeshFilter>();
//            MeshFilter m_8 = stringL.GetComponent<MeshFilter>();

//            MeshFilter m_9 = flag.GetComponent<MeshFilter>();


//            //Removal
//            m_1.mesh = null;
//            m_2.mesh = null;
//            m_3.mesh = null;
//            m_4.mesh = null;
//            m_5.mesh = null;
//            m_6.mesh = null;
//            m_7.mesh = null;
//            m_8.mesh = null;
//            m_9.mesh = null;
//        }


//        internal static void rep_ballista(Building b, BallistaTowerBuildingSkin skin)
//        {
//            //GameObjects
//            GameObject baseModel = b.transform.Find("Offset/ballista").gameObject;
//            GameObject veteranModel = b.transform.Find("Offset/ballistabase_veteran").gameObject;

//            GameObject topBase = b.transform.Find("Offset/ballista/ballistatop").gameObject;

//            GameObject armR = b.transform.Find("Offset/ballista/ballistatop/armRContainer/armR").gameObject;
//            GameObject armREnd = b.transform.Find("Offset/ballista/ballistatop/armRContainer/armR/armREnd").gameObject;
//            GameObject armL = b.transform.Find("Offset/ballista/ballistatop/armLContainer/armL").gameObject;
//            GameObject armLEnd = b.transform.Find("Offset/ballista/ballistatop/armLContainer/armL/armLEnd").gameObject;

//            GameObject arrow = b.transform.Find("Offset/ballista/ballistatop/arrow").gameObject;
//            GameObject arrowEnd = b.transform.Find("Offset/ballista/ballistatop/arrow/arrowEnd").gameObject;

//            GameObject stringR = b.transform.Find("Offset/ballista/ballistatop/stringR").gameObject;
//            GameObject stringL = b.transform.Find("Offset/ballista/ballistatop/stringL").gameObject;

//            GameObject flag = b.transform.Find("Offset/GameObject (1)").gameObject;

//            //Instantiation and assignment
//            GameObject.Instantiate(skin.baseModel, baseModel.transform);
//            GameObject.Instantiate(skin.veteranModel, veteranModel.transform);

//            GameObject.Instantiate(skin.topBase, topBase.transform);

//            GameObject.Instantiate(skin.armR, armR.transform);
//            GameObject.Instantiate(skin.armL, armL.transform);

//            armREnd.transform.position = skin.armREnd.position;
//            armREnd.transform.rotation = skin.armREnd.rotation;

//            armLEnd.transform.position = skin.armLEnd.position;
//            armLEnd.transform.rotation = skin.armLEnd.rotation;

//            GameObject.Instantiate(skin.projectile, arrow.transform);

//            arrowEnd.transform.position = skin.projectileEnd.position;
//            arrowEnd.transform.rotation = skin.projectileEnd.rotation;

//            GameObject.Instantiate(skin.stringR, stringR.transform);
//            GameObject.Instantiate(skin.stringL, stringL.transform);

//            GameObject.Instantiate(skin.flag, flag.transform);

//        }

//        #endregion

//        #region PathType

//        internal static void rp_path(Building b)
//        {
//            Road road = b.GetComponent<Road>();
//            road.Straight = null;
//            road.Elbow = null;
//            road.Intersection3 = null;
//            road.Intersection4 = null;
//        }

//        internal static void rep_path(Building b, PathTypeBuildingSkin skin)
//        {
//            Road road = b.GetComponent<Road>();
//            if(skin.Straight)
//                road.Straight = skin.Straight.transform;
//            if(skin.Elbow)
//                road.Elbow = skin.Elbow.transform;
//            if(skin.Threeway)
//                road.Intersection3 = skin.Threeway.transform;
//            if(skin.Fourway)
//                road.Intersection4 = skin.Fourway.transform;
//        }

//        internal static void rp_garden(Building b)
//        {
//            Garden garden = b.GetComponent<Garden>();
//            garden.Straight = null;
//            garden.Elbow = null;
//            garden.Intersection3 = null;
//            garden.Intersection4 = null;
//            garden.Intersection4_Special = null;

//            garden.Straight_flowers = null;
//            garden.Elbow_flowers = null;
//            garden.Intersection3_flowers = null;
//            garden.Intersection4_flowers = null;
//            garden.Intersection4Special_flowers = null;
//        }


//        internal static void rep_garden(Building b, GardenBuildingSkin skin)
//        {
//            Garden garden = b.GetComponent<Garden>();
//            if(skin.Straight)
//                garden.Straight = skin.Straight.transform;
//            if(skin.Elbow)
//                garden.Elbow = skin.Elbow.transform;
//            if(skin.Threeway)
//                garden.Intersection3 = skin.Threeway.transform;
//            if(skin.Fourway)
//                garden.Intersection4 = skin.Fourway.transform;
//            if(skin.Fourway_Special)
//                garden.Intersection4_Special = skin.Fourway_Special.transform;

//            if(skin.Straight_flowers)
//                garden.Straight_flowers = skin.Straight_flowers;
//            if(skin.Elbow_flowers)
//                garden.Elbow_flowers = skin.Elbow_flowers;
//            if(skin.Threeway_flowers)
//                garden.Intersection3_flowers = skin.Threeway_flowers;
//            if(skin.Fourway_Special)
//                garden.Intersection4_flowers = skin.Fourway_flowers;
//            if(skin.Fourway_Special_flowers)
//                garden.Intersection4Special_flowers = skin.Fourway_Special_flowers;
//        }

//        #endregion

//        #region Market

//        internal static void rp_market(Building b)
//        {
//            Market market = b.GetComponent<Market>();

//            //Base Model
//            MeshFilter model = b.transform.Find("Offset/polySurface175").GetComponent<MeshFilter>();
//            model.mesh = null;

//            //Resource Stacks
//            GameObject stacks = market.stacks.gameObject;
//            market.stacks = null;
//            GameObject.Destroy(stacks);

//            //Resource Dropoff
//            Transform dropoff = market.dropOff;
//            market.dropOff = null;
//            GameObject.Destroy(dropoff.gameObject);


//        }


//        internal static void rep_market(Building b, MarketBuildingSkin skin)
//        {

//        }




//        #endregion

//        #region Orchard

//        internal static void rp_orchard(Building b)
//        {
//            GameObject model = b.gameObject.transform.Find("Offset/Container").GetChild(0).gameObject;
//            GameObject.Destroy(model);
//        }

//        #endregion

//    }
//}
