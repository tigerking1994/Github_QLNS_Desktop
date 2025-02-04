using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class MigrationDataService : IMigrationDataService
    {
        private readonly IHtTableMigrateRepository _htTableMigrateRepository;
        private readonly IMigrationDataRepository _migrationDataRepository;

        public MigrationDataService(IMigrationDataRepository migrationDataRepository, IHtTableMigrateRepository htTableMigrateRepository)
        {
            _migrationDataRepository = migrationDataRepository;
            _htTableMigrateRepository = htTableMigrateRepository;
        }

        public void ClearDataRedundancy()
        {
            _migrationDataRepository.ClearDataRedundancy();
        }

        public void CopyDoiTuong(int dest, int source)
        {
            _migrationDataRepository.CopyDoiTuong(dest, source);
        }

        public void RestoreMLNSThuNop()
        {
            _migrationDataRepository.RestoreMLNSThuNop();
        }

        public string RestoreMLSKT(int year)
        {
            return _migrationDataRepository.RestoreMLSKT(year);
        }

        public void RemoveWrongSKTChiTiet(int year)
        {
            _migrationDataRepository.RemoveWrongSKTChiTiet(year);
        }

        public DatabaseInfo AttachDatabase(string serverName, string databaseName, string mdfFilePath)
        {
            return _migrationDataRepository.AttachDatabase(serverName, databaseName, mdfFilePath);
        }

        public DatabaseInfo AttachDatabase(string databaseName, string mdfFilePath)
        {
            return AttachDatabase("(LocalDB)\\v11.0", databaseName, mdfFilePath);
        }
        public bool DetachDatabase(string serverName, string databaseName)
        {
            return _migrationDataRepository.DetachDatabase(serverName, databaseName);
        }

        public void ApplyScript(IEnumerable<string> scripts)
        {
            _migrationDataRepository.ApplyScript(scripts);
        }

        public IEnumerable<HtTableMigrate> GetListTable()
        {
            return _migrationDataRepository.GetListTable();
        }
        public IEnumerable<HtTableMigrate> GetListTable(string serverName, string databaseName)
        {
            return _migrationDataRepository.GetListTable(serverName, databaseName);
        }

        public IEnumerable<Tuple<string, string>> GetListStoredProcedure(string serverName, string databaseName)
        {
            return _migrationDataRepository.GetListStoredProcedure(serverName, databaseName);
        }

        public IEnumerable<Tuple<string, string>> GetListTableName(string serverName, string databaseName)
        {
            return _migrationDataRepository.GetListTableName(serverName, databaseName);
        }

        public IEnumerable<string> GetListFunctions(string serverName, string databaseName)
        {
            return _migrationDataRepository.GetListFunctions(serverName, databaseName);
        }

        public void MigrateTable(string databaseName, string tableName)
        {
            MigrateTable(@"(LocalDB)\v11.0", databaseName, tableName);
        }

        public void MigrateTable(string serverName, string databaseName, string tableName)
        {
            HtTableMigrate table = _htTableMigrateRepository.FirstOrDefault(x => x.Object == tableName);
            if (table is null) table = new HtTableMigrate
            {
                Object = tableName,
                MigrateFrequency = 0,
            };
            try
            {
                _migrationDataRepository.MigrateTable(serverName, databaseName, tableName);
                table.IsMigrated = true;
                table.Description = "SUCCESS";
                table.MigrateFrequency++;
            }
            catch (Exception ex)
            {
                table.IsMigrated = false;
                table.Description = ex.Message;
            }
            if (table.Id == Guid.Empty)
            {
                _htTableMigrateRepository.Add(table);
            }
            else
            {
                _htTableMigrateRepository.Update(table);
            }
        }

        public void ConfigBudgetCategory()
        {
            _migrationDataRepository.ConfigBudgetCategory();
        }
        public bool DropDatabase(string dbFile)
        {
            return _migrationDataRepository.DetachDatabase(@"(LocalDB)\v11.0", dbFile);
        }

        public List<DanhMuc> GetListDanhMuc(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListDanhMuc(attachDBFile, namLamViec);
        }

        public string GenerateAddForeignKey(string tableName)
        {
            return _migrationDataRepository.GenerateAddForeignKey(tableName);
        }
        public List<DmChuKy> GetListDanhMucChuKy(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListDanhMucChuKy(attachDBFile, namLamViec);
        }

        public List<DonVi> GetListDonVi(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListDonVi(attachDBFile, namLamViec);
        }

        public List<NsDtChungTu> GetListDtChungTu(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListDtChungTu(attachDBFile, namLamViec);
        }

        public List<NsDtChungTuChiTiet> GetListDtChungTuChiTiet(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListDtChungTuChiTiet(attachDBFile, namLamViec);
        }

        public List<NsDtdauNamChungTuChiTiet> GetListDtDauNamCTCT(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListDtDauNamCTCT(attachDBFile, namLamViec);
        }

        public List<NsMucLucNganSach> GetListExistMlns()
        {
            return _migrationDataRepository.GetListExistMlns();
        }

        public List<NsMlsktMlns> GetListMapMLNSSKT(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListMapMLNSSKT(attachDBFile, namLamViec);
        }

        public List<NsMucLucNganSach> GetListMlns(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListMlns(attachDBFile, namLamViec);
        }

        public List<NsBkChungTu> GetListNsBkChungTu(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListNsBkChungTu(attachDBFile, namLamViec);
        }

        public List<NsBkChungTuChiTiet> GetListNsBkChungTuChiTiet(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListNsBkChungTuChiTiet(attachDBFile, namLamViec);
        }

        public List<NsCpChungTu> GetListNsCpChungTu(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListNsCpChungTu(attachDBFile, namLamViec);
        }

        public List<NsCpChungTuChiTiet> GetListNsCpChungTuChiTiet(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListNsCpChungTuChiTiet(attachDBFile, namLamViec);
        }

        public List<NsQsChungTu> GetListNsQsChungTu(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListNsQsChungTu(attachDBFile, namLamViec);
        }

        public List<NsQsChungTuChiTiet> GetListNsQsChungTuChiTiet(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListNsQsChungTuChiTiet(attachDBFile, namLamViec);
        }

        public List<NsQsMucLuc> GetListNsQsMucluc(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListNsQsMucluc(attachDBFile, namLamViec);
        }

        public List<NsQtChungTu> GetListNsQtChungTu(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListNsQtChungTu(attachDBFile, namLamViec);
        }

        public List<NsQtChungTuChiTiet> GetListNsQtChungTuChiTiet(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListNsQtChungTuChiTiet(attachDBFile, namLamViec);
        }

        public List<NsQtChungTuChiTietGiaiThich> GetListNsQtChungTuChiTietGiaiThich(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListNsQtChungTuChiTietGiaiThich(attachDBFile, namLamViec);
        }

        public List<NsQtChungTuChiTietGiaiThichLuongTru> GetListNsQtChungTuChiTietGiaiThichLuongTru(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListNsQtChungTuChiTietGiaiThichLuongTru(attachDBFile, namLamViec);
        }

        public List<NsSktChungTu> GetListSktChungTu(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListSktChungTu(attachDBFile, namLamViec);
        }

        public List<NsSktChungTuChiTiet> GetListSktChungTuChiTiets(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListSktChungTuChiTiets(attachDBFile, namLamViec);
        }

        public List<NsSktMucLuc> GetListSktMucLuc(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListSktMucLuc(attachDBFile, namLamViec);
        }

        public List<NsMucLucNganSach> GetListMissingMlnsByNamLamViec(string attachDBFile, int namLamViec)
        {
            return _migrationDataRepository.GetListMissingMlnsByNamLamViec(attachDBFile, namLamViec);
        }

        public string GetPhysicalMDF(string dbName)
        {
            return _migrationDataRepository.GetPhysicalMDF(dbName);
        }

        public DatabaseInfo RestoreLocal(string filePath, string mdfFileName)
        {
            return _migrationDataRepository.RestoreLocal(filePath, mdfFileName);
        }

        public DatabaseInfo RestoreDatabase(string bakFile, string dbName)
        {
            return _migrationDataRepository.RestoreDatabase(bakFile, dbName);
        }
        public DatabaseInfo RestoreDatabase(string bakFile)
        {
            return _migrationDataRepository.RestoreDatabase(bakFile);
        }

        public void SaveData(IEnumerable<NsBkChungTu> nsBkChungTus, IEnumerable<NsBkChungTuChiTiet> nsBkChungTuChiTiets,
            IEnumerable<NsCpChungTu> nsCpChungTus, IEnumerable<NsCpChungTuChiTiet> nsCpChungTuChiTiets, IEnumerable<DanhMuc> danhMucs,
            IEnumerable<DmChuKy> dmChuKies, IEnumerable<NsDtChungTu> nsDtChungTus, IEnumerable<NsDtChungTuChiTiet> nsDtChungTuChiTiets,
            IEnumerable<DonVi> donVis, IEnumerable<NsMucLucNganSach> nsMucLucNganSaches, IEnumerable<NsQsChungTu> nsQsChungTus,
            IEnumerable<NsQsChungTuChiTiet> nsQsChungTuChiTiets, IEnumerable<NsQsMucLuc> nsQsMucLucs, IEnumerable<NsQtChungTu> nsQtChungTus,
            IEnumerable<NsQtChungTuChiTiet> nsQtChungTuChiTiets, IEnumerable<NsQtChungTuChiTietGiaiThich> nsQtChungTuChiTietGiaiThiches,
            IEnumerable<NsQtChungTuChiTietGiaiThichLuongTru> nsQtChungTuChiTietGiaiThichLuongTrus,
            IEnumerable<NsSktChungTu> nsSktChungTus,
            IEnumerable<NsSktChungTuChiTiet> nsSktChungTuChiTiets,
            IEnumerable<NsSktMucLuc> nsSktMucLucs,
            IEnumerable<NsMlsktMlns> nsMlsktMlns,
            IEnumerable<NsDtdauNamChungTu> nsDtdauNamChungTus,
            IEnumerable<NsDtdauNamChungTuChiTiet> nsDtdauNamChungTuChiTiets,
            IEnumerable<NsDtNhanPhanBoMap> nsDtNhanPhanBoMaps,
            IEnumerable<DmChuKy> dmChuKy,
            IEnumerable<DanhMuc> dmChuKyChucDanh,
            IEnumerable<DanhMuc> dmChuKyTen)
        {
            _migrationDataRepository.SaveData(nsBkChungTus, nsBkChungTuChiTiets, nsCpChungTus, nsCpChungTuChiTiets, danhMucs, dmChuKies, nsDtChungTus,
                        nsDtChungTuChiTiets, donVis, nsMucLucNganSaches, nsQsChungTus, nsQsChungTuChiTiets, nsQsMucLucs, nsQtChungTus, nsQtChungTuChiTiets,
                        nsQtChungTuChiTietGiaiThiches, nsQtChungTuChiTietGiaiThichLuongTrus, nsSktChungTus, nsSktChungTuChiTiets, nsSktMucLucs, nsMlsktMlns, nsDtdauNamChungTus,
                        nsDtdauNamChungTuChiTiets, nsDtNhanPhanBoMaps, dmChuKy, dmChuKyChucDanh, dmChuKyTen);
        }

        public List<DanhMuc> GetListDanhmucChuKyTen(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListDanhmucChuKyTen(attachDBFile, namLamViec);
        }

        public List<DanhMuc> GetListDanhmucChuKyChucDanh(string attachDBFile, int? namLamViec)
        {
            return _migrationDataRepository.GetListDanhmucChuKyChucDanh(attachDBFile, namLamViec);
        }

        public void ResetMigrateVersion190()
        {
            _migrationDataRepository.ResetMigrateVersion190();
        }
    }
}
