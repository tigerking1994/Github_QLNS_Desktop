using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlChungTuChiTietKeHoachNq104Query : INotifyPropertyChanged
    {
        public Guid? Id { get; set; }
        public Guid MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
        public string XauNoiMa { get; set; }
        public string Lns { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }
        public string Tng { get; set; }
        public int? NamLamViec { get; set; }
        public string MoTa { get; set; }
        public bool? BHangCha { get; set; }
        public decimal? TongCong { get; set; }
        public decimal? TongNamTruoc { get; set; }

        private decimal? _dieuChinh;
        public decimal? DieuChinh
        {
            get => _dieuChinh;
            set
            {
                SetProperty(ref _dieuChinh, value);
                OnPropertyChanged(nameof(ChenhLech));
            }
        }

        public string MaPhuCap { get; set; }
        public string GhiChu { get; set; }
        public string IdDonVi { get; set; }
        public string Ngach { get; set; }
        public string TenDonVi { get; set; }

        private decimal? _chenhLech;
        public decimal? ChenhLech
        {
            get => _chenhLech;
            set => SetProperty(ref _chenhLech, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual bool SetProperty<T>(ref T member, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(member, value))
            {
                return false;
            }

            member = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
