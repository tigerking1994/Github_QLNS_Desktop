using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlPhuCapDieuChinhNq104Model : ModelBase
    {
        private Guid? _idPhuCap;
        public Guid? IdPhuCap
        {
            get => _idPhuCap;
            set => SetProperty(ref _idPhuCap, value);
        }

        private decimal? _giaTriMoi;
        public decimal? GiaTriMoi
        {
            get => _giaTriMoi;
            set => SetProperty(ref _giaTriMoi, value);
        }

        private DateTime? _apDungTu;
        public DateTime? ApDungTu
        {
            get => _apDungTu;
            set => SetProperty(ref _apDungTu, value);
        }

        private string _tenPhuCap;
        public string TenPhuCap
        {
            get => _tenPhuCap;
            set => SetProperty(ref _tenPhuCap, value);
        }

        private string _maPhuCap;
        public string MaPhuCap
        {
            get => _maPhuCap;
            set => SetProperty(ref _maPhuCap, value);
        }

        private decimal? _giaTriCu;
        public decimal? GiaTriCu
        {
            get => _giaTriCu;
            set => SetProperty(ref _giaTriCu, value);
        }

        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }

        public bool BHangCha => IsHangCha;
    }
}
