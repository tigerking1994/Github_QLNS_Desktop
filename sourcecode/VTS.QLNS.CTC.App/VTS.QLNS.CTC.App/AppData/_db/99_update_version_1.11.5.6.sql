/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 09/08/2022 8:46:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thongtri_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlns]    Script Date: 09/08/2022 8:46:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data]    Script Date: 09/08/2022 8:46:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_data]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_data]
GO
/****** Object:  UserDefinedFunction [dbo].[f_findParentMucLucNganSach]    Script Date: 09/08/2022 8:46:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_findParentMucLucNganSach]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_findParentMucLucNganSach]
GO
/****** Object:  UserDefinedFunction [dbo].[f_findParentMucLucNganSach]    Script Date: 09/08/2022 8:46:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[f_findParentMucLucNganSach] 
	( @dest int, @xauNoiMa nvarchar(100))
returns uniqueidentifier
as
begin
	declare @result nvarchar(100);
	select top 1 @result = iID_MLNS from dbo.NS_MucLucNganSach ns where ns.iNamLamViec = @dest and (@xauNoiMa like ns.sXauNoiMa + '-%') order by LEN(ns.sXauNoiMa) desc;
	return @result
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data]    Script Date: 09/08/2022 8:46:45 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlns]    Script Date: 09/08/2022 8:46:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_clone_year_mlns]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
begin

select 
		   newid() as iID
		  ,[bDuPhong]
		  ,[bHangCha]
		  ,[bHangChaDuToan]
		  ,[bHangChaQuyetToan]
		  ,[bHangMua]
		  ,[bHangNhap]
		  ,[bHienVat]
		  ,[bNgay]
		  ,[bPhanCap]
		  ,[bSoNguoi]
		  ,[bTonKho]
		  ,[bTuChi]
		  ,[sChiTietToi]
		  ,[dNgaySua]
		  ,getdate() as dNgayTao
		  ,[iLoai]
		  ,[iLock]
		  ,[iTrangThai]
		  ,[iID_MaDonVi]
		  ,[iID_MaBQuanLy]
		  ,[sK]
		  ,[sL]
		  ,[sLNS]
		  ,[Log]
		  ,[sM]
		  ,[iID_MLNS]
		  ,null as iID_MLNS_Cha
		  ,[sMoTa]
		  ,@dest as  iNamLamViec
		  ,[sNG]
		  ,null as  sCPChiTietToi
		  ,null as sDuToanChiTietToi
		  ,[sNguoiSua]
		  ,@userCreate  as sNguoiTao
		  ,[sNhapTheoTruong]
		  ,null as sQuyetToanChiTietToi
		  ,[Tag]
		  ,[sTM]
		  ,[sTNG]
		  ,[sTNG1]
		  ,[sTNG2]
		  ,[sTNG3]
		  ,[sTTM]
		  ,[sXauNoiMa] 

into #nsmuclucngansachtem  		  
		  
from NS_MucLucNganSach c 
where c.iNamLamViec= @source and c.sXauNoiMa not in (select b.sXauNoiMa from NS_MucLucNganSach b where b.iNamLamViec = @dest)



insert into NS_MucLucNganSach 
		  ([iID]
		  ,[bDuPhong]
		  ,[bHangCha]
		  ,[bHangChaDuToan]
		  ,[bHangChaQuyetToan]
		  ,[bHangMua]
		  ,[bHangNhap]
		  ,[bHienVat]
		  ,[bNgay]
		  ,[bPhanCap]
		  ,[bSoNguoi]
		  ,[bTonKho]
		  ,[bTuChi]
		  ,[sChiTietToi]
		  ,[dNgaySua]
		  ,[dNgayTao]
		  ,[iLoai]
		  ,[iLock]
		  ,[iTrangThai]
		  ,[iID_MaDonVi]
		  ,[iID_MaBQuanLy]
		  ,[sK]
		  ,[sL]
		  ,[sLNS]
		  ,[Log]
		  ,[sM]
		  ,[iID_MLNS]
		  ,[iID_MLNS_Cha]
		  ,[sMoTa]
		  ,[iNamLamViec]
		  ,[sNG]
		  ,[sCPChiTietToi]
		  ,[sDuToanChiTietToi]
		  ,[sNguoiSua]
		  ,[sNguoiTao]
		  ,[sNhapTheoTruong]
		  ,[sQuyetToanChiTietToi]
		  ,[Tag]
		  ,[sTM]
		  ,[sTNG]
		  ,[sTNG1]
		  ,[sTNG2]
		  ,[sTNG3]
		  ,[sTTM]
		  ,[sXauNoiMa])
		
select * from #nsmuclucngansachtem

	-- Cap nhat lai gia tri
	update d
	set
		d.[sMoTa] = s.sMoTa,
		d.[sDuToanChiTietToi] = case when (d.sChiTietToi is null or d.sChiTietToi = '') then s.sDuToanChiTietToi else d.sDuToanChiTietToi end,
		d.[sQuyetToanChiTietToi] = case when (d.sQuyetToanChiTietToi is null or d.sQuyetToanChiTietToi = '') then s.sQuyetToanChiTietToi else d.sQuyetToanChiTietToi end,
		d.[sCPChiTietToi] = case when(d.sCPChiTietToi is null or d.sCPChiTietToi = '') then s.sCPChiTietToi else s.sCPChiTietToi end
	from
	NS_MucLucNganSach as s
	inner join NS_MucLucNganSach as d
	on s.iID_MLNS = d.iID_MLNS
	where s.iNamLamViec = @source and d.iNamLamViec = @dest
	-- cap nhat parent
	
select 
		   NS_MucLucNganSach.[iID]
		  ,NS_MucLucNganSach.[bDuPhong]
		  ,NS_MucLucNganSach.[bHangCha]
		  ,NS_MucLucNganSach.[bHangChaDuToan]
		  ,NS_MucLucNganSach.[bHangChaQuyetToan]
		  ,NS_MucLucNganSach.[bHangMua]
		  ,NS_MucLucNganSach.[bHangNhap]
		  ,NS_MucLucNganSach.[bHienVat]
		  ,NS_MucLucNganSach.[bNgay]
		  ,NS_MucLucNganSach.[bPhanCap]
		  ,NS_MucLucNganSach.[bSoNguoi]
		  ,NS_MucLucNganSach.[bTonKho]
		  ,NS_MucLucNganSach.[bTuChi]
		  ,NS_MucLucNganSach.[sChiTietToi]
		  ,NS_MucLucNganSach.[dNgaySua]
		  ,NS_MucLucNganSach.dNgayTao
		  ,NS_MucLucNganSach.[iLoai]
		  ,NS_MucLucNganSach.[iLock]
		  ,NS_MucLucNganSach.[iTrangThai]
		  ,NS_MucLucNganSach.[iID_MaDonVi]
		  ,NS_MucLucNganSach.[iID_MaBQuanLy]
		  ,NS_MucLucNganSach.[sK]
		  ,NS_MucLucNganSach.[sL]
		  ,NS_MucLucNganSach.[sLNS]
		  ,NS_MucLucNganSach.[Log]
		  ,NS_MucLucNganSach.[sM]
		  ,NS_MucLucNganSach.[iID_MLNS]
		  ,NS_MucLucNganSach.iID_MLNS_Cha
		  ,NS_MucLucNganSach.[sMoTa]
		  ,NS_MucLucNganSach.iNamLamViec
		  ,NS_MucLucNganSach.[sNG]
		  ,NS_MucLucNganSach.sCPChiTietToi
		  ,NS_MucLucNganSach.sDuToanChiTietToi
		  ,NS_MucLucNganSach.[sNguoiSua]
		  ,NS_MucLucNganSach.sNguoiTao
		  ,NS_MucLucNganSach.[sNhapTheoTruong]
		  ,NS_MucLucNganSach.sQuyetToanChiTietToi
		  ,NS_MucLucNganSach.[Tag]
		  ,NS_MucLucNganSach.[sTM]
		  ,NS_MucLucNganSach.[sTNG]
		  ,NS_MucLucNganSach.[sTNG1]
		  ,NS_MucLucNganSach.[sTNG2]
		  ,NS_MucLucNganSach.[sTNG3]
		  ,NS_MucLucNganSach.[sTTM]
		  ,NS_MucLucNganSach.[sXauNoiMa] 
into #nsmuclucngansachtem_Update  	
from  NS_MucLucNganSach, #nsmuclucngansachtem  
where   NS_MucLucNganSach.iNamLamViec = @dest  and  NS_MucLucNganSach.sXauNoiMa like CONCAT (#nsmuclucngansachtem.sxaunoima , '-%') 
order by NS_MucLucNganSach.sXauNoiMa;

insert into   #nsmuclucngansachtem_update
select * from #nsmuclucngansachtem;




	
	update NS_MucLucNganSach 
	set
	iID_MLNS_Cha = dbo.f_findParentMucLucNganSach(@dest,sXauNoiMa)
	where  iNamLamViec = @dest  and  iID in ( select iID from   #nsmuclucngansachtem_update ) 
	
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 09/08/2022 8:46:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
	@YearOfWork int,
	@BudgetSource int,
	@AgencyId nvarchar(100),
	@QuarterMonth nvarchar(100),
	@LNS nvarchar(max),
	@Dvt int,
	@IsInTongHop bit, 
	@IKhoi int
AS
BEGIN
declare @strChungTu nvarchar (500)
set @strChungTu=  (select sTongHop + ',' as 'data()' from NS_QT_ChungTu where  iID_MaDonVi in ( SELECT * FROM f_split(@AgencyId))  FOR XML PATH(''));
	if (@IsInTongHop = 0 OR @strChungTu = '')
		SELECT *
		FROM
		  (SELECT iID_MaDonVi,
				  TuChi = SUM(fTuChi_PheDuyet) / @Dvt,
				  cast(0 AS float) AS HienVat,
				  SoNguoi = SUM(fSoNguoi),
				  SoNgay = SUM(fSoNgay),
				  SoLuot = SUM(fSoLuot)
		   FROM NS_QT_ChungTuChiTiet
		   WHERE iNamLamViec = @YearOfWork
			 AND iID_MaNguonNganSach = @BudgetSource
			 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
			 AND (@QuarterMonth IS NULL OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
			 AND (@LNS IS NULL  OR sLNS in (SELECT *  FROM f_split(@LNS)))
		   GROUP BY iID_MaDonVi)AS ct 
		-- lay ten don vi
		LEFT JOIN
		  (SELECT sTenDonVi,
				  iID_MaDonVi AS dv_id
		   FROM DonVi
		   WHERE iTrangThai = 1
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
		ORDER BY iID_MaDonVi
	else 

	SELECT *
		FROM
		  (SELECT ctct.iID_MaDonVi,
				  TuChi = SUM(fTuChi_PheDuyet) / @Dvt,
				  cast(0 AS float) AS HienVat,
				  SoNguoi = SUM(fSoNguoi),
				  SoNgay = SUM(fSoNgay),
				  SoLuot = SUM(fSoLuot)
		   FROM NS_QT_ChungTuChiTiet ctct inner join ns_qt_Chungtu ct on  ctct.iID_QTChungTu =  ct.iID_QTChungTu 
		   WHERE ctct.iNamLamViec = @YearOfWork
			 AND ctct.iID_MaNguonNganSach = @BudgetSource
			 --AND (@AgencyId IS NULL OR ctct.iID_MaDonVi in  (select DonVi.iID_MaDonVi from DonVi  where DonVi.iID_Parent in ( SELECT * FROM f_split(@AgencyId))))
			 AND ct.bdatonghop = 1
			 AND (@QuarterMonth IS NULL OR ctct.iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
			 AND (@LNS IS NULL  OR sLNS in (SELECT *  FROM f_split(@LNS)))
			 AND ct.sSoChungTu in (select * from f_split(@strChungTu))
		   GROUP BY ctct.iID_MaDonVi)AS ct 
		-- lay ten don vi
		LEFT JOIN
		  (SELECT sTenDonVi,
				  iID_MaDonVi AS dv_id
		   FROM DonVi
		   WHERE iTrangThai = 1
		     AND (@IKhoi = -1 OR iKhoi = @IKhoi)
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
		ORDER BY iID_MaDonVi
END
;
;

GO


/****** Object:  Index [IX_NS_MucLucNganSach_iNamLamViec_sXauNoiMa]    Script Date: 08/08/2022 6:10:03 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_NS_MucLucNganSach_iNamLamViec_sXauNoiMa] ON [dbo].[NS_MucLucNganSach]
(
        [iNamLamViec] ASC,
        [sXauNoiMa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO