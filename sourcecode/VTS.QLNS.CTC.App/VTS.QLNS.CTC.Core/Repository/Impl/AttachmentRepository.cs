using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public AttachmentRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<Attachment> FindByIdIn(IEnumerable<Guid> ids)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Attachments.Where(x => ids.Contains(x.Id)).ToList();
            }
        }

        public IEnumerable<Attachment> FindByModule(int module)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Attachments.Where(x => x.ModuleType == module).ToList();
            }
        }

        public IEnumerable<Attachment> FindByModuleAndObjectId(int module, Guid objectId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Attachments.Where(x => x.ModuleType == module && x.ObjectId == objectId).ToList();
            }
        }

        public int DeleteByObjectIdAndModuleType(Guid objectId, int moduleType)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"DELETE FROM Attachment WHERE ObjectId = @ObjectId and ModuleType = @ModuleType";
                var parameters = new[]{
                    new SqlParameter("@ObjectId", objectId.ToString()),
                    new SqlParameter("@ModuleType", moduleType)
                };
                return ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
    }
}
