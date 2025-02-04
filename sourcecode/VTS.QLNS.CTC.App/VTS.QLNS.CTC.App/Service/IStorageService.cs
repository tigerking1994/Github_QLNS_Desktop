using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Service
{
    public interface IStorageService
    {
        void LoadConfig();
        void Upload(AttachmentEnum.Type objectType, Guid objectId, IEnumerable<AttachmentModel> attachmentUploads);
        void Copy(AttachmentEnum.Type objectType, Guid objectId, List<AttachmentModel> attachments);
        void Download(Guid id);
        void Download(Attachment entity);
        void DownloadAll(AttachmentEnum.Type objectType, Guid objectId);
        void DownloadAll(List<Attachment> entities);
        void SaveSingleFile(Attachment item);
        void Remove(List<Guid> ids);
        void View(Guid id);
        bool ValidateMaxlength(params string[] sourceFilePaths);
    }
}
