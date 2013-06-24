namespace FormFactory.RazorEngine
{
    public class RazorTemplateModelStateDictionary : IModelStateDictionary
    {
        public bool TryGetValue(string key, out ModelState modelState)
        {
            modelState = null;
            return false;
        }

        public ModelState this[string key]
        {
            get { return null; }
        }

        public bool ContainsKey(string key)
        {
            return false;
        }
    }
}