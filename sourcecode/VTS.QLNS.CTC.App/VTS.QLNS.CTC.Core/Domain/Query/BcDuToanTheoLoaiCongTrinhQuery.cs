using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BcDuToanTheoLoaiCongTrinhQuery
    {
        public Guid IIDLoaiCongTrinh { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public Guid? IIDParent { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public int IThuTu { get; set; }
        public double FCapPhatBangLenhChi { get; set; }
        public double FCapPhatTaiKhoBac { get; set; }
        public double FSum
        {
            get
            {
                return FCapPhatBangLenhChi + FCapPhatTaiKhoBac;
            }
        }
        public bool BIsHangCha { get; set; }
    }
}
