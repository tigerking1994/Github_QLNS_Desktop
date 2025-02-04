
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 13/07/2022 2:37:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_phuongannhapkhau_index]    Script Date: 13/07/2022 2:37:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_phuongannhapkhau_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_phuongannhapkhau_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hdnk_cacquyetdinh_index]    Script Date: 13/07/2022 2:37:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_hdnk_cacquyetdinh_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_hdnk_cacquyetdinh_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hdnk_cacquyetdinh_index]    Script Date: 13/07/2022 2:37:23 PM ******/
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
		END DieuChinhTu
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
WHERE @iLoai IS NULL OR cqd.iLoai = @iLoai
ORDER BY
	cqd.dNgayTao DESC END;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_phuongannhapkhau_index]    Script Date: 13/07/2022 2:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_phuongannhapkhau_index]
@iLoai INT = NULL
AS
BEGIN
	SELECT
		paNhapKhau.ID AS Id,
		paNhapKhau.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		paNhapKhau.iID_QDDauTuID AS IIdQddauTuId,
		paNhapKhau.iID_ChuTruongDauTuID AS IIdChuTruongDauTuId,
		paNhapKhau.iID_DuAnID AS IIdDuAnId,
		paNhapKhau.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		paNhapKhau.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		paNhapKhau.sSoQuyetDinh AS SSoQuyetDinh,
		paNhapKhau.dNgayQuyetDinh AS DNgayQuyetDinh,
		paNhapKhau.sMoTa AS SMoTa,
		paNhapKhau.iID_TiGiaID AS IIdTiGiaId,
		paNhapKhau.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		paNhapKhau.iID_PhuongAnNhapKhauGocID AS IIdPhuongAnNhapKhauGocId,
		paNhapKhau.dNgayTao AS DNgayTao,
		paNhapKhau.sNguoiTao AS SNguoiTao,
		paNhapKhau.dNgaySua AS DNgaySua,
		paNhapKhau.sNguoiSua AS SNguoiSua,
		paNhapKhau.dNgayXoa AS DNgayXoa,
		paNhapKhau.sNguoiXoa AS SNguoiXoa,
		paNhapKhau.bIsActive AS BIsActive,
		paNhapKhau.bIsKhoa AS BIsKhoa,
		ISNULL(paNhapKhau.iLanDieuChinh, 0) AS ILanDieuChinh,
		paNhapKhau.bIsGoc AS BIsGoc,
		paNhapKhau.iID_ParentID AS IIdParentId,
		paNhapKhau.bIsXoa AS BIsXoa,
		paNhapKhau.iLoai AS ILoai,
		paNhapKhau.sLoaiSoCu AS SLoaiSoCu,
		CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
		nvChi.STenChuongTrinh,
		nvChi.iID_KHTongTheID AS IIdKhTongTheId,
		paNhapKhauParent.sSoQuyetDinh AS SDieuChinhTu
	FROM NH_HDNK_PhuongAnNhapKhau paNhapKhau
	LEFT JOIN NH_HDNK_PhuongAnNhapKhau paNhapKhauParent
	ON paNhapKhau.iID_ParentID = paNhapKhauParent.ID
	LEFT JOIN DonVi donVi
	ON paNhapKhau.iID_DonViQuanLyID = donVi.iID_DonVi AND paNhapKhau.iID_MaDonViQuanLy = donVi.iID_MaDonVi
	LEFT JOIN
		(SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh
		 FROM NH_KHTongThe_NhiemVuChi AS n 
		 INNER JOIN NH_DM_NhiemVuChi AS d 
		 ON n.iID_NhiemVuChiID = d.ID) 
	AS nvChi
	ON paNhapKhau.iID_KHTT_NhiemVuChiID = nvChi.ID
	WHERE (@iLoai IS NULL) OR (paNhapKhau.iLoai = @iLoai)
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 13/07/2022 2:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
@iThuocMenu INT = NULL
AS
BEGIN
SELECT 
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
	hd.fGiaTriUSD AS FGiaTriUsd,
	hd.fGiaTriVND AS FGiaTriVnd,
	hd.fGiaTriEUR AS FGiaTriEur,
	hd.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
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
	CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
	hd.iID_DonViQuanLyID AS IIdDonViQuanLyId,
	hd.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
	CASE
		WHEN hd.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hd.iID_ParentAdjustId ) 
	END DieuChinhTu
	
FROM NH_DA_HopDong hd
LEFT JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
LEFT JOIN DonVi donVi
ON hd.iID_DonViQuanLyID = donVi.iID_DonVi AND hd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN
		(SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh
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
