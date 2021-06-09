using SampleRefactorApi.Models;

namespace SampleRefactorApi.Data
{
    public interface IRepository<TModel, TKey>
        where TModel : Model<TKey>
    {
        /// <summary>
        /// Inserts the provided <typeparamref name="TModel"/> to the database and returns the <typeparamref name="TKey"/> for that model.
        /// </summary>
        /// <param name="item">The model to insert.</param>
        /// <returns>The unique identifier for this record.</returns>
        TKey Add(TModel item);

        /// <summary>
        /// Gets the <typeparamref name="TModel"/> based on the unique <typeparamref name="TKey"/> identifier
        /// </summary>
        /// <param name="id">The id of the <typeparamref name="TModel"/> to select</param>
        /// <returns>A instance of <typeparamref name="TModel"/> populated from the database</returns>
        TModel Get(TKey id);
    }
}