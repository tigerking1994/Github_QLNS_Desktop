/****** Object:  StoredProcedure [dbo].[sp_ns_get_skt_chungtuchitiet1]    Script Date: 6/5/2024 5:22:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_get_skt_chungtuchitiet1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_get_skt_chungtuchitiet1]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_get_skt_chungtuchitiet1]    Script Date: 6/5/2024 5:22:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_get_skt_chungtuchitiet1]
	@YearOfWork int,
	@iID_MaBQuanLy nvarchar(200),
	@NguonNganSach int 
	
AS
BEGIN
	SET NOCOUNT ON;
	if(@NguonNganSach = 1) -- DU TOAN
	BEGIN
		select distinct sLNS into #temp1 
			from NS_MucLucNganSach dm 
			where 
			iNamLamViec = @YearOfWork 
			and dm.iID_MaBQuanLy = @iID_MaBQuanLy
			AND (sXauNoiMa NOT LIKE '1010100%' AND sXauNoiMa NOT LIKE '1020900%' AND sXauNoiMa NOT LIKE '1020902%'
					AND sXauNoiMa NOT LIKE '1010400%' AND sXauNoiMa NOT LIKE '1020600%' AND sXauNoiMa NOT LIKE '105%')


		select distinct mlskt.sKyHieu sSKT_KyHieu
		from NS_MLSKT_MLNS map
		inner join NS_MucLucNganSach ns
		on ns.sXauNoiMa = map.sNS_XauNoiMa
		inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
		where ns.sLNS in (select * from #temp1)
		and ns.iNamLamViec = @YearOfWork
		and map.iNamLamViec = @YearOfWork
		and mlskt.iNamLamViec = @YearOfWork		
		order by sSKT_KyHieu
		DROP TABLE #temp1;
	END
	ELSE IF(@NguonNganSach = 2) --BENH_VIEN
	BEGIN
	select distinct sLNS into #temp2 
			from NS_MucLucNganSach dm 
			where 
			iNamLamViec = @YearOfWork 
			and dm.iID_MaBQuanLy = @iID_MaBQuanLy
			AND (sXauNoiMa LIKE '1010100%' OR sXauNoiMa LIKE '1020900%' OR sXauNoiMa LIKE '1020902%')


		select distinct mlskt.sKyHieu sSKT_KyHieu
		from NS_MLSKT_MLNS map
		inner join NS_MucLucNganSach ns
		on ns.sXauNoiMa = map.sNS_XauNoiMa
		inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
		where ns.sLNS in (select * from #temp2)
		and ns.iNamLamViec = @YearOfWork
		and map.iNamLamViec = @YearOfWork
		and mlskt.iNamLamViec = @YearOfWork		
		order by sSKT_KyHieu
		DROP TABLE #temp2;
	END
	ELSE IF (@NguonNganSach = 3) -- DOANH_NGHIEP
	BEGIN
		select distinct sLNS into #temp3
			from NS_MucLucNganSach dm 
			where 
			iNamLamViec = @YearOfWork 
			and dm.iID_MaBQuanLy = @iID_MaBQuanLy
			AND (sXauNoiMa LIKE '1010400%' OR sXauNoiMa LIKE '1020600%' OR sXauNoiMa LIKE '105%')
		select distinct mlskt.sKyHieu sSKT_KyHieu
		from NS_MLSKT_MLNS map
		inner join NS_MucLucNganSach ns
		on ns.sXauNoiMa = map.sNS_XauNoiMa
		inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
		where ns.sLNS in (select * from #temp3)
		and ns.iNamLamViec = @YearOfWork
		and map.iNamLamViec = @YearOfWork
		and mlskt.iNamLamViec = @YearOfWork		
		order by sSKT_KyHieu;
		DROP TABLE #temp3;
	END
	ELSE
	BEGIN
		select distinct sLNS into #temp from NS_MucLucNganSach dm where iNamLamViec = @YearOfWork and (@iID_MaBQuanLy = '0' or dm.iID_MaBQuanLy = @iID_MaBQuanLy)
		select distinct mlskt.sKyHieu sSKT_KyHieu
		from NS_MLSKT_MLNS map
		inner join NS_MucLucNganSach ns
		on ns.sXauNoiMa = map.sNS_XauNoiMa
		inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
		where ns.sLNS in (select * from #temp)
		and ns.iNamLamViec = @YearOfWork
		and map.iNamLamViec = @YearOfWork
		and mlskt.iNamLamViec = @YearOfWork		
		order by sSKT_KyHieu;
		DROP TABLE #temp;
	END

END
;
;
;
GO
