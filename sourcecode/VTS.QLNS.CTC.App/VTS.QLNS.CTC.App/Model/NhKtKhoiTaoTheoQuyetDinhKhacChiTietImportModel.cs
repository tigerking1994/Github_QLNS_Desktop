using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Import;
using ColumnAttribute = VTS.QLNS.CTC.App.Model.Import.ColumnAttribute;
namespace VTS.QLNS.CTC.App.Model
{
    public class NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel : ModelBase
    {
        public string STT { get; set; }

        public string NoiDung { get; set; }

        public string SLoai { get; set; }
        public string SMaNhiemVuChi { get; set; }
        public string SSoQuyetDinhDauTu { get; set; }
        public string SSoHopDong { get; set; }
        public string SSoQuyetDinhKhac { get; set; }
        public string STenVietTatChiPhi { get; set; }
        public string STenChiPhiDetail { get; set; }
        public string TenChiPhiDisplay
        {
            get
            {
                if (string.IsNullOrEmpty(this.SMaChiPhi) && string.IsNullOrEmpty(this.STenVietTatChiPhi))
                    return string.Empty;
                return $"{this.SMaChiPhi} - {this.STenVietTatChiPhi}";
            }
        }
        public string SMaChiPhi { get; set; }
        public Guid? IIdKhoiTaoCapPhatID { get; set; }
        public Guid? IIdDuAnID { get; set; }
        public Guid? IIdHopDongID { get; set; }
        public double? FQTKinhPhiDuyetCacNamTruocUSD { get; set; }
        public double? FQTKinhPhiDuyetCacNamTruocVND { get; set; }
        public double? FDeNghiQTNamNayUSD { get; set; }
        public double? FDeNghiQTNamNayVND { get; set; }
        public double? FLuyKeKinhPhiDuocCapUSD { get; set; }
        public double? FLuyKeKinhPhiDuocCapVND { get; set; }
        public Guid? IIdParentID { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd { get; set; }
        public double? FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd { get; set; }
        public double? FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd { get; set; }
        public double? FDeNghiChuyenNamSauUsd { get; set; }
        public double? FDeNghiChuyenNamSauVnd { get; set; }
        public double? FKinhPhiThuaNopNsnnUSD { get; set; }
        public double? FKinhPhiThuaNopNsnnVND { get; set; }
        public double? FConLaiChuaGiaiNganUSD { get; set; }
        public double? FConLaiChuaGiaiNganVND { get; set; }
        public double? FGiaTriNoiDungUSD { get; set; }
        public double? FGiaTriNoiDungVND { get; set; }
        public Guid? IIdKhttNhiemVuChiID { get; set; }
        public Guid? IIdQuyetDinhKhacID { get; set; }
        public int? ILoaiNoiDung { get; set; }
        public string SMaNoiDung { get; set; }
        public string STenNoiDung { get; set; }
        public DateTime? DNgayNoiDung { get; set; }
        public Guid? IIdChiPhiID { get; set; }

        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }
        public bool BHangCha { get; set; }
    }
}
