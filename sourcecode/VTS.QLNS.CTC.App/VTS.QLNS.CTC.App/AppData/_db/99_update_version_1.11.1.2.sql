/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_by_pheduyet_detail]    Script Date: 06/06/2022 5:44:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_thongtri_by_pheduyet_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_thongtri_by_pheduyet_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_by_pheduyet_detail]    Script Date: 06/06/2022 5:44:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_thongtri_by_pheduyet_detail]
AS
BEGIN
	SELECT 
		(CASE tbl.iLoaiThanhToan WHEN 1 THEN 
			(CASE WHEN dt.colName in ('fGiaTriThanhToanTN', 'fGiaTriThanhToanNN') 
					THEN (CASE tbl.iID_NguonVonID WHEN 1 THEN 'TT_CTT_KPQP' WHEN 2 THEN 'TT_Cap_KPNN' ELSE 'TT_Cap_KPK' END)
				WHEN dt.colName in ('fGiaTriThuHoiNamTruocTN', 'fGiaTriThuHoiNamTruocNN', 'fGiaTriThuHoiNamNayTN', 'fGiaTriThuHoiNamNayNN', 'fGiaTriThuHoiUngTruocNamTruocTN', 'fGiaTriThuHoiUngTruocNamTruocNN', 'fGiaTriThuHoiUngTruocNamNayTN', 'fGiaTriThuHoiUngTruocNamNayNN')
					THEN (CASE tbl.iID_NguonVonID WHEN 1 THEN 'TT_ThuUng_KPQP' WHEN 2 THEN 'TT_ThuUng_KPNN' ELSE 'TT_ThuUng_KPK' END)
				END)
			WHEN 0 THEN (CASE tbl.iID_NguonVonID WHEN 1 THEN 'TT_TamUng_KPQP' WHEN 2 THEN 'TT_TamUng_KPNN' ELSE 'TT_TamUng_KPK' END) END) as SMaKieuThongTri,
		NULL as SSoThongTri,
		dt.FGiaTri as FSoTien,
		tbl.iID_DuAnId as IIdDuAnId,
		tbl.iID_NhaThauId as IIdNhaThauId,
	 	tbl.Id as IIdDeNghiThanhToanId,
		dt.iID_MucID as IIdMucId,
		dt.iID_TieuMucID as IIdTieuMucId,
		dt.iID_TietMucID as IIdTietMucId,
		dt.iID_NganhID as IIdNganhId,
		da.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId,
		NULL as IIdLoaiNguonVonId,

		da.iID_CapPheDuyetID as IIdCapPheDuyetId,
		da.STenDuAn,
		ml.SLns,
		ml.SL,
		ml.SK,
		ml.SM,
		ml.STm,
		ml.STtm,
		ml.SNg,
		tbl.sTenDonViThuHuong as SDonViThuHuong
	FROM VDT_TT_DeNghiThanhToan as tbl
	INNER JOIN (
		SELECT iID_DeNghiThanhToanID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, SUM(dt.fGiaTri) as fGiaTri, colName
			from 
			(select iID_DeNghiThanhToanID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, fGiaTriThanhToanTN, fGiaTriThanhToanNN, fGiaTriThuHoiNamTruocTN, fGiaTriThuHoiNamTruocNN, fGiaTriThuHoiNamNayTN, fGiaTriThuHoiNamNayNN, fGiaTriThuHoiUngTruocNamTruocTN, fGiaTriThuHoiUngTruocNamTruocNN, fGiaTriThuHoiUngTruocNamNayTN, fGiaTriThuHoiUngTruocNamNayNN from VDT_TT_PheDuyetThanhToan_ChiTiet) as tbl
			UNPIVOT
			(fGiaTri FOR colName IN (fGiaTriThanhToanTN, fGiaTriThanhToanNN, fGiaTriThuHoiNamTruocTN, fGiaTriThuHoiNamTruocNN, fGiaTriThuHoiNamNayTN, fGiaTriThuHoiNamNayNN, fGiaTriThuHoiUngTruocNamTruocTN, fGiaTriThuHoiUngTruocNamTruocNN, fGiaTriThuHoiUngTruocNamNayTN, fGiaTriThuHoiUngTruocNamNayNN)) as dt
			GROUP BY iID_DeNghiThanhToanID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, colName
			HAVING SUM(dt.fGiaTri) <> 0
	) as dt on tbl.Id = dt.iID_DeNghiThanhToanID
	INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnId = da.iID_DuAnID
	INNER JOIN NguonNganSach as nv on tbl.iID_NguonVonID = nv.iID_MaNguonNganSach
	LEFT JOIN NS_MucLucNganSach as ml on dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID
END
GO
