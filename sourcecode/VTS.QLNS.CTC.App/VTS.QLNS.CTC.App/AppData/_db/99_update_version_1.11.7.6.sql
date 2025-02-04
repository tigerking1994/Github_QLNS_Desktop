/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chitiet_kehoach]    Script Date: 24/08/2022 6:51:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chitiet_kehoach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chitiet_kehoach]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chitiet_kehoach]    Script Date: 24/08/2022 6:51:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_tl_get_data_ct_chitiet_kehoach]
@namNs int,
@nam int,
@idDonVi varchar(50)
AS
Begin
WITH 
ctct as 
(
	select  chungTuChiTiet.XauNoiMa,  SUM(chungTuChiTiet.TongNamTruoc) AS TongNamTruoc,
    SUM(chungTuChiTiet.TongCong) AS TongCong,
	SUM(chungTuChiTiet.DieuChinh) AS DieuChinh 
	from TL_QT_ChungTuChiTiet_KeHoach chungTuChiTiet 
	where chungTuChiTiet.Id_DonVi = @idDonVi and chungTuChiTiet.NamLamViec = @nam
	GROUP BY  chungTuChiTiet.XauNoiMa
   
),
SoLieuTongHop AS (
  SELECT
	mucLucNganSach.iID_MLNS as MlnsId,
	mucLucNganSach.iID_MLNS_Cha as MlnsIdParent,
	mucLucNganSach.sLNS,
	mucLucNganSach.sXauNoiMa as XauNoiMa,
	mucLucNganSach.sLns as Lns,
	mucLucNganSach.sMoTa as MoTa,
	mucLucNganSach.sL as L,
	mucLuCNganSach.sK as K,
	mucLucNganSach.sM as M,
	mucLucNganSach.sTM as TM,
	mucLucNganSach.sTTM as TTM,
	mucLucNganSach.sNG as NG,
	mucLucNganSach.sTNG as TNG,
	chungTuChiTiet.TongNamTruoc, 
    chungTuChiTiet.TongCong,
	chungTuChiTiet.DieuChinh,
	mucLucNganSach.BHangCha
FROM NS_MucLucNganSach mucLucNganSach
	LEFT JOIN ctct chungTuChiTiet ON mucLucNganSach.sXauNoiMa = chungTuChiTiet.XauNoiMa 
Where sLNS IN ('1010000', '1', '101')
	AND   mucLucNganSach.iNamLamViec = @namNs

)
SELECT
  chungTuChiTiet.Id as Id,
  chungTuChiTietTongHop.XauNoiMa as XauNoiMa,
  chungTuChiTietTongHop.Lns as Lns,
  chungTuChiTietTongHop.L as L,
  chungTuChiTietTongHop.K as K,
  chungTuChiTietTongHop.M as M,
  chungTuChiTietTongHop.TM as Tm,
  chungTuChiTietTongHop.TTM as Ttm,
  chungTuChiTietTongHop.NG as Ng,
  chungTuChiTietTongHop.TNG as Tng,
  chungTuChiTietTongHop.MoTa as MoTa,
  chungTuChiTietTongHop.TongNamTruoc as TongNamTruoc,
  chungTuChiTietTongHop.TongCong as TongCong,
  chungTuChiTietTongHop.DieuChinh as DieuChinh,
  chungTuChiTiet.GhiChu as GhiChu,
  chungTuChiTiet.Id_DonVi as IdDonVi,
  chungTuChiTiet.NamLamViec as NamLamViec,
  chungTuChiTietTongHop.BHangCha as BHangCha,
  chungTuChiTiet.TenDonVi as TenDonVi,
  chungTuChiTiet.Ngach as Ngach,
  chungTuChiTiet.MaPhuCap as MaPhuCap,
  chungTuChiTietTongHop.MlnsId,
  chungTuChiTietTongHop.MlnsIdParent,
  ChenhLech = null
FROM TL_QT_ChungTuChiTiet_KeHoach chungTuChiTiet
RIGHT JOIN SoLieuTongHop chungTuChiTietTongHop ON chungTuChiTiet.XauNoiMa = chungTuChiTietTongHop.XauNoiMa
And
  chungTuChiTiet.NamLamViec = @nam
  AND chungTuChiTiet.Thang IS NULL
  AND chungTuChiTiet.Id_DonVi = @idDonVi
ORDER BY chungTuChiTietTongHop.XauNoiMa
End
;
;
GO
