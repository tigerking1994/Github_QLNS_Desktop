/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]    Script Date: 4/12/2024 8:57:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]    Script Date: 4/12/2024 8:57:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@SoQuyetDinh nvarchar(200),
	@DVT int,
	@NgayQuyetDinh nvarchar(200)
AS
BEGIN
	---CHI---
	select ctct.* into TBL_DTC from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
	where ctct.iNamLamViec = @NamLamViec 
		and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
		and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
		and Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))

	
	--- khoi du toan 2
	select A.*
	 into #tempKhoiDuToan
	 from TBL_DTC A
	left join DonVi B on A.iID_MaDonVi=B.iID_MaDonVi and A.iNamLamViec=b.iNamLamViec
	where B.iKhoi=2

	--Chi các chế độ BHXH DUTOAN
	select * into TBL_CHI_CHEDO_DUTOAN from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Chi các chế độ BHXH' sMoTa, null fTongSoChi, null fDuToan, null fHachToan
	union all
	select 2 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Ốm đau' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0001%' or sXauNoiMa like '9010002-010-011-0001%'
	union all 
	select 3 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Thai sản' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0002%' or sXauNoiMa like '9010002-010-011-0002%'
	union all 
	select 4 rowNum, 0 bHangCha, null stt, N'- Trợ cấp tai nạn lao động, BNN' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0003%' or sXauNoiMa like '9010002-010-011-0003%'
	union all 
	select 5 rowNum, 0 bHangCha, null stt, N'- Trợ cấp hưu trí' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0004%' or sXauNoiMa like '9010002-010-011-0004%'
	union all 
	select 6 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Phục viên' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0005%' or sXauNoiMa like '9010002-010-011-0005%'
	union all 
	select 7 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Xuất ngũ' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0006%' or sXauNoiMa like '9010002-010-011-0006%'
	union all 
	select 8 rowNum, 0 bHangCha, null stt, N'- Trợ cấp thôi việc' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0007%' or sXauNoiMa like '9010002-010-011-0007%'
	union all 
	select 9 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Tử tuất' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0008%' or sXauNoiMa like '9010002-010-011-0008%'
	union all 
	select 10 rowNum, 1 bHangCha, 2 stt, N'Kinh phí quản lý BHXH, BHYT' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010003%' 
	union all 
	select 11 rowNum, 1 bHangCha, 3 stt, N'Kinh phí KCB tại quân y đơn vị' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010004%' or sXauNoiMa like '9010005%'
	union all 
	select 12 rowNum, 1 bHangCha, 4 stt, N'Kinh phí KCB tại Trường sa - DK' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010006%' or sXauNoiMa like '9010007%'
	) chidutoan

	--- khoi du toan 1
	select A.*
	 into #tempKhoiHoachToan
	 from TBL_DTC A
	left join DonVi B on A.iID_MaDonVi=B.iID_MaDonVi and A.iNamLamViec=b.iNamLamViec
	where B.iKhoi=1


	--Chi các chế độ BHXH hạch toán
	select * into TBL_CHI_CHEDO_HACHTOAN from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Chi các chế độ BHXH' sMoTa, null fTongSoChi, null fDuToan, null fHachToan
	union all
	select 2 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Ốm đau' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0001%' or sXauNoiMa like '9010002-010-011-0001%'
	union all 
	select 3 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Thai sản' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0002%' or sXauNoiMa like '9010002-010-011-0002%'
	union all 
	select 4 rowNum, 0 bHangCha, null stt, N'- Trợ cấp tai nạn lao động, BNN' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0003%' or sXauNoiMa like '9010002-010-011-0003%'
	union all 
	select 5 rowNum, 0 bHangCha, null stt, N'- Trợ cấp hưu trí' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0004%' or sXauNoiMa like '9010002-010-011-0004%'
	union all 
	select 6 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Phục viên' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0005%' or sXauNoiMa like '9010002-010-011-0005%'
	union all 
	select 7 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Xuất ngũ' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0006%' or sXauNoiMa like '9010002-010-011-0006%'
	union all 
	select 8 rowNum, 0 bHangCha, null stt, N'- Trợ cấp thôi việc' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0007%' or sXauNoiMa like '9010002-010-011-0007%'
	union all 
	select 9 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Tử tuất' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0008%' or sXauNoiMa like '9010002-010-011-0008%'
	union all 
	select 10 rowNum, 0 bHangCha, 2 stt, N'Kinh phí quản lý BHXH, BHYT' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010003%' 
	union all 
	select 11 rowNum, 0 bHangCha, 3 stt, N'Kinh phí KCB tại quân y đơn vị' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010004%' or sXauNoiMa like '9010005%'
	union all 
	select 12 rowNum, 0 bHangCha, 4 stt, N'Kinh phí KCB tại Trường sa - DK' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010006%' or sXauNoiMa like '9010007%'
	) chihachtoan

	select dt.rowNum, 
			dt.bHangCha, 
			dt.stt, 
			dt.sMoTa, 
			(isnull(dt.fDuToan, 0) + isnull(ht.fHachToan, 0))/@DVT fTongSoChi, 
			dt.fDuToan/@DVT fDuToan, 
			ht.fHachToan/@DVT fHachToan
	into TBL_DTC_RESULT
	from TBL_CHI_CHEDO_DUTOAN dt
	left join TBL_CHI_CHEDO_HACHTOAN ht on dt.rowNum = ht.rowNum

	update TBL_DTC_RESULT set fTongSoChi = (select sum(fTongSoChi) from TBL_DTC_RESULT where bHangCha = 0),
							fDuToan = (select sum(fDuToan) from TBL_DTC_RESULT where bHangCha = 0),
							fHachToan = (select sum(fHachToan) from TBL_DTC_RESULT where bHangCha = 0)
						where bHangCha = 1 and stt = 1

	--result
	select * from TBL_DTC_RESULT

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC]') AND type in (N'U')) drop table TBL_DTC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_CHI_CHEDO_DUTOAN]') AND type in (N'U')) drop table TBL_CHI_CHEDO_DUTOAN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_CHI_CHEDO_HACHTOAN]') AND type in (N'U')) drop table TBL_CHI_CHEDO_HACHTOAN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC_RESULT]') AND type in (N'U')) drop table TBL_DTC_RESULT;

END
;
GO
