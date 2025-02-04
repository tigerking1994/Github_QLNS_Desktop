/****** Object:  StoredProcedure [dbo].[sp_skt_nhan_so_kiem_tra_1]    Script Date: 11/29/2024 4:01:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_nhan_so_kiem_tra_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_nhan_so_kiem_tra_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]    Script Date: 11/29/2024 4:01:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]    Script Date: 11/29/2024 4:01:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_PAPBDT_m01_pl2_get_donvi]    Script Date: 11/29/2024 4:01:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_PAPBDT_m01_pl2_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_PAPBDT_m01_pl2_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel]    Script Date: 12/3/2024 9:31:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_PAPBDT_m01_pl2_get_donvi]    Script Date: 11/29/2024 4:01:14 PM ******/
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
	join #basemlns ml on ctct.XauNoiMa = ml.sXauNoiMa
	where ct.NamLamViec = @YearOfWork and ct.NamNganSach = @YearOfBudget
		and convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and isnull(ctct.TuChi, 0) <> 0
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	join #basemlns ml on ctct.sXauNoiMa = ml.sXauNoiMa
	where ct.iNamLamViec = @YearOfWork and ct.iNamNganSach = @YearOfBudget
		and convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and isnull(ctct.fTuChi, 0) <> 0

	select dv.IIdMaDonVi from
	(select distinct Id_DonVi IIdMaDonVi from #basedatathu where Id_donvi not in (SELECT * FROM f_split(@DonViBanThan))
	union
	select distinct iID_MaDonVi IIdMaDonVi from #basedatachi where iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))) dv
	join DonVi donvi on dv.IIdMaDonVi = donvi.iID_MaDonVi and donvi.iNamLamViec = @YearOfWork and donvi.iTrangThai = 1 and donvi.iLoai <> 0

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]    Script Date: 11/29/2024 4:01:14 PM ******/
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
	where ct.NamNganSach = @YearOfBudget
		and convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)

	select * into #basedatathudonvi from #basedatathu 
	where Id_DonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ctct.XauNoiMa, ctct.iPhanCap, ctct.NguonNganSach, sum(isnull(ctct.TuChi, 0)) TuChi 
	into #basedatathudutoan
	from TN_DT_ChungTuChiTiet ctct
	where ctct.Id_ChungTu in (select distinct Id_DotNhan from #basedatathudonvi)
	and ctct.NamLamViec = @YearOfWork
	group by ctct.XauNoiMa, ctct.iPhanCap, ctct.NguonNganSach
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	where ct.iNamNganSach = @YearOfBudget
		and convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)

	select * into #basedatachidonvi from #basedatachi 
	where iID_MaDonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ctct.sXauNoiMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach, sum(isnull(ctct.fTuChi, 0)) fTuChi 
	into #basedatachidutoan
	from NS_DT_ChungTuChiTiet ctct
	where ctct.iID_DTChungTu in (select distinct iID_CTDuToan_Nhan from #basedatachidonvi)
	and ctct.iNamLamViec = @YearOfWork
	group by ctct.sXauNoiMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach

	--Data Ban than

	CREATE TABLE #databanthan (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sMoTa nvarchar(max),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	bHangCha bit,
	FDuToanNSDuocGiao float,
	FSoPhanBoBanThan float,
	sLoai nvarchar(50));

	insert into #databanthan (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, sLoai)
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
		   mlns.bHangChaDuToan,
		   0 fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathudonvi donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		where mlns.sLNS like '8%'
		and donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	insert into #databanthan(IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, sLoai)
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
		   mlns.bHangChaDuToan,
		   0 fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachidonvi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	CREATE TABLE #datadonvi (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	FDuToanNSDuocGiao float,
	FSoPhanBo float,
	sLoai nvarchar(50));

	--Data Don vi
	insert into #datadonvi (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBo, sLoai)
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
		   mlns.bHangChaDuToan,
		   dtnsdg.TuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBo,
		   'THU' sLoai
		from #basemlns mlns
		left join (select * from #basedatathudonvi where Id_donvi not in (SELECT * FROM f_split(@DonViBanThan))) donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathudutoan dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		where mlns.sLNS like '8%'
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, dtnsdg.TuChi

	insert into #datadonvi (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBo, sLoai)
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
		   mlns.bHangChaDuToan,
		   dtnsdg.fTuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBo,
		   'CHI' sLoai
		from #basemlns mlns
		left join (select * from #basedatachidonvi where iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))) donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		left join #basedatachidutoan dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, dtnsdg.fTuChi

	--Ket qua
	CREATE TABLE #dataketqua (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	FDuToanNSDuocGiao float,
	FSoPhanBoBanThan float,
	FSoPhanBo float,
	sLoai nvarchar(50));

	insert into #dataketqua (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo, sLoai)
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
		mlns.bHangChaDuToan,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'THU' sLoai
	from #basemlns mlns
	left join #datadonvi donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'THU'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'THU'
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	insert into #dataketqua (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo, sLoai)
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
		mlns.bHangChaDuToan,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'CHI' sLoai
	from #basemlns mlns
	left join #datadonvi donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'CHI'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'CHI'
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	select
		IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, 
		FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo,
		isnull(FSoPhanBoBanThan, 0) + isnull(FSoPhanBo, 0) FTongSoPhanBo,
		sLoai
	from #dataketqua
	where isnull(FDuToanNSDuocGiao, 0) <> 0 or isnull(FSoPhanBoBanThan, 0) <> 0 or isnull(FSoPhanBo, 0) <> 0

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]    Script Date: 11/29/2024 4:01:14 PM ******/
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
	where ct.NamNganSach = @YearOfBudget
		and convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.Id_DonVi in (SELECT * FROM f_split(@MaDonVi))

	select * into #basedatathudonvi from #basedatathu 
	where Id_DonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ctct.XauNoiMa, ctct.iPhanCap, ctct.NguonNganSach, sum(isnull(ctct.TuChi, 0)) TuChi 
	into #basedatathudutoan
	from TN_DT_ChungTuChiTiet ctct
	where ctct.Id_ChungTu in (select distinct Id_DotNhan from #basedatathudonvi)
	and ctct.NamLamViec = @YearOfWork
	group by ctct.XauNoiMa, ctct.iPhanCap, ctct.NguonNganSach
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
	where ct.iNamNganSach = @YearOfBudget
		and convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))

	select * into #basedatachidonvi from #basedatachi 
	where iID_MaDonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ctct.sXauNoiMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach, sum(isnull(ctct.fTuChi, 0)) fTuChi 
	into #basedatachidutoan
	from NS_DT_ChungTuChiTiet ctct
	where ctct.iID_DTChungTu in (select distinct iID_CTDuToan_Nhan from #basedatachidonvi)
	and ctct.iNamLamViec = @YearOfWork
	group by ctct.sXauNoiMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach

	--Data Ban than
	CREATE TABLE #databanthan (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sMoTa nvarchar(max),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	bHangCha bit,
	FDuToanNSDuocGiao float,
	FChoPhanBo float,
	FSoPhanBoBanThan float,
	sLoai nvarchar(50));

	insert into #databanthan (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FChoPhanBo, FSoPhanBoBanThan, sLoai)
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
			mlns.bHangChaDuToan,
		   0 fDuToanNSDuocGiao,
		   0 FChoPhanBo,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathudonvi donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		where mlns.sLNS like '8%'
		and donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	insert into #databanthan (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FChoPhanBo, FSoPhanBoBanThan, sLoai)
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
		   mlns.bHangChaDuToan,
		   0 fDuToanNSDuocGiao,
		   0 fChoPhanBo,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachidonvi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	CREATE TABLE #datadonvi1 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	FSoPhanBo float,
	sLoai nvarchar(50));

	--Data Don vi
	insert into #datadonvi1 (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, FSoPhanBo, sLoai)
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
		   mlns.bHangChaDuToan,
		  dtnsdg.TuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBo,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathudonvi donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathudutoan dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		where mlns.sLNS like '8%'
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, dtnsdg.TuChi

		insert into #datadonvi1 (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, FSoPhanBo, sLoai)

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
		   mlns.bHangChaDuToan,
		   dtnsdg.fTuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBo,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachidonvi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa 
		left join #basedatachidutoan dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, dtnsdg.fTuChi
	
	--Ket qua
	CREATE TABLE #dataketqua (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	FDuToanNSDuocGiao float,
	FSoPhanBoBanThan float,
	FSoPhanBo float,
	sLoai nvarchar(50));

	insert into #dataketqua (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo, sLoai)
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
		mlns.bHangChaDuToan,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'THU' sLoai
	from #basemlns mlns
	left join #datadonvi1 donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'THU'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'THU'
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	insert into #dataketqua (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo, sLoai)
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
		mlns.bHangChaDuToan,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'CHI' sLoai
	from #basemlns mlns
	left join #datadonvi1 donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'CHI'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'CHI'
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	select
		IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, 
		FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo,
		isnull(FSoPhanBoBanThan, 0) + isnull(FSoPhanBo, 0) FTongSoPhanBo,
		sLoai
	from #dataketqua
	where isnull(FDuToanNSDuocGiao, 0) <> 0 or isnull(FSoPhanBoBanThan, 0) <> 0 or isnull(FSoPhanBo, 0) <> 0

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhan_so_kiem_tra_1]    Script Date: 11/29/2024 4:01:14 PM ******/
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
	@UserName nvarchar(100),
	@loaiNguonNganSach int
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
		ct.iLoaiNguonNganSach,
		ct.is_Sent
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
				AND (@loaiNguonNganSach = 0 OR ct.iLoaiNguonNganSach = @loaiNguonNganSach)
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
						AND EXISTS (SELECT * FROM NS_SKT_MucLuc WHERE sKyHieu = ctct.sKyHieu AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap)))
					)
					OR 
					(
						EXISTS 
						(
							SELECT * FROM (SELECT * FROM NS_SKT_MucLuc WHERE sKyHieu = ctct.sKyHieu AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap))) ml
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
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel]    Script Date: 12/3/2024 9:31:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel]
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
	where ct.NamNganSach = @YearOfBudget
		and convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.Id_DonVi in (SELECT * FROM f_split(@MaDonVi))

	select * into #basedatathudonvi from #basedatathu 
	where Id_DonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ctct.XauNoiMa, ctct.iPhanCap, ctct.NguonNganSach, sum(isnull(ctct.TuChi, 0)) TuChi 
	into #basedatathudutoan
	from TN_DT_ChungTuChiTiet ctct
	where ctct.Id_ChungTu in (select distinct Id_DotNhan from #basedatathudonvi)
	and ctct.NamLamViec = @YearOfWork
	group by ctct.XauNoiMa, ctct.iPhanCap, ctct.NguonNganSach
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
	where ct.iNamNganSach = @YearOfBudget
		and convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))

	select * into #basedatachidonvi from #basedatachi 
	where iID_MaDonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ctct.sXauNoiMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach, sum(isnull(ctct.fTuChi, 0)) fTuChi 
	into #basedatachidutoan
	from NS_DT_ChungTuChiTiet ctct
	where ctct.iID_DTChungTu in (select distinct iID_CTDuToan_Nhan from #basedatachidonvi)
	and ctct.iNamLamViec = @YearOfWork
	group by ctct.sXauNoiMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach

	--Data Ban than
	CREATE TABLE #databanthan (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sMoTa nvarchar(max),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	bHangCha bit,
	FDuToanNSDuocGiao float,
	FChoPhanBo float,
	FSoPhanBoBanThan float,
	sLoai nvarchar(50));

	insert into #databanthan (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FChoPhanBo, FSoPhanBoBanThan, sLoai)
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
			mlns.bHangChaDuToan,
		   0 fDuToanNSDuocGiao,
		   0 FChoPhanBo,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathudonvi donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		where mlns.sLNS like '8%'
		and donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	insert into #databanthan (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FChoPhanBo, FSoPhanBoBanThan, sLoai)
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
		   mlns.bHangChaDuToan,
		   0 fDuToanNSDuocGiao,
		   0 fChoPhanBo,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachidonvi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan

	--Data Don vi
	CREATE TABLE #datadonvi1 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	FSoPhanBo float,
	sLoai nvarchar(50),
	IIdMaDonVi nvarchar(500));

	insert into #datadonvi1 (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, FSoPhanBo, sLoai, IIdMaDonVi)
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
		   mlns.bHangChaDuToan,
		  dtnsdg.TuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBo,
		   'THU' sLoai,
		   donvi.Id_DonVi
		from #basemlns mlns
		left join #basedatathudonvi donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathudutoan dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		where mlns.sLNS like '8%'
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, dtnsdg.TuChi, donvi.Id_DonVi

		insert into #datadonvi1 (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, FSoPhanBo, sLoai, IIdMaDonVi)

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
		   mlns.bHangChaDuToan,
		   dtnsdg.fTuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBo,
		   'CHI' sLoai,
		   donvi.iID_MaDonVi
		from #basemlns mlns
		left join #basedatachidonvi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa 
		left join #basedatachidutoan dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, dtnsdg.fTuChi, donvi.iID_MaDonVi
	
	--Ket qua
	CREATE TABLE #dataketqua (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	FDuToanNSDuocGiao float,
	FSoPhanBoBanThan float,
	FSoPhanBo float,
	sLoai nvarchar(50),
	IIdMaDonVi nvarchar(500));

	insert into #dataketqua (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo, sLoai, IIdMaDonVi)
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
		mlns.bHangChaDuToan,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		banthan.FSoPhanBoBanThan FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'THU' sLoai,
		donvi.IIdMaDonVi
	from #basemlns mlns
	left join #datadonvi1 donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'THU'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'THU'
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, donvi.IIdMaDonVi, banthan.FSoPhanBoBanThan

	insert into #dataketqua (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa, sL, sK, sM, sTM, sTTM, sNG, bHangCha, FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo, sLoai, IIdMaDonVi)
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
		mlns.bHangChaDuToan,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		banthan.FSoPhanBoBanThan FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'CHI' sLoai,
		donvi.IIdMaDonVi
	from #basemlns mlns
	left join #datadonvi1 donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'CHI'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'CHI'
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS, mlns.sMoTa, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.bHangChaDuToan, donvi.IIdMaDonVi, banthan.FSoPhanBoBanThan

	select
		IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, 
		FDuToanNSDuocGiao, FSoPhanBoBanThan, FSoPhanBo,
		isnull(FSoPhanBoBanThan, 0) + isnull(FSoPhanBo, 0) FTongSoPhanBo,
		sLoai, IIdMaDonVi
	from #dataketqua
	where isnull(FDuToanNSDuocGiao, 0) <> 0 or isnull(FSoPhanBoBanThan, 0) <> 0 or isnull(FSoPhanBo, 0) <> 0

END
;
GO
