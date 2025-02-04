/****** Object:  StoredProcedure [dbo].[sp_rpt_du_toan_dau_nam_phan_cap_theo_nganh]    Script Date: 9/30/2024 5:16:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_du_toan_dau_nam_phan_cap_theo_nganh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_du_toan_dau_nam_phan_cap_theo_nganh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_du_toan_dau_nam_phan_cap_theo_nganh]    Script Date: 9/30/2024 5:16:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_du_toan_dau_nam_phan_cap_theo_nganh]
	@Nganh varchar(max),
	@MaDonVi varchar(max),
	@IdChungTu varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@Loai int,
	@dvt int,
	@bTongHop bit
AS
BEGIN
SELECT iID_MLNS as iID_MLNS,
       iID_MLNS_Cha as iID_MLNS_Cha,
	   dt_dv.iID_MaDonVi idDonVi,
	   dt_dv.sTenDonVi,
	   bHangCha,
       sLNS,
	   sK,
	   sL,
	   sM,
	   sTM,
	   sTTM,
       sNG,
	   sTNG,
       ConCat(' ', ' - ',sMoTa) as sMoTa,
	   TuChi =SUM(IsNull(A.TuChi,0))/@dvt,
	   sXauNoiMa
FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
				ml.sLNS,
                ml.sNG,
				ml.sL,
				ml.sK,
				ml.sM,
				ml.sTM,
				ml.sTTM,
				ml.sTNG,
                ml.sMoTa,
				ml.sXauNoiMa,
				ml.bHangCha,
                ct.iID_MaDonVi,
				sTenDonVi,
                IsNull(ct.fTuChi, 0) TuChi
   FROM NS_DTDauNam_PhanCap ct
   Inner JOIN NS_DTDauNam_ChungTu chungtu ON ct.iID_CTDTDauNam = chungtu.iID_CTDTDauNam
   RIGHT JOIN NS_MucLucNganSach ml ON ct.sXauNoiMa = ml.sXauNoiMa
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     --AND ct.iNamNganSach = @NamNganSach
     --AND ct.iID_MaNguonNganSach = @NguonNganSach
	 AND ((@bTongHop = 0 and chungtu.sDSSoChungTuTongHop is  null)  or (@bTongHop = 1 AND chungtu.bDaTongHop = @bTongHop ))
	 AND (@IdChungTu is null or ct.iID_CTDTDauNam in (select * from f_split(@IdChungTu)))
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi,
          sTenDonVi
   FROM DonVi
   WHERE iTrangThai=1
     AND iNamLamViec=@NamLamViec) AS dt_dv ON A.iID_MaDonVi = dt_dv.iID_MaDonVi		
GROUP BY iID_MLNS,
         iID_MLNS_Cha,
		 dt_dv.iID_MaDonVi,
	     dt_dv.sTenDonVi,
		 bHangCha,
		 sLNS,
		 sL,
		 sK,
		 sM,
		 sTM,
		 sTTM,
         sNG,
         sTNG,
         sMoTa,
		 sXauNoiMa
		 order by sNG, sTNG
END
;
;
;
;
;
;
GO
