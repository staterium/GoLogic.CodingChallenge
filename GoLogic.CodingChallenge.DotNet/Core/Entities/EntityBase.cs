namespace Core.Entities
{
    /// <summary>
    ///     The generic base class that all entities inherits from, and ensures that an entity has an Id property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityBase<T>
    {
        #region Properties

        public T Id { get; set; } = default!;

        #endregion
    }
}