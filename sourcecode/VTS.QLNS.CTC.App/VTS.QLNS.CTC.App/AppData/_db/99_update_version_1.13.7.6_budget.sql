/****** Object:  UserDefinedFunction [dbo].[f_dt_soquyetdinh_ngayquyetdinh]    Script Date: 04/01/2024 9:35:49 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dt_soquyetdinh_ngayquyetdinh]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dt_soquyetdinh_ngayquyetdinh]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dt_soquyetdinh_ngayquyetdinh]    Script Date: 04/01/2024 9:35:49 AM ******/
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
  AND iDuLieuNhan = 0
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
;
GO
