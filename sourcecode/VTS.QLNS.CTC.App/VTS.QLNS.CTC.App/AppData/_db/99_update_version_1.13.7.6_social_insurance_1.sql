/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 1/3/2024 4:34:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]    Script Date: 1/3/2024 4:34:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]    Script Date: 1/3/2024 4:34:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_du_toan_nhan_dtt]    Script Date: 1/3/2024 4:34:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_tong_du_toan_nhan_dtt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_tong_du_toan_nhan_dtt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_kht]    Script Date: 1/3/2024 4:34:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_kht]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_kht]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]    Script Date: 1/3/2024 4:34:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]    Script Date: 1/3/2024 4:34:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_export_thongtricaptamung_kcbbhyt]
@IdCsYTe NVARCHAR(MAX),
@ILoai int,
@LNS NVARCHAR(MAX),
@IQuy int,
@NamLamViec int,
@UserName NVARCHAR(100),
@Donvitinh int
As
begin
	select 
		mucluc.iID_MLNS_Cha,
		mucluc.iID_MLNS,
		mucluc.sLNS,
		mucluc.sL,
		mucluc.sK,
		mucluc.sM,
		mucluc.sTM,
		mucluc.sTTM,
		mucluc.sNG,
		mucluc.sMoTa,
		mucluc.bHangCha,
		mucluc.sXauNoiMa
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as mucluc
		where mucluc.iNamLamViec = @NamLamViec and mucluc.sLNS In (SELECT * FROM f_split(@LNS))

	
	select 
		cp_ct.iID_MLNS,
		sum(cp_ct.fQTQuyTruoc) as fQTQuyTruoc,
		sum(cp_ct.fTamUngQuyNay) as fTamUngQuyNay,
		sum(cp_ct.fLuyKeCapDenCuoiQuy) as fLuyKeCapDenCuoiQuy
		into #tblCapPhatChiTiet
		from BH_CP_CapTamUng_KCB_BHYT_ChiTiet as cp_ct
		inner join BH_CP_CapTamUng_KCB_BHYT as  cp on cp.iID_BH_CP_CapTamUng_KCB_BHYT = cp_ct.iID_BH_CP_CapTamUng_KCB_BHYT
		where cp_ct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
		and cp.iQuy = @IQuy and cp.iNamLamViec = @NamLamViec 
		and ((@ILoai = 5 and cp_ct.sLNS like '9040001') or (@ILoai = 6 and cp_ct.sLNS like '9040002'))
		group by cp_ct.iID_MLNS

	select 
		mucluc.iID_MLNS_Cha,
		mucluc.iID_MLNS,
		mucluc.sLNS,
		mucluc.sL,
		mucluc.sK,
		mucluc.sM,
		mucluc.sTM,
		mucluc.sTTM,
		mucluc.sNG,
		mucluc.sMoTa,
		mucluc.bHangCha,
		cp_ct.fLuyKeCapDenCuoiQuy,
		cp_ct.fQTQuyTruoc,
		cp_ct.fTamUngQuyNay
		from #tblMucLucNganSach as mucluc
		left  join #tblCapPhatChiTiet as cp_ct on  mucluc.iID_MLNS = cp_ct.iID_MLNS
		order by mucluc.sXauNoiMa


	drop table #tblMucLucNganSach;
	drop table #tblCapPhatChiTiet;
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_kht]    Script Date: 1/3/2024 4:34:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_luong_kht]
	@NamLamViec int
AS
BEGIN
	select 
	ctct.iID_MucLucNganSach iID_MLNS,
	sum(ctct.iQSBQNam) iQSBQNam,
	sum(ctct.fLuongChinh) fLuongChinh,
	sum(ctct.fPhuCapChucVu) fPhuCapChucVu,
	sum(ctct.fPCTNNghe) fPCTNNghe,
	sum(ctct.fPCTNVuotKhung) fPCTNVuotKhung,
	sum(ctct.fNghiOm) fNghiOm,
	sum(ctct.fHSBL) fHSBL,
	sum(ctct.fTongQTLN) fTongQTLN
	from
	(select iID_KHT_BHXH from BH_KHT_BHXH
	where iNamLamViec = @NamLamViec
	and iLoaiTongHop = 1
	and bIsKhoa = 1) ct
	join BH_KHT_BHXH_ChiTiet ctct
	on ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
	group by 
	ctct.iID_MucLucNganSach
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_du_toan_nhan_dtt]    Script Date: 1/3/2024 4:34:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_tong_du_toan_nhan_dtt]
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	select 
		ctct.iID_MLNS,
		sum(ctct.fTongCong) fTongCong
	from
		BH_DTT_BHXH_ChungTu_ChiTiet ctct
		join BH_DTT_BHXH_ChungTu ct on ctct.iID_DTT_BHXH = ct.iID_DTT_BHXH
	where ct.iNamLamViec = @NamLamViec
		and ct.iID_MaDonVi = @MaDonVi
		and ct.bIsKhoa = 1
		group by
		ctct.iID_MLNS
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi]    Script Date: 1/3/2024 4:34:55 PM ******/
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
		report.STenBhMLNS,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop]    Script Date: 1/3/2024 4:34:55 PM ******/
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
		report.STenBhMLNS,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 1/3/2024 4:34:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int
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
	sum(fThuBHXH_NLD) DttDauNam,
	sum(fThuBHXH_NLD_QTDauNam) Dtt6ThangDauNam,
	sum(fThuBHXH_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
	sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam) TongCong,
	((sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam)) - sum(fThuBHXH_NLD)) Tang,
	(sum(fThuBHXH_NLD) - (sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.sLNS = '9020001' -- Khối dự toán
	union
	select
	6 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	sum(fThuBHXH_NSD) BhxhNsdDauNam,
	sum(fThuBHXH_NSD_QTDauNam) BhxhNsd6ThangDauNam,
	sum(fThuBHXH_NSD_QTCuoiNam) BhxhNsd6ThangCuoiNam,
	sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam) TongCong,
	((sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam)) - sum(fThuBHXH_NSD)) Tang,
	(sum(fThuBHXH_NSD) - (sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union
	select
	8 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	sum(fThuBHXH_NLD) DttDauNam,
	sum(fThuBHXH_NLD_QTDauNam) Dtt6ThangDauNam,
	sum(fThuBHXH_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
	sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam) TongCong,
	((sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam)) - sum(fThuBHXH_NLD)) Tang,
	(sum(fThuBHXH_NLD) - (sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	9 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	sum(fThuBHXH_NSD) BhxhNsdDauNam,
	sum(fThuBHXH_NSD_QTDauNam) Dtt6ThangDauNam,
	sum(fThuBHXH_NSD_QTCuoiNam) Dtt6ThangCuoiNam,
	sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam) TongCong,
	((sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam)) - sum(fThuBHXH_NSD)) Tang,
	(sum(fThuBHXH_NSD) - (sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.sLNS = '9020002' -- Khối hạch toán
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	16 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	sum(0) DttDauNam,
	sum(0) Dtt6ThangDauNam,
	sum(0) Dtt6ThangCuoiNam,
	sum(0) TongCong,
	sum(0) Tang,
	sum(0) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
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
	sum(fThuBHYT_NLD) DttDauNam,
	sum(fThuBHYT_NLD_QTDauNam) Dtt6ThangDauNam,
	sum(fThuBHYT_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
	sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) TongCong,
	((sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam)) - sum(fThuBHYT_NLD)) Tang,
	(sum(fThuBHYT_NLD) - (sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
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
	sum(fThuBHYT_NSD) DttDauNam,
	sum(fThuBHYT_NSD_QTDauNam) Dtt6ThangDauNam,
	sum(fThuBHYT_NSD_QTCuoiNam) Dtt6ThangCuoiNam,
	sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam) TongCong,
	((sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam)) - sum(fThuBHYT_NSD)) Tang,
	(sum(fThuBHYT_NSD) - (sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
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
	sum(0) DttDauNam,
	sum(0) Dtt6ThangDauNam,
	sum(0) Dtt6ThangCuoiNam,
	sum(0) TongCong,
	sum(0) Tang,
	sum(0) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
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
	sum(fThuBHYT_NLD) DttDauNam,
	sum(fThuBHYT_NLD_QTDauNam) Dtt6ThangDauNam,
	sum(fThuBHYT_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
	sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) TongCong,
	((sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam)) - sum(fThuBHYT_NLD)) Tang,
	(sum(fThuBHYT_NLD) - (sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
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
	sum(fThuBHYT_NSD) DttDauNam,
	sum(fThuBHYT_NSD_QTDauNam) Dtt6ThangDauNam,
	sum(fThuBHYT_NSD_QTCuoiNam) Dtt6ThangCuoiNam,
	sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam) TongCong,
	((sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam)) - sum(fThuBHYT_NSD)) Tang,
	(sum(fThuBHYT_NSD) - (sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
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
	sum(fThuBHTN_NLD) DttDauNam,
	sum(fThuBHTN_NLD_QTDauNam) Dtt6ThangDauNam,
	sum(fThuBHTN_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
	sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam) TongCong,
	((sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam)) - sum(fThuBHTN_NLD)) Tang,
	(sum(fThuBHTN_NLD) - (sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.sLNS = '9020001' -- Khối dự toán
	union
	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	sum(fThuBHTN_NSD) BhxhNsdDauNam,
	sum(fThuBHTN_NSD_QTDauNam) BhxhNsd6ThangDauNam,
	sum(fThuBHTN_NSD_QTCuoiNam) BhxhNsd6ThangCuoiNam,
	sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam) TongCong,
	((sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam)) - sum(fThuBHXH_NSD)) Tang,
	(sum(fThuBHTN_NSD) - (sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu khối hạch toán
	union
	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	sum(fThuBHTN_NLD) DttDauNam,
	sum(fThuBHTN_NLD_QTDauNam) Dtt6ThangDauNam,
	sum(fThuBHTN_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
	sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam) TongCong,
	((sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam)) - sum(fThuBHTN_NLD)) Tang,
	(sum(fThuBHTN_NLD) - (sum(fThuBHTN_NLD_QTDauNam) + sum(fThuBHTN_NLD_QTCuoiNam))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	sum(fThuBHTN_NSD) BhxhNsdDauNam,
	sum(fThuBHTN_NSD_QTDauNam) Dtt6ThangDauNam,
	sum(fThuBHTN_NSD_QTCuoiNam) Dtt6ThangCuoiNam,
	sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam) TongCong,
	((sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam)) - sum(fThuBHTN_NSD)) Tang,
	(sum(fThuBHTN_NSD) - (sum(fThuBHTN_NSD_QTDauNam) + sum(fThuBHTN_NSD_QTCuoiNam))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	union
	select
	22 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	--BHTN
	union
	select
	26 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	0 BHangCha
	from tbl_child_cat
	where STT in (21, 22)
	) parent

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
	sum(DttDauNam) DttDauNam,
	sum(Dtt6ThangDauNam) Dtt6ThangDauNam,
	sum(Dtt6ThangCuoiNam) Dtt6ThangCuoiNam,
	(sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) TongCong,
	((sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam)) - sum(DttDauNam)) Tang,
	(sum(DttDauNam) - (sum(Dtt6ThangDauNam) + sum(Dtt6ThangCuoiNam))) Giam,
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
GO
