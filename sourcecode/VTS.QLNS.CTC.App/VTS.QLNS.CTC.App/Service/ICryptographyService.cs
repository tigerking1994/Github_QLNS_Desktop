using System.IO;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Service
{
    public interface ICryptographyService
    {
        void EncryptFile(string inputFile, string outputFile);
        Task EncryptFile(MemoryStream inputFile, ref MemoryStream outputFile, string tokenKey);
        void DecryptFile(string inputFile, string outputFile);
        void DecryptFile(Stream inputFile, MemoryStream outputFile, string tokenKey);
        void GetPokemon(string fileUrl);
        string GetSalt();
    }
}
