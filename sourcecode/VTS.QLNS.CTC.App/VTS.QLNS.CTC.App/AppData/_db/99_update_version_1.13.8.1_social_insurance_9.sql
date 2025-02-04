/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]    Script Date: 09/01/2024 1:00:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_lns_rpt_thongtri_lns_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]    Script Date: 09/01/2024 1:00:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_lns_bh]
	 @NamLamViec int,
	 @IDLoaiChi nvarchar(500),
	 @IdDonvi nvarchar(max),
	 @LNS nvarchar(max),
	 @UserName nvarchar(100),
	 @Dvt int,
     @Quy int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
		SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
	SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SMoTa,
		mlns.bHangCha as IsHangCha,
		ISNULL(ctct.fTienKeHoachCap, 0) / @Dvt as FTienKeHoach 
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(select SUM(ISNULL(ctct.fTienKeHoachCap, 0)) fTienKeHoachCap, ctct.iID_MucLucNganSach from BH_CP_ChungTu ct 
		left join BH_CP_ChungTu_ChiTiet ctct on ct.iID_CP_ChungTu=ctct.iID_CP_ChungTu
		where ct.iNamChungTu=@NamLamViec
		and ct.iID_LoaiCap=@IDLoaiChi
		and iID_MaDonVi in (select * from f_split(@IdDonVi))
		and ct.iQuy = @Quy
		group by ctct.iID_MucLucNganSach
		) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;
;
;
GO
