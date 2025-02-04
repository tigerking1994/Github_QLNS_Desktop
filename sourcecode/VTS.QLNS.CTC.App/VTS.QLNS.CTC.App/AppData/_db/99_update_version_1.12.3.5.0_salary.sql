/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangke_trichthue_tncn]    Script Date: 29/11/2022 11:03:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangke_trichthue_tncn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangke_trichthue_tncn]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_update_bangluongthang_hsqcs]    Script Date: 29/11/2022 11:31:36 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_update_bangluongthang_hsqcs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_update_bangluongthang_hsqcs]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_phuCapChiLuong]    Script Date: 29/11/2022 11:31:36 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_phuCapChiLuong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_phuCapChiLuong]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data]    Script Date: 29/11/2022 11:31:36 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_data]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_data]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]    Script Date: 29/11/2022 4:43:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]    Script Date: 29/11/2022 4:43:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TungNH
-- Create date: 25/04/2022
-- Description:	Báo cáo giải thích chi tiết lương theo ngạch, cấp bậc
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@MaPhuCap NVARCHAR(MAX),
	@MaPhuCapCount NVARCHAR(MAX),
	@DonViTinh int,
	@IsSummary bit
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
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_CBo			AS MaCanBo,
			bangLuong.MA_PHUCAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND bangLuong.Gia_Tri <> 0
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			donVi.Ma_DonVi		AS MaDonVi,
			canBo.Ma_Hieu_CanBo	AS MaHieuCanBo,
			capBac.Ma_Cb			AS MaCapBac,
			CASE
				WHEN capBac.XauNoiMa LIKE ''1%'' OR capBac.XauNoiMa LIKE ''4%'' THEN capBac.Lht_Hs
				ELSE canBo.HeSoLuong
			END AS HeSoLuong
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND canBo.Parent IN (SELECT * FROM f_split( ''' + @MaDonVi + '''))
	), SoLieuBaoCao AS (
		SELECT
			CASE WHEN ' + CAST(@IsSummary AS VARCHAR(1)) + ' = 1 THEN NULL ELSE canBo.MaDonVi END	AS MaDonVi,
			canBo.MaCapBac																			AS MaCapBac,
			canBo.HeSoLuong																			AS HeSoLuong,
			bangLuongThang.MaPhuCap																	AS MaPhuCap,
			COUNT(canBo.MaCanBo)																	AS SoNguoi,
			SUM(bangLuongThang.GiaTri) / ' + CAST(@DonViTinh AS VARCHAR(100)) + '					AS GiaTri
		FROM ThongTinCanBo canBo
		INNER JOIN BangLuongThang bangLuongThang
			ON bangLuongThang.MaCanBo = canBo.MaCanBo
		GROUP BY MaDonVi, MaCapBac, HeSoLuong, MaPhuCap
	), CanBoLuongCapBac AS (
		SELECT
			bangLuong.Ma_DonVi,
			bangLuong.Ma_CB			AS MaCapBac,
			bangLuong.GIA_TRI		AS HeSoLuong
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap = ''LHT_HS''
			AND bangLuong.Gia_Tri <> 0
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	)

	SELECT ''' + @MaCachTl + ''' AS CachTl, (SELECT COUNT(*) FROM CanBoLuongCapBac canBo INNER JOIN TL_DM_CapBac as cb on canBo.MaCapBac = cb.Ma_Cb WHERE ' + (CASE WHEN @IsSummary = 1 THEN '' ELSE ' canBo.Ma_DonVi = pvt.MaDonVi AND ' END)+ ' canBo.MaCapBac = pvt.MaCapBac AND ((cb.XauNoiMa LIKE ''1%'' OR cb.XauNoiMa LIKE ''4%'') OR canBo.HeSoLuong = pvt.HeSoLuong)) AS SoNguoi, pvt.* FROM (
		SELECT
			MaDonVi,
			MaCapBac,
			HeSoLuong,
			DATA,
			COLUMN_NAME + MaPhuCap AS PIV_COL
		FROM SoLieuBaoCao
		CROSS APPLY (
			VALUES (''COUNT_'', SoNguoi), ('''', GiaTri) 
		) CS(COLUMN_NAME, DATA)
	) a
	PIVOT (
		SUM(DATA)
		FOR PIV_COL IN (' + @MaPhuCap + ', ' + @MaPhuCapCount + ')
	) pvt ORDER BY MaDonVi, MaCapBac, HeSoLuong'

	execute(@Query)
END
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_clone_data]    Script Date: 29/11/2022 11:31:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_clone_data] 
	-- Add the parameters for the stored procedure here
	@userCreator varchar(100),
	@sourceYear int,
	@destinationYear int,
	@isUpdatedMLNS int,
	@isUpdatedNSDV int,
	@isUpdatedBQuanLy int,
	@isUpdateMLQS int,
	@isUpdateDanhMucChuyenNganh int,
	@isUpdateDanhMucNganh int,
	@isUpdateMuclucSkt int,
	@isUpdateDanhMucCapPhat int,
	@isUpdateCauHinhChiTieuLuongMLNS int,
	@isUpdateDmCapBacKh int,
	@isUpdateNSSKT int,
	@isUpdateCauHinhHeThong int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	if (@isUpdateDanhMucNganh = 1)
		Begin
			DELETE FROM DanhMuc where INamLamViec = @destinationYear and [sType] = 'NS_Nganh_Nganh';
			INSERT INTO [dbo].[DanhMuc]
			   ([sType]
			   ,[iID_MaDanhMuc]
			   ,[sTen]
			   ,[sGiaTri]
			   ,[sMoTa]
			   ,[iThuTu]
			   ,[iNamLamViec]
			   ,[iTrangThai]
			   ,[dNgayTao]
			   ,[sNguoiTao]
			   ,[dNgaySua]
			   ,[sNguoiSua]
			   ,[Tag]
			   ,[Log]
			   ,[NganSachNganh])
			 SELECT
				[sType]
				,[iID_MaDanhMuc]
				,[sTen]
				,[sGiaTri]
				,[sMoTa]
				,[iThuTu]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,[sNguoiSua]
				,[Tag]
				,[Log]
				,[NganSachNganh]
		  FROM [dbo].[DanhMuc] where iNamLamViec = @sourceYear and [sType] = 'NS_Nganh_Nganh';
		End;

	if (@isUpdateDanhMucChuyenNganh = 1)
		Begin
			DELETE FROM DanhMuc where iNamLamViec = @destinationYear and [sType] = 'NS_Nganh';
			INSERT INTO [dbo].[DanhMuc]
				([sType]
				,[iID_MaDanhMuc]
				,[sTen]
				,[sGiaTri]
				,[sMoTa]
				,[iThuTu]
				,[iNamLamViec]
				,[iTrangThai]
				,[dNgayTao]
				,[sNguoiTao]
				,[dNgaySua]
				,[sNguoiSua]
				,[Tag]
				,[Log]
				,[NganSachNganh])
			 SELECT
				[sType]
				,[iID_MaDanhMuc]
				,[sTen]
				,[sGiaTri]
				,[sMoTa]
				,[iThuTu]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,null
				,[Tag]
				,[Log]
				,[NganSachNganh]
		  FROM [dbo].[DanhMuc] where iNamLamViec = @sourceYear and [sType] = 'NS_Nganh';
		End;

	if (@isUpdateMLQS = 1)
		Begin
			Delete FROM NS_QS_MucLuc where iNamLamViec = @destinationYear;
			INSERT INTO [NS_QS_MucLuc]
				([iID_MLNS]
				,[iID_MLNS_Cha]
				,[sM]
				,[sTM]
				,[sKyHieu]
				,[sMoTa]
				,[iThuTu]
				,[sHienThi]
				,[bHangCha]
				,[iTrangThai]
				,[iNamLamViec])
			SELECT
				[iID_MLNS]
				,[iID_MLNS_Cha]
				,[sM]
				,[sTM]
				,[sKyHieu]
				,[sMoTa]
				,[iThuTu]
				,[sHienThi]
				,[bHangCha]
				,[iTrangThai]
				,@destinationYear
			  FROM [NS_QS_MucLuc]  where iNamLamViec = @sourceYear;
		END;

	if (@isUpdatedBQuanLy = 1)
		Begin
			DELETE FROM DM_BQuanLy where iNamLamViec = @destinationYear;
			INSERT INTO [DM_BQuanLy]
				([iID_MaBQuanLy]
				,[sTenBQuanLy]
				,[sKyHieu]
				,[sMoTa]
				,[iNamLamViec]
				,[iTrangThai]
				,[dNgayTao]
				,[sNguoiTao]
				,[dNgaySua]
				,[sNguoiSua])
			 SELECT
				[iID_MaBQuanLy]
				,[sTenBQuanLy]
				,[sKyHieu]
				,[sMoTa]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,null
			FROM [DM_BQuanLy] where iNamLamViec = @sourceYear;
		End;
	if (@isUpdatedNSDV = 1)
		Begin
			Delete FROM  [DonVi] where iNamLamViec = @destinationYear;
			INSERT INTO [dbo].[DonVi]
				([iID_Parent]
				,[iID_MaDonVi]
				,[sTenDonVi]
				,[sKyHieu]
				,[sMoTa]
				,[iLoai]
				,[iNamLamViec]
				,[iTrangThai]
				,[dNgayTao]
				,[sNguoiTao]
				,[dNgaySua]
				,[sNguoiSua]
				,[Tag]
				,[Log]
				,[LoaiNganSach]
				,[bCoNSNganh]
				,[iKhoi])
			 SELECT [iID_Parent]
				,[iID_MaDonVi]
				,[sTenDonVi]
				,[sKyHieu]
				,[sMoTa]
				,[iLoai]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,null
				,[Tag]
				,[Log]
				,[LoaiNganSach]
				,[bCoNSNganh]
				,[iKhoi]
			FROM [DonVi] where iNamLamViec = @sourceYear;
			INSERT INTO [NguoiDung_DonVi]
           ([iID_MaNguoiDung]
           ,[iID_MaDonVi]
           ,[iNamLamViec]
           ,[iSTT]
           ,[iTrangThai]
           ,[bPublic]
           ,[dNgayTao]
           ,[iSoLanSua]
           ,[dNgaySua]
           ,[sIPSua]
           ,[sTenDonVi])
			 SELECT [iID_MaNguoiDung]
			  ,[iID_MaDonVi]
			  ,@destinationYear
			  ,[iSTT]
			  ,[iTrangThai]
			  ,[bPublic]
			  ,[dNgayTao]
			  ,[iSoLanSua]
			  ,[dNgaySua]
			  ,[sIPSua]
			  ,[sTenDonVi]
		  FROM [NguoiDung_DonVi] where iNamLamViec = @sourceYear;
		End;

	if (@isUpdatedMLNS = 1)
		Begin
			DELETE FROM [NS_MucLucNganSach] WHERE iNamLamViec = @destinationYear;
			INSERT INTO [NS_MucLucNganSach]
				([iID_MLNS]
				,[iID_MLNS_Cha]
				,[sXauNoiMa]
				,[sLNS]
				,[sL]
				,[sK]
				,[sM]
				,[sTM]
				,[sTTM]
				,[sNG]
				,[sTNG]
				,[sMoTa]
				,[iNamLamViec]
				,[bHangCha]
				,[iTrangThai]
				,[iID_MaBQuanLy]
				,[dNgayTao]
				,[sNguoiTao]
				,[dNgaySua]
				,[sNguoiSua]
				,[Tag]
				,[Log]
				,[iLock]
				,[iLoai]
				,[sTNG1]
				,[sTNG2]
				,[sTNG3]
				,[sChiTietToi]
				,[bNgay]
				,[bSoNguoi]
				,[bTonKho]
				,[bTuChi]
				,[bHangNhap]
				,[bHangMua]
				,[bHienVat]
				,[bDuPhong]
				,[bPhanCap]
				,[sNhapTheoTruong]
				,[iID_MaDonVi]
				,[sCPChiTietToi]
				,[bHangChaDuToan]
				,[bHangChaQuyetToan]
				,[sDuToanChiTietToi]
				,[sQuyetToanChiTietToi])
			 SELECT [iID_MLNS]
				,[iID_MLNS_Cha]
				,[sXauNoiMa]
				,[sLNS]
				,[sL]
				,[sK]
				,[sM]
				,[sTM]
				,[sTTM]
				,[sNG]
				,[sTNG]
				,[sMoTa]
				,@destinationYear
				,[bHangCha]
				,[iTrangThai]
				,[iID_MaBQuanLy]
				,GETDATE()
				,@userCreator
				,null
				,null
				,[Tag]
				,[Log]
				,[iLock]
				,[iLoai]
				,[sTNG1]
				,[sTNG2]
				,[sTNG3]
				,[sChiTietToi]
				,[bNgay]
				,[bSoNguoi]
				,[bTonKho]
				,[bTuChi]
				,[bHangNhap]
				,[bHangMua]
				,[bHienVat]
				,[bDuPhong]
				,[bPhanCap]
				,[sNhapTheoTruong]
				,[iID_MaDonVi]
				,[sCPChiTietToi]
				,[bHangChaDuToan]
				,[bHangChaQuyetToan]
				,[sDuToanChiTietToi]
				,[sQuyetToanChiTietToi]
		  FROM [NS_MucLucNganSach] where iNamLamViec = @sourceYear;
		  DELETE FROM [NS_NguoiDung_LNS] WHERE iNamLamViec = @destinationYear;
		  INSERT INTO [NS_NguoiDung_LNS]
			   ([sMaNguoiDung]
			   ,[sLNS]
			   ,[iNamLamViec])
			   (SELECT [sMaNguoiDung]
				  ,[sLNS]
				  ,@destinationYear
				FROM [NS_NguoiDung_LNS] where iNamLamViec = @sourceYear)
		End;

	if (@isUpdateMuclucSkt = 1)
		Begin
			DELETE FROM [NS_SKT_MucLuc] WHERE iNamLamViec = @destinationYear;
			INSERT INTO [dbo].[NS_SKT_MucLuc]
			   ([iID_MLSKT]
			   ,[SKyHieu]
			   ,[sM]
			   ,[sNG_Cha]
			   ,[sNG]
			   ,[sSTT]
			   ,[sSTTBC]
			   ,[sMoTa]
			   ,[KyHieuCha]
			   ,[bHangCha]
			   ,[iTrangThai]
			   ,[iNamLamViec]
			   ,[dNgayTao]
			   ,[dNguoiTao]
			   ,[dNgaySua]
			   ,[dNguoiSua]
			   ,[Tag]
			   ,[Log]
			   ,[Muc]
			   ,[iID_MLSKTCha]
			   ,[sLoaiNhap])
			SELECT [iID_MLSKT]
			   ,[SKyHieu]
			   ,[sM]
			   ,[sNG_Cha]
			   ,[sNG]
			   ,[sSTT]
			   ,[sSTTBC]
			   ,[sMoTa]
			   ,[KyHieuCha]
			   ,[bHangCha]
			   ,[iTrangThai]
			  ,@destinationYear
			  ,GETDATE()
			  ,@userCreator
			  ,null
			  ,null
			  ,[Tag]
			  ,[Log]
			  ,[Muc]
			  ,[iID_MLSKTCha]
			   ,[sLoaiNhap]
		  FROM [dbo].[ns_SKT_MucLuc] where iNamLamViec = @sourceYear;
		End;

	if (@isUpdateDanhMucCapPhat = 1)
		Begin
			DELETE FROM [CP_DanhMuc] WHERE iNamLamViec = @destinationYear;
			INSERT INTO [dbo].[CP_DanhMuc]
			   ([iID_MaDMCapPhat]
			   ,[sTen]
			   ,[sTenThongTriCap]
			   ,[sTenThongTriThu]
			   ,[LNS]
			   ,[sMoTa]
			   ,[OrderIndex]
			   ,[iNamLamViec]
			   ,[iTrangThai]
			   ,[dNgayTao]
			   ,[sNguoiTao]
			   ,[dNgaySua]
			   ,[sNguoiSua]
			   ,[Tag]
			   ,[Log])
			SELECT [iID_MaDMCapPhat]
			   ,[sTen]
			   ,[sTenThongTriCap]
			   ,[sTenThongTriThu]
			   ,[LNS]
			   ,[sMoTa]
				,[OrderIndex]
				,@destinationYear
				,[iTrangThai]
				,GETDATE()
				,@userCreator
				,null
				,null
				,[Tag]
				,[Log]
			FROM [dbo].[CP_DanhMuc] where iNamLamViec = @sourceYear;
		End;

	if (@isUpdateCauHinhChiTieuLuongMLNS = 1)
		Begin
			DELETE FROM [TL_PhuCap_MLNS] WHERE NAM = @destinationYear;
			INSERT INTO [dbo].[TL_PhuCap_MLNS]
			   ([Ma_PhuCap]
			   ,[Ten_PhuCap]
			   ,[Ma_CachTL]
			   ,[XauNoiMa]
			   ,[LNS]
			   ,[L]
			   ,[K]
			   ,[M]
			   ,[TM]
			   ,[TTM]
			   ,[NG]
			   ,[MoTa]
			   ,[Ma_NguonNganSach]
			   ,[NguonNganSach]
			   ,[DateCreated]
			   ,[UserCreator]
			   ,[DateModified]
			   ,[UserModifier]
			   ,[iTrangThai]
			   ,[idPhuCap]
			   ,[idCachTinhLuong]
			   ,[idNguonNganSach]
			   ,[idMlns]
			   ,[Ma_Cb]
			   ,[ChiTietToi]
			   ,[Nam])
		 SELECT tbl.[Ma_PhuCap]
			   ,tbl.[Ten_PhuCap]
			   ,tbl.[Ma_CachTL]
			   ,tbl.[XauNoiMa]
			   ,tbl.[LNS]
			   ,tbl.[L]
			   ,tbl.[K]
			   ,tbl.[M]
			   ,tbl.[TM]
			   ,tbl.[TTM]
			   ,tbl.[NG]
			   ,tbl.[MoTa]
			   ,tbl.[Ma_NguonNganSach]
			   ,tbl.[NguonNganSach]
			   ,GETDATE()
			   ,@userCreator
			   ,null
			   ,null
			   ,tbl.[iTrangThai]
			   ,tbl.[idPhuCap]
			   ,tbl.[idCachTinhLuong]
			   ,tbl.[idNguonNganSach]
			   ,ml.iID
			   ,tbl.[Ma_Cb]
			   ,tbl.[ChiTietToi]
			   ,@destinationYear 
			   FROM [dbo].[TL_PhuCap_MLNS] as tbl
			   INNER JOIN NS_MucLucNganSach as ml on tbl.XauNoiMa = ml.sXauNoiMa AND ml.iNamLamViec = @destinationYear
			   where nam = @sourceYear;
		End;

	if (@isUpdateDmCapBacKh = 1)
		Begin
			DELETE FROM [TL_DM_CapBac_KeHoach] WHERE [iNamLamViec] = @destinationYear;
			INSERT INTO [dbo].[TL_DM_CapBac_KeHoach]
			   ([Ma_Cb]
			   ,[Ten_Cb]
			   ,[Splits]
			   ,[Parent]
			   ,[Readonly]
			   ,[MoTa]
			   ,[LHT_HS]
			   ,[BHXH_CQ]
			   ,[BHXH_CN]
			   ,[BHYT_CQ]
			   ,[BHYT_CN]
			   ,[BHTN_CQ]
			   ,[BHTN_CN]
			   ,[KPCD_CQ]
			   ,[KPCD_CN]
			   ,[Thoi_Han_Tang]
			   ,[Ma_Cb_KeHoach]
			   ,[Ten_Cb_KeHoach]
			   ,[MoTa_KeHoach]
			   ,[Tuoi_Huu_Nam]
			   ,[Tuoi_Huu_Nu]
			   ,[PCRQ_TT]
			   ,[HsLuongKeHoach]
			   ,[IdHslKeHoach]
			   ,[IdHslHienTai]
			   ,[iNamLamViec])
		SELECT 
			[Ma_Cb]
           ,[Ten_Cb]
           ,[Splits]
           ,[Parent]
           ,[Readonly]
           ,[MoTa]
           ,[LHT_HS]
           ,[BHXH_CQ]
           ,[BHXH_CN]
           ,[BHYT_CQ]
           ,[BHYT_CN]
           ,[BHTN_CQ]
           ,[BHTN_CN]
           ,[KPCD_CQ]
           ,[KPCD_CN]
           ,[Thoi_Han_Tang]
           ,[Ma_Cb_KeHoach]
           ,[Ten_Cb_KeHoach]
           ,[MoTa_KeHoach]
           ,[Tuoi_Huu_Nam]
           ,[Tuoi_Huu_Nu]
           ,[PCRQ_TT]
           ,[HsLuongKeHoach]
           ,[IdHslKeHoach]
           ,[IdHslHienTai]
           ,@destinationYear FROM [TL_DM_CapBac_KeHoach] WHERE [iNamLamViec] = @sourceYear
		End;

	if (@isUpdateNSSKT = 1)
		begin
			DELETE FROM NS_MLSKT_MLNS WHERE [iNamLamViec] = @destinationYear;
			INSERT INTO [dbo].[NS_MLSKT_MLNS]
			   ([sSKT_KyHieu]
			   ,[sNS_XauNoiMa]
			   ,[iNamLamViec]
			   ,[dNgayTao]
			   ,[sNguoiTao]
			   ,[dNgaySua]
			   ,[sNguoiSua]
			   ,[Tag]
			   ,[Log]
			   ,[iTrangThai])
			   SELECT [sSKT_KyHieu]
				   ,[sNS_XauNoiMa]
				   ,@destinationYear
				   ,GETDATE()
				   ,@userCreator
				   ,null
				   ,null
				   ,[Tag]
				   ,[Log]
				   ,[iTrangThai] FROM [NS_MLSKT_MLNS] WHERE [iNamLamViec] = @sourceYear;
		end

	if (@isUpdateCauHinhHeThong = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				DELETE FROM DanhMuc WHERE [iNamLamViec] = @destinationYear and sType = 'DM_CauHinh';
				INSERT INTO [dbo].[DanhMuc]
				   ([sType]
				   ,[iID_MaDanhMuc]
				   ,[sTen]
				   ,[sGiaTri]
				   ,[sMoTa]
				   ,[iThuTu]
				   ,[iNamLamViec]
				   ,[iTrangThai]
				   ,[dNgayTao]
				   ,[sNguoiTao]
				   ,[dNgaySua]
				   ,[sNguoiSua]
				   ,[Tag]
				   ,[Log]
				   ,[NganSachNganh])
				   SELECT [sType]
					   ,[iID_MaDanhMuc]
					   ,[sTen]
					   ,[sGiaTri]
					   ,[sMoTa]
					   ,[iThuTu]
					   ,@destinationYear
					   ,[iTrangThai]
					   ,[dNgayTao]
					   ,[sNguoiTao]
					   ,[dNgaySua]
					   ,[sNguoiSua]
					   ,[Tag]
					   ,[Log]
					   ,[NganSachNganh] FROM DanhMuc WHERE [iNamLamViec] = @sourceYear and sType = 'DM_CauHinh';
		end
				
		

END

;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_phuCapChiLuong]    Script Date: 29/11/2022 11:31:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_clone_year_phuCapChiLuong]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
begin
	DELETE FROM [TL_PhuCap_MLNS] WHERE NAM = @dest;
	DECLARE @countMlns int = (SELECT COUNT(*) FROM NS_MucLucNganSach WHERE iNamLamViec = @dest)

	IF(@countMlns = 0)
	BEGIN
		INSERT INTO [NS_MucLucNganSach]
				([iID_MLNS]
				,[iID_MLNS_Cha]
				,[sXauNoiMa]
				,[sLNS]
				,[sL]
				,[sK]
				,[sM]
				,[sTM]
				,[sTTM]
				,[sNG]
				,[sTNG]
				,[sMoTa]
				,[iNamLamViec]
				,[bHangCha]
				,[iTrangThai]
				,[iID_MaBQuanLy]
				,[dNgayTao]
				,[sNguoiTao]
				,[dNgaySua]
				,[sNguoiSua]
				,[Tag]
				,[Log]
				,[iLock]
				,[iLoai]
				,[sTNG1]
				,[sTNG2]
				,[sTNG3]
				,[sChiTietToi]
				,[bNgay]
				,[bSoNguoi]
				,[bTonKho]
				,[bTuChi]
				,[bHangNhap]
				,[bHangMua]
				,[bHienVat]
				,[bDuPhong]
				,[bPhanCap]
				,[sNhapTheoTruong]
				,[iID_MaDonVi]
				,[sCPChiTietToi]
				,[bHangChaDuToan]
				,[bHangChaQuyetToan]
				,[sDuToanChiTietToi]
				,[sQuyetToanChiTietToi])
			 SELECT [iID_MLNS]
				,[iID_MLNS_Cha]
				,[sXauNoiMa]
				,[sLNS]
				,[sL]
				,[sK]
				,[sM]
				,[sTM]
				,[sTTM]
				,[sNG]
				,[sTNG]
				,[sMoTa]
				,@dest
				,[bHangCha]
				,[iTrangThai]
				,[iID_MaBQuanLy]
				,GETDATE()
				,@userCreate
				,null
				,null
				,[Tag]
				,[Log]
				,[iLock]
				,[iLoai]
				,[sTNG1]
				,[sTNG2]
				,[sTNG3]
				,[sChiTietToi]
				,[bNgay]
				,[bSoNguoi]
				,[bTonKho]
				,[bTuChi]
				,[bHangNhap]
				,[bHangMua]
				,[bHienVat]
				,[bDuPhong]
				,[bPhanCap]
				,[sNhapTheoTruong]
				,[iID_MaDonVi]
				,[sCPChiTietToi]
				,[bHangChaDuToan]
				,[bHangChaQuyetToan]
				,[sDuToanChiTietToi]
				,[sQuyetToanChiTietToi]
		  FROM [NS_MucLucNganSach] where iNamLamViec = @source;
		  DELETE FROM [NS_NguoiDung_LNS] WHERE iNamLamViec = @dest;
		  INSERT INTO [NS_NguoiDung_LNS]
			   ([sMaNguoiDung]
			   ,[sLNS]
			   ,[iNamLamViec])
			   (SELECT [sMaNguoiDung]
				  ,[sLNS]
				  ,@dest
				FROM [NS_NguoiDung_LNS] where iNamLamViec = @source)
	END

	INSERT INTO [dbo].[TL_PhuCap_MLNS]
			   ([Ma_PhuCap]
			   ,[Ten_PhuCap]
			   ,[Ma_CachTL]
			   ,[XauNoiMa]
			   ,[LNS]
			   ,[L]
			   ,[K]
			   ,[M]
			   ,[TM]
			   ,[TTM]
			   ,[NG]
			   ,[MoTa]
			   ,[Ma_NguonNganSach]
			   ,[NguonNganSach]
			   ,[DateCreated]
			   ,[UserCreator]
			   ,[DateModified]
			   ,[UserModifier]
			   ,[iTrangThai]
			   ,[idPhuCap]
			   ,[idCachTinhLuong]
			   ,[idNguonNganSach]
			   ,[idMlns]
			   ,[Ma_Cb]
			   ,[ChiTietToi]
			   ,[Nam])
	SELECT  tbl.[Ma_PhuCap]
			   ,tbl.[Ten_PhuCap]
			   ,tbl.[Ma_CachTL]
			   ,tbl.[XauNoiMa]
			   ,tbl.[LNS]
			   ,tbl.[L]
			   ,tbl.[K]
			   ,tbl.[M]
			   ,tbl.[TM]
			   ,tbl.[TTM]
			   ,tbl.[NG]
			   ,tbl.[MoTa]
			   ,tbl.[Ma_NguonNganSach]
			   ,[NguonNganSach]
			   ,GETDATE()
			   ,@userCreate
			   ,null
			   ,null
			   ,tbl.[iTrangThai]
			   ,tbl.[idPhuCap]
			   ,tbl.[idCachTinhLuong]
			   ,tbl.[idNguonNganSach]
			   ,ml.iID
			   ,tbl.[Ma_Cb]
			   ,tbl.[ChiTietToi]
			   ,@dest
	FROM TL_PhuCap_MLNS as tbl
	INNER JOIN NS_MucLucNganSach as ml on tbl.XauNoiMa = ml.sXauNoiMa AND ml.iNamLamViec = @dest
	WHERE tbl.Nam = @source
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_update_bangluongthang_hsqcs]    Script Date: 29/11/2022 11:31:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_update_bangluongthang_hsqcs]
@iThang int,
@inam int,
@iIdMaDonVi t_tbl_string READONLY
AS
BEGIN
	CREATE TABLE #tmpReplace(Ma_PhuCapGoc nvarchar(500), Ma_PhuCapReplace nvarchar(500))
	INSERT INTO #tmpReplace(Ma_PhuCapGoc, Ma_PhuCapReplace) VALUES
	('BHXHDV_TT', 'BHXHDVCS_TT'),
	('BHYTDV_TT', 'BHYTDVCS_TT')

	SELECT tbl.Id, tbl.Ma_CBo, tmp.Ma_PhuCapGoc, Gia_Tri INTO #tblUpdate
	FROM @iIdMaDonVi as dv
	INNER JOIN TL_BangLuong_Thang as tbl on dv.sId = tbl.Ma_DonVi
	INNER JOIN #tmpReplace as tmp on tbl.Ma_PhuCap = tmp.Ma_PhuCapReplace
	WHERE tbl.THANG = @iThang AND tbl.NAM = @inam AND Ma_CB LIKE N'0%'

	UPDATE tbl
	SET
		Gia_Tri = tmp.Gia_Tri
	FROM #tblUpdate as tmp
	INNER JOIN TL_BangLuong_Thang as tbl on tmp.Ma_CBo = tbl.Ma_CBo AND tbl.Ma_PhuCap = tmp.Ma_PhuCapGoc

	--DELETE tbl
	--FROM #tblUpdate as tmp
	--INNER JOIN TL_BangLuong_Thang as tbl on tmp.Id = tbl.Id

	DROP TABLE #tblUpdate
	DROP TABLE #tmpReplace
END
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangke_trichthue_tncn]    Script Date: 29/11/2022 11:03:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 20/12/2021
-- Description:	Lấy dữ liệu nộp thuế thu nhập cá nhân theo tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangke_trichthue_tncn]
	@Thang int,
	@Nam AS int,
	@MaCachTL NVARCHAR(50),
	@MaDonVi NVARCHAR(MAX),
	@IsExportAll bit,
	@IsOrderChucVu bit = 0
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'GTNN,GTPT_SN,GTPT_DG,GTKHAC_TT,LUONGTHUE_TT,THUETNCN_TT,GIAMTHUE_TT,THUEDANOP_TT,LHT_TT,PCCT_TT,THUONG_TT,THUNHAPKHAC_TT,BHCN_TT,GTPT_TT'
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
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTL + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi										AS MaDonVi,
			canBo.Ma_CanBo										AS MaCanBo,
			canBo.Ten_CanBo										AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo)                  AS Ten,
			ISNULL(canBo.Ma_CB, ''0'')							AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'')						AS CapBac,
			ISNULL(chucVu.HeSo_Cv, 0)							AS HSChucVu,
			chucVu.Ma_Cv										AS MaChucVu,
			chucVu.Ten_Cv										AS TenChucVu
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
			AND canBo.Thang = ' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam = ' + CAST(@Nam AS VARCHAR(4)) + '
	)
	SELECT MaDonVi, MaCanBo, TenCanBo, (GTPT_SN*GTPT_DG) GIAM_TRU_PHU_THUOC, 
	(LHT_TT + PCCT_TT + THUONG_TT + ISNULL(THUNHAPKHAC_TT, 0) - GTPT_TT - GIAMTHUE_TT) TINHTHUE,
		' + @Cols + ' FROM (
		SELECT
			canBo.MaDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.Ten,
			canBo.MaCapBac,
			canBo.CapBac,
			canBo.HSChucVu,
			canBo.MaChucVu,
			canBo.TenChucVu,
			bangLuong.GiaTri,
			bangLuong.MaPhuCap
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @Cols + ')
	) pvt
	WHERE (1=1)'

	--WHERE (' + CAST(@IsExportAll AS VARCHAR(1)) + ' = 1 OR THUETNCN_TT > 0)'
	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
	execute(@Query)

	--select @Query
END
;
;
;
;
GO


IF EXISTS (SELECT * FROM TL_DM_PhuCap WHERE Ma_PhuCap=N'PCCV_TIEN')
DELETE FROM TL_DM_PhuCap WHERE Ma_PhuCap=N'PCCV_TIEN'
GO
IF EXISTS (SELECT * FROM TL_DM_PhuCap WHERE Ma_PhuCap=N'LOIICHKHAC_TT')
DELETE FROM TL_DM_PhuCap WHERE Ma_PhuCap=N'LOIICHKHAC_TT'
GO
