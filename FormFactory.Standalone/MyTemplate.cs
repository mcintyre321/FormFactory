using System.Text;
using System.Threading.Tasks;

namespace FormFactory.Standalone
{
    public abstract class MyTemplate<TModel> : MyTemplate
    {
        public new TModel Model
        {
            get { return (TModel) base.Model;
                
            }
            set { base.Model = value; }
        }
    }

    public abstract class MyTemplate
    {


        public StringBuilder sb = new StringBuilder();

        // this will map to @Model (property name)
        public object Model { get; set; }

        public void WriteLiteral(string literal)
        {
            // replace that by a text writer for example
            sb.Append(literal);
        }

        public void BeginWriteAttribute(string literal)
        {
            // replace that by a text writer for example
            sb.Append("\"");
        }

        public void BeginWriteAttribute(object a, object b, object c, object d, object e, object f)
        {
            // replace that by a text writer for example
            sb.Append("\"");
        }

        public void WriteAttributeValue(object a, object b, object c, object d, object e, object f)
        {
            // replace that by a text writer for example
            sb.Append("\"");
        }

        public void WriteAttributeValue(string value)
        {
            // replace that by a text writer for example
            sb.Append(value.Replace("\"", "\"\""));
        }

        public void EndWriteAttribute()
        {
            // replace that by a text writer for example
            sb.Append("\"");
        }

        public void Write(object obj)
        {
            // replace that by a text writer for example
            sb.Append(obj);
        }

        public async virtual Task ExecuteAsync()
        {
            await Task.Yield(); // whatever, we just need something that compiles...
        }

        public MyFfHtmlHelper Html { get; set; }

        public override string ToString() => sb.ToString();
    }
}