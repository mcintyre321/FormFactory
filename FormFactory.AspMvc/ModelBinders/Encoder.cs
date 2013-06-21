using System;
using System.Diagnostics;
using System.Text;
using System.Web.Security;
using FormFactory.ModelBinding;

namespace FormFactory.AspMvc.ModelBinders
{
    public class Encoder : IStringEncoder
    {
        public Type ReadTypeFromString(string typeString)
        {
            Debug.Assert(typeString != null, "typeString != null");
            return Type.GetType(Encoding.UTF7.GetString(MachineKey.Decode(typeString, MachineKeyProtection.All)));
        }

        public string WriteTypeToString(Type type)
        {
            Debug.Assert(type.AssemblyQualifiedName != null, "type.AssemblyQualifiedName != null");
            return MachineKey.Encode(Encoding.UTF7.GetBytes(type.AssemblyQualifiedName), MachineKeyProtection.All);
        }
    }
}