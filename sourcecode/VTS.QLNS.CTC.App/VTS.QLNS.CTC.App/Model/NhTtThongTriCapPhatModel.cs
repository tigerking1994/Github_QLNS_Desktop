using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhTtThongTriCapPhatModel : ModelBase
    {
        public string IIdMaDonViId { get; set; }
        public Guid? IIdDonViId { get; set; }
        public string SMaThongTri { get; set; }
        public DateTime? DNgayLapThongTri { get; set; }
        public int? INamThucHien { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public DateTime? DNgayGhiSo { get; set; }
        public string STk1 { get; set; }
        public string SSoCt1 { get; set; }
        public string STk2 { get; set; }
        public string SSoCt2 { get; set; }
        public Guid? IIdTiGiaUsdVndid { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
        public double? FTongGiaTriNgoaiTeKhac { get; set; }
        public double? FTongGiaTriUsd { get; set; }
        public double? FTongGiaTriVnd { get; set; }
        public string STongGiaTriBangChu { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiXoa { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public bool BIsActive { get; set; }
        public bool BIsGoc { get; set; }
        public bool BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public bool BIsXoa { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public int? IIdNguonVonId { get; set; }
        public string STenDonVi { get; set; }
        public string STenNguonVon { get; set; }
        public string STenTienTe { get; set; }
        public double? FTongGiaTriEUR { get; set; }
        private ObservableCollection<NhTtThongTriCapPhatChiTietModel> _nhTtThongTriCapPhatChiTiets = new ObservableCollection<NhTtThongTriCapPhatChiTietModel>();
        public ObservableCollection<NhTtThongTriCapPhatChiTietModel> NhTtThongTriCapPhatChiTiets
        {
            get => _nhTtThongTriCapPhatChiTiets;
            set => SetProperty(ref _nhTtThongTriCapPhatChiTiets, value);
        }
    }
}
