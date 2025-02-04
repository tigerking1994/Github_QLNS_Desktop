using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class AllowenceCanBoNq104Criteria
    {
        // PhuCap: Trường phụ cấp gồm một danh sách các phụ cấp
        public IEnumerable<AllowencePhuCapNq104Criteria> X { get; set; }
    }

    public class AllowencePhuCapNq104Criteria
    {
        // MaPhuCap: Mã phụ cấp
        public string A { get; set; }
        // GiaTri: Giá trị
        public decimal? B { get; set; }
        // NgayHuongPhuCap: Số ngày hưởng phụ cấp
        public decimal? C { get; set; }
    }
}
