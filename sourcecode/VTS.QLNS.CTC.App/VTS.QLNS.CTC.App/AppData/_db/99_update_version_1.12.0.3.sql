/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 05/10/2022 6:08:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinthanhtoan_index]    Script Date: 05/10/2022 6:08:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtinthanhtoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtinthanhtoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 05/10/2022 6:08:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 05/10/2022 6:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_index] 
	@YearOfWork int,
	@ILoai int
AS
BEGIN
	
	WITH SoLieuDieuChinh AS (
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM NH_DA_DuToan ct 
		WHERE ct.iID_ParentId IS NOT NULL 
		UNION ALL 
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM NH_DA_DuToan ct 
		JOIN SoLieuDieuChinh ctpr ON ct.iID_ParentId = ctpr.ID 
	  WHERE 
		ct.iID_ParentId IS NOT NULL
	), 
	SoLanDieuChinh AS (
		SELECT 
			sdc.Id, 
			sdc.iID_ParentId, 
			COUNT(sdc.Id) AS iSoLanDieuChinh 
		FROM SoLieuDieuChinh sdc 
		GROUP BY 
			sdc.iID_ParentId, 
			sdc.ID
	),
	ThongTinNguonVon AS (
		SELECT 
			nguonVon.iID_DuToanID AS iID_DuToanID, 
			SUM(nguonVon.fGiaTriNgoaiTeKhac) AS fGiaTriNgoaiTeKhac,
			SUM(nguonVon.fGiaTriUSD) AS fGiaTriUSD,
			SUM(nguonVon.fGiaTriVND) AS fGiaTriVND,
			SUM(nguonVon.fGiaTriEUR) AS fGiaTriEUR
		FROM NH_DA_DuToan_NguonVon nguonVon
		GROUP BY 
			nguonVon.iID_DuToanID
	)
	
	SELECT
		duToan.ID AS Id,
		duToan.iID_QDDauTuID AS IIdQdDauTuId,
		duToan.iID_DuAnID AS IIdDuAnId,
		duToan.sSoQuyetDinh AS SSoQuyetDinh,
		duToan.dNgayQuyetDinh AS DNgayQuyetDinh,
		duToan.sMoTa AS SMoTa,
		duToan.sTenChuongTrinh AS STenChuongTrinh,
		duToan.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndId,
		duToan.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurId,
		duToan.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		duToan.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
		duToan.fGiaTriUSD AS FGiaTriUsd,
		duToan.fGiaTriVND AS FGiaTriVnd,
		duToan.fGiaTriEUR AS FGiaTriEur,
		duToan.dNgayTao AS DNgayTao,
		duToan.sNguoiTao AS SNguoiTao,
		duToan.dNgaySua AS DNgaySua,
		duToan.sNguoiSua AS SNguoiSua,
		duToan.dNgayXoa AS DNgayXoa,
		duToan.sNguoiXoa AS SNguoiXoa,
		duToan.bIsActive AS BIsActive,
		duToan.bIsGoc AS BIsGoc,
		duToan.bIsKhoa AS BIsKhoa,
		duToan.bIsXoa AS BIsXoa,
		duToan.iID_DuToanGocID AS IIdDuToanGocId,
		duToan.iID_TiGiaID AS IIdTiGiaId,
		duToan.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		duToan.iID_ParentID AS IIdParentId,
		--donvi.sTenDonVi AS STenDonVi, 
		CONCAT(donvi.iID_MaDonVi, ' - ', donvi.sTenDonVi) AS STenDonVi,
		donvi.iID_MaDonVi AS IIdMaDonViQuanLy,
		CONCAT(duAn.sMaDuAn, ' - ', duAn.sTenDuAn) AS STenDuAn,
		ISNULL(soLieuDieuChinh.iSoLanDieuChinh, 0) AS ILanDieuChinh,
		duToanParent.sSoQuyetDinh AS SDieuChinhTu,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 406 AND ObjectId = duToan.ID) AS TotalFiles
	FROM NH_DA_DuToan duToan		
	LEFT JOIN NH_DA_DuToan duToanParent
		ON duToan.iID_ParentID = duToanParent.ID
	LEFT JOIN DonVi donVi
		ON duToan.iID_MaDonViQuanLy = donVi.iID_MaDonVi AND donVi.iNamLamViec = @YearOfWork
	LEFT JOIN NH_DA_DuAn duAn
		ON duToan.iID_DuAnID = duAn.ID
	LEFT JOIN SoLanDieuChinh soLieuDieuChinh
		ON soLieuDieuChinh.ID = duToan.ID
	LEFT JOIN ThongTinNguonVon nguonVon
		ON nguonVon.iID_DuToanID = duToan.ID
	WHERE duToan.iLoai = @ILoai 
	ORDER BY dNgayQuyetDinh DESC, sSoQuyetDinh DESC
END;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinthanhtoan_index]    Script Date: 05/10/2022 6:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thongtinthanhtoan_index]
	@YearOfWork int,
	@iTrangThai int
AS BEGIN
Select thanhtoanchitiet.iID_DeNghiThanhToanID as IdDeNghiThanhToan
	   ,sum(Isnull(thanhtoanchitiet.fDeNghiCapKyNay_USD,0)) as FTongDeNghiKyNayUsd
	   ,sum(Isnull(thanhtoanchitiet.fDeNghiCapKyNay_VND,0)) as FTongDeNghiKyNayVnd
	   ,sum(Isnull(thanhtoanchitiet.fDeNghiCapKyNay_EUR,0)) as FTongDeNghiKyNayEur
	   ,sum(Isnull(thanhtoanchitiet.fDeNghiCapKyNay_NgoaiTeKhac,0)) as FTongDeNghiKyNayNgoaiTeKhac
	   ,sum(Isnull(thanhtoanchitiet.fPheDuyetCapKyNay_USD,0)) as FTongPheDuyetCapKyNayUsd
	   ,sum(Isnull(thanhtoanchitiet.fPheDuyetCapKyNay_VND,0)) as FTongPheDuyetCapKyNayVnd
	   ,sum(Isnull(thanhtoanchitiet.fPheDuyetCapKyNay_EUR,0)) as FTongPheDuyetCapKyNayEur
	   ,sum(Isnull(thanhtoanchitiet.fPheDuyetCapKyNay_NgoaiTeKhac,0)) as FTongPheDuyetCapKyNayNgoaiTeKhac
	   into #tempThanhToanChiTiet
from NH_TT_ThanhToan_ChiTiet thanhtoanchitiet
group by thanhtoanchitiet.iID_DeNghiThanhToanID;


SELECT thanhtoan.ID as Id
      ,thanhtoan.iID_DonViCapTren as IIdDonViCapTren
      ,thanhtoan.iID_MaDonViCapTren as IIdMaDonViCapTren
      ,thanhtoan.iID_DonVi as IIdDonVi
      ,thanhtoan.iID_MaDonVi as IIdMaDonVi
      ,thanhtoan.sSoDeNghi as SSoDeNghi
      ,thanhtoan.dNgayDeNghi as DNgayDeNghi
      ,thanhtoan.sKinhGui as SKinhGui
      ,thanhtoan.iID_KHTongTheID as IIdKhtongTheId
      ,thanhtoan.iID_NhiemVuChiID as IIdNhiemVuChiId
      ,thanhtoan.iID_ChuDauTuID as IIdChuDauTuId
      ,thanhtoan.iID_MaChuDauTu as IIdMaChuDauTu
      ,thanhtoan.iID_HopDongID as IIdHopDongId
      ,thanhtoan.sCanCu as SCanCu
      ,thanhtoan.fSoDuTamUng as FSoDuTamUng
      ,thanhtoan.iLoaiDeNghi as ILoaiDeNghi
      ,thanhtoan.iNamKeHoach as INamKeHoach
      ,thanhtoan.iID_NguonVonID as IIdNguonVonId
      ,thanhtoan.iID_TiGiaID as IIdTiGiaId
      ,thanhtoan.sMaNgoaiTeKhac as SMaNgoaiTeKhac
      ,thanhtoan.iLoaiNoiDungChi as ILoaiNoiDungChi
      ,thanhtoan.fTongDeNghi_BangSo as FTongDeNghiBangSo
      ,thanhtoan.sTongDeNghi_BangChu as STongDeNghiBangChu
      ,thanhtoan.fThuHoiTamUng_BangSo as FThuHoiTamUngBangSo
      ,thanhtoan.fThuHoiTamUng_BangChu as FThuHoiTamUngBangChu
      ,thanhtoan.fTraDonViThuHuong_BangSo as FTraDonViThuHuongBangSo
      ,thanhtoan.fTraDonViThuHuong_BangChu as FTraDonViThuHuongBangChu
      ,thanhtoan.iID_NhaThauID as IIdNhaThauId
      ,thanhtoan.fChuyenKhoan_BangSo as FChuyenKhoanBangSo
      ,thanhtoan.sChuyenKhoan_BangChu as SChuyenKhoanBangChu
      ,thanhtoan.fTienMat_BangSo as FTienMatBangSo
      ,thanhtoan.sTienMat_BangChu as STienMatBangChu
      ,thanhtoan.iID_NhaThau_NguoiNhanID as IIdNhaThauNguoiNhanId
      ,thanhtoan.sTruongPhong as STruongPhong
      ,thanhtoan.sThuTruongDonVi as SThuTruongDonVi
      ,thanhtoan.sNguoiTao as SNguoiTao
      ,thanhtoan.dNgayTao as DNgayTao
      ,thanhtoan.sNguoiSua as SNguoiSua
      ,thanhtoan.dNgaySua as DNgaySua
      ,thanhtoan.sNguoiXoa as SNguoiXoa
      ,thanhtoan.dNgayXoa as DNgayXoa
	  ,thanhtoan.sSoTaiKhoan as SSoTaiKhoan
	  ,thanhtoan.sNganHang as SNganHang
	  ,thanhtoan.sNguoiLienHe as SNguoiLienHe
	  ,thanhtoan.sNoiCapCMND as SNoiCapCMND
	  ,thanhtoan.dNgayCapCMND as DNgayCapCMND
	  ,thanhtoan.sSoCMND as SSoCMND
      ,thanhtoan.bIsKhoa as BIsKhoa
      ,thanhtoan.bIsXoa as BIsXoa
      ,thanhtoan.iTrangThai as ITrangThai
      ,thanhtoan.iID_NhaThau_NganHangID as IIdNhaThauNganHangId
	  ,CONCAT(donvi.iID_MaDonVi, ' - ', donvi.sTenDonVi) AS STenDonViMaDonVi
	  ,nhiemvuchi.sTenNhiemVuChi As STenNhiemVuChi
	  ,CONCAT(hopdong.sSoHopDong, ' - ', hopdong.sTenHopDong) AS STenHopDongSoHopDong
	  ,nguonns.sTen as TenNguonVon
	  ,thanhtoanchitiet.FTongDeNghiKyNayUsd as FTongDeNghiKyNayUsd
	  ,thanhtoanchitiet.FTongDeNghiKyNayVnd as FTongDeNghiKyNayVnd
	  ,thanhtoanchitiet.FTongDeNghiKyNayEur as FTongDeNghiKyNayEur
	  ,thanhtoanchitiet.FTongDeNghiKyNayNgoaiTeKhac as FTongDeNghiKyNayNgoaiTeKhac
	  ,thanhtoanchitiet.FTongPheDuyetCapKyNayUsd as FTongPheDuyetCapKyNayUsd
	  ,thanhtoanchitiet.FTongPheDuyetCapKyNayVnd as FTongPheDuyetCapKyNayVnd
	  ,thanhtoanchitiet.FTongPheDuyetCapKyNayEur as FTongPheDuyetCapKyNayEur
	  ,thanhtoanchitiet.FTongPheDuyetCapKyNayNgoaiTeKhac as FTongPheDuyetCapKyNayNgoaiTeKhac
	  ,thanhtoan.dNgayPheDuyet as DNgayPheDuyet
	  ,(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 411 AND ObjectId = thanhtoan.ID) AS TotalFiles,
	  thanhtoan.iID_Parent as ParentId,
	  thanhtoan.bTongHop as BTongHop,
	  thanhtoan.iCoQuanThanhToan as ICoQuanThanhToan
  FROM NH_TT_ThanhToan thanhtoan
  left join DonVi donvi on donvi.iID_DonVi = thanhtoan.iID_DonVi
  left join NH_DM_NhiemVuChi nhiemvuchi on nhiemvuchi.ID = thanhtoan.iID_NhiemVuChiID
  left join NH_DA_HopDong hopdong on hopdong.Id = thanhtoan.iID_HopDongID
  left join NguonNganSach nguonns on thanhtoan.iID_NguonVonID = nguonns.iID_MaNguonNganSach 
  left join #tempThanhToanChiTiet thanhtoanchitiet on thanhtoanchitiet.IdDeNghiThanhToan = thanhtoan.ID
  where (@iTrangThai = -1 OR thanhtoan.iTrangThai = @iTrangThai) and (bTongHop is null or bTongHop != 1)
  order by thanhtoan.dNgayTao desc;

drop table #tempThanhToanChiTiet;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 05/10/2022 6:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 21/04/2022
-- Description:	Lấy dữ liệu cho thêm mới bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@SoNgay int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    WITH SoLieuTienAn AS (
		SELECT
			MA_CBO MaCanBo,
			SUM (
				CASE
					--WHEN MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG') THEN ISNULL(HuongPC_SN, 0) * GIA_TRI
					WHEN MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN 30 * GIA_TRI 
					ELSE ISNULL(HuongPC_SN, 0) * GIA_TRI
				END
			) GiaTri
		FROM TL_CanBo_PhuCap canBoPhuCap
		--WHERE
			--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		WHERE 
			canBoPhuCap.MA_PHUCAP IN (SELECT Ma_PhuCap FROM TL_DM_PhuCap WHERE PARENT LIKE 'TIENAN')
		GROUP BY canBoPhuCap.MA_CBO
	),
	SoLieuTienAn2 AS (
		SELECT
			MA_CBO MaCanBo,
			SUM (
				CASE
					--WHEN MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG') THEN ISNULL(HuongPC_SN, 0) * GIA_TRI
					WHEN MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN 30 * GIA_TRI 
					ELSE ISNULL(HuongPC_SN, 0) * GIA_TRI
				END
			) GiaTri
		FROM TL_CanBo_PhuCap canBoPhuCap
		--WHERE
			--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		WHERE 
			canBoPhuCap.MA_PHUCAP IN (SELECT Ma_PhuCap FROM TL_DM_PhuCap WHERE PARENT LIKE 'TIENAN2')
		GROUP BY canBoPhuCap.MA_CBO
	), 
	ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_Cb MaCapBac,
			canBo.BHTN,
			canBo.PCCV
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER  JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.Thang = @Thang
			AND canBo.Nam = @Nam
			AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
	)

	SELECT
		newid()					AS Id,
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in (1, 2, 3)) THEN 0 
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTri
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT2' THEN soLieuTienAn2.GiaTri
			WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
			ELSE canBoPhuCap.GIA_TRI
		END						AS GiaTri,
		canBo.MaHieuCanBo		AS MaHieuCanBo,
		canBo.MaCapBac			AS MaCb,
		canBoPhuCap.HuongPC_SN	AS HuongPcSn,
		cachTinhLuong.CongThuc	AS CongThuc,
		CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	FROM TL_CanBo_PhuCap canBoPhuCap
	INNER JOIN ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN SoLieuTienAn soLieuTienAn
		ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	LEFT JOIN SoLieuTienAn2 soLieuTienAn2
		ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	LEFT JOIN TL_DM_Cach_TinhLuong_Chuan cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb
END
;
GO



INSERT INTO TL_DM_PhuCap(Ma_PhuCap, Parent, Ten_PhuCap, Xau_Noi_Ma, bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Cong_Thuc, Dinh_Dang, Gia_Tri, He_So, HuongPC_SN, iDinhDang, iLoai, Is_Formula, Is_Readonly, IThang_ToiDa, Ma_KMCP, Ma_TTM_Ng, Numeric_Scale, PhanTram_CT, Readonly, Splits, Ten_Ngan, Tinh_BHXH, Tinh_TNCN, XSort)
SELECT 'TIENAN2', '', N'Tiền ăn 2', '-TA2', bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Cong_Thuc, Dinh_Dang, Gia_Tri, He_So, HuongPC_SN, iDinhDang, iLoai, Is_Formula, Is_Readonly, IThang_ToiDa, Ma_KMCP, Ma_TTM_Ng, Numeric_Scale, PhanTram_CT, Readonly, Splits, Ten_Ngan, Tinh_BHXH, Tinh_TNCN, XSort
FROM TL_DM_PhuCap
WHERE Ma_PhuCap = 'TIENAN'

INSERT INTO TL_DM_PhuCap(Ma_PhuCap, Parent, Ten_PhuCap, Xau_Noi_Ma, bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Cong_Thuc, Dinh_Dang, Gia_Tri, He_So, HuongPC_SN, iDinhDang, iLoai, Is_Formula, Is_Readonly, IThang_ToiDa, Ma_KMCP, Ma_TTM_Ng, Numeric_Scale, PhanTram_CT, Readonly, Splits, Ten_Ngan, Tinh_BHXH, Tinh_TNCN, XSort)
SELECT 'TA_TT2', 'SUM', N'Tổng tiền ăn bị trừ đầu tháng 2', 'SUM-TA_TT2', bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Cong_Thuc, Dinh_Dang, Gia_Tri, He_So, HuongPC_SN, iDinhDang, iLoai, Is_Formula, Is_Readonly, IThang_ToiDa, Ma_KMCP, Ma_TTM_Ng, Numeric_Scale, PhanTram_CT, Readonly, Splits, Ten_Ngan, Tinh_BHXH, Tinh_TNCN, XSort
FROM TL_DM_PhuCap
WHERE Ma_PhuCap = 'TA_TT'

INSERT INTO [dbo].[NS_CauHinh_CanCu]
           ([iID_CauHinh_CanCu]
           ,[bChinhSua]
           ,[iID_MaChucNang]
           ,[iNamCanCu]
           ,[iNamLamViec]
           ,[iThietLap]
           ,[sModule]
           ,[sTenCot])
     VALUES
          (newid(),
      1,
      'BUDGET_ESTIMATE',
      2022 ,
      2023 ,
      1 ,
      'BUDGET_DEMANDCHECK_PLAN',
      N'Dự toán năm 2022')
INSERT INTO [dbo].[NS_CauHinh_CanCu]
           ([iID_CauHinh_CanCu]
           ,[bChinhSua]
           ,[iID_MaChucNang]
           ,[iNamCanCu]
           ,[iNamLamViec]
           ,[iThietLap]
           ,[sModule]
           ,[sTenCot])
       values
       (newid(),
       1,
       'BUDGET_SETTLEMENT'
       ,2021,
       2023,
       2,
       'BUDGET_DEMANDCHECK_PLAN',
       N'Thực hiện năm 2021'
       )
