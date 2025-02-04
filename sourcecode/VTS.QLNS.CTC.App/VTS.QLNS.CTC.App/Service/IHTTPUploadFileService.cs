using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model;

namespace VTS.QLNS.CTC.App.Service
{
    public interface IHTTPUploadFileService
    {
        Task UploadFile(FileUploadModel fileUpload);
        Task UploadFile(bool isSendHTTP, FileUploadStreamModel fileUpload);
        Task<bool> UploadFileAsync(bool isSendHTTP, FileUploadStreamModel fileUpload);
        Task<(int, string)> GetToken(bool isSendHTTP);
        Task<MemoryStream> DownloadFile(Guid id);
        Task<MemoryStream> DownloadDecryptFile(bool isSendHTTP, Guid id, string tokenKey);
        Task<List<FileFilterQuery>> GetFile(bool isSendHTTP, FileFilterModel fileUpload);
        //Task<List<FileFilterQuery>> GetFileFTP(FileFilterModel fileUpload);
        Task<List<UserAPIModel>> FindAllChildren(bool isSendHTTP);
    }
}
