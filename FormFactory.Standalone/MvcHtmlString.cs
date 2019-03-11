namespace FormFactory.Standalone
{
    public class MvcHtmlString
    {
        private readonly string _value;

        public MvcHtmlString(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }
        public string ToHtmlString()
        {
            return _value;
        }
        public static implicit operator string(MvcHtmlString str) => str.ToString();
    }
}