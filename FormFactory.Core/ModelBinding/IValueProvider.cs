namespace FormFactory.ModelBinding
{
    public interface IValueProvider
    {
        IValueProviderResult GetValue(string key);
    }
}