/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam]    Script Date: 26/10/2023 6:49:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam]    Script Date: 26/10/2023 6:49:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_skt_rpt_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam] 
  @NamLamViec int, 
  @XauNoiMa ntext 
AS 
BEGIN  
 SET NOCOUNT ON; 
  ( 
  SELECT 
   * 
  FROM NS_MucLucNganSach 
  WHERE 
    iNamLamViec = @NamLamViec and bHangChaDuToan = 1 
   AND sXauNoiMa in (SELECT * FROM dbo.splitstring(@XauNoiMa))   
   AND iTrangThai = 1 
) Order by sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG 
   
END 
; 
;
;
GO
