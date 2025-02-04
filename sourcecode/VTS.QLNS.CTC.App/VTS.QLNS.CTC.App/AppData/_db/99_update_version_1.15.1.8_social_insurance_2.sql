/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]    Script Date: 11/27/2024 3:32:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]    Script Date: 11/27/2024 3:32:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 11/27/2024 3:32:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 11/27/2024 3:32:14 PM ******/
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

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTT]') AND type in (N'U')) drop table TBL_DTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHXH]') AND type in (N'U')) drop table TBL_THU_BHXH;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHTN]') AND type in (N'U')) drop table TBL_THU_BHTN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_QN]') AND type in (N'U')) drop table TBL_THU_BHYT_QN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_NLD]') AND type in (N'U')) drop table TBL_THU_BHYT_NLD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTTM]') AND type in (N'U')) drop table TBL_DTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_TN]') AND type in (N'U')) drop table TBL_THU_BHYT_TN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU]') AND type in (N'U')) drop table TBL_THU;
	---THU---
	--Dữ liệu phân bổ dự toán thu BHXH
	select ctct.*, dv.iKhoi into TBL_DTT from BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
	left join DonVi dv on ctct.iid_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec and dv.iTrangThai = 1
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
			ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHXH_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHXH_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHXH_NSD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHXH_NSD, 0)) END)),0) fTongSo,
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHXH_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHXH_NLD,0)) END),0) fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHXH_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHXH_NSD,0)) END), 0) fNSD, 
			null sLoai
	--from TBL_DTT where iKhoi = 2 --sXauNoiMa like '9020001%'
	from TBL_DTT where sXauNoiMa like '9020001%'

	union all

	select 3 rowNum, 
		   0 bHangCha, 
		   null stt, 
		   N'- Đơn vị hạch toán' sMoTa, 
		   ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHXH_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHXH_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(Sum(isnull(fBHXH_NSD, 0))/1000000, 0)* 1000000 ELSE Sum(isnull(fBHXH_NSD, 0)) END)),0) fTongSo, 
		   ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHXH_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHXH_NLD,0)) END), 0) fNLD, 
		   ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHXH_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHXH_NSD,0)) END),0) fNSD,
		   null sLoai
	--from TBL_DTT where iKhoi <> 2 --sXauNoiMa like '9020002%'
	from TBL_DTT where sXauNoiMa like '9020002%'
	) thubhxh

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
			ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NSD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NSD, 0)) END)),0) fTongSo, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NLD,0)) END),0) fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NSD,0)) END),0) fNSD, 
			null sLoai
	--from TBL_DTT where iKhoi = 2 --sXauNoiMa like '9020001%'
	from TBL_DTT where sXauNoiMa like '9020001%'
	union all

	select 6 rowNum, 
			0 bHangCha,
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NSD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NSD, 0)) END)),0) fTongSo, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NLD,0)) END),0) fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NSD,0)) END),0) fNSD,  
			null sLoai
	--from TBL_DTT where iKhoi <> 2 --sXauNoiMa like '9020002%'
	from TBL_DTT where sXauNoiMa like '9020002%'
	) thubhtn

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
			ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NSD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NSD, 0)) END)),0) fTongSo, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD,0)) END),0) fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NSD,0)) END),0) fNSD, 
			null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' or sXauNoiMa like '9020002-010-011-0001%')
	--					and iKhoi = 2
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' )
					
	union all
	
	select 9 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(Sum(isnull(fBHYT_NSD, 0))/1000000, 0)* 1000000 ELSE Sum(isnull(fBHYT_NSD, 0)) END)),0) fTongSo, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD,0)) END),0) fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NSD,0)) END),0) fNSD, 
			null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' or sXauNoiMa like '9020002-010-011-0001%')
	--					and iKhoi <> 2
	from TBL_DTT where (sXauNoiMa like '9020002-010-011-0001%' )
						) thubhytquannhan

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
			ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NSD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NSD, 0)) END)),0) fTongSo, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD,0)) END),0) fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NSD,0)) END),0) fNSD,
			null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%' or sXauNoiMa like '9020002-010-011-0002%')
	--					and iKhoi = 2
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%')
						
	union all
	
	select 12 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NSD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NSD, 0)) END)),0) fTongSo, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD,0)) END),0) fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NSD,0)) END),0) fNSD,
			null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%' or sXauNoiMa like '9020002-010-011-0002%')
	--					and iKhoi <> 2
	from TBL_DTT where ( sXauNoiMa like '9020002-010-011-0002%')
						) thubhytnld

	update TBL_THU_BHYT_NLD set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_NLD where bHangCha = 0)
						where bHangCha = 1
	
	--Dữ liệu phân bổ dự toán thu mua BHYT
	select ctct.*, dv.iKhoi into TBL_DTTM from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	join  BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
	left join DonVi dv on ctct.iid_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec and dv.iTrangThai = 1
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
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fTongSo, 
			null fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fNSD, 
			N'BHYT_THANNHAN_QN' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0001%' or sXauNoiMa like '9030001-010-011-0002%')
	--					and iKhoi = 2
	from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0001%' )
				
	union all
	select 16 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fTongSo, 
			null fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fNSD, 
			N'BHYT_THANNHAN_QN' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0001%' or sXauNoiMa like '9030001-010-011-0002%')
	--					and iKhoi <> 2
	from TBL_DTTM where ( sXauNoiMa like '9030001-010-011-0002%')
						
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
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fTongSo, 
			null fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fNSD, 
			N'BHYT_THANNHAN_VCQP' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030002-010-011-0000%' or sXauNoiMa like '9030002-010-011-0001%')
	--					and iKhoi = 2
	from TBL_DTTM where (sXauNoiMa like '9030002-010-011-0000%' )

	union all

	select 19 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa,
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fTongSo,
			null fNLD,
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fNSD,
			N'BHYT_THANNHAN_VCQP' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030002-010-011-0000%' or sXauNoiMa like '9030002-010-011-0001%')
	--					and iKhoi <> 2
	from TBL_DTTM where ( sXauNoiMa like '9030002-010-011-0001%')
						
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
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]    Script Date: 11/27/2024 3:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]
	@namLamViec int,
	@lstSelectedUnit ntext,
	@hSSV nvarchar(50),
	@luuHS nvarchar(50),
	@hVSQ nvarchar(50),
	@sQDuBi nvarchar(50),
	@soQuyetDinh nvarchar(500),
	@ngayQuyetDinh nvarchar(500),
	@dvt int,
	@IsMillionRound bit
AS
BEGIN
	declare @tbl_HSSV table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_HSSV float);
	declare @tbl_LuuHS table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_LuuHS float);
	declare @tbl_HVQS table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_HVQS float);
	declare @tbl_SQDuBi table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_SQDuBi float);

	INSERT INTO @tbl_HSSV (IdDonVi, TenDonVI, ThanhTien_HSSV)
	SELECT 
		dt_dv.id,
		dt_dv.sTenDonVi,
		CASE WHEN @IsMillionRound = 1 THEN SUM(ROUND(IsNull(A.ThanhTien, 0)/1000000, 0) * 1000000) ELSE SUM(IsNull(A.ThanhTien, 0)) END as ThanhTien
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
		   AND ml.sLNS = @hSSV
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

	INSERT INTO @tbl_LuuHS (IdDonVi, TenDonVI, ThanhTien_LuuHS)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   CASE WHEN @IsMillionRound = 1 THEN SUM(ROUND(IsNull(A.ThanhTien, 0)/1000000, 0) * 1000000) ELSE SUM(IsNull(A.ThanhTien, 0)) END as ThanhTien
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
		   AND ml.sLNS = @luuHS
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

	INSERT INTO @tbl_HVQS (IdDonVi, TenDonVI, ThanhTien_HVQS)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   CASE WHEN @IsMillionRound = 1 THEN SUM(ROUND(IsNull(A.ThanhTien, 0)/1000000, 0) * 1000000) ELSE SUM(IsNull(A.ThanhTien, 0)) END as ThanhTien
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
		   AND ml.sLNS = @hVSQ
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

	INSERT INTO @tbl_SQDuBi (IdDonVi, TenDonVI, ThanhTien_SQDuBi)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   CASE WHEN @IsMillionRound = 1 THEN SUM(ROUND(IsNull(A.ThanhTien, 0)/1000000, 0) * 1000000) ELSE SUM(IsNull(A.ThanhTien, 0)) END as ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   LEFT JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @sQDuBi
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
		result.STenDonVi, 
		result.HSSV/@dvt HSSV, 
		result.LuuHS/@dvt LuuHS,
		result.TongHSSV/@dvt TongHSSV,
		result.HVQS/@dvt HVQS,
		result.SQDuBi/@dvt SQDuBi,
		(IsNull(result.TongHSSV, 0) + IsNull(result.HVQS, 0) + IsNull(result.SQDuBi, 0))/@dvt TongCongHSSV
		FROM
		(SELECT donvi.iID_MaDonVi idDonVi, 
		donvi.STenDonVi,
		IsNull(hssv.ThanhTien_HSSV, 0) HSSV,
		IsNull(luuhs.ThanhTien_LuuHS, 0) LuuHS,
		IsNull(hssv.ThanhTien_HSSV, 0) + IsNull(luuhs.ThanhTien_LuuHS, 0) TongHSSV,
		IsNull(hvsq.ThanhTien_HVQS, 0) HVQS,
		IsNull(sqdb.ThanhTien_SQDuBi, 0) SQDuBi
		FROM DonVi donvi
		LEFT JOIN @tbl_HSSV hssv ON donvi.iID_MaDonVi = hssv.idDonVi
		LEFT JOIN @tbl_LuuHS luuhs ON donvi.iID_MaDonVi = luuhs.idDonVi
		LEFT JOIN @tbl_HVQS hvsq ON donvi.iID_MaDonVi = hvsq.idDonVi
		LEFT JOIN @tbl_SQDuBi sqdb ON donvi.iID_MaDonVi = sqdb.idDonVi
		WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) result
		order by result.idDonVi
		
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]    Script Date: 11/27/2024 3:32:14 PM ******/
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
	@dvt int,
	@IsMillionRound bit
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
	   CASE WHEN @IsMillionRound = 1 THEN SUM(ROUND(IsNull(A.ThanhTien, 0)/1000000, 0) * 1000000) ELSE SUM(IsNull(A.ThanhTien, 0)) END as ThanhTien
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
		CASE WHEN @IsMillionRound = 1 THEN SUM(ROUND(IsNull(A.ThanhTien, 0)/1000000, 0) * 1000000) ELSE SUM(IsNull(A.ThanhTien, 0)) END as ThanhTien
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
		   AND ml.sM = '0000'
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
			CASE WHEN @IsMillionRound = 1 THEN SUM(ROUND(IsNull(A.ThanhTien, 0)/1000000, 0) * 1000000) ELSE SUM(IsNull(A.ThanhTien, 0)) END as ThanhTien
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
		CASE WHEN @IsMillionRound = 1 THEN SUM(ROUND(IsNull(A.ThanhTien, 0)/1000000, 0) * 1000000) ELSE SUM(IsNull(A.ThanhTien, 0)) END as ThanhTien
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
		
		SELECT result.idDonVi, 
		result.STenDonVi, 
		result.TN_QN_DT/@dvt TNQNDuToan, 
		result.TN_CNVQP_DT/@dvt TNCNVQPDuToan,
		result.TongDuToan/@dvt TongDuToan,
		result.TN_QN_HT/@dvt TNQNHachToan,
		result.TN_CNVQP_HT/@dvt TNCNVQPHachToan,
		result.TongHachToan/@dvt TongHachToan,
		(result.TongDuToan + result.TongHachToan)/@dvt TongCongThanNhan
		FROM
		(SELECT donvi.iID_MaDonVi idDonVi, 
		donvi.sTenDonVi,
		IsNull(tnqn_dt.ThanhTien_TNQN_DuToan, 0) TN_QN_DT,
		IsNull(tncn_dt.ThanhTien_TN_CNVQP_DuToan, 0) TN_CNVQP_DT,
		(IsNull(tnqn_dt.ThanhTien_TNQN_DuToan, 0) + IsNull(tncn_dt.ThanhTien_TN_CNVQP_DuToan, 0)) TongDuToan,
		IsNull(tnqn_ht.ThanhTien_TNQN_HachToan, 0) TN_QN_HT,
		IsNull(tncn_ht.ThanhTien_TN_CNVQP_HachToan, 0) TN_CNVQP_HT,
		(IsNull(tnqn_ht.ThanhTien_TNQN_HachToan, 0) + IsNull(tncn_ht.ThanhTien_TN_CNVQP_HachToan, 0)) TongHachToan
		FROM
		DonVi donvi
		LEFT JOIN @TNQN_DuToan tnqn_dt ON donvi.iID_MaDonVi = tnqn_dt.idDonVi
		LEFT JOIN @TN_CNVQP_DuToan tncn_dt ON donvi.iID_MaDonVi = tncn_dt.idDonVi
		LEFT JOIN @TNQN_HachToan tnqn_ht ON donvi.iID_MaDonVi = tnqn_ht.idDonVi
		LEFT JOIN @TN_CNVQP_HachToan tncn_ht ON donvi.iID_MaDonVi = tncn_ht.idDonVi
		WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) result
		order by result.idDonVi
END
;
;
;
;
;
;
GO
