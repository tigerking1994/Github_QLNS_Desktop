/****** Object:  StoredProcedure [dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich_by_id]    Script Date: 28/06/2023 3:22:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich_by_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich_by_id]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich]    Script Date: 28/06/2023 3:22:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_index_find_all]    Script Date: 28/06/2023 3:22:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_kehoach5nam_index_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_kehoach5nam_index_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_dexuat_index_find_all]    Script Date: 28/06/2023 3:22:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_kehoach5nam_dexuat_index_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_kehoach5nam_dexuat_index_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_denghithanhtoan_index_2]    Script Date: 28/06/2023 3:22:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_denghithanhtoan_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_denghithanhtoan_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 28/06/2023 3:22:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_data_update_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_data_update_new]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_tong_hop_dieuchinh]    Script Date: 28/06/2023 3:22:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_du_toan_chi_tieu_tong_hop_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_du_toan_chi_tieu_tong_hop_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_tong_hop_dieuchinh]    Script Date: 28/06/2023 3:22:12 PM ******/
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
		select 
					ISNULL(ctctpb.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
			ISNULL(ctctpb.iID_DTChungTu, CAST(0x0 AS uniqueidentifier)) AS iID_DTChungTu,
		isnull(ctctpb.iNamNganSach, ctctdc.iNamNganSach) as iNamNganSach,
		isnull(ctctpb.iID_MaNguonNganSach, ctctdc.iID_MaNguonNganSach) as iID_MaNguonNganSach,
		isnull(ctctpb.iNamLamViec, ctctdc.iNamLamViec) as iNamLamViec,
		isnull(ctctpb.sXauNoiMa, ctctdc.sXauNoiMa) as sXauNoiMa,
		isnull(ctctpb.iID_MaDonVi, ctctdc.iID_MaDonVi) as iID_MaDonVi,
		isnull(ctctpb.iPhanCap, 0) + isnull(ctctdc.fPhanCap, 0) as iPhanCap,
		isnull(ctctpb.sGhiChu, '') AS sGhiChu,
       isnull(ctctpb.fHangMua, 0) AS fHangMua,
       isnull(ctctpb.fHangNhap, 0) AS fHangNhap,
       isnull(ctctpb.fDuPhong, 0) AS fDuPhong,
       isnull(ctctpb.fPhanCap, 0) AS fPhanCap,
       (isnull(ctctpb.fTuChi, 0) + isnull(ctctdc.fTuChi, 0)) AS fTuChi,
       (isnull(ctctpb.fHienVat, 0) + isnull(ctctdc.fHienVat, 0)) AS fHienVat,
       ctctpb.dNgayTao,
       ctctpb.sNguoiTao,
       ctctpb.dNgaySua,
       ctctpb.sNguoiSua, 
	   ctctpb.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctctpb.iDuLieuNhan, 0) as iDuLieuNhan
		
		from
(SELECT
			*
		FROM NS_DT_ChungTuChiTiet
		WHERE
			iID_DTChungTu in (SELECT * FROM dbo.f_split(@ChungTuId))
			AND iDuLieuNhan = 0
	) ctctpb
	full join
			(SELECT ctct.*, CONCAT(dv.iID_MaDonVi, ' - ', dv.sTenDonVi) AS sTenDonVi, ct.sSoQuyetDinh
			FROM NS_DT_ChungTuChiTiet ctct
			LEFT JOIN (SELECT * FROM DonVi WHERE iNamLamViec = 2023) dv
			on dv.iID_MaDonVi = ctct.iID_MaDonVi
			LEFT JOIN NS_DT_ChungTu ct
			on ct.iID_DTChungTu = ctct.iID_DTChungTu
			LEFT JOIN NS_DT_Nhan_PhanBo_Map ctmap ON ctmap.iID_CTDuToan_Nhan = ct.iID_DTChungTu
			WHERE ctmap.iID_CTDuToan_PhanBo = @ChungTuId) ctctdc
		on ctctpb.sXauNoiMa = ctctdc.sXauNoiMa and ctctpb.iID_MaDonVi = ctctdc.iID_MaDonVi
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
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 28/06/2023 3:22:12 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_vdt_denghithanhtoan_index_2]    Script Date: 28/06/2023 3:22:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_denghithanhtoan_index_2]
	@YearOfWork int,
	@UserName nvarchar(100)
AS
BEGIN

	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT DISTINCT iID_DeNghiThanhToanID, tbl.iLoai,
		(CASE WHEN khvn.Id IS NOT NULL THEN khvn.sSoQuyetDinh
			WHEN khvu.Id IS NOT NULL THEN khvu.sSoQuyetDinh
			WHEN qt.Id IS NOT NULL THEN qt.sSoDeNghi
		END) as sSoQuyetDinh INTO #tmp
	FROM VDT_TT_DeNghiThanhToan_KHV as tbl
	LEFT JOIN VDT_KHV_PhanBoVon as khvn on tbl.iID_KeHoachVonID = khvn.Id AND tbl.iLoai = 1
	LEFT JOIN VDT_KHV_KeHoachVonUng as khvu on tbl.iID_KeHoachVonID = khvu.Id AND tbl.iLoai = 2
	LEFT JOIN VDT_QT_BCQuyetToanNienDo as qt on tbl.iID_KeHoachVonID = qt.Id AND tbl.iLoai in (3,4)


	SELECT 
	  iID_DeNghiThanhToanID,
	  STUFF((
		SELECT '; ' + sSoQuyetDinh
		FROM #tmp 
		WHERE (iID_DeNghiThanhToanID = Results.iID_DeNghiThanhToanID) 
		FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
	  ,1,2,'') AS sKeHoachVon ,
	  STUFF((
		SELECT '; ' + CAST(iLoai as nvarchar(5))
		FROM #tmp 
		WHERE (iID_DeNghiThanhToanID = Results.iID_DeNghiThanhToanID) 
		FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
	  ,1,2,'') AS sLoaiKeHoachVon
	  INTO #tmpKhv
	FROM #tmp Results
	GROUP BY iID_DeNghiThanhToanID


	SELECT tbl.*, ns.sTen as sNguonVon, lnv.sMoTa as sLoaiNguonVon, dv.sTenDonVi as sTenDonVi, 
		da.sTenDuAn, hd.sSoHopDong, hd.dNgayHopDong, hd.fTienHopDong as fGiaTriHopDong, nt.sMaNhaThau, da.sMaDuAn, khv.sKeHoachVon, khv.sLoaiKeHoachVon, tbl.iID_ChiPhiID as IIdChiPhiId,
		--Start Duchm18 23-06-2023: Sua logic lay sTenHopDong khi iID_HopDongId null
		(CASE
			WHEN tbl.iID_HopDongId IS NULL THEN cp.sTenChiPhi
			ELSE hd.STenHopDong
		END) as sTenHopDong,	
		-- End Duchm18
		pdtt.fGiaTriThanhToanTN as fGiaTriThanhToanTNDuocDuyet, pdtt.fGiaTriThanhToanNN as fGiaTriThanhToanNNDuocDuyet
	FROM VDT_TT_DeNghiThanhToan as tbl
	left join VDT_TT_PheDuyetThanhToan_ChiTiet pdtt on tbl.Id = pdtt.iID_DeNghiThanhToanID
	LEFT JOIN NguonNganSach as ns on tbl.iID_NguonVonID = ns.iID_MaNguonNganSach
	LEFT JOIN NS_MucLucNganSach as lnv on tbl.iID_LoaiNguonVonID = lnv.iID_MLNS
	LEFT JOIN DonVi as dv on tbl.iID_DonViQuanLyID = dv.iID_DonVi
	LEFT JOIN VDT_DA_DuAn as da on tbl.iID_DuAnId = da.iID_DuAnID
	LEFT JOIN VDT_DA_TT_HopDong as hd on tbl.iID_HopDongId = hd.Id
	LEFT JOIN VDT_DM_NhaThau as nt on tbl.iID_NhaThauId = nt.Id
	LEFT JOIN #tmpKhv as khv on tbl.Id = khv.iID_DeNghiThanhToanID
	LEFT JOIN VDT_DM_ChiPhi cp on tbl.iID_ChiPhiID = cp.iID_ChiPhi -- Duchm18 23-06-2023: add Left Join VDT_DM_ChiPhi
	WHERE 
	(
		( EXISTS (SELECT * from f_split(tbl.iID_MaDonViQuanLy) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi  WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			AND (@CountDonViCha = 0)
		)
		OR (@CountDonViCha <> 0 AND tbl.bKhoa = 1)
		OR 
		(   EXISTS (SELECT * from f_split(tbl.iID_MaDonViQuanLy) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi  WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			AND (@CountDonViCha <> 0)
		)
	) and (tbl.bTongHop is null or tbl.bTongHop != 1)


	ORDER BY tbl.dDateCreate DESC

	DROP TABLE #tmp
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_dexuat_index_find_all]    Script Date: 28/06/2023 3:22:12 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_index_find_all]    Script Date: 28/06/2023 3:22:12 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich]    Script Date: 28/06/2023 3:22:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich]
@iIdMaDonVi nvarchar(100),
@iNamKeHoach int,
@iIdNguonVon int
AS
BEGIN
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn, ISNULL(pc.sMa, '') as sMaPhanCap, dt.iID_LoaiCongTrinh, lct.STenLoaiCongTrinh INTO #tmpUnion
	FROM VDT_KHV_PhanBoVon as tbl
	INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on dt.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
	WHERE da.iID_MaDonViThucHienDuAnID = @iIdMaDonVi AND tbl.iNamKeHoach = @iNamKeHoach AND tbl.iID_NguonVonID = @iIdNguonVon
	UNION ALL
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn, ISNULL(pc.sMa, '') as sMaPhanCap, dt.iID_LoaiCongTrinh, lct.STenLoaiCongTrinh
	FROM VDT_QT_BCQuyetToanNienDo as tbl
	INNER JOIN VDT_QT_BCQuyetToanNienDo_ChiTiet_01 as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on dt.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
	WHERE iNamKeHoach = @iNamKeHoach AND (ISNULL(dt.fGiaTriNamTruocChuyenNamSau, 0) <> 0 OR ISNULL(dt.fGiaTriNamNayChuyenNamSau, 0) <> 0)

	SELECT DISTINCT IIDDuAnID as IIDDuAnID, SMaDuAn, SDiaDiem, STenDuAn, sMaPhanCap, iID_LoaiCongTrinh, STenLoaiCongTrinh, CAST(0 AS BIT) BIsChuyenTiep  INTO #tmp
	FROM #tmpUnion

	-- Check du an chuyen tiep
	UPDATE tmp
	SET
		BIsChuyenTiep = 1
	FROM #tmp as tmp
	INNER JOIN (
		SELECT DISTINCT dt.iID_DuAnID 
		FROM VDT_KHV_PhanBoVon as tbl 
		INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID 
		WHERE tbl.bActive = 1 AND tbl.iNamKeHoach = (@iNamKeHoach - 1)
		) as mp on tmp.IIDDuAnID = mp.iID_DuAnID

	-- Bao cao nam truoc
	SELECT iID_DuAnID, dt.iID_LoaiCongTrinh,
		SUM(ISNULL(dt.fDuToanCNSChuaGiaiNganTaiKB, 0)) as FDuToanCNSChuaGiaiNganTaiKB,		-- col 1
		SUM(ISNULL(dt.fDuToanCNSChuaGiaiNganTaiDV, 0)) as FDuToanCNSChuaGiaiNganTaiDv,		-- col 2
		SUM(ISNULL(dt.fDuToanCNSChuaGiaiNganTaiCuc, 0)) as FDuToanCNSChuaGiaiNganTaiCuc		-- col 3
		INTO #tmpBaoCaoNamTruoc
	FROM VDT_QT_BCQuyetToanNienDo as tbl
	INNER JOIN VDT_QT_BCQuyetToanNienDo_PhanTich as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
	INNER JOIN #tmp as tmp on dt.iID_DuAnID = tmp.IIDDuAnID AND tmp.iID_LoaiCongTrinh = dt.iID_LoaiCongTrinh
	WHERE tbl.iNamKeHoach = (@iNamKeHoach - 1) AND tbl.iID_NguonVonID = @iIdNguonVon
	GROUP BY iID_DuAnID, dt.iID_LoaiCongTrinh

	-- Gia tri nam truoc
	SELECT IIDDuAnID, tbl.iID_LoaiCongTrinh,
		(SUM(ISNULL(fLuyKeTamUngChuaThuHoi_KhvNam, 0)) - SUM(ISNULL(fLuyKeTamUngChuaThuHoi_KhvNamDelete, 0))) as fLuyKeTamUngChuaThuHoi_KhvNam,
		(SUM(ISNULL(fLuyKeTamUngChuaThuHoi_KhvNam_UQ, 0)) - SUM(ISNULL(fLuyKeTamUngChuaThuHoi_KhvNam_UQDelete, 0))) as fLuyKeTamUngChuaThuHoi_KhvNam_UQ ,
		(SUM(ISNULL(fTamUngChuaThuHoiKHVN, 0)) - SUM(ISNULL(fTamUngChuaThuHoiKHVNDelete, 0))) as fTamUngChuaThuHoiKHVN 
		INTO #tmpNamTruoc
	FROM
	(
		SELECT tmp.IIDDuAnID, dt.iID_LoaiCongTrinh,
			(CASE WHEN sMaDich = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '100' AND sMaPhanCap <> 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fLuyKeTamUngChuaThuHoi_KhvNam,				-- col 18
			(CASE WHEN sMaNguon = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '300' AND sMaPhanCap <> 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fLuyKeTamUngChuaThuHoi_KhvNamDelete,

			(CASE WHEN sMaDich = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '100' AND sMaPhanCap = 'UC' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fLuyKeTamUngChuaThuHoi_KhvNam_UQ,				-- col 19
			(CASE WHEN sMaNguon = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '300' AND sMaPhanCap = 'UC' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fLuyKeTamUngChuaThuHoi_KhvNam_UQDelete,

			(CASE WHEN sMaDich = '413a' AND sMaNguonCha = '301' AND sMaTienTrinh = '100' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fTamUngChuaThuHoiKHVN,				-- col 19
			(CASE WHEN sMaNguon = '413a' AND sMaNguonCha = '301' AND sMaTienTrinh = '300' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fTamUngChuaThuHoiKHVNDelete

		FROM #tmp as tmp
		INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID AND tmp.iID_LoaiCongTrinh = dt.iID_LoaiCongTrinh
		WHERE dt.iID_NguonVonID = @iIdNguonVon AND dt.iNamKeHoach = (@iNamKeHoach - 1)
	) as tbl
	GROUP BY IIDDuAnID, tbl.iID_LoaiCongTrinh


	-- Gia tri nam nay
	SELECT IIDDuAnID, tbl.iID_LoaiCongTrinh,
		(SUM(ISNULL(fChiTieuNamNayKb, 0)) - SUM(ISNULL(fChiTieuNamNayKbDelete, 0))) as fChiTieuNamNayKb,
		(SUM(ISNULL(fChiTieuNamNayLc, 0)) - SUM(ISNULL(fChiTieuNamNayLcDelete, 0))) as fChiTieuNamNayLc,
		(SUM(ISNULL(fTamUngNamNayKB, 0)) - SUM(ISNULL(fTamUngNamNayKBDelete, 0))) as fTamUngNamNayKB,
		(SUM(ISNULL(fThuHoiUngNamNayKB, 0)) - SUM(ISNULL(fThuHoiUngNamNayKBDelete, 0))) as fThuHoiUngNamNayKB,
		(SUM(ISNULL(fTamUngNamNayLC, 0)) - SUM(ISNULL(fTamUngNamNayLCDelete, 0))) as fTamUngNamNayLC,
		(SUM(ISNULL(fThanhToanKHVNNamNay, 0)) - SUM(ISNULL(fThanhToanKHVNNamNayDelete, 0))) as fThanhToanKHVNNamNay,
		(SUM(ISNULL(fThanhToanKHVNChuyenSang, 0)) - SUM(ISNULL(fThanhToanKHVNChuyenSangDelete, 0))) as fThanhToanKHVNChuyenSang
		INTO #tmpNamNay
	FROM
	(
		SELECT tmp.IIDDuAnID, dt.iID_LoaiCongTrinh ,
			(CASE WHEN sMaNguon = '101' AND sMaTienTrinh = '200' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fChiTieuNamNayKb,		-- col 6
			(CASE WHEN sMaDich = '101' AND sMaTienTrinh = '300' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fChiTieuNamNayKbDelete,

			(CASE WHEN sMaNguon = '102' AND sMaTienTrinh = '200' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fChiTieuNamNayLc,		-- col 7
			(CASE WHEN sMaDich = '102' AND sMaTienTrinh = '300' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fChiTieuNamNayLcDelete,

			(CASE WHEN sMaDich = '202' AND sMaNguonCha = '112' AND sMaTienTrinh = '200' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThanhToanKHVNChuyenSang,		-- col 10
			(CASE WHEN sMaNguon = '202' AND sMaNguonCha = '112' AND sMaTienTrinh = '300' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThanhToanKHVNChuyenSangDelete,

			(CASE WHEN sMaNguon = '212a' AND iThuHoiTUCheDo in (1,2) AND sMaTienTrinh = '200' AND sMaNguonCha = '102' AND sMaPhanCap <> 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fTamUngNamNayKB,		-- col 18
			(CASE WHEN sMaDich = '212a' AND iThuHoiTUCheDo in (1,2) AND sMaTienTrinh = '300' AND sMaNguonCha = '102' AND sMaPhanCap <> 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fTamUngNamNayKBDelete,

			(CASE WHEN sMaNguon = '211a' AND iThuHoiTUCheDo in (1,2) AND sMaTienTrinh = '200' AND sMaNguonCha = '101' AND sMaPhanCap = 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThuHoiUngNamNayKB,		-- col 19
			(CASE WHEN sMaDich = '211a' AND iThuHoiTUCheDo in (1,2) AND sMaTienTrinh = '300' AND sMaNguonCha = '101' AND sMaPhanCap = 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThuHoiUngNamNayKBDelete,

			(CASE WHEN sMaDich = '212a' AND sMaTienTrinh = '200' AND sMaNguonCha = '102' AND sMaPhanCap <> 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fTamUngNamNayLC,		-- col 18
			(CASE WHEN sMaNguon = '212a' AND sMaTienTrinh = '300' AND sMaNguonCha = '102' AND sMaPhanCap <> 'UQ' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fTamUngNamNayLCDelete, 

			(CASE WHEN sMaDich = '202' AND sMaTienTrinh = '200' AND sMaNguonCha = '102' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThanhToanKHVNNamNay,		-- col 21
			(CASE WHEN sMaNguon = '202' AND sMaTienTrinh = '300' AND sMaNguonCha = '102' THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThanhToanKHVNNamNayDelete
		FROM #tmp as tmp
		INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID AND tmp.iID_LoaiCongTrinh = dt.iID_LoaiCongTrinh
		WHERE dt.iID_NguonVonID = @iIdNguonVon AND dt.iNamKeHoach = @iNamKeHoach
	) as tbl
	GROUP BY IIDDuAnID, tbl.iID_LoaiCongTrinh

	-- Thong tri nam nay
	SELECT IIDDuAnID, tbl.iID_LoaiCongTrinh,
		SUM(ISNULL(fCapHopThuc, 0)) as fCapHopThuc,
		SUM(ISNULL(fCapKinhPhi, 0)) as fCapKinhPhi,
		SUM(ISNULL(fCapHopThucBoXung, 0)) as fCapHopThucBoXung,
		SUM(ISNULL(fCapKinhPhiBoXung, 0)) as fCapKinhPhiBoXung,
		SUM(ISNULL(fCapHopThucChuyenSang, 0)) as fCapHopThucChuyenSang,
		SUM(ISNULL(fCapKinhPhiChuyenSang, 0)) as fCapKinhPhiChuyenSang
		INTO #tmpThongTri
	FROM
	(
		SELECT tmp.IIDDuAnID,tmp.iID_LoaiCongTrinh,
			(CASE WHEN tbl.iLoaiThongTri = 4 THEN ISNULL(dt.fSoTien, 0) ELSE 0 END) as fCapHopThuc,
			(CASE WHEN tbl.iLoaiThongTri = 3 THEN ISNULL(dt.fSoTien, 0) ELSE 0 END) as fCapKinhPhi,
			(CASE WHEN tbl.iLoaiThongTri = 4 AND tbl.iNamNganSach = 1 THEN ISNULL(dt.fSoTien, 0) ELSE 0 END) as fCapHopThucBoXung,
			(CASE WHEN tbl.iLoaiThongTri = 3 AND tbl.iNamNganSach = 1 THEN ISNULL(dt.fSoTien, 0) ELSE 0 END) as fCapKinhPhiBoXung,
			(CASE WHEN tbl.iLoaiThongTri = 4 AND tbl.iNamNganSach = 2 THEN ISNULL(dt.fSoTien, 0) ELSE 0 END) as fCapHopThucChuyenSang,
			(CASE WHEN tbl.iLoaiThongTri = 3 AND tbl.iNamNganSach = 2 THEN ISNULL(dt.fSoTien, 0) ELSE 0 END) as fCapKinhPhiChuyenSang
		FROM VDT_ThongTri as tbl
		INNER JOIN VDT_ThongTri_ChiTiet as dt on tbl.Id = dt.iID_ThongTriID
		INNER JOIN #tmp as tmp on dt.iID_DuAnID = tmp.IIDDuAnID AND dt.iID_LoaiCongTrinhID = tmp.iID_LoaiCongTrinh
		LEFT JOIN NguonNganSach as nv on tbl.sMaNguonVon = nv.sMoTa
		WHERE tbl.iNamThongTri = @iNamKeHoach 
			AND (nv.iID_MaNguonNganSach IS NULL OR nv.iID_MaNguonNganSach = @iIdNguonVon)
	) as tbl
	GROUP BY IIDDuAnID, tbl.iID_LoaiCongTrinh



	SELECT tmp.*, tmp.iID_LoaiCongTrinh as IIdLoaiCongTrinh, lct.STenLoaiCongTrinh as STenLoaiCongTrinh, lct.SMaLoaiCongTrinh as SMaLoaiCongTrinh, da.sTenDuAn,
		ISNULL(bcNamTruoc.FDuToanCNSChuaGiaiNganTaiKB, 0) as FDuToanCnsChuaGiaiNganTaiKbNamTruoc,												-- col 1
		ISNULL(bcNamTruoc.fDuToanCNSChuaGiaiNganTaiDV, 0) as FDuToanCnsChuaGiaiNganTaiDvNamTruoc,												-- col 2
		ISNULL(bcNamTruoc.fDuToanCNSChuaGiaiNganTaiCuc, 0) as FDuToanCnsChuaGiaiNganTaiCucNamTruoc,												-- col 3
		ISNULL(nn.fChiTieuNamNayKB, 0) as FChiTieuNamNayKb,																						-- col 6
		ISNULL(nn.fChiTieuNamNayLC, 0) as FChiTieuNamNayLc,																						-- col 7
		(ISNULL(nn.fThanhToanKHVNChuyenSang, 0) + ISNULL(tt.fCapHopThucBoXung, 0) + ISNULL(fCapKinhPhiBoXung, 0)) as FSoCapNamTrcCs,			-- col 10
		(ISNULL(nn.fThanhToanKHVNNamNay, 0) + ISNULL(tt.fCapHopThucChuyenSang, 0) + ISNULL(tt.fCapKinhPhiChuyenSang, 0)) as FSoCapNamNay,		-- col 11
		CAST(0 as float) as FDnQuyetToanNamTrc,																									-- col 13
		CAST(0 as float) as FDnQuyetToanNamNay,																									-- col 14
		(ISNULL(fLuyKeTamUngChuaThuHoi_KhvNam, 0) - ISNULL(fTamUngNamNayKB, 0) + ISNULL(fTamUngNamNayLC, 0)) as FTuChuaThuHoiTaiCuc,			-- col 18
		(ISNULL(fLuyKeTamUngChuaThuHoi_KhvNam_UQ, 0) - ISNULL(fTamUngNamNayKB, 0) + ISNULL(fTamUngNamNayLC, 0)) as FTuChuaThuHoiTaiDonVi,		-- col 19
		(ISNULL(bcNamTruoc.fDuToanCNSChuaGiaiNganTaiCuc, 0) + ISNULL(nn.fChiTieuNamNayKB, 0)													-- col 21
			- (ISNULL(fLuyKeTamUngChuaThuHoi_KhvNam, 0) - ISNULL(fTamUngNamNayKB, 0) + ISNULL(fTamUngNamNayLC, 0))
			- fThanhToanKHVNNamNay) as FDuToanCnsChuaGiaiNganTaiCuc,
		(ISNULL(bcNamTruoc.FDuToanCNSChuaGiaiNganTaiDv, 0) + ISNULL(tt.fCapHopThuc, 0) + ISNULL(tt.fCapKinhPhi, 0)								-- col 22
			- (ISNULL(nn.fTamUngNamNayLC, 0)) + ISNULL(nn.fThanhToanKHVNNamNay, 0) - ISNULL(nn.fTamUngNamNayKB, 0)) as FDuToanCnsChuaGiaiNganTaiDv	,																						
		(ISNULL(bcNamTruoc.fDuToanCNSChuaGiaiNganTaiKB, 0) + ISNULL(nn.fChiTieuNamNayKB, 0) - ISNULL(tt.fCapHopThuc, 0)) as FDuToanCnsChuaGiaiNganTaiKb	,
		CAST(0 as float) as FDuToanThuHoi
	FROM #tmp as tmp
	INNER JOIN VDT_DA_DuAn as da on tmp.IIDDuAnID = da.iID_DuAnID
	LEFT JOIN #tmpBaoCaoNamTruoc as bcNamTruoc on tmp.IIDDuAnID = bcNamTruoc.iID_DuAnID AND tmp.iID_LoaiCongTrinh = bcNamTruoc.iID_LoaiCongTrinh
	LEFT JOIN #tmpThongTri as tt on tmp.IIDDuAnID = tt.IIDDuAnID AND tmp.iID_LoaiCongTrinh = tt.iID_LoaiCongTrinh
	LEFT JOIN #tmpNamNay as nn on tmp.IIDDuAnID = nn.IIDDuAnID AND tmp.iID_LoaiCongTrinh = nn.iID_LoaiCongTrinh
	LEFT JOIN #tmpNamTruoc as nt on tmp.IIDDuAnID = nt.IIDDuAnID AND tmp.iID_LoaiCongTrinh = nt.iID_LoaiCongTrinh
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on tmp.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh

	DROP TABLE #tmpNamNay
	DROP TABLE #tmpNamTruoc
	DROP TABLE #tmpUnion
	DROP TABLE #tmp
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich_by_id]    Script Date: 28/06/2023 3:22:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_qt_baocaoquyettoanniendo_phantich_by_id]
@iIDQuyetToanId uniqueidentifier
AS
BEGIN
	DECLARE @iNamLamViec int
	DECLARE @iIdNguonVonId int
	DECLARE @iIdMaDonVi nvarchar(100)
	SELECT @iNamLamViec = iNamKeHoach, @iIdNguonVonId = iID_NguonVonID, @iIdMaDonVi = iID_MaDonViQuanLy
	FROM VDT_QT_BCQuyetToanNienDo WHERE Id = @iIDQuyetToanId

	SELECT DISTINCT iID_DuAnID, CAST(0 as BIT) BIsChuyenTiep INTO #tmp
	FROM VDT_QT_BCQuyetToanNienDo_PhanTich
	WHERE iID_BCQuyetToanNienDo = @iIDQuyetToanId

	-- Check du an chuyen tiep
	UPDATE tmp
	SET
		BIsChuyenTiep = 1
	FROM #tmp as tmp
	INNER JOIN (
		SELECT DISTINCT dt.iID_DuAnID 
		FROM VDT_KHV_PhanBoVon as tbl 
		INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID 
		WHERE tbl.bActive = 1 AND tbl.iNamKeHoach = (@iNamLamViec - 1)
		) as mp on tmp.iID_DuAnID = mp.iID_DuAnID

	SELECT dt.iID_DuAnID, dt.iID_LoaiCongTrinh,
		SUM(ISNULL(dt.FDuToanCnsChuaGiaiNganTaiKb, 0)) as FDuToanCnsChuaGiaiNganTaiKbNamTruoc,
		SUM(ISNULL(dt.FDuToanCnsChuaGiaiNganTaiDv, 0)) as FDuToanCnsChuaGiaiNganTaiDvNamTruoc,
		SUM(ISNULL(dt.FDuToanCnsChuaGiaiNganTaiCuc, 0)) as FDuToanCnsChuaGiaiNganTaiCucNamTruoc INTO #tmpNamTruoc
	FROM VDT_QT_BCQuyetToanNienDo as tbl
	INNER JOIN VDT_QT_BCQuyetToanNienDo_PhanTich as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
	WHERE iNamKeHoach = (@iNamLamViec - 1) AND iID_NguonVonID = @iIdNguonVonId AND iID_MaDonViQuanLy = @iIdMaDonVi
	GROUP BY dt.iID_DuAnID, dt.iID_LoaiCongTrinh

	SELECT tbl.iID_DuAnID as IIdDuAnId,
		tmp.BIsChuyenTiep,
		da.STenDuAn,
		tbl.iID_LoaiCongTrinh as IIdLoaiCongTrinh,
		lct.STenLoaiCongTrinh,
		nt.FDuToanCnsChuaGiaiNganTaiKbNamTruoc,
		nt.FDuToanCnsChuaGiaiNganTaiDvNamTruoc,
		nt.FDuToanCnsChuaGiaiNganTaiCucNamTruoc,
		tbl.FDuToanCnsChuaGiaiNganTaiKb,
		tbl.FDuToanCnsChuaGiaiNganTaiDv,
		tbl.FDuToanCnsChuaGiaiNganTaiCuc,
		tbl.FTuChuaThuHoiTaiCuc,
		tbl.FChiTieuNamNayKb,
		tbl.FChiTieuNamNayLc,
		tbl.FSoCapNamTrcCs,
		tbl.FSoCapNamNay,
		tbl.FDnQuyetToanNamTrc,
		tbl.FDnQuyetToanNamNay,
		tbl.FTuChuaThuHoiTaiDonVi,
		tbl.FDuToanThuHoi
	FROM VDT_QT_BCQuyetToanNienDo_PhanTich as tbl
	INNER JOIN #tmp as tmp on tbl.iID_DuAnID = tmp.iID_DuAnID
	INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN #tmpNamTruoc as nt on tbl.iID_DuAnID = nt.iID_DuAnID AND tbl.iID_LoaiCongTrinh = nt.iID_LoaiCongTrinh
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on tbl.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
	WHERE tbl.iID_BCQuyetToanNienDo = @iIDQuyetToanId

	DROP TABLE #tmpNamTruoc
END
;
;
GO
