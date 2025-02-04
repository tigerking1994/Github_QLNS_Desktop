using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    public class DanhMucCheDoBHXHImportModel : BaseImportModel
    {
        private string _sMaCheDom;
        [ColumnAttribute("Mã chế độ", 0, "Mã chế độ", "Mã chế độ", ValidateType.IsString, true)]
        [DisplayDetailInfo("Mã chế độ")]
        public string SMaCheDo
        {
            get => _sMaCheDom;
            set => SetProperty(ref _sMaCheDom, value);
        }

        private string _sTenCheDo;
        [ColumnAttribute("Tên chế độ", 1, "Tên chế độ", "Tên chế độ", ValidateType.IsString)]
        [DisplayDetailInfo("Tên chế độ")]
        public string STenCheDo
        {
            get => _sTenCheDo;
            set => SetProperty(ref _sTenCheDo, value);
        }

        //private string _iLoaiCheDo;
        //[ColumnAttribute("Loại chế độ", 2, "Loại chế độ", "Loại chế độ", ValidateType.IsIntNumber)]
        //[DisplayDetailInfo("Loại chế độ")]
        //public string ILoaiCheDo
        //{
        //    get => _iLoaiCheDo;
        //    set => SetProperty(ref _iLoaiCheDo, value);
        //}

        //private string _isFormula;
        //[DisplayDetailInfo("Tính theo công thức")]
        //[ColumnAttribute("Tính theo công thức", 3, "Tính theo công thức", "Tính theo công thức", ValidateType.IsIntNumber)]
        //public string IsFormula
        //{
        //    get => _isFormula;
        //    set => SetProperty(ref _isFormula, value);
        //}

        private string _sMaCheDoCha;
        [DisplayDetailInfo("Mã chế độ cha")]
        [ColumnAttribute("Mã chế độ cha", 2, "Mã chế độ cha", "Mã chế độ cha")]
        public string SMaCheDoCha
        {
            get => _sMaCheDoCha;
            set => SetProperty(ref _sMaCheDoCha, value);
        }

        private string _sMoTa;
        [DisplayDetailInfo("Mô tả")]
        [ColumnAttribute("Mô tả", 3, "Mô tả", "Mô tả")]
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }


    }
}
