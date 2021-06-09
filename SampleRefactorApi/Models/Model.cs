namespace SampleRefactorApi.Models
{
    public abstract class Model<TKey>
    {
        public TKey Id { get; set; }
    }
}