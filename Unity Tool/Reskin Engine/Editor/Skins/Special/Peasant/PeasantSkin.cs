using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public class PeasantSkin : Skin
    {
        public override string typeId => "peasant";

        public override GameVersion version => GameVersion.STABLE;

        [Tooltip("The root of this gameobject's material will have its color changed to a random skin color; instance model")]
        public GameObject head;
        /// <summary>
        /// <para>default value: (0f, 0.1410001f, 0f)</para>
        /// </summary>
        [Tooltip("default value: (0f, 0.1410001f, 0f)")]
        public Vector3 headPosition = new Vector3(0f, 0.1410001f, 0f);
        public Vector3 headScale = new Vector3(0.04514214f, 0.04514214f, 0.04514214f);

        [Tooltip("The root of this gameobject's material will ahve its color changed to a random shirt color; instance model")]
        public GameObject body;
        /// <summary>
        /// <para>default value: (0f, 0.0879999f, 0f)</para>
        /// </summary>
        [Tooltip("default value: (0f, 0.0879999f, 0f)")]
        public Vector3 bodyPosition = new Vector3(0, 0.0879999f, 0);
        public Vector3 bodyScale = new Vector3(0.07660437f, 0.07660437f, 0.07660437f);

        [Tooltip("This gameobject doesn't have any special behavior; just a basic instance model")]
        public GameObject legs;
        /// <summary>
        /// Negative values will go through the floor
        /// <para>default value: (0f, 0.02899987f, 0f)</para>
        /// </summary>
        [Tooltip("Negative values will go through the floor\ndefault value: (0f, 0.02899987f, 0f)")]
        public Vector3 legsPosition = new Vector3(0, 0.02899987f, 0);
        public Vector3 legsScale = new Vector3(0.05853106f, 0.05853106f, 0.05853106f);

        public string[] outlineMeshes; 
    }
}

#endif