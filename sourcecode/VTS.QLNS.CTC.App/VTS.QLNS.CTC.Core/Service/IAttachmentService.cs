using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IAttachmentService
    {
        void AddRange(IEnumerable<Attachment> attaches);
        IEnumerable<Attachment> FindByModule(int module);
        Attachment FindById(Guid id);
        IEnumerable<Attachment> FindByModuleAndObjectId(int module, Guid objectId);
        IEnumerable<Attachment> FindByIdIn(IEnumerable<Guid> ids);
        void RemoveAttachments(IEnumerable<Attachment> attachments);
        int DeleteByObjectIdAndModuleType(Guid objectId, int moduleType);
    }
}
