/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]    Script Date: 31/08/2022 2:00:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_data_report_phan_cap_ngan_sach_dac_thu]    Script Date: 31/08/2022 2:00:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_get_data_report_phan_cap_ngan_sach_dac_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_get_data_report_phan_cap_ngan_sach_dac_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_data_report_phan_cap_ngan_sach_dac_thu]    Script Date: 31/08/2022 2:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_get_data_report_phan_cap_ngan_sach_dac_thu]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@LoaiNganSach int,
	@AgencyId nvarchar(500),
	@Nganh nvarchar(max),
	@IsInTongHop bit
AS
BEGIN
	SET NOCOUNT ON;

	select 
	phancap.iID_DTDauNam_PhanCap AS IdDTDauNamPhanCap,
	 phancap.iID_MaDonVi AS MaDonVi,
	 phancap.sTenDonVi AS TenDonVi,
	 phancap.fTuChi AS TuChi,
	 phancap.sXauNoiMa AS XauNoiMa,
	 phancap.sGhiChu AS GhiChu,
	 mlns.sNG AS NG 
	FROM (
	(
		select * FROM NS_DTDauNam_PhanCap
		where iID_CTDTDauNamChiTiet in (
		select iID_CTDTDauNamChiTiet FROM NS_DTDauNam_ChungTuChiTiet
		where iNamLamViec = @YearOfWork
		and iID_MaNguonNganSach = @BudgetSource
		and iNamNganSach = @YearOfBudget
		and iLoaiChungTu = @LoaiNganSach
		and (@IsInTongHop = 0 and iID_MaDonVi != @AgencyId ) or (@IsInTongHop = 1 and iID_MaDonVi = @AgencyId)
		and sNG is not null and sNG <> ''
		and sNG in (select * FROM dbo.f_split((@Nganh)))
		)
	)
	phancap 
	left join (select * FROM NS_MucLucNganSach where iNamLamViec = @YearOfWork) mlns on phancap.iID_MLNS = mlns.iID_MLNS
	)
	
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]    Script Date: 31/08/2022 2:00:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--exec sp_vdt_lay_gia_tri_denghi_thanh_toan 0, '346dc9b1-5053-4ebe-a585-6765ab155f10', '2022/04/27', 1, 2022, 1

CREATE PROCEDURE [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]
	@bThanhToanTheoHopDong bit,
	@iIdChungTu varchar(max),
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

	SELECT
		(CASE WHEN iLoaiThanhToan = 1 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as ThanhToanTN,
		(CASE WHEN iLoaiThanhToan = 1 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as ThanhToanNN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiNamTruocTN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiNamNayTN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiNamTruocTN, 0) + ISNULL(fGiaTriThuHoiNamNayTN, 0)) ELSE SUM(0) END) as ThuHoiUngTN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiNamTruocNN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiNamNayNN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiNamTruocNN, 0) + ISNULL(fGiaTriThuHoiNamNayNN, 0)) ELSE SUM(0) END) as ThuHoiUngNN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiUngTruocNamNayTN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocTN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiUngTruocNamNayTN, 0) + ISNULL(fGiaTriThuHoiUngTruocNamTruocTN, 0)) ELSE SUM(0) END) as ThuHoiUngUngTruocTN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiUngTruocNamNayNN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocNN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiUngTruocNamNayNN, 0) + ISNULL(fGiaTriThuHoiUngTruocNamTruocNN, 0)) ELSE SUM(0) END) as ThuHoiUngUngTruocNN,
		(case WHEN iLoaiThanhToan = 0 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 AND khv.iLoai in(2,4) THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as TamUngUngTruocTN,
		(case WHEN iLoaiThanhToan = 0 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 AND khv.iLoai in(2,4) THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as TamUngUngTruocNN,
		(case WHEN iLoaiThanhToan = 0 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 AND khv.iLoai in(1,3) THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as TamUngCheDoTN,
		(case WHEN iLoaiThanhToan = 0 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 AND khv.iLoai in(1,3) THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as TamUngCheDoNN
		INTO #tmp
	FROM VDT_TT_DeNghiThanhToan tbl
	INNER JOIN VDT_TT_DeNghiThanhToan_KHV as khv on tbl.Id = khv.iID_DeNghiThanhToanID
	LEFT JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tbl.Id = dt.iID_DeNghiThanhToanID
	WHERE @iIdChungTu = (CASE WHEN @bThanhToanTheoHopDong = 1 THEN tbl.iID_HopDongID ELSE tbl.iID_ChiPhiID END)
		  AND 
		  (
			  tbl.dNgayPheDuyet <= @NgayDeNghi 
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
		fGiaTriThuHoiUngTruocNamNayTN, fGiaTriThuHoiUngTruocNamTruocTN, fGiaTriThuHoiUngTruocNamNayNN, fGiaTriThuHoiUngTruocNamTruocNN

	SELECT @fLuyKeTTKLHTNN = SUM(ISNULL(tt.fLuyKeTTKLHTNN_KHVN, 0) + ISNULL(tt.fLuyKeTTKLHTNN_KHVU, 0)),
		@fLuyKeTTKLHTTN = SUM(ISNULL(tt.fLuyKeTTKLHTTN_KHVN, 0) + ISNULL(tt.fLuyKeTTKLHTTN_KHVU, 0)) ,
		@fLuyKeTUChuaThuHoiCheDoNN = SUM(ISNULL(tt.fLuyKeTUChuaThuHoiNN_KHVN, 0)) ,
		@fLuyKeTUChuaThuHoiCheDoTN = SUM(ISNULL(tt.fLuyKeTUChuaThuHoiTN_KHVN, 0)) ,
		@fLuyKeTUChuaThuHoiUngTruocNN = SUM(ISNULL(tt.fLuyKeTUChuaThuHoiNN_KHVU, 0)),
		@fLuyKeTUChuaThuHoiUngTruocTN = SUM(ISNULL(tt.fLuyKeTUChuaThuHoiTN_KHVU, 0))
	FROM VDT_KT_KhoiTao_DuLieu as tbl
	INNER JOIN VDT_KT_KhoiTao_DuLieu_ChiTiet as dt on tbl.Id = dt.iID_KhoiTaoDuLieuID
	INNER JOIN VDT_KT_KhoiTao_DuLieu_ChiTiet_ThanhToan as tt on dt.Id = tt.iId_KhoiTaoDuLieuChiTietId
	WHERE @iIdChungTu = (CASE WHEN @bThanhToanTheoHopDong = 1 THEN tt.iID_HopDongID ELSE @uIdEmpty END)
		-- AND dt.iID_NguonVonID = @NguonVonId
		AND dt.iCoQuanThanhToan = @iCoQuanThanhToan
	

	SELECT (ISNULL(@fLuyKeTTKLHTTN, 0) + ISNULL(SUM(ISNULL(ThanhToanTN, 0)), 0)) as ThanhToanTN,
			(ISNULL(@fLuyKeTTKLHTNN, 0) + ISNULL(SUM(ISNULL(ThanhToanNN, 0)), 0)) as ThanhToanNN,
			ISNULL(SUM(ISNULL(ThuHoiUngTN, 0)), 0) as ThuHoiUngTN,
			ISNULL(SUM(ISNULL(ThuHoiUngNN, 0)), 0) as ThuHoiUngNN,
			(ISNULL(@fLuyKeTUChuaThuHoiCheDoTN, 0) + ISNULL(SUM(ISNULL(TamUngCheDoTN, 0)), 0)) as TamUngTN,
			(ISNULL(@fLuyKeTUChuaThuHoiCheDoNN, 0) +ISNULL(SUM(ISNULL(TamUngCheDoNN, 0)), 0)) as TamUngNN,
			ISNULL(SUM(ISNULL(ThuHoiUngUngTruocNN, 0)), 0) as ThuHoiUngUngTruocNN,
			ISNULL(SUM(ISNULL(ThuHoiUngUngTruocTN, 0)), 0) as ThuHoiUngUngTruocTN,
			(ISNULL(@fLuyKeTUChuaThuHoiUngTruocTN, 0) + ISNULL(SUM(ISNULL(TamUngUngTruocTN, 0)), 0)) as TamUngUngTruocTN,
			(ISNULL(@fLuyKeTUChuaThuHoiUngTruocNN, 0) +ISNULL(SUM(ISNULL(TamUngUngTruocNN, 0)), 0)) as TamUngUngTruocNN
	FROM  #tmp
	DROP TABLE #tmp
END
;
;
GO
