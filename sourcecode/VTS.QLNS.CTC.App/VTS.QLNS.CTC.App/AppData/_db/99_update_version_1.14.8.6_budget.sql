/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_3]    Script Date: 10/2/2024 3:54:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_index_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_index_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_2]    Script Date: 10/2/2024 3:54:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index]    Script Date: 10/2/2024 3:54:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_index_2]    Script Date: 10/2/2024 3:54:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_donvi0_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_donvi0_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]    Script Date: 10/2/2024 3:54:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_dutoandaunam_report_chingansach]    Script Date: 10/2/2024 3:54:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_dutoandaunam_report_chingansach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_dutoandaunam_report_chingansach]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_dutoandaunam_report_chingansach]    Script Date: 10/2/2024 3:54:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_dutoandaunam_report_chingansach]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT 
				NEWID() AS Id,
                NEWID() AS IdDb,
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
                isnull(chitiet.bHangCha, mlns.bHangCha) as bHangCha,
                mlns.sChiTietToi AS ChiTietToi,
                '' AS IdDonVi,
                '' AS TenDonVi,
                isnull(chitiet.fTuChi,0) AS ChiTiet,
                isnull(chitiet.fUocThucHien,0) AS UocThucHien,
                isnull(chitiet.fHangNhap,0) AS HangNhap,
                isnull(chitiet.fHangMua,0) AS HangMua,
                isnull(chitiet.fPhanCap,0) AS PhanCap,
                isnull(chitiet.fChuaPhanCap,0) AS ChuaPhanCap,
                isnull(chitiet.fMucTienPhanBo,0) AS MucTienPhanBo,
                map.sSKT_KyHieu AS SKT_KyHieu,
				mlns.bHangChaDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN
  (SELECT SUM(fTuChi) as fTuChi,
			SUM(fUocThucHien) as fUocThucHien,
			SUM(fHangNhap) as fHangNhap,
			SUM(fHangMua) as fHangMua,
			SUM(fPhanCap) as fPhanCap,
			SUM(fChuaPhanCap) as fChuaPhanCap,
			SUM(fMucTienPhanBo) as fMucTienPhanBo,
			bHangCha,
			--iID_MaDonVi,
			--sTenDonVi,
			sXauNoiMa
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec = @YearOfWork
     AND (iLoai = 3)
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach =@BudgetSource
     AND (iID_MaDonVi in  (SELECT *  FROM f_split(@AgencyId))
      --     AND bKhoa = 0
		 )
     AND iLoaiChungTu = @LoaiChungTu
	 group by 
	 sXauNoiMa, bHangCha
	 ) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa
LEFT JOIN
  (SELECT *
   FROM NS_MLSKT_MLNS
   WHERE iNamLamViec = @YearOfWork )map ON mlns.sXauNoiMa = map.sNS_XauNoiMa
WHERE mlns.iNamLamViec = @YearOfWork
  AND mlns.iTrangThai = 1 
  AND mlns.bHangChaDuToan IS NOT NULL
 -- and chitiet.fTuChi <> 0
 and(mlns.sLNS = '1'
     OR ((mlns.sLNS like '104%'
          AND @LoaiChungTu = '2')
         OR (mlns.sLNS not like '104%'
             AND @LoaiChungTu = '1')))
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]    Script Date: 10/2/2024 3:54:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_donvi0_chitiet_index_2]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai int,
	@TypeGet int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50),
	@ListChungTuTongHop nvarchar(max),
	@Lns nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	select distinct iID_MaDonVi into #listDonVi from NS_DTDauNam_ChungTu where iID_CTDTDauNam IN (select * FROM f_split(@ListChungTuTongHop))
	SELECT DISTINCT NEWID() AS Id,
                NEWID() AS IdDb,
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
                mlns.sChiTietToi AS ChiTietToi,
                --chitiet.iID_MaDonVi AS IdDonVi,
                mlns.iID_MaDonVi AS IdDonVi,
                donvi.sTenDonVi AS TenDonVi,
                chitiet.TuChi AS ChiTiet,
				chitiet.UocThucHien,
                chitiet.HangNhap,
                chitiet.HangMua,
                chitiet.PhanCap,
                chitiet.ChuaPhanCap,
				chitiet.MucTienPhanBo,
                map.sSKT_KyHieu AS SKT_KyHieu,
				mlns.bHangChaDuToan
FROM (select mlns1.iID_MLNS,
		     mlns1.iID_MLNS_Cha,
			 mlns1.sXauNoiMa,
             mlns1.sLNS,
             mlns1.sL,
             mlns1.sK,
             mlns1.sM,
             mlns1.sTM,
             mlns1.sTTM,
             mlns1.sNG,
             mlns1.sTNG,
             mlns1.sTNG1,
             mlns1.sTNG2,
             mlns1.sTNG3,
             mlns1.sMoTa,
             mlns1.bHangCha,
             mlns1.sChiTietToi,
             mlns1.bHangChaDuToan,
             mlns1.iNamLamViec,
			 mlns1.iID_MaDonVi
			 from NS_MucLucNganSach mlns1 where bHangChaDuToan = 1 and iNamLamViec = @YearOfWork
			 union all
	select	 mlns2.iID_MLNS,
		     mlns2.iID_MLNS_Cha,
			 mlns2.sXauNoiMa,
             mlns2.sLNS,
             mlns2.sL,
             mlns2.sK,
             mlns2.sM,
             mlns2.sTM,
             mlns2.sTTM,
             mlns2.sNG,
             mlns2.sTNG,
             mlns2.sTNG1,
             mlns2.sTNG2,
             mlns2.sTNG3,
             mlns2.sMoTa,
             mlns2.bHangCha,
             mlns2.sChiTietToi,
             mlns2.bHangChaDuToan,
             mlns2.iNamLamViec,
			 #listDonVi.iID_MaDonVi
			 from NS_MucLucNganSach mlns2, #listDonVi where bHangChaDuToan = 0 and iNamLamViec = @YearOfWork) mlns 
LEFT JOIN
  (SELECT sXauNoiMa,
          fDuPhong AS DuPhong,
		  fUocThucHien AS UocThucHien,
          fTuChi AS TuChi,
          fHangNhap AS HangNhap,
          fHangMua AS HangMua,
          fPhanCap AS PhanCap,
          fChuaPhanCap AS ChuaPhanCap,
          fMucTienPhanBo AS MucTienPhanBo,
		  iID_MaDonVi
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec = @YearOfWork
     AND (iLoai = 3)
     AND (
			(@Loai = 0 AND iID_MaDonVi = @AgencyId)
            OR (@Loai = 1 AND iID_MaDonVi <> @AgencyId AND @ListChungTuTongHop <> '' AND iID_CTDTDauNam IN (select * FROM f_split(@ListChungTuTongHop)))
		 )
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach = @BudgetSource
     AND iLoaiChungTu = @LoaiChungTu
     ) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa AND mlns.iID_MaDonVi = chitiet.iID_MaDonVi
LEFT JOIN (SELECT * from DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) donvi on donvi.iID_MaDonVi = mlns.iID_MaDonVi

LEFT JOIN 
( select * FROM  NS_MLSKT_MLNS   where iNamLamViec = @YearOfWork
) MAP ON mlns.sXauNoiMa = map.sNS_XauNoiMa  

WHERE mlns.iNamLamViec = @YearOfWork
  --AND (map.iNamLamViec = @YearOfWork
  --     OR mlns.bHangCha =1) 
  AND mlns.bHangChaDuToan IS NOT NULL
	   and(mlns.sLNS = '1'
            OR ((mlns.sLNS like '104%'
                    AND @LoaiChungTu = '2')
                OR (mlns.sLNS not like '104%'
                    AND @LoaiChungTu = '1')))
					AND mlns.sLNS IN (SELECT * from f_split(@Lns))

ORDER BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG
		 END;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_index_2]    Script Date: 10/2/2024 3:54:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_donvi0_index_2]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai int,
	@TypeGet int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50),
	@ListIdChungTu nvarchar(max),
	@Lns nvarchar(max)
AS
BEGIN

	DECLARE @TenDonVi nvarchar(max);
	SELECT 
		@TenDonVi = (SELECT sTenDonVi FROM DonVi WHERE iID_MaDonVi = @AgencyId AND iNamLamViec = @YearOfWork AND iTrangThai = 1)

	SET NOCOUNT ON;
	SELECT DISTINCT isnull(chitiet.iID_CTDTDauNamChiTiet, NEWID()) AS Id,
                chitiet.iID_CTDTDauNamChiTiet AS IdDb,
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
                mlns.sChiTietToi AS ChiTietToi,
                @AgencyId AS IdDonVi,
                @TenDonVi AS TenDonVi,
                chitiet.TuChi AS ChiTiet,
				chitiet.UocThucHien,
                chitiet.HangNhap,
                chitiet.HangMua,
                chitiet.PhanCap,
                chitiet.ChuaPhanCap,
				chitiet.MucTienPhanBo,
                map.sSKT_KyHieu AS SKT_KyHieu,
				mlns.bHangChaDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN
  (SELECT 
		  iID_CTDTDauNamChiTiet,
		  sXauNoiMa,
          SUM(fDuPhong) AS DuPhong,
		  SUM(fUocThucHien) AS UocThucHien,
          SUM(fTuChi) AS TuChi,
          SUM(fHangNhap) AS HangNhap,
          SUM(fHangMua) AS HangMua,
          SUM(fPhanCap) AS PhanCap,
          SUM(fChuaPhanCap) AS ChuaPhanCap,
          SUM(fMucTienPhanBo) AS MucTienPhanBo
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec = @YearOfWork
     AND (iLoai = 3)
     AND ((@Loai = 0
           AND iID_MaDonVi = @AgencyId AND @ListIdChungTu <> '' AND iID_CTDTDauNam IN (SELECT * from f_split(@ListIdChungTu)))
          OR (@Loai = 1
              AND iID_MaDonVi <> @AgencyId AND @ListIdChungTu <> '' AND iID_CTDTDauNam IN (SELECT * from f_split(@ListIdChungTu))))
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach = @BudgetSource
     AND iLoaiChungTu = @LoaiChungTu
   GROUP BY sXauNoiMa, iID_CTDTDauNamChiTiet) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa
LEFT JOIN 
( select * FROM  NS_MLSKT_MLNS   where iNamLamViec = @YearOfWork
) MAP ON mlns.sXauNoiMa = map.sNS_XauNoiMa  

WHERE mlns.iNamLamViec = @YearOfWork
  --AND (map.iNamLamViec = @YearOfWork
  --     OR mlns.bHangCha =1) 
    AND mlns.bHangChaDuToan IS NOT NULL
	   and(mlns.sLNS = '1'
            OR ((mlns.sLNS like '104%'
                    AND @LoaiChungTu = '2')
                OR (mlns.sLNS not like '104%'
                    AND @LoaiChungTu = '1')))
					AND mlns.sLNS IN (SELECT * from f_split(@Lns))
ORDER BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG
		 END;
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_LNS]    Script Date: 1/25/2022 8:16:57 AM ******/
SET ANSI_NULLS ON
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index]    Script Date: 10/2/2024 3:54:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_index]  
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai int,
	@TypeGet int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT isnull(chitiet.iID_CTDTDauNamChiTiet, NEWID()) AS Id,
                chitiet.iID_CTDTDauNamChiTiet AS IdDb,
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
                mlns.sChiTietToi AS ChiTietToi,
                chitiet.iID_MaDonVi AS IdDonVi,
                chitiet.sTenDonVi AS TenDonVi,
                chitiet.fTuChi AS ChiTiet,
                chitiet.fDuPhong AS DuPhong,
                chitiet.fHangNhap AS HangNhap,
                chitiet.fHangMua AS HangMua,
                chitiet.fPhanCap AS PhanCap,
                chitiet.fChuaPhanCap AS ChuaPhanCap,
                chitiet.fMucTienPhanBo AS MucTienPhanBo,
                map.sSKT_KyHieu AS SKT_KyHieu,
				mlns.bHangChaDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN
  (SELECT *
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec = @YearOfWork
     AND (iLoai = 3)
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach =@BudgetSource
     AND (iID_MaDonVi = @AgencyId
          OR (@AgencyId = '00'
              AND bKhoa = 0))
     AND iLoaiChungTu = @LoaiChungTu ) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa
LEFT JOIN
  (SELECT *
   FROM NS_MLSKT_MLNS
   WHERE iNamLamViec = @YearOfWork )map ON mlns.sXauNoiMa = map.sNS_XauNoiMa
WHERE mlns.iNamLamViec = @YearOfWork
  AND mlns.iTrangThai = 1 --and (map.NamLamViec = @YearOfWork or mlns.bHangCha =1)
  AND mlns.bHangChaDuToan IS NOT NULL
 and(mlns.sLNS = '1'
     OR ((mlns.sLNS like '104%'
          AND @LoaiChungTu = '2')
         OR (mlns.sLNS not like '104%'
             AND @LoaiChungTu = '1')))
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
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_2]    Script Date: 10/2/2024 3:54:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_index_2]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai int,
	@TypeGet int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50),
	@Lns nvarchar(max),
	@VoucherId nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select *  into #lnsTem  from f_split(@Lns);
	SELECT DISTINCT isnull(chitiet.iID_CTDTDauNamChiTiet, NEWID()) AS Id,
                chitiet.iID_CTDTDauNamChiTiet AS IdDb,
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
                mlns.sChiTietToi AS ChiTietToi,
                chitiet.iID_MaDonVi AS IdDonVi,
                chitiet.sTenDonVi AS TenDonVi,
                chitiet.fTuChi AS ChiTiet,
				chitiet.fUocThucHien AS UocThucHien,
                chitiet.fHangNhap AS HangNhap,
                chitiet.fHangMua AS HangMua,
                chitiet.fPhanCap AS PhanCap,
                chitiet.fChuaPhanCap AS ChuaPhanCap,
				chitiet.fMucTienPhanBo AS MucTienPhanBo,
                map.sSKT_KyHieu AS SKT_KyHieu,
				mlns.bHangChaDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN
  (SELECT *
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec = @YearOfWork
     AND (iLoai = 3)
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach =@BudgetSource
     AND (iID_MaDonVi = @AgencyId
          --OR (@AgencyId = '00'
          --    AND bKhoa = 0))
		OR (EXISTS (SELECT * FROM DonVi WHERE iLoai = '0' AND iID_MaDonVi = @AgencyId) AND bKhoa = 0))
	 AND iID_CTDTDauNam = @VoucherId
     AND iLoaiChungTu = @LoaiChungTu ) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa
LEFT JOIN
  (SELECT *
   FROM NS_MLSKT_MLNS
   WHERE iNamLamViec = @YearOfWork ) map ON mlns.sXauNoiMa = map.sNS_XauNoiMa
--inner join  lnsTem ON  mlns.sLNS  = LNSTEM.Item 
WHERE mlns.iNamLamViec = @YearOfWork
  AND mlns.iTrangThai = 1  
  AND mlns.bHangChaDuToan IS NOT NULL
 AND (mlns.sLNS = '1'
     OR ((mlns.sLNS like '104%'
          AND @LoaiChungTu = '2')
         OR (mlns.sLNS not like '104%'
             AND @LoaiChungTu = '1')))
--AND mlns.sLNS IN (SELECT * from f_split(@Lns))
AND mlns.sLNS IN (select * from #lnsTem)
--AND EXISTS (SELECT *  AS sLNS from f_split(@Lns) where Item = mlns.sLNS)
ORDER BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG;

drop table #lnsTem;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_3]    Script Date: 10/2/2024 3:54:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_index_3]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Loai int,
	@TypeGet int,
	@AgencyId nvarchar(500),
	@LoaiChungTu nvarchar(50),
	@Lns nvarchar(max),
	@VoucherId nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select *  into #lnsTem  from f_split(@Lns);
	SELECT DISTINCT isnull(chitiet.iID_CTDTDauNamChiTiet, NEWID()) AS Id,
                chitiet.iID_CTDTDauNamChiTiet AS IdDb,
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
                mlns.sChiTietToi AS ChiTietToi,
                chitiet.iID_MaDonVi AS IdDonVi,
                chitiet.sTenDonVi AS TenDonVi,
                chitiet.fTuChi AS ChiTiet,
				chitiet.fUocThucHien AS UocThucHien,
                chitiet.fHangNhap AS HangNhap,
                chitiet.fHangMua AS HangMua,
                ISNULL(chitiet.fPhanCap,0) AS PhanCap,
				case when chitiet_phancap.fTuChi is null then ISNULL(chitiet.fPhanCap,0) else ISNULL(chitiet.fPhanCap,0) - ISNULL(chitiet_phancap.fTuChi,0) end as PhanCapConLai,
				ISNull(chitiet_phancap.fTuChi,0) as TuChi,
                chitiet.fChuaPhanCap AS ChuaPhanCap,
				chitiet.fMucTienPhanBo AS MucTienPhanBo,
                map.sSKT_KyHieu AS SKT_KyHieu,
				mlns.bHangChaDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN
  (SELECT *
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec = @YearOfWork
     AND (iLoai = 3)
     AND iNamNganSach = @YearOfBudget
     AND iID_MaNguonNganSach =@BudgetSource
     AND (iID_MaDonVi = @AgencyId
          --OR (@AgencyId = '00'
          --    AND bKhoa = 0))
		  OR (EXISTS (SELECT * FROM DonVi WHERE iLoai = '0' AND iID_MaDonVi = @AgencyId) AND bKhoa = 0))
	 AND iID_CTDTDauNam = @VoucherId
     AND iLoaiChungTu = @LoaiChungTu ) chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa
LEFT JOIN
  (SELECT *
   FROM NS_MLSKT_MLNS
   WHERE iNamLamViec = @YearOfWork ) map ON mlns.sXauNoiMa = map.sNS_XauNoiMa

LEFT JOIN
( select IsNULL(Sum(fTuChi),0) as fTuChi, iID_CTDTDauNamChiTiet
		from  NS_DTDauNam_PhanCap
		group by iID_CTDTDauNamChiTiet
		) as chitiet_phancap ON chitiet_phancap.iID_CTDTDauNamChiTiet = chitiet.iID_CTDTDauNamChiTiet
--inner join  lnsTem ON  mlns.sLNS  = LNSTEM.Item 
WHERE mlns.iNamLamViec = @YearOfWork
  AND mlns.iTrangThai = 1  
  AND mlns.bHangChaDuToan IS NOT NULL
 AND (mlns.sLNS = '1'
     OR ((mlns.sLNS like '104%'
          AND @LoaiChungTu = '2')
         OR (mlns.sLNS not like '104%'
             AND @LoaiChungTu = '1')))
--AND mlns.sLNS IN (SELECT * from f_split(@Lns))
AND mlns.sLNS IN (select * from #lnsTem)
--AND EXISTS (SELECT *  AS sLNS from f_split(@Lns) where Item = mlns.sLNS)
ORDER BY mlns.sLNS,
         mlns.sL,
         mlns.sK,
         mlns.sM,
         mlns.sTM,
         mlns.sTTM,
         mlns.sNG,
         mlns.sTNG;

drop table #lnsTem;

END
;
;
;
;
;
;
;
GO
