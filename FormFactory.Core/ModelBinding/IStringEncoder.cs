using System;

namespace FormFactory.ModelBinding
{
    public interface IStringEncoder
    {
        Type ReadTypeFromString(string typeString);
        string WriteTypeToString(Type type);
    }
}