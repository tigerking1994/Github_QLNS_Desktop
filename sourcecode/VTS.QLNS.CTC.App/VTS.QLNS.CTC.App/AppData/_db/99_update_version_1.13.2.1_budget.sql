/****** Object:  StoredProcedure [dbo].[sp_dt_quyettoan_mucluccongkhai]    Script Date: 9/29/2023 4:21:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_quyettoan_mucluccongkhai]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_quyettoan_mucluccongkhai]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_quyettoan_mucluccongkhai]    Script Date: 9/29/2023 4:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_quyettoan_mucluccongkhai]
	-- Add the parameters for the stored procedure here
	@ListIdPublic nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Time int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--DECLARE @iLoaiThangQuyNamTruoc int = (		SELECT Count(ct.iID_QTChungTu)  
 --																FROM NS_QT_ChungTuChiTiet ctct
	--															INNER JOIN NS_DMCongKhai_MLNS dmck
	--															ON ctct.sXauNoiMa = dmck.sNS_XauNoiMa --AND ctct.iNamLamViec = dmck.iNamLamViec
	--															INNER JOIN NS_QT_ChungTu ct
	--															ON ctct.iID_QTChungTu = ct.iID_QTChungTu
	--															INNER JOIN NS_DanhMucCongKhai as ctck
	--															ON ctck.Id = dmck.iID_DMCongKhai
	--															WHERE 
	--																ctct.iNamLamViec = @YearOfWork -1 
	--																AND ctct.iNamNganSach = @YearOfBudget 
	--																AND ctct.iID_MaNguonNganSach = @BudgetSource
	--																AND( ct.iThangQuy = @Time AND ct.iThangQuyLoai = 1));
	--DECLARE @IloaiThangQuyNamNay int = (		SELECT Count(ct.iID_QTChungTu)  
 --																FROM NS_QT_ChungTuChiTiet ctct
	--															INNER JOIN NS_DMCongKhai_MLNS dmck
	--															ON ctct.sXauNoiMa = dmck.sNS_XauNoiMa --AND ctct.iNamLamViec = dmck.iNamLamViec
	--															INNER JOIN NS_QT_ChungTu ct
	--															ON ctct.iID_QTChungTu = ct.iID_QTChungTu
	--															INNER JOIN NS_DanhMucCongKhai as ctck
	--															ON ctck.Id = dmck.iID_DMCongKhai
	--															WHERE 
	--																ctct.iNamLamViec = @YearOfWork 
	--																AND ctct.iNamNganSach = @YearOfBudget 
	--																AND ctct.iID_MaNguonNganSach = @BudgetSource
	--																AND( ct.iThangQuy = @Time AND ct.iThangQuyLoai = 1));
    -- Insert statements for procedure here
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
			AND ct.iLoai = 0 and iDuLieuNhan = 0
			AND ((@Time = 0  AND ct.iLoaiDuToan = 1) 
			OR @Time = 12
			   OR (@Time <> 0 and (YEAR(ct.dNgayQuyetDinh) < @YearOfWork or (MONTH(ct.dNgayQuyetDinh) <= @Time and YEAR(ct.dNgayQuyetDinh) = @YearOfWork)))
			   )
			AND (ctck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@ListIdPublic)) OR (ctck.Id IN (SELECT * FROM f_split(@ListIdPublic))))
			--AND ct.iID_DTChungTu in (SELECT * From f_split(@VoucherIds))
		GROUP BY dmck.iID_DMCongKhai, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
	),

	tblSoPhanBo AS (
		SELECT 
		SUM(CASE When ctct.iNamLamViec=@YearOfWork Then ctct.fTuChi_PheDuyet Else 0 End ) as FTuChiNamNay,
		SUM(CASE When ctct.iNamLamViec= @YearOfWork - 1 Then ctct.fTuChi_PheDuyet Else 0 End ) as FTuChiNamTruoc,
		
		dmck.iID_DMCongKhai, ctck.iID_DMCongKhai_Cha
		FROM NS_QT_ChungTuChiTiet ctct
		INNER JOIN NS_DMCongKhai_MLNS dmck
		ON ctct.sXauNoiMa = dmck.sNS_XauNoiMa --AND ctct.iNamLamViec = dmck.iNamLamViec
	    INNER JOIN NS_QT_ChungTu ct
		ON ctct.iID_QTChungTu = ct.iID_QTChungTu
		INNER JOIN NS_DanhMucCongKhai as ctck
		ON ctck.Id = dmck.iID_DMCongKhai
		INNER JOIN DonVi dv on dv.iID_MaDonVi = ctct.iID_MaDonVi and dv.iLoai = 0 and dv.iNamLamViec = @YearOfWork

		WHERE
		(
			(ctct.iNamLamViec = @YearOfWork 
			AND ctct.iNamNganSach = @YearOfBudget 
			AND ctct.iID_MaNguonNganSach = @BudgetSource
			--AND ct.iLoai = 1
			AND (
			      (@Time = 0 And 1 = 2 ) -- đầu năm ko có quyết toán 
				OR @Time = 12
			    OR (@Time <> 0
					and (
							(ctct.iThangQuy = @Time and ctct.iThangQuyLoai = 1) OR ( ctct.iThangQuy <= @Time and ctct.iThangQuyLoai = 0)								
								
						)
					  )
					)
			)
			OR
			(ctct.iNamLamViec = @YearOfWork -1 
			AND ctct.iNamNganSach = @YearOfBudget 
			AND ctct.iID_MaNguonNganSach = @BudgetSource
			--AND ct.iLoai = 1
			AND (
			      (@Time = 0 And 1 = 2 ) -- đầu năm ko có quyết toán 
				OR @Time = 12
			    OR (@Time <> 0
					and (
							(ctct.iThangQuy = @Time and ctct.iThangQuyLoai = 1) OR ( ctct.iThangQuy <= @Time and ctct.iThangQuyLoai = 0)								

						)
					  )
					)
			)
		)
			AND (ctck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@ListIdPublic)) OR (ctck.Id IN (SELECT * FROM f_split(@ListIdPublic))))

		GROUP BY dmck.iID_DMCongKhai, ctck.iID_DMCongKhai_Cha
	)

		SELECT 
		dmck.STT AS sSTT,
		dmck.sMoTa AS sMoTa,
		ISNULL(dtdg.DuToanDuocGiao, 0) AS fDuToanDuocGiao,
		ISNULL(spb.FTuChiNamNay, 0) AS FTuChiNamNay,
		(ISNULL(spb.FTuChiNamNay, 0) /( CASE when (dtdg.DuToanDuocGiao = 0 OR dtdg.DuToanDuocGiao is null) THEN 1 else dtdg.DuToanDuocGiao end)) * 100 as FTiLeDuToan,
		(ISNULL(spb.FTuChiNamNay, 0) /( CASE when (spb.FTuChiNamTruoc = 0 OR spb.FTuChiNamTruoc is null) THEN 1 else spb.FTuChiNamTruoc end)) * 100 as FTiLeSoVoiNamTruoc,
		ISNULL(spb.FTuChiNamTruoc, 0) AS FTuChiNamTruoc,
		dmck.Id AS iID_DMCongKhai,
		dmck.iID_DMCongKhai_Cha,
		--dv.iID_MaDonVi,
		--dv.sTenDonVi,
		dmck.bHangCha
		INTO #DataOut		
	FROM NS_DanhMucCongKhai dmck
	LEFT JOIN tblDuToanDuocGiao dtdg ON dtdg.iID_DMCongKhai = dmck.Id
	LEFT JOIN tblSoPhanBo spb
	ON dtdg.iID_DMCongKhai = spb.iID_DMCongKhai
	--LEFT JOIN DonVi dv ON spb.iID_MaDonVi = dv.iID_MaDonVi AND spb.iNamLamViec = dv.iNamLamViec
	WHERE dmck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@ListIdPublic)) OR (dmck.Id IN (SELECT * FROM f_split(@ListIdPublic)));

	with #dataTree(sSTT,sMoTa,fDuToanDuocGiao,FTuChiNamNay,FTiLeDuToan,FTiLeSoVoiNamTruoc,FTuChiNamTruoc,iID_DMCongKhai, iID_DMCongKhai_Cha,bHangCha, position) as
	(
		Select * ,
					CAST(ROW_NUMBER() OVER(ORDER BY pr.sSTT) AS NVARCHAR(MAX)) AS position
		FROM #DataOut Pr WHERE  pr.iID_DMCongKhai_Cha is null
		UNION ALL
		SELECT
			child.sSTT,
			child.sMoTa,
			child.fDuToanDuocGiao,
			child.FTuChiNamNay,
			child.FTiLeDuToan,
			child.FTiLeSoVoiNamTruoc,
			child.FTuChiNamTruoc,
			child.iID_DMCongKhai,
			child.iID_DMCongKhai_Cha,
			child.bHangCha,
			CONCAT(parent.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY child.sSTT) AS NVARCHAR(MAX))) AS position
		FROM #DataOut child
		inner join #dataTree parent on parent.iID_DMCongKhai = child.iID_DMCongKhai_Cha
	)

	SELECT *,
		cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
	FROM  #dataTree
	ORDER  BY sort;	
	DROP TABLE #DataOut;

END
;



GO
