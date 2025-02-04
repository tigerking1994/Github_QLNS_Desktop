/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_index_2]    Script Date: 03/11/2023 4:18:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_donvi0_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_donvi0_index_2]
GO

/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_donvi0_index_2]    Script Date: 03/11/2023 4:18:49 PM ******/
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
          SUM(fChuaPhanCap) AS ChuaPhanCap
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
GO

delete x from NS_MLSKT_MLNS x
where x.sSKT_KyHieu not in 
(select skyhieu from ns_skt_mucluc where inamlamviec = x.inamlamviec)
go

