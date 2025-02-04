/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 5/9/2024 1:55:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]    Script Date: 5/9/2024 1:55:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]    Script Date: 5/9/2024 1:55:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@SoQuyetDinh nvarchar(200),
	@DVT int,
	@NgayQuyetDinh nvarchar(200),
	@IsMillionRound bit
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
	select 2 rowNum,
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp Ốm đau' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0001%' or sXauNoiMa like '9010002-010-011-0001%'
	union all 
	select 3 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp Thai sản' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0002%' or sXauNoiMa like '9010002-010-011-0002%'
	union all 
	select 4 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp tai nạn lao động, BNN' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0003%' or sXauNoiMa like '9010002-010-011-0003%'
	union all 
	select 5 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp hưu trí' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0004%' or sXauNoiMa like '9010002-010-011-0004%'
	union all 
	select 6 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp Phục viên' sMoTa,
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0005%' or sXauNoiMa like '9010002-010-011-0005%'
	union all 
	select 7 rowNum, 
			0 bHangCha, 
			null stt,
			N'- Trợ cấp Xuất ngũ' sMoTa,
			null fTongSo,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan,
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0006%' or sXauNoiMa like '9010002-010-011-0006%'
	union all 
	select 8 rowNum,
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp thôi việc' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan,
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0007%' or sXauNoiMa like '9010002-010-011-0007%'
	union all 
	select 9 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp Tử tuất' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0008%' or sXauNoiMa like '9010002-010-011-0008%'
	union all 
	select 10 rowNum, 
			1 bHangCha, 
			2 stt, 
			N'Kinh phí quản lý BHXH, BHYT' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010003%' 
	union all 
	select 11 rowNum, 
			1 bHangCha, 
			3 stt, 
			N'Kinh phí KCB tại quân y đơn vị' sMoTa,
			null fTongSo,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010004%' or sXauNoiMa like '9010005%'
	union all 
	select 12 rowNum,
			1 bHangCha, 
			4 stt, 
			N'Kinh phí KCB tại Trường sa - DK' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
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
	select 2 rowNum, 
			0 bHangCha, 
			null stt,
			N'- Trợ cấp Ốm đau' sMoTa, 
			null fTongSo, 
			null fDuToan, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0001%' or sXauNoiMa like '9010002-010-011-0001%'
	union all 
	select 3 rowNum, 
			0 bHangCha, 
			null stt,
			N'- Trợ cấp Thai sản' sMoTa, 
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0002%' or sXauNoiMa like '9010002-010-011-0002%'
	union all 
	select 4 rowNum,
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp tai nạn lao động, BNN' sMoTa,
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0003%' or sXauNoiMa like '9010002-010-011-0003%'
	union all 
	select 5 rowNum, 
			0 bHangCha,
			null stt, 
			N'- Trợ cấp hưu trí' sMoTa,
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0004%' or sXauNoiMa like '9010002-010-011-0004%'
	union all 
	select 6 rowNum,
			0 bHangCha, 
			null stt,
			N'- Trợ cấp Phục viên' sMoTa,
			null fTongSo,
			null fDuToan, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0005%' or sXauNoiMa like '9010002-010-011-0005%'
	union all 
	select 7 rowNum,
			0 bHangCha,
			null stt,
			N'- Trợ cấp Xuất ngũ' sMoTa,
			null fTongSo, 
			null fDuToan, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0006%' or sXauNoiMa like '9010002-010-011-0006%'
	union all 
	select 8 rowNum,
			0 bHangCha, 
			null stt,
			N'- Trợ cấp thôi việc' sMoTa,
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0007%' or sXauNoiMa like '9010002-010-011-0007%'
	union all 
	select 9 rowNum, 
			0 bHangCha,
			null stt,
			N'- Trợ cấp Tử tuất' sMoTa, 
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010001-010-011-0008%' or sXauNoiMa like '9010002-010-011-0008%'
	union all 
	select 10 rowNum,
			0 bHangCha, 
			2 stt,
			N'Kinh phí quản lý BHXH, BHYT' sMoTa, 
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010003%' 
	union all 
	select 11 rowNum,
			0 bHangCha, 
			3 stt, 
			N'Kinh phí KCB tại quân y đơn vị' sMoTa,
			null fTongSo,
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010004%' or sXauNoiMa like '9010005%'
	union all 
	select 12 rowNum, 
			0 bHangCha, 
			4 stt, 
			N'Kinh phí KCB tại Trường sa - DK' sMoTa, 
			null fTongSo,
			null fDuToan, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010006%' or sXauNoiMa like '9010007%'
	) chihachtoan

	--if (@IsMillionRound = 0)
	--begin
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
	--end
	--else 
	--	begin
	--select dt.rowNum, 
	--		dt.bHangCha, 
	--		dt.stt, 
	--		dt.sMoTa, 
	--		round((isnull(dt.fDuToan, 0) + isnull(ht.fHachToan, 0)) / 1000000, 0) * 1000000 /@DVT fTongSoChi, 
	--		round(dt.fDuToan / 1000000, 0) * 1000000 / @DVT fDuToan, 
	--		round(dt.fHachToan / 1000000, 0) * 1000000 / @DVT fHachToan
	--into TBL_DTC_RESULT1
	--from TBL_CHI_CHEDO_DUTOAN dt
	--left join TBL_CHI_CHEDO_HACHTOAN ht on dt.rowNum = ht.rowNum

	--update TBL_DTC_RESULT1 set fTongSoChi = (select sum(fTongSoChi) from TBL_DTC_RESULT1 where bHangCha = 0),
	--						fDuToan = (select sum(fDuToan) from TBL_DTC_RESULT1 where bHangCha = 0),
	--						fHachToan = (select sum(fHachToan) from TBL_DTC_RESULT1 where bHangCha = 0)
	--					where bHangCha = 1 and stt = 1

	----result
	--select * from TBL_DTC_RESULT1
	--end

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC]') AND type in (N'U')) drop table TBL_DTC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_CHI_CHEDO_DUTOAN]') AND type in (N'U')) drop table TBL_CHI_CHEDO_DUTOAN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_CHI_CHEDO_HACHTOAN]') AND type in (N'U')) drop table TBL_CHI_CHEDO_HACHTOAN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC_RESULT]') AND type in (N'U')) drop table TBL_DTC_RESULT;
	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC_RESULT1]') AND type in (N'U')) drop table TBL_DTC_RESULT1;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 5/9/2024 1:55:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@SoQuyetDinh nvarchar(200),
	@DVT int,
	@NgayQuyetDinh nvarchar(200),
	@IsMillionRound bit
AS
BEGIN
	---THU---
	--Dữ liệu phân bổ dự toán thu BHXH
	select ctct.* into TBL_DTT from BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
	where ctct.iNamLamViec = @NamLamViec 
		and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
		and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
		and Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
	--Thu BHXH
	select * into TBL_THU_BHXH from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Thu BHXH' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 2 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHXH_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHXH_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHXH_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHXH_NSD, 0) END)),0) fTongSo,
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHXH_NLD/1000000, 0)* 1000000 ELSE fBHXH_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHXH_NSD/1000000, 0)* 1000000 ELSE fBHXH_NSD END), 0) fNSD, 
			null sLoai
	from TBL_DTT where sXauNoiMa like '9020001%'
	union all
	select 3 rowNum, 
		   0 bHangCha, 
		   null stt, 
		   N'- Đơn vị hạch toán' sMoTa, 
		   ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHXH_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHXH_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHXH_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHXH_NSD, 0) END)),0) fTongSo, 
		   ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHXH_NLD/1000000, 0)* 1000000 ELSE fBHXH_NLD END), 0) fNLD, 
		   ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHXH_NSD/1000000, 0)* 1000000 ELSE fBHXH_NSD END),0) fNSD,
		   null sLoai
	from TBL_DTT where sXauNoiMa like '9020002%') thubhxh

	update TBL_THU_BHXH set fTongSo = (select sum(fTongSo) from TBL_THU_BHXH where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHXH where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHXH where bHangCha = 0)
						where bHangCha = 1

	--Thu BHTN
	select * into TBL_THU_BHTN from
	(select 4 rowNum, 1 bHangCha, '2' stt, N'Thu BHTN' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 5 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHTN_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHTN_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHTN_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHTN_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHTN_NLD/1000000, 0)* 1000000 ELSE fBHTN_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHTN_NSD/1000000, 0)* 1000000 ELSE fBHTN_NSD END),0) fNSD, 
			null sLoai
	from TBL_DTT where sXauNoiMa like '9020001%'
	union all
	select 6 rowNum, 
			0 bHangCha,
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHTN_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHTN_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHTN_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHTN_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHTN_NLD/1000000, 0)* 1000000 ELSE fBHTN_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHTN_NSD/1000000, 0)* 1000000 ELSE fBHTN_NSD END),0) fNSD,  
			null sLoai
	from TBL_DTT where sXauNoiMa like '9020002%') thubhtn

	update TBL_THU_BHTN set fTongSo = (select sum(fTongSo) from TBL_THU_BHTN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHTN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHTN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT quân nhân
	select * into TBL_THU_BHYT_QN from
	(select 7 rowNum, 1 bHangCha, '3' stt, N'Thu BHYT quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 8 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NLD/1000000, 0)* 1000000 ELSE fBHYT_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NSD/1000000, 0)* 1000000 ELSE fBHYT_NSD END),0) fNSD, 
			null sLoai
	from TBL_DTT where sXauNoiMa like '9020001-010-011-0001%'
	union all
	select 9 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NLD/1000000, 0)* 1000000 ELSE fBHYT_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NSD/1000000, 0)* 1000000 ELSE fBHYT_NSD END),0) fNSD, 
			null sLoai
	from TBL_DTT where sXauNoiMa like '9020002-010-011-0001%') thubhytquannhan

	update TBL_THU_BHYT_QN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_QN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_NLD from
	(select 10 rowNum, 1 bHangCha, '4' stt, N'Thu BHYT người lao động' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 11 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NLD/1000000, 0)* 1000000 ELSE fBHYT_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NSD/1000000, 0)* 1000000 ELSE fBHYT_NSD END),0) fNSD,
			null sLoai
	from TBL_DTT where sXauNoiMa like '9020001-010-011-0002%'
	union all
	select 12 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NLD/1000000, 0)* 1000000 ELSE fBHYT_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NSD/1000000, 0)* 1000000 ELSE fBHYT_NSD END),0) fNSD,
			null sLoai
	from TBL_DTT where sXauNoiMa like '9020002-010-011-0002%') thubhytnld

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
	select 15 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fTongSo, 
			null fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fNSD, 
			N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where sXauNoiMa like '9030001-010-011-0001%'
	union all
	select 16 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fTongSo, 
			null fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fNSD, 
			N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where sXauNoiMa like '9030001-010-011-0002%'
	union all
	select 17 rowNum,
			1 bHangCha, 
			'b' stt, 
			N'Công nhân, VCQP' sMoTa, 
			null fTongSo, 
			null fNLD, 
			null fNSD, 
			N'BHYT_THANNHAN_VCQP' sLoai
	union all
	select 18 rowNum,
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fTongSo, 
			null fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fNSD, 
			N'BHYT_THANNHAN_VCQP' sLoai
	from TBL_DTTM where sXauNoiMa like '9030002-010-011-0000%'
	union all
	select 19 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa,
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fTongSo,
			null fNLD,
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fNSD,
			N'BHYT_THANNHAN_VCQP' sLoai
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
	--if (@IsMillionRound = 1)
	--select
	--rowNum, 
	--bHangCha, 
	--stt, 
	--sMoTa, 
	--round(fTongSo / 1000000, 0) * 1000000 /@DVT fTongSo, 
	--round(fNLD / 1000000, 0) * 1000000 /@DVT fNLD, 
	--round(fNSD / 1000000, 0) * 1000000 /@DVT fNSD
	--from TBL_THU
	--else
	select
	rowNum, 
	bHangCha, 
	stt, 
	sMoTa, 
	fTongSo/@DVT fTongSo, 
	fNLD/@DVT fNLD, 
	fNSD/@DVT fNSD
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
;
;
;
;
;
;
GO
