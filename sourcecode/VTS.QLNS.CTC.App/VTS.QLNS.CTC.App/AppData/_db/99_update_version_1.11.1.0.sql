INSERT INTO [dbo].[HT_ChucNang]([iID_MaChucNang], [iID_ChucNang], [iID_ChucNangCha], [sSTT], [ITrangThai], [sTenChucNang], [BHangCha]) VALUES (N'INVESTMENT_STANDARD_GIAO_DU_TOAN_CHI_PHI', '29E7C8B2-6079-4F13-98B0-D18D2305AF0E', '904B6009-2410-42E2-879A-D8ED67391B25', N'04-01-05-00-00', '1', N'Qu?n lí giao d? toán chi phí', '0');
INSERT INTO [dbo].[HT_ChucNang]([iID_MaChucNang], [iID_ChucNang], [iID_ChucNangCha], [sSTT], [ITrangThai], [sTenChucNang], [BHangCha]) VALUES (N'INVESTMENT_STANDARD_GIAO_DU_TOAN_CHI_PHI_INDEX', '3B3C97FD-738C-4318-A519-6D61F6C706BE', '904B6009-2410-42E2-879A-D8ED67391B25', N'04-01-05-01-00', '1', N'Qu?n lí giao d? toán chi phí - index', '0');
GO
INSERT INTO [dbo].[HT_Quyen_ChucNang]([iID_MaChucNang], [iID_MaQuyen]) VALUES (N'INVESTMENT_STANDARD_GIAO_DU_TOAN_CHI_PHI', N'ADMIN');
INSERT INTO [dbo].[HT_Quyen_ChucNang]([iID_MaChucNang], [iID_MaQuyen]) VALUES (N'INVESTMENT_STANDARD_GIAO_DU_TOAN_CHI_PHI_INDEX', N'ADMIN');
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quanli_giao_duan_index]    Script Date: 31/05/2022 10:20:49 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_quanli_giao_duan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_quanli_giao_duan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index_2]    Script Date: 31/05/2022 10:20:49 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnhathau_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnhathau_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index]    Script Date: 31/05/2022 10:20:49 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnhathau_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnhathau_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index]    Script Date: 31/05/2022 10:20:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnhathau_index]
	@iThuocMenu int
AS
BEGIN
	SELECT tbl.Id, tbl.SSoQuyetDinh, tbl.DNgayQuyetDinh, tbl.SMoTa, da.iID_DonViQuanLyID as IIdDonViQuanLy, da.iID_MaDonViQuanLy as SMaDonViQuanLy, dv.STenDonVi, 
		tbl.iID_DuAnID as IIdDuAnID, da.STenDuAn, tbl.ILanDieuChinh, tbl.iID_ParentID as IIdParentID , pr.sSoQuyetDinh as SSoQuyetDinhParent,
		tbl.BIsKhoa, tbl.BIsActive, CAST(0 as int) as ITepDinhKem, tbl.SNguoiTao, tbl.iID_QDDauTuID as IIdQDDauTuID, tbl.iID_DuToanID as IIdDuToanID, 
		tbl.iID_TiGiaID as IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.iLoaiKHLCNT, tbl.iLoai, tbl.iThuocMenu, tbl.iID_DonViQuanLyID as IIdDonViQuanLyId ,
		tbl.iID_MaDonViQuanLy as IidMaDonViQuanLy, tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId
	FROM NH_DA_KHLCNhaThau as tbl
	INNER JOIN NH_DA_DuAn as da on tbl.iID_DuAnID = da.ID
	LEFT JOIN DonVi as dv on da.iID_DonViQuanLyID = dv.iID_DonVi
	LEFT JOIN NH_DA_KHLCNhaThau as pr on tbl.iID_ParentID = pr.Id
	WHERE tbl.iThuocMenu = @iThuocMenu
	ORDER BY tbl.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index_2]    Script Date: 31/05/2022 10:20:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnhathau_index_2]
AS
BEGIN
	SELECT tbl.Id, tbl.SSoQuyetDinh, tbl.DNgayQuyetDinh, tbl.SMoTa, nvc.iID_DonViThuHuongID as IIdDonViQuanLy, nvc.iID_MaDonViThuHuong as SMaDonViQuanLy, dv.STenDonVi, 
		tbl.iID_DuAnID as IIdDuAnID, tbl.ILanDieuChinh, tbl.iID_ParentID as IIdParentID , pr.sSoQuyetDinh as SSoQuyetDinhParent,
		tbl.BIsKhoa, tbl.BIsActive, CAST(0 as int) as ITepDinhKem, tbl.SNguoiTao, tbl.iID_QDDauTuID as IIdQDDauTuID, tbl.iID_DuToanID as IIdDuToanID, 
		tbl.iID_TiGiaID as IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.iLoaiKHLCNT, tbl.iLoai, tbl.iThuocMenu, tbl.iID_DonViQuanLyID as IIdDonViQuanLyId ,
		tbl.iID_MaDonViQuanLy as IidMaDonViQuanLy, tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId
	FROM NH_DA_KHLCNhaThau as tbl
	left join NH_KHTongThe_NhiemVuChi as nvc on tbl.iID_KHTT_NhiemVuChiID = nvc.ID  
	LEFT JOIN DonVi as dv on nvc.iID_DonViThuHuongID = dv.iID_DonVi
	LEFT JOIN NH_DA_KHLCNhaThau as pr on tbl.iID_ParentID = pr.Id
	ORDER BY tbl.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quanli_giao_duan_index]    Script Date: 31/05/2022 10:20:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_quanli_giao_duan_index] 
AS
BEGIN
	WITH SoLieuDieuChinh AS 
	 (
		SELECT 
			pbvcp.Id , pbvcp.iID_ParentId
		FROM 
			VDT_KHV_PhanBoVon_ChiPhi pbvcp 
		WHERE 
			pbvcp.iID_ParentId is not null

		UNION ALL

		SELECT 
			pbvcp.Id , pbvcp.iID_ParentId
		FROM 
			VDT_KHV_PhanBoVon_ChiPhi pbvcp JOIN SoLieuDieuChinh ctpr ON pbvcp.iID_ParentId = ctpr.Id 
		WHERE pbvcp.iID_ParentId is not null
	  ),SoLanDieuChinh AS (
		   SELECT
			sdc.Id,sdc.iID_ParentId,  COUNT(sdc.Id) AS iSoLanDieuChinh
		  FROM 
			SoLieuDieuChinh sdc
		  GROUP BY sdc.iID_ParentId,sdc.Id
	  )
	select pbvcp.Id as Id, 
		pbvcp.sSoQuyetDinh as SSoQuyetDinh,
		pbvcp.dNgayQuyetDinh as DNgayQuyetDinh,
		pbvcp.iID_DuAnID as IIdDuAnId,
		pbvcp.iNamKeHoach as INamKeHoach,
		pbvcp.iID_LoaiNguonVonID as IIdLoaiNguonVonId,
		pbvcp.iID_DonViID as IIdDonViId,
		pbvcp.iID_MaDonVi as IIdMaDonVi,
		pbvcp.sLoaiDieuChinh as SLoaiDieuChinh,
		pbvcp.iID_ParentId as IIdParentId,
		pbvcp.bActive as BActive,
		pbvcp.bIsGoc as BIsGoc,
		pbvcp.fGiaTriDuocDuyet as FGiaTriDuocDuyet,
		pbvcp.iLoai as ILoai,
		pbvcp.iID_PhanBoGoc_ChiPhiID as IIdPhanBoGocChiPhiId,
		isnull(tbl.iSoLanDieuChinh,0) AS ILanDieuChinh ,
		pbvcp.bKhoa as BKhoa,
		dv.sTenDonVi as STenDonVi,
		nns.sTen as STenNguonVon,
		case
			when tbl.iID_ParentId is null then ''
			else (select pbvcp.sSoQuyetDinh from VDT_KHV_PhanBoVon_ChiPhi pbvcp where pbvcp.Id = tbl.iID_ParentId)
		end DieuChinhTu
	from VDT_KHV_PhanBoVon_ChiPhi pbvcp
	left join DonVi dv on pbvcp.iID_DonViID = dv.iID_DonVi  
	left join NguonNganSach nns on pbvcp.iID_LoaiNguonVonID = nns.iID_MaNguonNganSach 
	left join SoLanDieuChinh tbl ON tbl.Id = pbvcp.Id
END;
GO
