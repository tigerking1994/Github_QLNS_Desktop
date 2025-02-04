/****** Object:  StoredProcedure [dbo].[sp_ns_get_skt_chungtuchitiet]    Script Date: 03/06/2024 8:25:22 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_get_skt_chungtuchitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_get_skt_chungtuchitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_get_skt_chungtuchitiet]    Script Date: 03/06/2024 8:25:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_get_skt_chungtuchitiet]
	@YearOfWork int,
	@iID_MaBQuanLy nvarchar(200)
	
AS
BEGIN
	SET NOCOUNT ON;

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
	order by sSKT_KyHieu

END
;
;
GO
