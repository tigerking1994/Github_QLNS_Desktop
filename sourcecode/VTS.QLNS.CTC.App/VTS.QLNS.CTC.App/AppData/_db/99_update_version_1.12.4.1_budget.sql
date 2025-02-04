/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]    Script Date: 14/12/2022 5:41:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_mucluccongkhai]    Script Date: 14/12/2022 5:41:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_mucluccongkhai]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_mucluccongkhai]
GO
/****** Object:  StoredProcedure [dbo].[sp_check_used_mlns]    Script Date: 14/12/2022 5:41:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_check_used_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_check_used_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_check_used_mlns]    Script Date: 14/12/2022 5:41:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_check_used_mlns] 
	-- Add the parameters for the stored procedure here
	@YearOfWork int,
	@MLNS_ID uniqueidentifier,
	@iResult bit output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @count int;
	select @count = count(*) from (
		select iid_mlns from NS_BK_ChungTu t1 left join NS_MucLucNganSach t2 on t1.sXauNoiMa = t2.sXauNoiMa and t1.iNamLamViec = @YearOfWork
		union
		select iid_mlns from NS_CP_ChungTuChiTiet where iNamLamViec = @YearOfWork
		union 
		select iid_mlns from NS_DT_ChungTuChiTiet where iNamLamViec = @YearOfWork
		union 
		select iid_mlns from NS_Nganh_ChungTuChiTiet where iNamLamViec = @YearOfWork
		union 
		select iid_mlns from NS_Nganh_ChungTuChiTiet_PhanCap where iNamLamViec = @YearOfWork
		union 
		select iid_mlns from NS_QT_ChungTuChiTiet where iNamLamViec = @YearOfWork
		union
		select iid_mlns from NS_DTDauNam_ChungTuChiTiet t1 left join NS_MucLucNganSach t2 on t1.sXauNoiMa = t2.sXauNoiMa and t1.iNamLamViec = t2.iNamLamViec where t1.iNamLamViec = @YearOfWork
		union
		select iid_mlns from NS_DTDauNam_PhanCap ) tbl where iID_MLNS = @MLNS_ID
	if @count > 0
		set @iResult = 1
	else
		set @iResult = 0
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_mucluccongkhai]    Script Date: 14/12/2022 5:41:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
		INNER JOIN NS_MucLucNganSach mlns 
		ON mlns.sXauNoiMa = ctct.sXauNoiMa
		AND mlns.iNamLamViec = @YearOfWork
		AND mlns.bHangChaDuToan =0
		WHERE ctct.iNamLamViec = @YearOfWork 
			AND ctct.iNamNganSach = @YearOfBudget 
			AND ctct.iID_MaNguonNganSach = @BudgetSource
			AND ct.iLoai = 0 and iDuLieuNhan = 0
			AND ((@Time = 0  AND ct.iLoaiDuToan = 1) 
			OR @Time = 12
			   OR (@Time <> 0 and (YEAR(ct.dNgayQuyetDinh) < @YearOfWork or (MONTH(ct.dNgayQuyetDinh) <= @Time and YEAR(ct.dNgayQuyetDinh) = @YearOfWork)))
			   )
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
			AND (
			      (@Time = 0  AND ct.iLoaiDuToan = 1) 
				OR @Time = 12
			    OR (@Time <> 0 and (YEAR(ct.dNgayQuyetDinh) < @YearOfWork or (MONTH(ct.dNgayQuyetDinh) <= @Time and YEAR(ct.dNgayQuyetDinh) = @YearOfWork)))
			)
			AND ctck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@ListIdPublic))
		GROUP BY dmck.iID_DMCongKhai, iID_MaDonVi, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
	)

	--SELECT 
	--	dmck.STT AS sSTT,
	--	dmck.sMoTa AS sMoTa,
	--	ISNULL(dtdg.DuToanDuocGiao, 0) AS fDuToanDuocGiao,
	--	ISNULL(dtdg.DuToanDuocGiao, 0) - ISNULL(spb.SoPhanBo, 0) AS fSoChuaPhanBo,
	--	ISNULL(spb.SoPhanBo, 0) AS fSoPhanBo,
	--	dtdg.iID_DMCongKhai,
	--	dtdg.iID_DMCongKhai_Cha,
	--	dv.iID_MaDonVi,
	--	dv.sTenDonVi
	--FROM tblDuToanDuocGiao dtdg
	--LEFT JOIN tblSoPhanBo spb
	--ON dtdg.iID_DMCongKhai = spb.iID_DMCongKhai
	--LEFT JOIN NS_DanhMucCongKhai dmck ON dtdg.iID_DMCongKhai = dmck.Id
	--LEFT JOIN DonVi dv ON spb.iID_MaDonVi = dv.iID_MaDonVi AND spb.iNamLamViec = dv.iNamLamViec

	SELECT 
		dmck.STT AS sSTT,
		dmck.sMoTa AS sMoTa,
		ISNULL(dtdg.DuToanDuocGiao, 0) AS fDuToanDuocGiao,
		ISNULL(dtdg.DuToanDuocGiao, 0) - ISNULL(spb.SoPhanBo, 0) AS fSoChuaPhanBo,
		ISNULL(spb.SoPhanBo, 0) AS fSoPhanBo,
		dmck.Id,
		dmck.iID_DMCongKhai_Cha,
		dv.iID_MaDonVi,
		dv.sTenDonVi
	FROM NS_DanhMucCongKhai dmck
	LEFT JOIN tblDuToanDuocGiao dtdg ON dtdg.iID_DMCongKhai = dmck.Id
	LEFT JOIN tblSoPhanBo spb ON spb.iID_DMCongKhai = dmck.Id
	LEFT JOIN DonVi dv ON spb.iID_MaDonVi = dv.iID_MaDonVi AND spb.iNamLamViec = dv.iNamLamViec

END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]    Script Date: 14/12/2022 5:41:05 PM ******/
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
		and CT.iLoai = 0 and iDuLieuNhan = 0
		and ((@iQuarterMonths = 0 and ct.iLoaiDuToan = 1) or (@iQuarterMonths <> 0 and (YEAR(ct.dNgayQuyetDinh) < @iNamLamViec or (MONTH(ct.dNgayQuyetDinh) <= @iQuarterMonths and YEAR(ct.dNgayQuyetDinh) = @iNamLamViec) ) ))
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
;
GO
