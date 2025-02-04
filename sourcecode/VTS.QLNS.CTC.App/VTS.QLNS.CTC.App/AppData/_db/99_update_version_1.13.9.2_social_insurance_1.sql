/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]    Script Date: 1/22/2024 5:51:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 1/22/2024 5:51:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 1/22/2024 5:51:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]    Script Date: 1/22/2024 5:51:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]    Script Date: 1/22/2024 5:51:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]    Script Date: 1/22/2024 5:51:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]    Script Date: 1/22/2024 5:51:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]    Script Date: 1/22/2024 5:51:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]
	@NamLamViec int,
	@VoucherID uniqueidentifier,
	@MaDonVi nvarchar(50),
	@LoaiGiaiThich int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_giai_thich_1]') AND type in (N'U')) drop table tmp_giai_thich_1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_giai_thich_2]') AND type in (N'U')) drop table tmp_giai_thich_2;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_giai_thich]') AND type in (N'U')) drop table tmp_giai_thich;

	select qtt.* into tmp_giai_thich_1 from
		(select 'BHXH' sMaGiaiThich, ct.iID_MaDonvi, sum(isnull(ct.fTongSoPhaiThuBHXH, 0)) fSoPhaiThuNop from BH_QTT_BHXH_ChungTu_ChiTiet ct
		where ct.iID_QTT_BHXH_ChungTu = @VoucherID
		group by ct.iID_MaDonvi
		union all
		select 'BHTN' sMaGiaiThich, iID_MaDonvi, sum(fTongSoPhaiThuBHTN) fSoPhaiThuNop from BH_QTT_BHXH_ChungTu_ChiTiet
		where iID_QTT_BHXH_ChungTu = @VoucherID
		group by iID_MaDonvi
		union all
		select 'BHYT_QN' sMaGiaiThich, iID_MaDonvi, sum(fTongSoPhaiThuBHYT) fSoPhaiThuNop from BH_QTT_BHXH_ChungTu_ChiTiet
		where iID_QTT_BHXH_ChungTu = @VoucherID
		and (sXauNoiMa like '9020001-010-011-0001%' or sXauNoiMa like '9020002-010-011-0001%')
		group by iID_MaDonvi
		union all
		select 'BHYT_LDHD' sMaGiaiThich, iID_MaDonvi, sum(fTongSoPhaiThuBHYT) fSoPhaiThuNop from BH_QTT_BHXH_ChungTu_ChiTiet
		where iID_QTT_BHXH_ChungTu = @VoucherID
		and (sXauNoiMa like '9020001-010-011-0002%' or sXauNoiMa like '9020002-010-011-0002%')
		group by iID_MaDonvi) qtt

	select qtt.* into tmp_giai_thich_2 from
		(select 'BHXH' sMaGiaiThich, iID_MaDonVi, sum(isnull(fTruyThu_BHXH_TongCong, 0)) fSoPhaiThuNop from BH_QTT_BHXH_CTCT_GiaiThich
		where iID_QTT_BHXH_ChungTu = @VoucherID
		group by iID_MaDonVi
		union all
		select 'BHTN' sMaGiaiThich, iID_MaDonVi, sum(isnull(fTruyThu_BHTN_TongCong, 0)) fSoPhaiThuNop from BH_QTT_BHXH_CTCT_GiaiThich
		where iID_QTT_BHXH_ChungTu = @VoucherID
		group by iID_MaDonVi
		union all
		select 'BHYT_QN' sMaGiaiThich, iID_MaDonvi, sum(fTruyThu_BHYT_TongCong) fSoPhaiThuNop from BH_QTT_BHXH_CTCT_GiaiThich
		where iID_QTT_BHXH_ChungTu = @VoucherID
		and (sXauNoiMa like '9020001-010-011-0001%' or sXauNoiMa like '9020002-010-011-0001%')
		group by iID_MaDonvi
		union all
		select 'BHYT_LDHD' sMaGiaiThich, iID_MaDonvi, sum(fTruyThu_BHYT_TongCong) fSoPhaiThuNop from BH_QTT_BHXH_CTCT_GiaiThich
		where iID_QTT_BHXH_ChungTu = @VoucherID
		and (sXauNoiMa like '9020001-010-011-0002%' or sXauNoiMa like '9020002-010-011-0002%')
		group by iID_MaDonvi) qtt

	select temp1.sMaGiaiThich, temp1.iID_MaDonVi, (isnull(temp1.fSoPhaiThuNop, 0) + isnull(temp2.fSoPhaiThuNop, 0)) fSoPhaiThuNop
	into tmp_giai_thich
	from tmp_giai_thich_1 temp1 
	join tmp_giai_thich_2 temp2 on temp1.sMaGiaiThich = temp2.sMaGiaiThich and temp1.iID_MaDonVi = temp2.iID_MaDonVi
	
		select
			--ml.iSTT,
			chungtudonvi.iID_QT_CTCT_GiaiThich,
			ml.iID_MLGT IIDMLNS,
			ml.sNoiDung,
			chungtudonvi.fTruyThu_BHXH_NLD FTruyThuBHXHNLD,
			chungtudonvi.fTruyThu_BHXH_NSD FTruyThuBHXHNSD,
			chungtudonvi.fTruyThu_BHXH_TongCong FTruyThuBHXHTongCong,
			chungtudonvi.fTruyThu_BHYT_NLD FTruyThuBHYTNLD,
			chungtudonvi.fTruyThu_BHYT_NSD FTruyThuBHYTNSD,
			chungtudonvi.fTruyThu_BHYT_TongCong FTruyThuBHYTTongCong,
			chungtudonvi.fTruyThu_BHTN_NLD FTruyThuBHTNNLD,
			chungtudonvi.fTruyThu_BHTN_NSD FTruyThuBHTNNSD,
			chungtudonvi.fTruyThu_BHTN_TongCong FTruyThuBHTNTongCong,
			chungtudonvi.fTongTruyThu_BHXH FTongTruyThuBHXH,
			chungtudonvi.sKienNghi,
			chungtudonvi.fPhaiNop_BHXH FPhaiNopBHXH,
			chungtudonvi.fPhaiNop_TrongQuyNam FPhaiNopTrongQuyNam,
			chungtudonvi.fTruyThu_QuyNamTruoc FTruyThuQuyNamTruoc,
			chungtudonvi.fPhaiNop_QuyNamTruoc FPhaiNopQuyNamTruoc,
			chungtudonvi.fDaNop_TrongQuyNam FDaNopTrongQuyNam,
			chungtudonvi.fConPhaiNopTiep,
			case
				when isnull(chungtudonvi.fSoPhaiThuNop, 0) <> 0 then chungtudonvi.fSoPhaiThuNop
				else gt.fSoPhaiThuNop
			end as fSoPhaiThuNop,
			chungtudonvi.fSoDaNopTrongNam,
			chungtudonvi.fSoDaNopSau31_12 FSoDaNopSau3112,
			chungtudonvi.fTongSoDaNop,
			chungtudonvi.fSoConPhaiNop,
			chungtudonvi.iQuanSo,
			chungtudonvi.fQuyTienLuongCanCu,
			chungtudonvi.fSoTienGiamDong,
			chungtudonvi.dTuNgay,
			chungtudonvi.dDenNgay,
			chungtudonvi.sNguoiTao,
			chungtudonvi.dNgayTao,
			chungtudonvi.sNguoiSua,
			chungtudonvi.sNguoiSua
		from
		(select
			iSTT, iID_MLGT, sNoiDung, iLoai, sMaGiaiThich
		from BH_QTT_MucLucGiaiThich
		where iLoai = @LoaiGiaiThich) ml
		left join
		(select distinct
			ctgt.iID_QT_CTCT_GiaiThich,
			ctgt.iID_QTT_BHXH_ChungTu,
			ctgt.sNguoiTao,
			ctgt.sNguoiSua,
			ctgt.dNgayTao,
			ctgt.dNgaySua,
			ctgt.iID_MaDonVi,
			ctgt.iNamLamViec,
			ctgt.iQuyNam,
			ctgt.iQuyNamLoai,
			ctgt.sQuyNamMoTa,
			ctgt.sNoiDung,
			ctgt.sKienNghi,
			ctgt.fPhaiNop_BHXH,
			ctgt.fPhaiNop_TrongQuyNam,
			ctgt.fTruyThu_QuyNamTruoc,
			ctgt.fPhaiNop_QuyNamTruoc,
			ctgt.fDaNop_TrongQuyNam,
			ctgt.fConPhaiNopTiep,
			ctgt.fTruyThu_BHXH_NLD,
			ctgt.fTruyThu_BHXH_NSD,
			ctgt.fTruyThu_BHXH_TongCong,
			ctgt.fTruyThu_BHYT_NLD,
			ctgt.fTruyThu_BHYT_NSD,
			ctgt.fTruyThu_BHYT_TongCong,
			ctgt.fTruyThu_BHTN_NLD,
			ctgt.fTruyThu_BHTN_NSD,
			ctgt.fTruyThu_BHTN_TongCong,
			ctgt.fTongTruyThu_BHXH,
			ctgt.fSoPhaiThuNop,
			ctgt.fSoDaNopTrongNam,
			ctgt.fSoDaNopSau31_12,
			ctgt.fTongSoDaNop,
			ctgt.fSoConPhaiNop,
			ctgt.iQuanSo,
			ctgt.fQuyTienLuongCanCu,
			ctgt.fSoTienGiamDong,
			ctgt.dTuNgay,
			ctgt.dDenNgay,
			ctgt.iID_MLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			join
			BH_QTT_BHXH_CTCT_GiaiThich ctgt on ct.iID_QTT_BHXH_ChungTu = ctgt.iID_QTT_BHXH_ChungTu
			where
			ct.iID_QTT_BHXH_ChungTu = @VoucherID
			and ctgt.iID_MaDonVi = @MaDonVi
				) chungtudonvi 
			on ml.iID_MLGT = chungtudonvi.iID_MLNS
		left join tmp_giai_thich gt on ml.sMaGiaiThich = gt.sMaGiaiThich
		order by ml.iSTT

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_giai_thich_1]') AND type in (N'U')) drop table tmp_giai_thich_1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_giai_thich_2]') AND type in (N'U')) drop table tmp_giai_thich_2;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_giai_thich]') AND type in (N'U')) drop table tmp_giai_thich;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop]    Script Date: 1/22/2024 5:51:10 PM ******/
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
		dutoan.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		(isnull(dutoan.fDuToan, 0) - isnull(chungtudonvi.fDaQuyetToan, 0) - isnull(chungtudonvi.fSoPhaiThu, 0))/@Donvitinh fConLai,
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
				) chungtudonvi on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		left join
		(select ctct.sXauNoiMa, isnull(sum(ctct.fDuToan), 0) fDuToan 
			from BH_DTTM_BHYT_ThanNhan_ChiTiet ctct
			join BH_DTTM_BHYT_ThanNhan ct on ctct.iID_DTTM_BHYT_ThanNhan = ct.iID_DTTM_BHYT_ThanNhan
			where ct.iNamLamViec = @NamLamViec
			and ct.iID_MaDonVi = @IdDonVi
			and ct.bIsKhoa = 1
			group by ctct.sXauNoiMa) dutoan on mlns.sXauNoiMa = dutoan.sXauNoiMa
			
		order by mlns.sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld]    Script Date: 1/22/2024 5:51:10 PM ******/
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
		(isnull(chungtudonvi.fDuToan, 0) - isnull(chungtudonvi.fDaQuyetToan, 0) - isnull(chungtudonvi.fSoPhaiThu, 0))/@Donvitinh fConLai,
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
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]    Script Date: 1/22/2024 5:51:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]
@ChungTuId NVARCHAR(255),
@DonViTinh int,
@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
	(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
	and ctct.sLNS = '9020001' -- Khối dự toán
	union
	select
	6 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union
	select
	8 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
	(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	9 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
	and ctct.sLNS = '9020002' -- Khối hạch toán
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	16 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0) - isnull(sum(fThuBHYT_NSD), 0))) Tang,
	((isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)
	union
	select
	18 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(fThuBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0)) Tang,
	(isnull(sum(fThuBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)
	union
	select
	19 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NSD), 0)) Tang,
	(isnull(sum(fThuBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)
	-- Khối hạch toán
	union
	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0) - isnull(sum(fThuBHYT_NSD), 0))) Tang,
	((isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)
	union
	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(fThuBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0)) Tang,
	(isnull(sum(fThuBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	union
	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NSD), 0)) Tang,
	(isnull(sum(fThuBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	--BHTN
	--Lấy dữ liệu khối dự toán
	union
	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
	(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
	and ctct.sLNS = '9020001' -- Khối dự toán
	union
	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu khối hạch toán
	union
	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
	(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NSD), 0)) Tang,
	(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_DTT_BHXH_DieuChinh = @ChungTuId
	and ctct.sLNS = '9020002' -- Khối hạch toán
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	union
	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	union
	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	union
	select
	7 STT,
	N'7=8+9' MaSo,
	N'a) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	--BHYT
	union
	select
	11 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	union
	select
	13 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	union
	select
	14 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	union
	select
	17 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and MaSo in (18,19)
	union
	select
	22 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and MaSo in (23,24)
	--BHTN
	union
	select
	26 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	union
	select
	27 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	union
	select
	28 STT,
	N'28=29+330' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	union
	select
	31 STT,
	N'31=32+33' MaSo,
	N'a) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'
	union
	select
	25 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'
	union
	select
	12 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	union
	select
	15 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	0 BHangCha
	from tbl_child_cat
	where STT in (16, 17)
	union
	select
	20 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	0 BHangCha
	from tbl_child_cat
	where STT in (21, 22)
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (16)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (16)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (16)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (21)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (21)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (21)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	10 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (15, 20)
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 1/22/2024 5:51:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
	(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	union
	select
	6 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union
	select
	8 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
	(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	9 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	16 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0) - isnull(sum(fThuBHYT_NSD), 0))) Tang,
	((isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)
	union
	select
	18 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(fThuBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0)) Tang,
	(isnull(sum(fThuBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)
	union
	select
	19 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NSD), 0)) Tang,
	(isnull(sum(fThuBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)
	-- Khối hạch toán
	union
	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0) - isnull(sum(fThuBHYT_NSD), 0))) Tang,
	((isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)
	union
	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(fThuBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0)) Tang,
	(isnull(sum(fThuBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	union
	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NSD), 0)) Tang,
	(isnull(sum(fThuBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	--BHTN
	--Lấy dữ liệu khối dự toán
	union
	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
	(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	union
	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu khối hạch toán
	union
	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
	(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NSD), 0)) Tang,
	(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	union
	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	union
	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	union
	select
	7 STT,
	N'7=8+9' MaSo,
	N'a) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	--BHYT
	union
	select
	11 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	union
	select
	13 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	union
	select
	14 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	union
	select
	17 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and MaSo in (18,19)
	union
	select
	22 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and MaSo in (23,24)
	--BHTN
	union
	select
	26 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	union
	select
	27 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	union
	select
	28 STT,
	N'28=29+330' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	union
	select
	31 STT,
	N'31=32+33' MaSo,
	N'a) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'
	union
	select
	25 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'
	union
	select
	12 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	union
	select
	15 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	0 BHangCha
	from tbl_child_cat
	where STT in (16, 17)
	union
	select
	20 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	0 BHangCha
	from tbl_child_cat
	where STT in (21, 22)
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (16)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (16)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (16)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (21)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (21)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (21)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	10 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (15, 20)
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 1/22/2024 5:51:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0,
@IsNew AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)
DECLARE @ThangTruoc AS int
DECLARE @NamTruoc AS int

IF @Thang = 1 
BEGIN
	SET @ThangTruoc = 12;
	SET @NamTruoc = @Nam - 1;
END
ELSE 
BEGIN
	SET @ThangTruoc =  @Thang - 1;
	SET @NamTruoc = @Nam;
END

SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo canbo
			Where 
				canbo.Thang = ' + CAST(@ThangTruoc AS VARCHAR(2)) + '
				And canbo.Nam = ' + CAST(@NamTruoc AS VARCHAR(4)) + '
				AND canbo.Parent IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		),
blt AS (
	SELECT * FROM TL_BangLuong_Thang
	WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	AND ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (Ma_Hieu_CanBo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = Ma_DonVi)))
),
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_LBH_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_LBH_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_LBH_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo

	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_PhuCap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM blt bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi AND bangLuong.NAM = bhxh.iNam AND bangLuong.THANG = bhxh.iThang
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
canBo.Nam_TN AS NTN,
dbo.f_luong_ntn(canBo.Ngay_NN, canBo.Ngay_XN, canBo.Ngay_TN, canBo.Thang_TNN, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') AS NamTn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
canBo.IsDelete = 1
AND canBo.Khong_Luong = 0
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.NamTN,
canBo.Tnn,
canBo.NTN,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]    Script Date: 1/22/2024 5:51:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH 
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_LBH_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_LBH_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_LBH_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo

	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_PhuCap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM TL_BangLuong_Thang bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi AND bangLuong.NAM = bhxh.iNam AND bangLuong.THANG = bhxh.iThang
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
canBo.IsDelete = 1
AND canBo.Khong_Luong = 0
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.Tnn,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
GO


/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 1/22/2024 6:23:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 1/22/2024 6:23:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMucLucNganSachDTT]') AND type in (N'U')) drop table tblMucLucNganSachDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDonViDTT]') AND type in (N'U')) drop table tblDonViDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChungTuNhanPhanBoDTT]') AND type in (N'U')) drop table tblChungTuNhanPhanBoDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChiTietDuToanNhanDTT]') AND type in (N'U')) drop table tblChiTietDuToanNhanDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_tblChiTietDuToanNhan_MucLucDTT]') AND type in (N'U')) drop table tbl_tblChiTietDuToanNhan_MucLucDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDTT]') AND type in (N'U')) drop table tempDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1DTT]') AND type in (N'U')) drop table temp1DTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSoChuaPhanBoDTT]') AND type in (N'U')) drop table tblSoChuaPhanBoDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMucLucNganSach_duplicateDTT]') AND type in (N'U')) drop table tblMucLucNganSach_duplicateDTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpNhanDuToanThu]') AND type in (N'U')) drop table tmpNhanDuToanThu;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpDaPhanBoThu]') AND type in (N'U')) drop table tmpDaPhanBoThu;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblResult]') AND type in (N'U')) drop table tblResult;
	
	---Lấy danh sách mục lục ngân sách
	select 
	NEWID()  as iID_CTDuToan_Nhan,
	CAST(NULL AS VARCHAR(50)) as iID_DTT_BHXH_ChungTu_ChiTiet,
	BH_DM_MucLucNganSach.iID_MLNS as iID_MLNS,
	BH_DM_MucLucNganSach.iID_MLNS_Cha,
	BH_DM_MucLucNganSach.sLNS,
	BH_DM_MucLucNganSach.sL,
	BH_DM_MucLucNganSach.sK,
	BH_DM_MucLucNganSach.sM,
	BH_DM_MucLucNganSach.sTM,
	BH_DM_MucLucNganSach.sTTM,
	BH_DM_MucLucNganSach.sNG,
	BH_DM_MucLucNganSach.sTNG,
	BH_DM_MucLucNganSach.sXauNoiMa,
	BH_DM_MucLucNganSach.sMoTa,
	0 as fBHXHNLD,
	0 as fBHXHNLDTruocDieuChinh,
	0 as fBHXHNSD,
	0 as fBHXHNSDTruocDieuChinh,
	0 as fBHYTNLD,
	0 as fBHYTNLDTruocDieuChinh,
	0 as fBHYTNSD,
	0 as fBHYTNSDTruocDieuChinh,
	0 as fBHTNNLD,
	0 as fBHTNNLDTruocDieuChinh,
	0 as fBHTNNSD,
	0 as fBHTNNSDTruocDieuChinh,
	0 as iRowType,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	0 as IsRemainRow
	into tblMucLucNganSachDTT
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec;

	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  tblDonViDTT
	from DonVi where iNamLamViec = @NamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi));
	
	--lấy danh sách dự toán nhận phân bổ
	select *
	into tblChungTuNhanPhanBoDTT
	from BH_DTT_Nhan_Phan_Bo_Map
	where iID_CTDuToan_PhanBo = @ChungTuId

	---Lấy danh sách chi tiết dự toán toán nhận phân bổ
	select 
			nhanpb_chitiet.iID_DTT_BHXH_ChiTiet,
			nhanpb_chitiet.iID_DTT_BHXH as iID_CTDuToan_Nhan,
			'' as iID_MaDonVi,
			nhanpb_chitiet.iID_MLNS,
			nhanpb_chitiet.fThu_BHXH_NLD fThuBHXHNLD,
			nhanpb_chitiet.fThu_BHXH_NSD fThuBHXHNSD,
			nhanpb_chitiet.fThu_BHYT_NLD fThuBHYTNLD,
			nhanpb_chitiet.fThu_BHYT_NSD fThuBHYTNSD,
			nhanpb_chitiet.fThu_BHTN_NLD fThuBHTNNLD,
			nhanpb_chitiet.fThu_BHTN_NSD fThuBHTNNSD,
			nhanpb.sSoQuyetDinh
	into tblChiTietDuToanNhanDTT
	from BH_DTT_BHXH_ChungTu_ChiTiet as nhanpb_chitiet
	inner join BH_DTT_BHXH_ChungTu as nhanpb on nhanpb.iID_DTT_BHXH = nhanpb_chitiet.iID_DTT_BHXH
	where nhanpb.iID_DTT_BHXH in (select iID_CTDuToan_Nhan from tblChungTuNhanPhanBoDTT)

	---Lấy danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		tblChiTietDuToanNhan.iID_CTDuToan_Nhan,
		tblChiTietDuToanNhan.iID_MLNS,
		tblMucLucNganSach.iID_MLNS_Cha,
		tblMucLucNganSach.sLNS,
		tblMucLucNganSach.sL,
		tblMucLucNganSach.sK, 
		tblMucLucNganSach.sM,
		tblMucLucNganSach.sTM,
		tblMucLucNganSach.sTTM,
		tblMucLucNganSach.sNG,
		tblMucLucNganSach.sTNG,
		tblMucLucNganSach.sXauNoiMa,
		tblMucLucNganSach.sMoTa,
		tblChiTietDuToanNhan.sSoQuyetDinh,
		tblChiTietDuToanNhan.fThuBHXHNLD,
		tblChiTietDuToanNhan.fThuBHXHNSD,
		tblChiTietDuToanNhan.fThuBHYTNLD,
		tblChiTietDuToanNhan.fThuBHYTNSD,
		tblChiTietDuToanNhan.fThuBHTNNLD,
		tblChiTietDuToanNhan.fThuBHTNNSD,
		3 as iRowType
	into tbl_tblChiTietDuToanNhan_MucLucDTT
	from tblMucLucNganSachDTT tblMucLucNganSach
	inner join tblChiTietDuToanNhanDTT tblChiTietDuToanNhan on tblMucLucNganSach.iID_MLNS = tblChiTietDuToanNhan.iID_MLNS


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  tbl_tblChiTietDuToanNhan_MucLucDTT.*, tblDonVi.iID_MaDonVi, concat(tblDonVi.iID_MaDonVi, '-', tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into tempDTT
	from tbl_tblChiTietDuToanNhan_MucLucDTT cross join tblDonViDTT tblDonVi

	---Map với bảng BH_DTT_BHXH_PhanBo_ChungTuChiTiet để lấy thông tin đã được phân bổ
	select 
		tempDTT.iID_CTDuToan_Nhan, 
		chitiet_phanbo.iID_DTT_BHXH_ChungTu_ChiTiet,
		tempDTT.iID_MLNS,
		tempDTT.iID_MLNS_Cha,
		tempDTT.sLNS,
		tempDTT.sL,
		tempDTT.sK,
		tempDTT.sM,
		tempDTT.sTM,
		tempDTT.sTTM,
		tempDTT.sNG,
		tempDTT.sTNG,
		tempDTT.sXauNoiMa,
		tempDTT.sMoTa as sMoTa,
		ISNULL(chitiet_phanbo.fBHXH_NLD, 0) as fBHXHNLD,
		ISNULL(tempDTT.fThuBHXHNLD, 0) as fBHXHNLDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHXH_NSD, 0) as fBHXHNSD,
		ISNULL(tempDTT.fThuBHXHNSD, 0) as fBHXHNSDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHYT_NLD, 0) as fBHYTNLD,
		ISNULL(tempDTT.fThuBHYTNLD, 0) as fBHYTNLDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHYT_NSD, 0) as fBHYTNSD,
		ISNULL(tempDTT.fThuBHYTNSD, 0) as fBHYTNSDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHTN_NLD, 0) as fBHTNNLD,
		ISNULL(tempDTT.fThuBHTNNLD, 0) as fBHTNNLDTruocDieuChinh,
		ISNULL(chitiet_phanbo.fBHTN_NSD, 0) as fBHTNNSD,
		tempDTT.fThuBHTNNSD as fBHTNNSDTruocDieuChinh,
		3 as iRowType,
		tempDTT.iID_MaDonVi,
		tempDTT.sTenDonVi,
		tempDTT.sSoQuyetDinh,
		0 as bHangCha,
		0 as IsRemainRow
	into temp1DTT
	from tempDTT
	left join 
		(
			select * 
			from BH_DTT_BHXH_PhanBo_ChungTuChiTiet 
			where iID_DTT_BHXH_ChungTu = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_CTDuToan_Nhan = tempDTT.iID_CTDuToan_Nhan and chitiet_phanbo.iID_MaDonVi = tempDTT.iID_MaDonVi and chitiet_phanbo.iID_MLNS = tempDTT.iID_MLNS


	-----Lấy danh sách Số chưa phân bổ
	select 
	npb.iID_DTT_BHXH as iID_CTDuToan_Nhan,
	CAST(NULL AS VARCHAR(50)) as iID_DTT_BHXH_ChungTu_ChiTiet,
	muluc_ngansach.iID_MLNS as iID_MLNS,
	muluc_ngansach.iID_MLNS_Cha,
	muluc_ngansach.sLNS,
	muluc_ngansach.sL,
	muluc_ngansach.sK,
	muluc_ngansach.sM,
	muluc_ngansach.sTM,
	muluc_ngansach.sTTM,
	muluc_ngansach.sNG,
	muluc_ngansach.sTNG,
	muluc_ngansach.sXauNoiMa,
	N'Số chưa phân bổ' as sMoTa,
	chitiet_chuaphanbo.fBHXHNLD as fBHXHNLD,
	chitiet_chuaphanbo.fBHXHNLD as fBHXHNLDTruocDieuChinh,
	chitiet_chuaphanbo.fBHXHNSD as fBHXHNSD,
	chitiet_chuaphanbo.fBHXHNSD as fBHXHNSDTruocDieuChinh,
	chitiet_chuaphanbo.fBHYTNLD as fBHYTNLD,
	chitiet_chuaphanbo.fBHYTNLD as fBHYTNLDTruocDieuChinh,
	chitiet_chuaphanbo.fBHYTNSD as fBHYTNSD,
	chitiet_chuaphanbo.fBHYTNSD as fBHYTNSDTruocDieuChinh,
	chitiet_chuaphanbo.fBHTNNLD as fBHTNNLD,
	chitiet_chuaphanbo.fBHTNNLD as fBHTNNLDTruocDieuChinh,
	chitiet_chuaphanbo.fBHTNNSD as fBHTNNSD,
	chitiet_chuaphanbo.fBHTNNSD as fBHTNNSDTruocDieuChinh,
	2 as iRowType,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as IsRemainRow
	into tblSoChuaPhanBoDTT
	from tblMucLucNganSachDTT as muluc_ngansach
	inner join 
	(
		select (ISNULL(ct_npb.fThu_BHXH_NLD,0) - ISNULL(ct_pb_t.fBHXHNLD,0)) as fBHXHNLD,
				(ISNULL(ct_npb.fThu_BHXH_NSD,0) - ISNULL(ct_pb_t.fBHXHNSD,0)) as fBHXHNSD,
				(ISNULL(ct_npb.fThu_BHYT_NLD,0) - ISNULL(ct_pb_t.fBHYTNLD,0)) as fBHYTNLD,
				(ISNULL(ct_npb.fThu_BHYT_NSD,0) - ISNULL(ct_pb_t.fBHYTNSD,0)) as fBHYTNSD,
				(ISNULL(ct_npb.fThu_BHTN_NLD,0) - ISNULL(ct_pb_t.fBHTNNLD,0)) as fBHTNNLD,
				(ISNULL(ct_npb.fThu_BHTN_NSD,0) - ISNULL(ct_pb_t.fBHTNNSD,0)) as fBHTNNSD,
				ct_npb.iID_MLNS,
				ct_npb.iID_DTT_BHXH iID_CTDuToan_Nhan
				from BH_DTT_BHXH_ChungTu_ChiTiet as ct_npb
				left join
					(
						select 
							sum(ISNULL(fBHXH_NLD,0)) as fBHXHNLD,
							sum(ISNULL(fBHXH_NSD,0)) as fBHXHNSD,
							sum(ISNULL(fBHYT_NLD,0)) as fBHYTNLD,
							sum(ISNULL(fBHYT_NSD,0)) as fBHYTNSD,
							sum(ISNULL(fBHTN_NLD,0)) as fBHTNNLD,
							sum(ISNULL(fBHTN_NSD,0)) as fBHTNNSD,
							iID_MLNS,
							iID_CTDuToan_Nhan
						from BH_DTT_BHXH_PhanBo_ChungTuChiTiet as ct_pb
						where ct_pb.iID_CTDuToan_Nhan in (select iID_CTDuToan_Nhan from tblChungTuNhanPhanBoDTT)
						group by iID_MLNS, iID_CTDuToan_Nhan
					)as ct_pb_t  
					on ct_pb_t.iID_MLNS = ct_npb.iID_MLNS and ct_npb.iID_DTT_BHXH = ct_pb_t.iID_CTDuToan_Nhan) as chitiet_chuaphanbo on chitiet_chuaphanbo.iID_MLNS = muluc_ngansach.iID_MLNS
					inner join BH_DTT_BHXH_ChungTu as npb on npb.iID_DTT_BHXH = chitiet_chuaphanbo.iID_CTDuToan_Nhan
					where npb.iID_DTT_BHXH in ( select iID_CTDuToan_Nhan from tblChungTuNhanPhanBoDTT)

	------Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select 
	tblSoChuaPhanBo.iID_CTDuToan_Nhan as iID_CTDuToan_Nhan,
	CAST(NULL AS VARCHAR(50)) as iID_DTT_BHXH_ChungTu_ChiTiet,
	tblMucLucNganSach.iID_MLNS as iID_MLNS,
	tblMucLucNganSach.iID_MLNS_Cha,
	tblMucLucNganSach.sLNS,
	tblMucLucNganSach.sL,
	tblMucLucNganSach.sK,
	tblMucLucNganSach.sM,
	tblMucLucNganSach.sTM,
	tblMucLucNganSach.sTTM,
	tblMucLucNganSach.sNG,
	tblMucLucNganSach.sTNG,
	tblMucLucNganSach.sXauNoiMa,
	tblMucLucNganSach.sMoTa,
	ISNULL(tblSoChuaPhanBo.fBHXHNLD, 0) fBHXHNLD,
	ISNULL(tblSoChuaPhanBo.fBHXHNLDTruocDieuChinh, 0) fBHXHNLDTruocDieuChinh,
	ISNULL(tblSoChuaPhanBo.fBHXHNSD, 0) fBHXHNSD,
	ISNULL(tblSoChuaPhanBo.fBHXHNSDTruocDieuChinh, 0) fBHXHNSDTruocDieuChinh,
	ISNULL(tblSoChuaPhanBo.fBHYTNLD, 0) fBHYTNLD,
	ISNULL(tblSoChuaPhanBo.fBHYTNLDTruocDieuChinh, 0) fBHYTNLDTruocDieuChinh,
	ISNULL(tblSoChuaPhanBo.fBHYTNSD, 0) fBHYTNSD,
	ISNULL(tblSoChuaPhanBo.fBHYTNSDTruocDieuChinh, 0) fBHYTNSDTruocDieuChinh,
	ISNULL(tblSoChuaPhanBo.fBHTNNLD, 0) fBHTNNLD,
	ISNULL(tblSoChuaPhanBo.fBHTNNLDTruocDieuChinh, 0) fBHTNNLDTruocDieuChinh,
	ISNULL(tblSoChuaPhanBo.fBHTNNSD, 0) fBHTNNSD,
	ISNULL(tblSoChuaPhanBo.fBHTNNSDTruocDieuChinh, 0) fBHTNNSDTruocDieuChinh,
	case when tblSoChuaPhanBo.iRowType = 2 then 1 else tblMucLucNganSach.iRowType end iRowType,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when tblSoChuaPhanBo.iRowType = 2 then 1 else tblMucLucNganSach.bHangCha end bHangCha,
	0 as IsRemainRow
	into tblMucLucNganSach_duplicateDTT
	from tblMucLucNganSachDTT tblMucLucNganSach
	left join tblSoChuaPhanBoDTT tblSoChuaPhanBo on tblMucLucNganSach.iID_MLNS = tblSoChuaPhanBo.iID_MLNS
	left join tbl_tblChiTietDuToanNhan_MucLucDTT nhanPB on tblMucLucNganSach.iID_MLNS = nhanPB.iID_MLNS
	order by sXauNoiMa

	-----------
	-- Dữ liệu nhận phân bổ
	declare @IdDotNhan nvarchar(500) = (select iID_DotNhan from BH_DTT_BHXH_PhanBo_ChungTu where iID_DTT_BHXH_PhanBo_ChungTu = @ChungTuId);

	select ct.iID_MLNS, ct.iID_DTT_BHXH, ct.fThu_BHXH_NLD, ct.fThu_BHXH_NSD, ct.fThu_BHYT_NLD, ct.fThu_BHYT_NSD, ct.fThu_BHTN_NLD, ct.fThu_BHTN_NSD INTO tmpNhanDuToanThu from BH_DTT_BHXH_ChungTu_ChiTiet ct
	JOIN BH_DTT_BHXH_ChungTu dt on dt.iID_DTT_BHXH = ct.iID_DTT_BHXH
	where dt.iID_DTT_BHXH IN (select * from splitstring( @IdDotNhan))
	-----------
	-- Dữ liệu đã phân bổ
	declare @dNgayQuyetDinh Datetime = (select dNgayQuyetDinh from BH_DTT_BHXH_PhanBo_ChungTu where iID_DTT_BHXH_PhanBo_ChungTu = @ChungTuId);

	select iID_MLNS, iID_CTDuToan_Nhan, 
		0 - SUM(ISNULL(fBHXH_NLD, 0)) fBHXH_NLD,
		0 - SUM(ISNULL(fBHXH_NSD, 0)) fBHXH_NSD,
		0 - SUM(ISNULL(fBHYT_NLD, 0)) fBHYT_NLD,
		0 - SUM(ISNULL(fBHYT_NSD, 0)) fBHYT_NSD,
		0 - SUM(ISNULL(fBHTN_NLD, 0)) fBHTN_NLD,
		0 - SUM(ISNULL(fBHTN_NSD, 0)) fBHTN_NSD
	INTO tmpDaPhanBoThu from BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
	where 
	ct.iNamLamViec = @NamLamViec
	AND ct.dNgayQuyetDinh < @dNgayQuyetDinh
	group by iID_MLNS, iID_CTDuToan_Nhan;
	-----------
	select * into tblResult from
	(
		SELECT * from tblMucLucNganSach_duplicateDTT
		UNION ALL
		SELECT * from tblSoChuaPhanBoDTT
		UNION ALL
		SELECT * from temp1DTT
	) as result
	--order by result.sXauNoiMa, result.sSoQuyetDinh, result.iID_MaDonVi, result.iRowType, result.IsRemainRow
	--Result

	SELECT
	rs.iID_CTDuToan_Nhan,
	rs.iID_DTT_BHXH_ChungTu_ChiTiet,
	rs.iID_MLNS,
	rs.iID_MLNS_Cha,
	rs.sLNS,
	rs.sL,
	rs.sK,
	rs.sM,
	rs.sTM,
	rs.sTTM,
	rs.sNG,
	rs.sTNG,
	rs.sXauNoiMa,
	rs.sMoTa,
	rs.iRowType,
	rs.iID_MaDonVi,
	rs.sTenDonVi,
	rs.sSoQuyetDinh as sSoQuyetDinh,
	rs.IsRemainRow,
	rs.bHangCha,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHXH_NLD, 0) + (ISNULL(dpb.fBHXH_NLD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHXH_NLD,0) + (ISNULL(dpb.fBHXH_NLD, 0))
		ELSE rs.fBHXHNLD
	END as fBHXHNLD,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHXH_NSD, 0) + (ISNULL(dpb.fBHXH_NSD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHXH_NSD,0) + (ISNULL(dpb.fBHXH_NSD, 0))
		ELSE rs.fBHXHNSD
	END as fBHXHNSD,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHYT_NLD, 0) + (ISNULL(dpb.fBHYT_NLD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHYT_NLD,0) + (ISNULL(dpb.fBHYT_NLD, 0))
		ELSE rs.fBHYTNLD
	END as fBHYTNLD,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHYT_NSD, 0) + (ISNULL(dpb.fBHYT_NSD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHYT_NSD,0) + (ISNULL(dpb.fBHYT_NSD, 0))
		ELSE rs.fBHYTNSD
	END as fBHYTNSD,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHTN_NLD, 0) + (ISNULL(dpb.fBHTN_NLD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHTN_NLD,0) + (ISNULL(dpb.fBHTN_NLD, 0))
		ELSE rs.fBHTNNLD
	END as fBHTNNLD,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHTN_NSD, 0) + (ISNULL(dpb.fBHTN_NSD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHTN_NSD,0) + (ISNULL(dpb.fBHTN_NSD, 0))
		ELSE rs.fBHTNNSD
	END as fBHTNNSD,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHXH_NLD, 0) + (ISNULL(dpb.fBHXH_NLD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHXH_NLD,0) + (ISNULL(dpb.fBHXH_NLD, 0))
		ELSE rs.fBHXHNLD
	END as fBHXHNLDTruocDieuChinh,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHXH_NSD, 0) + (ISNULL(dpb.fBHXH_NSD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHXH_NSD,0) + (ISNULL(dpb.fBHXH_NSD, 0))
		ELSE rs.fBHXHNSD
	END as fBHXHNSDTruocDieuChinh,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHYT_NLD, 0) + (ISNULL(dpb.fBHYT_NLD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHYT_NLD,0) + (ISNULL(dpb.fBHYT_NLD, 0))
		ELSE rs.fBHYTNLD
	END as fBHYTNLDTruocDieuChinh,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHYT_NSD, 0) + (ISNULL(dpb.fBHYT_NSD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHYT_NSD,0) + (ISNULL(dpb.fBHYT_NSD, 0))
		ELSE rs.fBHYTNSD
	END as fBHYTNSDTruocDieuChinh,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHTN_NLD, 0) + (ISNULL(dpb.fBHTN_NLD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHTN_NLD,0) + (ISNULL(dpb.fBHTN_NLD, 0))
		ELSE rs.fBHTNNLD
	END as fBHTNNLDTruocDieuChinh,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fThu_BHTN_NSD, 0) + (ISNULL(dpb.fBHTN_NSD, 0))
		WHEN rs.IsRemainRow = 0 AND rs.iRowType = 1 THEN ISNULL(dt.fThu_BHTN_NSD,0) + (ISNULL(dpb.fBHTN_NSD, 0))
		ELSE rs.fBHTNNSD
	END as fBHTNNSDTruocDieuChinh
	FROM tblResult rs
	LEFT JOIN tmpNhanDuToanThu dt ON rs.iID_MLNS = dt.iID_MLNS 
	LEFT JOIN tmpDaPhanBoThu dpb ON dpb.iID_MLNS = rs.iID_MLNS AND dpb.iID_CTDuToan_Nhan = rs.iID_CTDuToan_Nhan
	order by rs.sXauNoiMa, rs.sSoQuyetDinh, rs.iID_MaDonVi,rs.iRowType,rs.IsRemainRow

END
;
GO


/****** Object:  StoredProcedure [dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]    Script Date: 1/23/2024 8:31:32 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]    Script Date: 1/23/2024 8:31:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@ngayChungTu date
AS
BEGIN
	SELECT 
		ct.iID_MLNS,
		sum(ct.fBHXH_NLD) fBHXHNLD,
		sum(ct.fBHXH_NSD) fBHXHNSD,
		sum(ct.fBHYT_NLD) fBHYTNLD,
		sum(ct.fBHYT_NSD) fBHYTNSD,
		sum(ct.fBHTN_NLD) fBHTNNLD,
		sum(ct.fBHTN_NSD) fBHTNNSD
	FROM
		(
			SELECT
				ddv.sTenDonVi,
				bhct.iID_MLNS,
				bhct.fBHXH_NLD,
				bhct.fBHXH_NSD,
				bhct.fBHYT_NLD,
				bhct.fBHYT_NSD,
				bhct.fBHTN_NLD,
				bhct.fBHTN_NSD
			FROM 
				BH_DTT_BHXH_PhanBo_ChungTu bh
			JOIN 
				BH_DTT_BHXH_PhanBo_ChungTuChiTiet bhct ON bh.iID_DTT_BHXH_PhanBo_ChungTu = bhct.iID_DTT_BHXH_ChungTu 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bhct.iID_MaDonVi = @IdDonVi
				and bh.iNamLamViec = @NamLamViec
				and bh.bKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@ngayChungTu as date)
		) ct
		group by ct.iID_MLNS
END
;
;
GO



/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 1/23/2024 10:14:28 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 1/23/2024 10:14:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
As

begin
	---Lấy danh sách mục lục ngân sách theo điều kiện @LNS
	select 
	NEWID()  as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	BH_DM_MucLucNganSach.iID_MLNS as iID_MLNS,
	BH_DM_MucLucNganSach.iID_MLNS_Cha,
	BH_DM_MucLucNganSach.sLNS,
	BH_DM_MucLucNganSach.sL,
	BH_DM_MucLucNganSach.sK,
	BH_DM_MucLucNganSach.sM,
	BH_DM_MucLucNganSach.sTM,
	BH_DM_MucLucNganSach.sTTM,
	BH_DM_MucLucNganSach.sNG,
	BH_DM_MucLucNganSach.sTNG,
	BH_DM_MucLucNganSach.sXauNoiMa,
	BH_DM_MucLucNganSach.sMoTa as sNoiDung,
	0 as fTienTuChi,
	--0 as fTienTuChiTruocDieuChinh,
	--0 as fTienHienVat,
	--0 as fTienHienVatTruocDieuChinh,
	BH_DM_MucLucNganSach.sCPChiTietToi,
	BH_DM_MucLucNganSach.sDuToanChiTietToi,
	1 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	0 as IsRemainRow
	into #tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	--where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  and sLNS LIKE '901%'
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  

	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  #tblDonVi
	from DonVi where iNamLamViec = @NamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--lấy danh sách dự toán nhận phân bổ
	select *
	into #tblChungTuNhanPhanBo
	from BH_DTC_Nhan_PhanBo_Map
	where iID_BHDTC_PhanBo = @ChungTuId

	
	---Lấy danh sách chi tiết dự toán toán nhận phân bổ
	select 
			nhanpb_chitiet.ID as iIDNhan_ChiTiet,
			nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
			'' as iID_MaDonVi,
			nhanpb_chitiet.iID_MucLucNganSach,
			nhanpb_chitiet.fTienTuChi as fTuChi,
			--nhanpb_chitiet.fTienHienVat as fHienVat,
			nhanpb.sSoQuyetDinh
	into #tblChiTietDuToanNhan
	from BH_DTC_DuToanChiTrenGiao_ChiTiet as nhanpb_chitiet
	inner join BH_DTC_DuToanChiTrenGiao as nhanpb on nhanpb.ID = nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao
	where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)

	

	---Lấy  danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		#tblChiTietDuToanNhan.iID_DTC_DuToanChiTrenGiao,
		#tblChiTietDuToanNhan.iID_MucLucNganSach as iID_MLNS,
		#tblMucLucNganSach.iID_MLNS_Cha,
		#tblMucLucNganSach.sLNS,
		#tblMucLucNganSach.sL,
		#tblMucLucNganSach.sK, 
		#tblMucLucNganSach.sM,
		#tblMucLucNganSach.sTM,
		#tblMucLucNganSach.sTTM,
		#tblMucLucNganSach.sNG,
		#tblMucLucNganSach.sTNG,
		#tblMucLucNganSach.sXauNoiMa,
		#tblMucLucNganSach.sNoiDung,
		#tblChiTietDuToanNhan.sSoQuyetDinh,
		#tblChiTietDuToanNhan.fTuChi as fTienTuChi ,
		--#tblChiTietDuToanNhan.fHienVat as fTienHienVat,
		#tblMucLucNganSach.sCPChiTietToi,
		#tblMucLucNganSach.sDuToanChiTietToi,
		3 as Type
	into #tbl_tblChiTietDuToanNhan_MucLuc
	from #tblMucLucNganSach
	inner join #tblChiTietDuToanNhan on #tblMucLucNganSach.iID_MLNS = #tblChiTietDuToanNhan.iID_MucLucNganSach

	


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  #tbl_tblChiTietDuToanNhan_MucLuc.*,#tblDonVi.iID_MaDonVi, concat(#tblDonVi.iID_MaDonVi, '-', #tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into #temp
	from #tbl_tblChiTietDuToanNhan_MucLuc cross join #tblDonVi 


	---Map với bảng BH_DTC_PhanBoDuToanChi_ChiTiet để lấy thông tin fTuChi đã được phân bổ
	select 
		#temp.iID_DTC_DuToanChiTrenGiao, 
		chitiet_phanbo.ID as iID_DTC_PhanBoDuToanChiTiet,
		#temp.iID_MLNS,
		#temp.iID_MLNS_Cha,
		#temp.sLNS,
		#temp.sL,
		#temp.sK,
		#temp.sM,
		#temp.sTM,
		#temp.sTTM,
		#temp.sNG,
		#temp.sTNG,
		#temp.sXauNoiMa,
		#temp.sNoiDung as sNoiDung,
		chitiet_phanbo.fTienTuChi as fTienTuChi,
		#temp.fTienTuChi as fTienTuChiTruocDieuChinh,
		--chitiet_phanbo.fTienHienVat as fTienHienVat,
		--#temp.fTienHienVat as fTienHienVatTruocDieuChinh,
		#temp.sCPChiTietToi,
		#temp.sDuToanChiTietToi,
		3 as Type,
		#temp.iID_MaDonVi,
		#temp.sTenDonVi,
		#temp.sSoQuyetDinh,
		0 as bHangCha,
		0 as IsRemainRow
	into #temp1
	from #temp
	left join 
		(
			select * 
			from BH_DTC_PhanBoDuToanChi_ChiTiet 
			where iID_DTC_PhanBoDuToanChi = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_DTC_DuToanChiTrenGiao = #temp.iID_DTC_DuToanChiTrenGiao and chitiet_phanbo.iID_MaDonVi = #temp.iID_MaDonVi and chitiet_phanbo.iID_MucLucNganSach = #temp.iID_MLNS



	-----Lấy danh sách số chưa phân bổ
	select 
	npb.ID as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	muluc_ngansach.iID_MLNS as iID_MLNS,
	muluc_ngansach.iID_MLNS_Cha,
	muluc_ngansach.sLNS,
	muluc_ngansach.sL,
	muluc_ngansach.sK,
	muluc_ngansach.sM,
	muluc_ngansach.sTM,
	muluc_ngansach.sTTM,
	muluc_ngansach.sNG,
	muluc_ngansach.sTNG,
	muluc_ngansach.sXauNoiMa,
	N'Số chưa phân bổ' as sNoiDung,
	chitiet_chuaphanbo.fTuChi as fTienTuChi,
	chitiet_chuaphanbo.fTuChi as fTienTuChiTruocDieuChinh,
	--chitiet_chuaphanbo.fHienVat as fTienHienVat,
	--chitiet_chuaphanbo.fHienVat as fTienHienVatTruocDieuChinh,
	muluc_ngansach.sCPChiTietToi,
	muluc_ngansach.sDuToanChiTietToi,
	2 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as IsRemainRow
	into #tblSoChuaPhanBo
	from #tblMucLucNganSach as muluc_ngansach
	inner join 
	(
		select (
		
		ISNULL(ct_npb.fTienTuChi,0) -  ISNULL(ct_pb_t.fTuChi,0) ) as fTuChi ,
		ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		--select (ISNULL(ct_npb.fTienTuChi,0) - ISNULL(ct_pb_t.fTuChi,0)) as fTuChi , (ISNULL(ct_npb.fTienHienVat,0) - ISNULL(ct_pb_t.fHienVat,0)) as fHienVat, ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		from BH_DTC_DuToanChiTrenGiao_ChiTiet as ct_npb
		left join
			(
				select  sum(  fTienTuChi) as fTuChi , iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
				from BH_DTC_PhanBoDuToanChi_ChiTiet as ct_pb
				where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)
				group by  iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
			)as ct_pb_t  

		on ct_pb_t.iID_MucLucNganSach = ct_npb.iID_MucLucNganSach and ct_npb.iID_DTC_DuToanChiTrenGiao = ct_pb_t.iID_DTC_DuToanChiTrenGiao) as chitiet_chuaphanbo
		on chitiet_chuaphanbo.iID_MucLucNganSach = muluc_ngansach.iID_MLNS 
	inner join BH_DTC_DuToanChiTrenGiao as npb on npb.ID = chitiet_chuaphanbo.iID_DTC_DuToanChiTrenGiao
	where npb.ID in ( select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo);
	---- Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select 
	#tblSoChuaPhanBo.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	#tblMucLucNganSach.iID_MLNS as iID_MLNS,
	#tblMucLucNganSach.iID_MLNS_Cha,
	#tblMucLucNganSach.sLNS,
	#tblMucLucNganSach.sL,
	#tblMucLucNganSach.sK,
	#tblMucLucNganSach.sM,
	#tblMucLucNganSach.sTM,
	#tblMucLucNganSach.sTTM,
	#tblMucLucNganSach.sNG,
	#tblMucLucNganSach.sTNG,
	#tblMucLucNganSach.sXauNoiMa,
	#tblMucLucNganSach.sNoiDung as sNoiDung,
	#tblSoChuaPhanBo.fTienTuChi as fTienTuChi,
	#tblSoChuaPhanBo.fTienTuChiTruocDieuChinh as fTienTuChiTruocDieuChinh,
	--#tblSoChuaPhanBo.fTienHienVat as fTienHienVat,
	--#tblSoChuaPhanBo.fTienHienVatTruocDieuChinh as fTienHienVatTruocDieuChinh,
	#tblMucLucNganSach.sCPChiTietToi,
	#tblMucLucNganSach.sDuToanChiTietToi,
	case when #tblSoChuaPhanBo.Type = 2 then 2 else #tblMucLucNganSach.Type end Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	#tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangCha,
	0 as IsRemainRow
	into #tblMucLucNganSach_duplicate
	from #tblMucLucNganSach
	left join #tblSoChuaPhanBo on #tblMucLucNganSach.iID_MLNS = #tblSoChuaPhanBo.iID_MLNS
	order by sXauNoiMa
	
	--select * from tblMucLucNganSach_duplicate
	--select * from tblSoChuaPhanBo
	--select * from #temp1
	---Tính lại dự toán, số đã phân bổ
		-- Dữ liệu nhận phân bổ

declare @iiDotNhan nvarchar(500) =( select  iID_DotNhan from  BH_DTC_PhanBoDuToanChi where ID = @ChungTuId);
select iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao, fTienTuChi INTO #tmpNhanDuToan from BH_DTC_DuToanChiTrenGiao_ChiTiet ct
INNER JOIN BH_DTC_DuToanChiTrenGiao dt on dt.ID = ct.iID_DTC_DuToanChiTrenGiao
where dt.ID IN (select * from splitstring( @iiDotNhan))


-- Dữ liệu đã phân bổ
declare @dNgayQuyetDinh Datetime = (select dNgayQuyetDinh from BH_DTC_PhanBoDuToanChi where ID = @ChungTuId);
select iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao, 0 - SUM(ISNULL(fTienTuChi,0)) fTuChi   INTO #tmpDaPhanBo from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
INNER JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID
where 
ct.iNamChungTu = @NamLamViec
AND ct.dNgayQuyetDinh < @dNgayQuyetDinh
group by iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao;
	--Hiển thị kết quả trả về
	select * INTO #result from
	(
		SELECT * from #tblMucLucNganSach_duplicate
		UNION ALL
		SELECT * from #tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as test
	--order by test.sXauNoiMa, test.sSoQuyetDinh, test.iID_MaDonVi,test.Type,test.IsRemainRow

	----============
	SELECT
	rs.iID_DTC_DuToanChiTrenGiao,
	rs.iID_DTC_PhanBoDuToanChiTiet,
	rs.iID_MLNS,
	rs.iID_MLNS_Cha,
	rs.sLNS,
	rs.sL,
	rs.sK,
	rs.sM,
	rs.sTM,
	rs.sTTM,
	rs.sNG,
	rs.sTNG,
	rs.sXauNoiMa,
	rs.sNoiDung as sNoiDung,

	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		ELSE rs.fTienTuChi
	END as fTienTuChi,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		ELSE rs.fTienTuChi
	END as fTienTuChiTruocDieuChinh,
	--#tblSoChuaPhanBo.fTienHienVat as fTienHienVat,
	--#tblSoChuaPhanBo.fTienHienVatTruocDieuChinh as fTienHienVatTruocDieuChinh,
	rs.sCPChiTietToi,
	rs.sDuToanChiTietToi,
	rs.Type,
	rs.iID_MaDonVi,
	rs.sTenDonVi,
	rs.sSoQuyetDinh,
	rs.bHangCha,
	rs.IsRemainRow
	
	FROM #result rs
	LEFT JOIN #tmpNhanDuToan dt ON rs.iID_MLNS = dt.iID_MucLucNganSach 
	LEFT JOIN #tmpDaPhanBo dpb ON dpb.iID_MucLucNganSach = rs.iID_MLNS and dpb.iID_DTC_DuToanChiTrenGiao = rs.iID_DTC_DuToanChiTrenGiao
	LEFT JOIN (
	SELECT SUM(fTienTuChi) fTuChi, iID_MucLucNganSach FROM BH_DTC_PhanBoDuToanChi_ChiTiet WHERE iID_DTC_PhanBoDuToanChi = @ChungTuId GROUP BY iID_MucLucNganSach

	) ct ON ct.iID_MucLucNganSach = rs.iID_MLNS
		order by rs.sXauNoiMa, rs.sSoQuyetDinh, rs.iID_MaDonVi,rs.Type,rs.IsRemainRow

	--SELECT * from #tblSoChuaPhanBo


drop table #tblMucLucNganSach;
drop table #tblDonVi;
drop table #tblChungTuNhanPhanBo;
drop table #tblChiTietDuToanNhan;
drop table #tbl_tblChiTietDuToanNhan_MucLuc;
drop table #temp;
drop table #temp1;
drop table #tblSoChuaPhanBo;
drop table #tblMucLucNganSach_duplicate;

end
;
;
GO




/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]    Script Date: 1/23/2024 11:38:25 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]    Script Date: 1/23/2024 11:38:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_om_dau]
	@DsMaDonVi nvarchar(100),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN
	--Lay thong tin luong theo tro cap om dau
	select * into TBL_TCOD from
	(select luongcancu.fLuongCanCu, chedo.fSoNgayHuongBHXH, donvi.Ma_DonVi, donvi.Ten_DonVi, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	join TL_CanBo_CheDoBHXH chedo on luong.sMaCBo = chedo.sMaCanBo and luong.sMaCheDo = chedo.sMaCheDo
	left join 
	(select Ma_CBo, NAM, thang, sum(Gia_Tri) fLuongCanCu from TL_BangLuong_Thang
		where NAM = @NamLamViec
		and thang = @Thang
		and Ma_PhuCap in ('LHT_TT', 'PCCV_TT', 'PCTN_TT', 'PCTNVK_TT', 'HSBL_TT')
		group by Ma_CBo, NAM, thang) luongcancu on luong.sMaCBo = luongcancu.Ma_CBo
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('BENHDAINGAY_D14NGAY', 'BENHDAINGAY_T14NGAY', 'OMKHAC_D14NGAY', 'OMKHAC_T14NGAY', 'CONOM', 'OMDAU_DUONGSUCPHSK')) tcod

	--Lay gia tri phai tru BHXH
	select sMaCBo, sMaDonVi, sum(nGiaTri) nGiaTri
	into TBL_MINUS_BHXH
	from TL_BangLuong_ThangBHXH
	where upper(sMaCheDo) in ('OK_D14N_BHXHCN_TT','BDN_D14N_BHXHCN_TT')
		and sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
		and iNam = @NamLamViec
		and iThang = @Thang
	group by sMaCBo, sMaDonVi

	--Lay gia tri phai tru BHYT
	select sMaCBo, sMaDonVi, sum(nGiaTri) nGiaTri
	into TBL_MINUS_BHYT
	from TL_BangLuong_ThangBHXH
	where upper(sMaCheDo) in ('BDN_D14N_BHYTCN_TT','OK_D14N_BHYTCN_TT')
		and sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
		and iNam = @NamLamViec
		and iThang = @Thang
	group by sMaCBo, sMaDonVi

	---
	select distinct 
		TBL_TCOD.sMaCB,
		TBL_TCOD.sMaCBo,
		--TBL_TCOD.sMaCheDo,
		TBL_TCOD.sTenCbo,
		TBL_TCOD.Ma_DonVi,
		TBL_TCOD.Ten_DonVi,
		BENHDAINGAYD14NGAY.SoNgayBENHDAINGAYD14NGAY SoNgayBenhDaiNgayD14Ngay,
		BENHDAINGAY_T14NGAY.SoNgayBenhDaiNgayT14Ngay,
		OMKHAC_D14NGAY.SoNgayOmKhacD14Ngay,
		OMKHAC_T14NGAY.SoNgayOmKhacT14Ngay,
		CONOM.SoNgayConOm,
		DUONGSUCPHSK.SoNgayDuongSuc,
		TBL_TCOD.fLuongCanCu,
		BENHDAINGAYD14NGAY.nGiaTri fBENHDAINGAY_D14NGAY,
		BENHDAINGAY_T14NGAY.nGiaTri fBENHDAINGAY_T14NGAY,
		OMKHAC_D14NGAY.nGiaTri fOMKHAC_D14NGAY,
		OMKHAC_T14NGAY.nGiaTri fOMKHAC_T14NGAY,
		CONOM.nGiaTri fCONOM,
		DUONGSUCPHSK.nGiaTri fDUONGSUCPHSK
		into TBL_TCOD_DOC
	from TBL_TCOD TBL_TCOD
		left join
		(select tcod.sMaDonVi, tcod.nGiaTri, tcod.sMaCB, tcod.sMaCBo, tcod.sTenCbo, tcod.fSoNgayHuongBHXH SoNgayBenhDaiNgayT14Ngay
		from TBL_TCOD tcod
		where tcod.sMaCheDo = 'BENHDAINGAY_T14NGAY') BENHDAINGAY_T14NGAY
		on TBL_TCOD.sMaCBo = BENHDAINGAY_T14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = BENHDAINGAY_T14NGAY.sMaDonVi
		left join
		(select tcod_1.sMaDonVi, tcod_1.nGiaTri, tcod_1.sMaCB, tcod_1.sMaCBo, tcod_1.sTenCbo, tcod_1.fSoNgayHuongBHXH SoNgayOmKhacD14Ngay
		from TBL_TCOD tcod_1
		where tcod_1.sMaCheDo = 'OMKHAC_D14NGAY') OMKHAC_D14NGAY
		on TBL_TCOD.sMaCBo = OMKHAC_D14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = OMKHAC_D14NGAY.sMaDonVi
		left join
		(select tcod_2.sMaDonVi, tcod_2.nGiaTri, tcod_2.sMaCB, tcod_2.sMaCBo, tcod_2.sTenCbo, tcod_2.fSoNgayHuongBHXH SoNgayOmKhacT14Ngay
		from TBL_TCOD tcod_2
		where tcod_2.sMaCheDo = 'OMKHAC_T14NGAY') OMKHAC_T14NGAY
		on TBL_TCOD.sMaCBo = OMKHAC_T14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = OMKHAC_T14NGAY.sMaDonVi
		left join
		(select conom.sMaDonVi, conom.nGiaTri, conom.sMaCB, conom.sMaCBo, conom.sTenCbo, conom.fSoNgayHuongBHXH SoNgayConOm
		from TBL_TCOD conom
		where conom.sMaCheDo = 'CONOM') CONOM
		on TBL_TCOD.sMaCBo = CONOM.sMaCBo and TBL_TCOD.sMaDonVi = CONOM.sMaDonVi
		left join
		(select duongsuc.sMaDonVi, duongsuc.nGiaTri, duongsuc.sMaCB, duongsuc.sMaCBo, duongsuc.sTenCbo, duongsuc.fSoNgayHuongBHXH SoNgayDuongSuc
		from TBL_TCOD duongsuc
		where duongsuc.sMaCheDo = 'OMDAU_DUONGSUCPHSK') DUONGSUCPHSK
		on TBL_TCOD.sMaCBo = DUONGSUCPHSK.sMaCBo and TBL_TCOD.sMaDonVi = DUONGSUCPHSK.sMaDonVi
		left join
		(select tcod_3.sMaDonVi, tcod_3.nGiaTri, tcod_3.sMaCB, tcod_3.sMaCBo, tcod_3.sTenCbo, tcod_3.fSoNgayHuongBHXH SoNgayBENHDAINGAYD14NGAY
		from TBL_TCOD tcod_3
		where tcod_3.sMaCheDo = 'BENHDAINGAY_D14NGAY') BENHDAINGAYD14NGAY
		on TBL_TCOD.sMaCBo = BENHDAINGAYD14NGAY.sMaCBo and TBL_TCOD.sMaDonVi = BENHDAINGAYD14NGAY.sMaDonVi

	--Lấy lương Sĩ quan
	select * into TBL_TCOD_SQ from
	(select
		1 bHangCha, 1 RowNum, 'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB like '1%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_SQ) > 1
		update TBL_TCOD_SQ set bHasData = 1

	--Lấy lương QNCN
	select * into TBL_TCOD_QNCN from
	(select
		1 bHangCha, 2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB like '2%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_QNCN) > 1
		update TBL_TCOD_QNCN set bHasData = 1

	--Lấy lương HSQ_BS
	select * into TBL_TCOD_HSQBS from
	(select
		1 bHangCha, 3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB like '0%' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_HSQBS) > 1
		update TBL_TCOD_HSQBS set bHasData = 1

	--Lấy lương VCQP
	select * into TBL_TCOD_VCQP from
	(select
		1 bHangCha, 4 RowNum, 'IV' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB in ('3.1', '3.2', '3.3') and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_VCQP) > 1
		update TBL_TCOD_VCQP set bHasData = 1

	--Lấy lương Lao động hợp đông
	select * into TBL_TCOD_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null SoNgayBenhDaiNgayD14Ngay, null SoNgayBenhDaiNgayT14Ngay, null SoNgayOmKhacD14Ngay, null SoNgayOmKhacT14Ngay, null SoNgayConOm, null SoNgayDuongSuc, null fLuongCanCu, null fBENHDAINGAY_D14NGAY, null fBENHDAINGAY_T14NGAY, null fOMKHAC_D14NGAY, null fOMKHAC_T14NGAY, null fCONOM, null fDUONGSUCPHSK, 0 bHasData
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK, 0 bHasData
	from TBL_TCOD_DOC
	where sMaCB = '43' and (isnull(fLuongCanCu, 0) <> 0 or isnull(fBENHDAINGAY_D14NGAY, 0) <> 0 or isnull(fBENHDAINGAY_T14NGAY, 0) <> 0 or isnull(fOMKHAC_D14NGAY, 0) <> 0 or isnull(fOMKHAC_T14NGAY, 0) <> 0 or isnull(fCONOM, 0) <> 0  or isnull(fDUONGSUCPHSK, 0) <> 0)) ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCOD_LDHD) > 1
		update TBL_TCOD_LDHD set bHasData = 1

	--Ket qua
	select result.* into TBL_TCOD_RESULT from
	(select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_SQ
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_QNCN
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_HSQBS
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_VCQP
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, SoNgayBenhDaiNgayD14Ngay, SoNgayBenhDaiNgayT14Ngay, SoNgayOmKhacD14Ngay, SoNgayOmKhacT14Ngay, SoNgayConOm, SoNgayDuongSuc, fLuongCanCu, fBENHDAINGAY_D14NGAY, fBENHDAINGAY_T14NGAY, fOMKHAC_D14NGAY, fOMKHAC_T14NGAY, fCONOM, fDUONGSUCPHSK,
	(isnull(fBENHDAINGAY_D14NGAY, 0) + isnull(fBENHDAINGAY_T14NGAY, 0) + isnull(fOMKHAC_D14NGAY, 0) + isnull(fOMKHAC_T14NGAY, 0) + isnull(fCONOM, 0) + isnull(fDUONGSUCPHSK, 0)) fTongSoTien
	from TBL_TCOD_LDHD) result

	select
		result.RowNum,
		result.STT,
		result.DoiTuong, 
		result.LoaiDoiTuong,
		result.sMaCB MaCb, 
		result.sMaCBo MaCbo,
		result.sTenCbo TenCbo,
		result.Ma_DonVi MaDonVi,
		result.TenDonVi,
		result.SoNgayBenhDaiNgayD14Ngay, 
		result.SoNgayBenhDaiNgayT14Ngay, 
		result.SoNgayOmKhacD14Ngay, 
		result.SoNgayOmKhacT14Ngay, 
		result.SoNgayConOm, 
		result.SoNgayDuongSuc, 
		result.fLuongCanCu/@DonViTinh FLuongCanCu, 
		result.fBENHDAINGAY_D14NGAY/@DonViTinh FBenhDaiNgayD14Ngay, 
		result.fBENHDAINGAY_T14NGAY/@DonViTinh FBenhDauNgayT14Ngay, 
		result.fOMKHAC_D14NGAY/@DonViTinh FOmKhacD14Ngay, 
		result.fOMKHAC_T14NGAY/@DonViTinh FOmKhacT14Ngay, 
		result.fCONOM/@DonViTinh FConOm, 
		result.fDUONGSUCPHSK/@DonViTinh FDuongSucPHSK,
		result.fTongSoTien/@DonViTinh FTongSoTien,
		minus.nGiaTri/@DonViTinh FSoPhaiTruBHXH,
		bhyt.nGiaTri/@DonViTinh FSoPhaiTruBHYT,
		(isnull(minus.nGiaTri, 0) + isnull(bhyt.nGiaTri, 0))/@DonViTinh FTongPhaiTru,
		(isnull(result.fTongSoTien, 0) - (isnull(minus.nGiaTri, 0) + isnull(bhyt.nGiaTri, 0)))/@DonViTinh FDuocNhan,
		result.bHangCha IsHangCha,
		result.bHasData IsHasData
	from TBL_TCOD_RESULT result
	left join TBL_MINUS_BHXH minus on result.sMaCBo = minus.sMaCBo and result.Ma_DonVi = minus.sMaDonVi
	left join TBL_MINUS_BHYT bhyt on result.sMaCBo = bhyt.sMaCBo and result.Ma_DonVi = bhyt.sMaDonVi
	order by RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD]') AND type in (N'U')) drop table TBL_TCOD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_MINUS_BHXH]') AND type in (N'U')) drop table TBL_MINUS_BHXH;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_MINUS_BHYT]') AND type in (N'U')) drop table TBL_MINUS_BHYT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_DOC]') AND type in (N'U')) drop table TBL_TCOD_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_SQ]') AND type in (N'U')) drop table TBL_TCOD_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_QNCN]') AND type in (N'U')) drop table TBL_TCOD_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_HSQBS]') AND type in (N'U')) drop table TBL_TCOD_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_VCQP]') AND type in (N'U')) drop table TBL_TCOD_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_LDHD]') AND type in (N'U')) drop table TBL_TCOD_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCOD_RESULT]') AND type in (N'U')) drop table TBL_TCOD_RESULT;

END
;
;
;
;
;
;
GO
