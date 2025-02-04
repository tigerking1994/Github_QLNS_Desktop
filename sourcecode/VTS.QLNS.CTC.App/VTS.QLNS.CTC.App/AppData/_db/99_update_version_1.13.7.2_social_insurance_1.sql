/****** Object:  StoredProcedure [dbo].[sp_rpt_tong_hop_du_toan_thu_chi]    Script Date: 12/26/2023 1:30:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_tong_hop_du_toan_thu_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_tong_hop_du_toan_thu_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 12/26/2023 1:30:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_cap_phat_tam_ung_tao_ct_tonghop]    Script Date: 12/26/2023 1:30:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_cap_phat_tam_ung_tao_ct_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_cap_phat_tam_ung_tao_ct_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_cap_phat_bo_sung_tao_ct_tonghop]    Script Date: 12/26/2023 1:30:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_cap_phat_bo_sung_tao_ct_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_cap_phat_bo_sung_tao_ct_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_cap_phat_bo_sung_tao_ct_tonghop]    Script Date: 12/26/2023 1:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_cap_phat_bo_sung_tao_ct_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@NamLamViec int
AS
BEGIN
	INSERT INTO BH_CP_CapBoSung_KCB_BHYT_ChiTiet (
		iID_CTCapPhatBS,
		iNamLamViec,
		fDaQuyetToan,
		fDaCapUng,
		fThuaThieu,
		fSoCapBoSung,
		sXauNoiMa,
		sLNS,
		iID_CoSoYTe,
		iID_MaCoSoYTe,
		iID_MLNS
		)
		SELECT @IdChungTu,
			@NamLamViec,
			sum(fDaQuyetToan),
			sum(fDaCapUng),
			sum(fThuaThieu),
			sum(fSoCapBoSung),
			sXauNoiMa,
			sLNS,
			iID_CoSoYTe,
			iID_MaCoSoYTe,
			iID_MLNS
		FROM BH_CP_CapBoSung_KCB_BHYT_ChiTiet
		WHERE iID_CTCapPhatBS in
			(SELECT *
			 FROM f_split(@ListIdChungTuTongHop))
			 GROUP BY 
				sXauNoiMa,
				sLNS,
				iID_CoSoYTe,
				iID_MaCoSoYTe,
				iID_MLNS

	--danh dau chung tu da tong hop
	update BH_CP_CapBoSung_KCB_BHYT set bDaTongHop = 1 
	where iID_CTCapPhatBS in
		(SELECT *
		 FROM f_split(@ListIdChungTuTongHop));
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_cap_phat_tam_ung_tao_ct_tonghop]    Script Date: 12/26/2023 1:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_cap_phat_tam_ung_tao_ct_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@NamLamViec int
AS
BEGIN
	INSERT INTO BH_CP_CapTamUng_KCB_BHYT_ChiTiet (
		iID_BH_CP_CapTamUng_KCB_BHYT,
		iNamLamViec,
		fQTQuyTruoc,
		fTamUngQuyNay,
		fLuyKeCapDenCuoiQuy,
		sXauNoiMa,
		sLNS,
		iID_CoSoYTe,
		iID_MaCoSoYTe,
		iID_MLNS
		)
		SELECT @IdChungTu,
			@NamLamViec,
			sum(fQTQuyTruoc),
			sum(fTamUngQuyNay),
			sum(fLuyKeCapDenCuoiQuy),
			sXauNoiMa,
			sLNS,
			iID_CoSoYTe,
			iID_MaCoSoYTe,
			iID_MLNS
		FROM BH_CP_CapTamUng_KCB_BHYT_ChiTiet
		WHERE iID_BH_CP_CapTamUng_KCB_BHYT in
			(SELECT *
			 FROM f_split(@ListIdChungTuTongHop))
			 GROUP BY 
				sXauNoiMa,
				sLNS,
				iID_CoSoYTe,
				iID_MaCoSoYTe,
				iID_MLNS

	--danh dau chung tu da tong hop
	update BH_CP_CapTamUng_KCB_BHYT set bIsTongHop = 1 
	where iID_BH_CP_CapTamUng_KCB_BHYT in
		(SELECT *
		 FROM f_split(@ListIdChungTuTongHop));
END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 12/26/2023 1:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@SoQuyetDinh nvarchar(200),
	@DVT int,
	@NgayQuyetDinh nvarchar(200)
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
	select 2 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHXH_NLD, 0)) + sum(isnull(fBHXH_NSD, 0))) fTongSo, sum(fBHXH_NLD) fNLD, sum(fBHXH_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001%'
	union all
	select 3 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHXH_NLD, 0)) + sum(isnull(fBHXH_NSD, 0))) fTongSo, sum(fBHXH_NLD) fNLD, sum(fBHXH_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020002%') thubhxh

	update TBL_THU_BHXH set fTongSo = (select sum(fTongSo) from TBL_THU_BHXH where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHXH where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHXH where bHangCha = 0)
						where bHangCha = 1

	--Thu BHTN
	select * into TBL_THU_BHTN from
	(select 4 rowNum, 1 bHangCha, '2' stt, N'Thu BHTN' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 5 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHTN_NLD, 0)) + sum(isnull(fBHTN_NSD, 0))) fTongSo, sum(fBHTN_NLD) fNLD, sum(fBHTN_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001%'
	union all
	select 6 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHTN_NLD, 0)) + sum(isnull(fBHTN_NSD, 0))) fTongSo, sum(fBHTN_NLD) fNLD, sum(fBHTN_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020002%') thubhtn

	update TBL_THU_BHTN set fTongSo = (select sum(fTongSo) from TBL_THU_BHTN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHTN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHTN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT quân nhân
	select * into TBL_THU_BHYT_QN from
	(select 7 rowNum, 1 bHangCha, '3' stt, N'Thu BHYT quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 8 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001-010-011-0001%'
	union all
	select 9 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020002-010-011-0001%') thubhytquannhan

	update TBL_THU_BHYT_QN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_QN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_NLD from
	(select 10 rowNum, 1 bHangCha, '4' stt, N'Thu BHYT người lao động' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 11 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001-010-011-0002%'
	union all
	select 12 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001-010-011-0002%') thubhytnld

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
	select 15 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, sum(fDuToan) fNSD, N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where sXauNoiMa like '9030001-010-011-0000%'
	union all
	select 16 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, sum(fDuToan) fNSD, N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where sXauNoiMa like '9030001-010-011-0001%'
	union all
	select 17 rowNum, 1 bHangCha, 'b' stt, N'Công nhân, VCQP' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	union all
	select 18 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, sum(fDuToan) fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	from TBL_DTTM where sXauNoiMa like '9030002-010-011-0000%'
	union all
	select 19 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, sum(fDuToan) fNSD, N'BHYT_THANNHAN_VCQP' sLoai
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tong_hop_du_toan_thu_chi]    Script Date: 12/26/2023 1:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_tong_hop_du_toan_thu_chi]
	@namLamViec int,
	@lstSelectedUnit ntext,
	@dvt int
AS
BEGIN
	
SELECT
	--ml.sLNS,
	--ml.sM,
	--ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002')
			THEN 
				SUM(ISNULL(ctct.fThu_BHXH_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHXH_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHXH,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002')
			THEN 
				SUM(ISNULL(ctct.fThu_BHTN_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHTN_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHTN,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002') AND ml.sM = '1'
			THEN 
				SUM(ISNULL(ctct.fThu_BHYT_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHYT_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_QN,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002') AND ml.sM = '2'
			THEN 
				SUM(ISNULL(ctct.fThu_BHYT_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHYT_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_NLD
	INTO #temp1
FROM BH_KHT_BHXH_ChiTiet ctct
LEFT JOIN BH_KHT_BHXH ct 
ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = @namLamViec
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = @namLamViec
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS = '9030001'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_TNQN,
	CASE 
		WHEN ml.sLNS = '9030002'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_CNVQP,
	CASE 
		WHEN ml.sLNS = '9030003'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_HVQS,
	CASE 
		WHEN ml.sLNS = '9030004'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_HSSV,
	CASE 
		WHEN ml.sLNS = '9030005'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_SQDB,
	CASE 
		WHEN ml.sLNS = '9030006'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_LUU_HS
	INTO #temp2
FROM BH_KHTM_BHYT_ChiTiet ctct
LEFT JOIN BH_KHTM_BHYT ct 
ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_NoiDung = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = @namLamViec
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = @namLamViec
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010001', '9010002')
		THEN 
			SUM(ISNULL(ctct.fTienCNVQP, 0)) +
			SUM(ISNULL(ctct.fTienHSQBS, 0)) +
			SUM(ISNULL(ctct.fTienLDHD, 0)) +
			SUM(ISNULL(ctct.fTienQNCN, 0)) +
			SUM(ISNULL(ctct.fTienSQ, 0))
		ELSE 0 
	END AS fSoTienChiCheDo
	INTO #temp3
FROM BH_KHC_CheDoBHXH_ChiTiet ctct
LEFT JOIN BH_KHC_CheDoBHXH ct 
ON ct.ID = ctct.iID_KHC_CheDoBHXH
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = @namLamViec
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = @namLamViec
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010003')
		THEN 
			SUM(ISNULL(ctct.fTienCanBo, 0)) +
			SUM(ISNULL(ctct.fTienQuanLuc, 0)) +
			SUM(ISNULL(ctct.fTienTaiChinh, 0)) +
			SUM(ISNULL(ctct.fTienQuanY, 0))
		ELSE 0 
	END AS fSoTienChiKinhPhiQuanLy
	INTO #temp4
FROM BH_KHC_KinhPhiQuanLy_ChiTiet ctct
LEFT JOIN BH_KHC_KinhPhiQuanLy ct 
ON ct.iID_BH_KHC_KinhPhiQuanLy = ctct.iID_KHC_KinhPhiQuanLy
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = @namLamViec
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = @namLamViec
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010006', '9010007')
		THEN 
			SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiQuanYDonVi
	INTO #temp5
FROM BH_KHC_KCB_ChiTiet ctct
LEFT JOIN BH_KHC_KCB ct 
ON ct.iID_BH_KHC_KCB = ctct.iID_KHC_KCB
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = @namLamViec
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = @namLamViec
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010009')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiTTB,
	CASE 
		WHEN ml.sLNS IN ('9010004', '9010005')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiTSDK,
	CASE 
		WHEN ml.sLNS IN ('9050001')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiNLD,
	CASE 
		WHEN ml.sLNS IN ('9050002')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiHSSV,
	CASE 
		WHEN ml.sLNS IN ('9040001', '9040002')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiCSYT
	INTO #temp6
FROM BH_KHC_K_ChiTiet ctct
LEFT JOIN BH_KHC_K ct 
ON ct.iID_BH_KHC_K = ctct.iID_KHC_K
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = @namLamViec
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = @namLamViec
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

IF NOT EXISTS(SELECT 1 FROM #temp1) INSERT INTO #temp1 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp2) INSERT INTO #temp2 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp3) INSERT INTO #temp3 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp4) INSERT INTO #temp4 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp5) INSERT INTO #temp5 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp6) INSERT INTO #temp6 DEFAULT VALUES 


SELECT NoiDung, SoTien  
INTO #temp7
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienThuBHXH, 0)) / @dvt AS N'1.1.0,1,Dự toán thu BHXH',
		SUM(ISNULL(fSoTienThuBHTN, 0))/ @dvt AS N'1.2.0,2,Dự toán thu BHTN',
		SUM(ISNULL(fSoTienThuBHYT_QN, 0)) / @dvt AS N'1.3.0,3,Dự toán thu BHYT quân nhân',
		SUM(ISNULL(fSoTienThuBHYT_NLD, 0)) / @dvt AS N'1.4.0,4,Dự toán thu BHYT người lao động'
   FROM #temp1) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([1.1.0,1,Dự toán thu BHXH], 
	  [1.2.0,2,Dự toán thu BHTN], 
	  [1.3.0,3,Dự toán thu BHYT quân nhân], 
	  [1.4.0,4,Dự toán thu BHYT người lao động])  
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienThuBHYT_TNQN, 0)) / @dvt AS N'1.5.1,-,Thân nhân quân nhân',
		SUM(ISNULL(fSoTienThuBHYT_CNVQP, 0)) / @dvt AS N'1.5.2,-,Thân nhân người lao động'
		--SUM(ISNULL(fSoTienThuBHYT_HVQS, 0)) AS N'1.3.7,-,BHYT HV QS xã phường (Phụ lục VII)',
		--SUM(ISNULL(fSoTienThuBHYT_HSSV, 0)) AS N'1.3.5,-,BHYT học sinh, sinh viên (Phụ lục VII)',
		--SUM(ISNULL(fSoTienThuBHYT_SQDB, 0)) AS N'1.3.8,-,BHYT SQ dự bị (Phụ lục VII)',
		--SUM(ISNULL(fSoTienThuBHYT_LUU_HS, 0)) AS N'1.3.6,-,BHYT lưu học sinh (Phụ lục VII)'
   FROM #temp2) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([1.5.1,-,Thân nhân quân nhân], 
	  [1.5.2,-,Thân nhân người lao động])
	  --[1.3.7,-,BHYT HV QS xã phường (Phụ lục VII)], 
	  --[1.3.5,-,BHYT học sinh, sinh viên (Phụ lục VII)], 
	  --[1.3.8,-,BHYT SQ dự bị (Phụ lục VII)], 
	  --[1.3.6,-,BHYT lưu học sinh (Phụ lục VII)])  
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiCheDo, 0)) / @dvt AS N'2.1.0,1,Dự toán chi các chế độ BHXH'
   FROM #temp3) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.1.0,1,Dự toán chi các chế độ BHXH])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiKinhPhiQuanLy, 0)) / @dvt AS N'2.2.0,2,Dự toán chi kinh phí quản lý BHXH, BHYT'
   FROM #temp4) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.2.0,2,Dự toán chi kinh phí quản lý BHXH, BHYT])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiQuanYDonVi, 0)) / @dvt AS N'2.3.0,3,Dự toán chi kinh phí KCB tại quân y đơn vị'
   FROM #temp5) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.3.0,3,Dự toán chi kinh phí KCB tại quân y đơn vị])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		--SUM(ISNULL(fSoTienChiTTB, 0)) AS N'2.3.0,3,Chi mua sắm TTB y tế (Phụ lục X)',
		SUM(ISNULL(fSoTienChiTSDK, 0)) AS N'2.4.0,4,Dự toán chi kinh phí KCB Trường Sa - DK'
		--SUM(ISNULL(fSoTienChiNLD, 0)) AS N'2.4.3,-,Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)',
		--SUM(ISNULL(fSoTienChiHSSV, 0)) AS N'2.4.4,-,Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)',
		--SUM(ISNULL(fSoTienChiCSYT, 0)) AS N'2.4.5,-,Chi KCB tại các cơ sở y tế (Phụ lục XV)'
   FROM #temp6) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      --([2.3.0,3,Chi mua sắm TTB y tế (Phụ lục X)], 
	  ([2.4.0,4,Dự toán chi kinh phí KCB Trường Sa - DK]) 
	  --[2.4.3,-,Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)], 
	  --[2.4.4,-,Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)], 
	  --[2.4.5,-,Chi KCB tại các cơ sở y tế (Phụ lục XV)])
) AS unpvt



SELECT * FROM #temp7
UNION SELECT N'1.0.0,A,Dự toán thu' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '1.%')
UNION SELECT N'2.0.0,B,Dự toán chi' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '2.%')
UNION SELECT N'1.5.0,5,Dự toán thu BHYT TN' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '1.5%')
--UNION SELECT N'2.4.0,4,Chi KCB BHYT' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '2.4%')


DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #temp4
DROP TABLE #temp5
DROP TABLE #temp6
DROP TABLE #temp7

END
GO
