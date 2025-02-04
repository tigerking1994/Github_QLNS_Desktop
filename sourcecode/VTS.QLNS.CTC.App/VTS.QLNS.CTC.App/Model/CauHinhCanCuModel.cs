using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class CauHinhCanCuModel : ModelBase
    {
        private List<Guid> _lstIdChungTuCanCu;
        public List<Guid> LstIdChungTuCanCu
        {
            get => _lstIdChungTuCanCu;
            set => SetProperty(ref _lstIdChungTuCanCu, value);
        }

        private Guid _idChungTuCanCuLuyKe;
        public Guid IdChungTuCanCuLuyKe
        {
            get => _idChungTuCanCuLuyKe;
            set => SetProperty(ref _idChungTuCanCuLuyKe, value);
        }

        private string _idLns;
        public string IdLns
        {
            get => _idLns;
            set => SetProperty(ref _idLns, value);
        }

        private string _mlnsSelected;
        public string MlnsSelected
        {
            get => _mlnsSelected;
            set => SetProperty(ref _mlnsSelected, value);
        }

        private string _nganhSelected;
        public string NganhSelected
        {
            get => _nganhSelected;
            set => SetProperty(ref _nganhSelected, value);
        }

        private string _sModule;
        [DisplayName("Module")]
        [DisplayDetailInfo("Module")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadAllCauHinhCanCuModule")]
        public string SModule 
        {
            get => _sModule;
            set => SetProperty(ref _sModule, value); 
        }

        private string _iIDMaChucNang;
        [DisplayName("Căn cứ")]
        [DisplayDetailInfo("Căn cứ")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadAllCauHinhCanCu")]
        public string IIDMaChucNang
        {
            get => _iIDMaChucNang;
            set => SetProperty(ref _iIDMaChucNang, value);
        }

        private string _sTenCot;
        [DisplayName("Tên cột")]
        [DisplayDetailInfo("Tên cột")]
        public string STenCot 
        {
            get => _sTenCot;
            set => SetProperty(ref _sTenCot, value);
        }

        private int? _iNamCanCu;
        [DisplayName("Năm")]
        [DisplayDetailInfo("Năm")]
        public int? INamCanCu 
        {
            get => _iNamCanCu;
            set => SetProperty(ref _iNamCanCu, value);
        }

        private int? _iNamLamViec;
        [DisplayDetailInfo("Năm làm việc")]
        public int? INamLamViec
        {
            get => _iNamLamViec;
            set => SetProperty(ref _iNamLamViec, value);
        }

        private int? _iThietLap;
        [DisplayName("Thiết lập")]
        [DisplayDetailInfo("Thiết lập")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadAllCauHinhCanCuThietLap")]
        public int? IThietLap
        {
            get => _iThietLap;
            set => SetProperty(ref _iThietLap, value);
        }

        public string CanCuString
        {
            get => VoucherType.TypeCanCuDict.GetValueOrDefault(IIDMaChucNang, string.Empty);
            set {}
        }

        public string ThietLapString
        {
            get => VoucherType.ThietLapCanCuDict.GetValueOrDefault(IThietLap.GetValueOrDefault(), string.Empty);
            set { }
        }


        private string _soCTCanCu;
        public string SoCTCanCu
        {
            get => _soCTCanCu;
            set => SetProperty(ref _soCTCanCu, value);
        }

        private double? _tuChi;
        public double? TuChi
        {
            get => _tuChi;
            set => SetProperty(ref _tuChi, value);
        }

        private double? _hangNhap;
        public double? HangNhap
        {
            get => _hangNhap;
            set => SetProperty(ref _hangNhap, value);
        }

        private double? _hangMua;
        public double? HangMua
        {
            get => _hangMua;
            set => SetProperty(ref _hangMua, value);
        }

        private double? _phanCap;
        public double? PhanCap
        {
            get => _phanCap;
            set => SetProperty(ref _phanCap, value);
        }

        private double? _muaHangHienVat;
        public double? MuaHangHienVat
        {
            get => _muaHangHienVat;
            set => SetProperty(ref _muaHangHienVat, value);
        }

        private double? _dacThu;

        public double? DacThu
        {
            get => _dacThu;
            set => SetProperty(ref _dacThu, value);
        }

        private bool? _bChinhSua;
        [DisplayName("Chỉnh sửa")]
        [DisplayDetailInfo("Chỉnh sửa")]
        [ColumnTypeAttribute(ColumnType.Checkbox)]
        public bool? BChinhSua
        {
            get => _bChinhSua;
            set => SetProperty(ref _bChinhSua, value);
        }

        private bool _isSaved;
        public bool IsSaved
        {
            get => _isSaved;
            set => SetProperty(ref _isSaved, value);
        }
    }
}
