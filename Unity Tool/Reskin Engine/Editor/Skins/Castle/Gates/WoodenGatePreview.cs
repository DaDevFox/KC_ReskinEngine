#if UNITY_EDITOR

namespace ReskinEngine.Editor
{
    #region Gates

    public class WoodenGatePreview : GatePreview<WoodenGateSkin>
    {
        public override string path => $"{buildingsRoot}/WoodenGate";
    }

    #endregion
}

#endif