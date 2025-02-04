update TL_Bao_Cao set Ten_BaoCao = N'Giải thích chi tiết phụ cấp Thường xuyên' WHERE Ma_BaoCao = '1.6'
update TL_Bao_Cao set Ma_BaoCao = '1.17' WHERE Ma_BaoCao = '1.14'
update TL_Bao_Cao set Ma_BaoCao = '1.16' WHERE Ma_BaoCao = '1.13'
update TL_Bao_Cao set Ma_BaoCao = '1.15' WHERE Ma_BaoCao = '1.12'
update TL_Bao_Cao set Ma_BaoCao = '1.14' WHERE Ma_BaoCao = '1.11'
update TL_Bao_Cao set Ma_BaoCao = '1.13' WHERE Ma_BaoCao = '1.10'
update TL_Bao_Cao set Ma_BaoCao = '1.12' WHERE Ma_BaoCao = '1.9'
update TL_Bao_Cao set Ma_BaoCao = '1.11' WHERE Ma_BaoCao = '1.8'
update TL_Bao_Cao set Ma_BaoCao = '1.10' WHERE Ma_BaoCao = '1.7'
INSERT [dbo].[TL_Bao_Cao] ([Id], [IsParent], [Ma_BaoCao], [Ma_Parent], [Note], [Ten_BaoCao]) VALUES (N'b4e0b833-ae54-4012-b78a-1a4d1822b727', 0, N'1.8', N'1', NULL, N'Giải thích chi tiết phụ cấp Thu nhập khác')
GO
INSERT [dbo].[TL_Bao_Cao] ([Id], [IsParent], [Ma_BaoCao], [Ma_Parent], [Note], [Ten_BaoCao]) VALUES (N'32d02dac-8566-4283-a5f7-88fc87621c82', 0, N'1.9', N'1', NULL, N'Giải thích chi tiết phụ cấp Giảm trừ khác')
GO
INSERT [dbo].[TL_Bao_Cao] ([Id], [IsParent], [Ma_BaoCao], [Ma_Parent], [Note], [Ten_BaoCao]) VALUES (N'1b830b1e-c584-40da-88ed-9666b13dee41', 0, N'1.7', N'1', NULL, N'Giải thích chi tiết phụ cấp Nghiệp vụ')
GO
insert into TL_Bao_Cao(Ma_BaoCao, Ten_BaoCao, Ma_Parent, IsParent)
values ('1.18',N'Bảng thanh toán tiền lương truy lĩnh (nhiều ngày nhận QĐ truy lĩnh)','1','0')
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 19/07/2022 11:11:49 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_dutoan]    Script Date: 19/07/2022 11:11:49 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_duan_find_from_dutoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_duan_find_from_dutoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_duan_find_from_dutoan]    Script Date: 19/07/2022 11:11:49 AM ******/
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
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 19/07/2022 11:11:49 AM ******/
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
					WHEN MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN @SoNgay * GIA_TRI 
					ELSE ISNULL(HuongPC_SN, 0) * GIA_TRI
				END
			) GiaTri
		FROM TL_CanBo_PhuCap canBoPhuCap
		--WHERE
			--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		GROUP BY canBoPhuCap.MA_CBO
	), ThongTinCanBo AS (
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
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTri
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
	LEFT JOIN TL_DM_Cach_TinhLuong_Chuan cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb
END
GO
