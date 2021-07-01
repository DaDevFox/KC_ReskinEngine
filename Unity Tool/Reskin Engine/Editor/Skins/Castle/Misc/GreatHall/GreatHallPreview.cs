#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Misc Buildings

    public class GreatHallPreview : GenericBuildingPreview<GreatHallSkin>
    {
        public override string path => $"{buildingsRoot}/GreatHall";
    }

    #endregion
}

#endif