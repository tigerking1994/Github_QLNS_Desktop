/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_qtt_thong_tri_giai_thich_truy_thu_donvi]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_rpt_qtt_thong_tri_giai_thich_truy_thu_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_rpt_qtt_thong_tri_giai_thich_truy_thu_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_qtt_thong_tri_giai_thich_truy_thu]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_rpt_qtt_thong_tri_giai_thich_truy_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_rpt_qtt_thong_tri_giai_thich_truy_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_qtt_thong_tri]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_qtt_thong_tri]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_qtt_thong_tri]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu_tong_hop_don_vi]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu_tong_hop_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu_tong_hop_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_quy]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_can_cu_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_nam]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_can_cu_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_truy_thu]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_giai_thich_truy_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_truy_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]    Script Date: 9/9/2024 2:05:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_tong_hop_so_sanh]    Script Date: 9/9/2024 2:05:00 PM ******/
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
			--join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			join BH_QTT_BHXH_CTCT_GiaiThich ctgt on ct.iID_QTT_BHXH_ChungTu = ctgt.iID_QTT_BHXH_ChungTu
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_truy_thu]    Script Date: 9/9/2024 2:05:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_truy_thu]
	@NamLamViec int,
	@VoucherID uniqueidentifier,
	@MaDonVi nvarchar(50),
	@VoucherType int
AS
BEGIN

	select
		sum(isnull(fTruyThu_BHXH_NLD, 0)) FTruyThuBHXHNLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) FTruyThuBHXHNSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) FTruyThuBHXHTongCong,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) FTruyThuBHYTNLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) FTruyThuBHYTNSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) FTruyThuBHYTTongCong,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) FTruyThuBHTNNLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) FTruyThuBHTNNSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) FTruyThuBHTNTongCong,
		((sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) + (sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) + (sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0)))) FTongTruyThuBHXH,
		sum(isnull(fLuongChinh, 0)) FLuongChinh,
		sum(isnull(fPCChucVu, 0)) FPCChucVu,
		sum(isnull(fPCTNNghe, 0)) FPCTNNghe,
		sum(isnull(fPCTNVuotKhung, 0)) FPCTNVuotKhung,
		sum(isnull(fNghiOm, 0)) FNghiOm,
		sum(isnull(fHSBL, 0)) FHSBL,
		iID_MaDonVi IIDMaDonVi,
		sXauNoiMa SXauNoiMa,
		1 IsModified
	into #MonthQuarterData
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iNamLamViec = @NamLamViec
		and iQuyNamLoai <> 2
		and iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	group by iID_MaDonVi, sXauNoiMa
	
	select
			chungtudonvi.iID_QT_CTCT_GiaiThich,
			mlns.iID_MLNS,
			mlns.sMoTa sNoiDung,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sLNS,
			mlns.sXauNoiMa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			chungtudonvi.fTruyThu_BHXH_NLD fTruyThu_BHXH_NLD,
			chungtudonvi.fTruyThu_BHXH_NSD fTruyThu_BHXH_NSD,
			chungtudonvi.fTruyThu_BHXH_TongCong fTruyThu_BHXH_TongCong,
			chungtudonvi.fTruyThu_BHYT_NLD fTruyThu_BHYT_NLD,
			chungtudonvi.fTruyThu_BHYT_NSD fTruyThu_BHYT_NSD,
			chungtudonvi.fTruyThu_BHYT_TongCong fTruyThu_BHYT_TongCong,
			chungtudonvi.fTruyThu_BHTN_NLD fTruyThu_BHTN_NLD,
			chungtudonvi.fTruyThu_BHTN_NSD fTruyThu_BHTN_NSD,
			chungtudonvi.fTruyThu_BHTN_TongCong fTruyThu_BHTN_TongCong,
			(chungtudonvi.fTruyThu_BHXH_TongCong + chungtudonvi.fTruyThu_BHYT_TongCong + chungtudonvi.fTruyThu_BHTN_TongCong) fTongTruyThu_BHXH,
			chungtudonvi.sKienNghi,
			chungtudonvi.fPhaiNop_BHXH FPhaiNopBHXH,
			chungtudonvi.fPhaiNop_TrongQuyNam FPhaiNopTrongQuyNam,
			chungtudonvi.fTruyThu_QuyNamTruoc FTruyThuQuyNamTruoc,
			chungtudonvi.fPhaiNop_QuyNamTruoc FPhaiNopQuyNamTruoc,
			chungtudonvi.fDaNop_TrongQuyNam FDaNopTrongQuyNam,
			chungtudonvi.fConPhaiNopTiep,
			chungtudonvi.fSoPhaiThuNop,
			chungtudonvi.fSoDaNopTrongNam,
			chungtudonvi.fSoDaNopSau31_12,
			chungtudonvi.fTongSoDaNop,
			chungtudonvi.fSoConPhaiNop,
			chungtudonvi.iQuanSo,
			chungtudonvi.fQuyTienLuongCanCu,
			chungtudonvi.fSoTienGiamDong,
			chungtudonvi.dTuNgay,
			chungtudonvi.dDenNgay,
			chungtudonvi.sNguoiTao,
			chungtudonvi.dNgayTao,
			chungtudonvi.dNgaySua,
			chungtudonvi.sNguoiSua,
			case when @VoucherType = 2 and isnull(chungtudonvi.fLuongChinh, 0) = 0 then dttq.FLuongChinh
			else chungtudonvi.fLuongChinh end FLuongChinh,
			case when @VoucherType = 2 and isnull(chungtudonvi.fPCChucVu, 0) = 0 then dttq.fPCChucVu
			else chungtudonvi.fPCChucVu end FPCChucVu,
			case when @VoucherType = 2 and isnull(chungtudonvi.fPCTNNghe, 0) = 0 then dttq.fPCTNNghe
			else chungtudonvi.fPCTNNghe end FPCTNNghe,
			case when @VoucherType = 2 and isnull(chungtudonvi.fPCTNVuotKhung, 0) = 0 then dttq.fPCTNVuotKhung
			else chungtudonvi.fPCTNVuotKhung end FPCTNVuotKhung,
			case when @VoucherType = 2 and isnull(chungtudonvi.fNghiOm, 0) = 0 then dttq.fNghiOm
			else chungtudonvi.fNghiOm end FNghiOm,
			case when @VoucherType = 2 and isnull(chungtudonvi.fHSBL, 0) = 0 then dttq.fHSBL
			else chungtudonvi.fHSBL end FHSBL,
			case when @VoucherType = 2 and ((select count(1) from #MonthQuarterData) > 0) 
					and ((isnull(chungtudonvi.fLuongChinh, 0) = 0 and isnull(dttq.fLuongChinh, 0) <> 0) or (isnull(chungtudonvi.fPCChucVu, 0) = 0 and isnull(dttq.fPCChucVu, 0) <> 0) or (isnull(chungtudonvi.fPCTNNghe, 0) = 0 and isnull(dttq.fPCTNNghe, 0) <> 0) or (isnull(chungtudonvi.fPCTNVuotKhung, 0) = 0 and isnull(dttq.fPCTNVuotKhung, 0) <> 0) or (isnull(chungtudonvi.fNghiOm, 0) = 0 and isnull(dttq.fNghiOm, 0) <> 0) or (isnull(chungtudonvi.fHSBL, 0) = 0 and isnull(dttq.fHSBL, 0) <> 0)) then dttq.IsModified
			else 0 end IsModified
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
				ctgt.iID_MLNS,
				ctgt.fLuongChinh,
				ctgt.fPCChucVu,
				ctgt.fPCTNNghe,
				ctgt.fPCTNVuotKhung,
				ctgt.fNghiOm,
				ctgt.fHSBL
				from
				BH_QTT_BHXH_ChungTu ct
				--join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				join BH_QTT_BHXH_CTCT_GiaiThich ctgt on ct.iID_QTT_BHXH_ChungTu = ctgt.iID_QTT_BHXH_ChungTu
				where
					ct.iID_QTT_BHXH_ChungTu = @VoucherID
					and ctgt.iID_MaDonVi = @MaDonVi
				) chungtudonvi on mlns.iID_MLNS = chungtudonvi.iID_MLNS
				left join #MonthQuarterData dttq on mlns.sXauNoiMa = dttq.sXauNoiMa
			order by mlns.sXauNoiMa
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_nam]    Script Date: 9/9/2024 2:05:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_nam] 
	@MaDonVi nvarchar(50),
	@Year int
AS
BEGIN
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_y]') AND type in (N'U')) drop table tbl_cancu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach3_y]') AND type in (N'U')) drop table tbl_cancu_cach3_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach4_y]') AND type in (N'U')) drop table tbl_cancu_cach4_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_base_y]') AND type in (N'U')) drop table tbl_base_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_y]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_c3_y]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_c3_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_so_nguoi_f_y]') AND type in (N'U')) drop table tbl_so_nguoi_f_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_y]') AND type in (N'U')) drop table tbl_cancu_result_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final_y]') AND type in (N'U')) drop table tbl_cancu_result_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_y]') AND type in (N'U')) drop table tbl_luong_can_cu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final_y]') AND type in (N'U')) drop table tbl_luong_can_cu_final_y;
	
	-- Lấy dữ liệu quyết toán lương full công
	select
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi,
	 sum(ctct.DieuChinh) fGiaTri,
	 pc.Ma_Cb
	into tbl_cancu_y
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	join DonVi donvi on ct.Ma_DonVi = donvi.iID_MaDonVi
	where ctct.MaCachTl in ('CACH0', 'CACH5')
	and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
	and ct.Nam = @Year
	and pc.Nam = @Year
	and ct.Ma_DonVi = @MaDonVi
	and ct.bKhoa = 1
	and isnull(ct.STongHop, '') <> ''
	group by
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi,
	 pc.Ma_Cb

	 -- Lấy dữ liệu quyết toán quỹ lương không thuộc ốm dưới 14 ngày/tháng
	 select
		 pc.Ma_PhuCap,
		 pc.XauNoiMa,
		 donvi.iKhoi,
		 sum(isnull(ctct.DieuChinh, 0)) fGiaTri,
		 pc.Ma_Cb
	 into tbl_cancu_cach3_y
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	join DonVi donvi on ct.Ma_DonVi = donvi.iID_MaDonVi and donvi.iNamLamViec = ct.Nam and donvi.iTrangThai = 1
	where isnull(ctct.MaCachTl,'') = 'CACH3'
		and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
		and isnull(ct.STongHop, '') <> ''
	group by
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi,
	 pc.Ma_Cb

	 -- Lấy dữ liệu quyết toán quỹ lương BHXH chi trả
	 select
		 ctct.XauNoiMa,
		 donvi.iKhoi,
		 sum(isnull(ctct.DieuChinh, 0)) fGiaTri
	 into tbl_cancu_cach4_y
	from TL_QT_ChungTuChiTiet ctct
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	join DonVi donvi on ct.Ma_DonVi = donvi.iID_MaDonVi and donvi.iNamLamViec = ct.Nam and donvi.iTrangThai = 1
	where isnull(ctct.MaCachTl,'') = 'CACH4'
		and ct.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
		and isnull(ct.STongHop, '') <> ''
	group by
	 ctct.XauNoiMa,
		 donvi.iKhoi

	-- Lấy dữ liệu sang QTT = full công - BHXH chi trả - quỹ lương != ốm dưới 14 ngày
	 select distinct
		 c.Ma_PhuCap,
		 c.XauNoiMa,
		 c.iKhoi,
		 c.fGiaTri ChuaTru,
		 c3.fGiaTri Cach3,
		 (isnull(c.fGiaTri, 0) - isnull(c3.fGiaTri, 0) - isnull(c2.fGiaTri, 0)) as fGiaTri,
		 c.Ma_Cb
		  into tbl_base_y
	 from tbl_cancu_y c
	 left join tbl_cancu_cach3_y c3 on c.Ma_PhuCap = c3.Ma_PhuCap and c.XauNoiMa = c3.XauNoiMa and c.iKhoi = c3.iKhoi
	 left join tbl_cancu_cach4_y c2 on c.XauNoiMa = c2.XauNoiMa and c.iKhoi = c2.iKhoi
	---------------------------
	--Get so nguoi full công
	select
	 pc.XauNoiMa,
	 sum(ctct.SoNguoi) IQSBQNam
	  into tbl_cancu_so_nguoi_y
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
		and pc.Ma_PhuCap = 'LHT_TT'
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
		and isnull(ct.STongHop, '') <> ''
	group by
	 pc.XauNoiMa

	 --Get so nguoi không thuộc ốm dưới 14 ngày/tháng
	 select
	 pc.XauNoiMa,
	 sum(ctct.SoNguoi) IQSBQNam
	  into tbl_cancu_so_nguoi_c3_y
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = 'CACH3'
		and pc.Ma_PhuCap = 'LHT_TT'
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
		and isnull(ct.STongHop, '') <> ''
	group by
	 pc.XauNoiMa

	 --Get số người
	 select a.XauNoiMa, (isnull(a.IQSBQNam, 0) - isnull(b.IQSBQNam, 0)) IQSBQNam
	  into tbl_so_nguoi_f_y
	 from tbl_cancu_so_nguoi_y a
	 left join tbl_cancu_so_nguoi_c3_y b on a.XauNoiMa = b.XauNoiMa
	 ------------------------------------------

	 select distinct
	  cancu.XauNoiMa,
	  cancu.iKhoi,
	  songuoi.IQSBQNam IQSBQNam,
	  --songuoi.IQSBQNam/12 IQSBQNam,
	  luongchinh.fGiaTri FGiaTriLuongChinh,
	  pccv.fGiaTri FGiaTriPCCV,
	  pctn.fGiaTri FGiaTriPCTN,
	  pctnvk.fGiaTri FGiaTriPCTNVK,
	  hsbl.fGiaTri FGiaTriHSBL,
	  cancu.Ma_Cb
	 into tbl_cancu_result_y
	 from tbl_base_y cancu
	 left join tbl_base_y luongchinh on cancu.XauNoiMa = luongchinh.XauNoiMa and luongchinh.Ma_PhuCap = 'LHT_TT'
	 left join tbl_base_y pccv on cancu.XauNoiMa = pccv.XauNoiMa and pccv.Ma_PhuCap = 'PCCV_TT'
	 left join tbl_base_y pctn on cancu.XauNoiMa = pctn.XauNoiMa and pctn.Ma_PhuCap = 'PCTN_TT'
	 left join tbl_base_y pctnvk on cancu.XauNoiMa = pctnvk.XauNoiMa and pctnvk.Ma_PhuCap = 'PCTNVK_TT'
	 left join tbl_base_y hsbl on cancu.XauNoiMa = hsbl.XauNoiMa and hsbl.Ma_PhuCap = 'HSBL_TT'
	 left join tbl_cancu_so_nguoi_y songuoi on cancu.XauNoiMa = songuoi.XauNoiMa

	 select distinct
	  cancu.XauNoiMa,
	  cancu.iKhoi,
	  sum(cancu.IQSBQNam) IQSBQNam,
	  sum(cancu.FGiaTriLuongChinh) FGiaTriLuongChinh,
	  sum(cancu.FGiaTriPCCV) FGiaTriPCCV,
	  sum(cancu.FGiaTriPCTN) FGiaTriPCTN,
	  sum(cancu.FGiaTriPCTNVK) FGiaTriPCTNVK,
	  sum(cancu.FGiaTriHSBL) FGiaTriHSBL,
	  cancu.Ma_Cb
	 into tbl_cancu_result_final_y
	 from tbl_cancu_result_y cancu
	 group by cancu.XauNoiMa, cancu.iKhoi, cancu.Ma_Cb

	 --Luong BHXH
	 select
	  ctct.sMaCB XauNoiMa,
	  sum(ctct.nGiaTri) FNghiOm
	 into tbl_luong_can_cu_y
	 from TL_BangLuong_ThangBHXH ctct
	 join TL_DS_CapNhap_BangLuong ct on ctct.iID_Parent = ct.Id
	 where ctct.iNam = @Year
		and ctct.sMaDonVi = @MaDonVi
		and ctct.sMaCheDo in ('BENHDAINGAY_D14NGAY', 'OMKHAC_D14NGAY')
		and (ctct.sMaCB like '1%' or ctct.sMaCB like '2%' or ctct.sMaCB like '0%' or ctct.sMaCB = '43' or ctct.sMaCB = '423' or ctct.sMaCB = '425' or ctct.sMaCB in ('3.1', '3.2', '3.3', '413', '415'))
		and ct.KhoaBangLuong = 1
		and isnull(ct.STongHop, '') <> ''
	 group by ctct.sMaCB

	 update tbl_luong_can_cu_y set XauNoiMa = '1' where XauNoiMa like '1%'
	 update tbl_luong_can_cu_y set XauNoiMa = '2' where XauNoiMa like '2%'
	 update tbl_luong_can_cu_y set XauNoiMa = '4' where XauNoiMa like '0%'
	 update tbl_luong_can_cu_y set XauNoiMa = '3' where XauNoiMa in ('3.1', '3.2', '3.3', '413', '415')
	 update tbl_luong_can_cu_y set XauNoiMa = '43' where XauNoiMa in ('423', '425')

	 select
	  XauNoiMa,
	  sum(FNghiOm) FNghiOm
	 into tbl_luong_can_cu_final_y
	 from tbl_luong_can_cu_y
	 group by XauNoiMa

	 --result
	 select
	  luong.XauNoiMa sXauNoiMa,
	  luong.IQSBQNam,
	  luong.iKhoi,
	  luong.FGiaTriLuongChinh,
	  luong.FGiaTriPCCV,
	  luong.FGiaTriPCTN,
	  luong.FGiaTriPCTNVK,
	  luong.FGiaTriHSBL,
	  CAST(bhxh.FNghiOm AS FLOAT) FNghiOm,
	  luong.Ma_Cb SMaCapBac
	 from tbl_cancu_result_final_y luong
	 left join tbl_luong_can_cu_final_y bhxh on luong.XauNoiMa = bhxh.XauNoiMa
	 -----------------------------------------------
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_y]') AND type in (N'U')) drop table tbl_cancu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach3_y]') AND type in (N'U')) drop table tbl_cancu_cach3_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach4_y]') AND type in (N'U')) drop table tbl_cancu_cach4_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_base_y]') AND type in (N'U')) drop table tbl_base_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_y]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_c3_y]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_c3_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_so_nguoi_f_y]') AND type in (N'U')) drop table tbl_so_nguoi_f_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_y]') AND type in (N'U')) drop table tbl_cancu_result_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final_y]') AND type in (N'U')) drop table tbl_cancu_result_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_y]') AND type in (N'U')) drop table tbl_luong_can_cu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final_y]') AND type in (N'U')) drop table tbl_luong_can_cu_final_y;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_quy]    Script Date: 9/9/2024 2:05:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_quy] 
	@MaDonVi nvarchar(50),
	@Year int,
	@Months nvarchar(20),
	@LoaiQuyNam int
AS
BEGIN

	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu]') AND type in (N'U')) drop table tbl_cancu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach3]') AND type in (N'U')) drop table tbl_cancu_cach3;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach4]') AND type in (N'U')) drop table tbl_cancu_cach4;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_base]') AND type in (N'U')) drop table tbl_base;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi]') AND type in (N'U')) drop table tbl_cancu_so_nguoi;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_c3]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_c3;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result]') AND type in (N'U')) drop table tbl_cancu_result;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final]') AND type in (N'U')) drop table tbl_cancu_result_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu]') AND type in (N'U')) drop table tbl_luong_can_cu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final]') AND type in (N'U')) drop table tbl_luong_can_cu_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_so_nguoi_f]') AND type in (N'U')) drop table tbl_so_nguoi_f;

	-- Lấy dữ liệu quyết toán lương full công
	select
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi,
	 sum(isnull(ctct.DieuChinh, 0)) fGiaTri,
	 pc.Ma_Cb
	 into tbl_cancu
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	join DonVi donvi on ct.Ma_DonVi = donvi.iID_MaDonVi and donvi.iNamLamViec = ct.Nam and donvi.iTrangThai = 1
	where ctct.MaCachTl in ('CACH0', 'CACH5')
		and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
		and ct.Thang in (SELECT * FROM f_split(@Months))
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
		and isnull(ct.STongHop, '') <> ''
	group by
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi,
	 pc.Ma_Cb

	 -----------------
	 -- Lấy dữ liệu quyết toán quỹ lương không thuộc ốm dưới 14 ngày/tháng
	 select
		 pc.Ma_PhuCap,
		 pc.XauNoiMa,
		 donvi.iKhoi,
		 sum(isnull(ctct.DieuChinh, 0)) fGiaTri,
		 pc.Ma_Cb
	 into tbl_cancu_cach3
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	join DonVi donvi on ct.Ma_DonVi = donvi.iID_MaDonVi and donvi.iNamLamViec = ct.Nam and donvi.iTrangThai = 1
	where isnull(ctct.MaCachTl,'') = 'CACH3'
		and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
		and ct.Thang in (SELECT * FROM f_split(@Months))
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
		and isnull(ct.STongHop, '') <> ''
	group by
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi,
	 pc.Ma_Cb

	 -----------------
	 -- Lấy dữ liệu quyết toán quỹ lương BHXH chi trả
	 select
		 ctct.XauNoiMa,
		 donvi.iKhoi,
		 sum(isnull(ctct.DieuChinh, 0)) fGiaTri
	 into tbl_cancu_cach4
	from TL_QT_ChungTuChiTiet ctct
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	join DonVi donvi on ct.Ma_DonVi = donvi.iID_MaDonVi and donvi.iNamLamViec = ct.Nam and donvi.iTrangThai = 1
	where isnull(ctct.MaCachTl,'') = 'CACH4'
		and ct.Thang in (SELECT * FROM f_split(@Months))
		and ct.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
		and isnull(ct.STongHop, '') <> ''
	group by
	 ctct.XauNoiMa,
		 donvi.iKhoi
	 -------------------
	 -- Lấy dữ liệu sang QTT = full công - BHXH chi trả - quỹ lương != ốm dưới 14 ngày
	 select distinct
		 c.Ma_PhuCap,
		 c.XauNoiMa,
		 c.iKhoi,
		 c.fGiaTri ChuaTru,
		 c3.fGiaTri Cach3,
		 (isnull(c.fGiaTri, 0) - isnull(c3.fGiaTri, 0) - isnull(c2.fGiaTri, 0)) as fGiaTri,
		 c.Ma_Cb
		  into tbl_base
	 from tbl_cancu c
	 left join tbl_cancu_cach3 c3 on c.Ma_PhuCap = c3.Ma_PhuCap and c.XauNoiMa = c3.XauNoiMa and c.iKhoi = c3.iKhoi
	 left join tbl_cancu_cach4 c2 on c.XauNoiMa = c2.XauNoiMa and c.iKhoi = c2.iKhoi
	 --------------------------
	--Get so nguoi full công
	select
	 pc.XauNoiMa,
	 sum(ctct.SoNguoi) IQSBQNam
	  into tbl_cancu_so_nguoi
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
		and pc.Ma_PhuCap = 'LHT_TT'
		and ct.Thang in (SELECT * FROM f_split(@Months))
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
		and isnull(ct.STongHop, '') <> ''
	group by
	 pc.XauNoiMa

	 --Get so nguoi không thuộc ốm dưới 14 ngày/tháng
	 select
	 pc.XauNoiMa,
	 sum(ctct.SoNguoi) IQSBQNam
	  into tbl_cancu_so_nguoi_c3
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = 'CACH3'
		and pc.Ma_PhuCap = 'LHT_TT'
		and ct.Thang in (SELECT * FROM f_split(@Months))
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
		and isnull(ct.STongHop, '') <> ''
	group by
	 pc.XauNoiMa
	 --Get số người
	 select a.XauNoiMa, (isnull(a.IQSBQNam, 0) - isnull(b.IQSBQNam, 0)) IQSBQNam
	  into tbl_so_nguoi_f
	 from tbl_cancu_so_nguoi a
	 left join tbl_cancu_so_nguoi_c3 b on a.XauNoiMa = b.XauNoiMa

	 ------------------------------------------
	 select distinct
	  cancu.XauNoiMa,
	  cancu.iKhoi,
	  case
		when @LoaiQuyNam = 0 then songuoi.IQSBQNam
		else songuoi.IQSBQNam/3
	  end as IQSBQNam,
	  luongchinh.fGiaTri FGiaTriLuongChinh,
	  pccv.fGiaTri FGiaTriPCCV,
	  pctn.fGiaTri FGiaTriPCTN,
	  pctnvk.fGiaTri FGiaTriPCTNVK,
	  hsbl.fGiaTri FGiaTriHSBL,
	  cancu.Ma_Cb
	  into tbl_cancu_result
	 from tbl_base cancu
	 left join tbl_base luongchinh on cancu.XauNoiMa = luongchinh.XauNoiMa and luongchinh.Ma_PhuCap = 'LHT_TT'
	 left join tbl_base pccv on cancu.XauNoiMa = pccv.XauNoiMa and pccv.Ma_PhuCap = 'PCCV_TT'
	 left join tbl_base pctn on cancu.XauNoiMa = pctn.XauNoiMa and pctn.Ma_PhuCap = 'PCTN_TT'
	 left join tbl_base pctnvk on cancu.XauNoiMa = pctnvk.XauNoiMa and pctnvk.Ma_PhuCap = 'PCTNVK_TT'
	 left join tbl_base hsbl on cancu.XauNoiMa = hsbl.XauNoiMa and hsbl.Ma_PhuCap = 'HSBL_TT'
	 left join tbl_so_nguoi_f songuoi on cancu.XauNoiMa = songuoi.XauNoiMa

	 select distinct
	  cancu.XauNoiMa,
	  cancu.iKhoi,
	  sum(cancu.IQSBQNam) IQSBQNam,
	  sum(cancu.FGiaTriLuongChinh) FGiaTriLuongChinh,
	  sum(cancu.FGiaTriPCCV) FGiaTriPCCV,
	  sum(cancu.FGiaTriPCTN) FGiaTriPCTN,
	  sum(cancu.FGiaTriPCTNVK) FGiaTriPCTNVK,
	  sum(cancu.FGiaTriHSBL) FGiaTriHSBL,
	  cancu.Ma_Cb
	  into tbl_cancu_result_final
	 from tbl_cancu_result cancu
	 group by cancu.XauNoiMa, cancu.iKhoi, cancu.Ma_Cb

	 --Luong BHXH
	 select
	  ctct.sMaCB XauNoiMa,
	  sum(ctct.nGiaTri) FNghiOm
	  into tbl_luong_can_cu
	 from TL_BangLuong_ThangBHXH ctct
	 join TL_DS_CapNhap_BangLuong ct on ctct.iID_Parent = ct.Id
	 where ctct.iNam = @Year
		and ctct.sMaDonVi = @MaDonVi
		and ctct.iThang in (SELECT * FROM f_split(@Months))
		and ctct.sMaCheDo in ('BENHDAINGAY_D14NGAY', 'OMKHAC_D14NGAY')
		and (ctct.sMaCB like '1%' or ctct.sMaCB like '2%' or ctct.sMaCB like '0%' or ctct.sMaCB = '43' or ctct.sMaCB = '423' or ctct.sMaCB = '425' or ctct.sMaCB in ('3.1', '3.2', '3.3', '413', '415'))
		and ct.KhoaBangLuong = 1
		and isnull(ct.STongHop, '') <> ''
	 group by ctct.sMaCB

	update tbl_luong_can_cu set XauNoiMa = '1' where XauNoiMa like '1%'
	update tbl_luong_can_cu set XauNoiMa = '2' where XauNoiMa like '2%'
	update tbl_luong_can_cu set XauNoiMa = '4' where XauNoiMa like '0%'
	update tbl_luong_can_cu set XauNoiMa = '3' where XauNoiMa in ('3.1', '3.2', '3.3', '413', '415')
	update tbl_luong_can_cu set XauNoiMa = '43' where XauNoiMa in ('423', '425')

	select
	  XauNoiMa,
	  sum(FNghiOm) FNghiOm
	  into tbl_luong_can_cu_final
	 from tbl_luong_can_cu
	 group by XauNoiMa

	 --result
	 select
	  luong.XauNoiMa sXauNoiMa,
	  luong.IQSBQNam,
	  luong.iKhoi,
	  luong.FGiaTriLuongChinh,
	  luong.FGiaTriPCCV,
	  luong.FGiaTriPCTN,
	  luong.FGiaTriPCTNVK,
	  luong.FGiaTriHSBL,
	  CAST(bhxh.FNghiOm AS FLOAT) FNghiOm,
	  luong.Ma_Cb SMaCapBac
	 from tbl_cancu_result_final luong
	 left join tbl_luong_can_cu_final bhxh on luong.Ma_Cb = bhxh.XauNoiMa
	 -----------------------------------------------

	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu]') AND type in (N'U')) drop table tbl_cancu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach3]') AND type in (N'U')) drop table tbl_cancu_cach3;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_cach4]') AND type in (N'U')) drop table tbl_cancu_cach4;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_base]') AND type in (N'U')) drop table tbl_base;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi]') AND type in (N'U')) drop table tbl_cancu_so_nguoi;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_c3]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_c3;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result]') AND type in (N'U')) drop table tbl_cancu_result;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final]') AND type in (N'U')) drop table tbl_cancu_result_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu]') AND type in (N'U')) drop table tbl_luong_can_cu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final]') AND type in (N'U')) drop table tbl_luong_can_cu_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_so_nguoi_f]') AND type in (N'U')) drop table tbl_so_nguoi_f;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]    Script Date: 9/9/2024 2:05:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]
	@NamLamViec int,
	@MaDonVi nvarchar(50)
AS
BEGIN
	
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dutoan_tbl]') AND type in (N'U')) drop table dutoan_tbl
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hachtoan_tbl]') AND type in (N'U')) drop table hachtoan_tbl

	--Tạo dữ liệu khói dự toán
	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sMoTa,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sXauNoiMa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		chungtudonvi.fTruyThu_BHXH_NLD,
		chungtudonvi.fTruyThu_BHXH_NSD,
		chungtudonvi.fTruyThu_BHXH_TongCong,
		chungtudonvi.fTruyThu_BHYT_NLD,
		chungtudonvi.fTruyThu_BHYT_NSD,
		chungtudonvi.fTruyThu_BHYT_TongCong,
		chungtudonvi.fTruyThu_BHTN_NLD,
		chungtudonvi.fTruyThu_BHTN_NSD,
		chungtudonvi.fTruyThu_BHTN_TongCong
	into dutoan_tbl
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
		where sLNS = '9020001' and sXauNoiMa <> '9020001' -- Khối dự toán
		and iNamLamViec = @NamLamViec
		and iID_MLNS_Cha is not null
		and iTrangThai = 1) mlns
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
			ctgt.fTruyThu_BHXH_NLD,
			ctgt.fTruyThu_BHXH_NSD,
			ctgt.fTruyThu_BHXH_TongCong,
			ctgt.fTruyThu_BHYT_NLD,
			ctgt.fTruyThu_BHYT_NSD,
			ctgt.fTruyThu_BHYT_TongCong,
			ctgt.fTruyThu_BHTN_NLD,
			ctgt.fTruyThu_BHTN_NSD,
			ctgt.fTruyThu_BHTN_TongCong,
			ctgt.iID_MLNS
		from BH_QTT_BHXH_CTCT_GiaiThich ctgt
		where ctgt.iLoaiGiaiThich = 2
			and ctgt.iQuyNam = @NamLamViec
			and ctgt.iID_MaDonVi = @MaDonVi) chungtudonvi 
	on mlns.iID_MLNS = chungtudonvi.iID_MLNS
---------
--Tạo dữ liệu khói hạch toán
	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sMoTa,
		mlns.bHangCha,
		mlns.sLNS,
		replace(mlns.sXauNoiMa, '9020002-', '9020001-') sXauNoiMa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		chungtudonvi.fTruyThu_BHXH_NLD,
		chungtudonvi.fTruyThu_BHXH_NSD,
		chungtudonvi.fTruyThu_BHXH_TongCong,
		chungtudonvi.fTruyThu_BHYT_NLD,
		chungtudonvi.fTruyThu_BHYT_NSD,
		chungtudonvi.fTruyThu_BHYT_TongCong,
		chungtudonvi.fTruyThu_BHTN_NLD,
		chungtudonvi.fTruyThu_BHTN_NSD,
		chungtudonvi.fTruyThu_BHTN_TongCong
	into hachtoan_tbl
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
		where sLNS = '9020002' and sXauNoiMa <> '9020002' -- Khối hạch toán
		and iNamLamViec = @NamLamViec
		and iID_MLNS_Cha is not null
		and iTrangThai = 1) mlns
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
			ctgt.fTruyThu_BHXH_NLD,
			ctgt.fTruyThu_BHXH_NSD,
			ctgt.fTruyThu_BHXH_TongCong,
			ctgt.fTruyThu_BHYT_NLD,
			ctgt.fTruyThu_BHYT_NSD,
			ctgt.fTruyThu_BHYT_TongCong,
			ctgt.fTruyThu_BHTN_NLD,
			ctgt.fTruyThu_BHTN_NSD,
			ctgt.fTruyThu_BHTN_TongCong,
			ctgt.iID_MLNS
		from BH_QTT_BHXH_CTCT_GiaiThich ctgt
		where ctgt.iLoaiGiaiThich = 2
			and ctgt.iQuyNam = @NamLamViec
			and ctgt.iID_MaDonVi = @MaDonVi) chungtudonvi 
	on mlns.iID_MLNS = chungtudonvi.iID_MLNS
------------
	select
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.sMoTa,
		ml.bHangCha,
		ml.sLNS,
		ml.sXauNoiMa,
		isnull(dt.fTruyThu_BHXH_NLD, 0) fTruyThu_BHXH_NLD_DT,
		isnull(dt.fTruyThu_BHXH_NSD, 0) fTruyThu_BHXH_NSD_DT,
		isnull(dt.fTruyThu_BHXH_TongCong, 0) fTruyThu_BHXH_TongCong_DT,
		isnull(dt.fTruyThu_BHYT_NLD, 0) fTruyThu_BHYT_NLD_DT,
		isnull(dt.fTruyThu_BHYT_NSD, 0) fTruyThu_BHYT_NSD_DT,
		isnull(dt.fTruyThu_BHYT_TongCong, 0) fTruyThu_BHYT_TongCong_DT,
		isnull(dt.fTruyThu_BHTN_NLD, 0) fTruyThu_BHTN_NLD_DT,
		isnull(dt.fTruyThu_BHTN_NSD, 0) fTruyThu_BHTN_NSD_DT,
		isnull(dt.fTruyThu_BHTN_TongCong, 0) fTruyThu_BHTN_TongCong_DT,
		
		isnull(ht.fTruyThu_BHXH_NLD, 0) fTruyThu_BHXH_NLD_HT,
		isnull(ht.fTruyThu_BHXH_NSD, 0) fTruyThu_BHXH_NSD_HT,
		isnull(ht.fTruyThu_BHXH_TongCong, 0) fTruyThu_BHXH_TongCong_HT,
		isnull(ht.fTruyThu_BHYT_NLD, 0) fTruyThu_BHYT_NLD_HT,
		isnull(ht.fTruyThu_BHYT_NSD, 0) fTruyThu_BHYT_NSD_HT,
		isnull(ht.fTruyThu_BHYT_TongCong, 0) fTruyThu_BHYT_TongCong_HT,
		isnull(ht.fTruyThu_BHTN_NLD, 0) fTruyThu_BHTN_NLD_HT,
		isnull(ht.fTruyThu_BHTN_NSD, 0) fTruyThu_BHTN_NSD_HT,
		isnull(ht.fTruyThu_BHTN_TongCong, 0) fTruyThu_BHTN_TongCong_HT,

		(isnull(dt.fTruyThu_BHXH_TongCong, 0) + isnull(ht.fTruyThu_BHXH_TongCong, 0)) fTong_TruyThu_BHXH,
		(isnull(dt.fTruyThu_BHYT_TongCong, 0) + isnull(ht.fTruyThu_BHYT_TongCong, 0)) fTong_TruyThu_BHYT,
		(isnull(dt.fTruyThu_BHTN_TongCong, 0) + isnull(ht.fTruyThu_BHTN_TongCong, 0)) fTong_TruyThu_BHTN,

		(isnull(dt.fTruyThu_BHXH_TongCong, 0) + isnull(ht.fTruyThu_BHXH_TongCong, 0) + isnull(dt.fTruyThu_BHYT_TongCong, 0) + isnull(ht.fTruyThu_BHYT_TongCong, 0) + isnull(dt.fTruyThu_BHTN_TongCong, 0) + isnull(ht.fTruyThu_BHTN_TongCong, 0)) fTong_TruyThu

	from
		BH_DM_MucLucNganSach ml
		left join dutoan_tbl dt on ml.sXauNoiMa = dt.sXauNoiMa
		left join hachtoan_tbl ht on ml.sXauNoiMa = ht.sXauNoiMa
	where ml.sLNS = '9020001' and ml.sXauNoiMa <> '9020001'
		and ml.iNamLamViec = @NamLamViec
		and ml.iID_MLNS_Cha is not null
		and ml.iTrangThai = 1
	order by ml.sXauNoiMa

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dutoan_tbl]') AND type in (N'U')) drop table dutoan_tbl
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hachtoan_tbl]') AND type in (N'U')) drop table hachtoan_tbl
--------

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu_tong_hop_don_vi]    Script Date: 9/9/2024 2:05:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu_tong_hop_don_vi] 
	@NamLamViec int,
	@IdDonVis nvarchar(50)
AS
BEGIN
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dutoan_donvi_tbl]') AND type in (N'U')) drop table dutoan_donvi_tbl
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hachtoan_donvi_tbl]') AND type in (N'U')) drop table hachtoan_donvi_tbl

	--Tạo dữ liệu khói dự toán
	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sMoTa,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sXauNoiMa,
		sum(chungtudonvi.fTruyThu_BHXH_NLD) fTruyThu_BHXH_NLD,
		sum(chungtudonvi.fTruyThu_BHXH_NSD) fTruyThu_BHXH_NSD,
		sum(chungtudonvi.fTruyThu_BHXH_TongCong) fTruyThu_BHXH_TongCong,
		sum(chungtudonvi.fTruyThu_BHYT_NLD) fTruyThu_BHYT_NLD,
		sum(chungtudonvi.fTruyThu_BHYT_NSD) fTruyThu_BHYT_NSD,
		sum(chungtudonvi.fTruyThu_BHYT_TongCong) fTruyThu_BHYT_TongCong,
		sum(chungtudonvi.fTruyThu_BHTN_NLD) fTruyThu_BHTN_NLD,
		sum(chungtudonvi.fTruyThu_BHTN_NSD) fTruyThu_BHTN_NSD,
		sum(chungtudonvi.fTruyThu_BHTN_TongCong) fTruyThu_BHTN_TongCong
	into dutoan_donvi_tbl
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
		where sLNS = '9020001' and sXauNoiMa <> '9020001' -- Khối dự toán
		and iNamLamViec = @NamLamViec
		and iID_MLNS_Cha is not null) mlns
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
			ctgt.fTruyThu_BHXH_NLD,
			ctgt.fTruyThu_BHXH_NSD,
			ctgt.fTruyThu_BHXH_TongCong,
			ctgt.fTruyThu_BHYT_NLD,
			ctgt.fTruyThu_BHYT_NSD,
			ctgt.fTruyThu_BHYT_TongCong,
			ctgt.fTruyThu_BHTN_NLD,
			ctgt.fTruyThu_BHTN_NSD,
			ctgt.fTruyThu_BHTN_TongCong,
			ctgt.iID_MLNS
		from BH_QTT_BHXH_CTCT_GiaiThich ctgt
		where ctgt.iLoaiGiaiThich = 2
			and ctgt.iQuyNam = @NamLamViec
			and ctgt.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) chungtudonvi 
	on mlns.iID_MLNS = chungtudonvi.iID_MLNS
	group by
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sMoTa,
			mlns.bHangCha,
			mlns.sLNS,
			mlns.sXauNoiMa
---------
--Tạo dữ liệu khói hạch toán
	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sMoTa,
		mlns.bHangCha,
		mlns.sLNS,
		replace(mlns.sXauNoiMa, '9020002-', '9020001-') sXauNoiMa,
		sum(chungtudonvi.fTruyThu_BHXH_NLD) fTruyThu_BHXH_NLD,
		sum(chungtudonvi.fTruyThu_BHXH_NSD) fTruyThu_BHXH_NSD,
		sum(chungtudonvi.fTruyThu_BHXH_TongCong) fTruyThu_BHXH_TongCong,
		sum(chungtudonvi.fTruyThu_BHYT_NLD) fTruyThu_BHYT_NLD,
		sum(chungtudonvi.fTruyThu_BHYT_NSD) fTruyThu_BHYT_NSD,
		sum(chungtudonvi.fTruyThu_BHYT_TongCong) fTruyThu_BHYT_TongCong,
		sum(chungtudonvi.fTruyThu_BHTN_NLD) fTruyThu_BHTN_NLD,
		sum(chungtudonvi.fTruyThu_BHTN_NSD) fTruyThu_BHTN_NSD,
		sum(chungtudonvi.fTruyThu_BHTN_TongCong) fTruyThu_BHTN_TongCong
	into hachtoan_donvi_tbl
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
		where sLNS = '9020002' and sXauNoiMa <> '9020002' -- Khối hạch toán
		and iNamLamViec = @NamLamViec
		and iID_MLNS_Cha is not null) mlns
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
			ctgt.fTruyThu_BHXH_NLD,
			ctgt.fTruyThu_BHXH_NSD,
			ctgt.fTruyThu_BHXH_TongCong,
			ctgt.fTruyThu_BHYT_NLD,
			ctgt.fTruyThu_BHYT_NSD,
			ctgt.fTruyThu_BHYT_TongCong,
			ctgt.fTruyThu_BHTN_NLD,
			ctgt.fTruyThu_BHTN_NSD,
			ctgt.fTruyThu_BHTN_TongCong,
			ctgt.iID_MLNS
		from BH_QTT_BHXH_CTCT_GiaiThich ctgt
		where ctgt.iLoaiGiaiThich = 2
			and ctgt.iQuyNam = @NamLamViec
			and ctgt.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) chungtudonvi 
	on mlns.iID_MLNS = chungtudonvi.iID_MLNS
	group by
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sMoTa,
			mlns.bHangCha,
			mlns.sLNS,
			mlns.sXauNoiMa
------------
	select
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.sMoTa,
		ml.bHangCha,
		ml.sLNS,
		ml.sXauNoiMa,
		isnull(dt.fTruyThu_BHXH_NLD, 0) fTruyThu_BHXH_NLD_DT,
		isnull(dt.fTruyThu_BHXH_NSD, 0) fTruyThu_BHXH_NSD_DT,
		isnull(dt.fTruyThu_BHXH_TongCong, 0) fTruyThu_BHXH_TongCong_DT,
		isnull(dt.fTruyThu_BHYT_NLD, 0) fTruyThu_BHYT_NLD_DT,
		isnull(dt.fTruyThu_BHYT_NSD, 0) fTruyThu_BHYT_NSD_DT,
		isnull(dt.fTruyThu_BHYT_TongCong, 0) fTruyThu_BHYT_TongCong_DT,
		isnull(dt.fTruyThu_BHTN_NLD, 0) fTruyThu_BHTN_NLD_DT,
		isnull(dt.fTruyThu_BHTN_NSD, 0) fTruyThu_BHTN_NSD_DT,
		isnull(dt.fTruyThu_BHTN_TongCong, 0) fTruyThu_BHTN_TongCong_DT,
		
		isnull(ht.fTruyThu_BHXH_NLD, 0) fTruyThu_BHXH_NLD_HT,
		isnull(ht.fTruyThu_BHXH_NSD, 0) fTruyThu_BHXH_NSD_HT,
		isnull(ht.fTruyThu_BHXH_TongCong, 0) fTruyThu_BHXH_TongCong_HT,
		isnull(ht.fTruyThu_BHYT_NLD, 0) fTruyThu_BHYT_NLD_HT,
		isnull(ht.fTruyThu_BHYT_NSD, 0) fTruyThu_BHYT_NSD_HT,
		isnull(ht.fTruyThu_BHYT_TongCong, 0) fTruyThu_BHYT_TongCong_HT,
		isnull(ht.fTruyThu_BHTN_NLD, 0) fTruyThu_BHTN_NLD_HT,
		isnull(ht.fTruyThu_BHTN_NSD, 0) fTruyThu_BHTN_NSD_HT,
		isnull(ht.fTruyThu_BHTN_TongCong, 0) fTruyThu_BHTN_TongCong_HT,

		(isnull(dt.fTruyThu_BHXH_TongCong, 0) + isnull(ht.fTruyThu_BHXH_TongCong, 0)) fTong_TruyThu_BHXH,
		(isnull(dt.fTruyThu_BHYT_TongCong, 0) + isnull(ht.fTruyThu_BHYT_TongCong, 0)) fTong_TruyThu_BHYT,
		(isnull(dt.fTruyThu_BHTN_TongCong, 0) + isnull(ht.fTruyThu_BHTN_TongCong, 0)) fTong_TruyThu_BHTN,

		(isnull(dt.fTruyThu_BHXH_TongCong, 0) + isnull(ht.fTruyThu_BHXH_TongCong, 0) + isnull(dt.fTruyThu_BHYT_TongCong, 0) + isnull(ht.fTruyThu_BHYT_TongCong, 0) + isnull(dt.fTruyThu_BHTN_TongCong, 0) + isnull(ht.fTruyThu_BHTN_TongCong, 0)) fTong_TruyThu

	from BH_DM_MucLucNganSach ml
		left join dutoan_donvi_tbl dt on ml.sXauNoiMa = dt.sXauNoiMa
		left join hachtoan_donvi_tbl ht on ml.sXauNoiMa = ht.sXauNoiMa
	where ml.sLNS = '9020001' and ml.sXauNoiMa <> '9020001'
		and ml.iNamLamViec = @NamLamViec
		and ml.iID_MLNS_Cha is not null
		and ml.iTrangThai = 1
	order by ml.sXauNoiMa

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dutoan_donvi_tbl]') AND type in (N'U')) drop table dutoan_donvi_tbl
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hachtoan_donvi_tbl]') AND type in (N'U')) drop table hachtoan_donvi_tbl

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]    Script Date: 9/9/2024 2:05:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam] 
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVi;

	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
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
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh FTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
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
		union select NewId() iID_MLNS, (select distinct iID_MLNS from BH_DM_MucLucNganSach where sLNS = '9020001'  and (sL = '' or sL is null)
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
		join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
		where
			ct.iQuyNam = @IQuy
			and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVi) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVi))
			--and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1) chungtudonvi 
		on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa

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
		and iID_MaDonVi = @IdDonVi
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]    Script Date: 9/9/2024 2:05:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int  
AS
BEGIN
declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;
DECLARE @sSoChungTuTH nvarchar(1000) = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr 	where pr.iNamLamViec = @NamLamViec
																		and pr.iQuyNam = @IQuy
																		and pr.iID_MaDonVi = @IdDonVis
																		and pr.iLoaiTongHop = 2
																		and pr.bDaTongHop = 0)
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

----------------END DETAIL AGENCY----------------
		select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu fPhuCapChucVu
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
			and ct.sSoChungTu in (select * from splitstring(@sSoChungTuTH))
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = 1;

----------------END DETAIL----------------
----------------INSERT TOTAL----------------
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
select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sM,
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
		(isnull(chungtudonvi.fLuongChinh, 0) + isnull(chungtudonvi.fPCChucVu, 0) + isnull(chungtudonvi.fPCTNNghe, 0) + isnull(chungtudonvi.fPCTNVuotKhung, 0) + isnull(chungtudonvi.fNghiOm, 0) + isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan,
		chungtudonvi.fDaQuyetToan,
		chungtudonvi.fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongSoPhaiThuBHTN,
		(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0)) fTongNLD,
		(isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)) fTongNSD,
		(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongCong,
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
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS FROM BH_DM_MucLucNganSach WHERE  sLNS = '9020001' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS FROM BH_DM_MucLucNganSach WHERE  sLNS = '9020002' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		LEFT JOIN(
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
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVis) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVis))
			--and ct.iID_MaDonVi = @IdDonVis
			and ct.iLoaiTongHop = 2
--			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end			
		)chungtudonvi 
			on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa
		ORDER BY mlns.sXauNoiMa;


----------------END INSERT DETAIL----------------
----------------INSERT DETAIL----------------
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
SELECT
NULL ,
NULL ,
0 bHangCha , 
sM,
sXauNoiMa , 
dv.sTenDonVi,
#tempChiTietDonVi.iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu,
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
		and iID_MaDonVi = @IdDonVis
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
SELECT * FROM #result ORDER BY sXauNoiMa, MaDonVi;

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
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]    Script Date: 9/9/2024 2:05:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]    Script Date: 9/9/2024 2:05:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]
	@NamLamViec int,
	@IdDonVis nvarchar(50),
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]    Script Date: 9/9/2024 2:05:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam] 
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
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
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh FTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
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
			where
			ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
				) chungtudonvi 
			on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa

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
		and iID_MaDonVi = @IdDonVi
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_qtt_thong_tri]    Script Date: 9/9/2024 2:05:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_qtt_thong_tri]
	-- Add the parameters for the stored procedure here
	@NamLamViec int ,
	@LstMaDonVi nvarchar(max),
	@iQuyNam int, 
	@iQuyNamLoai int,
	@DVT int
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)

	IF (@iQuyNam = 3 AND @iQuyNamLoai = 1) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@iQuyNam = 6 AND @iQuyNamLoai= 1) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@iQuyNam = 9 AND @iQuyNamLoai= 1) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@iQuyNam = 12 AND @iQuyNamLoai = 1) BEGIN SET @SMonths = '10,11,12' END
	ELSE BEGIN SET @SMonths = @iQuyNam END

	SELECT * INTO #result FROM 
	(
		SELECT 1 STT
			,N'Bảo hiểm xã hội' as SNoiDung
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) / @DVT as FSoTien  
			,1 ILevel
		FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu
			AND (ctct.sXauNoiMa  like '9020001-010-011-0001%' 
			OR ctct.sXauNoiMa like '9020001-010-011-0002%' 
			OR ctct.sXauNoiMa like '9020002-010-011-0001%' 
			OR ctct.sXauNoiMa like '9020002-010-011-0002%')
		left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.sXauNoiMa = gt.sXauNoiMa
			AND (gt.sXauNoiMa  like '9020001-010-011-0001%' 
			OR gt.sXauNoiMa like '9020001-010-011-0002%' 
			OR gt.sXauNoiMa like '9020002-010-011-0001%' 
			OR gt.sXauNoiMa like '9020002-010-011-0002%')
		WHERE ct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			--AND iQuyNamLoai = @iQuyNamLoai
			AND ct.iQuyNamLoai <> 2
			AND ct.iNamLamViec = @NamLamViec

		UNION ALL
		
		SELECT 2 STT
				,N'Bảo hiểm y tế' as SNoiDung
				,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) / @DVT as FSoTien 
				,1 ILevel
		FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu
			AND (ctct.sXauNoiMa  like  '9020001-010-011-0001%' 
			OR ctct.sXauNoiMa like '9020001-010-011-0002%' 
			OR ctct.sXauNoiMa like '9020002-010-011-0001%'
			OR ctct.sXauNoiMa like '9020002-010-011-0002%')
		left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.sXauNoiMa = gt.sXauNoiMa
		WHERE ct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			--AND iQuyNamLoai=@iQuyNamLoai
			AND ct.iQuyNamLoai <> 2
			AND ct.iNamLamViec = @NamLamViec

		UNION ALL

		SELECT 3 STT
			,N'Bảo hiểm thất nghiệp' as SNoiDung
			,(sum(isnull(ctct.fThu_BHTN_NLD,0)) + sum(isnull(ctct.fThu_BHTN_NSD,0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) / @DVT as FSoTien 
			,1 ILevel
		FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu
			AND (ctct.sXauNoiMa  like  '9020001-010-011-0001%' 
			OR ctct.sXauNoiMa like '9020001-010-011-0002%' 
			OR ctct.sXauNoiMa like '9020002-010-011-0001%' 
			OR ctct.sXauNoiMa like '9020002-010-011-0002%')
		left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.sXauNoiMa = gt.sXauNoiMa
		WHERE  ct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			--AND iQuyNamLoai=@iQuyNamLoai
			AND ct.iQuyNamLoai <> 2
			AND ct.iNamLamViec = @NamLamViec
	) result

	select * from #result
	DROP TABLE #result;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_qtt_thong_tri_giai_thich_truy_thu]    Script Date: 9/9/2024 2:05:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_rpt_qtt_thong_tri_giai_thich_truy_thu] 
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuy = 3 AND @ILoaiQuy = 1) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @ILoaiQuy = 1) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @ILoaiQuy = 1) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @ILoaiQuy = 1) BEGIN SET @SMonths = '10,11,12' END
	ELSE BEGIN SET @SMonths = @IQuy END

	select
		iID_MLNS,
		sNoiDung,
		sXauNoiMa,
		sum(isnull(fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
		and iNamLamViec = @NamLamViec
		and ILoaiGiaiThich = 2
		and iQuyNamLoai <> 2
		and iQuyNam in (SELECT * FROM f_split(@SMonths))
	group by iID_MLNS, sNoiDung, sXauNoiMa

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_rpt_qtt_thong_tri_giai_thich_truy_thu_donvi]    Script Date: 9/9/2024 2:05:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_rpt_qtt_thong_tri_giai_thich_truy_thu_donvi] 
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuy = 3 AND @ILoaiQuy = 1) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @ILoaiQuy = 1) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @ILoaiQuy = 1) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @ILoaiQuy = 1) BEGIN SET @SMonths = '10,11,12' END
	ELSE BEGIN SET @SMonths = @IQuy END

	select
		gt.iID_MaDonVi SMaDonVi,
		dv.sTenDonVi STenDonVi,
		sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(gt.fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(gt.fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(gt.fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD
	from BH_QTT_BHXH_CTCT_GiaiThich gt
	join DonVi dv on gt.iID_MaDonVi = dv.iID_MaDonVi
	where gt.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
		and gt.iNamLamViec = @NamLamViec
		and gt.ILoaiGiaiThich = 2
		and gt.iQuyNamLoai <> 2
		and gt.iQuyNam in (SELECT * FROM f_split(@SMonths))
		and dv.iNamLamViec = @NamLamViec
		and dv.iTrangThai = 1
	group by gt.iID_MaDonVi, dv.sTenDonVi

END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]    Script Date: 9/9/2024 2:05:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN

	DECLARE @checkTongHop INT; 
	SELECT @checkTongHop = count(*) FROM f_split(@maCachTl);

	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
		INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') or pc.Parent in ('TIENAN', 'TIENAN2')

	select * into #tmpMapping
	FROM (
		select Ma_Cot, Ma_Cot as Ma_PhuCap from #tmp
		union 
		select Ma_PhuCap as Ma_Cot, Ma_PhuCap as Ma_PhuCap from #tmp
	) AS c

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.fSoNgayHuongBHXH, 0)) as HuongPC_SN 
	INTO #tmpSoNgay
	FROM TL_DM_CanBo as cb
	INNER JOIN TL_CanBo_CheDoBHXH as pc on cb.Ma_CanBo = pc.sMaCanBo
	INNER JOIN #tmpMapping as mp on pc.sMaCheDo = mp.Ma_PhuCap
	WHERE cb.Parent in (SELECT * FROM f_split(@MaDonVi))
		AND Thang = @thang
		AND Nam = @nam
	GROUP BY mp.Ma_Cot, cb.Ma_CanBo;

	SELECT
		dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
		dsCapNhapBangLuong.Thang	AS Thang,
		dsCapNhapBangLuong.Nam		AS Nam,
		bangLuong.MA_PHUCAP			AS MaPhuCap,
		--bangLuong.Ma_CB				AS MaCapBac,
		canbo.Ma_CB MaCapBac,
		case when canbo.Ma_CB in ('3.1', '3.2', '3.3', '3.4', '3.5', '413', '415') then canbo.Ma_CB else capBac.Parent end AS Ngach,
		SUM(ISNULL(cbpc.HuongPC_SN, 0)) AS SoNgay,
		SUM(bangLuong.Gia_Tri) AS GiaTri,
		SUM(CASE WHEN @checkTongHop = 2 AND dsCapNhapBangLuong.Ma_CachTL ='CACH5' THEN 0 ELSE 1 END) AS SoNguoi
	INTO #LuongCapBac
	FROM (SELECT * FROM TL_BangLuong_Thang WHERE Gia_Tri != 0 AND Ma_PhuCap in ('LHT_TT', 'PCCV_TT', 'PCTN_TT', 'PCTNVK_TT', 'HSBL_TT')) bangLuong
	INNER JOIN (
		select * from TL_CanBo_CheDoBHXH 
		where inam = @nam 
			and ithang = @thang 
			and sMaCheDo in ('TAINANLD_DUONGSUCPHSK', 'THAISAN_DUONGSUCPHSK', 'OMDAU_DUONGSUCPHSK', 'OMKHAC_T14NGAY', 'BENHDAINGAY_T14NGAY','CONOM', 'KHAMTHAI', 'KHHGD', 'NAMNGHIKHIVOSINHCON')
		) as cbpc1 on bangLuong.Ma_Hieu_CanBo = SUBSTRING(cbpc1.sMaCanBo, 7, 2) and bangLuong.NAM = cbpc1.iNamCanCuDong AND bangLuong.THANG = cbpc1.iThangLuongCanCuDong
	LEFT JOIN #tmpSoNgay as cbpc on bangLuong.Ma_PhuCap = cbpc.Ma_Cot AND bangLuong.Ma_CBo = cbpc.Ma_CanBo
	JOIN (
		SELECT * FROM TL_DS_CapNhap_BangLuong 
		WHERE Status = 1 AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi)) 
		and isnull(STongHop, '') <> '' 
	) dsCapNhapBangLuong 
	ON bangLuong.parent = dsCapNhapBangLuong.Id AND bangLuong.THANG = dsCapNhapBangLuong.Thang and bangLuong.NAM = dsCapNhapBangLuong.Nam 
	LEFT JOIN (
		select Ma_Hieu_CanBo, Ma_CB from TL_DM_CanBo CanBo where Nam = @nam and Thang = @thang
	) canbo ON bangLuong.Ma_Hieu_CanBo = canbo.Ma_Hieu_CanBo
	LEFT JOIN TL_DM_CapBac capBac ON canbo.Ma_CB = capBac.Ma_Cb
	GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.MA_PHUCAP, canbo.Ma_CB, capBac.Parent
		
	SELECT distinct
		luongCapBac.MaDonVi,
		phuCapMlns.XauNoiMa,
		phuCapMlns.Ma_Cb,
		SoNguoi,
		SoNgay,
		GiaTri,
		luongCapBac.Thang
		INTO #LuongCapBacMlns
	FROM TL_PhuCap_MLNS phuCapMlns
	JOIN #LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
	WHERE
		phuCapMlns.Nam = @nam

	Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
	INTO #DataDuToan
	From TL_QT_ChungTuChiTiet ctchitiet
	Join TL_QT_ChungTu chungtu
	on chungtu.ID = ctchitiet.Id_ChungTu
	Where Nam = @nam
	And Ma_DonVi IN (SELECT * FROM f_split(@maCachTl))
	Group By XauNoiMa, Ma_DonVi

	SELECT
		luong.MaDonVi					AS MaDonVi,
		NEWID()							AS Id,
		iID_MLNS						AS MlnsId, 
		iID_MLNS_Cha					AS MlnsIdParent, 
		luong.XauNoiMa					AS XauNoiMa, 
		sLNS							AS Lns, 
		sL								AS L, 
		sK								AS K, 
		sM								AS M, 
		sTM								AS TM, 
		sTTM							AS TTM, 
		sNG								AS Ng, 
		sTNG							AS TNG, 
		sTNG1							AS TNG1, 
		sTNG2							AS TNG2, 
		sTNG3							AS TNG3, 
		sMoTa							AS Mota, 
		iNamLamViec						AS NamLamViec,
		bHangCha						AS BHangCha,
		sChiTietToi						AS ChiTietToi,
		CONVERT(decimal, SUM(SoNguoi))	AS SoNguoi,
		SUM(ISNULL(SoNgay, 0))			AS SoNgay,
		SUM(GiaTri)						AS TongCong,
		SUM(GiaTri)						AS DieuChinh,
		@maCachTl						AS MaCachTl,
		ISNULL(dataDuToan.DuToan, 0)	AS DDuToan,
		luong.Ma_Cb						AS MaCb,
		luong.Thang as Thang
	FROM NS_MucLucNganSach mlns
	JOIN #LuongCapBacMlns luong ON mlns.sXauNoiMa = luong.XauNoiMa
	LEFT JOIN #DataDuToan dataDuToan ON luong.XauNoiMa = dataDuToan.XauNoiMa and luong.MaDonVi = dataDuToan.Ma_DonVi
	WHERE
		mlns.sLNS IN ('1', '101', '1010000')
		AND mlns.iNamLamViec = @nam
		--AND luong.Thang = @thangTruoc
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, luong.XauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan, luong.Ma_Cb, luong.Thang
	ORDER BY MaDonVi, luong.XauNoiMa

	DROP TABLE #LuongCapBac
	DROP TABLE #DataDuToan
	DROP TABLE #LuongCapBacMlns
	DROP TABLE #tmpSoNgay
	DROP TABLE #tmpMapping
END
;
;
GO
