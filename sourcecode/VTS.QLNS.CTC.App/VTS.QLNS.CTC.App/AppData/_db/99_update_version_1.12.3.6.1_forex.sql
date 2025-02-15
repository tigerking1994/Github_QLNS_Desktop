/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstnkehoachdathang_index]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_mstnkehoachdathang_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_mstnkehoachdathang_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstn_khdh_delete_by_id]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_mstn_khdh_delete_by_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_mstn_khdh_delete_by_id]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index_2]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnhathau_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnhathau_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_trongnuoc_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_trongnuoc_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_goithau_listall]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_nguonvon_by_goithau_listall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_goithau_listall]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_goithau]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_nguonvon_by_goithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_goithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_hangmuc_by_khlcnt]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_hangmuc_by_khlcnt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_hangmuc_by_khlcnt]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_hangmuc_by_goithau]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_hangmuc_by_goithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_hangmuc_by_goithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_goithau]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_chiphi_by_goithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_chiphi_by_goithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_qddautu]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_duan_find_from_qddautu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_duan_find_from_qddautu]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_dutoan]    Script Date: 05/12/2022 6:04:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_duan_find_from_dutoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_duan_find_from_dutoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_dutoan]    Script Date: 05/12/2022 6:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 16/03/2022
-- Description:	Lấy danh sách dự án cho màn dự toán ngoại hối
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_duan_find_from_dutoan]
	@YearOfWork INT, 
	@MaDonVi NVARCHAR(50),
	@DuToanId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		duAn.ID AS Id,
		duAn.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		duAn.sMaDuAn AS SMaDuAn,
		duAn.sTenDuAn AS STenDuAn,
		duAn.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		duAn.iID_ChuDauTuID AS IIdChuDauTuId,
		duAn.iID_MaChuDauTu AS IIdMaChuDauTu,
		duAn.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		duAn.iID_CapPheDuyetID AS IIdCapPheDuyetId,
		duAn.sKhoiCong AS SKhoiCong,
		duAn.sKetThuc AS SKetThuc,
		duAn.bIsDuPhong AS BIsDuPhong,
		duAn.sDiaDiem AS SDiaDiem,
		duAn.sMucTieu AS SMucTieu,
		duAn.sQuyMo AS SQuyMo,
		duAn.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurid,
		duAn.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndid,
		duAn.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		duAn.fUSD AS FUsd,
		duAn.fNgoaiTeKhac AS FNgoaiTeKhac,
		duAn.fVND AS FVnd,
		duAn.fEUR AS FEur,
		duAn.dNgayTao AS DNgayTao,
		duAn.sNguoiTao AS SNguoiTao,
		duAn.dNgaySua AS DNgaySua,
		duAn.sNguoiSua AS SNguoiSua,
		duAn.dNgayXoa AS DNgayXoa,
		duAn.sNguoiXoa AS SNguoiXoa,
		duAn.iID_ChuTruongDauTuID AS IIdChuTruongDauTuId,
		duAn.iID_TiGiaID AS IIdTiGiaId,
		duAn.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		duAn.iLoai AS ILoai,
		NULL AS STenDonVi,
		NULL AS STenPheDuyet,
		NULL AS STenChuDauTu
	FROM NH_DA_DuAn duAn
	WHERE
		1=1
		AND duAn.iID_MaDonViQuanLy = @MaDonVi
		AND duAn.ID IN (SELECT iID_DuAnID FROM NH_DA_QDDauTu) -- Lấy dự án đã có chủ trương đầu tư
		--AND duAn.ID NOT IN (SELECT DISTINCT(iID_DuAnID) FROM NH_DA_DuToan WHERE iID_DuAnID IS NOT NULL AND (@DuToanId IS NULL OR ID <> @DuToanId)) -- Lấy dự án chưa có quyết định đầu tư
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_qddautu]    Script Date: 05/12/2022 6:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 16/03/2022
-- Description:	Lấy danh sách dự án cho màn quyết định đầu tư
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_duan_find_from_qddautu]
	@YearOfWork INT, 
	@MaDonVi NVARCHAR(50),
	@ILoai INT,
	@QdDauTuId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DuAnId UNIQUEIDENTIFIER;
	SELECT @DuAnId = iID_DuAnID FROM NH_DA_QDDauTu WHERE ID = @QdDauTuId;

    SELECT
		duAn.ID AS Id,
		duAn.iID_KHTT_NhiemVuChiID AS IIdKhttNhiemVuChiId,
		duAn.sMaDuAn AS SMaDuAn,
		duAn.sTenDuAn AS STenDuAn,
		duAn.iID_DonViQuanLyID AS IIdDonViQuanLyId,
		duAn.iID_ChuDauTuID AS IIdChuDauTuId,
		duAn.iID_MaChuDauTu AS IIdMaChuDauTu,
		duAn.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		duAn.iID_CapPheDuyetID AS IIdCapPheDuyetId,
		duAn.sKhoiCong AS SKhoiCong,
		duAn.sKetThuc AS SKetThuc,
		duAn.bIsDuPhong AS BIsDuPhong,
		duAn.sDiaDiem AS SDiaDiem,
		duAn.sMucTieu AS SMucTieu,
		duAn.sQuyMo AS SQuyMo,
		duAn.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurid,
		duAn.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndid,
		duAn.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		duAn.fUSD AS FUsd,
		duAn.fNgoaiTeKhac AS FNgoaiTeKhac,
		duAn.fVND AS FVnd,
		duAn.fEUR AS FEur,
		duAn.dNgayTao AS DNgayTao,
		duAn.sNguoiTao AS SNguoiTao,
		duAn.dNgaySua AS DNgaySua,
		duAn.sNguoiSua AS SNguoiSua,
		duAn.dNgayXoa AS DNgayXoa,
		duAn.sNguoiXoa AS SNguoiXoa,
		duAn.iID_ChuTruongDauTuID AS IIdChuTruongDauTuId,
		duAn.iID_TiGiaID AS IIdTiGiaId,
		duAn.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		duAn.iLoai AS ILoai, --KhaiPD update 18/11
		NULL AS STenDonVi,
		NULL AS STenPheDuyet,
		NULL AS STenChuDauTu
	FROM NH_DA_DuAn duAn
	WHERE
		1=1
		AND duAn.iID_MaDonViQuanLy = @MaDonVi
		AND duAn.ID IN (SELECT iID_DuAnID FROM NH_DA_ChuTruongDauTu) -- Lấy dự án đã có chủ trương đầu tư
		AND duAn.ID NOT IN (SELECT DISTINCT(iID_DuAnID) FROM NH_DA_QDDauTu WHERE iID_DuAnID IS NOT NULL AND ILoai = @ILoai AND (@QdDauTuId IS NULL OR iID_DuAnID <> @DuAnId)) -- Lấy dự án chưa có quyết định đầu tư
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]    Script Date: 05/12/2022 6:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]
	@IdDuAn nvarchar(max),
	@NgayBatDau datetime,
	@NgayKetThuc datetime
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
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_goithau]    Script Date: 05/12/2022 6:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_chiphi_by_goithau]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT DISTINCT cp.ID as IIdChiPhiID, cp.iID_ParentID as IIdParentID, cp.sTenChiPhi as STenChiPhi, cp.sMaOrder as SMaOrder,
			ISNULL(dt.fTienGoiThau_EUR, 0) as FGiaTriEUR, ISNULL(dt.fTienGoiThau_NgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac ,
			ISNULL(dt.fTienGoiThau_USD, 0) as FGiaTriUSD, ISNULL(dt.fTienGoiThau_VND, 0) as FGiaTriVND, dt.iID_GoiThauID as IIdGoiThauID
	FROM NH_DA_GoiThau as tbl
	inner JOIN NH_DA_GoiThau_ChiPhi as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID
	inner JOIN NH_DA_DuToan_ChiPhi as cp on dt.iID_DuToan_ChiPhiID = cp.ID
	WHERE tbl.iID_GoiThauID = @iIdKhlcnt
	UNION ALL
	SELECT cp.ID as IIdChiPhiID, cp.iID_ParentID as IIdParentID, cp.sTenChiPhi as STenChiPhi, cp.sMaOrder as SMaOrder,
			ISNULL(dt.fTienGoiThau_EUR, 0) as FGiaTriEUR, ISNULL(dt.fTienGoiThau_NgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac ,
			ISNULL(dt.fTienGoiThau_USD, 0) as FGiaTriUSD, ISNULL(dt.fTienGoiThau_VND, 0) as FGiaTriVND, dt.iID_GoiThauID as IIdGoiThauID
	FROM NH_DA_GoiThau as tbl
	inner JOIN NH_DA_GoiThau_ChiPhi as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID
	inner JOIN NH_DA_QDDauTu_ChiPhi as cp on dt.iID_QDDauTu_ChiPhiID = cp.ID
	WHERE tbl.iID_GoiThauID = @iIdKhlcnt
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]    Script Date: 05/12/2022 6:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[sp_nh_goithau_chiphi_by_GoiThau_listall]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT * FROM NH_DA_GoiThau_ChiPhi cp
	Left Join NH_DA_GoiThau gt on gt.iID_GoiThauID = cp.iID_GoiThauID
	Where gt.iID_GoiThauId = @iIdKhlcnt
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_hangmuc_by_goithau]    Script Date: 05/12/2022 6:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_hangmuc_by_goithau]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT DISTINCT hm.ID as IIdHangMucID, hm.iID_ParentID as IIdParentID, hm.iID_DuToan_ChiPhiID as IIdChiPhiID, 
		hm.sMaHangMuc as SMaHangMuc, hm.sTenHangMuc as STenHangMuc, hm.sMaOrder as SMaOrder,
		dt.fTienGoiThau_EUR as FGiaTriPheDuyetEUR, dt.fTienGoiThau_NgoaiTeKhac as FGiaTriPheDuyetNgoaiTeKhac ,
		dt.fTienGoiThau_USD as FGiaTriPheDuyetUSD, dt.fTienGoiThau_VND as FGiaTriPheDuyetVND, tbl.iID_GoiThauID as IIdGoiThauID,
		CAST(0 as float) as FGiaTriNgoaiTeKhac, 
		CAST(0 as float) as FGiaTriUSD, 
		CAST(0 as float) as FGiaTriEUR, 
		CAST(0 as float) as FGiaTriVND
	FROM NH_DA_GoiThau as tbl	
	INNER JOIN NH_DA_GoiThau_ChiPhi as cp on tbl.iID_GoiThauID = cp.iID_GoiThauID
	INNER JOIN NH_DA_GoiThau_HangMuc as dt on cp.Id = dt.iID_GoiThau_ChiPhiID
	INNER JOIN NH_DA_DuToan_HangMuc as hm on dt.iID_DuToan_HangMucID = hm.ID
	WHERE tbl.iID_GoiThauID = @iIdKhlcnt
	UNION ALL
	SELECT hm.ID as IIdHangMucID, hm.iID_ParentID as IIdParentID, hm.iID_QDDauTu_ChiPhiID as IIdChiPhiID, 
		hm.sMaHangMuc as SMaHangMuc, hm.sTenHangMuc as STenHangMuc, hm.sMaOrder as SMaOrder,
		dt.fTienGoiThau_EUR as FGiaTriPheDuyetEUR, dt.fTienGoiThau_NgoaiTeKhac as FGiaTriPheDuyetNgoaiTeKhac ,
		dt.fTienGoiThau_USD as FGiaTriPheDuyetUSD, dt.fTienGoiThau_VND as FGiaTriPheDuyetVND, tbl.iID_GoiThauID as IIdGoiThauID,
		CAST(0 as float) as FGiaTriNgoaiTeKhac, 
		CAST(0 as float) as FGiaTriUSD, 
		CAST(0 as float) as FGiaTriEUR, 
		CAST(0 as float) as FGiaTriVND
	FROM NH_DA_GoiThau as tbl	
	INNER JOIN NH_DA_GoiThau_ChiPhi as cp on tbl.iID_GoiThauID = cp.iID_GoiThauID
	INNER JOIN NH_DA_GoiThau_HangMuc as dt on cp.Id = dt.iID_GoiThau_ChiPhiID
	INNER JOIN NH_DA_QDDauTu_HangMuc as hm on dt.iID_QDDauTu_HangMucID = hm.ID
	WHERE tbl.iID_GoiThauID = @iIdKhlcnt
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_hangmuc_by_khlcnt]    Script Date: 05/12/2022 6:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_hangmuc_by_khlcnt]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT hm.ID as IIdHangMucID, hm.iID_ParentID as IIdParentID, hm.iID_DuToan_ChiPhiID as IIdChiPhiID, 
		hm.sMaHangMuc as SMaHangMuc, hm.sTenHangMuc as STenHangMuc, hm.sMaOrder as SMaOrder,
		dt.fTienGoiThau_EUR as FGiaTriPheDuyetEUR, dt.fTienGoiThau_NgoaiTeKhac as FGiaTriPheDuyetNgoaiTeKhac ,
		dt.fTienGoiThau_USD as FGiaTriPheDuyetUSD, dt.fTienGoiThau_VND as FGiaTriPheDuyetVND, tbl.iID_GoiThauID as IIdGoiThauID,
		CAST(0 as float) as FGiaTriNgoaiTeKhac, 
		CAST(0 as float) as FGiaTriUSD, 
		CAST(0 as float) as FGiaTriEUR, 
		CAST(0 as float) as FGiaTriVND
	FROM NH_DA_GoiThau as tbl	
	INNER JOIN NH_DA_GoiThau_ChiPhi as cp on tbl.iID_GoiThauID = cp.iID_GoiThauID
	INNER JOIN NH_DA_GoiThau_HangMuc as dt on cp.Id = dt.iID_GoiThau_ChiPhiID
	INNER JOIN NH_DA_DuToan_HangMuc as hm on dt.iID_DuToan_HangMucID = hm.ID
	WHERE tbl.iId_KHLCNhaThau = @iIdKhlcnt
	UNION ALL
	SELECT hm.ID as IIdHangMucID, hm.iID_ParentID as IIdParentID, hm.iID_QDDauTu_ChiPhiID as IIdChiPhiID, 
		hm.sMaHangMuc as SMaHangMuc, hm.sTenHangMuc as STenHangMuc, hm.sMaOrder as SMaOrder,
		dt.fTienGoiThau_EUR as FGiaTriPheDuyetEUR, dt.fTienGoiThau_NgoaiTeKhac as FGiaTriPheDuyetNgoaiTeKhac ,
		dt.fTienGoiThau_USD as FGiaTriPheDuyetUSD, dt.fTienGoiThau_VND as FGiaTriPheDuyetVND, tbl.iID_GoiThauID as IIdGoiThauID,
		CAST(0 as float) as FGiaTriNgoaiTeKhac, 
		CAST(0 as float) as FGiaTriUSD, 
		CAST(0 as float) as FGiaTriEUR, 
		CAST(0 as float) as FGiaTriVND
	FROM NH_DA_GoiThau as tbl	
	INNER JOIN NH_DA_GoiThau_ChiPhi as cp on tbl.iID_GoiThauID = cp.iID_GoiThauID
	INNER JOIN NH_DA_GoiThau_HangMuc as dt on cp.Id = dt.iID_GoiThau_ChiPhiID
	INNER JOIN NH_DA_QDDauTu_HangMuc as hm on dt.iID_QDDauTu_HangMucID = hm.ID
	WHERE tbl.iId_KHLCNhaThau = @iIdKhlcnt
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_goithau]    Script Date: 05/12/2022 6:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_nguonvon_by_goithau]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT dt.iID_NguonVonID as IIdNguonVonID, nv.sTen as STenNguonVon, 
		ISNULL(dt.fTienGoiThau_NgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac,
		ISNULL(dt.fTienGoiThau_USD, 0) as FGiaTriUSD,
		ISNULL(dt.fTienGoiThau_VND, 0) as FGiaTriVND,
		ISNULL(dt.fTienGoiThau_EUR, 0) as FGiaTriEUR,
		dt.iID_GoiThauID as IIdGoiThauID
	FROM NH_DA_GoiThau as tbl
	INNER JOIN NH_DA_GoiThau_NguonVon as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID
	INNER JOIN NguonNganSach as nv on dt.iID_NguonVonID = nv.iID_MaNguonNganSach
	WHERE tbl.iID_GoiThauID = @iIdKhlcnt
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_goithau_listall]    Script Date: 05/12/2022 6:04:42 PM ******/
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
	SELECT * FROM NH_DA_GoiThau_NguonVon cp
	Left Join NH_DA_GoiThau gt on gt.iID_GoiThauID = cp.iID_GoiThauID
	Where gt.iID_GoiThauID = @iIdKhlcnt
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]    Script Date: 05/12/2022 6:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_khlcnt_listall]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT * FROM NH_DA_GoiThau_NguonVon cp
	Left Join NH_DA_GoiThau gt on gt.iID_GoiThauID = cp.iID_GoiThauID
	Where gt.iId_KHLCNhaThau = @iIdKhlcnt
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 05/12/2022 6:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_nh_goithau_trongnuoc_index]
	@ILoai int,
	@IThuocMenu int
as begin
	Select 
		goiThau.iID_GoiThauID as Id,
		goiThau.iID_GoiThauGocID as IIdGoiThauGocId,
		goiThau.iID_ParentID as IIdParentId,
		goiThau.iID_NhaThauID as IIdNhaThauId,
		case when goiThau.iCheckLuong is null then khlcnt.iID_DuToanID else   goiThau.iID_DuToanID  end  as IIdDuToanId,
		goiThau.iID_CacQuyetDinhID as IIdCacQuyetDinhId,
		goiThau.iID_ParentAdjustID as IIdParentAdjustId,
		goiThau.iId_KHLCNhaThau as IIdKhlcnhaThau,
		goiThau.iID_TiGiaUSD_NgoaiTeKhacID as IIdTiGiaUSDNgoaiTeKhacId,
		goiThau.iID_TiGiaUSD_VNDID as IIdTiGiaUSDVNDId,
		goiThau.iID_TiGiaUSD_EURID as IIdTiGiaUSDEURId,
		goiThau.iID_HinhThucChonNhaThauID as IIdHinhThucChonNhaThauId,
		goiThau.iID_PhuongThucDauThauID as IIdPhuongThucDauThauId,
		goiThau.iID_LoaiHopDongID as IIdLoaiHopDongId,
		--DuAn.ID as IIdDuAnId,
		DuAn.ID as IIdDuAnId,
		goiThau.sSoQuyetDinh as SSoQuyetDinh,
		case when   goiThau.iCheckLuong is null then LCNhaThau.iID_DonViQuanLyID else  goiThau.iID_DonViQuanLyID  end  as IID_DonViQuanLyID,
		case when   goiThau.iCheckLuong is null then nvc.ID else  goiThau.iID_KHTT_NhiemVuChiID  end  as IID_KHTT_NhiemVuChiID,
		case when goiThau.iCheckLuong is null then LCNhaThau.dNgayQuyetDinh else goiThau.dNgayQuyetDinh end as DNgayQuyetDinh, --KhaiPD update 13/10/2022
		goiThau.sMaGoiThau as SMaGoiThau,
		goiThau.sTenGoiThau as STenGoiThau,
		goiThau.LoaiGoiThau as LoaiGoiThau,
		goiThau.dBatDauChonNhaThau as DBatDauChonNhaThau,
		goiThau.dKetThucChonNhaThau as DKetThucChonNhaThau,
		goiThau.iThoiGianThucHien as IThoiGianThucHien,
		goiThau.fGiaGoiThauEUR as FGiaGoiThauEUR,
		goiThau.fGiaGoiThauUSD as FGiaGoiThauUSD,
		goiThau.fGiaGoiThauVND as FGiaGoiThauVND,
		goiThau.fGiaGoiThauNgoaiTeKhac as fGiaGoiThauNgoaiTeKhac,
		goiThau.bIsGoc as BIsGoc,
		goiThau.sSoKeHoachDatHang as SSoKeHoachDatHang,
		goiThau.dNgayKeHoach as DNgayKeHoach,
		goiThau.iCheckLuong as IcheckLuong,
		goiThau.bActive as BActive,
		goiThau.iLanDieuChinh as ILanDieuChinh,
		goiThau.bIsKhoa as BIsKhoa,
		goiThau.dNgayTao as DNgayTao,
		goiThau.sNguoiTao as SNguoiTao,
		goiThau.sNguoiSua as SNguoiSua,
		goiThau.dNgaySua as DNgaySua,
		goiThau.dNgayXoa as DNgayXoa,
		goiThau.sNguoiXoa as SNguoiXoa,
		 case when goiThau.iCheckLuong is null then khlcnt.iID_TiGiaID else   goiThau.iID_TiGiaID  end  as IIdTiGiaId,
		 case when goiThau.iCheckLuong is null then khlcnt.sMaNgoaiTeKhac else   goiThau.sMaNgoaiTeKhac  end  as SMaNgoaiTeKhac,
		goiThau.bIsXoa as BIsXoa,
		 concat(DonVi.iID_MaDonVi,' -', DonVi.sTenDonVi )as TenDonVi,
		nvc.sTenNhiemVuChi as STenNhiemVuChi,
		DuAn.sTenDuAn as STenDuAn,
		HinhThucChonNhaThau.sTenHinhThucChonNhaThau as STenHinhThucChonNhaThau,
		PhuongThucChonNhaThau.sTenPhuongThucChonNhaThau as STenPhuongThucChonNhaThau,
		ChuDauTu.sTenDonVi as STenChuDauTu,
		DuAn.sDiaDiem as SDiaDiem,
		QDDauTu.fGiaTriUSD as FQDDTTongPheDuyetUSD,
		QDDauTu.fGiaTriVND as FQDDTTongPheDuyetVND,
		QDDauTu.fGiaTriEUR as FQDDTTongPheDuyetEUR,
		QDDauTu.fGiaTriNgoaiTeKhac as FQDDTTongPheDuyetNgoaiTeKhac,
		LoaiHopDong.sTenLoaiHopDong as STenHopDong,
		DuToan.fGiaTriUSD as FDTTongPheDuyetUSD,
		DuToan.fGiaTriVND as FDTTongPheDuyetVND,
		DuToan.fGiaTriEUR as FDTTongPheDuyetEUR,
		DuToan.fGiaTriNgoaiTeKhac as FDTTongPheDuyetNgoaiTeKhac,
		khttnvc.ID as IIdKHTTNhiemVuChiId,  
		( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 405 AND ObjectId = goiThau.iID_GoiThauID ) AS TotalFiles,
		CASE
		WHEN goiThau.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sMaGoiThau FROM NH_DA_GoiThau hdpr WHERE hdpr.iID_GoiThauID = goiThau.iID_ParentAdjustId ) 
		END DieuChinhTu ,
		LCNhaThau.sMoTa as SMota
		from  NH_DA_GoiThau goiThau
	left join NH_DA_KHLCNhaThau LCNhaThau
		on LCNhaThau.Id = GoiThau.iId_KHLCNhaThau
	left join NH_DA_DuAn DuAn
		on goiThau.iID_DuAnID = DuAn.ID
	left join DonVi on (LCNhaThau.iID_DonViQuanLyID = DonVi.iID_DonVi AND goiThau.iCheckLuong is null) OR (goiThau.iID_DonViQuanLyID = DonVi.iID_DonVi  AND goiThau.iCheckLuong = 1)
	left join DM_ChuDauTu ChuDauTu
		on DuAn.iID_ChuDauTuID = ChuDauTu.iID_DonVi
	left join NH_KHTongThe_NhiemVuChi khttnvc 
		on  (LCNhaThau.iID_KHTT_NhiemVuChiID = khttnvc.ID  AND goiThau.iCheckLuong is null) OR (goiThau.iID_KHTT_NhiemVuChiID = khttnvc.ID AND goiThau.iCheckLuong = 1) 
	left join NH_DM_NhiemVuChi nvc
		--on (khttnvc.iID_NhiemVuChiID = nvc.ID  AND goiThau.iCheckLuong is null) OR (goiThau.iID_KHTT_NhiemVuChiID = nvc.ID AND goiThau.iCheckLuong = 1) 
		on khttnvc.iID_NhiemVuChiID = nvc.ID
	left join NH_DA_QDDauTu QDDauTu
		on LCNhaThau.iID_QDDauTuID = QDDauTu.ID
	left join NH_DA_DuToan DuToan
		on LCNhaThau.iID_DuToanID = DuToan.ID
	left join NH_DM_HinhThucChonNhaThau HinhThucChonNhaThau
		on goiThau.iID_HinhThucChonNhaThauID = HinhThucChonNhaThau.ID 
	left join NH_DM_PhuongThucChonNhaThau PhuongThucChonNhaThau
		on goiThau.iID_PhuongThucDauThauID = PhuongThucChonNhaThau.ID 
	left join NH_DM_LoaiHopDong LoaiHopDong
		on goiThau.iID_LoaiHopDongID = LoaiHopDong.iID_LoaiHopDongID 
	left join NH_DA_KHLCNhaThau khlcnt
		on khlcnt.Id=goiThau.iId_KHLCNhaThau
	WHERE goiThau.iLoai = @ILoai and goiThau.iThuocMenu = @IThuocMenu
	 ORDER BY goiThau.dNgayTao DESC
end
;

GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index_2]    Script Date: 05/12/2022 6:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnhathau_index_2]
AS
BEGIN
	SELECT tbl.Id, tbl.SSoQuyetDinh, tbl.DNgayQuyetDinh, tbl.SMoTa, nvc.iID_DonViThuHuongID as IIdDonViQuanLy, nvc.iID_MaDonViThuHuong as SMaDonViQuanLy,CONCAT(dv.iID_MaDonVi, ' - ', dv.sTenDonVi) AS sTenDonVi, 
		tbl.iID_DuAnID as IIdDuAnID, tbl.ILanDieuChinh, tbl.iID_ParentID as IIdParentID , pr.sSoQuyetDinh as SSoQuyetDinhParent,dutoan.sTenChuongTrinh as STenChuongTrinh,
		tbl.BIsKhoa, tbl.BIsActive, CAST(0 as int) as ITepDinhKem, tbl.SNguoiTao, tbl.iID_QDDauTuID as IIdQDDauTuID, tbl.iID_DuToanID as IIdDuToanID, 
		tbl.iID_TiGiaID as IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.iLoaiKHLCNT, tbl.iLoai, tbl.iThuocMenu, tbl.iID_DonViQuanLyID as IIdDonViQuanLyId ,
		tbl.iID_MaDonViQuanLy as IidMaDonViQuanLy, tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId
	FROM NH_DA_KHLCNhaThau as tbl
	left join NH_KHTongThe_NhiemVuChi as nvc on tbl.iID_KHTT_NhiemVuChiID = nvc.ID  
	LEFT JOIN DonVi as dv on nvc.iID_DonViThuHuongID = dv.iID_DonVi
	LEFT JOIN NH_DA_KHLCNhaThau as pr on tbl.iID_ParentID = pr.Id
	LEFT JOIN NH_DA_DuToan as dutoan on tbl.iID_DuToanID = dutoan.ID
	WHERE tbl.bIsActive=1
	ORDER BY tbl.dNgayTao DESC
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstn_khdh_delete_by_id]    Script Date: 05/12/2022 6:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[sp_nh_mstn_khdh_delete_by_id]
@iId uniqueidentifier
AS
BEGIN
	DELETE NH_MSTN_KeHoachDatHang_DanhMuc WHERE iID_KeHoachDatHang = @iId;
	DELETE NH_MSTN_KeHoachDatHang WHERE ID = @iId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstnkehoachdathang_index]    Script Date: 05/12/2022 6:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_mstnkehoachdathang_index]
AS
BEGIN
	SELECT tbl.Id, tbl.SSoQuyetDinh, tbl.DNgayQuyetDinh, tbl.SMoTa, tbl.iID_DonViID as IIdDonViQuanLy, dv.sTenDonVi,
		tbl.ILanDieuChinh, tbl.iID_ParentID as IIdParentID , pr.sSoQuyetDinh as SSoQuyetDinhParent,
		tbl.BIsActive, tbl.SNguoiTao, 
		tbl.iID_TiGiaID as IIdTiGiaID, tbl.sMaNgoaiTeKhac, 
		tbl.fGiaTriEUR as FGiaTriEur, tbl.fGiaTriUSD as FGiaTriUsd, tbl.fGiaTriVND as FGiaTriVnd, tbl.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac,
		tbl.iID_MaDonVi as SMaDonViQuanLy, tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId, dmnvc.sTenNhiemVuChi as STenChuongTrinh, nvc.iID_KHTongTheID as IIdKhtongTheId
	FROM NH_MSTN_KeHoachDatHang as tbl
	LEFT JOIN DonVi as dv on tbl.iID_DonViID = dv.iID_DonVi
	LEFT JOIN NH_MSTN_KeHoachDatHang as pr on tbl.iID_ParentID = pr.Id
	left join NH_KHTongThe_NhiemVuChi as nvc on tbl.iID_KHTT_NhiemVuChiID = nvc.ID  
	left join NH_DM_NhiemVuChi as dmnvc on nvc.iID_NhiemVuChiID = dmnvc.ID  
	ORDER BY tbl.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 05/12/2022 6:04:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
@iThuocMenu INT = NULL
AS
BEGIN
SELECT DISTINCT
	hd.Id,
	hd.sSoHopDong AS SSoHopDong,
	hd.dNgayHopDong AS DNgayHopDong,
	hd.sTenHopDong AS STenHopDong,
	hd.dKhoiCongDuKien AS DKhoiCongDuKien,
	hd.dKetThucDuKien AS DKetThucDuKien,
	hd.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
	hd.iID_LoaiHopDongID AS IIdLoaiHopDongId,
	hd.iID_ParentAdjustID AS IIdParentAdjustId,
	hd.iID_GoiThauID AS IIdGoiThauId,
	hd.iID_ParentID AS IIdParentId,
	hd.iID_NhaThauThucHienID AS IIdNhaThauThucHienId,
	hd.iID_KeHoachDatHangID AS IIdKeHoachDatHangId,
	hd.iID_TiGiaID AS IIdTiGiaId,
	hd.iID_DuAnID AS IIdDuAnId,
	hd.iLoai AS ILoai,
	hd.iThuocMenu AS IThuocMenu,
	hd.iThoiGianThucHien AS IThoiGianThucHien,
	hd.fGiaTriHopDongUSD AS FGiaTriHopDongUSD,
	hd.fGiaTriHopDongVND AS FGiaTriHopDongVND,
	hd.fGiaTriHopDongEUR AS FGiaTriHopDongEUR,
	hd.fGiaTriHopDongNgoaiTeKhac AS FGiaTriHopDongNgoaiTeKhac,
	gtnt.fGiaTRiHopDong_USD AS FGiaTriUsd,
	gtnt.fGiaTRiHopDong_VND AS FGiaTriVnd,
	gtnt.fGiaTRiHopDong_EUR AS FGiaTriEur,
	gtnt.fGiaTriHopDong_NgoaiTeKhac AS FGiaTriNgoaiTeKhac,
	hd.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
	hd.sNguoiTao AS SNguoiTao,
	hd.sNguoiSua AS SNguoiSua,
	hd.sNguoiXoa AS SNguoiXoa,
	hd.dNgaySua AS DNgaySua,
	hd.dNgayTao AS DNgayTao,
	hd.dNgayXoa AS DNgayXoa,
	hd.bIsActive AS BIsActive,
	hd.bIsGoc AS BIsGoc,
	hd.bIsKhoa AS BIsKhoa,
	lhd.sTenLoaiHopDong AS SLoaiHopDong,
	hd.iLanDieuChinh AS ILanDieuChinh,
	nvChi.iID_KHTongTheID AS IIdKhTongTheId,
	nvChi.STenChuongTrinh AS STenChuongTrinh,
	da.sTenDuAn AS STenDuAn,
	CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
	hd.iID_DonViQuanLyID AS IIdDonViQuanLyId,
	hd.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
	CASE
		WHEN hd.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hd.iID_ParentAdjustId ) 
	END DieuChinhTu
	
FROM NH_DA_HopDong hd
LEFT JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
LEFT JOIN NH_DA_DuAn da on hd.iID_DuAnID = da.ID
LEFT JOIN DonVi donVi
ON hd.iID_DonViQuanLyID = donVi.iID_DonVi AND hd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN
		(SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
		 FROM NH_KHTongThe_NhiemVuChi AS n 
		 INNER JOIN NH_DM_NhiemVuChi AS d 
		 ON n.iID_NhiemVuChiID = d.ID) 
AS nvChi
ON hd.iID_KHTongThe_NhiemVuChiID = nvChi.ID
LEFT JOIN (select iID_HopDongID, sum(fGiaTRiHopDong_USD) as fGiaTRiHopDong_USD
				, sum(fGiaTRiHopDong_VND) as fGiaTRiHopDong_VND
				, sum(fGiaTRiHopDong_EUR) as fGiaTRiHopDong_EUR
				, sum(fGiaTriHopDong_NgoaiTeKhac) as fGiaTriHopDong_NgoaiTeKhac
				from NH_DA_HopDong_GoiThau_NhaThau 
				group by iID_HopDongID
	      ) gtnt on hd.ID = gtnt.iID_HopDongID
WHERE @iThuocMenu IS NULL OR hd.iThuocMenu = @iThuocMenu
ORDER BY
	hd.dNgayTao DESC
END
GO
