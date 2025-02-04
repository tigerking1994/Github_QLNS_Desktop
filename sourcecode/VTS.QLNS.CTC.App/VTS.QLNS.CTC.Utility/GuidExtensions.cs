using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility
{
    public static class GuidExtensions
    {
        public static bool IsNullOrEmpty(this Guid guid) => guid == null || guid == Guid.Empty;
        public static bool IsNullOrEmpty(this Guid? guid) => guid == null || guid == Guid.Empty;
    }
}
