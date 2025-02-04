using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class DisplayHTChucNangModel : ModelBase
    {
        public Guid? IIDChucNangCha { get; set; }

        private string _iIDMaChucNang;
        [DisplayName("Mã chức năng")]
        [DisplayDetailInfo("Mã chức năng")]
        public string IIDMaChucNang 
        {
            get => _iIDMaChucNang;
            set => SetProperty(ref _iIDMaChucNang, value);
        }

        private string _sTenChucNang;
        [DisplayName("Tên chức năng")]
        [DisplayDetailInfo("Tên chức năng")]
        public string STenChucNang
        {
            get => _sTenChucNang;
            set => SetProperty(ref _sTenChucNang, value);
        }

        public string OldName { get; set; }
        public string OldFuncCode { get; set; }

        private string _sSTT;
        [DisplayName("Thứ tự")]
        public string SSTT 
        { 
            get => _sSTT;
            set => SetProperty(ref _sSTT, value);
        }

        public bool ITrangThai { get; set; }
        [JsonIgnore]
        public ICollection<HTQuyenModel> HTQuyens { get; set; }

        private bool _bHangCha;
        public bool BHangCha
        {
            get => _bHangCha;
            set
            {
                SetProperty(ref _bHangCha, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        public bool IsHangCha => BHangCha;

        public DisplayHTChucNangModel()
        {
            HTQuyens = new HashSet<HTQuyenModel>();
        }
    }
}
