/****** Object:  StoredProcedure [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc_tong_hop]    Script Date: 05/11/2022 3:47:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc]    Script Date: 05/11/2022 3:47:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc]    Script Date: 05/11/2022 3:47:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc]
	@Nganh varchar(max),
	@MaDonVi varchar(max),
	@IdChungTu varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@Loai int,
	@dvt int,
	@bTongHop bit
AS
BEGIN
SELECT iID_MLNS as iID_MLNS,
       iID_MLNS_Cha as iID_MLNS_Cha,
	   dt_dv.id idDonVi,
	   dt_dv.sTenDonVi,
	   bHangCha,
       sNG,
       ConCat(' ', ' - ',sMoTa) as sMoTa,
	   sXauNoiMa,
       QuyetToan =SUM(IsNull(A.QuyetToan,0))/@dvt,
       DuToan =SUM(IsNull(A.DuToan,0))/@dvt,
       TuChi =SUM(IsNull(A.TuChi,0))/@dvt,
       PhanCap =SUM(IsNull(A.PhanCap,0))/@dvt,
       DacThu =SUM(IsNull(A.PhanCap,0))/@dvt
FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sNG,
                ml.sMoTa,
				ml.sXauNoiMa,
				ml.bHangCha,
                ct.iID_MaDonVi,
				sTenDonVi,
                QuyetToan =0,
                DuToan =0,
                IsNull(ct.fTuChi, 0) TuChi,
                IsNull(ct.fPhanCap, 0) PhanCap,
                --IsNull(ct.fMuaHangCapHienVat, 0) MuaHangHienVat,
                IsNull(ct.fPhanCap, 0) DacThu
   FROM NS_DTDauNam_ChungTuChiTiet ct
   Inner JOIN NS_DTDauNam_ChungTu chungtu ON ct.iID_CTDTDauNam = chungtu.iID_CTDTDauNam
   RIGHT JOIN NS_MucLucNganSach ml ON ct.sXauNoiMa = ml.sXauNoiMa
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND ct.iLoaiChungTu = 1
	 AND ((@bTongHop = 0 and chungtu.sDSSoChungTuTongHop is  null)  or (@bTongHop = 1 AND chungtu.bDaTongHop = @bTongHop ))
	 AND (@IdChungTu is null or ct.iID_CTDTDauNam in (select * from f_split(@IdChungTu)))
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi
   FROM DonVi
   WHERE iTrangThai=1
     AND iNamLamViec=@NamLamViec AND iLoai=1) AS dt_dv ON A.iID_MaDonVi=dt_dv.id		--thêm iLoai = 1
GROUP BY iID_MLNS,
         iID_MLNS_Cha,
		 dt_dv.id,
	     dt_dv.sTenDonVi,
		 bHangCha,
         sNG,
         sMoTa,
		 sXauNoiMa
		 order by sNG
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc_tong_hop]    Script Date: 05/11/2022 3:47:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc_tong_hop]
	@Nganh varchar(max),
	@MaDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@Loai int,
	@dvt int,
	@bTongHop bit
AS
BEGIN
SELECT iID_MLNS as  iID_MLNS,
       iID_MLNS_Cha as iID_MLNS_Cha,
	   dt_dv.id idDonVi,
	   dt_dv.sTenDonVi,
	   dt_dv.iLoai,
	   bHangCha,
       sNG,
       sMoTa,
       QuyetToan =SUM(IsNull(A.QuyetToan,0))/@dvt,
       DuToan =SUM(IsNull(A.DuToan,0))/@dvt,
       TuChi =SUM(IsNull(A.TuChi,0))/@dvt,
       PhanCap =SUM(IsNull(A.PhanCap,0))/@dvt,
       DacThu =SUM(IsNull(A.PhanCap,0))/@dvt
FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sNG,
                ml.sMoTa,
				ml.bHangCha,
                ct.iID_MaDonVi,
				sTenDonVi,
                QuyetToan =0,
                DuToan =0,
                IsNull(ct.fTuChi, 0) TuChi,
                IsNull(ct.fPhanCap, 0) PhanCap,
                IsNull(ct.fPhanCap, 0) DacThu
   FROM NS_DTDauNam_ChungTuChiTiet ct
   LEFT JOIN NS_DTDauNam_ChungTu chungtu ON ct.iID_CTDTDauNam = chungtu.iID_CTDTDauNam
   RIGHT JOIN NS_MucLucNganSach ml ON ct.sXauNoiMa = ml.sXauNoiMa
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND ct.iLoaiChungTu = 1
	 AND ((@bTongHop = 0 and chungtu.bDaTongHop is not null)  or (@bTongHop = 1 AND chungtu.bDaTongHop = @bTongHop))
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai=1
     AND iNamLamViec=@NamLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
	WHERE dt_dv.iLoai != 0
GROUP BY iID_MLNS,
         iID_MLNS_Cha,
		 dt_dv.id,
	     dt_dv.sTenDonVi,
		 bHangCha,
         sNG,
         sMoTa,
		 iLoai
		 order by iID_MLNS
END
;
;
;
;
GO
