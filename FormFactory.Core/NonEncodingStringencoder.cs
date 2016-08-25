using System;
using FormFactory.ModelBinding;

namespace FormFactory
{
    public class NonEncodingStringencoder : IStringEncoder
    {
        public Type ReadTypeFromString(string typeString)
        {
            return Type.GetType(typeString);
        }

        public string WriteTypeToString(Type type)
        {
            return type.AssemblyQualifiedName;
        }
    }
}