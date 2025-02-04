/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_dexuat_index_find_all]    Script Date: 19/06/2023 4:21:48 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_kehoach5nam_dexuat_index_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_kehoach5nam_dexuat_index_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]    Script Date: 19/06/2023 4:21:48 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 19/06/2023 4:21:48 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_data_update_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_data_update_new]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 19/06/2023 4:21:48 PM ******/
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
	@isUpdateDanhMucCanCu int,

	@isUpdateDanhMucChuDauTu int,
	@IsUpdateDanhMucDonviQuanLyDuAn int,
	@isUpdateDanhMucNhaThau int
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
		
			if (@isUpdateDanhMucChuDauTu = 1)
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

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]    Script Date: 19/06/2023 4:21:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]
	@Nganh varchar(max),
	@MaDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@Loai int,
	@dvt int,
	@bTongHop bit
AS
BEGIN
SELECT iID_MLSKT IIdMlskt,
       iID_MLSKTCha IIdMlsktCha,
	   dt_dv.id idDonVi,
	   dt_dv.sTenDonVi,
	   dt_dv.iLoai,
       sKyHieu,
	   sSTT STT,
	   bHangCha,
       sNG,
       sMoTa,
       sNG_Cha sNgCha,
       QuyetToan =SUM(IsNull(A.QuyetToan,0))/@dvt,
       DuToan =SUM(IsNull(A.DuToan,0))/@dvt,
       TuChi =SUM(IsNull(A.TuChi,0))/@dvt,
       PhanCap =SUM(IsNull(A.PhanCap,0))/@dvt,
       MuaHangHienVat =SUM(IsNull(A.MuaHangHienVat,0))/@dvt,
       DacThu =SUM(IsNull(A.PhanCap,0))/@dvt
FROM
  (SELECT ml.iID_MLSKT ,
                ml.iID_MLSKTCha,
                ml.sKyHieu,
                ml.sNG,
                ml.sMoTa,
                ml.sNG_Cha,
				ml.sSTT,
				ml.bHangCha,
                ct.iID_MaDonVi,
				sTenDonVi,
                QuyetToan =0,
                DuToan =0,
                IsNull(ct.fTuChi, 0) TuChi,
                IsNull(ct.fPhanCap, 0) PhanCap,
                IsNull(ct.fMuaHangCapHienVat, 0) MuaHangHienVat,
                IsNull(ct.fPhanCap, 0) DacThu
   FROM NS_SKT_ChungTuChiTiet ct
   LEFT JOIN NS_SKT_ChungTu chungtu ON ct.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra
   RIGHT JOIN NS_SKT_MucLuc ml ON ct.iID_MLSKT = ml.iID_MLSKT
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     --AND ((ct.iLoai = @Loai AND @Loai <> 4) OR (ct.iLoai IN (2, 4) AND @Loai = 4))
     AND ct.iLoaiChungTu = 1
	 AND ((@bTongHop = 0)  or (@bTongHop = 1 AND chungtu.bDaTongHop = @bTongHop))
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai=1
   AND iNamLamViec=@NamLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --WHERE (@Loai = 0 and dt_dv.iLoai != 0) or @Loai <> 0
GROUP BY iID_MLSKT,
         iID_MLSKTCha,
		 dt_dv.id,
	     dt_dv.sTenDonVi,
         sKyHieu,
		 sSTT,
		 bHangCha,
         sNG,
         sMoTa,
         sNG_Cha,
		 iLoai
		 order by sKyHieu
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_dexuat_index_find_all]    Script Date: 19/06/2023 4:21:48 PM ******/
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
