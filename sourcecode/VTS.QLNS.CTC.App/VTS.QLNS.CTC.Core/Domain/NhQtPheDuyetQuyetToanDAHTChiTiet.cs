using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhQtPheDuyetQuyetToanDAHTChiTiet : EntityBase
    {
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


    }
}
