using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITLDanhMucChucVuNq104Repository : IRepository<TlDmChucVuNq104>
    {
        TlDmChucVuNq104 FindByMa(string ma);
    }
}
