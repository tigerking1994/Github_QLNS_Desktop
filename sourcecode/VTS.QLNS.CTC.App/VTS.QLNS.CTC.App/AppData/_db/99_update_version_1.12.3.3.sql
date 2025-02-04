/****** Object:  StoredProcedure [dbo].[sp_ns_delete_chungtuchitiet_by_dotnhan]    Script Date: 21/11/2022 6:16:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_delete_chungtuchitiet_by_dotnhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_delete_chungtuchitiet_by_dotnhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_hienvat_1]    Script Date: 21/11/2022 6:16:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_hienvat_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_hienvat_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_1]    Script Date: 21/11/2022 6:16:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_thongke_soquyetdinh]    Script Date: 21/11/2022 6:16:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_thongke_soquyetdinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_thongke_soquyetdinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_dutoan_theodot]    Script Date: 21/11/2022 6:16:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_dutoan_theodot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_dutoan_theodot]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]    Script Date: 21/11/2022 6:16:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_dot_soquyetdinh_ngayquyetdinh_full]    Script Date: 21/11/2022 6:16:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dt_dot_soquyetdinh_ngayquyetdinh_full]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dt_dot_soquyetdinh_ngayquyetdinh_full]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_soquyetdinh_ngayquyetdinh_full]    Script Date: 21/11/2022 6:16:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dt_soquyetdinh_ngayquyetdinh_full]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dt_soquyetdinh_ngayquyetdinh_full]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_soquyetdinh_ngayquyetdinh]    Script Date: 21/11/2022 6:16:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dt_soquyetdinh_ngayquyetdinh]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dt_soquyetdinh_ngayquyetdinh]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_soquyetdinh_ngayquyetdinh]    Script Date: 21/11/2022 6:16:33 PM ******/
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
/****** Object:  UserDefinedFunction [dbo].[f_dt_soquyetdinh_ngayquyetdinh_full]    Script Date: 21/11/2022 6:16:33 PM ******/
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
/****** Object:  UserDefinedFunction [dbo].[f_dt_dot_soquyetdinh_ngayquyetdinh_full]    Script Date: 21/11/2022 6:16:33 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]    Script Date: 21/11/2022 6:16:33 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_dutoan_theodot]    Script Date: 21/11/2022 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dt_rpt_dutoan_theodot]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	--@VoucherDate datetime,
	@ChungTuId nvarchar(max),
	@dvt int
AS
BEGIN
	SELECT --isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       --ctct.iID_DTChungTu,
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
       sum(isnull(ctct.iPhanCap, 0)) AS iPhanCap,
       ctct.iID_MaDonVi,
       --sum(isnull(ctct.sGhiChu, '')) AS sGhiChu,
       sum(isnull(ctct.fHangMua, 0))/@dvt AS fHangMua,
       sum(isnull(ctct.fHangNhap, 0))/@dvt AS fHangNhap,
       sum(isnull(ctct.fDuPhong, 0))/@dvt AS fDuPhong,
       sum(isnull(ctct.fPhanCap, 0))/@dvt AS fPhanCap,
       sum(isnull(ctct.fTuChi, 0))/@dvt AS fTuChi,
       sum(isnull(ctct.fHienVat, 0))/@dvt AS fHienVat,
	   sum(isnull(ctct.fTonKho, 0))/@dvt AS fTonKho,
       --ctct.dNgayTao,
       --ctct.sNguoiTao,
       --ctct.dNgaySua,
       --ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   mlns.sChiTietToi,
	   dv.sTenDonVi,
	   mlns.bHangChaDuToan
	FROM NS_MucLucNganSach mlns
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
				 FROM dbo.f_split(@ChungTuId)) ) ) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	WHERE mlns.iNamLamViec = @YearOfWork
	  AND mlns.bHangChaDuToan IS NOT NULL
	  AND mlns.sLNS in
		(SELECT *
		 FROM dbo.f_split(@LNS))
	GROUP BY mlns.iID_MLNS,
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
       ctct.iID_MaDonVi,
       --ctct.dNgayTao,
       --ctct.sNguoiTao,
       --ctct.dNgaySua,
       --ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctct.iDuLieuNhan, 0),
	   mlns.sChiTietToi,
	   dv.sTenDonVi,
	   mlns.bHangChaDuToan
	ORDER BY mlns.sXauNoiMa;
END
/****** Object:  StoredProcedure [dbo].[sp_dt_phan_bo_du_toan_chi_tiet_used_selected_in_dialog]    Script Date: 17/12/2021 8:07:59 AM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_thongke_soquyetdinh]    Script Date: 21/11/2022 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dutoan_rpt_thongke_soquyetdinh]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@SoQuyetDinh nvarchar(100),
	@LNS nvarchar(max),
	@dvt int
AS
BEGIN

	-- lấy ra số liệu dự toán theo quyết định
	WITH TblSoLieuDuToan AS (SELECT 
		SUM(fTuChi) + SUM(fHienVat) + SUM(fDuPhong) + SUM(fHangNhap) + SUM(fHangMua) + SUM(fPhanCap) AS SoDuToan, 
		iID_MLNS 
	FROM 
		NS_DT_ChungTuChiTiet 
	WHERE 
		iID_DTChungTu IN 
		(
			SELECT iID_DTChungTu 
			FROM NS_DT_ChungTu 
			WHERE iNamLamViec = @YearOfWork AND iNamNganSach = @YearOfBudget AND iID_MaNguonNganSach = @BudgetSource AND sSoQuyetDinh = @SoQuyetDinh and iLoai = 0
		)
		AND iDuLieuNhan = 0
	GROUP BY iID_MLNS),

	-- lấy ra số phân bổ theo số quyết định
	TblPhanBo AS (SELECT 
		iID_CTDuToan_PhanBo AS IdPhanBo 
	FROM NS_DT_Nhan_PhanBo_Map 
	WHERE iID_CTDuToan_Nhan IN (
		SELECT 
			iID_DTChungTu 
		FROM 
			NS_DT_ChungTu 
		WHERE sSoQuyetDinh = @SoQuyetDinh
			AND	iNamLamViec = @YearOfWork
			AND iNamNganSach = @YearOfBudget
			AND iID_MaNguonNganSach = @BudgetSource
			AND iLoai = 0
	)),

	-- lấy ra số phân bổ điều chỉnh từ số phân bổ
	tempTblDieuChinh as (
		select map.* from NS_DT_Nhan_PhanBo_Map map
		inner join TblPhanBo pb
		on map.iID_CTDuToan_Nhan = pb.IdPhanBo
		union all
		select map.* from NS_DT_Nhan_PhanBo_Map map
		inner join tempTblDieuChinh
		on tempTblDieuChinh.iID_CTDuToan_PhanBo = map.iID_CTDuToan_Nhan
	),
	tblDieuChinh as (
		select iID_CTDuToan_PhanBo from tempTblDieuChinh
	),

	TblCtct AS (SELECT * 
	FROM
	(
	SELECT * 
	FROM NS_DT_ChungTuChiTiet 
	WHERE iID_DTChungTu IN (SELECT * FROM TblPhanBo) AND iID_CTDuToan_Nhan IN (SELECT iID_DTChungTu 
			FROM NS_DT_ChungTu 
			WHERE iNamLamViec = @YearOfWork AND iNamNganSach = @YearOfBudget AND iID_MaNguonNganSach = @BudgetSource AND sSoQuyetDinh = @SoQuyetDinh)

	UNION ALL
	SELECT * FROM NS_DT_ChungTuChiTiet WHERE iID_DTChungTu IN (SELECT * FROM TblDieuChinh)
	) ctct),
	
	-- lấy ra số liệu phân bổ
	TblSoLIeuPhanBo AS (SELECT 
		SUM(fTuChi) + SUM(fHienVat) + SUM(fDuPhong) + SUM(fHangNhap) + SUM(fHangMua) + SUM(fPhanCap) AS TongSoPhanBo, 
		iID_MLNS
	FROM 
		TblCtct 
	GROUP BY iID_MLNS),

	tblData as (
	SELECT 
		   case sSoQuyetDinh
		   when '' then convert(nvarchar(100), dt.dNgayChungTu, 103)
		   else dt.sSoQuyetDinh
		   end
		   as SoQuyetDinh,
		   ctct.iID_MLNS AS MlnsId,
		   iID_MLNS_Cha AS MlnsIdCha,
		   sLNS AS LNS,
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
           ctct.sMoTa AS MoTa ,
	       ctct.iID_MaDonVi as MaDonVi,
	       dv.sTenDonVi as TenDonVi,
		   ctct.bHangCha as BHangCha,
           SoPhanBo = sum(fTuChi)/@Dvt + sum(fHienVat)/@Dvt + sum(fDuPhong)/@Dvt + sum(fHangNhap)/@Dvt + sum(fHangMua)/@Dvt + sum(fPhanCap)/@Dvt,
		   case when SoDuToan is null then 0
		   else SoDuToan/@dvt
		   end as SoDuToan,
		   TongSoPhanBo/@dvt as TongSoPhanBo,
		   case when SoDuToan is null then 0 - TongSoPhanBo/@dvt
		   else SoDuToan/@dvt - TongSoPhanBo/@dvt
		   end as ConLai
	FROM TblCtct AS ctct
	INNER JOIN NS_DT_ChungTu dt
		ON dt.iID_DTChungTu = ctct.iID_DTChungTu
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork) dv
		ON ctct.iID_MaDonVi = dv.iID_MaDonVi
	LEFT JOIN
	 (select * from	TblSoLieuDuToan) AS tsldt
	ON tsldt.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		TblSoLIeuPhanBo AS tslpb
	ON tslpb.iID_MLNS = ctct.iID_MLNS
	GROUP BY 
		 dt.sSoQuyetDinh,
		 dt.dNgayChungTu,
		 ctct.iID_MLNS,
		 iID_MLNS_Cha,
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
         ctct.sMoTa,
		 ctct.iID_MaDonVi,
		 dv.sTenDonVi,
		 ctct.bHangCha,
		 SoDuToan,
		 TongSoPhanBo
	HAVING sum(fTuChi) <> 0
	OR sum(fHienVat) <> 0
	OR sum(fDuPhong) <> 0
	OR sum(fHangNhap) <> 0
	OR sum(fHangMua) <> 0
	OR sum(fPhanCap) <> 0
	),
	tblMlns as
	(
		select mlns.* from NS_MucLucNganSach mlns
		where iNamLamViec = @YearOfWork and iID_MLNS in (select distinct MlnsId from tblData where LNS in (select * from f_split_lns(@LNS)))
		union all
		select mlns.* from NS_MucLucNganSach mlns
		inner join tblMlns tbl
		on tbl.iID_MLNS_Cha = mlns.iID_MLNS
		and mlns.iNamLamViec = @YearOfWork
	),
	tblDataParent as (
		select 
			'' as SoQuyetDinh,
			iID_MLNS AS MlnsId,
			iID_MLNS_Cha as MlnsIdCha,
			sLNS as LNS,
			sL as L,
			sK as K,
			sM as M,
			sTM as TM,
			sTTM as TTM,
			sNG as NG,
			sTNG as TNG,
			sTNG1 as TNG1,
			sTNG2 as TNG2,
			sTNG3 as TNG3,
			sXauNoiMa as XauNoiMa,
			sMoTa as MoTa,
			'' as MaDonVi,
			'' as TenDonVi,
			bHangCha as BHangCha,
			0 as SoPhanBo,
			0 as SoDuToan,
			0 as TongSoPhanBo,
			0 as ConLai
		from 
			(select distinct * from tblMlns) mlns
	)
	select * from (select * from tblDataParent
	union all 
	select * from tblData
	) result
	order by XauNoiMa asc, MaDonVi asc

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_1]    Script Date: 21/11/2022 6:16:33 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_hienvat_1]    Script Date: 21/11/2022 6:16:33 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_ns_delete_chungtuchitiet_by_dotnhan]    Script Date: 21/11/2022 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_delete_chungtuchitiet_by_dotnhan]
@iID_DTChungTu AS uniqueidentifier ,
@iID_CTDuToan_Nhan AS nvarchar(max)

AS
BEGIN

DELETE FROM NS_DT_ChungTuChiTiet 
WHERE iID_DTChungTu = @iID_DTChungTu 
AND iID_CTDuToan_Nhan NOT IN (SELECT * FROM f_split(@iID_CTDuToan_Nhan))

END


select * from NS_DT_ChungTuChiTiet
GO
