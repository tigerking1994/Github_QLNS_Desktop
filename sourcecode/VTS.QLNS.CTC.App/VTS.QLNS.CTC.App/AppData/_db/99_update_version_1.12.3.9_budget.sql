/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]    Script Date: 13/12/2022 8:24:06 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoan_giaithichbangloi]    Script Date: 13/12/2022 8:24:06 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoan_giaithichbangloi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoan_giaithichbangloi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_mucluccongkhai]    Script Date: 13/12/2022 8:24:06 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_mucluccongkhai]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_mucluccongkhai]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_mucluccongkhai]    Script Date: 13/12/2022 8:24:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dutoan_mucluccongkhai]
	@ListIdPublic nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Time int
AS
BEGIN
	WITH tblDuToanDuocGiao AS (
		SELECT SUM(ctct.fTuChi) AS DuToanDuocGiao, dmck.iID_DMCongKhai, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
		FROM NS_DT_ChungTuChiTiet ctct
		INNER JOIN NS_DMCongKhai_MLNS dmck
		ON ctct.sXauNoiMa = dmck.sNS_XauNoiMa AND ctct.iNamLamViec = dmck.iNamLamViec
	    INNER JOIN NS_DT_ChungTu ct
		ON ctct.iID_DTChungTu = ct.iID_DTChungTu
		INNER JOIN NS_DanhMucCongKhai as ctck
		ON ctck.Id = dmck.iID_DMCongKhai

		WHERE ctct.iNamLamViec = @YearOfWork 
			AND ctct.iNamNganSach = @YearOfBudget 
			AND ctct.iID_MaNguonNganSach = @BudgetSource
			AND ct.iLoai = 0
			AND (((@Time = 0 OR @Time = 12) AND ct.iLoaiDuToan = 0) 
			   OR (@Time <> 0 AND MONTH(CAST(ct.dNgayChungTu AS DATE)) <= @Time))
			AND ctck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@ListIdPublic))
		GROUP BY dmck.iID_DMCongKhai, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
	),

	tblSoPhanBo AS (
		SELECT SUM(ctct.fTuChi) AS SoPhanBo, dmck.iID_DMCongKhai, ctct.iID_MaDonVi, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
		FROM NS_DT_ChungTuChiTiet ctct
		INNER JOIN NS_DMCongKhai_MLNS dmck
		ON ctct.sXauNoiMa = dmck.sNS_XauNoiMa AND ctct.iNamLamViec = dmck.iNamLamViec
	    INNER JOIN NS_DT_ChungTu ct
		ON ctct.iID_DTChungTu = ct.iID_DTChungTu
		INNER JOIN NS_DanhMucCongKhai as ctck
		ON ctck.Id = dmck.iID_DMCongKhai

		WHERE ctct.iNamLamViec = @YearOfWork 
			AND ctct.iNamNganSach = @YearOfBudget 
			AND ctct.iID_MaNguonNganSach = @BudgetSource
			AND ct.iLoai = 1
			AND (((@Time = 0 OR @Time = 12) AND ct.iLoaiDuToan = 0) 
			   OR (@Time <> 0 AND MONTH(CAST(ct.dNgayChungTu AS DATE)) <= @Time))
			AND ctck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@ListIdPublic))
		GROUP BY dmck.iID_DMCongKhai, iID_MaDonVi, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
	)

	SELECT 
		dmck.STT AS sSTT,
		dmck.sMoTa AS sMoTa,
		ISNULL(dtdg.DuToanDuocGiao, 0) AS fDuToanDuocGiao,
		ISNULL(dtdg.DuToanDuocGiao, 0) - ISNULL(spb.SoPhanBo, 0) AS fSoChuaPhanBo,
		ISNULL(spb.SoPhanBo, 0) AS fSoPhanBo,
		dtdg.iID_DMCongKhai,
		dtdg.iID_DMCongKhai_Cha,
		dv.iID_MaDonVi,
		dv.sTenDonVi
	FROM tblDuToanDuocGiao dtdg
	LEFT JOIN tblSoPhanBo spb
	ON dtdg.iID_DMCongKhai = spb.iID_DMCongKhai
	LEFT JOIN NS_DanhMucCongKhai dmck ON dtdg.iID_DMCongKhai = dmck.Id
	LEFT JOIN DonVi dv ON spb.iID_MaDonVi = dv.iID_MaDonVi AND spb.iNamLamViec = dv.iNamLamViec

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoan_giaithichbangloi]    Script Date: 13/12/2022 8:24:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_ns_rpt_quyettoan_giaithichbangloi]
@sLoai nvarchar(50),
@iID_MaDonVi nvarchar(50),
@iThang int,
@iQuy int,
@iNamLamViec int,
@iNamNganSach int,
@iID_NguonNganSach int,
@isTongHop int,
@explainId nvarchar(50)
as 
begin

select top 1 sMoTa_KienNghi as SMoTaKienNghi , sMoTa_TinhHinh as SMoTaTinhHinh
			into #temp
			from  NS_QT_ChungTuChiTiet_GiaiThich ctgt
			inner join  NS_QT_ChungTu ct on ctgt.iID_QTChungTu = ct.iID_QTChungTu
			where ctgt.iNamLamViec = @iNamLamViec
			and ct.iNamNganSach = @iNamNganSach
			and ct.iID_MaNguonNganSach = @iID_NguonNganSach
			and ct.iThangQuyLoai = @iQuy and ct.iThangQuy = @iThang
			and ctgt.iID_QTChungTu in ( select iID_QTChungTu from NS_QT_ChungTu where iID_MaDonVi = @iID_MaDonVi and sLoai = @sLoai)  

if @isTongHop = 1
	BEGIN
		select top 1 sMoTa_KienNghi as SMoTaKienNghi , sMoTa_TinhHinh as SMoTaTinhHinh
		into #temp_tonghop
		from  NS_QT_ChungTuChiTiet_GiaiThich ctgt
		where iID_GiaiThich = @explainId;

		if ((select count(*) from #temp) = 1)
			begin
				select * from #temp;
			end
			
		else
		   	select * from #temp_tonghop;
			drop table #temp_tonghop;
    END
else
   begin
		select * from #temp;
   end

drop table #temp
end
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]    Script Date: 13/12/2022 8:24:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]
	@iNamLamViec int,
	@iNamNganSach int,
	@iMaNguonNganSach int,
	@iQuarterMonths int,
	@sIdDanhMucCongKhai nvarchar(max),
	@dvt int

AS
BEGIN
	select   sum(isnull(ctct.fTuChi,0))/@dvt as fTuChi, dm_mlns.iID_DMCongKhai as iID_DMCongKhai
		into #temp
		from NS_DT_ChungTuChiTiet as ctct
		inner join NS_DMCongKhai_MLNS as dm_mlns on  dm_mlns.sNS_XauNoiMa = ctct.sXauNoiMa
		inner join NS_DT_ChungTu as ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
		inner join NS_DanhMucCongKhai as dm on dm.Id =  dm_mlns.iID_DMCongKhai
		where ct.iNamLamViec = @iNamLamViec and ct.iID_MaNguonNganSach = @iMaNguonNganSach and ct.iNamNganSach = @iNamNganSach
		and iLoai = 0
		and ((@iQuarterMonths = 0 and ct.iLoaiDuToan = 0) or (@iQuarterMonths <> 0 and MONTH(ct.dNgayQuyetDinh) <= @iQuarterMonths and YEAR(ct.dNgayQuyetDinh) = @iNamLamViec))
		and dm.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@sIdDanhMucCongKhai))
		group by dm_mlns.iID_DMCongKhai

	select 
		dm.Id as Id_DanhMuc,
		dm.iID_DMCongKhai_Cha as Id_DanhMucCha,
		dm.STT as STT,
		dm.sMoTa as sMoTa,
		dm.bHangCha as bHangCha,
		dm.sMa as sMa,
		fTuChi as fTuChi
		from NS_DanhMucCongKhai as dm
		left join #temp as temp on dm.Id = temp.iID_DMCongKhai
		where dm.iNamLamViec = @iNamLamViec 
		order by sMa
	
END
GO
