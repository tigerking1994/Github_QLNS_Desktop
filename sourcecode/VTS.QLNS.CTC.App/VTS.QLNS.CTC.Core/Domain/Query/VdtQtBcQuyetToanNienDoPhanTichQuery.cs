using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtQtBcQuyetToanNienDoPhanTichQuery
    {
        public bool BIsChuyenTiep { get; set; }
        public Guid IIdDuAnId { get; set; }
        public string STenDuAn { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public string SMaLoaiCongTrinh { get; set; }

        /// <summary>
        ///  col 1
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiKbNamTruoc { get; set; }
        /// <summary>
        /// col 2
        /// </summary>        
        public double FDuToanCnsChuaGiaiNganTaiDvNamTruoc { get; set; }
        /// <summary>
        /// col 3
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiCucNamTruoc { get; set; }
        /// <summary>
        /// col 23
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiKb { get; set; }
        /// <summary>
        /// col 22
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiDv { get; set; }
        /// <summary>
        /// col 21
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiCuc { get; set; }
        /// <summary>
        /// col 18
        /// </summary>
        public double FTuChuaThuHoiTaiCuc { get; set; }
        /// <summary>
        /// col 6
        /// </summary>
        public double FChiTieuNamNayKb { get; set; }
        /// <summary>
        /// col 7
        /// </summary>
        public double FChiTieuNamNayLc { get; set; }
        /// <summary>
        /// col 10
        /// </summary>
        public double FSoCapNamTrcCs { get; set; }
        /// <summary>
        /// col 11
        /// </summary>
        public double FSoCapNamNay { get; set; }
        /// <summary>
        /// col 13
        /// </summary>
        public double FDnQuyetToanNamTrc { get; set; }
        /// <summary>
        /// col 14
        /// </summary>
        public double FDnQuyetToanNamNay { get; set; }
        /// <summary>
        /// col 19
        /// </summary>
        public double FTuChuaThuHoiTaiDonVi { get; set; }
        /// <summary>
        /// col 24
        /// </summary>
        public double FDuToanThuHoi { get; set; }
    }
}
