/****** Object:  StoredProcedure [dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]    Script Date: 12/29/2023 11:48:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]    Script Date: 12/29/2023 11:48:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]    Script Date: 12/29/2023 11:48:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_tong_hop_so_sanh_nam]    Script Date: 12/29/2023 11:48:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_tong_hop_so_sanh_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_tong_hop_so_sanh_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]    Script Date: 12/29/2023 11:48:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_kht_rpt_chi_tiet_tong_hop]    Script Date: 12/29/2023 11:48:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_kht_rpt_chi_tiet_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_kht_rpt_chi_tiet_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_kht_rpt_chi_tiet_tong_hop]    Script Date: 12/29/2023 11:48:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_kht_rpt_chi_tiet_tong_hop] 
	@NamLamViec int,
	@IdDonVis nvarchar(600),
	@Donvitinh int
AS
BEGIN
	
	select
		mlns.iID_MLNS iID_MucLucNganSach,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha IsHangCha,
		mlns.sXauNoiMa XauNoiMa,
		mlns.sMoTa STenBhMLNS,
		--@NamLamViec iNamLamViec,
		sum(chungtudonvi.iQSBQNam) iQSBQNam,
		sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
		sum(chungtudonvi.fPhuCapChucVu)/@Donvitinh fPhuCapChucVu,
		sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
		sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
		sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
		sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
		(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPhuCapChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
		sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
		sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongThuBHXH,
		sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
		sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
		(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongThuBHYT,
		sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
		sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
		(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongThuBHTN,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong
		from
		(select
			mlns.iID_MLNS,
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
			mlns.sMoTa
		from BH_DM_MucLucNganSach mlns
		where mlns.sLNS in ('9020001','9020002')
		and mlns.iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_KHT_BHXHChiTiet,
			ctct.dNgaySua,
			ctct.dNgayTao,
			ctct.fLuongChinh,
			ctct.fNghiOm,
			ctct.fPCTNNghe,
			ctct.fPCTNVuotKhung,
			ctct.fPhuCapChucVu,
			ctct.fThu_BHTN_NLD,
			ctct.fThu_BHTN_NSD,
			ctct.fThu_BHXH_NLD,
			ctct.fThu_BHXH_NSD,
			ctct.fThu_BHYT_NLD,
			ctct.fThu_BHYT_NSD,
			ctct.fTongCong,
			ctct.fTongQTLN,
			ctct.fTongThuBHTN,
			ctct.fTongThuBHXH,
			ctct.fTongThuBHYT,
			ctct.iID_LoaiDoiTuong,
			ctct.iID_MucLucNganSach,
			ctct.iQSBQNam,
			ctct.iID_KHT_BHXH,
			ctct.sNguoiSua,
			ctct.sNguoiTao,
			ctct.sTenLoaiDoiTuong,
			ctct.sTenDonVi,
			ctct.fHSBL,
			ctct.sLNS,
			ctct.sXauNoiMa
			from
			BH_KHT_BHXH ct
			join
			BH_KHT_BHXH_ChiTiet ctct on ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
			where ct.iNamLamViec = @NamLamViec
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = 0
				) chungtudonvi on mlns.iID_MLNS = chungtudonvi.iID_MucLucNganSach
		group by
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa
		order by mlns.sXauNoiMa

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]    Script Date: 12/29/2023 11:48:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int
AS
BEGIN
	select
		--chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		--mlns.sLNS,
		--mlns.sL,
		--mlns.sK,
		--mlns.sM,
		--mlns.sTM,
		--mlns.sTTM,
		--mlns.sNG,
		--mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		sum(chungtudonvi.iQSBQNam) iQSBQNam,
		sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
		sum(chungtudonvi.fPCChucVu)/@Donvitinh fPCChucVu,
		sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
		sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
		sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
		sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
		(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
		sum(chungtudonvi.fDuToan) fDuToan,
		sum(chungtudonvi.fDaQuyetToan) fDaQuyetToan,
		sum(chungtudonvi.fConLai) fConLai,
		sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
		sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
		sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
		sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
		(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
		sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
		sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
		(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))) fTongNLD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))) fTongNSD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong
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
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = 0
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		group by
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa
		order by mlns.sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_tong_hop_so_sanh_nam]    Script Date: 12/29/2023 11:48:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_tong_hop_so_sanh_nam]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN
	select
		ct.iID_MaDonVi,
		N'1. Bảo hiểm xã hội' sMoTa,
		ct.fTongSoPhaiThuBHXH/@Donvitinh fSoPhaiThu,
		ct.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		(ct.fDaQuyetToan)/@Donvitinh fTongCong,
		(ct.fTongSoPhaiThuBHXH - (ct.fDaQuyetToan))/@Donvitinh fConLai
		from
		BH_QTT_BHXH_ChungTu ct
		where
		ct.iQuyNam = @IQuy
		and ct.iID_MaDonVi = @IdDonVi
		and ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
		and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
	union
	select
		ct.iID_MaDonVi,
		N'2. Bảo hiểm thất nghiệp' sMoTa,
		ct.fTongSoPhaiThuBHTN/@Donvitinh fSoPhaiThu,
		ct.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		(ct.fDaQuyetToan) fTongCong,
		(ct.fTongSoPhaiThuBHXH - (ct.fDaQuyetToan))/@Donvitinh fConLai
		from
		BH_QTT_BHXH_ChungTu ct
		where
		ct.iQuyNam = @IQuy
		and ct.iID_MaDonVi = @IdDonVi
		and ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
		and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
	union
	SELECT
		A.iID_MaDonVi,
		N'3. Bảo hiểm y tế quân nhân' sMoTa,
		SUM(A.fTongSoPhaiThuBHTN)/@Donvitinh fSoPhaiThu,
		SUM(A.fDaQuyetToan)/@Donvitinh,
		(SUM(A.fDaQuyetToan))/@Donvitinh fTongCong,
		(SUM(A.fTongSoPhaiThuBHTN) - (SUM(A.fDaQuyetToan)))/@Donvitinh fConLai
		FROM
			(SELECT 
				ct.iID_MaDonVi,
				IsNull(ctct.fTongSoPhaiThuBHYT, 0) fTongSoPhaiThuBHTN,
				IsNull(ctct.fDaQuyetToan, 0) fDaQuyetToan
			FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
			LEFT JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
			AND ml.iNamLamViec = @NamLamViec
			AND ml.iTrangThai = 1
			AND ml.sM = '1' -- SM Quân nhân
			WHERE ct.iQuyNam = @IQuy
				and ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
				and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				AND ct.iID_MaDonVi = @IdDonVi) AS A
   
		GROUP BY A.iID_MaDonVi
	union
	SELECT
		A.iID_MaDonVi,
		N'4. Bảo hiểm y tế của CC, CNV, LĐHĐ' sMoTa,
		SUM(A.fTongSoPhaiThuBHTN)/@Donvitinh fSoPhaiThu,
		SUM(A.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
		(SUM(A.fDaQuyetToan))/@Donvitinh fTongCong,
		(SUM(A.fTongSoPhaiThuBHTN) - (SUM(A.fDaQuyetToan)))/@Donvitinh fConLai
		FROM
			(SELECT 
				ct.iID_MaDonVi,
				IsNull(ctct.fTongSoPhaiThuBHYT, 0) fTongSoPhaiThuBHTN,
				IsNull(ctct.fDaQuyetToan, 0) fDaQuyetToan
			FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
			LEFT JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
			AND ml.iNamLamViec = @NamLamViec
			AND ml.iTrangThai = 1
			AND ml.sM = '2' -- SM Người lao động
			WHERE ct.iQuyNam = @IQuy
				and ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
				and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				AND ct.iID_MaDonVi = @IdDonVi) AS A
   
		GROUP BY A.iID_MaDonVi
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]    Script Date: 12/29/2023 11:48:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@Dvt int,
	@SoQuyetDinh nvarchar(200)
AS
BEGIN
	---THU---
	--Dữ liệu phân bổ dự toán thu BHXH
	select ctct.* into TBL_DTT from BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
	--Thu BHXH
	select * into TBL_THU_BHXH from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Thu BHXH' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 2 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHXH_NLD, 0)) + sum(isnull(fBHXH_NSD, 0))) fTongSo, sum(fBHXH_NLD) fNLD, sum(fBHXH_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001%'
	union all
	select 3 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHXH_NLD, 0)) + sum(isnull(fBHXH_NSD, 0))) fTongSo, sum(fBHXH_NLD) fNLD, sum(fBHXH_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020002%') thubhxh

	update TBL_THU_BHXH set fTongSo = (select sum(fTongSo) from TBL_THU_BHXH where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHXH where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHXH where bHangCha = 0)
						where bHangCha = 1

	--Thu BHTN
	select * into TBL_THU_BHTN from
	(select 4 rowNum, 1 bHangCha, '2' stt, N'Thu BHTN' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 5 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHTN_NLD, 0)) + sum(isnull(fBHTN_NSD, 0))) fTongSo, sum(fBHTN_NLD) fNLD, sum(fBHTN_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001%'
	union all
	select 6 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHTN_NLD, 0)) + sum(isnull(fBHTN_NSD, 0))) fTongSo, sum(fBHTN_NLD) fNLD, sum(fBHTN_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020002%') thubhtn

	update TBL_THU_BHTN set fTongSo = (select sum(fTongSo) from TBL_THU_BHTN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHTN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHTN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT quân nhân
	select * into TBL_THU_BHYT_QN from
	(select 7 rowNum, 1 bHangCha, '3' stt, N'Thu BHYT quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 8 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001-010-011-0001%'
	union all
	select 9 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020002-010-011-0001%') thubhytquannhan

	update TBL_THU_BHYT_QN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_QN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_NLD from
	(select 10 rowNum, 1 bHangCha, '4' stt, N'Thu BHYT người lao động' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 11 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001-010-011-0002%'
	union all
	select 12 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001-010-011-0002%') thubhytnld

	update TBL_THU_BHYT_NLD set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_NLD where bHangCha = 0)
						where bHangCha = 1
	
	--Dữ liệu phân bổ dự toán thu mua BHYT
	select ctct.* into TBL_DTTM from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	join  BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_TN from
	(select 13 rowNum, 1 bHangCha, '5' stt, N'Thu BHYT thân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN' sLoai
	union all
	select 14 rowNum, 1 bHangCha, 'a' stt, N'Quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_QN' sLoai
	union all
	select 15 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, sum(fDuToan) fNSD, N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where sXauNoiMa like '9030001-010-011-0000%'
	union all
	select 16 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, sum(fDuToan) fNSD, N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where sXauNoiMa like '9030001-010-011-0001%'
	union all
	select 17 rowNum, 1 bHangCha, 'b' stt, N'Công nhân, VCQP' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	union all
	select 18 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, sum(fDuToan) fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	from TBL_DTTM where sXauNoiMa like '9030002-010-011-0000%'
	union all
	select 19 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, sum(fDuToan) fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	from TBL_DTTM where sXauNoiMa like '9030002-010-011-0001%'
	) thubhytnld

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN_QN'

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN_VCQP'

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN'

	select * into TBL_THU from
	(select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHXH
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHTN
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_QN
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_NLD
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_TN) tblthu

	--Result
	select
	rowNum, 
	bHangCha, 
	stt, 
	sMoTa, 
	fTongSo/@Dvt fTongSo, 
	fNLD/@Dvt fNLD, 
	fNSD/@Dvt fNSD
	from TBL_THU

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTT]') AND type in (N'U')) drop table TBL_DTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHXH]') AND type in (N'U')) drop table TBL_THU_BHXH;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHTN]') AND type in (N'U')) drop table TBL_THU_BHTN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_QN]') AND type in (N'U')) drop table TBL_THU_BHYT_QN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_NLD]') AND type in (N'U')) drop table TBL_THU_BHYT_NLD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTTM]') AND type in (N'U')) drop table TBL_DTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_TN]') AND type in (N'U')) drop table TBL_THU_BHYT_TN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU]') AND type in (N'U')) drop table TBL_THU;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]    Script Date: 12/29/2023 11:48:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]
	@namLamViec int,
	@lstSelectedUnit ntext,
	@thanNhanQuanNhan nvarchar(50),
	@thanNhanCNVQP nvarchar(50),
	@smDuToan nvarchar(50),
	@smHachToan nvarchar(50),
	@soQuyetDinh nvarchar(500),
	@ngayQuyetDinh nvarchar(500),
	@dvt int
AS
BEGIN
	declare @TNQN_DuToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TNQN_DuToan float);
	declare @TN_CNVQP_DuToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TN_CNVQP_DuToan float);
	declare @TNQN_HachToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TNQN_HachToan float);
	declare @TN_CNVQP_HachToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TN_CNVQP_HachToan float);

	INSERT INTO @TNQN_DuToan (IdDonVi, TenDonVI, ThanhTien_TNQN_DuToan)
	SELECT
		dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @thanNhanQuanNhan
		   AND ml.sM = '0001'
		   WHERE ct.iNamLamViec = @namLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @TN_CNVQP_DuToan (IdDonVi, TenDonVI, ThanhTien_TN_CNVQP_DuToan)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @thanNhanCNVQP
		   AND ml.sM = '0001'
		   WHERE ct.iNamLamViec = @namLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @TNQN_HachToan (IdDonVi, TenDonVI, ThanhTien_TNQN_HachToan)
	SELECT 
			dt_dv.id,
			dt_dv.sTenDonVi,
		  SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
			 (SELECT
					  ml.sMoTa,
					  ctct.iID_MaDonVi,
					  IsNull(ctct.fDuToan, 0) ThanhTien,
					  ml.sLNS
			  FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
			  JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
			  RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
			  AND ml.iNamLamViec = @namLamViec
			  AND ml.iTrangThai = 1
			  AND ml.sLNS = @thanNhanQuanNhan
			  AND ml.sM = '0002'
			  WHERE ct.iNamLamViec = @namLamViec
				AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
				AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
			  ) AS A 
			   JOIN
			  (SELECT iID_MaDonVi AS id,
					  sTenDonVi, iLoai
			   FROM DonVi
			   WHERE iTrangThai = 1
			   AND iNamLamViec = @namLamViec
			   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
			GROUP BY
			dt_dv.sTenDonVi,
			dt_dv.id;

	INSERT INTO @TN_CNVQP_HachToan (IdDonVi, TenDonVI, ThanhTien_TN_CNVQP_HachToan)
	SELECT 
		dt_dv.id,
		dt_dv.sTenDonVi,
		SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		(SELECT
					ml.sMoTa,
					ctct.iID_MaDonVi,
					IsNull(ctct.fDuToan, 0) ThanhTien,
					ml.sLNS
			FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
			JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
			RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
			AND ml.iNamLamViec = @namLamViec
			AND ml.iTrangThai = 1
			AND ml.sLNS = @thanNhanCNVQP
			AND ml.sM = '0002'
			WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
			) AS A 
			JOIN
			(SELECT iID_MaDonVi AS id,
					sTenDonVi, iLoai
			FROM DonVi
			WHERE iTrangThai = 1
			AND iNamLamViec = @namLamViec
			AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;
		
		SELECT result.idDonVi, 
		result.TenDonVI STenDonVi, 
		result.TN_QN_DT/@dvt TNQNDuToan, 
		result.TN_CNVQP_DT/@dvt TNCNVQPDuToan,
		result.TongDuToan/@dvt TongDuToan,
		result.TN_QN_HT/@dvt TNQNHachToan,
		result.TN_CNVQP_HT/@dvt TNCNVQPHachToan,
		result.TongHachToan/@dvt TongHachToan,
		(result.TongDuToan + result.TongHachToan)/@dvt TongCongThanNhan
		FROM
		(SELECT tnqn_dt.idDonVi, 
		tnqn_dt.TenDonVI,
		IsNull(tnqn_dt.ThanhTien_TNQN_DuToan, 0) TN_QN_DT,
		IsNull(tncn_dt.ThanhTien_TN_CNVQP_DuToan, 0) TN_CNVQP_DT,
		(IsNull(tnqn_dt.ThanhTien_TNQN_DuToan, 0) + IsNull(tncn_dt.ThanhTien_TN_CNVQP_DuToan, 0)) TongDuToan,
		IsNull(tnqn_ht.ThanhTien_TNQN_HachToan, 0) TN_QN_HT,
		IsNull(tncn_ht.ThanhTien_TN_CNVQP_HachToan, 0) TN_CNVQP_HT,
		(IsNull(tnqn_ht.ThanhTien_TNQN_HachToan, 0) + IsNull(tncn_ht.ThanhTien_TN_CNVQP_HachToan, 0)) TongHachToan
		FROM @TNQN_DuToan tnqn_dt
		LEFT JOIN @TN_CNVQP_DuToan tncn_dt ON tnqn_dt.idDonVi = tncn_dt.idDonVi
		LEFT JOIN @TNQN_HachToan tnqn_ht ON tnqn_dt.idDonVi = tnqn_ht.idDonVi
		LEFT JOIN @TN_CNVQP_HachToan tncn_ht ON tnqn_dt.idDonVi = tncn_ht.idDonVi) result
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]    Script Date: 12/29/2023 11:48:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]
@NamLamViec int,
@IdDonVi NVARCHAR(MAX),
@DonViTinh int,
@IsTongHop int
AS
BEGIN
	
SELECT
	--ml.sLNS,
	--ml.sM,
	--ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002')
			THEN 
				SUM(ISNULL(ctct.fThu_BHXH_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHXH_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHXH,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002')
			THEN 
				SUM(ISNULL(ctct.fThu_BHTN_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHTN_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHTN,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002') AND ml.sM = '1'
			THEN 
				SUM(ISNULL(ctct.fThu_BHYT_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHYT_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_QN,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002') AND ml.sM = '2'
			THEN 
				SUM(ISNULL(ctct.fThu_BHYT_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHYT_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_NLD
	INTO #temp1
FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
LEFT JOIN BH_QTT_BHXH_ChungTu ct 
ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MLNS = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = @NamLamViec
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = @NamLamViec
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS = '9030001'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_TNQN,
	CASE 
		WHEN ml.sLNS = '9030002'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_CNVQP,
	CASE 
		WHEN ml.sLNS = '9030003'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_HVQS,
	CASE 
		WHEN ml.sLNS = '9030004'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_HSSV,
	CASE 
		WHEN ml.sLNS = '9030005'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_SQDB,
	CASE 
		WHEN ml.sLNS = '9030006'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_LUU_HS
	INTO #temp2
FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MLNS = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = @NamLamViec
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = @NamLamViec
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010001', '9010002')
		THEN 
			SUM(ISNULL(ctct.fTienCNVCQP_ThucChi, 0)) +
			SUM(ISNULL(ctct.fTienHSQBS_ThucChi, 0)) +
			SUM(ISNULL(ctct.fTienLDHD_ThucChi, 0)) +
			SUM(ISNULL(ctct.fTienQNCN_ThucChi, 0)) +
			SUM(ISNULL(ctct.fTienSQ_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiCheDo
	INTO #temp3
FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = @NamLamViec
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = @NamLamViec
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010003')
		THEN 
			SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiKinhPhiQuanLy
	INTO #temp4
FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
LEFT JOIN BH_QTC_Nam_KinhPhiQuanLy ct 
ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = @NamLamViec
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = @NamLamViec
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010006', '9010007')
		THEN 
			SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiQuanYDonVi
	INTO #temp5
FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet ctct
LEFT JOIN BH_QTC_Nam_KCB_QuanYDonVi ct 
ON ct.ID_QTC_Nam_KCB_QuanYDonVi = ctct.iID_QTC_Nam_KCB_QuanYDonVi
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = @NamLamViec
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = @NamLamViec
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010009')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiTTB,
	CASE 
		WHEN ml.sLNS IN ('9010004', '9010005')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiTSDK,
	CASE 
		WHEN ml.sLNS IN ('9050001')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiNLD,
	CASE 
		WHEN ml.sLNS IN ('9050002')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiHSSV,
	CASE 
		WHEN ml.sLNS IN ('9040001', '9040002')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiCSYT
	INTO #temp6
FROM BH_QTC_Nam_KPK_ChiTiet ctct
LEFT JOIN BH_QTC_Nam_KPK ct 
ON ct.ID_QTC_Nam_KPK = ctct.ID_QTC_Nam_KPK_ChiTiet
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = @NamLamViec
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = @NamLamViec
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

IF NOT EXISTS(SELECT 1 FROM #temp1) INSERT INTO #temp1 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp2) INSERT INTO #temp2 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp3) INSERT INTO #temp3 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp4) INSERT INTO #temp4 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp5) INSERT INTO #temp5 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp6) INSERT INTO #temp6 DEFAULT VALUES 




SELECT NoiDung, SoTien  
INTO #temp7
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienThuBHXH, 0)) / @DonViTinh AS N'1.1.0,1,Thu bảo hiểm xã hội (Phụ lục II)',
		SUM(ISNULL(fSoTienThuBHTN, 0)) / @DonViTinh AS N'1.2.0,2,Thu bảo hiểm thất nghiệp (Phụ lục III)',
		SUM(ISNULL(fSoTienThuBHYT_QN, 0)) / @DonViTinh AS N'1.3.1,-,BHYT quân nhân (Phụ lục IV)',
		SUM(ISNULL(fSoTienThuBHYT_NLD, 0)) / @DonViTinh AS N'1.3.2,-,BHYT người lao động (Phụ lục V)'
   FROM #temp1) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([1.1.0,1,Thu bảo hiểm xã hội (Phụ lục II)], 
	  [1.2.0,2,Thu bảo hiểm thất nghiệp (Phụ lục III)], 
	  [1.3.1,-,BHYT quân nhân (Phụ lục IV)], 
	  [1.3.2,-,BHYT người lao động (Phụ lục V)])  
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienThuBHYT_TNQN, 0)) / @DonViTinh AS N'1.3.3,-,BHYT thân nhân quân nhân (Phụ lục VI)',
		SUM(ISNULL(fSoTienThuBHYT_CNVQP, 0)) / @DonViTinh AS N'1.3.4,-,BHYT thân nhân CN, viên chức QP (Phụ lục VI)',
		SUM(ISNULL(fSoTienThuBHYT_HVQS, 0)) / @DonViTinh AS N'1.3.7,-,BHYT HV QS xã phường (Phụ lục VII)',
		SUM(ISNULL(fSoTienThuBHYT_HSSV, 0)) / @DonViTinh AS N'1.3.5,-,BHYT học sinh, sinh viên (Phụ lục VII)',
		SUM(ISNULL(fSoTienThuBHYT_SQDB, 0)) / @DonViTinh AS N'1.3.8,-,BHYT SQ dự bị (Phụ lục VII)',
		SUM(ISNULL(fSoTienThuBHYT_LUU_HS, 0)) / @DonViTinh AS N'1.3.6,-,BHYT lưu học sinh (Phụ lục VII)'
   FROM #temp2) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([1.3.3,-,BHYT thân nhân quân nhân (Phụ lục VI)], 
	  [1.3.4,-,BHYT thân nhân CN, viên chức QP (Phụ lục VI)], 
	  [1.3.7,-,BHYT HV QS xã phường (Phụ lục VII)], 
	  [1.3.5,-,BHYT học sinh, sinh viên (Phụ lục VII)], 
	  [1.3.8,-,BHYT SQ dự bị (Phụ lục VII)], 
	  [1.3.6,-,BHYT lưu học sinh (Phụ lục VII)])  
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiCheDo, 0)) / @DonViTinh AS N'2.1.0,1,Chi các chế độ BHXH (Phụ lục VIII)'
   FROM #temp3) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.1.0,1,Chi các chế độ BHXH (Phụ lục VIII)])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiKinhPhiQuanLy, 0)) / @DonViTinh AS N'2.2.0,2,Chi KP quản lý BHXH, BHYT (Phụ lục IX)'
   FROM #temp4) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.2.0,2,Chi KP quản lý BHXH, BHYT (Phụ lục IX)])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiQuanYDonVi, 0)) / @DonViTinh AS N'2.4.2,-,Chi KCB tại quân y đơn vị (Phụ lục XII)'
   FROM #temp5) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.4.2,-,Chi KCB tại quân y đơn vị (Phụ lục XII)])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiTTB, 0)) / @DonViTinh AS N'2.3.0,3,Chi mua sắm TTB y tế (Phụ lục X)',
		SUM(ISNULL(fSoTienChiTSDK, 0)) / @DonViTinh AS N'2.4.1,-,Chi KCB cho quân nhân tại TS-DK (Phụ lục XI)',
		SUM(ISNULL(fSoTienChiNLD, 0)) / @DonViTinh AS N'2.4.3,-,Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)',
		SUM(ISNULL(fSoTienChiHSSV, 0)) / @DonViTinh AS N'2.4.4,-,Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)',
		SUM(ISNULL(fSoTienChiCSYT, 0)) / @DonViTinh AS N'2.4.5,-,Chi KCB tại các cơ sở y tế (Phụ lục XV)'
   FROM #temp6) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.3.0,3,Chi mua sắm TTB y tế (Phụ lục X)], 
	  [2.4.1,-,Chi KCB cho quân nhân tại TS-DK (Phụ lục XI)], 
	  [2.4.3,-,Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)], 
	  [2.4.4,-,Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)], 
	  [2.4.5,-,Chi KCB tại các cơ sở y tế (Phụ lục XV)])
) AS unpvt



SELECT * FROM #temp7
UNION SELECT N'1.0.0,I,Quyết toán thu' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '1.%')
UNION SELECT N'2.0.0,II,Quyết toán chi' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '2.%')
UNION SELECT N'1.3.0,3,Thu bảo hiểm y tế' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '1.3%')
UNION SELECT N'2.4.0,4,Chi KCB BHYT' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '2.4%')


DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #temp4
DROP TABLE #temp5
DROP TABLE #temp6
DROP TABLE #temp7

END
;
GO
