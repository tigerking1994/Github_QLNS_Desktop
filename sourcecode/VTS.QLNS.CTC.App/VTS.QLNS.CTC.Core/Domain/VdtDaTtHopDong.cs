using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaTtHopDong : EntityBase
    {
        //public Guid Id { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIdHopDongGocId { get; set; }
        public bool? BIsGoc { get; set; }
        public bool? BActive { get; set; }
        public string SSoHopDong { get; set; }
        public int IThoiGianThucHien { get; set; }
        public DateTime DNgayHopDong { get; set; }
        public DateTime? DKhoiCongDuKien { get; set; }
        public DateTime? DKetThucDuKien { get; set; }
        public Guid? IIdNhaThauThucHienId { get; set; }
        public string SSoTaiKhoan { get; set; }
        public string SNganHang { get; set; }
        public int ITinhTrangHopDong { get; set; }
        public Guid? IIdLoaiHopDongId { get; set; }
        public string SHinhThucHopDong { get; set; }
        public double? FTienHopDong { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public string NoiDungHopDong { get; set; }
        public bool? BKhoa { get; set; }
        public int? ILandieuchinh { get; set; }
        public string STenHopDong { get; set; }
        public DateTime? DBatDauBaoLanhHopDong { get; set; }
        public DateTime? DKetThucBaoLanhHopDong { get; set; }
        [NotMapped]
        public ObservableCollection<VdtDaHopDongGoiThauNhaThau> ListGoiThau { get; set; }
    }
}
