/****** Object:  StoredProcedure [dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_tonghop]    Script Date: 12/26/2023 4:00:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_chitiet]    Script Date: 12/26/2023 4:00:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_chitiet]    Script Date: 12/26/2023 4:00:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_chitiet]
	@Quy int,
	@NamLamViec int,
	@LNS nvarchar(50),
	@DonViTinh int,
	@IdMaCoSoYTe nvarchar(50)
As
begin
	select 
		ml.sLNS as SLNS,
		ml.sL as SL,
		ml.sK as SK,
		ml.sM as SM,
		ml.sTM as STM,
		ml.sTTM as STTM,
		ml.sNG as SNG,
		ml.sXauNoiMa as SXauNoiMa,
		ml.iID_MLNS as IID_MLNS,
		ml.iID_MLNS_Cha as IID_MLNS_Cha,
		ml.sMoTa as SMoTa,
		Sum(ct_ct.fQuyetToanQuyNay) as FQuyetToanQuyNay
	from BH_QTC_CapKinhPhi_KCB_ChiTiet as ct_ct
	inner join BH_QTC_CapKinhPhi_KCB as ct on ct_ct.iID_ChungTu = ct.iID_ChungTu
	inner join BH_DM_MucLucNganSach as ml on ct_ct.iID_MLNS = ml.iID_MLNS
	where	ct.iNamLamViec = @NamLamViec 
			and ml.iNamLamViec = @NamLamViec
			and ct.iQuy = @Quy
			and ct_ct.sLNS = @LNS
			and ct_ct.iID_MaCoSoYTe in (SELECT * FROM f_split(@IdMaCoSoYTe))
	group by ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, ml.sXauNoiMa, ml.iID_MLNS, ml.iID_MLNS_Cha, ml.sMoTa
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_tonghop]    Script Date: 12/26/2023 4:00:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_tonghop]
	@Quy int,
	@NamLamViec int,
	@LNS nvarchar(50),
	@DonViTinh int,
	@IdMaCoSoYTe nvarchar(50)
As
begin
	select 
		cs.iID_MaCoSoYTe as IID_MaCoSoYTe,
		cs.sTenCoSoYTe as STenCoSoYTe,
		Sum(ct_ct.fQuyetToanQuyNay) as FQuyetToanQuyNay
	from BH_QTC_CapKinhPhi_KCB_ChiTiet as ct_ct
	inner join BH_QTC_CapKinhPhi_KCB as ct on ct_ct.iID_ChungTu = ct.iID_ChungTu
	inner join DM_CoSoYTe as cs on cs.iID_MaCoSoYTe =  ct_ct.iID_MaCoSoYTe
	where	ct.iNamLamViec = @NamLamViec 
			and cs.iNamLamViec = @NamLamViec
			and ct.iQuy = @Quy
			and ct_ct.sLNS = @LNS
			and ct_ct.iID_MaCoSoYTe in (SELECT * FROM f_split(@IdMaCoSoYTe))
	group by cs.iID_MaCoSoYTe, cs.sTenCoSoYTe
	
end
GO
