/****** Object:  StoredProcedure [dbo].[sp_get_mlns_bhxh_by_year_of_work]    Script Date: 1/26/2024 4:44:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_mlns_bhxh_by_year_of_work]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_mlns_bhxh_by_year_of_work]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_muclucngansach_chitiet]    Script Date: 1/26/2024 4:44:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_muclucngansach_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_muclucngansach_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_dt_export_dieuchinh_chi_tiet]    Script Date: 1/26/2024 4:44:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_dt_export_dieuchinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_dt_export_dieuchinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_dt_export_dieuchinh_chi_tiet]    Script Date: 1/26/2024 4:44:27 PM ******/
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
			Ml.bHangChaDuToanDieuChinh IsHangCha,
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
		AND ML.bHangChaDuToanDieuChinh IS NOT NULL
		--AND (ML.sDuToanChiTietToi IS NOT NULL or ML.sL is null)
		ORDER BY ML.sXauNoiMa

	
End
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_muclucngansach_chitiet]    Script Date: 1/26/2024 4:44:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_muclucngansach_chitiet]
	 @namLamViec int,
	 @SLNS nvarchar(MAX)
AS
BEGIN 
	SET NOCOUNT ON;
	SELECT *
	FROM BH_DM_MucLucNganSach
	WHERE sLNS in( SELECT * FROM splitstring(@SLNS))
	  AND iNamLamViec = @NamLamViec
	  AND bHangChaDuToanDieuChinh is not null
	 ORDER BY sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_get_mlns_bhxh_by_year_of_work]    Script Date: 1/26/2024 4:44:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_get_mlns_bhxh_by_year_of_work]
	@NamLamViec int
AS
BEGIN
	with lns as
		(
			select * from BH_DM_MucLucNganSach where iNamLamViec = @NamLamViec
			and sL = '' and sK = '' and sM = '' and sTM = '' and sTTM = '' and sNG = '' and sTNG = ''
		),
		finalLns as (
		select iID_MLNS from lns where (select count(*) from lns t1 where t1.iID_MLNS_Cha = lns.iID_MLNS) = 0),
		tmp1 as
		(
		select distinct sXauNoiMa from BH_DTC_DieuChinhDuToanChi_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_DTC_DuToanChiTrenGiao_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_DTC_PhanBoDuToanChi_ChiTiet where iNamLamViec = @NamLamViec
		UNION 
		select distinct sXauNoiMa from BH_DTT_BHXH_ChungTu_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_DTT_BHXH_DieuChinh_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_DTT_BHXH_PhanBo_ChungTuChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_DTTM_BHYT_ThanNhan_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet where iNamLamViec = @NamLamViec
		),
		tmp2 as 
		(
			select sXauNoiMa from BH_CP_CapBoSung_KCB_BHYT_ChiTiet where iNamLamViec = @NamLamViec
			UNION
			select sXauNoiMa from BH_CP_CapTamUng_KCB_BHYT_ChiTiet where iNamLamViec = @NamLamViec
			UNION 
			select sXauNoiMa from BH_CP_ChungTu_ChiTiet ct
			INNER JOIN BH_CP_ChungTu m on m.iID_CP_ChungTu = ct.iID_CP_ChungTu
			where m.iNamChungTu = @NamLamViec
		),
		tmp3 as 
		(
		select distinct ct.sXauNoiMa from BH_QTC_CapKinhPhi_KCB_ChiTiet ct
			INNER JOIN BH_QTC_CapKinhPhi_KCB d on d.iID_ChungTu = ct.iID_ChungTu
			where d.iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTC_Nam_CheDoBHXH_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet where iNamLamViec = @NamLamViec
		UNION 
		select distinct sXauNoiMa from BH_QTC_Nam_KinhPhiQuanLy_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTC_Quy_CheDoBHXH_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTC_Quy_KCB_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTC_Quy_KinhPhiQuanLy_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTC_Quy_KPK_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTT_BHXH_ChungTu_ChiTiet where iNamLamViec = @NamLamViec
		UNION
		select distinct sXauNoiMa from BH_QTTM_BHYT_Chung_Tu_ChiTiet where iNamLamViec = @NamLamViec
		),
		root as (select * from BH_DM_MucLucNganSach where iNamLamViec = @NamLamViec)

		select distinct 
		r.*, 
		tmp1.sXauNoiMa as UsedDuToanChiTietToi, 
		tmp2.sXauNoiMa as UsedCPChiTietToi,
		tmp3.sXauNoiMa as UsedQuyetToanChiTietToi,
		finalLns.iID_MLNS as LNSID,
		parent.sXauNoiMa as MlnsParentName, parent.iID_MLNS as iID_MLNS_Cha
		from root r
		left join tmp1 on r.sXauNoiMa = tmp1.sXauNoiMa and iNamLamViec = @NamLamViec
		left join tmp2 on r.sXauNoiMa = tmp2.sXauNoiMa and iNamLamViec = @NamLamViec
		left join tmp3 on r.sXauNoiMa = tmp3.sXauNoiMa and iNamLamViec = @NamLamViec
		left join root parent on r.iID_MLNS_Cha = parent.iID_MLNS
		left join finalLns on finalLns.iID_MLNS = r.iID_MLNS 
		order by r.sxaunoima;
END
;
;
GO
