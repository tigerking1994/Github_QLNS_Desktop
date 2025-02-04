INSERT [dbo].[TL_Bao_Cao] ([IsParent], [Ma_BaoCao], [Ma_Parent], [Note], [Ten_BaoCao]) VALUES (0, N'1.21', 1, NULL, N'Giải thích các phụ cấp theo ngày')

/****** Object:  StoredProcedure [dbo].[sp_qt_qt_chutuchitiet]    Script Date: 5/19/2023 7:24:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_qt_chutuchitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_qt_chutuchitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo_by_condition]    Script Date: 5/22/2023 9:42:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_danhsach_canbo_by_condition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_danhsach_canbo_by_condition]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]    Script Date: 5/22/2023 5:40:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_giaithich_bangso_export]    Script Date: 5/23/2023 9:25:39 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_giaithich_bangso_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_giaithich_bangso_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_canbo_quyettoan_quanso]    Script Date: 5/23/2023 9:25:39 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_canbo_quyettoan_quanso]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_canbo_quyettoan_quanso]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_songay_phucap_theongay]    Script Date: 5/24/2023 10:14:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_songay_phucap_theongay]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_songay_phucap_theongay]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_phucap_theongay]    Script Date: 5/24/2023 10:14:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_phucap_theongay]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_phucap_theongay]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_phucap_theongay]    Script Date: 5/24/2023 10:14:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_phucap_theongay]
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@MaPhuCap NVARCHAR(MAX),
	@DonViTinh int,
	@IsOrderChucVu bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Query AS NVARCHAR(MAX)

	SET @Query =
	'
	WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang										AS Thang,
			dsCapNhapBangLuong.Nam											AS Nam,
			bangLuong.Ma_CBo												AS MaCanBo,
			bangLuong.MA_PHUCAP												AS MaPhuCap,
			bangLuong.GIA_TRI / ' + CAST(@DonViTinh AS VARCHAR(100)) + '	AS GiaTri
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			donVi.Ma_DonVi		AS MaDonVi,
			capBac.Ma_Cb			AS MaCapBac,
			capBac.XauNoiMa
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
		  ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac capBac
		  ON canBo.Ma_CB = capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu
		  ON canBo.Ma_CV=chucVu.Ma_Cv
		WHERE
			canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND donVi.Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	)

	SELECT MaDonVi, MaCanBo, TenCanBo, Ten, HSChucVu, MaCapBac, XauNoiMa, ' + @MaPhuCap + ' FROM (
		SELECT
			canBo.MaDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.Ten,
			canBo.HSChucVu,
			canBo.MaCapBac,
			bangLuong.MaPhuCap,
			bangLuong.GiaTri,
			canBo.XauNoiMa
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @MaPhuCap + ')
	) pvt'

	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY HSChucVu , MaCapBac , Ten ';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac , Ten ';
	execute(@Query)
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_songay_phucap_theongay]    Script Date: 5/24/2023 10:14:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_songay_phucap_theongay]
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@MaPhuCap NVARCHAR(MAX),
	@DonViTinh int,
	@IsOrderChucVu bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Query AS NVARCHAR(MAX)
	SET @Query =
	'
	WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang										AS Thang,
			dsCapNhapBangLuong.Nam											AS Nam,
			bangLuong.Ma_CBo												AS MaCanBo,
			bangLuong.MA_PHUCAP												AS MaPhuCap,
			bangLuong.Huongpc_sn											AS SoNgay
		FROM (select * from TL_BangLuong_Thang where thang = '+ CAST(@Thang AS VARCHAR(2)) + ' and nam ='+ CAST(@Nam AS VARCHAR(4)) +') bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			donVi.Ma_DonVi		AS MaDonVi,
			capBac.Ma_Cb			AS MaCapBac,
			capBac.XauNoiMa
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
		  ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac capBac
		  ON canBo.Ma_CB = capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu
		  ON canBo.Ma_CV=chucVu.Ma_Cv
		WHERE
			canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND donVi.Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	)

	SELECT MaDonVi, MaCanBo, TenCanBo, Ten, HSChucVu, MaCapBac, XauNoiMa, ' + @MaPhuCap + ' FROM (
		SELECT
			canBo.MaDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.Ten,
			canBo.HSChucVu,
			canBo.MaCapBac,
			bangLuong.MaPhuCap,
			isnull(bangLuong.SoNgay, dbo.fnTotalDayOfMonth(' + CAST(@Thang AS VARCHAR(2)) + ',' + CAST(@Nam AS VARCHAR(4)) + ')) SoNgay,
			canBo.XauNoiMa
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(SoNgay)
		FOR MaPhuCap IN (' + @MaPhuCap + ')
	) pvt'

	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY HSChucVu , MaCapBac , Ten ';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac , Ten ';
	execute(@Query)
END
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_find_canbo_quyettoan_quanso]    Script Date: 5/23/2023 9:25:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_find_canbo_quyettoan_quanso] @maDonVi nvarchar(MAX), @thang int, @nam int
as
begin

declare @thangtruoc int, @namtruoc int, @strThang nvarchar(2)
if @thang = 1
	begin
		set @thangtruoc = 12;
		set @strThang = concat('0', @thang);
		set @namtruoc = @nam - 1;
	end
else if (@thang > 1 and @thang < 10)
	begin
		set @thangtruoc = @thang - 1;
		set @strThang = concat('0', @thang);
		set @namtruoc = @nam;
	end
else 
	begin
		set @thangtruoc = @thang - 1;
		set @strThang = @thang;
		set @namtruoc = @nam;
end

select	cbThangnay.Id, cbThangnay.BHTN, cbThangnay.bNuocNgoai, cbThangnay.Cb_KeHoach, cbThangnay.Cccd, cbThangnay.DateCreated, cbThangnay.DateModified, cbThangnay.Dia_Chi, cbThangnay.Dien_Thoai, 
		cbThangnay.GTGC, cbThangnay.HeSoLuong, cbThangnay.HsLuongKeHoach, cbThangnay.HsLuongTran, cbThangnay.iTrangThai, cbThangnay.IdLuongTran, cbThangnay.IsDelete, cbThangnay.IsLock, 
		cbThangnay.Is_Nam, cbThangnay.Khong_Luong, cbThangnay.Ma_BL, cbThangnay.Ma_CanBo, cbThangnay.Ma_CB, cbThangnay.Ma_CV, cbThangnay.Ma_DiaBan_HC, cbThangnay.Ma_Hieu_CanBo, 
		cbThangnay.Ma_KhoBac, cbThangnay.Ma_PBan, cbThangnay.MaSo_DV_SDNS, cbThangnay.MaSo_VAT, cbThangnay.Ma_TangGiam, cbThangnay.Ma_TangGiamCu, cbThangnay.MaTK_LQ, cbThangnay.Nam, cbThangnay.Nam_TN, 
		cbThangnay.Nam_VK, cbThangnay.NgayCap_CMT, cbThangnay.Ngay_NhanCB, cbThangnay.Ngay_NN, cbThangnay.NgaySinh, cbThangnay.Ngay_TN, cbThangnay.NgayTruyLinh, cbThangnay.Ngay_XN, cbThangnay.Nhom, 
		cbThangnay.NoiCap_CMT, cbThangnay.NoiCongTac, cbThangnay.PCCV, cbThangnay.Parent, cbThangTruoc.Parent as ParentCu, cbThangnay.[Readonly], cbThangnay.So_CMT, cbThangnay.So_SoLuong, cbThangnay.So_TaiKhoan, cbThangnay.Splits, 
		cbThangnay.Ten_CanBo, cbThangnay.Ten_DonVi, cbThangnay.Ten_KhoBac, cbThangnay.Thang, cbThangnay.Thang_TNN, cbThangnay.ThoiHan_TangCb, 
		cbThangnay.TM, cbThangnay.UserCreator, cbThangnay.UserModifier, cbThangnay.bKhongTinhNTN,
		case 
			when cbThangTruoc.Ma_Cb = cbThangnay.Ma_CB then null 
			else cbThangTruoc.Ma_CB 
		end as Ma_CbCu
from (select * from TL_DM_CanBo where Thang = @thang and Nam = @nam and Parent = @maDonVi) cbThangnay
left join (select * from TL_DM_CanBo where Thang = @thangtruoc and Nam = @namtruoc) cbThangTruoc on cbThangnay.Ma_CanBo = concat(@nam , @strThang, substring(cbThangTruoc.Ma_CanBo, 7, len(cbThangTruoc.Ma_CanBo) - 6))
end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_giaithich_bangso_export]    Script Date: 5/23/2023 9:25:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_get_data_giaithich_bangso_export]
	@lstId nvarchar(max),
	@lstCach nvarchar(100),
	@nam int
AS
BEGIN
	CREATE TABLE #tmp(id nvarchar(100))
	DECLARE @isHaveCachTinhLuong bit = 0

	if(ISNULL(@lstCach, '') <> '')
	BEGIN
		INSERT INTO #tmp(id)
		SELECT *
		FROM f_split(@lstCach)

		SET @isHaveCachTinhLuong = 1
	END

	SELECT tbl.ID, Thang INTO #tblMaxThang
	FROM f_split(@lstId) as tmp
	INNER JOIN TL_QT_ChungTu as tbl on tmp.Item = tbl.ID;


	with ctct as (
	  select dt.XauNoiMa,  MaCachTl, MaCb,  Sum(TongCong) AS TongCong,
		   Sum(DieuChinh) AS DieuChinh,
		 Sum(ISNULL(dt.DDuToan, 0)) As DDuToan
	  from TL_QT_ChungTu as tbl
	  INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
	  where  (ISNULL(@lstId, '') <> '' AND tbl.ID in (SELECT *  FROM f_split(@lstId)))
		AND ((dt.MaCachTl = '' AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	  group by dt.XauNoiMa, dt.MaCachTl, dt.MaCb
	)

SELECT 
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
       mlns.sTNG AS TNG,
       mlns.sTNG1 AS TNG1,
       mlns.sTNG2 AS TNG2,
       mlns.sTNG3 AS TNG3,
	   ctct.MaCb as MaCb,
       sMoTa AS Mota,
       MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
       DieuChinh,
     DDuToan,
	 ctct.MaCb as MaCb,
	 capbac.Parent as MaCbCha
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
LEFT JOIN TL_DM_CapBac capbac ON ctct.MaCb = capbac.Ma_Cb

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam
  and bHangCha = 0

order by XauNoiMa

DROP TABLE #tblMaxThang
DROP TABLE #tmp
END
;
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]    Script Date: 5/22/2023 5:40:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@IdDonVi nvarchar(10),
	@TenDonVi nvarchar(250),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int
AS
BEGIN

DECLARE @iIdChungTuOld uniqueidentifier,
	@iIdChungTuDuToanOld uniqueidentifier

SELECT TOP(1) @iIdChungTuOld = ol.ID, @iIdChungTuDuToanOld = case ol.IIdChungTuDuToan when '' then NULL else ol.IIdChungTuDuToan end
FROM TL_QT_ChungTu as ol
INNER JOIN (SELECT * FROM TL_QT_ChungTu WHERE ID = @IdChungTu) as nw 
on ol.Thang = (CASE WHEN nw.Thang = 1 THEN 12 ELSE nw.Thang - 1 END)
	AND ol.Nam = (CASE WHEN nw.Thang = 1 THEN nw.Nam - 1 ELSE nw.Nam END)
	AND ol.Ma_DonVi = nw.Ma_DonVi
	AND ol.sTongHop IS NOT NULL
ORDER BY ol.Ngay_tao DESC

CREATE TABLE #tmp(sXauNoiMa nvarchar(100), DDuToan decimal)

IF(@iIdChungTuOld IS NOT NULL AND @iIdChungTuDuToanOld IS NOT NULL)
BEGIN
	UPDATE TL_QT_ChungTu SET IIdChungTuDuToan = @iIdChungTuDuToanOld WHERE ID = @IdChungTu

	INSERT INTO #tmp(sXauNoiMa, DDuToan)
	SELECT DISTINCT XauNoiMa, ISNULL(DDuToan, 0)
	FROM TL_QT_ChungTuChiTiet 
	WHERE Id_ChungTu = @iIdChungTuOld AND ISNULL(DDuToan, 0) <> 0
END

--INSERT INTO [dbo].[TL_QT_ChungTuChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan], [MaCb])
INSERT INTO [dbo].[TL_QT_ChungTuChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan])
SELECT @idChungTu,
       MLNS_Id,
       MLNS_Id_Parent,
       XauNoiMa,
       LNS,
       L,
       K,
       M,
       TM,
       TTM,
       NG,
       TNG,
       TNG1,
       TNG2,
       TNG3,
       MoTa,
       NULL,
       @NamNganSach,
       @NguonNganSach,
       @NamLamViec,
       1,
       NULL,
       @IdDonVi,
       @TenDonVi,
       NULL,
       NULL,
       GETDATE(),
       NULL,
       NULL,
       NULL,
       sum(Isnull(TongCong, 0)),
       BHangCha,
       NULL,
       sum(Isnull(SoNgay, 0)),
       sum(Isnull(SoNguoi, 0)),
       sum(Isnull(DieuChinh, 0)),
	   MaCachTl,
	   ISNULL(tmp.DDuToan, 0)
	   --,ct.MaCb
FROM TL_QT_ChungTuChiTiet ct
LEFT JOIN #tmp as tmp on ct.XauNoiMa = tmp.sXauNoiMa
WHERE ct.Id_ChungTu in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY MLNS_Id,
         MLNS_Id_Parent,
         XauNoiMa,
         LNS,
         L,
         K,
         M,
         TM,
         TTM,
         NG,
         TNG,
         TNG1,
         TNG2,
         TNG3,
         MoTa,
         BHangCha,
		 MaCachTl,
		 tmp.sXauNoiMa,
		 ISNULL(tmp.DDuToan, 0)
		 --,ct.MaCb

	-- add du toan thang truoc da co
	--INSERT INTO [dbo].[TL_QT_ChungTuChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan], [MaCb])
	INSERT INTO [dbo].[TL_QT_ChungTuChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan])
	SELECT @idChungTu, 
		ml.iID_MLNS, 
		ml.iID_MLNS_Cha, 
		tmp.sXauNoiMa, 
		ml.sLNS,
		ml.sL, 
		ml.sK, 
		ml.sM, 
		ml.sTM, 
		ml.sTTM, 
		ml.sNG,
		ml.sTNG, 
		ml.sTNG1, 
		ml.sTNG2, 
		ml.sTNG3,
		ml.sMoTa,
		NULL, 
		@NamNganSach,
       @NguonNganSach,
       @NamLamViec,
       1,
       NULL,
       @IdDonVi,
       @TenDonVi,
       NULL,
       NULL,
       GETDATE(),
       NULL,
       NULL,
       NULL,
       Isnull(TongCong, 0),
       ml.bHangCha,
       NULL,
       Isnull(SoNgay, 0),
       Isnull(SoNguoi, 0),
       Isnull(DieuChinh, 0),
	   '',
	   ISNULL(tmp.DDuToan, 0)
	   --,dt.MaCb
	FROM #tmp as tmp
	INNER JOIN NS_MucLucNganSach as ml on ml.iNamLamViec = @NamLamViec AND ml.sXauNoiMa = tmp.sXauNoiMa
	LEFT JOIN TL_QT_ChungTuChiTiet as dt on tmp.sXauNoiMa = dt.XauNoiMa AND dt.Id_ChungTu = @idChungTu
	WHERE dt.XauNoiMa IS NULL AND ISNULL(tmp.DDuToan, 0) <> 0

	DROP TABLE #tmp
END
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo_by_condition]    Script Date: 5/22/2023 9:42:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Procedure [dbo].[sp_tl_find_danhsach_canbo_by_condition]
	@thang int,
	@nam int,
	@maDonVi nvarchar(250)
AS
	Select
	  canbo.[Id]
      ,[BHTN]
      ,[bNuocNgoai]
      ,[Cb_KeHoach]
      ,[Cccd]
      ,[DateCreated]
      ,[DateModified]
      ,[Dia_Chi]
      ,[Dien_Thoai]
      ,[GTGC]
      ,[HeSoLuong]
      ,[HsLuongKeHoach]
      ,[HsLuongTran]
      ,canbo.[iTrangThai]
      ,[IdLuongTran]
      ,[IsDelete]
      ,[IsLock]
      ,[Is_Nam]
      ,[Khong_Luong]
      ,[Ma_BL]
      ,[Ma_CanBo]
      ,canbo.[Ma_CB]
      ,[Ma_CbCu]
      ,canbo.[Ma_CV]
      ,[Ma_DiaBan_HC]
      ,[Ma_Hieu_CanBo]
      ,[Ma_KhoBac]
      ,[Ma_PBan]
      ,[MaSo_DV_SDNS]
      ,[MaSo_VAT]
      ,[Ma_TangGiam]
      ,[Ma_TangGiamCu]
      ,[MaTK_LQ]
      ,[Nam]
      ,[Nam_TN]
      ,[Nam_VK]
      ,[NgayCap_CMT]
      ,[Ngay_NhanCB]
      ,[Ngay_NN]
      ,[NgaySinh]
      ,[Ngay_TN]
      ,[NgayTruyLinh]
      ,[Ngay_XN]
      ,[Nhom]
      ,[NoiCap_CMT]
      ,[NoiCongTac]
      ,[PCCV]
      ,canbo.[Parent]
      ,canbo.[Readonly]
      ,[So_CMT]
      ,[So_SoLuong]
      ,[So_TaiKhoan]
      ,canbo.[Splits]
      ,[Ten_CanBo]
      ,[Ten_KhoBac]
      ,[Thang]
      ,[Thang_TNN]
      ,[ThoiHan_TangCb]
      ,[TM]
      ,[UserCreator]
      ,[UserModifier]
      ,[bKhongTinhNTN],
	  donvi.Ten_DonVi as Ten_DonVi,
	  ISNULL(capbac.Note, '') CapBac,
	  ISNULL(chucvu.Ten_Cv, '') ChucVu,
	  dbo.f_split_empty(canbo.Ten_CanBo) AS Ten
	From TL_DM_CanBo canbo
	Join TL_DM_CapBac capbac on capbac.Ma_Cb = canbo.Ma_CB
	Left join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi 
	Left join TL_DM_ChucVu chucvu on canbo.Ma_CV = chucvu.Ma_Cv
	Where canbo.IsDelete = 1 
		and canbo.Thang = @thang 
		and canbo.Nam = @nam
		and (@maDonVi = '' or canbo.Parent = @maDonVi)
	--Order By canbo.Parent, canbo.Ma_CV desc, canbo.Ma_CB desc
;
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_qt_qt_chutuchitiet]    Script Date: 5/19/2023 7:24:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_qt_qt_chutuchitiet]
  @strIdDonVi NVARCHAR (2000),
  @strThang NVARCHAR (50),
  @strNam int,
  @strThangTruoc NVARCHAR (50),
  @strNamTruoc int
AS
BEGIN

if (SELECT count (*)  FROM f_split(@strThang)) = 1
begin 

with Thang as (
		select ctct.MLNS_Id_Parent MlnsIdParent, XauNoiMa, MoTa, NamLamViec, 
		sum(ctct.TongSo ) TongSo, sum(ctct.ThieuUy) ThieuUy, sum(ctct.TrungUy) TrungUy, sum(ctct.ThuongUy) ThuongUy, sum(ctct.DaiUy) DaiUy,sum(ctct.ThieuTa) ThieuTa, sum(ctct.TrungTa) TrungTa, sum(ctct.ThuongTa) ThuongTa, sum(ctct.DaiTa) DaiTa, sum(ctct.Tuong) Tuong,
		sum(ctct.BinhNhi) BinhNhi, sum(ctct.BinhNhat) BinhNhat, sum(ctct.HaSi) HaSi, sum(ctct.TrungSi) TrungSi, sum(ctct.ThuongSi ) ThuongSi,
		sum(ctct.ThieuUyCn) ThieuUyCn, sum(ctct.TrungUycn) TrungUycn, sum(ctct.ThuongUyCn) ThuongUyCn, sum(ctct.DaiUyCn) DaiUyCn,sum(ctct.ThieuTaCn) ThieuTaCn, sum(ctct.TrungTaCn) TrungTaCn, sum(ctct.ThuongTaCn) ThuongTaCn,
		sum(ctct.CNQP) CNQP, sum(ctct.LDHD) LDHD, sum(ctct.VCQP) VCQP, sum(ctct.QNCN) Qncn, sum(ctct.CCQP) Ccqp
from  tl_qs_chungtuchitiet ctct 
where 1=1  
and ctct.Thang in  (SELECT * FROM f_split(@strThang))
and ctct.NamLamViec = @strNam
and ctct.Id_DonVi in  (SELECT * FROM f_split(@strIdDonVi))
group by  xaunoima, mota , NamLamViec, MLNS_Id_Parent
)
select * from  thang  order by  xaunoima;
end 

else
WITH ThoiGianTruoc as (
select  ctct.MLNS_Id_Parent MlnsIdParent, XauNoiMa, 
		case 
			when (select count(*) from f_split(@strThangTruoc)) = 1 then MoTa
			when (select count(*) from f_split(@strThangTruoc)) = 3 then replace(MoTa, N'tháng', N'quý')
			when (select count(*) from f_split(@strThangTruoc)) = 12 then replace(MoTa, N'tháng', N'năm')
		end as MoTa,
		NamLamViec, sum(ctct.TongSo ) TongSo, sum(ctct.ThieuUy) ThieuUy, sum(ctct.TrungUy) TrungUy, sum(ctct.ThuongUy) ThuongUy, sum(ctct.DaiUy) DaiUy,sum(ctct.ThieuTa) ThieuTa, sum(ctct.TrungTa) TrungTa, sum(ctct.ThuongTa) ThuongTa, sum(ctct.DaiTa) DaiTa, sum(ctct.Tuong) Tuong,
		sum(ctct.BinhNhi) BinhNhi, sum(ctct.BinhNhat) BinhNhat, sum(ctct.HaSi) HaSi, sum(ctct.TrungSi) TrungSi, sum(ctct.ThuongSi ) ThuongSi,
		sum(ctct.ThieuUyCn) ThieuUyCn, sum(ctct.TrungUycn) TrungUycn, sum(ctct.ThuongUyCn) ThuongUyCn, sum(ctct.DaiUyCn) DaiUyCn,sum(ctct.ThieuTaCn) ThieuTaCn, sum(ctct.TrungTaCn) TrungTaCn, sum(ctct.ThuongTaCn) ThuongTaCn,
		sum(ctct.CNQP) CNQP, sum(ctct.LDHD) LDHD, sum(ctct.VCQP) VCQP, sum(ctct.QNCN) Qncn, sum(ctct.CCQP) Ccqp
from  tl_qs_chungtuchitiet ctct 
where 1=1  
and ctct.Thang in (SELECT * FROM f_split(@strThangTruoc))
and ctct.NamLamViec = @strNamTruoc
and ctct.Id_DonVi in  (SELECT * FROM f_split(@strIdDonVi))
and XauNoiMa in ('100','500')
group by  xaunoima, mota , NamLamViec, MLNS_Id_Parent
),
ThoiGianNay  as(
select  ctct.MLNS_Id_Parent MlnsIdParent, XauNoiMa, 
		case 
			when (select count(*) from f_split(@strThang)) = 1 then MoTa
			when (select count(*) from f_split(@strThang)) = 3 then replace(MoTa, N'tháng', N'quý')
			when (select count(*) from f_split(@strThang)) = 12 then replace(MoTa, N'tháng', N'năm')
		end as MoTa,
		NamLamViec, sum(ctct.TongSo ) TongSo, sum(ctct.ThieuUy) ThieuUy, sum(ctct.TrungUy) TrungUy, sum(ctct.ThuongUy) ThuongUy, sum(ctct.DaiUy) DaiUy,sum(ctct.ThieuTa) ThieuTa, sum(ctct.TrungTa) TrungTa, sum(ctct.ThuongTa) ThuongTa, sum(ctct.DaiTa) DaiTa, sum(ctct.Tuong) Tuong,
		sum(ctct.BinhNhi) BinhNhi, sum(ctct.BinhNhat) BinhNhat, sum(ctct.HaSi) HaSi, sum(ctct.TrungSi) TrungSi, sum(ctct.ThuongSi ) ThuongSi,
		sum(ctct.ThieuUyCn) ThieuUyCn, sum(ctct.TrungUycn) TrungUycn, sum(ctct.ThuongUyCn) ThuongUyCn, sum(ctct.DaiUyCn) DaiUyCn,sum(ctct.ThieuTaCn) ThieuTaCn, sum(ctct.TrungTaCn) TrungTaCn, sum(ctct.ThuongTaCn) ThuongTaCn,
		sum(ctct.CNQP) CNQP, sum(ctct.LDHD) LDHD, sum(ctct.VCQP) VCQP, sum(ctct.QNCN) Qncn, sum(ctct.CCQP) Ccqp
from  tl_qs_chungtuchitiet ctct 
where 1=1  
and ctct.Thang in  (SELECT * FROM f_split(@strThang))
and ctct.NamLamViec = @strNam
and ctct.Id_DonVi in  (SELECT * FROM f_split(@strIdDonVi))
and  XauNoiMa not in ('100','500')
group by  xaunoima, mota , NamLamViec, MLNS_Id_Parent
)
select * from ThoiGianTruoc  
union all 
select * from ThoiGianNay 
order by xaunoima;
END
;
GO