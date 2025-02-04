/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]    Script Date: 12/17/2024 10:18:16 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel]    Script Date: 12/17/2024 10:18:16 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]    Script Date: 12/17/2024 10:18:16 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]    Script Date: 12/17/2024 10:18:16 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]    Script Date: 12/17/2024 10:18:16 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]    Script Date: 12/17/2024 10:18:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]
	@MaCongKhai nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@DVT int
AS
BEGIN
	
	declare @CapDonVi int = (select top 1 iCapDonVi from DonVi where iLoai = 0 and inamlamviec = @YearOfWork)

	declare @DsDonViBanThan nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iCapDonVi = @CapDonVi and iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai <> 0
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	declare @DsDonViDuToan nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iKhoi = 2 and inamlamviec = @YearOfWork and iTrangThai = 1 and iLoai <> 0
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	declare @DsDonViDoanhNghiep nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iKhoi = 1 and inamlamviec = @YearOfWork and iTrangThai = 1 and iLoai <> 0
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	declare @DsDonViBVTC nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iKhoi = 3 and inamlamviec = @YearOfWork and iTrangThai = 1 and iLoai <> 0
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	select ck.* into #basemlns
	from NS_DanhMucCongKhai ck
	where (ck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@MaCongKhai)) OR (ck.Id IN (SELECT * FROM f_split(@MaCongKhai))))
		and ck.iNamLamViec = @YearOfWork
	
	--Thu
	select ctct.*, ck.sMa sMaMlck into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ctct.Id_ChungTu = ct.Id
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.XauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.NamLamViec
	where convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.LNS like '8%'
		and ct.NamLamViec = @YearOfWork

	select * into #basedatathudonvi from #basedatathu 
	where Id_DonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ck.sMa sMaMlck, ctct.iPhanCap, ctct.NguonNganSach, sum(isnull(ctct.TuChi, 0)) TuChi 
	into #basedatathudutoan
	from TN_DT_ChungTuChiTiet ctct
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.XauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.NamLamViec
	where ctct.Id_ChungTu in (select distinct Id_DotNhan from #basedatathudonvi)
	and ctct.NamLamViec = @YearOfWork
	group by ck.sMa, ctct.iPhanCap, ctct.NguonNganSach
	------------------------------------------------------------------------	
	--Chi
	select ctct.*, ck.sMa sMaMlck into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.sXauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.iNamLamViec
	where convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.sLNS not like '8%'
		and ct.iNamLamViec = @YearOfWork

	select * into #basedatachidonvi from #basedatachi 
	where iID_MaDonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ck.sMa sMaMlck, ctct.iPhanCap, ctct.iID_MaNguonNganSach, sum(isnull(ctct.fTuChi, 0)) fTuChi 
	into #basedatachidutoan
	from NS_DT_ChungTuChiTiet ctct
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.sXauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.iNamLamViec
	where ctct.iID_DTChungTu in (select distinct iID_CTDuToan_Nhan from #basedatachidonvi)
	and ctct.iNamLamViec = @YearOfWork
	group by ck.sMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach

	CREATE TABLE #datapl1(
	IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sMa nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fChiTaiBanThan float,
	fKhoiDuToan float,
	fKhoiDoanhNghiep float,
	fBVTC float,
	sLoai nvarchar(50));

	insert into #datapl1(IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fChiTaiBanThan, fKhoiDuToan, fKhoiDoanhNghiep, fBVTC, sLoai)
	select
       mlns.Id IIdMlns,
       mlns.iID_DMCongKhai_Cha IIdMlnsCha,
       mlns.sMa,
       concat(mlns.STT,'. ', mlns.sMoTa) sMoTa,
       mlns.bHangCha,
	   dtnsdg.TuChi fDuToanNSDuocGiao,
	   sum(isnull(ctbt.fChiTaiBanThan, 0)) fChiTaiBanThan,
	   sum(isnull(dt.fKhoiDuToan, 0)) fKhoiDuToan,
	   sum(isnull(dn.fKhoiDoanhNghiep, 0)) fKhoiDoanhNghiep,
	   sum(isnull(bvtc.fBVTC, 0)) fBVTC,
	   'THU' sLoai
	from #basemlns mlns
	left join #basedatathudutoan dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
	left join (select ctbt.sMaMlck, sum(isnull(ctbt.TuChi, 0)) fChiTaiBanThan
		from #basedatathudonvi ctbt
		where ctbt.iPhanCap = 1 and ctbt.NguonNganSach = @BudgetSource and ctbt.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by ctbt.sMaMlck
	) ctbt ON mlns.sMa = ctbt.sMaMlck
		
	left join (select dt.sMaMlck, sum(isnull(dt.TuChi, 0)) fKhoiDuToan 
		from #basedatathudonvi dt
		where dt.iPhanCap = 1 and dt.NguonNganSach = @BudgetSource and dt.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonViDuToan)) and dt.Id_DonVi not in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by dt.sMaMlck
	) dt ON mlns.sMa = dt.sMaMlck
	left join (select dn.sMaMlck, sum(isnull(dn.TuChi, 0)) fKhoiDoanhNghiep 
		from #basedatathudonvi dn
		where dn.iPhanCap = 1 and dn.NguonNganSach = @BudgetSource and dn.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonViDoanhNghiep)) and dn.Id_DonVi not in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by dn.sMaMlck
	) dn ON mlns.sMa = dn.sMaMlck 
	left join (select bvtc.sMaMlck, sum(isnull(bvtc.TuChi, 0)) fBVTC 
		from #basedatathudonvi bvtc
		where bvtc.iPhanCap = 1 and bvtc.NguonNganSach = @BudgetSource and bvtc.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonViBVTC)) and bvtc.Id_DonVi not in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by bvtc.sMaMlck
	) bvtc ON mlns.sMa = bvtc.sMaMlck
	group by mlns.Id, mlns.iID_DMCongKhai_Cha, mlns.sMa, concat(mlns.STT,'. ', mlns.sMoTa), mlns.bHangCha, dtnsdg.TuChi

	insert into #datapl1(IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fChiTaiBanThan, fKhoiDuToan, fKhoiDoanhNghiep, fBVTC, sLoai)
	select
      mlns.Id IIdMlns,
       mlns.iID_DMCongKhai_Cha IIdMlnsCha,
       mlns.sMa,
       concat(mlns.STT,'. ', mlns.sMoTa) sMoTa,
       mlns.bHangCha,
	   dtnsdg.fTuChi fDuToanNSDuocGiao,
	   sum(isnull(ctbt.fChiTaiBanThan, 0)) fChiTaiBanThan,
	   sum(isnull(dt.fKhoiDuToan, 0)) fKhoiDuToan,
	   sum(isnull(dn.fKhoiDoanhNghiep, 0)) fKhoiDoanhNghiep,
	   sum(isnull(bvtc.fBVTC, 0)) fBVTC,
	   'CHI' sLoai
	from #basemlns mlns
	left join #basedatachidutoan dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
	left join (select ctbt.sMaMlck, sum(isnull(ctbt.fTuChi, 0)) fChiTaiBanThan
		from #basedatachidonvi ctbt
		where ctbt.iPhanCap = 1 and ctbt.iID_MaNguonNganSach = @BudgetSource and ctbt.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by ctbt.sMaMlck
	) ctbt ON mlns.sMa = ctbt.sMaMlck
		
	left join (select dt.sMaMlck, sum(isnull(dt.fTuChi, 0)) fKhoiDuToan 
		from #basedatachidonvi dt
		where dt.iPhanCap = 1 and dt.iID_MaNguonNganSach = @BudgetSource and dt.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonViDuToan)) and dt.iID_MaDonVi not in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by dt.sMaMlck
	) dt ON mlns.sMa = dt.sMaMlck
	left join (select dn.sMaMlck, sum(isnull(dn.fTuChi, 0)) fKhoiDoanhNghiep 
		from #basedatachidonvi dn
		where dn.iPhanCap = 1 and dn.iID_MaNguonNganSach = @BudgetSource and dn.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonViDoanhNghiep)) and dn.iID_MaDonVi not in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by dn.sMaMlck
	) dn ON mlns.sMa = dn.sMaMlck 
	left join (select bvtc.sMaMlck, sum(isnull(bvtc.fTuChi, 0)) fBVTC 
		from #basedatachidonvi bvtc
		where bvtc.iPhanCap = 1 and bvtc.iID_MaNguonNganSach = @BudgetSource and bvtc.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonViBVTC)) and bvtc.iID_MaDonVi not in (SELECT * FROM dbo.f_split(@DsDonViBanThan))
		group by bvtc.sMaMlck
	) bvtc ON mlns.sMa = bvtc.sMaMlck
	group by mlns.Id, mlns.iID_DMCongKhai_Cha, mlns.sMa, concat(mlns.STT,'. ', mlns.sMoTa), mlns.bHangCha, dtnsdg.fTuChi

	select
		IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha,
		fDuToanNSDuocGiao/@DVT fDuToanNSDuocGiao,
		fChiTaiBanThan/@DVT fChiTaiBanThan,
		fKhoiDuToan/@DVT fKhoiDuToan,
		fKhoiDoanhNghiep/@DVT fKhoiDoanhNghiep,
		fBVTC/@DVT fBVTC,
		sLoai
	from #datapl1

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]    Script Date: 12/17/2024 10:18:16 AM ******/
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
		and ct.NamLamViec = @YearOfWork

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
		and ct.iNamLamViec = @YearOfWork

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
		   sum(isnull(donvi.TuChi, 0)) FSoPhanBoBanThan,
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
		   sum(isnull(donvi.fTuChi, 0)) FSoPhanBoBanThan,
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
		   sum(isnull(donvi.TuChi, 0)) FSoPhanBo,
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
		   sum(isnull(donvi.fTuChi, 0)) FSoPhanBo,
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
		FDuToanNSDuocGiao/@DVT FDuToanNSDuocGiao, FSoPhanBoBanThan/@DVT FSoPhanBoBanThan, FSoPhanBo/@DVT FSoPhanBo,
		(isnull(FSoPhanBoBanThan, 0) + isnull(FSoPhanBo, 0))/@DVT FTongSoPhanBo,
		sLoai
	from #dataketqua
	where isnull(FDuToanNSDuocGiao, 0) <> 0 or isnull(FSoPhanBoBanThan, 0) <> 0 or isnull(FSoPhanBo, 0) <> 0

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]    Script Date: 12/17/2024 10:18:16 AM ******/
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
		and ct.NamLamViec = @YearOfWork

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
		and ct.iNamLamViec = @YearOfWork

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
		   sum(isnull(donvi.TuChi, 0)) FSoPhanBoBanThan,
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
		   sum(isnull(donvi.fTuChi, 0)) FSoPhanBoBanThan,
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
		   sum(isnull(donvi.TuChi, 0)) FSoPhanBo,
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
		   sum(isnull(donvi.fTuChi, 0)) FSoPhanBo,
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
		FDuToanNSDuocGiao/@DVT FDuToanNSDuocGiao, FSoPhanBoBanThan/@DVT FSoPhanBoBanThan, FSoPhanBo/@DVT FSoPhanBo,
		(isnull(FSoPhanBoBanThan, 0) + isnull(FSoPhanBo, 0))/@DVT FTongSoPhanBo,
		sLoai
	from #dataketqua
	where isnull(FDuToanNSDuocGiao, 0) <> 0 or isnull(FSoPhanBoBanThan, 0) <> 0 or isnull(FSoPhanBo, 0) <> 0

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel]    Script Date: 12/17/2024 10:18:16 AM ******/
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
		and ct.NamLamViec = @YearOfWork

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
		and ct.iNamLamViec = @YearOfWork

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
		   sum(isnull(donvi.TuChi, 0)) FSoPhanBoBanThan,
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
		   sum(isnull(donvi.fTuChi, 0)) FSoPhanBoBanThan,
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
		   sum(isnull(donvi.TuChi, 0)) FSoPhanBo,
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
		   sum(isnull(donvi.fTuChi, 0)) FSoPhanBo,
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
		FDuToanNSDuocGiao/@DVT FDuToanNSDuocGiao, FSoPhanBoBanThan/@DVT FSoPhanBoBanThan, FSoPhanBo/@DVT FSoPhanBo,
		(isnull(FSoPhanBoBanThan, 0) + isnull(FSoPhanBo, 0))/@DVT FTongSoPhanBo,
		sLoai, IIdMaDonVi
	from #dataketqua
	where isnull(FDuToanNSDuocGiao, 0) <> 0 or isnull(FSoPhanBoBanThan, 0) <> 0 or isnull(FSoPhanBo, 0) <> 0

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]    Script Date: 12/17/2024 10:18:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]
	@MaCongKhai nvarchar(max),
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

	select ck.* into #basemlns
	from NS_DanhMucCongKhai ck
	where (ck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@MaCongKhai)) OR (ck.Id IN (SELECT * FROM f_split(@MaCongKhai))))
		and ck.iNamLamViec = @YearOfWork

	--Thu
	select ctct.*, ck.sMa sMaMlck into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ctct.Id_ChungTu = ct.Id
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.XauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.NamLamViec
	where convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.LNS like '8%'
		and ct.NamLamViec = @YearOfWork

	select * into #basedatathudonvi from #basedatathu 
	where Id_DonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ck.sMa sMaMlck, ctct.iPhanCap, ctct.NguonNganSach, sum(isnull(ctct.TuChi, 0)) TuChi 
	into #basedatathudutoan
	from TN_DT_ChungTuChiTiet ctct
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.XauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.NamLamViec
	where ctct.Id_ChungTu in (select distinct Id_DotNhan from #basedatathudonvi)
	and ctct.NamLamViec = @YearOfWork
	group by ck.sMa, ctct.iPhanCap, ctct.NguonNganSach
	------------------------------------------------------------------------	
	--Chi
	select ctct.*, ck.sMa sMaMlck into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.sXauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.iNamLamViec
	where convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.sLNS not like '8%'
		and ct.iNamLamViec = @YearOfWork

	select * into #basedatachidonvi from #basedatachi 
	where iID_MaDonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select distinct ck.sMa sMaMlck, ctct.iPhanCap, ctct.iID_MaNguonNganSach, sum(isnull(ctct.fTuChi, 0)) fTuChi 
	into #basedatachidutoan
	from NS_DT_ChungTuChiTiet ctct
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.sXauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.iNamLamViec
	where ctct.iID_DTChungTu in (select distinct iID_CTDuToan_Nhan from #basedatachidonvi)
	and ctct.iNamLamViec = @YearOfWork
	group by ck.sMa, ctct.iPhanCap, ctct.iID_MaNguonNganSach

	CREATE TABLE #databanthanm02 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sMa nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fSoPhanBoBanThan float,
	sLoai nvarchar(50));

	--Data Ban than
	insert into #databanthanm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, sLoai)
		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0)) fSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathudonvi donvi ON mlns.sMa = donvi.sMaMlck
		where donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha

		insert into #databanthanm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, sLoai)
		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0)) FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachidonvi donvi ON mlns.sMa = donvi.sMaMlck
		where donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha

	CREATE TABLE #datadonvim02 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sMa nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fSoPhanBo float,
	sLoai nvarchar(50));

	--Data Don vi
		insert into #datadonvim02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBo, sLoai)
		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   dtnsdg.TuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/1 FSoPhanBo,
		   'THU' sLoai
		from #basemlns mlns
		left join (select * from #basedatathudonvi where Id_donvi not in (SELECT * FROM f_split(@DonViBanThan))) donvi ON mlns.sMa = donvi.sMaMlck
		left join #basedatathudutoan dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = 1
		--where donvi.Id_donvi not in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha,
			dtnsdg.TuChi

		insert into #datadonvim02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBo, sLoai)

		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   dtnsdg.fTuChi fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/1 FSoPhanBo,
		   'CHI' sLoai
		from #basemlns mlns
		left join (select * from #basedatachidonvi where iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))) donvi ON mlns.sMa = donvi.sMaMlck
		left join #basedatachidutoan dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = 1
		--where donvi.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha,
			dtnsdg.fTuChi

	--Ket qua

	CREATE TABLE #tblresultm02 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sMa nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fSoPhanBoBanThan float,
	fSoPhanBo float,
	sLoai nvarchar(50),
	iLoai int,
	iRoot int,
	hasData bit);

	--A. DỰ TOÁN THU
	--insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, FSoPhanBo, sLoai, iLoai, iRoot, hasData)
	--select newid() IIdMlns, '00000000-0000-0000-0000-000000000000' IIdMlnsCha, null sMa, N'A. DỰ TOÁN THU' sMoTa, 1 bHangCha, 0 fDuToanNSDuocGiao, 0 fSoPhanBoBanThan, 0 fSoPhanBo, 'THU' sLoai, 1 iLoai, 1 iRoot, 1 hasData
	
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select
		mlns.Id IIdMlns,
		mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		mlns.sMa,
		concat(mlns.STT,'. ', mlns.sMoTa),
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) fSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'THU' sLoai,
		1 iLoai,
		2 iRoot,
		1 hasData
	from #basemlns mlns
	left join #datadonvim02 donvi on mlns.sMa = donvi.sMa and donvi.sLoai = 'THU'
	left join #databanthanm02 banthan on mlns.sMa = banthan.sMa and banthan.sLoai = 'THU'
	group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			concat(mlns.STT,'. ', mlns.sMoTa),
			mlns.bHangCha
	
	--B. DỰ TOÁN CHI
	--insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, sLoai, iLoai, iRoot, hasData)
	--select newid() IIdMlns, '00000000-0000-0000-0000-000000000000' IIdMlnsCha, null sMa, N'B. DỰ TOÁN CHI' sMoTa, 1 bHangCha, 0 fDuToanNSDuocGiao, 0 FSoPhanBoBanThan, 0 FSoPhanBo, 'CHI' sLoai, 2 iLoai, 1 iRoot, 1 hasData
	
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select
		mlns.Id IIdMlns,
		mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		mlns.sMa,
		concat(mlns.STT,'. ', mlns.sMoTa) sMoTa,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		'CHI' sLoai,
		2 iLoai,
		2 iRoot,
		1 hasData
	from #basemlns mlns
	left join #datadonvim02 donvi on mlns.sMa = donvi.sMa and donvi.sLoai = 'CHI'
	left join #databanthanm02 banthan on mlns.sMa = banthan.sMa and banthan.sLoai = 'CHI'
	group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			concat(mlns.STT,'. ', mlns.sMoTa),
			mlns.bHangCha

	update #tblresultm02 set hasData = 0
	where (fDuToanNSDuocGiao = 0 and FSoPhanBoBanThan = 0 and FSoPhanBo = 0 and iRoot = 2)
	--or (iRoot = 1 and iLoai not in (select distinct iLoai from #tblresultm02
	--			where fDuToanNSDuocGiao <> 0 or FSoPhanBoBanThan <> 0 or FSoPhanBo <> 0 or iRoot = 2))

	select
		IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha,
		fDuToanNSDuocGiao/@DVT fDuToanNSDuocGiao,
		fSoPhanBoBanThan/@DVT fSoPhanBoBanThan,
		fSoPhanBo/@DVT fSoPhanBo,
		sLoai, iLoai, iRoot, hasData
	from #tblresultm02 where hasData = 1
	order by iLoai, iRoot
END
;
;
;
GO
