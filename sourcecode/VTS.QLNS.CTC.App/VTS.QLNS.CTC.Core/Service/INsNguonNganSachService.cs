using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsNguonNganSachService
    {
        public IEnumerable<NsNguonNganSach> FindNguonNganSach();
        public NsNguonNganSach FindNguonNganSachById(int IdMaNguon);
        public IEnumerable<NsNguonNganSach> FindAll();
        public List<NsNguonNganSach> FindByDuAnId(string duAnId);
        NsNguonNganSach FindById(Guid id);
    }
}
