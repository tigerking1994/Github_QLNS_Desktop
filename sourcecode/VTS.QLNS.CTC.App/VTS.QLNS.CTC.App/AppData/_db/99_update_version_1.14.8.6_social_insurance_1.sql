/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]    Script Date: 10/2/2024 6:57:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]    Script Date: 10/2/2024 6:57:15 PM ******/
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

	select 
		mlns.iID_MLNS iID_MucLucNganSach,
		mlns.iID_MLNS_Cha iID_MLNS_Cha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		mlns.sXauNoiMa XauNoiMa,
		mlns.sMoTa STenBhMLNS,
		chungtu.iID_MaDonVi iID_MaDonVi,
		chungtu.sTenDonVi,
		chungtu.iQSBQNam,
		chungtu.fLuongChinh,
		chungtu.fPhuCapChucVu,
		chungtu.fPCTNNghe,
		chungtu.fPCTNVuotKhung,
		chungtu.fNghiOm,
		chungtu.fHSBL,
		chungtu.fTongQTLN,
		chungtu.fThu_BHXH_NLD,
		chungtu.fThu_BHXH_NSD,
		chungtu.fTongThuBHXH,
		chungtu.fThu_BHYT_NLD,
		chungtu.fThu_BHYT_NSD,
		chungtu.fTongThuBHYT,
		chungtu.fThu_BHTN_NLD,
		chungtu.fThu_BHTN_NSD,
		chungtu.fTongThuBHTN,
		chungtu.fTongCong
	into #tbl_temp_thchdv
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
				and iNamLamViec = @NamLamViec
				) mlns
				left join
	(select
					ctct.sXauNoiMa,
					ct.iID_MaDonVi,
					dv.sTenDonVi,
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
				left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec AND dv.iTrangThai = 1 
				where ct.iID_MaDonVi in ((SELECT * FROM f_split(@MaDonVi)))
					and ct.iNamLamViec = @NamLamViec
					and ct.bDaTongHop = 1
					and ct.iLoaiTongHop = @LoaiChungTu
				group by
					ctct.sXauNoiMa,
					ct.iID_MaDonVi,
					dv.sTenDonVi
				) chungtu on mlns.sXauNoiMa = chungtu.sXauNoiMa

				union

				select 
					mlns.iID_MLNS,
					mlns.iID_MLNS_Cha,
					mlns.bHangCha,
					mlns.sLNS,
					mlns.sXauNoiMa,
					sMoTa,
					ct2.iID_MaDonVi,
					ct2.sTenDonVi,
					ct2.iQSBQNam,
					ct2.fLuongChinh,
					ct2.fPhuCapChucVu,
					ct2.fPCTNNghe,
					ct2.fPCTNVuotKhung,
					ct2.fNghiOm,
					ct2.fHSBL,
					ct2.fTongQTLN,
					ct2.fThu_BHXH_NLD,
					ct2.fThu_BHXH_NSD,
					ct2.fTongThuBHXH,
					ct2.fThu_BHYT_NLD,
					ct2.fThu_BHYT_NSD,
					ct2.fTongThuBHYT,
					ct2.fThu_BHTN_NLD,
					ct2.fThu_BHTN_NSD,
					ct2.fTongThuBHTN,
					ct2.fTongCong
				
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
				and iNamLamViec = @NamLamViec
				) mlns
				left join
				(select distinct
					ctct.sXauNoiMa,
					'' iID_MaDonVi,
					'' sTenDonVi,
					0 iQSBQNam,
					0 fLuongChinh,
					0 fPhuCapChucVu,
					0 fPCTNNghe,
					0 fPCTNVuotKhung,
					0 fNghiOm,
					0 fHSBL,
					0 fTongQTLN,
					0 fThu_BHXH_NLD,
					0 fThu_BHXH_NSD,
					0 fTongThuBHXH,
					0 fThu_BHYT_NLD,
					0 fThu_BHYT_NSD,
					0 fTongThuBHYT,
					0 fThu_BHTN_NLD,
					0 fThu_BHTN_NSD,
					0 fTongThuBHTN,
					0 fTongCong
				from 
				BH_KHT_BHXH_ChiTiet ctct
				join BH_KHT_BHXH ct on ctct.iID_KHT_BHXH = ct.iID_KHT_BHXH
			
				where ct.iID_MaDonVi in ((SELECT * FROM f_split(@MaDonVi)))
					and ct.iNamLamViec = @NamLamViec
					and ct.bDaTongHop = 1
					and ct.iLoaiTongHop = @LoaiChungTu
				group by
					ctct.sXauNoiMa,
					ct.iID_MaDonVi) ct2 on mlns.sXauNoiMa = ct2.sXauNoiMa

				order by mlns.sXauNoiMa
			
		update #tbl_temp_thchdv set STenBhMLNS = concat('  ',sTenDonVi),
		iID_MLNS_Cha = iID_MucLucNganSach
		where isnull(iID_MaDonVi, '') <> ''

		update #tbl_temp_thchdv set IsHangCha = 1
		where isnull(iID_MaDonVi, '') = ''

		select * from #tbl_temp_thchdv order by XauNoiMa
END
GO
