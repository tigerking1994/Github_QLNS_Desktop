using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace VTS.QLNS.CTC.Core.Extensions
{
    public static class ObjExtension
    {
        public static void CloneObj<T>(this T source, T destination) where T : class
        {
            PropertyInfo[] destinationProperties = destination.GetType().GetProperties();
            foreach (PropertyInfo destinationPi in destinationProperties)
            {
                PropertyInfo sourcePi = source.GetType().GetProperty(destinationPi.Name);
                destinationPi.SetValue(destination, sourcePi.GetValue(source, null), null);
            }
        }
    }
}
