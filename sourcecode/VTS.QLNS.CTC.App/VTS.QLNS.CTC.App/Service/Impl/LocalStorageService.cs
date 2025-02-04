using Microsoft.Extensions.Configuration;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Service.Impl
{
    public class LocalStorageService : StorageService, IStorageService
    {
        private readonly ILog _logger;
        private readonly IConfiguration _configuration;
        private readonly IAttachmentService _attachService;
        private readonly IDanhMucService _danhmucService;
        private readonly ReportPreviewViewModel _reportPreviewViewModel;

        public LocalStorageService(
            ILog logger,
            IConfiguration configuration,
            IAttachmentService attachService,
            IDanhMucService danhmucService,
            ReportPreviewViewModel reportPreviewViewModel)
        {
            _logger = logger;
            _configuration = configuration;
            _attachService = attachService;
            _danhmucService = danhmucService;
            _reportPreviewViewModel = reportPreviewViewModel;
        }

        public void LoadConfig()
        {
            _uploadStoragePath = Path.Combine(IOExtensions.ApplicationPath, _configuration.GetSection(ConfigHelper.STORAGE_UPLOAD_PATH).Value);
        }

        public void Upload(AttachmentEnum.Type objectType, Guid objectId, IEnumerable<AttachmentModel> attachmentsUpload)
        {
            try
            {
                List<string> sourceFilePaths = new List<string>();
                if (attachmentsUpload.Any())
                    sourceFilePaths = attachmentsUpload.Select(n => n.FilePath).ToList();
                if (!ValidateMaxlength(sourceFilePaths.ToArray()))
                {
                    return;
                }

                this.LoadConfig();

                string uploadFolder = Path.Combine(DateTime.Now.ToString("yyyy-MM"), AttachmentEnum.ValueOf(objectType));
                string uploadPath = Path.Combine(_uploadStoragePath, uploadFolder);
                IOExtensions.CreateDirectoryIfNotExists(uploadPath);

                List<Attachment> attachments = new List<Attachment>();
                foreach (var file in attachmentsUpload)
                {
                    string fileName = Path.GetFileName(file.FilePath);
                    string desFileName = EncodeFileName(file.FilePath);
                    string desFilePath = Path.Combine(uploadPath, desFileName);
                    File.Copy(file.FilePath, desFilePath, true);

                    Attachment attach = new Attachment
                    {
                        ObjectId = objectId,
                        ModuleType = (int)objectType,
                        FilePath = Path.Combine(uploadFolder, desFileName),
                        UploadType = (int)StorageTypeEnum.Type.LOCAL,
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
                string uploadPath = Path.Combine(_uploadStoragePath, uploadFolder);
                IOExtensions.CreateDirectoryIfNotExists(uploadPath);

                List<Attachment> entities = new List<Attachment>();
                foreach (var item in attachments)
                {
                    string desFileName = EncodeFileName(item.FilePath);
                    string desFilePath = Path.Combine(uploadPath, desFileName);
                    string sourceFilePath = Path.Combine(_uploadStoragePath, item.FilePath);
                    File.Copy(sourceFilePath, desFilePath, true);

                    Attachment attach = new Attachment
                    {
                        ObjectId = objectId,
                        ModuleType = (int)objectType,
                        FilePath = Path.Combine(uploadFolder, desFileName),
                        UploadType = (int)StorageTypeEnum.Type.LOCAL,
                        FileName = item.FileName,
                        DNgayCanCu = item.DNgayCanCu,
                        ILoaiCanCu = item.ILoaiCanCu,
                        SSoCanCu = item.SSoCanCu,
                        CreatedDate = DateTime.Now
                    };
                    entities.Add(attach);
                }
                _attachService.AddRange(entities);
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
                                    string filePath = Path.Combine(_uploadStoragePath, item.FilePath);

                                    if (File.Exists(filePath))
                                    {
                                        File.Copy(filePath, Path.Combine(dialog.SelectedPath, item.FileName), true);
                                    }
                                    else
                                    {
                                        ShowMessageNotExistFile();
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

        public void SaveSingleFile(Attachment item)
        {
            using (var dialog = new System.Windows.Forms.SaveFileDialog())
            {
                dialog.FileName = item.FileName;
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    this.LoadConfig();

                    string filePath = Path.Combine(_uploadStoragePath, item.FilePath);
                    if (File.Exists(filePath))
                    {
                        File.Copy(filePath, dialog.FileName, true);
                    }
                    else
                    {
                        ShowMessageNotExistFile();
                    }
                    // Open folder selected
                    IOExtensions.OpenFiles(dialog.FileName);
                }
            }
        }

        public void View(Guid id)
        {
            try
            {
                var rs = _attachService.FindById(id);
                if (rs != null)
                {
                    this.LoadConfig();

                    string sourceFileName = Path.Combine(_uploadStoragePath, rs.FilePath);
                    string destFileName = Path.Combine(IOExtensions.TempPath, rs.FileName);
                    File.Copy(sourceFileName, destFileName, true);

                    var pdfFiles = new System.Collections.ObjectModel.ObservableCollection<Model.PdfFileModel>()
                    {
                        new PdfFileModel()
                        {
                            Title = rs.FileName,
                            FilePath = destFileName
                        }
                    };

                    _reportPreviewViewModel.Items = pdfFiles;
                    _reportPreviewViewModel.Init();
                    _reportPreviewViewModel.ShowDialog();
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

        public void Remove(List<Guid> ids)
        {
            try
            {
                IEnumerable<Attachment> attachments = _attachService.FindByIdIn(ids);
                _attachService.RemoveAttachments(attachments);
                this.LoadConfig();
                foreach (var rs in attachments)
                {
                    string sourceFileName = Path.Combine(_uploadStoragePath, rs.FilePath);
                    File.Delete(sourceFileName);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void Download(Attachment entity)
        {
            throw new NotImplementedException();
        }

        public void DownloadAll(List<Attachment> entities)
        {
            throw new NotImplementedException();
        }
    }
}
