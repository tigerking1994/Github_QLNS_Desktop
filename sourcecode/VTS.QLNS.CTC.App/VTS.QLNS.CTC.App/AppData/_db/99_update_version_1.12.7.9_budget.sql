UPDATE DM_ChuKy
SET TieuDe1_MoTa = N'Tổng hợp dự toán ngân sách năm 2022',
TieuDe2_MoTa = N'Đến đợt: {1}'
WHERE Id = 'ED30058E-AD33-4C0E-8B83-35EAB52EA07E'

UPDATE DM_ChuKy
SET TieuDe1_MoTa = N'Tổng hợp dự toán ngân sách năm 2021 - Phần Hiện vật',
TieuDe2_MoTa = N'Đến đợt: {1}'
WHERE Id = '27D322C0-656B-4E47-AF86-76F75B9C675B'

UPDATE DM_ChuKy
SET TieuDe1_MoTa = N'Tổng hợp dự toán ngân sách năm 2021 - Phần Tự chi',
TieuDe2_MoTa = N'Đến đợt: {1}'
WHERE Id = 'E4FF4CF8-BC14-42ED-BEAA-9993FED7D709'

UPDATE DM_ChuKy
SET TieuDe1_MoTa = N'Dự toán chi ngân sách năm 2022',
TieuDe2_MoTa = N'Đến đợt ngày {1}'
WHERE Id = 'A91A7909-B9B7-440B-8E8A-B2D07D8169A0'

UPDATE DM_ChuKy
SET TieuDe1_MoTa = N'Dự toán chi ngân sách năm 2022',
TieuDe2_MoTa = N'Kèm theo Thông báo số: {0}, ngày {1} của Ban Tài chính'
WHERE Id = '4B14CBFD-6BDE-4A13-A163-D88B77B1BAE6'

/****** Object:  StoredProcedure [dbo].[sp_skt_nhap_so_nhu_cau]    Script Date: 21/05/2023 2:13:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_nhap_so_nhu_cau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_nhap_so_nhu_cau]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhan_so_kiem_tra_1]    Script Date: 21/05/2023 2:13:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_nhan_so_kiem_tra_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_nhan_so_kiem_tra_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhan_so_kiem_tra]    Script Date: 21/05/2023 2:13:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_nhan_so_kiem_tra]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_nhan_so_kiem_tra]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]    Script Date: 21/05/2023 2:13:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data]    Script Date: 21/05/2023 2:13:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_data]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_data]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data]    Script Date: 21/05/2023 2:13:03 PM ******/
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
	@isUpdateDanhMucDonViTinh int
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

END

;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]    Script Date: 21/05/2023 2:13:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3]
	@LoaiChungTu int,
	@IdDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@MaNguonNganSach int,
	@MaBQuanLy varchar(max),
	@dvt int
AS
BEGIN
declare @countDonVi int;
SELECT @countDonVi = count(*) FROM f_split(@IdDonVi);
declare @sModule nvarchar(max) = 'BUDGET_DEMANDCHECK_DEMAND',
	@idDuToan nvarchar(max) = 'BUDGET_ESTIMATE',
	@idSoKiemTra nvarchar(max) = 'BUDGET_DEMANDCHECK_CHECK'
declare @tblDuToan table (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200), TuChi float)
declare @tblSkt table (iID_MLSKT uniqueidentifier, KyHieu nvarchar(200), TuChi float)
declare @countCanCuDuToan int;
declare @countCanCuSkt int ;
declare @countSktChiTiet int ; 

--count chi tiet
--SELECT @countSktChiTiet = count(*) 
--	FROM NS_SKT_ChungTuChiTiet
--	WHERE 
--		iID_MaDonVi in (select * from f_split(@IdDonVi)) 
--		AND iNamLamViec = @NamLamViec
--		AND iNamNganSach = @NamNganSach
--		AND iID_MaNguonNganSach = @MaNguonNganSach
--		AND iLoaiChungTu = @LoaiChungTu
--		AND iLoai = 0;

---- count can cu du toan
--SELECT @countCanCuDuToan = count(*) 
--	FROM NS_SKT_ChungTuChiTiet_CanCu 
--	WHERE 
--		iNamLamViec = @NamLamViec
--		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
--						FROM NS_CauHinh_CanCu 
--						WHERE sModule = @sModule 
--							AND iID_MaChucNang = @idDuToan 
--							AND inamLamViec = @NamLamViec
--							AND iNamCancu = @NamLamViec - 1)
--		AND iiID_CTSoKiemTra in 
--		(SELECT iID_CTSoKiemTra
--			FROM NS_SKT_ChungTu
--		WHERE 
--		iID_MaDonVi in (select * from f_split(@IdDonVi))
--		AND iNamLamViec = @NamLamViec
--		AND iNamNganSach = @NamNganSach
--		AND iID_MaNguonNganSach = @MaNguonNganSach
--		AND iLoaiChungTu = @LoaiChungTu
--		AND iLoai = 0);

---- count can cu so kiem tra
--SELECT @countCanCuSkt = count(*) 
--	FROM NS_SKT_ChungTuChiTiet_CanCu 
--	WHERE 
--		iNamLamViec = @NamLamViec
--		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
--						FROM NS_CauHinh_CanCu 
--						WHERE sModule = @sModule 
--							AND iID_MaChucNang = @idSoKiemTra 
--							AND inamLamViec = @NamLamViec
--							AND iNamCancu = @NamLamViec - 1)
--		AND iiID_CTSoKiemTra in 
--		(SELECT iID_CTSoKiemTra
--			FROM NS_SKT_ChungTu
--		WHERE 
--		iID_MaDonVi in (select * from f_split(@IdDonVi))
--		AND iNamLamViec = @NamLamViec
--		AND iNamNganSach = @NamNganSach
--		AND iID_MaNguonNganSach = @MaNguonNganSach
--		AND iLoaiChungTu = @LoaiChungTu
--		AND iLoai = 0);

--IF (@countSktChiTiet = 0 and @countCanCuDuToan = 0 and @countCanCuSkt = 0)
--	INSERT INTO @tblDuToan (iID_MLSKT, KyHieu, TuChi)
--	SELECT null, mp.sSKT_KyHieu KyHieu,
--       Sum(TuChi) TuChi
--	FROM NS_MLSKT_MLNS mp
--	JOIN
--	  (SELECT SXauNoiMa,
--			  sum(fTuChi) TuChi,
--			  sum(fHangNhap) HangNhap,
--			  sum(fHangMua) HangMua,
--			  sum(fPhanCap) PhanCap,
--			  sum(fTuChi) MuaHangHienVat,
--			  sum(fTuChi) DacThu
--	   FROM NS_DT_ChungTuChiTiet
--	   WHERE iID_DTChungTu in
--		   (SELECT iID_DTChungTu
--			FROM NS_DT_ChungTu
--			WHERE iNamLamViec = @NamLamViec - 1
--			  AND iNamNganSach = @NamNganSach
--			  AND iID_MaNguonNganSach = @MaNguonNganSach
--			  AND iLoaiChungTu = @LoaiChungTu
--			  AND (iloai = 0 OR iLoai = 1)
--			  AND iLoaiDuToan = 1 )
--			  AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
--	   GROUP BY SXauNoiMa) dtct ON mp.sNS_XauNoiMa = dtct.SXauNoiMa
--	WHERE mp.iNamLamViec = @NamLamViec
--	GROUP BY mp.sSKT_KyHieu
--ELSE
INSERT INTO @tblDuToan (iID_MLSKT, KyHieu, TuChi)
SELECT cc.iID_MLSKT, sKyHieu, sum(isnull(cc.fTuChi,0))
FROM 
	(SELECT * 
	FROM NS_SKT_ChungTuChiTiet_CanCu 
	WHERE 
		iNamLamViec = @NamLamViec
		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
						FROM NS_CauHinh_CanCu 
						WHERE sModule = @sModule 
							AND iID_MaChucNang = @idDuToan 
							AND inamLamViec = @NamLamViec
							AND iNamCancu = @NamLamViec - 1)
			AND iiID_CTSoKiemTra in 
		(SELECT iID_CTSoKiemTra
			FROM NS_SKT_ChungTu
		WHERE 
		iID_MaDonVi in (select * from f_split(@IdDonVi))
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach
		AND iID_MaNguonNganSach = @MaNguonNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT,sKyHieu;

--IF (@countSktChiTiet = 0 and @countCanCuDuToan = 0 and @countCanCuSkt = 0)
--	INSERT INTO @tblSkt (iID_MLSKT, KyHieu, TuChi)
--	SELECT ctct.iID_MLSKT, sKyHieu,ctct.fTuChi
--	FROM NS_SKT_ChungTuChiTiet ctct
--	JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
--	WHERE ct.iNamLamViec = @NamLamViec - 1
--	  AND ct.iNamNganSach = @NamNganSach
--	  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
--	  AND ct.iLoaiChungTu = @LoaiChungTu
--	  AND ct.iloai = 3
--	  AND ctct.iLoai = 3
--ELSE
INSERT INTO @tblSkt (iID_MLSKT,KyHieu, TuChi)
SELECT cc.iID_MLSKT, cc.sKyHieu, sum(isnull(cc.fTuChi,0))
FROM 
	(SELECT * 
	FROM NS_SKT_ChungTuChiTiet_CanCu 
	WHERE 
		iNamLamViec = @NamLamViec
		AND iID_CanCu IN (SELECT iID_CauHinh_CanCu 
						FROM NS_CauHinh_CanCu 
						WHERE sModule = @sModule 
							AND iID_MaChucNang = @idSoKiemTra 
							AND inamLamViec = @NamLamViec
							AND iNamCancu = @NamLamViec - 1)
	   	AND iiID_CTSoKiemTra in 
		(SELECT iID_CTSoKiemTra
			FROM NS_SKT_ChungTu
		WHERE 
		iID_MaDonVi in (select * from f_split(@IdDonVi))
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach
		AND iID_MaNguonNganSach = @MaNguonNganSach
		AND iLoaiChungTu = @LoaiChungTu
		AND iLoai = 0)
	) cc
GROUP BY cc.iID_MLSKT,cc.sKyHieu;

select iID_MLSKT,
       sKyHieu,
       sum(isnull(fTuChi,0)) fTuChi,
       sum(isnull(fHuyDongTonKho,0)) fHuyDongTonKho,
       sum(isnull(fTonKhoDenNgay,0)) fTonKhoDenNgay,
       sum(isnull(fMuaHangCapHienVat,0)) fMuaHangCapHienVat,
       sum(isnull(fPhanCap,0)) fPhanCap,
       STUFF((
			SELECT ', ' + ctct2.sGhiChu
			FROM sncChiTiet ctct2
			WHERE (ctct2.iID_MLSKT = sncChiTiet.iID_MLSKT AND ctct2.sGhiChu <> '') 
			FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
		  ,1,2,'') AS sGhiChu
	   
	   INTO #SoNhuCauTongHop 
	   from 
(SELECT ctct.iID_MLSKT,
       ctct.sKyHieu,
       isnull(ctct.fTuChi,0) fTuChi,
       isnull(ctct.fHuyDongTonKho,0) fHuyDongTonKho,
       isnull(ctct.fTonKhoDenNgay,0) fTonKhoDenNgay,
       isnull(ctct.fMuaHangCapHienVat,0) fMuaHangCapHienVat,
       isnull(ctct.fPhanCap,0) fPhanCap
       --case when @countDonVi = 1 then ctct.sGhiChu else '' end as sGhiChu
FROM NS_SKT_ChungTuChiTiet ctct
JOIN NS_SKT_ChungTu ct ON ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
WHERE ct.iNamLamViec = @NamLamViec
  AND ct.iNamNganSach = @NamNganSach
  AND ct.iID_MaNguonNganSach = @MaNguonNganSach
  AND ct.iLoaiChungTu = @LoaiChungTu
  AND ct.iloai = 0
  --AND ct.bKhoa = 1
  AND ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
  ) as sncChiTiet
GROUP BY iID_MLSKT, sKyHieu;


SELECT map.sSKT_KyHieu INTO #KyHieuSktBQuanLy
FROM NS_MLSKT_MLNS MAP
JOIN NS_MucLucNganSach mlns ON mlns.sXauNoiMa = map.sNS_XauNoiMa
AND map.iNamLamViec = mlns.iNamLamViec
WHERE map.iNamLamViec = @NamLamViec
AND sLNS in (select sLNS from NS_MucLucNganSach where iNamLamViec = @NamLamViec and iID_MaBQuanLy = @MaBQuanLy);

SELECT ml.sSTT STT,
       ml.bHangCha,
	   ml.iID_MLSKT,
	   ml.iID_MLSKTCha,
	   ml.sKyHieu,
	   ml.sMoTa,
       isnull(skt.TuChi,0) / @dvt AS SoKiemTraNamTruoc ,
       isnull(dt.TuChi,0) / @dvt AS DuToanDauNam ,
       isnull(snc.fTonKhoDenNgay,0) / @dvt AS TonKhoDenNgay,
       isnull(snc.fHuyDongTonKho,0) / @dvt AS HuyDongTonKho,
       isnull(snc.fTuChi,0) / @dvt AS TuChi,
	   snc.sGhiChu
FROM NS_SKT_MucLuc ml
LEFT JOIN #SoNhuCauTongHop snc on snc.iID_MLSKT = ml.iID_MLSKT
LEFT JOIN @tblSkt skt on skt.KyHieu = ml.sKyHieu
LEFT JOIN @tblDuToan dt on dt.KyHieu = ml.sKyHieu and skt.iID_MLSKT = dt.iID_MLSKT
WHERE ml.iNamLamViec = @NamLamViec
AND (@MaBQuanLy = '0' OR ml.sKyHieu in (SELECT * FROM #KyHieuSktBQuanLy))
AND (skt.TuChi <> 0 OR dt.TuChi <> 0 OR snc.fTonKhoDenNgay <> 0 OR snc.fHuyDongTonKho <> 0 OR snc.fTuChi <> 0 OR (snc.sGhiChu is not null and snc.sGhiChu != ''));

DROP TABLE #SoNhuCauTongHop;
DROP TABLE #KyHieuSktBQuanLy;
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhan_so_kiem_tra]    Script Date: 21/05/2023 2:13:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_skt_nhan_so_kiem_tra]
	@Loai nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@LoaiChungTu int,
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @NamLamViec AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT 
		ct.iID_CTSoKiemTra,
		ct.iID_MaDonVi,
		ct.SSoChungTu,
		ct.DNgayChungTu,
		ct.SSoQuyetDinh,
		ct.DNgayQuyetDinh,
		ct.SMoTa,
		ct.iLoai,
		ct.bKhoa,
		ct.iNamLamViec,
		ct.iNamNganSach,
		ct.iID_MaNguonNganSach,
		ct.DNgayTao,
		ct.DNgaySua,
		ct.SNguoiTao,
		ct.SNguoiSua,
		ct.iSoChungTuIndex,
		ct.iLoaiChungTu,
		ct.sDSSoChungTuTongHop,
		ctct.fTongTuChi,
		ctct.fTongPhanCap,
		ctct.fTongMuaHangCapHienVat,
		ct.sTenDonVi,
		ct.bDaTongHop,
		ct.iLoaiNguonNganSach
	FROM
		(
			SELECT 
				ct.*, ddv.sTenDonVi 
			FROM 
				NS_SKT_ChungTu ct
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE 
				ct.iNamLamViec = @NamLamViec 
				AND ct.iNamNganSach = @NamNganSach
				AND ct.iID_MaNguonNganSach = @NguonNganSach
				--AND ct.iLoai = @Loai 
				AND ct.iLoai in (select * from f_split(@Loai)) 
				AND ct.iLoaiChungTu = @LoaiChungTu
				-- AND EXISTS (SELECT * from f_split(ct.iID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1)
				-- AND ((@CountDonViCha = 0 and bKhoa = 1) OR (@CountDonViCha <> 0))

				AND ((EXISTS (SELECT * from f_split(ct.iID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1)
				AND (@CountDonViCha = 0 and bKhoa = 1)) OR (@CountDonViCha <> 0))

		) ct
	LEFT JOIN 
		(
			SELECT 
				iID_CTSoKiemTra,
				sum(fTuChi) as fTongTuChi,
				sum(fPhanCap) as fTongPhanCap,
				sum(fMuaHangCapHienVat) as fTongMuaHangCapHienVat
			FROM 
				NS_SKT_ChungTuChiTiet ctct
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi
			WHERE 
				--ctct.iLoai = @Loai
				ctct.iLoai in (select * from f_split(@Loai)) 
				AND ctct.iNamLamViec = @NamLamViec
				AND ctct.iNamNganSach = @NamNganSach
				AND ctct.iID_MaNguonNganSach = @NguonNganSach
				AND 
				(
					(
						(dv.iLoai = '0' OR @LoaiChungTu = 1) 
						AND EXISTS (SELECT * FROM NS_SKT_MucLuc WHERE iID_MLSKT = ctct.iID_MLSKT AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap)))
					)
					OR 
					(
						EXISTS 
						(
							SELECT * FROM (SELECT * FROM NS_SKT_MucLuc WHERE iID_MLSKT = ctct.iID_MLSKT AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap))) ml
							JOIN DanhMuc dm ON dm.iID_MaDanhMuc = ml.sNG AND dm.sType = 'NS_Nganh' AND dm.sGiaTri = dv.iID_MaDonVi AND dm.iNamLamViec = @NamLamViec
						)
					)
				)
		GROUP BY iID_CTSoKiemTra
		) ctct
	ON ctct.iID_CTSoKiemTra =  ct.iID_CTSoKiemTra;
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhan_so_kiem_tra_1]    Script Date: 21/05/2023 2:13:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_skt_nhan_so_kiem_tra_1]
	@Loai nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@LoaiChungTu int,
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @NamLamViec AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT 
		ct.iID_CTSoKiemTra,
		ct.iID_MaDonVi,
		ct.SSoChungTu,
		ct.DNgayChungTu,
		ct.SSoQuyetDinh,
		ct.DNgayQuyetDinh,
		ct.SMoTa,
		ct.iLoai,
		ct.bKhoa,
		ct.iNamLamViec,
		ct.iNamNganSach,
		ct.iID_MaNguonNganSach,
		ct.DNgayTao,
		ct.DNgaySua,
		ct.SNguoiTao,
		ct.SNguoiSua,
		ct.iSoChungTuIndex,
		ct.iLoaiChungTu,
		ct.sDSSoChungTuTongHop,
		ctct.fTongTuChi,
		ctct.fTongPhanCap,
		ctct.fTongMuaHangCapHienVat,
		ct.sTenDonVi,
		ct.bDaTongHop,
		ctct.iLoai as iLoai_CTCT,
		ct.iLoaiNguonNganSach
	Into #Temp
	FROM
		(
			SELECT 
				ct.*, ddv.sTenDonVi 
			FROM 
				NS_SKT_ChungTu ct
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE 
				ct.iNamLamViec = @NamLamViec 
				AND ct.iNamNganSach = @NamNganSach
				AND ct.iID_MaNguonNganSach = @NguonNganSach
				--AND ct.iLoai = @Loai 
				AND ct.iLoai in (select * from f_split(@Loai)) 
				AND ct.iLoaiChungTu = @LoaiChungTu
				-- AND EXISTS (SELECT * from f_split(ct.iID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1)
				-- AND ((@CountDonViCha = 0 and bKhoa = 1) OR (@CountDonViCha <> 0))

				AND ((EXISTS (SELECT * from f_split(ct.iID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1)
				AND (@CountDonViCha = 0 and bKhoa = 1)) OR (@CountDonViCha <> 0))

		) ct
	LEFT JOIN 
		(
			SELECT 
				iID_CTSoKiemTra,
				sum(fTuChi) as fTongTuChi,
				sum(fPhanCap) as fTongPhanCap,
				sum(fMuaHangCapHienVat) as fTongMuaHangCapHienVat,
				ctct.iLoai as iLoai
			FROM 
				NS_SKT_ChungTuChiTiet ctct
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi
			WHERE 
				--ctct.iLoai = @Loai
				ctct.iLoai in (select * from f_split(@Loai))
				AND ctct.iNamLamViec = @NamLamViec
				AND ctct.iNamNganSach = @NamNganSach
				AND ctct.iID_MaNguonNganSach = @NguonNganSach
				AND 
				(
					(
						(dv.iLoai = '0' OR @LoaiChungTu = 1) 
						AND EXISTS (SELECT * FROM NS_SKT_MucLuc WHERE iID_MLSKT = ctct.iID_MLSKT AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap)))
					)
					OR 
					(
						EXISTS 
						(
							SELECT * FROM (SELECT * FROM NS_SKT_MucLuc WHERE iID_MLSKT = ctct.iID_MLSKT AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap))) ml
							JOIN DanhMuc dm ON dm.iID_MaDanhMuc = ml.sNG AND dm.sType = 'NS_Nganh' AND dm.sGiaTri = dv.iID_MaDonVi AND dm.iNamLamViec = @NamLamViec
						)
					)
				)
		GROUP BY iID_CTSoKiemTra, ctct.iLoai
		) ctct
	ON ctct.iID_CTSoKiemTra =  ct.iID_CTSoKiemTra;

select * from #Temp
except
select * from #Temp
where iLoai = 3 and iLoai_CTCT = 2 

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhap_so_nhu_cau]    Script Date: 21/05/2023 2:13:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_skt_nhap_so_nhu_cau]
	@Loai int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@LoaiChungTu int,
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @NamLamViec AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT 
		ct.iID_CTSoKiemTra,
		ct.iID_MaDonVi,
		ct.sSoChungTu,
		ct.dNgayChungTu,
		ct.sSoQuyetDinh,
		ct.dNgayQuyetDinh,
		ct.sMoTa,
		ct.iLoai,
		ct.bKhoa,
		ct.iNamLamViec,
		ct.iNamNganSach,
		ct.iID_MaNguonNganSach,
		ct.DNgayTao,
		ct.DNgaySua,
		ct.SNguoiTao,
		ct.SNguoiSua,
		ct.iSoChungTuIndex,
		ct.iLoaiChungTu,
		ct.SDssoChungTuTongHop,
		ct.fTongTuChi,
		ct.fTongPhanCap,
		ct.fTongMuaHangCapHienVat,
		ct.sTenDonVi,
		ct.bDaTongHop,
		ct.iLoaiNguonNganSach
		into #sktChungTu
	FROM
		(
			SELECT 
				ct.*, ddv.sTenDonVi 
			FROM 
				NS_SKT_ChungTu ct
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE 
				ct.iNamLamViec = @NamLamViec
				AND ct.iNamNganSach = @NamNganSach
				AND ct.iID_MaNguonNganSach = @NguonNganSach
				AND ct.iLoai = @Loai 
				AND ct.iLoaiChungTu = @LoaiChungTu
		) ct;
	--LEFT JOIN 
	--	(
	--		SELECT 
	--			iID_CTSoKiemTra, 
	--			sum(fTuChi) as fTongTuChi,
	--			sum(fPhanCap) as fTongPhanCap,
	--			sum(fMuaHangCapHienVat) as fTongMuaHangCapHienVat
	--		FROM 
	--			NS_SKT_ChungTuChiTiet ctct
	--		LEFT JOIN 
	--			(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi
	--		WHERE 
	--			ctct.iLoai = @Loai
	--			AND ctct.iNamLamViec = @NamLamViec
	--			AND ctct.iNamNganSach = @NamNganSach
	--			AND ctct.iID_MaNguonNganSach = @NguonNganSach
	--			AND 
	--			(
	--				(
	--					(dv.iLoai = '0' OR @LoaiChungTu = 1) 
	--					AND EXISTS (SELECT * FROM NS_SKT_MucLuc WHERE iID_MLSKT = ctct.iID_MLSKT AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap)))
	--				)
	--				OR 
	--				(
	--					EXISTS 
	--					(
	--						SELECT * FROM (SELECT * FROM NS_SKT_MucLuc WHERE iID_MLSKT = ctct.iID_MLSKT AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap))) ml
	--						JOIN DanhMuc dm ON dm.iID_MaDanhMuc = ml.sNG AND dm.sType = 'NS_Nganh' AND dm.sGiaTri = dv.iID_MaDonVi AND dm.iNamLamViec = @NamLamViec
	--					)
	--				)
				
	--			)
	--	GROUP BY iID_CTSoKiemTra
	--	) ctct
	--ON ctct.iID_CTSoKiemTra =  ct.iID_CTSoKiemTra;

	IF @CountDonViCha = 0
		SELECT 
			sktct.*
		FROM #sktChungTu sktct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1) dv
		ON sktct.iID_MaDonVi = dv.iID_MaDonVi
		ORDER BY sSoChungTu desc;
	ELSE
		SELECT 
			sktct.*
		FROM #sktChungTu sktct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1) dv
		ON sktct.iID_MaDonVi = dv.iID_MaDonVi
		WHERE ((dv.iID_MaDonVi IS NULL AND sktct.bKhoa = 1) OR (dv.iID_MaDonVi IS NOT NULL))
		ORDER BY sSoChungTu desc;

	drop table #sktChungTu;
END
;
;
GO


