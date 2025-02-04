/****** Object:  StoredProcedure [dbo].[sp_clone_year_danh_muc_ngay_nghi]    Script Date: 7/4/2024 9:38:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_danh_muc_ngay_nghi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_danh_muc_ngay_nghi]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 7/4/2024 9:38:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_data_update_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_data_update_new]
GO
/****** Object:  Table [dbo].[Tl_DM_NgayNghi]    Script Date: 7/4/2024 9:38:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tl_DM_NgayNghi]') AND type in (N'U'))
	DELETE FROM [dbo].[Tl_DM_NgayNghi] WHERE iNamLamViec IN (2023,2024) 
GO
/****** Object:  Table [dbo].[Tl_DM_NgayNghi]    Script Date: 7/4/2024 9:38:20 AM ******/

GO
INSERT [dbo].[Tl_DM_NgayNghi] ([Id], [dDenNgay], [dTuNgay], [sMaNgayNghi], [sTenNgayNghi], [iNamLamViec]) VALUES (N'c1eb342d-1a81-4674-b321-13eae67aa602', CAST(N'2024-05-01T00:00:00.000' AS DateTime), CAST(N'2024-05-01T00:00:00.000' AS DateTime), N'003', N'Ngày Quốc tế lao động', 2024)
GO
INSERT [dbo].[Tl_DM_NgayNghi] ([Id], [dDenNgay], [dTuNgay], [sMaNgayNghi], [sTenNgayNghi], [iNamLamViec]) VALUES (N'd535c5a1-7fda-4399-ae2a-1d13da28c331', CAST(N'2024-04-30T00:00:00.000' AS DateTime), CAST(N'2024-04-30T00:00:00.000' AS DateTime), N'002', N'Ngày Giải phóng miền Nam', 2024)
GO
INSERT [dbo].[Tl_DM_NgayNghi] ([Id], [dDenNgay], [dTuNgay], [sMaNgayNghi], [sTenNgayNghi], [iNamLamViec]) VALUES (N'96162f61-fb31-44f4-810c-22cf67e16602', CAST(N'2023-01-01T00:00:00.000' AS DateTime), CAST(N'2023-01-01T00:00:00.000' AS DateTime), N'001', N'Tết dương lịch', 2023)
GO
INSERT [dbo].[Tl_DM_NgayNghi] ([Id], [dDenNgay], [dTuNgay], [sMaNgayNghi], [sTenNgayNghi], [iNamLamViec]) VALUES (N'7f81e64e-6844-4f79-9b01-4dfc04bd1490', CAST(N'2023-05-01T00:00:00.000' AS DateTime), CAST(N'2023-05-01T00:00:00.000' AS DateTime), N'003', N'Ngày Quốc tế lao động', 2023)
GO
INSERT [dbo].[Tl_DM_NgayNghi] ([Id], [dDenNgay], [dTuNgay], [sMaNgayNghi], [sTenNgayNghi], [iNamLamViec]) VALUES (N'5f861dbf-6023-4d5b-98e8-5466bff5acc5', CAST(N'2023-09-02T00:00:00.000' AS DateTime), CAST(N'2023-09-02T00:00:00.000' AS DateTime), N'004', N'Lễ Quốc Khánh', 2023)
GO
INSERT [dbo].[Tl_DM_NgayNghi] ([Id], [dDenNgay], [dTuNgay], [sMaNgayNghi], [sTenNgayNghi], [iNamLamViec]) VALUES (N'7f92d45b-6d15-4262-98be-7d6a2184aec5', CAST(N'2024-01-01T00:00:00.000' AS DateTime), CAST(N'2024-01-01T00:00:00.000' AS DateTime), N'001', N'Tết dương lịch', 2024)
GO
INSERT [dbo].[Tl_DM_NgayNghi] ([Id], [dDenNgay], [dTuNgay], [sMaNgayNghi], [sTenNgayNghi], [iNamLamViec]) VALUES (N'2cdd5de2-c506-44fa-b8b5-a1a5b8dfa8d0', CAST(N'2024-09-02T00:00:00.000' AS DateTime), CAST(N'2024-09-02T00:00:00.000' AS DateTime), N'004', N'Lễ Quốc Khánh', 2024)
GO
INSERT [dbo].[Tl_DM_NgayNghi] ([Id], [dDenNgay], [dTuNgay], [sMaNgayNghi], [sTenNgayNghi], [iNamLamViec]) VALUES (N'9634f86c-09e2-4696-8228-b8ca45351c41', CAST(N'2023-04-30T00:00:00.000' AS DateTime), CAST(N'2023-04-30T00:00:00.000' AS DateTime), N'002', N'Ngày Giải phóng miền Nam', 2023)


/* START Feature #58032 UPDATE AND INSERT MASTER DATA THAM DINH QUYET TOAN*/

BEGIN
/*
***	UPDATE iSTT dữ liệu cũ (từ dưới lên để đảm bảo iSTT toàn vẹn )
*** + 5 , 8, 10, 13 là theo thứ tự insert vào những bản ghi mới 
***	+11, 12 là để update lại data theo thứ tự tiêu chuẩn liền mạch
***	CÁC NĂM UPDATE: 2023, 2024, 2025 */

-- Row + 11
	UPDATE BH_DM_ThamDinhQuyetToan
	SET iSTT = iSTT + 11
	where iSTT BETWEEN 204 AND 260 and iNamLamViec IN (2023,2024,2025);
	
	-- Rows + 12
	UPDATE BH_DM_ThamDinhQuyetToan
	SET iSTT = iSTT + 12
	where iSTT BETWEEN 194 AND 202 and iNamLamViec IN (2023,2024,2025);
	
	-- Rows + 13
	UPDATE BH_DM_ThamDinhQuyetToan
	SET iSTT = iSTT + 13
	where iSTT BETWEEN 138 AND 192 and iNamLamViec IN (2023,2024,2025);
	
	-- Rows + 10
	UPDATE BH_DM_ThamDinhQuyetToan
	SET iSTT = iSTT + 10
	where iSTT BETWEEN 38 AND 137 and iNamLamViec IN (2023,2024,2025);
	
	-- Rows + 8
	UPDATE BH_DM_ThamDinhQuyetToan
	SET iSTT = iSTT + 8
	where iSTT BETWEEN 22 AND 37 and iNamLamViec IN (2023,2024,2025);
	
	-- Rows + 5
	UPDATE BH_DM_ThamDinhQuyetToan
	SET iSTT = iSTT + 5
	where iSTT BETWEEN 10 AND 21 and iNamLamViec IN (2023,2024,2025);

--*******INSERT MASTER DATA THAM_DINH_QUYET_TOAN 2023,2024,2025
	INSERT INTO BH_DM_ThamDinhQuyetToan(
					iKieuChu,
					iMa,
					iMaCha,
					iNamLamViec,
					iTrangThai,
					sNguoiSua, 
					sNguoiTao, 
					sNoiDung,
					sSTT,
					iSTT
					)
					VALUES
					--Nam 2023
					(3,261,259,2023,1,'admin','admin',N'Sĩ quan','+',10),
					(3,262,259,2023,1,'admin','admin',N'QNCN','+',11),
					(3,263,6,2023,1,'admin','admin',N'Tùy viên QP','-',12),
					(3,264,263,2023,1,'admin','admin',N'Sĩ quan','+',13),
					(3,265,263,2023,1,'admin','admin',N'QNCN','+',14),
					(3,266,18,2023,1,'admin','admin',N'Tùy viên QP','-',27),
					(3,267,266,2023,1,'admin','admin',N'Sĩ quan','+',28),
					(3,268,266,2023,1,'admin','admin',N'QNCN','+',29),
					(3,269,260,2023,1,'admin','admin',N'Sĩ quan','+',46),
					(3,270,260,2023,1,'admin','admin',N'QNCN','+',47),
					(3,271,132,2023,1,'admin','admin',N'Tạm giam, tạm giữ','-',148),
					(3,272,271,2023,1,'admin','admin',N'Sĩ quan','+',149),
					(3,273,271,2023,1,'admin','admin',N'QNCN','+',150),
					--nam 2024
					(3,261,259,2024,1,'admin','admin',N'Sĩ quan','+',10),
					(3,262,259,2024,1,'admin','admin',N'QNCN','+',11),
					(3,263,6,2024,1,'admin','admin',N'Tùy viên QP','-',12),
					(3,264,263,2024,1,'admin','admin',N'Sĩ quan','+',13),
					(3,265,263,2024,1,'admin','admin',N'QNCN','+',14),
					(3,266,18,2024,1,'admin','admin',N'Tùy viên QP','-',27),
					(3,267,266,2024,1,'admin','admin',N'Sĩ quan','+',28),
					(3,268,266,2024,1,'admin','admin',N'QNCN','+',29),
					(3,269,260,2024,1,'admin','admin',N'Sĩ quan','+',46),
					(3,270,260,2024,1,'admin','admin',N'QNCN','+',47),
					(3,271,132,2024,1,'admin','admin',N'Tạm giam, tạm giữ','-',148),
					(3,272,271,2024,1,'admin','admin',N'Sĩ quan','+',149),
					(3,273,271,2024,1,'admin','admin',N'QNCN','+',150),
	
					--nam 2025
					(3,261,259,2025,1,'admin','admin',N'Sĩ quan','+',10),
					(3,262,259,2025,1,'admin','admin',N'QNCN','+',11),
					(3,263,6,2025,1,'admin','admin',N'Tùy viên QP','-',12),
					(3,264,263,2025,1,'admin','admin',N'Sĩ quan','+',13),
					(3,265,263,2025,1,'admin','admin',N'QNCN','+',14),
					(3,266,18,2025,1,'admin','admin',N'Tùy viên QP','-',27),
					(3,267,266,2025,1,'admin','admin',N'Sĩ quan','+',28),
					(3,268,266,2025,1,'admin','admin',N'QNCN','+',29),
					(3,269,260,2025,1,'admin','admin',N'Sĩ quan','+',46),
					(3,270,260,2025,1,'admin','admin',N'QNCN','+',47),
					(3,271,132,2025,1,'admin','admin',N'Tạm giam, tạm giữ','-',148),
					(3,272,271,2025,1,'admin','admin',N'Sĩ quan','+',149),
					(3,273,271,2025,1,'admin','admin',N'QNCN','+',150)
	
END								
/* END Feature #58032 UPDATE AND INSERT MASTER DATA THAM DINH QUYET TOAN*/

/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 7/4/2024 9:38:21 AM ******/
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
	@isUpdateDanhMucCHTSBHXH int,
	@isUpdateDanhMucNgayNghi int
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
		if (@isUpdateDanhMucNgayNghi = 1)
		begin
			-- COPY DANH MUC NGAY NGHI
				
				DELETE FROM Tl_DM_NgayNghi WHERE iNamLamViec = @destinationYear;
				INSERT INTO Tl_DM_NgayNghi (
					Id,
					dTuNgay,
					dDenNgay,
					sMaNgayNghi,
					sTenNgayNghi,
					iNamLamViec
					)
				select 
					NEWID(),
					DATEADD(YEAR, @destinationYear-@sourceYear, dTuNgay),
					DATEADD(YEAR, @destinationYear-@sourceYear, dDenNgay),
					sMaNgayNghi,
					sTenNgayNghi,
					@destinationYear
				from Tl_DM_NgayNghi
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
/****** Object:  StoredProcedure [dbo].[sp_clone_year_danh_muc_ngay_nghi]    Script Date: 7/4/2024 9:38:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_clone_year_danh_muc_ngay_nghi]
	@source int,
	@dest int
AS
BEGIN
	
	insert into Tl_DM_NgayNghi (
					Id,
					dTuNgay,
					dDenNgay,
					sMaNgayNghi,
					sTenNgayNghi,
					iNamLamViec
		)
	select 
					NEWID(),
					DATEADD(YEAR, @dest-@source, dTuNgay),
					DATEADD(YEAR, @dest-@source, dDenNgay),
					sMaNgayNghi,
					sTenNgayNghi,
					@dest
	from Tl_DM_NgayNghi
	where iNamLamViec = @source and sMaNgayNghi not in (select sMaNgayNghi from Tl_DM_NgayNghi where iNamLamViec = @dest)

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan_chitiet]    Script Date: 7/5/2024 3:48:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_thamdinhquyettoan_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_thamdinhquyettoan_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan_chitiet]    Script Date: 7/5/2024 3:48:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_thamdinhquyettoan_chitiet]
 @IdChungTu uniqueidentifier,
@INamLamViec int ,
@IdDonVi nvarchar(max) 
AS
BEGIN
	
	declare @LoaiDonVi nvarchar(50) = (select iLoai from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi = @IdDonVi);

			-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @INamLamViec 
		AND iTrangThai=1
		AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	SELECT * INTO #dmtdqt
	FROM BH_DM_ThamDinhQuyetToan WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1
	ORDER BY iSTT

	SELECT 
		iID,
		ctct.iID_BH_TDQT_ChungTuChiTiet,
		dmtdqp.iMa,
		dmtdqp.iSTT,
		iMaCha,
		sSTT,
		sNoiDung,
		sXauNoiMa,
		iKieuChu,
		dmtdqp.iTrangThai,
		dmtdqp.iNamLamViec,
		dmtdqp.sNguoiTao, dmtdqp.sNguoiSua,
		CASE WHEN ctct.fSoBaoCao is null or ctct.fSoBaoCao = 0 THEN temp.fSoBaoCao
			 ELSE ctct.fSoBaoCao
		END AS fSoBaoCao,
		ctct.fSoThamDinh,
		CASE WHEN ctct.fQuanNhan is null or ctct.fQuanNhan = 0 THEN trocapnuoicon.fQuanNhan
			ELSE ctct.fQuanNhan 
		END AS fQuanNhan,
		CASE WHEN ctct.fCNVLDHD is null or ctct.fCNVLDHD = 0 THEN trocapnuoicon.fCNVLDHD
			ELSE ctct.fCNVLDHD 
		END AS fCNVLDHD
		
	INTO #dmtdqtResult
	FROM #dmtdqt dmtdqp
	LEFT JOIN
	(SELECT 257 iMa, (ISNULL(SUM(ctct.fTienSQ_ThucChi), 0) + ISNULL(SUM(ctct.fTienQNCN_ThucChi), 0)) fQuanNhan,
		(ISNULL(SUM(ctct.fTienCNVCQP_ThucChi), 0) + ISNULL(SUM(ctct.fTienLDHD_ThucChi), 0)) fCNVLDHD
		FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
		LEFT JOIN BH_QTC_Nam_CheDoBHXH ct ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
		WHERE (ctct.sXauNoiMa LIKE '9010001-010-011-0002-0001-0001-00%' OR ctct.sXauNoiMa LIKE '9010002-010-011-0002-0001-0001-00%')
		AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ct.iNamLamViec = @INamLamViec
		AND ct.bIsKhoa = 1) trocapnuoicon ON dmtdqp.iMa = trocapnuoicon.iMa
	LEFT JOIN
	(
	SELECT 7 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2


	UNION

	SELECT 8 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION
	--phu nhan phu quan khoi du toan
	--SELECT 259 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	--LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	--ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa = '9020001-010-011-0001-0003'
	--AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ct.iNamLamViec = @INamLamViec
	--AND ct.bIsKhoa = 1
	SELECT 259 iMa, 0 as fSoBaoCao

	UNION

	SELECT 9 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 10 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 12 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 13 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 14 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 15 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 19 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 20 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 21 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2


	UNION

	SELECT 22 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 24 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 25 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 26 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 27 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 29 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 30 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 34 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 35 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2


UNION
 --phu nhan phu quan khoi hach toan
	--SELECT 260 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	--LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	--ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa = '9020002-010-011-0001-0003'
	--AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ct.iNamLamViec = @INamLamViec
	--AND ct.bIsKhoa = 1
	SELECT 260 iMa, 0 as fSoBaoCao

	UNION

	SELECT 36 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 37 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 39 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 40 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 41 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 42 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 46 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 47 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 48 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 49 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 51 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 52 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 53 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 54 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 56 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 57 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 70 iMa, SUM(ctct.fThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
--	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020001-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 71 iMa, SUM(ctct.fTruyThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020001-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	--AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iID_MaDonVi = '002'
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 73 iMa, SUM(ctct.fThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020001-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 74 iMa, SUM(ctct.fTruyThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020001-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 77 iMa, SUM(ctct.fThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020002-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 78 iMa, SUM(ctct.fTruyThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020002-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 80 iMa, SUM(ctct.fThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020002-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 81 iMa, SUM(ctct.fTruyThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020002-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2
	
	UNION

	SELECT 95 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 96 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 98 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 99 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 102 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 103 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 105 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 106 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 110 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 111 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 113 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 114 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 17 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 118 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 120 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 121 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 133 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 134 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 135 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 136 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 138 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 139 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 140 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 141 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 151 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa LIKE '9030001-010-011-0001%' OR sXauNoiMa LIKE '9030001-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 155 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa LIKE '9030002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 159 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 163 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 167 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030006'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 171 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030004'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 178 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 179 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 180 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	--WHERE (ctct.sXauNoiMa LIKE '9010001%' OR ctct.sXauNoiMa LIKE '9010002%')
	WHERE (ctct.sXauNoiMa LIKE '901%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 183 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 184 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 185 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 186 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 187 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 188 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 189 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 190 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0008%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 193 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 194 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 195 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 196 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 197 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 198 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 199 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 200 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0008%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 183 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	--UNION

	--SELECT 207 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(fTien_ThucChi), 0) FROM BH_QTC_Nam_KPK_ChiTiet WHERE sXauNoiMa like '9050001-010%' AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND iNamLamViec = @INamLamViec - 1) fSoBaoCao 
	--FROM BH_CP_ChungTu_ChiTiet ctct
	--LEFT JOIN BH_CP_ChungTu ct 
	--ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	--WHERE sXauNoiMa like '9050001%'
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ct.iNamChungTu = @INamLamViec - 1
	--AND ct.bIsKhoa = 1
	
	UNION

	SELECT 208 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 209 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	--UNION

	--SELECT 213 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(fTien_ThucChi), 0) FROM BH_QTC_Nam_KPK_ChiTiet WHERE sXauNoiMa LIKE '9050002-010%' AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND iNamLamViec = @INamLamViec - 1) fSoBaoCao 
	--FROM BH_CP_ChungTu_ChiTiet ctct
	--LEFT JOIN BH_CP_ChungTu ct 
	--ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	--WHERE sXauNoiMa LIKE '9050002%'
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ct.iNamChungTu = @INamLamViec - 1
	--AND ct.bIsKhoa = 1
	
	UNION

	SELECT 214 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 215 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa like '9050002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 218 iMa, 0.1 * (ISNULL(SUM(ctct.fBHYT_NLD), 0) + ISNULL(SUM(ctct.fBHYT_NSD), 0)) fSoBaoCao 
	FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	LEFT JOIN  BH_DTT_BHXH_PhanBo_ChungTu ct 
	ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	WHERE (ctct.sXauNoiMa like '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ct.iNamLamViec = @INamLamViec
		AND ct.bKhoa = 1

	UNION

	--SELECT 220 iMa, 0.1 * (ISNULL(SUM(ctct.fBHYT_NLD), 0) + ISNULL(SUM(ctct.fBHYT_NSD), 0) - (SELECT ISNULL(SUM(ctct.fThu_BHYT_NLD), 0) + ISNULL(SUM(ctct.fThu_BHYT_NSD), 0)
	--	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	--	JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%') 
	--		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
	--		AND ctct.iNamLamViec = @INamLamViec - 1
	--		AND ct.iQuyNam = @INamLamViec - 1
	--		AND ct.bIsKhoa = 1)) fSoBaoCao
	--FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	--JOIN BH_DTT_BHXH_PhanBo_ChungTu ct on ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	--WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ctct.iNamLamViec = @INamLamViec - 1
	--AND ct.bKhoa = 1

	--UNION

	SELECT 221 iMa, (ISNULL(SUM(ctct.fThu_BHYT_NLD), 0) + ISNULL(SUM(ctct.fThu_BHYT_NSD), 0)) * 0.1 fSoBaoCao
	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	JOIN BH_QTT_BHXH_ChungTu ct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa LIKE '9020002-010-011-0001%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 222 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010004%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 223 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KCB_QuanYDonVi ct 
	ON ct.ID_QTC_Nam_KCB_QuanYDonVi = ctct.iID_QTC_Nam_KCB_QuanYDonVi
	WHERE ctct.sXauNoiMa LIKE '9010004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 228 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct on ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010009-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE ctct.sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 229 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 230 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 231 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010009-010%' 
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 235 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct on ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010006-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN  BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 236 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 237 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 238 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 242 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010010%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 243 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010010-010%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 247 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct on ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 and sM is not null and sM <> '' AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 248 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 250 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct on ID_QTC_Nam_KinhPhiQuanLy = ID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 251 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 252 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KinhPhiQuanLy ct 
	ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 261 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0003-0001'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 262 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0003-0002'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 263 iMa, 0 fSoBaoCao 

	UNION

	SELECT 264 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0004-0001'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 265 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0004-0002'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 266 iMa, 0 fSoBaoCao 

	UNION

	SELECT 267 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0004-0001'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 268 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0004-0002'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 269 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020002-010-011-0001-0003-0001'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 270 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020002-010-011-0001-0003-0002'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 271 iMa, 0 fSoBaoCao 

	UNION

	SELECT 272 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0005-0001'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 273 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0005-0002'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	) AS temp ON temp.iMa = dmtdqp.iMa
	
	LEFT JOIN BH_ThamDinhQuyetToan_ChungTuChiTiet ctct ON dmtdqp.iMa = ctct.iMa 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec
	LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ctct.iID_BH_TDQT_ChungTu = ct.iID_BH_TDQT_ChungTu 
	AND ct.iID_BH_TDQT_ChungTu = @IdChungTu AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ct.iNamLamViec = @INamLamViec
	LEFT JOIN (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi

	--ORDER BY iMa

	--Lấy dữ liệu chứng từ đơn vị cha
	SELECT dvc.iMa, dvc.fSoBaoCao INTO #dmtdqtCha FROM
	(
	SELECT 178 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 179 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010002%'
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ctct.iNamLamViec = @INamLamViec
		AND ct.bIsKhoa = 1

	UNION

	SELECT 218 iMa, 0.1 * (ISNULL(SUM(ctct.fThu_BHYT_NLD), 0) + ISNULL(SUM(ctct.fThu_BHYT_NSD), 0)) fSoBaoCao 
	FROM BH_DTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN  BH_DTT_BHXH_ChungTu ct 
	ON ct.iID_DTT_BHXH = ctct.iID_DTT_BHXH
	WHERE (ctct.sXauNoiMa like '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ct.iNamLamViec = @INamLamViec
		AND ct.bIsKhoa = 1

	UNION

	--SELECT 220 iMa, 0.1 * (ISNULL(SUM(fThu_BHYT_NLD + fThu_BHYT_NSD), 0) - (SELECT ISNULL(SUM(ctct.fThu_BHYT_NLD + ctct.fThu_BHYT_NSD), 0) 
	--	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	--	JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%') 
	--		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
	--		AND ctct.iNamLamViec = @INamLamViec - 1
	--		AND ct.iQuyNam = @INamLamViec - 1
	--		AND ct.bIsKhoa = 1)) fSoBaoCao
	--FROM BH_DTT_BHXH_ChungTu_ChiTiet ctct
	--JOIN BH_DTT_BHXH_ChungTu ct ON ct.iID_DTT_BHXH = ctct.iID_DTT_BHXH
	--WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ctct.iNamLamViec = @INamLamViec - 1
	--AND ct.bIsKhoa = 1 

	--UNION

	SELECT 228 iMa, ISNULL(SUM(fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010009-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 229 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	LEFT JOIN BH_DTC_DuToanChiTrenGiao ct 
	ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE 
	sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 235 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010006-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 236 iMa, SUM(ctct.fTienTuChi) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 247 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 and sM is not null and sM <> '' AND ct.BIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 248 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	) dvc

	IF @LoaiDonVi = 0
		UPDATE dmtdqtResult
		SET dmtdqtResult.fSoBaoCao = dmtdqtCha.fSoBaoCao
		FROM #dmtdqtResult AS dmtdqtResult
		JOIN #dmtdqtCha AS dmtdqtCha ON dmtdqtResult.iMa = dmtdqtCha.iMa;

	--Result
	SELECT #dmtdqtResult.*,
	CASE WHEN #dmtdqtResult.iKieuChu <> 1 THEN #tblDonVi.iID_MaDonVi  ELSE '' END as iID_MaDonVi,
	CASE WHEN #dmtdqtResult.iKieuChu <> 1 THEN #tblDonVi.sTenDonVi  ELSE '' END as sTenDonVi
	FROM #dmtdqtResult,#tblDonVi
	ORDER BY iSTT;

	DROP TABLE #dmtdqt;
	DROP TABLE #dmtdqtCha;
	DROP TABLE #dmtdqtResult;
	DROP TABLE #tblDonVi;
END
;
;
;
;
;
;
;
GO
