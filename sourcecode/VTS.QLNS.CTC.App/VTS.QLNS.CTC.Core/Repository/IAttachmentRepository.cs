using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IAttachmentRepository : IRepository<Attachment>
    {
        IEnumerable<Attachment> FindByModule(int module);
        IEnumerable<Attachment> FindByModuleAndObjectId(int module, Guid objectId);
        IEnumerable<Attachment> FindByIdIn(IEnumerable<Guid> ids);
        int DeleteByObjectIdAndModuleType(Guid objectId, int moduleType);
    }
}
