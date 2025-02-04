/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chitiet_kehoach]    Script Date: 04/01/2023 2:33:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chitiet_kehoach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chitiet_kehoach]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong]    Script Date: 09/01/2023 3:26:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong]    Script Date: 10/01/2023 8:45:23 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong]    Script Date: 10/01/2023 8:45:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_dong]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@lstColumnInclude nvarchar(max),
@lstHeaderInclude nvarchar(max)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Header AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Header = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,TA_TONG,PHAITRUKHAC_SUM'

IF(ISNULL(@lstColumnInclude, '') <> '')
BEGIN
	SET @Cols = CONCAT(@Cols, ',', @lstColumnInclude)
	SET @Header = CONCAT(@Header, ',', @lstHeaderInclude)
END

SET @Query =
'
WITH BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Header + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
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
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
canBo.So_TaiKhoan AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
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
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.Tnn,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong]    Script Date: 09/01/2023 3:26:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 09/12/2021
-- Description:	Lấy dữ liệu báo cáo bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_bang_tonghop_luong_phucap_bienphong]
	@maDonVi NVARCHAR(max), 
	@thang int,
	@nam int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'NTN,LHT_HS,LHT_TT,PCCV_HS,PCCV_TT,PCTHD_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,PCTRA_SUM,LUONGTHANG_SUM,PHAITRU_SUM,TM,THANHTIEN,PCNU_TT,HSBL_HS,PCTNVK_HS,PCTNVK_TT,PCTN_TT'
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
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
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
	SELECT ROW_NUMBER() over(order by HSChucVu DESC, MaCapBac DESC, Ten ASC) as stt, MaDonVi, MaCanBo, TenCanBo, Ten, Tnn, MaCapBac, CapBac, HSChucVu, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangTnn, XauNoiMa, ' + @Cols + ' FROM (
		SELECT
			bangLuong.Thang AS Thang,
			bangLuong.Nam AS Nam, 
			canBo.MaDonVi,
			canBo.TenDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
		    canBo.Ten,
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
	--WHERE MaCapBac LIKE ''0%''
	ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC'
	execute(@Query)
END
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chitiet_kehoach]    Script Date: 04/01/2023 2:33:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_tl_get_data_ct_chitiet_kehoach]
@namNs int,
@nam int,
@idDonVi varchar(50)
AS
Begin
SELECT
  mlns.sXauNoiMa as XauNoiMa,
  mlns.sLNS as Lns,
  mlns.sL as L,
  mlns.sK as K,
  mlns.sM as M,
  mlns.sTM as Tm,
  mlns.sTTM as Ttm,
  mlns.sNG as Ng,
  mlns.sTNG as Tng,
  mlns.sMoTa as MoTa,
  SUM(chungTuChiTiet.TongNamTruoc) as TongNamTruoc,
  SUM(chungTuChiTiet.TongCong) as TongCong,
  SUM(chungTuChiTiet.DieuChinh) as DieuChinh,
  chungTuChiTiet.GhiChu as GhiChu,
  chungTuChiTiet.Id_DonVi as IdDonVi,
  chungTuChiTiet.NamLamViec as NamLamViec,
  mlns.BHangCha as BHangCha,
  chungTuChiTiet.TenDonVi as TenDonVi,
  chungTuChiTiet.Ngach as Ngach,
  chungTuChiTiet.MaPhuCap as MaPhuCap,
  mlns.iID_MLNS as MlnsId,
  mlns.iID_MLNS_Cha as MlnsIdParent,
  ChenhLech = null
FROM (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @namNs) mlns
LEFT JOIN (SELECT * FROM TL_QT_ChungTuChiTiet_KeHoach WHERE Id_DonVi = @idDonVi and NamLamViec = @nam) chungTuChiTiet ON mlns.sXauNoiMa = chungTuChiTiet.XauNoiMa 
WHERE sLNS IN ('1010000', '1', '101')
group by mlns.sXauNoiMa,
  mlns.sLNS,
  mlns.sL,
  mlns.sK,
  mlns.sM,
  mlns.sTM,
  mlns.sTTM,
  mlns.sNG,
  mlns.sTNG,
  mlns.sMoTa,
  chungTuChiTiet.TongNamTruoc,
  chungTuChiTiet.TongCong,
  chungTuChiTiet.DieuChinh,
  chungTuChiTiet.GhiChu,
  chungTuChiTiet.Id_DonVi,
  chungTuChiTiet.NamLamViec,
  mlns.BHangCha,
  chungTuChiTiet.TenDonVi,
  chungTuChiTiet.Ngach,
  chungTuChiTiet.MaPhuCap,
  mlns.iID_MLNS,
  mlns.iID_MLNS_Cha
ORDER BY mlns.sXauNoiMa
End
GO

INSERT [dbo].[TL_Bao_Cao] ([Id], [IsParent], [Ma_BaoCao], [Ma_Parent], [Note], [Ten_BaoCao]) VALUES (N'7aa8bc56-7425-4446-a115-4a25b4e56989', 0, N'1.20', N'1', NULL, N'Bảng tổng hợp lương, phụ cấp biên phòng')
GO

delete from [TL_Map_Column_Config]

INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'2319f0b7-6d0a-498a-b0c5-069305271c3e', 1, 0, NULL, NULL, N'TC4', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'a9da8ae1-4a8c-4457-a365-156e5418e75a', 0, 1, NULL, N'SoSoLuong', N'SSL', 0, 2)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'26a1efda-ac0a-45ed-92c7-1bd7e5453699', 1, 0, NULL, NULL, N'TC2', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'c49635a9-b20d-4e43-8955-2499f7e0f3de', 0, 1, NULL, N'PCCV_HS', N'HSCV', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'1007e9e3-ed65-4e74-ade8-293ee247aa69', 0, 1, NULL, N'MaCb', N'CBAC', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'05712c12-1df6-4ed6-8edb-2da93b66f1bb', 0, 1, NULL, N'IsNam', N'NU', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'9bffecc2-cd71-47d2-9369-35a0d24eb603', 0, 1, NULL, N'SoTaiKhoan', N'SOTK', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'48717e9b-fc13-4378-ad46-36d311cb29bb', 0, 1, NULL, N'LHT_TT', N'LCB', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'572392c7-6355-413f-ab19-40611117040a', 0, 1, NULL, N'MaTangGiam', N'TG', 1, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'fb765465-8832-484b-9c5a-4326f371b7c6', 0, 1, NULL, N'LHT_HS', N'HSLG', 0, 2)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'c3ff4833-7fcf-406e-a7f7-4406a4243571', 0, 1, NULL, N'PCKHAC3_TT', N'TC5', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'a2f379bd-cc66-407c-9bf6-4a71e84019f1', 0, 1, NULL, N'NgayTn', N'TAINGU', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'96df3cdd-37ab-4c54-ac98-599aecc59f14', 0, 1, NULL, N'GTPT_SN', N'SONGUOIPT', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'f071a549-d35a-4203-b790-646f6f1692e2', 0, 1, N'(VK*1.0)/100.0', N'PCTNVK_HS', N'VK', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'b271d0cc-fe28-42fd-b04c-6840684ddbe3', 1, 0, NULL, NULL, N'TC_3', 0, 2)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'ca79a367-f344-4d04-8032-6e1bd957527b', 0, 1, NULL, N'TenCanBo', N'HOTEN', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'a8d39fae-1a24-451f-b5f9-735d6bf3646e', 0, 1, NULL, N'MaCb', N'CBAC', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'1322ba79-64bc-4e41-bb24-845c833c09cf', 1, 0, NULL, NULL, N'TC1', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'3ddebb23-ec77-4bb5-bac1-877af5e508c4', 0, 1, NULL, N'LHT_HS_CU', N'HSLG', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'9b208ebb-d418-4b33-bb20-8d1b5827bfe4', 0, 1, N'(KV*1.0)/100.0', N'PCKV_HS', N'KV', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'828bc0e8-e080-414c-ad9b-92e48c9213a5', 0, 1, NULL, N'SoTaiKhoan', N'SOTK', 0, 2)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'427ff851-03ac-46e2-90a8-9977f760d72e', 0, 1, NULL, N'GTKHAC_TT', N'TRUKHAC', 0, 2)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'7bf3f1d8-9d7c-46e9-a513-a03fa20bffad', 0, 1, NULL, N'MaCb', N'CB', 0, 2)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'52eea3c5-0e60-43d9-8401-a089035d638a', 1, 0, NULL, NULL, N'TC_1', 0, 2)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'1cdf50c4-0d64-4f8b-b276-a4c849cd4ff5', 1, 0, NULL, NULL, N'TC3', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'c5eeafe2-ee2a-44d8-bf87-b3a46d18ec2d', 0, 1, N'(KV*1.0)/100.0', N'PCKV_HS', N'KV', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'f8c0aea0-f9ed-400c-8b49-b638e8d37ad4', 0, 1, NULL, N'NgayNn', N'NHAPNGU', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'2633a9da-feaa-449c-a8bc-b76c7c997e20', 1, 0, NULL, NULL, N'TC_2', 0, 2)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'd31b7d36-19bc-4f63-84da-ba4df28463d0', 0, 1, NULL, N'Parent', N'DVI', 0, 2)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'cdca091f-e35f-4779-84ba-bc8356608ab1', 0, 1, NULL, N'LHT_TT', N'LCB', 0, 2)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'90e17790-f34f-4d71-b95a-d990a4096630', 0, 1, NULL, N'NgayXn', N'XUATNGU', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'2cf799b7-855c-4709-b2c6-db985488d0c7', 0, 1, NULL, N'GTKHAC_TT', N'TRUKHAC', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'b1109fc6-0485-43a0-9ea2-dd3650b96b6f', 0, 1, NULL, N'Parent', N'DVI', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'2bad4aa0-31c0-4d60-b7e8-e2493a1b8c04', 0, 1, NULL, N'ThangTnn', N'SOTNNGHE', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'55e2c020-1076-4987-ac22-ec9c78a0a159', 0, 1, NULL, N'LHT_HS', N'HSLG', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'2ceb1957-b755-4c18-8893-eee8a8e9ee2a', 0, 1, NULL, N'LHT_HS_CU', N'HSLG', 0, 2)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'922645b2-2740-4e4b-bbe0-f3e19c075dfb', 0, 1, NULL, N'SoSoLuong', N'SSL', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'0b4c3d49-584e-40a7-a913-f94f9bee2da2', 0, 1, NULL, N'TA_BB_DG', N'AN1N', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'08126a49-12b6-43aa-96a9-fcba5aa2058b', 0, 1, NULL, N'HSBL_HS', N'HSBL', 0, 1)
GO
INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value], [Mau]) VALUES (N'581e3f42-58fd-48ac-af8e-ffccec10e934', 0, 1, NULL, N'MaCv', N'CVU', 0, 2)
GO
