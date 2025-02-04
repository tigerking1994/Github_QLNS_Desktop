//using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class MigrationDataRepository : Repository<EntityBase>, IMigrationDataRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        private readonly ILogger<MigrationDataRepository> _logger;
        private readonly INsMucLucNganSachRepository _nsMucLucNganSachRepository;
        private readonly INsQtChungTuChiTietRepository _nsQtChungTuChiTietRepository;
        private readonly INsQsChungTuChiTietRepository _nsQsChungTuChiTietRepository;
        private readonly ISktChungTuChiTietRepository _sktChungTuChiTietRepository;
        private readonly ISktMucLucRepository _sktMucLucRepository;
        private readonly ISktMucLucMapRepository _sktMucLucMapRepository;
        private readonly ISktSoLieuRepository _sktSoLieuRepository;
        private readonly INsDtNhanPhanBoMapRepository _nsDtNhanPhanBoMapRepository;
        private readonly INsDonViService _nsDonViService;
        private readonly IConfiguration _configuration;
        private readonly int NAM_LAM_VIEC = 2100;

        public MigrationDataRepository(ApplicationDbContextFactory contextFactory, ILogger<MigrationDataRepository> logger,
            INsMucLucNganSachRepository nsMucLucNganSachRepository,
            INsQtChungTuChiTietRepository nsQtChungTuChiTietRepository,
            INsQsChungTuChiTietRepository nsQsChungTuChiTietRepository,
            ISktChungTuChiTietRepository sktChungTuChiTietRepository,
            ISktMucLucRepository sktMucLucRepository,
            ISktMucLucMapRepository sktMucLucMapRepository,
            ISktSoLieuRepository sktSoLieuRepository,
            INsDtNhanPhanBoMapRepository nsDtNhanPhanBoMapRepository,
            INsDonViService nsDonViService, IConfiguration configuration)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
            _logger = logger;
            _nsMucLucNganSachRepository = nsMucLucNganSachRepository;
            _nsQtChungTuChiTietRepository = nsQtChungTuChiTietRepository;
            _nsQsChungTuChiTietRepository = nsQsChungTuChiTietRepository;
            _sktChungTuChiTietRepository = sktChungTuChiTietRepository;
            _sktMucLucRepository = sktMucLucRepository;
            _sktMucLucMapRepository = sktMucLucMapRepository;
            _sktSoLieuRepository = sktSoLieuRepository;
            _nsDtNhanPhanBoMapRepository = nsDtNhanPhanBoMapRepository;
            _nsDonViService = nsDonViService;
            _configuration = configuration;
        }

        private string GetConnectionString(string attachDBFile)
        {
            return @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + attachDBFile;
        }

        public DatabaseInfo RestoreLocal(string filePath, string databaseName)
        {
            string mdfFileName = $"{databaseName}_{DateTime.Now.ToString("ddMMyyyyhhmmss")}";
            SqlConnection.ClearAllPools();
            string mdfPath = $"{Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AppData", mdfFileName)}.mdf";
            string ldfPath = mdfPath.Replace(".mdf", "_log.ldf");
            DataTable logicalDataFile = new DataTable();

            string restoreFilePath = "";

            try
            {
                using SqlConnection connection = new SqlConnection();
                connection.ConnectionString = @"Data Source=(LocalDB)\v11.0;Database=master";
                connection.Open();

                // unzip restore file

                if (Path.GetExtension(filePath).EndsWith(".zip"))
                {
                    using ZipArchive zipArchive = ZipFile.OpenRead(filePath);
                    string dirPath = Path.GetDirectoryName(filePath);
                    zipArchive.ExtractToDirectory(dirPath);
                    restoreFilePath = Path.Combine(dirPath, zipArchive.Entries[0].FullName);
                }
                else
                {
                    restoreFilePath = filePath;
                }

                // get logical file name
                using SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                command.CommandText = $@"restore filelistonly from disk = '{restoreFilePath}'";
                command.CommandTimeout = 900;
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(logicalDataFile);
                }
                string logicalName = logicalDataFile.AsEnumerable().FirstOrDefault(x => x.Field<string>("Type") == "D").Field<string>("LogicalName");

                // restore database 
                command.CommandText = $@"
                    restore database {mdfFileName}
                    from disk = '{restoreFilePath}'
                    with move '{logicalName}' to '{mdfPath}',
                    move '{logicalName}_log' to '{ldfPath}'
                ";
                command.ExecuteNonQuery();

                return new DatabaseInfo { Name = mdfFileName, MdfFilePath = mdfPath, LogFilePath = ldfPath };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
            finally
            {
                if (File.Exists(restoreFilePath))
                {
                    File.Delete(restoreFilePath);
                }
            }
        }

        public DatabaseInfo RestoreDatabase(string bakFile, string dbName)
        {
            string destinationFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AppData"); ;
            string destinationMdf = Path.Combine(destinationFolder, dbName + ".mdf");
            string destinationLog = Path.Combine(destinationFolder, dbName + "_log.mdf");
            string sql = string.Format(@"RESTORE DATABASE {0} FROM  DISK = N'{1}'
                        WITH replace, MOVE N'ns_donvi' TO N'{2}',  MOVE N'ns_donvi_log' TO N'{3}'", dbName, bakFile, destinationMdf, destinationLog);
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = @"Data Source=(LocalDB)\v11.0;Database=master";
                conn.Open();
                using (SqlCommand queryCmd = new SqlCommand())
                {
                    queryCmd.Connection = conn;
                    queryCmd.CommandText = sql;
                    queryCmd.ExecuteNonQuery();
                }
            }
            return new DatabaseInfo { Name = dbName, MdfFilePath = destinationMdf, LogFilePath = destinationLog };
        }

        public string GetPhysicalMDF(string dbName)
        {
            using ApplicationDbContext ctx = _contextFactory.CreateDbContext();
            return ctx.GetDataTable($"select physical_name from sys.master_files where type_desc = 'ROWS' and database_id = db_id('{dbName}')", CommandType.Text).AsEnumerable().Select(x => x.Field<string>("physical_name")).FirstOrDefault();
        }

        public DatabaseInfo RestoreDatabase(string bakFile)
        {
            string dbName = "QLNS_MIGRATE" + DateTime.Now.ToString("ddMMyyyyhhmmss");
            return RestoreDatabase(bakFile, dbName);
        }

        public void ClearDataRedundancy()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_clear_data_redundancy";
                ctx.Database.ExecuteSqlCommand(sql);
            }
        }

        public void CopyDoiTuong(int dest, int source)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter destYearOfWorkParam = new SqlParameter("@DestYearOfWork", dest);
                SqlParameter sourceYearOfWorkParam = new SqlParameter("@SourceYearOfWork", source);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_copy_doi_tuong_mlns @DestYearOfWork, @SourceYearOfWork", destYearOfWorkParam, sourceYearOfWorkParam);
            }
        }

        public void RestoreMLNSThuNop()
        {
            using var context = _contextFactory.CreateDbContext();
            using var transction = context.Database.BeginTransaction();
            try
            {
                context.Database.ExecuteSqlCommand("delete from NS_MucLucNganSach where inamlamviec = 2025 and sxaunoima like '8%'");
                context.Database.ExecuteSqlCommand("insert into NS_MucLucNganSach select * from NS_MucLucNganSach_ThuNop_2025");
                transction.Commit();
            } catch (Exception ex)
            {
                transction.Rollback();
                throw ex;
            }
        }

        public string RestoreMLSKT(int year)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearParam = new SqlParameter("@Year", year);
                return ctx.FromSqlRaw<string>("execute dbo.sp_restore_mlskt @Year", yearParam).FirstOrDefault();
            }
        }

        public void ConfigBudgetCategory()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "execute dbo.sp_config_budget_category";
                ctx.Database.ExecuteSqlCommand(sql);
            }
        }

        public void RemoveWrongSKTChiTiet(int year)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearParam = new SqlParameter("@Year", year);
                string sql = "execute dbo.sp_remove_wrong_skt_chitiet @Year";
                ctx.Database.ExecuteSqlCommand(sql, yearParam);
            }
        }

        public DatabaseInfo AttachDatabase(string serverName, string databaseName, string mdfFilePath)
        {
            bool exists = false;
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = $@"Data Source={serverName};Database=master; Trusted_Connection=True;";
                    conn.Open();
                    using (SqlCommand queryCmd = new SqlCommand())
                    {
                        queryCmd.Connection = conn;
                        queryCmd.CommandText = $@"select count(*) from sys.databases where name = '{databaseName}'";
                        exists = (int)queryCmd.ExecuteScalar() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking database existence: {ex.Message}");
                return null;
            }

            if (exists) return new DatabaseInfo { Name = databaseName };

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = $@"Data Source={serverName};Database=master; Trusted_Connection=True;";
                    conn.Open();
                    using (SqlCommand queryCmd = new SqlCommand())
                    {
                        queryCmd.Connection = conn;
                        queryCmd.CommandText = $@"
                            create database [{databaseName}]
                            on (filename = '{mdfFilePath}')
                            for attach_rebuild_log";
                        queryCmd.ExecuteNonQuery();
                    }
                    return new DatabaseInfo
                    {
                        Name = databaseName,
                        MdfFilePath = mdfFilePath,
                        LogFilePath = mdfFilePath.Replace(".mdf", "_log.ldf")
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error attaching database: {ex.Message}");
                return null;
            }
        }

        public bool DetachDatabase(string serverName, string databaseName)
        {
            bool exists = false;
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = $@"Data Source={serverName};Database=master; Trusted_Connection=True;";
                    conn.Open();
                    using (SqlCommand queryCmd = new SqlCommand())
                    {
                        queryCmd.Connection = conn;
                        queryCmd.CommandText = $@"select count(*) from sys.databases where name = '{databaseName}'";
                        exists = (int)queryCmd.ExecuteScalar() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking database existence: {ex.Message}");
                return false;
            }

            if (exists)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = $@"Data Source={serverName};Database=master; Trusted_Connection=True;";
                        conn.Open();
                        using (SqlCommand queryCmd = new SqlCommand())
                        {
                            queryCmd.Connection = conn;
                            queryCmd.CommandText = $@"alter database [{databaseName}] set single_user with rollback immediate; exec dbo.sp_detach_db '{databaseName}'";
                            queryCmd.ExecuteNonQuery();
                        }
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error detaching database: {ex.Message}");
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<Tuple<string, string>> GetListTableName(string serverName, string databaseName)
        {
            try
            {
                List<Tuple<string, string>> result = new List<Tuple<string, string>>();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = $@"Data Source={serverName};Database={databaseName};Trusted_Connection=True;";
                    conn.Open();
                    using (SqlCommand queryCmd = new SqlCommand())
                    {
                        queryCmd.Connection = conn;
                        queryCmd.CommandText = $@"
	                        select table_name, table_schema from information_schema.tables
	                        where table_type = 'BASE TABLE' and table_catalog = '{databaseName}'
	                        order by table_name
                        ";
                        queryCmd.CommandTimeout = 120;
                        using (SqlDataReader reader = queryCmd.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            if (dataTable.Rows.Count > 0)
                            {
                                result = dataTable.AsEnumerable().Select(x => Tuple.Create(x.Field<string>("table_name"), x.Field<string>("table_schema"))).ToList();
                            }


                            //result = TypeUtilities.DataTableToListObject<string>(dataTable);
                        }
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error get list table name: {ex.Message}");
                return new List<Tuple<string, string>>();
            }
        }

        public IEnumerable<HtTableMigrate> GetListTable()
        {
            try
            {
                List<NsMucLucNganSach> result = new List<NsMucLucNganSach>();
                using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
                {
                    return ctx.FromSqlRaw<HtTableMigrate>($@"
                            select t.table_name Object, p.rows SourceRowCount from information_schema.tables t
	                        join sys.objects s on t.table_name = s.name
	                        join sys.partitions p on s.object_id = p.object_id
	                        where t.table_type = 'BASE TABLE'
	                        group by table_name, p.object_id, p.rows
	                        order by table_name");
                }
            }
            catch (Exception)
            {
                return new List<HtTableMigrate>();
            }
        }

        public IEnumerable<HtTableMigrate> GetListTable(string serverName, string databaseName)
        {
            try
            {
                List<HtTableMigrate> result = new List<HtTableMigrate>();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = $@"Data Source={serverName};Database={databaseName};Trusted_Connection=True;";
                    conn.Open();
                    using (SqlCommand queryCmd = new SqlCommand())
                    {
                        queryCmd.Connection = conn;
                        queryCmd.CommandText = $@"
	                        select t.table_name, p.rows from information_schema.tables t
	                        join sys.objects s on t.table_name = s.name
	                        join sys.partitions p on s.object_id = p.object_id
	                        where t.table_type = 'BASE TABLE' and t.table_catalog = '{databaseName}'
	                        group by table_name, p.object_id, p.rows
	                        order by table_name
                        ";
                        queryCmd.CommandTimeout = 120;
                        using (SqlDataReader reader = queryCmd.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            if (dataTable.Rows.Count > 0)
                            {
                                result = dataTable.AsEnumerable().Select(x => new HtTableMigrate
                                {
                                    Object = x.Field<String>("table_name"),
                                    DestinationRowCount = x.Field<Int64>("rows")
                                }).ToList();
                            }


                            //result = TypeUtilities.DataTableToListObject<string>(dataTable);
                        }
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error get list table name: {ex.Message}");
                return new List<HtTableMigrate>();
            }
        }

        public IEnumerable<Tuple<string, string>> GetListStoredProcedure(string serverName, string databaseName)
        {
            try
            {
                List<Tuple<string, string>> result = new List<Tuple<string, string>>();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = $@"Data Source={serverName};Database={databaseName};Trusted_Connection=True;";
                    conn.Open();
                    using (SqlCommand queryCmd = new SqlCommand())
                    {
                        queryCmd.Connection = conn;
                        queryCmd.CommandText = $@"select p.name, schema_name(p.schema_id) schema_name from sys.procedures p
                                                  join sys.objects o on p.object_id = o.object_id";
                        queryCmd.CommandTimeout = 120;
                        using (SqlDataReader reader = queryCmd.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            result = dataTable.AsEnumerable().Select(x => Tuple.Create(x.Field<string>(0), x.Field<string>(1))).ToList();
                        }
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error get list stored procedures name: {ex.Message}");
                return new List<Tuple<string, string>>();
            }
        }

        public IEnumerable<string> GetListFunctions(string serverName, string databaseName)
        {
            try
            {
                List<string> result = new List<string>();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = $@"Data Source={serverName};Database={databaseName};Trusted_Connection=True;";
                    conn.Open();
                    using (SqlCommand queryCmd = new SqlCommand())
                    {
                        queryCmd.Connection = conn;
                        queryCmd.CommandText = $@"select name from sys.objects where type in ('FN', 'IF', 'TF') order by name";
                        queryCmd.CommandTimeout = 120;
                        using (SqlDataReader reader = queryCmd.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            result = dataTable.AsEnumerable().Select(x => x.Field<string>(0)).ToList();
                        }
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error get list functions name: {ex.Message}");
                return new List<string>();
            }
        }

        public string GenerateAddForeignKey(string tableName)
        {
            try
            {
                using ApplicationDbContext ctx = _contextFactory.CreateDbContext();
                return ctx.FromSqlRaw<string>($@"
                DECLARE @sql NVARCHAR(MAX) = '';

                SELECT @sql += 'ALTER TABLE ' + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) +
                ' ADD CONSTRAINT ' + QUOTENAME(fk.name) +
                ' FOREIGN KEY (' + QUOTENAME(c1.name) + ') REFERENCES ' +
                QUOTENAME(OBJECT_NAME(fk.referenced_object_id)) + '(' + QUOTENAME(c2.name) + '); '
                FROM sys.foreign_keys AS fk
                JOIN sys.foreign_key_columns AS fkc ON fk.object_id = fkc.constraint_object_id
                JOIN sys.columns AS c1 ON fkc.parent_object_id = c1.object_id AND fkc.parent_column_id = c1.column_id
                JOIN sys.columns AS c2 ON fkc.referenced_object_id = c2.object_id AND fkc.referenced_column_id = c2.column_id
                WHERE OBJECT_NAME(fk.referenced_object_id) = '{tableName}';

                SELECT @sql;
            ").FirstOrDefault();
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        public void ApplyScript(IEnumerable<string> scripts)
        {
            using ApplicationDbContext ctx = _contextFactory.CreateDbContext();
            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = ctx.Database.BeginTransaction();
            try
            {
                foreach (string script in scripts)
                {
                    ctx.Database.ExecuteSqlCommand(script);
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public void MigrateTable(string serverName, string databaseName, string tableName)
        {
            try
            {
                DataTable sourceDataTable = new DataTable();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = @$"Data Source={serverName};Database={databaseName};Trusted_Connection=True;";
                    conn.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = $@"select * from {tableName}";
                        command.CommandTimeout = 120;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(sourceDataTable);
                        }
                    }
                    conn.Close();
                }

                using ApplicationDbContext ctx = _contextFactory.CreateDbContext();
                ctx.Database.ExecuteSqlCommand($"alter table {tableName} nocheck constraint all");
                string foreignKeys = ctx.GetDataTable(@$"
                        DECLARE @sql NVARCHAR(MAX) = '';

                        SELECT @sql += 'ALTER TABLE ' + QUOTENAME(OBJECT_NAME(fk.parent_object_id)) +
                        ' DROP CONSTRAINT ' + QUOTENAME(fk.name) + '; '
                        FROM sys.foreign_keys AS fk
                        WHERE fk.referenced_object_id = OBJECT_ID('{tableName}');

                        SELECT @sql;
                    ", CommandType.Text).AsEnumerable().Select(x => x.Field<string>(0)).FirstOrDefault();

                if (!string.IsNullOrEmpty(foreignKeys)) ctx.Database.ExecuteSqlCommand(foreignKeys);
                ctx.Database.ExecuteSqlCommand($"truncate table {tableName}");

                DataTable destinationSchema = ctx.GetDataTable($"select * from {tableName} where 1 = 0", CommandType.Text);


                using (SqlConnection connection = new SqlConnection())
                {
                    ConnectionType _connectionType = _configuration.GetSection("DbSettings:ConnectionType").Value == ConnectionType.SqlServer.ToString() ? ConnectionType.SqlServer : ConnectionType.LocalDb;
                    connection.ConnectionString = _configuration.GetConnectionString(_connectionType.ToString());
                    connection.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = tableName;
                        foreach (DataColumn column in sourceDataTable.Columns)
                        {
                            if (destinationSchema.Columns.Contains(column.ColumnName))
                            {
                                bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                            }
                        }
                        bulkCopy.WriteToServer(sourceDataTable);
                    }
                }

                //ctx.Database.ExecuteSqlCommand($"alter table {tableName} with check check constraint all");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error get list functions name: {ex.Message}");
                throw;
            }
        }

        public void SaveData(IEnumerable<NsBkChungTu> nsBkChungTus, IEnumerable<NsBkChungTuChiTiet> nsBkChungTuChiTiets,
            IEnumerable<NsCpChungTu> nsCpChungTus, IEnumerable<NsCpChungTuChiTiet> nsCpChungTuChiTiets,
            IEnumerable<DanhMuc> danhMucs, IEnumerable<DmChuKy> dmChuKies, IEnumerable<NsDtChungTu> nsDtChungTus,
            IEnumerable<NsDtChungTuChiTiet> nsDtChungTuChiTiets, IEnumerable<DonVi> donVis, IEnumerable<NsMucLucNganSach> nsMucLucNganSaches,
            IEnumerable<NsQsChungTu> nsQsChungTus, IEnumerable<NsQsChungTuChiTiet> nsQsChungTuChiTiets, IEnumerable<NsQsMucLuc> nsQsMucLucs,
            IEnumerable<NsQtChungTu> nsQtChungTus, IEnumerable<NsQtChungTuChiTiet> nsQtChungTuChiTiets, IEnumerable<NsQtChungTuChiTietGiaiThich> nsQtChungTuChiTietGiaiThiches,
            IEnumerable<NsQtChungTuChiTietGiaiThichLuongTru> nsQtChungTuChiTietGiaiThichLuongTrus, IEnumerable<NsSktChungTu> nsSktChungTus,
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
            IEnumerable<int?> namLamViec = nsMucLucNganSaches.Select(t => t.NamLamViec).Distinct();
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string deleteMLNS = string.Format("DELETE FROM NS_MUCLUCNGANSACH WHERE INAMLAMVIEC IN ({0})", string.Join(",", namLamViec));
                ctx.Database.ExecuteSqlCommand(deleteMLNS);
                deleteMLNS = "DELETE FROM NS_QS_MucLuc";
                ctx.Database.ExecuteSqlCommand(deleteMLNS);
                ctx.NsBkChungTus.AddRange(nsBkChungTus);
                ctx.NsBkChungTuChiTiets.AddRange(nsBkChungTuChiTiets);
                ctx.NsCpChungTus.AddRange(nsCpChungTus);
                ctx.NsCpChungTuChiTiets.AddRange(nsCpChungTuChiTiets);
                ctx.DanhMucs.AddRange(danhMucs);
                ctx.NsDtChungTus.AddRange(nsDtChungTus);
                ctx.NsDtChungTuChiTiets.AddRange(nsDtChungTuChiTiets);
                ctx.NsDonVis.AddRange(donVis);
                ctx.NsDtdauNamChungTus.AddRange(nsDtdauNamChungTus);
                ctx.NsQsChungTus.AddRange(nsQsChungTus);
                ctx.NsQsMucLucs.AddRange(nsQsMucLucs);
                ctx.NsQtChungTus.AddRange(nsQtChungTus);
                ctx.NsQtChungTuChiTietGiaiThiches.AddRange(nsQtChungTuChiTietGiaiThiches);
                ctx.NsQtChungTuChiTietGiaiThichLuongTrus.AddRange(nsQtChungTuChiTietGiaiThichLuongTrus);
                ctx.NsSktChungTus.AddRange(nsSktChungTus);
                ctx.DmChuKies.UpdateRange(dmChuKy);
                // remove cac chu ky cu
                IEnumerable<string> dmChuKyChucDanhCode = dmChuKyChucDanh.Select(t => t.IIDMaDanhMuc);
                IEnumerable<string> dmChuKyTenCode = dmChuKyTen.Select(t => t.IIDMaDanhMuc);
                List<DanhMuc> dmChuKyOld = ctx.DanhMucs.Where(t => t.SType.Equals(DanhMucChuKy.CHUC_DANH) && dmChuKyChucDanhCode.Contains(t.IIDMaDanhMuc) ||
                                                                    t.SType.Equals(DanhMucChuKy.TEN) && dmChuKyTenCode.Contains(t.IIDMaDanhMuc)).ToList();
                ctx.DanhMucs.RemoveRange(dmChuKyOld);
                ctx.DanhMucs.AddRange(dmChuKyChucDanh);
                ctx.DanhMucs.AddRange(dmChuKyTen);
                ctx.SaveChanges();
            }
            _nsMucLucNganSachRepository.BulkInsert(nsMucLucNganSaches.ToList());
            _nsQtChungTuChiTietRepository.BulkInsert(nsQtChungTuChiTiets.ToList());
            _sktChungTuChiTietRepository.BulkInsert(nsSktChungTuChiTiets.ToList());
            _nsQsChungTuChiTietRepository.BulkInsert(nsQsChungTuChiTiets.ToList());
            _sktMucLucRepository.BulkInsert(nsSktMucLucs.ToList());
            _sktMucLucMapRepository.BulkInsert(nsMlsktMlns.ToList());
            _sktSoLieuRepository.BulkInsert(nsDtdauNamChungTuChiTiets.ToList());
            _nsDtNhanPhanBoMapRepository.BulkInsert(nsDtNhanPhanBoMaps.ToList());
            foreach (int? nam in namLamViec)
            {
                if (nam.HasValue)
                {
                    UpdateChiTietToi(nam.Value);
                    _nsDonViService.CopyDataToDonViThucHienDuAn(nam.Value);
                }
            }
        }

        public List<T> GetListData<T>(string attachDBFile, string query)
        {
            List<T> result = new List<T>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = GetConnectionString(attachDBFile);
                conn.Open();
                using (SqlCommand queryCmd = new SqlCommand())
                {
                    queryCmd.Connection = conn;
                    queryCmd.CommandText = query;
                    queryCmd.CommandTimeout = 120;
                    using (SqlDataReader reader = queryCmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        result = TypeUtilities.DataTableToListObject<T>(dataTable);
                    }
                    return result;
                }
            }
        }

        public List<NsBkChungTu> GetListNsBkChungTu(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                          ,[SoChungTu] as SSoChungTu
                          ,[SoChungTuIndex] as ISoChungTuIndex
                          ,[NgayChungTu] as DNgayChungTu
                          ,[SoQuyetDinh] as SSoQuyetDinh
                          ,[NgayQuyetDinh] as DNgayQuyetDinh
                          ,[Id_DonVi] as IIdMaDonVi
                          ,[TenDonVi]
                          ,[Id_PhongBan]
                          ,[LNS] as SXauNoiMa
                          ,[MoTa] as SMoTa
                          ,[NoiDung] as SNoiDung
                          ,[iLoai] as SLoai
                          ,[iThangQuyLoai] as IThangQuyLoai
                          ,[iThangQuy] as IThangQuy
                          ,[iThangQuy_MoTa] as SThangQuyMoTa
                          ,[TuChi] as FTongTuChi
                          ,[HienVat] as FTongHienVat
                          ,[NamNganSach] as INamNganSach
                          ,[NguonNganSach] as IIdMaNguonNganSach
                          ,[NamLamViec] as INamLamViec
                          ,[iTrangThai]
                          ,[DateCreated] as DNgayTao
                          ,[UserCreator] as SNguoiTao
                          ,[DateModified] as DNgaySua
                          ,[UserModifier] as SNguoiSua
                          ,[Tag]
                          ,[Log]
                          ,[IsLocked] as BKhoa
                      FROM [BK_ChungTu] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsBkChungTu>(attachDBFile, sql);
        }

        public List<NsBkChungTuChiTiet> GetListNsBkChungTuChiTiet(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                          ,[Id_ChungTu] as IIdBkchungTu
                          ,[MLNS_Id] as IIdMlns
                          ,[MLNS_Id_Parent] as IIdMlnsCha
                          ,[XauNoiMa] as SXauNoiMa
                          ,[LNS] as SLns
                          ,[L] as SL
                          ,[K] as SK
                          ,[M] as SM
                          ,[TM] as STm
                          ,[TTM] as STtm
                          ,[NG] as SNg
                          ,[TNG] as STng
                          ,[MoTa] as SMoTa
                          ,[Chuong]
                          ,[bHangCha] as BHangCha
                          ,[NamNganSach] as INamNganSach
                          ,[NguonNganSach] as IIdMaNguonNganSach
                          ,[NamLamViec] as INamLamViec
                          ,[iTrangThai]
                          ,[iLoai] as SLoai
                          ,[iThangQuyLoai] as IThangQuyLoai
                          ,[iThangQuy] as IThangQuy
                          ,[Id_DonVi] as IIdMaDonVi
                          ,[TenDonVi]
                          ,[Id_PhongBan]
                          ,[Id_PhongBanDich]
                          ,[TuChi] as FTongTuChi
                          ,[HienVat] as FTongHienVat
                          ,[SoChungTu] as SSoChungTu
                          ,[NgayChungTu] as DNgayChungTu
                          ,[GhiChu] as SGhiChu
                          ,[DateCreated] as DNgayTao
                          ,[UserCreator] as SNguoiTao
                          ,[DateModified] as DNgaySua
                          ,[UserModifier] as SNguoiSua
                          ,[Tag]
                          ,[Log]
                      FROM [BK_ChungTuChiTiet] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsBkChungTuChiTiet>(attachDBFile, sql);
        }

        public List<NsCpChungTu> GetListNsCpChungTu(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                          ,[SoChungTu] as SSoChungTu
                          ,[SoChungTuIndex] as ISoChungTuIndex
                          ,[NgayChungTu] as DNgayChungTu
                          ,[SoQuyetDinh] as SSoQuyetDinh
                          ,[NgayQuyetDinh] as DNgayQuyetDinh
                          ,[MoTa] as SMoTa
                          ,[Id_DonVi] as SDsidMaDonVi
                          ,[TenDonVi]
                          ,[LNS] as SDslns
                          ,[iType]
                          ,[iType_MoTa] as ITypeMoTa
                          --------,[iLoai] as ILoai
                          ,[iLoai_MoTa]
                          ,[NamLamViec] as INamLamViec
                          ,[iTrangThai]
                          ,[DateCreated] as DNgayTao
                          ,[UserCreator] as SNguoiTao
                          ,[DateModified] as DNgaySua
                          ,[UserModifier] as SNguoiSua
                          ,[Tag]
                          ,[Log]
                          ,[IsLocked] as BKhoa
                          ,[NguonNganSach] as IIdMaNguonNganSach
                          --,[iPhanCap]
                      FROM [CP_ChungTu] where namlamviec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsCpChungTu>(attachDBFile, sql);
        }

        public List<NsCpChungTuChiTiet> GetListNsCpChungTuChiTiet(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                          ,[Id_ChungTu] as IIdCtcapPhat
                          ,[MLNS_Id] as IIdMlns
                          ,[MLNS_Id_Parent] as IIdParentCha
                          ,[XauNoiMa] as SXauNoiMa
                          ,[LNS] as SLns
                          ,[L] as SL
                          ,[K] as SK
                          ,[M] as SM
                          ,[TM] as STm
                          ,[TTM] as STtm
                          ,[NG] as SNg
                          ,[TNG] as STng
                          ,[MoTa] as SMoTa
                          ,[Chuong] as SChuong
                          ,[NamLamViec] as INamLamViec
                          ,[bHangCha] as BHangCha
                          ,[iTrangThai]
                          ,[iLoai] as ILoai
                          ,[Id_DonVi] as IIdMaDonVi
                          ,[TenDonVi] as STenDonVi
                          ,[Id_PhongBan]
                          ,[Id_PhongBanDich]
                          ,[TuChi] as FTuChi
                          ,[HienVat] as FHienVat
                          ,[GhiChu] as SGhiChu
                          ,[DateCreated] as DNgayTao
                          ,[UserCreator] as SNguoiTao
                          ,[DateModified] as DNgaySua
                          ,[UserModifier] as SNguoiSua
                          ,[Tag]
                          ,[Log]
                          ,[NguonNganSach] as IIdMaNguonNganSach
                          --,[iPhanCap]
                      FROM [CP_ChungTuChiTiet] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsCpChungTuChiTiet>(attachDBFile, sql);
        }

        public List<DanhMuc> GetListDanhMuc(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT NEWID() as Id
                          ,[Type] as SType
                          ,[Id_Code] as IIDMaDanhMuc
                          ,[Ten] as STen
                          ,[Value] as SGiaTri
                          ,[MoTa] as SMoTa
                          ,[OrderIndex] as IThuTu
                          ,[NamLamViec] as INamLamViec
                          ,[iTrangThai] as ITrangThai
                          ,[DateCreated] as DNgayTao
                          ,[UserCreator] as SNguoiTao
                          ,[DateModified] as DNgaySua
                          ,[UserModifier] as SNguoiSua
                          ,[Tag]
                          ,[Log]
                        FROM [DanhMuc] where type in ('NS_NamNganSach','NS_NguonNganSach')
						or (type= 'NS_NamLamViec' and TRY_CONVERT(int, value) is not null
						and TRY_CONVERT(int, value) < " + NAM_LAM_VIEC + " AND TRY_CONVERT(int, value) <> " + namLamViec + ")";
            return GetListData<DanhMuc>(attachDBFile, sql);
        }

        public List<DmChuKy> GetListDanhMucChuKy(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                          ,[Id_Code] as IdCode
                          ,[Id_Type] as IdType
                          ,[Ten] as Ten
                          ,[KyHieu] as KyHieu
                          ,[MoTa] as MoTa
                          ,[TieuDe1] as TieuDe1
                          ,[TieuDe1_MoTa] as TieuDe1MoTa
                          ,[TieuDe2] as TieuDe2
                          ,[TieuDe2_MoTa] as TieuDe2MoTa
                          ,[ChucDanh1] as ChucDanh1
                          ,[ChucDanh1_MoTa] as ChucDanh1MoTa
                          ,[ThuaLenh1] as ThuaLenh1
                          ,[ThuaLenh1_MoTa] as ThuaLenh1MoTa
                          ,[Ten1] as Ten1
                          ,[Ten1_MoTa] as Ten1MoTa
                          ,[ChucDanh2] as ChucDanh2
                          ,[ChucDanh2_MoTa] as ChucDanh2MoTa
                          ,[ThuaLenh2] as ThuaLenh2
                          ,[ThuaLenh2_MoTa] as ThuaLenh2MoTa
                          ,[Ten2] as Ten2
                          ,[Ten2_MoTa] as Ten2MoTa
                          ,[ChucDanh3] as ChucDanh3
                          ,[ChucDanh3_MoTa] as ChucDanh3MoTa
                          ,[ThuaLenh3] as ThuaLenh3
                          ,[ThuaLenh3_MoTa] as ThuaLenh3MoTa
                          ,[Ten3] as Ten3
                          ,[Ten3_MoTa] as Ten3MoTa
                          ,[iTrangThai] as ITrangThai
                          ,[DateCreated] as DateCreated
                          ,[UserCreator] as UserCreator
                          ,[DateModified] as DateModified
                          ,[UserModifier] as UserModifier
                          ,[Tag]
                          ,[Log]
                      FROM [DM_ChuKy]";
            return GetListData<DmChuKy>(attachDBFile, sql);
        }

        public List<NsDtChungTu> GetListDtChungTu(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                          ,[SoChungTu] as SSoChungTu
                          ,[SoChungTuIndex] as ISoChungTuIndex
                          ,[NgayChungTu] as DNgayChungTu
                          ,[SoQuyetDinh] as SSoQuyetDinh
                          ,[NgayQuyetDinh] as DNgayQuyetDinh
                          ,[MoTa] as SMoTa
                          ,[Id_DonVi] as SDsidMaDonVi
                          ,[TenDonVi]
                          ,[LNS] as SDslns
                          ,[iLoai] as ILoai
                          ,[NamNganSach] as INamNganSach
                          ,[NguonNganSach] as IIdMaNguonNganSach
                          ,[NamLamViec] as INamLamViec
                          ,[iTrangThai]
                          ,[DateCreated] as DNgayTao
                          ,[UserCreator] as SNguoiTao
                          ,[DateModified] as DNgaySua
                          ,[UserModifier] as SNguoiSua
                          ,[Tag]
                          ,[Log]
                          ,[TuChiSum] as FTongTuChi
                          ,[HienVatSum] as FTongHienVat
                          ,[IsLocked] as BKhoa
                          ,[GhiChu]
                          ,[iDot] as ILoaiDuToan
                        FROM [DT_ChungTu] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsDtChungTu>(attachDBFile, sql);
        }

        public List<NsDtChungTuChiTiet> GetListDtChungTuChiTiet(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                          ,[Id_ChungTu] as IIdDtchungTu
                          ,[MLNS_Id] as IIdMlns
                          ,[MLNS_Id_Parent] as IIdMlnsCha
                          ,[XauNoiMa] as SXauNoiMa
                          ,[LNS] as SLns
                          ,[L] as SL
                          ,[K] as SK
                          ,[M] as SM
                          ,[TM] as STm
                          ,[TTM] as STtm
                          ,[NG] as SNg
                          ,[TNG] as STng
                          ,[MoTa] as SMoTa
                          ,[Chuong]
                          ,[NamNganSach] as INamNganSach
                          ,[NguonNganSach] as IIdMaNguonNganSach
                          ,[NamLamViec] as INamLamViec
                          ,[bHangCha] as BHangCha
                          ,[iTrangThai]
                          ,[iPhanCap]
                          ,[Id_DonVi] as IIdMaDonVi
                          ,[TenDonVi]
                          ,[Id_PhongBan]
                          ,[Id_PhongBanDich]
                          ,[TuChi] as FTuChi
                          ,[HienVat] as FHienVat
                          ,[HangNhap] as FHangNhap
                          ,[HangMua] as FHangMua
                          ,[TonKho] as FTonKho
                          ,[PhanCap] as FPhanCap
                          ,[DuPhong] as FDuPhong
                          ,[GhiChu] as SGhiChu
                          ,[DateCreated] as DNgayTao
                          ,[UserCreator] as SNguoiTao
                          ,[DateModified] as DNgaySua
                          ,[UserModifier] as SNguoiSua
                          ,[Tag]
                          ,[Log]
                        FROM [DT_ChungTuChiTiet] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsDtChungTuChiTiet>(attachDBFile, sql);
        }

        public List<DonVi> GetListDonVi(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id]
                          ,[Id_Parent] as IdParent
                          ,[Id_DonVi] as IIDMaDonVi
                          ,[TenDonVi] as TenDonVi
                          ,[KyHieu] as KyHieu
                          ,[MoTa] as MoTa
                          ,[Loai] as Loai
                          ,[NamLamViec] as NamLamViec
                          ,[iTrangThai] as ITrangThai
                          ,[DateCreated] as DNgayTao
                          ,[UserCreator] as SNguoiTao
                          ,[DateModified] as DNgaySua
                          ,[UserModifier] as SNguoiSua
                          ,[Tag]
                          ,[Log]
                        FROM [NS_DonVi] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<DonVi>(attachDBFile, sql);
        }

        public List<NsMucLucNganSach> GetListMlns(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                          ,[MLNS_Id] as MlnsId
                          ,[MLNS_Id_Parent] as MlnsIdParent
                          ,[XauNoiMa] as XauNoiMa
                          ,[LNS] as Lns
                          ,[L] as L
                          ,[K] as K
                          ,[M] as M
                          ,[TM] as Tm
                          ,[TTM] as Ttm
                          ,[NG] as Ng
                          ,[TNG] as Tng
                          ,[MoTa] as MoTa
                          ,[Chuong]
                          ,[NamLamViec] as NamLamViec
                          ,[bHangCha] as BHangCha
                          ,[iTrangThai] as ITrangThai
                          ,[Id_PhongBan] as IdPhongBan
                          ,[DateCreated] as DNgayTao
                          ,[UserCreator] as SNguoiTao
                          ,[DateModified] as DNgaySua
                          ,[UserModifier] as SNguoiSua
                          ,[Tag]
                          ,[Log]
                          ,[iLock] as ILock
                          ,[MucAn]
                        FROM [NS_MucLucNganSach] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec + " order by XauNoiMa";
            return GetListData<NsMucLucNganSach>(attachDBFile, sql);
        }

        public List<NsQsChungTu> GetListNsQsChungTu(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                          ,[SoChungTu] as SSoChungTu
                          ,[SoChungTuIndex] as ISoChungTuIndex
                          ,[NgayChungTu] as DNgayChungTu
                          ,[SoQuyetDinh] as SSoQuyetDinh
                          ,[NgayQuyetDinh] as DNgayQuyetDinh
                          ,[MoTa] as SMoTa
                          ,[Id_DonVi] as IIdMaDonVi
                          ,[TenDonVi]
                          ,[Id_PhongBan]
                          ,[iLoai]
                          ,[iThangQuy] as IThangQuy
                          ,[iTrangThai]
                          ,[NamLamViec] as INamLamViec
                          ,[DateCreated] as DNgayTao
                          ,[UserCreator] as SNguoiTao
                          ,[DateModified] as DNgaySua
                          ,[UserModifier] as SNguoiSua
                          ,[Tag]
                          ,[Log]
                          ,[IsLocked] as BKhoa
                      FROM [QS_ChungTu] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsQsChungTu>(attachDBFile, sql);
        }

        public List<NsQsChungTuChiTiet> GetListNsQsChungTuChiTiet(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                          ,[Id_ChungTu] as IIdQschungTu
                          ,[MLNS_Id] as IIdMlns
                          ,[MLNS_Id_Parent] as IIdMlnsCha
                          ,[XauNoiMa] as SKyHieu
                          ,[MoTa] as SMoTa
                          ,[bHangCha] as BHangCha
                          ,[iLoai]
                          ,[iThangQuy] as IThangQuy
                          ,[iQuy]
                          ,[Id_DonVi] as IIdMaDonVi
                          ,[TenDonVi]
                          ,[Id_PhongBan]
                          ,[Id_PhongBanDich]
                          ,[rThieuUy] as FSoThieuUy
                          ,[rTrungUy] as FSoTrungUy
                          ,[rThuongUy] as FSoThuongUy
                          ,[rDaiUy] as FSoDaiUy
                          ,[rThieuTa] as FSoThieuTa
                          ,[rTrungTa] as FSoTrungTa
                          ,[rThuongTa] as FSoThuongTa
                          ,[rDaiTa] as FSoDaiTa
                          ,[rTuong] as FSoTuong
                          ,[rTSQ] as FSoTsq
                          ,[rBinhNhi] as FSoBinhNhi
                          ,[rBinhNhat] as FSoBinhNhat
                          ,[rHaSi] as FSoHaSi
                          ,[rTrungSi] as FSoTrungSi
                          ,[rThuongSi] as FSoThuongSi
                          ,[rQNCN] as FSoQncn
                          ,[rCNVQP] as FSoCnvqp
                          ,[rLDHD] as FSoLdhd
                          ,[rCNVQPCT] as FSoCnvqpct
                          ,[rQNVQPHD] as FSoQnvqphd
                          ,[rTongSo] as FTongSo
                          ,[rSQ_KH] as FSoSqKh
                          ,[rHSQBS_KH] as FSoHsqbsKh
                          ,[rCNVQP_KH] as FSoCnvqpKh
                          ,[rLDHD_KH] as FSoLdhdKh
                          ,[rQNCN_KH] as FSoQncnKh
                          ,[GhiChu] as SGhiChu
                          ,[iSTT]
                          ,[iTrangThai]
                          ,[NamLamViec] as INamLamViec
                          ,[DateCreated] as DNgayTao
                          ,[UserCreator] as SNguoiTao
                          ,[DateModified] as DNgaySua
                          ,[UserModifier] as SNgaySua
                          ,[Tag]
                          ,[Log]
                          ,[rVCQP] as FSoVcqp
                          ,[rCY_H] as FSoCyH
                          ,[rCY_KT] as FSoCyKt
                      FROM [QS_ChungTuChiTiet] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsQsChungTuChiTiet>(attachDBFile, sql);
        }

        public List<NsQsMucLuc> GetListNsQsMucluc(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                          ,[MLNS_Id] as IIdMlns
                          ,[MLNS_Id_Parent] as IIdMlnsCha
                          ,[M] as SM
                          ,[TM] as STm
                          ,[XauNoiMa] as SKyHieu
                          ,[MoTa] as SMoTa
                          ,[iSTT] as IThuTu
                          ,[bHienThi] as SHienThi
                          ,[bHangCha] as BHangCha
                          ,[iTrangThai] as ITrangThai
                          ,[NamLamViec] as INamLamViec
                      FROM [QS_MucLuc] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsQsMucLuc>(attachDBFile, sql);
        }

        public List<NsQtChungTu> GetListNsQtChungTu(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                          ,[SoChungTu] as SSoChungTu
                          ,[SoChungTuIndex] as ISoChungTuIndex
                          ,[NgayChungTu] as DNgayChungTu
                          ,[SoQuyetDinh]
                          ,[NgayQuyetDinh]
                          ,[MoTa] as SMoTa
                          ,[Id_DonVi] as IIdMaDonVi
                          ,[TenDonVi]
                          ,[Id_PhongBan]
                          ,[LNS] as SDslns
                          ,[iLoai] as SLoai
                          ,[iThangQuyLoai] as IThangQuyLoai
                          ,[iThangQuy] as IThangQuy
                          ,[iThangQuy_MoTa] as SThangQuyMoTa
                          ,[iTuChi]
                          ,[iTuChi_MoTa]
                          ,[NamNganSach] as INamNganSach
                          ,[NguonNganSach] as IIdMaNguonNganSach
                          ,[NamLamViec] as INamLamViec
                          ,[iTrangThai]
                          ,[DateCreated] as DNgayTao
                          ,[UserCreator] as SNguoiTao
                          ,[DateModified] as DNgaySua
                          ,[UserModifier] as SNguoiSua
                          ,[Tag]
                          ,[Log]
                          ,[TuChiSum] as FTongTuChiPheDuyet
                          ,[HienVatSum]
                          ,[IsLocked] as BKhoa
                      FROM [QT_ChungTu] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsQtChungTu>(attachDBFile, sql);
        }

        public List<NsQtChungTuChiTiet> GetListNsQtChungTuChiTiet(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                              ,[Id_ChungTu] as IIdQtchungTu
                              ,[MLNS_Id] as IIdMlns
                              ,[MLNS_Id_Parent] as IIdMlnsCha
                              ,[XauNoiMa] as SXauNoiMa
                              ,[LNS] as SLns
                              ,[L] as SL
                              ,[K] as SK
                              ,[M] as SM
                              ,[TM] as STm
                              ,[TTM] as STtm
                              ,[NG] as SNg
                              ,[TNG] as STng
                              ,[MoTa] as SMoTa
                              ,[Chuong]
                              ,[bHangCha] as BHangCha
                              ,[NamNganSach] as INamNganSach
                              ,[NguonNganSach] as IIdMaNguonNganSach
                              ,[NamLamViec] as INamLamViec
                              ,[iTrangThai]
                              ,[iLoai]
                              ,[iThangQuyLoai] as IThangQuyLoai
                              ,[iThangQuy] as IThangQuy
                              ,[Id_DonVi] as IIdMaDonVi
                              ,[TenDonVi] 
                              ,[Id_PhongBan]
                              ,[Id_PhongBanDich]
                              ,[TuChi] as FTuChiPheDuyet
                              ,[TuChi] as FTuChiDeNghi
                              ,[HienVat]
                              ,[SoNguoi] as FSoNguoi
                              ,[SoNgay] as FSoNgay
                              ,[SoLuot] as FSoLuot
                              ,[MucAn]
                              ,[GhiChu] as SGhiChu
                              ,[DateCreated] as DNgayTao
                              ,[UserCreator] as SNguoiTao
                              ,[DateModified] as DNgaySua
                              ,[UserModifier] as SNguoiSua
                              ,[Tag]
                              ,[Log]
                          FROM [QT_ChungTuChiTiet] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsQtChungTuChiTiet>(attachDBFile, sql);
        }

        public List<NsQtChungTuChiTietGiaiThich> GetListNsQtChungTuChiTietGiaiThich(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                              ,[Id_ChungTu] as IIdQtchungTu
                              ,[Id_GiaiThich] as IIdGiaiThich
                              ,[iThangQuy] as IThangQuy
                              ,[iThangQuyLoai] as IThangQuyLoai	
                              ,[Id_DonVi] as IIdMaDonVi
                              ,[TenDonVi]
                              ,[MoTa_TinhHinh] as SMoTaTinhHinh
                              ,[MoTa_KienNghi] as SMoTaKienNghi
                              ,[MoTa] as SMoTa
                              ,[Luong_SiQuan] as FLuongSiQuan
                              ,[Luong_SiQuan_Tru] as FLuongSiQuanTru
                              ,[Luong_SiQuan_QT] as FLuongSiQuanQt
                              ,[Luong_QNCN] as FLuongQncn
                              ,[Luong_QNCN_Tru] as FLuongQncnTru
                              ,[Luong_QNCN_QT] as FLuongQncnQt
                              ,[Luong_CNVQP] as FLuongCnvqp
                              ,[Luong_CNVQP_Tru] as FLuongCnvqpTru
                              ,[Luong_CNVQP_QT] as FLuongCnvqpQt
                              ,[Luong_HD] as FLuongHd
                              ,[Luong_HD_Tru] as FLuongHdTru
                              ,[Luong_HD_QT] as FLuongHdQt
                              ,[PhuCap_SiQuan] as FPhuCapSiQuan
                              ,[PhuCap_SiQuan_Tru] as FPhuCapSiQuanTru
                              ,[PhuCap_SiQuan_QT] as FPhuCapSiQuanQt
                              ,[PhuCap_QNCN] as FPhuCapQncn
                              ,[PhuCap_QNCN_Tru] as FPhuCapQncnTru
                              ,[PhuCap_QNCN_QT] as FPhuCapQncnQt
                              ,[PhuCap_CNVQP] as FPhuCapCnvqp
                              ,[PhuCap_CNVQP_Tru] as FPhuCapCnvqpTru
                              ,[PhuCap_CNVQP_QT] as FPhuCapCnvqpQt
                              ,[PhuCap_HD] as FPhuCapHd
                              ,[PhuCap_HD_Tru] as FPhuCapHdTru
                              ,[PhuCap_HD_QT] as FPhuCapHdQt
                              ,[NgayAn] as FNgayAn
                              ,[NgayAn_Cong] as FNgayAnCong
                              ,[NgayAn_Tru] as FNgayAnTru
                              ,[NgayAn_QT] as FNgayAnQt
                              ,[RaQuan_SiQuan_Nguoi_XuatNgu] as FRaQuanSiQuanNguoiXuatNgu
                              ,[RaQuan_SiQuan_Tien_XuatNgu] as FRaQuanSiQuanTienXuatNgu
                              ,[RaQuan_SiQuan_Nguoi_Huu] as FRaQuanSiQuanNguoiHuu
                              ,[RaQuan_SiQuan_Tien_Huu] as FRaQuanSiQuanTienHuu
                              ,[RaQuan_SiQuan_Nguoi_ThoiViec] as FRaQuanSiQuanNguoiThoiViec
                              ,[RaQuan_SiQuan_Tien_ThoiViec] as FRaQuanSiQuanTienThoiViec
                              ,[RaQuan_QNCN_Nguoi_XuatNgu] as FRaQuanQncnNguoiXuatNgu
                              ,[RaQuan_QNCN_Tien_XuatNgu] as FRaQuanQncnTienXuatNgu
                              ,[RaQuan_QNCN_Nguoi_Huu] as FRaQuanQncnNguoiHuu
                              ,[RaQuan_QNCN_Tien_Huu] as FRaQuanQncnTienHuu
                              ,[RaQuan_QNCN_Nguoi_ThoiViec] as FRaQuanQncnNguoiThoiViec
                              ,[RaQuan_QNCN_Tien_ThoiViec] as FRaQuanQncnTienThoiViec
                              ,[RaQuan_CNVQP_Nguoi_XuatNgu] as FRaQuanCnvqpNguoiXuatNgu
                              ,[RaQuan_CNVQP_Tien_XuatNgu] as FRaQuanCnvqpTienXuatNgu
                              ,[RaQuan_CNVQP_Nguoi_Huu] as FRaQuanCnvqpNguoiHuu
                              ,[RaQuan_CNVQP_Tien_Huu] as FRaQuanCnvqpTienHuu
                              ,[RaQuan_CNVQP_Nguoi_ThoiViec] as FRaQuanCnvqpNguoiThoiViec
                              ,[RaQuan_CNVQP_Tien_ThoiViec] as FRaQuanCnvqpTienThoiViec
                              ,[RaQuan_HSQCS_Nguoi_XuatNgu] as FRaQuanHsqcsNguoiXuatNgu
                              ,[RaQuan_HSQCS_Tien_XuatNgu] as FRaQuanHsqcsTienXuatNgu
                              ,[RaQuan_HSQCS_Nguoi_Huu] as FRaQuanHsqcsNguoiHuu
                              ,[RaQuan_HSQCS_Tien_Huu] as FRaQuanHsqcsTienHuu
                              ,[RaQuan_HSQCS_Nguoi_ThoiViec] as FRaQuanHsqcsNguoiThoiViec
                              ,[RaQuan_HSQCS_Tien_ThoiViec] as FRaQuanHsqcsTienThoiViec
                              ,[iTrangThai]
                              ,[NamLamViec] as INamLamViec
                              ,[DateCreated] as DNgayTao
                              ,[UserCreator] as SNguoiTao
                              ,[DateModified] as DNgaySua
                              ,[UserModifier] as SNgaySua
                              ,[Tag]
                              ,[Log]
                          FROM [QT_ChungTuChiTiet_GiaiThich] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsQtChungTuChiTietGiaiThich>(attachDBFile, sql);
        }

        public List<NsQtChungTuChiTietGiaiThichLuongTru> GetListNsQtChungTuChiTietGiaiThichLuongTru(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                          ,[Id_ChungTu] as IIdQtchungTu
                          ,[Id_GiaiThich] as IIdGiaiThich
                          ,[iThangQuy] as IThangQuy
                          ,[iThangQuyLoai] as IThangQuyLoai
                          ,[Id_DonVi] as IIdMaDonVi
                          ,[TenDonVi]
                          ,[MoTa] as SMoTa
                          ,[Id_DoiTuong] as IIdDoiTuong
                          ,[HoTen] as SHoTen
                          ,[LuongThang] as FLuongThang
                          ,[NgayNghi] as FNgayNghi
                          ,[SoNguoi] as FSoNguoi
                          ,[LuongCapBac] as FLuongCapBac
                          ,[LuongThamNien] as FLuongThamNien
                          ,[LuongPhuCapCongVu] as FLuongPhuCapCongVu
                          ,[LuongPhuCapKhacBH] as FLuongPhuCapKhacBh
                          ,[LuongPhuCapKhac] as FLuongPhuCapKhac
                          ,[Tong_BaoHiem] as FTongBaoHiem
                          ,[iTrangThai]
                          ,[NamLamViec] as INamLamViec
                          ,[DateCreated] as DNgayTao
                          ,[UserCreator] as SNguoiTao
                          ,[DateModified] as DNgaySua
                          ,[UserModifier] as SNguoiSua
                          ,[Tag]
                          ,[Log]
                          ,[iStatus] as IStatus
                      FROM [QT_ChungTuChiTiet_GiaiThich_LuongTru] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsQtChungTuChiTietGiaiThichLuongTru>(attachDBFile, sql);
        }

        public List<NsSktChungTu> GetListSktChungTu(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                          ,[Id_PhongBan]
                          ,[TenPhongBan]
                          ,[Id_DonVi] as IIdMaDonVi
                          ,[TenDonVi]
                          ,[SoChungTu] as SSoChungTu
                          ,[NgayChungTu] as DNgayChungTu
                          ,[SoQuyetDinh] as SSoQuyetDinh
                          ,[NgayQuyetDinh] as DNgayQuyetDinh
                          ,[MoTa] as SMoTa
                          ,[iLoai] as ILoai
                          ,[IsLocked] as BKhoa
                          ,[iTrangThai]
                          ,[NamLamViec] as INamLamViec
                          ,[DateCreated] as DNgayTao
                          ,[UserCreator] as SNguoiTao
                          ,[DateModified] as DNgaySua
                          ,[UserModifier] as SNguoiSua
                          ,[Tag]
                          ,[Log] FROM [SKT_ChungTu] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsSktChungTu>(attachDBFile, sql);
        }

        public List<NsSktChungTuChiTiet> GetListSktChungTuChiTiets(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT a.[Id] as Id
                              ,[Id_ChungTu] as IIdCtsoKiemTra
                              ,[Id_DonVi] as IIdMaDonVi
                              ,[TenDonVi]
                              ,b.[ID_MucLuc] as IIdMlskt
                              ,b.[XauNoiMa] as SKyHieu
                              ,b.[M]
                              ,b.[TM]
                              ,b.[NG]
                              ,b.[NK]
                              ,b.[NC]
                              ,b.[MoTa] as SMoTa
                              ,[TonKho_DV]
                              ,[HuyDong_DV]
                              ,[TuChi_DV]
                              ,[MuaHang_DV]
                              ,[PhanCap_DV]
                              ,[TonKho]
                              ,[HuyDong]
                              ,[TuChi] as FTuChi
                              ,[HangMua]
                              ,[PhanCap]
                              ,[GhiChu] as SGhiChu
                              ,[iLoai]
                              ,a.[iTrangThai]
                              ,a.[NamLamViec] as INamLamViec
                              ,a.[DateCreated] as DNgayTao
                              ,a.[UserCreator] as SNguoiTao
                              ,a.[DateModified] as DNgaySua
                              ,a.[UserModifier] as SNguoiSua
                              ,a.[Tag]
                              ,a.[Log]
                              ,[QuyetToan]
                              ,[DuToan]
                              ,[HienVat]
                              ,[HangNhap]
                              ,[DuToan1]
                              ,[DuToan2]
                              ,[DuToan3]
                              ,[QuyetToan1]
                              ,[QuyetToan2]
                              ,[QuyetToan3]
                              ,[SoKiemTra1]
                              ,[SoKiemTra2]
                              ,[SoKiemTra3]
                              ,[NhuCau1]
                              ,[NhuCau2]
                              ,[NhuCau3]
                          FROM [SKT_ChungTuChiTiet] a
						  inner join SKT_MucLuc b
						  on a.XauNoiMa = b.XauNoiMa
						  and a.NamLamViec = b.NamLamViec where a.NamLamViec < " + NAM_LAM_VIEC + " AND a.NamLamViec <> " + namLamViec;
            return GetListData<NsSktChungTuChiTiet>(attachDBFile, sql);
        }

        public List<NsSktMucLuc> GetListSktMucLuc(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                            ,[ID_MucLuc] as IIDMLSKT
                            ,[KyHieu]
                            ,[XauNoiMa] as SKyHieu
                            ,[M] as SM
                            ,[TM]
                            ,[NG] as SNg
                            ,[NK]
                            ,[NC]
                            ,'' as SNGCha
                            ,[STT] as SSTT
                            ,[MoTa] as SMoTa
                            ,[bHangCha] as BHangCha
                            ,[iTrangThai] as ITrangThai
                            ,[NamLamViec] as INamLamViec
                            ,[DateCreated] as DNgayTao
                            ,[UserCreator] as DNguoiTao
                            ,[DateModified] as DNgaySua
                            ,[UserModifier] as DNguoiSua
                            ,[Tag]
                            ,[Log]
                            ,[Muc]
                            ,[LNS]
                            ,[iLock]
                        FROM [SKT_MucLuc] where NamLamViec < " + NAM_LAM_VIEC + " order by XauNoiMa";
            return GetListData<NsSktMucLuc>(attachDBFile, sql);
        }

        public List<NsMlsktMlns> GetListMapMLNSSKT(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT newId() as Id
                            ,[SKT_XauNoiMa]  
                            ,[SKT_MoTa]
                            ,[NS_XauNoiMa] as SNsXauNoiMa
                            ,[NS_MoTa]
                            ,t1.[NamLamViec] as INamLamViec
                            ,t1.[DateCreated]
                            ,t1.[UserCreator]
                            ,t1.[DateModified]
                            ,t1.[UserModifier]
                            ,t1.[Tag]
                            ,t1.[Log]
                            ,t1.[iTrangThai] as ITrangThai, T1.SKT_XauNoiMa as SSktKyHieu
                            FROM [SKT_MucLuc_Map] T1 where T1.SKT_XauNoiMa is not null and T1.SKT_XauNoiMa != '' and T1.NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsMlsktMlns>(attachDBFile, sql);
        }

        public List<NsMucLucNganSach> GetListExistMlns()
        {
            List<NsMucLucNganSach> result = new List<NsMucLucNganSach>();
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucNganSaches.ToList();
            }
        }

        public List<NsDtdauNamChungTuChiTiet> GetListDtDauNamCTCT(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id] as Id
                              ,[XauNoiMa] as SXauNoiMa
                              ,[LNS] as SLns
                              ,[L] as SL
                              ,[K] as SK
                              ,[M] as SM
                              ,[TM] as STm
                              ,[TTM] as STtm
                              ,[NG] as SNg
                              ,[TNG] as STng
                              ,[MoTa] as SMoTa
                              ,[Chuong] as SChuong
                              ,[NamNganSach] as INamNganSach
                              ,[NguonNganSach] as IIdMaNguonNganSach
                              ,[NamLamViec] as INamLamViec
                              ,[bHangCha]  as BHangCha
                              ,[iLoai] as iLoai
                              ,[iTrangThai]
                              ,[Id_DonVi] as IIdMaDonVi
                              ,[TenDonVi] as STenDonVi
                              ,[TuChi] as FTuChi
                              ,[HienVat] as FHienVat
                              ,[HangNhap] as FHangNhap
                              ,[HangMua] as FHangMua
                              ,[TonKho]
                              ,[PhanCap] as FPhanCap
                              ,[DuPhong] as FDuPhong
                              ,[GhiChu] as SGhiChu
                              ,[DateCreated] as DNgayTao
                              ,[UserCreator] as SNguoiTao
                              ,[DateModified] as DNgaySua
                              ,[UserModifier] as SNguoiSua
                              ,[Tag]
                              ,[Log]
                          FROM [SKT_SoLieuChiTiet] where NamLamViec < " + NAM_LAM_VIEC + " AND NamLamViec <> " + namLamViec;
            return GetListData<NsDtdauNamChungTuChiTiet>(attachDBFile, sql);
        }

        public List<NsMucLucNganSach> GetListMissingMlnsByNamLamViec(string attachDBFile, int namLamViec)
        {
            string sql = $@"SELECT distinct XauNoiMa, LNS,L,K,M,TM,TTM,NG,TNG,NamLamViec from 
                            (SELECT XauNoiMa, LNS,L,K,M,TM,TTM,NG,TNG,NamLamViec
                              FROM [SKT_SoLieuChiTiet]
                              where NamLamViec = {namLamViec} and xaunoima not in (select xaunoima from NS_MucLucNganSach where NamLamViec = {namLamViec})
                              union
                              SELECT XauNoiMa, LNS,L,K,M,TM,TTM,NG,TNG,NamLamViec
                              FROM DT_ChungTuChiTiet
                              where NamLamViec = {namLamViec} and xaunoima not in (select xaunoima from NS_MucLucNganSach where NamLamViec = {namLamViec})
                              union
                              SELECT XauNoiMa, LNS,L,K,M,TM,TTM,NG,TNG,NamLamViec
                              FROM QT_ChungTuChiTiet
                              where NamLamViec = {namLamViec} and xaunoima not in (select xaunoima from NS_MucLucNganSach where NamLamViec = {namLamViec})
                              union
                                SELECT XauNoiMa, LNS,L,K,M,TM,TTM,NG,TNG,NamLamViec
                              FROM CP_ChungTuChiTiet
                              where NamLamViec = {namLamViec} and xaunoima not in (select xaunoima from NS_MucLucNganSach where NamLamViec = {namLamViec})
                              union
                                SELECT XauNoiMa, LNS,L,K,M,TM,TTM,NG,TNG,NamLamViec
                              FROM BK_ChungTuChiTiet
                              where NamLamViec = {namLamViec} and xaunoima not in (select xaunoima from NS_MucLucNganSach where NamLamViec = {namLamViec}) ) t1";
            return GetListData<NsMucLucNganSach>(attachDBFile, sql);
        }

        private void UpdateChiTietToi(int namLamViec)
        {
            string sql = $@"with tmp as
                            (
                              select * from NS_MucLucNganSach where iNamLamViec = {namLamViec}
                              and sL = '' and sK = '' and sM = '' and sTM = '' and sTTM = '' and sNG = '' and sTNG = ''
                            )
                            update NS_MucLucNganSach set sCpchitiettoi = 'NG' where iid in
                              (select iid from tmp
                              where(select count(*) from tmp t1 where t1.inamlamviec = tmp.inamlamviec and t1.iID_MLNS_Cha = tmp.iID_MLNS) = 0);";

            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand(sql);
            }
        }

        public List<DanhMuc> GetListDanhmucChuKyTen(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT tbl.ten1 as IIDMaDanhMuc, dm.Value as SGiaTri, 'DM_CHU_KY_TEN' AS SType, ISNULL(dm.Value, '') as STen FROM (
                              SELECT ten1
                              FROM [DM_ChuKy]
                              UNION
                              SELECT ten2
                              FROM [DM_ChuKy]
                              UNION
                              SELECT ten3
                              FROM [DM_ChuKy] ) tbl 
                              left join danhmuc dm on tbl.ten1 = dm.Id_Code
                              WHERE ten1 is not null";
            return GetListData<DanhMuc>(attachDBFile, sql);
        }

        public List<DanhMuc> GetListDanhmucChuKyChucDanh(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT tbl.ChucDanh1 as IIDMaDanhMuc, dm.Value as SGiaTri, 'DM_CHU_KY_CHUC_DANH' AS SType, ISNULL(dm.Value, '') as STen FROM (
                              SELECT ChucDanh1
                              FROM [DM_ChuKy]
                              UNION 
                              SELECT ChucDanh2
                              FROM [DM_ChuKy]
                              UNION
                              SELECT ChucDanh3
                              FROM [DM_ChuKy]
                              UNION
                              SELECT ThuaLenh1
                              FROM [DM_ChuKy]
                              UNION 
                              SELECT ThuaLenh2
                              FROM [DM_ChuKy]
                              UNION
                              SELECT ThuaLenh3
                              FROM [DM_ChuKy]) tbl 
                              LEFT JOIN danhmuc dm on tbl.ChucDanh1 = dm.Id_Code 
                              WHERE ChucDanh1 IS NOT NULL";
            return GetListData<DanhMuc>(attachDBFile, sql);
        }

        public List<DmChuKy> GetListDanhmucBaoCao(string attachDBFile, int? namLamViec)
        {
            string sql = @"SELECT [Id]
                              ,[Id_Code] as IdCode
                              ,[Id_Type] as IdType
                              ,[Ten]
                              ,[KyHieu]
                              ,[MoTa]
                              ,[TieuDe1]
                              ,[TieuDe1_MoTa] as TieuDe1MoTa
                              ,[TieuDe2]
                              ,[TieuDe2_MoTa] as TieuDe2MoTa
                              ,[ChucDanh1]
                              ,[ChucDanh1_MoTa] as ChucDanh1MoTa
                              ,[ThuaLenh1] as ThuaLenh1
                              ,[ThuaLenh1_MoTa] as ThuaLenh1MoTa
                              ,[Ten1]
                              ,[Ten1_MoTa] as Ten1MoTa
                              ,[ChucDanh2]
                              ,[ChucDanh2_MoTa] as ChucDanh2MoTa
                              ,[ThuaLenh2]
                              ,[ThuaLenh2_MoTa] as ThuaLenh2MoTa
                              ,[Ten2]
                              ,[Ten2_MoTa] as Ten2MoTa
                              ,[ChucDanh3]
                              ,[ChucDanh3_MoTa] as ChucDanh3MoTa
                              ,[ThuaLenh3]
                              ,[ThuaLenh3_MoTa] as ThuaLenh3MoTa
                              ,[Ten3]
                              ,[Ten3_MoTa] as Ten3MoTa
                              ,[iTrangThai]
                              ,[DateCreated]
                              ,[UserCreator]
                              ,[DateModified]
                              ,[UserModifier]
                              ,[Tag]
                              ,[Log]
                            FROM [DM_ChuKy]";
            return GetListData<DmChuKy>(attachDBFile, sql);
        }

        public void ResetMigrateVersion190()
        {
            string sql = "";
            sql += "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type in (N'U')) ";
            sql += "BEGIN ";
            sql += "IF EXISTS (SELECT * FROM __EFMigrationsHistory WHERE MigrationId ='20220519100924_UpdateVersion_1.9.0.0') ";
            sql += "BEGIN ";
            sql += "DELETE FROM [dbo].[__EFMigrationsHistory] ";
            sql += "INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220520015618_CreateSchema', N'1.1.6') ";
            sql += "INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220520015731_InitDatabase', N'1.1.6') ";
            sql += "END ";
            sql += "END ";

            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand(sql);
            }
        }
    }
}
