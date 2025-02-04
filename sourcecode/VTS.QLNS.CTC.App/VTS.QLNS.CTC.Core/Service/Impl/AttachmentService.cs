using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _reposirory;

        public AttachmentService(IAttachmentRepository attachRepository)
        {
            _reposirory = attachRepository;
        }

        public IEnumerable<Attachment> FindByModule(int module)
        {
            return _reposirory.FindByModule(module);
        }

        public void AddRange(IEnumerable<Attachment> attaches)
        {
            _reposirory.AddRange(attaches);
        }

        public IEnumerable<Attachment> FindByModuleAndObjectId(int module, Guid objectId)
        {
            return _reposirory.FindByModuleAndObjectId(module, objectId);
        }

        public Attachment FindById(Guid id)
        {
            return _reposirory.Find(id);
        }

        public IEnumerable<Attachment> FindByIdIn(IEnumerable<Guid> ids)
        {
            return _reposirory.FindByIdIn(ids);
        }

        public void RemoveAttachments(IEnumerable<Attachment> attachments)
        {
            _reposirory.RemoveRange(attachments);
        }

        public int DeleteByObjectIdAndModuleType(Guid objectId, int moduleType)
        {
            return _reposirory.DeleteByObjectIdAndModuleType(objectId, moduleType);
        }
    }
}
