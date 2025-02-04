using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Model.ConvertGenericData;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class DonViModel : ModelBase
    {
        public override Guid Id { get; set; }

        private Guid? _idParent;
        public Guid? IdParent
        {
            get => _idParent;
            set => SetProperty(ref _idParent, value);
        }

        private string _iIDMaDonVi;
        [ColumnIndexAttribute(0)]
        [DisplayName("Phiên hiệu quân sự")]
        [DisplayDetailInfo("Phiên hiệu quân sự")]
        public string IIDMaDonVi 
        {
            get => _iIDMaDonVi;
            set => SetProperty(ref _iIDMaDonVi, value);
        }

        private string _tenDonVi;
        [DisplayName("Tên đơn vị")]
        [DisplayDetailInfo("Tên đơn vị")]
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }
        private Guid? _IIdDonViQuanLyId;
        public Guid? IIdDonViQuanLyId
        {
            get => _IIdDonViQuanLyId;
            set => SetProperty(ref _IIdDonViQuanLyId, value);
        }

        private string _kyHieu;
        public string KyHieu
        {
            get => _kyHieu;
            set => SetProperty(ref _kyHieu, value);
        }

        private string _moTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private string _maSoDVQHNS;
        [DisplayName("Mã số ĐVQHNS")]
        [DisplayDetailInfo("Mã số ĐVQHNS")]
        public string MaSoDVQHNS
        {
            get => _maSoDVQHNS;
            set => SetProperty(ref _maSoDVQHNS, value);
        }

        private string _maSoKBNN;
        [DisplayName("Mã số KBNN")]
        [DisplayDetailInfo("Mã số KBNN")]
        public string MaSoKBNN
        {
            get => _maSoKBNN;
            set => SetProperty(ref _maSoKBNN, value);
        }

        private string _loai;
        [DisplayName("Loại hình")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadAllLoaiDonVi")]
        public string Loai
        {
            get => _loai;
            set => SetProperty(ref _loai, value);
        }

        private string _khoi;
        [DisplayName("Khối")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadAllKhoiDonVi")]
        public string Khoi 
        {
            get => _khoi;
            set => SetProperty(ref _khoi, value);
        }

        private int? _namLamViec;
        [DisplayDetailInfo("Năm làm việc")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        public int? NamLamViec
        {
            get => _namLamViec;
            set => SetProperty(ref _namLamViec, value);
        }

        private string _parentName;
        [DisplayDetailInfo("Tên đơn vị cha")]
        public string ParentName
        {
            get => _parentName;
            set => SetProperty(ref _parentName, value);
        }

        private int? _iTrangThai;
        [DisplayName("Trạng thái")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadAllTrangThaiDonVi")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        public int? ITrangThai
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }

        public string TenDonViIdDonVi
        {
            get
            {
                if (IIDMaDonVi != null && TenDonVi != null)
                        return string.Concat(IIDMaDonVi, " - ", TenDonVi);
                return null;
            }
        }

        private DateTime? _dNgayTao;
        [DisplayDetailInfo("Ngày tạo")]
        public DateTime? DNgayTao
        {
            get => _dNgayTao;
            set => SetProperty(ref _dNgayTao, value);
        }
        [DisplayDetailInfo("Người tạo")]
        public string SNguoiTao { get; set; }
        [DisplayDetailInfo("Ngày cập nhật")]
        public DateTime? DNgaySua { get; set; }
        [DisplayDetailInfo("Người cập nhật")]
        public string SNguoiSua { get; set; }
        [DisplayDetailInfo("Tag")]
        public string Tag { get; set; }
        [DisplayDetailInfo("Log")]
        public string Log { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private bool _bCoNSNganh;
        [ColumnTypeAttribute(ColumnType.Checkbox)]
        [DisplayName("Có ngân sách ngành")]
        [DisplayDetailInfo("Có ngân sách ngành")]
        public bool BCoNSNganh
        {
            get => _bCoNSNganh;
            set => SetProperty(ref _bCoNSNganh, value);
        }

        [DisplayDetailInfo("Trạng thái")]
        public string TrangThaiDisplay
        {
            get => ITrangThai switch
            {
                0 => "Không sử dụng",
                1 => "Đang sử dụng",
                2 => "Ngành nghiệp vụ",
                _ => ""
            };
        }

        [DisplayDetailInfo("Loại")]
        public string LoaiDisplay
        {
            get => Loai switch
            {
                "0" => "",
                "1" => "Toàn quân",
                "2" => "Nội bộ",
                _ => ""
            };
        }

        public ICollection<DanhMucNganhModel> DanhMucChuyenNganh { get; set; }

        private string _tenDanhMuc;
        [DisplayName("Chuyên ngành (F6)")]
        [DisplayDetailInfo("Chuyên ngành")]
        [TypeOfDialogAttribute(typeof(DanhMucNganhModel), typeof(DanhMuc), typeof(DanhMucNganhService), typeof(DanhMucNganhService))]
        [MapperMethodAttribute("MapDanhMucChuyenNganhToDanhMucChuyenNganhDonvi")]
        [InitSelectedItemsMethodAttribute("SetSelecteDanhMucChuyenNganhOfNsDonVi")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(true)]
        public string TenDanhMuc
        {
            get => _tenDanhMuc;
            set => SetProperty(ref _tenDanhMuc, value);
        }

        public ICollection<NsMuclucNgansachModel> LNS { get; set; }

        private string _tenLNS;
        [DisplayName("LNS (F6)")]
        [DisplayDetailInfo("LNS")]
        [TypeOfDialogAttribute(typeof(NsMuclucNgansachModel), typeof(NsMucLucNganSach), typeof(MucLucNganSachService), typeof(IMucLucNganSachService))]
        [MapperMethodAttribute("MapLnsToLnsOfDonvi")]
        [InitSelectedItemsMethodAttribute("SetSelecteLnsOfNsDonVi")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(true)]
        public string TenLNS 
        {
            get => _tenLNS;
            set => SetProperty(ref _tenLNS, value);
        }

        public override string DetailInfoModalTitle => "Chi tiết đơn vị " + TenDonVi;

        public override bool IsEditable => !IsDeleted;

        public string DisplayMemberPath => IIDMaDonVi + " - " + TenDonVi;

        private int? _iCapDonVi;
        [DisplayName("Cấp đơn vị")]
        [DisplayDetailInfo("Cấp đơn vị")]
        public int? iCapDonVi 
        {
            get => _iCapDonVi;
            set => SetProperty(ref _iCapDonVi, value);
        }

        private bool? _isPhongBan;
        [DisplayName("Phòng ban")]
        [DisplayDetailInfo("Phòng ban")]
        [ColumnTypeAttribute(ColumnType.Checkbox)]
        public bool? IsPhongBan
        {
            get => _isPhongBan;
            set => SetProperty(ref _isPhongBan, value);
        }

        public string TenDonViDisplay => string.Concat(IIDMaDonVi, " - ", TenDonVi);
    }
}