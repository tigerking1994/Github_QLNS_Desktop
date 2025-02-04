/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_index_find_all]    Script Date: 05/07/2023 7:03:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_kehoach5nam_index_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_kehoach5nam_index_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_dexuat_index_find_all]    Script Date: 05/07/2023 7:03:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_kehoach5nam_dexuat_index_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_kehoach5nam_dexuat_index_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_chitiet]    Script Date: 05/07/2023 7:03:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_kehoach5nam_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_kehoach5nam_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_dutoan_nguonvon_by_duan]    Script Date: 05/07/2023 7:03:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_all_dutoan_nguonvon_by_duan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_all_dutoan_nguonvon_by_duan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_chutruongdautu_index_update]    Script Date: 05/07/2023 7:03:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_chutruongdautu_index_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_chutruongdautu_index_update]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_baocaotinhhinhduan]    Script Date: 05/07/2023 7:03:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_baocaotinhhinhduan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_baocaotinhhinhduan]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhyt]    Script Date: 05/07/2023 7:03:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh]    Script Date: 05/07/2023 7:03:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 05/07/2023 7:03:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_thuongxuyen]    Script Date: 05/07/2023 7:03:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_thuongxuyen]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_thuongxuyen]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 05/07/2023 7:03:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]    Script Date: 05/07/2023 7:03:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 05/07/2023 7:03:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_data_update_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_data_update_new]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_tong_hop_dieuchinh]    Script Date: 05/07/2023 7:03:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_du_toan_chi_tieu_tong_hop_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_du_toan_chi_tieu_tong_hop_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_tong_hop_dieuchinh]    Script Date: 05/07/2023 7:03:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rpt_du_toan_chi_tieu_tong_hop_dieuchinh]
	@ChungTuId nvarchar(4000),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(100)
AS
BEGIN
	SELECT isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       ctct.iID_DTChungTu,
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
       ctct.iNamNganSach,
       ctct.iID_MaNguonNganSach,
       ctct.iNamLamViec,
       isnull(ctct.iPhanCap, 0) AS iPhanCap,
       ctct.iID_MaDonVi,
       isnull(ctct.sGhiChu, '') AS sGhiChu,
       isnull(ctct.fHangMua, 0) AS fHangMua,
       isnull(ctct.fHangNhap, 0) AS fHangNhap,
       isnull(ctct.fDuPhong, 0) AS fDuPhong,
       isnull(ctct.fPhanCap, 0) AS fPhanCap,
       isnull(ctct.fTuChi, 0) AS fTuChi,
       isnull(ctct.fHienVat, 0) AS fHienVat,
       ctct.dNgayTao,
       ctct.sNguoiTao,
       ctct.dNgaySua,
       ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   mlns.sChiTietToi,
	   dv.sTenDonVi,
	   mlns.bHangChaDuToan
	FROM 
		(select * from NS_MucLucNganSach 
		where iNamLamViec = @YearOfWork and iTrangThai = 1 and bHangChaDuToan is not null and 
		sLNS in 
				(
					SELECT 
						DISTINCT VALUE
					FROM 
					(
						SELECT 
							CAST(LEFT(Item, 1) AS nvarchar(10)) sLNS1, 
							CAST(LEFT(Item, 3) AS nvarchar(10)) sLNS3, 
							CAST(LEFT(Item, 5) AS nvarchar(10)) sLNS5, 
							CAST(Item AS nvarchar(10)) sLNS 
						FROM f_split(@LNS)
					) sLNS
					UNPIVOT
					(
					  value
					  FOR col in (sLNS1, sLNS3, sLNS5, sLNS)
					) un
				)
	) mlns
	LEFT JOIN
	(
		(SELECT
			*
		FROM NS_DT_ChungTuChiTiet
		WHERE
			iID_DTChungTu in (SELECT * FROM dbo.f_split(@ChungTuId))
			AND iDuLieuNhan = 0) 	
	) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
	
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 05/07/2023 7:03:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_clone_data_update_new] 
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

	--@isUpdateDanhMucChuDauTu int,
	--@IsUpdateDanhMucDonviQuanLyDuAn int,
	--@isUpdateDanhMucNhaThau int
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
		
/*			if (@isUpdateDanhMucChuDauTu = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				DELETE FROM [dbo].[DM_ChuDauTu] WHERE [iNamLamViec] = @destinationYear;
				INSERT INTO [dbo].[DM_ChuDauTu]
				   ([iID_DonVi]
					,[bHangCha]
					,[ChiNhanhNuocNgoai]
					,[dNgaySua]
					,[dNgayTao]
					,[iID_DonViCha]
					,[iID_MaDonVi]
					,[iNamLamViec]
					,[iTrangThai]
					,[Loai]
					,[MaSoDVSDNS]
					,[sKyHieu]
					,[sMoTa]
					,[sNguoiSua]
					,[sNguoiTao]
					,[STKNuocNgoai]
					,[STKTrongNuoc]
					,[sTenDonVi])
				   SELECT NEWID()
					,[bHangCha]
					,[ChiNhanhNuocNgoai]
					,[dNgaySua]
					,[dNgayTao]
					,[iID_DonViCha]
					,[iID_MaDonVi]
					,@destinationYear
					,[iTrangThai]
					,[Loai]
					,[MaSoDVSDNS]
					,[sKyHieu]
					,[sMoTa]
					,[sNguoiSua]
					,[sNguoiTao]
					,[STKNuocNgoai]
					,[STKTrongNuoc]
					,[sTenDonVi] FROM [dbo].[DM_ChuDauTu] WHERE [iNamLamViec] = @sourceYear;
		end	

			if (@isUpdateDanhMucDonviQuanLyDuAn = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG   VdtDmDonViThucHienDuAn
				DELETE FROM [dbo].[VDT_DM_DonViThucHienDuAn];
				INSERT INTO [dbo].[VDT_DM_DonViThucHienDuAn]
				   ([iID_DonVi]
					,[BHangCha]
					,[iCapDonVi]
					,[iID_DonViCha]
					,[iID_MaDonVi]
					,[sDiaChi]
					,[sKyHieu]
					,[sTenDonVi])
				   SELECT NEWID()
					,[BHangCha]
					,[iCapDonVi]
					,[iID_DonViCha]
					,[iID_MaDonVi]
					,[sDiaChi]
					,[sKyHieu]
					,[sTenDonVi] FROM [dbo].[VDT_DM_DonViThucHienDuAn];
		end	

			if (@isUpdateDanhMucNhaThau = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG  VdtDmNhaThau
				DELETE FROM [dbo].[VDT_DM_NhaThau] ;
				INSERT INTO [dbo].[VDT_DM_NhaThau]
				   ([Id]
					,[sChucVu]
					,[sDaiDien]
					,[sDiaChi]
					,[sDienThoai]
					,[sDienThoaiLienHe]
					,[sEmail]
					,[sFax]
					,[sMaNganHang]
					,[sMaNhaThau]
					,[sMaSoThue]
					,[sNganHang]
					,[sNguoiLienHe]
					,[sSoTaiKhoan]
					,[sTenNhaThau]
					,[sWebsite]
					,[sSoTaiKhoan2]
					,[sSoTaiKhoan3])
				   SELECT NEWID()
					,[sChucVu]
					,[sDaiDien]
					,[sDiaChi]
					,[sDienThoai]
					,[sDienThoaiLienHe]
					,[sEmail]
					,[sFax]
					,[sMaNganHang]
					,[sMaNhaThau]
					,[sMaSoThue]
					,[sNganHang]
					,[sNguoiLienHe]
					,[sSoTaiKhoan]
					,[sTenNhaThau]
					,[sWebsite]
					,[sSoTaiKhoan2]
					,[sSoTaiKhoan3] FROM [dbo].[VDT_DM_NhaThau];
		end	
		*/

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]    Script Date: 05/07/2023 7:03:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop] 
	@YearOfWork int,
	@daTongHop bit
AS
BEGIN
	SELECT
	KHT.iID_KHT_BHXH,
	KHT.sSoChungTu,
	KHT.iNamChungTu,
	KHT.dNgayChungTu,
	KHT.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHT.sMoTa,
	KHT.bIsKhoa,
	KHT.fTongKeHoach,
	KHT.sSoQuyetDinh,
	KHT.dNgayQuyetDinh,
	KHT.sNguoiTao,
	KHT.sNguoiSua,
	KHT.dNgayTao,
	KHT.dNgaySua,
	KHT.iLoaiTongHop,
	KHT.sTongHop,
	KHT.fThuBHXHNLDDong,
	KHT.fThuBHXHNSDDong,
	KHT.fThuBHXH,
	KHT.fThuBHYTNLDDong,
	KHT.fThuBHYTNSDDong,
	KHT.fTongBHYT,
	KHT.fThuBHTNNLDDong,
	KHT.fThuBHTNNSDDong,
	KHT.fThuBHTN,
	KHT.fTong,
	KHT.bDaTongHop
	FROM BH_KHT_BHXH KHT
	LEFT JOIN DonVi DV
	ON KHT.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	AND KHT.iNamChungTu = @YearOfWork
	AND KHT.bDaTongHop = @daTongHop
	ORDER BY KHT.dNgayQuyetDinh DESC
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 05/07/2023 7:03:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@NamLamViec int
AS
BEGIN
	INSERT INTO BH_KHT_BHXH_ChiTiet (iID_KHT_BHXH, iID_MucLucNganSach, iQSBQNam, fLuongChinh , fPhuCapChucVu, fPCTNNghe, fPCTNVuotKhung, fNghiOm , fThuBHXHNguoiLaoDongDong
	, fThuBHXHNguoiSuDungLaoDongDong , fThuBHYTNguoiLaoDongDong , fThuBHYTNguoiSuDungLaoDongDong , fThuBHTNNguoiLaoDongDong , fThuBHTNNguoiSuDungLaoDongDong,fTongThuBHXH, fTongThuBHYT, fTongThuBHTN, fTongCong
	, dNgayTao, dNgaySua, sNguoiTao)
SELECT @idChungTu,
       iID_MucLucNganSach,
	   iQSBQNam,
       sum(fLuongChinh) ,
       sum(fPhuCapChucVu) ,
	   sum(fPCTNNghe) ,
	   sum(fPCTNVuotKhung) ,
	   sum(fNghiOm),
       sum(fThuBHXHNguoiLaoDongDong) ,
	   sum(fThuBHXHNguoiSuDungLaoDongDong) ,
	   sum(fThuBHYTNguoiLaoDongDong) ,
	   sum(fThuBHYTNguoiSuDungLaoDongDong) ,
	   sum(fThuBHTNNguoiLaoDongDong) ,
	   sum(fThuBHTNNguoiSuDungLaoDongDong) ,
	   sum(fTongThuBHXH) ,
	   Sum(fTongThuBHYT) ,
	   Sum(fTongThuBHTN) ,
	   Sum(fTongCong) ,
       NULL ,
       NULL ,
       NULL 
FROM BH_KHT_BHXH_ChiTiet
WHERE iID_KHT_BHXH in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY iID_MucLucNganSach,
		iQSBQNam;

--danh dau chung tu da tong hop
update BH_KHT_BHXH set bDaTongHop = 1 
where iID_KHT_BHXH in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop));

END
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_thuongxuyen]    Script Date: 05/07/2023 7:03:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_thuongxuyen]
	@month1 int,
	@month2 int,
	@month3 int,
	@month4 int,
	@yearOfWork int,
	@agencyId nvarchar(max)
AS
BEGIN
	SELECT 
		rSQ = SUM(fSoSQ),
		rQNCN = SUM(fSoQNCN) ,
		rCNVHD = SUM(fSoCNVHD),
		rHSQCS = SUM(fSoHSQCS),

		rSQ1 = SUM(CASE WHEN ((iThangQuy =@month1 AND iNamLamViec=@yearOfWork))  THEN fSoSQ ELSE 0 END),
		rSQ2 = SUM(CASE WHEN ((iThangQuy =@month2 AND iNamLamViec=@yearOfWork))   THEN fSoSQ ELSE 0 END),
		rSQ3 = SUM(CASE WHEN ((iThangQuy =@month3 AND iNamLamViec=@yearOfWork))   THEN fSoSQ ELSE 0 END),
		rSQ4 = SUM(CASE WHEN ((iThangQuy =@month4 AND iNamLamViec=@yearOfWork))  THEN fSoSQ ELSE 0 END),
				
		rQNCN1 = SUM(CASE WHEN ((iThangQuy =@month1 AND iNamLamViec=@yearOfWork))   THEN fSoQNCN ELSE 0 END),
		rQNCN2 = SUM(CASE WHEN ((iThangQuy =@month2 AND iNamLamViec=@yearOfWork))   THEN fSoQNCN ELSE 0 END),
		rQNCN3 = SUM(CASE WHEN ((iThangQuy =@month3 AND iNamLamViec=@yearOfWork))   THEN fSoQNCN ELSE 0 END),
		rQNCN4 = SUM(CASE WHEN ((iThangQuy =@month4 AND iNamLamViec=@yearOfWork))  THEN fSoQNCN ELSE 0 END),
				
		rCNVHD1 = SUM(CASE WHEN ((iThangQuy =@month1 AND iNamLamViec=@yearOfWork))   THEN fSoCNVHD ELSE 0 END),
		rCNVHD2 = SUM(CASE WHEN ((iThangQuy =@month2 AND iNamLamViec=@yearOfWork))   THEN fSoCNVHD ELSE 0 END),
		rCNVHD3 = SUM(CASE WHEN ((iThangQuy =@month3 AND iNamLamViec=@yearOfWork))   THEN fSoCNVHD ELSE 0 END),
		rCNVHD4 = SUM(CASE WHEN ((iThangQuy =@month4 AND iNamLamViec=@yearOfWork))  THEN fSoCNVHD ELSE 0 END),
				
		rHSQCS1 = SUM(CASE WHEN ((iThangQuy =@month1 AND iNamLamViec=@yearOfWork))   THEN fSoHSQCS ELSE 0 END),
		rHSQCS2 = SUM(CASE WHEN ((iThangQuy =@month2 AND iNamLamViec=@yearOfWork))   THEN fSoHSQCS ELSE 0 END),
		rHSQCS3 = SUM(CASE WHEN ((iThangQuy =@month3 AND iNamLamViec=@yearOfWork))   THEN fSoHSQCS ELSE 0 END),
		rHSQCS4 = SUM(CASE WHEN ((iThangQuy =@month4 AND iNamLamViec=@yearOfWork))  THEN fSoHSQCS ELSE 0 END),
		dv.iID_MaDonVi as Id_DonVi,
		dv.sTenDonVi as TenDonVi,
		dv.MoTa,
		iNamLamViec,
		iThangQuy
	FROM (
		SELECT
			fSoSQ = SUM(CASE WHEN sKyHieu = '700' THEN fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa+ fSoTrungTa + fSoThuongTa + fSoDaiTa+ fSoTuong ELSE 0 END),
			fSoQNCN = SUM(CASE WHEN sKyHieu = '700' THEN fSoQNCN ELSE 0 END),
			fSoCNVHD = SUM(CASE WHEN sKyHieu = '700' THEN fSoCNVQP + fSoLDHD ELSE 0 END),
			fSoHSQCS = SUM(CASE WHEN sKyHieu = '700' THEN fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi ELSE 0 END),
			iID_MaDonVi,
			iThangQuy,
			iNamLamViec
		from  
			NS_QS_ChungTuChiTiet 
		WHERE 
			iNamLamViec = @yearOfWork
			and (@agencyId is null or iID_MaDonVi in (select * from f_split(@agencyId)))
		group by  
			iID_MaDonVi, iThangQuy, iNamLamViec 
	) as qs
	RIGHT JOIN 
		(
			SELECT iID_MaDonVi, sTenDonVi, MoTa = iID_MaDonVi + ' - ' + sTenDonVi from DonVi 
			WHERE iTrangThai=1 and iNamLamViec = @yearOfWork and iLoai=1 and (@AgencyId is null or iID_MaDonVi in (select * from f_split(@AgencyId)))) as dv
	on dv.iID_MaDonVi = qs.iID_MaDonVi
	
	group by dv.iID_MaDonVi, dv.sTenDonVi, dv.MoTa, iNamLamViec, iThangQuy
	ORDER BY dv.iID_MaDonVi		
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 05/07/2023 7:03:20 PM ******/
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
		 GROUP BY sXauNoiMa
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh]    Script Date: 05/07/2023 7:03:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhxh] 
	@namLamViec int,
	@loaiChungTu int,
	@daTongHop bit,
	@lstSelectedUnit ntext,
	@khoiDuToan nvarchar(50),
	@khoiHachToan nvarchar(50)
AS
BEGIN
declare @DataDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldDongDuToan float, BhxhNsddDongDuToan float, BHXHTongCongDuToan float, BhtnNldDongDuToan float, BhtnNsddDongDuToan float, BHTNTongCongDuToan float);
declare @DataHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldDongHachToan float, BhxhNsddDongHachToan float, BHXHTongCongHachToan float, BhtnNldDongHachToan float, BhtnNsddDongHachToan float, BHTNTongCongHachToan float);

INSERT INTO @DataDuToan (sTenDonVI, idDonVi, BhxhNldDongDuToan, BhxhNsddDongDuToan, BHXHTongCongDuToan, BhtnNldDongDuToan, BhtnNsddDongDuToan, BHTNTongCongDuToan)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
       BhxhNldDong = SUM(IsNull(A.ThuBHXHNLDDong,0)),
       BhxhNsddDong = SUM(IsNull(A.ThuBHXHNSDDong,0)),
	   SUM(IsNull(A.ThuBHXHNLDDong,0)) + SUM(IsNull(A.ThuBHXHNSDDong,0)),
	   BhtnNldDong = SUM(IsNull(A.ThuBHTNNLDDong,0)),
       BhtnNsddDong = SUM(IsNull(A.ThuBHTNNSDDong,0)),
	   SUM(IsNull(A.ThuBHTNNLDDong,0)) + SUM(IsNull(A.ThuBHTNNSDDong,0))

FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sNG,
                ml.sMoTa,
				ml.bHangCha,
				ml.sXauNoiMa,
                ct.iID_MaDonVi,
                IsNull(ctct.fThuBHXHNguoiLaoDongDong, 0) ThuBHXHNLDDong,
                IsNull(ctct.fThuBHXHNguoiSuDungLaoDongDong, 0) ThuBHXHNSDDong,
				IsNull(ctct.fThuBHTNNguoiLaoDongDong, 0) ThuBHTNNLDDong,
                IsNull(ctct.fThuBHTNNguoiSuDungLaoDongDong, 0) ThuBHTNNSDDong
   FROM BH_KHT_BHXH_ChiTiet ctct
   LEFT JOIN BH_KHT_BHXH ct ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @khoiDuToan
   WHERE ct.iNamChungTu = @namLamViec
	 AND ct.bDaTongHop = @daTongHop
	 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
   JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec
   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id;

INSERT INTO @DataHachToan (sTenDonVI, idDonVi, BhxhNldDongHachToan, BhxhNsddDongHachToan, BHXHTongCongHachToan, BhtnNldDongHachToan, BhtnNsddDongHachToan, BHTNTongCongHachToan)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
       BhxhNldDong = SUM(IsNull(A.ThuBHXHNLDDong,0)),
       BhxhNsddDong = SUM(IsNull(A.ThuBHXHNSDDong,0)),
	   SUM(IsNull(A.ThuBHXHNLDDong,0)) + SUM(IsNull(A.ThuBHXHNSDDong,0)),
	   BhtnNldDong = SUM(IsNull(A.ThuBHTNNLDDong,0)),
       BhtnNsddDong = SUM(IsNull(A.ThuBHTNNSDDong,0)),
	   SUM(IsNull(A.ThuBHTNNLDDong,0)) + SUM(IsNull(A.ThuBHTNNSDDong,0))

FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sNG,
                ml.sMoTa,
				ml.bHangCha,
				ml.sXauNoiMa,
                ct.iID_MaDonVi,
                IsNull(ctct.fThuBHXHNguoiLaoDongDong, 0) ThuBHXHNLDDong,
                IsNull(ctct.fThuBHXHNguoiSuDungLaoDongDong, 0) ThuBHXHNSDDong,
				IsNull(ctct.fThuBHTNNguoiLaoDongDong, 0) ThuBHTNNLDDong,
                IsNull(ctct.fThuBHTNNguoiSuDungLaoDongDong, 0) ThuBHTNNSDDong
   FROM BH_KHT_BHXH_ChiTiet ctct
   LEFT JOIN BH_KHT_BHXH ct ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @khoiHachToan
   WHERE ct.iNamChungTu = @namLamViec
	 AND ct.bDaTongHop = @daTongHop
	 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
   JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec
   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
IsNull(dt.BhxhNldDongDuToan, 0) BhxhNldDongDuToan, 
IsNull(dt.BhxhNsddDongDuToan, 0) BhxhNsddDongDuToan, 
IsNull(ht.BhxhNldDongHachToan, 0) BhxhNldDongHachToan, 
IsNull(ht.BhxhNsddDongHachToan, 0) BhxhNsddDongHachToan,
IsNull(dt.BHXHTongCongDuToan, 0) BHXHTongCongDuToan,
IsNull(ht.BHXHTongCongHachToan, 0) BHXHTongCongHachToan,
IsNull(dt.BhtnNldDongDuToan, 0) BhtnNldDongDuToan, 
IsNull(dt.BhtnNsddDongDuToan, 0) BhtnNsddDongDuToan,
IsNull(ht.BhtnNldDongHachToan, 0) BhtnNldDongHachToan, 
IsNull(ht.BhtnNsddDongHachToan, 0) BhtnNsddDongHachToan,
IsNull(dt.BHTNTongCongDuToan, 0) BHTNTongCongDuToan,
IsNull(ht.BHTNTongCongHachToan, 0) BHTNTongCongHachToan
FROM @DataDuToan dt
LEFT JOIN @DataHachToan ht ON dt.idDonVi = ht.idDonVi;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhyt]    Script Date: 05/07/2023 7:03:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhyt] 
	@namLamViec int,
	@loaiChungTu int,
	@daTongHop bit,
	@lstSelectedUnit ntext,
	@khoiDuToan nvarchar(50),
	@khoiHachToan nvarchar(50),
	@sm nvarchar(50)
AS
BEGIN
declare @BhytDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sm nvarchar(50),BhytNSDDongDuToan float, BhytNLDDongDuToan float, tongBhytDuToan float);
declare @BhytHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sm nvarchar(50), BhytNSDDongHachToan float,BhytNLDDongHachToan float, tongBhytHachToan float);

INSERT INTO @BhytDuToan (sTenDonVI, idDonVi, sm, BhytNSDDongDuToan, BhytNLDDongDuToan, tongBhytDuToan)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sm,
	   SUM(IsNull(A.ThuBHYTNSDDongDuToan, 0)) BhytNSDDongDuToan,
	   SUM(IsNull(A.ThuBHYTNLDDongDuToan, 0)) BhytNLDDongDuToan,
	   SUM(IsNull(A.TongBhytDuToan, 0)) TongBhytDuToan

FROM
  (SELECT ml.sm,
           ml.sMoTa,
           ct.iID_MaDonVi,
		   IsNull(ctct.fThuBHYTNguoiSuDungLaoDongDong, 0) ThuBHYTNSDDongDuToan,
		   IsNull(ctct.fThuBHYTNguoiLaoDongDong, 0) ThuBHYTNLDDongDuToan,
		   IsNull(ctct.fTongThuBHYT, 0) TongBhytDuToan
   FROM BH_KHT_BHXH_ChiTiet ctct
   LEFT JOIN BH_KHT_BHXH ct ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @khoiDuToan
   AND ml.sM = @sm
   WHERE ct.iNamChungTu = @namLamViec
	 AND ct.bDaTongHop = @daTongHop
	 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
   JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec
   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		A.sm;

INSERT INTO @BhytHachToan (idDonVi, sTenDonVI, sm, BhytNSDDongHachToan, BhytNLDDongHachToan, tongBhytHachToan)
	SELECT
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sm,
	   SUM(IsNull(A.ThuBHYTNSDDongHachToan, 0)) BhytNSDDongHachToan,
	   SUM(IsNull(A.ThuBHYTNLDDongHachToan, 0)) BhytNLDDongHachToan,
	   SUM(IsNull(A.TongBhytHachToan, 0)) TongBhytHachToan

FROM
  (SELECT ml.sm,
           ml.sMoTa,
           ct.iID_MaDonVi,
		   IsNull(ctct.fThuBHYTNguoiSuDungLaoDongDong, 0) ThuBHYTNSDDongHachToan,
		   IsNull(ctct.fThuBHYTNguoiLaoDongDong, 0) ThuBHYTNLDDongHachToan,
		   IsNull(ctct.fTongThuBHYT, 0) TongBhytHachToan
   FROM BH_KHT_BHXH_ChiTiet ctct
   LEFT JOIN BH_KHT_BHXH ct ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @khoiHachToan
   AND ml.sM = @sm
   WHERE ct.iNamChungTu = @namLamViec
	 AND ct.bDaTongHop = @daTongHop
	 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
   JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec
   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		A.sm;

SELECT dt.idDonVi, 
dt.sTenDonVI,

IsNull(dt.BhytNLDDongDuToan, 0) BhytNldDongDuToan, 
IsNull(dt.BhytNSDDongDuToan, 0) BhytNsddDongDuToan, 
IsNull(ht.BhytNLDDongHachToan, 0) BhytNldDongHachToan, 
IsNull(ht.BhytNSDDongHachToan, 0) BhytNsddDongHachToan,
IsNull(dt.tongBhytDuToan, 0) BHYTTongCongDuToan,
IsNull(ht.tongBhytHachToan, 0) BHYTTongCongHachToan

FROM @BhytDuToan dt
LEFT JOIN @BhytHachToan ht ON dt.idDonVi = ht.idDonVi;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_baocaotinhhinhduan]    Script Date: 05/07/2023 7:03:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_baocaotinhhinhduan]
@IdDuAn nvarchar(500),
@NgayDeNghi datetime
AS
BEGIN      
	SELECT  tbl.iID_HopDongId ,
		tbl.iID_NhaThauId,
		SUM(CASE WHEN iLoaiThanhToan = 1 THEN (ISNULL(dt.fGiaTriThanhToanTN, 0) + ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE 0 END) as SoThanhToan,
		SUM(CASE WHEN iLoaiThanhToan = 0 THEN (ISNULL(dt.fGiaTriThanhToanTN, 0) + ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE 0 END) as SoTamUng,
		SUM(ISNULL(dt.fGiaTriThuHoiNamNayNN, 0)+ ISNULL(dt.fGiaTriThuHoiNamNayTN, 0) + ISNULL(dt.fGiaTriThuHoiNamTruocNN, 0) + ISNULL(fGiaTriThuHoiNamTruocTN, 0) 
			+ ISNULL(dt.fGiaTriThuHoiUngTruocNamNayNN, 0) + ISNULL(dt.fGiaTriThuHoiUngTruocNamNayTN, 0) + ISNULL(fGiaTriThuHoiUngTruocNamTruocNN, 0) 
			+ ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocTN, 0)) as SoThuHoiTamUng INTO #tmp
	from VDT_TT_DeNghiThanhToan tbl
	inner join VDT_TT_PheDuyetThanhToan_ChiTiet dt on tbl.Id = dt.iID_DeNghiThanhToanID
	and tbl.iID_DuAnId = @IdDuAn
	and CAST(tbl.dNgayPheDuyet as DATE) <= CAST(@NgayDeNghi as DATE)
	group by  tbl.iID_HopDongId, tbl.iID_NhaThauId
	
	SELECT 
		CAST(0 as bit) as IsHangCha,
		NULL as NguonVonId,
		NULL as Loai,
		NULL as Mlns,
		nt.sTenNhaThau as TenNhaThau,
		hd.sSoHopDong as SoHopDong,
		hd.iThoiGianThucHien as ThoiGianThucHien,
		hd.fTienHopDong as TienHopDong,
		NULL as SoDeNghi,
		NULL as IdDeNghiThanhToan,
		NULL as NgayThanhToan,
		(ISNULL(tmp.SoThanhToan, 0) + ISNULL(tmp.SoTamUng, 0) - ISNULL(tmp.SoThuHoiTamUng, 0)) as TongCongGiaiNgan,
		NULL as NgayCapUng,
		NULL as SoDaCapUng,
		tmp.*
	FROM #tmp as tmp
	LEFT JOIN VDT_DM_NhaThau as nt on tmp.iID_NhaThauId = nt.Id
	LEFT JOIN VDT_DA_TT_HopDong as hd on tmp.iID_HopDongId = hd.Id
	DROP TABLE #tmp
END


GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_chutruongdautu_index_update]    Script Date: 05/07/2023 7:03:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_chutruongdautu_index_update] 
	@UserName nvarchar(100)
	
AS
BEGIN
	SELECT DISTINCT iID_MaDonVi INTO #tmpDonVi 
	FROM NguoiDung_DonVi 
	WHERE iID_MaNguoiDung = @UserName AND iTrangThai = 1;


	WITH SoLieuDieuChinh AS 
	 (
		SELECT 
			ct.iID_ChuTruongDauTuID, ct.iID_ParentId
		FROM 
			VDT_DA_ChuTruongDauTu ct 
		WHERE 
			ct.iID_ParentId is not null

		UNION ALL

		SELECT 
			ct.iID_ChuTruongDauTuID, ct.iID_ParentId
		FROM 
			VDT_DA_ChuTruongDauTu ct JOIN SoLieuDieuChinh ctpr ON ct.iID_ParentId = ctpr.iID_ChuTruongDauTuID
		WHERE ct.iID_ParentId is not null
	  ),SoLanDieuChinh AS (
		   SELECT
			sdc.iID_ChuTruongDauTuID,sdc.iID_ParentId,  COUNT(sdc.iID_ChuTruongDauTuID) AS iSoLanDieuChinh
		  FROM 
			SoLieuDieuChinh sdc
		  GROUP BY sdc.iID_ParentId,sdc.iID_ChuTruongDauTuID
	  )

	SELECT
		ctdt.iID_ChuTruongDauTuID AS Id,
		ctdt.sSoQuyetDinh AS SSoQuyetDinh,
		ctdt.dNgayQuyetDinh AS DNgayQuyetDinh,
		ctdt.sSoToTrinh AS SSoToTrinh,
		ctdt.dNgayToTrinh AS DNgayToTrinh,
		ctdt.sSoThamDinh AS SSoThamDinh,
		ctdt.dNgayThamDinh AS DNgayThamDinh,
		ctdt.sCoQuanThamDinh AS SCoQuanThamDinh,
		ctdt.iID_DuAnID AS IIdDuAnId,
		ctdt.fTMDTDuKienPheDuyet AS FTmdtduKienPheDuyet,
		ctdt.sLoaiDieuChinh AS SLoaiDieuChinh,
		ctdt.sKhoiCong AS SKhoiCong,
		ctdt.sHoanThanh AS SHoanThanh,
		(da.sMaDuAn +'-'+ da.sTenDuAn ) AS STenDuAn,
		ctdt.sDiaDiem AS SDiaDiem,
		ctdt.sNguonGocSuDungDat AS SNguonGocSuDungDat,
		ctdt.sDienTichSuDungDat AS SDienTichSuDungDat,
		ctdt.iID_ChuDauTuID AS IIdChuDauTuId,
		ctdt.sSuCanThietDauTu AS SSuCanThietDauTu,
		ctdt.sMucTieu AS SMucTieu,
		ctdt.sQuyMo AS SQuyMo,
		ctdt.iID_DonViThucHienID AS IIdDonViThucHienId,
		ctdt.iID_LoaiDuAn AS IIdLoaiDuAn,
		ctdt.iID_HinhThucDauTuID AS IIdHinhThucDauTuId,
		ctdt.iID_HinhThucQuanLyID AS IIdHinhThucQuanLyId,
		ctdt.iID_NhomDuAnID AS IIdNhomDuAnId,
		da.iID_DonViThucHienDuAnID AS IIdDonViQuanLyId,
		ctdt.iID_CapPheDuyetID AS IIdCapPheDuyetId,
		ctdt.iID_LoaiCongTrinhID AS IIdLoaiCongTrinhId,
		ctdt.iID_NhomQuanLyID AS IIdNhomQuanLyId,
		ctdt.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
		ctdt.iID_MaChuDauTuID AS IIdMaChuDauTuId,
		ctdt.bIsGoc AS BIsGoc,
		ctdt.iID_ParentID AS IIdParentId,
		ctdt.bActive AS BActive,
		ctdt.sMota as SMoTa,
		isnull(tbl.iSoLanDieuChinh,0) AS ILanDieuChinh ,
		dv.sTenDonVi AS TenDonVi,
		dv.iID_MaDonVi AS IdDonvi,
		cdt.sTenDonVi AS TenChuDauTu,
		cpd.sTen AS TenCapPheDuyet,
		ctdt.sUserCreate as SUserCreate,
		ctdt.bKhoa as BKhoa,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 203 AND ObjectId = ctdt.iID_ChuTruongDauTuID) AS TotalFiles
	FROM
		VDT_DA_ChuTruongDauTu ctdt
		INNER JOIN #tmpDonVi as dvCheck on ctdt.iID_MaDonViQuanLy = dvCheck.iID_MaDonVi
		LEFT JOIN VDT_DA_DuAn da ON da.iID_DuAnID = ctdt.iID_DuAnID
		--LEFT JOIN VDT_DM_DonViThucHienDuAn dv ON da.iID_DonViThucHienDuAnID = dv.iID_DonVi 
		LEFT JOIN (SELECT DISTINCT iID_MaDonVi, sTenDonVi FROM DONVI WHERE DONVI.iLoai in ('0','1'))dv on ctdt.iID_MaDonViQuanLy = dv.iID_MaDonVi 
			LEFT JOIN DM_ChuDauTu cdt ON cdt.iID_DonVi = ctdt.iID_ChuDauTuID

		LEFT JOIN VDT_DM_PhanCapDuAn cpd ON cpd.iID_PhanCapID = ctdt.iID_CapPheDuyetID
		left join SoLanDieuChinh tbl ON tbl.iID_ChuTruongDauTuID = ctdt.iID_ChuTruongDauTuID

		--where dv.iLoai in ('0','1') -- để đồng bộ với điều kiện tìm kiếm khi thêm mới

	ORDER BY ctdt.dDateCreate DESC

	DROP TABLE #tmpDonVi
END;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_dutoan_nguonvon_by_duan]    Script Date: 05/07/2023 7:03:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_get_all_dutoan_nguonvon_by_duan] 
	@duAnId nvarchar(200)
AS
BEGIN

select tb1.TenNguonVon,tb1.IdDuToanNguonVon,tb1.IdNguonVon,tb1.IdDuToan,tb1.GiaTriPheDuyet  as GiaTriPheDuyet ,
		null as FGiaTriDieuChinh,
		null as GiaTriTruocDieuChinh,
		tb1.FTienPheDuyetQDDT
		from
		(
			select ns.sTen as TenNguonVon,
				null as IdDuToanNguonVon,
				qdnv.iID_NguonVonID as IdNguonVon,
				null as IdDuToan,
				CAST(0 as float) as GiaTriPheDuyet,
				qdnv.fTienPheDuyet as FTienPheDuyetQDDT
			from VDT_DA_QDDauTu_NguonVon qdnv
				inner join NguonNganSach ns ON ns.iID_MaNguonNganSach = qdnv.iID_NguonVonID
				inner join VDT_DA_QDDauTu qd ON qd.iID_QDDauTuID = qdnv.iID_QDDauTuID AND qd.bActive = 1
			where qd.iID_DuAnID =  @duAnId
		)tb1
END;
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan]    Script Date: 26/11/2021 10:19:45 PM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_chitiet]    Script Date: 05/07/2023 7:03:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_kehoach5nam_chitiet]
	@IdKh5nam nvarchar(100)
AS
BEGIN
	SELECT
		ctct.iID_KeHoach5Nam_ChiTietID	AS Id,
		ctct.iID_KeHoach5NamID			AS IIdKeHoach5NamId,
		ctct.iID_KeHoach5Nam_ChiTietID	AS IIdKeHoach5NamChiTietId,
		da.iID_DuAnID					AS IIdDuAnId,
		da.sMaDuAn						AS SMaDuAn,
		ctct.sTen						AS STenDuAn,
		ct.iGiaiDoanTu					AS IGiaiDoanTu,
		ct.iGiaiDoanDen					AS IGiaiDoanDen,
		da.sDiaDiem						AS SDiaDiem,
		ctct.sTrangThai					AS STrangThai,
		ctct.iID_NguonVonID				AS IIdNguonVonId,
		ns.sTen							AS STenNguonVon,
		ctct.iID_LoaiCongTrinhID		AS IIdLoaiCongTrinhId,
		lct.sTenLoaiCongTrinh			AS STenLoaiCongTrinh,
		dv.iID_DonVi					AS IIdDonViId,
		dv.iID_MaDonVi					AS IIdMaDonVi,
		dv.sTenDonVi					AS STenDonVi,
		ctct.sGhiChu					AS SGhiChu,
		CASE
			WHEN ctctParent.iID_ParentID IS NOT NULL THEN CAST(da.sKhoiCong AS NVARCHAR) + '-' + CAST(da.bIsKetThuc AS NVARCHAR)
			ELSE CAST(ct.iGiaiDoanTu AS NVARCHAR) + '-' + CAST(ct.iGiaiDoanDen AS NVARCHAR)
		END	AS ThoiGianThucHien,
		ctct.fHanMucDauTu				AS FHanMucDauTu,
		ctct.fVonDaGiao					AS FVonDaGiao,
		ctct.fVonBoTriTuNamDenNam		AS FVonBoTriTuNamDenNam,
		ctct.fGiaTriBoTri				AS FGiaTriSau5Nam,
		ctctParent.fVonDaGiao			AS FVonDaGiaoOrigin,
		ctctParent.fVonBoTriTuNamDenNam	AS FVonBoTriTuNamDenNamOrigin,
		ctctParent.fGiaTriBoTri			AS FGiaTriSau5NamOrigin,
		NULL							AS FVonBoTriNamTruoc,
		NULL							AS FVonDaBoTriNamNay,
		ctct.iID_ParentID				AS IIdParentId,
		ctct.bActive					AS BActive,
		ctct.sStt                       AS STT

	FROM VDT_KHV_KeHoach5Nam_ChiTiet ctct
	LEFT JOIN VDT_KHV_KeHoach5Nam_ChiTiet ctctParent
		ON ctctParent.iID_KeHoach5Nam_ChiTietID = ctct.iID_ParentID
	INNER JOIN VDT_KHV_KeHoach5Nam ct
		ON ctct.iID_KeHoach5NamID = ct.iID_KeHoach5NamID
	LEFT JOIN VDT_DA_DuAn da
		ON ctct.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN NguonNganSach ns
		ON ctct.iID_NguonVonID = ns.iID_MaNguonNganSach
	LEFT JOIN VDT_DM_LoaiCongTrinh lct
		ON ctct.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh
	LEFT JOIN VDT_DM_DonViThucHienDuAn dv
		ON ctct.iID_DonViQuanLyID = dv.iID_DonVi
	WHERE
		ct.iID_KeHoach5NamID = @IdKh5nam
    ORDER BY STT
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_dexuat_index_find_all]    Script Date: 05/07/2023 7:03:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_vdt_kehoach5nam_dexuat_index_find_all] 

AS
BEGIN
	--So lan dieu chinh
	WITH SoLieuDieuChinh AS 
	 (
		SELECT 
			ct.Id, ct.iID_ParentId, ct.sSoQuyetDinh, ct.NamLamViec
		FROM 
			VDT_KHV_KeHoach5Nam_DeXuat ct 
		WHERE 
			ct.iID_ParentId is not null

		UNION ALL

		SELECT ct.Id, ct.iID_ParentId, ct.sSoQuyetDinh, ct.NamLamViec
		FROM VDT_KHV_KeHoach5Nam_DeXuat ct JOIN SoLieuDieuChinh ctpr ON ct.iID_ParentId = ctpr.Id 
		WHERE ct.iID_ParentId is not null
	  ),SoLanDieuChinh AS (
		   SELECT 
			sdc.iID_ParentId, sdc.NamLamViec, COUNT(sdc.Id) AS iSoLanDieuChinh
		  FROM 
			SoLieuDieuChinh sdc
		  GROUP BY sdc.iID_ParentId, sdc.NamLamViec
	  )

	SELECT
	distinct
		khv.Id AS Id,
		khv.iID_DonViQuanLyID AS IIdDonViId,
		khv.iID_MaDonViQuanLy AS IIdMaDonVi,
		khv.iID_ParentId AS IIdParentId,
		khv.bActive AS BActive,
		khv.bIsGoc AS BIsGoc,
		khv.fGiaTriKeHoach AS FGiaTriKeHoach,
		khv.sTrangThai AS STrangThai,
		khv.dDateCreate AS DDateCreate,
		khv.sUserCreate AS SUserCreate,
		khv.dDateUpdate AS DDateUpdate,
		khv.sUserUpdate AS SUserUpdate,
		khv.dDateDelete AS DDateDelete,
		khv.sUserDelete AS SUserDelete,
		khv.sSoQuyetDinh AS SoKeHoach,
		khv.dNgayQuyetDinh AS NgayLap,
		khv.iGiaiDoanDen AS GiaiDoanDen,
		khv.iGiaiDoanTu AS GiaiDoanTu,
		khv.ILoai AS ILoai,
		khv.NamLamViec AS NamLamViec,
		khv.iID_TongHopParent as IIdTongHop,
		dv.sTenDonVi AS STenDonVi,
		('(' + cast(isnull(dc.iSoLanDieuChinh, 0) AS nvarchar) + ')') AS SoLanDC, 
		khv.MoTaChiTiet,
		CASE
			WHEN khv.iID_ParentId is null THEN ''
			ELSE
				(SELECT TOP 1 khvpr.sSoQuyetDinh FROM VDT_KHV_KeHoach5Nam_DeXuat khvpr WHERE khvpr.Id = khv.iID_ParentId)
		END DieuChinhTu,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 201 AND ObjectId = khv.Id) AS TotalFiles,
		khv.bKhoa as BKhoa,
		khv.sTongHop as STongHop 
		FROM VDT_KHV_KeHoach5Nam_DeXuat khv
		LEFT JOIN DonVi dv
			ON khv.iID_MaDonViQuanLy = dv.iID_MaDonVi and dv.iNamLamViec = khv.NamLamViec
		LEFT JOIN SoLanDieuChinh AS dc
			ON khv.iID_ParentId = dc.iID_ParentId and khv.NamLamViec = dc.NamLamViec
		
		ORDER BY khv.dDateCreate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_index_find_all]    Script Date: 05/07/2023 7:03:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_kehoach5nam_index_find_all]
	
AS
BEGIN
	--So lan dieu chinh
	WITH SoLieuDieuChinh AS 
	 (
		SELECT 
			ct.iID_KeHoach5NamID, ct.iID_ParentId, ct.sSoQuyetDinh, ct.NamLamViec
		FROM 
			VDT_KHV_KeHoach5Nam ct 
		WHERE 
			ct.iID_ParentId is not null

		UNION ALL

		SELECT 
			ct.iID_KeHoach5NamID, ct.iID_ParentId, ct.sSoQuyetDinh, ct.NamLamViec
		FROM 
			VDT_KHV_KeHoach5Nam ct JOIN SoLieuDieuChinh ctpr ON ct.iID_ParentId = ctpr.iID_KeHoach5NamID
		WHERE ct.iID_ParentId is not null
	  ),SoLanDieuChinh AS (
		   SELECT 
			sdc.iID_ParentId, sdc.NamLamViec, COUNT(sdc.iID_KeHoach5NamID) AS iSoLanDieuChinh
		  FROM 
			SoLieuDieuChinh sdc
		  GROUP BY sdc.iID_ParentId, sdc.NamLamViec
	  )

	select
	distinct
		khv.iID_KeHoach5NamID as IIdKeHoach5NamId,
		khv.iID_DonViQuanLyID as IIdDonViId,
		khv.iID_MaDonViQuanLy as IIdMaDonVi,
		khv.iID_ParentId as IIdParentId,
		khv.bActive as BActive,
		khv.bIsGoc as BIsGoc,
		khv.bKhoa as BKhoa,
		khv.fGiaTriDuocDuyet as FGiaTriKeHoach,
		khv.sTrangThai as STrangThai,
		khv.dDateCreate as DDateCreate,
		khv.sUserCreate as SUserCreate,
		khv.dDateUpdate as DDateUpdate,
		khv.sUserUpdate as SUserUpdate,
		khv.dDateDelete as DDateDelete,
		khv.sUserDelete as SUserDelete,
		khv.sSoQuyetDinh as SoKeHoach,
		khv.dNgayQuyetDinh as NgayLap,
		khv.iGiaiDoanDen as GiaiDoanDen,
		khv.iGiaiDoanTu as GiaiDoanTu,
		khv.ILoai as ILoai,
		khv.NamLamViec as NamLamViec,
		dv.sTenDonVi as STenDonVi,
		'(' + cast(isnull(dc.iSoLanDieuChinh, 0) as nvarchar) + ')' as SoLanDC,
		khv.MoTaChiTiet,
		khv.iID_KhthDeXuat as IIDKhthDeXuat,
		(khvdx.sSoQuyetDinh + ' - ' + cast(khvdx.iGiaiDoanTu as nvarchar) + '-' + cast(khvdx.iGiaiDoanDen as nvarchar)) as SKeHoachDeXuat,
		(case
			when khv.iID_ParentId is null then ''
			else
				(select khvpr.sSoQuyetDinh from VDT_KHV_KeHoach5Nam khvpr where khvpr.iID_KeHoach5NamID = khv.iID_ParentId)
		end) AS DieuChinhTu,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 202 AND ObjectId = khv.iID_KeHoach5NamID) AS TotalFiles
		FROM VDT_KHV_KeHoach5Nam khv
		LEFT JOIN DonVi dv ON khv.iID_MaDonViQuanLy = dv.iID_MaDonVi
		LEFT JOIN SoLanDieuChinh AS dc
			ON khv.iID_ParentId = dc.iID_ParentId and khv.NamLamViec = dc.NamLamViec
		LEFT JOIN VDT_KHV_KeHoach5Nam_DeXuat khvdx on khv.iID_KhthDeXuat = khvdx.Id
		ORDER BY khv.dDateCreate DESC
END
;
;
GO
