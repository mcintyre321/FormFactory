using System.Collections.Generic;
using System.Dynamic;

namespace FormFactory.Standalone
{
    public class DictionaryDynamic : DynamicObject
    {
        private readonly IDictionary<string, object> _viewData;

        public DictionaryDynamic(IDictionary<string, object> viewData)
        {
            _viewData = viewData;
        }
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _viewData.Keys;
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_viewData.ContainsKey(binder.Name))
            {
                result = _viewData[binder.Name];
                return true;
            }
            result = null;
            return true;
        }
    }

}