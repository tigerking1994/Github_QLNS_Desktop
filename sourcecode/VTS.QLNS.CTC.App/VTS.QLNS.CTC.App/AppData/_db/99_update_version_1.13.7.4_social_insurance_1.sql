/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet]    Script Date: 12/28/2023 10:12:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chung_tu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chung_tu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi]    Script Date: 12/28/2023 10:12:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]    Script Date: 12/28/2023 10:12:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]    Script Date: 12/28/2023 10:12:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]    Script Date: 12/28/2023 10:12:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTTM_BHYT_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fSoPhaiThu/@Donvitinh fSoPhaiThu
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '903%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTTM_BHYT_ChungTu_ChiTiet,
			ctct.fConLai,
			ctct.fDaQuyetToan,
			ctct.fDuToan,
			ctct.fSoPhaiThu,
			ctct.iID_MLNS,
			ctct.iID_MLNS_Cha,
			ctct.sGhiChu,
			ctct.sLNS,
			ctct.sXauNoiMa,
			ctct.iID_QTTM_BHYT_ChungTu
			from
			BH_QTTM_BHYT_Chung_Tu ct
			join
			BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct on ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		order by mlns.sXauNoiMa
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]    Script Date: 12/28/2023 10:12:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTTM_BHYT_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fSoPhaiThu/@Donvitinh fSoPhaiThu
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '903%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTTM_BHYT_ChungTu_ChiTiet,
			ctct.fConLai,
			ctct.fDaQuyetToan,
			ctct.fDuToan,
			ctct.fSoPhaiThu,
			ctct.iID_MLNS,
			ctct.iID_MLNS_Cha,
			ctct.sGhiChu,
			ctct.sLNS,
			ctct.sXauNoiMa,
			ctct.iID_QTTM_BHYT_ChungTu
			from
			BH_QTTM_BHYT_Chung_Tu ct
			join
			BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct on ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi]    Script Date: 12/28/2023 10:12:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi] 
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int
AS
BEGIN
	select
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		@NamLamViec iNamLamViec,
		sum(chungtudonvi.fDuToan)/@Donvitinh fDuToan,
		sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
		sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
		sum(chungtudonvi.fSoPhaiThu)/@Donvitinh fSoPhaiThu
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '903%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTTM_BHYT_ChungTu_ChiTiet,
			ctct.fConLai,
			ctct.fDaQuyetToan,
			ctct.fDuToan,
			ctct.fSoPhaiThu,
			ctct.iID_MLNS,
			ctct.iID_MLNS_Cha,
			ctct.sGhiChu,
			ctct.sLNS,
			ctct.sXauNoiMa,
			ctct.iID_QTTM_BHYT_ChungTu
			from
			BH_QTTM_BHYT_Chung_Tu ct
			join
			BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct on ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = 0
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			group by
				mlns.iID_MLNS,
				mlns.sMoTa,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa
		order by mlns.sXauNoiMa
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet]    Script Date: 12/28/2023 10:12:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chung_tu_chi_tiet]
	@YearOfWork int,
	@DaTongHop bit
AS
BEGIN
	SELECT
	KHT.iID_KHT_BHXH,
	KHT.sSoChungTu,
	KHT.iNamLamViec,
	KHT.dNgayChungTu,
	KHT.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHT.sMoTa,
	KHT.bIsKhoa,
	KHT.fTongKeHoach,
	KHT.sSoQuyetDinh,
	KHT.dNgayQuyetDinh,
	KHT.sNguoiTao,
	KHT.sNguoiSua,
	KHT.dNgayTao,
	KHT.dNgaySua,
	KHT.iLoaiTongHop,
	KHT.sTongHop,
	KHT.fBHXH_NLD FThuBHXHNLDDong,
	KHT.fBHXH_NSD FThuBHXHNSDDong,
	KHT.fTongBHXH FThuBHXH,
	KHT.fBHYT_NLD FThuBHYTNLDDong,
	KHT.fBHYT_NSD FThuBHYTNSDDong,
	KHT.fTongBHYT FTongBHYT,
	KHT.fBHTN_NLD FThuBHTNNLDDong,
	KHT.fBHTN_NSD FThuBHTNNSDDong,
	KHT.fTongBHTN FThuBHTN,
	KHT.fTong,
	KHT.bDaTongHop
	FROM BH_KHT_BHXH KHT
	LEFT JOIN DonVi DV
	ON KHT.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	AND KHT.iNamLamViec = @YearOfWork
	--AND KHT.bDaTongHop = @DaTongHop
	AND KHT.iLoaiTongHop = 1
	ORDER BY KHT.dNgayQuyetDinh DESC
END
;
GO
