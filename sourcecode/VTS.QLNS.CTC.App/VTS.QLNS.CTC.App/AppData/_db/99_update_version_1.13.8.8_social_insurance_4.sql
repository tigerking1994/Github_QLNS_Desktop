/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khc]    Script Date: 1/17/2024 8:16:49 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_khc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_khc]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khc]    Script Date: 1/17/2024 8:16:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_get_data_khc]
	@NamLamViec int,
	@SoChungTu nvarchar(500),
	@SLNS nvarchar(500)
AS
BEGIN
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		ctct.fTienKeHoachThucHienNamNay
		into #temp
	from
	BH_DM_MucLucNganSach mlns
	left join
	BH_KHC_CheDoBHXH_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_CheDoBHXH
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_CheDoBHXH = ct.id
	--join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	----------------
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		ctct.fTienKeHoachThucHienNamNay
	from
	 BH_DM_MucLucNganSach mlns  
	 left join 
	BH_KHC_K_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_K
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_K = ct.iID_BH_KHC_K
	--join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	------------------
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		ctct.fTienKeHoachThucHienNamNay
	from
	 BH_DM_MucLucNganSach mlns 
	left join
	BH_KHC_KCB_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_KCB
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_KCB = ct.iID_BH_KHC_KCB
	
	----------------
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		ctct.fTienKeHoachThucHienNamNay
	from
	 BH_DM_MucLucNganSach mlns 
	left join BH_KHC_KinhPhiQuanLy_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	join
	(select * from BH_KHC_KinhPhiQuanLy
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_KinhPhiQuanLy = ct.iID_BH_KHC_KinhPhiQuanLy


	if	@SLNS='9010001,9010002'
		select SUBSTRING(A.sXauNoiMa,1,20) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.fTienKeHoachThucHienNamNay) fTienKeHoachThucHienNamNay
		from #temp A
		Group by SUBSTRING(A.sXauNoiMa,1,20),A.IIdMaDonVi
		order by SUBSTRING(A.sXauNoiMa,1,20)
	else if @SLNS='905,9050001,9050002'
	select SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.fTienKeHoachThucHienNamNay) fTienKeHoachThucHienNamNay
		from #temp A
		Group by SUBSTRING(A.sXauNoiMa,1,3),A.IIdMaDonVi
		order by SUBSTRING(A.sXauNoiMa,1,3)
	else if @SLNS='9010004'
	select SUBSTRING(A.sXauNoiMa,1,7) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.fTienKeHoachThucHienNamNay) fTienKeHoachThucHienNamNay
		from #temp A
		Group by SUBSTRING(A.sXauNoiMa,1,7),A.IIdMaDonVi
		order by SUBSTRING(A.sXauNoiMa,1,7)
	else if @SLNS='9010006'
	select SUBSTRING(A.sXauNoiMa,1,7) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.fTienKeHoachThucHienNamNay) fTienKeHoachThucHienNamNay
		from #temp A
		Group by SUBSTRING(A.sXauNoiMa,1,7),A.IIdMaDonVi
		order by SUBSTRING(A.sXauNoiMa,1,7)
	else 
	select SUBSTRING(A.sXauNoiMa,1,7) sXauNoiMa ,A.IIdMaDonVi, 
		SUM(A.fTienKeHoachThucHienNamNay) fTienKeHoachThucHienNamNay
		from #temp A
		Group by SUBSTRING(A.sXauNoiMa,1,7),A.IIdMaDonVi
		order by SUBSTRING(A.sXauNoiMa,1,7)
	--select dm.* , tbl.fTienKeHoachThucHienNamNay, tbl.IIdMaDonVi from BH_DM_MucLucNganSach  dm
	--left join 
	--#temp tbl on dm.iID_MLNS=tbl.IID_MucLucNganSach
	--where iNamLamViec=@NamLamViec and sLNS in (select * from splitstring(@SLNS))
	--order by dm.sXauNoiMa 

	drop table #temp

END
;
;
GO
