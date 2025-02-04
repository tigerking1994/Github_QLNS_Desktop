using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ", 7, 0)]
    public class PhanBoVon_MoMoiImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private string _stt;
        [ColumnAttribute("STT", 0)]
        public string Stt
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _sTenDuAn;
        [ColumnAttribute("Tên dự án", 1)]
        public string sTenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _sMaDuAn;
        [ColumnAttribute("Mã dự án", 2, isRequired:true)]
        public string sMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _sLns;
        [ColumnAttribute("Loại ngân sách", 3)]
        public string sLns
        {
            get => _sLns;
            set => SetProperty(ref _sLns, value);
        }

        private string _sL;
        [ColumnAttribute("Loại", 4)]
        public string sL
        {
            get => _sL;
            set => SetProperty(ref _sL, value);
        }

        private string _sK;
        [ColumnAttribute("Khoản", 5)]
        public string sK
        {
            get => _sK;
            set => SetProperty(ref _sK, value);
        }

        private string _sM;
        [ColumnAttribute("Mục", 6)]
        public string sM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }

        private string _sTm;
        [ColumnAttribute("Tiểu mục", 7)]
        public string sTm
        {
            get => _sTm;
            set => SetProperty(ref _sTm, value);
        }

        private string _sTtm;
        [ColumnAttribute("Tiết mục", 8)]
        public string sTtm
        {
            get => _sTtm;
            set => SetProperty(ref _sTtm, value);
        }

        private string _sNg;
        [ColumnAttribute("Ngành", 9)]
        public string sNg
        {
            get => _sNg;
            set => SetProperty(ref _sNg, value);
        }

        private string _fCapPhatTaiKhoBac;
        [ColumnAttribute("Rút dự toán tại KBNN", 10)]
        public string FCapPhatTaiKhoBac
        {
            get => _fCapPhatTaiKhoBac;
            set => SetProperty(ref _fCapPhatTaiKhoBac, value);
        }

        private string _fCapPhatBangLenhChi;
        [ColumnAttribute("Cấp bằng lệnh chi tiền", 11)]
        public string FCapPhatBangLenhChi
        {
            get => _fCapPhatBangLenhChi;
            set => SetProperty(ref _fCapPhatBangLenhChi, value);
        }


        private string _fGiaTriThuHoiNamTruocKhoBac;
        [ColumnAttribute("Thu hồi năm trước kho bạc", 12)]
        public string FGiaTriThuHoiNamTruocKhoBac
        {
            get => _fGiaTriThuHoiNamTruocKhoBac;
            set => SetProperty(ref _fGiaTriThuHoiNamTruocKhoBac, value);
        }

        private string _fGiaTriThuHoiNamTruocLenhChi;
        [ColumnAttribute("Thu hồi năm trước lệnh chi", 13)]
        public string FGiaTriThuHoiNamTruocLenhChi
        {
            get => _fGiaTriThuHoiNamTruocLenhChi;
            set => SetProperty(ref _fGiaTriThuHoiNamTruocLenhChi, value);
        }

        private string _sGhiChu;
        [ColumnAttribute("Thu hồi năm trước lệnh chi", 14)]
        public string sGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private string _idChungTu;
        [ColumnAttribute("Id Chứng từ", 15)]
        public string IdChungTu
        {
            get => _idChungTu;
            set => SetProperty(ref _idChungTu, value);
        }

        private string _idChungTuParent;
        [ColumnAttribute("Id Chứng từ parent", 16)]
        public string IdChungTuParent
        {
            get => _idChungTuParent;
            set => SetProperty(ref _idChungTuParent, value);
        }

        private string _isActive;
        [ColumnAttribute("Chứng từ Active", 17)]
        public string IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        private string _isGoc;
        [ColumnAttribute("Chứng từ Gốc", 18)]
        public string IsGoc
        {
            get => _isGoc;
            set => SetProperty(ref _isGoc, value);
        }

        private string _iIdLoaiCongTrinh;
        [ColumnAttribute("Loại công trình", 19)]
        public string IIdLoaiCongTrinh
        {
            get => _iIdLoaiCongTrinh;
            set => SetProperty(ref _iIdLoaiCongTrinh, value);
        }

        private string _iLoaiDuAn;
        [ColumnAttribute("Loại dự án", 20)]
        public string ILoaiDuAn
        {
            get => _iLoaiDuAn;
            set => SetProperty(ref _iLoaiDuAn, value);
        }
    }
}
