/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_tong_hop_chi_tiet_donvi]    Script Date: 5/17/2024 8:51:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_tong_hop_chi_tiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_tong_hop_chi_tiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]    Script Date: 5/17/2024 8:51:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]    Script Date: 5/17/2024 8:51:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_tonghop]    Script Date: 5/21/2024 9:46:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_chitiet]    Script Date: 5/21/2024 9:46:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]    Script Date: 5/17/2024 8:51:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi] 
	@NamLamViec int,
	@MaDonVi nvarchar(500),
	@LoaiChungTu int,
	@DVT int
AS
BEGIN

	select mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha IsHangCha,
		mlns.sXauNoiMa XauNoiMa,
		mlns.sMoTa STenBhMLNS,
		chungtuchitiet.iID_MaDonVi,
		donvi.sTenDonVi,
		chungtuchitiet.iQSBQNam,
		chungtuchitiet.fLuongChinh,
		chungtuchitiet.fPhuCapChucVu,
		chungtuchitiet.fPCTNNghe,
		chungtuchitiet.fPCTNVuotKhung,
		chungtuchitiet.fNghiOm,
		chungtuchitiet.fHSBL,
		chungtuchitiet.fTongQTLN,
		chungtuchitiet.fThu_BHXH_NLD,
		chungtuchitiet.fThu_BHXH_NSD,
		chungtuchitiet.fTongThuBHXH,
		chungtuchitiet.fThu_BHYT_NLD,
		chungtuchitiet.fThu_BHYT_NSD,
		chungtuchitiet.fTongThuBHYT,
		chungtuchitiet.fThu_BHTN_NLD,
		chungtuchitiet.fThu_BHTN_NSD,
		chungtuchitiet.fTongThuBHTN,
		chungtuchitiet.fTongCong
		into tbl_kht_report
		from
			(select
				iID_MLNS,
				iID_MLNS_Cha,
				bHangCha,
				sLNS,
				sXauNoiMa,
				sMoTa
			from BH_DM_MucLucNganSach 
			where sLNS in ('9020001','9020002')
			and iNamLamViec = @NamLamViec) mlns
			left join
			(select * from
			(select '9020001-010-011-0001-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0001-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0001-0002' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0002-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0002-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0001-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0001-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0001-0002' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0002-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0002-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong

			union all
			select
				ctct.sXauNoiMa,
				ct.iID_MaDonVi,
				sum(ctct.iQSBQNam) iQSBQNam,
				sum(ctct.fLuongChinh) fLuongChinh,
				sum(ctct.fPhuCapChucVu) fPhuCapChucVu,
				sum(ctct.fPCTNNghe) fPCTNNghe,
				sum(ctct.fPCTNVuotKhung) fPCTNVuotKhung,
				sum(ctct.fNghiOm) fNghiOm,
				sum(ctct.fHSBL) fHSBL,
				(isnull(sum(ctct.fLuongChinh), 0) + isnull(sum(ctct.fPhuCapChucVu), 0) + isnull(sum(ctct.fPCTNNghe), 0) + isnull(sum(ctct.fPCTNVuotKhung), 0) + isnull(sum(ctct.fNghiOm), 0) + isnull(sum(ctct.fHSBL), 0)) fTongQTLN,
				sum(ctct.fThu_BHXH_NLD) fThu_BHXH_NLD,
				sum(ctct.fThu_BHXH_NSD) fThu_BHXH_NSD,
				(isnull(sum(ctct.fThu_BHXH_NLD), 0) + isnull(sum(ctct.fThu_BHXH_NSD), 0)) fTongThuBHXH,
				sum(ctct.fThu_BHYT_NLD) fThu_BHYT_NLD,
				sum(ctct.fThu_BHYT_NSD) fThu_BHYT_NSD,
				(isnull(sum(ctct.fThu_BHYT_NLD), 0) + isnull(sum(ctct.fThu_BHYT_NSD), 0)) fTongThuBHYT,
				sum(ctct.fThu_BHTN_NLD) fThu_BHTN_NLD,
				sum(ctct.fThu_BHTN_NSD) fThu_BHTN_NSD,
				(isnull(sum(ctct.fThu_BHTN_NLD), 0) + isnull(sum(ctct.fThu_BHTN_NSD), 0)) fTongThuBHTN,
				(isnull(sum(ctct.fThu_BHXH_NLD), 0) + isnull(sum(ctct.fThu_BHXH_NSD), 0) + isnull(sum(ctct.fThu_BHYT_NLD), 0) + isnull(sum(ctct.fThu_BHYT_NSD), 0) + isnull(sum(ctct.fThu_BHTN_NLD), 0) + isnull(sum(ctct.fThu_BHTN_NSD), 0)) fTongCong
			from 
			BH_KHT_BHXH_ChiTiet ctct
			join BH_KHT_BHXH ct on ctct.iID_KHT_BHXH = ct.iID_KHT_BHXH
			where ct.iID_MaDonVi in ((SELECT * FROM f_split(@MaDonVi)))
				and ct.iNamLamViec = @NamLamViec
				and ct.iLoaiTongHop = @LoaiChungTu
			group by
				ctct.sXauNoiMa,
				ct.iID_MaDonVi
			) chungtu) chungtuchitiet on mlns.sXauNoiMa = chungtuchitiet.sXauNoiMa
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) donvi ON chungtuchitiet.iID_MaDonVi = donvi.iID_MaDonVi
	order by mlns.sXauNoiMa

	update tbl_kht_report
	set STenBhMLNS = sTenDonVi
	where (fTongCong is not null and fTongCong <> 0) or (fTongQTLN is not null and fTongQTLN <> 0) or (iQSBQNam is not null and iQSBQNam <> 0)

	update tbl_kht_report
	set IsHangCha = 1
	where XauNoiMa in ('9020001-010-011-0001-0000','9020001-010-011-0001-0001','9020001-010-011-0001-0002','9020001-010-011-0002-0000','9020001-010-011-0002-0001',
						'9020002-010-011-0001-0000','9020002-010-011-0001-0001','9020002-010-011-0001-0002','9020002-010-011-0002-0000','9020002-010-011-0002-0001')
	and iID_MaDonVi is null

	--Lay MLNS cha
	select iID_MLNS,
		iID_MLNS_Cha,
		IsHangCha,
		XauNoiMa,
		STenBhMLNS
	into tbl_kht_parent
	from tbl_kht_report
	where IsHangCha = 1

	--Result
	select 
		report.iID_MLNS iID_MucLucNganSach,
		case
			when report.IsHangCha = 1 then report.iID_MLNS_Cha
			else parent.iID_MLNS
		end as iID_MLNS_Cha,
		report.IsHangCha,
		report.XauNoiMa,
		case
			when report.IsHangCha = 1 then report.STenBhMLNS
			else ('   '+ report.STenBhMLNS)
		end as STenBhMLNS,
		report.iID_MaDonVi,
		report.sTenDonVi,
		report.iQSBQNam,
		report.fLuongChinh fLuongChinh,
		report.fPhuCapChucVu fPhuCapChucVu,
		report.fPCTNNghe fPCTNNghe,
		report.fPCTNVuotKhung fPCTNVuotKhung,
		report.fNghiOm fNghiOm,
		report.fHSBL fHSBL,
		report.fTongQTLN fTongQTLN,
		report.fThu_BHXH_NLD fThu_BHXH_NLD,
		report.fThu_BHXH_NSD fThu_BHXH_NSD,
		report.fTongThuBHXH fTongThuBHXH,
		report.fThu_BHYT_NLD fThu_BHYT_NLD,
		report.fThu_BHYT_NSD fThu_BHYT_NSD,
		report.fTongThuBHYT fTongThuBHYT,
		report.fThu_BHTN_NLD fThu_BHTN_NLD,
		report.fThu_BHTN_NSD fThu_BHTN_NSD,
		report.fTongThuBHTN fTongThuBHTN,
		report.fTongCong
	into tbl_kht_result
	from tbl_kht_report report
		left join tbl_kht_parent parent on report.XauNoiMa = parent.XauNoiMa
	order by report.XauNoiMa

	update tbl_kht_result
	set iID_MucLucNganSach = newid()
	where IsHangCha = 0
	and iID_MaDonVi is not null
	---
	select * from tbl_kht_result
	order by XauNoiMa

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_kht_report]') AND type in (N'U')) drop table tbl_kht_report;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_kht_parent]') AND type in (N'U')) drop table tbl_kht_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_kht_result]') AND type in (N'U')) drop table tbl_kht_result;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]    Script Date: 5/17/2024 8:51:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop] 
	@NamLamViec int,
	@MaDonVi nvarchar(500),
	@LoaiChungTu int,
	@DVT int
AS
BEGIN

	select mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha IsHangCha,
		mlns.sXauNoiMa XauNoiMa,
		mlns.sMoTa STenBhMLNS,
		chungtuchitiet.iID_MaDonVi,
		donvi.sTenDonVi,
		chungtuchitiet.iQSBQNam,
		chungtuchitiet.fLuongChinh,
		chungtuchitiet.fPhuCapChucVu,
		chungtuchitiet.fPCTNNghe,
		chungtuchitiet.fPCTNVuotKhung,
		chungtuchitiet.fNghiOm,
		chungtuchitiet.fHSBL,
		chungtuchitiet.fTongQTLN,
		chungtuchitiet.fThu_BHXH_NLD,
		chungtuchitiet.fThu_BHXH_NSD,
		chungtuchitiet.fTongThuBHXH,
		chungtuchitiet.fThu_BHYT_NLD,
		chungtuchitiet.fThu_BHYT_NSD,
		chungtuchitiet.fTongThuBHYT,
		chungtuchitiet.fThu_BHTN_NLD,
		chungtuchitiet.fThu_BHTN_NSD,
		chungtuchitiet.fTongThuBHTN,
		chungtuchitiet.fTongCong
		into tbl_kht_report_th
		from
			(select
				iID_MLNS,
				iID_MLNS_Cha,
				bHangCha,
				sLNS,
				sXauNoiMa,
				sMoTa
			from BH_DM_MucLucNganSach 
			where sLNS in ('9020001','9020002')
			and iNamLamViec = @NamLamViec) mlns
			left join
			(select * from
			(select '9020001-010-011-0001-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0001-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0001-0002' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0002-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020001-010-011-0002-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0001-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0001-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0001-0002' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0002-0000' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong
			union all
			select '9020002-010-011-0002-0001' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
					null fTongThuBHXH, null fThu_BHYT_NLD, null fThu_BHYT_NSD, null fTongThuBHYT, null fThu_BHTN_NLD, null fThu_BHTN_NSD, null fTongThuBHTN, null fTongCong

			union all
			select
				ctct.sXauNoiMa,
				ct.iID_MaDonVi,
				sum(ctct.iQSBQNam) iQSBQNam,
				sum(ctct.fLuongChinh) fLuongChinh,
				sum(ctct.fPhuCapChucVu) fPhuCapChucVu,
				sum(ctct.fPCTNNghe) fPCTNNghe,
				sum(ctct.fPCTNVuotKhung) fPCTNVuotKhung,
				sum(ctct.fNghiOm) fNghiOm,
				sum(ctct.fHSBL) fHSBL,
				(isnull(sum(ctct.fLuongChinh), 0) + isnull(sum(ctct.fPhuCapChucVu), 0) + isnull(sum(ctct.fPCTNNghe), 0) + isnull(sum(ctct.fPCTNVuotKhung), 0) + isnull(sum(ctct.fNghiOm), 0) + isnull(sum(ctct.fHSBL), 0)) fTongQTLN,
				sum(ctct.fThu_BHXH_NLD) fThu_BHXH_NLD,
				sum(ctct.fThu_BHXH_NSD) fThu_BHXH_NSD,
				(isnull(sum(ctct.fThu_BHXH_NLD), 0) + isnull(sum(ctct.fThu_BHXH_NSD), 0)) fTongThuBHXH,
				sum(ctct.fThu_BHYT_NLD) fThu_BHYT_NLD,
				sum(ctct.fThu_BHYT_NSD) fThu_BHYT_NSD,
				(isnull(sum(ctct.fThu_BHYT_NLD), 0) + isnull(sum(ctct.fThu_BHYT_NSD), 0)) fTongThuBHYT,
				sum(ctct.fThu_BHTN_NLD) fThu_BHTN_NLD,
				sum(ctct.fThu_BHTN_NSD) fThu_BHTN_NSD,
				(isnull(sum(ctct.fThu_BHTN_NLD), 0) + isnull(sum(ctct.fThu_BHTN_NSD), 0)) fTongThuBHTN,
				(isnull(sum(ctct.fThu_BHXH_NLD), 0) + isnull(sum(ctct.fThu_BHXH_NSD), 0) + isnull(sum(ctct.fThu_BHYT_NLD), 0) + isnull(sum(ctct.fThu_BHYT_NSD), 0) + isnull(sum(ctct.fThu_BHTN_NLD), 0) + isnull(sum(ctct.fThu_BHTN_NSD), 0)) fTongCong
			from 
			BH_KHT_BHXH_ChiTiet ctct
			join BH_KHT_BHXH ct on ctct.iID_KHT_BHXH = ct.iID_KHT_BHXH
			where ct.iID_MaDonVi in ((SELECT * FROM f_split(@MaDonVi)))
				and ct.iNamLamViec = @NamLamViec
				and ct.bDaTongHop = 1
				and ct.iLoaiTongHop = @LoaiChungTu
			group by
				ctct.sXauNoiMa,
				ct.iID_MaDonVi
			) chungtu) chungtuchitiet on mlns.sXauNoiMa = chungtuchitiet.sXauNoiMa
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) donvi ON chungtuchitiet.iID_MaDonVi = donvi.iID_MaDonVi
	order by mlns.sXauNoiMa

	update tbl_kht_report_th
	set STenBhMLNS = sTenDonVi
	where (fTongCong is not null and fTongCong <> 0) or (fTongQTLN is not null and fTongQTLN <> 0) or (iQSBQNam is not null and iQSBQNam <> 0)

	update tbl_kht_report_th
	set IsHangCha = 1
	where XauNoiMa in ('9020001-010-011-0001-0000','9020001-010-011-0001-0001','9020001-010-011-0001-0002','9020001-010-011-0002-0000','9020001-010-011-0002-0001',
						'9020002-010-011-0001-0000','9020002-010-011-0001-0001','9020002-010-011-0001-0002','9020002-010-011-0002-0000','9020002-010-011-0002-0001')
	and iID_MaDonVi is null

	--Lay MLNS cha
	select iID_MLNS,
		iID_MLNS_Cha,
		IsHangCha,
		XauNoiMa,
		STenBhMLNS
	into tbl_kht_parent_th
	from tbl_kht_report_th
	where IsHangCha = 1

	--Result
	select 
		report.iID_MLNS iID_MucLucNganSach,
		case
			when report.IsHangCha = 1 then report.iID_MLNS_Cha
			else parent.iID_MLNS
		end as iID_MLNS_Cha,
		report.IsHangCha,
		report.XauNoiMa,
		case
			when report.IsHangCha = 1 then report.STenBhMLNS
			else ('   '+ report.STenBhMLNS)
		end as STenBhMLNS,
		report.iID_MaDonVi,
		report.sTenDonVi,
		report.iQSBQNam,
		report.fLuongChinh fLuongChinh,
		report.fPhuCapChucVu fPhuCapChucVu,
		report.fPCTNNghe fPCTNNghe,
		report.fPCTNVuotKhung fPCTNVuotKhung,
		report.fNghiOm fNghiOm,
		report.fHSBL fHSBL,
		report.fTongQTLN fTongQTLN,
		report.fThu_BHXH_NLD fThu_BHXH_NLD,
		report.fThu_BHXH_NSD fThu_BHXH_NSD,
		report.fTongThuBHXH fTongThuBHXH,
		report.fThu_BHYT_NLD fThu_BHYT_NLD,
		report.fThu_BHYT_NSD fThu_BHYT_NSD,
		report.fTongThuBHYT fTongThuBHYT,
		report.fThu_BHTN_NLD fThu_BHTN_NLD,
		report.fThu_BHTN_NSD fThu_BHTN_NSD,
		report.fTongThuBHTN fTongThuBHTN,
		report.fTongCong
	into tbl_kht_result_th
	from tbl_kht_report_th report
		left join tbl_kht_parent_th parent on report.XauNoiMa = parent.XauNoiMa
	order by report.XauNoiMa

	update tbl_kht_result_th
	set iID_MucLucNganSach = newid()
	where IsHangCha = 0
	and iID_MaDonVi is not null
	---
	select * from tbl_kht_result_th
	order by XauNoiMa

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_kht_report_th]') AND type in (N'U')) drop table tbl_kht_report_th;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_kht_parent_th]') AND type in (N'U')) drop table tbl_kht_parent_th;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_kht_result_th]') AND type in (N'U')) drop table tbl_kht_result_th;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_tong_hop_chi_tiet_donvi]    Script Date: 5/17/2024 8:51:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_kht_bhxh_tong_hop_chi_tiet_donvi] 
	@NamLamViec int,
	@MaDonVi nvarchar(500),
	@LoaiChungTu int,
	@DVT int
AS
BEGIN

	-- Chi tiet chung tu
	select ctct.sXauNoiMa,
	ctct.iID_NoiDung,
	ml.iID_MLNS_Cha IdParent,
	ctct.sLNS,
	('     '+dv.sTenDonVi) as sTenNoiDung ,
	ctct.iSoNguoi,
	ctct.iSoThang,
	ctct.fDinhMuc,
	ctct.fThanhTien,
	ctct.sGhiChu,
	1 Type,
	0 bHangCha
	into #tempChungTu
	from BH_KHTM_BHYT_ChiTiet ctct
	left join BH_KHTM_BHYT ct on ctct.iID_KHTM_BHYT=ct.iID_KHTM_BHYT
	left join DonVi dv on ct.iID_MaDonVi=dv.iID_MaDonVi
	left join BH_DM_MucLucNganSach ml on ctct.iID_NoiDung=ml.iID_MLNS
	where  ct.iNamLamViec=@NamLamViec
	and ml.iNamLamViec=@NamLamViec
	and ml.iTrangThai=1
	and dv.iNamLamViec=@NamLamViec
	and dv.iTrangThai=1
	and dv.iID_MaDonVi in (select * from splitstring(@MaDonVi))
	order by ctct.sXauNoiMa

	-- tinh sum chung tu
	select ctct.sXauNoiMa,
	ctct.iID_NoiDung,
	ctct.sLNS,
	ctct.sTenNoiDung as sTenNoiDung ,
	SUM(ctct.iSoNguoi) iSoNguoi,
	SUM(ctct.iSoThang) iSoThang,
	SUM(ctct.fDinhMuc) fDinhMuc,
	SUM(ctct.fThanhTien) fThanhTien,
	ctct.sGhiChu,
	0 Type
	into #tempChungTuSum
	from BH_KHTM_BHYT_ChiTiet ctct
	left join BH_KHTM_BHYT ct on ctct.iID_KHTM_BHYT=ct.iID_KHTM_BHYT
	left join DonVi dv on ct.iID_MaDonVi=dv.iID_MaDonVi
	where  ct.iNamLamViec=@NamLamViec
	and dv.iNamLamViec=@NamLamViec
	and dv.iTrangThai=1
	and dv.iID_MaDonVi in (select * from splitstring(@MaDonVi))
	group by ctct.sXauNoiMa,
	ctct.iID_NoiDung,
	ctct.sLNS,
	ctct.sTenNoiDung ,
	ctct.sGhiChu
	order by ctct.sXauNoiMa

	-- join voi muc luc ngan sach
	select mlns.sXauNoiMa,
	mlns.iID_MLNS iID_NoiDung,
	mlns.iID_MLNS_Cha IdParent,
	tm.sLNS,
	mlns.sMoTa as sTenNoiDung ,
	tm.iSoNguoi,
	tm.iSoThang,
	tm.fDinhMuc,
	tm.fThanhTien,
	tm.sGhiChu,
	0 Type,
	mlns.bHangCha
	into #tempmlnsChungTu
	from BH_DM_MucLucNganSach mlns
	left join #tempChungTuSum tm  on mlns.sXauNoiMa=tm.sXauNoiMa
	where mlns.sLNS like '903%'
	and mlns.iNamLamViec=@NamLamViec
	and mlns.iTrangThai=1
	order by sXauNoiMa, Type asc


	select * into #tempAll from 
	( select * from #tempmlnsChungTu
	union all 
	select * from #tempChungTu
	) temp

	
	select fGiaTri into #tempLCS from BH_DM_CauHinhThamSo
	where iNamLamViec=@NamLamViec and sMa='LCS' and bTrangThai=1

	select fGiaTri into #tempHESO_BHYT from BH_DM_CauHinhThamSo
	where iNamLamViec=@NamLamViec and sMa='HESO_BHYT' and bTrangThai=1


	select sXauNoiMa,
	iID_NoiDung ,
	IdParent,
	sTenNoiDung,
	iSoNguoi,
	iSoThang,
	fDinhMuc,
	fThanhTien,
	sGhiChu,
	sLNS,
	bHangCha,
	Type,
	#tempLCS.fGiaTri as DHeSoLCS
	,#tempHESO_BHYT.fGiaTri as DHeSoBHYT 
	from #tempAll,#tempLCS ,#tempHESO_BHYT
	order by sXauNoiMa, Type asc

	drop table #tempAll
	drop table #tempChungTu
	drop table #tempmlnsChungTu
END
;
GO


update BH_DM_MucLucNganSach 
set bHangChaDuToanDieuChinh= bHangCha


/****** Object:  StoredProcedure [dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_chitiet]    Script Date: 5/21/2024 9:46:46 AM ******/
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
		Sum(ct_ct.fQuyetToanQuyNay)/@DonViTinh as FQuyetToanQuyNay
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qt_report_thongtriquyettoanKCBBHHY_tonghop]    Script Date: 5/21/2024 9:46:46 AM ******/
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
		Sum(ct_ct.fQuyetToanQuyNay)/@DonViTinh as FQuyetToanQuyNay
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
;
GO