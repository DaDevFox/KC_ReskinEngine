#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Gates

    public class StoneGatePreview : GatePreview<StoneGateSkin>
    {
        public override string path => $"{buildingsRoot}/Gate";
    }

    #endregion
}

#endif