/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]    Script Date: 21/07/2022 4:26:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_muclucDanhMuc]    Script Date: 21/07/2022 4:26:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_muclucDanhMuc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_muclucDanhMuc]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlskt]    Script Date: 21/07/2022 4:26:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlskt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlskt]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlcauhinh]    Script Date: 21/07/2022 4:26:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlcauhinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlcauhinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data]    Script Date: 21/07/2022 4:26:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_data]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_data]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_nguonvon]    Script Date: 21/07/2022 4:28:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_nguonvon]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_nguonvon]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_nguonvon]    Script Date: 21/07/2022 4:28:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_nh_dutoan_nguonvon]
@iIdDuToanId uniqueidentifier
AS
BEGIN
	SELECT tbl.iID_NguonVonID as IIDNguonVonID, nv.sTen as STenNguonVon, NULL as IIDGoiThauID,
		ISNULL(tbl.FGiaTriNgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac, ISNULL(tbl.FGiaTriUSD, 0) as FGiaTriUSD, ISNULL(tbl.FGiaTriVND, 0) as FGiaTriVND, ISNULL(tbl.FGiaTriEUR, 0) as FGiaTriEUR,
		CAST(0 as float) as FGiaTriPheDuyetUSD,
		CAST(0 as float) as FGiaTriPheDuyetVND,
		CAST(0 as float) as FGiaTriPheDuyetEUR,
		tbl.ID AS Id
	FROM NH_DA_DuToan_NguonVon as tbl
	INNER JOIN NguonNganSach as nv on tbl.iID_NguonVonID = nv.iID_MaNguonNganSach
	WHERE tbl.iID_DuToanID = @iIdDuToanId
END
GO

/****** Object:  StoredProcedure [dbo].[sp_clone_data]    Script Date: 21/07/2022 4:26:55 PM ******/
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
	@isUpdatedNSPB int,
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
				,null
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

	if (@isUpdatedNSPB = 1)
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
		 SELECT [Ma_PhuCap]
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
			   ,GETDATE()
			   ,@userCreator
			   ,null
			   ,null
			   ,[iTrangThai]
			   ,[idPhuCap]
			   ,[idCachTinhLuong]
			   ,[idNguonNganSach]
			   ,[idMlns]
			   ,[Ma_Cb]
			   ,[ChiTietToi]
			   ,@destinationYear FROM [dbo].[TL_PhuCap_MLNS] where nam = @sourceYear;
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

	if(@isUpdateNSSKT = 1)
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

	if(@isUpdateCauHinhHeThong = 1)
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
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlcauhinh]    Script Date: 21/07/2022 4:26:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_clone_year_mlcauhinh]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
begin
	insert into DanhMuc
	([iID_DanhMuc]
      ,[dNgaySua]
      ,[dNgayTao]
      ,[iID_MaDanhMuc]
      ,[iNamLamViec]
      ,[iThuTu]
      ,[iTrangThai]
      ,[Log]
      ,[NganSachNganh]
      ,[sGiaTri]
      ,[sMoTa]
      ,[sNguoiSua]
      ,[sNguoiTao]
      ,[sTen]
      ,[sType]
      ,[Tag]
	)
	select
		[iID_DanhMuc]
      ,null
      ,GETDATE()
      ,[iID_MaDanhMuc]
      ,@dest
      ,[iThuTu]
      ,[iTrangThai]
      ,[Log]
      ,[NganSachNganh]
      ,[sGiaTri]
      ,[sMoTa]
      ,[sNguoiSua]
      ,[sNguoiTao]
      ,[sTen]
      ,[sType]
      ,[Tag]
	  from DanhMuc b where b.iNamLamViec = @source and b.sType='DM_CauHinh' and (b.iID_MaDanhMuc not in (select iID_MaDanhMuc from DanhMuc c where c.iNamLamViec = @dest))

	 --UPDATE
	update d
	set
		d.dNgaySua = getdate(),
		d.iThuTu = s.iThuTu,
		d.iTrangThai = s.iTrangThai,
		d.[Log] = s.[Log],
		d.NganSachNganh = s.NganSachNganh,
		d.sGiaTri = s.sGiaTri,
		d.sMoTa = s.sMoTa,
		d.sNguoiSua = @userCreate,
		d.sType = 'DM_CauHinh',
		d.[Tag] = s.[Tag]

	from
	DanhMuc as s
	inner join DanhMuc as d
	on s.iID_MaDanhMuc = d.iID_MaDanhMuc
	where s.iNamLamViec = @source and d.iNamLamViec = @dest and d.sType = 'DM_CauHinh' and s.sType = 'DM_CauHinh'

end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlskt]    Script Date: 21/07/2022 4:26:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_clone_year_mlskt]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
begin
	insert into NS_SKT_MucLuc
	([iID]
      ,[bHangCha]
      ,[dNgaySua]
      ,[dNgayTao]
      ,[dNguoiSua]
      ,[dNguoiTao]
      ,[iID_MLSKT]
      ,[iID_MLSKTCha]
      ,[iNamLamViec]
      ,[iTrangThai]
      ,[KyHieuCha]
      ,[Log]
      ,[Muc]
      ,[sKyHieu]
      ,[sLoaiNhap]
      ,[sM]
      ,[sMoTa]
      ,[sNG_Cha]
      ,[sNG]
      ,[sSTT]
      ,[sSTTBC]
      ,[Tag]
	)
	select
		newid()
      ,[bHangCha]
      ,[dNgaySua]
      ,[dNgayTao]
      ,[dNguoiSua]
      ,@userCreate
      ,[iID_MLSKT]
      ,[iID_MLSKTCha]
      ,@dest
      ,[iTrangThai]
      ,[KyHieuCha]
      ,[Log]
      ,[Muc]
      ,[sKyHieu]
      ,[sLoaiNhap]
      ,[sM]
      ,[sMoTa]
      ,[sNG_Cha]
      ,[sNG]
      ,[sSTT]
      ,[sSTTBC]
      ,[Tag]
	  from NS_SKT_MucLuc b where b.iNamLamViec = @source and (b.sKyHieu not in (select sKyHieu from NS_SKT_MucLuc c where c.iNamLamViec = @dest))

	update d
	set
		d.dNgaySua = getdate(),
		d.bHangCha = s.bHangCha,
		d.dNguoiSua = @userCreate,
		d.sM = s.sM,
		d.sMoTa = case when (d.sMoTa is null or d.sMoTa = '') then s.sMoTa else d.sMoTa end,
		d.sNG_Cha = s.sNG_Cha,
		d.sNG = s.sNG,
		d.sSTT = s.sSTT,
		d.sSTTBC = s.sSTTBC,
		d.[Tag] = s.[Tag],
		d.iID_MLSKT = s.iID_MLSKT
	from
	NS_SKT_MucLuc as s
	inner join NS_SKT_MucLuc as d
	on s.sKyHieu = d.sKyHieu
	where s.iNamLamViec = @source and d.iNamLamViec = @dest

	-- cap nhat parent
	update NS_SKT_MucLuc
	set
		iID_MLSKTCha = dbo.f_findParentMucLucSkt(@dest,sKyHieu)
	where iNamLamViec = @dest
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_muclucDanhMuc]    Script Date: 21/07/2022 4:26:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_clone_year_muclucDanhMuc]
	@source int,
	@dest int,
	@userCreate nvarchar(200),
	@type nvarchar(200),
	@isCopyGiaTri int
as
begin
	insert into DanhMuc
	([iID_DanhMuc]
      ,[dNgaySua]
      ,[dNgayTao]
      ,[iID_MaDanhMuc]
      ,[iNamLamViec]
      ,[iThuTu]
      ,[iTrangThai]
      ,[Log]
      ,[NganSachNganh]
      ,[sGiaTri]
      ,[sMoTa]
      ,[sNguoiSua]
      ,[sNguoiTao]
      ,[sTen]
      ,[sType]
      ,[Tag]
	)
	select
		newid()
      ,null
      ,GETDATE()
      ,[iID_MaDanhMuc]
      ,@dest
      ,[iThuTu]
      ,[iTrangThai]
      ,[Log]
      ,[NganSachNganh]
      ,case when(@isCopyGiaTri = 1) then b.[sGiaTri] else '' end
      ,[sMoTa]
      ,[sNguoiSua]
      ,[sNguoiTao]
      ,[sTen]
      ,[sType]
      ,[Tag]
	  from DanhMuc b where b.iNamLamViec = @source and b.sType= @type and (b.iID_MaDanhMuc not in (select iID_MaDanhMuc from DanhMuc c where c.iNamLamViec = @dest))

	 --UPDATE
	update d
	set
		d.dNgaySua = getdate(),
		d.iThuTu = s.iThuTu,
		d.iTrangThai = s.iTrangThai,
		d.[Log] = s.[Log],
		d.NganSachNganh = s.NganSachNganh,
		--d.sGiaTri = s.sGiaTri,
		d.sMoTa = s.sMoTa,
		d.sNguoiSua = @userCreate,
		d.sType = @type,
		d.[Tag] = s.[Tag]

	from
	DanhMuc as s
	inner join DanhMuc as d
	on s.iID_MaDanhMuc = d.iID_MaDanhMuc
	where s.iNamLamViec = @source and d.iNamLamViec = @dest and d.sType = @type and s.sType = @type

end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]    Script Date: 21/07/2022 4:26:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]
	@YearOfWork int,
	@YearOfBudget nvarchar(20),
	@ListLNS nvarchar(200),
	@ListUnitId nvarchar(200),
	@DataType int,
	@BudgetSource int,
	@dvt int
as
begin

--Dự toán năm trước chuyển sang
select 
	a.iID_MLNS,
	a.iID_MLNS_Cha as MLNS_Id_Parent,
	a.sLNS as LNS,
	a.sL as L,
	a.sK as K,
	a.sM as M,
	a.sTM as TM,
	a.sTTM as TTM,
	a.sNG as NG,
	a.sTNG1 as TNG1,
	a.sTNG2 as TNG2,
	a.sTNG3 as TNG3,
	a.sXauNoiMa as XauNoiMa,
	a.sMoTa as MoTa,
	cast (0 as bit) as IsHangCha,
	-- dự toán năm trước chuyển sang
	case when (@DataType = 1 and a.iNamNganSach in (1,4)) then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt
		when (@DataType = 2 and a.iNamNganSach in (1,4)) then a.fHienVat / @dvt else 0 end as DuToanNamTruocChuyenSang,
	-- dự toán tổng số
	case when (@DataType = 1 and a.iNamNganSach = 2 and ct.iLoaiDuToan in (1,3,4,5)) then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt 
		when (@DataType = 2 and a.iNamNganSach = 2 and ct.iLoaiDuToan in (1,3,4,5)) then fHienVat / @dvt else 0 end as DuToanTongSo,
	-- dự toán bổ sung sau
	case when (@DataType = 1 and a.iNamNganSach = 2 and ct.iLoaiDuToan = 5) then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt 
		when (@DataType = 2 and a.iNamNganSach = 2 and ct.iLoaiDuToan = 5) then fHienVat / @dvt else 0 end as DuToanBoSungSau,

	0 as SoDeNghiQuyetToanNam,
	0 as DuToanChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi,
	a.iNamNganSach as NamNganSach
	from NS_DT_ChungTuChiTiet a
	inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi 
	inner join NS_DT_ChungTu ct on ct.iID_DTChungTu = a.iID_DTChungTu
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))

-- số đề nghị quyết toán năm & số chuyển năm sau
	union all

	select
		a.iID_MLNS,
		a.iID_MLNS_Cha as MLNS_Id_Parent,
		a.sLNS as LNS,
		a.sL as L,
		a.sK as K,
		a.sM as M,
		a.sTM as TM,
		a.sTTM as TTM,
		a.sNG as NG,
		a.sTNG1 as TNG1,
		a.sTNG2 as TNG2,
		a.sTNG3 as TNG3,
		a.sXauNoiMa as XauNoiMa,
		a.sMoTa as MoTa,
		cast (0 as bit) as IsHangCha,
		0 as DuToanNamTruocChuyenSang,
		0 as DuToanTongSo,
		0 as DuToanBoSungSau,
		-- thiếu cột fHienVat trong bảng NS_QT_ChungTuChiTiet
		case when @DataType = 1 then a.fTuChi_PheDuyet when @DataType = 2 then 0 end as SoDeNghiQuyetToanNam,
		(ISNULL(fChuyenNamSau_ChuaCap,0) + ISNULL(fChuyenNamSau_DaCap,0)) / @dvt as DuToanChuyenNamSau,
		a.iID_MaDonVi as IdDonVi,
		b.sTenDonVi as TenDonVi,
		a.iNamNganSach as NamNganSach
	from NS_QT_ChungTuChiTiet a
		inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach in (select * from f_split(@YearOfBudget))
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
end
GO
