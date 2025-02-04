/****** Object:  StoredProcedure [dbo].[sp_bhxh_dt_export_dieuchinh_chi_tiet]    Script Date: 1/19/2024 11:17:29 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_dt_export_dieuchinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_dt_export_dieuchinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_phan_bo_du_toan_chi_tiet]    Script Date: 1/19/2024 11:17:29 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_phan_bo_du_toan_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_phan_bo_du_toan_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_phan_bo_du_toan_chi_tiet]    Script Date: 1/19/2024 11:17:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[sp_bh_export_phan_bo_du_toan_chi_tiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@NamLamViec int,
	@MaDonVi nvarchar(255)
As
Begin

select 
ngan_sach.iID_MLNS,
ngan_sach.iID_MLNS_Cha,
ngan_sach.sLNS,
ngan_sach.sL,
ngan_sach.sK,
ngan_sach.sM,
ngan_sach.sTM,
ngan_sach.sTTM,
ngan_sach.sNG,
ngan_sach.sTNG,
ngan_sach.sXauNoiMa,
ngan_sach.sMoTa as sNoiDung,
ngan_sach.sDuToanChiTietToi,
ngan_sach.bHangCha,
pb_ct.fTienTuChi,
pb_ct.fTienHienVat,
pb_ct.IID_MaDonVi
from
BH_DM_MucLucNganSach as ngan_sach
left join
(
	select sum(fTienTuChi) as fTienTuChi, sum(fTienHienVat) as fTienHienVat, IID_MaDonVi, iID_MucLucNganSach
	from BH_DTC_PhanBoDuToanChi_ChiTiet 
	where iID_DTC_PhanBoDuToanChi = @ChungTuId
	group by iID_MucLucNganSach, IID_MaDonVi) as pb_ct on pb_ct.iID_MucLucNganSach = ngan_sach.iID_MLNS
where ngan_sach.iNamLamViec  = @NamLamViec and ngan_sach.sLNS in (select * from f_split(@LNS))
and pb_ct.iID_MaDonVi=@MaDonVi
order by sXauNoiMa
End
;


GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_dt_export_dieuchinh_chi_tiet]    Script Date: 1/19/2024 11:17:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_bhxh_dt_export_dieuchinh_chi_tiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@NamLamViec int,
	@MaLoaiChi nvarchar(255),
	@MaDonVi nvarchar(255)
As
Begin

		SELECT Ml.*,
			Ml.sMoTa sNoiDung,
			Ml.bHangCha IsHangCha,
			CTCT.fTienDuToanDuocGiao FTienDuToanDuocGiao,
			CTCT.fTienSoSanhGiam FTienSoSanhGiam,
			CTCT.fTienSoSanhTang FTienSoSanhTang,
			CTCT.fTienUocThucHien06ThangCuoiNam FTienUocThucHien06ThangCuoiNam,
			CTCT.FTienThucHien06ThangDauNam FTienThucHien06ThangDauNam,
			CTCT.fTienUocThucHienCaNam  FTienUocThucHienCaNam
		FROM BH_DM_MucLucNganSach ML
		LEFT JOIN
			( SELECT * FROM BH_DTC_DieuChinhDuToanChi_ChiTiet 
					WHERE iID_BH_DTC IN
					(
					SELECT iID_BH_DTC FROM BH_DTC_DieuChinhDuToanChi
							WHERE iID_BH_DTC=@ChungTuId
							AND sMaLoaiChi=@MaLoaiChi
							AND iNamLamViec=@NamLamViec
							AND iID_MaDonVi=@MaDonVi
					)) CTCT ON ML.sXauNoiMa =CTCT.sXauNoiMa 

					WHERE ML.iNamLamViec=@NamLamViec
					AND ML.sLNS IN (SELECT * FROM splitstring(@LNS))
					--AND (ML.sDuToanChiTietToi IS NOT NULL or ML.sL is null)
					ORDER BY ML.sXauNoiMa

	
End
;
GO
