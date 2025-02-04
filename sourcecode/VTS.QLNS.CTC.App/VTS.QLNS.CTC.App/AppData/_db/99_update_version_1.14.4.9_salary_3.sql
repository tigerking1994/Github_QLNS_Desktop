/****** Object:  StoredProcedure [dbo].[sp_clone_year_danh_muc_tdqt]    Script Date: 5/23/2024 2:00:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_danh_muc_tdqt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_danh_muc_tdqt]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 5/23/2024 2:00:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_data_update_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_data_update_new]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 5/23/2024 2:00:45 PM ******/
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
	@isUpdateDanhMucCKTC int,
	@isUpdateDanhMucBHXH int,
	@isUpdateMucLucCacLoaiChi int,
	@isUpdateDanhMucCoSoYTe int,
	@isUpdateDanhMucTDQT int,
	@isUpdateDanhMucCHTSBHXH int

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
				,[sQuyetToanChiTietToi]
				,[sMaCB])
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
				,[sMaCB]
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
			   ,[SKyHieuCu]
			   ,[sL]
			   ,[sK]
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
			   ,[SKyHieuCu]
			   ,[sL]
			   ,[sK]
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

		if (@isUpdateDanhMucCKTC = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM [dbo].[NS_DanhMucCongKhai] WHERE [iNamLamViec] = @destinationYear;
				INSERT INTO [dbo].[NS_DanhMucCongKhai]
					  ([Id]
					  ,[dNgayTao]
					  ,[iNamLamViec]
					  ,[sMoTa]
					  ,[sNguoiSua]
					  ,[sNguoiTao]
					  ,[STT]
					  ,[bHangCha]
					  ,[iID_DMCongKhai_Cha]
					  ,[sMa]
					  ,[sMaCha])
				   SELECT NEWID()
					  ,GETDATE()
					  ,@destinationYear
					  ,[sMoTa]
					  ,[sNguoiSua]
					  ,[sNguoiTao]
					  ,[STT]
					  ,[bHangCha]
					  ,[iID_DMCongKhai_Cha]
					  ,[sMa]
					  ,[sMaCha] FROM [dbo].[NS_DanhMucCongKhai] WHERE [iNamLamViec] = @sourceYear;

				update con
				set con.iID_DMCongKhai_Cha = cha.Id 
				from NS_DanhMucCongKhai con
				join NS_DanhMucCongKhai cha on con.sMaCha = cha.sMa 
				and con.iNamLamViec = cha.iNamLamViec
				where con.iNamLamViec = @destinationYear

				DELETE FROM [dbo].[NS_DMCongKhai_MLNS] WHERE [iNamLamViec] = @destinationYear;
				INSERT INTO [dbo].[NS_DMCongKhai_MLNS]
					  ([Id]
					  ,[dNgaySua]
					  ,[dNgayTao]
					  ,[iID_DMCongKhai]
					  ,[iNamLamViec]
					  ,[sNS_XauNoiMa]
					  ,[sNguoiSua]
					  ,[sNguoiTao])
				   SELECT NEWID()
					,GETDATE()
					,GETDATE()
					,[iID_DMCongKhai_NEW]
					,@destinationYear
					,[sNS_XauNoiMa]
					,[sNguoiSua]
					,[sNguoiTao] 
				   FROM (
						select map.*, b.Id [iID_DMCongKhai_NEW] from NS_DMCongKhai_MLNS map
						join NS_DanhMucCongKhai a on map.iID_DMCongKhai = a.Id 
						and map.iNamLamViec = a.iNamLamViec
						join (select * from NS_DanhMucCongKhai where iNamLamViec = @destinationYear) b
						on a.sMa = b.sMa
						where map.iNamLamViec = @sourceYear
					) tab
				WHERE tab.[iNamLamViec] = @sourceYear;
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

		if (@isUpdateDanhMucBHXH = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM BH_DM_MucLucNganSach WHERE iNamLamViec = @destinationYear;
				INSERT INTO BH_DM_MucLucNganSach (
					iID,
					sXauNoiMa,
					sLNS,
					sL,
					sK,
					sM,
					sTM,
					sTTM,
					sNG,
					sTNG,
					sMoTa,
					bHangCha,
					iTrangThai,
					bDuPhong,
					bHangChaDuToan,
					bHangChaQuyetToan,
					bHangMua,
					bHangNhap,
					bHienVat,
					bNgay,
					bPhanCap,
					bSoNguoi,
					bTonKho,
					bTuChi,
					sChiTietToi,
					dNgaySua,
					dNgayTao,
					iLoai,
					iLock,
					iID_MaDonVi,
					iID_MaBQuanLy,
					[Log],
					iID_MLNS,
					iID_MLNS_Cha,
					iNamLamViec,
					sCPChiTietToi,
					sDuToanChiTietToi,
					sNguoiSua,
					sNguoiTao,
					sNhapTheoTruong,
					sQuyetToanChiTietToi,
					Tag,
					sTNG1,
					sTNG2,
					sTNG3,
					iLoaiNganSach,
					sMaCB,
					sMaPhuCap,
					bHangChaDuToanDieuChinh,
					sDuToanDieuChinhChiTietToi,
					iDonViTinh,
					fTyLe_BHXH_NSD,
					fTyLe_BHXH_NLD,
					fTyLe_BHYT_NSD,
					fTyLe_BHYT_NLD,
					fTyLe_BHTN_NSD,
					fTyLe_BHTN_NLD)
				select newid(),
					sXauNoiMa,
					sLNS,
					sL,
					sK,
					sM,
					sTM,
					sTTM,
					sNG,
					sTNG,
					sMoTa,
					bHangCha,
					iTrangThai,
					bDuPhong,
					bHangChaDuToan,
					bHangChaQuyetToan,
					bHangMua,
					bHangNhap,
					bHienVat,
					bNgay,
					bPhanCap,
					bSoNguoi,
					bTonKho,
					bTuChi,
					sChiTietToi,
					dNgaySua,
					getdate(),
					iLoai,
					iLock,
					iID_MaDonVi,
					iID_MaBQuanLy,
					[Log],
					iID_MLNS,
					iID_MLNS_Cha,
					@destinationYear,
					sCPChiTietToi,
					sDuToanChiTietToi,
					sNguoiSua,
					@userCreator,
					sNhapTheoTruong,
					sQuyetToanChiTietToi,
					Tag,
					sTNG1,
					sTNG2,
					sTNG3,
					iLoaiNganSach,
					sMaCB,
					sMaPhuCap,
					bHangChaDuToanDieuChinh,
					sDuToanDieuChinhChiTietToi,
					iDonViTinh,
					fTyLe_BHXH_NSD,
					fTyLe_BHXH_NLD,
					fTyLe_BHYT_NSD,
					fTyLe_BHYT_NLD,
					fTyLe_BHTN_NSD,
					fTyLe_BHTN_NLD
				from BH_DM_MucLucNganSach
				where iNamLamViec = @sourceYear;
		end	

		if (@isUpdateMucLucCacLoaiChi = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM BH_DM_LoaiChi WHERE iNamLamViec = @destinationYear;
				INSERT INTO BH_DM_LoaiChi (
					iID,
					sMaLoaiChi,
					sTenDanhMucLoaiChi,
					iNamLamViec,
					dNgaySua,
					dNgayTao,
					sNguoiSua,
					sNguoiTao,
					sMoTa,
					iTrangThai,
					sLNS,
					sDSXauNoiMa)
				select NEWID(),
					sMaLoaiChi,
					sTenDanhMucLoaiChi,
					@destinationYear,
					dNgaySua,
					getdate(),
					sNguoiSua,
					@userCreator,
					sMoTa,
					iTrangThai,
					sLNS,
					sDSXauNoiMa
				from BH_DM_LoaiChi
				where iNamLamViec = @sourceYear
		end	

		if (@isUpdateDanhMucCoSoYTe = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM DM_CoSoYTe WHERE iNamLamViec = @destinationYear;
				INSERT INTO DM_CoSoYTe (
					iID_CoSoYTe,
					iID_MaCoSoYTe,
					iNamLamViec,
					sTenCoSoYTe,
					dNgaySua,
					dNgayTao,
					iTrangThai,
					sNguoiSua,
					sNguoiTao)
				select NEWID(),
					iID_MaCoSoYTe,
					@destinationYear,
					sTenCoSoYTe,
					dNgaySua,
					GETDATE(),
					iTrangThai,
					sNguoiSua,
					@userCreator
				from DM_CoSoYTe
				where iNamLamViec = @sourceYear
		end	

		if (@isUpdateDanhMucTDQT = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM BH_DM_ThamDinhQuyetToan WHERE iNamLamViec = @destinationYear;
				INSERT INTO BH_DM_ThamDinhQuyetToan (
					iID,
					iKieuChu,
					iMa,
					iMaCha,
					iNamLamViec,
					iTrangThai,
					sNguoiSua,
					sNguoiTao,
					sNoiDung,
					sSTT,
					iSTT,
					sXauNoiMa)
				select NEWID(),
					iKieuChu,
					iMa,
					iMaCha,
					@destinationYear,
					iTrangThai,
					sNguoiSua,
					@userCreator,
					sNoiDung,
					sSTT,
					iSTT,
					sXauNoiMa
				from BH_DM_ThamDinhQuyetToan
				where iNamLamViec = @sourceYear
		end	

		if (@isUpdateDanhMucCHTSBHXH = 1)
		begin
			-- COPY DANH MUC CAU HINH HE THONG
				
				DELETE FROM BH_DM_CauHinhThamSo WHERE iNamLamViec = @destinationYear;
				INSERT INTO BH_DM_CauHinhThamSo (
					iID,
					bTrangThai,
					dNgaySua,
					dNgayTao,
					iNamLamViec,
					sMa,
					sMoTa,
					sNguoiSua,
					sNguoiTao,
					sTen,
					fGiaTri)
				select NEWID(),
					bTrangThai,
					dNgaySua,
					GETDATE(),
					@destinationYear,
					sMa,
					sMoTa,
					sNguoiSua,
					@userCreator,
					sTen,
					fGiaTri
				from BH_DM_CauHinhThamSo
				where iNamLamViec = @sourceYear
		end	

END
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_danh_muc_tdqt]    Script Date: 5/23/2024 2:00:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_clone_year_danh_muc_tdqt]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
AS
BEGIN
	
	insert into BH_DM_ThamDinhQuyetToan (
		iID,
		iKieuChu,
		iMa,
		iMaCha,
		iNamLamViec,
		iTrangThai,
		sNguoiSua,
		sNguoiTao,
		sNoiDung,
		sSTT,
		iSTT,
		sXauNoiMa)
	select NEWID(),
		iKieuChu,
		iMa,
		iMaCha,
		@dest,
		iTrangThai,
		sNguoiSua,
		@userCreate,
		sNoiDung,
		sSTT,
		iSTT,
		sXauNoiMa
	from BH_DM_ThamDinhQuyetToan
	where iNamLamViec = @source and iMa not in (select iMa from BH_DM_ThamDinhQuyetToan where iNamLamViec = @dest)

END
;
GO

DELETE FROM [dbo].[TL_DM_Cach_TinhLuong_TruyThu]
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'abea1f70-40bf-4ab2-8c5d-0a9092433222', N'PCGS_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCGS_TT', N'', N'', NULL, N'Phụ cấp giáo sư', NULL, N'Phụ cấp giáo sư', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'e9d3f9c8-1592-458e-82e7-0de7b86b00a8', N'BHYTDVCS_HS*LCS', N'CACH1', N'BHYTDVCS_TT', NULL, NULL, 2022, NULL, NULL, N'Bảo hiểm y tế đơn vị đóng cho chiến sĩ (thành tiền)', 11)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'473a9aab-00f1-413b-b21b-10e3a1d09aa6', N'PCTRA35_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCTRA35_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp trách nhiệm 35%', 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'11b3c7ed-a779-4f9e-b32c-12c663d06ccf', N'(LHT_TT+PCCV_TT+PCTNVK_TT+HSBL_TT)*PCTHANHTRA_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCTHANHTRA_TT', N'', N'', NULL, N'', NULL, N'Phụ cấp thanh, kiểm tra', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'a3a6c7a8-40d9-46e8-8d5d-187793e58c8b', N'PCBCV_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCBCV_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp báo cáo viên', 6)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'c1bbdebb-3005-4fb0-9a51-1e1adcc7d46c', N'PCNU_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCNU_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp vệ sinh nữ chiến sĩ', 3)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'32bab3b2-f3b1-4d9b-a092-205862102dd6', N'(LHT_TT+PCCV_TT+PCTN_TT+PCTNVK_TT+HSBL_TT)*BHXHDV_HS', N'CACH1', N'BHXHDV_TT', N'', N'', NULL, N'', NULL, N'Bảo hiểm xã hội đơn vị đóng (thành tiền)', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'f682b48a-2320-4a5b-a5ef-308a446cbab6', N'PCBVBG_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCBVBG_TT', NULL, NULL, NULL, N'', NULL, N'Phụ cấp bảo vệ biên giới', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'1fd5b41b-5f3b-4c68-a677-32b1c262b930', N'PCTRA30_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCTRA30_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp trách nhiệm 30%', 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'd16c769f-902c-46b7-8df9-3a840c9a990c', N'(LHT_TT+PCCV_TT+PCTNVK_TT+HSBL_TT)*PCBD_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCBD_TT', N'', N'', NULL, N'Phụ cấp biểu diễn (thanh sắc)', NULL, N'Phụ cấp biểu diễn, thanh sắc', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'e4aea5c5-d6c8-448e-8875-3bb24c26105e', N'PCCONGDOAN_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCCONGDOAN_TT', N'', N'', NULL, N'Phụ cấp công đoàn', NULL, N'Phụ cấp công đoàn', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'a7c100c4-323f-4b4d-8e74-40cc417afd4d', N'PCTRA10_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCTRA10_TT', N'', N'', NULL, N'Phụ cấp trách nhiệm', NULL, N'Phụ cấp trách nhiệm 10%', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'dc9bf588-919e-402d-8e4c-4aa647732fd7', N'PCTRA25_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCTRA25_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp trách nhiệm 25%', 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'519549c2-cbe6-4a21-8a63-533334741418', N'XAUTO', N'CACH1', N'THUETNCN_TT', N'', N'', NULL, N'', NULL, N'Thuế thu nhập cá nhân (thành tiền)', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'0c1a61fe-8410-4d4f-b69b-58ab27eca2c9', N'(LHT_TT+PCCV_TT+PCTNVK_TT+HSBL_TT)*PC15_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PC15_TT', N'', N'', NULL, N'Phụ cấp kỹ thuật bay', NULL, N'Phụ cấp kỹ thuật bay', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'9eb4335d-a6ed-4912-a7c7-5b3d9a9682b0', N'PCANQP_HS*(LHT_TT+PCCV_TT+PCTNVK_TT+HSBL_TT)*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCANQP_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp an ninh quốc phòng', 3)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'f5b2dc00-8b54-4a81-a4cf-66b60632b4eb', N'LCS*0.4*PCKVCS_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCKVCS_TT', NULL, NULL, NULL, N'Phụ cấp khu vực dành cho chiến sĩ', NULL, N'Phụ cấp khu vực chiến sĩ', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'0d958a2c-fd96-4ed0-9030-70ef62edbdd9', N'LCS*PCCOYEU_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCCOYEU_TT', NULL, NULL, NULL, N'Phụ cấp cơ yếu', NULL, N'Phụ cấp trách nhiệm cơ yếu', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'1834336b-66c2-4fc5-afda-71101bf1ad6b', N'(LHT_TT+PCCV_TT+PCTNVK_TT+HSBL_TT)*PCKIE_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCKIE_TT', N'', N'', NULL, N'', NULL, N'Phụ cấp kiêm nhiệm', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'eee392c2-89a2-4ec0-ae72-734b03817b22', N'PCPHICONG_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCPHICONG_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp phi công', 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'4dab7f37-f764-48d4-bfe1-741f11969bf2', N'THUETNCN_TT+TA_TONG+GTKHAC_TT', N'CACH1', N'PHAITRU_SUM', N'', N'', NULL, N'Tổng nhóm các khoản phải trừ trên bảng lương', NULL, N'Tổng các khoản phải trừ trên bảng lương', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'68b6b29d-1eed-4d43-8bbd-77aa475c4929', N'(LHT_TT+PCCV_TT+PCTNVK_TT+HSBL_TT)*PCTGI_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCTGI_TT', N'', N'', NULL, N'Phụ cấp quản lý tạm giam, nhà tạm giữ', NULL, N'Phụ cấp quản lý trại tạm giam, tạm giữ', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'e026d9f7-7872-4b35-ae5d-7a5b11040c3b', N'(LHT_TT+PCCV_TT+HSBL_TT+PCTNVK_TT)*PCDTQUANSU_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCDTQUANSU_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp đặc thù quân sự', 3)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'ad910ed1-eda3-4b60-bad1-8058798f622d', N'PCTRA20_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCTRA20_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp trách nhiệm 20%', 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'985fbe3e-629b-48ea-abbf-819d499d76fd', N'(LHT_TT+PCCV_TT+PCTN_TT+PCTNVK_TT+HSBL_TT)*BHYTCN_HS', N'CACH1', N'BHYTCN_TT', N'', N'', NULL, N'', NULL, N'Bảo hiểm y tế cá nhân đóng (thành tiền)', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'0b24a02f-63ce-43c9-b408-85a57f8f4dc9', N'LUONGTHANG_SUM-PHAITRU_SUM', N'CACH1', N'THANHTIEN', N'', N'', NULL, N'', NULL, N'Lương tháng còn được nhận', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'75d8d72e-6cd0-41a5-9d49-8f421a279317', N'(LHT_TT+PCCV_TT+PCTNVK_TT+HSBL_TT)*PCCOV_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCCOV_TT', N'', N'', NULL, N'', NULL, N'Phụ cấp công vụ', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'b4602908-e60d-4cb3-b506-911ffc4c9647', N'PCHOIPHUNU_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCHOIPHUNU_TT', N'', N'', NULL, N'Phụ cấp hội phụ nữ', NULL, N'Phụ cấp hội phụ nữ', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'd8ec560d-c9b2-4e46-8ec7-913436924b29', N'LCS*PCBAOMAT_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCBAOMAT_TT', NULL, NULL, NULL, N'Phụ cấp văn thư bảo mật', NULL, N'Phụ cấp trách nhiệm bảo mật', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'1db12884-d64a-4630-9da6-92a79d2b33ab', N'PCCOV_TT+PCKIE_TT', N'CACH1', N'PCCT_TT', N'', N'', NULL, N'Tổng các phụ cấp chịu thuế', NULL, N'Tổng phụ cấp chịu thuế', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'cc3307d8-1675-432a-b47c-9707d8b9f8b8', N'PCDTN_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCDTN_TT', N'', N'', NULL, N'Phụ cấp đoàn thanh niên', NULL, N'Phụ cấp đoàn thanh niên', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'87a181c8-4cd1-4704-abdf-9dc1b9f52965', N'PCTRA45_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCTRA45_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp trách nhiệm 45%', 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'51ea1225-f45d-4a58-921b-9df561023725', N'PCTRA50_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCTRA50_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp trách nhiệm 50%', 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'1ee09741-2ec1-4a25-89c9-9f8052c4c6e5', N'THANG_TCXN*LCS', N'CACH1', N'TCRAQUAN_TT', NULL, NULL, NULL, NULL, NULL, N'Trợ cấp xuất ngũ', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'599e54c3-d6b4-43ce-8223-a0dff24cd09a', N'PCTS_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCTS_TT', NULL, NULL, 2024, NULL, NULL, N'Phụ cấp tiến sĩ', 5)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'8118bbde-9b47-4cae-8971-a260593115eb', N'PCTRA40_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCTRA40_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp trách nhiệm 40%', 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'7121571f-cba9-42d0-95b1-a3ae5c4dbc26', N'(LHT_TT+PCCV_TT+PCTN_TT+PCTNVK_TT+HSBL_TT)*KPCDDV_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'KPCDDV_TT', N'', N'', NULL, N'', NULL, N'Kinh phí công đoàn đơn vị đóng (thành tiền)', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'eabf0bcd-12f2-4629-ba33-a3f3e89a2434', N'PCCOV_TT+PCTRA_SUM+PCDACTHU_SUM+PCKHAC_SUM+HSBL_TT+PCKV_TT+PCBVBG_TT+PCNU_TT', N'CACH1', N'LUONGTHANG_SUM', N'', N'', NULL, N'', NULL, N'Tổng lương, phụ cấp được hưởng', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'160937a0-7e8b-4d92-b53f-adfa4f24e896', N'PCTHANHTRA_TT+PCDTQUANSU_TT+PCDTN_TT+PCCU_TT+PCHOIPHUNU_TT+PCCONGDOAN_TT+PCANQP_TT+PCGS_TT+PCTS_TT+PCPGS_TT+PCBCV_TT', N'CACH1', N'PCKHAC_SUM', N'', N'', NULL, N'', NULL, N'Tổng phụ cấp đưa vào cột phụ cấp khác', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'ec45c073-2fe4-4379-a5c7-afdfdf725cfe', N'PCTRA15_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCTRA15_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp trách nhiệm 15%', 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'7e2c8366-acc6-4741-b207-b4dcab46a963', N'LCS*PCBAOVU_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCBAOVU_TT', NULL, NULL, NULL, N'Phụ cấp báo vụ', NULL, N'Phụ cấp báo vụ', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'99900155-6520-4729-8ba7-b826d46c000d', N'PCPGS_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCPGS_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp phó giáo sư', 4)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'58d65a13-2bd8-4042-9242-b97eeff8cc7c', N'PCTRA10_TT+PCTRA15_TT+PCTRA20_TT+PCTRA25_TT+PCTRA30_TT+PCTRA35_TT+PCTRA40_TT+PCTRA45_TT+PCTRA50_TT+PCBVBG_TT+PCTAUBP_TT+PCBAOMAT_TT+PCBAOVU_TT+PCCOYEU_TT', N'CACH1', N'PCTRA_SUM', N'', N'', NULL, N'Tổng các phụ cấp trách nhiệm', NULL, N'Tổng phụ cấp trách nhiệm', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'28aff24f-17b1-4cf5-b095-ba2d4aee476e', N'LCS*PC8_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PC8_TT', N'', N'', NULL, N'Phụ cấp nóng, độc hại, nguy hiểm', NULL, N'Phụ cấp nóng, độc hại, nguy hiểm', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'817160c9-e44f-4fa3-96df-d679e39da640', N'PCTHD_HS*(LHT_TT+HSBL_TT)*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCTHD_TT', N'', N'', NULL, N'Phụ cấp trên hạn định (HSQ+CS)', NULL, N'Phụ cấp trên hạn định', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'5880b05d-aedd-4fb3-9859-dba5f41648df', N'LCS*PC5_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PC5_TT', N'', N'', NULL, N'Phụ cấp quân binh chủng kỹ thuật', NULL, N'Phụ cấp quân binh chủng kỹ thuật', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'126354f7-c171-4746-9986-dee560b319c2', N'TA_THANG+TA_TT', N'CACH1', N'TA_TONG', NULL, NULL, NULL, N'Tổng các loại tiền ăn', NULL, N'Tổng các loại tiền ăn', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'96e3a9a7-4fbe-435f-970f-df8be28b0272', N'(LHT_TT+PCCV_TT+PCTNVK_TT+HSBL_TT)*PCGV_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCGV_TT', N'', N'', NULL, N'Phụ cấp ưu đãi giáo viên', NULL, N'Phụ cấp ưu đãi giáo viên', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'6ee6def7-edb3-495f-bfff-e55c28f95a6a', N'GTKHAC_TT+TRICHLUONG_TT', N'CACH1', N'PHAITRUKHAC_SUM', NULL, NULL, NULL, N'Tổng các loại giảm trừ khác', NULL, N'Tổng các loại giảm trừ khác', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'3953a927-c254-4cc8-9b33-eac84bc30c70', N'PCKV_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN+PCKVCS_TT', N'CACH1', N'PCKV_TT', N'', N'', NULL, N'Phụ cấp khu vực', NULL, N'Phụ cấp khu vực', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'0ccb5d34-88e7-43c7-89e2-efc03ea91ae6', N'PCCT_TT-GTPT_TT', N'CACH1', N'LUONGTHUE_TT', N'', N'', NULL, N'Tổng các khoản thu nhập tính thuế TNCN', NULL, N'Thu nhập tính thuế TNCN', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'd5764394-e954-4317-9529-f38d07695821', N'LCS*PC2_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PC2_TT', N'', N'', NULL, N'Phụ cấp Hải quân', NULL, N'Phụ cấp Hải quân', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'4468fdf2-bf4e-494a-a5e4-f3d188f90af7', N'BHXHDVCS_HS*LCS', N'CACH1', N'BHXHDVCS_TT', NULL, NULL, 2022, NULL, NULL, N'Bảo hiểm xã hội đơn vị đóng cho chiến sĩ (thành tiền)', 11)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'7cc4ec8a-1ab5-4f47-8be5-f5a9601841db', N'PC15_TT+PC2_TT+PC5_TT+PC8_TT+PCBD_TT+PCDACTHU_KHAC2+PCDACTHU_KHAC1+PCDACTHU_KHAC3+PCKB_TT+PCTGI_TT+PCGV_TT+PCTHUHUT_TT+PCPHICONG_TT', N'CACH1', N'PCDACTHU_SUM', N'', N'', NULL, N'Tổng nhóm các khoản phải trừ trên bảng lương', NULL, N'Tổng các phụ cấp dặc biệt, đặc thù', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'dbcea318-3a69-48d8-b62a-f6afe1cb9576', N'PCCU_HS*LCS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCCU_TT', N'', N'', NULL, N'Phụ cấp cấp ủy', NULL, N'Phụ cấp cấp ủy', NULL)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'8d88f54b-56bf-44fd-bdea-f86c9ea04fa3', N'THANG_TCVIECLAM*LCS', N'CACH1', N'TCVIECLAM_TT', NULL, NULL, 2022, NULL, NULL, N'Trợ cấp việc làm', 8)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_TruyThu] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'a3766783-4370-4fa1-a166-f9fed64aaacb', N'(LHT_TT+PCCV_TT+PCTNVK_TT+HSBL_TT)*PCTHUHUT_HS*TRUYTHU_SN/CONGCHUAN_SN', N'CACH1', N'PCTHUHUT_TT', NULL, NULL, 2022, NULL, NULL, N'Phụ cấp thu hút', 3)
GO
-- CACH TINH LUONG
INSERT [dbo].[TL_DM_ThemCachTinhLuong] ([Id], [Ma_ThemCachTL], [Ten_ThemCachTL]) VALUES (N'7fa2abf3-78ee-4b6f-8be4-9b9c6e6f5795', N'CACH1', N'Cách tính lương truy thu')
GO

--DM_PHUCAP
INSERT [dbo].[TL_DM_PhuCap] ([Id], [bGiaTri], [bHuongPc_Sn], [bSaoChep], [Chon], [Cong_Thuc], [Dinh_Dang], [Gia_Tri], [He_So], [HuongPC_SN], [iDinhDang], [iLoai], [Is_Formula], [Is_Readonly], [IThang_ToiDa], [Ma_KMCP], [Ma_PhuCap], [Ma_TTM_Ng], [Numeric_Scale], [Parent], [PhanTram_CT], [Readonly], [Splits], [Ten_Ngan], [Ten_PhuCap], [Tinh_BHXH], [Tinh_TNCN], [Xau_Noi_Ma], [XSort], [fGiaTriLonNhat], [fGiaTriNhoNhat], [fGiaTriPhuCap_KemTheo], [iId_Ma_PhuCap_KemTheo], [iId_PhuCap_KemTheo], [Ten_NganHang]) VALUES (N'f3fed731-7e39-4865-b865-39cd3f335ecb', 0, 0, 0, 1, N'', NULL, CAST(0.000 AS Numeric(17, 3)), NULL, NULL, NULL, NULL, 0, 0, NULL, N'', N'TRUYTHU', NULL, 0, N'', CAST(0.0000 AS Numeric(17, 4)), NULL, 1, N'', N'Truy thu', 1, NULL, N'TRUYTHU                                                                                                                                                                                                                                                       ', 13, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TL_DM_PhuCap] ([Id], [bGiaTri], [bHuongPc_Sn], [bSaoChep], [Chon], [Cong_Thuc], [Dinh_Dang], [Gia_Tri], [He_So], [HuongPC_SN], [iDinhDang], [iLoai], [Is_Formula], [Is_Readonly], [IThang_ToiDa], [Ma_KMCP], [Ma_PhuCap], [Ma_TTM_Ng], [Numeric_Scale], [Parent], [PhanTram_CT], [Readonly], [Splits], [Ten_Ngan], [Ten_PhuCap], [Tinh_BHXH], [Tinh_TNCN], [Xau_Noi_Ma], [XSort], [fGiaTriLonNhat], [fGiaTriNhoNhat], [fGiaTriPhuCap_KemTheo], [iId_Ma_PhuCap_KemTheo], [iId_PhuCap_KemTheo], [Ten_NganHang]) VALUES (N'91b527eb-deb1-47c3-8f39-af406f0ca6fc', 1, 0, 0, 1, NULL, 0, CAST(0.000 AS Numeric(17, 3)), NULL, NULL, 2, NULL, 0, 0, NULL, NULL, N'TRUYTHU_SN', NULL, NULL, N'TRUYTHU', NULL, NULL, NULL, NULL, N'Số ngày công truy thu', 1, NULL, N'TRUYTHU-TRUYTHU_SN', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

