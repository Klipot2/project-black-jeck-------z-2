using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
 
namespace Poker
{
    static class Helper
    {
        public static IEnumerable<MethodInfo> GetMethodsBySignature(this Type type, Type returnType, params Type[] parameterTypes)
        //dot обьяснить
        {
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where((m) =>
            {
                if (m.ReturnType != returnType)
                {
                    return false;
                }
 
                var parameters = m.GetParameters();
 
                if ((parameterTypes == null || parameterTypes.Length == 0))
                {
                    return parameters.Length == 0;
                }
 
                if (parameters.Length != parameterTypes.Length)
                {
                    return false;
                }
 
                for (int i = 0; i < parameterTypes.Length; i++)
                {
                    if (parameters[i].ParameterType != parameterTypes[i])
                    {
                        return false;
                    }
                }
 
                return true;
            });
        }
    }
}