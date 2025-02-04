
INSERT [dbo].[NS_QS_MucLuc] ([iID_QSMucLuc], [bHangCha], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [iThuTu], [iTrangThai], [sHienThi], [sKyHieu], [sM], [sMoTa], [sTM]) VALUES (N'8f13fbf5-89a4-4bb5-84a6-50b8ad73e472', 1, N'8d680461-ad5d-41fe-9343-dde894d810e8', NULL, 2024, 9, 1, N'', N'', N'0', N'Bảo hiểm xã hội', N'')
GO
INSERT [dbo].[NS_QS_MucLuc] ([iID_QSMucLuc], [bHangCha], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [iThuTu], [iTrangThai], [sHienThi], [sKyHieu], [sM], [sMoTa], [sTM]) VALUES (N'6d071cc5-eb48-4229-beff-1a6ddb54add9', 0, N'4a0e01b2-c34f-413d-af5a-d197e730522a', N'8d680461-ad5d-41fe-9343-dde894d810e8', 2024, 9, 1, N'', N'001', N'0', N'Thai sản', N'001')
GO
INSERT [dbo].[NS_QS_MucLuc] ([iID_QSMucLuc], [bHangCha], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [iThuTu], [iTrangThai], [sHienThi], [sKyHieu], [sM], [sMoTa], [sTM]) VALUES (N'4a63fe8c-8ac0-4e6f-83bc-0afeef342a18', 0, N'562a38d6-fb0a-45ba-b4df-c193d9122a92', N'8d680461-ad5d-41fe-9343-dde894d810e8', 2024, 9, 1, N'', N'002', N'0', N'Ốm đau', N'002')
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 3/4/2024 8:28:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 3/4/2024 8:28:09 PM ******/
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
					WHEN MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG') THEN ISNULL(HuongPC_SN, 0) * GIA_TRI
					WHEN MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN @SoNgay * GIA_TRI 
					ELSE 0
				END
			) GiaTri
		FROM TL_CanBo_PhuCap canBoPhuCap
		WHERE
			canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		GROUP BY canBoPhuCap.MA_CBO
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_Cb MaCapBac,
			canBo.BHTN,
			canBo.PCCV,
			canBo.Ma_TangGiam
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
		canBo.Ma_TangGiam as MaTangGiam,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in (1, 2, 4)) THEN 0 
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