namespace MyFarm.ApiModels.Animal
{
    /// <summary>
    /// A model class used to manage animal.
    /// </summary>
    public class AnimalModel
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
