namespace DataModel.Shared
{
    public class ApplicationRoleClaim : ApplicationClaim
    {
        /// <summary>
        /// Holds the ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Holds the RoleID
        /// </summary>
        public string RoleID { get; set; }
    }
}