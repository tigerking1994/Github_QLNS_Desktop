using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class DuAnDetailModel : DetailModelBase
    {
        public int iLoai { get; set; }
        public Guid? iIdLoaiChiPhi { get; set; }
        public string sNoiChuoi { get; set; }
        public Guid? iIdHangMuc { get; set; }
        public Guid? iIdChiPhi { get; set; }

        private int? _iIdNguonVon;
        public int? iIdNguonVon 
        { 
            get => _iIdNguonVon; 
            set => SetProperty(ref _iIdNguonVon, value); 
        }

        private string _sNoiDung;
        public string sNoiDung 
        { 
            get => _sNoiDung; 
            set => SetProperty(ref _sNoiDung, value); 
        }

        public Guid? iIdParentId { get; set;}
        
        private double? _fTienPheDuyet;
        public double? fTienPheDuyet 
        { 
            get => _fTienPheDuyet; 
            set => SetProperty(ref _fTienPheDuyet, value); 
        }

        private double? _fTienPheDuyetOld;
        public double? fTienPheDuyetOld
        {
            get => _fTienPheDuyetOld;
            set => SetProperty(ref _fTienPheDuyetOld, value);
        }

        private bool _isMaster;
        public bool IsMaster
        {
            get => _isMaster;
            set => SetProperty(ref _isMaster, value);
        }

        private Guid? _idLoaiCongTrinh;
        public Guid? IdLoaiCongTrinh
        {
            get => _idLoaiCongTrinh;
            set => SetProperty(ref _idLoaiCongTrinh, value);
        }

        private bool _isLog;
        public bool IsHangCha
        {
            get => _isLog;
            set => SetProperty(ref _isLog, value);
        }

        private bool _isEditHangMuc;
        public bool IsEditHangMuc
        {
            get => _isEditHangMuc;
            set => SetProperty(ref _isEditHangMuc, value);
        }

        private bool _isHaveChild;
        public bool IsHaveChild
        {
            get => _isHaveChild;
            set => SetProperty(ref _isHaveChild, value);
        }

        public int ActionState { get; set; }

        private string _sMaHangMuc;
        public string sMaHangMuc
        {
            get => _sMaHangMuc;
            set => SetProperty(ref _sMaHangMuc, value);
        }
    }
}
