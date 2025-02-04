/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_bao_cao]    Script Date: 13/12/2023 9:35:24 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_bao_cao]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_bao_cao]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_bao_cao]    Script Date: 13/12/2023 9:35:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from VDT_TT_DeNghiThanhToan
CREATE PROCEDURE [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_bao_cao]
	@bThanhToanTheoHopDong bit,
	@iIdChungTu varchar(max), --- ID hợp đồng hoặc chi phí theo loại thanh toán theo hợp đồng hoặc chi phí
	@NgayDeNghi datetime,
	@NguonVonId int,
	@NamKeHoach int,
	@iCoQuanThanhToan int,
	@loaiCoQuanTaiChinh int,
	@loaiKhv int 
AS
BEGIN
	DECLARE @uIdEmpty uniqueidentifier = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER)
	DECLARE @fLuyKeTTKLHTNN float
	DECLARE @fLuyKeTTKLHTTN float
	DECLARE @fLuyKeTUChuaThuHoiCheDoNN float
	DECLARE @fLuyKeTUChuaThuHoiCheDoTN float
	DECLARE @fLuyKeTUChuaThuHoiUngTruocNN float
	DECLARE @fLuyKeTUChuaThuHoiUngTruocTN float
	DECLARE @fLuyKeThuHoiTN float
	DECLARE @fLuyKeThuHoiNN float

	SELECT
		(CASE WHEN iLoaiThanhToan = 1 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as ThanhToanTN,
		(CASE WHEN iLoaiThanhToan = 1 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as ThanhToanNN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiNamTruocTN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiNamNayTN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiNamTruocTN, 0) + ISNULL(fGiaTriThuHoiNamNayTN, 0)) ELSE SUM(0) END) as ThuHoiUngTN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiNamTruocNN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiNamNayNN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiNamTruocNN, 0) + ISNULL(fGiaTriThuHoiNamNayNN, 0)) ELSE SUM(0) END) as ThuHoiUngNN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiUngTruocNamNayTN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocTN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiUngTruocNamNayTN, 0) + ISNULL(fGiaTriThuHoiUngTruocNamTruocTN, 0)) ELSE SUM(0) END) as ThuHoiUngUngTruocTN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiUngTruocNamNayNN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocNN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiUngTruocNamNayNN, 0) + ISNULL(fGiaTriThuHoiUngTruocNamTruocNN, 0)) ELSE SUM(0) END) as ThuHoiUngUngTruocNN,
		(case WHEN iLoaiThanhToan = 2 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 AND khv.iLoai in(2,4) THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as TamUngUngTruocTN,
		(case WHEN iLoaiThanhToan = 2 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 AND khv.iLoai in(2,4) THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as TamUngUngTruocNN,
		(case WHEN iLoaiThanhToan = 2 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 AND khv.iLoai in(1,3) THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as TamUngCheDoTN,
		(case WHEN iLoaiThanhToan = 2 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 AND khv.iLoai in(1,3) THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as TamUngCheDoNN,
		(CASE WHEN iLoaiThanhToan = 1 AND ISNULL(fGiaTriThuHoiTN,0) <> 0 THEN SUM(ISNULL(fGiaTriThuHoiTN, 0)) ELSE SUM(0) END) as ThuHoiTN,		
		(CASE WHEN iLoaiThanhToan = 1 AND ISNULL(fGiaTriThuHoiNN,0) <> 0 THEN SUM(ISNULL(fGiaTriThuHoiNN, 0)) ELSE SUM(0) END) as ThuHoiNN
		INTO #tmp
	FROM VDT_TT_DeNghiThanhToan tbl
	INNER JOIN VDT_TT_DeNghiThanhToan_KHV as khv on tbl.Id = khv.iID_DeNghiThanhToanID
	LEFT JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tbl.Id = dt.iID_DeNghiThanhToanID
	WHERE @iIdChungTu = (CASE WHEN @bThanhToanTheoHopDong = 1 THEN tbl.iID_HopDongID ELSE tbl.iID_ChiPhiID END)
		  AND 
		  (
			  CONVERT(DATE, tbl.dNgayPheDuyet) < CONVERT(DATE, @NgayDeNghi) 
			  --CONVERT(DATE, tbl.dNgayPheDuyet) < CONVERT(DATE, '2023/12/12 00:00:00') 
			  and iNamKeHoach <= @NamKeHoach
		  )
		  AND (tbl.iCoQuanThanhToan = @iCoQuanThanhToan and (@iCoQuanThanhToan = 1 or (@iCoQuanThanhToan = 2 and tbl.loaiCoQuanTaiChinh = @loaiCoQuanTaiChinh)))
		  AND tbl.iID_NguonVonID = @NguonVonId
		  		  and (
						iLoaiThanhToan = 0 or
						-- nếu loại thanh toán là thanh toán -> tính tổng những đề nghị trước có loại kế hoạch vốn năm là 1,3 hoặc 2,4 phụ thuộc loại đang chọn
						(@loaiKhv in (1,3) and khv.iLoai in (1,3))
						or
						(@loaiKhv in (2,4) and khv.iLoai in (2,4))
						or @loaiKhv = 0
					)
	GROUP BY iLoaiThanhToan,dt.fGiaTriThanhToanTN, dt.fGiaTriThanhToanNN, dt.fGiaTriThuHoiNamTruocTN, dt.fGiaTriThuHoiNamNayTN, dt.fGiaTriThuHoiNamTruocNN, dt.fGiaTriThuHoiNamNayNN, khv.iLoai,
		fGiaTriThuHoiUngTruocNamNayTN, fGiaTriThuHoiUngTruocNamTruocTN, fGiaTriThuHoiUngTruocNamNayNN, fGiaTriThuHoiUngTruocNamTruocNN, fGiaTriThuHoiTN, fGiaTriThuHoiNN
	

	SELECT ISNULL(SUM(ISNULL(ThanhToanTN, 0)), 0) as ThanhToanTN,
			ISNULL(SUM(ISNULL(ThanhToanNN, 0)), 0) as ThanhToanNN,
			ISNULL(SUM(ISNULL(ThuHoiUngTN, 0)), 0) as ThuHoiUngTN,
			ISNULL(SUM(ISNULL(ThuHoiUngNN, 0)), 0) as ThuHoiUngNN,
			ISNULL(SUM(ISNULL(TamUngCheDoTN, 0)), 0) as TamUngTN,
			ISNULL(SUM(ISNULL(TamUngCheDoNN, 0)), 0) as TamUngNN,
			ISNULL(SUM(ISNULL(ThuHoiUngUngTruocNN, 0)), 0) as ThuHoiUngUngTruocNN,
			ISNULL(SUM(ISNULL(ThuHoiUngUngTruocTN, 0)), 0) as ThuHoiUngUngTruocTN,
			ISNULL(SUM(ISNULL(TamUngUngTruocTN, 0)), 0) as TamUngUngTruocTN,
			ISNULL(SUM(ISNULL(TamUngUngTruocNN, 0)), 0) as TamUngUngTruocNN,	
			ISNULL(SUM(ISNULL(ThuHoiTN, 0)), 0) as ThuHoiTN,
			ISNULL(SUM(ISNULL(ThuHoiNN, 0)), 0) as ThuHoiNN
	FROM  #tmp
	DROP TABLE #tmp
END
;
;
;
;
GO
