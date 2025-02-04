using AutoMapper;
using log4net;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.Impl
{
    public class CryptographyService : ICryptographyService
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly string _templatePath;
        private readonly string _tmpPath;
        private readonly string PRIVATE_KEY = @"QLNS_CTC_BQP";
        private static int saltLengthLimit = 32;

        public CryptographyService(
            ILog logger,
            IMapper mapper,
            IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;

            _templatePath = _configuration.GetSection(ConfigHelper.TEMPLATE_XLXSPATH).Value;
            _tmpPath = Path.Combine(IOExtensions.ApplicationPath, _configuration.GetSection(ConfigHelper.TEMP_PATH).Value);

            IOExtensions.CreateDirectoryIfNotExists(_templatePath);
            IOExtensions.CreateDirectoryIfNotExists(_tmpPath);

            try
            {
                // Clear temp folder
                IOExtensions.ClearForlder(_tmpPath);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        public string GetSalt()
        {
            return BitConverter.ToString(GetSalt(saltLengthLimit));
        }
        private static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }


        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Encrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public void EncryptFile(string inputFile, string outputFile)
        {
            try
            {
                SHA256 sha256Hash = SHA256.Create();
                //string password = @"myKey123"; // Your Key Here 16 bytes
                string hashPassword = GetHash(sha256Hash, PRIVATE_KEY);

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(hashPassword);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);

                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                //MessageBox.Show("Encryption failed!", "Error");
                MessageBox.Show("Mã hóa không thành công!", "Lỗi mã hóa");
            }
        }

        public Task EncryptFile(MemoryStream inputFile, ref MemoryStream outputFile, string tokenKey)
        {
            try
            {
                SHA256 sha256Hash = SHA256.Create();
                //string password = @"myKey123"; // Your Key Here 16 bytes
                string hashPassword = GetHash(sha256Hash, tokenKey);

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(hashPassword);

                //string cryptFile = outputFile;
                //FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();
                CryptoStream cs = new CryptoStream(outputFile,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);
                int data;
                while ((data = inputFile.ReadByte()) != -1)
                    cs.WriteByte((byte)data);
                cs.FlushFinalBlock();
                outputFile.Seek(0, SeekOrigin.Begin);
                //byte[] buffer = new byte[16 * 1024];
                //int read;
                //while ((read = inputFile.Read(buffer, 0, buffer.Length)) > 0)
                //{
                //    cs.Write(buffer, 0, read);
                //}
                return Task.Run(() =>
                {
                    Console.WriteLine("Encryption Success!");
                    //cs.Close();
                });
            }
            catch (Exception ex)
            {
                return Task.Run(() =>
                {
                    _logger.Error(ex.Message, ex);
                    Console.WriteLine("Encryption failed!");
                    //MessageBox.Show("Encryption failed!", "Error");
                    MessageBox.Show("Mã hóa không thành công!", "Lỗi mã hóa");
                });
            }
        }

        public void DecryptFile(Stream fsCrypt, MemoryStream fsOut, string tokenKey)
        {

            try
            {
                SHA256 sha256Hash = SHA256.Create();
                //string password = @"myKey123"; // Your Key Here 16 bytes
                string hashPassword = GetHash(sha256Hash, tokenKey);
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(hashPassword);


                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                //MessageBox.Show("Decryption failed!", "Error");
                MessageBox.Show("Giải mã không thành công!", "Lỗi giải mã");
            }
        }

        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public void DecryptFile(string inputFile, string outputFile)
        {

            try
            {
                SHA256 sha256Hash = SHA256.Create();
                //string password = @"myKey123"; // Your Key Here 16 bytes
                string hashPassword = GetHash(sha256Hash, PRIVATE_KEY);
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(hashPassword);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                //MessageBox.Show("Decryption failed!", "Error");
                MessageBox.Show("Giải mã không thành công!", "Lỗi giải mã");
            }
        }

        private string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString(0, 8);
        }

        // Verify a hash against a string.
        private bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            // Hash the input.
            var hashOfInput = GetHash(hashAlgorithm, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }


        //Now define your asynchronous method which will retrieve all your pokemon.
        public async void GetPokemon(string fileUrl)
        {
            //Define your baseUrl
            string baseUrl = "http://localhost:8080/";
            //Have your using statements within a try/catch block
            try
            {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using (HttpClient client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _bearerToken);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                    //var json = JsonConvert.SerializeObject(person);
                    //var json = "Hello";
                    //var data = new StringContent(json, Encoding.UTF8, "application/json");
                    var data = new MultipartFormDataContent($"Upload {DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture)}");
                    data.Headers.ContentType.MediaType = "multipart/form-data";
                    var fileStream = File.OpenRead(fileUrl);
                    data.Add(new StreamContent(fileStream), "File", "File");

                    // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                    using (HttpResponseMessage res = await client.PostAsync(baseUrl, data))
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
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception Hit------------");
                Console.WriteLine(exception);
            }
        }

    }
}

