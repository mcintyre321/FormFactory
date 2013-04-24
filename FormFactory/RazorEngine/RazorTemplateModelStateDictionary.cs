namespace FormFactory.RazorEngine
{
    public class RazorTemplateModelStateDictionary : IModelStateDictionary
    {
        public bool TryGetValue(string key, out ModelState modelState)
        {
            modelState = null;
            return false;
        }
    }
}