using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhTtThucHienNganSachQuery
    {
        public virtual Guid ID { get; set; }
        public virtual Guid? iID_ThanhToanID { get; set; }
        public virtual Guid? iID_MucLucNganSachID { get; set; }
        public virtual Guid? iID_MLNS_ID { get; set; }
        public virtual string sTenNoiDungChi { get; set; }
        public virtual double? fDeNghiCapKyNay_USD { get; set; }
        public virtual double? fDeNghiCapKyNay_VND { get; set; }
        public virtual double? fPheDuyetCapKyNay_USD { get; set; }
        public virtual double? fPheDuyetCapKyNay_VND { get; set; }

        public Guid IDNhiemVuChi { get; set; }
        public Guid IDDuAn { get; set; }
        public Guid IDHopDong { get; set; }
        public Guid iID_DonVi { get; set; }
        public int iNamKeHoach { get; set; }
        public int? iGiaiDoanDen { get; set; }
        public int? iGiaiDoanTu { get; set; }
        public int iLoaiNoiDungChi { get; set; }
        public double? HopDongUSD { get; set; } = 0;
        public double? HopDongVND { get; set; } = 0;
        public double? NCVTTCP { get; set; } = 0;
        public double? NhiemVuChi { get; set; } = 0;
        public double? KinhPhiUSD { get; set; } = 0;
        public double? TongKinhPhiUSD { get; set; } = 0;
        public double? KinhPhiVND { get; set; } = 0;
        public double? TongKinhPhiVND { get; set; } = 0;
        public double? KinhPhiToYUSD { get; set; } = 0;
        public double? KinhPhiToYVND { get; set; } = 0;
        public double? KinhPhiDaChiUSD { get; set; } = 0;
        public double? KinhPhiDaChiVND { get; set; } = 0;
        public double? KinhPhiDaChiToYUSD { get; set; } = 0;
        public double? KinhPhiDaChiToYVND { get; set; } = 0;
        public double? TongKinhPhiDaChiUSD { get; set; } = 0;
        public double? TongKinhPhiDaChiVND { get; set; } = 0;
        public string sTenNhiemVuChi { get; set; }
        public string sTenDuAn { get; set; }
        public string sTenHopDong { get; set; }
        public double? KinhPhiDuocCapChuaChiUSD { get; set; } = 0;
        public double? KinhPhiDuocCapChuaChiVND { get; set; } = 0;
        public double? QuyGiaiNganTheoQuy { get; set; } = 0;
        public double? fLuyKeKinhPhiDuocCap_USD { get; set; } = 0;
        public double? fLuyKeKinhPhiDuocCap_VND { get; set; } = 0;
        public double? fDeNghiQTNamNay_USD { get; set; } = 0;
        public double? fDeNghiQTNamNay_VND { get; set; } = 0;
        public double? KinhPhiChuaQuyetToanUSD { get; set; } = 0;
        public double? KinhPhiChuaQuyetToanVND { get; set; } = 0;
        public double? KeHoachGiaiNgan { get; set; } = 0;
        public Boolean? isSum { get; set; }
        public string isTitle { get; set; }
        public Boolean? isHopDong { get; set; }
        public Boolean? isDuAn { get; set; }
        public string sTenCDT { get; set; }
        public string sTenDonVi { get; set; }
        public string depth { get; set; }
        public DateTime dNgayDeNghi { get; set; }
        public List<NhTtThucHienNganSachGiaiDoanQuery> lstGiaiDoanTTCP { get; set; }
        public List<NhTtThucHienNganSachGiaiDoanQuery> lstGiaiDoanKinhPhiDaGiaiNgan { get; set; }
        public List<NhTtThucHienNganSachGiaiDoanQuery> lstGiaiDoanKinhPhiDuocCap { get; set; }

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
}
