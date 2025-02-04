using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Danh sách khởi tạo", 2, 0)]
    public class NhKtKhoiTaoCapPhatImportModel : BindableBase
    {
        [ColumnAttribute("Mã dự án*", 1)]
        public string SMaDuAn { get; set; }
        [ColumnAttribute("Mã hợp đồng*", 2)]
        public string SMaHopDong { get; set; }

        private string _fQTKinhPhiDuyetCacNamTruocUSD;
        [ColumnAttribute("USD", 3)]
        public string FQTKinhPhiDuyetCacNamTruocUSD
        {
            get => _fQTKinhPhiDuyetCacNamTruocUSD;
            set => SetProperty(ref _fQTKinhPhiDuyetCacNamTruocUSD, value);
        }
        private string _fQTKinhPhiDuyetCacNamTruocVND;
        [ColumnAttribute("VND", 4)]
        public string FQTKinhPhiDuyetCacNamTruocVND
        {
            get => _fQTKinhPhiDuyetCacNamTruocVND;
            set => SetProperty(ref _fQTKinhPhiDuyetCacNamTruocVND, value);
        }
        private string _fDeNghiQTNamNayUSD;
        [ColumnAttribute("USD", 5)]
        public string FDeNghiQTNamNayUSD
        {
            get => _fDeNghiQTNamNayUSD;
            set => SetProperty(ref _fDeNghiQTNamNayUSD, value);
        }
        private string _fDeNghiQTNamNayVND;
        [ColumnAttribute("VND", 6)]
        public string FDeNghiQTNamNayVND
        {
            get => _fDeNghiQTNamNayVND;
            set => SetProperty(ref _fDeNghiQTNamNayVND, value);
        }
        private string _fLuyKeKinhPhiDuocCapUSD;
        [ColumnAttribute("USD", 7)]
        public string FLuyKeKinhPhiDuocCapUSD
        {
            get => _fLuyKeKinhPhiDuocCapUSD;
            set => SetProperty(ref _fLuyKeKinhPhiDuocCapUSD, value);
        }
        private string _fLuyKeKinhPhiDuocCapVND;
        [ColumnAttribute("VND", 8)]
        public string FLuyKeKinhPhiDuocCapVND
        {
            get => _fLuyKeKinhPhiDuocCapVND;
            set => SetProperty(ref _fLuyKeKinhPhiDuocCapVND, value);
        }
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }
    }
}
