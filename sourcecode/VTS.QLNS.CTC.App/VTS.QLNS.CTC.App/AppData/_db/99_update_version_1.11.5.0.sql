/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 03/08/2022 8:17:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_list_dexuat_dieuchinh_chitiet]    Script Date: 03/08/2022 8:17:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khv_khth_list_dexuat_dieuchinh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khv_khth_list_dexuat_dieuchinh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_list_dexuat_dieuchinh_chitiet]    Script Date: 03/08/2022 8:17:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khv_khth_list_dexuat_dieuchinh_chitiet]
	@VoucherId nvarchar(255)
AS
BEGIN
	declare @VoucherAgregate uniqueidentifier;
	select @VoucherAgregate = iID_TongHopParent from VDT_KHV_KeHoach5Nam_DeXuat where Id = @VoucherId;

	SELECT DISTINCT
		ctct.STT					AS STT,
		ctct.Id						AS Id,
		ctct.iID_KeHoach5NamID		AS IIdKeHoach5NamId,
		ctctdd.iID_DuAnID			AS iIdDuAnId,
		ctct.sTen					AS STen,
		ctct.SDiaDiem				AS SDiaDiem,
		ctct.IGiaiDoanTu			AS IGiaiDoanTu,
		ctct.IGiaiDoanDen			AS IGiaiDoanDen,
		ctct.iID_LoaiCongTrinhID	AS IIdLoaiCongTrinhId,
		NULL						AS STenLoaiCongTrinh,
		ctct.iID_NguonVonID			AS IIdNguonVonId,
		NULL						AS STenNguonVon,
		ctct.iID_DonViQuanLyID		AS IIdDonViId,
		ctct.iID_MaDonVi			AS IIdMaDonVi,
		NULL						AS STenDonVi,
		ctct.fHanMucDauTu			AS FHanMucDauTu,
		CAST(0 AS float)			AS FVonNSQPLuyKe,
		CAST(0 AS float)			AS FVonNSQP,
		ctct.fGiaTriNamThuNhat		AS FGiaTriNamThuNhat,
		ctct.fGiaTriNamThuHai		AS FGiaTriNamThuHai,
		ctct.fGiaTriNamThuBa		AS FGiaTriNamThuBa,
		ctct.fGiaTriNamThuTu		AS FGiaTriNamThuTu,
		ctct.fGiaTriNamThuNam		AS FGiaTriNamThuNam,
		ctct.fGiaTriBoTri			AS FGiaTriBoTri,
		ctct.fGiaTriKeHoach			AS FGiaTriKeHoach,
		CAST(0 AS float)			AS FTongSoNhuCauNSQP,
		CAST(0 AS float)			AS FGiaTriNamThuNhatOrigin,
		CAST(0 AS float)			AS FGiaTriNamThuHaiOrigin,
		CAST(0 AS float)			AS FGiaTriNamThuBaOrigin,
		CAST(0 AS float)			AS FGiaTriNamThuTuOrigin,
		CAST(0 AS float)			AS FGiaTriNamThuNamOrigin,
		CAST(0 AS float)			AS FGiaTriBoTriOrigin,
		CAST(0 AS float)			AS FGiaTriKeHoachOrigin,
		CAST(0 AS float)			AS FTongSoNhuCauNSQPOrigin,
		ctct.iID_ParentModified		AS IdParentModified,
		ctct.sGhiChu				AS SGhiChu,
		CASE 
			WHEN ctct.IdParent IS NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
		END							AS IsHangCha,
		ctct.Level					AS Level,
		ctct.indexCode				AS IndexCode,
		ctct.SMaOrder				AS SMaOrder,
		ctct.IdReference			AS IdReference,
		ctct.IdParent				AS IdParent,
		ctct.IsParent				AS IsParent,
		ctct.IsStatus				AS IsStatus,
		ctct.sTrangThai				AS STrangThai
  FROM VDT_KHV_KeHoach5Nam_DeXuat as tbl
	INNER JOIN VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet ctct 
		On ctct.iID_KeHoach5NamID = tbl.Id
	INNER JOIN VDT_DA_DuAn da
		ON ctct.Id = da.Id_DuAnKhthDeXuat OR ctct.IdReference = da.Id_DuAnKhthDeXuat		
	INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet ctctdd
		ON ctctdd.iID_DuAnID = da.iID_DuAnID
	WHERE 
		ctct.iID_KeHoach5NamID = @VoucherAgregate
		and ctct.iID_TongHop = @VoucherId
	
END
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_kehoach_chitiet_canbo]    Script Date: 15/12/2021 6:34:37 PM ******/
SET ANSI_NULLS ON
;

select * from VDT_KHV_KeHoach5Nam_ChiTiet
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 03/08/2022 8:17:43 AM ******/
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
GO
