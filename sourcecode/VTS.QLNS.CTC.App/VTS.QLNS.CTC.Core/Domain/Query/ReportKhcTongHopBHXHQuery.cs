using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportKhcTongHopBHXHQuery
    {
        public int STT { get; set; }
        public string IDDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SM { get; set; }
        public int SoDaThucHienNamTruoC { get; set; }

        public int SoUocThucHienNamTruoc { get; set; }

        public int SoKeHoachThucHienNamNay { get; set; }

        public int SoSQ { get; set; }

        public int SoQNCN { get; set; }

        public int SoCNVQP { get; set; }

        public int SoLDHD { get; set; }

        public int SoHSQBS { get; set; }

        public double TienDaThucHienNamTruoc { get; set; }

        public double TienUocThucHienNamTruoc { get; set; }

        public double TienKeHoachThucHienNamNay { get; set; }

        public double TienSQ { get; set; }

        public double TienQNCN { get; set; }

        public double TienCNVQP { get; set; }

        public double TienLDHD { get; set; }

        public double TienHSQBS { get; set; }

        public string SLNS { get; set; }
    }
}
