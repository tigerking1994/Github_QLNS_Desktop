using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhQtPheDuyetQuyetToanDAHTChiTietModel : ModelBase
    {
        public Guid? IIDDuAnId { get; set; }
        public Guid? IIDHopDongId { get; set; }
        public Guid? IIDThanhToanChiTietId { get; set; }
        public Guid? IIDKHTTNhiemVuChiId { get; set; }
        public Guid? IIDPheDuyetQuyetToanDAHTId { get; set; }
        public Guid? IIDDonViId { get; set; }

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
        public bool? IsSum { get; set; }
        public int? SLevel { get; set; }

        public string STenDuAn { get; set; }
        public string STenHopDong { get; set; }
        public string STenNoiDungChi { get; set; }
        public string STenNhiemVuChi { get; set; }
        public string STenDonVi { get; set; }
        public int? ILoaiNoiDungChi { get; set; }
        public double? FHopDongUsdDuAn { get; set; }
        public double? FHopDongVndDuAn { get; set; }
        public double? FHopDongUsdHopDong { get; set; }
        public double? FHopDongVndHopDong { get; set; }
        public double? FSumTTCP { get; set; }
        public double? FSumKPDCUsd { get; set; }
        public double? FSumKPDCVnd { get; set; }
        public double? FSumQTDDUsd { get; set; }
        public double? FSumQTDDVnd { get; set; }
        public string depth { get; set; }
        public List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan> lstData { get; set; }
        public List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan> listDataTTCP { get; set; }
        public List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan> listDataKPDC { get; set; }
        public List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan> listDataQTDD { get; set; }
        #region data generate
        public double? FKeHoachTTCPUsd1 { get; set; }
        public double? FKeHoachTTCPUsd2 { get; set; }
        public double? FKeHoachTTCPUsd3 { get; set; }
        public double? FKeHoachTTCPUsd4 { get; set; }
        public double? FKeHoachTTCPUsd5 { get; set; }
        public double? FKeHoachTTCPUsd6 { get; set; }
        public double? FKeHoachTTCPUsd7 { get; set; }
        public double? FKeHoachTTCPUsd8 { get; set; }
        public double? FKeHoachTTCPUsd9 { get; set; }
        public double? FKeHoachTTCPUsd10 { get; set; }

        public double? FKinhPhiDuocCapTongVnd1 { get; set; }
        public double? FKinhPhiDuocCapTongVnd2 { get; set; }
        public double? FKinhPhiDuocCapTongVnd3 { get; set; }
        public double? FKinhPhiDuocCapTongVnd4 { get; set; }
        public double? FKinhPhiDuocCapTongVnd5 { get; set; }
        public double? FKinhPhiDuocCapTongVnd6 { get; set; }
        public double? FKinhPhiDuocCapTongVnd7 { get; set; }
        public double? FKinhPhiDuocCapTongVnd8 { get; set; }
        public double? FKinhPhiDuocCapTongVnd9 { get; set; }
        public double? FKinhPhiDuocCapTongVnd10 { get; set; }

        public double? FKinhPhiDuocCapTongUsd1 { get; set; }
        public double? FKinhPhiDuocCapTongUsd2 { get; set; }
        public double? FKinhPhiDuocCapTongUsd3 { get; set; }
        public double? FKinhPhiDuocCapTongUsd4 { get; set; }
        public double? FKinhPhiDuocCapTongUsd5 { get; set; }
        public double? FKinhPhiDuocCapTongUsd6 { get; set; }
        public double? FKinhPhiDuocCapTongUsd7 { get; set; }
        public double? FKinhPhiDuocCapTongUsd8 { get; set; }
        public double? FKinhPhiDuocCapTongUsd9 { get; set; }
        public double? FKinhPhiDuocCapTongUsd10 { get; set; }

        public double? FQuyetToanDuocDuyetTongUsd1 { get; set; }
        public double? FQuyetToanDuocDuyetTongUsd2 { get; set; }
        public double? FQuyetToanDuocDuyetTongUsd3 { get; set; }
        public double? FQuyetToanDuocDuyetTongUsd4 { get; set; }
        public double? FQuyetToanDuocDuyetTongUsd5 { get; set; }
        public double? FQuyetToanDuocDuyetTongUsd6 { get; set; }
        public double? FQuyetToanDuocDuyetTongUsd7 { get; set; }
        public double? FQuyetToanDuocDuyetTongUsd8 { get; set; }
        public double? FQuyetToanDuocDuyetTongUsd9 { get; set; }
        public double? FQuyetToanDuocDuyetTongUsd10 { get; set; }

        public double? FQuyetToanDuocDuyetTongVnd1 { get; set; }
        public double? FQuyetToanDuocDuyetTongVnd2 { get; set; }
        public double? FQuyetToanDuocDuyetTongVnd3 { get; set; }
        public double? FQuyetToanDuocDuyetTongVnd4 { get; set; }
        public double? FQuyetToanDuocDuyetTongVnd5 { get; set; }
        public double? FQuyetToanDuocDuyetTongVnd6 { get; set; }
        public double? FQuyetToanDuocDuyetTongVnd7 { get; set; }
        public double? FQuyetToanDuocDuyetTongVnd8 { get; set; }
        public double? FQuyetToanDuocDuyetTongVnd9 { get; set; }
        public double? FQuyetToanDuocDuyetTongVnd10 { get; set; }

        #endregion

    }

    public class NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan : NH_QT_PheDuyetQuyetToanDAHTGiaiDoan
    {
        public Guid ID { get; set; }
        public string sGiaiDoan { get; set; }
        public virtual double? value { get; set; }
        public virtual double? valueUSD { get; set; }
        public virtual double? valueVND { get; set; }


    }
    public class NH_QT_PheDuyetQuyetToanDAHTGiaiDoan
    {
        public int? INamBaoCaoTu { get; set; }
        public int? INamBaoCaoDen { get; set; }

    }

}
