using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    public class GreatHallSkin : GenericBuildingSkin
    {
        public override string UniqueName => "greathall";
        public override string friendlyName => "Great Hall";

        public override Vector3 bounds => new Vector3(3f, 1f, 2f);
    }

}

#endif