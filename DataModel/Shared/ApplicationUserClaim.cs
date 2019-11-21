namespace DataModel.Shared
{
    /// <summary>
    /// Holds an ApplicationClaim
    /// <see cref="ApplicationClaim"/>
    /// <para>string ClaimType</para>
    /// <para>string ClaimValue</para>
    /// </summary>
    public class ApplicationUserClaim : ApplicationClaim
    {
        /// <summary>
        /// Holds the ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Holds the UserID
        /// </summary>
        public string UserID { get; set; }
    }
}