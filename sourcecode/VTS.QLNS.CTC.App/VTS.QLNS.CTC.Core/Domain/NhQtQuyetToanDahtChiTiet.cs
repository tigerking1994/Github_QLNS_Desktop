using System;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhQtQuyetToanDahtChiTiet
    {
        public Guid Id { get; set; }
        public Guid? IIdDeNghiQuyetToanDahtId { get; set; }
        public Guid? IIDHMCP { get; set; }
        public double? FGiaTriQuyetToanAbUsd { get; set; }
        public double? FGiaTriQuyetToanAbVnd { get; set; }
        public double? FGiaTriQuyetToanAbEur { get; set; }
        public double? FGiaTriQuyetToanAbNgoaiTeKhac { get; set; }
        public double? FKetQuaKiemToanUsd { get; set; }
        public double? FKetQuaKiemToanVnd { get; set; }
        public double? FKetQuaKiemToanEur { get; set; }
        public double? FKetQuaKiemToanNgoaiTeKhac { get; set; }
        public double? FDeNghiQuyetToanUsd { get; set; }
        public double? FDeNghiQuyetToanVnd { get; set; }
        public double? FDeNghiQuyetToanEur { get; set; }
        public double? FDeNghiQuyetToanNgoaiTeKhac { get; set; }
        public double? FDeNghiSoVoiDuToanUsd { get; set; }
        public double? FDeNghiSoVoiDuToanVnd { get; set; }
        public double? FDeNghiSoVoiDuToanEur { get; set; }
        public double? FDeNghiSoVoiDuToanNgoaiTeKhac { get; set; }
        public double? FDeNghiSoVoiQuyetToanAbUsd { get; set; }
        public double? FDeNghiSoVoiQuyetToanAbVnd { get; set; }
        public double? FDeNghiSoVoiQuyetToanAbEur { get; set; }
        public double? FDeNghiSoVoiQuyetToanAbNgoaiTeKhac { get; set; }
        public double? FDeNghiSoVoiKetQuaKiemToanUsd { get; set; }
        public double? FDeNghiSoVoiKetQuaKiemToanVnd { get; set; }
        public double? FDeNghiSoVoiKetQuaKiemToanEur { get; set; }
        public double? FDeNghiSoVoiKetQuaKiemToanNgoaiTeKhac { get; set; }
        public double? FGiaTriThamTraUsd { get; set; }
        public double? FGiaTriThamTraVnd { get; set; }
        public double? FGiaTriThamTraEur { get; set; }
        public double? FGiaTriThamTraNgoaiTeKhac { get; set; }
        public double? FGiaTriPheDuyetQuyetToanUsd { get; set; }
        public double? FGiaTriPheDuyetQuyetToanVnd { get; set; }
        public double? FGiaTriPheDuyetQuyetToanEur { get; set; }
        public double? FGiaTriPheDuyetQuyetToanNgoaiTeKhac { get; set; }
        public double? FPheDuyetSoVoiDuToanUsd { get; set; }
        public double? FPheDuyetSoVoiDuToanVnd { get; set; }
        public double? FPheDuyetSoVoiDuToanEur { get; set; }
        public double? FPheDuyetSoVoiDuToanNgoaiTeKhac { get; set; }
        public double? FPheDuyetSoVoiDeNghiUsd { get; set; }
        public double? FPheDuyetSoVoiDeNghiVnd { get; set; }
        public double? FPheDuyetSoVoiDeNghiEur { get; set; }
        public double? FPheDuyetSoVoiDeNghiNgoaiTeKhac { get; set; }
        public string SMaOrder { get; set; }
        public int? IType { get; set; }
    }
}
