/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]    Script Date: 9/13/2024 1:42:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]    Script Date: 9/13/2024 1:42:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]    Script Date: 9/13/2024 1:42:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]    Script Date: 9/13/2024 1:42:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]    Script Date: 9/13/2024 1:42:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]    Script Date: 9/13/2024 1:42:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 9/13/2024 1:42:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang_quy]    Script Date: 9/13/2024 1:42:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang_quy]    Script Date: 9/13/2024 1:42:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang_quy]
	@NamLamViec int,
	@Quy int,
	@LoaiQuy int,
	@MaDonVi nvarchar(max),
	@Donvitinh int,
	@IsLuyKe bit
AS
BEGIN

	-- Ko In luy ke
	if (@IsLuyKe = 0)
	begin
	select temp.* from (
	select distinct
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		--ctgt.iID_MaDonVi,
		ctgt.iNamLamViec,
		sum(isnull(ctgt.fLuongChinh, 0))/@Donvitinh fLuongChinh,
		sum(isnull(ctgt.fPCChucVu, 0))/@Donvitinh fPCChucVu,
		sum(isnull(ctgt.fPCTNNghe, 0))/@Donvitinh fPCTNNghe,
		sum(isnull(ctgt.fPCTNVuotKhung, 0))/@Donvitinh fPCTNVuotKhung,
		sum(isnull(ctgt.fNghiOm, 0))/@Donvitinh fNghiOm,
		sum(isnull(ctgt.fHSBL, 0))/@Donvitinh fHSBL,
		(sum(isnull(ctgt.fLuongChinh, 0)) + sum(isnull(ctgt.fPCChucVu, 0)) + sum(isnull(ctgt.fPCTNNghe, 0)) + sum(isnull(ctgt.fPCTNVuotKhung, 0)) + sum(isnull(ctgt.fNghiOm, 0)) + sum(isnull(ctgt.fHSBL, 0)))/@Donvitinh FTongQuyLuong,
		sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0))/@Donvitinh fTruyThu_BHXH_NLD,
		sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0))/@Donvitinh fTruyThu_BHXH_NSD,
		(sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0)))/@Donvitinh fTruyThu_BHXH_TongCong,
		sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0))/@Donvitinh fTruyThu_BHYT_NLD,
		sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0))/@Donvitinh fTruyThu_BHYT_NSD,
		(sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0)))/@Donvitinh fTruyThu_BHYT_TongCong,
		sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0))/@Donvitinh fTruyThu_BHTN_NLD,
		sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0))/@Donvitinh fTruyThu_BHTN_NSD,
		(sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0)))/@Donvitinh fTruyThu_BHTN_TongCong,
		(sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0)))/@Donvitinh fTongTruyThu_BHXH
	from BH_DM_MucLucNganSach mlns
	left join BH_QTT_BHXH_CTCT_GiaiThich ctgt on mlns.iID_MLNS = ctgt.iID_MLNS
												and ctgt.iQuyNam = @Quy
												and ctgt.iQuyNamLoai = @LoaiQuy
												and ctgt.iNamLamViec = @NamLamViec
												and ctgt.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
												and ctgt.iLoaiGiaiThich = 2
	where mlns.sLNS like '902%' and mlns.sLNS <> '902'
		and mlns.iNamLamViec = @NamLamViec
	group by
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		--ctgt.iID_MaDonVi,
		ctgt.iNamLamViec,
		mlns.sXauNoiMa) temp
	order by temp.sXauNoiMa

	end
	-- In luy ke
	else
	begin
	select temp.* from (
	select distinct
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		--ctgt.iID_MaDonVi,
		ctgt.iNamLamViec,
		sum(isnull(ctgt.fLuongChinh, 0))/@Donvitinh fLuongChinh,
		sum(isnull(ctgt.fPCChucVu, 0))/@Donvitinh fPCChucVu,
		sum(isnull(ctgt.fPCTNNghe, 0))/@Donvitinh fPCTNNghe,
		sum(isnull(ctgt.fPCTNVuotKhung, 0))/@Donvitinh fPCTNVuotKhung,
		sum(isnull(ctgt.fNghiOm, 0))/@Donvitinh fNghiOm,
		sum(isnull(ctgt.fHSBL, 0))/@Donvitinh fHSBL,
		(sum(isnull(ctgt.fLuongChinh, 0)) + sum(isnull(ctgt.fPCChucVu, 0)) + sum(isnull(ctgt.fPCTNNghe, 0)) + sum(isnull(ctgt.fPCTNVuotKhung, 0)) + sum(isnull(ctgt.fNghiOm, 0)) + sum(isnull(ctgt.fHSBL, 0)))/@Donvitinh FTongQuyLuong,
		sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0))/@Donvitinh fTruyThu_BHXH_NLD,
		sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0))/@Donvitinh fTruyThu_BHXH_NSD,
		(sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0)))/@Donvitinh fTruyThu_BHXH_TongCong,
		sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0))/@Donvitinh fTruyThu_BHYT_NLD,
		sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0))/@Donvitinh fTruyThu_BHYT_NSD,
		(sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0)))/@Donvitinh fTruyThu_BHYT_TongCong,
		sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0))/@Donvitinh fTruyThu_BHTN_NLD,
		sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0))/@Donvitinh fTruyThu_BHTN_NSD,
		(sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0)))/@Donvitinh fTruyThu_BHTN_TongCong,
		(sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0)))/@Donvitinh fTongTruyThu_BHXH
	from BH_DM_MucLucNganSach mlns
	left join BH_QTT_BHXH_CTCT_GiaiThich ctgt on mlns.iID_MLNS = ctgt.iID_MLNS
												and ctgt.iQuyNam <= @Quy
												and ctgt.iQuyNamLoai = @LoaiQuy
												and ctgt.iNamLamViec = @NamLamViec
												and ctgt.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
												and ctgt.iLoaiGiaiThich = 2
	where mlns.sLNS like '902%' and mlns.sLNS <> '902'
		and mlns.iNamLamViec = @NamLamViec
	group by
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		--ctgt.iID_MaDonVi,
		ctgt.iNamLamViec,
		mlns.sXauNoiMa) temp
	order by temp.sXauNoiMa

	end
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 9/13/2024 1:42:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
	@NamLamViec int,
	@IdDonVi nvarchar(500),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempchungtudonvi]') AND type in (N'U')) drop table tempchungtudonvi;

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVi;

	-- Ko In luy ke
	if (@IsLuyKe = 0)
	begin
		select distinct
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,ctct.iQSBQNam
			,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
			,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
			,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
			,(isnull(ctct.fPCTNVuotKhung, 0) +isnull( gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
			,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
			,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
			,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
			,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
		into tempchungtudonvi
		from
			BH_QTT_BHXH_ChungTu ct
			join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
		where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			--and ct.iID_MaDonVi = @IdDonVi
			and ((@isCha = 0 and ctct.iID_MaDonVi = @IdDonVi) or (@isCha = 1 and ct.iID_MaDonVi = @IdDonVi))
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
	end
	-- In luy ke
	else
	begin
		select distinct
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
			,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0))) fLuongChinh
			,(sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0))) fPCChucVu
			,(sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0))) fPCTNNghe
			,(sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0))) fPCTNVuotKhung
			,(sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0))) fNghiOm
			,(sum(isnull(ctct.fHSBL, 0)) + sum(isnull(ctct.fHSBL, 0))) fHSBL
			,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0)) + sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0)) + sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0)) + sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0)) + sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0)) + sum(isnull(ctct.fHSBL, 0)) + sum(isnull(ctct.fHSBL, 0))) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0))) fThu_BHXH_NLD
			,(sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fThu_BHXH_NSD
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fTongSoPhaiThuBHXH
			,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0))) fThu_BHYT_NLD
			,(sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fThu_BHYT_NSD
			,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0))+ sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fTongSoPhaiThuBHYT
			,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0))) fThu_BHTN_NLD
			,(sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fThu_BHTN_NSD
			,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongSoPhaiThuBHTN
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0))+ sum(isnull(gt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			into tempchungtudonvi
			from
				BH_QTT_BHXH_ChungTu ct
				join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
			where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam <= @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi = @IdDonVi
				and ((@isCha = 0 and ctct.iID_MaDonVi = @IdDonVi) or (@isCha = 1 and ct.iID_MaDonVi = @IdDonVi))
				and ct.iLoaiTongHop = 1
				--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
			group by
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.sGhiChu,
				ctct.iID_MLNS,
				ctct.iID_MLNS_Cha,
				ctct.sXauNoiMa,
				ctct.sLNS
		end

	select
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
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
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
		left join tempchungtudonvi chungtudonvi 
		on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		order by mlns.sXauNoiMa

		IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempchungtudonvi]') AND type in (N'U')) drop table tempchungtudonvi;
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]    Script Date: 9/13/2024 1:42:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]
	@NamLamViec int ,
	@IdDonVis nvarchar(max),
	@Donvitinh int ,
	@IQuy int
AS
BEGIN
	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

CREATE TABLE #result(
iID_MLNS uniqueidentifier,
iID_MLNS_Cha uniqueidentifier,
bHangCha bit, 
sM nvarchar(200),
sXauNoiMa nvarchar(200), 
sMoTa nvarchar(200), 
iNamLamViec int,
iQSBQNam int,
fLuongChinh float,
fPhuCapChucVu float,
fPCTNNghe float,
fPCTNVuotKhung float,
fNghiOm float,
fHSBL float,
fTongQTLN float,
fDuToan float,
fDaQuyetToan float,
fConLai float,
fThu_BHXH_NLD float,
fThu_BHXH_NSD float,
fTongSoPhaiThuBHXH float,
fThu_BHYT_NLD float,
fThu_BHYT_NSD float,
fTongSoPhaiThuBHYT float,
fThu_BHTN_NLD float,
fThu_BHTN_NSD float,
fTongSoPhaiThuBHTN float,
fTongNLD float,
fTongNSD float,
fTongCong float,
MaDonVi nvarchar(50),
TenDonVi nvarchar(50)
);

--- GET CHI TIẾT ĐƠN VỊ
		select
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
			,mlns.sM
			,ctct.sXauNoiMa
			,ctct.sLNS
			,mlns.sMoTa
			INTO #tempChiTietDonVi
			from
			BH_QTT_BHXH_ChungTu ct
			INNER JOIN
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = 0;
--END chi tiet

INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sM,
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
fTongNLD ,
fTongNSD ,
fTongCong ,
MaDonVi, 
TenDonVi 
)
--INSERT TOTAL
select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sM,
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
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong,
		null,
		null
		FROM
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
			AND iNamLamViec = @NamLamViec
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS from BH_DM_MucLucNganSach where sLNS = '9020001' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS from BH_DM_MucLucNganSach where sLNS = '9020002' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa

			) mlns

		LEFT JOIN #tempChiTietDonVi chungtudonvi 
			on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa
		GROUP BY
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sM,
			mlns.sXauNoiMa,
			mlns.sMoTa
		order by mlns.sXauNoiMa;
	--INSERT CHI TIẾT	
INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
SM,
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
fTongNLD ,
fTongNSD ,
fTongCong ,
MaDonVi, 
TenDonVi 
)
SELECT
NULL ,
NULL ,
0 bHangCha , 
SM,
sXauNoiMa , 
dv.sTenDonVi,
#tempChiTietDonVi.iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPCChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
0 fTongNLD ,
0 fTongNSD ,
fTongCong ,
dv.iID_MaDonVi, 
dv.sTenDonVi as TenDonVi 
FROM #tempChiTietDonVi 
LEFT JOIN DonVi dv ON dv.iID_MaDonVi = #tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;

-- Lay data truy thu
	select sLNS,
		sum(isnull(fLuongChinh, 0)) fLuongChinh,
		sum(isnull(fPCChucVu, 0)) fPhuCapChucVu,
		sum(isnull(fPCTNNghe, 0)) fPCTNNghe,
		sum(isnull(fPCTNVuotKhung, 0)) fPCTNVuotKhung,
		sum(isnull(fNghiOm, 0)) fNghiOm,
		sum(isnull(fHSBL, 0)) fHSBL,
		(sum(isnull(fLuongChinh, 0)) + sum(isnull(fPCChucVu, 0)) + sum(isnull(fPCTNNghe, 0)) + sum(isnull(fPCTNVuotKhung, 0)) + sum(isnull(fNghiOm, 0)) + sum(isnull(fHSBL, 0))) fTongQTLN,
		sum(isnull(fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) fTongTruyThu_BHXH,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) fTongTruyThu_BHYT,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) fTongTruyThu_BHTN
	into #tbl_qtn_truythu
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iLoaiGiaiThich = 2
		and iQuyNam = @IQuy
		and iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
	group by sLNS

	--Update so truy thu
	update #result
	set 
		fLuongChinh = (select fLuongChinh from #tbl_qtn_truythu where sLNS = '9020001'),
		fPhuCapChucVu = (select fPhuCapChucVu from #tbl_qtn_truythu where sLNS = '9020001'),
		fPCTNNghe = (select fPCTNNghe from #tbl_qtn_truythu where sLNS = '9020001'),
		fPCTNVuotKhung = (select fPCTNVuotKhung from #tbl_qtn_truythu where sLNS = '9020001'),
		fNghiOm = (select fNghiOm from #tbl_qtn_truythu where sLNS = '9020001'),
		fHSBL = (select fHSBL from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongQTLN = (select fTongQTLN from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from #tbl_qtn_truythu where sLNS = '9020001')
	where sXauNoiMa = '9020001-010-011-0003'

	update #result
	set 
		fLuongChinh = (select fLuongChinh from #tbl_qtn_truythu where sLNS = '9020002'),
		fPhuCapChucVu = (select fPhuCapChucVu from #tbl_qtn_truythu where sLNS = '9020002'),
		fPCTNNghe = (select fPCTNNghe from #tbl_qtn_truythu where sLNS = '9020002'),
		fPCTNVuotKhung = (select fPCTNVuotKhung from #tbl_qtn_truythu where sLNS = '9020002'),
		fNghiOm = (select fNghiOm from #tbl_qtn_truythu where sLNS = '9020002'),
		fHSBL = (select fHSBL from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongQTLN = (select fTongQTLN from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from #tbl_qtn_truythu where sLNS = '9020002')
	where sXauNoiMa = '9020002-010-011-0003'

-- Ket qua
SELECT * FROM #result ORDER BY sXauNoiMa , MaDonVi;

DROP TABLE #tempChiTietDonVi;
DROP TABLE #tbl_qtn_truythu;
DROP TABLE #result;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]    Script Date: 9/13/2024 1:42:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]
	@NamLamViec int ,
	@IdDonVis nvarchar(max),
	@Donvitinh int ,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempChiTietDonVi]') AND type in (N'U')) drop table tempChiTietDonVi;

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	CREATE TABLE #result(
		iID_MLNS uniqueidentifier,
		iID_MLNS_Cha uniqueidentifier,
		bHangCha bit, 
		sXauNoiMa nvarchar(200), 
		sMoTa nvarchar(200), 
		iNamLamViec int,
		iQSBQNam int,
		fLuongChinh float,
		fPhuCapChucVu float,
		fPCTNNghe float,
		fPCTNVuotKhung float,
		fNghiOm float,
		fHSBL float,
		fTongQTLN float,
		fDuToan float,
		fDaQuyetToan float,
		fConLai float,
		fThu_BHXH_NLD float,
		fThu_BHXH_NSD float,
		fTongSoPhaiThuBHXH float,
		fThu_BHYT_NLD float,
		fThu_BHYT_NSD float,
		fTongSoPhaiThuBHYT float,
		fThu_BHTN_NLD float,
		fThu_BHTN_NSD float,
		fTongSoPhaiThuBHTN float,
		fTongNLD float,
		fTongNSD float,
		fTongCong float,
		MaDonVi nvarchar(50),
		TenDonVi nvarchar(50)
	);

	--- GET CHI TIẾT ĐƠN VỊ
	-- Ko In luy ke
	if (@IsLuyKe = 0)
	begin
			select distinct
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.iID_QTT_BHXH_ChungTu_ChiTiet
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
				,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
				,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
				,(isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
				,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
				,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
				,ctct.fDuToan
				,ctct.fDaQuyetToan
				,ctct.fConLai
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
				,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
				,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
				,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam = @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
				and ct.iLoaiTongHop = 1
				--and ct.bDaTongHop = 0;
		end
		--In luy ke
		else
		begin
			select distinct
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
				,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0))) fLuongChinh
				,(sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0))) fPCChucVu
				,(sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0))) fPCTNNghe
				,(sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0))) fPCTNVuotKhung
				,(sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0))) fNghiOm
				,(sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fHSBL
				,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0)) + sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0)) + sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0)) + sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0)) + sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0)) + sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fTongQTLN
				,sum(isnull(ctct.fDuToan, 0)) fDuToan
				,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
				,sum(isnull(ctct.fConLai, 0)) fConLai
				,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0))) fThu_BHXH_NLD
				,(sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fThu_BHXH_NSD
				,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fTongSoPhaiThuBHXH
				,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0))) fThu_BHYT_NLD
				,(sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fThu_BHYT_NSD
				,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fTongSoPhaiThuBHYT
				,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0))) fThu_BHTN_NLD
				,(sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fThu_BHTN_NSD
				,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongSoPhaiThuBHTN
				,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam <= @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
				and ct.iLoaiTongHop = 1
				group by 
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				--and ct.bDaTongHop = 0;
		end
	--END chi tiet

	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	--INSERT TOTAL
	select
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
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
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong,
			null,
			null
			FROM
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
				AND iNamLamViec = @NamLamViec) mlns
			LEFT JOIN tempChiTietDonVi chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			GROUP BY
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa
			order by mlns.sXauNoiMa;
		--INSERT CHI TIẾT	
	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	SELECT
	NULL ,
	NULL ,
	0 bHangCha , 
	sXauNoiMa , 
	'   ' + dv.sTenDonVi,
	tempChiTietDonVi.iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPCChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	0 fTongNLD ,
	0 fTongNSD ,
	fTongCong ,
	dv.iID_MaDonVi, 
	dv.sTenDonVi as TenDonVi 
	FROM tempChiTietDonVi 
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;
	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;


	DROP TABLE #result;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempChiTietDonVi]') AND type in (N'U')) drop table tempChiTietDonVi;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]    Script Date: 9/13/2024 1:42:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]
	@NamLamViec int ,
	@IdDonVis nvarchar(max),
	@Donvitinh int ,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN
	
	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@IQuy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	CREATE TABLE #result(
		iID_MLNS uniqueidentifier,
		iID_MLNS_Cha uniqueidentifier,
		bHangCha bit, 
		sXauNoiMa nvarchar(200), 
		sMoTa nvarchar(200), 
		iNamLamViec int,
		iQSBQNam int,
		fLuongChinh float,
		fPhuCapChucVu float,
		fPCTNNghe float,
		fPCTNVuotKhung float,
		fNghiOm float,
		fHSBL float,
		fTongQTLN float,
		fDuToan float,
		fDaQuyetToan float,
		fConLai float,
		fThu_BHXH_NLD float,
		fThu_BHXH_NSD float,
		fTongSoPhaiThuBHXH float,
		fThu_BHYT_NLD float,
		fThu_BHYT_NSD float,
		fTongSoPhaiThuBHYT float,
		fThu_BHTN_NLD float,
		fThu_BHTN_NSD float,
		fTongSoPhaiThuBHTN float,
		fTongNLD float,
		fTongNSD float,
		fTongCong float,
		MaDonVi nvarchar(50),
		TenDonVi nvarchar(50)
	);

	--- GET CHI TIẾT ĐƠN VỊ
			select distinct
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.iID_QTT_BHXH_ChungTu_ChiTiet
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam iQSBQNam
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
				,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
				,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
				,(isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
				,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
				,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
				,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
				,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
				,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
			INTO #tempChiTietDonVi
			from
			BH_QTT_BHXH_ChungTu ct
			INNER JOIN BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
			left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
			where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
				and ct.iLoaiTongHop = 1
	--END chi tiet

	INSERT INTO #result
	(
		iID_MLNS ,
		iID_MLNS_Cha ,
		bHangCha , 
		sXauNoiMa , 
		sMoTa , 
		iNamLamViec ,
		iQSBQNam ,
		fLuongChinh ,
		fPhuCapChucVu ,
		fPCTNNghe ,
		fPCTNVuotKhung ,
		fNghiOm ,
		fHSBL ,
		fTongQTLN ,
		fDuToan ,
		fDaQuyetToan ,
		fConLai ,
		fThu_BHXH_NLD ,
		fThu_BHXH_NSD ,
		fTongSoPhaiThuBHXH ,
		fThu_BHYT_NLD ,
		fThu_BHYT_NSD ,
		fTongSoPhaiThuBHYT ,
		fThu_BHTN_NLD ,
		fThu_BHTN_NSD ,
		fTongSoPhaiThuBHTN ,
		fTongNLD ,
		fTongNSD ,
		fTongCong ,
		MaDonVi, 
		TenDonVi 
	)
	--INSERT TOTAL
	select
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
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
			sum(chungtudonvi.fDuToan)/@Donvitinh fDuToan,
			sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
			sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
			sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
			sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
			sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
			(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
			sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
			(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)))/@Donvitinh fTongNLD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongNSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong,
			null,
			null
			FROM
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
				AND iNamLamViec = @NamLamViec) mlns
			LEFT JOIN #tempChiTietDonVi chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			GROUP BY
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa
			order by mlns.sXauNoiMa;
		--INSERT CHI TIẾT	
	INSERT INTO #result
	(
		iID_MLNS ,
		iID_MLNS_Cha ,
		bHangCha , 
		sXauNoiMa , 
		sMoTa , 
		iNamLamViec ,
		iQSBQNam ,
		fLuongChinh ,
		fPhuCapChucVu ,
		fPCTNNghe ,
		fPCTNVuotKhung ,
		fNghiOm ,
		fHSBL ,
		fTongQTLN ,
		fDuToan ,
		fDaQuyetToan ,
		fConLai ,
		fThu_BHXH_NLD ,
		fThu_BHXH_NSD ,
		fTongSoPhaiThuBHXH ,
		fThu_BHYT_NLD ,
		fThu_BHYT_NSD ,
		fTongSoPhaiThuBHYT ,
		fThu_BHTN_NLD ,
		fThu_BHTN_NSD ,
		fTongSoPhaiThuBHTN ,
		fTongNLD ,
		fTongNSD ,
		fTongCong ,
		MaDonVi, 
		TenDonVi 
	)
	SELECT
		NULL ,
		NULL ,
		0 bHangCha , 
		dt.sXauNoiMa , 
		'   ' + dv.sTenDonVi,
		dt.iNamLamViec ,
		sum(isnull(dt.iQSBQNam, 0)) iQSBQNam,
		sum(isnull(dt.fLuongChinh, 0)) fLuongChinh,
		sum(isnull(dt.fPCChucVu, 0)) fPCChucVu,
		sum(isnull(dt.fPCTNNghe, 0)) fPCTNNghe,
		sum(isnull(dt.fPCTNVuotKhung, 0)) fPCTNVuotKhung,
		sum(isnull(dt.fNghiOm, 0)) fNghiOm,
		sum(isnull(dt.fHSBL, 0)) fHSBL,
		sum(isnull(dt.fTongQTLN, 0)) fTongQTLN,
		sum(isnull(dt.fDuToan, 0)) fDuToan,
		sum(isnull(dt.fDaQuyetToan, 0)) fDaQuyetToan,
		sum(isnull(dt.fConLai, 0)) fConLai,
		sum(isnull(dt.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD,
		sum(isnull(dt.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH,
		sum(isnull(dt.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD,
		sum(isnull(dt.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT,
		sum(isnull(dt.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD,
		sum(isnull(dt.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN,
		0 fTongNLD ,
		0 fTongNSD ,
		sum(isnull(dt.fTongCong, 0)) ,
		dv.iID_MaDonVi, 
		dv.sTenDonVi as TenDonVi 
	FROM #tempChiTietDonVi dt
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = dt.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
	group by
		dt.sXauNoiMa, 
		dv.sTenDonVi,
		dt.iNamLamViec,
		dv.iID_MaDonVi, 
		dv.sTenDonVi;

	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

	DROP TABLE #tempChiTietDonVi;
	DROP TABLE #result;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]    Script Date: 9/13/2024 1:42:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]
	@NamLamViec int,
	@IdDonVis nvarchar(max),
	@Donvitinh int,
	@IQuy int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	select
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sM,
		mlns.sXauNoiMa,
		@NamLamViec iNamLamViec,
		sum(chungtudonvi.iQSBQNam) iQSBQNam,
		sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
		sum(chungtudonvi.fPCChucVu)/@Donvitinh fPhuCapChucVu,
		sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
		sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
		sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
		sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
		(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
		sum(chungtudonvi.fDuToan)/@Donvitinh fDuToan,
		sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
		sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
		sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
		sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh FTongSoPhaiThuBHXH,
		sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
		sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
		(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
		sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
		sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
		(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong
		into tbl_qtn_result
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
		and iNamLamViec = @NamLamViec
		union select NewId() iID_MLNS, (select distinct iID_MLNS from BH_DM_MucLucNganSach where sLNS = '9020001' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS from BH_DM_MucLucNganSach where sLNS = '9020002' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
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
			and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
			--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ct.iLoaiTongHop = 1
				) chungtudonvi 
			on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa
		group by
			mlns.iID_MLNS,
			mlns.sMoTa,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sM,
			mlns.sXauNoiMa
		

	-- Lay data truy thu
	select sLNS,
		sum(isnull(fLuongChinh, 0)) fLuongChinh,
		sum(isnull(fPCChucVu, 0)) fPhuCapChucVu,
		sum(isnull(fPCTNNghe, 0)) fPCTNNghe,
		sum(isnull(fPCTNVuotKhung, 0)) fPCTNVuotKhung,
		sum(isnull(fNghiOm, 0)) fNghiOm,
		sum(isnull(fHSBL, 0)) fHSBL,
		(sum(isnull(fLuongChinh, 0)) + sum(isnull(fPCChucVu, 0)) + sum(isnull(fPCTNNghe, 0)) + sum(isnull(fPCTNVuotKhung, 0)) + sum(isnull(fNghiOm, 0)) + sum(isnull(fHSBL, 0))) fTongQTLN,
		sum(isnull(fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) fTongTruyThu_BHXH,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) fTongTruyThu_BHYT,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) fTongTruyThu_BHTN
	into tbl_qtn_truythu
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iLoaiGiaiThich = 2
		and iQuyNam = @IQuy
		and iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
	group by sLNS

	--Update so truy thu
	update tbl_qtn_result
	set 
		fLuongChinh = (select fLuongChinh from tbl_qtn_truythu where sLNS = '9020001'),
		fPhuCapChucVu = (select fPhuCapChucVu from tbl_qtn_truythu where sLNS = '9020001'),
		fPCTNNghe = (select fPCTNNghe from tbl_qtn_truythu where sLNS = '9020001'),
		fPCTNVuotKhung = (select fPCTNVuotKhung from tbl_qtn_truythu where sLNS = '9020001'),
		fNghiOm = (select fNghiOm from tbl_qtn_truythu where sLNS = '9020001'),
		fHSBL = (select fHSBL from tbl_qtn_truythu where sLNS = '9020001'),
		fTongQTLN = (select fTongQTLN from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from tbl_qtn_truythu where sLNS = '9020001'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from tbl_qtn_truythu where sLNS = '9020001')
	where sXauNoiMa = '9020001-010-011-0003'

	update tbl_qtn_result
	set 
		fLuongChinh = (select fLuongChinh from tbl_qtn_truythu where sLNS = '9020002'),
		fPhuCapChucVu = (select fPhuCapChucVu from tbl_qtn_truythu where sLNS = '9020002'),
		fPCTNNghe = (select fPCTNNghe from tbl_qtn_truythu where sLNS = '9020002'),
		fPCTNVuotKhung = (select fPCTNVuotKhung from tbl_qtn_truythu where sLNS = '9020002'),
		fNghiOm = (select fNghiOm from tbl_qtn_truythu where sLNS = '9020002'),
		fHSBL = (select fHSBL from tbl_qtn_truythu where sLNS = '9020002'),
		fTongQTLN = (select fTongQTLN from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from tbl_qtn_truythu where sLNS = '9020002'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from tbl_qtn_truythu where sLNS = '9020002')
	where sXauNoiMa = '9020002-010-011-0003'

	-- Ket qua
	select * from tbl_qtn_result order by sXauNoiMa;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]    Script Date: 9/13/2024 1:42:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]
	@NamLamViec int,
	@IdDonVis nvarchar(max),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN
	
	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	-- Ko In luy ke
	if (@IsLuyKe = 0)
	begin
		select distinct
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa,
			@NamLamViec iNamLamViec,
			sum(chungtudonvi.iQSBQNam) iQSBQNam,
			sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
			sum(chungtudonvi.fPCChucVu)/@Donvitinh fPhuCapChucVu,
			sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
			sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
			sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
			sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
			(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
			sum(chungtudonvi.fDuToan)/@Donvitinh fDuToan,
			sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
			sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
			sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
			sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
			sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
			(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
			sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
			(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)))/@Donvitinh fTongNLD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongNSD,
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
			(select distinct
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.iID_QTT_BHXH_ChungTu_ChiTiet
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
				,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
				,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
				,(isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
				,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
				,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
				,ctct.fDuToan
				,ctct.fDaQuyetToan
				,ctct.fConLai
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
				,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
				,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
				,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				from
				BH_QTT_BHXH_ChungTu ct
				join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam = @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
				and ct.iLoaiTongHop = 1
					) chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			group by
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa
			order by mlns.sXauNoiMa
	end
	-- In luy ke
	else
	begin
		select
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa,
			@NamLamViec iNamLamViec,
			sum(chungtudonvi.iQSBQNam) iQSBQNam,
			sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
			sum(chungtudonvi.fPCChucVu)/@Donvitinh fPhuCapChucVu,
			sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
			sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
			sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
			sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
			(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
			sum(chungtudonvi.fDuToan)/@Donvitinh fDuToan,
			sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
			sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
			sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
			sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
			sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
			(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
			sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
			(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)))/@Donvitinh fTongNLD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongNSD,
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
			(select distinct
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
				,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0))) fLuongChinh
				,(sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0))) fPCChucVu
				,(sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0))) fPCTNNghe
				,(sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0))) fPCTNVuotKhung
				,(sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0))) fNghiOm
				,(sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fHSBL
				,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0)) + sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0)) + sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0)) + sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0)) + sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0)) + sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fTongQTLN
				,sum(isnull(ctct.fDuToan, 0)) fDuToan
				,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
				,sum(isnull(ctct.fConLai, 0)) fConLai
				,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0))) fThu_BHXH_NLD
				,(sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fThu_BHXH_NSD
				,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fTongSoPhaiThuBHXH
				,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0))) fThu_BHYT_NLD
				,(sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fThu_BHYT_NSD
				,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fTongSoPhaiThuBHYT
				,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0))) fThu_BHTN_NLD
				,(sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fThu_BHTN_NSD
				,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongSoPhaiThuBHTN
				,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				from
				BH_QTT_BHXH_ChungTu ct
				join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
				where ct.iNamLamViec = @NamLamViec
					and ct.iQuyNam <= @IQuy
					and ct.iQuyNamLoai = @ILoaiQuy
					and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
					and ct.iLoaiTongHop = 1
				group by 
					ct.iID_MaDonVi,
					ct.iNamLamViec
					,ctct.sGhiChu
					,ctct.iID_MLNS
					,ctct.iID_MLNS_Cha
					,ctct.sXauNoiMa
					,ctct.sLNS
					) chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			group by
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa
			order by mlns.sXauNoiMa
	end

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]    Script Date: 9/13/2024 1:42:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]
	@NamLamViec int,
	@IdDonVis nvarchar(max),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)
	DECLARE @SLoaiQuy NVARCHAR(50)

	IF (@IQuy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@IQuy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		sum(chungtudonvi.iQSBQNam) iQSBQNam,
		(sum(chungtudonvi.fLuongChinh))/@Donvitinh fLuongChinh,
		(sum(chungtudonvi.fPCChucVu))/@Donvitinh fPhuCapChucVu,
		(sum(chungtudonvi.fPCTNNghe))/@Donvitinh fPCTNNghe,
		(sum(chungtudonvi.fPCTNVuotKhung))/@Donvitinh fPCTNVuotKhung,
		(sum(chungtudonvi.fNghiOm))/@Donvitinh fNghiOm,
		(sum(chungtudonvi.fHSBL))/@Donvitinh fHSBL,
		((sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0))))/@Donvitinh fTongQTLN,
		(sum(chungtudonvi.fDuToan))/@Donvitinh fDuToan,
		(sum(chungtudonvi.fDaQuyetToan))/@Donvitinh fDaQuyetToan,
		(sum(chungtudonvi.fConLai))/@Donvitinh fConLai,
		(sum(chungtudonvi.fThu_BHXH_NLD))/@Donvitinh fThu_BHXH_NLD,
		(sum(chungtudonvi.fThu_BHXH_NSD))/@Donvitinh fThu_BHXH_NSD,
		((sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0))))/@Donvitinh fTongSoPhaiThuBHXH,
		(sum(chungtudonvi.fThu_BHYT_NLD))/@Donvitinh fThu_BHYT_NLD,
		(sum(chungtudonvi.fThu_BHYT_NSD))/@Donvitinh fThu_BHYT_NSD,
		((sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0))))/@Donvitinh fTongSoPhaiThuBHYT,
		(sum(chungtudonvi.fThu_BHTN_NLD))/@Donvitinh fThu_BHTN_NLD,
		(sum(chungtudonvi.fThu_BHTN_NSD))/@Donvitinh fThu_BHTN_NSD,
		((sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))))/@Donvitinh fTongSoPhaiThuBHTN,
		((sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))))/@Donvitinh fTongNLD,
		((sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))))/@Donvitinh fTongNSD,
		((sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))))/@Donvitinh fTongCong
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
		(select distinct
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
			,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
			,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
			,(isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
			,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
			,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
			,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
			,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			and ct.iQuyNamLoai = @ILoaiQuy
			and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
			and ct.iLoaiTongHop = 1
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
;
;
;
;
;
;
GO
