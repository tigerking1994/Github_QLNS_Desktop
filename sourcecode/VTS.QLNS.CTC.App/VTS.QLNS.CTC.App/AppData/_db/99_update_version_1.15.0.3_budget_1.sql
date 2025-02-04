GO
IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_Plan_DuToan_NganSach_ChiTiet_DonVi')
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_Plan_DuToan_NganSach_ChiTiet_DonVi', NULL, N'rptNS_Plan_DuToan_NganSach_ChiTiet_DonVi', NULL, NULL, NULL, NULL, NULL, N'LAP_DU_TOAN_NGAN_SACH', NULL, N'Báo cáo dự toán ngân sách - Các nội dung thực hiện định mức thí điểm', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CHI TIẾT DỰ TOÁN NGÂN SÁCH NHÀ NƯỚC CHI THƯỜNG XUYÊN CHO QUỐC PHÒNG ĐỐI VỚI NỘI DUNG CHI THỰC HIỆN ĐỊNH MỨC THÍ ĐIỂM NĂM 2024', NULL, NULL, N'.../QĐ-BQP', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_skt_get_donvi_solieu_chitiet_all_loai]    Script Date: 10/28/2024 4:23:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_get_donvi_solieu_chitiet_all_loai]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_get_donvi_solieu_chitiet_all_loai]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]    Script Date: 10/28/2024 4:23:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]    Script Date: 10/28/2024 4:23:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]    Script Date: 10/28/2024 4:23:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]    Script Date: 10/28/2024 4:23:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	select ns.* into #mlns_skt from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1
	
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		case when mlns.bHangCha = 1 then ctct.sMoTa
			else concat('   ', ctct.sMoTa) end sMoTa,
		(sum(isnull(ctct.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0)))/@DVT fTuChi
	from NS_DTDauNam_ChungTuChiTiet ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		ctct.sMoTa

END
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]    Script Date: 10/28/2024 4:23:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @NamLamViec 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select ns.* into #mlns_skt from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1

	--Ban than
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		case when mlns.bHangCha = 1 then ctct.sMoTa
			else concat('   ', ctct.sMoTa) end sMoTa,
		(sum(isnull(ctct.fTuChi, 0)))/@DVT fTuChiBanThan
	into #tbl_banthan
	from NS_DTDauNam_ChungTuChiTiet ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa
	
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		case when mlns.bHangCha = 1 then ctct.sMoTa
			else concat('   ', ctct.sMoTa) end sMoTa,
		(sum(isnull(ctct.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0)))/@DVT fTuChi,
		(sum(isnull(banthan.fTuChiBanThan, 0)))/@DVT fTuChiBanThan,
		((sum(isnull(banthan.fTuChiBanThan, 0))) + (sum(isnull(ctct.fTuChi, 0))))/@DVT TongTuChi
	from NS_DTDauNam_ChungTuChiTiet ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	left join #tbl_banthan banthan on ctct.sXauNoiMa = banthan.sXauNoiMa
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa

END
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]    Script Date: 10/28/2024 4:23:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @NamLamViec 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select ns.* into #mlns_skt from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1

	--Ban than
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		case when mlns.bHangCha = 1 then ctct.sMoTa
			else concat('   ', ctct.sMoTa) end sMoTa,
		(sum(isnull(ctct.fTuChi, 0)))/@DVT fTuChiBanThan
	into #tbl_banthan
	from NS_DTDauNam_ChungTuChiTiet ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa
	
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		case when mlns.bHangCha = 1 then ctct.sMoTa
			else concat('   ', ctct.sMoTa) end sMoTa,
		(sum(isnull(ctct.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0)))/@DVT fTuChi,
		(sum(isnull(banthan.fTuChiBanThan, 0)))/@DVT fTuChiBanThan,
		((sum(isnull(banthan.fTuChiBanThan, 0))) + (sum(isnull(ctct.fTuChi, 0))))/@DVT TongTuChi,
		ctct.iID_MaDonVi IIdMaDonVi
	from NS_DTDauNam_ChungTuChiTiet ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	left join #tbl_banthan banthan on ctct.sXauNoiMa = banthan.sXauNoiMa
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iID_MaNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa, ctct.iID_MaDonVi

END
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_donvi_solieu_chitiet_all_loai]    Script Date: 10/28/2024 4:23:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_get_donvi_solieu_chitiet_all_loai]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetOfSource int,
	@iLoaiNNS int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
	FROM DonVi
	WHERE iNamLamViec = @YearOfWork
	  AND iTrangThai = 1
	  AND iLoai <> 0
	  AND iID_MaDonVi in
		(SELECT DISTINCT chitiet.iID_MaDonVi
		 FROM NS_DTDauNam_ChungTuChiTiet chitiet
		 INNER JOIN NS_DTDauNam_ChungTu chungtu 
		 ON chitiet.iID_CTDTDauNam = chungtu.iID_CTDTDauNam
		 AND chitiet.iNamLamViec = chungtu.iNamLamViec
		 AND (@iLoaiNNS = 0 OR chungtu.iLoaiNguonNganSach = @iLoaiNNS) 
		 WHERE chitiet.iNamLamViec = @YearOfWork
		   AND chitiet.iNamNganSach = @YearOfBudget
		   AND chitiet.iID_MaNguonNganSach = @BudgetOfSource
		   AND chitiet.iLoai = 3
		   AND iTrangThai = 1
		   AND (fTuChi <> 0
				OR fDuPhong <> 0
				OR fHangNhap <> 0
				OR fHangMua <> 0
				OR fPhanCap <> 0) )
ORDER BY iID_MaDonVi 
END
;
;
;
GO
