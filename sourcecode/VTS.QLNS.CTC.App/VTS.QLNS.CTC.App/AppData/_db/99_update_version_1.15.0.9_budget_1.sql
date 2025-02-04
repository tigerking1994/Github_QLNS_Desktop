GO
IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_DuToan_PAPB_MauSo1_PhuLucII')
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_DuToan_PAPB_MauSo1_PhuLucII', NULL, N'rptNS_DuToan_PAPB_MauSo1_PhuLucII', NULL, NULL, NULL, NULL, NULL, N'DU_TOAN_PHUONG_AN_PHAN_BO', NULL, N'Phương án phân bổ dự toán - Theo Công văn 2344/QĐ-CTC', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'Phụ lục II', N'2', N'CHI TIẾT PHƯƠNG ÁN PHÂN BỔ NGÂN SÁCH NĂM 2024', N'(Kèm theo Báo cáo số …………/BC-…………ngày……/……/20…… của…………………………………………)', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]    Script Date: 11/11/2024 3:47:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]    Script Date: 11/11/2024 3:47:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_PAPBDT_m01_pl2_get_donvi_banthan]    Script Date: 11/11/2024 3:47:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_PAPBDT_m01_pl2_get_donvi_banthan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_PAPBDT_m01_pl2_get_donvi_banthan]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_PAPBDT_m01_pl2_get_donvi]    Script Date: 11/11/2024 3:47:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_PAPBDT_m01_pl2_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_PAPBDT_m01_pl2_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_PAPBDT_m01_pl2_get_donvi]    Script Date: 11/11/2024 3:47:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_PAPBDT_m01_pl2_get_donvi]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100)
AS
BEGIN
	
	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @YearOfWork 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @YearOfWork and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select * into #basemlns
	from NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and bHangChaDuToan IS NOT NULL 
	and iTrangThai = 1 
	and sLNS in (select * from f_split(@LNS))
	
	--Thu
	select ctct.* into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ctct.Id_ChungTu = ct.Id
	where ct.NamLamViec = @YearOfWork and ct.NamNganSach = @YearOfBudget
		and convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	where ct.iNamLamViec = @YearOfWork and ct.iNamNganSach = @YearOfBudget
		and convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)

	select dv.IIdMaDonVi from
	(select distinct Id_DonVi IIdMaDonVi from #basedatathu where Id_donvi not in (SELECT * FROM f_split(@DonViBanThan))
	union
	select distinct iID_MaDonVi IIdMaDonVi from #basedatachi where iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))) dv
	join DonVi donvi on dv.IIdMaDonVi = donvi.iID_MaDonVi and donvi.iNamLamViec = @YearOfWork and donvi.iTrangThai = 1 and donvi.iLoai <> 0

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_PAPBDT_m01_pl2_get_donvi_banthan]    Script Date: 11/11/2024 3:47:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_PAPBDT_m01_pl2_get_donvi_banthan]
	@YearOfWork int
AS
BEGIN
	
	SELECT pr.iID_MaDonVi
	FROM DonVi pr
	WHERE iNamLamViec = @YearOfWork 
	and iLoai = 1
	and iTrangThai = 1
	and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @YearOfWork and iLoai = 0)									

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]    Script Date: 11/11/2024 3:47:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@DVT int
AS
BEGIN
	
	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @YearOfWork 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @YearOfWork and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select * into #basemlns
	from NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and bHangChaDuToan IS NOT NULL 
	and iTrangThai = 1 
	and sLNS in (select * from f_split(@LNS))
	
	--Thu
	select ctct.* into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ctct.Id_ChungTu = ct.Id
	where ct.NamLamViec = @YearOfWork and ct.NamNganSach = @YearOfBudget
		and convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	where ct.iNamLamViec = @YearOfWork and ct.iNamNganSach = @YearOfBudget
		and convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)

	--Data Ban than
	select temp.* into #databanthan from(
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   0 fChoPhanBo,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathu dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		left join #basedatathu cpb ON mlns.sXauNoiMa = cpb.XauNoiMa and cpb.iPhanCap = 1 and dtnsdg.NguonNganSach = 1
		where mlns.sLNS like '8%'
		and donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangCha

		UNION

		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   0 fChoPhanBo,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		left join #basedatachi dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		left join #basedatachi cpb ON mlns.sXauNoiMa = cpb.sXauNoiMa and cpb.iPhanCap = 1 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangCha
	) temp

	--Data Don vi
	select temp.* into #datadonvi from(
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.TuChi, 0))/@DVT fDuToanNSDuocGiao,
		   (sum(isnull(dtnsdg.TuChi, 0)) - sum(isnull(cpb.TuChi, 0)))/@DVT fChoPhanBo,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBo,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathu dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		left join #basedatathu cpb ON mlns.sXauNoiMa = cpb.XauNoiMa and cpb.iPhanCap = 1 and dtnsdg.NguonNganSach = 1
		where mlns.sLNS like '8%'
		and donvi.Id_donvi not in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.bHangCha

		UNION

		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.fTuChi, 0))/@DVT fDuToanNSDuocGiao,
		   (sum(isnull(dtnsdg.fTuChi, 0)) - sum(isnull(cpb.fTuChi, 0)))/@DVT fChoPhanBo,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBo,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		left join #basedatachi dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		left join #basedatachi cpb ON mlns.sXauNoiMa = cpb.sXauNoiMa and cpb.iPhanCap = 1 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.bHangCha
	) temp

	--Ket qua
	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(donvi.fChoPhanBo, 0)) fChoPhanBo,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) FTongSoPhanBo,
		'THU' sLoai
	from #basemlns mlns
	left join #datadonvi donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'THU'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'THU'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.bHangCha

	union

	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(donvi.fChoPhanBo, 0)) fChoPhanBo,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) FTongSoPhanBo,
		'CHI' sLoai
	from #basemlns mlns
	left join #datadonvi donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'CHI'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'CHI'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.bHangCha

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]    Script Date: 11/11/2024 3:47:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN
	
	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @YearOfWork 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @YearOfWork and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select * into #basemlns
	from NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and bHangChaDuToan IS NOT NULL 
	and iTrangThai = 1 
	and sLNS in (select * from f_split(@LNS))
	
	--Thu
	select ctct.* into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ct.Id = ctct.Id_ChungTu
	where ct.NamLamViec = @YearOfWork and ct.NamNganSach = @YearOfBudget
		and convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.Id_DonVi in (SELECT * FROM f_split(@MaDonVi))
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
	where ct.iNamLamViec = @YearOfWork and ct.iNamNganSach = @YearOfBudget
		and convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))

	--Data Ban than
	select temp.* into #databanthan from(
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   0 fChoPhanBo,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathu dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		left join #basedatathu cpb ON mlns.sXauNoiMa = cpb.XauNoiMa and cpb.iPhanCap = 1 and dtnsdg.NguonNganSach = 1
		where mlns.sLNS like '8%'
		and donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.bHangCha,
			donvi.Id_donvi

		UNION

		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   0 fChoPhanBo,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		left join #basedatachi dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		left join #basedatachi cpb ON mlns.sXauNoiMa = cpb.sXauNoiMa and cpb.iPhanCap = 1 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.bHangCha
	) temp

	--Data Don vi
	select temp.* into #datadonvi from(
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.TuChi, 0))/@DVT fDuToanNSDuocGiao,
		   (sum(isnull(dtnsdg.TuChi, 0)) - sum(isnull(cpb.TuChi, 0)))/@DVT fChoPhanBo,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBo,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathu dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		left join #basedatathu cpb ON mlns.sXauNoiMa = cpb.XauNoiMa and cpb.iPhanCap = 1 and dtnsdg.NguonNganSach = 1
		where mlns.sLNS like '8%'
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.bHangCha

		UNION

		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.fTuChi, 0))/@DVT fDuToanNSDuocGiao,
		   (sum(isnull(dtnsdg.fTuChi, 0)) - sum(isnull(cpb.fTuChi, 0)))/@DVT fChoPhanBo,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBo,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		left join #basedatachi dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		left join #basedatachi cpb ON mlns.sXauNoiMa = cpb.sXauNoiMa and cpb.iPhanCap = 1 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.bHangCha
	) temp

	--Ket qua
	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(donvi.fChoPhanBo, 0)) fChoPhanBo,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) FTongSoPhanBo,
		'THU' sLoai
	from #basemlns mlns
	left join #datadonvi donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'THU'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'THU'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha

	union

	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(donvi.fChoPhanBo, 0)) fChoPhanBo,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) FTongSoPhanBo,
		'CHI' sLoai
	from #basemlns mlns
	left join #datadonvi donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'CHI'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'CHI'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha

END
GO
