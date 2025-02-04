/****** Object:  StoredProcedure [dbo].[sp_ns_get_skt_chungtuchitiet]    Script Date: 01/06/2024 12:46:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_get_skt_chungtuchitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_get_skt_chungtuchitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_get_skt_chungtuchitiet]    Script Date: 01/06/2024 12:46:05 PM ******/
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

	if(@iID_MaBQuanLy = '0') 
		begin
			select distinct mlskt.sKyHieu sSKT_KyHieu
			from NS_MLSKT_MLNS map
			inner join NS_MucLucNganSach ns
			on ns.sXauNoiMa = map.sNS_XauNoiMa
			inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
			where ns.sLNS in (select sLNS from  NS_MucLucNganSach where iNamLamViec = @YearOfWork)
			and ns.iNamLamViec = @YearOfWork
			and map.iNamLamViec = @YearOfWork
			and mlskt.iNamLamViec = @YearOfWork		
			order by sSKT_KyHieu
		end


	if(@iID_MaBQuanLy != '0')
		begin
			select distinct mlskt.sKyHieu sSKT_KyHieu
			from NS_MLSKT_MLNS map
			inner join NS_MucLucNganSach ns
			on ns.sXauNoiMa = map.sNS_XauNoiMa
			inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
			where ns.sLNS in (select sLNS from  NS_MucLucNganSach dm where dm.iID_MaBQuanLy = @iID_MaBQuanLy and iNamLamViec = @YearOfWork)
			and ns.iNamLamViec = @YearOfWork
			and map.iNamLamViec = @YearOfWork
			and mlskt.iNamLamViec = @YearOfWork
			order by sSKT_KyHieu
		end


	--if(@iID_MaBQuanLy = '0')
	--	begin
	--		select distinct mlskt.sKyHieu sSKT_KyHieu, mlskt.sM, mlskt.sNG_Cha, mlskt.sNG 
	--		into #NS_MLSKT_MLNS_map_tem
	--		from NS_MLSKT_MLNS map
	--		inner join NS_MucLucNganSach ns
	--		on ns.sXauNoiMa = map.sNS_XauNoiMa
	--		inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
	--		where ns.sLNS in (select sLNS from  NS_MucLucNganSach)
	--		and ns.iNamLamViec = @YearOfWork
	--		and map.iNamLamViec = @YearOfWork
	--		and mlskt.iNamLamViec = @YearOfWork

	--		select distinct sSKT_KyHieu from  #NS_MLSKT_MLNS_map_tem 
	--		union all
	--		select distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem on mlskt.sNG_Cha = #NS_MLSKT_MLNS_map_tem.sNG_Cha 
	--		where (mlskt.sNG is null or mlskt.sNG  = '') and  mlskt.iNamLamViec = @YearOfWork
	--		union all
	--		select  distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem on mlskt.sM = #NS_MLSKT_MLNS_map_tem.sM 
	--		where (mlskt.sNG is null or mlskt.sNG ='')  and (mlskt.sNG_Cha is null or mlskt.sNG_Cha = '') 
	--		and mlskt.iNamLamViec = @YearOfWork
	--		order by sSKT_KyHieu

	--		drop table #NS_MLSKT_MLNS_map_tem
	--	end
		
	--if(@iID_MaBQuanLy != '0')
	--	begin
	--		select distinct mlskt.sKyHieu sSKT_KyHieu, mlskt.sM, mlskt.sNG_Cha, mlskt.sNG 
	--		into #NS_MLSKT_MLNS_map_tem_
	--		from NS_MLSKT_MLNS map
	--		inner join NS_MucLucNganSach ns
	--		on ns.sXauNoiMa = map.sNS_XauNoiMa
	--		inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
	--		where ns.sLNS in (select sLNS from  NS_MucLucNganSach dm where dm.iID_MaBQuanLy = @iID_MaBQuanLy)
	--		and ns.iNamLamViec = @YearOfWork
	--		and map.iNamLamViec = @YearOfWork
	--		and mlskt.iNamLamViec = @YearOfWork

	--		select distinct sSKT_KyHieu from  #NS_MLSKT_MLNS_map_tem_ 
	--		union all
	--		select distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem_ on mlskt.sNG_Cha = #NS_MLSKT_MLNS_map_tem_.sNG_Cha 
	--		where (mlskt.sNG is null or  mlskt.sNG  = '') and  mlskt.iNamLamViec = @YearOfWork
	--		union all
	--		select  distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem_ on mlskt.sM = #NS_MLSKT_MLNS_map_tem_.sM 
	--		where (mlskt.sNG is null or mlskt.sNG ='')  and (mlskt.sNG_Cha is null or mlskt.sNG_Cha = '') 
	--		and mlskt.iNamLamViec = @YearOfWork
	--		order by sSKT_KyHieu

	--		drop table #NS_MLSKT_MLNS_map_tem_
	--	end
END
;
;
GO
