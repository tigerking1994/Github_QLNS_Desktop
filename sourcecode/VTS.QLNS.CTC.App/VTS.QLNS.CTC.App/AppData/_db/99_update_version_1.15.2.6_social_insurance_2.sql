/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkcb_tonghopchi]    Script Date: 12/12/2024 5:20:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_qkcb_tonghopchi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_qkcb_tonghopchi]
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_qkcb_tonghopchi]    Script Date: 12/12/2024 5:20:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_qtc_qkcb_tonghopchi]
@YearOfWork int,
@DonViTinh int,
@AgencyId nvarchar(max),
@Quy int

AS BEGIN
SET NOCOUNT ON;
		Select 
		 sum(case when ctct.sXauNoiMa like '9010004-010-011-0001-0000' or 
		 ctct.sXauNoiMa like '9010004-010-011-0002-0000'
		 then (ctct.FTienDeNghiQuyetToanQuyNay) else 0 end) thuoc
		 , sum(case when ctct.sXauNoiMa like '9010004-010-011-0001-0001' or 
		 ctct.sXauNoiMa like '9010004-010-011-0002-0001'
		 then (ctct.FTienDeNghiQuyetToanQuyNay) else 0 end)  vtyt
		 , sum(case when ctct.sXauNoiMa like '9010004-010-011-0001-0002' or 
		 ctct.sXauNoiMa like '9010004-010-011-0002-0002'
		 then (ctct.FTienDeNghiQuyetToanQuyNay) else 0 end)  DVKT
		 , sum(case when ctct.sXauNoiMa like '9010004-010-011-0001-0003' or 
		 ctct.sXauNoiMa like '9010004-010-011-0002-0003'
		 then (ctct.FTienDeNghiQuyetToanQuyNay) else 0 end)  dcyt
		 ,(ml.sLNS +'-' + ml.sL+'-'+ml.sK+'-'+ml.sM+'-'+ml.sTM) sXauNoiMa
		 , dv.sTenDonVi
		 , dv.iID_MaDonVi
		 into #tempData
		from BH_QTC_Quy_KCB_ChiTiet ctct 
		left join DonVi dv on dv.iID_MaDonVi=ctct.iID_MaDonVi
		left join BH_DM_MucLucNganSach ml on ctct.sXauNoiMa=ml.sXauNoiMa
		where dv.iNamLamViec=@YearOfWork
		and dv.iTrangThai=1

		and ctct.iID_QTC_Quy_KCB in (select ID_QTC_Quy_KCB from BH_QTC_Quy_KCB
									where iNamChungTu=@YearOfWork
										and iQuyChungTu=@Quy)
		and ctct.FTienDeNghiQuyetToanQuyNay >0

		and ml.iNamLamViec=@YearOfWork
		and ml.iTrangThai=1
		group by ctct.sXauNoiMa,ml.sLNS,ml.sL,ml.sK,ml.sM,ml.sTM,dv.iID_MaDonVi,dv.sTenDonVi

		-- Get ghi chu
		select 
		( (case when ctct.sXauNoiMa = '9010004-010-011-0001-0000' then ctct.sGhiChu 
		 else ' ' end ) + 
		(case when ctct.sXauNoiMa = '9010004-010-011-0001-0001' then ctct.sGhiChu 
		else ' ' end)  + 
		(case when ctct.sXauNoiMa = '9010004-010-011-0001-0002' then ctct.sGhiChu 
		else ' ' end) +
		(case when ctct.sXauNoiMa = '9010004-010-011-0001-0003' then ctct.sGhiChu 
		else ' ' end ) ) a
		,((case when ctct.sXauNoiMa = '9010004-010-011-0002-0000' then ctct.sGhiChu 
		else ' ' end) 
		+(case when ctct.sXauNoiMa = '9010004-010-011-0002-0001' then ctct.sGhiChu 
		else  ' ' end) 
		+(case when ctct.sXauNoiMa = '9010004-010-011-0002-0002' then ctct.sGhiChu 
		else ' ' end) 
		+(case when ctct.sXauNoiMa = '9010004-010-011-0002-0003' then ctct.sGhiChu 
		else ' ' end) )b
		 , dv.sTenDonVi
		 , dv.iID_MaDonVi
		 --, case when ctct.sXauNoiMa = '9010004-010-011-0001-0000' or  ctct.sXauNoiMa = '9010004-010-011-0002-0000' then 1 else 0 end isThuoc
		 --, case when ctct.sXauNoiMa = '9010004-010-011-0001-0001' or  ctct.sXauNoiMa = '9010004-010-011-0002-0001' then 2 else 0 end isVTYT
		 --, case when ctct.sXauNoiMa = '9010004-010-011-0001-0002' or  ctct.sXauNoiMa = '9010004-010-011-0002-0002' then 3 else 0 end isDVKT
		 --, case when ctct.sXauNoiMa = '9010004-010-011-0001-0003' or  ctct.sXauNoiMa = '9010004-010-011-0002-0003' then 4 else 0 end isDCYT
		 --, 0 isUpdate
		 into #tempGhiChu
		 from BH_QTC_Quy_KCB_ChiTiet ctct 
		left join DonVi dv on dv.iID_MaDonVi=ctct.iID_MaDonVi
		left join BH_DM_MucLucNganSach ml on ctct.sXauNoiMa=ml.sXauNoiMa
		where dv.iNamLamViec=@YearOfWork
		and dv.iTrangThai=1

		and ctct.iID_QTC_Quy_KCB in (select ID_QTC_Quy_KCB from BH_QTC_Quy_KCB
									where iNamChungTu=@YearOfWork
										and iQuyChungTu=@Quy)
		and ctct.FTienDeNghiQuyetToanQuyNay >0

		and ml.iNamLamViec=@YearOfWork
		and ml.iTrangThai=1

		group by ctct.sXauNoiMa,ctct.sGhiChu,dv.iID_MaDonVi,dv.sTenDonVi


		SELECT 
		sTenDonVi,
		iID_MaDonVi,
		Replace(STUFF((
			SELECT ', ' + a
			FROM #tempGhiChu T2
			WHERE T2.sTenDonVi = T1.sTenDonVi
			  AND T2.iID_MaDonVi = T1.iID_MaDonVi
			  AND (a IS NOT NULL or a <> ' ')
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ''),'    ,','') AS SGhiChu1,
		Replace(STUFF((
			SELECT ', ' + b
			FROM #tempGhiChu T2
			WHERE T2.sTenDonVi = T1.sTenDonVi
			  AND T2.iID_MaDonVi = T1.iID_MaDonVi
			  AND (b IS NOT NULL or b <> ' ')
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ''),'    ,','') AS SGhiChu2
			into #tempDataGhiChu
		FROM #tempGhiChu T1 
		GROUP BY sTenDonVi, iID_MaDonVi;

		select 
		 Round(sum(case when sXauNoiMa like '9010004-010-011-0001-0000' or 
		 sXauNoiMa like '9010004-010-011-0002-0000'
		 then (thuoc) else 0 end)/ @DonViTinh,0) fTienThuoc
		 , Round(sum(case when sXauNoiMa like '9010004-010-011-0001-0001' or 
		 sXauNoiMa like '9010004-010-011-0002-0001'
		 then (vtyt) else 0 end)/ @DonViTinh,0) fTienVTYT
		 , Round(sum(case when sXauNoiMa like '9010004-010-011-0001-0002' or 
		 sXauNoiMa like '9010004-010-011-0002-0002'
		 then (DVKT) else 0 end)/ @DonViTinh,0) fTienDVKT
		 , Round(sum(case when sXauNoiMa like '9010004-010-011-0001-0003' or 
		 sXauNoiMa like '9010004-010-011-0002-0003'
		 then (dcyt) else 0 end)/ @DonViTinh,0) fTienDCYT
		 , Round((sum(case when sXauNoiMa like '9010004-010-011-0001-0000' or 
		 sXauNoiMa like '9010004-010-011-0002-0000'
		 then (thuoc) else 0 end) 
		 + sum(case when sXauNoiMa like '9010004-010-011-0001-0001' or 
		 sXauNoiMa like '9010004-010-011-0002-0001'
		 then (vtyt) else 0 end) 
		 + sum(case when sXauNoiMa like '9010004-010-011-0001-0002' or 
		 sXauNoiMa like '9010004-010-011-0002-0002'
		 then (DVKT) else 0 end) 
		 + sum(case when sXauNoiMa like '9010004-010-011-0001-0003' or 
		 sXauNoiMa like '9010004-010-011-0002-0003'
		 then (dcyt) else 0 end) )/@DonViTinh,0) fTienTongCong
		 , iID_MaDonVi as IIDMaDonVi
		 , sTenDonVi
		 into #tempReulst
		from #tempData
		where iID_MaDonVi in (select * from f_split(@AgencyId))
		group by iID_MaDonVi, sTenDonVi
		order by iID_MaDonVi

		select 
		ROW_NUMBER() OVER(ORDER BY T1.IIDMaDonVi ASC) AS STT
		,T1.*
		,LTRIM(RTRIM(T2.SGhiChu1)) SGhiChu1
		,LTRIM(RTRIM(T2.SGhiChu2)) SGhiChu2 from #tempReulst T1
		left join #tempDataGhiChu T2 on T1.IIDMaDonVi=t2.iID_MaDonVi
		order by iID_MaDonVi

		drop table #tempGhiChu
		drop table #tempReulst
		drop table #tempData
		drop table #tempDataGhiChu
END
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_tonghop_bh]    Script Date: 12/24/2024 11:49:48 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_get_donvi_tonghop_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_get_donvi_tonghop_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_get_donvi_tonghop_bh]    Script Date: 12/24/2024 11:49:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay don vi cua chung tu tổng hop cap phat chi tiet theo id
-- =============================================
CREATE PROCEDURE [dbo].[sp_cp_get_donvi_tonghop_bh]
	@YearOfWork int,
	@Quy int,
	@IDLoaiChi nvarchar(500)
AS
BEGIN
	SELECT 
	distinct
	dv.* 
	FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN  DonVi  dv on dv.iID_MaDonVi IN (SELECT * FROM f_split(ctct.iID_MaDonVi))
	where ctct.iID_CP_ChungTu in 
	(
		select cp.iID_CP_ChungTu from BH_CP_ChungTu cp
		where cp.iNamChungTu=@YearOfWork
			and cp.iID_LoaiCap=@IDLoaiChi
			and cp.iQuy=@Quy
	)
	and dv.iNamLamViec=@YearOfWork
	order by dv.iID_MaDonVi
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_lns_BH]    Script Date: 12/24/2024 1:36:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_get_donvi_lns_BH]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_get_donvi_lns_BH]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_get_donvi_lns_BH]    Script Date: 12/24/2024 1:36:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_rpt_get_donvi_lns_BH]
@NamLamViec int,
@Quy int,
@LoaiChi uniqueidentifier
AS BEGIN
SET NOCOUNT ON;

SELECT 
distinct
	dv.iID_DonVi AS Id,
	dv.iID_MaDonVi as IIDMaDonVi,
	dv.sTenDonVi as TenDonVi,
	dv.sKyHieu as KyHieu,
	dv.sMoTa as MoTa,
	dv.iLoai as Loai,
	dv.iNamLamViec as NamLamViec,
	dv.iTrangThai as iTrangThai,
	dv.dNgayTao as DNgayTao,
	dv.sNguoiTao as SNguoiTao,
	dv.dNgaySua as DNgaySua,
	dv.dNgaySua as SNguoiSua,
	dv.*

		FROM BH_CP_ChungTu_ChiTiet ctct
			LEFT JOIN  DonVi  dv on dv.iID_MaDonVi IN (SELECT * FROM f_split(ctct.iID_MaDonVi))
			where ctct.iID_CP_ChungTu in 
			(
				select cp.iID_CP_ChungTu from BH_CP_ChungTu cp
				where cp.iNamChungTu=@NamLamViec
					and cp.iID_LoaiCap=@LoaiChi
					and cp.iQuy=@Quy
			)
			and dv.iNamLamViec=@NamLamViec
			order by dv.iID_MaDonVi
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]    Script Date: 12/24/2024 2:13:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]    Script Date: 12/24/2024 2:13:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@IdChungTu nvarchar(Max),
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int,
	--@DotNhan int,
	@IsMillionRound bit
AS
BEGIN

	select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
	right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
	where ct.iNamChungTu=@INamLamViec
	--and ct.iID_LoaiDanhMucChi=@IDLoaiChi
	and ctct.sMaLoaiChi=@MaLoaiChi
	--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
	--and ct.iLoaiDotNhanPhanBo=@DotNhan
	and ct.ID in (select * from f_split(@IdChungTu))
	--and ct.sSoQuyetDinh=@SoQuyetdinh
	and ctct.iID_MaDonVi IN (SELECT * FROM f_split(  @IdMaDonVi));

	select 
		 CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT
		, dv.sTenDonVi
		, dv.iID_MaDonVi
		, SUM(CASE WHEN @IsMillionRound = 1 THEN ROUND(ctct.fTongTien/1000000, 0)* 1000000 ELSE ctct.fTongTien END) as fTongTienDuToan
		, 0 IsHangCha
		, 0 RowNumber
		into #temp1
	from 
	#tempall ctct
	left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
	where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi))
	and
	dv.iNamLamViec=@INamLamViec
	and
	ctct.iNamLamViec=@INamLamViec
	group by  dv.iID_MaDonVi, dv.sTenDonVi 

	select 
	N'A' STT,
	N'Đơn vị dự toán' sTenDonVi,
	'' iID_MaDonVi,
	0 fTongTienDuToan,
	1 IsHangCha,
	0 RowNumber
	into #tempDonViDuToan

	select 
	N'B' STT,
	N'Đơn vị hạch toán' sTenDonVi,
	'' iID_MaDonVi,
	0 fTongTienDuToan,
	1 IsHangCha,
	0 RowNumber
	into #tempDonViHachToan

	------ create data don vi du toan
	CREATE TABLE #tempDvKDT(STT VARCHAR(6), sTenDonVi nvarchar(50), iID_MaDonVi varchar(50), fTongTienDuToan float, IsHangCha int, RowNumber int)
	INSERT INTO #tempDvKDT(STT, sTenDonVi, iID_MaDonVi, fTongTienDuToan, IsHangCha, RowNumber)
		SELECT B.* 
		from #temp1 B
	LEFT JOIN DonVi A ON A.iID_MaDonVi=B.iID_MaDonVi
	where A.iNamLamViec=@INamLamViec
	 and A.iiD_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))
	 and A.iKhoi=2
	 ORDER BY B.STT;
	------ Create table Stt
		Select  CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT,
			dv.iID_MaDonVi,
			dv.sTenDonVi
			into #tempSttKDT
			From #tempDvKDT dv
	------ Update stt 
		Update #tempDvKDT set #tempDvKDT.STT=A.STT
			From #tempSttKDT A
			where #tempDvKDT.iID_MaDonVi=A.iID_MaDonVi
				and #tempDvKDT.sTenDonVi=A.sTenDonVi
	------ create data don vi hach toan
		SELECT B.* into #tempDvKHT
		From DonVi A
		LEFT JOIN #temp1 B ON A.iID_MaDonVi=B.iID_MaDonVi
		where A.iNamLamViec=@INamLamViec
		 and A.iiD_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))
		 and A.iKhoi!=2

	 ------ Create table Stt
		Select  CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT,
			dv.iID_MaDonVi,
			dv.sTenDonVi
			into #tempSttKHT
			From #tempDvKHT dv
	------ Update stt 
		Update #tempDvKHT set #tempDvKHT.STT=A.STT
			From #tempSttKHT A
			where #tempDvKHT.iID_MaDonVi=A.iID_MaDonVi
				and #tempDvKHT.sTenDonVi=A.sTenDonVi

	 --- Create data merge don vi du toan
		SELECT  1 iLoai, * INTO #tempDataDVDT
		FROM
		(
			SELECT * FROM #tempDonViDuToan
			UNION ALL 
			SELECT * FROM #tempDvKDT
		)tempDataDVDT

		--- Tinh tong theo don vi du toan
		SELECT SUM(fTongTienDuToan) fTongTienDuToan
		INTO #SumTotalDuToan
		FROM #tempDvKDT
		--- update tong tien don vị du toan
		UPDATE #tempDataDVDT SET #tempDataDVDT.fTongTienDuToan=A.fTongTienDuToan
		FROM #SumTotalDuToan A
		WHERE #tempDataDVDT.sTenDonVi=N'Đơn vị dự toán' 
		AND #tempDataDVDT.STT=N'A'
	
		 --- Create data merge don vi hach toan
		SELECT  2 iLoai,* INTO #tempDataDVHT
		FROM
		(
			SELECT * FROM #tempDonViHachToan
			UNION ALL 
			SELECT * FROM #tempDvKHT
		)tempDataDVHT

		--- Tinh tong theo don vi hach toan
		SELECT SUM(fTongTienDuToan) fTongTienDuToan
		INTO #SumTotalHachToan
		FROM #tempDvKHT
		--- update tong tien don vị hach toan
		UPDATE #tempDataDVHT SET #tempDataDVHT.fTongTienDuToan=A.fTongTienDuToan
		FROM #SumTotalHachToan A
		WHERE #tempDataDVHT.sTenDonVi=N'Đơn vị hạch toán'
		AND #tempDataDVHT.STT=N'B'

		--- create merge don vi du toan voi don vi hach toan vào
		SELECT * into #tblresult
		FROM
		(
			SELECT * FROM #tempDataDVDT
			UNION ALL 
			SELECT * FROM #tempDataDVHT
		)tempDataAll

	select rs.STT
		 , rs.iID_MaDonVi
		 , rs.IsHangCha
		 , rs.RowNumber
		 , rs.sTenDonVi
		 , rs.fTongTienDuToan/@Donvitinh fTongTienDuToan
		 , rs.iLoai
	FROM  #tblresult rs
	order by rs.iLoai,rs.iID_MaDonVi

DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #tempDonViDuToan
DROP TABLE #tempDonViHachToan
DROP TABLE #tempDvKDT
DROP TABLE #tempDvKHT
DROP TABLE #tempSttKDT
DROP TABLE #tempSttKHT
DROP TABLE #SumTotalDuToan
DROP TABLE #SumTotalHachToan
DROP TABLE #tempDataDVDT
DROP TABLE #tempDataDVHT
DROP TABLE #tblresult
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
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 12/24/2024 3:46:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 12/24/2024 3:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100),
	@DotPB int
As

begin
	-- Lấy danh mục Phân bổ thu BHXH
	SELECT round(SUM(0.1*(isnull(pbctct.fBHYT_NLD,0)+ isnull(pbctct.fBHYT_NSD,0))) / 1000000,0) * 1000000 as fTienPhanBo, pbctct.iID_MaDonVi INTO #temPBThuBHXH
	FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet as pbctct
	JOIN BH_DTT_BHXH_PhanBo_ChungTu as pbct
	ON pbctct.iID_DTT_BHXH_ChungTu = pbct.iID_DTT_BHXH_PhanBo_ChungTu
	WHERE (pbctct.sXauNoiMa like '9020001-010-011-0001%' or pbctct.sXauNoiMa like '9020002-010-011-0001%')
	AND pbctct.iNamLamViec = @NamLamViec
	AND pbctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND pbct.iLoaiDuToan = @DotPB
	GROUP BY pbctct.iID_MaDonVi

	---Lấy danh sách mục lục ngân sách theo điều kiện @LNS
	select 
	NEWID() as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	BH_DM_MucLucNganSach.iID_MLNS as iID_MLNS,
	BH_DM_MucLucNganSach.iID_MLNS_Cha,
	BH_DM_MucLucNganSach.sLNS,
	BH_DM_MucLucNganSach.sL,
	BH_DM_MucLucNganSach.sK,
	BH_DM_MucLucNganSach.sM,
	BH_DM_MucLucNganSach.sTM,
	BH_DM_MucLucNganSach.sTTM,
	BH_DM_MucLucNganSach.sNG,
	BH_DM_MucLucNganSach.sTNG,
	BH_DM_MucLucNganSach.sXauNoiMa,
	BH_DM_MucLucNganSach.sMoTa as sNoiDung,
	0 as fTienTuChi,
	--0 as fTienTuChiTruocDieuChinh,
	--0 as fTienHienVat,
	--0 as fTienHienVatTruocDieuChinh,
	BH_DM_MucLucNganSach.sCPChiTietToi,
	BH_DM_MucLucNganSach.sDuToanChiTietToi,
	1 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	BH_DM_MucLucNganSach.bHangChaDuToan,
	0 as IsRemainRow
	into #tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	--where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  and sLNS LIKE '901%'
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  
	and bHangChaDuToan is not null
	and iTrangThai=1
	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  #tblDonVi
	from DonVi where iNamLamViec = @NamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--lấy danh sách dự toán nhận phân bổ
	select *
	into #tblChungTuNhanPhanBo
	from BH_DTC_Nhan_PhanBo_Map
	where iID_BHDTC_PhanBo = @ChungTuId

	
	---Lấy danh sách chi tiết dự toán toán nhận phân bổ
	select 
			nhanpb_chitiet.ID as iIDNhan_ChiTiet,
			nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
			'' as iID_MaDonVi,
			nhanpb_chitiet.iID_MucLucNganSach,
			nhanpb_chitiet.fTienTuChi as fTuChi,
			--nhanpb_chitiet.fTienHienVat as fHienVat,
			nhanpb.sSoQuyetDinh
	into #tblChiTietDuToanNhan
	from BH_DTC_DuToanChiTrenGiao_ChiTiet as nhanpb_chitiet
	inner join BH_DTC_DuToanChiTrenGiao as nhanpb on nhanpb.ID = nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao
	where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)

	

	---Lấy  danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		#tblChiTietDuToanNhan.iID_DTC_DuToanChiTrenGiao,
		#tblChiTietDuToanNhan.iID_MucLucNganSach as iID_MLNS,
		#tblMucLucNganSach.iID_MLNS_Cha,
		#tblMucLucNganSach.sLNS,
		#tblMucLucNganSach.sL,
		#tblMucLucNganSach.sK, 
		#tblMucLucNganSach.sM,
		#tblMucLucNganSach.sTM,
		#tblMucLucNganSach.sTTM,
		#tblMucLucNganSach.sNG,
		#tblMucLucNganSach.sTNG,
		#tblMucLucNganSach.sXauNoiMa,
		#tblMucLucNganSach.sNoiDung,
		#tblChiTietDuToanNhan.sSoQuyetDinh,
		#tblChiTietDuToanNhan.fTuChi as fTienTuChi ,
		--#tblChiTietDuToanNhan.fHienVat as fTienHienVat,
		#tblMucLucNganSach.sCPChiTietToi,
		#tblMucLucNganSach.sDuToanChiTietToi,
		3 as Type
	into #tbl_tblChiTietDuToanNhan_MucLuc
	from #tblMucLucNganSach
	inner join #tblChiTietDuToanNhan on #tblMucLucNganSach.iID_MLNS = #tblChiTietDuToanNhan.iID_MucLucNganSach

	


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  #tbl_tblChiTietDuToanNhan_MucLuc.*,#tblDonVi.iID_MaDonVi, concat(#tblDonVi.iID_MaDonVi, '-', #tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into #temp
	from #tbl_tblChiTietDuToanNhan_MucLuc cross join #tblDonVi 


	---Map với bảng BH_DTC_PhanBoDuToanChi_ChiTiet để lấy thông tin fTuChi đã được phân bổ
	select 
		#temp.iID_DTC_DuToanChiTrenGiao, 
		chitiet_phanbo.ID as iID_DTC_PhanBoDuToanChiTiet,
		#temp.iID_MLNS,
		#temp.iID_MLNS_Cha,
		#temp.sLNS,
		#temp.sL,
		#temp.sK,
		#temp.sM,
		#temp.sTM,
		#temp.sTTM,
		#temp.sNG,
		#temp.sTNG,
		#temp.sXauNoiMa,
		#temp.sNoiDung as sNoiDung,
		chitiet_phanbo.fTienTuChi as fTienTuChi,
		#temp.fTienTuChi as fTienTuChiTruocDieuChinh,
		--chitiet_phanbo.fTienHienVat as fTienHienVat,
		--#temp.fTienHienVat as fTienHienVatTruocDieuChinh,
		#temp.sCPChiTietToi,
		#temp.sDuToanChiTietToi,
		3 as Type,
		#temp.iID_MaDonVi,
		#temp.sTenDonVi,
		#temp.sSoQuyetDinh,
		0 as bHangCha,
		0 as bHangChaDuToan,
		0 as IsRemainRow
	into #temp1
	from #temp
	left join 
		(
			select * 
			from BH_DTC_PhanBoDuToanChi_ChiTiet 
			where iID_DTC_PhanBoDuToanChi = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_DTC_DuToanChiTrenGiao = #temp.iID_DTC_DuToanChiTrenGiao and chitiet_phanbo.iID_MaDonVi = #temp.iID_MaDonVi and chitiet_phanbo.iID_MucLucNganSach = #temp.iID_MLNS



	-----Lấy danh sách số chưa phân bổ
	select 
	npb.ID as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	muluc_ngansach.iID_MLNS as iID_MLNS,
	muluc_ngansach.iID_MLNS_Cha,
	muluc_ngansach.sLNS,
	muluc_ngansach.sL,
	muluc_ngansach.sK,
	muluc_ngansach.sM,
	muluc_ngansach.sTM,
	muluc_ngansach.sTTM,
	muluc_ngansach.sNG,
	muluc_ngansach.sTNG,
	muluc_ngansach.sXauNoiMa,
	N'Số chưa phân bổ' as sNoiDung,
	chitiet_chuaphanbo.fTuChi as fTienTuChi,
	chitiet_chuaphanbo.fTuChi as fTienTuChiTruocDieuChinh,
	--chitiet_chuaphanbo.fHienVat as fTienHienVat,
	--chitiet_chuaphanbo.fHienVat as fTienHienVatTruocDieuChinh,
	muluc_ngansach.sCPChiTietToi,
	muluc_ngansach.sDuToanChiTietToi,
	2 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as bHangChaDuToan,
	1 as IsRemainRow
	into #tblSoChuaPhanBo
	from #tblMucLucNganSach as muluc_ngansach
	inner join 
	(
		select (
		
		ISNULL(ct_npb.fTienTuChi,0) -  ISNULL(ct_pb_t.fTuChi,0) ) as fTuChi ,
		ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		--select (ISNULL(ct_npb.fTienTuChi,0) - ISNULL(ct_pb_t.fTuChi,0)) as fTuChi , (ISNULL(ct_npb.fTienHienVat,0) - ISNULL(ct_pb_t.fHienVat,0)) as fHienVat, ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		from BH_DTC_DuToanChiTrenGiao_ChiTiet as ct_npb
		left join
			(
				select  sum(  fTienTuChi) as fTuChi , iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
				from BH_DTC_PhanBoDuToanChi_ChiTiet as ct_pb
				where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)
				group by  iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
			)as ct_pb_t  

		on ct_pb_t.iID_MucLucNganSach = ct_npb.iID_MucLucNganSach and ct_npb.iID_DTC_DuToanChiTrenGiao = ct_pb_t.iID_DTC_DuToanChiTrenGiao) as chitiet_chuaphanbo
		on chitiet_chuaphanbo.iID_MucLucNganSach = muluc_ngansach.iID_MLNS 
	inner join BH_DTC_DuToanChiTrenGiao as npb on npb.ID = chitiet_chuaphanbo.iID_DTC_DuToanChiTrenGiao
	where npb.ID in ( select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo);
	---- Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select 
	#tblSoChuaPhanBo.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	#tblMucLucNganSach.iID_MLNS as iID_MLNS,
	#tblMucLucNganSach.iID_MLNS_Cha,
	#tblMucLucNganSach.sLNS,
	#tblMucLucNganSach.sL,
	#tblMucLucNganSach.sK,
	#tblMucLucNganSach.sM,
	#tblMucLucNganSach.sTM,
	#tblMucLucNganSach.sTTM,
	#tblMucLucNganSach.sNG,
	#tblMucLucNganSach.sTNG,
	#tblMucLucNganSach.sXauNoiMa,
	#tblMucLucNganSach.sNoiDung as sNoiDung,
	#tblSoChuaPhanBo.fTienTuChi as fTienTuChi,
	#tblSoChuaPhanBo.fTienTuChiTruocDieuChinh as fTienTuChiTruocDieuChinh,
	--#tblSoChuaPhanBo.fTienHienVat as fTienHienVat,
	--#tblSoChuaPhanBo.fTienHienVatTruocDieuChinh as fTienHienVatTruocDieuChinh,
	#tblMucLucNganSach.sCPChiTietToi,
	#tblMucLucNganSach.sDuToanChiTietToi,
	case when #tblSoChuaPhanBo.Type = 2 then 2 else #tblMucLucNganSach.Type end Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	#tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangCha,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangChaDuToan,
	0 as IsRemainRow
	into #tblMucLucNganSach_duplicate
	from #tblMucLucNganSach
	left join #tblSoChuaPhanBo on #tblMucLucNganSach.iID_MLNS = #tblSoChuaPhanBo.iID_MLNS
	order by sXauNoiMa
	
	--select * from tblMucLucNganSach_duplicate
	--select * from tblSoChuaPhanBo
	--select * from #temp1
	---Tính lại dự toán, số đã phân bổ
	-- Dữ liệu nhận phân bổ
	declare @iiDotNhan nvarchar(500) =( select  iID_DotNhan from  BH_DTC_PhanBoDuToanChi where ID = @ChungTuId);
	select iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao, fTienTuChi INTO #tmpNhanDuToan from BH_DTC_DuToanChiTrenGiao_ChiTiet ct
	INNER JOIN BH_DTC_DuToanChiTrenGiao dt on dt.ID = ct.iID_DTC_DuToanChiTrenGiao
	where dt.ID IN (select * from splitstring( @iiDotNhan))


	-- Dữ liệu đã phân bổ
	declare @dNgayQuyetDinh Datetime = (select dNgayQuyetDinh from BH_DTC_PhanBoDuToanChi where ID = @ChungTuId);
	select iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao, 0 - SUM(ISNULL(fTienTuChi,0)) fTuChi   INTO #tmpDaPhanBo from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	INNER JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID
	where 
	ct.iNamChungTu = @NamLamViec
	AND ct.dNgayQuyetDinh < @dNgayQuyetDinh
	group by iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao;

	--Hiển thị kết quả trả về
	select * INTO #result from
	(
		SELECT * from #tblMucLucNganSach_duplicate
		UNION ALL
		SELECT * from #tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as test
	--order by test.sXauNoiMa, test.sSoQuyetDinh, test.iID_MaDonVi,test.Type,test.IsRemainRow

	----============
	SELECT
	rs.iID_DTC_DuToanChiTrenGiao,
	rs.iID_DTC_PhanBoDuToanChiTiet,
	rs.iID_MLNS,
	rs.iID_MLNS_Cha,
	rs.sLNS,
	rs.sL,
	rs.sK,
	rs.sM,
	rs.sTM,
	rs.sTTM,
	rs.sNG,
	rs.sTNG,
	rs.sXauNoiMa,
	rs.sNoiDung as sNoiDung,

	CASE WHEN rs.sXauNoiMa = '9010004' THEN pbbhxh.fTienPhanBo
	ELSE (
		CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		ELSE rs.fTienTuChi
		END
	)
	END as fTienTuChi,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		ELSE rs.fTienTuChi
	END as fTienTuChiTruocDieuChinh,
	--#tblSoChuaPhanBo.fTienHienVat as fTienHienVat,
	--#tblSoChuaPhanBo.fTienHienVatTruocDieuChinh as fTienHienVatTruocDieuChinh,
	rs.sCPChiTietToi,
	rs.sDuToanChiTietToi,
	rs.Type,
	rs.iID_MaDonVi,
	rs.sTenDonVi,
	rs.sSoQuyetDinh,
	rs.bHangCha,
	rs.bHangChaDuToan,
	rs.IsRemainRow
	FROM #result rs
	LEFT JOIN #tmpNhanDuToan dt ON rs.iID_MLNS = dt.iID_MucLucNganSach 
	LEFT JOIN #tmpDaPhanBo dpb ON dpb.iID_MucLucNganSach = rs.iID_MLNS and dpb.iID_DTC_DuToanChiTrenGiao = rs.iID_DTC_DuToanChiTrenGiao
	LEFT JOIN (
	SELECT SUM(fTienTuChi) fTuChi, iID_MucLucNganSach FROM BH_DTC_PhanBoDuToanChi_ChiTiet WHERE iID_DTC_PhanBoDuToanChi = @ChungTuId GROUP BY iID_MucLucNganSach

	) ct ON ct.iID_MucLucNganSach = rs.iID_MLNS
	LEFT JOIN #temPBThuBHXH pbbhxh ON rs.iID_MaDonVi = pbbhxh.iID_MaDonVi
	order by rs.sXauNoiMa, rs.sSoQuyetDinh, rs.iID_MaDonVi,rs.Type,rs.IsRemainRow
	--SELECT * from #tblSoChuaPhanBo


drop table #tblMucLucNganSach;
drop table #tblDonVi;
drop table #tblChungTuNhanPhanBo;
drop table #tblChiTietDuToanNhan;
drop table #tbl_tblChiTietDuToanNhan_MucLuc;
drop table #temp;
drop table #temp1;
drop table #tblSoChuaPhanBo;
drop table #tblMucLucNganSach_duplicate;

end
;
;
;
;
;
;
GO
