using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public enum QDDauTuTypeEnum
    {
        CHIPHI = 1,
        NGUONVON = 2,
        HANGMUC = 3
    }

    public struct QDDauTuType
    {
        public const string CHI_PHI = "Chi phí";
        public const string NGUON_VON = "Nguồn vốn";
    }
    
    public struct HasQDDT
    {
        public const string Has_QDDT = "Có quyết định đầu tư";
        public const string No_QDDT = "Chưa có quyết định đầu tư";
        public const string No_CTDT = "Chưa có chủ trương đầu tư";
        public const string ALL = "Tất cả";
    }
    
}
