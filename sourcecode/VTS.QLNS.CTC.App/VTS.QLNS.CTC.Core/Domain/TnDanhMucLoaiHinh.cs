using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TnDanhMucLoaiHinh : EntityBase
    {
        public bool BLaHangCha { get; set; }
        public int? INamLamViec { get; set; }
        public string IdPhongBan { get; set; }
        public string IStt { get; set; }
        public int? ITrangThai { get; set; }
        public string Lns { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public int? ISoLanSua { get; set; }
        public string SIpsua { get; set; }
        public string MoTa { get; set; }
        // public Guid Id { get; set; }
        public string IIdMaNhomNguoiDungPublic { get; set; }
        public string IIdMaNhomNguoiDungDuocGiao { get; set; }
        public string SIdMaNguoiDungDuocGiao { get; set; }
        public Guid? IdMaLoaiHinh { get; set; }
        public Guid? IdMaLoaiHinhCha { get; set; }
    }
}
