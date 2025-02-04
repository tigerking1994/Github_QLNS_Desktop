/****** Object:  StoredProcedure [dbo].[sp_tn_qt_rpt_tong_hop_thu_nop]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_qt_rpt_tong_hop_thu_nop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_qt_rpt_tong_hop_thu_nop]
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_qt_rpt_thong_tri_thu_nop]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_qt_rpt_thong_tri_thu_nop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_qt_rpt_thong_tri_thu_nop]
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_qt_chungtu_chitiet1]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_qt_chungtu_chitiet1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_qt_chungtu_chitiet1]
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_qt_chungtu_chitiet]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_qt_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_qt_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dt_rpt_du_toan_ngan_sach]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_dt_rpt_du_toan_ngan_sach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_dt_rpt_du_toan_ngan_sach]
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dt_report_du_toan_ngan_sach]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_dt_report_du_toan_ngan_sach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_dt_report_du_toan_ngan_sach]
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dt_phanbo_dutoan_chitiet]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_dt_phanbo_dutoan_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_dt_phanbo_dutoan_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dt_chungtu_chitiet]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_dt_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_dt_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thuong_xuyen]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thuong_xuyen]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thuong_xuyen]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_thuongxuyen]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thongtri_thuongxuyen]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thongtri_thuongxuyen]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_lns]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thongtri_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thongtri_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thongtri_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_quyetoannam_donvi]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_quyetoannam_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_quyetoannam_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_tonghop_thang]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_tonghop_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_tonghop_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_thang]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_quy]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_chungtu_tonghop_qp]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_chungtu_tonghop_qp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_chungtu_tonghop_qp]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_chungtu_tonghop]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_chungtu_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_chungtu_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_chung_tu]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_chung_tu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_chung_tu]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_donvi_nam]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_donvi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_donvi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_donvi]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet_chitieu]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet_chitieu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet_chitieu]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_mlns_get_by_user]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_mlns_get_by_user]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_mlns_get_by_user]
GO
/****** Object:  StoredProcedure [dbo].[sp_mlns_quocphong]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_mlns_quocphong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_mlns_quocphong]
GO
/****** Object:  StoredProcedure [dbo].[sp_mlns_nhanuoc]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_mlns_nhanuoc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_mlns_nhanuoc]
GO
/****** Object:  StoredProcedure [dbo].[sp_lb_chungtu_chitiet]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_lb_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_lb_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_parent_dutoan_lns]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_parent_dutoan_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_parent_dutoan_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_dutoan_tonghop]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_dutoan_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_dutoan_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dutoan_chitiet]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_phanbo_dutoan_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_phanbo_dutoan_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phan_bo_du_toan_chi_tiet_used_luy_ke]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_phan_bo_du_toan_chi_tiet_used_luy_ke]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_phan_bo_du_toan_chi_tiet_used_luy_ke]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phan_bo_du_toan_chi_tiet_used]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_phan_bo_du_toan_chi_tiet_used]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_phan_bo_du_toan_chi_tiet_used]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_export_phan_bo_du_toan_chi_tiet]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_export_phan_bo_du_toan_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_export_phan_bo_du_toan_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_summary]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chitiet_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chitiet_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_export]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chitiet_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chitiet_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_dutoan]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chitiet_dutoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chitiet_dutoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_cap_tm]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chitiet_cap_tm]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chitiet_cap_tm]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_cap_m]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chitiet_cap_m]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chitiet_cap_m]
GO
/****** Object:  StoredProcedure [dbo].[sp_bk_tonghop]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bk_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bk_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bk_donvi_tonghop]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bk_donvi_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bk_donvi_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_tong_hop_used]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_du_toan_chi_tieu_tong_hop_used]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_du_toan_chi_tieu_tong_hop_used]
GO
/****** Object:  UserDefinedFunction [dbo].[f_mlns_by_lns]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_mlns_by_lns]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_mlns_by_lns]
GO
/****** Object:  UserDefinedFunction [dbo].[f_loai_hinh_by_lns]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_loai_hinh_by_lns]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_loai_hinh_by_lns]
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_parent_mlns_by_lns_xau_noi_ma]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_get_parent_mlns_by_lns_xau_noi_ma]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_get_parent_mlns_by_lns_xau_noi_ma]
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_parent_mlns_by_lns_mlns_id]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_get_parent_mlns_by_lns_mlns_id]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_get_parent_mlns_by_lns_mlns_id]
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_mlns_by_lns]    Script Date: 03/08/2022 6:18:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_get_mlns_by_lns]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_get_mlns_by_lns]
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_mlns_by_lns]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_get_mlns_by_lns]
(	
	@YearOfWork int,
	@LNS nvarchar(max)
)
RETURNS TABLE 
AS
RETURN 
(
	WITH LNSTreeChild AS (
		SELECT
			*
		FROM NS_MucLucNganSach
		WHERE
			sL = ''
			AND iNamLamViec = @YearOfWork
			AND sLNS in (SELECT * FROM dbo.splitstring(@LNS))
			and iTrangThai = 1
		UNION ALL
		SELECT
			mlnsChild.*
		FROM NS_MucLucNganSach mlnsChild
		INNER JOIN LNSTreeChild
			ON mlnsChild.iID_MLNS_Cha = LNSTreeChild.iID_MLNS
		WHERE
			mlnsChild.iNamLamViec = @YearOfWork
			AND mlnsChild.sLNS in (SELECT * FROM dbo.splitstring(@LNS))
	),
	LNSTreeParent AS (
		SELECT
			*
		FROM NS_MucLucNganSach
		WHERE
			sL = ''
			AND iNamLamViec = @YearOfWork
			AND sLNS in (SELECT * FROM dbo.splitstring(@LNS))
			AND iTrangThai = 1
		UNION ALL
		SELECT
			mlnsParent.*
		FROM NS_MucLucNganSach mlnsParent
		INNER JOIN LNSTreeParent
			ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
		WHERE mlnsParent.iNamLamViec = @YearOfWork 
	)
	SELECT DISTINCT * FROM LNSTreeParent 
	UNION
	SELECT DISTINCT * FROM LNSTreeChild
)
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_parent_mlns_by_lns_mlns_id]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_get_parent_mlns_by_lns_mlns_id] (
@YearOfWork int,
@LNS nvarchar(max),
@MLNS_ID ntext) 
RETURNS TABLE 
AS RETURN
  (WITH LNSTreeParent AS
     (SELECT *
      FROM NS_MucLucNganSach
      WHERE iNamLamViec = @YearOfWork
        AND iID_MLNS in
          (SELECT *
           FROM dbo.splitstring(@MLNS_ID))
        AND sLNS in
          (SELECT *
           FROM dbo.splitstring(@LNS))
      UNION ALL SELECT mlnsParent.*
      FROM NS_MucLucNganSach mlnsParent
      INNER JOIN LNSTreeParent ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
      WHERE mlnsParent.iNamLamViec = @YearOfWork ) SELECT DISTINCT *
   FROM LNSTreeParent --where L is null or L = '' Order by LNS, L, K, M, TM, TTM, NG, TNG
)
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_get_parent_mlns_by_lns_xau_noi_ma]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_get_parent_mlns_by_lns_xau_noi_ma]
(	
	@YearOfWork int,
	@LNS nvarchar(max),
	@XauNoiMa ntext
)
RETURNS TABLE 
AS
RETURN 
(
	with
LNSTreeParent AS (
		SELECT
			*
		FROM NS_MucLucNganSach
		WHERE
			 iNamLamViec = @YearOfWork
			AND sXauNoiMa in (SELECT * FROM dbo.splitstring(@XauNoiMa))
			AND sLNS in (SELECT * FROM dbo.splitstring(@LNS))
			AND iTrangThai = 1
		UNION ALL
		SELECT
			mlnsParent.*
		FROM NS_MucLucNganSach mlnsParent
		INNER JOIN LNSTreeParent
			ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
		WHERE mlnsParent.iNamLamViec = @YearOfWork 
	)
	SELECT distinct * FROM LNSTreeParent where sLNS <> '1' and (sTTM is null or sTTM = '')-- Order by LNS desc, L desc, K desc, M desc, TM desc, TTM desc, NG desc, TNG desc
)
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_loai_hinh_by_lns]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_loai_hinh_by_lns]
(	
	@YearOfWork int,
	@LNS nvarchar(max)
)
RETURNS TABLE 
AS
RETURN 
(
	WITH LNSTreeChild AS (
		SELECT
			*
		FROM TN_DanhMucLoaiHinh
		WHERE
			iNamLamViec = @YearOfWork
			AND LNS in (SELECT * FROM dbo.splitstring(@LNS))
			and iTrangThai = 1
		UNION ALL
		SELECT
			mlnsChild.*
		FROM TN_DanhMucLoaiHinh mlnsChild
		INNER JOIN LNSTreeChild
			ON mlnsChild.ID_MaLoaiHinh_Cha = LNSTreeChild.ID_MaLoaiHinh
		WHERE
			mlnsChild.iNamLamViec = @YearOfWork
			AND mlnsChild.LNS in (SELECT * FROM dbo.splitstring(@LNS))
	),
	LNSTreeParent AS (
		SELECT
			*
		FROM TN_DanhMucLoaiHinh
		WHERE
			iNamLamViec = @YearOfWork
			AND LNS in (SELECT * FROM dbo.splitstring(@LNS))
			AND iTrangThai = 1
		UNION ALL
		SELECT
			mlnsParent.*
		FROM TN_DanhMucLoaiHinh mlnsParent
		INNER JOIN LNSTreeParent
			ON mlnsParent.ID_MaLoaiHinh = LNSTreeParent.ID_MaLoaiHinh_Cha
		WHERE mlnsParent.iNamLamViec = @YearOfWork 
	)
	SELECT DISTINCT * FROM LNSTreeParent 
	UNION
	SELECT DISTINCT * FROM LNSTreeChild
)
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_mlns_by_lns]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[f_mlns_by_lns]
(	
	@YearOfWork int,
	@LNS nvarchar(max)
)
RETURNS TABLE 
AS
RETURN 
(
	WITH LNSTreeChild AS (
		SELECT
			*
		FROM NS_MucLucNganSach
		WHERE
			sL = ''
			AND iNamLamViec = @YearOfWork
			AND sLNS in (SELECT * FROM dbo.splitstring(@LNS))
			AND iTrangThai = 1
		UNION ALL
		SELECT
			mlnsChild.*
		FROM NS_MucLucNganSach mlnsChild
		INNER JOIN LNSTreeChild
			ON mlnsChild.iID_MLNS_Cha = LNSTreeChild.iID_MLNS
		WHERE
			mlnsChild.iNamLamViec = @YearOfWork
			AND mlnsChild.sLNS in (SELECT * FROM dbo.splitstring(@LNS))
	),
	LNSTreeParent AS (
		SELECT
			*
		FROM NS_MucLucNganSach
		WHERE
			sL = ''
			AND iNamLamViec = @YearOfWork
			AND sLNS in (SELECT * FROM dbo.splitstring(@LNS))
			AND iTrangThai = 1
		UNION ALL
		SELECT
			mlnsParent.*
		FROM NS_MucLucNganSach mlnsParent
		INNER JOIN LNSTreeParent
			ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
		WHERE mlnsParent.iNamLamViec = @YearOfWork 
	)
	SELECT DISTINCT * FROM LNSTreeParent 
	UNION
	SELECT DISTINCT * FROM LNSTreeChild
)
;
;
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_tong_hop_used]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[rpt_du_toan_chi_tieu_tong_hop_used]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(100)
AS
BEGIN
	SELECT isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       ctct.iID_DTChungTu,
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
	   isnull(mlns.bHangChaDuToan, 0) AS bHangChaDuToan,
       ctct.iNamNganSach,
       ctct.iID_MaNguonNganSach,
       ctct.iNamLamViec,
       isnull(ctct.iPhanCap, 0) AS iPhanCap,
       ctct.iID_MaDonVi,
       isnull(ctct.sGhiChu, '') AS sGhiChu,
       isnull(ctct.fHangMua, 0) AS fHangMua,
       isnull(ctct.fHangNhap, 0) AS fHangNhap,
       isnull(ctct.fDuPhong, 0) AS fDuPhong,
       isnull(ctct.fPhanCap, 0) AS fPhanCap,
       isnull(ctct.fTuChi, 0) AS fTuChi,
       isnull(ctct.fHienVat, 0) AS fHienVat,
       ctct.dNgayTao,
       ctct.sNguoiTao,
       ctct.dNgaySua,
       ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   mlns.sChiTietToi,
	   dv.sTenDonVi
	FROM (select * from NS_MucLucNganSach where iNamLamViec = @YearOfWork and iTrangThai = 1 and sLNS in (select * from f_split(@LNS))) mlns
	LEFT JOIN
	(
		SELECT
			*
		FROM NS_DT_ChungTuChiTiet
		WHERE
			iID_DTChungTu in (SELECT distinct iID_CTDuToan_PhanBo FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_Nhan in (select distinct iID_CTDuToan_Nhan from NS_DT_Nhan_PhanBo_Map where iID_CTDuToan_PhanBo= @ChungTuId))
			AND iID_CTDuToan_Nhan is not null
			AND iDuLieuNhan = 0
	) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	WHERE
		isnull(ctct.fTuChi, 0) > 0 OR isnull(ctct.fHienVat, 0) > 0 OR isnull(ctct.fPhanCap, 0) > 0 OR isnull(ctct.fHangNhap, 0) > 0 OR isnull(ctct.fHangMua, 0) > 0
	ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bk_donvi_tonghop]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bk_donvi_tonghop]
	@YearOfWork int,
	@QuarterMonth int,
	@LNS nvarchar(max)
AS
BEGIN
	SELECT dv.*
	FROM
	  (SELECT DISTINCT iID_MaDonVi
	   FROM NS_BK_ChungTuChiTiet
	   WHERE iNamLamViec=@YearOfWork
		 AND iThangQuy=@QuarterMonth
		 AND iID_MaDonVi is not null
		 AND (@LNS IS NULL
			  OR sLNS in
				(SELECT *
				 FROM f_split(@LNS))) ) AS ctct
	LEFT JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec=@YearOfWork) AS dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bk_tonghop]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bk_tonghop]
	@YearOfWork int,
	@QuarterMonth int,
	@LNS nvarchar(max),
	@AgencyId nvarchar(100),
	@DataType int,
	@Dvt int
AS
BEGIN
	SELECT mlns.sLNS,
       mlns.sL,
       mlns.sK,
       mlns.sM,
       mlns.sTM,
       mlns.sTTM,
       mlns.sNG,
       mlns.sTNG,
       mlns.sXauNoiMa,
       mlns.iID_MLNS,
       mlns.iID_MLNS_Cha,
       mlns.sMoTa,
       NoiDung,
       sSoChungTu,
       dNgayChungTu,
       sSoQuyetDinh,
	   sLoai,
       sTenDonVi,
       fTongTuChi / @Dvt AS TuChi,
       fTongHienVat / @Dvt AS HienVat INTO #tblBkTongHop
	FROM
	  (SELECT sLNS,
			  sL,
			  sK,
			  sM,
			  sTM,
			  sTTM,
			  sNG,
			  sTNG,
			  sXauNoiMa,
			  iID_MLNS,
			  iID_MLNS_Cha,
			  ctct.sMoTa AS NoiDung,
			  iID_BKChungTu,
			  sSoChungTu,
			  sLoai,
			  dNgayChungTu,
			  dv.iID_MaDonVi,
			  dv.sTenDonVi,
			  fTongTuChi,
			  fTongHienVat
	   FROM NS_BK_ChungTuChiTiet ctct
	   LEFT JOIN DonVi dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	   WHERE iTrangThai=1
		 AND iThangQuy=@QuarterMonth
		 AND (@LNS IS NULL
			  OR sLNS in
				(SELECT *
				 FROM f_split(@LNS)))
		 AND (@AgencyId IS NULL
			  OR ctct.iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))
		 AND ctct.iNamLamViec = @YearOfWork 
		 AND (@DataType IS NULL
			  OR (@DataType=1
				  AND fTongTuChi<>0)
			  OR (@DataType=2
				  AND fTongHienVat<>0)) ) AS ctct -- lay so ghi so

	LEFT JOIN
	  (SELECT iID_BKChungTu,
			  sSoQuyetDinh,
			  dNgayQuyetDinh
	   FROM NS_BK_ChungTu
	   WHERE iNamLamViec=@YearOfWork) AS ct ON ct.iID_BKChungTu = ctct.iID_BKChungTu
	LEFT JOIN
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
		 AND iTrangThai = 1) AS mlns ON mlns.iID_MLNS = ctct.iID_MLNS;

	WITH LNSTreeParent AS
	  (SELECT sLNS,
			  sL,
			  sK,
			  sM,
			  sTM,
			  sTTM,
			  sNG,
			  sTNG,
			  sXauNoiMa,
			  iID_MLNS,
			  iID_MLNS_Cha,
			  sMoTa ,
			  cast(NoiDung AS nvarchar(MAX)) AS NoiDung ,
			  cast(sSoChungTu AS nvarchar(500)) AS SoChungTu ,
			  CONVERT(NVARCHAR(100), dNgayChungTu, 103) AS NgayChungTu ,
			  cast(sSoQuyetDinh AS nvarchar(500)) AS SoQuyetDinh ,
			  cast(sTenDonVi AS nvarchar(500)) AS TenDonVi ,
			  cast(sLoai AS nvarchar(500)) AS SLoai ,
			  cast(TuChi AS float) AS TuChi ,
			  cast(HienVat AS float) AS HienVat ,
			  cast(0 AS bit) AS IsHangCha
	   FROM #tblBkTongHop
	   UNION ALL SELECT mlnsParent.sLNS,
						mlnsParent.sL,
						mlnsParent.sK,
						mlnsParent.sM,
						mlnsParent.sTM,
						mlnsParent.sTTM,
						mlnsParent.sNG,
						mlnsParent.sTNG,
						mlnsParent.sXauNoiMa,
						mlnsParent.iID_MLNS,
						mlnsParent.iID_MLNS_Cha,
						mlnsParent.sMoTa ,
						cast('' AS nvarchar(MAX)) AS NoiDung ,
						cast('' AS nvarchar(500)) AS SoChungTu ,
						cast('' AS nvarchar(100)) AS NgayChungTu ,
						cast('' AS nvarchar(500)) AS SoQuyetDinh ,
						cast('' AS nvarchar(500)) AS TenDonVi ,
						cast('' AS nvarchar(500)) AS SLoai ,
						cast(0 AS float) AS TuChi ,
						cast(0 AS float) AS HienVat ,
						cast(1 AS bit) AS IsHangCha
	   FROM NS_MucLucNganSach mlnsParent
	   INNER JOIN LNSTreeParent ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
	   WHERE mlnsParent.iNamLamViec = @YearOfWork )
	SELECT DISTINCT *
	FROM LNSTreeParent
	ORDER BY sLNS,
			 sL,
			 sK,
			 sM,
			 sTM,
			 sTTM,
			 sNG;


	DROP TABLE #tblBkTongHop;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_cap_m]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chitiet_cap_m] 
	@VoucherId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int, @YearOfBudget int, @BudgetSource int, @AgencyId nvarchar(500),
	@VoucherDate datetime,
	@UserName nvarchar(100) 
AS BEGIN
SET NOCOUNT ON;

DECLARE @SoChungTuIndex int
SET @SoChungTuIndex =
  (SELECT iSoChungTuIndex
   FROM NS_CP_ChungTu
   WHERE iID_CTCapPhat = @VoucherId)
SELECT * INTO #mlns
FROM NS_MucLucNganSach
WHERE iNamLamViec = @YearOfWork
  AND iTrangThai = 1
  AND bHangCha = 1
  AND (sM IS NOT NULL
       AND sM <> ''
       AND (sTM IS NULL
            OR sTM = ''))
ORDER BY sXauNoiMa;


SELECT * INTO #dv
FROM DonVi
WHERE iNamLamViec = @YearOfWork
  AND iID_MaDonVi in
    (SELECT *
     FROM dbo.f_split(
                        (SELECT sDSID_MaDonVi
                         FROM NS_CP_ChungTu
                         WHERE iID_CTCapPhat = @VoucherId)));


SELECT * INTO #tbl_global
FROM
  (SELECT #mlns.*,
          #dv.iID_MaDonVi AS Id_DonVi,
          #dv.sTenDonVi AS TenDonVi
   FROM #mlns,
        #dv
   UNION ALL SELECT *,
                    '' AS Id_DonVi,
                    '' AS TenDonVi
   FROM NS_MucLucNganSach mlns
   WHERE mlns.bHangCha =1
     AND ((sL IS NULL
           OR sL = '')
          OR (sK IS NULL
              OR sK = ''))
     AND mlns.iNamLamViec = @YearOfWork --Order by mlns.LNS, mlns.L, mlns.K, mlns.M, mlns.TM, mlns.TTM, mlns.NG, mlns.TNG
 ) tbl DECLARE @CountDonViCha int;


SELECT @CountDonViCha = count(*)
FROM
  (SELECT *
   FROM NguoiDung_DonVi
   WHERE iID_MaNguoiDung = @UserName
     AND iNamLamViec = @YearOfWork
     AND iTrangThai = 1) nddv
INNER JOIN
  (SELECT *
   FROM DonVi
   WHERE iNamLamViec = @YearOfWork
     AND iTrangThai = 1
     AND iLoai = 0) dv ON dv.iID_MaDonVi = nddv.iID_MaDonVi;


SELECT DISTINCT isnull(ctct.iID_CTCapPhatChiTiet, NEWID()) AS Id,
                ctct.iID_CTCapPhat AS IdChungTu,
                mlns.iID_MLNS AS MlnsId,
                mlns.iID_MLNS_Cha AS MlnsIdParent,
                mlns.sXauNoiMa AS XauNoiMa,
                mlns.sLNS AS LNS,
                mlns.sL AS L,
                mlns.sK AS K,
                mlns.sM AS M,
                mlns.sTM AS TM,
                mlns.sTTM AS TTM,
                mlns.sNG AS NG,
                mlns.sTNG AS TNG,
                mlns.sTNG1 AS TNG1,
                mlns.sTNG2 AS TNG2,
                mlns.sTNG3 AS TNG3,
                mlns.sMoTa AS MoTa,
                '' AS Chuong,
                mlns.bHangCha,
                isnull(ctct.iNamLamViec, 0) AS NamLamViec,
                cast(0 AS int) AS iTrangThai,
                ctct.iLoai,
                isnull(ctct.iID_MaDonVi, mlns.Id_DonVi) AS IdDonVi,
                isnull(ctct.sTenDonVi, mlns.TenDonVi) AS TenDonVi,
                isnull(ctct.fTuChi, 0) AS TuChi,
				isnuLL(ctct.fDeNghiDonVi, 0) AS DeNghiDonVi,
                isnull(ctct.fHienVat, 0) AS HienVat,
                ctct.sGhiChu AS GhiChu,
                isnull(ctct.dNgayTao, getdate()) AS DateCreated,
                isnull(ctct.dNgaySua, getdate()) AS DateModified,
                ctct.sNguoiTao AS UserCreator,
                ctct.sNguoiSua AS UserModifier,
                isnull(ctct.iID_MaNguonNganSach, '') AS NguonNganSach,
                CASE
                    WHEN mlns.sLNS = '1040200' THEN dtctct.ChiTieu2
                    WHEN mlns.sLNS = '104300' THEN dtctct.ChiTieu3
                    ELSE dtctct.ChiTieu
                END AS ChiTieu,
                ctctdqt.DaCap
FROM
  (SELECT *
   FROM #tbl_global
   WHERE sLNS in
       (SELECT DISTINCT VALUE
        FROM
          (SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
                  CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
                  CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
                  CAST(sLNS AS nvarchar(10)) LNS
           FROM NS_NguoiDung_LNS
           WHERE sMaNguoiDung = @UserName
             AND iNamLamViec = @YearOfWork
             AND sLNS IN
               (SELECT *
                FROM f_split(@LNS)) ) LNS UNPIVOT (value
                                                   FOR col in (LNS1, LNS3, LNS5, LNS)) un)
     --OR @CountDonViCha <> 0 
	 ) mlns
LEFT JOIN
  (SELECT chitiet.*
   FROM NS_CP_ChungTuChiTiet chitiet
   INNER JOIN NS_CP_ChungTu chungtu ON chitiet.iID_CTCapPhat = chungtu.iID_CTCapPhat
   WHERE chitiet.iID_CTCapPhat = @VoucherId
     AND chitiet.iNamLamViec = @YearOfWork ----AND NamNganSach = @YearOfBudget

     AND chitiet.iID_MaNguonNganSach = @BudgetSource ) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
AND mlns.Id_DonVi = ctct.iID_MaDonVi
LEFT JOIN
  (SELECT SUM(fTuChi) AS ChiTieu,
          SUM(fHangNhap) AS ChiTieu2,
          SUM(fHangMua) AS ChiTieu3,
          iID_MaDonVi AS Id_DonVi,
          iID_MLNS AS MLNS_Id
   FROM NS_DT_ChungTuChiTiet
   WHERE iID_DTChungTu IN
       (SELECT iID_DTChungTu
        FROM NS_DT_ChungTu
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iLoai = 1 --and @AgencyId = Id_DonVi

          AND iNamLamViec = @YearOfWork
          AND iNamNganSach = @YearOfBudget
          AND iID_MaNguonNganSach = @BudgetSource
          AND dNgayChungTu <= dateadd(dd, -1,(dateadd(mm, 1, @VoucherDate))) )
     AND iID_MaDonVi in
       (SELECT *
        FROM f_split(@AgencyId))
   GROUP BY iID_MaDonVi,
            iID_MLNS) dtctct ON mlns.iID_MLNS = dtctct.MLNS_Id
AND dtctct.Id_DonVi = mlns.Id_DonVi
LEFT JOIN
  (SELECT SUM(fTuChi) AS DaCap,
          iID_MaDonVi AS Id_DonVi,
          iID_MLNS AS MLNS_Id
   FROM NS_CP_ChungTuChiTiet
   WHERE iID_CTCapPhat IN
       (SELECT iID_CTCapPhat
        FROM NS_CP_ChungTu
        WHERE iNamLamViec = @YearOfWork
          AND iID_CTCapPhat <> @VoucherId
          AND iSoChungTuIndex < @SoChungTuIndex
          AND dbo.f_check_contain(iID_MaDonVi, @AgencyId) > 0 --Id_DonVi in (select * FROM f_split(@AgencyId))

          AND iID_MaNguonNganSach = @BudgetSource
          AND dNgayChungTu <= dateadd(dd, -1,(dateadd(mm, 1, @VoucherDate))) )
     AND iID_MaDonVi in
       (SELECT *
        FROM f_split(@AgencyId))
   GROUP BY iID_MaDonVi,
            iID_MLNS) ctctdqt ON mlns.iID_MLNS = ctctdqt.MLNS_Id
AND ctctdqt.Id_DonVi = mlns.Id_DonVi
WHERE mlns.iNamLamViec = @YearOfWork
  AND mlns.iTrangThai = 1 --and (ctct.TuChi <> 0 or ctct.HienVat <> 0 or
 --dtctct.ChiTieu <> 0 or		ctctdqt.DaCap <> 0)

ORDER BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG;


DROP TABLE #mlns
DROP TABLE #dv
DROP TABLE #tbl_global END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_cap_tm]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chitiet_cap_tm] 
	@VoucherId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int, @YearOfBudget int, @BudgetSource int, @AgencyId nvarchar(500),
	@VoucherDate datetime,
	@UserName nvarchar(100) 
AS BEGIN
SET NOCOUNT ON;

DECLARE @SoChungTuIndex int
SET @SoChungTuIndex =
  (SELECT iSoChungTuIndex
   FROM NS_CP_ChungTu
   WHERE iID_CTCapPhat = @VoucherId)
SELECT * INTO #mlns
FROM NS_MucLucNganSach
WHERE iNamLamViec = @YearOfWork
  AND iTrangThai = 1
  AND bHangCha = 1
  AND (sTM IS NOT NULL
       AND sTM <> ''
       AND (sTTM IS NULL
            OR sTTM = ''))
ORDER BY sXauNoiMa;


SELECT * INTO #dv
FROM DonVi
WHERE iNamLamViec = @YearOfWork
  AND iID_MaDonVi in
    (SELECT *
     FROM dbo.f_split(
                        (SELECT sDSID_MaDonVi
                         FROM NS_CP_ChungTu
                         WHERE iID_CTCapPhat = @VoucherId)));


SELECT * INTO #tbl_global
FROM
  (SELECT #mlns.*,
          #dv.iID_MaDonVi as Id_DonVi,
          #dv.sTenDonVi as TenDonVi
   FROM #mlns,
        #dv
   UNION ALL SELECT *,
                    '' AS Id_DonVi,
                    '' AS TenDonVi
   FROM NS_MucLucNganSach mlns
   WHERE mlns.bHangCha =1
     AND ((sL IS NULL
           OR sL = '')
          OR (sK IS NULL
              OR sK = '')
          OR (sM IS NULL
              OR sM = '')
          OR (sTM IS NULL
              OR sTM = ''))
     AND mlns.iNamLamViec = @YearOfWork --Order by mlns.LNS, mlns.L, mlns.K, mlns.M, mlns.TM, mlns.TTM, mlns.NG, mlns.TNG
 ) tbl DECLARE @CountDonViCha int;


SELECT @CountDonViCha = count(*)
FROM
  (SELECT *
   FROM NguoiDung_DonVi
   WHERE iID_MaNguoiDung = @UserName
     AND iNamLamViec = @YearOfWork
     AND iTrangThai = 1) nddv
INNER JOIN
  (SELECT *
   FROM DonVi
   WHERE iNamLamViec = @YearOfWork
     AND iTrangThai = 1
     AND iLoai = 0) dv ON dv.iID_MaDonVi = nddv.iID_MaDonVi;


SELECT DISTINCT isnull(ctct.iID_CTCapPhatChiTiet, NEWID()) AS Id,
                ctct.iID_CTCapPhat AS IdChungTu,
                mlns.iID_MLNS AS MlnsId,
                mlns.iID_MLNS_Cha AS MlnsIdParent,
                mlns.sXauNoiMa as XauNoiMa,
                mlns.sLNS as LNS,
                mlns.sL as L,
                mlns.sK as K,
                mlns.sM as M,
                mlns.sTM as TM,
                mlns.sTTM as TTM,
                mlns.sNG as NG,
                mlns.sTNG as TNG,
                mlns.sTNG1 as TNG1,
                mlns.sTNG2 as TNG2,
                mlns.sTNG3 as TNG3,
                mlns.sMoTa as MoTa,
                '' as Chuong,
                mlns.bHangCha,
                isnull(ctct.iNamLamViec, 0) AS NamLamViec,
             
                ctct.iLoai,
                isnull(ctct.iID_MaDonVi, mlns.Id_DonVi) AS IdDonVi,
                isnull(ctct.sTenDonVi, mlns.TenDonVi) AS TenDonVi,
                
                isnull(ctct.fTuChi, '') AS TuChi,
				isnuLL(ctct.fDeNghiDonVi, 0) AS DeNghiDonVi,
                isnull(ctct.fHienVat, '') AS HienVat,
                ctct.sGhiChu as GhiChu,
                isnull(ctct.dNgayTao, getdate()) AS DateCreated,
                isnull(ctct.dNgaySua, getdate()) AS DateModified,
                ctct.sNguoiTao as UserCreator,
                ctct.sNguoiSua as UserModifier,
              
                isnull(ctct.iID_MaNguonNganSach, '') AS NguonNganSach,
                CASE
                    WHEN mlns.sLNS = '1040200' THEN dtctct.ChiTieu2
                    WHEN mlns.sLNS = '104300' THEN dtctct.ChiTieu3
                    ELSE dtctct.ChiTieu
                END AS ChiTieu,
                ctctdqt.DaCap
FROM
  (SELECT *
   FROM #tbl_global
   WHERE sLNS in
       (SELECT DISTINCT VALUE
        FROM
          (SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
                  CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
                  CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
                  CAST(sLNS AS nvarchar(10)) LNS
           FROM NS_NguoiDung_LNS
           WHERE sMaNguoiDung = @UserName
             AND iNamLamViec = @YearOfWork
             AND sLNS IN
               (SELECT *
                FROM f_split(@LNS)) ) LNS UNPIVOT (value
                                                   FOR col in (LNS1, LNS3, LNS5, LNS)) un)
     --OR @CountDonViCha <> 0 
	 ) mlns
LEFT JOIN
  (SELECT chitiet.*
   FROM NS_CP_ChungTuChiTiet chitiet
   INNER JOIN NS_CP_ChungTu chungtu ON chitiet.iID_CTCapPhat = chungtu.iID_CTCapPhat
   WHERE chitiet.iID_CTCapPhat = @VoucherId
     AND chitiet.iNamLamViec = @YearOfWork 

     AND chitiet.iID_MaNguonNganSach = @BudgetSource ) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
AND mlns.Id_DonVi = ctct.iID_MaDonVi
LEFT JOIN
  (SELECT SUM(fTuChi) AS ChiTieu,
          SUM(fHangNhap) AS ChiTieu2,
          SUM(fHangMua) AS ChiTieu3,
          iID_MaDonVi as Id_DonVi,
          iID_MLNS as MLNS_Id
   FROM NS_DT_ChungTuChiTiet
   WHERE iID_DTChungTu IN
       (SELECT iID_DTChungTu
        FROM NS_DT_ChungTu
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iLoai = 1 --and @AgencyId = Id_DonVi

          AND iNamLamViec = @YearOfWork
          AND iNamNganSach = @YearOfBudget
          AND iID_MaNguonNganSach = @BudgetSource
          AND dNgayChungTu <= dateadd(dd, -1,(dateadd(mm, 1, @VoucherDate))) )
     AND iID_MaDonVi in
       (SELECT *
        FROM f_split(@AgencyId))
   GROUP BY iID_MaDonVi,
            iID_MLNS) dtctct ON mlns.iID_MLNS = dtctct.MLNS_Id
AND dtctct.Id_DonVi = mlns.Id_DonVi
LEFT JOIN
  (SELECT SUM(fTuChi) AS DaCap,
          iID_MaDonVi as Id_DonVi,
          iID_MLNS as MLNS_Id
   FROM NS_CP_ChungTuChiTiet
   WHERE iID_CTCapPhat IN
       (SELECT iID_CTCapPhat
        FROM NS_CP_ChungTu
        WHERE iNamLamViec = @YearOfWork
          AND iID_CTCapPhat <> @VoucherId
          AND iSoChungTuIndex < @SoChungTuIndex
          AND dbo.f_check_contain(iID_MaDonVi, @AgencyId) > 0 --Id_DonVi in (select * FROM f_split(@AgencyId))

          AND iID_MaNguonNganSach = @BudgetSource
          AND dNgayChungTu <= dateadd(dd, -1,(dateadd(mm, 1, @VoucherDate))) )
     AND iID_MaDonVi in
       (SELECT *
        FROM f_split(@AgencyId))
   GROUP BY iID_MaDonVi,
            iID_MLNS) ctctdqt ON mlns.iID_MLNS = ctctdqt.MLNS_Id
AND ctctdqt.Id_DonVi = mlns.Id_DonVi
WHERE mlns.iNamLamViec = @YearOfWork
  AND mlns.iTrangThai = 1 --and (ctct.TuChi <> 0 or ctct.HienVat <> 0 or
 --dtctct.ChiTieu <> 0 or		ctctdqt.DaCap <> 0)

ORDER BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG;


DROP TABLE #mlns
DROP TABLE #dv
DROP TABLE #tbl_global END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_dutoan]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chitiet_dutoan] 
@VoucherId nvarchar(255),
@LNS nvarchar(max),
@YearOfWork int,
@YearOfBudget int,
@BudgetSource int,
@AgencyId nvarchar(500),
@VoucherDate datetime,
@AgencyIdChild nvarchar(max)
AS 
BEGIN
SET NOCOUNT ON;

DECLARE @IdDonVi0 nvarchar(50)
SET @IdDonVi0 =
  (SELECT iID_MaDonVi
   FROM DonVi
   WHERE iLoai = 0
     AND iNamLamViec = @YearOfWork)
SELECT mlns.iID_MLNS AS MlnsId,
       mlns.iID_MLNS_Cha AS MlnsIdParent,
       mlns.sXauNoiMa as XauNoiMa,
       mlns.sLNS as LNS,
       mlns.sL as L,
       mlns.sK as K,
       mlns.sM as M,
       mlns.sTM as TM,
       mlns.sTTM as TTM,
       mlns.sNG as  NG,
       mlns.sTNG as TNG,
       mlns.sTNG1 as TNG1,
       mlns.sTNG2 as TNG2,
       mlns.sTNG3 as TNG3,
       mlns.sMoTa as MoTa,
       '' as Chuong,
       mlns.bHangCha AS BHangCha,
        ISNULL(CASE
            WHEN mlns.sLNS = '1040200' THEN (dtctct.ChiTieu2 + dtctct.ChiTieu)
            WHEN mlns.sLNS = '1040300' THEN (dtctct.ChiTieu3 + dtctct.ChiTieu)
            ELSE dtctct.ChiTieu
        END, 0)  AS ChiTieu ,
	  
       isnull(dtctct.Id_DonVi, @AgencyId) AS IdDonVi
FROM NS_MucLucNganSach mlns
LEFT JOIN
  (SELECT SUM(fTuChi) AS ChiTieu,
          SUM(fHangNhap) AS ChiTieu2,
          SUM(fHangMua) AS ChiTieu3,
          iID_MaDonVi as Id_DonVi,
          iID_MLNS as MLNS_Id,
          sXauNoiMa as XauNoiMa,
          sLNS as LNS
   FROM NS_DT_ChungTuChiTiet
   WHERE iID_DTChungTu IN
       (SELECT iID_DTChungTu
        FROM NS_DT_ChungTu
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND (iLoai = 1
               OR (iLoai=0
                   AND sDSID_MaDonVi = @IdDonVi0) ) 

          AND iNamLamViec = @YearOfWork
          AND ((@YearOfBudget = 2
                AND (iNamNganSach = 2
                     OR iNamNganSach = 4))
               OR (@YearOfBudget <> 2
                   AND iNamNganSach = @YearOfBudget))
          AND iID_MaNguonNganSach = @BudgetSource
          AND dNgayQuyetDinh <= dateadd(dd, -1,(dateadd(mm, 1, @VoucherDate))) )
     AND ((@IdDonVi0 = @AgencyId  AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyIdChild)))
          OR ((@IdDonVi0 <> @AgencyId) AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId)))
		   )
     
   GROUP BY iID_MaDonVi,
            iID_MLNS,
            sXauNoiMa,
            sLNS) dtctct ON 
 (mlns.iID_MLNS = dtctct.MLNS_Id
  AND dtctct.LNS <> '1040100'
  --AND mlns.sLNS <> '1040200'
  --AND mlns.sLNS <> '1040300'
  )
OR (dtctct.LNS ='1040100'
    AND REPLACE(dtctct.XauNoiMa, '1040100', '1040200') = mlns.sXauNoiMa
    --AND mlns.sLNS ='1040200'
	)
OR (dtctct.LNS ='1040100'
    AND REPLACE(dtctct.XauNoiMa, '1040100', '1040300') = mlns.sXauNoiMa
    --AND mlns.sLNS ='1040300'
	)
WHERE mlns.iNamLamViec = @YearOfWork
  AND mlns.iTrangThai = 1
ORDER BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_export]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chitiet_export]
@VoucherId nvarchar(255),
@LNS nvarchar(max),
@YearOfWork int,
@YearOfBudget int,
@BudgetSource int,
@AgencyId nvarchar(500),
@VoucherDate datetime 
AS 
BEGIN
SET NOCOUNT ON;


SELECT DISTINCT isnull(ctct.iID_CTCapPhatChiTiet, NEWID()) AS Id,
                ctct.iID_CTCapPhat AS IdChungTu,
                mlns.iID_MLNS AS MlnsId,
                mlns.iID_MLNS_Cha AS MlnsIdParent,
                mlns.sXauNoiMa as XauNoiMa,
                mlns.sLNS as LNS,
                mlns.sL as L,
                mlns.sK as K,
                mlns.sM as M,
                mlns.sTM as TM,
                mlns.sTTM as TTM,
                mlns.sNG as NG,
                mlns.sTNG as TNG,
                mlns.sTNG1 as TNG1,
                mlns.sTNG2 as TNG2,
                mlns.sTNG3 as TNG3,
                mlns.sMoTa as MoTa,
				'' as Chuong,
                mlns.bHangCha,
                isnull(ctct.iNamLamViec, 0) AS NamLamViec,
           
                ctct.iLoai,
                isnull(ctct.iID_MaDonVi, '') AS IdDonVi,
                isnull(ctct.sTenDonVi, '') AS TenDonVi,
                
                isnull(ctct.fTuChi, '') AS TuChi,
				isnuLL(ctct.fDeNghiDonVi, 0) AS DeNghiDonVi,
                isnull(ctct.fHienVat, '') AS HienVat,
                ctct.sGhiChu as GhiChu,
                isnull(ctct.dNgayTao, getdate()) AS DateCreated,
                isnull(ctct.dNgaySua, getdate()) AS DateModified,
                ctct.sNguoiTao as UserCreator,
                ctct.sNguoiSua as UserModifier,
              
                isnull(ctct.iID_MaNguonNganSach, '') AS NguonNganSach,
                dtctct.DuToan,
                ctctdqt.DaCap
FROM NS_MucLucNganSach mlns
LEFT JOIN
  (SELECT chitiet.*
   FROM NS_CP_ChungTuChiTiet chitiet
   INNER JOIN NS_CP_ChungTu chungtu ON chitiet.iID_CTCapPhat = chungtu.iID_CTCapPhat
   WHERE chitiet.iID_CTCapPhat = @VoucherId
     AND chitiet.iID_MaDonVi =@AgencyId
     AND chitiet.iNamLamViec = @YearOfWork ----AND NamNganSach = @YearOfBudget

     AND chitiet.iID_MaNguonNganSach = @BudgetSource ) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
LEFT JOIN
  (SELECT SUM(fTuChi) AS DuToan,
          iID_MLNS as MLNS_Id
   FROM NS_DT_ChungTuChiTiet
   WHERE iID_DTChungTu IN
       (SELECT iID_DTChungTu
        FROM NS_DT_ChungTu
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iLoai = 1 --and @AgencyId = Id_DonVi

          AND iNamLamViec = @YearOfWork
          AND iNamNganSach = @YearOfBudget
          AND iID_MaNguonNganSach = @BudgetSource
          AND dNgayChungTu <= dateadd(dd, -1,(dateadd(mm, 1, @VoucherDate))) )
     AND iID_MaDonVi in
       (SELECT *
        FROM f_split(@AgencyId))
   GROUP BY iID_MLNS) dtctct ON mlns.iID_MLNS = dtctct.MLNS_Id
LEFT JOIN
  (SELECT SUM(fTuChi) AS DaCap,
          iID_MLNS as MLNS_Id
   FROM NS_CP_ChungTuChiTiet
   WHERE iID_CTCapPhat IN
       (SELECT iID_CTCapPhat
        FROM NS_CP_ChungTu
        WHERE iNamLamViec = @YearOfWork
          AND iID_CTCapPhat <> @VoucherId
          AND dbo.f_check_contain(iID_MaDonVi, @AgencyId) > 0 --Id_DonVi in (select * FROM f_split(@AgencyId))

          AND iID_MaNguonNganSach = @BudgetSource
          AND dNgayChungTu <= dateadd(dd, -1,(dateadd(mm, 1, @VoucherDate))) )
     AND iID_MaDonVi in
       (SELECT *
        FROM f_split(@AgencyId))
   GROUP BY iID_MLNS) ctctdqt ON mlns.iID_MLNS = ctctdqt.MLNS_Id
WHERE mlns.iNamLamViec = @YearOfWork
  AND mlns.iTrangThai = 1
ORDER BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_summary]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chitiet_summary]
	@VoucherId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@UserName nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;

	declare @SoChungTuIndex int
	set @SoChungTuIndex = (select ISoChungTuIndex  FROM NS_CP_ChungTu where iID_CTCapPhat = @VoucherId)

	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE INamLamViec = @YearOfWork AND iTrangThai = 1 AND ILoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT 
		distinct
		isnull(ctct.iID_CTCapPhatChiTiet, NEWID()) as Id,
		ctct.iID_CTCapPhat as IdChungTu,
		mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdParent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		mlns.sTNG1 as TNG1,
		mlns.sTNG2 as TNG2,
		mlns.sTNG3 as TNG3,
		mlns.sMoTa as MoTa,
		'' Chuong, 
		mlns.bHangCha,
		isnull(ctct.INamLamViec, 0) as NamLamViec,
		ctct.iLoai,
		isnull(ctct.iID_MaDonVi, '') as IdDonVi,
		isnull(ctct.sTenDonVi,'') as TenDonVi,
	 
		isnull(ctct.fTuChi, '') as TuChi,
		isnuLL(ctct.fDeNghiDonVi, 0) AS DeNghiDonVi,
		isnull(ctct.fHienVat, '') as HienVat,
		ctct.sGhiChu AS GhiChu,
		isnull(ctct.dNgayTao, getdate()) as DateCreated,
		isnull(ctct.dNgaySua, getdate()) as DateModified,
		ctct.sNguoiTao as UserCreator,
		ctct.sNguoiSua as UserModifier,
		isnull(ctct.iID_MaNguonNganSach, '') as NguonNganSach,
		CASE
			WHEN mlns.sLNS = '1040200' THEN dtctct.ChiTieu2
			WHEN mlns.sLNS = '1040300' THEN dtctct.ChiTieu3
			ELSE dtctct.ChiTieu
		END as ChiTieu,
		--ctctdqt.DaCap
		cast(0 as float) as DaCap
	FROM (
		select * FROM NS_MucLucNganSach
		where sLNS in 
				(
					SELECT 
						DISTINCT VALUE
					FROM 
					(
						SELECT 
							CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
							CAST(sLNS AS nvarchar(10)) LNS 
						FROM
							NS_NguoiDung_LNS 
						WHERE 
							sMaNguoiDung = @UserName
							AND INamLamViec = @YearOfWork
							AND sLNS IN (SELECT * FROM f_split(@LNS))
					) LNS
					UNPIVOT
					(
					  value
					  FOR col in (LNS1, LNS3, LNS5, LNS)
					) un
				)
				or @CountDonViCha <> 0

		) mlns
	LEFT JOIN (
		SELECT 
			chitiet.* 
		FROM 
			NS_CP_ChungTuChiTiet chitiet
		INNER JOIN NS_CP_ChungTu chungtu on chitiet.iID_CTCapPhat = chungtu.iID_CTCapPhat
		WHERE 
		 	chitiet.iID_CTCapPhat = @VoucherId
		 	AND chitiet.INamLamViec = @YearOfWork
		 
		 	----AND NamNganSach = @YearOfBudget
		 	AND chitiet.iID_MaNguonNganSach = @BudgetSource
	) ctct ON mlns.iID_MLNS = ctct.iID_MLNS  
	LEFT JOIN (
		SELECT
			SUM(fTuChi) AS ChiTieu,
			SUM(fHangNhap) as ChiTieu2,
			SUM(fHangMua) as ChiTieu3,
			iID_MaDonVi as Id_DonVi,
			iID_MLNS as MLNS_Id
		 FROM 
			NS_DT_ChungTuChiTiet
		 WHERE
		 	iID_DTChungTu 
		 	IN (
				SELECT
					iID_DTChungTu
				FROM NS_DT_ChungTu 
		 		WHERE
		 			 sSoQuyetDinh <> '' and sSoQuyetDinh is not null
		 			AND iLoai = 1
					--and @AgencyId = Id_DonVi
		 			AND INamLamViec = @YearOfWork
		 			AND INamNganSach = @YearOfBudget
		 			AND iID_MaNguonNganSach = @BudgetSource
		 			AND dNgayChungTu <= dateadd(dd,-1,(dateadd(mm,1,@VoucherDate)))
		 	)
			and iID_MaDonVi in (select * FROM f_split(@AgencyId)) 
		GROUP BY iID_MaDonVi, iID_MLNS
	) dtctct ON mlns.iID_MLNS = dtctct.MLNS_Id   
 
	WHERE
		mlns.INamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1

		--and (ctct.TuChi <> 0 or ctct.HienVat <> 0 or 
		--dtctct.ChiTieu <> 0 or		ctctdqt.DaCap <> 0)

	Order by mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;

 
	
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_export_phan_bo_du_toan_chi_tiet]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_export_phan_bo_du_toan_chi_tiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(100)
AS
BEGIN
	SELECT isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       ctct.iID_DTChungTu,
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
       ctct.iNamNganSach,
       ctct.iID_MaNguonNganSach,
       ctct.iNamLamViec,
       isnull(ctct.iPhanCap, 0) AS iPhanCap,
       ctct.iID_MaDonVi,
       isnull(ctct.sGhiChu, '') AS sGhiChu,
       isnull(ctct.fHangMua, 0) AS fHangMua,
       isnull(ctct.fHangNhap, 0) AS fHangNhap,
       isnull(ctct.fDuPhong, 0) AS fDuPhong,
       isnull(ctct.fPhanCap, 0) AS fPhanCap,
       isnull(ctct.fTuChi, 0) AS fTuChi,
       isnull(ctct.fHienVat, 0) AS fHienVat,
       ctct.dNgayTao,
       ctct.sNguoiTao,
       ctct.dNgaySua,
       ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   mlns.sChiTietToi,
	   dv.sTenDonVi,
	   mlns.bHangChaDuToan
	FROM
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
	     AND bHangChaDuToan IS NOT NULL
		 AND iTrangThai = 1
		 AND sLNS in
		   (SELECT *
			FROM f_split(@LNS))) mlns
	LEFT JOIN
	  (SELECT *
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iPhanCap = 1
		 AND iDuLieuNhan = 0
		 AND iID_DTChungTu = @ChungTuId ) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY mlns.sLNS,
			 mlns.sL,
			 mlns.sK,
			 mlns.sM,
			 mlns.sTM,
			 mlns.sTTM,
			 mlns.sNG,
			 mlns.sTNG;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Level int,
	@Status int
AS
BEGIN
	SELECT isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       ctct.iID_DTChungTu,
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
       isnull(mlns.bHangCha, 0) AS bHangCha,
	   isnull(mlns.bHangChaDuToan, 0) AS bHangChaDuToan,
       ctct.iNamNganSach,
       ctct.iID_MaNguonNganSach,
       ctct.iNamLamViec,
       isnull(ctct.iPhanCap, 0) AS iPhanCap,
       ctct.iID_MaDonVi,
       isnull(ctct.sGhiChu, '') AS sGhiChu,
       isnull(ctct.fHangMua, 0) AS fHangMua,
       isnull(ctct.fHangNhap, 0) AS fHangNhap,
       isnull(ctct.fDuPhong, 0) AS fDuPhong,
       isnull(ctct.fPhanCap, 0) AS fPhanCap,
       isnull(ctct.fTuChi, 0) AS fTuChi,
       isnull(ctct.fHienVat, 0) AS fHienVat,
       ctct.dNgayTao,
       ctct.sNguoiTao,
       ctct.dNgaySua,
       ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   mlns.sChiTietToi,
	   dv.sTenDonVi
	FROM NS_MucLucNganSach mlns
	LEFT JOIN
	  (SELECT *
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iPhanCap = @Level
		 AND iID_DTChungTu in
		   (SELECT DISTINCT iID_CTDuToan_Nhan
			FROM NS_DT_Nhan_PhanBo_Map
			WHERE iID_CTDuToan_PhanBo = @ChungTuId)
		 AND iDuLieuNhan = 0 ) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	WHERE mlns.iNamLamViec = @YearOfWork
	  AND mlns.sLNS in
		(SELECT *
		 FROM dbo.f_split(@LNS))
	  AND (isnull(ctct.fTuChi, 0) > 0
		   OR isnull(ctct.fHienVat, 0) > 0
		   OR isnull(ctct.fPhanCap, 0) > 0
		   OR isnull(ctct.fHangNhap, 0) > 0
		   OR isnull(ctct.fHangMua, 0) > 0)
	ORDER BY mlns.sLNS,
			 mlns.sL,
			 mlns.sK,
			 mlns.sM,
			 mlns.sTM,
			 mlns.sTTM,
			 mlns.sNG,
			 mlns.sTNG;
END
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 08/12/2021 2:57:30 PM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phan_bo_du_toan_chi_tiet_used]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_phan_bo_du_toan_chi_tiet_used]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(100)
AS
BEGIN
	SELECT isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       ctct.iID_DTChungTu,
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
       ctct.iNamNganSach,
       ctct.iID_MaNguonNganSach,
       ctct.iNamLamViec,
       isnull(ctct.iPhanCap, 0) AS iPhanCap,
       ctct.iID_MaDonVi,
       isnull(ctct.sGhiChu, '') AS sGhiChu,
       isnull(ctct.fHangMua, 0) AS fHangMua,
       isnull(ctct.fHangNhap, 0) AS fHangNhap,
       isnull(ctct.fDuPhong, 0) AS fDuPhong,
       isnull(ctct.fPhanCap, 0) AS fPhanCap,
       isnull(ctct.fTuChi, 0) AS fTuChi,
       isnull(ctct.fHienVat, 0) AS fHienVat,
       ctct.dNgayTao,
       ctct.sNguoiTao,
       ctct.dNgaySua,
       ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   mlns.sChiTietToi,
	   dv.sTenDonVi
	FROM
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
		 AND iTrangThai = 1
		 AND sLNS in
		   (SELECT *
			FROM f_split(@LNS))) mlns
	LEFT JOIN
	  (SELECT *
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iID_DTChungTu in
		   (SELECT DISTINCT iID_CTDuToan_PhanBo
			FROM NS_DT_Nhan_PhanBo_Map
			WHERE iID_CTDuToan_Nhan in
				(SELECT DISTINCT iID_CTDuToan_Nhan
				 FROM NS_DT_Nhan_PhanBo_Map
				 WHERE iID_CTDuToan_PhanBo= @ChungTuId)
			  AND iID_CTDuToan_PhanBo <> @ChungTuId) 
			AND iDuLieuNhan = 0
		) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	WHERE
		(SELECT dNgayChungTu
		 FROM NS_DT_ChungTu
		 WHERE iID_DTChungTu= ctct.iID_DTChungTu) < @VoucherDate
	  AND ctct.iID_CTDuToan_Nhan IS NOT NULL
	  AND (isnull(ctct.fTuChi, 0) > 0
		   OR isnull(ctct.fHienVat, 0) > 0
		   OR isnull(ctct.fPhanCap, 0) > 0
		   OR isnull(ctct.fHangNhap, 0) > 0
		   OR isnull(ctct.fHangMua, 0) > 0)
	ORDER BY mlns.sLNS,
			 mlns.sL,
			 mlns.sK,
			 mlns.sM,
			 mlns.sTM,
			 mlns.sTTM,
			 mlns.sNG,
			 mlns.sTNG;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phan_bo_du_toan_chi_tiet_used_luy_ke]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[sp_dt_phan_bo_du_toan_chi_tiet_used_luy_ke]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(100)
AS
BEGIN
	SELECT isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       ctct.iID_DTChungTu,
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
       ctct.iNamNganSach,
       ctct.iID_MaNguonNganSach,
       ctct.iNamLamViec,
       isnull(ctct.iPhanCap, 0) AS iPhanCap,
       ctct.iID_MaDonVi,
       isnull(ctct.sGhiChu, '') AS sGhiChu,
       isnull(ctct.fHangMua, 0) AS fHangMua,
       isnull(ctct.fHangNhap, 0) AS fHangNhap,
       isnull(ctct.fDuPhong, 0) AS fDuPhong,
       isnull(ctct.fPhanCap, 0) AS fPhanCap,
       isnull(ctct.fTuChi, 0) AS fTuChi,
       isnull(ctct.fHienVat, 0) AS fHienVat,
       ctct.dNgayTao,
       ctct.sNguoiTao,
       ctct.dNgaySua,
       ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   mlns.sChiTietToi,
	   dv.sTenDonVi
	FROM
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
		 AND iTrangThai = 1
		 AND sLNS in
		   (SELECT *
			FROM f_split(@LNS))) mlns
	LEFT JOIN
	  (SELECT *
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iID_DTChungTu in
		   (SELECT DISTINCT iID_CTDuToan_PhanBo
			FROM NS_DT_Nhan_PhanBo_Map
			WHERE iID_CTDuToan_Nhan in
				(SELECT DISTINCT iID_CTDuToan_Nhan
				 FROM NS_DT_Nhan_PhanBo_Map
				 WHERE iID_CTDuToan_PhanBo= @ChungTuId)
			  AND iID_CTDuToan_PhanBo <> @ChungTuId) 
			AND iDuLieuNhan = 0
		) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	WHERE
		(SELECT cast(dNgayChungTu as Date)
		 FROM NS_DT_ChungTu
		 WHERE iID_DTChungTu= ctct.iID_DTChungTu) < @VoucherDate
	  AND (isnull(ctct.fTuChi, 0) > 0
		   OR isnull(ctct.fHienVat, 0) > 0
		   OR isnull(ctct.fPhanCap, 0) > 0
		   OR isnull(ctct.fHangNhap, 0) > 0
		   OR isnull(ctct.fHangMua, 0) > 0)
	ORDER BY mlns.sLNS,
			 mlns.sL,
			 mlns.sK,
			 mlns.sM,
			 mlns.sTM,
			 mlns.sTTM,
			 mlns.sNG,
			 mlns.sTNG;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dutoan_chitiet]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_phanbo_dutoan_chitiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(100),
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT @CountDonViCha = count(*)
	FROM
	  (SELECT *
	   FROM NguoiDung_DonVi
	   WHERE iID_MaNguoiDung = @UserName
		 AND iNamLamViec = @YearOfWork
		 AND iTrangThai = 1) nddv
	INNER JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @YearOfWork
		 AND iTrangThai = 1
		 AND iLoai = 0) dv ON dv.iID_MaDonVi = nddv.iID_MaDonVi;


	SELECT isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
		   ctct.iID_DTChungTu,
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
		   ctct.iNamNganSach,
		   ctct.iID_MaNguonNganSach,
		   ctct.iNamLamViec,
		   isnull(ctct.iPhanCap, 0) AS iPhanCap,
		   ctct.iID_MaDonVi,
		   isnull(ctct.sGhiChu, '') AS sGhiChu,
		   isnull(ctct.fHangMua, 0) AS fHangMua,
		   isnull(ctct.fHangNhap, 0) AS fHangNhap,
		   isnull(ctct.fDuPhong, 0) AS fDuPhong,
		   isnull(ctct.fPhanCap, 0) AS fPhanCap,
		   isnull(ctct.fTuChi, 0) AS fTuChi,
		   isnull(ctct.fHienVat, 0) AS fHienVat,
		   ctct.dNgayTao,
		   ctct.sNguoiTao,
		   ctct.dNgaySua,
		   ctct.sNguoiSua, 
		   ctct.iID_CTDuToan_Nhan,
		   --ctct.Id_DotPhanBoTruoc,
		   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
		   mlns.sChiTietToi,
		   dv.sTenDonVi,
		   mlns.bHangChaDuToan
	FROM
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
		 AND iTrangThai = 1
		 AND bHangChaDuToan IS NOT NULL
		 AND ((@CountDonViCha <> 0
			   AND sLNS in
				 (SELECT *
				  FROM f_split(@LNS)))
			  OR (@CountDonViCha = 0
				  AND sLNS in
					(SELECT DISTINCT VALUE
					 FROM
					   (SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
							   CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
							   CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
							   CAST(sLNS AS nvarchar(10)) LNS
						FROM NS_NguoiDung_LNS
						WHERE sMaNguoiDung = @UserName
						  AND iNamLamViec = @YearOfWork
						  AND sLNS IN
							(SELECT *
							 FROM f_split(@LNS)) ) LNS UNPIVOT (value
																FOR col in (LNS1, LNS3, LNS5, LNS)) un))) ) mlns
	LEFT JOIN
	  (SELECT *
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iPhanCap = 1
		 AND iID_DTChungTu = @ChungTuId 
		 AND iDuLieuNhan = 0) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY mlns.sLNS,
			 mlns.sL,
			 mlns.sK,
			 mlns.sM,
			 mlns.sTM,
			 mlns.sTTM,
			 mlns.sNG,
			 mlns.sTNG;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(100)
AS
BEGIN
	SELECT isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       ctct.iID_DTChungTu,
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
       ctct.iNamNganSach,
       ctct.iID_MaNguonNganSach,
       ctct.iNamLamViec,
       isnull(ctct.iPhanCap, 0) AS iPhanCap,
       ctct.iID_MaDonVi,
       isnull(ctct.sGhiChu, '') AS sGhiChu,
       isnull(ctct.fHangMua, 0) AS fHangMua,
       isnull(ctct.fHangNhap, 0) AS fHangNhap,
       isnull(ctct.fDuPhong, 0) AS fDuPhong,
       isnull(ctct.fPhanCap, 0) AS fPhanCap,
       isnull(ctct.fTuChi, 0) AS fTuChi,
       isnull(ctct.fHienVat, 0) AS fHienVat,
	   isnull(ctct.fTonKho, 0) AS fTonKho,
       ctct.dNgayTao,
       ctct.sNguoiTao,
       ctct.dNgaySua,
       ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   mlns.sChiTietToi,
	   dv.sTenDonVi,
	   mlns.bHangChaDuToan
	FROM
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
	     AND bHangChaDuToan IS NOT NULL
		 AND iTrangThai = 1
		 AND sLNS in
		   (SELECT *
			FROM f_split(@LNS))) mlns
	LEFT JOIN
	  (SELECT *
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iPhanCap = 1 
		 AND iDuLieuNhan = 0) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY mlns.sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int
AS
BEGIN
	SELECT isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       ctct.iID_DTChungTu,
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
       ctct.iNamNganSach,
       ctct.iID_MaNguonNganSach,
       ctct.iNamLamViec,
       isnull(ctct.iPhanCap, 0) AS iPhanCap,
       ctct.iID_MaDonVi,
       isnull(ctct.sGhiChu, '') AS sGhiChu,
       isnull(ctct.fHangMua, 0) AS fHangMua,
       isnull(ctct.fHangNhap, 0) AS fHangNhap,
       isnull(ctct.fDuPhong, 0) AS fDuPhong,
       isnull(ctct.fPhanCap, 0) AS fPhanCap,
       isnull(ctct.fTuChi, 0) AS fTuChi,
       isnull(ctct.fHienVat, 0) AS fHienVat,
	   isnull(ctct.fTonKho, 0) AS fTonKho,
       ctct.dNgayTao,
       ctct.sNguoiTao,
       ctct.dNgaySua,
       ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   mlns.sChiTietToi,
	   dv.sTenDonVi,
	   mlns.bHangChaDuToan
	FROM
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
	     AND bHangChaDuToan IS NOT NULL
		 AND iTrangThai = 1
		 AND sLNS in
		   (SELECT *
			FROM f_split(@LNS))) mlns
	LEFT JOIN
	  (SELECT *
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iPhanCap = 0 
		 AND iDuLieuNhan = 0) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY mlns.sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_dutoan_tonghop]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_dutoan_tonghop]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	--@VoucherDate datetime,
	@ChungTuId nvarchar(max)
AS
BEGIN
	SELECT isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       ctct.iID_DTChungTu,
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
       ctct.iNamNganSach,
       ctct.iID_MaNguonNganSach,
       ctct.iNamLamViec,
       isnull(ctct.iPhanCap, 0) AS iPhanCap,
       ctct.iID_MaDonVi,
       isnull(ctct.sGhiChu, '') AS sGhiChu,
       isnull(ctct.fHangMua, 0) AS fHangMua,
       isnull(ctct.fHangNhap, 0) AS fHangNhap,
       isnull(ctct.fDuPhong, 0) AS fDuPhong,
       isnull(ctct.fPhanCap, 0) AS fPhanCap,
       isnull(ctct.fTuChi, 0) AS fTuChi,
       isnull(ctct.fHienVat, 0) AS fHienVat,
       ctct.dNgayTao,
       ctct.sNguoiTao,
       ctct.dNgaySua,
       ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   mlns.sChiTietToi,
	   dv.sTenDonVi
	FROM
	  (SELECT *
	   FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
	LEFT JOIN
	  (SELECT *
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iPhanCap = 0
		 AND iDuLieuNhan = 0
		 AND iID_DTChungTu IN
		   (SELECT iID_DTChungTu
			FROM NS_DT_ChungTu
			WHERE iLoai = 0
			  AND iNamLamViec = @YearOfWork
			  AND iNamNganSach = @YearOfBudget
			  AND iID_MaNguonNganSach = @BudgetSource
			  AND convert(nvarchar(MAX), iID_DTChungTu) IN
				(SELECT *
				 FROM dbo.splitstring(@ChungTuId)) ) ) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY mlns.sLNS,
			 mlns.sL,
			 mlns.sK,
			 mlns.sM,
			 mlns.sTM,
			 mlns.sTTM,
			 mlns.sNG,
			 mlns.sTNG;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_parent_dutoan_lns]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_parent_dutoan_lns]  
	 @NamLamViec int,																
	 @LNS nvarchar(max)
AS
BEGIN 
	SET NOCOUNT ON;
	SELECT * FROM f_get_mlns_by_lns(@NamLamViec,@LNS) 
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_lb_chungtu_chitiet]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_lb_chungtu_chitiet] 
@VoucherId nvarchar(255),
@LNS nvarchar(max),
@YearOfWork int,
@YearOfBudget int,
@BudgetSource int,
@AgencyId nvarchar(500),
@VoucherDate datetime,
@UserName nvarchar(100),
@LoaiChungTu nvarchar(20)
AS
BEGIN
SET NOCOUNT ON;

DECLARE @CountDonViCha int;

SELECT @CountDonViCha = count(*)
FROM
  (SELECT *
   FROM NguoiDung_DonVi
   WHERE iID_MaNguoiDung = @UserName
     AND iNamLamViec = @YearOfWork) nddv
INNER JOIN
  (SELECT *
   FROM DonVi
   WHERE iNamLamViec = @YearOfWork
  AND iLoai = 0) dv ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

SELECT DISTINCT isnull(ctct.iID_CTNganhChiTiet, NEWID()) AS Id,
                ctct.iID_CTNganh AS IdChungTu,
                mlns.iID_MLNS AS MlnsId,
                mlns.iID_MLNS_Cha AS MlnsIdParent,
                mlns.sXauNoiMa AS XauNoiMa,
                mlns.sLNS AS LNS,
                mlns.sL AS L,
                mlns.sK AS K,
                mlns.sM AS M,
                mlns.sTM AS TM,
                mlns.sTTM AS TTM,
                mlns.sNG AS NG,
                mlns.sTNG AS TNG,
                mlns.sTNG1 AS TNG1,
                mlns.sTNG2 AS TNG2,
                mlns.sTNG3 AS TNG3,
                mlns.sMoTa AS MoTa,
                mlns.bHangCha,
                isnull(ctct.iNamLamViec, 0) AS NamLamViec,
                isnull(ctct.iID_MaDonVi, '') AS IdDonVi,
                isnull(ctct.sTenDonVi, '') AS TenDonVi,
                isnull(ctct.fTuChi, 0) AS TuChi,
                isnull(ctct.fPhanCap, 0) AS PhanCap, --isnull(ctct.PhanCap, 0) - isnull(phancap.PhanCap, 0) AS ChuaPhanCap,
 isnull(ctct.fChuaPhanCap, 0) AS ChuaPhanCap,
 isnull(ctct.fHangNhap, 0) AS HangNhap,
 isnull(ctct.fHangMua, 0) AS HangMua,
 (isnull(ctct.fPhanCap, 0) - isnull(phancap.PhanCap, 0)) AS SoChuaPhan,
 isnull(phancap.PhanCap, 0) AS PhanCapCon,
 ctct.sGhiChu AS GhiChu,
 isnull(ctct.dNgayTao, getdate()) AS DateCreated,
 isnull(ctct.dNgaySua, getdate()) AS DateModified,
 ctct.sNguoiTao AS UserCreator,
 ctct.sNguoiSua AS UserModifier,
 isnull(ctct.iID_MaNguonNganSach, '') AS NguonNganSach
FROM NS_MucLucNganSach mlns
LEFT JOIN
  (SELECT chitiet.*
   FROM NS_Nganh_ChungTuChiTiet chitiet
   INNER JOIN NS_Nganh_ChungTu chungtu ON chitiet.iID_CTNganh = chungtu.iID_CTNganh
   WHERE chitiet.iID_CTNganh = @VoucherId
     AND chitiet.iNamLamViec = @YearOfWork
     AND chitiet.iNamNganSach = @YearOfBudget
     AND chitiet.iID_MaNguonNganSach = @BudgetSource ) ctct ON mlns.iID_MLNS = ctct.iID_MLNS

LEFT JOIN
  (SELECT SUM(fPhanCap) AS PhanCap,
          iID_CTNganhChiTiet,sXauNoiMa,REPLACE(sXauNoiMa,'1020100','1040100') AS sXauNoiMaReplace,
          iID_MLNS
   FROM NS_Nganh_ChungTuChiTiet_PhanCap
   WHERE iNamLamViec = @YearOfWork  
   GROUP BY iID_CTNganhChiTiet,sXauNoiMa,
            iID_MLNS
			) phancap ON 
			--mlns.iID_MLNS = phancap.iID_MLNS AND
			((ctct.sXauNoiMa = phancap.sXauNoiMa and @LoaiChungTu = '1')
			OR (ctct.sXauNoiMa = phancap.sXauNoiMaReplace and @LoaiChungTu = '2'  )
			) and ctct.iID_CTNganhChiTiet = phancap.iID_CTNganhChiTiet

WHERE mlns.iNamLamViec = @YearOfWork
  AND (mlns.sXauNoiMa like '104%'
       OR mlns.sXauNoiMa like '109%'
       OR mlns.sXauNoiMa like '2%'
       OR mlns.sXauNoiMa = '1')
  AND mlns.iTrangThai = 1
  AND mlns.bHangChaDuToan IS NOT NULL
  AND (mlns.sLNS in
         (SELECT DISTINCT VALUE
          FROM
            (SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
                    CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
                    CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
                    CAST(sLNS AS nvarchar(10)) LNS
             FROM NS_NguoiDung_LNS
             WHERE sMaNguoiDung = @UserName
               AND iNamLamViec = @YearOfWork
               AND sLNS IN
                 (SELECT *
                  FROM f_split(@LNS)) ) LNS UNPIVOT (value
                                                     FOR col in (LNS1, LNS3, LNS5, LNS)) un)
       OR @CountDonViCha <> 0)
ORDER BY mlns.sXauNoiMa;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_mlns_nhanuoc]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_mlns_nhanuoc] 
	@YearOfWork int,
	@LNS nvarchar(max),
	@GenerateAgencyId nvarchar(50)
AS
BEGIN
	WITH LNSTree
		AS
		(
			SELECT *
			FROM NS_MucLucNganSach
			WHERE 
				sL = ''
				AND sK = ''
				AND sM = ''
				AND sTM = ''
				AND sTTM = ''
				AND sNG = ''
				AND sTNG = ''
				AND iNamLamViec = @YearOfWork
				AND sLNS in (SELECT * FROM f_split(@LNS))
			UNION ALL
			SELECT
				mlnsChild.*
			FROM NS_MucLucNganSach mlnsChild
			INNER JOIN LNSTree
				ON mlnsChild.iID_MLNS_Cha = lnstree.iID_MLNS
			WHERE 
				mlnsChild.sL = '' 
				AND mlnsChild.sK = '' 
				AND mlnsChild.sM = '' 
				AND mlnsChild.sTM = '' 
				AND mlnsChild.sTTM = '' 
				AND mlnsChild.sNG = '' 
				AND mlnsChild.sTNG = ''
				AND mlnsChild.iNamLamViec = @YearOfWork
		)
		SELECT distinct * FROM LNSTree ORDER BY sLNS;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_mlns_quocphong]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_mlns_quocphong]
	@YearOfWork int,
	@LNS nvarchar(max),
	@GenerateAgencyId nvarchar(50)
AS
BEGIN
	WITH LNSTree
		AS
		(
			SELECT *
			FROM NS_MucLucNganSach
			WHERE 
				sL = ''
				AND sK = ''
				AND sM = ''
				AND sTM = ''
				AND sTTM = ''
				AND sNG = ''
				AND sTNG = ''
				AND iNamLamViec = @YearOfWork
				AND sLNS in (SELECT * FROM f_split(@LNS))
				AND sLNS <> '101'
			UNION ALL
			SELECT
				mlnsChild.*
			FROM NS_MucLucNganSach mlnsChild
			INNER JOIN LNSTree
				ON mlnsChild.iID_MLNS_Cha = lnstree.iID_MLNS
			WHERE 
				mlnsChild.sL = '' 
				AND mlnsChild.sK = '' 
				AND mlnsChild.sM = '' 
				AND mlnsChild.sTM = '' 
				AND mlnsChild.sTTM = '' 
				AND mlnsChild.sNG = '' 
				AND mlnsChild.sTNG = ''
				AND mlnsChild.iNamLamViec = @YearOfWork
				AND mlnsChild.sLNS <> '101'
		)
		SELECT DISTINCT * FROM LNSTree ORDER BY sLNS;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_mlns_get_by_user]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ns_mlns_get_by_user]
	@LNS nvarchar(max),
	@UserName nvarchar(100),
	@YearOfWork int,
	@LNSExcept nvarchar(10)
AS
BEGIN
	WITH LNSTree
		AS
		(
			SELECT *
			FROM NS_MucLucNganSach
			WHERE 
				sL = ''
				AND iNamLamViec = @YearOfWork
				AND (sLNS <> '' and sLNS in (SELECT * FROM f_split(@LNS)) or (@LNS = ''))
				AND ((@LNSExcept <> '' AND sLNS <> @LNSExcept) OR (@LNSExcept = ''))
			UNION ALL
			SELECT
				mlnsChild.*
			FROM NS_MucLucNganSach mlnsChild
			INNER JOIN LNSTree
				ON mlnsChild.iID_MLNS_Cha = lnstree.iID_MLNS
			WHERE 
				mlnsChild.sL = '' 
				AND mlnsChild.iNamLamViec = @YearOfWork
				AND ((@LNSExcept <> '' AND mlnsChild.sLNS <> @LNSExcept) OR (@LNSExcept = ''))
		)
		SELECT 
			mlns.* 
		FROM ( SELECT DISTINCT * FROM LNSTree) mlns
	    INNER JOIN 
		(
			SELECT 
				DISTINCT VALUE
			FROM 
			(
				SELECT 
					CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
					CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
					CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
					CAST(sLNS AS nvarchar(10)) LNS 
				FROM
					NS_NguoiDung_LNS 
				WHERE 
					sMaNguoiDung = @UserName 
					AND iNamLamViec = @YearOfWork
			) LNS
			UNPIVOT
			(
			  value
			  FOR col in (LNS1, LNS3, LNS5, LNS)
			) un) lns
		ON mlns.sLNS = lns.value
		ORDER BY sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 03/08/2022 6:18:16 PM ******/
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
	@UserName nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	Declare @YearOfBudgetCurrent int = 2,
		@YearOfBudgetAfter int = 4;
	Declare @voucherIndex int,
	@IsLocked bit,
	@STongHop nvarchar(500);
	SELECT @voucherIndex = iSoChungTuIndex, @IsLocked = bKhoa, @STongHop = sTongHop FROM NS_QT_ChungTu WHERE iID_QTChungTu = @VoucherId;

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
		ctct.iID_MaDonVi,
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
		mlns.bHangChaQuyetToan
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
	ON mlns.iID_MLNS = ctct.iID_MLNS 
	LEFT JOIN 
		(select 
			(case sLNS1
				when '1040200' then SUM(fHangNhap)
				when '1040300' then SUM(fHangMua)
				else SUM(fTuChi)
			end) as DuToan,
			iID_MLNS1 as iID_MLNS 
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
								((@STongHop IS NULL AND iID_MaDonVi like '%' + @AgencyId + '%') OR @STongHop IS NOT NULL)
								AND (iLoai = 1 or iLoai = 2)
								AND iNamLamViec = @YearOfWork
								AND iNamNganSach = @YearOfBudget
								AND iID_MaNguonNganSach = @BudgetSource
								AND bKhoa = 1
								AND cast(dNgayQuyetDinh as date) <= cast(@VoucherDate as date)
								AND sSoQuyetDinh is not null AND sSoQuyetDinh <> ''
						)
						and ((@STongHop IS NULL AND iID_MaDonVi = @AgencyId) OR @STongHop IS NOT NULL)
						and IDuLieuNhan = 0) ctct
					left join 
						(select '1040100' as sLNS1, * from NS_MucLucNganSach where sLNS in ('1040100', '1040200', '1040300') and iNamLamViec = @YearOfWork) mlns
					on mlns.sLNS1 = ctct.sLNS and mlns.sL = ctct.sL and mlns.sK = ctct.sK and mlns.sM = ctct.sM and mlns.sTM = ctct.sTM and mlns.sNG = ctct.sNG and mlns.sTTM = ctct.sTTM
				) dt
				GROUP BY iID_MLNS1, sLNS1
			) dtctct
	on mlns.iID_MLNS = dtctct.iID_MLNS
	LEFT JOIN 
		(SELECT
			SUM(fTuChi_PheDuyet) AS DaQuyetToan,
			iID_MLNS
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
					AND cast(dNgayChungTu as date) <= cast(@voucherDate as Date)
					AND iID_QTChungTu <> @VoucherId
			)
		 GROUP BY iID_MLNS
		) ctctdqt
	ON mlns.iID_MLNS = ctctdqt.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
	Order by mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG, mlns.sTNG1, mlns.sTNG2, mlns.sTNG3
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet_chitieu]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_chungtu_chitiet_chitieu]
	-- Add the parameters for the stored procedure here
	@VoucherId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type nvarchar(10),
	@AgencyId nvarchar(100),
	@VoucherDate datetime,
	@UserName nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	Declare @YearOfBudgetCurrent int = 2,
		@YearOfBudgetAfter int = 4;
	Declare @voucherIndex int,
	@IsLocked bit,
	@ITongHop nvarchar(500);
	SELECT @voucherIndex = iSoChungTuIndex, @IsLocked = bKhoa, @ITongHop = sTongHop FROM NS_QT_ChungTu WHERE iID_QTChungTu = @VoucherId;

	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT 
		isnull(ctct.iID_QTCTChiTiet, NEWID()) as Id,
		ctct.iID_QTChungTu as IdChungTu,
		mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdParent,
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
		isnull(ctct.iNamNganSach, @YearOfBudget) as NamNganSach,
		isnull(ctct.iID_MaNguonNganSach, @BudgetSource) as NguonNganSach,
		isnull(ctct.iNamLamViec, @YearOfWork) as iNamLamViec,
		isnull(ctct.iThangQuyLoai, 0) as iThangQuyLoai,
		isnull(ctct.iThangQuy, 0) as iThangQuy,
		ctct.iID_MaDonVi as IdDonVi,
		dv.sTenDonVi,
		isnull(ctct.fSoNguoi, '') as SoNguoi,
		isnull(ctct.fSoNgay,'') as SoNgay,
		isNull(ctct.fSoLuot, '') as SoLuot,
		isnull(ctct.fTuChi_DeNghi, '') as DeNghi,
		isnull(ctct.fTuChi_PheDuyet, '') as PheDuyet,
		ctct.sGhiChu,
		isnull(ctct.dNgayTao, getdate()) as DateCreated,
		isnull(ctct.dNgaySua, getdate()) as DateModified,
		ctct.sNguoiTao,
		ctct.sNguoiSua,
		dtctct.ChiTieu as ChiTieu,
		ctctdqt.DaQuyetToan as DaQuyetToan,
		mlns.sChiTietToi
	FROM 
	(
		select * from NS_MucLucNganSach where iNamLamViec = @YearOfWork and iTrangThai = 1 and
		(
			(@CountDonViCha > 0 and (@IsLocked = 1 or @ITongHop is not null) and sLNS in (select * from f_split(@LNS)))
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
	ON mlns.iID_MLNS = ctct.iID_MLNS 
	LEFT JOIN 
		(select 
			(case sLNS1
				when '1040200' then SUM(fHangNhap)
				when '1040300' then SUM(fHangMua)
				else SUM(fTuChi)
			end) as ChiTieu,
			iID_MLNS1 as iID_MLNS 
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
								iID_MaDonVi like '%' + @AgencyId + '%'
								AND iLoai = 1
								AND iNamLamViec = @YearOfWork
								AND iNamNganSach = @YearOfBudget
								AND iID_MaNguonNganSach = @BudgetSource
								AND dNgayChungTu <= dateadd(dd,-1,(dateadd(mm,1,@VoucherDate)))
								AND sSoQuyetDinh is not null AND sSoQuyetDinh <> ''
						)
						and iID_MaDonVi = @AgencyId
						and IDuLieuNhan is null) ctct
					left join 
						(select '1040100' as sLNS1, * from NS_MucLucNganSach where sLNS in ('1040100', '1040200', '1040300') and iNamLamViec = @YearOfWork) mlns
					on mlns.sLNS1 = ctct.sLNS and mlns.sL = ctct.sL and mlns.sK = ctct.sK and mlns.sM = ctct.sM and mlns.sTM = ctct.sTM and mlns.sNG = ctct.sNG and mlns.sTTM = ctct.sTTM
				) dt
				GROUP BY iID_MLNS1, sLNS1
			) dtctct
	on mlns.iID_MLNS = dtctct.iID_MLNS
	LEFT JOIN 
		(SELECT
			SUM(fTuChi_PheDuyet) AS DaQuyetToan,
			iID_MLNS
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
					AND ((dNgayChungTu < cast(@voucherDate as Date)) OR (iSoChungTuIndex < @voucherIndex AND dNgayChungTu = cast(@voucherDate as Date)))
					AND iID_QTChungTu <> @VoucherId
			)
		 GROUP BY iID_MLNS
		) ctctdqt
	ON mlns.iID_MLNS = ctctdqt.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
	Order by mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG, mlns.sTNG1, mlns.sTNG2, mlns.sTNG3
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_donvi]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_donvi]
	@YearOfWork int,
	@BudgetSource int,
	@DataType int,
	@LNS nvarchar(max)
AS
BEGIN
	SELECT dv.*
	FROM
	  (SELECT DISTINCT iID_MaDonVi
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec=@YearOfWork
		 AND iID_MaNguonNganSach=@BudgetSource
		 AND ((@DataType=0
			   AND (fTuChi_DeNghi<>0
					OR fTuChi_PheDuyet<>0))
			  OR (@DataType=1
				  AND fTuChi_DeNghi<>0)
			  OR (@DataType=2
				  AND fTuChi_DeNghi<>0))
		 AND (@LNS IS NULL
			  OR sLNS in
				(SELECT *
				 FROM f_split(@LNS)))
	   UNION SELECT DISTINCT iID_MaDonVi
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec=@YearOfWork
	     AND iDuLieuNhan = 0
		 AND iPhanCap=1
		 AND iID_MaNguonNganSach=@BudgetSource
		 AND ((@DataType=0
			   AND (fTuChi<>0
					OR fHienVat<>0))
			  OR (@DataType=1
				  AND fTuChi<>0)
			  OR (@DataType=2
				  AND fHienVat<>0))
		 AND (@LNS IS NULL
			  OR sLNS in
				(SELECT *
				 FROM f_split(@LNS))) )AS ct -- lay ten don vi

	INNER JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec=@YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	ORDER BY iID_MaDonVi

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_donvi_nam]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_donvi_nam]
	@YearOfWork int,
	@BudgetSource int,
	@DataType int,
	@LNS nvarchar(max),
	@Loai nvarchar(10)
AS
BEGIN
	SELECT dv.*
	FROM
	  (SELECT DISTINCT iID_MaDonVi
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec=@YearOfWork
		 AND iID_MaNguonNganSach=@BudgetSource
		 AND ((@DataType=0
			   AND (fTuChi_DeNghi<>0
					OR fTuChi_PheDuyet<>0))
			  OR (@DataType=1
				  AND fTuChi_DeNghi<>0)
			  OR (@DataType=2
				  AND fTuChi_DeNghi<>0))
		 AND (@LNS IS NULL
			  OR sLNS in
				(SELECT *
				 FROM f_split(@LNS)))
	   UNION SELECT DISTINCT iID_MaDonVi
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec=@YearOfWork
	     AND iDuLieuNhan = 0
		 AND iPhanCap=1
		 AND iID_MaNguonNganSach=@BudgetSource
		 AND ((@DataType=0
			   AND (fTuChi<>0
					OR fHienVat<>0))
			  OR (@DataType=1
				  AND fTuChi<>0)
			  OR (@DataType=2
				  AND fHienVat<>0))
		 AND (@LNS IS NULL
			  OR sLNS in
				(SELECT *
				 FROM f_split(@LNS))) )AS ct -- lay ten don vi

	INNER JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iLoai = @Loai
		 AND iNamLamViec=@YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	ORDER BY iID_MaDonVi

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_chung_tu]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_rpt_chung_tu]
	@VoucherId nvarchar(100),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(max),
	@QuarterMonthType int,
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@VoucherDate datetime,
	@Dvt int
AS
BEGIN
	DECLARE @STongHop nvarchar(500);
	SELECT @STongHop = sTongHop FROM NS_QT_ChungTu WHERE iID_QTChungTu = @VoucherId;
	SELECT iID_MLNS,
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi2 = SUM(TuChi2) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt,
		   HienVat = SUM(HienVat) / @Dvt,
		   SoNguoi = SUM(SoNguoi),
		   SoNgay = SUM(SoNgay),
		   SoLuot = SUM(SoLuot) 
		   INTO #tblData
	FROM
	  (--chi tieu theo don vi
	  select iID_MLNS,
			(case sLNS1
				when '1040200' then SUM(fHangNhap)
				when '1040300' then SUM(fHangMua)
				else SUM(fTuChi)
			end) as ChiTieu,
			TuChi2 = 0,
			TuChi = 0,
			HienVat = 0,
			SoNguoi = 0,
			SoNgay = 0,
			SoLuot = 0
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
								((@STongHop IS NULL AND iID_MaDonVi like '%' + @AgencyId + '%') OR @STongHop IS NOT NULL)
								AND iLoai = 1
								AND iNamLamViec = @YearOfWork
								AND iNamNganSach = @YearOfBudget
								AND iID_MaNguonNganSach = @BudgetSource
								AND cast(dNgayQuyetDinh as date) <= cast(@VoucherDate as date)
								AND sSoQuyetDinh is not null AND sSoQuyetDinh <> ''
						)
						AND (@lns IS NULL OR sLNS in (SELECT * FROM f_split(@lns)))
						and ((@STongHop IS NULL AND iID_MaDonVi = @AgencyId) OR @STongHop IS NOT NULL)
						and IDuLieuNhan = 0) ctct
					left join 
						(select '1040100' as sLNS1, * from NS_MucLucNganSach where sLNS in ('1040100', '1040200', '1040300') and iNamLamViec = @YearOfWork) mlns
					on mlns.sLNS1 = ctct.sLNS and mlns.sL = ctct.sL and mlns.sK = ctct.sK and mlns.sM = ctct.sM and mlns.sTM = ctct.sTM and mlns.sNG = ctct.sNG and mlns.sTTM = ctct.sTTM
				) dt
				GROUP BY iID_MLNS, sLNS1
	   --so da quyết toán
	   UNION ALL 
	   SELECT iID_MLNS,
			ChiTieu = 0,
			TuChi2 = fTuChi_PheDuyet,
			TuChi = 0,
			HienVat = 0,
			SoNguoi = 0,
			SoNgay = 0,
			SoLuot = 0
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iID_QTChungTu in
		   (SELECT iID_QTChungTu
			FROM NS_QT_ChungTu
			WHERE iNamLamViec = @YearOfWork
			  AND iNamNganSach = @YearOfBudget
			  AND iID_MaNguonNganSach = @BudgetSource
			  AND iThangQuyLoai = @QuarterMonthType
			  AND iThangQuy IN (SELECT * FROM f_split(@QuarterMonthBefore))
		 AND iID_QTChungTu != @VoucherId
		 AND iID_MaDonVi = @AgencyId
		 AND (@lns IS NULL OR sLNS in (SELECT * FROM splitstring(@lns))))
		 
	  -- -- quyet toan dot nay
	   UNION ALL 
	   SELECT iID_MLNS,
			ChiTieu = 0,
			TuChi2 = 0,
			fTuChi_PheDuyet AS TuChi,
			HienVat = 0,
			fSoNguoi AS SoNguoi,
			fSoNgay AS SoNgay,
			fSoLuot AS SoLuot
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iID_QTChungTu = @VoucherId
		 AND iID_MaDonVi = @AgencyId
		 AND (@lns IS NULL OR sLNS in (SELECT * FROM f_split(@lns)))
	) AS ct
	GROUP BY iID_MLNS
	
	SELECT mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
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
		mlns.sXauNoiMa,
		mlns.sMoTa,
		mlns.bHangCha as IsHangCha,
		isnull(dt.ChiTieu, 0) as ChiTieu,
		isnull(dt.TuChi2, 0) as TuChi2,
		isnull(dt.TuChi, 0) as TuChi,
		isnull(dt.TuChi2, 0) + isnull(dt.TuChi, 0) as ThucChi,
		isnull(dt.HienVat, 0) as HienVat,
		isnull(dt.SoNguoi, 0) as SoNguoi,
		isnull(dt.SoNgay, 0) as SoNgay,
		isnull(dt.SoLuot, 0) as SoLuot
	FROM (
		SELECT *
		   FROM NS_MucLucNganSach
		   WHERE iTrangThai=1
			 AND iNamLamViec = @YearOfWork
			 AND sLNS in
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
							CAST(sLNS AS nvarchar(10)) LNS
					FROM f_split(@LNS)) LNS UNPIVOT (value FOR col in (LNS1, LNS3, LNS5, LNS)) un)) mlns
	LEFT JOIN #tblData dt
	ON dt.iID_MLNS = mlns.iID_MLNS
	WHERE bHangCha = 1 OR (bHangCha = 0 AND (dt.TuChi <> 0 OR dt.ChiTieu <> 0 OR dt.TuChi2 <> 0 OR dt.HienVat <> 0 OR dt.SoNgay <> 0 OR dt.SoNguoi <> 0 OR dt.SoLuot <> 0))
	ORDER BY mlns.sXauNoiMa
	DROP TABLE #tblData;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_chungtu_tonghop]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_rpt_chungtu_tonghop]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(max),
	@AgencyId nvarchar(max),
	@VoucherDate datetime,
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@Dvt int
AS
BEGIN
	SELECT iID_MLNS,
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi2 = SUM(TuChi2) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt,
		   HienVat = SUM(HienVat) / @Dvt,
		   SoNguoi = SUM(SoNguoi),
		   SoNgay = SUM(SoNgay),
		   SoLuot = SUM(SoLuot)
		   into #tblData
	FROM
	  (-- dự toán
	  SELECT iID_MLNS,
			ChiTieu = TuChi,
			TuChi2 = 0,
			TuChi = 0,
			HienVat = 0,
			SoNguoi = 0,
			SoNgay = 0,
			SoLuot = 0
	   FROM f_dutoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @EstimateAgencyId, @lns) 

	   --so da quyết toán
	   UNION ALL 
	   SELECT iID_MLNS,
			ChiTieu = 0,
			TuChi2 = TuChi,
			TuChi = 0,
			HienVat = 0,
			SoNguoi = 0,
			SoNgay = 0,
			SoLuot = 0
	   FROM f_quyettoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthBefore, @AgencyId, @lns) 
	   -- quyet toan dot nay
	   UNION ALL 
	   SELECT iID_MLNS,
			ChiTieu = 0,
			TuChi2 = 0,
			TuChi,
			HienVat,
			SoNguoi,
			SoNgay,
			SoLuot
	   FROM f_quyettoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @lns)
	) AS ct
	GROUP BY iID_MLNS;

	SELECT mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
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
		mlns.sXauNoiMa,
		mlns.sMoTa,
		mlns.bHangCha as IsHangCha,
		isnull(dt.ChiTieu, 0) as ChiTieu,
		isnull(dt.TuChi2, 0) as TuChi2,
		isnull(dt.TuChi, 0) as TuChi,
		isnull(dt.TuChi2, 0) + isnull(dt.TuChi, 0) as ThucChi,
		isnull(dt.HienVat, 0) as HienVat,
		isnull(dt.SoNguoi, 0) as SoNguoi,
		isnull(dt.SoNgay, 0) as SoNgay,
		isnull(dt.SoLuot, 0) as SoLuot
	FROM (
		SELECT *
		   FROM NS_MucLucNganSach
		   WHERE iTrangThai=1
			 AND iNamLamViec = @YearOfWork
			 AND sLNS in
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
							CAST(sLNS AS nvarchar(10)) LNS
					FROM f_split(@LNS)) LNS UNPIVOT (value FOR col in (LNS1, LNS3, LNS5, LNS)) un)) mlns
	LEFT JOIN #tblData dt
	ON dt.iID_MLNS = mlns.iID_MLNS
	WHERE bHangCha = 1 OR (bHangCha = 0 AND (dt.TuChi <> 0 OR dt.ChiTieu <> 0 OR dt.TuChi2 <> 0 OR dt.HienVat <> 0 OR dt.SoNgay <> 0 OR dt.SoNguoi <> 0 OR dt.SoLuot <> 0))
	ORDER BY mlns.sXauNoiMa
	DROP TABLE #tblData;
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_chungtu_tonghop_qp]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_chungtu_tonghop_qp]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(50),
	@VoucherDate datetime,
	@QuarterMonthType int,
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(10),
	@Dvt int
as
BEGIN

	SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct.sXauNoiMa,
		   mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sMoTa,
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi2 = SUM(TuChi2) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt,
		   HienVat = SUM(HienVat) / @Dvt,
		   SoNguoi = SUM(SoNguoi),
		   SoNgay = SUM(SoNgay),
		   SoLuot = SUM(SoLuot) INTO #tblQuartlySUMmaryQP
	FROM
	  (--chi tieu theo don vi
	 SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			ChiTieu =TuChi,
			TuChi2 = 0,
			TuChi = 0,
			HienVat = 0,
			SoNguoi = 0,
			SoNgay = 0,
			SoLuot = 0
	   FROM f_ns(@YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @AgencyId, @LNS) --so da quyết toán

	   UNION ALL 
	   SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			ChiTieu = 0,
			TuChi2 =TuChi,
			TuChi = 0,
			HienVat = 0,
			SoNguoi = 0,
			SoNgay = 0,
			SoLuot = 0
	   FROM f_qt_done(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthType, @QuarterMonthBefore, @AgencyId, @LNS) -- quyet toan dot nay

	   UNION ALL 
	   SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			ChiTieu = 0,
			TuChi2 = 0,
			TuChi,
			HienVat,
			SoNguoi,
			SoNgay,
			SoLuot
	   FROM f_qt_dot(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthType, @QuarterMonth, @AgencyId, @LNS))AS ct --mota lns
	LEFT JOIN
	  (SELECT sMoTa,
			  sXauNoiMa,
			  iID_MLNS,
			  iID_MLNS_Cha
	   FROM NS_MucLucNganSach
	   WHERE iTrangThai=1
		 AND iNamLamViec=@YearOfWork) AS mlns ON mlns.sXauNoiMa = ct.sXauNoiMa
	GROUP BY sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct.sXauNoiMa,
			 mlns.iID_MLNS,
			 mlns.iID_MLNS_Cha,
			 mlns.sMoTa
	HAVING SUM(ChiTieu) <> 0
	OR SUM(TuChi2) <> 0
	OR SUM(TuChi) <> 0
	OR SUM(HienVat) <> 0;

	WITH LNSTreeParent AS
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
			cast(ChiTieu AS float) AS ChiTieu,
			cast(TuChi2 AS float) AS TuChi2,
			cast(TuChi AS float) AS TuChi,
			cast(HienVat AS float) AS HienVat,
			cast(SoNguoi AS float) AS SoNguoi,
			cast(SoNgay AS float) AS SoNgay,
			cast(SoLuot AS float) AS SoLuot,
			ThucChi = TuChi2 + TuChi,
			sMoTa,
			cast(0 AS bit) AS IsHangCha
	   FROM #tblQuartlySUMmaryQP
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
			cast(0 AS float) AS ChiTieu,
			cast(0 AS float) AS TuChi2,
			cast(0 AS float) AS TuChi,
			cast(0 AS float) AS HienVat,
			cast(0 AS float) AS SoNguoi,
			cast(0 AS float) AS SoNgay,
			cast(0 AS float) AS SoLuot,
			cast(0 AS float) AS ThucChi,
			mlnsParent.sMoTa,
			cast(1 AS bit) AS IsHangCha
	   FROM NS_MucLucNganSach mlnsParent
	   INNER JOIN LNSTreeParent ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
	   WHERE mlnsParent.iNamLamViec = @YearOfWork
	 )
	SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa, iID_MLNS, iID_MLNS_Cha, sMoTa,
	sum(ChiTieu) as ChiTieu, sum(TuChi2) as TuChi2, sum(TuChi) as TuChi,
	sum(HienVat) as HienVat, sum(SoNguoi) as SoNguoi, sum(SoNgay) as SoNgay, sum(SoLuot) as SoLuot, sum(ThucChi) as ThucChi,
	cast(max(convert(int, IsHangCha)) as bit) as IsHangCha
	FROM LNSTreeParent
	group by sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa, iID_MLNS, iID_MLNS_Cha, sMoTa
	ORDER BY sLNS,
			 sL,
			 sK,
			 sM,
			 sTM,
			 sTTM,
			 sNG,
			 sTNG1,
			 sTNG2,
			 sTNG3;
	DROP TABLE #tblQuartlySUMmaryQP;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(100),
	@AgencyId nvarchar(100),
	@LNS nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@VoucherDate datetime,
	@Dvt int
AS
BEGIN
	DECLARE @IsDonViCha bit;
	IF @EstimateAgencyId = @AgencyId
		SET @IsDonViCha = 0
	ELSE
		SET @IsDonViCha = 1
	SELECT sLNS,
		   iID_MLNS,
		   TenDonVi,
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi2 = SUM(TuChi2) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt 
		   INTO #tblEstimateSettlement
	FROM
	  (--chi tieu theo don vi
	  SELECT sLNS,
			iID_MLNS,
			iID_MaDonVi,
			ChiTieu = fTuChi,
			TuChi2 = 0,
			TuChi = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iID_DTChungTu in 
		(
			SELECT iID_DTChungTu 
			FROM NS_DT_ChungTu 
			WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND (sSoQuyetDinh is not null or sSoQuyetDinh <> '')
				AND cast(dNgayQuyetDinh as DATE) <= cast(@VoucherDate as date)
		)
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@EstimateAgencyId)))
		 AND (@LNS IS NULL OR sLNS like @LNS + '%')
		 AND IDuLieuNhan = 0
		 
	   --số đã quyết toán
	   UNION ALL 
	   SELECT sLNS,
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu =0,
			TuChi2 = fTuChi_PheDuyet,
			TuChi = 0
	   FROM f_quyettoan(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthBefore, @AgencyId, @LNS)
		 
	   --quyết toán đợt này
	   UNION ALL 
	   SELECT sLNS,
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu = 0,
			TuChi2 = 0,
			fTuChi_PheDuyet AS TuChi
	   FROM f_quyettoan(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)) AS ct
	LEFT JOIN
	  (SELECT iID_MaDonVi,
			  TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	GROUP BY 
			 sLNS,
			 TenDonVi,
			 iID_MLNS
	HAVING SUM(tuchi) <> 0
	OR SUM(TuChi2) <> 0
	OR SUM(ChiTieu) <> 0;

	select distinct sLNS into #tblLNS from #tblEstimateSettlement;
	SELECT mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
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
		mlns.sXauNoiMa,
		mlns.sMoTa,
		mlns.bHangCha as IsHangCha,
		isnull(dt.ChiTieu, 0) as ChiTieu,
		isnull(dt.TuChi2, 0) as TuChi2,
		isnull(dt.TuChi, 0) as TuChi,
		TenDonVi
	FROM (
		SELECT *
		   FROM NS_MucLucNganSach
		   WHERE iTrangThai=1
			 AND iNamLamViec = @YearOfWork
			 AND sLNS in
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
							CAST(sLNS AS nvarchar(10)) LNS
					FROM #tblLNS) LNS UNPIVOT (value FOR col in (LNS1, LNS3, LNS5, LNS)) un)) mlns
	LEFT JOIN #tblEstimateSettlement dt
	ON dt.iID_MLNS = mlns.iID_MLNS
	WHERE bHangCha = 1 OR (bHangCha = 0 AND (dt.TuChi <> 0 OR dt.ChiTieu <> 0 OR dt.TuChi2 <> 0))
	ORDER BY mlns.sXauNoiMa
	DROP TABLE #tblEstimateSettlement, #tblLNS;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_quy]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_quy]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(100),
	@AgencyId nvarchar(100),
	@LNS nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@VoucherDate datetime,
	@Dvt int
AS
BEGIN
	DECLARE @IsDonViCha bit;
	IF @EstimateAgencyId = @AgencyId
		SET @IsDonViCha = 0
	ELSE
		SET @IsDonViCha = 1
	SELECT sLNS,
		   iID_MLNS,
		   TenDonVi,
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt,
		   Quy1 = SUM(Quy1) / @Dvt,
		   Quy2 = SUM(Quy2) / @Dvt,
		   Quy3 = SUM(Quy3) / @Dvt,
		   Quy4 = SUM(Quy4) / @Dvt
		   INTO #tblEstimateSettlement
	FROM
	  (--chi tieu theo don vi
	  SELECT sLNS,
			iID_MLNS,
			iID_MaDonVi,
			ChiTieu = fTuChi,
			TuChi = 0,
			Quy1 = 0,
			Quy2 = 0,
			Quy3 = 0,
			Quy4 = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iID_DTChungTu in 
		(
			SELECT iID_DTChungTu 
			FROM NS_DT_ChungTu 
			WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND (sSoQuyetDinh is not null or sSoQuyetDinh <> '')
				AND cast(dNgayQuyetDinh as DATE) <= cast(@VoucherDate as date)
		)
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@EstimateAgencyId)))
		 AND (@LNS IS NULL OR sLNS like @LNS + '%')
		 AND IDuLieuNhan = 0
		 
	   UNION ALL 
	   SELECT sLNS,
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu = 0,
			TuChi = fTuChi_PheDuyet,
			Quy1 = 0,
			Quy2 = 0,
			Quy3 = 0,
			Quy4 = 0
	   FROM f_quyettoan(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)

	   UNION ALL 
	   select sLNS, 
			iID_MLNS, 
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu = 0,
			TuChi = 0,
			isnull([1], 0) AS Quy1,
			isnull([2], 0) AS Quy2,
			isnull([3], 0) AS Quy3,
			isnull([4], 0) AS Quy4
			from (
				select sLNS, iID_MLNS, iID_MaDonVi, isnull(fTuChi_PheDuyet, 0) as TuChi,
					case 
						when iThangQuy = 1 then 1
						when iThangQuy = 2 then 1
						when iThangQuy = 3 then 1
						when iThangQuy = 4 then 2
						when iThangQuy = 5 then 2
						when iThangQuy = 6 then 2
						when iThangQuy = 7 then 3
						when iThangQuy = 8 then 3
						when iThangQuy = 9 then 3
						when iThangQuy = 10 then 4
						when iThangQuy = 11 then 4
						when iThangQuy = 12 then 4
					end as quy
				FROM NS_QT_ChungTuChiTiet
				WHERE iID_QTChungTu in
					(SELECT iID_QTChungTu
					FROM NS_QT_ChungTu
					WHERE iNamLamViec = @YearOfWork
						AND INamNganSach = @YearOfBudget
						AND iID_MaNguonNganSach = @BudgetSource
						AND iThangQuy in (select * from f_split(@QuarterMonth))
						--AND iThangQuyLoai = @QuarterMonthType
						)
					AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
					AND (@LNS IS NULL OR sLNS like @lns + '%')
			) as data
			pivot 
			(
				SUM(TuChi) for quy IN ( [1], [2], [3], [4] )
			) as Thang
	) AS ct
	LEFT JOIN
	  (SELECT iID_MaDonVi,
			  TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	GROUP BY 
			 sLNS,
			 TenDonVi,
			 iID_MLNS
	HAVING SUM(TuChi) <> 0
	OR SUM(ChiTieu) <> 0
	OR SUM(Quy1) <> 0
	OR SUM(Quy2) <> 0
	OR SUM(Quy3) <> 0
	OR SUM(Quy4) <> 0;

	select distinct sLNS into #tblLNS from #tblEstimateSettlement;
	SELECT mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
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
		mlns.sXauNoiMa,
		mlns.sMoTa,
		mlns.bHangCha as IsHangCha,
		isnull(dt.ChiTieu, 0) as ChiTieu,
		isnull(dt.TuChi, 0) as TuChi,
		isnull(Quy1, 0) as Quy1,
		isnull(Quy2, 0) as Quy2,
		isnull(Quy3, 0) as Quy3,
		isnull(Quy4, 0) as Quy4,
		TenDonVi
	FROM (
		SELECT *
		   FROM NS_MucLucNganSach
		   WHERE iTrangThai=1
			 AND iNamLamViec = @YearOfWork
			 AND sLNS in
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
							CAST(sLNS AS nvarchar(10)) LNS
					FROM #tblLNS) LNS UNPIVOT (value FOR col in (LNS1, LNS3, LNS5, LNS)) un)) mlns
	LEFT JOIN #tblEstimateSettlement dt
	ON dt.iID_MLNS = mlns.iID_MLNS
	WHERE bHangCha = 1 OR (bHangCha = 0 AND (dt.TuChi <> 0 OR dt.ChiTieu <> 0 OR dt.Quy1 <> 0 OR dt.Quy2 <> 0
		OR dt.Quy3 <> 0 OR dt.Quy4 <> 0))
	ORDER BY mlns.sXauNoiMa
	DROP TABLE #tblEstimateSettlement, #tblLNS;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_thang]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_thang]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(100),
	@AgencyId nvarchar(100),
	@LNS nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@VoucherDate datetime,
	@Dvt int
AS
BEGIN
	DECLARE @IsDonViCha bit;
	IF @EstimateAgencyId = @AgencyId
		SET @IsDonViCha = 0
	ELSE
		SET @IsDonViCha = 1
	SELECT sLNS,
		   iID_MLNS,
		   TenDonVi,
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt,
		   Thang1 = SUM(Thang1) / @Dvt,
		   Thang2 = SUM(Thang2) / @Dvt,
		   Thang3 = SUM(Thang3) / @Dvt,
		   Thang4 = SUM(Thang4) / @Dvt,
		   Thang5 = SUM(Thang5) / @Dvt,
		   Thang6 = SUM(Thang6) / @Dvt,
		   Thang7 = SUM(Thang7) / @Dvt,
		   Thang8 = SUM(Thang8) / @Dvt,
		   Thang9 = SUM(Thang9) / @Dvt,
		   Thang10 = SUM(Thang10) / @Dvt,
		   Thang11 = SUM(Thang11) / @Dvt,
		   Thang12 = SUM(Thang12) / @Dvt
		   INTO #tblEstimateSettlement
	FROM
	  (--chi tieu theo don vi
	  SELECT sLNS,
			iID_MLNS,
			iID_MaDonVi,
			ChiTieu = fTuChi,
			TuChi = 0,
			Thang1 = 0,
			Thang2 = 0,
			Thang3 = 0,
			Thang4 = 0,
			Thang5 = 0,
			Thang6 = 0,
			Thang7 = 0,
			Thang8 = 0, 
			Thang9 = 0,
			Thang10 = 0,
			Thang11 = 0,
			Thang12 = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iID_DTChungTu in 
		(
			SELECT iID_DTChungTu 
			FROM NS_DT_ChungTu 
			WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND (sSoQuyetDinh is not null or sSoQuyetDinh <> '')
				AND cast(dNgayQuyetDinh as DATE) <= cast(@VoucherDate as date)
		)
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@EstimateAgencyId)))
		 AND (@LNS IS NULL OR sLNS like @LNS + '%')
		 AND IDuLieuNhan = 0
		 
	   UNION ALL 
	   SELECT sLNS,
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu = 0,
			TuChi = fTuChi_PheDuyet,
			Thang1 = 0,
			Thang2 = 0,
			Thang3 = 0,
			Thang4 = 0,
			Thang5 = 0,
			Thang6 = 0,
			Thang7 = 0,
			Thang8 = 0, 
			Thang9 = 0,
			Thang10 = 0,
			Thang11 = 0,
			Thang12 = 0
	   FROM f_quyettoan(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)

	   UNION ALL 
	   select sLNS, 
			iID_MLNS, 
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu = 0,
			TuChi = 0,
			isnull([1], 0) AS Thang1,
			isnull([2], 0) AS Thang2,
			isnull([3], 0) AS Thang3,
			isnull([4], 0) AS Thang4,
			isnull([5], 0) AS Thang5,
			isnull([6], 0) AS Thang6,
			isnull([7], 0) AS Thang7,
			isnull([8], 0) AS Thang8,
			isnull([9], 0) AS Thang9,
			isnull([10], 0) AS Thang10,
			isnull([11], 0) AS Thang11,
			isnull([12], 0) AS Thang12
			from (
				select sLNS, iID_MLNS, iID_MaDonVi, isnull(fTuChi_PheDuyet, 0) as TuChi, iThangQuy 
				FROM NS_QT_ChungTuChiTiet
				WHERE iID_QTChungTu in
					(SELECT iID_QTChungTu
					FROM NS_QT_ChungTu
					WHERE iNamLamViec = @YearOfWork
						AND INamNganSach = @YearOfBudget
						AND iID_MaNguonNganSach = @BudgetSource
						AND iThangQuy in (select * from f_split(@QuarterMonth))
						--AND iThangQuyLoai = @QuarterMonthType
						)
					AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
					AND (@LNS IS NULL OR sLNS like @lns + '%')
			) as data
			pivot 
			(
				SUM(TuChi) for iThangQuy IN ( [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12] )
			) as Thang
	) AS ct
	LEFT JOIN
	  (SELECT iID_MaDonVi,
			  TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	GROUP BY 
			 sLNS,
			 TenDonVi,
			 iID_MLNS
	HAVING SUM(TuChi) <> 0
	OR SUM(ChiTieu) <> 0
	OR SUM(Thang1) <> 0
	OR SUM(Thang2) <> 0
	OR SUM(Thang3) <> 0
	OR SUM(Thang4) <> 0
	OR SUM(Thang5) <> 0
	OR SUM(Thang6) <> 0
	OR SUM(Thang7) <> 0
	OR SUM(Thang8) <> 0
	OR SUM(Thang9) <> 0
	OR SUM(Thang10) <> 0
	OR SUM(Thang11) <> 0
	OR SUM(Thang12) <> 0;

	select distinct sLNS into #tblLNS from #tblEstimateSettlement;
	SELECT mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
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
		mlns.sXauNoiMa,
		mlns.sMoTa,
		mlns.bHangCha as IsHangCha,
		isnull(dt.ChiTieu, 0) as ChiTieu,
		isnull(dt.TuChi, 0) as TuChi,
		isnull(Thang1, 0) as Thang1,
		isnull(Thang2, 0) as Thang2,
		isnull(Thang3, 0) as Thang3,
		isnull(Thang4, 0) as Thang4,
		isnull(Thang5, 0) as Thang5,
		isnull(Thang6, 0) as Thang6,
		isnull(Thang7, 0) as Thang7,
		isnull(Thang8, 0) as Thang8,
		isnull(Thang9, 0) as Thang9,
		isnull(Thang10, 0) as Thang10,
		isnull(Thang11, 0) as Thang11,
		isnull(Thang12, 0) as Thang12,
		TenDonVi
	FROM (
		SELECT *
		   FROM NS_MucLucNganSach
		   WHERE iTrangThai=1
			 AND iNamLamViec = @YearOfWork
			 AND sLNS in
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
							CAST(sLNS AS nvarchar(10)) LNS
					FROM #tblLNS) LNS UNPIVOT (value FOR col in (LNS1, LNS3, LNS5, LNS)) un)) mlns
	LEFT JOIN #tblEstimateSettlement dt
	ON dt.iID_MLNS = mlns.iID_MLNS
	WHERE bHangCha = 1 OR (bHangCha = 0 AND (dt.TuChi <> 0 OR dt.ChiTieu <> 0 OR dt.Thang1 <> 0 OR dt.Thang2 <> 0
		OR dt.Thang3 <> 0 OR dt.Thang4 <> 0 OR dt.Thang5 <> 0 OR dt.Thang6 <> 0 OR dt.Thang7 <> 0 OR dt.Thang8 <> 0
		OR dt.Thang9 <> 0 OR dt.Thang10 <> 0 OR dt.Thang11 <> 0 OR dt.Thang12 <> 0))
	ORDER BY mlns.sXauNoiMa
	DROP TABLE #tblEstimateSettlement, #tblLNS;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_tonghop_thang]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_dutoan_tonghop_thang]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(100),
	@AgencyId nvarchar(100),
	@LNS nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@VoucherDate datetime,
	@Dvt int
AS
BEGIN
	SELECT dv.iID_MaDonVi,
		   TenDonVi,
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt,
		   Thang1 = SUM(Thang1) / @Dvt,
		   Thang2 = SUM(Thang2) / @Dvt,
		   Thang3 = SUM(Thang3) / @Dvt,
		   Thang4 = SUM(Thang4) / @Dvt,
		   Thang5 = SUM(Thang5) / @Dvt,
		   Thang6 = SUM(Thang6) / @Dvt,
		   Thang7 = SUM(Thang7) / @Dvt,
		   Thang8 = SUM(Thang8) / @Dvt,
		   Thang9 = SUM(Thang9) / @Dvt,
		   Thang10 = SUM(Thang10) / @Dvt,
		   Thang11 = SUM(Thang11) / @Dvt,
		   Thang12 = SUM(Thang12) / @Dvt
	FROM
	  (--chi tieu theo don vi
	  SELECT 
			iID_MaDonVi,
			ChiTieu = fTuChi,
			TuChi = 0,
			Thang1 = 0,
			Thang2 = 0,
			Thang3 = 0,
			Thang4 = 0,
			Thang5 = 0,
			Thang6 = 0,
			Thang7 = 0,
			Thang8 = 0, 
			Thang9 = 0,
			Thang10 = 0,
			Thang11 = 0,
			Thang12 = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS like @LNS + '%')
		 AND IDuLieuNhan = 0
		 AND iID_DTChungTu IN (
			SELECT iID_DTChungTu
			FROM NS_DT_ChungTu
			WHERE (sSoQuyetDinh IS NOT NULL OR sSoQuyetDinh <> '')
				AND cast(dNgayQuyetDinh as date) <= cast(@VoucherDate AS date))
		 
	   UNION ALL 
	   SELECT 
			iID_MaDonVi,
			ChiTieu = 0,
			TuChi = fTuChi_PheDuyet,
			Thang1 = 0,
			Thang2 = 0,
			Thang3 = 0,
			Thang4 = 0,
			Thang5 = 0,
			Thang6 = 0,
			Thang7 = 0,
			Thang8 = 0, 
			Thang9 = 0,
			Thang10 = 0,
			Thang11 = 0,
			Thang12 = 0
	   FROM f_quyettoan(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)

	   UNION ALL 
	   select 
			iID_MaDonVi,
			ChiTieu = 0,
			TuChi = 0,
			isnull([1], 0) AS Thang1,
			isnull([2], 0) AS Thang2,
			isnull([3], 0) AS Thang3,
			isnull([4], 0) AS Thang4,
			isnull([5], 0) AS Thang5,
			isnull([6], 0) AS Thang6,
			isnull([7], 0) AS Thang7,
			isnull([8], 0) AS Thang8,
			isnull([9], 0) AS Thang9,
			isnull([10], 0) AS Thang10,
			isnull([11], 0) AS Thang11,
			isnull([12], 0) AS Thang12
			from (
				select sLNS, iID_MLNS, iID_MaDonVi, isnull(fTuChi_PheDuyet, 0) as TuChi, iThangQuy 
				FROM NS_QT_ChungTuChiTiet
				WHERE iID_QTChungTu in
					(SELECT iID_QTChungTu
					FROM NS_QT_ChungTu
					WHERE iNamLamViec = @YearOfWork
						AND INamNganSach = @YearOfBudget
						AND iID_MaNguonNganSach = @BudgetSource
						AND iThangQuy in (select * from f_split(@QuarterMonth))
						)
					AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
					AND (@LNS IS NULL OR sLNS like @lns + '%')
			) as data
			pivot 
			(
				SUM(TuChi) for iThangQuy IN ( [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12] )
			) as Thang
	) AS ct
	LEFT JOIN
	  (SELECT iID_MaDonVi,
			  TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	GROUP BY dv.iID_MaDonVi,
			 TenDonVi
	HAVING SUM(TuChi) <> 0
	OR SUM(ChiTieu) <> 0
	OR SUM(Thang1) <> 0
	OR SUM(Thang2) <> 0
	OR SUM(Thang3) <> 0
	OR SUM(Thang4) <> 0
	OR SUM(Thang5) <> 0
	OR SUM(Thang6) <> 0
	OR SUM(Thang7) <> 0
	OR SUM(Thang8) <> 0
	OR SUM(Thang9) <> 0
	OR SUM(Thang10) <> 0
	OR SUM(Thang11) <> 0
	OR SUM(Thang12) <> 0
	ORDER BY dv.iID_MaDonVi

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_quyetoannam_donvi]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_quyetoannam_donvi]
	@YearOfWork int,
	@AgencyId nvarchar(100),
	@LNS nvarchar(max),
	@DataType int,
	@Dvt int
AS
BEGIN

	SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct.sXauNoiMa,
		   mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sMoTa,
		   iID_MaDonVi,
		   TenDonVi,
		   ChiTieuNamNay = SUM(ChiTieuNamNay) / @Dvt,
		   ChiTieuNamSau = SUM(ChiTieuNamSau) / @Dvt,
		   QuyetToan = SUM(QuyetToan) / @Dvt INTO #tblQuyetToanNamDonVi
	FROM
	  (-- DU TOAN NAM TRUOC
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
		 AND iNamNganSach = 1
		 AND iPhanCap = 1
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT * FROM f_split(@LNS)))
		 AND IDuLieuNhan = 0 
		 
	   -- DU TOAN NAM TRUOC DA CAP
	   UNION ALL 
	   SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			iID_MaDonVi,
			Loai = 1,
			ChiTieuNamNay = 0,
			ChiTieuNamSau = CASE
								WHEN @DataType = 1 THEN fTuChi
								ELSE fHienVat
							END,
			QuyetToan =0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = 5
		 AND iPhanCap = 1
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT * FROM f_split(@LNS)))
		 AND IDuLieuNhan = 0 
		 
	   -- DU TOAN NAM NAY
	   UNION ALL 
	   SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			iID_MaDonVi,
			Loai = 2,
			ChiTieuNamNay = CASE
								WHEN @DataType = 1 THEN fTuChi
								ELSE fHienVat
							END,
			ChiTieuNamSau = 0,
			QuyetToan = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = 2
		 AND iPhanCap=1
		 AND (@AgencyId IS NULL  OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT * FROM f_split(@LNS)))
		 AND IDuLieuNhan = 0
		 
	   -- DU TOAN CHUYEN NAM SAU
	   UNION ALL 
	   SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			iID_MaDonVi,
			Loai = 2,
			ChiTieuNamNay = 0,
			ChiTieuNamSau = CASE
								WHEN @DataType=1 THEN fTuChi
								ELSE fHienVat
							END,
			QuyetToan = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = 4
		 AND iPhanCap = 1
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT * FROM f_split(@LNS)))
		 AND IDuLieuNhan = 0
		 
	   --so da quyết toán nam truoc
	   UNION ALL 
	   SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			iID_MaDonVi,
			Loai = 1,
			ChiTieuNamNay = 0,
			ChiTieuNamSau = 0,
			QuyetToan = CASE
							WHEN @DataType = 1 THEN fTuChi_PheDuyet
							ELSE cast(0 AS float)
						END
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec=@YearOfWork
		 AND iNamNganSach in (1, 5)
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT *  FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT *  FROM f_split(@LNS)))
		 
	   -- QUYET TOAN NAM NAY
	   UNION ALL 
	   SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			iID_MaDonVi,
			Loai=2,
			ChiTieuNamNay = 0,
			ChiTieuNamSau = 0,
			QuyetToan = CASE
							WHEN @DataType = 1 THEN fTuChi_PheDuyet
							ELSE cast(0 AS float)
						END
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach in (2)
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT *  FROM f_split(@AgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT * FROM f_split(@LNS))) )AS ct  
	--mota lns
	LEFT JOIN
	  (SELECT sMoTa,
			  sXauNoiMa,
			  iID_MLNS,
			  iID_MLNS_Cha
	   FROM NS_MucLucNganSach
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork) AS mlns ON mlns.sXauNoiMa = ct.sXauNoiMa 
    --donvi
	LEFT JOIN
	  (SELECT iID_MaDonVi AS dv_id,
			  TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
	GROUP BY Loai, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct.sXauNoiMa,
			 iID_MaDonVi, TenDonVi,
			 mlns.iID_MLNS,
		     mlns.iID_MLNS_Cha,
			 mlns.sMoTa
	HAVING SUM(ChiTieuNamNay) <> 0
	OR SUM(ChiTieuNamSau) <> 0
	OR SUM(QuyetToan) <> 0;

	WITH LNSTreeParent AS
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
			cast(0 AS bit) AS IsHangCha
	   FROM #tblQuyetToanNamDonVi
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
			cast(1 AS bit) AS IsHangCha
	   FROM NS_MucLucNganSach mlnsParent
	   INNER JOIN LNSTreeParent ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
	   WHERE mlnsParent.iNamLamViec = @YearOfWork)
	SELECT DISTINCT *
	FROM LNSTreeParent
	ORDER BY sXauNoiMa;
	DROP TABLE #tblQuyetToanNamDonVi;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
	@YearOfWork int,
	@BudgetSource int,
	@AgencyId nvarchar(100),
	@QuarterMonth nvarchar(100),
	@LNS nvarchar(max),
	@Dvt int
AS
BEGIN
	SELECT *
	FROM
	  (SELECT iID_MaDonVi,
			  TuChi = SUM(fTuChi_PheDuyet) / @Dvt,
			  cast(0 AS float) AS HienVat,
			  SoNguoi = SUM(fSoNguoi),
			  SoNgay = SUM(fSoNgay),
			  SoLuot = SUM(fSoLuot)
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		 AND (@QuarterMonth IS NULL OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
		 AND (@LNS IS NULL  OR sLNS in (SELECT *  FROM f_split(@LNS)))
	   GROUP BY iID_MaDonVi)AS ct 
	-- lay ten don vi
	LEFT JOIN
	  (SELECT sTenDonVi,
			  iID_MaDonVi AS dv_id
	   FROM DonVi
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
	ORDER BY iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_lns]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_thongtri_lns]
	@VoucherId nvarchar(100),
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(100),
	@Dvt int
AS
BEGIN
	SELECT ct.iID_MLNS,
		sLNS,
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
		mlns.iID_MLNS_Cha,
		sMoTa,
		TuChi = SUM(TuChi) / @Dvt,
		HienVat = SUM(HienVat) / @Dvt,
		SoNguoi = SUM(SoNguoi),
		SoNgay = SUM(SoNgay),
		SoLuot = SUM(SoLuot) INTO #tblThongTriLns
	FROM
	  (SELECT iID_MLNS,
			sLNS,
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
			fTuChi_PheDuyet AS TuChi,
			cast(0 AS float) AS HienVat,
			fSoNguoi AS SoNguoi,
			fSoNgay AS SoNgay,
			fSoLuot AS SoLuot
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iID_QTChungTu = @VoucherId
		 AND iID_MaDonVi = @AgencyId
		 AND sLNS in (SELECT * FROM f_split(@LNS)) ) AS ct 
	--mota lns
	LEFT JOIN
	  (SELECT sMoTa,
			  iID_MLNS,
			  sXauNoiMa,
			  iID_MLNS_Cha
	   FROM NS_MucLucNganSach
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS mlns ON mlns.iID_MLNS = ct.iID_MLNS
	GROUP BY sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa, sMoTa, ct.iID_MLNS, mlns.iID_MLNS_Cha
	HAVING sum(TuChi) <> 0
	OR sum(HienVat) <> 0;

	WITH LNSTreeParent AS
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
			cast(TuChi AS float) AS TuChi,
			cast(HienVat AS float) AS HienVat,
			cast(SoNguoi AS float) AS SoNguoi,
			cast(SoNgay AS float) AS SoNgay,
			cast(SoLuot AS float) AS SoLuot,
			sMoTa,
			cast(0 AS bit) AS IsHangCha
	   FROM #tblThongTriLns
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
			cast(0 AS float) AS TuChi,
			cast(0 AS float) AS HienVat,
			cast(0 AS float) AS SoNguoi,
			cast(0 AS float) AS SoNgay,
			cast(0 AS float) AS SoLuot,
			mlnsParent.sMoTa,
			cast(1 AS bit) AS IsHangCha
	   FROM NS_MucLucNganSach mlnsParent
	   INNER JOIN LNSTreeParent ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
	   WHERE mlnsParent.iNamLamViec = @YearOfWork )
	SELECT DISTINCT *
	FROM LNSTreeParent
	ORDER BY sXauNoiMa;
	DROP TABLE #tblThongTriLns;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_thuongxuyen]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_thongtri_thuongxuyen]
	@VoucherId nvarchar(100),
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(100),
	@Dvt int
AS
BEGIN
	SELECT ct.iID_MLNS,
		sLNS,
		sL,
		sK,
		sM,
		sTM,
		sTTM,
		sNG,
		sXauNoiMa,
		mlns.iID_MLNS_Cha,
		sMoTa,
		TuChi = SUM(TuChi) / @Dvt,
		HienVat = SUM(HienVat) / @Dvt,
		SoNguoi = SUM(SoNguoi),
		SoNgay = SUM(SoNgay),
		SoLuot = SUM(SoLuot) INTO #tblThongTriLns
	FROM
	  (SELECT iID_MLNS,
			sLNS,
			sL,
			sK,
			sM,
			sTM,
			sTTM,
			sNG,
			fTuChi_PheDuyet AS TuChi,
			cast(0 AS float) AS HienVat,
			fSoNguoi AS SoNguoi,
			fSoNgay AS SoNgay,
			fSoLuot AS SoLuot
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iID_QTChungTu = @VoucherId
		 AND iID_MaDonVi = @AgencyId
		 AND sLNS in (SELECT * FROM f_split(@LNS)) ) AS ct 
	--mota lns
	LEFT JOIN
	  (SELECT sMoTa,
			  iID_MLNS,
			  sXauNoiMa,
			  iID_MLNS_Cha
	   FROM NS_MucLucNganSach
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS mlns ON mlns.iID_MLNS = ct.iID_MLNS
	GROUP BY sLNS, sL, sK, sM, sTM, sTTM, sNG, sXauNoiMa, sMoTa, ct.iID_MLNS, mlns.iID_MLNS_Cha
	HAVING sum(TuChi) <> 0
	OR sum(HienVat) <> 0;

	WITH LNSTreeParent AS
	  (SELECT sLNS,
			sL,
			sK,
			sM,
			sTM,
			sTTM,
			sNG,
			sXauNoiMa,
			iID_MLNS,
			iID_MLNS_Cha,
			cast(TuChi AS float) AS TuChi,
			cast(HienVat AS float) AS HienVat,
			cast(SoNguoi AS float) AS SoNguoi,
			cast(SoNgay AS float) AS SoNgay,
			cast(SoLuot AS float) AS SoLuot,
			sMoTa,
			cast(0 AS bit) AS IsHangCha
	   FROM #tblThongTriLns
	   UNION ALL 
	   SELECT mlnsParent.sLNS,
			mlnsParent.sL,
			mlnsParent.sK,
			mlnsParent.sM,
			mlnsParent.sTM,
			mlnsParent.sTTM,
			mlnsParent.sNG,
			mlnsParent.sXauNoiMa,
			mlnsParent.iID_MLNS,
			mlnsParent.iID_MLNS_Cha,
			cast(0 AS float) AS TuChi,
			cast(0 AS float) AS HienVat,
			cast(0 AS float) AS SoNguoi,
			cast(0 AS float) AS SoNgay,
			cast(0 AS float) AS SoLuot,
			mlnsParent.sMoTa,
			cast(1 AS bit) AS IsHangCha
	   FROM NS_MucLucNganSach mlnsParent
	   INNER JOIN LNSTreeParent ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
	   WHERE mlnsParent.iNamLamViec = @YearOfWork )
	SELECT DISTINCT *
	FROM LNSTreeParent
	ORDER BY sLNS,
			 sL,
			 sK,
			 sM,
			 sTM,
			 sTTM,
			 sNG;
	DROP TABLE #tblThongTriLns;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thuong_xuyen]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_thuong_xuyen]
	@VoucherId nvarchar(100),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(50),
	@VoucherDate datetime,
	@Dvt int
AS
BEGIN
	SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct.sXauNoiMa,
		   mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sMoTa,
		   mlns.sChiTietToi,
		   ChiTieu = SUM(ChiTieu) / @Dvt,
		   TuChi2 = SUM(TuChi2) / @Dvt,
		   TuChi = SUM(TuChi) / @Dvt,
		   HienVat = SUM(HienVat) / @Dvt,
		   SoNguoi = SUM(SoNguoi),
		   SoNgay = SUM(SoNgay),
		   SoLuot = SUM(SoLuot) INTO #tblData
	FROM
	  (--chi tieu theo don vi
	  SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			ChiTieu = fTuChi,
			TuChi2 = 0,
			TuChi = 0,
			HienVat = 0,
			SoNguoi = 0,
			SoNgay = 0,
			SoLuot = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iPhanCap = 1
		 AND iID_MaDonVi = @AgencyId
		 AND (@lns IS NULL OR sLNS in (SELECT * FROM f_split(@lns)))
		 AND IDuLieuNhan = 0 
	   --so da quyết toán
	   UNION ALL 
	   SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			ChiTieu = 0,
			TuChi2 = fTuChi_PheDuyet,
			TuChi = 0,
			HienVat = 0,
			SoNguoi = 0,
			SoNgay = 0,
			SoLuot = 0
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iID_QTChungTu in
		   (SELECT iID_QTChungTu
			FROM NS_QT_ChungTu
			WHERE iNamLamViec = @YearOfWork
			  AND iNamNganSach = @YearOfBudget
			  AND iID_MaNguonNganSach = @BudgetSource
			  AND dNgayChungTu <= @VoucherDate)
		 AND iID_QTChungTu != @VoucherId
		 AND iID_MaDonVi = @AgencyId
		 AND (@lns IS NULL OR sLNS in (SELECT * FROM splitstring(@lns)))
		 
	   -- quyet toan dot nay
	   UNION ALL 
	   SELECT sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sXauNoiMa,
			ChiTieu = 0,
			TuChi2 = 0,
			fTuChi_PheDuyet AS TuChi,
			HienVat = 0,
			fSoNguoi AS SoNguoi,
			fSoNgay AS SoNgay,
			fSoLuot AS SoLuot
	   FROM NS_QT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iID_QTChungTu = @VoucherId
		 AND iID_MaDonVi = @AgencyId
		 AND (@lns IS NULL OR sLNS in (SELECT * FROM f_split(@lns))))AS ct
	LEFT JOIN
	  (SELECT sMoTa,
			  sXauNoiMa,
			  iID_MLNS,
			  iID_MLNS_Cha,
			  sChiTietToi
	   FROM NS_MucLucNganSach
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS mlns ON mlns.sXauNoiMa = ct.sXauNoiMa
	GROUP BY sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, ct.sXauNoiMa,
			 mlns.iID_MLNS,
			 mlns.iID_MLNS_Cha,
			 mlns.sMoTa,
			 mlns.sChiTietToi
	HAVING SUM(ChiTieu) <> 0
		OR SUM(TuChi2) <> 0
		OR SUM(TuChi) <> 0
		OR SUM(HienVat) <> 0;

	WITH LNSTreeParent AS
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
			cast(ChiTieu AS float) AS ChiTieu,
			cast(TuChi2 AS float) AS TuChi2,
			cast(TuChi AS float) AS TuChi,
			cast(HienVat AS float) AS HienVat,
			cast(SoNguoi AS float) AS SoNguoi,
			cast(SoNgay AS float) AS SoNgay,
			cast(SoLuot AS float) AS SoLuot,
			ThucChi = TuChi2 + TuChi,
			sMoTa,
			sChiTietToi,
			cast(0 AS bit) AS IsHangCha
	   FROM #tblData
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
			cast(0 AS float) AS ChiTieu,
			cast(0 AS float) AS TuChi2,
			cast(0 AS float) AS TuChi,
			cast(0 AS float) AS HienVat,
			cast(0 AS float) AS SoNguoi,
			cast(0 AS float) AS SoNgay,
			cast(0 AS float) AS SoLuot,
			cast(0 AS float) AS ThucChi,
			mlnsParent.sMoTa,
			mlnsParent.sChiTietToi,
			cast(1 AS bit) AS IsHangCha
	   FROM NS_MucLucNganSach mlnsParent
	   INNER JOIN LNSTreeParent ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
	   WHERE mlnsParent.INamLamViec = @YearOfWork
		 AND mlnsParent.sL <>  ''
		 AND mlnsParent.sK <>  ''
		 AND mlnsParent.sM <>  '')
	SELECT DISTINCT *
	FROM LNSTreeParent
	ORDER BY sLNS,
			 sL,
			 sK,
			 sM,
			 sTM,
			 sTTM,
			 sNG,
			 sTNG1,
			 sTNG2,
			 sTNG3;
	DROP TABLE #tblData;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dt_chungtu_chitiet]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_dt_chungtu_chitiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type int
AS
BEGIN
	SELECT
		isnull(ctct.Id, NEWID()) AS Id,
		ctct.Id_ChungTu,
		mlns.iID_MLNS as MLNS_Id,
		mlns.iID_MLNS_Cha as MLNS_Id_Parent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		mlns.sTNG1 as TNG1,
		mlns.sTNG2 as TNG2,
		mlns.sTNG3 as TNG3,
		isnull(mlns.sMoTa, '') as NoiDung,
		'' as Chuong,
		isnull(ctct.NamNganSach, @YearOfBudget) as NamNganSach,
		ctct.NguonNganSach,
		ctct.NamLamViec,
		mlns.bHangCha,
		isnull(ctct.iTrangThai, 0) as iTrangThai,
		isnull(ctct.iPhanCap, @Type) as iPhanCap,
		ctct.Id_DonVi,
		ctct.TenDonVi,
		ctct.Id_PhongBan,
		ctct.Id_PhongBanDich,
		isnull(ctct.GhiChu, '') as GhiChu,
		isnull(ctct.TuChi, 0) as TuChi,
		ctct.DateCreated,
		ctct.DateModified,
		isnull(ctct.UserCreator, '') as UserCreator ,
		isnull(ctct.UserModifier, '') as UserModifier,
		isnull(ctct.Log, '') as Log,
		isnull(ctct.Tag, '') as Tag,
		ctct.Id_DotNhan,
		isnull(ctct.B, '') as B
	FROM (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
	--FROM NS_MucLucNganSach mlns
	LEFT JOIN (
		SELECT
			*
		FROM
			TN_DT_ChungTuChiTiet
		WHERE
			NamLamViec = @YearOfWork
			AND NamNganSach = @YearOfBudget
			AND NguonNganSach = @BudgetSource
			AND iPhanCap = @Type
			AND Id_ChungTu = @ChungTuId
			--AND LNS in (select * from dbo.splitstring(@LNS))
	) ctct ON mlns.iID_MLNS = ctct.MLNS_Id
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
		--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
	ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dt_phanbo_dutoan_chitiet]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_dt_phanbo_dutoan_chitiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int
AS
BEGIN
	declare @VoucherDate datetime,
	@Agencies nvarchar(100)
	SELECT
		@VoucherDate = NgayChungTu,
		@Agencies = Id_DonVi
	FROM TN_DT_ChungTu
	WHERE id = @ChungTuId;

	select 
	tbl_sum.Id,
	tbl_sum.IdChungTu,
	tbl_sum.LNS,
	tbl_sum.XauNoiMa,
	tbl_sum.MLNS_Id,
	tbl_sum.MLNS_Id_Parent,
	tbl_sum.NguonNganSach,
	tbl_sum.NamLamViec,
	tbl_sum.NamNganSach,
	tbl_sum.iTrangThai,
	tbl_sum.iPhanCap,
	tbl_sum.IdDonVi,
	tbl_sum.TenDonVi,
	tbl_sum.IdPhongBan,
	tbl_sum.IdPhongBanDich,
	tbl_sum.GhiChu,
	tbl_sum.DateCreated,
	tbl_sum.DateModified,
	tbl_sum.UserCreator,
	tbl_sum.UserModifier,
	tbl_sum.Log,
	tbl_sum.Tag,
	tbl_sum.IdDotNhan,
	tbl_sum.B,
	SUM(tbl_sum.TuChi) as TuChi,
	tbl_sum.Loai
	
	into #tmpSum
from
	(
	select 
		null as Id,
		null as IdChungTu,
		mlns.sLNS as LNS,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.iID_MLNS as MLNS_Id,
		mlns.iID_MLNS_Cha as MLNS_Id_Parent,
		@BudgetSource as NguonNganSach,
		@YearOfWork as NamLamViec,
		@YearOfBudget as NamNganSach,
		1 as iTrangThai,
		0 as iPhanCap,
		'' as IdDonVi,
		'' as TenDonVi,
		'' as IdPhongBan,
		'' as IdPhongBanDich,
		'' as GhiChu,
		null as DateCreated,
		null as DateModified,
		'' as UserCreator,
		'' as UserModifier,
		'' as Log,
		'' as Tag,
		null as IdDotNhan,
		'' as B,
		ctct.TuChi as TuChi,
		case
			when mlns.bHangCha = 0 then cast(1 as bit) else 0
		end Loai
	from NS_MucLucNganSach mlns
	left join
		(
			select
				*
			from 
				TN_DT_ChungTuChiTiet ctct1
			where 
				ctct1.Id_ChungTu in (
					select
						Id
					from 
						TN_DT_ChungTu
					where
						iLoai = 0
						and NamLamViec = @YearOfWork
						and iTrangThai = 1
						and NamNganSach = @YearOfBudget
						and NguonNganSach = @BudgetSource
						--and FORMAT(cast(NgayChungTu as Date), 'yyyy-MM-dd') <= FORMAT(cast(@VoucherDate as Date), 'yyyy-MM-dd')
						and NgayChungTu <= @VoucherDate
				)
		) ctct
	on mlns.iID_MLNS = ctct.MLNS_Id
	where mlns.iNamLamViec = @YearOfWork and mlns.iTrangThai = 1 and mlns.sLNS in (select distinct * from f_split_lns(@LNS))) as tbl_sum
	where tbl_sum.Loai = 1
	group by 
		tbl_sum.Id,tbl_sum.IdChungTu,tbl_sum.LNS,
		tbl_sum.XauNoiMa,tbl_sum.MLNS_Id,tbl_sum.MLNS_Id_Parent,tbl_sum.NguonNganSach,
		tbl_sum.NamLamViec,tbl_sum.NamNganSach,tbl_sum.iTrangThai,tbl_sum.iPhanCap,
		tbl_sum.IdDonVi,tbl_sum.TenDonVi,tbl_sum.IdPhongBan,tbl_sum.IdPhongBanDich,
		tbl_sum.GhiChu,tbl_sum.DateCreated,tbl_sum.DateModified,
		tbl_sum.UserCreator,tbl_sum.UserModifier,tbl_sum.Log,
		tbl_sum.Tag,tbl_sum.IdDotNhan,tbl_sum.B, tbl_sum.Loai
		
	select 
	null as Id,
	null as IdChungTu,
	tbl_subSum.LNS,
	tbl_subSum.XauNoiMa,
	tbl_subSum.MLNS_Id,
	tbl_subSum.MLNS_Id_Parent,
	tbl_subSum.NguonNganSach,
	tbl_subSum.NamLamViec,
	tbl_subSum.NamNganSach,
	0 as iTrangThai,
	0 as iPhanCap,
	'' as IdDonVi,
	'' as TenDonVi,
	'' as IdPhongBan,
	'' as IdPhongBanDich,
	'' as GhiChu,
	null as DateCreated,
	null as DateModified,
	'' as UserCreator,
	'' as UserModifier,
	'' as Log,
	'' as Tag,
	null as IdDotNhan,
	'' as B,
	SUM(tbl_subSum.TuChi) as TuChi,
	1 as Loai

	into #tmpSubSum 
from
(select
	isnull(ctct.Id, NEWID()) as Id,
	ctct.Id_ChungTu as IdChungTu,
	ctct.LNS,
	ctct.XauNoiMa,
	ctct.MLNS_Id,
	ctct.MLNS_Id_Parent,
	ctct.NguonNganSach,
	ctct.NamLamViec,
	ctct.NamNganSach,
	isnull(ctct.iTrangThai, 0) as iTrangThai,
	isnull(ctct.iPhanCap, 0) as iPhanCap,
	ctct.Id_DonVi as IdDonVi,
	ctct.TenDonVi,
	ctct.Id_PhongBan as IdPhongBan,
	ctct.Id_PhongBanDich as IdPhongBanDich,
	ctct.GhiChu as GhiChu,
	ctct.DateCreated,
	ctct.DateModified,
	isnull(ctct.UserCreator, '') as UserCreator ,
	isnull(ctct.UserModifier, '') as UserModifier,
	isnull(ctct.Log, '') as Log,
	isnull(ctct.Tag, '') as Tag,
	null as IdDotNhan,
	isnull(ctct.B, '') as B,
	ctct.TuChi,
	1 as Loai
from
	TN_DT_ChungTuChiTiet ctct
inner join
	TN_DT_ChungTu ct
on ctct.Id_ChungTu = ct.Id
where
	ct.iLoai = 1
	and ct.NamLamViec = @YearOfWork
	and ct.NamNganSach = @YearOfBudget
	and ct.NguonNganSach = @BudgetSource
	and ct.NgayChungTu < @VoucherDate
	) as tbl_subSum
	
group by 
	tbl_subSum.LNS,
	tbl_subSum.XauNoiMa,
	tbl_subSum.MLNS_Id,
	tbl_subSum.MLNS_Id_Parent,
	tbl_subSum.NguonNganSach,
	tbl_subSum.NamLamViec,
	tbl_subSum.NamNganSach
	
select tbl_total.* into #tmpTotal from
(select
	total.Id,
	total.IdChungTu,
	total.LNS,
	total.XauNoiMa,
	total.MLNS_Id,
	total.MLNS_Id_Parent,
	total.NguonNganSach,
	total.NamLamViec,
	total.NamNganSach,
	total.iTrangThai,
	total.iPhanCap,
	total.IdDonVi,
	total.TenDonVi,
	total.IdPhongBan,
	total.IdPhongBanDich,
	total.GhiChu,
	total.DateCreated,
	total.DateModified,
	total.UserCreator,
	total.UserModifier,
	total.Log,
	total.Tag,
	total.IdDotNhan,
	total.B,
	(isnull(total.TuChi, 0) - isnull(tmp_sub_sum.TuChi, 0)) as TuChi,
	total.Loai
from
	#tmpSum total
left join
	#tmpSubSum tmp_sub_sum
on total.XauNoiMa = tmp_sub_sum.XauNoiMa) as tbl_total
		
		
select tbl_data.* into #tmpData from
(select 
		tmp.LNS,
		tmp.MLNS_Id,
		tmp.MLNS_Id_Parent,
		tmp.TuChi,
		tmp.Loai,
		NEWID() as Id,
		NEWID() as IdChungTu,
		tmp.XauNoiMa,
		tmp.NguonNganSach,
		tmp.NamLamViec,
		tmp.NamNganSach,
		tmp.iTrangThai,
		tmp.iPhanCap,
		tmp.IdDonVi,
		tmp.TenDonVi,
		tmp.IdPhongBan,
		tmp.IdPhongBanDich,
		tmp.GhiChu,
		tmp.DateCreated,
		tmp.DateModified,
		tmp.UserCreator,
		tmp.UserModifier,
		tmp.Log,
		tmp.Tag,
		NEWID() as IdDotNhan,
		tmp.B
	from 
		#tmpTotal tmp

	union all

	select tbl_sub_data.* from
	(select 
		mlns.sLNS as LNS,
		mlns.iID_MLNS as MLNS_Id,
		mlns.iID_MLNS_Cha as MLNS_Id_Parent,
		isnull(ctct.TuChi, 0) as TuChi,
		case 
			when mlns.bHangCha = 1 then 0 else 2
		end Loai,
		isnull(ctct.Id, NEWID()) as Id,
		ctct.Id_ChungTu as IdChungTu,
		mlns.sXauNoiMa as XauNoiMa,
		ctct.NguonNganSach,
		ctct.NamLamViec,
		ctct.NamNganSach,
		ctct.iTrangThai,
		ctct.iPhanCap,
		ctct.Id_DonVi as IdDonVi,
		ctct.TenDonVi,
		ctct.Id_PhongBan as IdPhongBan,
		ctct.Id_PhongBanDich as IdPhongBanDich,
		ctct.GhiChu as GhiChu,
		ctct.DateCreated as DateCreated,
		ctct.DateModified as DateModified,
		ctct.UserCreator as UserCreator,
		ctct.UserModifier as UserModifier,
		ctct.Log,
		ctct.Tag,
		NEWID() as IdDotNhan,
		ctct.B
	from NS_MucLucNganSach mlns
	left join
		(
			select
				*
			from 
				TN_DT_ChungTuChiTiet ctct1
			where 
				ctct1.iPhanCap = 1
				and ctct1.Id_ChungTu in (select * from dbo.splitstring(@ChungTuId))
		) ctct
	on mlns.iID_MLNS = ctct.MLNS_Id
	where mlns.iNamLamViec = @YearOfWork and mlns.iTrangThai = 1 and mlns.sLNS in (select distinct * from f_split_lns(@LNS))) as tbl_sub_data
	where tbl_sub_data.Loai = 2

	) as tbl_data
	order by tbl_data.LNS, tbl_data.Loai


	--select 
	--	*
	--into #mlns from NS_MucLucNganSach where 
	--NamLamViec = @YearOfWork and iTrangThai = 1 and bHangCha = 0 and LNS in (select * from dbo.splitstring(@LNS)) order by XauNoiMa;
	--select * into #dv from NS_DonVi where NamLamViec = @YearOfWork and Id_DonVi in (
	--		select * FROM dbo.f_split((select Id_DonVi FROM TN_DT_ChungTu where Id = @ChungTuId))
	--);

	--select * into #tbl_global from (
	-- select  #mlns.*, #dv.Id_DonVi, #dv.TenDonVi from #mlns, #dv
	-- union all
	-- select 
	--	*
	-- ,'' as Id_DonVi,'' as TenDonVi FROM NS_MucLucNganSach mlns where mlns.bHangCha =1 and mlns.NamLamViec = @YearOfWork and LNS in (select * from dbo.splitstring(@LNS))
	--) tbl

	SELECT
	    mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdParent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		mlns.sTNG1 as TNG1,
		mlns.sTNG2 as TNG2,
		mlns.sTNG3 as TNG3,
		isnull(mlns.sMoTa, '') as NoiDung,
		'' as Chuong,
		CASE 
			when tmp1.Loai = 1 then cast(1 as bit) else mlns.bHangCha
		END bHangCha,
		isnull(tmp1.Id, NEWID()) as Id,
		tmp1.IdChungTu,
		tmp1.NguonNganSach,
		tmp1.NamLamViec,
		tmp1.NamNganSach,
		tmp1.iTrangThai,
		tmp1.iPhanCap,
		tmp1.IdDonVi,
		tmp1.TenDonVi,
		tmp1.IdPhongBan,
		tmp1.IdPhongBanDich,
		tmp1.GhiChu,
		tmp1.DateCreated,
		tmp1.DateModified,
		tmp1.UserCreator,
		tmp1.UserModifier,
		tmp1.Log,
		tmp1.Tag,
		tmp1.IdDotNhan,
		tmp1.B,
		isnull(tmp1.TuChi, 0) as TuChi,
		tmp1.Loai
		into #data
	FROM (select * from NS_MucLucNganSach where iNamLamViec = @YearOfWork and iTrangThai = 1 and sLNS in (select distinct * from f_split_lns(@LNS))) mlns
	left join #tmpData tmp1
	on mlns.iID_MLNS = tmp1.MLNS_Id
	Order by mlns.sXauNoiMa, mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG, mlns.sTNG1, mlns.sTNG2, mlns.sTNG3
	
	select * into #dv from DonVi where iNamLamViec = @YearOfWork and iID_MaDonVi in (select * from f_split(@Agencies))
	insert into #data
	select 
		data1.MlnsId,
		data1.MlnsIdParent,
		data1.XauNoiMa,
		data1.LNS,
		data1.L,
		data1.K,
		data1.M,
		data1.TM,
		data1.TTM,
		data1.NG,
		data1.TNG,
		data1.TNG1,
		data1.TNG2,
		data1.TNG3,
		data1.NoiDung,
		data1.Chuong,
		data1.bHangCha,
		data1.Id,
		data1.IdChungTu,
		data1.NguonNganSach,
		data1.NamLamViec,
		data1.NamNganSach,
		data1.iTrangThai,
		data1.iPhanCap,
		#dv.iID_MaDonVi as IdDonVi,
		#dv.sTenDonVi as TenDonVi,
		data1.IdPhongBan,
		data1.IdPhongBanDich,
		data1.GhiChu,
		data1.DateCreated,
		data1.DateModified,
		data1.UserCreator,
		data1.UserModifier,
		data1.Log,
		data1.Tag,
		data1.IdDotNhan,
		data1.B,
	case 
    when IdDonVi = iID_MaDonVi then data1.TuChi
    when IdDonVi is null then 0
    end as TuChi,
    data1.Loai
  from 
  (select * from #data where bHangCha = 0) as data1, #dv
  where IdDonVi = iID_MaDonVi or IdDonVi is null
  order by XauNoiMa, iID_MaDonVi

	delete from #data where bHangCha = 0 and IdDonVi is null;

	select distinct * from #data
	order by XauNoiMa, LNS, Loai


	drop table #tmpTotal
	drop table #tmpData
	drop table #tmpSubSum
	drop table #tmpSum
	drop table #data
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dt_report_du_toan_ngan_sach]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_dt_report_du_toan_ngan_sach]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type int,
	@IdDonVi nvarchar(50),
	@reportType int
AS
BEGIN
	IF (@reportType = 0)
	BEGIN
		SELECT
		isnull(ctct.Id, NEWID()) AS Id,
		ctct.Id_ChungTu,
		mlns.iID_MLNS as MLNS_Id,
		mlns.iID_MLNS_Cha as MLNS_Id_Parent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		isnull(mlns.sMoTa, '') as NoiDung,
		'' as Chuong,
		isnull(ctct.NamNganSach, @YearOfBudget) as NamNganSach,
		ctct.NguonNganSach,
		ctct.NamLamViec,
		mlns.bHangCha,
		isnull(ctct.iTrangThai, 0) as iTrangThai,
		isnull(ctct.iPhanCap, @Type) as iPhanCap,
		ctct.Id_DonVi,
		ctct.TenDonVi,
		ctct.Id_PhongBan,
		ctct.Id_PhongBanDich,
		isnull(ctct.GhiChu, '') as GhiChu,
		isnull(ctctsum.TuChiSum, 0) as TuChi,
		ctct.DateCreated,
		ctct.DateModified,
		isnull(ctct.UserCreator, '') as UserCreator ,
		isnull(ctct.UserModifier, '') as UserModifier,
		isnull(ctct.Log, '') as Log,
		isnull(ctct.Tag, '') as Tag,
		ctct.Id_DotNhan,
		isnull(ctct.B, '') as B
		FROM (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
		LEFT JOIN (
			SELECT
				*
			FROM
				TN_DT_ChungTuChiTiet
			WHERE
				NamLamViec = @YearOfWork
				AND NamNganSach = @YearOfBudget
				AND NguonNganSach = @BudgetSource
				AND iPhanCap = @Type
		) ctct ON mlns.iID_MLNS = ctct.MLNS_Id
		LEFT JOIN(
			SELECT
				MLNS_Id,
				SUM(TuChi) as TuChiSum
			FROM
				TN_DT_ChungTuChiTiet
			WHERE
				NamLamViec = @YearOfWork
				AND NamNganSach = @YearOfBudget
				AND NguonNganSach = @BudgetSource
				AND iPhanCap = @Type
			GROUP BY MLNS_Id
		)ctctsum ON mlns.iID_MLNS = ctctsum.MLNS_Id
		WHERE
			mlns.iNamLamViec = @YearOfWork
			AND mlns.iTrangThai = 1
		ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
	END
	ELSE
	BEGIN
		SELECT
		isnull(ctct.Id, NEWID()) AS Id,
		ctct.Id_ChungTu,
		mlns.iID_MLNS as MLNS_Id,
		mlns.iID_MLNS_Cha as MLNS_Id_Parent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		isnull(mlns.sMoTa, '') as NoiDung,
		'' as Chuong,
		isnull(ctct.NamNganSach, @YearOfBudget) as NamNganSach,
		ctct.NguonNganSach,
		ctct.NamLamViec,
		mlns.bHangCha,
		isnull(ctct.iTrangThai, 0) as iTrangThai,
		isnull(ctct.iPhanCap, @Type) as iPhanCap,
		ctct.Id_DonVi,
		ctct.TenDonVi,
		ctct.Id_PhongBan,
		ctct.Id_PhongBanDich,
		isnull(ctct.GhiChu, '') as GhiChu,
		isnull(ctctsum.TuChiSum, 0) as TuChi,
		ctct.DateCreated,
		ctct.DateModified,
		isnull(ctct.UserCreator, '') as UserCreator ,
		isnull(ctct.UserModifier, '') as UserModifier,
		isnull(ctct.Log, '') as Log,
		isnull(ctct.Tag, '') as Tag,
		ctct.Id_DotNhan,
		isnull(ctct.B, '') as B
		FROM (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
		LEFT JOIN (
			SELECT
				*
			FROM
				TN_DT_ChungTuChiTiet
			WHERE
				NamLamViec = @YearOfWork
				AND NamNganSach = @YearOfBudget
				AND NguonNganSach = @BudgetSource
				AND iPhanCap = @Type
		) ctct ON mlns.iID_MLNS = ctct.MLNS_Id
		LEFT JOIN(
			SELECT
				MLNS_Id,
				SUM(TuChi) as TuChiSum
			FROM
				TN_DT_ChungTuChiTiet
			WHERE
				NamLamViec = @YearOfWork
				AND NamNganSach = @YearOfBudget
				AND NguonNganSach = @BudgetSource
				AND iPhanCap = @Type
			GROUP BY MLNS_Id
		)ctctsum ON mlns.iID_MLNS = ctctsum.MLNS_Id
		WHERE
			mlns.iNamLamViec = @YearOfWork
			AND mlns.iTrangThai = 1
			AND ctct.Id_DonVi = @IdDonVi
		ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
	END
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dt_rpt_du_toan_ngan_sach]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_dt_rpt_du_toan_ngan_sach]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type int,
	@IdDonVi nvarchar(50),
	@reportType int
AS
BEGIN
	IF (@reportType = 0)
	BEGIN
		SELECT
		isnull(ctct.Id, NEWID()) AS Id,
		ctct.Id_ChungTu as IdChungTu,
		mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdParent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		isnull(mlns.sMoTa, '') as NoiDung,
		'' as Chuong,
		isnull(ctct.NamNganSach, @YearOfBudget) as NamNganSach,
		ctct.NguonNganSach,
		ctct.NamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.iTrangThai, 0) as iTrangThai,
		isnull(ctct.iPhanCap, @Type) as iPhanCap,
		ctct.Id_DonVi as IdDonVi,
		ctct.TenDonVi,
		ctct.Id_PhongBan as IdPhongBan,
		ctct.Id_PhongBanDich as IdPhongBanDich,
		isnull(ctct.GhiChu, '') as GhiChu,
		isnull(ctctsum.TuChiSum, 0) as TuChi,
		ctct.DateCreated,
		ctct.DateModified,
		isnull(ctct.UserCreator, '') as UserCreator ,
		isnull(ctct.UserModifier, '') as UserModifier,
		isnull(ctct.Log, '') as Log,
		isnull(ctct.Tag, '') as Tag,
		ctct.Id_DotNhan as IdDotNhan,
		isnull(ctct.B, '') as B,
		CASE
			WHEN ctctdv.Loai_don_vi = 0 THEN ctct.TuChi
		END KDuToan,
		CASE
			WHEN ctctdv.Loai_don_vi = 1 THEN ctct.TuChi
		END KDoanhNghiep
		FROM (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
		LEFT JOIN (
			SELECT
				*
			FROM
				TN_DT_ChungTuChiTiet
			WHERE
				NamLamViec = @YearOfWork
				AND NamNganSach = @YearOfBudget
				AND NguonNganSach = @BudgetSource
				AND iPhanCap = @Type
		) ctct ON mlns.iID_MLNS = ctct.MLNS_Id
		LEFT JOIN(
			SELECT
				MLNS_Id,
				SUM(TuChi) as TuChiSum
			FROM
				TN_DT_ChungTuChiTiet
			WHERE
				NamLamViec = @YearOfWork
				AND NamNganSach = @YearOfBudget
				AND NguonNganSach = @BudgetSource
				AND iPhanCap = @Type
			GROUP BY MLNS_Id
		)ctctsum ON mlns.iID_MLNS = ctctsum.MLNS_Id
		LEFT JOIN(
			SELECT
				iID_MaDonVi as Id_DonVi,
				iLoai as Loai_don_vi
			FROM 
				DonVi
			WHERE 
				iNamLamViec = @YearOfWork	
		)ctctdv ON ctct.Id_DonVi = ctctdv.Id_DonVi
		WHERE
			mlns.iNamLamViec = @YearOfWork
			AND mlns.iTrangThai = 1
		ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
	END
	ELSE
	BEGIN
		SELECT
		isnull(ctct.Id, NEWID()) AS Id,
		ctct.Id_ChungTu as IdChungTu,
		mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdParent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		isnull(mlns.sMoTa, '') as NoiDung,
		'' as Chuong,
		isnull(ctct.NamNganSach, @YearOfBudget) as NamNganSach,
		ctct.NguonNganSach,
		ctct.NamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.iTrangThai, 0) as iTrangThai,
		isnull(ctct.iPhanCap, @Type) as iPhanCap,
		ctct.Id_DonVi as IdDonVi,
		ctct.TenDonVi,
		ctct.Id_PhongBan as IdPhongBan,
		ctct.Id_PhongBanDich as IdPhongBanDich,
		isnull(ctct.GhiChu, '') as GhiChu,
		isnull(ctct.TuChi, 0) as TuChi,
		ctct.DateCreated,
		ctct.DateModified,
		isnull(ctct.UserCreator, '') as UserCreator ,
		isnull(ctct.UserModifier, '') as UserModifier,
		isnull(ctct.Log, '') as Log,
		isnull(ctct.Tag, '') as Tag,
		ctct.Id_DotNhan as IdDotNhan,
		isnull(ctct.B, '') as B,
		CASE
			WHEN ctctdv.Loai_don_vi = 0 THEN ctct.TuChi
		END KDuToan,
		CASE
			WHEN ctctdv.Loai_don_vi = 1 THEN ctct.TuChi
		END KDoanhNghiep
		FROM (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
		LEFT JOIN (
			SELECT
				*
			FROM
				TN_DT_ChungTuChiTiet
			WHERE
				NamLamViec = @YearOfWork
				AND NamNganSach = @YearOfBudget
				AND NguonNganSach = @BudgetSource
				AND iPhanCap = @Type
		) ctct ON mlns.iID_MLNS = ctct.MLNS_Id
		LEFT JOIN(
			SELECT
				iID_MaDonVi as Id_DonVi,
				iLoai as Loai_don_vi
			FROM 
				DonVi
			WHERE 
				iNamLamViec = @YearOfWork	
		)ctctdv ON ctct.Id_DonVi = ctctdv.Id_DonVi
		WHERE
			mlns.iNamLamViec = @YearOfWork
			AND mlns.iTrangThai = 1
			AND (mlns.bHangCha = 1 or ctct.Id_DonVi in (SELECT * FROM dbo.splitstring(@IdDonVi)))
		ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
	END
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_qt_chungtu_chitiet]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_qt_chungtu_chitiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int
AS
BEGIN
	SELECT
		isnull(ctct.Id, NEWID()) AS Id,
		ctct.Id_ChungTu,
		mlns.ID_MaLoaiHinh,
		mlns.ID_MaLoaiHinh_Cha,
		mlns.LNS,
		isnull(mlns.MoTa, '') as Noidung,
		isnull(ctct.iThangQuyLoai, 0) as iThangQuyLoai,
		isnull(ctct.iThangQuy, 1) as iThangQuy,
		isnull(ctct.TongSoThu, 0) as TongSoThu,
		mlns.bLaHangCha,
		isnull(ctct.NamNganSach, @YearOfBudget) as NamNganSach,
		isnull(ctct.NguonNganSach, @BudgetSource) as NguonNganSach,
		mlns.iNamLamViec as NamLamViec,
		mlns.iTrangThai,
		isnull(ctct.iLoai, 0) as iLoai,
		isnull(ctct.TongSoChiPhi, 0) as TongSoChiPhi,
		isnull(ctct.QT_TongSoQTNS, 0) as QT_TongSoQTNS,
		isnull(ctct.Id_DonVi, '') as Id_DonVi,
		isnull(ctct.TenDonVi, '') as TenDonVi,
		isnull(ctct.Id_PhongBan, '') as Id_PhongBan,
		isnull(ctct.Id_PhongBanDich, '') as Id_PhongBanDich,
		isnull(ctct.QT_KhauHaoTSCĐ, 0) as QT_KhauHaoTSCĐ,
		isnull(ctct.QT_TienLuong, 0) as QT_TienLuong,
		isnull(ctct.QT_QTNSKhac, 0) as QT_QTNSKhac,
		isnull(ctct.ChiPhiKhac, 0) as ChiPhiKhac,
		isnull(ctct.TongnopNSNN, 0) as TongnopNSNN,
		isnull(ctct.ThueGTGT, 0) as ThueGTGT,
		isnull(ctct.GhiChu, '') as GhiChu,
		ctct.DateCreated,
		isnull(ctct.UserCreator, '') as UserCreator,
		ctct.DateModified,
		isnull(ctct.UserModifier, '') as UserModifier,
		isnull(ctct.Tag, '') as Tag,
		isnull(ctct.Log, '') as Log,
		isnull(ctct.Id_DonViTao, '') as Id_DonViTao,
		isnull(ctct.iGuiNhan, 0) as iGuiNhan,
		isnull(ctct.ThueTNDN, 0) as ThueTNDN,
		isnull(ctct.ThueTNDN_BQP, 0) as ThueTNDN_BQP,
		isnull(ctct.Phi_LePhi, 0) as Phi_LePhi,
		isnull(ctct.NSNN_Khac, 0) as NSNN_Khac,
		isnull(ctct.NSNN_Khac_BQP, 0) as NSNN_Khac_BQP,
		isnull(ctct.ChenhLech, 0) as ChenhLech,
		isnull(ctct.PP_NopNSQP, 0) as PP_NopNSQP,
		isnull(ctct.PP_BoSungKinhPhi, 0) as PP_BoSungKinhPhi,
		isnull(ctct.PP_TrichCacQuy, 0) as PP_TrichCacQuy,
		isnull(ctct.PP_SoChuaPhanPhoi, 0) as PP_SoChuaPhanPhoi,
		isnull(ctct.bThoaiThu, 0) as bThoaiThu
	FROM  (SELECT * FROM f_loai_hinh_by_lns(@YearOfWork, @LNS)) mlns
	LEFT JOIN (
		SELECT
			*
		FROM
			TN_QT_ChungTuChiTiet
		WHERE
			NamLamViec = @YearOfWork
			AND NamNganSach = @YearOfBudget
			AND NguonNganSach = @BudgetSource
			AND Id_ChungTu = @ChungTuId
	) ctct ON mlns.ID_MaLoaiHinh = ctct.ID_MaLoaiHinh
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
		--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
	ORDER BY mlns.LNS
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_qt_chungtu_chitiet1]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_qt_chungtu_chitiet1]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int
AS
BEGIN
	SELECT
		isnull(ctct.Id, NEWID()) AS Id,
		ctct.Id_ChungTu as IdChungTu,
		mlns.ID_MaLoaiHinh as IdMaLoaiHinh, 
		mlns.ID_MaLoaiHinh_Cha as IdMaLoaiHinhCha,
		mlns.LNS as Lns,
		isnull(mlns.MoTa, '') as Noidung,
		isnull(ctct.iThangQuyLoai, 0) as IThangQuyLoai,
		isnull(ctct.iThangQuy, 1) as IThangQuy,
		isnull(ctct.TongSoThu, 0) as TongSoThu,
		mlns.bLaHangCha as BLaHangCha,
		isnull(ctct.NamNganSach, @YearOfBudget) as NamNganSach,
		isnull(ctct.NguonNganSach, @BudgetSource) as NguonNganSach,
		mlns.iNamLamViec as NamLamViec,
		mlns.iTrangThai as ITrangThai,
		isnull(ctct.iLoai, 0) as ILoai,
		isnull(ctct.TongSoChiPhi, 0) as TongSoChiPhi,
		isnull(ctct.QT_TongSoQTNS, 0) as QtTongSoQtns,
		isnull(ctct.Id_DonVi, '') as IdDonVi,
		isnull(ctct.TenDonVi, '') as TenDonVi,
		isnull(ctct.Id_PhongBan, '') as IdPhongBan,
		isnull(ctct.Id_PhongBanDich, '') as IdPhongBanDich,
		isnull(ctct.QT_KhauHaoTSCĐ, 0) as QtKhauHaoTscđ,
		isnull(ctct.QT_TienLuong, 0) as QtTienLuong,
		isnull(ctct.QT_QTNSKhac, 0) as QtQtnskhac,
		isnull(ctct.ChiPhiKhac, 0) as ChiPhiKhac,
		isnull(ctct.TongnopNSNN, 0) as TongnopNsnn,
		isnull(ctct.ThueGTGT, 0) as ThueGtgt,
		isnull(ctct.GhiChu, '') as GhiChu,
		ctct.DateCreated as DateCreated,
		isnull(ctct.UserCreator, '') as UserCreator,
		ctct.DateModified as DateModified,
		isnull(ctct.UserModifier, '') as UserModifier,
		isnull(ctct.Tag, '') as Tag,
		isnull(ctct.Log, '') as Log,
		isnull(ctct.Id_DonViTao, '') as IdDonViTao,
		isnull(ctct.iGuiNhan, 0) as IGuiNhan,
		isnull(ctct.ThueTNDN, 0) as ThueTndn,
		isnull(ctct.ThueTNDN_BQP, 0) as ThueTndnBqp,
		isnull(ctct.Phi_LePhi, 0) as PhiLePhi,
		isnull(ctct.NSNN_Khac, 0) as NsnnKhac,
		isnull(ctct.NSNN_Khac_BQP, 0) as NsnnKhacBqp,
		isnull(ctct.ChenhLech, 0) as ChenhLech,
		isnull(ctct.PP_NopNSQP, 0) as PpNopNsqp,
		isnull(ctct.PP_BoSungKinhPhi, 0) as PpBoSungKinhPhi,
		isnull(ctct.PP_TrichCacQuy, 0) as PpTrichCacQuy,
		isnull(ctct.PP_SoChuaPhanPhoi, 0) as PpSoChuaPhanPhoi,
		isnull(ctct.bThoaiThu, 0) as BThoaiThu
	FROM  (SELECT * FROM f_loai_hinh_by_lns(@YearOfWork, @LNS)) mlns
	LEFT JOIN (
		SELECT
			*
		FROM
			TN_QT_ChungTuChiTiet
		WHERE
			NamLamViec = @YearOfWork
			AND NamNganSach = @YearOfBudget
			AND NguonNganSach = @BudgetSource
			AND Id_ChungTu = @ChungTuId
	) ctct ON mlns.ID_MaLoaiHinh = ctct.ID_MaLoaiHinh
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
		--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
	ORDER BY mlns.LNS
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_qt_rpt_thong_tri_thu_nop]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_qt_rpt_thong_tri_thu_nop]
	@IdDonVi nvarchar(50),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@IMonthQuater int,
	@IMonthQuaterType int
AS
BEGIN
	SELECT
			isnull(ctct.Id, NEWID()) AS Id,
			ctct.Id_ChungTu as IdChungTu,
			mlns.ID_MaLoaiHinh as IdMaLoaiHinh,
			mlns.ID_MaLoaiHinh_Cha as IdMaLoaiHinhCha,
			mlns.LNS,
			isnull(mlns.MoTa, '') as Noidung,
			isnull(ctct.iThangQuyLoai, 0) as iThangQuyLoai,
			isnull(ctct.iThangQuy, 1) as iThangQuy,
			isnull(ctctsum.TongSoThuSum, 0) as TongSoThu,
			mlns.bLaHangCha,
			isnull(ctct.NamNganSach, @YearOfBudget) as NamNganSach,
			isnull(ctct.NguonNganSach, @BudgetSource) as NguonNganSach,
			mlns.iNamLamViec as NamLamViec,
			mlns.iTrangThai,
			isnull(ctct.iLoai, 0) as iLoai,
			isnull(ctctsum.TongSoChiPhiSum, 0) as TongSoChiPhi,
			isnull(ctctsum.QT_TongSoQTNSSum, 0) as QtTongSoQtns,
			isnull(ctct.Id_DonVi, '') as IdDonVi,
			isnull(ctct.TenDonVi, '') as TenDonVi,
			isnull(ctct.Id_PhongBan, '') as IdPhongBan,
			isnull(ctct.Id_PhongBanDich, '') as IdPhongBanDich,
			isnull(ctctsum.QT_KhauHaoTSCĐSum, 0) as QtKhauHaoTscđ,
			isnull(ctctsum.QT_TienLuongSum, 0) as QtTienLuong,
			isnull(ctctsum.QT_QTNSKhacSum, 0) as QtQtnskhac,
			isnull(ctctsum.ChiPhiKhacSum, 0) as ChiPhiKhac,
			isnull(ctctsum.TongnopNSNNSum, 0) as TongnopNSNN,
			isnull(ctctsum.ThueGTGTSum, 0) as ThueGTGT,
			isnull(ctct.GhiChu, '') as GhiChu,
			ctct.DateCreated,
			isnull(ctct.UserCreator, '') as UserCreator,
			ctct.DateModified,
			isnull(ctct.UserModifier, '') as UserModifier,
			isnull(ctct.Tag, '') as Tag,
			isnull(ctct.Log, '') as Log,
			isnull(ctct.Id_DonViTao, '') as IdDonViTao,
			isnull(ctct.iGuiNhan, 0) as iGuiNhan,
			isnull(ctctsum.ThueTNDNSum, 0) as ThueTndn,
			isnull(ctctsum.ThueTNDN_BQPSum, 0) as ThueTndnBqp,
			isnull(ctctsum.Phi_LePhiSum, 0) as PhiLePhi,
			isnull(ctctsum.NSNN_KhacSum, 0) as NsnnKhac,
			isnull(ctctsum.NSNN_Khac_BQPSum, 0) as NsnnKhacBqp,
			isnull(ctctsum.ChenhLechSum, 0) as ChenhLech,
			isnull(ctctsum.PP_NopNSQPSum, 0) as PpNopNsqp,
			isnull(ctctsum.PP_BoSungKinhPhiSum, 0) as PpBoSungKinhPhi,
			isnull(ctctsum.PP_TrichCacQuySum, 0) as PpTrichCacQuy,
			isnull(ctctsum.PP_SoChuaPhanPhoiSum, 0) as PpSoChuaPhanPhoi,
			isnull(ctct.bThoaiThu, 0) as bThoaiThu
		FROM  (SELECT * FROM f_loai_hinh_by_lns(@YearOfWork, @LNS)) mlns
		LEFT JOIN (
			SELECT
				*
			FROM
				TN_QT_ChungTuChiTiet
			WHERE
				NamLamViec = @YearOfWork
				AND NamNganSach = @YearOfBudget
				AND NguonNganSach = @BudgetSource
		) ctct ON mlns.ID_MaLoaiHinh = ctct.ID_MaLoaiHinh
		LEFT JOIN
		(
			SELECT
				SUM(TongSoThu) as TongSoThuSum,
				SUM(TongSoChiPhi) as TongSoChiPhiSum, 
				SUM(QT_KhauHaoTSCĐ) as QT_KhauHaoTSCĐSum,
				SUM(QT_TienLuong) as QT_TienLuongSum,
				SUM(QT_QTNSKhac) as QT_QTNSKhacSum, 
				SUM(ChiPhiKhac) as ChiPhiKhacSum,
				SUM(TongnopNSNN) as TongnopNSNNSum,
				SUM(ThueGTGT) as ThueGTGTSum,
				SUM(ThueTNDN) as ThueTNDNSum,
				SUM(ThueTNDN_BQP) as ThueTNDN_BQPSum,
				SUM(Phi_LePhi) as Phi_LePhiSum,
				SUM(NSNN_Khac) as NSNN_KhacSum,
				SUM(NSNN_Khac_BQP) as NSNN_Khac_BQPSum, 
				SUM(ChenhLech) as ChenhLechSum,
				SUM(PP_NopNSQP) as PP_NopNSQPSum,
				SUM(PP_BoSungKinhPhi) as PP_BoSungKinhPhiSum,
				SUM(PP_TrichCacQuy) as PP_TrichCacQuySum,
				SUM(PP_SoChuaPhanPhoi) as PP_SoChuaPhanPhoiSum,
				SUM(QT_TongSoQTNS) as QT_TongSoQTNSSum,
				ID_MaLoaiHinh
			FROM
				TN_QT_ChungTuChiTiet
			WHERE
				NamLamViec = @YearOfWork
				AND NamNganSach = @YearOfBudget
				AND NguonNganSach = @BudgetSource
				AND iThangQuyLoai = @IMonthQuaterType
				AND Id_DonVi =@IdDonVi
				AND iThangQuy = @IMonthQuater
			GROUP BY ID_MaLoaiHinh
		)ctctsum ON mlns.ID_MaLoaiHinh = ctctsum.ID_MaLoaiHinh
		WHERE
			mlns.iNamLamViec = @YearOfWork
			AND mlns.iTrangThai = 1
			AND ((ctct.iThangQuy = @IMonthQuater and ctct.iThangQuyLoai = @IMonthQuaterType and ctct.Id_DonVi = @IdDonVi) or mlns.bLaHangCha = 1)
		ORDER BY mlns.LNS
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_qt_rpt_tong_hop_thu_nop]    Script Date: 03/08/2022 6:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_qt_rpt_tong_hop_thu_nop]
	@IdDonVi nvarchar(50),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type int,
	@IMonthQuater int,
	@IMonthQuaterType int
AS
BEGIN
	IF (@Type = 2)
	BEGIN
		SELECT
			isnull(ctct.Id, NEWID()) AS Id,
			ctct.Id_ChungTu as IdChungTu,
			mlns.ID_MaLoaiHinh as IdMaLoaiHinh,
			mlns.ID_MaLoaiHinh_Cha as IdMaLoaiHinhCha,
			mlns.LNS,
			isnull(mlns.MoTa, '') as Noidung,
			isnull(ctct.iThangQuyLoai, 0) as iThangQuyLoai,
			isnull(ctct.iThangQuy, 1) as iThangQuy,
			isnull(ctct.TongSoThu, 0) as TongSoThu,
			mlns.bLaHangCha,
			isnull(ctct.NamNganSach, @YearOfBudget) as NamNganSach,
			isnull(ctct.NguonNganSach, @BudgetSource) as NguonNganSach,
			mlns.iNamLamViec as NamLamViec,
			mlns.iTrangThai,
			isnull(ctct.iLoai, 0) as iLoai,
			isnull(ctct.TongSoChiPhi, 0) as TongSoChiPhi,
			isnull(ctct.QT_TongSoQTNS, 0) as QtTongSoQtns,
			isnull(ctct.Id_DonVi, '') as IdDonVi,
			isnull(ctct.TenDonVi, '') as TenDonVi,
			isnull(ctct.Id_PhongBan, '') as IdPhongBan,
			isnull(ctct.Id_PhongBanDich, '') as IdPhongBanDich,
			isnull(ctct.QT_KhauHaoTSCĐ, 0) as QtKhauHaoTscđ,
			isnull(ctct.QT_TienLuong, 0) as QtTienLuong,
			isnull(ctct.QT_QTNSKhac, 0) as QtQtnskhac,
			isnull(ctct.ChiPhiKhac, 0) as ChiPhiKhac,
			isnull(ctct.TongnopNSNN, 0) as TongnopNSNN,
			isnull(ctct.ThueGTGT, 0) as ThueGTGT,
			isnull(ctct.GhiChu, '') as GhiChu,
			ctct.DateCreated,
			isnull(ctct.UserCreator, '') as UserCreator,
			ctct.DateModified,
			isnull(ctct.UserModifier, '') as UserModifier,
			isnull(ctct.Tag, '') as Tag,
			isnull(ctct.Log, '') as Log,
			isnull(ctct.Id_DonViTao, '') as IdDonViTao,
			isnull(ctct.iGuiNhan, 0) as iGuiNhan,
			isnull(ctct.ThueTNDN, 0) as ThueTndn,
			isnull(ctct.ThueTNDN_BQP, 0) as ThueTndnBqp,
			isnull(ctct.Phi_LePhi, 0) as PhiLePhi,
			isnull(ctct.NSNN_Khac, 0) as NsnnKhac,
			isnull(ctct.NSNN_Khac_BQP, 0) as NsnnKhacBqp,
			isnull(ctct.ChenhLech, 0) as ChenhLech,
			isnull(ctct.PP_NopNSQP, 0) as PpNopNsqp,
			isnull(ctct.PP_BoSungKinhPhi, 0) as PpBoSungKinhPhi,
			isnull(ctct.PP_TrichCacQuy, 0) as PpTrichCacQuy,
			isnull(ctct.PP_SoChuaPhanPhoi, 0) as PpSoChuaPhanPhoi,
			isnull(ctct.bThoaiThu, 0) as bThoaiThu
		FROM  (SELECT * FROM f_loai_hinh_by_lns(@YearOfWork, @LNS)) mlns
		LEFT JOIN (
			SELECT
				*
			FROM
				TN_QT_ChungTuChiTiet
			WHERE
				NamLamViec = @YearOfWork
				AND NamNganSach = @YearOfBudget
				AND NguonNganSach = @BudgetSource
		) ctct ON mlns.ID_MaLoaiHinh = ctct.ID_MaLoaiHinh
		WHERE
			mlns.iNamLamViec = @YearOfWork
			AND mlns.iTrangThai = 1
			AND (mlns.bLaHangCha = 1 or (ctct.Id_DonVi = @IdDonVi and ctct.iThangQuyLoai = @IMonthQuaterType))
		ORDER BY mlns.LNS
	END
	ELSE
	BEGIN
		SELECT
			isnull(ctct.Id, NEWID()) AS Id,
			ctct.Id_ChungTu as IdChungTu,
			mlns.ID_MaLoaiHinh as IdMaLoaiHinh,
			mlns.ID_MaLoaiHinh_Cha as IdMaLoaiHinhCha,
			mlns.LNS,
			isnull(mlns.MoTa, '') as Noidung,
			isnull(ctct.iThangQuyLoai, 0) as iThangQuyLoai,
			isnull(ctct.iThangQuy, 1) as iThangQuy,
			isnull(ctctsum.TongSoThuSum, 0) as TongSoThu,
			mlns.bLaHangCha,
			isnull(ctct.NamNganSach, @YearOfBudget) as NamNganSach,
			isnull(ctct.NguonNganSach, @BudgetSource) as NguonNganSach,
			mlns.iNamLamViec as NamLamViec,
			mlns.iTrangThai,
			isnull(ctct.iLoai, 0) as iLoai,
			isnull(ctctsum.TongSoChiPhiSum, 0) as TongSoChiPhi,
			isnull(ctctsum.QT_TongSoQTNSSum, 0) as QtTongSoQtns,
			isnull(ctct.Id_DonVi, '') as IdDonVi,
			isnull(ctct.TenDonVi, '') as TenDonVi,
			isnull(ctct.Id_PhongBan, '') as IdPhongBan,
			isnull(ctct.Id_PhongBanDich, '') as IdPhongBanDich,
			isnull(ctctsum.QT_KhauHaoTSCĐSum, 0) as QtKhauHaoTscđ,
			isnull(ctctsum.QT_TienLuongSum, 0) as QtTienLuong,
			isnull(ctctsum.QT_QTNSKhacSum, 0) as QtQtnskhac,
			isnull(ctctsum.ChiPhiKhacSum, 0) as ChiPhiKhac,
			isnull(ctctsum.TongnopNSNNSum, 0) as TongnopNSNN,
			isnull(ctctsum.ThueGTGTSum, 0) as ThueGTGT,
			isnull(ctct.GhiChu, '') as GhiChu,
			ctct.DateCreated,
			isnull(ctct.UserCreator, '') as UserCreator,
			ctct.DateModified,
			isnull(ctct.UserModifier, '') as UserModifier,
			isnull(ctct.Tag, '') as Tag,
			isnull(ctct.Log, '') as Log,
			isnull(ctct.Id_DonViTao, '') as IdDonViTao,
			isnull(ctct.iGuiNhan, 0) as iGuiNhan,
			isnull(ctctsum.ThueTNDNSum, 0) as ThueTndn,
			isnull(ctctsum.ThueTNDN_BQPSum, 0) as ThueTndnBqp,
			isnull(ctctsum.Phi_LePhiSum, 0) as PhiLePhi,
			isnull(ctctsum.NSNN_KhacSum, 0) as NsnnKhac,
			isnull(ctctsum.NSNN_Khac_BQPSum, 0) as NsnnKhacBqp,
			isnull(ctctsum.ChenhLechSum, 0) as ChenhLech,
			isnull(ctctsum.PP_NopNSQPSum, 0) as PpNopNsqp,
			isnull(ctctsum.PP_BoSungKinhPhiSum, 0) as PpBoSungKinhPhi,
			isnull(ctctsum.PP_TrichCacQuySum, 0) as PpTrichCacQuy,
			isnull(ctctsum.PP_SoChuaPhanPhoiSum, 0) as PpSoChuaPhanPhoi,
			isnull(ctct.bThoaiThu, 0) as bThoaiThu
		FROM  (SELECT * FROM f_loai_hinh_by_lns(@YearOfWork, @LNS)) mlns
		LEFT JOIN (
			SELECT
				*
			FROM
				TN_QT_ChungTuChiTiet
			WHERE
				NamLamViec = @YearOfWork
				AND NamNganSach = @YearOfBudget
				AND NguonNganSach = @BudgetSource
		) ctct ON mlns.ID_MaLoaiHinh = ctct.ID_MaLoaiHinh
		LEFT JOIN
		(
			SELECT
				SUM(TongSoThu) as TongSoThuSum,
				SUM(TongSoChiPhi) as TongSoChiPhiSum, 
				SUM(QT_KhauHaoTSCĐ) as QT_KhauHaoTSCĐSum,
				SUM(QT_TienLuong) as QT_TienLuongSum,
				SUM(QT_QTNSKhac) as QT_QTNSKhacSum, 
				SUM(ChiPhiKhac) as ChiPhiKhacSum,
				SUM(TongnopNSNN) as TongnopNSNNSum,
				SUM(ThueGTGT) as ThueGTGTSum,
				SUM(ThueTNDN) as ThueTNDNSum,
				SUM(ThueTNDN_BQP) as ThueTNDN_BQPSum,
				SUM(Phi_LePhi) as Phi_LePhiSum,
				SUM(NSNN_Khac) as NSNN_KhacSum,
				SUM(NSNN_Khac_BQP) as NSNN_Khac_BQPSum, 
				SUM(ChenhLech) as ChenhLechSum,
				SUM(PP_NopNSQP) as PP_NopNSQPSum,
				SUM(PP_BoSungKinhPhi) as PP_BoSungKinhPhiSum,
				SUM(PP_TrichCacQuy) as PP_TrichCacQuySum,
				SUM(PP_SoChuaPhanPhoi) as PP_SoChuaPhanPhoiSum,
				SUM(QT_TongSoQTNS) as QT_TongSoQTNSSum,
				ID_MaLoaiHinh
			FROM
				TN_QT_ChungTuChiTiet
			WHERE
				NamLamViec = @YearOfWork
				AND NamNganSach = @YearOfBudget
				AND NguonNganSach = @BudgetSource
				AND iThangQuyLoai = @Type
				AND Id_DonVi =@IdDonVi
				AND iThangQuy = @IMonthQuater
			GROUP BY ID_MaLoaiHinh
		)ctctsum ON mlns.ID_MaLoaiHinh = ctctsum.ID_MaLoaiHinh
		WHERE
			mlns.iNamLamViec = @YearOfWork
			AND mlns.iTrangThai = 1
			AND ((ctct.iThangQuy = @IMonthQuater and ctct.iThangQuyLoai = @Type and ctct.Id_DonVi = @IdDonVi) or mlns.bLaHangCha = 1)
		ORDER BY mlns.LNS
	END
END
;
;
GO
