using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Import
{
    public class PhanBoVonImportModel: BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
        }

        public Guid IdChungTu { get; set; }

        private string _stt;
        public string Stt
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private Guid _iID_DuAnID;
        public Guid iID_DuAnID
        {
            get => _iID_DuAnID;
            set => SetProperty(ref _iID_DuAnID, value);
        }

        private Guid _iID_LoaiNguonVonID;
        public Guid iID_LoaiNguonVonID
        {
            get => _iID_LoaiNguonVonID;
            set => SetProperty(ref _iID_LoaiNguonVonID, value);
        }

        private string _sTenDuAn;
        public string sTenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _sMaDuAn;
        public string sMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _sLns;
        public string sLns
        {
            get => _sLns;
            set => SetProperty(ref _sLns, value);
        }

        private string _sL;
        public string sL
        {
            get => _sL;
            set => SetProperty(ref _sL, value);
        }

        private string _sK;
        public string sK
        {
            get => _sK;
            set => SetProperty(ref _sK, value);
        }

        private string _sM;
        public string sM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }

        private string _sTm;
        public string sTm
        {
            get => _sTm;
            set => SetProperty(ref _sTm, value);
        }

        private string _sTtm;
        public string sTtm
        {
            get => _sTtm;
            set => SetProperty(ref _sTtm, value);
        }

        private string _sNg;
        public string sNg
        {
            get => _sNg;
            set => SetProperty(ref _sNg, value);
        }

        private double _fCapPhatTaiKhoBac;
        public double FCapPhatTaiKhoBac
        {
            get => _fCapPhatTaiKhoBac;
            set => SetProperty(ref _fCapPhatTaiKhoBac, value);
        }

        private double _fCapPhatBangLenhChi;
        public double FCapPhatBangLenhChi
        {
            get => _fCapPhatBangLenhChi;
            set => SetProperty(ref _fCapPhatBangLenhChi, value);
        }

        private double _fGiaTriThuHoiNamTruocKhoBac;
        public double FGiaTriThuHoiNamTruocKhoBac
        {
            get => _fGiaTriThuHoiNamTruocKhoBac;
            set => SetProperty(ref _fGiaTriThuHoiNamTruocKhoBac, value);
        }

        private double _fGiaTriThuHoiNamTruocLenhChi;
        public double FGiaTriThuHoiNamTruocLenhChi
        {
            get => _fGiaTriThuHoiNamTruocLenhChi;
            set => SetProperty(ref _fGiaTriThuHoiNamTruocLenhChi, value);
        }

        public string sXauNoiMa
        {
            get
            {
                return sLns + "-" + sL + "-" + sK + "-" + sM + "-" + sTm + "-" + sTtm + "-" + sNg;
            }
        }

        private string _iIdLoaiCongTrinh;
        public string IIdLoaiCongTrinh
        {
            get => _iIdLoaiCongTrinh;
            set => SetProperty(ref _iIdLoaiCongTrinh, value);
        }

        private string _iLoaiDuAn;
        public string ILoaiDuAn
        {
            get => _iLoaiDuAn;
            set => SetProperty(ref _iLoaiDuAn, value);
        }
    }
}
