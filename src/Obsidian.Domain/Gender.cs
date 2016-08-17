using Obsidian.Domain;

namespace Obsidian.Domain
{
    /// <summary>
    /// Represents the gender of a <see cref="User"/>.
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// It a secret.
        /// </summary>
        Privacy = 0,

        /// <summary>
        /// Male.
        /// </summary>
        Male = 1,

        /// <summary>
        /// Female
        /// </summary>
        Female = 2
    }
}