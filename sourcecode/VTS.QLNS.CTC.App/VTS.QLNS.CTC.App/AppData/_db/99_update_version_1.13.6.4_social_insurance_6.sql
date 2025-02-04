/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]    Script Date: 07/12/2023 4:26:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]    Script Date: 07/12/2023 4:26:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_getalldotdutoanbhxh]    Script Date: 07/12/2023 4:26:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_getalldotdutoanbhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_getalldotdutoanbhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dutoan_rpt_get_target_donvi]    Script Date: 07/12/2023 4:26:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dutoan_rpt_get_target_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dutoan_rpt_get_target_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dutoan_rpt_get_target_donvi]    Script Date: 07/12/2023 4:26:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_dutoan_rpt_get_target_donvi]
	 @NamLamViec int,	 
	 @IdChungTu nvarchar(4000)	 
AS
BEGIN 
	DECLARE @dtt INT;
	DECLARE @dttm INT;
	DECLARE @dtc INT;
	SELECT @dtt = COUNT(*) from BH_DTT_BHXH_PhanBo_ChungTu where iID_DTT_BHXH_PhanBo_ChungTu in (select * from f_split(@IdChungTu));
	SELECT @dttm = COUNT(*) from BH_DTTM_BHYT_ThanNhan_PhanBo where iID_DTTM_BHYT_ThanNhan_PhanBo in (select * from f_split(@IdChungTu));
	SELECT @dtc = COUNT(*) from BH_DTC_PhanBoDuToanChi where ID in (select * from f_split(@IdChungTu));

	IF @dtt > 0
	BEGIN
		select DISTINCT 
			DonVi.* 
		from BH_DTT_BHXH_PhanBo_ChungTu bhdtt
		INNER JOIN 
			BH_DTT_BHXH_PhanBo_ChungTuChiTiet bhdttct
		ON
			bhdtt.iID_DTT_BHXH_PhanBo_ChungTu = bhdttct.iID_DTT_BHXH_ChungTu
		INNER JOIN
			  (
			   SELECT *
			   FROM DonVi
			   WHERE iNamLamViec = @NamLamViec
			  ) donvi 
			  ON donvi.iID_MaDonVi = bhdttct.iID_MaDonVi
		WHERE bhdtt.iNamLamViec = @NamLamViec
				AND bhdttct.iNamLamViec = @NamLamViec		
				AND bhdtt.iID_DTT_BHXH_PhanBo_ChungTu in (select * from f_split(@IdChungTu));
	END
	ELSE IF @dttm > 0
	BEGIN
		select DISTINCT 
				DonVi.* 
		from BH_DTTM_BHYT_ThanNhan_PhanBo bhdttm
		INNER JOIN 
			BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet bhdttmct
		ON
			bhdttm.iID_DTTM_BHYT_ThanNhan_PhanBo = bhdttmct.iID_DTTM_BHYT_ThanNhan_PhanBo
		INNER JOIN
			  (
			   SELECT *
			   FROM DonVi
			   WHERE iNamLamViec = @NamLamViec
			  ) donvi 
			  ON donvi.iID_MaDonVi = bhdttmct.iID_MaDonVi
		WHERE bhdttm.iNamLamViec = @NamLamViec
				AND bhdttmct.iNamLamViec = @NamLamViec		
				AND bhdttm.iID_DTTM_BHYT_ThanNhan_PhanBo in (select * from f_split(@IdChungTu));
	END
	ELSE IF @dtc > 0
	BEGIN
		select DISTINCT 
			DonVi.* 
	from BH_DTC_PhanBoDuToanChi bhdtc
	INNER JOIN 
		BH_DTC_PhanBoDuToanChi_ChiTiet bhdtcct
	ON
		bhdtc.ID = bhdtcct.iID_DTC_PhanBoDuToanChi
	INNER JOIN
		  (
		   SELECT *
		   FROM DonVi
		   WHERE iNamLamViec = @NamLamViec
		  ) donvi 
		  ON donvi.iID_MaDonVi = bhdtcct.iID_MaDonVi
	WHERE bhdtc.iNamChungTu = @NamLamViec
			AND bhdtc.ID in (select * from f_split(@IdChungTu));
	END
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_getalldotdutoanbhxh]    Script Date: 07/12/2023 4:26:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_getalldotdutoanbhxh]
As
	SELECT 
		sSoQuyetDinh,
		dNgayQuyetDinh,
		iID_DTChungTu,
		sNgayQuyetDinh,
		ROW_NUMBER() OVER (PARTITION BY sSoQuyetDinh, sNgayQuyetDinh ORDER BY iID_DTChungTu) as RowNum
	INTO TBL_DT
	FROM
	(
		SELECT iID_DTT_BHXH_PhanBo_ChungTu as iID_DTChungTu
			 , sSoQuyetDinh
			 , dNgayQuyetDinh
			 , CONVERT(varchar, dNgayQuyetDinh, 101) sNgayQuyetDinh
		FROM BH_DTT_BHXH_PhanBo_ChungTu

		UNION

		SELECT iID_DTTM_BHYT_ThanNhan_PhanBo as iID_DTChungTu
			 , sSoQuyetDinh
			 , dNgayQuyetDinh
			 , CONVERT(varchar, dNgayQuyetDinh, 101) sNgayQuyetDinh
		FROM BH_DTTM_BHYT_ThanNhan_PhanBo

		UNION 

		SELECT ID as iID_DTChungTu
			 , sSoQuyetDinh
			 , dNgayQuyetDinh
			 , CONVERT(varchar, dNgayQuyetDinh, 101) sNgayQuyetDinh
		FROM BH_DTC_PhanBoDuToanChi
	) tbldt

	SELECT 
		sSoQuyetDinh,
		dNgayQuyetDinh,
		iID_DTChungTu,
		sNgayQuyetDinh
	FROM TBL_DT
	WHERE RowNum = 1;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DT]') AND type in (N'U')) drop table TBL_DT;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]    Script Date: 07/12/2023 4:26:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@Dvt int,
	@SoQuyetDinh nvarchar(200)
AS
BEGIN
	---CHI---
	select ctct.* into TBL_DTC from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
	--Chi các chế độ BHXH DUTOAN
	select * into TBL_CHI_CHEDO_DUTOAN from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Chi các chế độ BHXH' sMoTa, null fTongSoChi, null fDuToan, null fHachToan
	union all
	select 2 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Ốm đau' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0001%'
	union all 
	select 3 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Thai sản' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0002%'
	union all 
	select 4 rowNum, 0 bHangCha, null stt, N'- Trợ cấp tai nạn lao động, BNN' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0003%'
	union all 
	select 5 rowNum, 0 bHangCha, null stt, N'- Trợ cấp hưu trí' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0004%'
	union all 
	select 6 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Phục viên' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0005%'
	union all 
	select 7 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Xuất ngũ' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0006%'
	union all 
	select 8 rowNum, 0 bHangCha, null stt, N'- Trợ cấp thôi việc' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0007%'
	union all 
	select 9 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Tử tuất' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0008%'
	union all 
	select 10 rowNum, 1 bHangCha, 2 stt, N'Kinh phí quản lý BHXH, BHYT' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010003%'
	union all 
	select 11 rowNum, 1 bHangCha, 3 stt, N'Kinh phí KCB tại quân y đơn vị' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010004%' or sXauNoiMa like '9010005%'
	union all 
	select 12 rowNum, 1 bHangCha, 4 stt, N'Kinh phí KCB tại Trường sa - DK' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010006%' or sXauNoiMa like '9010007%'
	) chidutoan



	--Chi các chế độ BHXH hạch toán
	select * into TBL_CHI_CHEDO_HACHTOAN from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Chi các chế độ BHXH' sMoTa, null fTongSoChi, null fDuToan, null fHachToan
	union all
	select 2 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Ốm đau' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0001%'
	union all 
	select 3 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Thai sản' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0002%'
	union all 
	select 4 rowNum, 0 bHangCha, null stt, N'- Trợ cấp tai nạn lao động, BNN' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0003%'
	union all 
	select 5 rowNum, 0 bHangCha, null stt, N'- Trợ cấp hưu trí' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0004%'
	union all 
	select 6 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Phục viên' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0005%'
	union all 
	select 7 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Xuất ngũ' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0006%'
	union all 
	select 8 rowNum, 0 bHangCha, null stt, N'- Trợ cấp thôi việc' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0007%'
	union all 
	select 9 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Tử tuất' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0008%') chihachtoan

	select dt.rowNum, 
			dt.bHangCha, 
			dt.stt, 
			dt.sMoTa, 
			(isnull(dt.fDuToan, 0) + isnull(ht.fHachToan, 0))/@Dvt fTongSoChi, 
			dt.fDuToan/@Dvt fDuToan, 
			ht.fHachToan/@Dvt fHachToan
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]    Script Date: 07/12/2023 4:26:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@Dvt int,
	@SoQuyetDinh nvarchar(200)
AS
BEGIN
	---THU---
	--Dữ liệu phân bổ dự toán thu BHXH
	select ctct.* into TBL_DTT from BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
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
	select 15 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where sXauNoiMa like '9030001-010-011-0000%'
	union all
	select 16 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where sXauNoiMa like '9030001-010-011-0001%'
	union all
	select 17 rowNum, 1 bHangCha, 'b' stt, N'Công nhân, VCQP' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	union all
	select 18 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	from TBL_DTTM where sXauNoiMa like '9030002-010-011-0000%'
	union all
	select 19 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_VCQP' sLoai
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
	fTongSo/@Dvt fTongSo, 
	fNLD/@Dvt fNLD, 
	fNSD/@Dvt fNSD
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
GO
