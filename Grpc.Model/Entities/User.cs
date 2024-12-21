namespace Model.Entities
{
    /// <summary>
    /// The User Entity
    /// </summary>
    public class User : BaseEntity<Guid>
    {
        /// <summary>
        /// The first name of user
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// The last name of user
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// The time of DOB of user
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// The national code of user
        /// </summary>
        public string NationalCode { get; set; } = string.Empty;
    }
}
