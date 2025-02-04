using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IDanhMucRepository : IRepository<DanhMuc>
    {
        IEnumerable<DanhMuc> FindDanhMucTheoNganh(string idChungTu, int nameLamViec, string type);
        IEnumerable<DanhMuc> FindByType(string type, int namLamViec);
        IEnumerable<DanhMuc> FindChuKyChucDanh();
        IEnumerable<DanhMuc> FindChuKyTen();
        IEnumerable<DanhMuc> FindChuKyTieuDe1();
        IEnumerable<DanhMuc> FindChuKyTieuDe2();
        IEnumerable<DanhMuc> FindNhomChuKy(int year);
        DanhMuc FindByCode(string idCode, int? namLamViec = null);
        IEnumerable<DanhMuc> FindByType(string type);
        int countDanhMucByTypeAndNLV(string type, int namLamViec);
        IEnumerable<DanhMuc> FindDmChuyenNganhByNsDonvi(IEnumerable<Guid> excludeIds, int year);
        void UpdateDonViOfDanhMuc(IEnumerable<DanhMuc> entities, string idDonVi);
        void AddDonViOfDanhMuc(IEnumerable<DanhMuc> entities, string idDonVi);
        void RemoveDonViOfDanhMuc(IEnumerable<DanhMuc> entities, string idDonVi);
        int CountDmCauHinhHeThongByYear(int namLamViec);
        string FindDonViQuanLy(int namLamViec);
        int CountDanhMucNganhChuyenNganh(int year);
        IEnumerable<DanhMuc> FindNhomChuKyTen();
        IEnumerable<DanhMuc> FindNhomChuKyChucDanh();
    }
}
