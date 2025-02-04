/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_2]    Script Date: 04/08/2022 5:27:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_2]    Script Date: 04/08/2022 5:27:57 PM ******/
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
GO
