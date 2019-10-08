namespace Updog.Domain {
    public sealed class VoteReadView : IValueObject {
        #region Properties
        public VoteDirection Direction { get; set; }
        #endregion
    }
}