namespace Obsidian.Domain
{
    public class ObsidianClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }

        #region Equality

        public override bool Equals(object obj)
        {
            if (obj is ObsidianClaim claim)
            {
                return claim.Type == Type && claim.Value == Value;
            }
            return false;
        }

        public override int GetHashCode() => 31 * Type.GetHashCode() + Value.GetHashCode();

        #endregion Equality
    }
}
