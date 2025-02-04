/****** Object:  StoredProcedure [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]    Script Date: 24/11/2023 5:51:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_tonghopkhoitaothongtinduan]    Script Date: 24/11/2023 5:51:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_delete_tonghopkhoitaothongtinduan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_delete_tonghopkhoitaothongtinduan]
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_tonghopkhoitaothongtinduan]    Script Date: 24/11/2023 5:51:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_delete_tonghopkhoitaothongtinduan]
@iId uniqueidentifier
AS
BEGIN
	-- insert ban ghi revert
	INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTri, bIsLog, iStatus, sMaTienTrinh, iThuHoiTUCheDo, iLoaiUng, bKeHoach, iID_LoaiCongTrinh)	
	SELECT tbl.iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTri, 1, 2, '300', tbl.iThuHoiTUCheDo, iLoaiUng, bKeHoach, tbl.iID_LoaiCongTrinh
	FROM VDT_TongHop_NguonNSDauTu as tbl
	WHERE tbl.iID_ChungTu = @iId
	AND bIsLog = 0
	AND sMaTienTrinh COLLATE DATABASE_DEFAULT in ('100') 

	-- khoa but toan da update
	UPDATE tbl 
	SET 
		bIsLog = 1
	FROM VDT_TongHop_NguonNSDauTu as tbl
	WHERE tbl.iID_ChungTu = @iId
		AND bIsLog = 0
		AND sMaTienTrinh COLLATE DATABASE_DEFAULT in ('100') 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]    Script Date: 24/11/2023 5:51:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]
	@IdDonVi nvarchar(max)
AS
BEGIN
	SELECT DISTINCT dt.iID_DuAnID INTO #tmpDuAnKHTH
	FROM VDT_KHV_KeHoach5Nam as tbl
	INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet as dt on tbl.iID_KeHoach5NamID = dt.iID_KeHoach5NamID
	WHERE tbl.bActive = 1

	SELECT
			duan.iID_DuAnID AS IDDuAnID,
			duan.sMaDuAn as SMaDuAn,
			duan.sTenDuAn AS STenDuAn,
			duan.sDiaDiem AS SDiaDiem,
			CAST(duan.sKhoiCong AS int) AS IGiaiDoanTu,
			CAST(duan.sKetThuc AS int) AS IGiaiDoanDen,
			duan.fHanMucDauTu AS FHanMucDauTu,
			donvi.iID_DonVi AS IIdDonViId,
			donvi.iID_MaDonVi AS IIDMaDonVi,
			donvi.sTenDonVi AS STenDonVi,
			null AS IIDLoaiCongTrinhID,
			'' AS STenLoaiCongTrinh,
			null AS IIDNguonVonID,
			'' AS STenNguonVon
		FROM VDT_DA_DuAn duan
		INNER JOIN #tmpDuAnKHTH as khth on duan.iID_DuAnID = khth.iID_DuAnID
		LEFT JOIN VDT_DM_DonViThucHienDuAn donvi
			ON duan.iID_DonViThucHienDuAnID  = donvi.iID_DonVi
		WHERE
			1=1
			--AND duan.sTrangThaiDuAn = 'THUC_HIEN'
			AND duan.bIsKetThuc IS NULL
			AND iID_MaDonViQuanLy = @IdDonVi
		ORDER BY duan.dDateCreate			--them ngay 13/8
	DROP TABLE #tmpDuAnKHTH
END
;
;
GO
