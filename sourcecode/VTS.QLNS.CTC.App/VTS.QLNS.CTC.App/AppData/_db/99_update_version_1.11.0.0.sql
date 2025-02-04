/****** Object:  StoredProcedure [dbo].[sp_vdt_thanhtoanchiphi_detail_by_id]    Script Date: 24/05/2022 9:09:52 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_thanhtoanchiphi_detail_by_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_thanhtoanchiphi_detail_by_id]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thanhtoanchiphi_detail]    Script Date: 24/05/2022 9:09:52 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_thanhtoanchiphi_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_thanhtoanchiphi_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_denghithanhtoanchiphi_index]    Script Date: 24/05/2022 9:09:52 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_tt_denghithanhtoanchiphi_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_tt_denghithanhtoanchiphi_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_denghithanhtoanchiphi_index]    Script Date: 24/05/2022 9:09:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_tt_denghithanhtoanchiphi_index]
AS
BEGIN
	SELECT 
	tbl.Id,
	tbl.iID_DeNghiThanhToanID as IIdDeNghiThanhToanId,
	tbl.iID_DonViQuanLyID as IIdDonViQuanLyId,
	tbl.iID_MaDonViQuanLy as IIdMaDonViQuanLy,
	tbl.SNguoiLap,
	tbl.iID_LoaiNguonVonID as IIdLoaiNguonVonId,
	tbl.INamKeHoach,
	tbl.SGhiChu,
	tbl.SUserCreate,
	tbl.iID_DuAnId as IIdDuAnId,
	tbl.iID_PhanBoVon_ChiPhi_ID as IIdPhanBoVonChiPhiId,
	tbl.BKhoa,
	tbl.SGhiChuPheDuyet,
	tbl.SLyDoTuChoi,
	tbl.iID_Parent as IIdParent,
	tbl.BTongHop,
	dv.STenDonVi,
	da.STenDuAn
	FROM VDT_TT_DeNghiThanhToan_ChiPhi as tbl
	INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnId = da.iID_DuAnID
	LEFT JOIN DonVi as dv on tbl.iID_DonViQuanLyID = dv.iID_DonVi
	ORDER BY tbl.dDateCreate DESC
END




GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thanhtoanchiphi_detail]    Script Date: 24/05/2022 9:09:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_thanhtoanchiphi_detail]
@iIdDuToanId uniqueidentifier
AS
BEGIN
	SELECT dt.iID_DanhMuc_DT_chi as IIdNoiDungChi, dm.sTenDuToanChi as SNoiDungChi, CAST(0 as float) as FGiaTriDeNghi, null as SGhiChu
	FROM VDT_KHV_PhanBoVon_ChiPhi as tbl
	INNER JOIN VDT_KHV_PhanBoVon_ChiPhi_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVon_ChiPhi_ID
	INNER JOIN VDT_DM_DuToanChi as dm on dt.iID_DanhMuc_DT_chi = dm.iID_DuToanChi
	WHERE tbl.Id = @iIdDuToanId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thanhtoanchiphi_detail_by_id]    Script Date: 24/05/2022 9:09:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_thanhtoanchiphi_detail_by_id]
@iId uniqueidentifier
AS
BEGIN
	SELECT dt.iID_NoiDungChi as IIdNoiDungChi, dm.sTenDuToanChi as SNoiDungChi, dt.FGiaTriDeNghi, dt.SGhiChu
	FROM VDT_TT_DeNghiThanhToan_ChiPhi_ChiTiet as dt
	INNER JOIN VDT_DM_DuToanChi as dm on dt.iID_NoiDungChi = dm.iID_DuToanChi
	WHERE dt.iID_DeNghiThanhToan_ChiPhiID = @iId
END
GO
