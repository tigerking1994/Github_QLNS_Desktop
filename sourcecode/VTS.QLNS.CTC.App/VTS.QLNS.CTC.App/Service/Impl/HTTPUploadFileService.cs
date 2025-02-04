using AutoMapper;
using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.Impl
{
    public class HTTPUploadFileService : IHTTPUploadFileService
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IDanhMucService _danhMucService;
        private readonly string PRIVATE_KEY = @"QLNS_CTC_BQP_KEY";
        private string _hTTPHost;
        private string _hTTPPort;
        private string _hTTPUser;
        private string _hTTPPassword;
        private string _hTTPToken;
        private int _statusCode;
        private string _fTPHost;
        private string _fTPPort;
        private string _fTPUser;
        private string _fTPPassword;
        private string _fTPToken;
        private readonly ICryptographyService _cryptographyService;
        private readonly ISessionService _sessionService;

        public HTTPUploadFileService(
            ILog logger,
            IMapper mapper,
            IConfiguration configuration,
            IDanhMucService danhMucService,
            ICryptographyService cryptographyService,
            ISessionService sessionService)
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _danhMucService = danhMucService;
            _sessionService = sessionService;
            _cryptographyService = cryptographyService;
        }
        
        public async Task<MemoryStream> DownloadDecryptFile(bool isSendHTTP, Guid id, string tokenKey)
        {
            await LoadConfiguration();
            try
            {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + (isSendHTTP ? _hTTPToken : _fTPToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));


                // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                using (HttpResponseMessage res = await client.GetAsync($"{(isSendHTTP ? _hTTPHost : _fTPHost)}:{(isSendHTTP ? _hTTPPort : _fTPPort)}/api/ReportAPI/GetFile/{id.ToString()}"))
                {
                    using (HttpContent content = res.Content)
                    {
                        //Now assign your content to your data variable, by converting into a string using the await keyword.
                        var contentReturn = await content.ReadAsStreamAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        if (contentReturn != null)
                        {
                            var outStream = new MemoryStream();
                            _cryptographyService.DecryptFile(contentReturn, outStream, tokenKey);
                            return outStream;
                        }
                        else
                        {
                            return new MemoryStream();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                return new MemoryStream();
            }
        }
        
        public async Task<MemoryStream> DownloadDecryptFileFTP(Guid id, string tokenKey)
        {
            await LoadConfigurationFTP();
            try
            {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _fTPToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));


                // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                using (HttpResponseMessage res = await client.GetAsync($"{_fTPHost}:{_fTPPort}/api/ReportAPI/GetFile/{id.ToString()}"))
                {
                    using (HttpContent content = res.Content)
                    {
                        //Now assign your content to your data variable, by converting into a string using the await keyword.
                        var contentReturn = await content.ReadAsStreamAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        if (contentReturn != null)
                        {
                            var outStream = new MemoryStream();
                            _cryptographyService.DecryptFile(contentReturn, outStream, tokenKey);
                            return outStream;
                        }
                        else
                        {
                            return new MemoryStream();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                return new MemoryStream();
            }
        }


        public async Task<MemoryStream> DownloadFile(Guid id)
        {
            await LoadConfiguration();
            try
            {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _hTTPToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));


                // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                using (HttpResponseMessage res = await client.GetAsync($"{_hTTPHost}:{_hTTPPort}/api/ReportAPI/GetFile/{id.ToString()}"))
                {
                    using (HttpContent content = res.Content)
                    {
                        //Now assign your content to your data variable, by converting into a string using the await keyword.
                        var contentReturn = await content.ReadAsStreamAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        if (contentReturn != null)
                        {
                            var _ms = new MemoryStream();
                            contentReturn.CopyTo(_ms);
                            _ms.Seek(0, SeekOrigin.Begin);
                            _ms.Position = 0;
                            return _ms;
                        }
                        else
                        {
                            return new MemoryStream();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                return new MemoryStream();
            }
        }

        
        public async Task<List<FileFilterQuery>> GetFile(bool isSendHTTP, FileFilterModel fileUpload)
        {
            await LoadConfiguration();
            try
            {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + (isSendHTTP ? _hTTPToken : _fTPToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

                var json = JsonConvert.SerializeObject(fileUpload);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                using (HttpResponseMessage res = await client.PostAsync($"{(isSendHTTP ? _hTTPHost : _fTPHost)}:{(isSendHTTP ? _hTTPPort : _fTPPort)}/api/ReportAPI/Filter", data))
                {
                    using (HttpContent content = res.Content)
                    {
                        //Now assign your content to your data variable, by converting into a string using the await keyword.
                        var contentReturn = await content.ReadAsStringAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        if (contentReturn != null)
                        {
                            var objReturn = JsonConvert.DeserializeObject<List<FileFilterQuery>>(contentReturn);
                            return objReturn;
                        }
                        else
                        {
                            return new List<FileFilterQuery>();
                        }

                    }
                }
            }
            catch (Exception)
            {
                return new List<FileFilterQuery>();
            }
        }
        
        public async Task<List<FileFilterQuery>> GetFileFTP(FileFilterModel fileUpload)
        {
            await LoadConfigurationFTP();
            try
            {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _fTPToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

                var json = JsonConvert.SerializeObject(fileUpload);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                using (HttpResponseMessage res = await client.PostAsync($"{_fTPHost}:{_fTPPort}/api/ReportAPI/Filter", data))
                {
                    using (HttpContent content = res.Content)
                    {
                        //Now assign your content to your data variable, by converting into a string using the await keyword.
                        var contentReturn = await content.ReadAsStringAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        if (contentReturn != null)
                        {
                            var objReturn = JsonConvert.DeserializeObject<List<FileFilterQuery>>(contentReturn);
                            return objReturn;
                        }
                        else
                        {
                            return new List<FileFilterQuery>();
                        }

                    }
                }
            }
            catch (Exception)
            {
                return new List<FileFilterQuery>();
            }
        }

        public async Task<List<UserAPIModel>> FindAllChildren(bool isSendHTTP)
        {
            await LoadConfiguration();
            try
            {
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + (isSendHTTP ? _hTTPToken : _fTPToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

                // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                using (HttpResponseMessage res = await client.GetAsync($"{(isSendHTTP ? _hTTPHost : _fTPHost)}:{(isSendHTTP ? _hTTPPort : _fTPPort)}/api/ReportAPI/FindAllChildren"))
                {
                    using (HttpContent content = res.Content)
                    {
                        var contentReturn = await content.ReadAsStringAsync();
                        if (contentReturn != null)
                        {
                            var objReturn = JsonConvert.DeserializeObject<List<UserAPIModel>>(contentReturn);
                            return objReturn;
                        }
                        else
                        {
                            return new List<UserAPIModel>();
                        }

                    }
                }
            }
            catch (Exception)
            {
                return new List<UserAPIModel>();
            }
        }



        private async Task LoadConfiguration()
        {
            var configCodes = new List<ConfigurationUploadFile>()
                {
                    ConfigurationUploadFile.HTTP_HOST,
                    ConfigurationUploadFile.HTTP_PORT,
                    ConfigurationUploadFile.HTTP_USER,
                    ConfigurationUploadFile.HTTP_PASSWORD,
                };
            try
            {
                var rs = _danhMucService.FindByCodes(configCodes.Select(x => x.ToString()).ToList()).Where(x => x.INamLamViec == _sessionService.Current.YearOfWork);

                _hTTPHost = rs.FirstOrDefault(x => ConfigurationUploadFile.HTTP_HOST.ToString().Equals(x.IIDMaDanhMuc)).SGiaTri;
                _hTTPPort = rs.FirstOrDefault(x => ConfigurationUploadFile.HTTP_PORT.ToString().Equals(x.IIDMaDanhMuc)).SGiaTri;
                _hTTPUser = rs.FirstOrDefault(x => ConfigurationUploadFile.HTTP_USER.ToString().Equals(x.IIDMaDanhMuc)).SGiaTri;
                _hTTPPassword = rs.FirstOrDefault(x => ConfigurationUploadFile.HTTP_PASSWORD.ToString().Equals(x.IIDMaDanhMuc)).SGiaTri;

                using (HttpClient client = new HttpClient())
                {
                    var person = new
                    {
                        username = _hTTPUser,
                        password = _hTTPPassword
                    };
                    var json = JsonConvert.SerializeObject(person);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                    using HttpResponseMessage res = await client.PostAsync($"{_hTTPHost}:{_hTTPPort}/api/Auth/GetToken", data);
                    if (res.IsSuccessStatusCode)
                    {
                        using HttpContent content = res.Content;
                        //Now assign your content to your data variable, by converting into a string using the await keyword.
                        var contentReturn = await content.ReadAsStringAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        if (contentReturn != null)
                        {
                            dynamic objReturn = JsonConvert.DeserializeObject(contentReturn);
                            _hTTPToken = objReturn.Message;
                            _statusCode = objReturn.StatusCode;
                        }
                    }
                    else
                    {
                        _hTTPToken = null;
                        _statusCode = 200;
                    }                    
                }
            }
            catch (Exception ex)
            {
                _hTTPToken = null;
                _statusCode = 200;
                _logger.Error(ex.Message, ex);
            }
        }
        
        private async Task LoadConfigurationFTP()
        {
            var configCodes = new List<ConfigurationUploadFile>()
                {
                    ConfigurationUploadFile.FTP_HOST,
                    ConfigurationUploadFile.FTP_PORT,
                    ConfigurationUploadFile.FTP_USER,
                    ConfigurationUploadFile.FTP_PASSWORD,
                };
            try
            {
                var rs = _danhMucService.FindByCodes(configCodes.Select(x => x.ToString()).ToList()).Where(x => x.INamLamViec == _sessionService.Current.YearOfWork);

                _fTPHost = rs.FirstOrDefault(x => ConfigurationUploadFile.FTP_HOST.ToString().Equals(x.IIDMaDanhMuc)).SGiaTri;
                _fTPPort = rs.FirstOrDefault(x => ConfigurationUploadFile.FTP_PORT.ToString().Equals(x.IIDMaDanhMuc)).SGiaTri;
                _fTPUser = rs.FirstOrDefault(x => ConfigurationUploadFile.FTP_USER.ToString().Equals(x.IIDMaDanhMuc)).SGiaTri;
                _fTPPassword = rs.FirstOrDefault(x => ConfigurationUploadFile.FTP_PASSWORD.ToString().Equals(x.IIDMaDanhMuc)).SGiaTri;

                using (HttpClient client = new HttpClient())
                {
                    var person = new
                    {
                        username = _fTPUser,
                        password = _fTPPassword
                    };
                    var json = JsonConvert.SerializeObject(person);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                    using HttpResponseMessage res = await client.PostAsync($"{_fTPHost}:{_fTPPort}/api/Auth/GetToken", data);
                    if(res.IsSuccessStatusCode)
                    {
                        using HttpContent content = res.Content;
                        //Now assign your content to your data variable, by converting into a string using the await keyword.
                        var contentReturn = await content.ReadAsStringAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        if (contentReturn != null)
                        {
                            dynamic objReturn = JsonConvert.DeserializeObject(contentReturn);
                            _hTTPToken = objReturn.Message;
                            _statusCode = objReturn.StatusCode;
                        }
                    }
                    else
                    {
                        _fTPToken = null;
                        _statusCode = 200;
                    }                    
                }
            }
            catch (Exception ex)
            {
                _fTPToken = null;
                _statusCode = 200;
                _logger.Error(ex.Message, ex);
            }
        }

        public async Task UploadFile(FileUploadModel fileUpload)
        {
            await LoadConfiguration();
            try
            {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _hTTPToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

                var data = new MultipartFormDataContent($"Upload {DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture)}");
                data.Headers.ContentType.MediaType = "multipart/form-data";
                var fileStream = File.OpenRead(fileUpload.FileUrl);
                data.Add(new StreamContent(fileStream), "formFile", Path.GetFileName(fileUpload.FileUrl));
                data.Add(new StringContent(fileUpload.Description), "description");
                data.Add(new StringContent(fileUpload.Module), "module");
                data.Add(new StringContent(fileUpload.ModuleName), "moduleName");
                data.Add(new StringContent(fileUpload.SubModule), "subModule");
                data.Add(new StringContent(fileUpload.SubModuleName), "subModuleName");
                data.Add(new StringContent(fileUpload.Quarter.ToString()), "quarter");
                data.Add(new StringContent(fileUpload.YearOfWork.ToString()), "yearOfWork");
                data.Add(new StringContent(fileUpload.YearOfBudget.ToString()), "yearOfBudget");
                data.Add(new StringContent(fileUpload.SourceOfBudget.ToString()), "sourceOfBudget");
                data.Add(new StringContent(fileUpload.TypeOfSettlement.ToString()), "typeOfSettlement");
                data.Add(new StringContent(fileUpload.TokenKey), "tokenKey");

                // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                using (HttpResponseMessage res = await client.PostAsync($"{_hTTPHost}:{_hTTPPort}/api/ReportAPI/PostFile", data))
                {
                    using (HttpContent content = res.Content)
                    {
                        //Now assign your content to your data variable, by converting into a string using the await keyword.
                        var contentReturn = await content.ReadAsStringAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        if (contentReturn != null)
                        {
                            //Now log your data in the console
                            Console.WriteLine("Data ------------{0}", contentReturn);
                        }
                        else
                        {
                            Console.WriteLine("No Data----------");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception Hit------------");
                Console.WriteLine(exception);
            }
        }


        public async Task<(int, string)> GetToken(bool isSendHTTP)
        {
            try
            {
                if (isSendHTTP)
                {
                    await LoadConfiguration();
                    return (_statusCode, _hTTPToken);
                }
                else
                {
                    await LoadConfigurationFTP();
                    return (_statusCode, _hTTPToken);
                }                
            }
            catch (ConfigurationErrorsException exception)
            {
                throw exception;
            }
        }

        public async Task<string> GetTokenFTP()
        {
            try
            {
                await LoadConfigurationFTP();
                return _fTPToken;
            }
            catch (ConfigurationErrorsException exception)
            {
                throw exception;
            }
        }


        public async Task UploadFile(FileUploadStreamModel fileUpload)
        {
            // await LoadConfiguration();            
            try
            {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _hTTPToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

                var data = new MultipartFormDataContent($"Upload {DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture)}");
                data.Headers.ContentType.MediaType = "multipart/form-data";
                data.Add(new StreamContent(fileUpload.File), "formFile", fileUpload.Name);
                data.Add(new StringContent(fileUpload.Description), "description");
                data.Add(new StringContent(fileUpload.Department), "department");
                data.Add(new StringContent(fileUpload.Module), "module");
                data.Add(new StringContent(fileUpload.ModuleName), "moduleName");
                data.Add(new StringContent(fileUpload.SubModule), "subModule");
                data.Add(new StringContent(fileUpload.SubModuleName), "subModuleName");
                data.Add(new StringContent(fileUpload.Quarter), "quarter");
                data.Add(new StringContent(fileUpload.YearOfWork.ToString()), "yearOfWork");
                data.Add(new StringContent(fileUpload.YearOfBudget.ToString()), "yearOfBudget");
                data.Add(new StringContent(fileUpload.SourceOfBudget.ToString()), "sourceOfBudget");
                data.Add(new StringContent(fileUpload.TypeOfSettlement.ToString()), "typeOfSettlement");
                data.Add(new StringContent(fileUpload.TokenKey), "tokenKey");
                data.Add(new StringContent(fileUpload.IdChild), "idChild");

                // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                using (HttpResponseMessage res = await client.PostAsync($"{_hTTPHost}:{_hTTPPort}/api/ReportAPI/PostFile", data))
                {
                    using (HttpContent content = res.Content)
                    {
                        //Now assign your content to your data variable, by converting into a string using the await keyword.
                        var contentReturn = await content.ReadAsStringAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        if (contentReturn != null)
                        {
                            //Now log your data in the console
                            Console.WriteLine("Data ------------{0}", contentReturn);
                        }
                        else
                        {
                            Console.WriteLine("No Data----------");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception Hit------------");
                Console.WriteLine(exception);
            }
        }
        
        public async Task UploadFile(bool isSendHTTP, FileUploadStreamModel fileUpload)
        {
            // await LoadConfiguration();            
            try
            {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + (isSendHTTP ? _hTTPToken : _fTPToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

                var data = new MultipartFormDataContent($"Upload {DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture)}");
                data.Headers.ContentType.MediaType = "multipart/form-data";
                data.Add(new StreamContent(fileUpload.File), "formFile", fileUpload.Name);
                data.Add(new StringContent(fileUpload.Description), "description");
                data.Add(new StringContent(fileUpload.Department), "department");
                data.Add(new StringContent(fileUpload.Module), "module");
                data.Add(new StringContent(fileUpload.ModuleName), "moduleName");
                data.Add(new StringContent(fileUpload.SubModule), "subModule");
                data.Add(new StringContent(fileUpload.SubModuleName), "subModuleName");
                data.Add(new StringContent(fileUpload.Quarter), "quarter");
                data.Add(new StringContent(fileUpload.YearOfWork.ToString()), "yearOfWork");
                data.Add(new StringContent(fileUpload.YearOfBudget.ToString()), "yearOfBudget");
                data.Add(new StringContent(fileUpload.SourceOfBudget.ToString()), "sourceOfBudget");
                data.Add(new StringContent(fileUpload.TypeOfSettlement.ToString()), "typeOfSettlement");
                data.Add(new StringContent(fileUpload.TokenKey), "tokenKey");
                data.Add(new StringContent(fileUpload.IdChild ?? Guid.Empty.ToString()), "idChild");

                // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                using (HttpResponseMessage res = await client.PostAsync($"{(isSendHTTP ? _hTTPHost : _fTPHost)}:{(isSendHTTP ? _hTTPPort : _fTPPort)}/api/ReportAPI/PostFile", data))
                {
                    using (HttpContent content = res.Content)
                    {
                        //Now assign your content to your data variable, by converting into a string using the await keyword.
                        var contentReturn = await content.ReadAsStringAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        if (contentReturn != null)
                        {
                            //Now log your data in the console
                            Console.WriteLine("Data ------------{0}", contentReturn);
                        }
                        else
                        {
                            Console.WriteLine("No Data----------");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception Hit------------");
                Console.WriteLine(exception);
            }
        }
        
        public async Task<bool> UploadFileAsync(bool isSendHTTP, FileUploadStreamModel fileUpload)
        {
            // await LoadConfiguration();            
            try
            {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + (isSendHTTP ? _hTTPToken : _fTPToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

                var data = new MultipartFormDataContent($"Upload {DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture)}");
                data.Headers.ContentType.MediaType = "multipart/form-data";
                if (fileUpload.listAgencyUpload.Count > 0)
                {
                    foreach(var item in fileUpload.listAgencyUpload)
                    {
                        data.Add(new StreamContent(item.File), "formFile", item.Name);
                    }
                }
                //data.Add(new StreamContent(fileUpload.File), "formFile", fileUpload.Name);
                data.Add(new StringContent(fileUpload.Description), "description");
                data.Add(new StringContent(fileUpload.Department), "department");
                data.Add(new StringContent(fileUpload.Module), "module");
                data.Add(new StringContent(fileUpload.ModuleName), "moduleName");
                data.Add(new StringContent(fileUpload.SubModule), "subModule");
                data.Add(new StringContent(fileUpload.SubModuleName), "subModuleName");
                data.Add(new StringContent(fileUpload.Quarter), "quarter");
                data.Add(new StringContent(fileUpload.YearOfWork.ToString()), "yearOfWork");
                data.Add(new StringContent(fileUpload.YearOfBudget.ToString()), "yearOfBudget");
                data.Add(new StringContent(fileUpload.SourceOfBudget.ToString()), "sourceOfBudget");
                data.Add(new StringContent(fileUpload.TypeOfSettlement.ToString()), "typeOfSettlement");
                data.Add(new StringContent(fileUpload.TokenKey), "tokenKey");
                data.Add(new StringContent(fileUpload.IdChild), "idChild");

                // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                using (HttpResponseMessage res = await client.PostAsync($"{(isSendHTTP ? _hTTPHost : _fTPHost)}:{(isSendHTTP ? _hTTPPort : _fTPPort)}/api/ReportAPI/PostListFile", data))
                {
                    using (HttpContent content = res.Content)
                    {
                        //Now assign your content to your data variable, by converting into a string using the await keyword.
                        var contentReturn = await content.ReadAsStringAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        if (contentReturn != null)
                        {
                            //Now log your data in the console
                            Console.WriteLine("Data ------------{0}", contentReturn);                            
                        }
                        else
                        {
                            Console.WriteLine("No Data----------");
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception Hit------------");
                Console.WriteLine(exception);
                return false;
            }
        }
    }
}

