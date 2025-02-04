using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsNguonNgansachRepository : IRepository<NsNguonNganSach>
    {
        public int AddOrUpdateNsNguonNganSach(IEnumerable<NsNguonNganSach> entities);
        public IEnumerable<NsNguonNganSach> FindNguonNganSach();
        NsNguonNganSach FindNguonNganSachById(int idMaNguon);
        public List<NsNguonNganSach> FindByDuAnId(string duAnId);
    }
}
