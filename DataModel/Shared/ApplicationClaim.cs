namespace DataModel.Shared
{
    /// <summary>
    /// Holds an ApplicationClaim
    /// <para>string ClaimType</para>
    /// <para>string ClaimValue</para>
    /// </summary>
    public class ApplicationClaim
    {
        /// <summary>
        /// Holds the ClaimType
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// Holds the ClaimValue
        /// </summary>
        public string ClaimValue { get; set; }
    }
}