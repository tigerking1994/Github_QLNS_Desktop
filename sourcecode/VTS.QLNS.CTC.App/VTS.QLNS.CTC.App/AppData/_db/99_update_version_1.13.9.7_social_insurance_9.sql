/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]    Script Date: 2/5/2024 7:07:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nkpk_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]    Script Date: 2/5/2024 7:07:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
CREATE PROCEDURE [dbo].[sp_rpt_qtc_nkpk_kehoach_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @LoaiChi uniqueidentifier
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, sDuToanChiTietToi
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		--AND (
		--	(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
		--	OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
		--	OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
		--	)
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		--(
		--			SELECT DISTINCT VALUE
		--			FROM 
		--			(
		--				SELECT 
		--					CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
		--					CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
		--					CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
		--					CAST(sLNS AS nvarchar(10)) LNS 
		--				FROM
		--					NS_NguoiDung_LNS 
		--				WHERE 
		--					sMaNguoiDung = @UserName
		--					AND INamLamViec = @NamLamViec
		--					AND sLNS IN (SELECT * FROM f_split(@LNS))
		--			) LNS
		--			UNPIVOT
		--			(
		--				value
		--				FOR col in (LNS1, LNS3, LNS5, LNS)
		--			) un) 

SELECT   isnull(ctct.ID_QTC_Nam_KPK_ChiTiet, @iDCT) as ID_QTC_Nam_KPK_ChiTiet,
		@iD as iID_QTC_Nam_KPK,
		mlns.iID_MLNS as iID_MucLucNganSach,
		mlns.iID_MLNS_Cha as IdParent,
		mlns.sXauNoiMa as sXauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		mlns.sDuToanChiTietToi as SDuToanChiTietToi,
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS sNguoiTao,
		ctct.sNguoiSua AS sNguoiSua,
		ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0)  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0)  as fTien_DuToanGiaoNamNay,
		ISNULL(Sum(ctct.fTien_TongDuToanDuocGiao), 0)  as fTien_TongDuToanDuocGiao, 
		ISNULL(Sum(ctct.fTien_ThucChi), 0)  as fTien_ThucChi, 
		ISNULL(Sum(ctct.fTienThua), 0)  as fTienThua, 
		ISNULL(Sum(ctct.fTienThieu), 0)  as fTienThieu, 
		ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0)  as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KPK_ChiTiet 
				WHERE iID_QTC_Nam_KPK in
					( SELECT ID_QTC_Nam_KPK FROM BH_QTC_Nam_KPK
								WHERE iNamLamViec=@NamLamViec
								AND iID_LoaiChi=@LoaiChi
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 

	Group by ctct.ID_QTC_Nam_KPK_ChiTiet,
		ctct.iID_QTC_Nam_KPK,
		mlns.iID_MLNS ,
		mlns.iID_MLNS_Cha ,
		mlns.sXauNoiMa ,
		mlns.sLNS ,
		mlns.sL ,
		mlns.sK ,
		mlns.sM ,
		mlns.sTM ,
		mlns.sTTM ,
		mlns.sNG ,
		mlns.sTNG ,
		mlns.sMoTa ,
		mlns.bHangCha ,
		mlns.sDuToanChiTietToi ,
		ctct.sNguoiTao ,
		ctct.sNguoiSua, 
		ctct.dNgayTao,
		ctct.dNgaySua
	Order by mlns.sXauNoiMa
END
;
;
;
GO
