using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlPhuCapMlnsModel : ModelBase
    {
        public string MaPhuCap { get; set; }

        private string _tenPhuCap;
        public string TenPhuCap
        {
            get => _tenPhuCap;
            set => SetProperty(ref _tenPhuCap, value);
        }

        public string MaCachTl { get; set; }

        private string _xauNoiMa;
        [DisplayName("Xâu nối mã (F6)")]
        [DisplayDetailInfo("Xâu nối mã")]
        [TypeOfDialogAttribute(typeof(TlPhuCapMlnsModel), typeof(TlPhuCapMln), typeof(TlPhuCapMlnsService), typeof(ITlPhuCapMlnsService))]
        [MapperMethodAttribute("MapMLNSToMLNSOfTlPhuCapMlns")]
        [InitSelectedItemsMethodAttribute("SetSelectedMLNSOfPhuCapMlns")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(true)]
        public string XauNoiMa
        {
            get => _xauNoiMa;
            set => SetProperty(ref _xauNoiMa, value);
        }
        public string Lns { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }

        private string _moTa;
        public string MoTa 
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        public string MaNguonNganSach { get; set; }

        private string _nguonNganSach;
        public string NguonNganSach 
        {
            get => _nguonNganSach;
            set => SetProperty(ref _nguonNganSach, value);
        }

        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string ITrangThai { get; set; }

        private Guid? _idPhuCap;
        public Guid? IdPhuCap
        {
            get => _idPhuCap;
            set => SetProperty(ref _idPhuCap, value);
        }

        private Guid? _idCachTinhLuong;
        public Guid? IdCachTinhLuong
        {
            get => _idCachTinhLuong;
            set => SetProperty(ref _idCachTinhLuong, value);
        }

        private Guid? _idNguonNganSach;
        public Guid? IdNguonNganSach
        {
            get => _idNguonNganSach;
            set => SetProperty(ref _idNguonNganSach, value);
        }

        private Guid? _idMlns;
        public Guid? IdMlns
        {
            get => _idMlns;
            set => SetProperty(ref _idMlns, value);
        }

        private string _maCb;
        public string MaCb
        {
            get => _maCb;
            set => SetProperty(ref _maCb, value);
        }

        private string _chiTietToi;
        public string ChiTietToi
        {
            get => _chiTietToi;
            set => SetProperty(ref _chiTietToi, value);
        }

        private int? _nam;
        public int? Nam
        {
            get => _nam;
            set => SetProperty(ref _nam, value);
        }

        public bool? BHangCha => IsHangCha;
        public bool? IsRemainRow { get; set; }

        private string _sTNG;
        public string STNG
        {
            get => _sTNG;
            set => SetProperty(ref _sTNG, value);
        }

        private string _sTNG1;
        public string STNG1
        {
            get => _sTNG1;
            set => SetProperty(ref _sTNG1, value);
        }

        private string _sTNG2;
        public string STNG2
        {
            get => _sTNG2;
            set => SetProperty(ref _sTNG2, value);
        }

        private string _sTNG3;
        public string STNG3
        {
            get => _sTNG3;
            set => SetProperty(ref _sTNG3, value);
        }




    }
}
