using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Import;
using ColumnAttribute = VTS.QLNS.CTC.App.Model.Import.ColumnAttribute;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhKtKhoiTaoCapPhatChiTietModel : ModelBase
    {
        public Guid? IIdKhoiTaoCapPhatID { get; set; }
        public Guid? IIdDuAnID { get; set; }
        public Guid? IIdHopDongID { get; set; }
        private double? _fQTKinhPhiDuyetCacNamTruocUSD;
        [Validate("Kinh phí duyệt các năm trước USD", Utility.Enum.DATA_TYPE.Double, true)]
        public double? FQTKinhPhiDuyetCacNamTruocUSD
        {
            get => _fQTKinhPhiDuyetCacNamTruocUSD;
            set => SetProperty(ref _fQTKinhPhiDuyetCacNamTruocUSD, value);
        }
        private double? _fQTKinhPhiDuyetCacNamTruocVND;
        [Validate("Kinh phí duyệt các năm trước VND", Utility.Enum.DATA_TYPE.Double, true)]
        public double? FQTKinhPhiDuyetCacNamTruocVND
        {
            get => _fQTKinhPhiDuyetCacNamTruocVND;
            set => SetProperty(ref _fQTKinhPhiDuyetCacNamTruocVND, value);
        }
        private double? _fDeNghiQTNamNayUSD;
        [Validate("Đề nghị quyết toán năm nay USD", Utility.Enum.DATA_TYPE.Double, true)]
        public double? FDeNghiQTNamNayUSD
        {
            get => _fDeNghiQTNamNayUSD;
            set => SetProperty(ref _fDeNghiQTNamNayUSD, value);
        }
        private double? _fDeNghiQTNamNayVND;
        [Validate("Đề nghị quyết toán năm nay VND", Utility.Enum.DATA_TYPE.Double, true)]
        public double? FDeNghiQTNamNayVND
        {
            get => _fDeNghiQTNamNayVND;
            set => SetProperty(ref _fDeNghiQTNamNayVND, value);
        }
        private double? _fLuyKeKinhPhiDuocCapUSD;
        [Validate("Lũy kế kinh phí được cấp USD", Utility.Enum.DATA_TYPE.Double, true)]
        public double? FLuyKeKinhPhiDuocCapUSD
        {
            get => _fLuyKeKinhPhiDuocCapUSD;
            set => SetProperty(ref _fLuyKeKinhPhiDuocCapUSD, value);
        }
        private double? _fLuyKeKinhPhiDuocCapVND;
        [Validate("Lũy kế kinh phí được cấp VND", Utility.Enum.DATA_TYPE.Double, true)]
        public double? FLuyKeKinhPhiDuocCapVND
        {
            get => _fLuyKeKinhPhiDuocCapVND;
            set => SetProperty(ref _fLuyKeKinhPhiDuocCapVND, value);
        }
        public Guid? IIdParentID { get; set; }
        public string SMaDuAn { get; set; }
        public string STenDuAn { get; set; }
        public string SMaHopDong { get; set; }
        public string STenHopDong { get; set; }
        public int STT { get; set; }

        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }
    }
}
