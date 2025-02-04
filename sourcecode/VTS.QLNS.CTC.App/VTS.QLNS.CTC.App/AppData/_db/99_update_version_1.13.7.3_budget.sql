/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_tonghop_quy_lns]    Script Date: 27/12/2023 1:51:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_tonghop_quy_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_tonghop_quy_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_tonghop_quy_lns]    Script Date: 27/12/2023 1:51:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_tonghop_quy_lns]
	@YearOfWork int,
	@YearOfBudget nvarchar(100),
	@BudgetSource int,
	@AgencyId nvarchar(100),
	@QuarterMonth nvarchar(100),
	@LoaiQuyetToan nvarchar(20),
	@UserName nvarchar(100)
AS
BEGIN

	DECLARE @CountDonViCha int;
	DECLARE @tblIdChungTuQT table (iID_QTChungTu uniqueidentifier)
	DECLARE @tblIdChungTuDT table (iID_DTChungTu uniqueidentifier)
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	IF @CountDonViCha = 0
		INSERT INTO @tblIdChungTuQT (iID_QTChungTu)
		SELECT ct.iID_QTChungTu
		FROM 
			(SELECT 
				* 
			FROM 
				NS_QT_ChungTu 
			WHERE 
				sLoai in (select * from f_split(@LoaiQuyetToan))
				AND iNamNganSach IN (SELECT * FROM f_split(@YearOfBudget))
				AND iNamLamViec = @YearOfWork
				AND iID_MaNguonNganSach = @BudgetSource
				AND (@QuarterMonth IS NULL OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
				AND EXISTS (SELECT * from f_split(sDSLNS) INTERSECT SELECT sLNS FROM NS_NguoiDung_LNS WHERE sMaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			) ct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) nddv
		ON ct.iID_MaDonVi = nddv.iID_MaDonVi;
	ELSE
		INSERT INTO @tblIdChungTuQT (iID_QTChungTu)
		SELECT ct.iID_QTChungTu
		FROM 
			(SELECT 
				* 
			FROM 
				NS_QT_ChungTu 
			WHERE 
				sLoai in (select * from f_split(@LoaiQuyetToan))
				AND iNamNganSach IN (SELECT * FROM f_split(@YearOfBudget))
				AND iID_MaNguonNganSach = @BudgetSource
				AND (@QuarterMonth IS NULL OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
				AND EXISTS (SELECT * from f_split(sDSLNS) INTERSECT SELECT sLNS FROM NS_NguoiDung_LNS WHERE sMaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			) ct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) nddv
		ON ct.iID_MaDonVi = nddv.iID_MaDonVi
		WHERE ((ct.iID_MaDonVi IS NULL OR ct.sNguoiTao <> @UserName) AND ct.bKhoa = 1) OR (ct.sNguoiTao = @UserName);

	INSERT INTO @tblIdChungTuDT (iID_DTChungTu)
	SELECT iID_DTChungTu
	FROM NS_DT_ChungTu
	WHERE iLoai in (0, 1, 2)
	  AND iNamNganSach IN (SELECT * FROM f_split(@YearOfBudget))
	  AND iNamLamViec = @YearOfWork
	  AND iID_MaNguonNganSach = @BudgetSource
	  AND ((@CountDonViCha = 0
			AND EXISTS
			  (SELECT *
			   FROM f_split(sDSLNS) INTERSECT SELECT sLNS
			   FROM NS_NguoiDung_LNS
			   WHERE sMaNguoiDung = @UserName
				 AND iNamLamViec = @YearOfWork)
			OR (@CountDonViCha <> 0)))
	  AND ((@CountDonViCha = 0
			AND EXISTS
			  (SELECT *
			   FROM f_split(sDSID_MaDonVi) INTERSECT SELECT iID_MaDonVi
			   FROM NguoiDung_DonVi
			   WHERE iID_MaNguoiDung = @UserName
				 AND iNamLamViec = @YearOfWork
				 AND iTrangThai = 1)
			OR (@CountDonViCha <> 0)))
	  AND ((@CountDonViCha = 0
			AND bKhoa = 1)
		   OR (@CountDonViCha <> 0))

	SELECT sLNS into #tblLNS
	FROM
	(
		SELECT sLNS
		FROM NS_QT_ChungTuChiTiet
		WHERE iID_QTChungTu IN (SELECT * FROM @tblIdChungTuQT)
			AND fTuChi_PheDuyet <> 0
			AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		UNION
		SELECT sLNS
		FROM NS_DT_ChungTuChiTiet
		WHERE iID_DTChungTu IN (SELECT * FROM @tblIdChungTuDT)
			AND fTuChi <> 0
			AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
			AND 
				(
					(@LoaiQuyetToan = '101' AND sLNS like '101%')
					OR (@LoaiQuyetToan = '1' AND sLNS like '1%' and sLNS not like '101%')
					OR (@LoaiQuyetToan = '2' AND sLNS like '2%')
					OR (@LoaiQuyetToan = '3' AND sLNS like '3%')
					OR (@LoaiQuyetToan = '4' AND sLNS like '4%')
					OR (@LoaiQuyetToan = '101,1,2,3,4' AND (sLNS like '1%' OR sLNS like '2%' OR sLNS like '3%' OR sLNS like '4%'))
				)
	) ctct
	
	SELECT sLNS as LNS, 
		sMoTa as MoTa, 
		iID_MLNS as MlnsId, 
		iID_MLNS_Cha as MlnsIdParent,
		iID_MaBQuanLy as IdPhongban
	FROM NS_MucLucNganSach 
	WHERE iNamLamViec = @YearOfWork
		AND sLNS in 
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
						#tblLNS
				) sLNS
				UNPIVOT
				(
					value
					FOR col in (sLNS1, sLNS3, sLNS5, sLNS)
				) un
			)
			and sL = ''
	order by sXauNoiMa;
	drop table #tblLNS;
END
;
;
;
GO
