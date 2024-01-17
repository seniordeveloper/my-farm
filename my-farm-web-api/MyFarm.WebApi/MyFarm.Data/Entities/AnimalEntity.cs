namespace MyFarm.Data.Entities
{
    /// <summary>
    /// An entity that is used to map [dbo].[Animals] table.
    /// </summary>
    public class AnimalEntity
    {
        /// <summary>
        /// Gets or sets a primary key of this animal.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name of this animal.
        /// </summary>
        public string Name { get; set; }
    }
}
