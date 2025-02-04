/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_phucap]    Script Date: 17/11/2022 11:19:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_phucap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_phucap]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 17/11/2022 11:19:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet]    Script Date: 17/11/2022 11:19:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_namkehoach]    Script Date: 17/11/2022 11:19:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_namkehoach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_namkehoach]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet]    Script Date: 17/11/2022 11:19:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 17/11/2022 11:19:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 17/11/2022 11:19:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_get_dmphucap_dctapthecanbo]    Script Date: 17/11/2022 11:19:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_luong_get_dmphucap_dctapthecanbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_luong_get_dmphucap_dctapthecanbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_hienvat_1]    Script Date: 17/11/2022 11:19:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_hienvat_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_hienvat_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_1]    Script Date: 17/11/2022 11:19:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]    Script Date: 17/11/2022 11:19:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_dot_soquyetdinh_ngayquyetdinh_full]    Script Date: 17/11/2022 11:19:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dt_dot_soquyetdinh_ngayquyetdinh_full]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dt_dot_soquyetdinh_ngayquyetdinh_full]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_soquyetdinh_ngayquyetdinh_full]    Script Date: 17/11/2022 11:19:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dt_soquyetdinh_ngayquyetdinh_full]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dt_soquyetdinh_ngayquyetdinh_full]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_soquyetdinh_ngayquyetdinh]    Script Date: 17/11/2022 11:19:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dt_soquyetdinh_ngayquyetdinh]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dt_soquyetdinh_ngayquyetdinh]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_soquyetdinh_ngayquyetdinh]    Script Date: 17/11/2022 11:19:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[f_dt_soquyetdinh_ngayquyetdinh]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @lns nvarchar(500),
	  @NgayQuyetDinh datetime,
	  @dvt int,
	  @LoaiDuToan int,
	  @SoQuyetDinh nvarchar(50)
)
RETURNS TABLE
AS RETURN
SELECT sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
       sTNG AS TNG,
	   sTNG1 AS TNG1,
	   sTNG2 AS TNG2,
	   sTNG3 AS TNG3,
       sXauNoiMa AS XauNoiMa,
       sMoTa AS MoTa, --Id_DonVi,
 TuChi =sum(fTuChi) /@dvt,
 HienVat =sum(fHienVat) /@dvt,
 HangNhap =sum(fHangNhap) /@dvt,
 HangMua =sum(fHangMua) /@dvt,
 PhanCap =sum(fPhanCap) /@dvt,
 DuPhong =sum(fDuPhong) /@dvt
FROM NS_DT_ChungTuChiTiet as ctct
Inner join (
	SELECT iID_DTChungTu, iLoaiDuToan
     FROM NS_DT_ChungTu
     WHERE iNamLamViec= @NamLamViec
       AND (@NamNganSach IS NULL
            OR iNamNganSach in
              (SELECT *
               FROM f_split(@NamNganSach)))
	  AND (@NguonNganSach IS NULL
            OR iID_MaNguonNganSach in
              (SELECT *
               FROM f_split(@NguonNganSach)))
       AND iLoai=0
	   AND (@SoQuyetDinh IS NULL OR sSoQuyetDinh = @SoQuyetDinh)
	   AND cast(dNgayQuyetDinh AS date) <= cast(@NgayQuyetDinh AS date) 
	   AND ((iLoaiDuToan = @LoaiDuToan) or (@LoaiDuToan = 0) or(@LoaiDuToan = 3 and iLoaiDuToan in(3,5)))
	   
) as ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
WHERE iNamLamViec=@NamLamViec
  AND (@NamNganSach IS NULL
       OR iNamNganSach in
         (SELECT *
          FROM f_split(@NamNganSach)))
  AND (@NguonNganSach IS NULL
       OR iID_MaNguonNganSach in
         (SELECT *
          FROM f_split(@NguonNganSach)))
  AND iPhanCap=0
  AND (@lns IS NULL
       OR sLNS in
         (SELECT *
          FROM f_split(@lns)))
  
GROUP BY sLNS,
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
         sMoTa
HAVING sum(fTuChi)<>0

;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_soquyetdinh_ngayquyetdinh_full]    Script Date: 17/11/2022 11:19:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[f_dt_soquyetdinh_ngayquyetdinh_full]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @lns nvarchar(500),
	  @NgayQuyetDinh datetime,
	  @dvt int,
	  @LoaiDuToan int,
	  @SoQuyetDinh nvarchar(50)
)
RETURNS TABLE
AS RETURN

select	
		LNS,L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3,
		XauNoiMa,MoTa
		,TuChi		=sum(TuChi),
		HienVat		=sum(HienVat)
from 
(
	-- trenphanbo

	select 
		LNS,L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3
		,XauNoiMa,MoTa
		,TuChi
		,HienVat
	from f_dt_soquyetdinh_ngayquyetdinh(@NamLamViec,@NamNganSach,@NguonNganSach,@lns,@NgayQuyetDinh,@dvt,@LoaiDuToan,@SoQuyetDinh)

	-- hangnhap
	union all
	select 
		LNS='1040200',L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3
		,XauNoiMa='1040200'+'-'+L+'-'+K+'-'+M+'-'+TM+'-'+TTM+'-'+NG+(case tng when '' then '' else (+'-'+TNG) end)
		,MoTa
		,TuChi		=HangNhap
		,HienVat=0
	from f_dt_soquyetdinh_ngayquyetdinh(@NamLamViec,@NamNganSach,@NguonNganSach,'1040100',@NgayQuyetDinh,@dvt,@LoaiDuToan,@SoQuyetDinh)
	where	(@lns is null or '1040100' in (select * from f_split(@lns)))

	-- hangmua
	union all
	select 
		LNS='1040300',L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3
		,XauNoiMa='1040300'+'-'+L+'-'+K+'-'+M+'-'+TM+'-'+TTM+'-'+NG+(case tng when '' then '' else (+'-'+TNG) end)
		,MoTa
		,TuChi		=HangMua
		,HienVat=0
	from f_dt_soquyetdinh_ngayquyetdinh(@NamLamViec,@NamNganSach,@NguonNganSach,'1040100',@NgayQuyetDinh,@dvt,@LoaiDuToan,@SoQuyetDinh)
	where	(@lns is null or '1040100' in (select * from f_split(@lns)))

)as a
group by LNS, L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3,XauNoiMa,MoTa
having	sum(TuChi)<>0
;
;
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_dot_soquyetdinh_ngayquyetdinh_full]    Script Date: 17/11/2022 11:19:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[f_dt_dot_soquyetdinh_ngayquyetdinh_full]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @lns nvarchar(500),
	  @NgayQuyetDinh datetime,
	  @dvt int,
	  @LoaiDuToan int,
	  @SoQuyetDinh nvarchar(50)
)
RETURNS TABLE
AS RETURN

SELECT sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
       sTNG AS TNG,
	   sTNG1 AS TNG1,
	   sTNG2 AS TNG2,
	   sTNG3 AS TNG3,
       sXauNoiMa AS XauNoiMa,
       sMoTa AS MoTa,
       TuChi =sum(fTuChi)/@dvt,
       HienVat =sum(fHienVat)/@dvt
FROM NS_DT_ChungTuChiTiet
WHERE iNamLamViec=@NamLamViec
  AND (@NamNganSach IS NULL
       OR iNamNganSach in
         (SELECT *
          FROM f_split(@NamNganSach)))
  AND (@NguonNganSach IS NULL
       OR iID_MaNguonNganSach in
         (SELECT *
          FROM f_split(@NguonNganSach)))
  AND iPhanCap=0
  AND (@lns IS NULL
       OR sLNS in
         (SELECT *
          FROM f_split(@lns)))
  AND iID_DTChungTu in
    (SELECT iID_DTChungTu
     FROM NS_DT_ChungTu
     WHERE ((iLoaiDuToan = @LoaiDuToan) OR (@LoaiDuToan = 0) or (@LoaiDuToan = 3 and iLoaiDuToan in (3,5)))
       AND iNamLamViec=@NamLamViec
       AND (@NamNganSach IS NULL
            OR iNamNganSach in
              (SELECT *
               FROM f_split(@NamNganSach)))
       AND (@NguonNganSach IS NULL
            OR iID_MaNguonNganSach in
              (SELECT *
               FROM f_split(@NguonNganSach)))
       AND iLoai=0
       AND cast(dNgayQuyetDinh AS date) <= cast(@NgayQuyetDinh AS date)
	   AND (@SoQuyetDinh is null or sSoQuyetDinh = @SoQuyetDinh)
	   )
GROUP BY sLNS,
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
         sMoTa
HAVING sum(fHienVat)<>0 or sum(fTuChi)<>0
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]    Script Date: 17/11/2022 11:19:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type int,
	@VoucherType int,
	@Date datetime,
	@Index int
AS
BEGIN
	DECLARE @DieuChinh int = 4;
	WITH tblNhanPhanBo AS (
		SELECT *, 
			   fTongTuChi + fTongHienVat + fTongPhanCap + fTongHangNhap + fTongHangMua + fTongDuPhong + fTongTonKho AS SoPhanBo
		FROM NS_DT_ChungTu 
		WHERE iNamLamViec = @YearOfWork 
			AND iNamNganSach = @YearOfBudget 
			AND iID_MaNguonNganSach = @BudgetSource
			AND iLoai = @Type 
			AND iLoaiChungTu = @VoucherType
			AND (fTongTuChi <> 0 OR fTongHienVat <> 0 OR fTongPhanCap <> 0 OR fTongHangNhap <> 0 OR fTongHangMua <> 0 OR fTongDuPhong <> 0 OR fTongTonKho <> 0)
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))
	),
	tblChungTuNhanPhanBoMap AS (
		SELECT * 
		FROM NS_DT_Nhan_PhanBo_Map 
		WHERE iID_CTDuToan_Nhan IN (SELECT iID_DTChungTu FROM tblNhanPhanBo)
	),
	tblDaPhanBo AS (
		SELECT ctpb.iID_CTDuToan_Nhan, 
			   SUM(fTuChi) + SUM(fHienVat) + SUM(fPhanCap) + SUM(fHangNhap) + SUM(fHangMua) + SUM(fDuPhong) + SUM(fTonKho) AS DaPhanBo 
		FROM tblChungTuNhanPhanBoMap ctpb
		LEFT JOIN 
			(SELECT dtctct.*, dtct.iLoaiDuToan, dtct.iID_DotNhan FROM NS_DT_ChungTuChiTiet dtctct
			 LEFT JOIN NS_DT_ChungTu dtct
			 ON dtctct.iID_DTChungTu = dtct.iID_DTChungTu
			 WHERE dtctct.iNamLamViec = @YearOfWork 
				   AND dtct.iLoai = 1
				   AND dtct.iLoaiChungTu = 1
				   AND (dNgayQuyetDinh IS NOT NULL AND (CAST(dNgayQuyetDinh AS DATE) < CAST(@Date AS DATE) OR (CAST(dNgayQuyetDinh AS DATE) = CAST(@Date AS DATE) AND @Index > iSoChungTuIndex))) 
				   OR (dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND (CAST(dNgayChungTu AS DATE) < CAST(@Date AS DATE) OR (CAST(dNgayChungTu AS DATE) = CAST(@Date AS DATE) AND @Index > iSoChungTuIndex)))
				   AND dtctct.iNamNganSach = @YearOfBudget 
				   AND dtctct.iID_MaNguonNganSach = @BudgetSource) ctct
		ON ctpb.iID_CTDuToan_Nhan = ctct.iID_CTDuToan_Nhan
		WHERE ctct.iID_DTChungTu IS NOT NULL
		and 
		(
			(ctpb.iID_CTDuToan_PhanBo = ctct.iID_DTChungTu AND ctct.iLoaiDuToan <> @DieuChinh)
			OR
			(ctct.iLoaiDuToan = @DieuChinh AND ctct.iID_DotNhan = ctpb.iID_CTDuToan_PhanBo)
		)
		GROUP BY ctpb.iID_CTDuToan_Nhan
	)

	SELECT 
		   npb.iID_DTChungTu AS Id,
		   npb.sSoChungTu AS SSoChungTu,
		   npb.dNgayChungTu AS DNgayChungTu,
		   npb.sDSLNS AS SDslns,
		   npb.sDSID_MaDonVi AS SDsidMaDonVi,
		   npb.sSoQuyetDinh AS SSoQuyetDinh,
		   npb.dNgayQuyetDinh AS DNgayQuyetDinh,
		   ISNULL(npb.SoPhanBo, 0) AS SoPhanBo,
		   ISNULL(DaPhanBo, 0) AS DaPhanBo,
		   ISNULL(npb.SoPhanBo, 0) - ISNULL(DaPhanBo, 0) AS SoChuaPhanBo 
	FROM tblNhanPhanBo npb
	LEFT JOIN tblDaPhanBo dpb
	ON npb.iID_DTChungTu = dpb.iID_CTDuToan_Nhan
	WHERE ISNULL(npb.SoPhanBo, 0) > ISNULL(DaPhanBo, 0)
	ORDER BY npb.sSoChungTu

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_1]    Script Date: 17/11/2022 11:19:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_1]
	 @NamLamViec int,							
	 @NguonNganSach nvarchar(50),
	 @NamNganSach nvarchar(50),
	 @NgayQuyetDinh datetime,
	 @LNS ntext,
	 @Dvt int,
	 @LoaiDuToan int,
	 @SoQuyetDinh nvarchar(50)
AS	 
BEGIN 
	SET NOCOUNT ON;
	select  
		LNS1 =left(LNS,1),
		LNS3 =left(LNS,3),
		LNS5 =left(LNS,5),
		LNS,L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3,XauNoiMa,
		MoTa,
		TuChi	=sum(TuChi),
		HienVat	=sum(HienVat)
		,mlns.iID_MLNS AS MlnsId
		,mlns.iID_MLNS_Cha AS MlnsIdParent
from f_dt_soquyetdinh_ngayquyetdinh_full(@NamLamViec,@NamNganSach,@NguonNganSach,@LNS,@NgayQuyetDinh,@Dvt,@LoaiDuToan,@SoQuyetDinh) 
inner join (select * from NS_MucLucNganSach where iNamLamViec = @NamLamViec and bHangChaDuToan = 0 and sLNS in (select * from f_split(@LNS))) mlns
on mlns.sXauNoiMa = XauNoiMa
group by LNS, L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3,XauNoiMa,MoTa,iID_MLNS
		,iID_MLNS_Cha
having sum(TuChi)<>0


END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_hienvat_1]    Script Date: 17/11/2022 11:19:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_hienvat_1]
	 @NamLamViec int,							
	 @NguonNganSach nvarchar(50),
	 @NamNganSach nvarchar(50),
	 @NgayChungTu datetime,
	 @LNS ntext,
	 @Dvt int,
	 @LoaiDuToan int,
	 @SoQuyetDinh nvarchar(50)
AS	 
BEGIN 
	SET NOCOUNT ON;
	select  
		LNS1 =left(LNS,1),
		LNS3 =left(LNS,3),
		LNS5 =left(LNS,5),
		LNS,L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3,XauNoiMa,
		MoTa,
		TuChi	=sum(TuChi),
		HienVat	=sum(HienVat)
		,mlns.iID_MLNS AS MlnsId
		,mlns.iID_MLNS_Cha AS MlnsIdParent
from f_dt_dot_soquyetdinh_ngayquyetdinh_full(@NamLamViec,@NamNganSach,@NguonNganSach,@LNS,@NgayChungTu,@Dvt,@LoaiDuToan,@SoQuyetDinh) 
inner join (select * from NS_MucLucNganSach where iNamLamViec = @NamLamViec and bHangChaDuToan = 0 and sLNS in (select * from f_split(@LNS))) mlns
on mlns.sXauNoiMa = XauNoiMa
group by LNS, L,K,M,TM,TTM,NG,TNG,TNG1,TNG2,TNG3,XauNoiMa,MoTa,iID_MLNS
		,iID_MLNS_Cha
having sum(HienVat)<>0 or sum(TuChi)<>0
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_get_dmphucap_dctapthecanbo]    Script Date: 17/11/2022 11:19:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_luong_get_dmphucap_dctapthecanbo]
AS
BEGIN
	CREATE TABLE #tmpExclude(code nvarchar(200))
	INSERT INTO #tmpExclude(code) VALUES('THUONG_TT'),('GIAMTHUE_TT'),('THUNHAPKHAC_TT'),('THUEDANOP_TT'),('TIENCTLH_TT'),('TIENANDUONG_TT'),('TIENTAUXE_TT')

	CREATE TABLE #tmp(id uniqueidentifier, code nvarchar(200))
	CREATE TABLE #child(id uniqueidentifier)

	INSERT INTO #tmp(id, code)
	SELECT Id , Ma_PhuCap
	FROM TL_DM_PhuCap as pc
	LEFT JOIN #tmpExclude as ec on pc.Ma_PhuCap = ec.code
	WHERE ISNULL(Parent, '') = ''
	AND Chon = 1 AND Is_Formula = 0 AND Is_Readonly = 0 AND ec.code IS NULL

	INSERT INTO #child(id)
	SELECT dt.Id
	FROM #tmp as tmp
	INNER JOIN TL_DM_PhuCap as dt on tmp.code = dt.Parent
	LEFT JOIN #tmpExclude as ec on dt.Ma_PhuCap = ec.code
	WHERE dt.Chon = 1 AND Is_Formula = 0 AND Is_Readonly = 0 AND ec.code IS NULL

	SELECT tbl.*
	FROM #child as tmp
	INNER JOIN TL_DM_PhuCap AS tbl on tmp.id = tbl.Id

	DROP TABLE #tmp
	DROP TABLE #child
END
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet]    Script Date: 17/11/2022 11:19:51 AM ******/
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
								AND (iLoai = 1 or iLoai = 0)
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 17/11/2022 11:19:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@SoNgay int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    WITH SoLieuTienAn AS (
		SELECT
			MA_CBO MaCanBo,
			SUM (
				CASE WHEN PARENT <> N'TIENAN' THEN 0
					WHEN canBoPhuCap.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI 
					ELSE ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
				END
			) GiaTriTA1,
			SUM (
				CASE WHEN PARENT <> N'TIENAN2' THEN 0
					WHEN canBoPhuCap.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI 
					ELSE ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
				END
			) GiaTriTA2
		FROM TL_CanBo_PhuCap canBoPhuCap
		INNER JOIN TL_DM_PhuCap as pc on canBoPhuCap.MA_PHUCAP = pc.Ma_PhuCap
		--WHERE
			--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		WHERE 
			pc.PARENT IN ('TIENAN', 'TIENAN2')
		GROUP BY canBoPhuCap.MA_CBO
	),

	ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_Cb MaCapBac,
			canBo.BHTN,
			canBo.PCCV
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER  JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.Thang = @Thang
			AND canBo.Nam = @Nam
			AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
			AND canBo.IsDelete = 1
			AND canBo.Khong_Luong <> 1
	)

	SELECT
		newid()					AS Id,
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in ('1', '2', '3')) THEN 0 
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTriTA1
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT2' THEN soLieuTienAn.GiaTriTA2
			WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
			ELSE canBoPhuCap.GIA_TRI
		END						AS GiaTri,
		canBo.MaHieuCanBo		AS MaHieuCanBo,
		canBo.MaCapBac			AS MaCb,
		canBoPhuCap.HuongPC_SN	AS HuongPcSn,
		cachTinhLuong.CongThuc	AS CongThuc,
		CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	FROM TL_CanBo_PhuCap canBoPhuCap
	INNER JOIN ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN SoLieuTienAn soLieuTienAn
		ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	LEFT JOIN TL_DM_Cach_TinhLuong_Chuan cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet]    Script Date: 17/11/2022 11:19:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_chungtu_chitiet]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	
SELECT Ma_Cot, pc.Ma_PhuCap INTO #tmpMapping
	FROM 
	(SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
	INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') ;

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.HuongPC_SN, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo as cb
	INNER JOIN TL_CanBo_PhuCap as pc on cb.Ma_CanBo = pc.MA_CBO
	INNER JOIN #tmpMapping as mp on pc.MA_PHUCAP = mp.Ma_PhuCap
	WHERE cb.Parent in (SELECT * FROM f_split(@MaDonVi))
	AND Thang = @thang
	AND Nam = @nam
	GROUP BY mp.Ma_Cot, cb.Ma_CanBo;

	WITH LuongCapBac AS (
		SELECT
			dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.MA_PHUCAP			AS MaPhuCap,
			bangLuong.Ma_CB				AS MaCapBac,
			capBac.Parent				AS Ngach,
			SUM(ISNULL(cbpc.HuongPC_SN, 0)) AS SoNgay,
			SUM(
				CASE WHEN pc.Ma_PhuCap IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN (dbo.fnTotalDayOfMonth(@thang,@nam)*bangLuong.Gia_Tri)
					WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN ISNULL(pc.HuongPC_SN, 0) * bangLuong.Gia_Tri
					ELSE bangLuong.Gia_Tri END
			)		AS GiaTri,
			COUNT(bangLuong.Ma_CBo)		AS SoNguoi
		FROM TL_BangLuong_Thang bangLuong
		INNER JOIN TL_DM_PhuCap as pc on bangLuong.Ma_PhuCap = pc.Ma_PhuCap
		LEFT JOIN #tmpSoNgay  as cbpc on bangLuong.Ma_PhuCap = cbpc.Ma_Cot AND bangLuong.Ma_CBo = cbpc.Ma_CanBo
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		JOIN TL_DM_CapBac capBac
			ON bangLuong.Ma_CB = capBac.Ma_Cb
		WHERE
			dsCapNhapBangLuong.Ma_CachTL IN (SELECT * FROM f_split(@maCachTl))
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
			AND dsCapNhapBangLuong.Thang=@Thang
			AND dsCapNhapBangLuong.Nam=@Nam
			AND dsCapNhapBangLuong.Status=1
			AND bangLuong.Gia_Tri != 0
		GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.MA_PHUCAP,bangLuong.Ma_CB, capBac.Parent
	), LuongCapBacMlns AS (
		SELECT
			luongCapBac.MaDonVi,
			phuCapMlns.XauNoiMa,
			SoNguoi,
			SoNgay,
			GiaTri
		FROM TL_PhuCap_MLNS phuCapMlns
		JOIN LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
		WHERE
			phuCapMlns.Nam = @nam
	),

	DataDuToan as (
		Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
		From TL_QT_ChungTuChiTiet ctchitiet
		Join TL_QT_ChungTu chungtu
		on chungtu.ID = ctchitiet.Id_ChungTu
		Where Nam = @nam
		And Ma_DonVi IN (SELECT * FROM f_split(@maCachTl))
		Group By XauNoiMa, Ma_DonVi
	)

	SELECT
		luong.MaDonVi					AS MaDonVi,
		NEWID()							AS Id,
		iID_MLNS						AS MlnsId, 
		iID_MLNS_Cha					AS MlnsIdParent, 
		sXauNoiMa						AS XauNoiMa, 
		sLNS							AS Lns, 
		sL								AS L, 
		sK								AS K, 
		sM								AS M, 
		sTM								AS TM, 
		sTTM							AS TTM, 
		sNG								AS Ng, 
		sTNG							AS TNG, 
		sTNG1							AS TNG1, 
		sTNG2							AS TNG2, 
		sTNG3							AS TNG3, 
		sMoTa							AS Mota, 
		iNamLamViec						AS NamLamViec,
		bHangCha						AS BHangCha,
		sChiTietToi						AS ChiTietToi,
		CONVERT(decimal, SUM(SoNguoi))	AS SoNguoi,
		SUM(ISNULL(SoNgay, 0))			AS SoNgay,
		SUM(GiaTri)						AS TongCong,
		SUM(GiaTri)						AS DieuChinh,
		@maCachTl						AS MaCachTl,
		ISNULL(dataDuToan.DuToan, 0)	AS DDuToan
	FROM NS_MucLucNganSach mlns
	JOIN LuongCapBacMlns luong
		ON mlns.sXauNoiMa = luong.XauNoiMa
	LEFT JOIN DataDuToan dataDuToan
		ON luong.XauNoiMa = dataDuToan.XauNoiMa and luong.MaDonVi = dataDuToan.Ma_DonVi
	WHERE
		sLNS IN ('1', '101', '1010000')
		AND iNamLamViec = @nam
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan
	ORDER BY MaDonVi, sXauNoiMa

	DROP TABLE #tmpSoNgay
	DROP TABLE #tmpMapping
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_namkehoach]    Script Date: 17/11/2022 11:19:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_tl_chungtu_chitiet_namkehoach]
@maDonVi varchar(50), @thang int, @nam int

As
select iID_MLNS as MlnsId, 
	   iID_MLNS_Cha as MlnsIdParent, 
	   sXauNoiMa as XauNoiMa, 
	   sLNS as Lns, 
	   sL as L, 
	   sK as K, 
	   sM as M, 
	   sTM as TM, 
	   sTTM as TTM, 
	   sNG as Ng, 
	   sTNG as TNG, 
	   sTNG1 as TNG1, 
	   sTNG2 as TNG2, 
	   sTNG3 as TNG3, 
	   sMoTa as Mota, 
	   iNamLamViec as NamLamViec,
	   bHangCha as BHangCha,
	   sChiTietToi as ChiTietToi,
	   Sum(Tong) as TongCong,
	   NULL AS DDuToan,
	   CAST(0 as int) as SoNgay
from NS_MucLucNganSach
left join
(select TL_PhuCap_MLNS.Ma_PhuCap, XauNoiMa, MoTa, TL_PhuCap_MLNS.Ma_Cb, Ma_DonVi, THANG, Luong_CapBac.NAM, SUM(Gia_Tri) as Tong
from TL_PhuCap_MLNS
join
(select Ma_PhuCap, Ma_DonVi, THANG, NAM, TL_BangLuong_KeHoach.Ma_CB, TL_DM_CapBac.Parent, Gia_Tri
from [dbo].[TL_BangLuong_KeHoach]
Join [dbo].[TL_DM_CapBac]
On TL_BangLuong_KeHoach.Ma_CB = TL_DM_CapBac.Ma_Cb) as Luong_CapBac
on TL_PhuCap_MLNS.Ma_Cb = Luong_CapBac.Parent and TL_PhuCap_MLNS.Ma_PhuCap = Luong_CapBac.Ma_PhuCap
Where Ma_DonVi = @maDonVi
And THANG = @thang
And Luong_CapBac.NAM = @nam
And TL_PhuCap_MLNS.Nam = @nam
group by TL_PhuCap_MLNS.Ma_PhuCap, XauNoiMa, MoTa, TL_PhuCap_MLNS.Ma_Cb, Ma_DonVi, THANG, Luong_CapBac.NAM) as Luong_Capbac_Mlns
on NS_MucLucNganSach.sXauNoiMa = Luong_Capbac_Mlns.XauNoiMa
Where sLNS IN ('1', '101', '1010000')
And iNamLamViec = @nam
group by iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec,sChiTietToi, bHangCha
order by sXauNoiMa
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet]    Script Date: 17/11/2022 11:19:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet] @idChungTu nvarchar(MAX),
                                                       @nam int, @maCachTl nvarchar(50) AS BEGIN

with ctct as (
  select Id as Id, XauNoiMa,MaCachTl,  Sum(TongCong) AS TongCong,
       convert(decimal,Sum(SoNguoi)) as SoNguoi,
	   convert(decimal,Sum(SoNgay)) as SoNgay,
       Sum(DieuChinh) AS DieuChinh,
     Sum(DDuToan) As DDuToan
  from TL_QT_ChungTuChiTiet 
  where   Id_ChungTu in (SELECT *  FROM f_split(@idChungTu))
    AND MaCachTl in (SELECT *  FROM f_split(@maCachTl))
  group by id, XauNoiMa, MaCachTl
)
SELECT 
     ctct.Id as Id,
     iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS Mota,
     MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
    SoNguoi,
       DieuChinh,
     DDuToan, 
	 SoNgay
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = 2022

order by mlns.sXauNoiMa

/*
SELECT 
     --ctct.Id as Id,
     iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS Mota,
     MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       Sum(TongCong) AS TongCong,
       convert(decimal,Sum(SoNguoi)) as SoNguoi,
       Sum(DieuChinh) AS DieuChinh,
     Sum(DDuToan) As DDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN TL_QT_ChungTuChiTiet ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
AND ctct.Id_ChungTu in (SELECT *
   FROM f_split(@idChungTu))
AND ctct.MaCachTl in
  (SELECT *
   FROM f_split(@maCachTl))
WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam
GROUP BY 
     --ctct.Id,
     iID_MLNS,
         iID_MLNS_Cha,
         sXauNoiMa,
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
         sMoTa,
         sChiTietToi,
         mlns.bHangCha,
         iNamLamViec,
     MaCachTl
ORDER BY sXauNoiMa
*/


END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 17/11/2022 11:19:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_tl_get_data_ct_chi_tiet_export]
	@lstId nvarchar(max),
	@lstCach nvarchar(100)
AS
BEGIN
	CREATE TABLE #tmp(id nvarchar(100))
	DECLARE @isHaveCachTinhLuong bit = 0

	if(ISNULL(@lstCach, '') <> '')
	BEGIN
		INSERT INTO #tmp(id)
		SELECT *
		FROM f_split(@lstCach)

		SET @isHaveCachTinhLuong = 1
	END

	SELECT tbl.ID, Thang INTO #tblMaxThang
	FROM f_split(@lstId) as tmp
	INNER JOIN TL_QT_ChungTu as tbl on tmp.Item = tbl.ID;

	with ctct as (
	  select XauNoiMa,MaCachTl,  Sum(TongCong) AS TongCong,
		   Sum(DieuChinh) AS DieuChinh,
		 Sum(ISNULL(dt.DDuToan, 0)) As DDuToan
	  from TL_QT_ChungTu as tbl
	  INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
	  where  (ISNULL(@lstId, '') <> '' AND tbl.ID in (SELECT *  FROM f_split(@lstId)))
		AND ((dt.MaCachTl = '' AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	  group by dt.XauNoiMa, dt.MaCachTl
	),
	lstSoNguoi as (
		SELECT XauNoiMa,
			SoNguoi,SoNgay
		from TL_QT_ChungTu as tbl
		INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
		where tbl.ID in (SELECT TOP(1) ID FROM #tblMaxThang ORDER BY Thang DESC)
		AND ((@isHaveCachTinhLuong = 0 AND dt.MaCachTl = '') OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	)
SELECT 
     NEWID() as Id,
     iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS Mota,
     MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
    SoNguoi,
	SoNgay,
       DieuChinh,
     DDuToan
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
LEFT JOIN lstSoNguoi as sn on mlns.sXauNoiMa = sn.XauNoiMa

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = 2022

order by mlns.sXauNoiMa

DROP TABLE #tblMaxThang
DROP TABLE #tmp
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_phucap]    Script Date: 17/11/2022 11:19:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_get_data_phucap] @maCanBo AS nvarchar(50) AS BEGIN

CREATE TABLE #tmpExclude(code nvarchar(200))
INSERT INTO #tmpExclude(code) VALUES('THUONG_TT'),('GIAMTHUE_TT'),('THUNHAPKHAC_TT'),('THUEDANOP_TT'),('TIENCTLH_TT'),('TIENANDUONG_TT'),('TIENTAUXE_TT')

SELECT PhuCap.Id AS Id,
       PhuCap.Ma_PhuCap AS MaPhuCap,
       PhuCap.Parent AS Parent,
	   PhuCap.bGiaTri as BGiaTri,
	   PhuCap.bHuongPc_Sn as BHuongPcSn,
	   PhuCap.Ten_PhuCap AS TenPhuCap,
       CONCAT(PhuCapCha.Ma_PhuCap, '-', PhuCapCha.Ten_PhuCap) AS ParentName,
       CanboPhucap.DateStart AS DateStart,
       CanboPhucap.ISoThang_Huong AS ISoThangHuong,
       CanboPhucap.HuongPC_SN AS HuongPCSN,
     (case
     when CanboPhucap.GIA_TRI is null then PhuCap.Gia_Tri
     else CanboPhucap.GIA_TRI
     end) as GiaTri,
	 PhuCap.FGiaTriNhoNhat,
	 PhuCap.FGiaTriLonNhat,
	 PhuCap.fGiaTriPhuCap_KemTheo as FGiaTriPhuCapKemTheo,
	 PhuCap.iId_PhuCap_KemTheo as IIdPhuCapKemTheo,
	 PhuCap.iId_Ma_PhuCap_KemTheo as IIdMaPhuCapKemTheo
FROM TL_DM_PhuCap PhuCap
LEFT JOIN #tmpExclude as ec on PhuCap.Ma_PhuCap = ec.code
LEFT JOIN TL_DM_PhuCap PhuCapCha ON PhuCap.Parent = PhuCapCha.Ma_PhuCap
LEFT JOIN TL_CanBo_PhuCap AS CanboPhucap ON PhuCap.Ma_PhuCap = CanboPhucap.MA_PHUCAP
AND CanboPhucap.MA_CBO = @maCanBo
WHERE PhuCap.Chon = 1
  AND PhuCap.Is_Formula = 0
  AND PhuCap.Is_Readonly = 0
  AND PhuCap.Parent IN ( select Ma_PhuCap from TL_DM_PhuCap where Parent = '' and Chon = 1)
  AND  ec.code IS NULL
Order By ParentName
end
;
GO
