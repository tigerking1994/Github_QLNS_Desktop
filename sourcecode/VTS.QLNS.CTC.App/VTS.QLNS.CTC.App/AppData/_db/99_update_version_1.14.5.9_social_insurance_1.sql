/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]    Script Date: 6/6/2024 6:26:44 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_tong_hop_chi_tiet_donvi]    Script Date: 6/6/2024 6:26:44 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_tong_hop_chi_tiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_tong_hop_chi_tiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 6/6/2024 6:26:44 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]    Script Date: 6/6/2024 6:26:44 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]    Script Date: 6/6/2024 6:26:44 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]    Script Date: 6/6/2024 6:26:44 PM ******/
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
			select '9020001-010-011-0001-0003' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
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
			select '9020002-010-011-0001-0003' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
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
						'9020002-010-011-0001-0000','9020002-010-011-0001-0001','9020002-010-011-0001-0002','9020002-010-011-0002-0000','9020002-010-011-0002-0001',
						'9020001-010-011-0001-0003', '9020002-010-011-0001-0003')
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]    Script Date: 6/6/2024 6:26:44 PM ******/
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
			select '9020001-010-011-0001-0003' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
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
			select '9020002-010-011-0001-0003' sXauNoiMa, null iID_MaDonVi, null iQSBQNam, null fLuongChinh, null fPhuCapChucVu, null fPCTNNghe, null fPCTNVuotKhung, null fNghiOm, null fHSBL, null fTongQTLN, null fThu_BHXH_NLD, null fThu_BHXH_NSD,
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
						'9020002-010-011-0001-0000','9020002-010-011-0001-0001','9020002-010-011-0001-0002','9020002-010-011-0002-0000','9020002-010-011-0002-0001',
						'9020001-010-011-0001-0003', '9020002-010-011-0001-0003')
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 6/6/2024 6:26:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@NamLamViec int
AS
BEGIN
	INSERT INTO BH_KHT_BHXH_ChiTiet (iID_KHT_BHXH, iID_MucLucNganSach, iQSBQNam, fLuongChinh , fPhuCapChucVu, fPCTNNghe, fPCTNVuotKhung, fNghiOm, fHSBL, fThu_BHXH_NLD
	, fThu_BHXH_NSD , fThu_BHYT_NLD , fThu_BHYT_NSD , fThu_BHTN_NLD , fThu_BHTN_NSD,fTongThuBHXH, fTongThuBHYT, fTongThuBHTN, fTongCong
	, dNgayTao, dNgaySua, sNguoiTao, sLNS, sXauNoiMa)
SELECT @idChungTu,
       iID_MucLucNganSach,
	   sum(iQSBQNam),
       sum(fLuongChinh) ,
       sum(fPhuCapChucVu) ,
	   sum(fPCTNNghe) ,
	   sum(fPCTNVuotKhung) ,
	   sum(fNghiOm),
	   sum(fHSBL),
       sum(fThu_BHXH_NLD) ,
	   sum(fThu_BHXH_NSD) ,
	   sum(fThu_BHYT_NLD) ,
	   sum(fThu_BHYT_NSD) ,
	   sum(fThu_BHTN_NLD) ,
	   sum(fThu_BHTN_NSD) ,
	   sum(fTongThuBHXH) ,
	   Sum(fTongThuBHYT) ,
	   Sum(fTongThuBHTN) ,
	   Sum(fTongCong) ,
       NULL ,
       NULL ,
       NULL ,
	   sLNS,
	   sXauNoiMa
FROM BH_KHT_BHXH_ChiTiet
WHERE iID_KHT_BHXH in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY iID_MucLucNganSach, sLNS, sXauNoiMa

--danh dau chung tu da tong hop
update BH_KHT_BHXH set bDaTongHop = 1 
where iID_KHT_BHXH in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop));

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_tong_hop_chi_tiet_donvi]    Script Date: 6/6/2024 6:26:44 PM ******/
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
	
	--select fGiaTri into #tempLCS from BH_DM_CauHinhThamSo
	--where iNamLamViec=@NamLamViec and sMa='LCS' and bTrangThai=1

	--select fGiaTri into #tempHESO_BHYT from BH_DM_CauHinhThamSo
	--where iNamLamViec=@NamLamViec and sMa='HESO_BHYT' and bTrangThai=1


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
	@NamLamViec iNamLamViec
	into #tempresult
	from #tempAll
	order by sXauNoiMa, Type asc

	alter table #tempresult add DHeSoLCS float, DHeSoBHYT float;

	update #tempresult
	set DHeSoLCS = (select top 1 fGiaTri from BH_DM_CauHinhThamSo where iNamLamViec = @NamLamViec and sMa = 'LCS' and bTrangThai = 1),
		DHeSoBHYT = (select top 1 fGiaTri from BH_DM_CauHinhThamSo where iNamLamViec = @NamLamViec and sMa = 'HESO_BHYT' and bTrangThai = 1)

	select * from #tempresult 
	order by sXauNoiMa, Type asc;

	drop table #tempAll;
	drop table #tempChungTu;
	drop table #tempmlnsChungTu;
	drop table #tempresult;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]    Script Date: 6/6/2024 6:26:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@NamLamViec int
AS
BEGIN
	INSERT INTO BH_KHTM_BHYT_ChiTiet (iID_KHTM_BHYT, iID_NoiDung, sTenNoiDung, iSoNguoi, iSoThang, fDinhMuc, fThanhTien, sGhiChu, dNgayTao, dNgaySua, sNguoiTao, sNguoiSua, iID_MaDonVi, sTenDonVi, sLNS, sXauNoiMa)
SELECT @idChungTu,
       iID_NoiDung,
	   sTenNoiDung,
       sum(isnull(iSoNguoi, 0)) iSoNguoi,
       sum(isnull(iSoThang, 0)) iSoThang,
	   fDinhMuc,
	   sum(isnull(fThanhTien, 0)) fThanhTien,
	   NULL,
       NULL,
       NULL,
       NULL,
	   NULL,
	   NULL,
	   NULL,
	   sLNS,
	   sXauNoiMa
FROM BH_KHTM_BHYT_ChiTiet
WHERE iID_KHTM_BHYT in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY iID_NoiDung,
	   sTenNoiDung,
	   fDinhMuc,
	   sLNS,
	   sXauNoiMa

--danh dau chung tu da tong hop
update BH_KHTM_BHYT set bDaTongHop = 1 
where iID_KHTM_BHYT in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop));
END
;
;
;
GO


DELETE FROM TL_PhuCap_MLNS WHERE Nam = 2024 AND Ma_CachTL in ('CACH2', 'CACH4')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:06:52.723' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'1101adef-d533-4c59-be12-d2cd1e8189a0', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'NAMNGHIVIEC_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2024, N'00', N'Phụ cấp chức vụ - CCQP', N'Nam nghỉ việc khi vợ snh con - PCCV BH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:56:16.363' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'99c33598-1205-4d57-abd6-7c3d921631cb', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'CONOM_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2024, N'00', N'Phụ cấp chức vụ - QNCN', N'Con ốm - PCCVBH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:04:33.123' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'59ece0d7-50fb-4be7-94f8-1502a6daf49d', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'NAMNGHIVIEC_LBH_TT', N'Lương viên chức quốc phòng', 2024, N'00', N'Lương viên chức quốc phòng', N'Nam nghỉ việc khi vợ sinh con - Lương BH thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:59:19.820' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'a914d9d4-91b2-41d0-8be5-824316a08083', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'KT_LBH_TT', N'Lương công nhân quốc phòng', 2024, N'00', N'Lương công nhân quốc phòng', N'Khám thai - Lương BH thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:36:51.863' AS DateTime), CAST(N'2024-05-20T16:00:57.140' AS DateTime), NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'136886af-46cd-49e1-933c-8ccb4fd90432', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'TAINANLD_DUONGSUCPHSK', N'Lương công nhân quốc phòng', 2024, N'00', N'Lương công nhân quốc phòng', N'Dưỡng sức phục hồi sức khỏe sau TNLĐ', N'6001', N'40', N'admin', N'admin', N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:34:30.413' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'cd0e6c4b-4c24-4b5b-ac07-0eb2add58210', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'OMDAU_DUONGSUCPHSK', N'Lương sĩ quan', 2024, N'00', N'Lương sĩ quan', N'Dưỡng sức, PHSK sau nghỉ ốm đau', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:08.773' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.1', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2024, N'00', N'Phụ cấp chức vụ - CNQP', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:54:47.773' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'dc958a25-4f07-48c1-a02d-8838bdf0b6db', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'CONOM_LBH_TT', N'Lương công chức quốc phòng', 2024, N'00', N'Lương công chức quốc phòng', N'Con ốm - Lương bảo hiểm thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:58:43.100' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'a914d9d4-91b2-41d0-8be5-824316a08083', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'KT_LBH_TT', N'Lương viên chức quốc phòng', 2024, N'00', N'Lương viên chức quốc phòng', N'Khám thai - Lương BH thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:37:34.857' AS DateTime), CAST(N'2024-05-20T15:42:32.423' AS DateTime), NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'136886af-46cd-49e1-933c-8ccb4fd90432', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'TAINANLD_DUONGSUCPHSK', N'Lương quân nhân chuyên nghiệp', 2024, N'00', N'Lương quân nhân chuyên nghiệp', N'Dưỡng sức phục hồi sức khỏe sau TNLĐ', N'6001', N'20', N'admin', N'admin', N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:04.167' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH4', N'3.3', NULL, N'OK_D14N_LBH_TT', N'Lương viên chức quốc phòng', 2024, N'00', N'Lương viên chức quốc phòng', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:43:49.007' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'af321143-0afe-416f-a4a1-d9af2a001707', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'BDN_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2024, N'00', N'Phụ cấp chức vụ - VCQP', N'Bệnh dài ngày - Trên 14 ngày - PCCV BH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:00:49.337' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'7cd90ee2-ff39-469f-94b0-40115adeec03', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'KT_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2024, N'00', N'Phụ cấp chức vụ - QNCN', N'Khám thai - PCCV BH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:03:46.740' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'fdfae951-8eb4-4a8a-b9af-ad68326ad742', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'KHHGD_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2024, N'00', N'Phụ cấp chức vụ - QNCN', N'KHHGĐ- PCCV BH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:37:27.343' AS DateTime), CAST(N'2024-05-20T15:42:32.427' AS DateTime), NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'136886af-46cd-49e1-933c-8ccb4fd90432', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'TAINANLD_DUONGSUCPHSK', N'Lương sĩ quan', 2024, N'00', N'Lương sĩ quan', N'Dưỡng sức phục hồi sức khỏe sau TNLĐ', N'6001', N'10', N'admin', N'admin', N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:56:42.670' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'99c33598-1205-4d57-abd6-7c3d921631cb', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'CONOM_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2024, N'00', N'Phụ cấp chức vụ - SQ', N'Con ốm - PCCVBH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:59:59.173' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'a914d9d4-91b2-41d0-8be5-824316a08083', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'KT_LBH_TT', N'Lương sĩ quan', 2024, N'00', N'Lương sĩ quan', N'Khám thai - Lương BH thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:36:24.720' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'cd0e6c4b-4c24-4b5b-ac07-0eb2add58210', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'OMDAU_DUONGSUCPHSK', N'Lương viên chức quốc phòng', 2024, N'00', N'Lương viên chức quốc phòng', N'Dưỡng sức, PHSK sau nghỉ ốm đau', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:47.677' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'OK_D14N_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2024, N'00', N'Lương quân nhân chuyên nghiệp', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:40:24.827' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'62cc5656-1345-48ac-9a5b-66d6031d1ec8', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'THAISAN_DUONGSUCPHSK', N'Lương công nhân quốc phòng', 2024, N'00', N'Lương công nhân quốc phòng', N'Dưỡng sức, PHSK sau nghỉ thai sản', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:35:46.503' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'cd0e6c4b-4c24-4b5b-ac07-0eb2add58210', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'OMDAU_DUONGSUCPHSK', N'Lương quân nhân chuyên nghiệp', 2024, N'00', N'Lương quân nhân chuyên nghiệp', N'Dưỡng sức, PHSK sau nghỉ ốm đau', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:05:06.243' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'59ece0d7-50fb-4be7-94f8-1502a6daf49d', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'NAMNGHIVIEC_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2024, N'00', N'Lương quân nhân chuyên nghiệp', N'Nam nghỉ việc khi vợ sinh con - Lương BH thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:57.463' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH4', N'1', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2024, N'00', N'Phụ cấp chức vụ - SQ', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:51:30.590' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'1a119acc-2f19-4d58-adf3-82a477c4dc68', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'OK_T14N_LBH_TT', N'Phụ cấp quân hàm', 2024, N'00', N'Phụ cấp quân hàm', N'Ốm khác - Trên 14 ngày - Lương bảo hiểm thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:34.477' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH4', N'3.2', NULL, N'OK_D14N_LBH_TT', N'Lương công chức quốc phòng', 2024, N'00', N'Lương công chức quốc phòng', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:35:13.993' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2024, N'00', N'Phụ cấp chức vụ - CCQP', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:44:07.773' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'af321143-0afe-416f-a4a1-d9af2a001707', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'BDN_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2024, N'00', N'Phụ cấp chức vụ - CCQP', N'Bệnh dài ngày - Trên 14 ngày - PCCV BH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:02:01.717' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'4b8377d3-950c-4278-bc62-4b29a33b062c', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'KHHGD_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2024, N'00', N'Lương quân nhân chuyên nghiệp', N'KHHGĐ - Lương BH thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:59:51.493' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'a914d9d4-91b2-41d0-8be5-824316a08083', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'KT_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2024, N'00', N'Lương quân nhân chuyên nghiệp', N'Khám thai - Lương BH thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:00:40.357' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'7cd90ee2-ff39-469f-94b0-40115adeec03', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'KT_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2024, N'00', N'Phụ cấp chức vụ - CCQP', N'Khám thai - PCCV BH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:55.983' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6400', N'CACH4', N'4', NULL, N'OK_D14N_LBH_TT', N'Phụ cấp quân hàm', 2024, N'00', N'Phụ cấp quân hàm', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:01:30.420' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'4b8377d3-950c-4278-bc62-4b29a33b062c', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'KHHGD_LBH_TT', N'Lương viên chức quốc phòng', 2024, N'00', N'Lương viên chức quốc phòng', N'KHHGĐ - Lương BH thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:42:03.567' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'6b0fd88b-f8e6-4738-9a5e-3877dd618b14', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'BDN_T14N_LBH_TT', N'Lương công chức quốc phòng', 2024, N'00', N'Lương công chức quốc phòng', N'Bệnh dài ngày - Trên 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:34:46.483' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'BDN_D14N_LBH_TT', N'Lương viên chức quốc phòng', 2024, N'00', N'Lương viên chức quốc phòng', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:34:57.913' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'BDN_D14N_LBH_TT', N'Lương công chức quốc phòng', 2024, N'00', N'Lương công chức quốc phòng', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:55:04.223' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'dc958a25-4f07-48c1-a02d-8838bdf0b6db', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'CONOM_LBH_TT', N'Phụ cấp quân hàm', 2024, N'00', N'Phụ cấp quân hàm', N'Con ốm - Lương bảo hiểm thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:51:45.157' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'1a119acc-2f19-4d58-adf3-82a477c4dc68', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'OK_T14N_LBH_TT', N'Lương sĩ quan', 2024, N'00', N'Lương sĩ quan', N'Ốm khác - Trên 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:30:42.810' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH4', N'2', NULL, N'BDN_D14N_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2024, N'00', N'Lương quân nhân chuyên nghiệp', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:05:56.653' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'1101adef-d533-4c59-be12-d2cd1e8189a0', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'NAMNGHIVIEC_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2024, N'00', N'Phụ cấp chức vụ - SQ', N'Nam nghỉ việc khi vợ snh con - PCCV BH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:57.463' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2024, N'00', N'Phụ cấp chức vụ - SQ', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:40:12.010' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'62cc5656-1345-48ac-9a5b-66d6031d1ec8', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'THAISAN_DUONGSUCPHSK', N'Lương quân nhân chuyên nghiệp', 2024, N'00', N'Lương quân nhân chuyên nghiệp', N'Dưỡng sức, PHSK sau nghỉ thai sản', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:50:35.733' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'1a119acc-2f19-4d58-adf3-82a477c4dc68', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'OK_T14N_LBH_TT', N'Lương viên chức quốc phòng', 2024, N'00', N'Lương viên chức quốc phòng', N'Ốm khác - Trên 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:14.757' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'OK_D14N_LBH_TT', N'Lương công nhân quốc phòng', 2024, N'00', N'Lương công nhân quốc phòng', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:43:18.263' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'6b0fd88b-f8e6-4738-9a5e-3877dd618b14', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'BDN_T14N_LBH_TT', N'Phụ cấp quân hàm', 2024, N'00', N'Phụ cấp quân hàm', N'Bệnh dài ngày - Trên 14 ngày -  Lương bảo hiểm thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:45:13.590' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'af321143-0afe-416f-a4a1-d9af2a001707', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'BDN_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2024, N'00', N'Phụ cấp chức vụ - QNCN', N'Bệnh dài ngày - Trên 14 ngày - PCCV BH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:48:49.220' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH4', N'3.3', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2024, N'00', N'Phụ cấp chức vụ - VCQP', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:29:37.330' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH4', N'1', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2024, N'00', N'Phụ cấp chức vụ - SQ', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:35:43.643' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH4', N'3.1', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2024, N'00', N'Phụ cấp chức vụ - CNQP', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:34:33.177' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH4', N'3.1', NULL, N'BDN_D14N_LBH_TT', N'Lương công nhân quốc phòng', 2024, N'00', N'Lương công nhân quốc phòng', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:37:09.927' AS DateTime), CAST(N'2024-05-20T15:42:32.427' AS DateTime), NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'136886af-46cd-49e1-933c-8ccb4fd90432', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'TAINANLD_DUONGSUCPHSK', N'Lương viên chức quốc phòng', 2024, N'00', N'Lương viên chức quốc phòng', N'Dưỡng sức phục hồi sức khỏe sau TNLĐ', N'6001', N'30', N'admin', N'admin', N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:40:43.033' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'62cc5656-1345-48ac-9a5b-66d6031d1ec8', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'THAISAN_DUONGSUCPHSK', N'Lương công chức quốc phòng', 2024, N'00', N'Lương công chức quốc phòng', N'Dưỡng sức, PHSK sau nghỉ thai sản', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:55:25.277' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'dc958a25-4f07-48c1-a02d-8838bdf0b6db', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'CONOM_LBH_TT', N'Lương sĩ quan', 2024, N'00', N'Lương sĩ quan', N'Con ốm - Lương bảo hiểm thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:29:37.330' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2024, N'00', N'Phụ cấp chức vụ - SQ', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:01:52.060' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'4b8377d3-950c-4278-bc62-4b29a33b062c', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'KHHGD_LBH_TT', N'Lương công chức quốc phòng', 2024, N'00', N'Lương công chức quốc phòng', N'KHHGĐ - Lương BH thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:59:28.350' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'a914d9d4-91b2-41d0-8be5-824316a08083', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'KT_LBH_TT', N'Lương công chức quốc phòng', 2024, N'00', N'Lương công chức quốc phòng', N'Khám thai - Lương BH thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:06:34.477' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'1101adef-d533-4c59-be12-d2cd1e8189a0', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.1', NULL, N'NAMNGHIVIEC_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2024, N'00', N'Phụ cấp chức vụ - CNQP', N'Nam nghỉ việc khi vợ snh con - PCCV BH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:05:12.470' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'59ece0d7-50fb-4be7-94f8-1502a6daf49d', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'NAMNGHIVIEC_LBH_TT', N'Phụ cấp quân hàm', 2024, N'00', N'Phụ cấp quân hàm', N'Nam nghỉ việc khi vợ sinh con - Lương BH thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:20.517' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH4', N'3.2', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2024, N'00', N'Phụ cấp chức vụ - CCQP', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:52:20.157' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'f93a4718-9a11-42c1-8b4f-9db792c58fed', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.1', NULL, N'OK_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2024, N'00', N'Phụ cấp chức vụ - CNQP', N'Ốm khác - Trên 14 ngày - PCCVBH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:53:09.397' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'f93a4718-9a11-42c1-8b4f-9db792c58fed', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'OK_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2024, N'00', N'Phụ cấp chức vụ - SQ', N'Ốm khác - Trên 14 ngày - PCCVBH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:53:31.990' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'f93a4718-9a11-42c1-8b4f-9db792c58fed', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'OK_T14N_PCCVBH_TT', N'Phụ cấp chức vụ', 2024, N'00', N'Phụ cấp chức vụ', N'Ốm khác - Trên 14 ngày - PCCVBH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:06:16.270' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'1101adef-d533-4c59-be12-d2cd1e8189a0', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'NAMNGHIVIEC_PCCVBH_TT', N'Phụ cấp chức vụ', 2024, N'00', N'Phụ cấp chức vụ', N'Nam nghỉ việc khi vợ snh con - PCCV BH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:48:49.220' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2024, N'00', N'Phụ cấp chức vụ - VCQP', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:54:17.893' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'dc958a25-4f07-48c1-a02d-8838bdf0b6db', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'CONOM_LBH_TT', N'Lương viên chức quốc phòng', 2024, N'00', N'Lương viên chức quốc phòng', N'Con ốm - Lương bảo hiểm thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:35:55.650' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2024, N'00', N'Phụ cấp chức vụ - VCQP', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:04:58.483' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'59ece0d7-50fb-4be7-94f8-1502a6daf49d', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'NAMNGHIVIEC_LBH_TT', N'Lương công chức quốc phòng', 2024, N'00', N'Lương công chức quốc phòng', N'Nam nghỉ việc khi vợ sinh con - Lương BH thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:40:34.407' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'62cc5656-1345-48ac-9a5b-66d6031d1ec8', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'THAISAN_DUONGSUCPHSK', N'Lương viên chức quốc phòng', 2024, N'00', N'Lương viên chức quốc phòng', N'Dưỡng sức, PHSK sau nghỉ thai sản', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:00:14.283' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'7cd90ee2-ff39-469f-94b0-40115adeec03', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'KT_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2024, N'00', N'Phụ cấp chức vụ - VCQP', N'Khám thai - PCCV BH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:29:11.403' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH4', N'1', NULL, N'BDN_D14N_LBH_TT', N'Lương sĩ quan', 2024, N'00', N'Lương sĩ quan', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:54:38.213' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'dc958a25-4f07-48c1-a02d-8838bdf0b6db', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'CONOM_LBH_TT', N'Lương công nhân quốc phòng', 2024, N'00', N'Lương công nhân quốc phòng', N'Con ốm - Lương bảo hiểm thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:03:25.027' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'fdfae951-8eb4-4a8a-b9af-ad68326ad742', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'KHHGD_PCCVBH_TT', N'Phụ cấp chức vụ', 2024, N'00', N'Phụ cấp chức vụ', N'KHHGĐ- PCCV BH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:04:47.877' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'59ece0d7-50fb-4be7-94f8-1502a6daf49d', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'NAMNGHIVIEC_LBH_TT', N'Lương công nhân quốc phòng', 2024, N'00', N'Lương công nhân quốc phòng', N'Nam nghỉ việc khi vợ sinh con - Lương BH thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:02:53.260' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'fdfae951-8eb4-4a8a-b9af-ad68326ad742', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'KHHGD_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2024, N'00', N'Phụ cấp chức vụ - VCQP', N'KHHGĐ- PCCV BH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:40:02.033' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'62cc5656-1345-48ac-9a5b-66d6031d1ec8', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'THAISAN_DUONGSUCPHSK', N'Lương sĩ quan', 2024, N'00', N'Lương sĩ quan', N'Dưỡng sức, PHSK sau nghỉ thai sản', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:02:09.907' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'4b8377d3-950c-4278-bc62-4b29a33b062c', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'KHHGD_LBH_TT', N'Phụ cấp quân hàm', 2024, N'00', N'Phụ cấp quân hàm', N'KHHGĐ - Lương BH thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:35:43.643' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.1', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2024, N'00', N'Phụ cấp chức vụ - CNQP', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:03:05.237' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'fdfae951-8eb4-4a8a-b9af-ad68326ad742', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.1', NULL, N'KHHGD_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2024, N'00', N'Phụ cấp chức vụ - CNQP', N'KHHGĐ- PCCV BH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:47.677' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH4', N'2', NULL, N'OK_D14N_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2024, N'00', N'Lương quân nhân chuyên nghiệp', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:56:04.270' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'99c33598-1205-4d57-abd6-7c3d921631cb', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'CONOM_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2024, N'00', N'Phụ cấp chức vụ - CCQP', N'Con ốm - PCCVBH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:37:19.530' AS DateTime), CAST(N'2024-05-20T15:42:32.430' AS DateTime), NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'136886af-46cd-49e1-933c-8ccb4fd90432', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'TAINANLD_DUONGSUCPHSK', N'Lương công chức quốc phòng', 2024, N'00', N'Lương công chức quốc phòng', N'Dưỡng sức phục hồi sức khỏe sau TNLĐ', N'6001', N'70', N'admin', N'admin', N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:36:36.483' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'cd0e6c4b-4c24-4b5b-ac07-0eb2add58210', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'OMDAU_DUONGSUCPHSK', N'Lương công chức quốc phòng', 2024, N'00', N'Lương công chức quốc phòng', N'Dưỡng sức, PHSK sau nghỉ ốm đau', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:45:56.973' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'af321143-0afe-416f-a4a1-d9af2a001707', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'BDN_T14N_PCCVBH_TT', N'Phụ cấp chức vụ', 2024, N'00', N'Phụ cấp chức vụ', N'Bệnh dài ngày - Trên 14 ngày - PCCV BH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:04.167' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'OK_D14N_LBH_TT', N'Lương viên chức quốc phòng', 2024, N'00', N'Lương viên chức quốc phòng', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:37:42.497' AS DateTime), CAST(N'2024-05-20T15:42:32.430' AS DateTime), NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'136886af-46cd-49e1-933c-8ccb4fd90432', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'TAINANLD_DUONGSUCPHSK', N'Phụ cấp quân hàm', 2024, N'00', N'Phụ cấp quân hàm', N'Dưỡng sức phục hồi sức khỏe sau TNLĐ', N'6449', N'10', N'admin', N'admin', N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:03:55.173' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'fdfae951-8eb4-4a8a-b9af-ad68326ad742', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'KHHGD_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2024, N'00', N'Phụ cấp chức vụ - SQ', N'KHHGĐ- PCCV BH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:31.923' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6400', N'CACH4', N'4', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ', 2024, N'00', N'Phụ cấp chức vụ', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:02:24.587' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'4b8377d3-950c-4278-bc62-4b29a33b062c', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'KHHGD_LBH_TT', N'Lương sĩ quan', 2024, N'00', N'Lương sĩ quan', N'KHHGĐ - Lương BH thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:40:33.213' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'6b0fd88b-f8e6-4738-9a5e-3877dd618b14', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.3', NULL, N'BDN_T14N_LBH_TT', N'Lương viên chức quốc phòng', 2024, N'00', N'Lương viên chức quốc phòng', N'Bệnh dài ngày - Trên 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:52:58.237' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'f93a4718-9a11-42c1-8b4f-9db792c58fed', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'OK_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2024, N'00', N'Phụ cấp chức vụ - CCQP', N'Ốm khác - Trên 14 ngày - PCCVBH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:34.477' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'OK_D14N_LBH_TT', N'Lương công chức quốc phòng', 2024, N'00', N'Lương công chức quốc phòng', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:31:08.730' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2024, N'00', N'Phụ cấp chức vụ - QNCN', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:32:48.443' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'BDN_D14N_LBH_TT', N'Phụ cấp quân hàm', 2024, N'00', N'Phụ cấp quân hàm', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:01:43.900' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'4b8377d3-950c-4278-bc62-4b29a33b062c', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'KHHGD_LBH_TT', N'Lương công nhân quốc phòng', 2024, N'00', N'Lương công nhân quốc phòng', N'KHHGĐ - Lương BH thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:44:56.893' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'af321143-0afe-416f-a4a1-d9af2a001707', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'BDN_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2024, N'00', N'Phụ cấp chức vụ - CCQP', N'Bệnh dài ngày - Trên 14 ngày - PCCV BH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:51:23.470' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'1a119acc-2f19-4d58-adf3-82a477c4dc68', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'OK_T14N_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2024, N'00', N'Lương quân nhân chuyên nghiệp', N'Ốm khác - Trên 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:35:55.943' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'cd0e6c4b-4c24-4b5b-ac07-0eb2add58210', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'OMDAU_DUONGSUCPHSK', N'Phụ cấp quân hàm', 2024, N'00', N'Phụ cấp quân hàm', N'Dưỡng sức, PHSK sau nghỉ ốm đau', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:03:12.957' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'fdfae951-8eb4-4a8a-b9af-ad68326ad742', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'KHHGD_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2024, N'00', N'Phụ cấp chức vụ - CCQP', N'KHHGĐ- PCCV BH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:00:58.020' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'7cd90ee2-ff39-469f-94b0-40115adeec03', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'KT_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2024, N'00', N'Phụ cấp chức vụ - SQ', N'Khám thai - PCCV BH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:53:22.133' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'f93a4718-9a11-42c1-8b4f-9db792c58fed', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'OK_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2024, N'00', N'Phụ cấp chức vụ - QNCN', N'Ốm khác - Trên 14 ngày - PCCVBH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:40:59.047' AS DateTime), CAST(N'2024-05-17T17:41:58.600' AS DateTime), NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'6b0fd88b-f8e6-4738-9a5e-3877dd618b14', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'BDN_T14N_LBH_TT', N'Lương công nhân quốc phòng', 2024, N'00', N'Lương công nhân quốc phòng', N'Bệnh dài ngày - Trên 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'40', N'admin', N'admin', N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:34:46.483' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f32c3070-ceee-4c70-ba44-4adc661414bf', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH4', N'3.3', NULL, N'BDN_D14N_LBH_TT', N'Lương viên chức quốc phòng', 2024, N'00', N'Lương viên chức quốc phòng', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'30', N'admin', NULL, N'1010000-010-011-6000-6001-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:01:10.037' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'7cd90ee2-ff39-469f-94b0-40115adeec03', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'KT_PCCVBH_TT', N'Phụ cấp chức vụ', 2024, N'00', N'Phụ cấp chức vụ', N'Khám thai - PCCV BH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:50:07.500' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2024, N'00', N'Phụ cấp chức vụ - QNCN', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:06:07.810' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'1101adef-d533-4c59-be12-d2cd1e8189a0', N'011', N'010', N'1010000', N'6100', N'CACH2', N'2', NULL, N'NAMNGHIVIEC_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2024, N'00', N'Phụ cấp chức vụ - QNCN', N'Nam nghỉ việc khi vợ snh con - PCCV BH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:31:08.730' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH4', N'2', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2024, N'00', N'Phụ cấp chức vụ - QNCN', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:34:33.177' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'BDN_D14N_LBH_TT', N'Lương công nhân quốc phòng', 2024, N'00', N'Lương công nhân quốc phòng', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:36:14.633' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'cd0e6c4b-4c24-4b5b-ac07-0eb2add58210', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'OMDAU_DUONGSUCPHSK', N'Lương công nhân quốc phòng', 2024, N'00', N'Lương công nhân quốc phòng', N'Dưỡng sức, PHSK sau nghỉ ốm đau', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:51:12.317' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'1a119acc-2f19-4d58-adf3-82a477c4dc68', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.2', NULL, N'OK_T14N_LBH_TT', N'Lương công chức quốc phòng', 2024, N'00', N'Lương công chức quốc phòng', N'Ốm khác - Trên 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:42:59.323' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'6b0fd88b-f8e6-4738-9a5e-3877dd618b14', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'BDN_T14N_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2024, N'00', N'Lương quân nhân chuyên nghiệp', N'Bệnh dài ngày - Trên 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:00:29.563' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'7cd90ee2-ff39-469f-94b0-40115adeec03', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.1', NULL, N'KT_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2024, N'00', N'Phụ cấp chức vụ - CNQP', N'Khám thai - PCCV BH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:35:55.650' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH4', N'3.3', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2024, N'00', N'Phụ cấp chức vụ - VCQP', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:33:57.300' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ', 2024, N'00', N'Phụ cấp chức vụ', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:31.923' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ', 2024, N'00', N'Phụ cấp chức vụ', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:55:54.213' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'99c33598-1205-4d57-abd6-7c3d921631cb', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.1', NULL, N'CONOM_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2024, N'00', N'Phụ cấp chức vụ - CNQP', N'Con ốm - PCCVBH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:44:18.903' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'90eeca80-1a7f-4699-81f6-99dbe9914dc8', NULL, N'af321143-0afe-416f-a4a1-d9af2a001707', N'011', N'010', N'1010000', N'6100', N'CACH2', N'1', NULL, N'BDN_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - SQ', 2024, N'00', N'Phụ cấp chức vụ - SQ', N'Bệnh dài ngày - Trên 14 ngày - PCCV BH thành tiền', N'6101', N'10', N'admin', NULL, N'1010000-010-011-6100-6101-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:35:13.993' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6100', N'CACH4', N'3.2', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2024, N'00', N'Phụ cấp chức vụ - CCQP', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:08.773' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'fd15c855-615e-4107-8a91-b538f7d5de22', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH4', N'3.1', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CNQP', 2024, N'00', N'Phụ cấp chức vụ - CNQP', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'40', N'admin', NULL, N'1010000-010-011-6100-6101-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:48:32.767' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH4', N'1', NULL, N'OK_D14N_LBH_TT', N'Lương sĩ quan', 2024, N'00', N'Lương sĩ quan', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:55:39.550' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'99c33598-1205-4d57-abd6-7c3d921631cb', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'CONOM_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2024, N'00', N'Phụ cấp chức vụ - VCQP', N'Con ốm - PCCVBH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:32:48.443' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6400', N'CACH4', N'4', NULL, N'BDN_D14N_LBH_TT', N'Phụ cấp quân hàm', 2024, N'00', N'Phụ cấp quân hàm', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:06:43.210' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'1101adef-d533-4c59-be12-d2cd1e8189a0', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'NAMNGHIVIEC_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2024, N'00', N'Phụ cấp chức vụ - VCQP', N'Nam nghỉ việc khi vợ snh con - PCCV BH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:30:42.810' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'BDN_D14N_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2024, N'00', N'Lương quân nhân chuyên nghiệp', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:50:07.500' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'1076b8a5-e6eb-4fdd-a7d7-11d86c026a20', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH4', N'2', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - QNCN', 2024, N'00', N'Phụ cấp chức vụ - QNCN', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'20', N'admin', NULL, N'1010000-010-011-6100-6101-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:48:32.767' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'OK_D14N_LBH_TT', N'Lương sĩ quan', 2024, N'00', N'Lương sĩ quan', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:55.983' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'OK_D14N_LBH_TT', N'Phụ cấp quân hàm', 2024, N'00', N'Phụ cấp quân hàm', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:47:14.757' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'774a99e9-b02d-409d-80c6-07532853b651', N'011', N'010', N'1010000', N'6000', N'CACH4', N'3.1', NULL, N'OK_D14N_LBH_TT', N'Lương công nhân quốc phòng', 2024, N'00', N'Lương công nhân quốc phòng', N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:59:35.220' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'a914d9d4-91b2-41d0-8be5-824316a08083', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'KT_LBH_TT', N'Phụ cấp quân hàm', 2024, N'00', N'Phụ cấp quân hàm', N'Khám thai - Lương BH thành tiền', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:56:26.247' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'99c33598-1205-4d57-abd6-7c3d921631cb', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'CONOM_PCCVBH_TT', N'Phụ cấp chức vụ', 2024, N'00', N'Phụ cấp chức vụ', N'Con ốm - PCCVBH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T18:05:27.197' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'59ece0d7-50fb-4be7-94f8-1502a6daf49d', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'NAMNGHIVIEC_LBH_TT', N'Lương sĩ quan', 2024, N'00', N'Lương sĩ quan', N'Nam nghỉ việc khi vợ sinh con - Lương BH thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:33:57.300' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'073699af-9e9a-4c4b-960c-e82dcab00856', NULL, N'e0f66df9-e893-46cc-9897-acf5e5535134', N'011', N'010', N'1010000', N'6400', N'CACH4', N'4', NULL, N'BDN_D14N_PCCVBH_TT', N'Phụ cấp chức vụ', 2024, N'00', N'Phụ cấp chức vụ', N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', N'6449', N'30', N'admin', NULL, N'1010000-010-011-6400-6449-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:34:57.913' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'13cc0860-a1d7-4192-9e66-e7cd83e62aab', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH4', N'3.2', NULL, N'BDN_D14N_LBH_TT', N'Lương công chức quốc phòng', 2024, N'00', N'Lương công chức quốc phòng', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'70', N'admin', NULL, N'1010000-010-011-6000-6001-70-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:54:58.230' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'18bcfd16-6919-46e6-8963-e54f2a503388', NULL, N'dc958a25-4f07-48c1-a02d-8838bdf0b6db', N'011', N'010', N'1010000', N'6000', N'CACH2', N'2', NULL, N'CONOM_LBH_TT', N'Lương quân nhân chuyên nghiệp', 2024, N'00', N'Lương quân nhân chuyên nghiệp', N'Con ốm - Lương bảo hiểm thành tiền', N'6001', N'20', N'admin', NULL, N'1010000-010-011-6000-6001-20-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-20T09:39:21.200' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'a7185d31-d137-4e81-87dd-b9a39eecf264', NULL, N'62cc5656-1345-48ac-9a5b-66d6031d1ec8', N'011', N'010', N'1010000', N'6400', N'CACH2', N'4', NULL, N'THAISAN_DUONGSUCPHSK', N'Phụ cấp quân hàm', 2024, N'00', N'Phụ cấp quân hàm', N'Dưỡng sức, PHSK sau nghỉ thai sản', N'6449', N'10', N'admin', NULL, N'1010000-010-011-6400-6449-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:29:11.403' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'db92fe6b-b243-4bcc-8134-ddbd1ba64963', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'BDN_D14N_LBH_TT', N'Lương sĩ quan', 2024, N'00', N'Lương sĩ quan', N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:51:01.460' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'f2111e56-5f62-43a2-8b2d-5b247e114b4d', NULL, N'1a119acc-2f19-4d58-adf3-82a477c4dc68', N'011', N'010', N'1010000', N'6000', N'CACH2', N'3.1', NULL, N'OK_T14N_LBH_TT', N'Lương công nhân quốc phòng', 2024, N'00', N'Lương công nhân quốc phòng', N'Ốm khác - Trên 14 ngày - Lương bảo hiểm thành tiền', N'6001', N'40', N'admin', NULL, N'1010000-010-011-6000-6001-40-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:52:47.797' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'10d2675e-1a94-4a4e-af14-5e4fe4cf2d11', NULL, N'f93a4718-9a11-42c1-8b4f-9db792c58fed', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.3', NULL, N'OK_T14N_PCCVBH_TT', N'Phụ cấp chức vụ - VCQP', 2024, N'00', N'Phụ cấp chức vụ - VCQP', N'Ốm khác - Trên 14 ngày - PCCVBH thành tiền', N'6101', N'30', N'admin', NULL, N'1010000-010-011-6100-6101-30-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:42:33.750' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'89383363-cbd5-4f1e-a07a-d06fc0440a69', NULL, N'6b0fd88b-f8e6-4738-9a5e-3877dd618b14', N'011', N'010', N'1010000', N'6000', N'CACH2', N'1', NULL, N'BDN_T14N_LBH_TT', N'Lương sĩ quan', 2024, N'00', N'Lương sĩ quan', N'Bệnh dài ngày - Trên 14 ngày -  Lương bảo hiểm thành tiền', N'6001', N'10', N'admin', NULL, N'1010000-010-011-6000-6001-10-00', N'', N'', N'', N'')
GO
INSERT [dbo].[TL_PhuCap_MLNS] ([ID], [ChiTietToi], [DateCreated], [DateModified], [iTrangThai], [idCachTinhLuong], [idMlns], [idNguonNganSach], [idPhuCap], [K], [L], [LNS], [M], [Ma_CachTL], [Ma_Cb], [Ma_NguonNganSach], [Ma_PhuCap], [MoTa], [Nam], [NG], [NguonNganSach], [Ten_PhuCap], [TM], [TTM], [UserCreator], [UserModifier], [XauNoiMa], [sTNG], [sTNG1], [sTNG2], [sTNG3]) VALUES (NEWID(), NULL, CAST(N'2024-05-17T17:49:20.517' AS DateTime), NULL, NULL, N'ed7c2ab8-da85-4b74-a4be-47e968f0a86c', N'ce95e2b0-00d4-4ebb-8285-64d1914a3a00', NULL, N'bb3c6995-5fa6-454f-93ed-fe292c0a88ac', N'011', N'010', N'1010000', N'6100', N'CACH2', N'3.2', NULL, N'OK_D14N_PCCVBH_TT', N'Phụ cấp chức vụ - CCQP', 2024, N'00', N'Phụ cấp chức vụ - CCQP', N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', N'6101', N'70', N'admin', NULL, N'1010000-010-011-6100-6101-70-00', N'', N'', N'', N'')
GO