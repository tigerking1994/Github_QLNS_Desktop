using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IMigrationDataService
    {
        DatabaseInfo RestoreLocal(string filePath, string mdfFileName);
        DatabaseInfo RestoreDatabase(string bakFile, string dbName);
        DatabaseInfo RestoreDatabase(string bakFile);
        bool DropDatabase(string dbFile);
        void ClearDataRedundancy();
        void ConfigBudgetCategory();
        void CopyDoiTuong(int dest, int source);
        string RestoreMLSKT(int year);
        void RestoreMLNSThuNop();
        void RemoveWrongSKTChiTiet(int year);
        string GetPhysicalMDF(string dbName);
        DatabaseInfo AttachDatabase(string serverName, string databaseName, string mdfFilePath);
        DatabaseInfo AttachDatabase(string databaseName, string mdfFilePath);
        bool DetachDatabase(string serverName, string databaseName);
        void ApplyScript(IEnumerable<string> scripts);
        IEnumerable<HtTableMigrate> GetListTable();
        IEnumerable<HtTableMigrate> GetListTable(string serverName, string databaseName);
        IEnumerable<Tuple<string, string>> GetListStoredProcedure(string serverName, string databaseName);
        IEnumerable<Tuple<string, string>> GetListTableName(string serverName, string databaseName);
        IEnumerable<string> GetListFunctions(string serverName, string databaseName);
        void MigrateTable(string databaseName, string tableName);
        void MigrateTable(string serverName, string databaseName, string tableName);
        List<NsBkChungTu> GetListNsBkChungTu(string attachDBFile, int? namLamViec);
        List<NsBkChungTuChiTiet> GetListNsBkChungTuChiTiet(string attachDBFile, int? namLamViec);
        List<NsCpChungTu> GetListNsCpChungTu(string attachDBFile, int? namLamViec);
        List<NsCpChungTuChiTiet> GetListNsCpChungTuChiTiet(string attachDBFile, int? namLamViec);
        List<DanhMuc> GetListDanhMuc(string attachDBFile, int? namLamViec);
        string GenerateAddForeignKey(string tableName);
        List<DmChuKy> GetListDanhMucChuKy(string attachDBFile, int? namLamViec);
        List<NsDtChungTu> GetListDtChungTu(string attachDBFile, int? namLamViec);
        List<NsDtChungTuChiTiet> GetListDtChungTuChiTiet(string attachDBFile, int? namLamViec);
        List<DonVi> GetListDonVi(string attachDBFile, int? namLamViec);
        List<NsMucLucNganSach> GetListMlns(string attachDBFile, int? namLamViec);
        List<NsQsChungTu> GetListNsQsChungTu(string attachDBFile, int? namLamViec);
        List<NsQsChungTuChiTiet> GetListNsQsChungTuChiTiet(string attachDBFile, int? namLamViec);
        List<NsQsMucLuc> GetListNsQsMucluc(string attachDBFile, int? namLamViec);
        List<NsQtChungTu> GetListNsQtChungTu(string attachDBFile, int? namLamViec);
        List<NsQtChungTuChiTiet> GetListNsQtChungTuChiTiet(string attachDBFile, int? namLamViec);
        List<NsQtChungTuChiTietGiaiThich> GetListNsQtChungTuChiTietGiaiThich(string attachDBFile, int? namLamViec);
        List<NsQtChungTuChiTietGiaiThichLuongTru> GetListNsQtChungTuChiTietGiaiThichLuongTru(string attachDBFile, int? namLamViec);
        List<NsMlsktMlns> GetListMapMLNSSKT(string attachDBFile, int? namLamViec);
        List<NsSktMucLuc> GetListSktMucLuc(string attachDBFile, int? namLamViec);
        List<NsSktChungTuChiTiet> GetListSktChungTuChiTiets(string attachDBFile, int? namLamViec);
        List<NsSktChungTu> GetListSktChungTu(string attachDBFile, int? namLamViec);
        List<NsMucLucNganSach> GetListExistMlns();
        List<NsDtdauNamChungTuChiTiet> GetListDtDauNamCTCT(string attachDBFile, int? namLamViec);
        List<NsMucLucNganSach> GetListMissingMlnsByNamLamViec(string attachDBFile, int namLamViec);
        List<DanhMuc> GetListDanhmucChuKyTen(string attachDBFile, int? namLamViec);
        List<DanhMuc> GetListDanhmucChuKyChucDanh(string attachDBFile, int? namLamViec);
        void SaveData(IEnumerable<NsBkChungTu> nsBkChungTus, IEnumerable<NsBkChungTuChiTiet> nsBkChungTuChiTiets,
            IEnumerable<NsCpChungTu> nsCpChungTus, IEnumerable<NsCpChungTuChiTiet> nsCpChungTuChiTiets,
            IEnumerable<DanhMuc> danhMucs, IEnumerable<DmChuKy> dmChuKies, IEnumerable<NsDtChungTu> nsDtChungTus,
            IEnumerable<NsDtChungTuChiTiet> nsDtChungTuChiTiets, IEnumerable<DonVi> donVis, IEnumerable<NsMucLucNganSach> nsMucLucNganSaches,
            IEnumerable<NsQsChungTu> nsQsChungTus, IEnumerable<NsQsChungTuChiTiet> nsQsChungTuChiTiets, IEnumerable<NsQsMucLuc> nsQsMucLucs,
            IEnumerable<NsQtChungTu> nsQtChungTus, IEnumerable<NsQtChungTuChiTiet> nsQtChungTuChiTiets, IEnumerable<NsQtChungTuChiTietGiaiThich> nsQtChungTuChiTietGiaiThiches,
            IEnumerable<NsQtChungTuChiTietGiaiThichLuongTru> nsQtChungTuChiTietGiaiThichLuongTrus,
            IEnumerable<NsSktChungTu> nsSktChungTus,
            IEnumerable<NsSktChungTuChiTiet> nsSktChungTuChiTiets,
            IEnumerable<NsSktMucLuc> nsSktMucLucs,
            IEnumerable<NsMlsktMlns> nsMlsktMlns, IEnumerable<NsDtdauNamChungTu> nsDtdauNamChungTus,
            IEnumerable<NsDtdauNamChungTuChiTiet> nsDtdauNamChungTuChiTiets, IEnumerable<NsDtNhanPhanBoMap> nsDtNhanPhanBoMaps,
            IEnumerable<DmChuKy> dmChuKy,
            IEnumerable<DanhMuc> dmChuKyChucDanh,
            IEnumerable<DanhMuc> dmChuKyTen);
        void ResetMigrateVersion190();
    }
}
