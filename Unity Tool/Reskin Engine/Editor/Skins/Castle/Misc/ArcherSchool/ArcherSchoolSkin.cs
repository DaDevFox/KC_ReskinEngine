using UnityEngine;

#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Castle Stairs



    #endregion

    #region Misc Buildings

    public class ArcherSchoolSkin : GenericBuildingSkin
    {
        public override string UniqueName => "archerschool";
        public override string friendlyName => "Archer School";

        public override Vector3 bounds => new Vector3(3f, 1f, 2f);
    }

    #endregion
}

#endif