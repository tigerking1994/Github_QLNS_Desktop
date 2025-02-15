/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_index_find_all]    Script Date: 7/24/2023 10:28:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_kehoach5nam_index_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_kehoach5nam_index_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_index_find_all]    Script Date: 7/24/2023 10:28:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_kehoach5nam_index_find_all]
	
AS
BEGIN
	--So lan dieu chinh
	WITH SoLieuDieuChinh AS 
	 (
		SELECT 
			ct.iID_KeHoach5NamID, ct.iID_ParentId, ct.sSoQuyetDinh, ct.NamLamViec
		FROM 
			VDT_KHV_KeHoach5Nam ct 
		WHERE 
			ct.iID_ParentId is not null

		UNION ALL

		SELECT 
			ct.iID_KeHoach5NamID, ct.iID_ParentId, ct.sSoQuyetDinh, ct.NamLamViec
		FROM 
			VDT_KHV_KeHoach5Nam ct JOIN SoLieuDieuChinh ctpr ON ct.iID_ParentId = ctpr.iID_KeHoach5NamID
		WHERE ct.iID_ParentId is not null
	  ),SoLanDieuChinh AS (
		   SELECT 
			sdc.iID_ParentId, sdc.NamLamViec, COUNT(sdc.iID_KeHoach5NamID) AS iSoLanDieuChinh
		  FROM 
			SoLieuDieuChinh sdc
		  GROUP BY sdc.iID_ParentId, sdc.NamLamViec
	  )

	select
	distinct
		khv.iID_KeHoach5NamID as IIdKeHoach5NamId,
		khv.iID_DonViQuanLyID as IIdDonViId,
		khv.iID_MaDonViQuanLy as IIdMaDonVi,
		khv.iID_ParentId as IIdParentId,
		khv.bActive as BActive,
		khv.bIsGoc as BIsGoc,
		khv.bKhoa as BKhoa,
		khv.fGiaTriDuocDuyet as FGiaTriKeHoach,
		khv.sTrangThai as STrangThai,
		khv.dDateCreate as DDateCreate,
		khv.sUserCreate as SUserCreate,
		khv.dDateUpdate as DDateUpdate,
		khv.sUserUpdate as SUserUpdate,
		khv.dDateDelete as DDateDelete,
		khv.sUserDelete as SUserDelete,
		khv.sSoQuyetDinh as SoKeHoach,
		khv.dNgayQuyetDinh as NgayLap,
		khv.iGiaiDoanDen as GiaiDoanDen,
		khv.iGiaiDoanTu as GiaiDoanTu,
		khv.ILoai as ILoai,
		khv.NamLamViec as NamLamViec,
		dv.sTenDonVi as STenDonVi,
		'(' + cast(isnull(dc.iSoLanDieuChinh, 0) as nvarchar) + ')' as SoLanDC,
		khv.MoTaChiTiet,
		khv.iID_KhthDeXuat as IIDKhthDeXuat,
		(khvdx.sSoQuyetDinh + ' - ' + cast(khvdx.iGiaiDoanTu as nvarchar) + '-' + cast(khvdx.iGiaiDoanDen as nvarchar)) as SKeHoachDeXuat,
		(case
			when khv.iID_ParentId is null then ''
			else
				(select khvpr.sSoQuyetDinh from VDT_KHV_KeHoach5Nam khvpr where khvpr.iID_KeHoach5NamID = khv.iID_ParentId)
		end) AS DieuChinhTu,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 202 AND ObjectId = khv.iID_KeHoach5NamID) AS TotalFiles
		FROM VDT_KHV_KeHoach5Nam khv
		LEFT JOIN DonVi dv ON khv.iID_MaDonViQuanLy = dv.iID_MaDonVi AND dv.iNamLamViec = khv.NamLamViec
		LEFT JOIN SoLanDieuChinh AS dc
			ON khv.iID_ParentId = dc.iID_ParentId and khv.NamLamViec = dc.NamLamViec
		LEFT JOIN VDT_KHV_KeHoach5Nam_DeXuat khvdx on khv.iID_KhthDeXuat = khvdx.Id
		ORDER BY khv.dDateCreate DESC
END
;
;
GO
