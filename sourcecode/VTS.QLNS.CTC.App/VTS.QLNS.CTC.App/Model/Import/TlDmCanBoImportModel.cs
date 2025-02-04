using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(4, "Danh mục cán bộ", 4, 0)]
    public class TlDmCanBoImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
        }

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }
        [ColumnAttribute("Tên cán bộ", 0)]
        public string MaCanBo { get; set; }
        [ColumnAttribute("Tên cán bộ", 1)]
        public string TenCanBo { get; set; }
        [ColumnAttribute("Tên cán bộ", 2)]
        public string DiaChi { get; set; }
        [ColumnAttribute("Tên cán bộ", 3)]
        public string MaCv { get; set; }
        [ColumnAttribute("Tên cán bộ", 4)]
        public string MaBl { get; set; }
        [ColumnAttribute("Tên cán bộ", 5)]
        public string MaCb { get; set; }
        [ColumnAttribute("Tên cán bộ", 6)]
        public string MaPban { get; set; }
        [ColumnAttribute("Tên cán bộ", 7)]
        public string Gtgc { get; set; }
        [ColumnAttribute("Tên cán bộ", 8)]
        public string DienThoai { get; set; }
        [ColumnAttribute("Tên cán bộ", 9)]
        public string MaSoVat { get; set; }
        [ColumnAttribute("Tên cán bộ", 10)]
        public string TenDonVi { get; set; }
        [ColumnAttribute("Tên cán bộ", 11)]
        public string SoCmt { get; set; }
        [ColumnAttribute("Tên cán bộ", 12)]
        public string NoiCapCmt { get; set; }
        [ColumnAttribute("Tên cán bộ", 13)]
        public string NgayCapCmt { get; set; }
        [ColumnAttribute("Tên cán bộ", 14)]
        public string SoTaiKhoan { get; set; }
        [ColumnAttribute("Tên cán bộ", 15)]
        public string TenKhoBac { get; set; }
        [ColumnAttribute("Tên cán bộ", 16)]
        public string MaSoDvSdns { get; set; }
        [ColumnAttribute("Tên cán bộ", 17)]
        public string MaDiaBanHc { get; set; }
        [ColumnAttribute("Tên cán bộ", 18)]
        public string MaTkLq { get; set; }
        [ColumnAttribute("Tên cán bộ", 19)]
        public string Parent { get; set; }
        [ColumnAttribute("Tên cán bộ", 20)]
        public string MaKhoBac { get; set; }
        [ColumnAttribute("Tên cán bộ", 21)]
        public string Splits { get; set; }
        [ColumnAttribute("Tên cán bộ", 22)]
        public string Readonly { get; set; }
        [ColumnAttribute("Tên cán bộ", 23)]
        public string KhongLuong { get; set; }
        [ColumnAttribute("Tên cán bộ", 24)]
        public string MaHieuCanBo { get; set; }
        [ColumnAttribute("Tên cán bộ", 25)]
        public string Thang { get; set; }
        [ColumnAttribute("Tên cán bộ", 26)]
        public string Nam { get; set; }
        [ColumnAttribute("Tên cán bộ", 27)]
        public string NgayNn { get; set; }
        [ColumnAttribute("Tên cán bộ", 28)]
        public string NgayXn { get; set; }
        [ColumnAttribute("Tên cán bộ", 29)]
        public string NgayTn { get; set; }
        [ColumnAttribute("Tên cán bộ", 30)]
        public string NamTn { get; set; }
        [ColumnAttribute("Tên cán bộ", 31)]
        public string ThangTnn { get; set; }
        [ColumnAttribute("Tên cán bộ", 32)]
        public string NamVk { get; set; }
        [ColumnAttribute("Tên cán bộ", 33)]
        public string IsNam { get; set; }
        [ColumnAttribute("Tên cán bộ", 34)]
        public string MaTangGiam { get; set; }
        [ColumnAttribute("Tên cán bộ", 35)]
        public string SoSoLuong { get; set; }
        [ColumnAttribute("Tên cán bộ", 36)]
        public string NgayNhanCb { get; set; }
        [ColumnAttribute("Tên cán bộ", 37)]
        public string ThoiHanTangCb { get; set; }
        [ColumnAttribute("Tên cán bộ", 38)]
        public string CbKeHoach { get; set; }
        [ColumnAttribute("Tên cán bộ", 39)]
        public string Cccd { get; set; }
        [ColumnAttribute("Tên cán bộ", 40)]
        public string NoiCongTac { get; set; }
        [ColumnAttribute("Tên cán bộ", 41)]
        public string NgaySinh { get; set; }
        [ColumnAttribute("Tên cán bộ", 42)]
        public string Tm { get; set; }
        [ColumnAttribute("Tên cán bộ", 43)]
        public string IsLock { get; set; }
        [ColumnAttribute("Tên cán bộ", 44)]
        public string IsDelete { get; set; }
        [ColumnAttribute("Tên cán bộ", 45)]
        public string DateCreated { get; set; }
        [ColumnAttribute("Tên cán bộ", 46)]
        public string DateModified { get; set; }
        [ColumnAttribute("Tên cán bộ", 47)]
        public string UserCreator { get; set; }
        [ColumnAttribute("Tên cán bộ", 48)]
        public string UserModifier { get; set; }
        [ColumnAttribute("Tên cán bộ", 49)]
        public string BHTN { get; set; }
        [ColumnAttribute("Tên cán bộ", 50)]
        public string PCCV { get; set; }
        [ColumnAttribute("Tên cán bộ", 51)]
        public string HsLuongTran { get; set; }
        [ColumnAttribute("Tên cán bộ", 52)]
        public string HsLuongKeHoach { get; set; }
        [ColumnAttribute("Tên cán bộ", 53)]
        public string HeSoLuong { get; set; }
        [ColumnAttribute("Tên cán bộ", 54)]
        public string IdLuongTran { get; set; }
        [ColumnAttribute("Tên cán bộ", 55)]
        public string MaCbCu { get; set; }
        [ColumnAttribute("Tên cán bộ", 56)]
        public string Nhom { get; set; }
    }
}
