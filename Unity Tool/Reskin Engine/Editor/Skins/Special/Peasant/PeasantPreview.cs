using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ReskinEngine.Utils;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public class PeasantPreview : SkinPreview<PeasantSkin>
    {
        public static string peasantModel = $"{environmentRoot}/Person 1";

        public override GameObject Create()
        {
            return GameObject.Instantiate(Resources.Load<GameObject>(peasantModel)); 
        }

        public override void Apply(GameObject obj, PeasantSkin skin)
        {
            base.Apply(obj, skin);

            GameObject head = obj.transform.Find("GameObject").Find("Head").gameObject;
            GameObject body = obj.transform.Find("GameObject").Find("Body").gameObject;
            GameObject legs = obj.transform.Find("GameObject").Find("Legs").gameObject;

            if (skin.head)
            {
                head.GetComponent<MeshFilter>().mesh = null;

                head.transform.localPosition = skin.headPosition;
                head.transform.localScale = skin.headScale;

                // ONLY FOR UNITY TOOL; to allow binding multiple times
                if(head.transform.childCount > 0)
                    head.ClearChildren();

                GameObject.Instantiate(skin.head, head.transform);

            }

            if (skin.body)
            {
                body.GetComponent<MeshFilter>().mesh = null;
                GameObject.Instantiate(skin.body, body.transform);

                body.transform.localPosition = skin.bodyPosition;
                body.transform.localScale = skin.bodyScale;
            }

            if (skin.legs)
            {
                legs.GetComponent<MeshFilter>().mesh = null;
                GameObject.Instantiate(skin.legs, legs.transform);

                legs.transform.localPosition = skin.legsPosition;
                legs.transform.localScale = skin.legsScale;
            }            
        }
    }
}

#endif
