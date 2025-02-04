//using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.SqlBulkTools;

namespace VTS.QLNS.CTC.Core.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public Repository(ApplicationDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public virtual IEnumerable<TEntity> FindAll()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Set<TEntity>().ToList();
            }
        }

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //return ctx.Set<TEntity>().Where(predicate.Compile()).ToList();
                return ctx.Set<TEntity>().Where(predicate).ToList();
            }
        }

        public TEntity Find(params object[] keyValues)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var entity = ctx.Set<TEntity>().Find(keyValues);
                return entity;
            }
        }

        public int Add(TEntity t)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Set<TEntity>().Add(t);
                return ctx.SaveChanges();
            }
        }

        public int Delete(TEntity t)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Remove<TEntity>(t);
                return ctx.SaveChanges();
            }
        }

        public int Delete(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var entity = ctx.Set<TEntity>().Find(id);
                ctx.Remove<TEntity>(entity);
                return ctx.SaveChanges();
            }
        }

        public int Update(TEntity t)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Update(t);
                return ctx.SaveChanges();
            }
        }

        public int UpdateRange(IEnumerable<TEntity> entities)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.UpdateRange(entities);
                return ctx.SaveChanges();
            }
        }

        public int AddOrUpdate(TEntity entity)
        {
            return AddOrUpdateRange(new[] { entity });
        }

        public int AddOrUpdateRange(IEnumerable<TEntity> entities)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    if (entity.IsDeleted)
                    {
                        if (!entity.Id.Equals(Guid.Empty))
                        {
                            ctx.Remove<TEntity>(entity);
                        }
                    }
                    else if (entity.IsModified)
                    {
                        if (!entity.Id.Equals(Guid.Empty))
                        {
                            ctx.Update(entity);
                        }
                        else
                        {
                            ctx.Set<TEntity>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }

        public int AddRange(IEnumerable<TEntity> entities)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.AddRange(entities);
                return ctx.SaveChanges();
            }
        }

        private Dictionary<string, object> DefaultValueDataTable(List<DbColumnInfo> columns)
        {
            var dataTable = new DataTable();
            string selectDefaultSql = "Select 1 as '1'";
            foreach (var col in columns)
            {
                if (!string.IsNullOrEmpty(col.DefaultValueSql))
                {
                    selectDefaultSql += string.Format(", {0} as {1}", col.DefaultValueSql, col.PropertyName);
                }
            }
            using (var ctx = _contextFactory.CreateDbContext())
            {
                using (SqlConnection sourceConnection =
                   new SqlConnection(ctx.Database.GetDbConnection().ConnectionString))
                {
                    sourceConnection.Open();
                    using (SqlCommand queryCmd = new SqlCommand())
                    {
                        queryCmd.Connection = sourceConnection;
                        queryCmd.CommandText = selectDefaultSql;
                        using (var reader = queryCmd.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
            }
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                string colName = dataTable.Columns[i].ColumnName;
                keyValuePairs.Add(colName, dataTable.Rows[0][colName]);
            }
            return keyValuePairs;
        }

        public void BulkInsert(IEnumerable<TEntity> entities)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //ctx.BulkInsert(entities.ToList());
                var mapping = ctx.Model.FindEntityType(typeof(TEntity)).Relational();
                var schema = mapping.Schema;
                var tableName = mapping.TableName;
                List<DbColumnInfo> columns = ctx.GetDbColums(typeof(TEntity)).ToList();
                Dictionary<string, object> DefaultValueData = DefaultValueDataTable(columns);
                using (SqlConnection sourceConnection =
                   new SqlConnection(ctx.Database.GetDbConnection().ConnectionString))
                {
                    ColumnSelect<TEntity> columnSelect = null;
                    var bulk = new BulkOperations();
                    var x = bulk.Setup<TEntity>(x => x.ForCollection(entities)).WithTable(tableName).WithSqlBulkCopyOptions(SqlBulkCopyOptions.CheckConstraints);
                    foreach (var col in columns)
                    {
                        if (columnSelect == null)
                        {
                            columnSelect = x.AddColumn(col.PropertyName).CustomColumnMapping(col.PropertyName, col.SqlName);
                        }
                        else
                            columnSelect = columnSelect.AddColumn(col.PropertyName).CustomColumnMapping(col.PropertyName, col.SqlName);
                    }
                    if (columnSelect != null)
                    {
                        columnSelect.CustomColumnDefaultValueMapping(DefaultValueData);
                        columnSelect.BulkInsert();
                    }
                    bulk.CommitTransaction(sourceConnection);
                }
            }
        }

        public void BulkUpdate(IEnumerable<TEntity> entities)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //ctx.BulkUpdate(entities.ToList());
                var mapping = ctx.Model.FindEntityType(typeof(TEntity)).Relational();
                var schema = mapping.Schema;
                var tableName = mapping.TableName;
                List<DbColumnInfo> columns = ctx.GetDbColums(typeof(TEntity)).ToList();
                using (SqlConnection sourceConnection =
                   new SqlConnection(ctx.Database.GetDbConnection().ConnectionString))
                {
                    var bulk = new BulkOperations();
                    ColumnSelect<TEntity> columnSelect = null;
                    var x = bulk.Setup<TEntity>(x => x.ForCollection(entities)).WithTable(tableName);
                    foreach (var col in columns)
                    {
                        if (columnSelect == null)
                            columnSelect = x.AddColumn(col.PropertyName).CustomColumnMapping(col.PropertyName, col.SqlName);
                        else
                            columnSelect = columnSelect.AddColumn(col.PropertyName).CustomColumnMapping(col.PropertyName, col.SqlName);
                    }
                    if (columnSelect != null)
                        columnSelect.BulkUpdate().MatchTargetOn(x => x.Id);
                    bulk.CommitTransaction(sourceConnection);
                }
            }
        }

        public int RemoveRange(IEnumerable<TEntity> entities)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.RemoveRange(entities);
                return ctx.SaveChanges();
            }
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Set<TEntity>().SingleOrDefault(predicate);
            }
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Set<TEntity>().FirstOrDefault(predicate);
            }
        }
    }
}
