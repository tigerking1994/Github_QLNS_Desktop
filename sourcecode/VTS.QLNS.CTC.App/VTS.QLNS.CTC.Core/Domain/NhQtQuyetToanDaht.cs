using System;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhQtQuyetToanDaht : EntityBase
    {
        public Guid? IIdParentId { get; set; }
        public Guid? IIdGocId { get; set; }
        public string SSoDeNghi { get; set; }
        public DateTime? DNgayDeNghi { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public Guid? IIdDonViId { get; set; }
        public string IIdMaDonVi { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public Guid? IIdTiGiaPheDuyetId { get; set; }
        public double? FDeNghiQuyetToanUsd { get; set; }
        public double? FDeNghiQuyetToanVnd { get; set; }
        public double? FDeNghiQuyetToanEur { get; set; }
        public double? FDeNghiQuyetToanNgoaiTeKhac { get; set; }
        public double? FCpthietHaiUsd { get; set; }
        public double? FCpthietHaiVnd { get; set; }
        public double? FCpthietHaiEur { get; set; }
        public double? FCpthietHaiNgoaiTeKhac { get; set; }
        public double? FCpkhongTaoTaiSanUsd { get; set; }
        public double? FCpkhongTaoTaiSanVnd { get; set; }
        public double? FCpkhongTaoTaiSanEur { get; set; }
        public double? FCpkhongTaoTaiSanNgoaiTeKhac { get; set; }
        public double? FTaiSanDaiHanUsd { get; set; }
        public double? FTaiSanDaiHanVnd { get; set; }
        public double? FTaiSanDaiHanEur { get; set; }
        public double? FTaiSanDaiHanNgoaiTeKhac { get; set; }
        public double? FTaiSanNganHanUsd { get; set; }
        public double? FTaiSanNganHanVnd { get; set; }
        public double? FTaiSanNganHanEur { get; set; }
        public double? FTaiSanNganHanNgoaiTeKhac { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool BIsActive { get; set; }
        public bool BIsGoc { get; set; }
        public bool BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public bool BIsXoa { get; set; }
        public Guid? ParentId { get; set; }
        public bool? BTongHop { get; set; }
    }
}
