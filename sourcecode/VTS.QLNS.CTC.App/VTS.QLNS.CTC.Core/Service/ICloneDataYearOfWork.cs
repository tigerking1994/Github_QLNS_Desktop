using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ICloneDataYearOfWork
    {
        void CloneData(int sourceYear, int destinationYear, AuthenticationInfo authenticationInfo,
            int isUpdatedMLNS,
            int isUpdatedNSDV,
            int isUpdatedBQuanly,
            int isUpdateMLQS,
            int isUpdateDanhMucChuyenNganh,
            int isUpdateDanhMucNganh,
            int isUpdateMuclucSkt,
            int isUpdateDanhMucCapPhat,
            int isUpdatePhuCapMLNS,
            int isUpdateDanhMucCapBacKH,
            int isUpdateMucLucSktMuclucNs,
            int isUpdateCauHinhHeThong,
            int isUpdateDanhMucDonViTinh,
            int isUpdateDanhMucCanCu,
            int isUpdateDanhMucCKTC,
            int isUpdateDanhMucBHXH,
            int isUpdateDanhMucCacLoaiChi,
            int isUpdateDanhMucCoSoYTe,
            int isUpdateDanhMucThamDinhQuyetToan,
            int isUpdateDanhMucCauHinhThamSoBHXH,
            int isUpdateDanhMucNgayNghi,
            int isUpdateMucLucQuyetToannam
            //int isUpdateDanhMucChuDauTu,
            //int isUpdateDanhMucDonviQuanLyDuAn,
            //int isUpdateDanhMucNhaThau

            );

        void CloneDataExistData(int sourceYear, int destinationYear, string tableName, AuthenticationInfo authenticationInfo, bool isCloneUnit);

        bool IsDuplicateUnit(int source, int dest);

    }
}
