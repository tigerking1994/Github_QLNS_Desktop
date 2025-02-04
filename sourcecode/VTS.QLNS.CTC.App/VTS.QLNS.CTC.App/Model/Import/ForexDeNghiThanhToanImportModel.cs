using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using System;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Sheet1", 17, 0)]
    public class ForexDeNghiThanhToanImportModel : BindableBase
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

        private string _iStt;
        //[ColumnAttribute("STT", 0, ValidateType.IsNumber)]
        public string iStt
        {
            get => _iStt;
            set => SetProperty(ref _iStt, value);
        }

        private string sXauNoiMa;
        [ColumnAttribute("Mục/TM/Tiết mục", 0)]
        public string SXauNoiMa
        {
            get => sXauNoiMa;
            set => SetProperty(ref sXauNoiMa, value);
        }
        private string sNoiDungChi;
        [ColumnAttribute("Nội dung chi", 1)]
        
        public string SNoiDungChi
        {
            get => sNoiDungChi;
            set => SetProperty(ref sNoiDungChi, value);
        }
        private string fGiaTriDuocDuyetUSD;
        [ColumnAttribute("USD", 2)]
        
        public string FGiaTriDuocDuyetUSD
        {
            get => fGiaTriDuocDuyetUSD;
            set => SetProperty(ref fGiaTriDuocDuyetUSD, value);
        }
        private string fGiaTriDuocDuyetVND;
        [ColumnAttribute("VND", 3)]
        
        public string FGiaTriDuocDuyetVND
        {
            get => fGiaTriDuocDuyetVND;
            set => SetProperty(ref fGiaTriDuocDuyetVND, value);
        }
        private string fLuyKeUSD;
        [ColumnAttribute("USD", 4)]
        
        public string FLuyKeUSD
        {
            get => fLuyKeUSD;
            set => SetProperty(ref fLuyKeUSD, value);
        }
        private string fLuyKeVND;
        [ColumnAttribute("VND", 5)]
        
        public string FLuyKeVND
        {
            get => fLuyKeVND;
            set => SetProperty(ref fLuyKeVND, value);
        }
        private string fGiaTriDeNghiUSD;
        [ColumnAttribute("USD", 6)]
        
        public string FGiaTriDeNghiUSD
        {
            get => fGiaTriDeNghiUSD;
            set => SetProperty(ref fGiaTriDeNghiUSD, value);
        }
        private string fGiaTriDeNghiVND;
        [ColumnAttribute("VND", 7)]
        
        public string FGiaTriDeNghiVND
        {
            get => fGiaTriDeNghiVND;
            set => SetProperty(ref fGiaTriDeNghiVND, value);
        }


        private Guid? iIDMLNS;
        public Guid? IIDMLNS
        {
            get => iIDMLNS;
            set => SetProperty(ref iIDMLNS, value);
        }

    }
}
