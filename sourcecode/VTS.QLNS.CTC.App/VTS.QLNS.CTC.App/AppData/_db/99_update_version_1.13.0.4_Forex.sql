/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_in_khlcnt]    Script Date: 7/24/2023 6:36:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_in_khlcnt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_in_khlcnt]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_in_khlcnt]    Script Date: 7/24/2023 6:36:25 PM ******/
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
		SELECT tbl.ID, tbl.iLoaiDuToan as IdLoaiDuToan,tbl.iLoaiDuToan,tbl.fGiaTriEUR,tbl.fGiaTriNgoaiTeKhac,tbl.fGiaTriUSD,tbl.fGiaTriVND,
		tbl.iID_DonViQuanLyID,tbl.iID_TiGiaID,tbl.iID_KHTT_NhiemVuChiID,tbl.sTenChuongTrinh,tbl.fTiGiaNhap,tbl.bIsActive,tbl.bIsGoc,
		tbl.bIsKhoa,tbl.bIsXoa,tbl.dNgayQuyetDinh,tbl.dNgaySua,tbl.dNgayTao,tbl.sNguoiTao,tbl.sNguoiSua,tbl.dNgayXoa,tbl.sNguoiXoa,
		tbl.iID_DuAnID,tbl.iID_DuToanGocID,tbl.iID_MaDonViQuanLy,tbl.iID_ParentID,tbl.iID_QDDauTuID,tbl.iID_TiGiaUSD_EURID,
		tbl.iID_TiGiaUSD_NgoaiTeKhacID,tbl.iID_TiGiaUSD_VNDID,tbl.iLanDieuChinh,tbl.iLoai,tbl.sMaNgoaiTeKhac,tbl.sMota, tbl.iID_TiGiaID as IIdTiGiaId,
		 Case When iLoaiDuToan = 1 then Concat(N'Dự toán mua sắm: ',sSoQuyetDinh)  
              When iLoaiDuToan = 2 then Concat(N'Dự toán đặt hàng: ',sSoQuyetDinh)  else sSoQuyetDinh end as sSoQuyetDinh
		FROM NH_DA_DuToan as tbl
		LEFT JOIN (SELECT DISTINCT iID_DuToanID FROM NH_DA_KHLCNhaThau WHERE bIsActive = 1 AND iID_DuToanID IS NOT NULL) as dt on tbl.ID = dt.iID_DuToanID
		WHERE tbl.iID_DuAnID = @iIdDuAnId and tbl.bIsActive=1
	 END
	 ELSE
	 BEGIN
		SELECT dt.*
		FROM NH_DA_KHLCNhaThau as tbl
		INNER JOIN NH_DA_DuToan as dt on tbl.iID_DuToanID = dt.ID
	 END
END
;
;
;

GO
