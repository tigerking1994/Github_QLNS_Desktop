/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_chitra_nganhang]    Script Date: 18/10/2022 10:46:21 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_danhsach_chitra_nganhang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_danhsach_chitra_nganhang]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap]    Script Date: 18/10/2022 10:46:21 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_danhsach_capphat_phucap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 18/10/2022 10:46:21 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet]    Script Date: 18/10/2022 10:46:21 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 18/10/2022 10:46:21 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]    Script Date: 18/10/2022 10:46:21 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_refresh_parent_mlns]    Script Date: 18/10/2022 10:46:21 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_refresh_parent_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_refresh_parent_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 18/10/2022 10:46:21 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_trongnuoc_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_trongnuoc_index]
GO
/****** Object:  UserDefinedFunction [dbo].[fnTotalDayOfMonth]    Script Date: 18/10/2022 10:46:21 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnTotalDayOfMonth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnTotalDayOfMonth]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_nh_tonghop]    Script Date: 18/10/2022 10:46:21 AM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_nh_tonghop' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_nh_tonghop]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_nh_tonghop]    Script Date: 18/10/2022 10:46:21 AM ******/
CREATE TYPE [dbo].[t_tbl_nh_tonghop] AS TABLE(
	[Id] [uniqueidentifier] NOT NULL,
	[iID_ChungTu] [uniqueidentifier] NOT NULL,
	[iID_DuAnId] [uniqueidentifier] NULL,
	[iID_HopDongId] [uniqueidentifier] NULL,
	[sMaNguon] [nvarchar](100) NULL,
	[sMaNguonCha] [nvarchar](100) NULL,
	[sMaDich] [nvarchar](100) NULL,
	[fGiaTriUsd] [float] NULL,
	[fGiaTriVnd] [float] NULL,
	[iID_TiGia] [uniqueidentifier] NULL,
	[iID_MucLucNganSach] [uniqueidentifier] NULL,
	[iStatus] [int] NULL,
	[bIsLog] [bit] NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[fnTotalDayOfMonth]    Script Date: 18/10/2022 10:46:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnTotalDayOfMonth] (@iMonth int, @iYear int)
RETURNS int
BEGIN
	RETURN DAY(EOMONTH(CONCAT(CAST(@iYear as nvarchar(10)),'/',CAST(@imonth as nvarchar(10)),'/01')))
END


GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 18/10/2022 10:46:21 AM ******/
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
		LCNhaThau.dNgayQuyetDinh as DNgayQuyetDinh, --KhaiPD update 13/10/2022
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
		goiThau.bActive as BActive,
		goiThau.iLanDieuChinh as ILanDieuChinh,
		goiThau.bIsKhoa as BIsKhoa,
		goiThau.dNgayTao as DNgayTao,
		goiThau.sNguoiTao as SNguoiTao,
		goiThau.sNguoiSua as SNguoiSua,
		goiThau.dNgaySua as DNgaySua,
		goiThau.dNgayXoa as DNgayXoa,
		goiThau.sNguoiXoa as SNguoiXoa,
		goiThau.iID_TiGiaID as IIdTiGiaId,
		goiThau.sMaNgoaiTeKhac as SMaNgoaiTeKhac,
		goiThau.bIsXoa as BIsXoa,
		DonVi.sTenDonVi as STenDonVi,
		nvc.sTenNhiemVuChi as STenChuongTrinh,
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
	from NH_DA_KHLCNhaThau LCNhaThau
	inner join NH_DA_GoiThau goiThau
		on LCNhaThau.Id = GoiThau.iId_KHLCNhaThau
	/*inner join NH_DA_DuAn DuAn
		on LCNhaThau.iID_DuAnID = DuAn.ID
	inner join DonVi
		on DuAn.iID_DonViQuanLyID = DonVi.iID_DonVi
	inner join DM_ChuDauTu ChuDauTu
		on DuAn.iID_ChuDauTuID = ChuDauTu.iID_DonVi*/
	left join NH_DA_DuAn DuAn
		on LCNhaThau.iID_DuAnID = DuAn.ID
	left join DonVi
		on LCNhaThau.iID_DonViQuanLyID = DonVi.iID_DonVi --KhaiPD update 13/10/2022
	left join DM_ChuDauTu ChuDauTu
		on DuAn.iID_ChuDauTuID = ChuDauTu.iID_DonVi
	left join NH_KHTongThe_NhiemVuChi khttnvc 
		on LCNhaThau.iID_KHTT_NhiemVuChiID = khttnvc.ID 
	left join NH_DM_NhiemVuChi nvc
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
	WHERE goiThau.iLoai = @ILoai and goiThau.iThuocMenu = @IThuocMenu
end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_refresh_parent_mlns]    Script Date: 18/10/2022 10:46:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_ns_refresh_parent_mlns]
	@iNamLamViec int,
	@parentIds t_tbl_uniqueidentifier READONLY
AS
BEGIN
	SELECT tmp.Id INTO #tmp
	FROM @parentIds as tmp
	LEFT JOIN NS_MucLucNganSach as child on tmp.Id = child.iID_MLNS_Cha AND child.iNamLamViec = @iNamLamViec
	WHERE child.iID IS NULL

	UPDATE ml
	SET
		bHangCha = 0
	FROM #tmp as tmp
	INNER JOIN NS_MucLucNganSach as ml on tmp.Id = ml.iID_MLNS AND ml.iNamLamViec = @iNamLamViec

	DROP TABLE #tmp
END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]    Script Date: 18/10/2022 10:46:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]
	@idDV varchar(20),
	@idChungTu varchar(200),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int,
	@iLoai int
AS
BEGIN
SELECT ml.sM AS M ,
       ml.sKyHieu AS KyHieu ,
       ml.sMoTa AS MoTa ,
       ml.iID_MLSKT AS IdMucLuc ,
       ml.iID_MLSKTCha AS IdParent ,
       ml.sSTT AS STT ,
       ml.bHangCha ,
	   ct.sGhiChu,
       TuChi =ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       HuyDong =ISNULL(SUM(ct.fHuyDongTonKho), 0)/@dvt ,
       PhanCap =ISNULL(SUM(ct.fPhanCap), 0)/@dvt ,
       MuaHangHienVat =ISNULL(SUM(ct.fMuaHangCapHienVat), 0)/@dvt ,
       DacThu =ISNULL(SUM(ct.fPhanCap), 0)/@dvt ,
       TongCongNSSD = ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       TongCongNSBD = ISNULL(SUM(ct.fTuChi), 0)/@dvt
FROM NS_SKT_MucLuc ml
LEFT JOIN NS_SKT_ChungTuChiTiet ct ON ml.iID_MLSKT = ct.iID_MLSKT
AND ml.iTrangThai=1
AND ml.iNamLamViec=@NamLamViec
AND ct.iNamLamViec = @NamLamViec
AND ct.iLoaiChungTu =@LoaiChungTu
AND ct.iLoai=@iLoai
AND (iID_MaDonVi = @idDV)
--AND ct.iID_CTSoKiemTra = @idChungTu
AND ct.iNamNganSach = @NamNganSach
AND ct.iID_MaNguonNganSach = @NguonNganSach
GROUP BY ml.sM ,
         ml.sKyHieu ,
         ml.sMoTa ,
         ml.iID_MLSKT ,
         ml.iID_MLSKTCha ,
         ml.sSTT ,
         ml.bHangCha,
		 ct.sGhiChu
ORDER BY ml.sKyHieu;
END
;
;





GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 18/10/2022 10:46:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TungNH
-- Create date: 21/04/2022
-- Description:	Lấy dữ liệu cho thêm mới bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@SoNgay int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    WITH SoLieuTienAn AS (
		SELECT
			MA_CBO MaCanBo,
			SUM (
				CASE
					--WHEN MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG') THEN ISNULL(HuongPC_SN, 0) * GIA_TRI
					WHEN MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * GIA_TRI 
					ELSE ISNULL(HuongPC_SN, 0) * GIA_TRI
				END
			) GiaTri
		FROM TL_CanBo_PhuCap canBoPhuCap
		--WHERE
			--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		WHERE 
			canBoPhuCap.MA_PHUCAP IN (SELECT Ma_PhuCap FROM TL_DM_PhuCap WHERE PARENT LIKE 'TIENAN' OR PARENT LIKE 'TIENAN2')
		GROUP BY canBoPhuCap.MA_CBO
	),
	--SoLieuTienAn2 AS (
	--	SELECT
	--		MA_CBO MaCanBo,
	--		SUM (
	--			CASE
	--				--WHEN MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG') THEN ISNULL(HuongPC_SN, 0) * GIA_TRI
	--				WHEN MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * GIA_TRI 
	--				ELSE ISNULL(HuongPC_SN, 0) * GIA_TRI
	--			END
	--		) GiaTri
	--	FROM TL_CanBo_PhuCap canBoPhuCap
	--	--WHERE
	--		--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
	--	WHERE 
	--		canBoPhuCap.MA_PHUCAP IN (SELECT Ma_PhuCap FROM TL_DM_PhuCap WHERE PARENT LIKE 'TIENAN2')
	--	GROUP BY canBoPhuCap.MA_CBO
	--), 
	ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_Cb MaCapBac,
			canBo.BHTN,
			canBo.PCCV
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER  JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.Thang = @Thang
			AND canBo.Nam = @Nam
			AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
	)

	SELECT
		newid()					AS Id,
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in (1, 2, 3)) THEN 0 
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' OR canBoPhuCap.MA_PHUCAP = 'TA_TT2' THEN soLieuTienAn.GiaTri
			--WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT2' THEN soLieuTienAn2.GiaTri
			WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
			ELSE canBoPhuCap.GIA_TRI
		END						AS GiaTri,
		canBo.MaHieuCanBo		AS MaHieuCanBo,
		canBo.MaCapBac			AS MaCb,
		canBoPhuCap.HuongPC_SN	AS HuongPcSn,
		cachTinhLuong.CongThuc	AS CongThuc,
		CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	FROM TL_CanBo_PhuCap canBoPhuCap
	INNER JOIN ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN SoLieuTienAn soLieuTienAn
		ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	--LEFT JOIN SoLieuTienAn2 soLieuTienAn2
	--	ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	LEFT JOIN TL_DM_Cach_TinhLuong_Chuan cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet]    Script Date: 18/10/2022 10:46:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_chungtu_chitiet]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	WITH LuongCapBac AS (
		SELECT
			dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.MA_PHUCAP			AS MaPhuCap,
			bangLuong.Ma_CB				AS MaCapBac,
			capBac.Parent				AS Ngach,
			SUM(
				CASE WHEN pc.Ma_PhuCap IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN (dbo.fnTotalDayOfMonth(@thang,@nam)*bangLuong.Gia_Tri)
					WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN ISNULL(HuongPC_SN, 0) * bangLuong.Gia_Tri
					ELSE bangLuong.Gia_Tri END
			)		AS GiaTri,
			COUNT(bangLuong.Ma_CBo)		AS SoNguoi
		FROM TL_BangLuong_Thang bangLuong
		INNER JOIN TL_DM_PhuCap as pc on bangLuong.Ma_PhuCap = pc.Ma_PhuCap
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		JOIN TL_DM_CapBac capBac
			ON bangLuong.Ma_CB = capBac.Ma_Cb
		WHERE
			dsCapNhapBangLuong.Ma_CachTL IN (SELECT * FROM f_split(@maCachTl))
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
			AND dsCapNhapBangLuong.Thang=@Thang
			AND dsCapNhapBangLuong.Nam=@Nam
			AND dsCapNhapBangLuong.Status=1
			AND bangLuong.Gia_Tri != 0
		GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.MA_PHUCAP,bangLuong.Ma_CB, capBac.Parent
	), LuongCapBacMlns AS (
		SELECT
			luongCapBac.MaDonVi,
			phuCapMlns.XauNoiMa,
			SoNguoi,
			GiaTri
		FROM TL_PhuCap_MLNS phuCapMlns
		JOIN LuongCapBac luongCapBac
			ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
		WHERE
			phuCapMlns.Nam = @nam
	),

	DataDuToan as (
		Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
		From TL_QT_ChungTuChiTiet ctchitiet
		Join TL_QT_ChungTu chungtu
		on chungtu.ID = ctchitiet.Id_ChungTu
		Where Nam = @nam
		And Ma_DonVi IN (SELECT * FROM f_split(@maCachTl))
		Group By XauNoiMa, Ma_DonVi
	)

	SELECT
		luong.MaDonVi					AS MaDonVi,
		NEWID()							AS Id,
		iID_MLNS						AS MlnsId, 
		iID_MLNS_Cha					AS MlnsIdParent, 
		sXauNoiMa						AS XauNoiMa, 
		sLNS							AS Lns, 
		sL								AS L, 
		sK								AS K, 
		sM								AS M, 
		sTM								AS TM, 
		sTTM							AS TTM, 
		sNG								AS Ng, 
		sTNG							AS TNG, 
		sTNG1							AS TNG1, 
		sTNG2							AS TNG2, 
		sTNG3							AS TNG3, 
		sMoTa							AS Mota, 
		iNamLamViec						AS NamLamViec,
		bHangCha						AS BHangCha,
		sChiTietToi						AS ChiTietToi,
		CONVERT(decimal, SUM(SoNguoi))	AS SoNguoi,
		SUM(GiaTri)						AS TongCong,
		SUM(GiaTri)						AS DieuChinh,
		@maCachTl						AS MaCachTl,
		ISNULL(dataDuToan.DuToan, 0)	AS DDuToan
	FROM NS_MucLucNganSach mlns
	JOIN LuongCapBacMlns luong
		ON mlns.sXauNoiMa = luong.XauNoiMa
	LEFT JOIN DataDuToan dataDuToan
		ON luong.XauNoiMa = dataDuToan.XauNoiMa and luong.MaDonVi = dataDuToan.Ma_DonVi
	WHERE
		sLNS IN ('1', '101', '1010000')
		AND iNamLamViec = @nam
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan
	ORDER BY MaDonVi, sXauNoiMa

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 18/10/2022 10:46:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_tl_get_data_ct_chi_tiet_export]
	@lstId nvarchar(max),
	@lstCach nvarchar(100)
AS
BEGIN
	with ctct as (
	  select XauNoiMa,MaCachTl,  Sum(TongCong) AS TongCong,
		   convert(decimal,Sum(SoNguoi)) as SoNguoi,
		   Sum(DieuChinh) AS DieuChinh,
		 Sum(DDuToan) As DDuToan
	  from TL_QT_ChungTu as tbl
	  INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
	  where  (ISNULL(@lstId, '') <> '' AND tbl.ID in (SELECT *  FROM f_split(@lstId)))
		AND  dt.MaCachTl in (SELECT *  FROM f_split(@lstCach))
	  group by dt.XauNoiMa, dt.MaCachTl
	)
SELECT 
     NEWID() as Id,
     iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS Mota,
     MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
    SoNguoi,
       DieuChinh,
     DDuToan
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = 2022

order by mlns.sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap]    Script Date: 18/10/2022 10:46:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TungNH
-- Create date: 09/12/2021
-- Description:	Lấy dữ liệu báo cáo bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap]
	@MaDonVi NVARCHAR(max), 
	@Thang int,
	@Nam AS int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'NTN,LHT_HS,LHT_TT,PCCV_TT,PCTHD_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,PCTRA_SUM,LUONGTHANG_SUM,PHAITRU_SUM,TM,THANHTIEN,PCNU_TT,HSBL_HS,PCTNVK_HS'
	SET @Query =
	'
	WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_CBo			AS MaCanBo,
			bangLuong.MA_PHUCAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi		AS MaDonVi,
			donVi.Ten_Donvi		AS TenDonvi,
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			canBo.Thang_TNN		AS Tnn,
			ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			chucVu.Ma_Cv AS MaChucVu,
			chucVu.Ten_Cv AS TenChucVu,
			CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) AS Stk,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
			canBo.Ngay_NN AS NgayNhapNguDate,
			canBo.Ngay_XN AS NgayXuatNguDate,
			canBo.Ngay_TN AS NgayTaiNguDate,
			CASE WHEN canBo.Thang_TNN IS NULL THEN 0 ELSE canBo.Thang_TNN END AS ThangTnn,
			capBac.XauNoiMa
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB=capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu
			ON canBo.Ma_CV=chucVu.Ma_Cv
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
	)
	SELECT MaDonVi, MaCanBo, TenCanBo, Tnn, MaCapBac, CapBac, HSChucVu, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangTnn, XauNoiMa, ' + @Cols + ' FROM (
		SELECT
			bangLuong.Thang AS Thang,
			bangLuong.Nam AS Nam, 
			canBo.MaDonVi,
			canBo.TenDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.MaCapBac,
			canBo.CapBac,
			canBo.HSChucVu,
			canBo.MaChucVu,
			canBo.TenChucVu,
			canBo.Stk,
			canBo.NgayNhapNgu,
			canBo.NgayXuatNgu,
			canBo.NgayTaiNgu,
			canBo.NgayNhapNguDate,
			canBo.NgayXuatNguDate,
			canBo.NgayTaiNguDate,
			canBo.ThangTnn,
			canBo.Tnn,
			CASE WHEN bangLuong.MaPhuCap = ' + '''NTN''' + ' THEN dbo.f_luong_ntn(canBo.NgayNhapNguDate, canBo.NgayXuatNguDate, canBo.NgayTaiNguDate, canBo.ThangTnn, 6, 2022) ELSE bangLuong.GiaTri END AS GiaTri,
			bangLuong.MaPhuCap,
			canBo.XauNoiMa
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @Cols + ')
	) pvt
	WHERE MaCapBac LIKE ''0%''
	ORDER BY HSChucVu DESC, MaCapBac DESC, TenCanBo DESC'
	execute(@Query)
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_chitra_nganhang]    Script Date: 18/10/2022 10:46:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TungNH
-- Create date: 23/04/2022
-- Description:	Lấy dữ liệu cho báo cáo chi trả cá nhân
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_danhsach_chitra_nganhang] 
	@Thang int,
	@Nam int,
	@MaDonVi NVARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    WITH BangLuongThang AS (
		SELECT
			bangLuongThang.Ma_CBo	AS MaCanBo,
			SUM(ISNULL(bangLuongThang.Gia_Tri, 0))	AS GiaTri
		FROM TL_BangLuong_Thang bangLuongThang
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuongThang.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuongThang.Ma_PhuCap = 'THANHTIEN'
			AND dsCapNhapBangLuong.Ma_CachTL in ('CACH0', 'CACH5')
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
			AND dsCapNhapBangLuong.Thang=@Thang
			AND dsCapNhapBangLuong.Nam=@Nam
			AND dsCapNhapBangLuong.Status=1
		GROUP BY bangLuongThang.Ma_CBo
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi		AS MaDonvi,
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			canBo.So_TaiKhoan	AS SoTaiKhoan,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			ISNULL(canBo.Ma_CB, '0') AS MaCapBac
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_ChucVu chucVu
			ON canBo.Ma_CV=chucVu.Ma_Cv
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=@Thang
			AND canBo.Nam=@Nam
			AND canBo.TM = 1
	)

	SELECT
		canBo.MaDonvi,
		canBo.MaCanBo,
		canBo.TenCanBo,
		canBo.SoTaiKhoan,
		bangLuongThang.GiaTri
	FROM BangLuongThang bangLuongThang
	INNER JOIN ThongTinCanBo canBo
		ON bangLuongThang.MaCanBo = canBo.MaCanBo
	ORDER BY HSChucVu DESC, MaCapBac DESC, TenCanBo ASC
END
;
;
GO
