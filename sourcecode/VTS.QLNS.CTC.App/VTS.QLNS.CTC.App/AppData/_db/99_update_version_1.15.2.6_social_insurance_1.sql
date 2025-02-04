
GO
IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptBH_ThongTriTongHop_Thu_All_TongHopChung_NLD')
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptBH_ThongTriTongHop_Thu_All_TongHopChung_NLD', NULL, N'rptBH_ThongTriTongHop_Thu_All_TongHopChung_NLD', NULL, NULL, NULL, NULL, NULL, N'QTT_THONG_TRI_TONG_HOP', NULL, N'Thu bảo hiểm xã hội, bảo hiểm y tế, bảo hiểm thất nghiệp NLĐ đóng', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'THÔNG TRI', N'2', N'Thu bảo hiểm xã hội, bảo hiểm y tế, bảo hiểm thất nghiệp NLĐ đóng', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptBH_ThongTriTongHop_Thu_All_TongHopChung_NSD')
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptBH_ThongTriTongHop_Thu_All_TongHopChung_NSD', NULL, N'rptBH_ThongTriTongHop_Thu_All_TongHopChung_NSD', NULL, NULL, NULL, NULL, NULL, N'QTT_THONG_TRI_TONG_HOP', NULL, N'Thu bảo hiểm xã hội, bảo hiểm y tế, bảo hiểm thất nghiệp NSDLĐ đóng', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'THÔNG TRI', N'2', N'Thu bảo hiểm xã hội, bảo hiểm y tế, bảo hiểm thất nghiệp NSDLĐ đóng', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlns_bhxh]    Script Date: 12/24/2024 10:54:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlns_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlns_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 12/24/2024 10:54:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_data_update_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_data_update_new]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_qtt_thong_tri]    Script Date: 12/24/2024 10:54:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_qtt_thong_tri]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_qtt_thong_tri]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dctt_get_data_quyet_toan]    Script Date: 12/24/2024 11:32:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dctt_get_data_quyet_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dctt_get_data_quyet_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dctt_get_data_quyet_toan]    Script Date: 12/24/2024 11:32:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
/****** Object:  StoredProcedure [dbo].[sp_bh_report_qtt_thong_tri]    Script Date: 12/24/2024 10:54:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_qtt_thong_tri]
	-- Add the parameters for the stored procedure here
	@NamLamViec int ,
	@LstMaDonVi nvarchar(max),
	@IQuyNam int, 
	@IQuyNamLoai int,
	@ILoaiThongTri int,
	@DVT int
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuyNam = 3 AND @IQuyNamLoai = 1) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuyNam = 6 AND @IQuyNamLoai= 1) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuyNam = 9 AND @IQuyNamLoai= 1) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuyNam = 12 AND @IQuyNamLoai = 1) BEGIN SET @SMonths = '10,11,12' END
	ELSE BEGIN SET @SMonths = @IQuyNam END

	SELECT * INTO #result FROM 
	(
		SELECT 1 STT
			,N'Bảo hiểm xã hội' as SNoiDung,
			case @ILoaiThongTri when 3 then (sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) / @DVT
			when 4 then (sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0))) / @DVT -- NLD
			when 5 then (sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) / @DVT -- NSD
			end as FSoTien  
			,1 ILevel
		FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu
			AND (ctct.sXauNoiMa  like '9020001-010-011-0001%' 
			OR ctct.sXauNoiMa like '9020001-010-011-0002%' 
			OR ctct.sXauNoiMa like '9020002-010-011-0001%' 
			OR ctct.sXauNoiMa like '9020002-010-011-0002%')
		left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.sXauNoiMa = gt.sXauNoiMa
			AND (gt.sXauNoiMa  like '9020001-010-011-0001%' 
			OR gt.sXauNoiMa like '9020001-010-011-0002%' 
			OR gt.sXauNoiMa like '9020002-010-011-0001%' 
			OR gt.sXauNoiMa like '9020002-010-011-0002%')
		WHERE ct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			--AND iQuyNamLoai = @IQuyNamLoai
			AND ct.iQuyNamLoai <> 2
			AND ct.iNamLamViec = @NamLamViec

		UNION ALL
		
		SELECT 2 STT
				,N'Bảo hiểm y tế' as SNoiDung,
				case @ILoaiThongTri when 3 then (sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) / @DVT 
				when 4 then (sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0))) / @DVT -- NLD
				when 5 then (sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) / @DVT -- NSD
				end as FSoTien 
				,1 ILevel
		FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu
			AND (ctct.sXauNoiMa  like  '9020001-010-011-0001%' 
			OR ctct.sXauNoiMa like '9020001-010-011-0002%' 
			OR ctct.sXauNoiMa like '9020002-010-011-0001%'
			OR ctct.sXauNoiMa like '9020002-010-011-0002%')
		left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.sXauNoiMa = gt.sXauNoiMa
		WHERE ct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			--AND iQuyNamLoai=@IQuyNamLoai
			AND ct.iQuyNamLoai <> 2
			AND ct.iNamLamViec = @NamLamViec

		UNION ALL

		SELECT 3 STT
			,N'Bảo hiểm thất nghiệp' as SNoiDung,
			case @ILoaiThongTri when 3 then (sum(isnull(ctct.fThu_BHTN_NLD,0)) + sum(isnull(ctct.fThu_BHTN_NSD,0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) / @DVT
			when 4 then (sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0))) / @DVT -- NLD
			when 5 then (sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) / @DVT -- NSD
			end as FSoTien 
			,1 ILevel
		FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu
			AND (ctct.sXauNoiMa  like  '9020001-010-011-0001%' 
			OR ctct.sXauNoiMa like '9020001-010-011-0002%' 
			OR ctct.sXauNoiMa like '9020002-010-011-0001%' 
			OR ctct.sXauNoiMa like '9020002-010-011-0002%')
		left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.sXauNoiMa = gt.sXauNoiMa
		WHERE  ct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			--AND iQuyNamLoai=@IQuyNamLoai
			AND ct.iQuyNamLoai <> 2
			AND ct.iNamLamViec = @NamLamViec
	) result

	select * from #result
	DROP TABLE #result;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_data_update_new]    Script Date: 12/24/2024 10:54:35 AM ******/
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
	@isUpdateDanhMucNgayNghi int,
	@isUpdateMucLucQuyetToanNam int
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
					fTyLe_BHTN_NLD,
					fHeSoLayQuyLuong,
					sLuongChinh,
					sNS_LuongChinh,
					sNS_PCCV,
					sNS_PCTN,
					sNS_PCTNVK,
					sPCCV,
					sPCTN,
					sPCTNVK,
					sNS_HSBL)
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
					fTyLe_BHTN_NLD,
					fHeSoLayQuyLuong,
					sLuongChinh,
					sNS_LuongChinh,
					sNS_PCCV,
					sNS_PCTN,
					sNS_PCTNVK,
					sPCCV,
					sPCTN,
					sPCTNVK,
					sNS_HSBL
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
					sXauNoiMa,
					ILock)
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
					sXauNoiMa,
					ILock
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
		if (@isUpdateMucLucQuyetToanNam = 1)
		begin
			-- COPY MUC LUC QUYET TOAN NAM
				DELETE FROM NS_MucLucQuyetToanNam WHERE iNamLamViec = @destinationYear;
				INSERT INTO NS_MucLucQuyetToanNam (
					iID,
					bHangCha,
					dNgaySua,
					dNgayTao,
					iTrangThai,
					sMa,
					sMaCha,
					sMoTa,
					iNamLamViec,
					sNguoiSua,
					sNguoiTao,
					sSTT)
				select 
					NEWID(),
					bHangCha,
					dNgaySua,
					GETDATE(),
					iTrangThai,
					sMa,
					sMaCha,
					sMoTa,
					@destinationYear,
					sNguoiSua,
					@userCreator,
					sSTT
				from NS_MucLucQuyetToanNam
				where iNamLamViec = @sourceYear

				-- COPY MUC LUC QUYET TOAN NAM MLNS
				DELETE FROM NS_MucLucQuyetToanNam_MLNS WHERE iNamLamViec = @destinationYear;
				INSERT INTO NS_MucLucQuyetToanNam_MLNS (
					iID,
					dNgaySua,
					dNgayTao,
					sMaMLQT,
					iNamLamViec,
					sNguoiSua,
					sNguoiTao,
					sXauNoiMa)
				select 
					NEWID(),
					dNgaySua,
					GETDATE(),
					sMaMLQT,
					@destinationYear,
					sNguoiSua,
					@userCreator,
					sXauNoiMa
				from NS_MucLucQuyetToanNam_MLNS
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
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlns_bhxh]    Script Date: 12/24/2024 10:54:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_clone_year_mlns_bhxh]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
AS
BEGIN
	
	
insert into BH_DM_MucLucNganSach (
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
	fTyLe_BHTN_NLD,
	fHeSoLayQuyLuong,
	sLuongChinh,
	sNS_LuongChinh,
	sNS_PCCV,
	sNS_PCTN,
	sNS_PCTNVK,
	sPCCV,
	sPCTN,
	sPCTNVK,
	sNS_HSBL)
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
	@dest,
	sCPChiTietToi,
	sDuToanChiTietToi,
	sNguoiSua,
	@userCreate,
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
	fTyLe_BHTN_NLD,
	fHeSoLayQuyLuong,
	sLuongChinh,
	sNS_LuongChinh,
	sNS_PCCV,
	sNS_PCTN,
	sNS_PCTNVK,
	sPCCV,
	sPCTN,
	sPCTNVK,
	sNS_HSBL
from BH_DM_MucLucNganSach
where iNamLamViec = @source and sXauNoiMa not in (select sXauNoiMa from BH_DM_MucLucNganSach where iNamLamViec = @dest)

END

GO

CREATE PROCEDURE [dbo].[sp_bh_dctt_get_data_quyet_toan] 
	@NamLamViec INT,
	@MaDonVi NVARCHAR(max),
	@ThangQuy INT,
	@LoaiThangQuy INT
AS
BEGIN
	--Data quyet toan
	SELECT ctct.sXauNoiMa,
		ctct.iID_MaDonVi,
		sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD,
		sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD,
		sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD,
		sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD,
		sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD,
		sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
	INTO #temp_qt
	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	JOIN BH_QTT_BHXH_ChungTu ct ON ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu
	WHERE ct.iNamLamViec = @NamLamViec
		AND ct.iID_MaDonVi IN (SELECT *FROM f_split(@MaDonVi))
		AND ct.iQuyNam <= @ThangQuy
		AND ct.iQuyNamLoai = @LoaiThangQuy
		AND ct.bIsKhoa = 1
	GROUP BY ctct.sXauNoiMa, ctct.iID_MaDonVi

	--Data giai thich
	SELECT ctct.sXauNoiMa,
		ctct.iID_MaDonVi,
		sum(isnull(ctct.fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(ctct.fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		sum(isnull(ctct.fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(ctct.fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		sum(isnull(ctct.fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(ctct.fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD
	INTO #temp_giaithich
	FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	JOIN BH_QTT_BHXH_ChungTu ct ON ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu
	WHERE ct.iNamLamViec = @NamLamViec
		AND ct.iID_MaDonVi IN (SELECT *FROM f_split(@MaDonVi))
		AND ct.iQuyNam <= @ThangQuy
		AND ct.iQuyNamLoai = @LoaiThangQuy
		AND ct.bIsKhoa = 1
	GROUP BY ctct.sXauNoiMa, ctct.iID_MaDonVi

	SELECT TEMP.sXauNoiMa, TEMP.iID_MaDonVi
	INTO #temp_base
	FROM (
		SELECT DISTINCT sXauNoiMa, iID_MaDonVi FROM #temp_qt
		UNION
		SELECT DISTINCT sXauNoiMa, iID_MaDonVi FROM #temp_giaithich) TEMP

	--Ket qua
	SELECT base.sXauNoiMa,
		base.iID_MaDonVi,
		isnull(qt.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) fThuBHXH_NLD_QTDauNam,
		isnull(qt.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) fThuBHXH_NSD_QTDauNam,
		isnull(qt.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) fThuBHYT_NLD_QTDauNam,
		isnull(qt.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) fThuBHYT_NSD_QTDauNam,
		isnull(qt.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) fThuBHTN_NLD_QTDauNam,
		isnull(qt.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0) fThuBHTN_NSD_QTDauNam
	FROM #temp_base base
	LEFT JOIN #temp_qt qt ON base.sXauNoiMa = qt.sXauNoiMa AND base.iID_MaDonVi = qt.iID_MaDonVi
	LEFT JOIN #temp_giaithich gt ON base.sXauNoiMa = gt.sXauNoiMa AND base.iID_MaDonVi = gt.iID_MaDonVi
END
;
