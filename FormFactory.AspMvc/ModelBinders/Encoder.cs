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
            var unprotect = MachineKey.Unprotect(Encoding.UTF7.GetBytes(typeString)) ?? new byte[0] {};
            return Type.GetType(Encoding.UTF7.GetString(unprotect));
        }

        public string WriteTypeToString(Type type)
        {
            Debug.Assert(type.AssemblyQualifiedName != null, "type.AssemblyQualifiedName != null");
            return Encoding.UTF7.GetString(MachineKey.Protect(Encoding.UTF7.GetBytes(type.AssemblyQualifiedName)));
        }
    }
}