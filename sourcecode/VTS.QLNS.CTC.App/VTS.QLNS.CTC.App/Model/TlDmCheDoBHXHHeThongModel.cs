using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmCheDoBHXHHeThongModel : ModelBase
    {
        private string _sMaCheDo;
        [DisplayName("Mã chế độ")]
        [DisplayDetailInfo("Mã chế độ")]
        public string SMaCheDo
        {
            get => _sMaCheDo;
            set => SetProperty(ref _sMaCheDo, value);
        }

        private string _sTenCheDo;
        [DisplayName("Tên chế độ")]
        [DisplayDetailInfo("Tên chế độ")]
        public string TenPhuCap
        {
            get => _sTenCheDo;
            set => SetProperty(ref _sTenCheDo, value);
        }

        private int? _iLoaiCheDo;
        [DisplayName("Loại chế độ")]
        [DisplayDetailInfo("Loại chế độ")]
        public int? ILoaiCheDo
        {
            get => _iLoaiCheDo;
            set => SetProperty(ref _iLoaiCheDo, value);
        }

        private bool _isFormula;
        [DisplayName("Tính theo công thức")]
        [ColumnType(ColumnType.Checkbox)]
        public bool IsFormula
        {
            get => _isFormula;
            set => SetProperty(ref _isFormula, value);
        }

        private string _sMaCheDoCha;
        [DisplayName("Chế độ cha")]
        [DisplayDetailInfo("Chế độ cha")]
        public string SMaCheDoCha
        {
            get => _sMaCheDoCha;
            set => SetProperty(ref _sMaCheDoCha, value);
        }

        private string _sMoTa;
        [DisplayName("Chế độ cha")]
        [DisplayDetailInfo("Chế độ cha")]
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMaCheDoCha, value);
        }

        private decimal? _fGiaTri;
        [DisplayName("Giá trị mặc định")]
        [DisplayDetailInfo("Giá trị mặc định")]
        [FormatAttribute("{0:N4}")]
        public decimal? FGiaTri
        {
            get => _fGiaTri;
            set => SetProperty(ref _fGiaTri, value);
        }
    }
}
