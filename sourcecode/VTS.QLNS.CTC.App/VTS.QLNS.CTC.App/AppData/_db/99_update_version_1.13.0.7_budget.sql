/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_goithau_listall]    Script Date: 8/4/2023 4:44:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_nguonvon_by_goithau_listall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_goithau_listall]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]    Script Date: 8/4/2023 4:44:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_in_khlcnt]    Script Date: 8/4/2023 4:44:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_in_khlcnt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_in_khlcnt]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_in_khlcnt]    Script Date: 8/4/2023 4:44:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_in_khlcnt]
@iId uniqueidentifier,
@iIdDuAnId uniqueidentifier 
AS
BEGIN
	 DECLARE @iExist int = (SELECT COUNT(*) FROM NH_DA_KHLCNhaThau WHERE Id = @iId)
	 IF(@iExist = 0)
	 BEGIN
		SELECT 
				tbl.ID, 
				tbl.iLoaiDuToan as IdLoaiDuToan, 
				tbl.fGiaTriEUR as FGiaTriEur,  
				tbl.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac, 
				tbl.fGiaTriUSD as FGiaTriUsd, 
				tbl.fGiaTriVND as FGiaTriVnd, 
				tbl.iID_DonViQuanLyID as IIdDonViQuanLyId, 
				tbl.iID_TiGiaID as IIdTiGiaId, 
				tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId, 
				tbl.sTenChuongTrinh as STenChuongTrinh, 
				tbl.fTiGiaNhap as FTiGiaNhap, 
				tbl.bIsActive as BIsActive, 
				tbl.bIsGoc as BIsGoc, 
				tbl.bIsKhoa as BIsKhoa, 
				tbl.bIsXoa as BIsXoa, 
				tbl.dNgayQuyetDinh as DNgayQuyetDinh, 
				tbl.dNgaySua as DNgaySua, 
				tbl.dNgayTao as DNgayTao, 
				tbl.sNguoiTao as SNguoiTao, 
				tbl.sNguoiSua as SNguoiSua, 
				tbl.dNgayXoa as DNgayXoa, 
				tbl.sNguoiXoa as SNguoiXoa, 
				tbl.iID_DuAnID as IIdDuAnId, 
				tbl.iID_DuToanGocID as IIdDuToanGocId, 
				tbl.iID_MaDonViQuanLy as IIdMaDonViQuanLy, 
				tbl.iID_ParentID as IIdParentId, 
				tbl.iID_QDDauTuID as IIdQdDauTuId, 
				tbl.iID_TiGiaUSD_EURID as IIdTiGiaUsdEurid, 
				tbl.iID_TiGiaUSD_NgoaiTeKhacID as IIdTiGiaUsdNgoaiTeKhacId, 
				tbl.iID_TiGiaUSD_VNDID as IIdTiGiaUsdVndid, 
				tbl.iID_TiGiaID as IIdTiGiaId,
				tbl.iLoai as ILoai,
				tbl.sMota as SMoTa,
				tbl.iLanDieuChinh as ILanDieuChinh,
				tbl.sMaNgoaiTeKhac as SMaNgoaiTeKhac,
				Case When iLoaiDuToan = 1 then Concat(N'Dự toán mua sắm: ',sSoQuyetDinh)  
				  When iLoaiDuToan = 2 then Concat(N'Dự toán đặt hàng: ',sSoQuyetDinh)  else sSoQuyetDinh end as sSoQuyetDinh
		FROM NH_DA_DuToan as tbl
		LEFT JOIN (SELECT DISTINCT iID_DuToanID FROM NH_DA_KHLCNhaThau WHERE bIsActive = 1 AND iID_DuToanID IS NOT NULL) as dt on tbl.ID = dt.iID_DuToanID
		WHERE tbl.iID_DuAnID = @iIdDuAnId and tbl.bIsActive=1
	 END
	 ELSE
	 BEGIN
		SELECT 
				dt.ID, 
				dt.iLoaiDuToan as IdLoaiDuToan, 
				dt.fGiaTriEUR as FGiaTriEur,  
				dt.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac, 
				dt.fGiaTriUSD as FGiaTriUsd, 
				dt.fGiaTriVND as FGiaTriVnd, 
				dt.iID_DonViQuanLyID as IIdDonViQuanLyId, 
				dt.iID_TiGiaID as IIdTiGiaId, 
				dt.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId, 
				dt.sTenChuongTrinh as STenChuongTrinh, 
				dt.fTiGiaNhap as FTiGiaNhap, 
				dt.bIsActive as BIsActive, 
				dt.bIsGoc as BIsGoc, 
				dt.bIsKhoa as BIsKhoa, 
				dt.bIsXoa as BIsXoa, 
				dt.dNgayQuyetDinh as DNgayQuyetDinh, 
				dt.dNgaySua as DNgaySua, 
				dt.dNgayTao as DNgayTao, 
				dt.sNguoiTao as SNguoiTao, 
				dt.sNguoiSua as SNguoiSua, 
				dt.dNgayXoa as DNgayXoa, 
				dt.sNguoiXoa as SNguoiXoa, 
				dt.iID_DuAnID as IIdDuAnId, 
				dt.iID_DuToanGocID as IIdDuToanGocId, 
				dt.iID_MaDonViQuanLy as IIdMaDonViQuanLy, 
				dt.iID_ParentID as IIdParentId, 
				dt.iID_QDDauTuID as IIdQdDauTuId, 
				dt.iID_TiGiaUSD_EURID as IIdTiGiaUsdEurid, 
				dt.iID_TiGiaUSD_NgoaiTeKhacID as IIdTiGiaUsdNgoaiTeKhacId, 
				dt.iID_TiGiaUSD_VNDID as IIdTiGiaUsdVndid, 
				dt.iID_TiGiaID as IIdTiGiaId,
				dt.iLoai as ILoai,
				dt.sSoQuyetDinh as SSoQuyetDinh,
				dt.sMota as SMoTa,
				dt.iLanDieuChinh as ILanDieuChinh,
				dt.sMaNgoaiTeKhac as SMaNgoaiTeKhac
		--dt.*,dt.iLoaiDuToan as IdLoaiDuToan
		FROM NH_DA_KHLCNhaThau as tbl
		INNER JOIN NH_DA_DuToan as dt on tbl.iID_DuToanID = dt.ID
	 END
END
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]    Script Date: 8/4/2023 4:44:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT cp.iID_GoiThauID as IIdGoiThauId, 
			cp.iID_DuToan_ChiPhiID as IIdDuToanChiPhiId,
			cp.iID_CacQuyetDinh_ChiPhiID as IIdCacQuyetDinhChiPhiId,
			cp.iID_ChiPhiID as IIdChiPhiId,
			cp.iID_GoiThau_NguonVonID as IIdGoiThauNguonVonId,
			cp.iID_QDDauTu_ChiPhiID as IIdQdDauTuChiPhiId,
			cp.iID_ParentID as IIdParentId,
			cp.sMaOrder,
			cp.sTenChiPhi,
			cp.*

	FROM NH_DA_GoiThau_ChiPhi cp
	Left Join NH_DA_GoiThau gt on gt.iID_GoiThauID = cp.iID_GoiThauID
	Where gt.iID_GoiThauId = @iIdKhlcnt
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_goithau_listall]    Script Date: 8/4/2023 4:44:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_goithau_listall]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT cp.iID_GoiThau_NguonVonID,
			cp.iID_GoiThauID as IIdGoiThauId,
			cp.iID_DuToan_NguonVonID as IIdDuToanNguonVonId,
			cp.iID_NguonVonID as IIdNguonVonID ,
			cp.iID_CacQuyetDinh_NguonVonID as IIdCacQuyetDinhNguonVonId,
			cp.iID_ChuTruongDauTu_NguonVonID as IIdChuTruongDauTuNguonVonId,
			cp.iID_DuAn_NguonVonID as IIdDuAnNguonVonId,
			cp.iID_QDDauTu_NguonVonID as IIdQdDauTuNguonVonId,
			cp.sMaOrder

	FROM NH_DA_GoiThau_NguonVon cp
	Left Join NH_DA_GoiThau gt on gt.iID_GoiThauID = cp.iID_GoiThauID
	Where gt.iID_GoiThauID = @iIdKhlcnt
END
;
;
GO
