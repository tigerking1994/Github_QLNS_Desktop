using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmTiGiaService
    {
        void Add(NhDmTiGia nhDmTiGia);
        void Remove(NhDmTiGia nhDmTiGia);
        void Update(NhDmTiGia nhDmTiGia);
        void Delete(Guid id);
        NhDmTiGia FindById(Guid id);
        IEnumerable<NhDmTiGia> FindAll();
        IEnumerable<NhDmTiGia> FindByCondition(Expression<Func<NhDmTiGia, bool>> predicate);
        /// <summary>
        /// Qui đổi tiền tệ
        /// </summary>
        /// <param name="sourceCurrency">Mã tiền tệ qui đổi</param>
        /// <param name="destCurrency">Mã tiền tệ muốn qui đổi sang</param>
        /// <param name="rootCurrency">Mã tiền tệ tham chiếu của tỉ giá</param>
        /// <param name="tiGiaThamChieu">Danh sách tỉ giá chi tiết</param>
        /// <param name="value">Giá trị qui đổi</param>
        /// <returns></returns>
        double CurrencyExchange(string sourceCurrency, string destCurrency, string rootCurrency, IEnumerable<NhDmTiGiaChiTiet> tiGiaThamChieu, double value);
    }
}
