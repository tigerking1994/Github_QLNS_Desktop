using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TnDanhMucLoaiHinhModel : ModelBase
    {
        public bool BLaHangCha { get; set; }
        public int? INamLamViec { get; set; }
        public string IdPhongBan { get; set; }
        public string IStt { get; set; }
        public int? ITrangThai { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public int? ISoLanSua { get; set; }
        public string SIpsua { get; set; }
        public Guid Id { get; set; }
        public string IIdMaNhomNguoiDungPublic { get; set; }
        public string IIdMaNhomNguoiDungDuocGiao { get; set; }
        public string SIdMaNguoiDungDuocGiao { get; set; }
        public Guid? IdMaLoaiHinh { get; set; }
        public Guid? IdMaLoaiHinhCha { get; set; }

        private string _lns;
        public string Lns
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _moTa;
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public string LNSDisplay => String.Format("{0} - {1}", Lns, MoTa);

        public TnDanhMucLoaiHinhModel() { }
        public TnDanhMucLoaiHinhModel(string lns, string moTa)
        {
            _lns = lns;
            _moTa = moTa;
        }
    }
}
