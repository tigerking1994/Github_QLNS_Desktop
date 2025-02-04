using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtTtKeHoachVonQuery
    {
        public Guid IIdKeHoachVonId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public int? INamKeHoach { get; set; }
        public int ILoaiKeHoachVon { get; set; }
        public int ILoaiNamKhv { get; set; }
        public int ILoaiNamTamUng { get; set; }
        public int ICoQuanThanhToan { get; set; }
        public double FGiaTriThanhToanTN { get; set; }
        public double FGiaTriThanhToanNN { get; set; }
        public double FGiaTriThuHoiTrongNuoc { get; set; }
        public double FGiaTriThuHoiNgoaiNuoc { get; set; }
        public double FGiaTriKHV { get; set; }
        public double FGiaTriKHVDaThanhToan { get; set; }
        public string SDisplayName
        {
            get
            { 
                string sLoaiKeHoach = string.Empty;
                switch (ILoaiNamKhv)
                {
                    case (int)NamKeHoachEnum.Type.NAM_TRUOC:
                        if (ILoaiKeHoachVon == 1 || ILoaiKeHoachVon == 3)
                            sLoaiKeHoach = "Kế hoạch vốn năm chuyển sang";
                        else
                            sLoaiKeHoach = "Kế hoạch vốn ứng chuyển sang";
                        break;
                    case (int)NamKeHoachEnum.Type.NAM_NAY:
                        if (ILoaiKeHoachVon == 1)
                            sLoaiKeHoach = "Kế hoạch vốn năm";
                        else
                            sLoaiKeHoach = "Kế hoạch vốn ứng";
                        break;
                }
                return string.Format("{0}-{1}", SSoQuyetDinh, sLoaiKeHoach);
            }
        }
        public Guid ID_DuAn_HangMuc { get; set; }
    }
}
