/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_quyetoannam_donvi_index1]    Script Date: 10/13/2023 3:18:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_quyetoannam_donvi_index1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_quyetoannam_donvi_index1]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_dt_rpt_lns_thang]    Script Date: 10/13/2023 3:18:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_dt_rpt_lns_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_dt_rpt_lns_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 10/13/2023 3:18:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_quyettoan_mucluccongkhai]    Script Date: 10/13/2023 3:18:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_quyettoan_mucluccongkhai]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_quyettoan_mucluccongkhai]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_quyettoan_mucluccongkhai]    Script Date: 10/13/2023 3:18:40 PM ******/
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
		(CASE when (spb.FTuChiNamNay = 0 OR spb.FTuChiNamNay IS NULL OR dtdg.DuToanDuocGiao = 0 OR dtdg.DuToanDuocGiao is null) THEN 0 else spb.FTuChiNamNay/dtdg.DuToanDuocGiao end) * 100 as FTiLeDuToan,
		--(ISNULL(spb.FTuChiNamNay, 0) /( CASE when (dtdg.DuToanDuocGiao = 0 OR dtdg.DuToanDuocGiao is null) THEN 1 else dtdg.DuToanDuocGiao end)) * 100 as FTiLeDuToan,
		--(ISNULL(spb.FTuChiNamNay, 0) /( CASE when (spb.FTuChiNamTruoc = 0 OR spb.FTuChiNamTruoc is null) THEN 1 else spb.FTuChiNamTruoc end)) * 100 as FTiLeSoVoiNamTruoc,
		(CASE when (spb.FTuChiNamNay = 0 OR spb.FTuChiNamNay IS NULL OR spb.FTuChiNamTruoc = 0 OR spb.FTuChiNamTruoc is null) THEN 0 else spb.FTuChiNamNay/spb.FTuChiNamTruoc end) * 100 as FTiLeSoVoiNamTruoc,

		ISNULL(spb.FTuChiNamTruoc, 0) AS FTuChiNamTruoc,
		dmck.Id AS iID_DMCongKhai,
		dmck.iID_DMCongKhai_Cha,
		--dv.iID_MaDonVi,
		--dv.sTenDonVi,
		dmck.bHangCha,
		dmck.sMa,
		dmck.sMaCha
		INTO #DataOut		
	FROM NS_DanhMucCongKhai dmck
	LEFT JOIN tblDuToanDuocGiao dtdg ON dtdg.iID_DMCongKhai = dmck.Id
	LEFT JOIN tblSoPhanBo spb
	ON dtdg.iID_DMCongKhai = spb.iID_DMCongKhai
	--LEFT JOIN DonVi dv ON spb.iID_MaDonVi = dv.iID_MaDonVi AND spb.iNamLamViec = dv.iNamLamViec
	WHERE dmck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@ListIdPublic)) OR (dmck.Id IN (SELECT * FROM f_split(@ListIdPublic)));

	--with #dataTree(sSTT,sMoTa,fDuToanDuocGiao,FTuChiNamNay,FTiLeDuToan,FTiLeSoVoiNamTruoc,FTuChiNamTruoc,iID_DMCongKhai, iID_DMCongKhai_Cha,bHangCha,sMa,sMaCha, position) as
	--(
	--	Select * ,
	--				CAST(ROW_NUMBER() OVER(ORDER BY pr.sSTT) AS NVARCHAR(MAX)) AS position
	--	FROM #DataOut Pr WHERE  pr.iID_DMCongKhai_Cha is null
	--	UNION ALL
	--	SELECT
	--		child.sSTT,
	--		child.sMoTa,
	--		child.fDuToanDuocGiao,
	--		child.FTuChiNamNay,
	--		child.FTiLeDuToan,
	--		child.FTiLeSoVoiNamTruoc,
	--		child.FTuChiNamTruoc,
	--		child.iID_DMCongKhai,
	--		child.iID_DMCongKhai_Cha,
	--		child.bHangCha,
	--		CONCAT(parent.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY child.sSTT) AS NVARCHAR(MAX))) AS position
	--	FROM #DataOut child
	--	inner join #dataTree parent on parent.iID_DMCongKhai = child.iID_DMCongKhai_Cha
	--)

	--SELECT *,
	--	cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
	--FROM  #dataTree
	--ORDER  BY sort;	
	select * from #DataOut Order by sMa;
	DROP TABLE #DataOut;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 10/13/2023 3:18:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_chungtu_chitiet]
	-- Add the parameters for the stored procedure here
	@VoucherId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type nvarchar(10),
	@AgencyId nvarchar(100),
	@VoucherDate datetime,
	@UserName nvarchar(100),
	@QuarterMonth nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	Declare @YearOfBudgetCurrent int = 2,
		@YearOfBudgetAfter int = 4;
	Declare @voucherIndex int,
	@IsLocked bit,
	@STongHop nvarchar(500),
	@ILanDieuChinh int,
	@ILoaiChungTu int;
	SELECT @voucherIndex = iSoChungTuIndex, @IsLocked = bKhoa, @STongHop = sTongHop, @ILoaiChungTu = iLoaiChungTu, @ILanDieuChinh = iLanDieuChinh FROM NS_QT_ChungTu WHERE iID_QTChungTu = @VoucherId;

	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;


	SELECT 
		isnull(ctct.iID_QTCTChiTiet, NEWID()) AS iID_QTCTChiTiet,
		ctct.iID_QTChungTu,
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sTNG1,
		mlns.sTNG2,
		mlns.sTNG3,
		mlns.sMoTa,
		mlns.bHangCha,
		isnull(ctct.iNamNganSach, @YearOfBudget) as iNamNganSach,
		isnull(ctct.iID_MaNguonNganSach, @BudgetSource) as iID_MaNguonNganSach,
		isnull(ctct.iNamLamViec, @YearOfWork) as iNamLamViec,
		isnull(ctct.iThangQuyLoai, 0) as iThangQuyLoai,
		isnull(ctct.iThangQuy, 0) as iThangQuy,
		isnull(ctct.iID_MaDonVi, dtctct.iID_MaDonVi) as iID_MaDonVi,
		--ctct.iID_MaDonVi,
		dv.sTenDonVi,
		isnull(ctct.fSoNguoi, 0) as fSoNguoi,
		isnull(ctct.fSoNgay, 0) as fSoNgay,
		isNull(ctct.fSoLuot, 0) as fSoLuot,
		isnull(ctct.fTuChi_DeNghi, 0) as fTuChi_DeNghi,
		isnull(ctct.fTuChi_PheDuyet, 0) as fTuChi_PheDuyet,
		ctct.sGhiChu,
		ctct.dNgayTao,
		ctct.dNgaySua,
		ctct.sNguoiTao,
		ctct.sNguoiSua,
		dtctct.DuToan as fDuToan,
		ctctdqt.DaQuyetToan as fDaQuyetToan,
		mlns.sChiTietToi,
		mlns.bHangChaQuyetToan,
		mlns.sMaCB,
		ctct.fDeNghi_ChuyenNamSau
	FROM 
	(
		select * from NS_MucLucNganSach where iNamLamViec = @YearOfWork and iTrangThai = 1 and bHangChaQuyetToan is not null and
		(
			(@CountDonViCha > 0 and (@IsLocked = 1 or @STongHop is not null) and sLNS in (select * from f_split(@LNS)))
			or
			(
				sLNS in 
				(
					SELECT 
						DISTINCT VALUE
					FROM 
					(
						SELECT 
							CAST(LEFT(sLNS, 1) AS nvarchar(10)) sLNS1, 
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) sLNS3, 
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) sLNS5, 
							CAST(sLNS AS nvarchar(10)) sLNS 
						FROM
							NS_NguoiDung_LNS 
						WHERE 
							sMaNguoiDung = @UserName 
							AND iNamLamViec = @YearOfWork
							AND sLNS IN (SELECT * FROM f_split(@LNS))
					) sLNS
					UNPIVOT
					(
					  value
					  FOR col in (sLNS1, sLNS3, sLNS5, sLNS)
					) un
				)
			)
		)
	) mlns
	LEFT JOIN
		(SELECT 
			* 
		 FROM 
			NS_QT_ChungTuChiTiet 
		 WHERE 
			iID_QTChungTu = @VoucherId
			AND iNamLamViec = @YearOfWork
			AND (@YearOfBudget = @YearOfBudgetCurrent and (iNamNganSach = @YearOfBudgetCurrent or iNamNganSach = @YearOfBudgetAfter) or (iNamNganSach = @YearOfBudget))
			AND iID_MaNguonNganSach = @BudgetSource
		) ctct
	ON mlns.sXauNoiMa = ctct.sXauNoiMa 
	LEFT JOIN 
		(select 
			(case sLNS1
				when '1040200' then (SUM(fHangNhap) + SUM(fTuChi))
				when '1040300' then (SUM(fHangMua) + SUM(fTuChi))
				else SUM(fTuChi)
			end) as DuToan,
			iID_MLNS1 as iID_MLNS,
			@AgencyId as iID_MaDonVi,
			sXauNoiMa
			from 
			(
				select 
					case 
						when ctct.sLNS = '1040100' then mlns.sLNS
						else ctct.sLNS
						end 
					as sLNS1,
					case 
						when ctct.sLNS = '1040100' then mlns.iID_MLNS
						else ctct.iID_MLNS
						end
					as iID_MLNS1,
					ctct.*
				from 
				(
					SELECT
					*
					FROM 
						NS_DT_ChungTuChiTiet
					 WHERE
						iID_DTChungTu 
						IN (SELECT iID_DTChungTu FROM NS_DT_ChungTu 
							where 
								((ISNULL(@STongHop, '') = '' AND sDSID_MaDonVi like '%' + @AgencyId + '%') OR (ISNULL(@STongHop, '') <> '' AND sDSID_MaDonVi = @AgencyId))
								AND (iLoai = 1 or iLoai = 0)
								AND iNamLamViec = @YearOfWork
								AND iNamNganSach = @YearOfBudget
								AND iID_MaNguonNganSach = @BudgetSource
								AND bKhoa = 1
								AND cast(dNgayQuyetDinh as date) <= cast(@VoucherDate as date)
								AND sSoQuyetDinh is not null AND sSoQuyetDinh <> ''
						)
						and iID_MaDonVi = @AgencyId
						and IDuLieuNhan = 0) ctct
					left join 
						(select '1040100' as sLNS1, * from NS_MucLucNganSach where sLNS in ('1040100', '1040200', '1040300') and iNamLamViec = @YearOfWork) mlns
					on mlns.sLNS1 = ctct.sLNS and mlns.sL = ctct.sL and mlns.sK = ctct.sK and mlns.sM = ctct.sM and mlns.sTM = ctct.sTM and mlns.sNG = ctct.sNG and mlns.sTTM = ctct.sTTM
				) dt
				GROUP BY iID_MLNS1, sLNS1, sXauNoiMa
			) dtctct
	on mlns.sXauNoiMa = dtctct.sXauNoiMa
	LEFT JOIN 
		(SELECT
			SUM(fTuChi_PheDuyet) AS DaQuyetToan,
			sXauNoiMa
		 FROM 
			NS_QT_ChungTuChiTiet
		 WHERE
			iID_QTChungTu 
			IN (SELECT iID_QTChungTu FROM NS_QT_ChungTu 
				where 
					iID_MaDonVi = @AgencyId
					AND sLoai = @Type
					AND iNamLamViec = @YearOfWork
					AND (@YearOfBudget = @YearOfBudgetCurrent and (iNamNganSach = @YearOfBudgetCurrent or iNamNganSach = @YearOfBudgetAfter) or (iNamNganSach = @YearOfBudget))
					AND iID_MaNguonNganSach = @BudgetSource
					AND ((iThangQuy < @QuarterMonth) OR
					((iThangQuy = @QuarterMonth AND @ILoaiChungTu = 2 AND ISNULL(iLanDieuChinh, 0) < @ILanDieuChinh) 
					OR (iThangQuy = @QuarterMonth AND ISNULL(@ILoaiChungTu, 1) = 1 AND ISNULL(iLoaiChungTu, 1) = 1)))
					--AND cast(dNgayChungTu as date) <= cast(@voucherDate as Date)
					AND iID_QTChungTu <> @VoucherId
			)
		 GROUP BY sXauNoiMa
		) ctctdqt
	ON mlns.sXauNoiMa = ctctdqt.sXauNoiMa
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi OR dv.iID_MaDonVi = dtctct.iID_MaDonVi
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
	Order by mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG, mlns.sTNG1, mlns.sTNG2, mlns.sTNG3
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
/****** Object:  StoredProcedure [dbo].[sp_qt_dt_rpt_lns_thang]    Script Date: 10/13/2023 3:18:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_dt_rpt_lns_thang]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthType int
AS
BEGIN

	SELECT DISTINCT sLNS AS LNS,
		LEFT(sLNS, 1) AS LNS1,
		LEFT(sLNS, 3) AS LNS3
	FROM (
	SELECT sLNS FROM NS_QT_ChungTuChiTiet
	WHERE iNamLamViec = @YearOfWork
      AND iNamNganSach = @YearOfBudget
	  AND iID_MaNguonNganSach = @BudgetSource
	  AND iID_MaDonVi in (SELECT * FROM f_split(@AgencyId))
	  AND (@QuarterMonth IS NULL
		   OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
	  AND (@QuarterMonthType IS NULL
		   OR iThangQuyLoai = @QuarterMonthType)
	  AND (fTuChi_DeNghi <> 0
		   OR fTuChi_PheDuyet <> 0)
	 UNION ALL
	 SELECT DISTINCT sLNS
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 --AND iPhanCap = 1		
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))		
	 ) temp
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_quyetoannam_donvi_index1]    Script Date: 10/13/2023 3:18:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_rpt_quyetoannam_donvi_index1]
	@YearOfWork int,
	@AgencyId nvarchar(max),
	@LNS nvarchar(max),
	@DataType int,
	@Dvt int,
	@YearOfBuget int --1: Tổng hợp, 2: Năm trước chuyển sang, 3: Năm nay
AS
BEGIN
    ----1. Lấy dữ liệu năm trước chuyển sang

	SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct_namtruocchuyensang.sXauNoiMa,
		   mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sMoTa,
		   iID_MaDonVi,
		   TenDonVi,
		   ChiTieuNamNay = SUM(ChiTieuNamNay) / @Dvt,
		   ChiTieuNamSau = SUM(ChiTieuNamSau) / @Dvt,
		   QuyetToan = SUM(QuyetToan) / @Dvt
		INTO #tbl_NamTruocChuyenSang
		FROM 
		(
			--- + Dự toán
			 SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
					iID_MaDonVi,
					Loai = 1,
					ChiTieuNamNay = CASE
									   WHEN @DataType=1 THEN fTuChi
									   ELSE fHienVat
									END,
					ChiTieuNamSau = 0,
					QuyetToan = 0
			   FROM NS_DT_ChungTuChiTiet
			   WHERE iNamLamViec = @YearOfWork
				 AND iNamNganSach in (1,4)
				 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
				 AND (@LNS IS NULL OR sLNS in (SELECT * FROM f_split(@LNS)))
				 AND IDuLieuNhan = 0 
				 --AND ( isnull(fTuChi,0) > 0 or isnull(fHienVat,0) > 0)
				 

			UNION ALL 

			--- + Quyêt toán
			SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
					iID_MaDonVi,
					Loai = 1,
					ChiTieuNamNay = 0,
					ChiTieuNamSau = fDeNghi_ChuyenNamSau,
					QuyetToan = CASE
									WHEN @DataType = 1 THEN fTuChi_PheDuyet
									ELSE cast(0 AS float)
								END
				   FROM NS_QT_ChungTuChiTiet
				   WHERE iNamLamViec=@YearOfWork
					 AND iNamNganSach in (1, 4)
					 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT *  FROM f_split(@AgencyId)))
					 AND (@LNS IS NULL OR sLNS in (SELECT *  FROM f_split(@LNS)))
					 --AND ( isnull(fTuChi_PheDuyet,0) > 0)

		) AS ct_namtruocchuyensang

		--Lấy thêm thông tin mục lục ngân sách
		LEFT JOIN
		  (SELECT sMoTa,
				  sXauNoiMa,
				  iID_MLNS,
				  iID_MLNS_Cha

		   FROM NS_MucLucNganSach
		   WHERE iTrangThai = 1
				 AND iNamLamViec = @YearOfWork) AS mlns ON mlns.sXauNoiMa = ct_namtruocchuyensang.sXauNoiMa 

		-- Lấy thêm thông tin đơn vị
		LEFT JOIN
		  (SELECT iID_MaDonVi AS dv_id,
				  TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
		   FROM DonVi
		   WHERE iTrangThai = 1
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct_namtruocchuyensang.iID_MaDonVi

		GROUP BY Loai, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct_namtruocchuyensang.sXauNoiMa,
				 iID_MaDonVi, TenDonVi,
				 mlns.iID_MLNS,
				 mlns.iID_MLNS_Cha,
				 mlns.sMoTa

		HAVING SUM(ChiTieuNamNay) <> 0
		OR SUM(ChiTieuNamSau) <> 0
		OR SUM(QuyetToan) <> 0;

	  ---Lấy danh sách cây cha con
	  WITH LNSTreeParentNamTruoc AS
	  (SELECT sLNS,
			sL,
			sK,
			sM,
			sTM,
			sTTM,
			sNG,
			sTNG,
			sTNG1, 
			sTNG2, 
			sTNG3,
			sXauNoiMa,
			iID_MLNS,
			iID_MLNS_Cha,
			cast(iID_MaDonVi AS nvarchar(500)) AS IdDonVi,
			cast(TenDonVi AS nvarchar(500)) AS TenDonVi,
			cast(ChiTieuNamNay AS float) AS ChiTieuNamNay,
			cast(ChiTieuNamSau AS float) AS ChiTieuNamSau,
			cast(QuyetToan AS float) AS QuyetToan,
			sMoTa,
			cast(0 AS bit) AS IsHangCha,
			1 as Level
	   FROM #tbl_NamTruocChuyenSang
	   UNION ALL 
	   SELECT mlnsParent.sLNS,
			mlnsParent.sL,
			mlnsParent.sK,
			mlnsParent.sM,
			mlnsParent.sTM,
			mlnsParent.sTTM,
			mlnsParent.sNG,
			mlnsParent.sTNG,
			mlnsParent.sTNG1,
			mlnsParent.sTNG2,
			mlnsParent.sTNG3,
			mlnsParent.sXauNoiMa,
			mlnsParent.iID_MLNS,
			mlnsParent.iID_MLNS_Cha,
			cast('' AS nvarchar(500)) AS IdDonVi,
			cast('' AS nvarchar(500)) AS TenDonVi,
			cast(0 AS float) AS ChiTieuNamNay,
			cast(0 AS float) AS ChiTieuNamSau,
			cast(0 AS float) AS QuyetToan,
			mlnsParent.sMoTa,
			cast(1 AS bit) AS IsHangCha,
			1 as Level
	   FROM NS_MucLucNganSach mlnsParent
	   INNER JOIN LNSTreeParentNamTruoc ON mlnsParent.iID_MLNS = LNSTreeParentNamTruoc.iID_MLNS_Cha
	   WHERE mlnsParent.iNamLamViec = @YearOfWork)

	---Insert dữ liệu vào bảng tạm
		SELECT DISTINCT LNSTreeParentNamTruoc.*
		INTO #tbl_NamTruocChuyenSang_Rs
		FROM LNSTreeParentNamTruoc 

	----Insert dòng tiêu đề
		INSERT INTO #tbl_NamTruocChuyenSang_Rs (sLNS,sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,sMoTa,iID_MLNS, iID_MLNS_Cha,IsHangCha,Level,IdDonVi,TenDonVi,ChiTieuNamNay,
				ChiTieuNamSau,QuyetToan   ) 
		SELECT '', '','','','','','','','','','',N'A. NĂM TRƯỚC CHUYỂN SANG',N'A. NĂM TRƯỚC CHUYỂN SANG', '00000000-0000-0000-0000-000000000001', NEWID(),1,0, '','',ChiTieuNamNay, ChiTieuNamSau, QuyetToan
				FROM
				(
					SELECT 
						SUM(ChiTieuNamNay) as ChiTieuNamNay,
						SUM(ChiTieuNamSau) as ChiTieuNamSau,
						SUM(QuyetToan) as QuyetToan
					FROM
					#tbl_NamTruocChuyenSang_Rs
					where isHangCha = 0
					
				) as ct_namtruoctong

	----Insert dòng tổng cộng
		--INSERT INTO tbl_NamTruocChuyenSang_Rs (sLNS,sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,sMoTa,iID_MLNS, iID_MLNS_Cha,IsHangCha,Level,IdDonVi,TenDonVi,ChiTieuNamNay,
		--		ChiTieuNamSau,QuyetToan   ) 
		--SELECT '', '','','','','','','','','','',N'CỘNG NĂM TRƯỚC',N' CỘNG', '00000000-0000-0000-0000-000000000001', NEWID(),1,2, '','',ChiTieuNamNay, ChiTieuNamSau, QuyetToan
		--FROM tbl_NamTruocChuyenSang_Rs
		--WHERE Level = 0;
		

	---- 2. Lấy dữ liệu Năm nay

	SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct_namnay.sXauNoiMa,
		   mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sMoTa,
		   iID_MaDonVi,
		   TenDonVi,
		   ChiTieuNamNay = SUM(ChiTieuNamNay) / @Dvt,
		   ChiTieuNamSau = SUM(ChiTieuNamSau) / @Dvt,
		   QuyetToan = SUM(QuyetToan) / @Dvt
	INTO #tbl_NamNay
	FROM 
		(
			--- + Dự toán
			SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
					iID_MaDonVi,
						Loai = 2,
						ChiTieuNamNay = CASE
										   WHEN @DataType=1 THEN fTuChi
										   ELSE fHienVat
										END,
						ChiTieuNamSau = 0,
						QuyetToan = 0
				   FROM NS_DT_ChungTuChiTiet
				   WHERE iNamLamViec = @YearOfWork
					 AND iNamNganSach = 2
					 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
					 AND (@LNS IS NULL OR sLNS in (SELECT * FROM f_split(@LNS)))
					 AND IDuLieuNhan = 0 
					 --AND ( isnull(fTuChi,0) > 0 or isnull(fHienVat,0) > 0)

				UNION ALL 
				--- + Quyêt toán
				SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
						iID_MaDonVi,
						Loai = 2,
						ChiTieuNamNay = 0,
						ChiTieuNamSau = fDeNghi_ChuyenNamSau,
						QuyetToan = CASE
										WHEN @DataType = 1 THEN fTuChi_PheDuyet
										ELSE cast(0 AS float)
									END
					   FROM NS_QT_ChungTuChiTiet
					   WHERE iNamLamViec=@YearOfWork
						 AND iNamNganSach = 2
						 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT *  FROM f_split(@AgencyId)))
						 AND (@LNS IS NULL OR sLNS in (SELECT *  FROM f_split(@LNS)))
						 --AND ( isnull(fTuChi_PheDuyet,0) > 0)
			) AS ct_namnay
					
			--Lấy thêm thông tin mục lục ngân sách
			LEFT JOIN
				(SELECT sMoTa,
					sXauNoiMa,
					iID_MLNS,
					iID_MLNS_Cha

				  FROM NS_MucLucNganSach
				  WHERE iTrangThai = 1
					AND iNamLamViec = @YearOfWork) AS mlns ON mlns.sXauNoiMa = ct_namnay.sXauNoiMa 

			--- Lấy thêm thông tin đơn vị
			LEFT JOIN
				 (SELECT iID_MaDonVi AS dv_id,TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
				  FROM DonVi
				  WHERE iTrangThai = 1
				AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct_namnay.iID_MaDonVi

			GROUP BY Loai, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct_namnay.sXauNoiMa,
						 iID_MaDonVi, TenDonVi,
						 mlns.iID_MLNS,
						 mlns.iID_MLNS_Cha,
						 mlns.sMoTa

			HAVING SUM(ChiTieuNamNay) <> 0
			OR SUM(ChiTieuNamSau) <> 0
			OR SUM(QuyetToan) <> 0;

		---Lấy danh sách cha con
		WITH LNSTreeParentNamNay AS
		  (SELECT sLNS,
				sL,
				sK,
				sM,
				sTM,
				sTTM,
				sNG,
				sTNG,
				sTNG1, 
				sTNG2, 
				sTNG3,
				sXauNoiMa,
				iID_MLNS,
				iID_MLNS_Cha,
				cast(iID_MaDonVi AS nvarchar(500)) AS IdDonVi,
				cast(TenDonVi AS nvarchar(500)) AS TenDonVi,
				cast(ChiTieuNamNay AS float) AS ChiTieuNamNay,
				cast(ChiTieuNamSau AS float) AS ChiTieuNamSau,
				cast(QuyetToan AS float) AS QuyetToan,
				sMoTa,
				cast(0 AS bit) AS IsHangCha,
				4 as Level
		   FROM #tbl_NamNay
		   UNION ALL 
		   SELECT mlnsParent.sLNS,
				mlnsParent.sL,
				mlnsParent.sK,
				mlnsParent.sM,
				mlnsParent.sTM,
				mlnsParent.sTTM,
				mlnsParent.sNG,
				mlnsParent.sTNG,
				mlnsParent.sTNG1,
				mlnsParent.sTNG2,
				mlnsParent.sTNG3,
				mlnsParent.sXauNoiMa,
				mlnsParent.iID_MLNS,
				mlnsParent.iID_MLNS_Cha,
				cast('' AS nvarchar(500)) AS IdDonVi,
				cast('' AS nvarchar(500)) AS TenDonVi,
				cast(0 AS float) AS ChiTieuNamNay,
				cast(0 AS float) AS ChiTieuNamSau,
				cast(0 AS float) AS QuyetToan,
				mlnsParent.sMoTa,
				cast(1 AS bit) AS IsHangCha,
				4 as Level
		   FROM NS_MucLucNganSach mlnsParent
		   INNER JOIN LNSTreeParentNamNay ON mlnsParent.iID_MLNS = LNSTreeParentNamNay.iID_MLNS_Cha
		   WHERE mlnsParent.iNamLamViec = @YearOfWork)

		---Insert dữ liệu vào bảng tạm
		SELECT DISTINCT  LNSTreeParentNamNay.*
		INTO #tbl_NamNay_Rs
		FROM LNSTreeParentNamNay 

		----Insert dòng tiêu đề
		INSERT INTO #tbl_NamNay_Rs (sLNS,sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,sMoTa,iID_MLNS, iID_MLNS_Cha,IsHangCha,Level,IdDonVi,TenDonVi,ChiTieuNamNay,
				ChiTieuNamSau,QuyetToan   ) 
		SELECT '', '','','','','','','','','','',N'B. NĂM NAY	',N'B. NĂM NAY', '00000000-0000-0000-0000-000000000002', NEWID(),1,3, '','',ChiTieuNamNay, ChiTieuNamSau, QuyetToan
				FROM
				(
					SELECT 
						SUM(ChiTieuNamNay) as ChiTieuNamNay,
						SUM(ChiTieuNamSau) as ChiTieuNamSau,
						SUM(QuyetToan) as QuyetToan
					FROM
					#tbl_NamNay_Rs
					where isHangCha = 0
					
				) as ct_namtruoctong

	----Insert dòng tổng cộng
		--INSERT INTO tbl_NamNay_Rs (sLNS,sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,sMoTa,iID_MLNS, iID_MLNS_Cha,IsHangCha,Level,IdDonVi,TenDonVi,ChiTieuNamNay,
		--		ChiTieuNamSau,QuyetToan   ) 
		--SELECT '', '','','','','','','','','','',N'CỘNG NĂM NAY',N' CỘNG', '00000000-0000-0000-0000-000000000002', NEWID(),1,5, '','',ChiTieuNamNay, ChiTieuNamSau, QuyetToan
		--FROM tbl_NamNay_Rs
		--WHERE Level = 3;


	---Hiển thị kết quả trả về
	 IF @YearOfBuget = 1
		BEGIN

			SELECT * , CAST(1 as int) as INamNganSach FROM #tbl_NamTruocChuyenSang_Rs
			UNION ALL 
			SELECT *, CAST(2 as int) as INamNganSach FROM #tbl_NamNay_Rs 
			ORDER BY  Level, sXauNoiMa
		END
	ELSE
		IF @YearOfBuget = 2 --< define theo input >
			BEGIN
				SELECT *, CAST(1 as int) as INamNganSach FROM #tbl_NamTruocChuyenSang_Rs
				ORDER BY Level, sXauNoiMa
			END 
		ELSE
			BEGIN
				SELECT *, CAST(2 as int) as INamNganSach from #tbl_NamNay_Rs 
				ORDER BY Level, sXauNoiMa
			END
	;;

	IF EXISTS(SELECT *
          FROM   #tbl_NamTruocChuyenSang)
	 DROP TABLE #tbl_NamTruocChuyenSang
	IF EXISTS(SELECT *
          FROM   #tbl_NamNay)
	 DROP TABLE #tbl_NamNay
	IF EXISTS(SELECT *
          FROM   #tbl_NamTruocChuyenSang_Rs)
	 DROP TABLE #tbl_NamTruocChuyenSang_Rs

	 IF EXISTS(SELECT *
          FROM   #tbl_NamNay_Rs)
	 DROP TABLE #tbl_NamNay_Rs;

END
;
;
;
;


