/****** Object:  StoredProcedure [dbo].[sp_get_mlns_year]    Script Date: 4/16/2024 5:08:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_mlns_year]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_mlns_year]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_mlns_year]    Script Date: 4/16/2024 5:08:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_get_mlns_year] 
	-- Add the parameters for the stored procedure here
	@INamLamViec int
AS
BEGIN
	
	select * into #lns from NS_MucLucNganSach where iNamLamViec = @INamLamViec
	and sL = '' and sK = '' and sM = '' and sTM = '' and sTTM = '' and sNG = '' and sTNG = ''
	
	select iID_MLNS into #finalLNS from #lns where (select count(*) from #lns t1 where t1.iID_MLNS_Cha = #lns.iID_MLNS) = 0
	
	select distinct sXauNoiMa 
	into	#tmp1
	from	(
			select distinct sXauNoiMa from NS_DT_ChungTuChiTiet where iNamLamViec = @INamLamViec
			union 
			select distinct sXauNoiMa from NS_Nganh_ChungTuChiTiet where iNamLamViec = @INamLamViec
			union 
			select distinct sXauNoiMa from NS_Nganh_ChungTuChiTiet_PhanCap where iNamLamViec = @INamLamViec
			union 
			select distinct sXaunoiMa from NS_DTDauNam_ChungTuChiTiet where iNamLamViec = @INamLamViec
			union
			select distinct sXaunoiMa from NS_DTDauNam_PhanCap where iNamLamViec = @INamLamViec) tmp
	
	select distinct sxaunoima into #tmp2 from NS_QT_ChungTuChiTiet where iNamLamViec = @INamLamViec

	select distinct iid_mlns 
	into #tmp3 
	from	(
			select distinct iid_mlns from NS_BK_ChungTu t1 left join NS_MucLucNganSach t2 on t1.sXauNoiMa = t2.sXauNoiMa and t1.iNamLamViec = @INamLamViec
			union
			select distinct iid_mlns from NS_CP_ChungTuChiTiet where iNamLamViec = @INamLamViec
			union 
			select distinct iid_mlns from NS_DT_ChungTuChiTiet where iNamLamViec = @INamLamViec
			union 
			select distinct iid_mlns from NS_Nganh_ChungTuChiTiet where iNamLamViec = @INamLamViec
			union 
			select distinct iid_mlns from NS_Nganh_ChungTuChiTiet_PhanCap where iNamLamViec = @INamLamViec
			union 
			select distinct iid_mlns from NS_QT_ChungTuChiTiet where iNamLamViec = @INamLamViec
			union
			select distinct iid_mlns from NS_DTDauNam_ChungTuChiTiet t1 left join NS_MucLucNganSach t2 on t1.sXauNoiMa = t2.sXauNoiMa and t1.iNamLamViec = t2.iNamLamViec where t1.iNamLamViec = @INamLamViec
			union
			select distinct iid_mlns from NS_DTDauNam_PhanCap where iNamLamViec = @INamLamViec) tmp
	
	select  * into #tmp4 from NS_MLSKT_MLNS where iNamLamViec = @INamLamViec

	select * into #root from NS_MucLucNganSach where iNamLamViec = @INamLamViec	
	
	select	r.*, #tmp1.sXauNoiMa as UsedDuToanChiTietToi, 
			#tmp2.sXauNoiMa as UsedQuyetToanChiTietToi, #tmp3.iID_MLNS as UsedMLNS, #finalLns.iID_MLNS as LNSID,
			parent.sXauNoiMa as MlnsParentName, parent.iID_MLNS as iID_MLNS_Cha, #tmp4.sSKT_KyHieu as SktKyHieu
	from	#root r
			left join #tmp1 on r.sXauNoiMa = #tmp1.sXauNoiMa and iNamLamViec = @INamLamViec
			left join #tmp2 on r.sXauNoiMa = #tmp2.sXauNoiMa and iNamLamViec = @INamLamViec
			left join #tmp3 on r.iID_MLNS = #tmp3.iID_MLNS and iNamLamViec = @INamLamViec
			left join #tmp4 on r.sXauNoiMa = #tmp4.sNS_XauNoiMa and #tmp4.iNamLamViec = @INamLamViec
			left join #root parent on r.iID_MLNS_Cha = parent.iID_MLNS
			left join #finalLns on #finalLns.iID_MLNS = r.iID_MLNS order by r.sxaunoima

	DROP TABLE #lns
	DROP TABLE #finalLNS
	DROP TABLE #tmp1
	DROP TABLE #tmp2
	DROP TABLE #tmp3
	DROP TABLE #tmp4
	DROP TABLE #root
END
;
;
;
GO
