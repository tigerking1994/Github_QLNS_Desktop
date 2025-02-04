using log4net;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Service.Impl
{
    public class FtpStorageService : StorageService, IStorageService
    {
        private readonly ILog _logger;
        private readonly IConfiguration _configuration;
        private readonly IAttachmentService _attachService;
        private readonly IDanhMucService _danhmucService;
        private readonly ReportPreviewViewModel _reportPreviewViewModel;
        private readonly IVdTftpfileService _ftpService;
        private readonly IVdtFtpRootService _ftpRootService;
        private readonly ImportExcelService _importExcelService;
        private readonly ISessionService _sessionService;


        public FtpStorageService(
            ILog logger,
            IConfiguration configuration,
            IAttachmentService attachService,
            IDanhMucService danhmucService,
            IVdTftpfileService ftpService,
            IVdtFtpRootService ftpRootService,
            ISessionService sessionService,
            ReportPreviewViewModel reportPreviewViewModel)
        {
            _logger = logger;
            _configuration = configuration;
            _attachService = attachService;
            _danhmucService = danhmucService;
            _ftpService = ftpService;
            _ftpRootService = ftpRootService;
            _sessionService = sessionService;

            _reportPreviewViewModel = reportPreviewViewModel;
        }

        public void LoadConfig()
        {
            try
            {
                List<string> configCodes = new List<string>()
                {
                    STORAGE_CONFIG.FTP_HOST,
                    STORAGE_CONFIG.FTP_USER,
                    STORAGE_CONFIG.FTP_PASSWORD
                };
                var rs = _danhmucService.FindByCodes(configCodes).ToList();

                _ftpHost = rs.FirstOrDefault(x => STORAGE_CONFIG.FTP_HOST.Equals(x.IIDMaDanhMuc)).SGiaTri;
                _ftpUser = rs.FirstOrDefault(x => STORAGE_CONFIG.FTP_USER.Equals(x.IIDMaDanhMuc)).SGiaTri;
                _ftpPassword = rs.FirstOrDefault(x => STORAGE_CONFIG.FTP_PASSWORD.Equals(x.IIDMaDanhMuc)).SGiaTri;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void Upload(AttachmentEnum.Type objectType, Guid objectId, IEnumerable<AttachmentModel> attachmentUploads)
        {
            try
            {
                this.LoadConfig();
                List<string> sourceFilePaths = new List<string>();
                if (attachmentUploads.Any())
                    sourceFilePaths = attachmentUploads.Select(n => n.FilePath).ToList();

                if (!ValidateMaxlength(sourceFilePaths.ToArray()))
                {
                    return;
                }

                string uploadFolder = Path.Combine(DateTime.Now.ToString("yyyy-MM"), AttachmentEnum.ValueOf(objectType));
                List<Attachment> attachments = new List<Attachment>();
                using (WebClient ftpClient = new WebClient())
                {
                    ftpClient.Proxy = null;
                    ftpClient.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                    this.CreateDirectory(_ftpHost, uploadFolder);
                    foreach (var file in attachmentUploads)
                    {
                        string fileName = Path.GetFileName(file.FilePath);
                        string desFileName = EncodeFileName(file.FilePath);
                        string desFilePath = Path.Combine(uploadFolder, desFileName);

                        string addressPath = Path.Combine(_ftpHost, desFilePath).Replace("\\", "/");
                        ftpClient.UploadFile(addressPath, file.FilePath);
                        Attachment attach = new Attachment
                        {
                            ObjectId = objectId,
                            ModuleType = (int)objectType,
                            FilePath = desFilePath,
                            UploadType = (int)StorageTypeEnum.Type.FTP_SERVER,
                            FileName = fileName,
                            DNgayCanCu = file.DNgayCanCu,
                            ILoaiCanCu = file.ILoaiCanCu,
                            SSoCanCu = file.SSoCanCu,
                            CreatedDate = DateTime.Now
                        };
                        attachments.Add(attach);
                    }
                    _attachService.AddRange(attachments);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void Copy(AttachmentEnum.Type objectType, Guid objectId, List<AttachmentModel> attachments)
        {
            try
            {
                this.LoadConfig();

                string uploadFolder = Path.Combine(DateTime.Now.ToString("yyyy-MM"), AttachmentEnum.ValueOf(objectType));
                List<Attachment> entityAttachments = new List<Attachment>();
                using (WebClient ftpClient = new WebClient())
                {
                    var credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                    ftpClient.Credentials = credentials;
                    ftpClient.Proxy = null;

                    this.CreateDirectory(_ftpHost, uploadFolder);
                    foreach (var item in attachments)
                    {
                        // Download file source
                        var requestUri = new Uri(Path.Combine(_ftpHost, item.FilePath).Replace("\\", "/"));
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(requestUri);
                        request.Method = WebRequestMethods.Ftp.DownloadFile;
                        request.Credentials = credentials;
                        request.Proxy = null;
                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            // Upload file
                            string desFileName = EncodeFileName(item.FilePath);
                            string desFilePath = Path.Combine(uploadFolder, desFileName);
                            string addressPath = Path.Combine(_ftpHost, desFilePath).Replace("\\", "/");
                            ftpClient.UploadData(addressPath, IOExtensions.ToByteArray(responseStream));

                            // Add database
                            Attachment attach = new Attachment
                            {
                                ObjectId = objectId,
                                ModuleType = (int)objectType,
                                FilePath = desFilePath,
                                UploadType = (int)StorageTypeEnum.Type.FTP_SERVER,
                                FileName = item.FileName,
                                DNgayCanCu = item.DNgayCanCu,
                                ILoaiCanCu = item.ILoaiCanCu,
                                SSoCanCu = item.SSoCanCu,
                                CreatedDate = DateTime.Now
                            };
                            entityAttachments.Add(attach);
                        }
                    }
                    _attachService.AddRange(entityAttachments);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void Download(Guid id)
        {
            try
            {
                var rs = _attachService.FindById(id);
                if (rs != null)
                {
                    SaveSingleFile(rs);
                }
                else
                {
                    // File not exits
                    ShowMessageNotExistFile();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void DownloadAll(AttachmentEnum.Type objectType, Guid objectId)
        {
            try
            {
                var rs = _attachService.FindByModuleAndObjectId((int)objectType, objectId).ToList();
                if (rs.Count > 0)
                {
                    if (rs.Count == 1)
                    {
                        // Download single file
                        SaveSingleFile(rs.ElementAt(0));
                    }
                    else
                    {
                        using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
                        {
                            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                            if (result == System.Windows.Forms.DialogResult.OK)
                            {
                                this.LoadConfig();

                                foreach (var item in rs)
                                {
                                    using (WebClient ftpClient = new WebClient())
                                    {
                                        ftpClient.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                                        ftpClient.Proxy = null;
                                        string addressPath = Path.Combine(_ftpHost, item.FilePath).Replace("\\", "/");
                                        string fileName = Path.Combine(dialog.SelectedPath, item.FileName).Replace("\\", "/");
                                        ftpClient.DownloadFile(addressPath, fileName);
                                    }
                                }

                                // Open folder selected
                                IOExtensions.OpenFolder(dialog.SelectedPath);
                            }
                        }
                    }
                }
                else
                {
                    // File not exits
                    ShowMessageNotExistFile();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #region Ftp_Server
        public int UploadCommand(Guid idFtpRoot, string localPath, string fileName, string ftpPath)
        {
            try
            {
                string ftpServerPath = "";
                this.LoadConfig();
                using (WebClient ftpClient = new WebClient())
                {
                    ftpClient.Proxy = null;
                    var _ftpUserFtp = _configuration.GetSection(ConfigHelper.FTP_USERNAME).Value;
                    var _ftpPasswordFtp = _configuration.GetSection(ConfigHelper.FTP_PASSWORD).Value;
                    ftpClient.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                    this.CreateDirectory(_ftpHost, ftpPath);
                    ftpServerPath = Path.Combine(_ftpHost, ftpPath, fileName).Replace("\\", "/");

                    ftpClient.UploadFile(ftpServerPath, localPath);
                }
                // inser VDT_FtpFile
                VdtFtpFile dataFile = new VdtFtpFile()
                {
                    IID_FtpRoot = idFtpRoot,
                    SFileName = fileName, // vd: ftp:\\10.60.108.246
                    SRootPath = ftpServerPath,
                    SNguoiTao = _sessionService.Current.Principal,
                    DNgayTao = DateTime.Now
                };
                _ftpService.Add(dataFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return 0;
            }
            return 1;
        }
        public List<FileFtpModel> GetFileServerFtp(string url)
        {
            List<FileFtpModel> lstFile = new List<FileFtpModel>();
            List<string> lstResponse = new List<string>();
            this.LoadConfig();
            using (WebClient ftpClient = new WebClient())
            {
                ftpClient.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                ftpClient.Proxy = null;
                string addressPath = Path.Combine(_ftpHost, url).Replace("\\", "/");
                var request = (FtpWebRequest)WebRequest.Create(addressPath);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                try
                {
                    using (var response = (FtpWebResponse)request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream, true))
                            {
                                while (!reader.EndOfStream)
                                {
                                    lstResponse.Add(reader.ReadLine());
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    _logger.Error(ex.Message, ex);
                }

                if (lstResponse.Count != 0)
                {
                    int iIndex = 1;
                    foreach (var line in lstResponse)
                    {
                        string[] tokens = line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                        string name = tokens[8];
                        FileFtpModel item = new FileFtpModel();
                        item.IStt = iIndex++;
                        item.BIsCheck = false;
                        item.SNameFile = name;
                        item.SUrl = Path.Combine(addressPath, name).Replace("\\", "/");
                        lstFile.Add(item);
                    }
                }
                return lstFile;
            }
        }
        public string DowLoadFileFtpGiveLocal(string filename, string fileSaveLocal)
        {
            this.LoadConfig();
            string sLocalSave = string.Empty;
            string sFolderSaveFileLocal = Path.Combine(IOExtensions.ApplicationPath, ConstantUrlPathPhanHe.UrlFolderFile);
            if (!Directory.Exists(sFolderSaveFileLocal))
            {
                Directory.CreateDirectory(sFolderSaveFileLocal);
            }
            using (WebClient ftpClient = new WebClient())
            {

                ftpClient.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                ftpClient.Proxy = null;
                // Đường dẫn Download file Path.
                sLocalSave = Path.Combine(sFolderSaveFileLocal, fileSaveLocal);
                ftpClient.DownloadFile(filename, sLocalSave);
            }
            return sLocalSave;
        }
        public void GiveFileFtpGiveLocal(string url, ref string urlLocal)
        {
            try
            {
                using (var dialog = new System.Windows.Forms.SaveFileDialog())
                {
                    string[] paths = url.Split("/");
                    dialog.FileName = paths[paths.Length - 1];
                    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        this.LoadConfig();

                        using (WebClient ftpClient = new WebClient())
                        {
                            ftpClient.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                            ftpClient.Proxy = null;
                            ftpClient.DownloadFile(url, dialog.FileName);
                            urlLocal = dialog.FileName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
        public void View(Guid id)
        {
            try
            {
                var rs = _attachService.FindById(id);
                if (rs != null)
                {
                    this.LoadConfig();

                    using (WebClient ftpClient = new WebClient())
                    {
                        string fileName = Path.Combine(IOExtensions.TempPath, rs.FileName);
                        string addressPath = Path.Combine(_ftpHost, rs.FilePath).Replace("\\", "/");

                        ftpClient.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                        ftpClient.Proxy = null;
                        ftpClient.DownloadFile(addressPath, fileName);

                        var pdfFiles = new System.Collections.ObjectModel.ObservableCollection<Model.PdfFileModel>()
                        {
                            new Model.PdfFileModel()
                            {
                                Title = rs.FileName,
                                FilePath = fileName
                            }
                        };

                        _reportPreviewViewModel.Items = pdfFiles;
                        _reportPreviewViewModel.Init();
                        _reportPreviewViewModel.ShowDialog();
                    }
                }
                else
                {
                    // File not exits
                    ShowMessageNotExistFile();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void SaveSingleFile(Attachment item)
        {
            using (var dialog = new System.Windows.Forms.SaveFileDialog())
            {
                dialog.FileName = item.FileName;
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    this.LoadConfig();

                    using (WebClient ftpClient = new WebClient())
                    {
                        ftpClient.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                        ftpClient.Proxy = null;
                        string addressPath = Path.Combine(_ftpHost, item.FilePath).Replace("\\", "/");
                        ftpClient.DownloadFile(addressPath, dialog.FileName);
                    }

                    // Open folder selected
                    IOExtensions.OpenFiles(dialog.FileName);
                }
            }
        }
        private bool CreateDirectory(string ftpUrl, string uploadFolder)
        {
            bool exits = true;
            string[] subFolder = uploadFolder.Split('\\');
            string pathftp = ftpUrl;
            foreach (string item in subFolder)
            {
                if (item != "")
                {
                    try
                    {
                        pathftp = Path.Combine(pathftp, item).Replace("\\", "/");
                        //create the directory
                        FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(pathftp));
                        requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                        requestDir.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                        requestDir.UsePassive = true;
                        requestDir.UseBinary = true;
                        requestDir.KeepAlive = false;
                        requestDir.Proxy = null;
                        FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
                        Stream ftpStream = response.GetResponseStream();
                        ftpStream.Close();
                        response.Close();
                    }
                    catch (WebException ex)
                    {
                        _logger.Error(ex.Message, ex);
                        FtpWebResponse response = (FtpWebResponse)ex.Response;
                        if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                        {
                            response.Close();
                            exits = true;
                        }
                        else
                        {
                            response.Close();
                            exits = false;
                        }
                    }
                }
            }
            return exits;
        }

        public void Remove(List<Guid> ids)
        {
            try
            {
                IEnumerable<Attachment> attachments = _attachService.FindByIdIn(ids);
                _attachService.RemoveAttachments(attachments);
                this.LoadConfig();
                var Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                foreach (var rs in attachments)
                {
                    string addressPath = Path.Combine(_ftpHost, rs.FilePath).Replace("\\", "/");
                    RemoveFtpFile(addressPath);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void RemoveFtpFile(string url)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
            request.GetResponse().Close();
        }

        public void Download(Attachment entity)
        {
            throw new NotImplementedException();
        }

        public void DownloadAll(List<Attachment> entities)
        {
            throw new NotImplementedException();
        }

        public string DowLoadFileFtpGiveLocalStr(string filename, string fileSaveLocal)
        {
            this.LoadConfig();
            var sLocalSave = string.Empty;
            string sFolderSaveFileLocal = Path.Combine(IOExtensions.ApplicationPath, ConstantUrlPathPhanHe.UrlFolderFile);
            if (!Directory.Exists(sFolderSaveFileLocal))
            {
                Directory.CreateDirectory(sFolderSaveFileLocal);
            }
            using (WebClient ftpClient = new WebClient())
            {

                ftpClient.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                ftpClient.Proxy = null;
                sLocalSave = Path.Combine(sFolderSaveFileLocal, fileSaveLocal);
                ftpClient.DownloadFile(filename, sLocalSave);
            }

            return sLocalSave;
        }
    }
}
