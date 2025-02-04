using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhCauHinhBaoCaoModel : ModelBase
    {
        public string _sMaBaoCao;
        public string SMaBaoCao
        {
            get => _sMaBaoCao;
            set
            {
                SetProperty(ref _sMaBaoCao, value);
            }
        }

        public string _iIdMaDonVi;
        public string IIdMaDonVi
        {
            get => _iIdMaDonVi;
            set
            {
                SetProperty(ref _iIdMaDonVi, value);
            }
        }

        public string _sTenDonVi;
        public string STenDonVi
        {
            get => _sTenDonVi;
            set
            {
                SetProperty(ref _sTenDonVi, value);
            }
        }

        public int? _iNamLamViec;
        public int? INamLamViec
        {
            get => _iNamLamViec;
            set
            {
                SetProperty(ref _iNamLamViec, value);
            }
        }

        public int? _iLoaiBaoCao;
        public int? ILoaiBaoCao
        {
            get => _iLoaiBaoCao;
            set
            {
                SetProperty(ref _iLoaiBaoCao, value);
            }
        }

        public string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set
            {
                SetProperty(ref _sGhiChu, value);
            }
        }

        public string _sCanCu1;
        public string SCanCu1
        {
            get => _sCanCu1;
            set
            {
                SetProperty(ref _sCanCu1, value);
            }
        }

        public string _sCanCu2;
        public string SCanCu2
        {
            get => _sCanCu2;
            set
            {
                SetProperty(ref _sCanCu2, value);
            }
        }

        public string _sTenBaoCao;
        public string STenBaoCao
        {
            get => _sTenBaoCao;
            set
            {
                SetProperty(ref _sTenBaoCao, value);
            }
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public int? ILoaiCauHinh
        {
            get
            {
                if (!string.IsNullOrEmpty(SGhiChu) && (!string.IsNullOrEmpty(SCanCu1) || !string.IsNullOrEmpty(SCanCu2))) return (int)LoaiCauHinh.TatCa;
                if (!string.IsNullOrEmpty(SGhiChu)) return (int)LoaiCauHinh.GhiChu;
                if (!string.IsNullOrEmpty(SCanCu1) || !string.IsNullOrEmpty(SCanCu2)) return (int)LoaiCauHinh.CanCu;
                return (int)LoaiCauHinh.MacDinh;
            }
        }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }

        [NotMapped]
        public List<BhCauHinhBaoCaoModel> ItemsChild = new List<BhCauHinhBaoCaoModel>();
    }
}
