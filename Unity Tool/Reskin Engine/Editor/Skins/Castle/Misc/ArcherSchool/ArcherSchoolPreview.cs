#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Misc Buildings

    public class ArcherSchoolPreview : GenericBuildingPreview<ArcherSchoolSkin>
    {
        public override string path => $"{buildingsRoot}/ArcherSchool";
    }

    #endregion
}

#endif