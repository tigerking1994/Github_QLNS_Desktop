using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhQtPheDuyetQuyetToanDAHTChiTietQuery
    {
        public Guid? ID { get; set; }
        public Guid? IIDDuAnId { get; set; }
        public Guid? IIDHopDongId { get; set; }
        public Guid? IIDThanhToanChiTietId { get; set; }
        public Guid? IIDKHTTNhiemVuChiId { get; set; }
        public Guid? IIDPheDuyetQuyetToanDAHTId { get; set; }

        public double? FHopDongUsd { get; set; }
        public double? FHopDongVnd { get; set; }
        public double? FKeHoachTTCPUsd { get; set; }
        public double? FKinhPhiDuocCapTongUsd { get; set; }
        public double? FKinhPhiDuocCapTongVnd { get; set; }
        public double? FQuyetToanDuocDuyetTongUsd { get; set; }
        public double? FQuyetToanDuocDuyetTongVnd { get; set; }
        public double? FSoSanhKinhPhiUsd { get; set; }
        public double? FSoSanhKinhPhiVnd { get; set; }
        public double? FThuaTraNSNNUsd { get; set; }
        public double? FThuaTraNSNNVnd { get; set; }
        public int? INamBaoCaoTu { get; set; }
        public int? INamBaoCaoDen { get; set; }

        // Another properties
        public bool? IsData { get; set; }

        public Guid? IIDDonViId { get; set; }
        public string STenDuAn { get; set; }
        public string STenDonVi { get; set; }
        public string STenHopDong { get; set; }
        public string STenNoiDungChi { get; set; }
        public string STenNhiemVuChi { get; set; }
        public int? ILoaiNoiDungChi { get; set; }
        public double? FHopDongUsdDuAn { get; set; }
        public double? FHopDongVndDuAn { get; set; }
        public double? FHopDongUsdHopDong { get; set; }
        public double? FHopDongVndHopDong { get; set; }
    }
}
