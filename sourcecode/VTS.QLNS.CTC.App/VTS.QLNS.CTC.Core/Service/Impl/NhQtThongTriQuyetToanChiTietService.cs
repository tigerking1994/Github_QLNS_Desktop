using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhQtThongTriQuyetToanChiTietService : INhQtThongTriQuyetToanChiTietService
    {
        private readonly INhQtThongTriQuyetToanChiTietRepository _repository;

        public NhQtThongTriQuyetToanChiTietService(INhQtThongTriQuyetToanChiTietRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<NhQtThongTriQuyetToanChiTietQuery> GetCreateThongTriChiTiet(Guid iID_TTQT, int iNamThongTri, Guid iID_DonViID, Guid iID_KHTT_NhiemVuChiID)
        {
            return GetChiTietThongTriQuyetToan(iID_TTQT, iNamThongTri, iID_DonViID, iID_KHTT_NhiemVuChiID);
        }

        public void SaveThongTriChiTiet(List<NhQtThongTriQuyetToanChiTiet> input)
        {
            _repository.AddRange(input);
        }

        public List<NhQtThongTriQuyetToanChiTietQuery> GetChiTietThongTriQuyetToan(Guid iID_TTQT, int iNamThucHien, Guid iID_DonViID, Guid iID_KHCTBQP_ChuongTrinhID)
        {
            /** NOTE **
                * Clone từ BaoCaoSoChuyenNamSauController - getListDetailChiTiet
                * Mặc định chỉ lấy theo 1 chương trình
            ** END - NOTE **/

            // Khỏi tạo kết quả
            var listResult = new List<NhQtThongTriQuyetToanChiTietQuery>();

            // Lấy data chi tiết
            var listData = new List<NhQtThongTriQuyetToanChiTietQuery>(); 
            var listDetail = _repository.GetThongTriChiTietByTTQTId(iID_TTQT).ToList();

            if (listDetail.Any())
            {
                listData = listDetail;
            }
            else
            {
                listData = _repository.GetCreateThongTriChiTiet(iNamThucHien, iID_DonViID, iID_KHCTBQP_ChuongTrinhID).ToList();
            }

            if (listData == null)
            {
                return listResult;
            }

            // Lấy 1 list có ParentID
            var listTitle = listDetail.Where(x => x.iID_ParentID != null).ToList();

            //// Get info chương trình
            var chuongTrinh = listData.FirstOrDefault();

            // Lấy nhiệm vụ chi
            var newObj = new NhQtThongTriQuyetToanChiTietQuery()
            {
                sMaThuTu = ConvertLetter(1),
                sTenNoiDungChi = chuongTrinh.sTenNhiemVuChi,
                bIsTittle = true,
                fDeNghiQuyetToanNam_USD = listData.Sum(x => x.fDeNghiQuyetToanNam_USD),
                fDeNghiQuyetToanNam_VND = listData.Sum(x => x.fDeNghiQuyetToanNam_VND),
                fThuaNopTraNSNN_USD = listData.Sum(x => x.fThuaNopTraNSNN_USD),
                fThuaNopTraNSNN_VND = listData.Sum(x => x.fThuaNopTraNSNN_VND),
                bIsNhiemVuChi = true
            };
            // Add nhiệm vụ chi to result
            listResult.Add(newObj);

            // Lấy những bản ghi có thông tin dự án
            var getListDuAn = listData.Where(x => x.iID_DuAnID != null && x.iID_ParentID == null).ToList();
            // Lấy những bản ghi có thông tin hợp đồng nhưng ko có dự án
            var getListHopDong = listData.Where(x => x.iID_DuAnID == null && x.iID_HopDongID != null && x.iID_ParentID == null).ToList();
            // Lấy những bản ghi không có thông tin hợp đồng và cũng ko có thông tin dự án
            var getListNone = listData.Where(x => x.iID_DuAnID == null && x.iID_HopDongID == null && x.iID_ParentID == null).ToList();
            var iCountDuAn = 0;

            // Nếu có dự án thì sum zô rồi add vào result
            if (getListDuAn.Any())
            {
                var getNameDuAn = getListDuAn.Select(x => new { x.sTenDuAn, x.iID_DuAnID }).Distinct().ToList();
                foreach (var hopDongDuAn in getNameDuAn)
                {
                    iCountDuAn++;
                    var newObjHopDongDuAn = new NhQtThongTriQuyetToanChiTietQuery();

                    // Get data theo dự án
                    var dataSumDuAn = listData.Where(x => x.iID_DuAnID == hopDongDuAn.iID_DuAnID).ToList();

                    newObjHopDongDuAn.fDeNghiQuyetToanNam_USD = dataSumDuAn.Sum(x => x.fDeNghiQuyetToanNam_USD);
                    newObjHopDongDuAn.fDeNghiQuyetToanNam_VND = dataSumDuAn.Sum(x => x.fDeNghiQuyetToanNam_VND);
                    newObjHopDongDuAn.fThuaNopTraNSNN_USD = dataSumDuAn.Sum(x => x.fThuaNopTraNSNN_USD);
                    newObjHopDongDuAn.fThuaNopTraNSNN_VND = dataSumDuAn.Sum(x => x.fThuaNopTraNSNN_VND);

                    // Gán thêm các thông tin khác
                    newObjHopDongDuAn.sMaThuTu = ConvertLaMa(Decimal.Parse(iCountDuAn.ToString()));
                    newObjHopDongDuAn.sTenNoiDungChi = hopDongDuAn.sTenDuAn;
                    newObjHopDongDuAn.bIsTittle = true;
                    newObjHopDongDuAn.bIsData = false;
                    newObjHopDongDuAn.iID_ParentID = hopDongDuAn.iID_DuAnID;
                    newObjHopDongDuAn.iID_KHTT_NhiemVuChiID = chuongTrinh.iID_KHTT_NhiemVuChiID;
                    newObjHopDongDuAn.iID_DuAnID = hopDongDuAn.iID_DuAnID;

                    listResult.Add(newObjHopDongDuAn);
                    listResult.AddRange(returnLoaiChi(chuongTrinh.iID_KHTT_NhiemVuChiID, hopDongDuAn.iID_DuAnID, true, getListDuAn.Where(x => x.iID_DuAnID == hopDongDuAn.iID_DuAnID).ToList(), listTitle));
                }
            }

            // Nếu có hợp đồng thì cũng sum zô rồi add vòa result
            if (getListHopDong.Any())
            {
                iCountDuAn++;
                var getSumHopDong = listData.Where(x => x.iID_DuAnID == null && x.iID_HopDongID != null).ToList();

                var newObjHopDong = new NhQtThongTriQuyetToanChiTietQuery()
                {
                    sMaThuTu = ConvertLaMa(Decimal.Parse(iCountDuAn.ToString())),
                    sTenNoiDungChi = "Chi hợp đồng",
                    bIsTittle = true,
                    fDeNghiQuyetToanNam_USD = getSumHopDong.Sum(x => x.fDeNghiQuyetToanNam_USD),
                    fDeNghiQuyetToanNam_VND = getSumHopDong.Sum(x => x.fDeNghiQuyetToanNam_VND),
                    fThuaNopTraNSNN_USD = getSumHopDong.Sum(x => x.fThuaNopTraNSNN_USD),
                    fThuaNopTraNSNN_VND = getSumHopDong.Sum(x => x.fThuaNopTraNSNN_VND)
                };
                listResult.Add(newObjHopDong);
                listResult.AddRange(returnLoaiChi(chuongTrinh.iID_KHTT_NhiemVuChiID, null, true, getListHopDong, listTitle));
            }

            if (getListNone.Any())
            {
                iCountDuAn++;
                var newObjKhac = new NhQtThongTriQuyetToanChiTietQuery()
                {
                    sMaThuTu = ConvertLaMa(Decimal.Parse(iCountDuAn.ToString())),
                    sTenNoiDungChi = "Chi khác",
                    bIsTittle = true,
                    fDeNghiQuyetToanNam_USD = getListNone.Sum(x => x.fDeNghiQuyetToanNam_USD),
                    fDeNghiQuyetToanNam_VND = getListNone.Sum(x => x.fDeNghiQuyetToanNam_VND),
                    fThuaNopTraNSNN_USD = getListNone.Sum(x => x.fThuaNopTraNSNN_USD),
                    fThuaNopTraNSNN_VND = getListNone.Sum(x => x.fThuaNopTraNSNN_VND),
                };
                listResult.Add(newObjKhac);
                listResult.AddRange(returnLoaiChi(chuongTrinh.iID_KHTT_NhiemVuChiID, null, false, getListNone, listTitle));
            }

            return listResult;
        }

        public List<NhQtThongTriQuyetToanChiTietQuery> returnLoaiChi(Guid? idChuongTrinh, Guid? idDuAn, bool isDuAn, List<NhQtThongTriQuyetToanChiTietQuery> list, List<NhQtThongTriQuyetToanChiTietQuery> listTittle)
        {
            List<NhQtThongTriQuyetToanChiTietQuery> returnData = new List<NhQtThongTriQuyetToanChiTietQuery>();
            var listLoaiChiPhi = list.Select(x => new { x.iLoaiNoiDungChi }).Distinct().OrderBy(x => x.iLoaiNoiDungChi)
                  .ToList();
            var countLoaiChiPhi = 0;
            foreach (var loaiChiPhi in listLoaiChiPhi)
            {
                countLoaiChiPhi++;
                var listChiPhi = list.Where(x => x.iLoaiNoiDungChi == loaiChiPhi.iLoaiNoiDungChi).ToList();
                var newObjLoaiChiPhi = new NhQtThongTriQuyetToanChiTietQuery()
                {
                    sMaThuTu = countLoaiChiPhi.ToString(),
                    sTenNoiDungChi = loaiChiPhi.iLoaiNoiDungChi == 1 ? "Chi ngoại tệ" : "Chi trong nước",
                    bIsTittle = true,
                    fDeNghiQuyetToanNam_USD = listChiPhi.Sum(x => x.fDeNghiQuyetToanNam_USD),
                    fDeNghiQuyetToanNam_VND = listChiPhi.Sum(x => x.fDeNghiQuyetToanNam_VND),
                    fThuaNopTraNSNN_USD = listChiPhi.Sum(x => x.fThuaNopTraNSNN_USD),
                    fThuaNopTraNSNN_VND = listChiPhi.Sum(x => x.fThuaNopTraNSNN_VND),
                };
                returnData.Add(newObjLoaiChiPhi);

                if (isDuAn)
                {
                    var listNameHopDong = list.Where(x => x.iLoaiNoiDungChi == loaiChiPhi.iLoaiNoiDungChi).Select(x => new { x.sTenHopDong, x.iID_HopDongID }).Distinct()
                    .ToList();
                    var countHopDong = 0;
                    foreach (var nameHopDong in listNameHopDong)
                    {
                        countHopDong++;
                        var newObjHopDongDuAn = new NhQtThongTriQuyetToanChiTietQuery();
                        var findTittle = listTittle.Find(x => x.iID_HopDongID == nameHopDong.iID_HopDongID && x.iID_DuAnID == idDuAn && x.iLoaiNoiDungChi == loaiChiPhi.iLoaiNoiDungChi);
                        if (findTittle != null)
                        {
                            newObjHopDongDuAn = findTittle;
                        }
                        newObjHopDongDuAn.sMaThuTu = countLoaiChiPhi.ToString() + "." + countHopDong.ToString();
                        newObjHopDongDuAn.sTenNoiDungChi = nameHopDong.iID_HopDongID != null ? nameHopDong.sTenHopDong : "Không phát sinh hợp đồng";
                        newObjHopDongDuAn.bIsData = false;
                        newObjHopDongDuAn.bIsTittle = true;
                        newObjHopDongDuAn.iID_ParentID = nameHopDong.iID_HopDongID;
                        newObjHopDongDuAn.iID_KHTT_NhiemVuChiID = idChuongTrinh;
                        newObjHopDongDuAn.iID_HopDongID = nameHopDong.iID_HopDongID;
                        newObjHopDongDuAn.iID_DuAnID = idDuAn;

                        var listHopDong = list.Where(x => x.iLoaiNoiDungChi == loaiChiPhi.iLoaiNoiDungChi && x.iID_HopDongID == nameHopDong.iID_HopDongID).ToList();
                        newObjHopDongDuAn.iID_ThanhToan_ChiTietID = listHopDong.FirstOrDefault().iID_ThanhToan_ChiTietID;
                        newObjHopDongDuAn.fDeNghiQuyetToanNam_USD = listHopDong.Sum(x => x.fDeNghiQuyetToanNam_USD);
                        newObjHopDongDuAn.fDeNghiQuyetToanNam_VND = listHopDong.Sum(x => x.fDeNghiQuyetToanNam_VND);
                        newObjHopDongDuAn.fThuaNopTraNSNN_USD = listHopDong.Sum(x => x.fThuaNopTraNSNN_USD);
                        newObjHopDongDuAn.fThuaNopTraNSNN_VND = listHopDong.Sum(x => x.fThuaNopTraNSNN_VND);
                        returnData.Add(newObjHopDongDuAn);

                        listHopDong.ForEach(x =>
                        {
                            x.bIsData = true;
                        });
                        returnData.AddRange(listHopDong);
                    }
                }
                else
                {
                    var listHopDong = list.Where(x => x.iLoaiNoiDungChi == loaiChiPhi.iLoaiNoiDungChi).ToList();
                    listHopDong.ForEach(x =>
                    {
                        x.bIsData = true;
                    });
                    returnData.AddRange(listHopDong);
                }

            }
            return returnData;
        }

        public void DeleteAllThongTriChiTietByTTId(Guid Id)
        {
            var lstThongChiChiTiet = _repository.FindAll(x => x.iID_ThongTriQuyetToanID == Id).Select(x => new NhQtThongTriQuyetToanChiTiet { Id = x.Id });
            _repository.RemoveRange(lstThongChiChiTiet);
        }

        public IEnumerable<NhQtThongTriQuyetToanChiTietQuery> GetThongTriChiTietByTTQTId(Guid id)
        {
            return _repository.GetThongTriChiTietByTTQTId(id);
        }

        private string ConvertLetter(int input)
        {
            StringBuilder res = new StringBuilder((input - 1).ToString());
            for (int j = 0; j < res.Length; j++)
                res[j] += (char)(17); // '0' is 48, 'A' is 65
            return res.ToString();
        }
        private string ConvertLaMa(decimal num)
        {
            string strRet = string.Empty;
            decimal _Number = num;
            Boolean _Flag = true;
            string[] ArrLama = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] ArrNumber = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            int i = 0;
            while (_Flag)
            {
                while (_Number >= ArrNumber[i])
                {
                    _Number -= ArrNumber[i];
                    strRet += ArrLama[i];
                    if (_Number < 1)
                        _Flag = false;
                }
                i++;
            }
            return strRet;
        }
    }
}
