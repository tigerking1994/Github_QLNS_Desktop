/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 31/05/2023 6:40:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_danhmuccancu]    Script Date: 31/05/2023 6:40:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_danhmuccancu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_danhmuccancu]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data]    Script Date: 31/05/2023 6:40:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_data]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_data]
GO
/****** Object:  StoredProcedure [dbo].[sp_bk_tonghop]    Script Date: 31/05/2023 6:40:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bk_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bk_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bk_tonghop]    Script Date: 31/05/2023 6:40:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bk_tonghop]
	@YearOfWork int,
	@QuarterMonth int,
	@LNS nvarchar(max),
	@AgencyId nvarchar(100),
	@DataType int,
	@Dvt int
AS
BEGIN
	SELECT mlns.sLNS,
       mlns.sL,
       mlns.sK,
       mlns.sM,
       mlns.sTM,
       mlns.sTTM,
       mlns.sNG,
       mlns.sTNG,
       mlns.sXauNoiMa,
       mlns.iID_MLNS,
       mlns.iID_MLNS_Cha,
       mlns.sMoTa,
       NoiDung,
       sSoChungTu,
       dNgayChungTu,
       sSoQuyetDinh,
	   sLoai,
       sTenDonVi,
       fTongTuChi / @Dvt AS TuChi,
       fTongHienVat / @Dvt AS HienVat INTO #tblBkTongHop
	FROM
	  (SELECT sLNS,
			  sL,
			  sK,
			  sM,
			  sTM,
			  sTTM,
			  sNG,
			  sTNG,
			  sXauNoiMa,
			  iID_MLNS,
			  iID_MLNS_Cha,
			  ctct.sMoTa AS NoiDung,
			  iID_BKChungTu,
			  sSoChungTu,
			  sLoai,
			  dNgayChungTu,
			  dv.iID_MaDonVi,
			  dv.sTenDonVi,
			  fTongTuChi,
			  fTongHienVat
	   FROM NS_BK_ChungTuChiTiet ctct
	   LEFT JOIN DonVi dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi AND dv.iNamLamViec = ctct.iNamLamViec
	   WHERE iTrangThai=1
		 AND iThangQuy=@QuarterMonth
		 AND (@LNS IS NULL
			  OR sLNS in
				(SELECT *
				 FROM f_split(@LNS)))
		 AND (@AgencyId IS NULL
			  OR ctct.iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))
		 AND ctct.iNamLamViec = @YearOfWork 
		 AND (@DataType IS NULL
			  OR (@DataType=1
				  AND fTongTuChi<>0)
			  OR (@DataType=2
				  AND fTongHienVat<>0)) ) AS ctct -- lay so ghi so

	LEFT JOIN
	  (SELECT iID_BKChungTu,
			  sSoQuyetDinh,
			  dNgayQuyetDinh
	   FROM NS_BK_ChungTu
	   WHERE iNamLamViec=@YearOfWork) AS ct ON ct.iID_BKChungTu = ctct.iID_BKChungTu
	LEFT JOIN
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
		 AND iTrangThai = 1) AS mlns ON mlns.iID_MLNS = ctct.iID_MLNS;

	WITH LNSTreeParent AS
	  (SELECT sLNS,
			  sL,
			  sK,
			  sM,
			  sTM,
			  sTTM,
			  sNG,
			  sTNG,
			  sXauNoiMa,
			  iID_MLNS,
			  iID_MLNS_Cha,
			  sMoTa ,
			  cast(NoiDung AS nvarchar(MAX)) AS NoiDung ,
			  cast(sSoChungTu AS nvarchar(500)) AS SoChungTu ,
			  CONVERT(NVARCHAR(100), dNgayChungTu, 103) AS NgayChungTu ,
			  cast(sSoQuyetDinh AS nvarchar(500)) AS SoQuyetDinh ,
			  cast(sTenDonVi AS nvarchar(500)) AS TenDonVi ,
			  cast(sLoai AS nvarchar(500)) AS SLoai ,
			  cast(TuChi AS float) AS TuChi ,
			  cast(HienVat AS float) AS HienVat ,
			  cast(0 AS bit) AS IsHangCha
	   FROM #tblBkTongHop
	   UNION ALL SELECT mlnsParent.sLNS,
						mlnsParent.sL,
						mlnsParent.sK,
						mlnsParent.sM,
						mlnsParent.sTM,
						mlnsParent.sTTM,
						mlnsParent.sNG,
						mlnsParent.sTNG,
						mlnsParent.sXauNoiMa,
						mlnsParent.iID_MLNS,
						mlnsParent.iID_MLNS_Cha,
						mlnsParent.sMoTa ,
						cast('' AS nvarchar(MAX)) AS NoiDung ,
						cast('' AS nvarchar(500)) AS SoChungTu ,
						cast('' AS nvarchar(100)) AS NgayChungTu ,
						cast('' AS nvarchar(500)) AS SoQuyetDinh ,
						cast('' AS nvarchar(500)) AS TenDonVi ,
						cast('' AS nvarchar(500)) AS SLoai ,
						cast(0 AS float) AS TuChi ,
						cast(0 AS float) AS HienVat ,
						cast(1 AS bit) AS IsHangCha
	   FROM NS_MucLucNganSach mlnsParent
	   INNER JOIN LNSTreeParent ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
	   WHERE mlnsParent.iNamLamViec = @YearOfWork )
	SELECT DISTINCT *
	FROM LNSTreeParent
	ORDER BY sLNS,
			 sL,
			 sK,
			 sM,
			 sTM,
			 sTTM,
			 sNG;


	DROP TABLE #tblBkTongHop;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data]    Script Date: 31/05/2023 6:40:56 PM ******/
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
	@isUpdateCauHinhHeThong int,
	@isUpdateDanhMucDonViTinh int,
	@isUpdateDanhMucCanCu int
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
	if (@isUpdateDanhMucDonViTinh = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				DELETE FROM DanhMuc WHERE [iNamLamViec] = @destinationYear and sType = 'DM_DonViTinh';
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
					   ,[NganSachNganh] FROM DanhMuc WHERE [iNamLamViec] = @sourceYear and sType = 'DM_DonViTinh';
		end		
		
		if (@isUpdateDanhMucCanCu = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				DELETE FROM [dbo].[NS_CauHinh_CanCu] WHERE [iNamLamViec] = @destinationYear;
				INSERT INTO [dbo].[NS_CauHinh_CanCu]
				   ([iID_CauHinh_CanCu]
					,[bChinhSua]
					,[iID_MaChucNang]
					,[iNamCanCu]
					,[iNamLamViec]
					,[iThietLap]
					,[sModule]
					,[sTenCot])
				   SELECT NEWID()
					,[bChinhSua]
					,[iID_MaChucNang]
					,[iNamCanCu]
					,@destinationYear
					,[iThietLap]
					,[sModule]
					,[sTenCot] FROM [dbo].[NS_CauHinh_CanCu] WHERE [iNamLamViec] = @sourceYear;
		end			

END

;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_danhmuccancu]    Script Date: 31/05/2023 6:40:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_clone_year_danhmuccancu]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
begin

	  INSERT INTO [dbo].[NS_CauHinh_CanCu]
				   ([iID_CauHinh_CanCu]
					,[bChinhSua]
					,[iID_MaChucNang]
					,[iNamCanCu]
					,[iNamLamViec]
					,[iThietLap]
					,[sModule]
					,[sTenCot])
				   SELECT NEWID()
					,[bChinhSua]
					,[iID_MaChucNang]
					,[iNamCanCu]
					,@dest
					,[iThietLap]
					,[sModule]
					,[sTenCot] FROM [dbo].[NS_CauHinh_CanCu] b WHERE [iNamLamViec] = @source AND (b.iID_MaChucNang NOT IN (SELECT c.iID_MaChucNang FROM [NS_CauHinh_CanCu] c WHERE c.iNamLamViec = @dest));


	 --UPDATE
	update d
	set
		d.bChinhSua = 0
	from
	[NS_CauHinh_CanCu] as s
	inner join [NS_CauHinh_CanCu] as d
	on s.iID_MaChucNang = d.iID_MaChucNang
	where s.iNamLamViec = @source and d.iNamLamViec = @dest

end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 31/05/2023 6:40:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_chungtu_chitiet]
	-- Add the parameters for the stored procedure here
	@VoucherId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type nvarchar(10),
	@AgencyId nvarchar(100),
	@VoucherDate datetime,
	@UserName nvarchar(100),
	@QuarterMonth nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	Declare @YearOfBudgetCurrent int = 2,
		@YearOfBudgetAfter int = 4;
	Declare @voucherIndex int,
	@IsLocked bit,
	@STongHop nvarchar(500);
	SELECT @voucherIndex = iSoChungTuIndex, @IsLocked = bKhoa, @STongHop = sTongHop FROM NS_QT_ChungTu WHERE iID_QTChungTu = @VoucherId;

	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;


	SELECT 
		isnull(ctct.iID_QTCTChiTiet, NEWID()) AS iID_QTCTChiTiet,
		ctct.iID_QTChungTu,
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sTNG1,
		mlns.sTNG2,
		mlns.sTNG3,
		mlns.sMoTa,
		mlns.bHangCha,
		isnull(ctct.iNamNganSach, @YearOfBudget) as iNamNganSach,
		isnull(ctct.iID_MaNguonNganSach, @BudgetSource) as iID_MaNguonNganSach,
		isnull(ctct.iNamLamViec, @YearOfWork) as iNamLamViec,
		isnull(ctct.iThangQuyLoai, 0) as iThangQuyLoai,
		isnull(ctct.iThangQuy, 0) as iThangQuy,
		isnull(ctct.iID_MaDonVi, dtctct.iID_MaDonVi) as iID_MaDonVi,
		--ctct.iID_MaDonVi,
		dv.sTenDonVi,
		isnull(ctct.fSoNguoi, 0) as fSoNguoi,
		isnull(ctct.fSoNgay, 0) as fSoNgay,
		isNull(ctct.fSoLuot, 0) as fSoLuot,
		isnull(ctct.fTuChi_DeNghi, 0) as fTuChi_DeNghi,
		isnull(ctct.fTuChi_PheDuyet, 0) as fTuChi_PheDuyet,
		ctct.sGhiChu,
		ctct.dNgayTao,
		ctct.dNgaySua,
		ctct.sNguoiTao,
		ctct.sNguoiSua,
		dtctct.DuToan as fDuToan,
		ctctdqt.DaQuyetToan as fDaQuyetToan,
		mlns.sChiTietToi,
		mlns.bHangChaQuyetToan,
		mlns.sMaCB
	FROM 
	(
		select * from NS_MucLucNganSach where iNamLamViec = @YearOfWork and iTrangThai = 1 and bHangChaQuyetToan is not null and
		(
			(@CountDonViCha > 0 and (@IsLocked = 1 or @STongHop is not null) and sLNS in (select * from f_split(@LNS)))
			or
			(
				sLNS in 
				(
					SELECT 
						DISTINCT VALUE
					FROM 
					(
						SELECT 
							CAST(LEFT(sLNS, 1) AS nvarchar(10)) sLNS1, 
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) sLNS3, 
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) sLNS5, 
							CAST(sLNS AS nvarchar(10)) sLNS 
						FROM
							NS_NguoiDung_LNS 
						WHERE 
							sMaNguoiDung = @UserName 
							AND iNamLamViec = @YearOfWork
							AND sLNS IN (SELECT * FROM f_split(@LNS))
					) sLNS
					UNPIVOT
					(
					  value
					  FOR col in (sLNS1, sLNS3, sLNS5, sLNS)
					) un
				)
			)
		)
	) mlns
	LEFT JOIN
		(SELECT 
			* 
		 FROM 
			NS_QT_ChungTuChiTiet 
		 WHERE 
			iID_QTChungTu = @VoucherId
			AND iNamLamViec = @YearOfWork
			AND (@YearOfBudget = @YearOfBudgetCurrent and (iNamNganSach = @YearOfBudgetCurrent or iNamNganSach = @YearOfBudgetAfter) or (iNamNganSach = @YearOfBudget))
			AND iID_MaNguonNganSach = @BudgetSource
		) ctct
	ON mlns.sXauNoiMa = ctct.sXauNoiMa 
	LEFT JOIN 
		(select 
			(case sLNS1
				when '1040200' then (SUM(fHangNhap) + SUM(fTuChi))
				when '1040300' then (SUM(fHangMua) + SUM(fTuChi))
				else SUM(fTuChi)
			end) as DuToan,
			iID_MLNS1 as iID_MLNS,
			@AgencyId as iID_MaDonVi,
			sXauNoiMa
			from 
			(
				select 
					case 
						when ctct.sLNS = '1040100' then mlns.sLNS
						else ctct.sLNS
						end 
					as sLNS1,
					case 
						when ctct.sLNS = '1040100' then mlns.iID_MLNS
						else ctct.iID_MLNS
						end
					as iID_MLNS1,
					ctct.*
				from 
				(
					SELECT
					*
					FROM 
						NS_DT_ChungTuChiTiet
					 WHERE
						iID_DTChungTu 
						IN (SELECT iID_DTChungTu FROM NS_DT_ChungTu 
							where 
								((ISNULL(@STongHop, '') = '' AND sDSID_MaDonVi like '%' + @AgencyId + '%') OR (ISNULL(@STongHop, '') <> '' AND sDSID_MaDonVi = @AgencyId))
								AND (iLoai = 1 or iLoai = 0)
								AND iNamLamViec = @YearOfWork
								AND iNamNganSach = @YearOfBudget
								AND iID_MaNguonNganSach = @BudgetSource
								AND bKhoa = 1
								AND cast(dNgayQuyetDinh as date) <= cast(@VoucherDate as date)
								AND sSoQuyetDinh is not null AND sSoQuyetDinh <> ''
						)
						and iID_MaDonVi = @AgencyId
						and IDuLieuNhan = 0) ctct
					left join 
						(select '1040100' as sLNS1, * from NS_MucLucNganSach where sLNS in ('1040100', '1040200', '1040300') and iNamLamViec = @YearOfWork) mlns
					on mlns.sLNS1 = ctct.sLNS and mlns.sL = ctct.sL and mlns.sK = ctct.sK and mlns.sM = ctct.sM and mlns.sTM = ctct.sTM and mlns.sNG = ctct.sNG and mlns.sTTM = ctct.sTTM
				) dt
				GROUP BY iID_MLNS1, sLNS1, sXauNoiMa
			) dtctct
	on mlns.sXauNoiMa = dtctct.sXauNoiMa
	LEFT JOIN 
		(SELECT
			SUM(fTuChi_PheDuyet) AS DaQuyetToan,
			iID_MLNS,
			sXauNoiMa
		 FROM 
			NS_QT_ChungTuChiTiet
		 WHERE
			iID_QTChungTu 
			IN (SELECT iID_QTChungTu FROM NS_QT_ChungTu 
				where 
					iID_MaDonVi = @AgencyId
					AND sLoai = @Type
					AND iNamLamViec = @YearOfWork
					AND (@YearOfBudget = @YearOfBudgetCurrent and (iNamNganSach = @YearOfBudgetCurrent or iNamNganSach = @YearOfBudgetAfter) or (iNamNganSach = @YearOfBudget))
					AND iID_MaNguonNganSach = @BudgetSource
					AND iThangQuy <= @QuarterMonth
					--AND cast(dNgayChungTu as date) <= cast(@voucherDate as Date)
					AND iID_QTChungTu <> @VoucherId
			)
		 GROUP BY iID_MLNS, sXauNoiMa
		) ctctdqt
	ON mlns.sXauNoiMa = ctctdqt.sXauNoiMa
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi OR dv.iID_MaDonVi = dtctct.iID_MaDonVi
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
	Order by mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG, mlns.sTNG1, mlns.sTNG2, mlns.sTNG3
END
;
;
;
;
;
;
GO
