using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhPbdttmBHYTModel : BindableBase
    {
        public Guid Id { get; set; }

        public string SSoChungTu { get; set; }

        public DateTime? DNgayChungTu { get; set; }

        public string SSoQuyetDinh { get; set; }

        public DateTime? DNgayQuyetDinh { get; set; }

        public int INamLamViec { get; set; }

        public string SDSLNS { get; set; }

        public string sDS_IDMaDonVi;
        public string SDS_IDMaDonVi { get => sDS_IDMaDonVi; set => SetProperty(ref sDS_IDMaDonVi, value); }

        public int ILoaiDuToan { get; set; }

        public Double? _fDuToan;
        public Double? FDuToan { get => _fDuToan; set => SetProperty(ref _fDuToan, value); }

        public Double? _fTongDuToanTruocDieuChinh;
        public Double? FTongDuToanTruocDieuChinh { get => _fTongDuToanTruocDieuChinh; set => SetProperty(ref _fTongDuToanTruocDieuChinh, value); }

        public Double? _fTongDuToan;
        public Double? FTongDuToan { get => _fTongDuToan; set => SetProperty(ref _fTongDuToan, value); }

        public Double? _fTongDuToanSauDieuChinh;
        public Double? FTongDuToanSauDieuChinh { get => _fTongDuToanSauDieuChinh; set => SetProperty(ref _fTongDuToanSauDieuChinh, value); }

        public string SNguoiTao { get; set; }

        public string SNguoiSua { get; set; }

        public DateTime? DNgayTao { get; set; }

        public DateTime? DNgaySua { get; set; }

        private bool _bIsKhoa;
        public bool BIsKhoa { get => _bIsKhoa; set => SetProperty(ref _bIsKhoa, value); }

        public string SMoTa { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public string sLoaiDuToan => ILoaiDuToan switch
        {
            1 => "Đầu năm",
            3 => "Điều chỉnh",
            2 => "Bổ sung",
            _ => string.Empty
        };

        public string SDS_DotNhan { get; set; }
        public string SDS_TenDotNhan { get; set; }
    }
}
