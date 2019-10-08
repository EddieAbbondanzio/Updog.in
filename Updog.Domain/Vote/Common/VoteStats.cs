namespace Updog.Domain {
    public sealed class VoteStats : IValueObject {
        #region Properties
        public int Upvotes { get; private set; }
        public int Downvotes { get; private set; }
        #endregion

        #region Constructor(s)
        public VoteStats() { }

        public VoteStats(int ups, int downs) {
            Upvotes = ups;
            Downvotes = downs;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Add a new vote to the counts.
        /// </summary>
        /// <param name="vote">The type of vote to add.</param>
        public void AddVote(VoteDirection vote) {
            switch (vote) {
                case VoteDirection.Up:
                    Upvotes++;
                    break;
                case VoteDirection.Down:
                    Downvotes++;
                    break;
            }
        }

        /// <summary>
        /// Remove a vote from the couns.
        /// </summary>
        /// <param name="vote">The vote to remove.</param>
        public void RemoveVote(VoteDirection vote) {
            switch (vote) {
                case VoteDirection.Up:
                    Upvotes--;
                    break;
                case VoteDirection.Down:
                    Downvotes--;
                    break;
            }
        }
        #endregion
    }
}