/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_cap_kp_kcb_get_tong_da_quyet_toan]    Script Date: 11/20/2023 5:25:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_cap_kp_kcb_get_tong_da_quyet_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_cap_kp_kcb_get_tong_da_quyet_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_cap_kp_kcb_get_ke_hoach_cap]    Script Date: 11/20/2023 5:25:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_cap_kp_kcb_get_ke_hoach_cap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_cap_kp_kcb_get_ke_hoach_cap]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_cap_kp_kcb_get_ke_hoach_cap]    Script Date: 11/20/2023 5:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtc_cap_kp_kcb_get_ke_hoach_cap]
	@NamLamViec int,
	@Quy int,
	@MaCSYT nvarchar(max)
AS
BEGIN
	select 
		ctct.iID_MLNS,
		sum(ctct.FTamUngQuyNay) fTamUngQuyNay,
		ctct.iID_MaCoSoYTe
	from BH_CP_CapTamUng_KCB_BHYT_ChiTiet ctct
		join BH_CP_CapTamUng_KCB_BHYT ct on ct.iID_BH_CP_CapTamUng_KCB_BHYT = ctct.iID_BH_CP_CapTamUng_KCB_BHYT
	where ct.iNamLamViec = @NamLamViec
		and ct.iQuy = @Quy
		and ctct.iID_MaCoSoYTe in (select * from splitstring(@MaCSYT))
	group by
		ctct.iID_MLNS,ctct.iID_MaCoSoYTe
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_cap_kp_kcb_get_tong_da_quyet_toan]    Script Date: 11/20/2023 5:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtc_cap_kp_kcb_get_tong_da_quyet_toan] 
	@ChungTuId nvarchar(100),
	@NamLamViec int,
	@sMaCSYT nvarchar(max)
AS
BEGIN
	SELECT ctctdqt.iID_MLNS,
		SUM(ctctdqt.fQuyetToanQuyNay) fDaQuyetToan,
		ctctdqt.iID_MaCoSoYTe
	FROM BH_QTC_CapKinhPhi_KCB ctdqt
		JOIN BH_QTC_CapKinhPhi_KCB_ChiTiet ctctdqt ON ctdqt.iID_ChungTu = ctctdqt.iID_ChungTu
	WHERE ctdqt.iNamLamViec = @NamLamViec
		AND ctdqt.iQuy < (SELECT iQuy from BH_QTC_CapKinhPhi_KCB WHERE iID_ChungTu = @ChungTuId)
		AND ctctdqt.iID_MaCoSoYTe in (SELECT * FROM splitstring(@sMaCSYT))
	GROUP BY ctctdqt.iID_MLNS, ctctdqt.iID_MaCoSoYTe
END
;
GO
