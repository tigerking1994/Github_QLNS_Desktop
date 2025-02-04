/****** Object:  StoredProcedure [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]    Script Date: 9/21/2023 2:35:48 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]    Script Date: 9/21/2023 2:35:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]
	@IdDuAn nvarchar(max),
	@NgayBatDau datetime,
	@NgayKetThuc datetime,
	@iID_HopDongID nvarchar(max),
	@iID_KHTongTheID nvarchar(max)
AS
BEGIN
	DECLARE @fGiaTriDuocCapUsd float
	DECLARE @fGiaTriDuocCapVnd float
	DECLARE @fGiaTriTTTU_Usd float
	DECLARE @fGiaTriTTTU_Vnd float

    -- Tính lũy kế được cấp
	SELECT @fGiaTriDuocCapUsd = SUM(ISNULL(tbl.fGiaTriUsd, 0)), @fGiaTriDuocCapVnd = SUM(ISNULL(tbl.fGiaTriVnd, 0))
	FROM NH_TH_TongHop tbl
	WHERE tbl.iID_DuAnId = @IdDuAn AND tbl.bIsLog = 0 AND tbl.iStatus = 0
		AND (@NgayBatDau is null or tbl.dNgayDeNghi >= @NgayBatDau)
		AND (@NgayKetThuc is null or tbl.dNgayDeNghi <= @NgayKetThuc)
		AND  (tbl.sMaNguon in ('101', '102') OR (tbl.sMaDich in ('111', '112', '121', '122') AND tbl.iCoQuanThanhToan = 1))
	GROUP BY tbl.iID_DuAnId

	-- Tính tổng thanh toán + tạm ứng
	SELECT @fGiaTriTTTU_Usd = SUM(ISNULL(tbl.fGiaTriUsd, 0)), @fGiaTriTTTU_Vnd = SUM(ISNULL(tbl.fGiaTriVnd, 0)) 
	FROM NH_TH_TongHop tbl
	WHERE tbl.iID_DuAnId = @IdDuAn AND tbl.bIsLog = 0 AND tbl.iStatus = 0
		AND (@NgayBatDau is null or tbl.dNgayDeNghi >= @NgayBatDau)
		AND (@NgayKetThuc is null or tbl.dNgayDeNghi <= @NgayKetThuc)
		AND tbl.sMaDich in ('111', '112', '121', '122') 
	GROUP BY tbl.iID_DuAnId

	select tt.ID, tt.sSoDeNghi, tt.dNgayDeNghi, 
		concat(DM_ChuDauTu.iID_MaDonVi,'-',DM_ChuDauTu.sTenDonVi) as sChuDauTu, 
		nt.sTenNhaThau as TenNhaThau, 
		tt.iLoaiNoiDungChi, 
		tt.iCoQuanThanhToan, 
		tt.iLoaiDeNghi, 
		--(
		--	select 
		--			distinct mlns.sXauNoiMa 
		--		from NS_MucLucNganSach mlns
		--		where 
		--			(mlns.iID = pdttct.iID_MucLucNganSachID OR mlns.iID_MLNS = pdttct.iID_MLNS_ID)
		--) as Mlns,
		tthd.id as IdHopDong,
		tthd.sSoHopDong as SoHopDong, 
		tt.fTongDeNghi_USD,
		tt.fTongDeNghi_VND,
		tt.fTongPheDuyet_BangSo_USD,
		tt.fTongPheDuyet_BangSo_VND,
		@fGiaTriDuocCapUsd as fGiaTriDuocCap_USD,
		@fGiaTriDuocCapVnd as fGiaTriDuocCap_VND,
		@fGiaTriTTTU_Usd as fGiaTriTTTU_USD,
		@fGiaTriTTTU_Vnd as fGiaTriTTTU_VND
	from NH_TT_ThanhToan tt 
	left join DM_ChuDauTu on tt.iID_ChuDauTuID  = DM_ChuDauTu.iID_DonVi
	left join NH_DA_QDDauTu  qddt on tt.iID_NhaThauID  = qddt.ID 
	left join NH_DM_NhaThau  nt on tt.iID_NhaThauID  = nt.ID 
	--left join NH_TT_ThanhToan_ChiTiet pdttct on pdttct.iID_DeNghiThanhToanID = tt.ID
	left join NH_DA_HopDong tthd on tthd.ID = tt.iID_HopDongID
	where  (@IdDuAn IS NULL  OR tt.iID_DuAnID = @IdDuAn)
		AND (@NgayBatDau is null or tt.dNgayDeNghi >= @NgayBatDau)
		AND (@NgayKetThuc is null or tt.dNgayDeNghi <= @NgayKetThuc)
		AND (ISNULL(@iID_HopDongID,'') ='' OR tt.iID_HopDongID = @iID_HopDongID)
		AND  (ISNULL(@iID_KHTongTheID,'') ='' OR tt.iID_NhiemVuChiID = @iID_KHTongTheID)

END
;
;
;

GO


DELETE NguonNganSach WHERE iID_MaNguonNganSach = 5;
SET IDENTITY_INSERT NguonNganSach ON
GO
INSERT INTO NguonNganSach(iID_MaNguonNganSach, bPublic,dNgayTao, iSTT, iTrangThai, sMoTa, sNguoiTao, sTen)
VALUES(5, 0, '2021-10-19 14:12:10.753',5,1,'QDTNH', 'admin', N'Quỹ dự trữ ngoại hối');
GO