using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmChucVuNq104Repository : IRepository<TlDmChucVuNq104>
    {
        TlDmChucVuNq104 FindByMaChucVu(string maChucVu);
        TlDmChucVuNq104 FindByHeSoChucVu(decimal? heSoCv);
    }
}
