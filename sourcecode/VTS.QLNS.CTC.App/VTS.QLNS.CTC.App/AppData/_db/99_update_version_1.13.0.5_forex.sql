/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinduan_index]    Script Date: 7/26/2023 8:09:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtinduan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtinduan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]    Script Date: 7/26/2023 8:09:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_goithau_index]    Script Date: 7/26/2023 8:09:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_goithau_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_goithau_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hdnk_cacquyetdinh_index]    Script Date: 7/26/2023 8:09:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_hdnk_cacquyetdinh_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_hdnk_cacquyetdinh_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hdnk_cacquyetdinh_index]    Script Date: 7/26/2023 8:09:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_hdnk_cacquyetdinh_index] 
@iLoai int = NULL
AS 
	BEGIN 
	SELECT
	cqd.ID AS Id,
		cqd.sSoQuyetDinh AS SSoQuyetDinh,
		cqd.dNgayQuyetDinh AS DNgayQuyetDinh,
		cqd.sMoTaChiTiet_QuyetDinh AS SMoTaChiTietQuyetDinh,
		cqd.iID_TiGiaID AS IIdTiGiaId,
		cqd.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
		cqd.iID_DuAnID AS IIdDuAnId,
		cqd.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		cqd.iLoaiQuyetDinh AS ILoaiQuyetDinh,
		cqd.iID_DonViThucHien AS IIdDonViThucHien,
		cqd.iID_DonViQuanLy AS IIdDonViQuanLy,
		cqd.fGiaTriUSD AS FGiaTriUSD,
		cqd.fGiaTriVND AS FGiaTriVND,
		cqd.fGiaTriEUR AS FGiaTriEUR,
		cqd.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
		cqd.iID_GocID AS IIdGocId,
		cqd.dNgayTao AS DNgayTao,
		cqd.sNguoiTao AS SNguoiTao,
		cqd.dNgaySua AS DNgaySua,
		cqd.sNguoiSua AS SNguoiSua,
		cqd.dNgayXoa AS DNgayXoa,
		cqd.sNguoiXoa AS SNguoiXoa,
		cqd.bIsActive AS BIsActive,
		cqd.bIsGoc AS BIsGoc,
		cqd.bIsKhoa AS BIsKhoa,
		cqd.iLanDieuChinh AS ILanDieuChinh,
		cqd.bIsXoa AS BIsXoa,
		cqd.iID_ParentId AS IIdParentId,
		cqd.iID_ParentAdjustId AS IIdParentAdjustId,
		cqd.iLoai AS ILoai,
		cqd.fTiGiaNhap AS FTiGiaNhap,
		CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
		nvChi.STenChuongTrinh,
		nvChi.iID_KHTongTheID AS IIdKhTongTheId,
		cqd.iID_PhuongAnNhapKhauID AS IIdPhuongAnNhapKhauId,
		CASE
			WHEN paNhapKhau.sMoTa IS NULL THEN paNhapKhau.sSoQuyetDinh
			ELSE CONCAT(paNhapKhau.sSoQuyetDinh, ' - ', paNhapKhau.sMoTa)
		END SPhuongAnNhapKhau,
		CASE
			WHEN cqd.iID_ParentAdjustId IS NULL THEN
			'' ELSE ( SELECT TOP 1 cqdpr.sSoQuyetDinh FROM NH_HDNK_CacQuyetDinh cqdpr WHERE cqdpr.Id = cqd.iID_ParentAdjustId ) 
		END DieuChinhTu,
		(CASE
		WHEN da.sTenDuAn IS NULL THEN ''
		WHEN da.sMaDuAn IS NULL THEN da.sTenDuAn
		ELSE CONCAT(da.sMaDuAn, ' - ', da.sTenDuAn)
		END) AS STenDuAn
FROM
	NH_HDNK_CacQuyetDinh cqd
LEFT JOIN DonVi donVi
ON cqd.iID_DonViQuanLy = donVi.iID_DonVi AND cqd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN
		(SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh
		 FROM NH_KHTongThe_NhiemVuChi AS n 
		 INNER JOIN NH_DM_NhiemVuChi AS d 
		 ON n.iID_NhiemVuChiID = d.ID) 
AS nvChi
ON cqd.iID_KHTongThe_NhiemVuChiID = nvChi.ID
LEFT JOIN NH_HDNK_PhuongAnNhapKhau paNhapKhau
ON cqd.iID_PhuongAnNhapKhauID = paNhapKhau.ID
LEFT JOIN NH_DA_DuAn da ON da.ID = cqd.iID_DuAnID
WHERE @iLoai IS NULL OR cqd.iLoai = @iLoai
ORDER BY
	cqd.dNgayTao DESC END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_goithau_index]    Script Date: 7/26/2023 8:09:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_thongtin_hopdong_goithau_index]
@iThuocMenu INT = NULL
AS
BEGIN
SELECT DISTINCT
	hd.Id,
	hd.sSoHopDong AS SSoHopDong,
	hd.dNgayHopDong AS DNgayHopDong,
	hd.sTenHopDong AS STenHopDong,
	hd.dKhoiCongDuKien AS DKhoiCongDuKien,
	hd.dKetThucDuKien AS DKetThucDuKien,
	hd.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
	hd.iID_LoaiHopDongID AS IIdLoaiHopDongId,
	hd.iID_ParentAdjustID AS IIdParentAdjustId,
	hd.iID_GoiThauID AS IIdGoiThauId,
	hd.iID_ParentID AS IIdParentId,
	hd.iID_NhaThauThucHienID AS IIdNhaThauThucHienId,
	hd.iID_KeHoachDatHangID AS IIdKeHoachDatHangId,
	hd.iID_TiGiaID AS IIdTiGiaId,
	hd.iID_DuAnID AS IIdDuAnId,
	hd.iLoai AS ILoai,
	hd.iThuocMenu AS IThuocMenu,
	hd.iThoiGianThucHien AS IThoiGianThucHien,
	hd.fGiaTriHopDongUSD AS FGiaTriHopDongUSD,
	hd.fGiaTriHopDongVND AS FGiaTriHopDongVND,
	hd.fGiaTriHopDongEUR AS FGiaTriHopDongEUR,
	hd.fGiaTriHopDongNgoaiTeKhac AS FGiaTriHopDongNgoaiTeKhac,
	hd.fTiGiaNhap AS FTiGiaNhap,
	gtnt.fGiaTRiHopDong_USD AS FGiaTriUsd,
	gtnt.fGiaTRiHopDong_VND AS FGiaTriVnd,
	gtnt.fGiaTRiHopDong_EUR AS FGiaTriEur,
	gtnt.fGiaTriHopDong_NgoaiTeKhac AS FGiaTriNgoaiTeKhac,
	hd.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
	hd.sNguoiTao AS SNguoiTao,
	hd.sNguoiSua AS SNguoiSua,
	hd.sNguoiXoa AS SNguoiXoa,
	hd.dNgaySua AS DNgaySua,
	hd.dNgayTao AS DNgayTao,
	hd.dNgayXoa AS DNgayXoa,
	hd.bIsActive AS BIsActive,
	hd.bIsGoc AS BIsGoc,
	hd.bIsKhoa AS BIsKhoa,
	lhd.sTenLoaiHopDong AS SLoaiHopDong,
	hd.iLanDieuChinh AS ILanDieuChinh,
	nvChi.iID_KHTongTheID AS IIdKhTongTheId,
	nvChi.STenChuongTrinh AS STenChuongTrinh,
	(CASE
		WHEN da.sTenDuAn IS NULL THEN ''
		WHEN da.sMaDuAn IS NULL THEN da.sTenDuAn
		ELSE CONCAT(da.sMaDuAn, ' - ', da.sTenDuAn)
		END) AS STenDuAn,
	CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
	hd.iID_DonViQuanLyID AS IIdDonViQuanLyId,
	hd.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
	lhd.sMaLoaiHopDong AS SMaLoaiHopDong,
	nt.sMaNhaThau AS SMaNhaThauThucHien,
	hd.sHinhThucHopDong AS SHinhThucHopDong,
	da.sMaDuAn AS SMaDuAn,
	CASE WHEN hd.iID_ParentAdjustId IS NULL THEN '' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hd.iID_ParentAdjustId) END DieuChinhTu
FROM NH_DA_HopDong hd
LEFT JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
LEFT JOIN NH_DA_DuAn da on hd.iID_DuAnID = da.ID
LEFT JOIN DonVi donVi ON hd.iID_DonViQuanLyID = donVi.iID_DonVi AND hd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN NH_DM_NhaThau nt ON hd.iID_NhaThauThucHienID = nt.Id
LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
) AS nvChi ON hd.iID_KHTongThe_NhiemVuChiID = nvChi.ID
LEFT JOIN (SELECT iID_HopDongID,
	SUM(fGiaTRiHopDong_USD) AS fGiaTRiHopDong_USD,
	SUM(fGiaTRiHopDong_VND) AS fGiaTRiHopDong_VND,
	SUM(fGiaTRiHopDong_EUR) AS fGiaTRiHopDong_EUR,
	SUM(fGiaTriHopDong_NgoaiTeKhac) AS fGiaTriHopDong_NgoaiTeKhac
	FROM NH_DA_HopDong_GoiThau_NhaThau 
	WHERE NH_DA_HopDong_GoiThau_NhaThau.isCheck=1
	GROUP BY iID_HopDongID
) gtnt ON hd.ID = gtnt.iID_HopDongID
WHERE @iThuocMenu IS NULL OR hd.iThuocMenu = @iThuocMenu
ORDER BY hd.dNgayTao DESC
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]    Script Date: 7/26/2023 8:09:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]
@iThuocMenu INT = NULL
AS
BEGIN
SELECT DISTINCT
	hd.Id,
	hd.sSoHopDong AS SSoHopDong,
	hd.dNgayHopDong AS DNgayHopDong,
	hd.sTenHopDong AS STenHopDong,
	hd.dKhoiCongDuKien AS DKhoiCongDuKien,
	hd.dKetThucDuKien AS DKetThucDuKien,
	hd.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
	hd.iID_LoaiHopDongID AS IIdLoaiHopDongId,
	hd.iID_ParentAdjustID AS IIdParentAdjustId,
	hd.iID_GoiThauID AS IIdGoiThauId,
	hd.iID_ParentID AS IIdParentId,
	hd.iID_NhaThauThucHienID AS IIdNhaThauThucHienId,
	hd.iID_TiGiaID AS IIdTiGiaId,
	hd.iID_DuAnID AS IIdDuAnId,
	hd.iLoai AS ILoai,
	hd.fGiaTriHopDongUSD AS FGiaTriUsd,
	hd.fGiaTriHopDongVND AS FGiaTriVnd,
	hd.fGiaTriHopDongEUR AS FGiaTriEur,
	hd.fGiaTriHopDongNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
	hd.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
	hd.sNguoiTao AS SNguoiTao,
	hd.sNguoiSua AS SNguoiSua,
	hd.sNguoiXoa AS SNguoiXoa,
	hd.dNgaySua AS DNgaySua,
	hd.dNgayTao AS DNgayTao,
	hd.dNgayXoa AS DNgayXoa,
	hd.bIsActive AS BIsActive,
	hd.bIsGoc AS BIsGoc,
	hd.bIsKhoa AS BIsKhoa,
	hd.fTiGiaNhap as FTiGiaNhap,
	lhd.sTenLoaiHopDong AS SLoaiHopDong,
	hd.iLanDieuChinh AS ILanDieuChinh,
	nvChi.iID_KHTongTheID AS IIdKhTongTheId,
	nvChi.STenChuongTrinh AS STenChuongTrinh,
	(CASE
		WHEN da.sTenDuAn IS NULL THEN ''
		WHEN da.sMaDuAn IS NULL THEN da.sTenDuAn
		ELSE CONCAT(da.sMaDuAn, ' - ', da.sTenDuAn)
	END) AS STenDuAn,
	CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
	hd.iID_DonViQuanLyID AS IIdDonViQuanLyId,
	hd.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
	CASE
		WHEN hd.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hd.iID_ParentAdjustId ) 
	END DieuChinhTu
	
FROM NH_DA_HopDong hd
LEFT JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
LEFT JOIN NH_DA_DuAn da on hd.iID_DuAnID = da.ID
LEFT JOIN DonVi donVi
ON hd.iID_DonViQuanLyID = donVi.iID_DonVi AND hd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN
		(SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
		 FROM NH_KHTongThe_NhiemVuChi AS n 
		 INNER JOIN NH_DM_NhiemVuChi AS d 
		 ON n.iID_NhiemVuChiID = d.ID) 
AS nvChi
ON hd.iID_KHTongThe_NhiemVuChiID = nvChi.ID
WHERE @iThuocMenu IS NULL OR hd.iThuocMenu = @iThuocMenu
ORDER BY
	hd.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtinduan_index]    Script Date: 7/26/2023 8:09:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thongtinduan_index]
	@iLoai int
AS BEGIN
	SELECT 
		duAn.ID AS Id,
		duAn.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		duAn.sMaDuAn AS SMaDuAn,
		CONCAT(duAn.sMaDuAn, ' - ',duAn.sTenDuAn ) AS STenDuAn,
		duAn.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		duAn.iID_ChuDauTuID AS IIdChuDauTuId,
		duAn.iID_MaChuDauTu AS IIdMaChuDauTu,
		duAn.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		duAn.iID_CapPheDuyetID AS IIdCapPheDuyetId,
		duAn.sKhoiCong AS SKhoiCong,
		duAn.sKetThuc AS SKetThuc,
		duAn.bIsDuPhong AS BIsDuPhong,
		duAn.sDiaDiem AS SDiaDiem,
		duAn.sMucTieu AS SMucTieu,
		duAn.sQuyMo AS SQuyMo,
		duAn.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurid,
		duAn.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndid,
		duAn.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		duAn.fUSD AS FUsd,
		duAn.fVND AS FVnd,
		duAn.fEUR AS FEur,
		duAn.fNgoaiTeKhac AS FNgoaiTeKhac,
		duAn.dNgayTao AS DNgayTao,
		duAn.sNguoiTao AS SNguoiTao,
		duAn.dNgaySua AS DNgaySua,
		duAn.sNguoiSua AS SNguoiSua,
		duAn.dNgayXoa AS DNgayXoa,
		duAn.sNguoiXoa AS SNguoiXoa,
		duAn.iID_ChuTruongDauTuID AS IIdChuTruongDauTuId,
		duAn.iID_TiGiaID AS IIdTiGiaID,
		duAn.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		CONCAT(DonVi.iID_MaDonVi, ' - ', DonVi.sTenDonVi) AS STenDonVi,
		pheDuyet.sTen AS STenPheDuyet,
		DM_ChuDauTu.sTenDonVi AS STenChuDauTu,
		( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 409 AND ObjectId = duAn.ID ) AS TotalFiles
	FROM NH_DA_DuAn duAn
	LEFT JOIN DonVi 
		ON DonVi.iID_DonVi = duAn.iID_DonViQuanLyID
	LEFT JOIN DM_ChuDauTu 
		ON DM_ChuDauTu.iID_DonVi = duAn.iID_ChuDauTuID
	LEFT JOIN NH_DM_PhanCapPheDuyet AS pheDuyet
		on pheDuyet.ID = duAn.iID_CapPheDuyetID
	WHERE duAn.iLoai = @iLoai
END
;
GO
