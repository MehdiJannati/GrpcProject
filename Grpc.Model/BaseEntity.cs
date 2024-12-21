using Model.Entities;

namespace Model
{
    public abstract class BaseEntity<T> where T : struct
    {
        /// <summary>
        /// The type of Id field in each table has created in system
        /// </summary>
        public T Id { get; set; }
        /// <summary>
        /// The date time of data has created in system
        /// </summary>
        public DateTime? CreatedDateTime { get; set; }
        /// <summary>
        /// The date time of data has updated in system
        /// </summary>
        public DateTime? UpdatedDataTime { get; set; }
        /// <summary>
        /// The id of user where created data in system
        /// </summary>
        public Guid? CreatedUserId { get; set; }
        /// <summary>
        /// The id of user where updated data in system
        /// </summary>
        public Guid? UpdatedUserId { get; set; }
        /// <summary>
        /// The navigation property of user for data of created user field
        /// </summary>
        public User? CreatedUser { get; set; }
        /// <summary>
        /// The navigation property of user for data updated user field
        /// </summary>
        public User? UpdatedUser { get; set; }
    }
}
