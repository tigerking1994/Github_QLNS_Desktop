/****** Object:  StoredProcedure [dbo].[sp_tn_qt_chungtu_chitietHD4554]    Script Date: 12/17/2024 11:15:44 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_qt_chungtu_chitietHD4554]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_qt_chungtu_chitietHD4554]
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_qt_chungtu_chitietHD4554]    Script Date: 12/17/2024 11:15:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_qt_chungtu_chitietHD4554]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int
AS
BEGIN
	SELECT
		isnull(ctct.Id, NEWID()) AS Id,
		ctct.iID_TN_QTChungTu as IIdTnQtChungTu,
		mlns.iID_MLNS as iID_MLNS, 
		mlns.iID_MLNS_Cha as iID_MLNS_Cha,
		isnull(ctct.sNguoiTao, '') as sNguoiTao,
		isnull(ctct.sNguoiSua, '') as sNguoiSua,
		ctct.fSoTien,
		ctct.fSoTien_DeNghi as fSoTienDeNghi,
		isnull(mlns.sMoTa, '') as sNoidung,
		isnull(ctct.iThangQuyLoai, 0) as IThangQuyLoai,
		isnull(ctct.iThangQuy, 1) as IThangQuy,
		mlns.bHangCha as bHangCha,
		isnull(ctct.iNamNganSach, @YearOfBudget) as iNamNganSach,
		isnull(ctct.iNguonNganSach, @BudgetSource) as iNguonNganSach,
		mlns.iNamLamViec as iNamLamViec,
		mlns.iTrangThai as ITrangThai,
		isnull(ctct.iID_MaDonVi, '') as IIdMaDonVi,
		isnull(ctct.sGhiChu, '') as sGhiChu,
		ctct.dNgayTao as dNgayTao,
		ctct.dNgaySua as dNgaySua,
		 mlns.sK as sK,
		 mlns.sLNS as sLNS,
		 mlns.sL as sL,
		 mlns.sM as sM,
		 mlns.sNG as sNG,
		 mlns.sTM as sTM,
		 mlns.sTNG as sTNG,
		 mlns.sTNG1 as sTNG1,
		 mlns.sTNG2 as sTNG2,
		 mlns.sTNG3 as sTNG3,
		 mlns.sTTM as sTTM,
		mlns.sXauNoiMa,
		mlns.sQuyetToanChiTietToi,
		mlns.sChiTietToi

	FROM  (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
	LEFT JOIN (
		SELECT
			*
		FROM
			TN_QuyetToan_ChungTuChiTiet_HD4554
		WHERE
			iNamLamViec = @YearOfWork
			AND iNamNganSach = @YearOfBudget
			AND iNguonNganSach = @BudgetSource
			AND iID_TN_QTChungTu = @ChungTuId
	) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
		--AND mlns.sLNS in (SELECT * FROM dbo.splitstring(@LNS))
	ORDER BY mlns.sXauNoiMa
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_hienvat_1]    Script Date: 12/17/2024 2:41:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_hienvat_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_hienvat_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_1]    Script Date: 12/17/2024 2:41:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_thongke_soquyetdinh]    Script Date: 12/17/2024 2:41:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_thongke_soquyetdinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_thongke_soquyetdinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_dutoan_theonganh]    Script Date: 12/17/2024 2:41:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_dutoan_theonganh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_dutoan_theonganh]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_dutoan_theodot]    Script Date: 12/17/2024 2:41:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_dutoan_theodot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_dutoan_theodot]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_dutoan_theodot]    Script Date: 12/17/2024 2:41:09 PM ******/
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
       round(sum(isnull(ctct.iPhanCap, 0)),0) AS iPhanCap,
       ctct.iID_MaDonVi,
       --sum(isnull(ctct.sGhiChu, '')) AS sGhiChu,
       round(sum(isnull(ctct.fHangMua, 0))/@dvt,0) AS fHangMua,
       round(sum(isnull(ctct.fHangNhap, 0))/@dvt,0) AS fHangNhap,
       round(sum(isnull(ctct.fDuPhong, 0))/@dvt,0) AS fDuPhong,
       round(sum(isnull(ctct.fPhanCap, 0))/@dvt,0) AS fPhanCap,
       round(sum(isnull(ctct.fTuChi, 0))/@dvt,0) AS fTuChi,
       round(sum(isnull(ctct.fHienVat, 0))/@dvt,0) AS fHienVat,
	   round(sum(isnull(ctct.fTonKho, 0))/@dvt,0) AS fTonKho,
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
	 -- AND mlns.sLNS in
		--(SELECT *
		-- FROM dbo.f_split(@LNS))
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_dutoan_theonganh]    Script Date: 12/17/2024 2:41:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_dutoan_theonganh]
  @LNS nvarchar(max),
  @YearOfWork int,
  @YearOfBudget int,
  @BudgetSource int,
  --@VoucherDate datetime,
  @ChungTuId nvarchar(max),
  @IdNganh nvarchar(max)
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
       round(sum(isnull(ctct.iPhanCap, 0)),0) AS iPhanCap,
       ctct.iID_MaDonVi,
       --isnull(ctct.sGhiChu, '') AS sGhiChu,
       round(sum(isnull(ctct.fHangMua, 0)),0) AS fHangMua,
       round(sum(isnull(ctct.fHangNhap, 0)),0) AS fHangNhap,
       round(sum(isnull(ctct.fDuPhong, 0)),0) AS fDuPhong,
       round(sum(isnull(ctct.fPhanCap, 0)),0) AS fPhanCap,
       round(sum(isnull(ctct.fTuChi, 0)),0) AS fTuChi,
       round(sum(isnull(ctct.fHienVat, 0)),0) AS fHienVat,
       round(sum(isnull(ctct.fTonKho, 0)),0) AS fTonKho,
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
         FROM dbo.splitstring(@ChungTuId)) )
     AND (sNG IN
        (SELECT *
         FROM dbo.splitstring(@IdNganh))) ) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
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
  ORDER BY mlns.sLNS,
       mlns.sL,
       mlns.sK,
       mlns.sM,
       mlns.sTM,
       mlns.sTTM,
       mlns.sNG,
       mlns.sTNG,
	   mlns.sTNG1,
	   mlns.sTNG2,
	   mlns.sTNG3;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_thongke_soquyetdinh]    Script Date: 12/17/2024 2:41:09 PM ******/
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
           SoPhanBo = round(sum(fTuChi)/@Dvt + sum(fHienVat)/@Dvt + sum(fDuPhong)/@Dvt + sum(fHangNhap)/@Dvt + sum(fHangMua)/@Dvt + sum(fPhanCap)/@Dvt,0),
		   round(case when SoDuToan is null then 0
		   else SoDuToan/@dvt
		   end,0) as SoDuToan,
		   round(TongSoPhanBo/@dvt,0) as TongSoPhanBo,
		   round(case when SoDuToan is null then 0 - TongSoPhanBo/@dvt
		   else SoDuToan/@dvt - TongSoPhanBo/@dvt
		   end,0) as ConLai
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_1]    Script Date: 12/17/2024 2:41:09 PM ******/
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
		TuChi	=round(sum(TuChi),0),
		HienVat	=round(sum(HienVat),0)
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_tong_hop_so_phan_bo_hienvat_1]    Script Date: 12/17/2024 2:41:09 PM ******/
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
		TuChi	=round(sum(TuChi),0),
		HienVat	=round(sum(HienVat),0)
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
;
GO
