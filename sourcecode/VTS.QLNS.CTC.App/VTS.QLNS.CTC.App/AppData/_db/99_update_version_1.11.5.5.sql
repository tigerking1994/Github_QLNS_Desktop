/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_so_nhu_cau_theo_nganh_phu_luc]    Script Date: 08/08/2022 9:21:49 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_so_nhu_cau_theo_nganh_phu_luc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_so_nhu_cau_theo_nganh_phu_luc]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_donvi]    Script Date: 08/08/2022 9:21:49 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_lns_rpt_thongtri_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_donvi]    Script Date: 08/08/2022 9:21:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_donvi] 
	@NamLamViec int,
    @CapPhatId nvarchar(100),
	@DonViId nvarchar(max),
	@Dvt int,
	@ILoaiNganSach int
AS
BEGIN
SET NOCOUNT ON;
	SELECT ctct.iID_MaDonVi AS MaDonVi,
		dv.sTenDonVi AS TenDonVi,
		SUM(fTuChi) / @Dvt AS CapPhat
	FROM NS_CP_ChungTuChiTiet ctct
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	INNER JOIN 
		(SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = 2022) ns 
	ON ctct.iID_MLNS = ns.iID_MLNS
	WHERE iID_CTCapPhat = @CapPhatId 
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@DonViId))
		AND @ILoaiNganSach = -1 OR ns.iLoaiNganSach = @ILoaiNganSach
	GROUP BY ctct.iID_MaDonVi, dv.sTenDonVi
	ORDER BY ctct.iID_MaDonVi
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_so_nhu_cau_theo_nganh_phu_luc]    Script Date: 08/08/2022 9:21:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_so_nhu_cau_theo_nganh_phu_luc]
	@Nganh varchar(max),
	@MaDonVi varchar(max),
	@IdChungTu varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@Loai int,
	@dvt int
AS
BEGIN
SELECT iID_MLSKT IIdMlskt,
       iID_MLSKTCha IIdMlsktCha,
	   dt_dv.id idDonVi,
	   dt_dv.sTenDonVi,
       sKyHieu,
	   sSTT STT,
	   bHangCha,
       sNG,
       sMoTa,
       sNG_Cha sNgCha,
       QuyetToan =SUM(IsNull(A.QuyetToan,0))/@dvt,
       DuToan =SUM(IsNull(A.DuToan,0))/@dvt,
       TuChi =SUM(IsNull(A.TuChi,0))/@dvt,
       PhanCap =SUM(IsNull(A.PhanCap,0))/@dvt,
       MuaHangHienVat =SUM(IsNull(A.MuaHangHienVat,0))/@dvt,
       DacThu =SUM(IsNull(A.PhanCap,0))/@dvt
FROM
  (SELECT ml.iID_MLSKT ,
                ml.iID_MLSKTCha,
                ml.sKyHieu,
                ml.sNG,
                ml.sMoTa,
                ml.sNG_Cha,
				ml.sSTT,
				ml.bHangCha,
                ct.iID_MaDonVi,
				sTenDonVi,
                QuyetToan =0,
                DuToan =0,
                IsNull(ct.fTuChi, 0) TuChi,
                IsNull(ct.fPhanCap, 0) PhanCap,
                IsNull(ct.fMuaHangCapHienVat, 0) MuaHangHienVat,
                IsNull(ct.fPhanCap, 0) DacThu
   FROM NS_SKT_ChungTuChiTiet ct
   RIGHT JOIN NS_SKT_MucLuc ml ON ct.iID_MLSKT = ml.iID_MLSKT
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND ct.iLoai= @Loai
     AND ct.iLoaiChungTu = 1
	 AND ct.iID_CTSoKiemTra in (select * from f_split(@IdChungTu))
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi
   FROM DonVi
   WHERE iTrangThai=1
     AND iNamLamViec=@NamLamViec AND iLoai=1) AS dt_dv ON A.iID_MaDonVi=dt_dv.id		--thêm iLoai = 1
GROUP BY iID_MLSKT,
         iID_MLSKTCha,
		 dt_dv.id,
	     dt_dv.sTenDonVi,
         sKyHieu,
		 sSTT,
		 bHangCha,
         sNG,
         sMoTa,
         sNG_Cha
		 order by sKyHieu
END
;
;
GO
