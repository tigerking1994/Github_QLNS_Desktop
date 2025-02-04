/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]    Script Date: 2/16/2024 2:56:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]    Script Date: 2/16/2024 2:56:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]    Script Date: 2/16/2024 2:56:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]    Script Date: 2/16/2024 2:56:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]    Script Date: 2/16/2024 2:56:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]    Script Date: 2/16/2024 2:56:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]    Script Date: 2/16/2024 2:56:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]    Script Date: 2/16/2024 2:56:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]    Script Date: 2/16/2024 2:56:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi]    Script Date: 2/16/2024 2:56:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]    Script Date: 2/16/2024 2:56:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]    Script Date: 2/16/2024 2:56:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitheodieuchinh]    Script Date: 2/16/2024 2:56:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitheodieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitheodieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitheodieuchinh]    Script Date: 2/16/2024 2:56:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitheodieuchinh]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50),
@IDLoaiChi nvarchar(100)

AS
BEGIN

SELECT 
	A.sLNS,
	A.sTM,
	A.sTTM,
	A.sM,
	A.sNG,
	A.sMoTa AS sNoiDung,
	A.sXauNoiMa,
	A.iID_MLNS,
	A.iID_MLNS_Cha,
	A.bHangCha,
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.FTienTangGiam as fTienTuChi,
	A.iNamlamViec,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN (
		SELECT 	
			@iDNdtctg as iID_DTC_DuToanChiTrenGiao
			, ctct.iID_MucLucNganSach
			, ctct.sNoiDung
			, ctct.fTienSoSanhTang
			, ctct.fTienSoSanhGiam
			, CASE WHEN  SUM(ISNULL(ctct.fTienSoSanhTang,0))-   SUM(ISNULL(ctct.fTienSoSanhGiam,0)) >0 THEN SUM(ISNULL(ctct.fTienSoSanhTang,0))-   SUM(ISNULL(ctct.fTienSoSanhGiam,0)) 
				ELSE - ( SUM(ISNULL(ctct.fTienSoSanhGiam,0))-SUM(ISNULL(ctct.fTienSoSanhTang,0))) END FTienTangGiam
		FROM BH_DTC_DieuChinhDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_DieuChinhDuToanChi ct on ctct.iID_BH_DTC=ct.iID_BH_DTC
	WHERE (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
		AND ct.iNamLamViec=@NamLamViec
		AND ct.bIsKhoa=1
		AND ct.iID_LoaiCap=@IDLoaiChi
		GROUP BY ctct.iID_MucLucNganSach, ctct.sNoiDung, ctct.fTienSoSanhTang
			, ctct.fTienSoSanhGiam) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
	order by A.sXauNoiMa
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]    Script Date: 2/16/2024 2:56:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]
	@NamLamViec int,
	@IdDonVi nvarchar(200)
AS
BEGIN

	
					SELECT 
					fBHYT_NLD,
					fBHYT_NSD,
					iID_MaDonVi,
					iID_MLNS,
					iNamLamViec
					into #temp1
					FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet 
					WHERE iID_DTT_BHXH_ChungTu IN
					(	
							SELECT iID_DTT_BHXH_PhanBo_ChungTu FROM BH_DTT_BHXH_PhanBo_ChungTu
							WHERE iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
							AND iNamLamViec=@NamLamViec
							AND bKhoa=1

					)
					and
                    (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0001%')
                    AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
                    AND iNamLamViec = @NamLamViec


					SELECT 
					fThuBHYT_NSD_QTCuoiNam,
					fThuBHYT_NLD_QTCuoiNam,
					fThuBHYT_NSD_QTDauNam,
					fThuBHYT_NLD_QTDauNam,
					iID_MaDonVi,
					iID_MucLucNganSach,
					iNamLamViec
					into #temp2
					 FROM BH_DTT_BHXH_DieuChinh_ChiTiet 
					WHERE iID_DTT_BHXH_DieuChinh IN
					(	
							SELECT iID_DTT_BHXH_DieuChinh FROM BH_DTT_BHXH_DieuChinh
							WHERE iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
							AND bIsKhoa=1
							AND iNamLamViec=@NamLamViec

					)
					and
                    (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0001%')
                    AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
                    AND iNamLamViec = @NamLamViec

					

		SELECT 
                    NEWID() as IID_MucLucNganSach,
                    N'10% kế hoạch thu BHYT quân nhân' as SNoiDung,
                    (sum
						(
						(	
						((ISNULL(B.fThuBHYT_NSD_QTCuoiNam,0))  +  (ISNULL(B.fThuBHYT_NLD_QTCuoiNam,0)) 
						+ (ISNULL(B.fThuBHYT_NSD_QTDauNam ,0))   + (ISNULL(B.fThuBHYT_NLD_QTDauNam,0)))
						- (A.fBHYT_NLD+A.fBHYT_NSD)
							)
					*10)
					)
					/100 as FTienTangGiam,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    1 as IRemainRow,
					'SLNS' SDuToanChiTietToi,
					null SM

				 FROM #temp1 A
				 LEFT JOIN #temp2 B ON A.iID_MaDonVi=B.iID_MaDonVi
					
              UNION
              SELECT 
                    NEWID() as IID_MucLucNganSach,
                    N'Số còn lại' as SNoiDung,
                    0 as FTienTangGiam,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    2 as IRemainRow,
					'SLNS' SDuToanChiTietToi,
					null SM
					;

			drop table #temp1
			drop table #temp2
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]    Script Date: 2/16/2024 2:56:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]
	@NamLamViec int,
	@IdDonVi nvarchar(200)
AS
BEGIN

	
					SELECT 
					fBHYT_NLD,
					fBHYT_NSD,
					iID_MaDonVi,
					iID_MLNS,
					iNamLamViec
					into #temp1
					FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet 
					WHERE iID_DTT_BHXH_ChungTu IN
					(	
							SELECT iID_DTT_BHXH_PhanBo_ChungTu FROM BH_DTT_BHXH_PhanBo_ChungTu
							WHERE iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
							AND iNamLamViec=@NamLamViec
							AND bKhoa=1

					)
					and
                    (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0001%')
                    AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
                    AND iNamLamViec = @NamLamViec


					SELECT 
					fThuBHYT_NSD_QTCuoiNam,
					fThuBHYT_NLD_QTCuoiNam,
					fThuBHYT_NSD_QTDauNam,
					fThuBHYT_NLD_QTDauNam,
					iID_MaDonVi,
					iID_MucLucNganSach,
					iNamLamViec
					into #temp2
					 FROM BH_DTT_BHXH_DieuChinh_ChiTiet 
					WHERE iID_DTT_BHXH_DieuChinh IN
					(	
							SELECT iID_DTT_BHXH_DieuChinh FROM BH_DTT_BHXH_DieuChinh
							WHERE iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
							AND bIsKhoa=1
							AND iNamLamViec=@NamLamViec

					)
					and
                    (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0001%')
                    AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
                    AND iNamLamViec = @NamLamViec

					

		SELECT 
                    NEWID() as iID_MLNS,
                    N'10% kế hoạch thu BHYT quân nhân' as SNoiDung,
                    (sum
						(
						(	
						((ISNULL(B.fThuBHYT_NSD_QTCuoiNam,0))  +  (ISNULL(B.fThuBHYT_NLD_QTCuoiNam,0)) 
						+ (ISNULL(B.fThuBHYT_NSD_QTDauNam ,0))   + (ISNULL(B.fThuBHYT_NLD_QTDauNam,0)))
						- (A.fBHYT_NLD+A.fBHYT_NSD)
							)
					*10)
					)
					/100 as fTienTuChi,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    1 as IRemainRow,
					'LNS' SDuToanChiTietToi,
					null SM

				 FROM #temp1 A
				 LEFT JOIN #temp2 B ON A.iID_MaDonVi=B.iID_MaDonVi
					
              UNION
              SELECT 
                    NEWID() as iID_MLNS,
                    N'Số còn lại' as SNoiDung,
                    0 as fTienTuChi,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    2 as IRemainRow,
					'LNS' SDuToanChiTietToi,
					null SM
					;

			drop table #temp1
			drop table #temp2
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi]    Script Date: 2/16/2024 2:56:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@IdChungTu nvarchar(4000)
AS BEGIN
SET NOCOUNT ON;

SELECT DISTINCT 
		donvi.iID_DonVi AS Id,
		donvi.iID_MaDonVi as IIDMaDonVi,
		donvi.sTenDonVi as TenDonVi,
		donvi.sKyHieu as KyHieu,
		donvi.sMoTa as MoTa,
		donvi.iLoai as Loai,
		donvi.iNamLamViec as NamLamViec,
		donvi.iTrangThai as iTrangThai

FROM BH_DTC_PhanBoDuToanChi chungtu
INNER JOIN  BH_DTC_PhanBoDuToanChi_ChiTiet  chitiet ON chungtu.ID = chitiet.iID_DTC_PhanBoDuToanChi
INNER JOIN
(
SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @NamLamViec
) donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chitiet.iNamLamViec = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiDotNhanPhanBo=@DotNhan
  AND chungtu.sMaLoaiChi=@MaLoaichi
  AND chungTu.ID in (select * from f_split(@IdChungTu))
	END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]    Script Date: 2/16/2024 2:56:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi_bandau_or_bosung]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@IdChungTu nvarchar(4000),
@DotNhan int
AS BEGIN
SET NOCOUNT ON;

SELECT DISTINCT 
		donvi.iID_DonVi AS Id,
		donvi.iID_MaDonVi as IIDMaDonVi,
		donvi.sTenDonVi as TenDonVi,
		donvi.sKyHieu as KyHieu,
		donvi.sMoTa as MoTa,
		donvi.iLoai as Loai,
		donvi.iNamLamViec as NamLamViec,
		donvi.iTrangThai as iTrangThai


FROM BH_DTC_PhanBoDuToanChi chungtu
INNER JOIN  BH_DTC_PhanBoDuToanChi_ChiTiet  chitiet ON chungtu.ID = chitiet.iID_DTC_PhanBoDuToanChi
INNER JOIN
(
SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @NamLamViec
) donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chitiet.iNamLamViec = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  AND chungtu.iLoaiDotNhanPhanBo=@DotNhan
  AND chungtu.sMaLoaiChi=@MaLoaichi
  AND chungTu.ID in (select * from f_split(@IdChungTu))
	END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]    Script Date: 2/16/2024 2:56:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@IdChungTu nvarchar(4000),
	@Donvitinh int 
	
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
inner join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103) <= convert(varchar,@SNgayQuyetDinh,101)
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and ct.ID in (select * from f_split(@IdChungTu))
select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, 0 fTienTroCapOmDau
	, 0 fTienTroCapThaiSan
	, 0 fTienTroCapTaiNan
	, 0 fTienTroCapHuuTri
	, 0 fTienTroCapPhucVien
	, 0 fTienTroCapXuatNgu
	, 0 fTienTroCapThoiViec
	, 0 fTienTroCapTuTuat
	, 1 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi

select
	null as STT
	, N'Khối dự toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 1 RowNumber
	into #temp2
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND(
ctct.sXauNoiMa like '9010001-010-011-0002%'
OR ctct.sXauNoiMa like '9010001-010-011-0001%'
OR ctct.sXauNoiMa like '9010001-010-011-0003%'
OR ctct.sXauNoiMa like '9010001-010-011-0004%'
OR ctct.sXauNoiMa like '9010001-010-011-0005%'
OR ctct.sXauNoiMa like '9010001-010-011-0006%'
OR ctct.sXauNoiMa like '9010001-010-011-0007%'
OR ctct.sXauNoiMa like '9010001-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi 

select
	null as STT
	, N'Khối hạch toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 2 RowNumber
	into #temp3
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND (

ctct.sXauNoiMa like '9010002-010-011-0001%'
OR ctct.sXauNoiMa like '9010002-010-011-0002%'
OR ctct.sXauNoiMa like '9010002-010-011-0003%'
OR ctct.sXauNoiMa like '9010002-010-011-0004%'
OR ctct.sXauNoiMa like '9010002-010-011-0005%'
OR ctct.sXauNoiMa like '9010002-010-011-0006%'
OR ctct.sXauNoiMa like '9010002-010-011-0007%'
OR ctct.sXauNoiMa like '9010002-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi

SELECT  NEWID() as Id,* INTO #tempRESULT
from
(
	SELECT * FROM #temp1
	UNION ALL 
	SELECT * FROM #temp2
	UNION ALL 
	SELECT * FROM #temp3
) tempRESULT;

With #tree(id,sTenDonVi,iID_MaDonVi, STT, IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat , RowNumber,position, iIdParent)
as
(
	SELECT  id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat,RowNumber,
						CAST(ROW_NUMBER() OVER(ORDER BY STT) AS NVARCHAR(MAX)) AS position, NEWID()
	FROM #tempRESULT
	WHERE  IsHangCha = 1 
	UNION ALL 
	SELECT chil.Id, chil.sTenDonVi, chil.iID_MaDonVi,   chil.STT, chil.IsHangCha,chil.fTienTroCapOmDau,chil.fTienTroCapThaiSan,chil.fTienTroCapTaiNan,chil.fTienTroCapHuuTri,chil.fTienTroCapPhucVien,chil.fTienTroCapXuatNgu, chil.fTienTroCapThoiViec, chil.fTienTroCapTuTuat,chil.RowNumber,
	CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.iID_MaDonVi) AS NVARCHAR(MAX))) AS position, pr.id
	FROM #tempRESULT chil
	INNER JOIN #tree pr ON pr.iID_MaDonVi = chil.iID_MaDonVi AND  pr.IsHangCha = 1
	WHERE chil.STT IS NULL AND chil.IsHangCha = 0 

	
)
select *, 
		cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
		INTO #result 
from #tree 
--temp table sum chill
SELECT  iIdParent
		,SUM(fTienTroCapOmDau) fTienTroCapOmDau
		,SUM(fTienTroCapThaiSan) fTienTroCapThaiSan
		,SUM(fTienTroCapTaiNan) fTienTroCapTaiNan
		,SUM(fTienTroCapHuuTri) fTienTroCapHuuTri
		,SUM(fTienTroCapPhucVien) fTienTroCapPhucVien
		,SUM(fTienTroCapXuatNgu) fTienTroCapXuatNgu
		,SUM(fTienTroCapThoiViec) fTienTroCapThoiViec
		,SUM(fTienTroCapTuTuat) fTienTroCapTuTuat
		INTO #tempSumParent
FROM #result
WHERE IsHangCha = 0
Group by iIdParent

Update #result
SET iIdParent = null
where IsHangCha = 1;

select id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapOmDau ELSE rs.fTienTroCapOmDau END) fTienTroCapOmDau,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThaiSan ELSE rs.fTienTroCapThaiSan END) fTienTroCapThaiSan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTaiNan ELSE rs.fTienTroCapTaiNan END) fTienTroCapTaiNan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapHuuTri ELSE rs.fTienTroCapHuuTri END) fTienTroCapHuuTri,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapPhucVien ELSE rs.fTienTroCapPhucVien END) fTienTroCapPhucVien,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapXuatNgu ELSE rs.fTienTroCapXuatNgu END) fTienTroCapXuatNgu,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThoiViec ELSE rs.fTienTroCapThoiViec END) fTienTroCapThoiViec,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTuTuat ELSE rs.fTienTroCapTuTuat END) fTienTroCapTuTuat,
			RowNumber,
			rs.position
from #result rs
LEFT JOIN #tempSumParent su ON rs.id = su.iIdParent
order by sort;

DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #tempRESULT
DROP TABLE #result
DROP TABLE #tempSumParent

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]    Script Date: 2/16/2024 2:56:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]	
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@IdChungTu nvarchar(4000),
	@Donvitinh int ,
	@DotNhan int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
inner join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103) <= convert(varchar,@SNgayQuyetDinh,101)
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and ct.iLoaiDotNhanPhanBo=@DotNhan
and ct.ID in (select * from f_split(@IdChungTu))
select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, 0 fTienTroCapOmDau
	, 0 fTienTroCapThaiSan
	, 0 fTienTroCapTaiNan
	, 0 fTienTroCapHuuTri
	, 0 fTienTroCapPhucVien
	, 0 fTienTroCapXuatNgu
	, 0 fTienTroCapThoiViec
	, 0 fTienTroCapTuTuat
	, 1 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi

select
	null as STT
	, N'Khối dự toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 1 RowNumber
	into #temp2
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND(
ctct.sXauNoiMa like '9010001-010-011-0002%'
OR ctct.sXauNoiMa like '9010001-010-011-0001%'
OR ctct.sXauNoiMa like '9010001-010-011-0003%'
OR ctct.sXauNoiMa like '9010001-010-011-0004%'
OR ctct.sXauNoiMa like '9010001-010-011-0005%'
OR ctct.sXauNoiMa like '9010001-010-011-0006%'
OR ctct.sXauNoiMa like '9010001-010-011-0007%'
OR ctct.sXauNoiMa like '9010001-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi 

select
	null as STT
	, N'Khối hạch toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 2 RowNumber
	into #temp3
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND (

ctct.sXauNoiMa like '9010002-010-011-0001%'
OR ctct.sXauNoiMa like '9010002-010-011-0002%'
OR ctct.sXauNoiMa like '9010002-010-011-0003%'
OR ctct.sXauNoiMa like '9010002-010-011-0004%'
OR ctct.sXauNoiMa like '9010002-010-011-0005%'
OR ctct.sXauNoiMa like '9010002-010-011-0006%'
OR ctct.sXauNoiMa like '9010002-010-011-0007%'
OR ctct.sXauNoiMa like '9010002-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi

SELECT  NEWID() as Id,* INTO #tempRESULT
from
(
	SELECT * FROM #temp1
	UNION ALL 
	SELECT * FROM #temp2
	UNION ALL 
	SELECT * FROM #temp3
) tempRESULT;

With #tree(id,sTenDonVi,iID_MaDonVi, STT, IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat , RowNumber,position, iIdParent)
as
(
	SELECT  id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat,RowNumber,
						CAST(ROW_NUMBER() OVER(ORDER BY STT) AS NVARCHAR(MAX)) AS position, NEWID()
	FROM #tempRESULT
	WHERE  IsHangCha = 1 
	UNION ALL 
	SELECT chil.Id, chil.sTenDonVi, chil.iID_MaDonVi,   chil.STT, chil.IsHangCha,chil.fTienTroCapOmDau,chil.fTienTroCapThaiSan,chil.fTienTroCapTaiNan,chil.fTienTroCapHuuTri,chil.fTienTroCapPhucVien,chil.fTienTroCapXuatNgu, chil.fTienTroCapThoiViec, chil.fTienTroCapTuTuat,chil.RowNumber,
	CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.iID_MaDonVi) AS NVARCHAR(MAX))) AS position, pr.id
	FROM #tempRESULT chil
	INNER JOIN #tree pr ON pr.iID_MaDonVi = chil.iID_MaDonVi AND  pr.IsHangCha = 1
	WHERE chil.STT IS NULL AND chil.IsHangCha = 0 

	
)
select *, 
		cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
		INTO #result 
from #tree 
--temp table sum chill
SELECT  iIdParent
		,SUM(fTienTroCapOmDau) fTienTroCapOmDau
		,SUM(fTienTroCapThaiSan) fTienTroCapThaiSan
		,SUM(fTienTroCapTaiNan) fTienTroCapTaiNan
		,SUM(fTienTroCapHuuTri) fTienTroCapHuuTri
		,SUM(fTienTroCapPhucVien) fTienTroCapPhucVien
		,SUM(fTienTroCapXuatNgu) fTienTroCapXuatNgu
		,SUM(fTienTroCapThoiViec) fTienTroCapThoiViec
		,SUM(fTienTroCapTuTuat) fTienTroCapTuTuat
		INTO #tempSumParent
FROM #result
WHERE IsHangCha = 0
Group by iIdParent

Update #result
SET iIdParent = null
where IsHangCha = 1;

select id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapOmDau ELSE rs.fTienTroCapOmDau END) fTienTroCapOmDau,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThaiSan ELSE rs.fTienTroCapThaiSan END) fTienTroCapThaiSan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTaiNan ELSE rs.fTienTroCapTaiNan END) fTienTroCapTaiNan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapHuuTri ELSE rs.fTienTroCapHuuTri END) fTienTroCapHuuTri,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapPhucVien ELSE rs.fTienTroCapPhucVien END) fTienTroCapPhucVien,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapXuatNgu ELSE rs.fTienTroCapXuatNgu END) fTienTroCapXuatNgu,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThoiViec ELSE rs.fTienTroCapThoiViec END) fTienTroCapThoiViec,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTuTuat ELSE rs.fTienTroCapTuTuat END) fTienTroCapTuTuat,
			RowNumber,
			rs.position
from #result rs
LEFT JOIN #tempSumParent su ON rs.id = su.iIdParent
order by sort;

DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #tempRESULT
DROP TABLE #result
DROP TABLE #tempSumParent

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]    Script Date: 2/16/2024 2:56:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]	
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@IdChungTu nvarchar(4000),
	@Donvitinh int,
	@DotNhan int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
inner join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103) <= convert(varchar,@SNgayQuyetDinh,101)
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and ct.ID in (select * from f_split(@IdChungTu))
and ct.iLoaiDotNhanPhanBo=@DotNhan

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, 0 fTienTroCapOmDau
	, 0 fTienTroCapThaiSan
	, 0 fTienTroCapTaiNan
	, 0 fTienTroCapHuuTri
	, 0 fTienTroCapPhucVien
	, 0 fTienTroCapXuatNgu
	, 0 fTienTroCapThoiViec
	, 0 fTienTroCapTuTuat
	, 1 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi

select
	null as STT
	, N'Khối dự toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 1 RowNumber
	into #temp2
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND(
ctct.sXauNoiMa like '9010001-010-011-0002%'
OR ctct.sXauNoiMa like '9010001-010-011-0001%'
OR ctct.sXauNoiMa like '9010001-010-011-0003%'
OR ctct.sXauNoiMa like '9010001-010-011-0004%'
OR ctct.sXauNoiMa like '9010001-010-011-0005%'
OR ctct.sXauNoiMa like '9010001-010-011-0006%'
OR ctct.sXauNoiMa like '9010001-010-011-0007%'
OR ctct.sXauNoiMa like '9010001-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi 

select
	null as STT
	, N'Khối hạch toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 2 RowNumber
	into #temp3
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND (

ctct.sXauNoiMa like '9010002-010-011-0001%'
OR ctct.sXauNoiMa like '9010002-010-011-0002%'
OR ctct.sXauNoiMa like '9010002-010-011-0003%'
OR ctct.sXauNoiMa like '9010002-010-011-0004%'
OR ctct.sXauNoiMa like '9010002-010-011-0005%'
OR ctct.sXauNoiMa like '9010002-010-011-0006%'
OR ctct.sXauNoiMa like '9010002-010-011-0007%'
OR ctct.sXauNoiMa like '9010002-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi

SELECT  NEWID() as Id,* INTO #tempRESULT
from
(
	SELECT * FROM #temp1
	UNION ALL 
	SELECT * FROM #temp2
	UNION ALL 
	SELECT * FROM #temp3
) tempRESULT;


With #tree(id,sTenDonVi,iID_MaDonVi, STT, IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat , RowNumber,position, iIdParent)
as
(
	SELECT  id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat,RowNumber,
						CAST(ROW_NUMBER() OVER(ORDER BY STT) AS NVARCHAR(MAX)) AS position, NEWID()
	FROM #tempRESULT
	WHERE  IsHangCha = 1 
	UNION ALL 
	SELECT chil.Id, chil.sTenDonVi, chil.iID_MaDonVi,   chil.STT, chil.IsHangCha,chil.fTienTroCapOmDau,chil.fTienTroCapThaiSan,chil.fTienTroCapTaiNan,chil.fTienTroCapHuuTri,chil.fTienTroCapPhucVien,chil.fTienTroCapXuatNgu, chil.fTienTroCapThoiViec, chil.fTienTroCapTuTuat,chil.RowNumber,
	CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.iID_MaDonVi) AS NVARCHAR(MAX))) AS position, pr.id
	FROM #tempRESULT chil
	INNER JOIN #tree pr ON pr.iID_MaDonVi = chil.iID_MaDonVi AND  pr.IsHangCha = 1
	WHERE chil.STT IS NULL AND chil.IsHangCha = 0 

	
)
select *, 
		cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
		INTO #result 
from #tree 
--temp table sum chill
SELECT  iIdParent
		,SUM(fTienTroCapOmDau) fTienTroCapOmDau
		,SUM(fTienTroCapThaiSan) fTienTroCapThaiSan
		,SUM(fTienTroCapTaiNan) fTienTroCapTaiNan
		,SUM(fTienTroCapHuuTri) fTienTroCapHuuTri
		,SUM(fTienTroCapPhucVien) fTienTroCapPhucVien
		,SUM(fTienTroCapXuatNgu) fTienTroCapXuatNgu
		,SUM(fTienTroCapThoiViec) fTienTroCapThoiViec
		,SUM(fTienTroCapTuTuat) fTienTroCapTuTuat
		INTO #tempSumParent
FROM #result
WHERE IsHangCha = 0
Group by iIdParent

Update #result
SET iIdParent = null
where IsHangCha = 1;

select id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapOmDau ELSE rs.fTienTroCapOmDau END) fTienTroCapOmDau,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThaiSan ELSE rs.fTienTroCapThaiSan END) fTienTroCapThaiSan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTaiNan ELSE rs.fTienTroCapTaiNan END) fTienTroCapTaiNan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapHuuTri ELSE rs.fTienTroCapHuuTri END) fTienTroCapHuuTri,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapPhucVien ELSE rs.fTienTroCapPhucVien END) fTienTroCapPhucVien,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapXuatNgu ELSE rs.fTienTroCapXuatNgu END) fTienTroCapXuatNgu,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThoiViec ELSE rs.fTienTroCapThoiViec END) fTienTroCapThoiViec,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTuTuat ELSE rs.fTienTroCapTuTuat END) fTienTroCapTuTuat,
			RowNumber,
			rs.position
			into #resultAllKhoi
from #result rs
LEFT JOIN #tempSumParent su ON rs.id = su.iIdParent
order by sort;


	--Mục A: Khối dự toán
	Select 
	newid() id,
	N'Đơn vị dự toán' sTenDonVi,
	'' iID_MaDonVi,
	N'A' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	 into #tempDonViDuToan

	--Mục B: Khối hạch toán
	Select 
	newid() id,
	N'Đơn vị hạch toán' sTenDonVi,
	'' iID_MaDonVi,
	N'B' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempDonViHachToan

	---Khoi du toán
	Select 
	newid() id,
	N'Khối dự toán' sTenDonVi,
	'' iID_MaDonVi,
	'' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempKhoiDuToan

	---Khoi hach toan
	Select 
	newid() id,
	N'Khối hạch toán' sTenDonVi,
	'' iID_MaDonVi,
	'' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempKhoiHachToan


--Mục B: Khối hạch toán --> Hiển thị các đơn vị có iKhoi = 1: Khối doanh nghiệp
	SELECT B.* into #tempDvKDN
	from DonVi A
LEFT JOIN #resultAllKhoi B ON A.iID_MaDonVi=B.iID_MaDonVi
where A.iNamLamViec=@INamLamViec
 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
 and A.iKhoi=1
 order by B.position


 -- Create bang tam stt cho Mục B: Khối hạch toán
 SELECT CAST(ROW_NUMBER() OVER (ORDER BY #tempDvKDN.sTenDonVi) AS VARCHAR(6))  STT,
 #tempDvKDN.sTenDonVi,
 #tempDvKDN.iID_MaDonVi
 INTO #tempSTTKDN
 FROM #tempDvKDN
 WHERE #tempDvKDN.IsHangCha=1


 --- update stt cho Mục B: Khối hạch toán
 UPDATE #tempDvKDN SET STT= A.STT
 FROM #tempSTTKDN A
 WHERE #tempDvKDN.sTenDonVi=A.sTenDonVi
	 and  #tempDvKDN.iID_MaDonVi=A.iID_MaDonVi

 --Mục A: Khối dự toán --> hiển thị các đơn vị có iKhoi = 2: Khối dự toán
	select B.* 
	into #tempDvKDT 
	from DonVi A
 LEFT JOIN #resultAllKhoi B ON A.iID_MaDonVi=B.iID_MaDonVi
where A.iNamLamViec=@INamLamViec
 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
 and A.iKhoi=2
 order by B.position

  -- Create bang tam stt cho Mục A: Khối dự toán
	SELECT CAST(ROW_NUMBER() OVER (ORDER BY #tempDvKDT.sTenDonVi) AS VARCHAR(6))  STT,
	#tempDvKDT.sTenDonVi,
	#tempDvKDT.iID_MaDonVi
	INTO #tempSTTKDT
	FROM #tempDvKDT
	WHERE #tempDvKDT.IsHangCha=1

 --- update stt cho Mục A: Khối dự toán
	UPDATE #tempDvKDT SET STT= A.STT
 FROM #tempSTTKDT A
 WHERE #tempDvKDT.sTenDonVi=A.sTenDonVi
	 and  #tempDvKDT.iID_MaDonVi=A.iID_MaDonVi


 --- Create data khoi du toan doanh nghiep ( Mục B: Khối hạch toán )
	SELECT 2 iLoai,* INTO #tempKhoiDN
from
(
	SELECT * FROM #tempDonViHachToan
	UNION ALL 
	SELECT * FROM #tempKhoiDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiHachToan
	UNION ALL 
	SELECT iD,
	sTenDonVi,
	iID_MaDonVi,
	convert(nvarchar(6), STT),
	IsHangCha,
	fTienTroCapOmDau,
	fTienTroCapThaiSan,
	fTienTroCapTaiNan,
	fTienTroCapHuuTri,
	fTienTroCapPhucVien,
	fTienTroCapXuatNgu,
	fTienTroCapThoiViec,
	fTienTroCapTuTuat, 
	RowNumber,
	position FROM #tempDvKDN
	
) tempRESULTKhoiDonViHoachToan order by  tempRESULTKhoiDonViHoachToan.position

 --- Create data khoi du toan  (Mục A: Khối dự toán)
	SELECT 1 iLoai, * INTO #tempKhoiDT
from
(
	SELECT * FROM #tempDonViDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiHachToan
	UNION ALL 
	SELECT iD,
	sTenDonVi,
	iID_MaDonVi,
	convert(nvarchar(6), STT),
	IsHangCha,
	fTienTroCapOmDau,
	fTienTroCapThaiSan,
	fTienTroCapTaiNan,
	fTienTroCapHuuTri,
	fTienTroCapPhucVien,
	fTienTroCapXuatNgu,
	fTienTroCapThoiViec,
	fTienTroCapTuTuat, 
	RowNumber,
	position FROM #tempDvKDT

)  tempKhoiDonViDuToan 	order by  tempKhoiDonViDuToan.position



--- Mục B: Khối hạch toán
--- Mục B: Sum khoi du toan cua khoi hach toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiDuToanOfKhoiDN 
		from  #tempKhoiDN
where sTenDonVi = N'Khối dự toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Mục B: Sum khoi du toan cua khoi hach toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiHachToanOfKhoiDN 
		from  #tempKhoiDN
where sTenDonVi = N'Khối hạch toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

	--- Mục B: Sum Don vi theo khoi hach toan(khoi doanh nghiep) 
	select 
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalForDonViKhoiDN 
		from  #tempKhoiDN
where
	iID_MaDonVi <>''
	and iID_MaDonVi is not null
	and STT is not null
	and IsHangCha <> 0

--- update Total Mục B: Đon vi khoi doanh nghiep
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalForDonViKhoiDN A
	where #tempKhoiDN.STT=N'B'
		and #tempKhoiDN.sTenDonVi=N'Đơn vị hạch toán'	
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is  null)


--- update Total Mục B: khoi du toan
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiDuToanOfKhoiDN A
	where #tempKhoiDN.sTenDonVi=N'Khối dự toán'	
		and #tempKhoiDN.STT=N''
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is null)

--- update Total Mục B:khoi hoach toan
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiHachToanOfKhoiDN A
	where #tempKhoiDN.sTenDonVi=N'Khối hạch toán'
		and #tempKhoiDN.STT=N''
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is null)


--- Mục A: Khối dự toán
--- Sum khoi du toan cua khoi du toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiDuToanOfKhoiDT
		from  #tempKhoiDT
where sTenDonVi = N'Khối dự toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Sum khoi hach toan cua Khoi du toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiHachToanOfKhoiDT 
		from  #tempKhoiDT
where sTenDonVi = N'Khối hạch toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Sum Don vi theo khoi du toan(khoi du toan) 
	select 
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalForDonViKhoiDT 
		from  #tempKhoiDT
where
	iID_MaDonVi <>''
	and iID_MaDonVi is not null
	and STT is not null
	and IsHangCha <> 0


--- update Total Mục B: Don vị khối dự toán
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalForDonViKhoiDT A
	where #tempKhoiDT.STT=N'A'
		and #tempKhoiDT.sTenDonVi=N'Đơn vị dự toán'	
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is  null)

--- update Total Mục B: khoi du toan
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiDuToanOfKhoiDT A
	where #tempKhoiDT.sTenDonVi=N'Khối dự toán'	
		and #tempKhoiDT.STT=N''
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is null)

--- update Total Mục B:khoi hoach toan
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiHachToanOfKhoiDT A
	where #tempKhoiDT.sTenDonVi=N'Khối hạch toán'
		and #tempKhoiDT.STT=N''
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is null)

SELECT  * 
from
(
	SELECT * FROM #tempKhoiDT
	UNION ALL 
	SELECT * FROM #tempKhoiDN
	
) tempRESULTALL order by iLoai, position;


DROP TABLE #tempDonViDuToan
DROP TABLE #tempDonViHachToan
DROP TABLE #tempKhoiDuToan
DROP TABLE #tempKhoiHachToan
DROP TABLE #tempDvKDN
DROP TABLE #tempDvKDT
DROP TABLE #tempSTTKDN
DROP TABLE #tempSTTKDT
DROP TABLE #SumTotalKhoiDuToanOfKhoiDN
DROP TABLE #SumTotalKhoiHachToanOfKhoiDN
DROP TABLE #SumTotalForDonViKhoiDN
DROP TABLE #SumTotalKhoiDuToanOfKhoiDT
DROP TABLE #SumTotalKhoiHachToanOfKhoiDT
DROP TABLE #SumTotalForDonViKhoiDT
DROP TABLE #tempKhoiDN
DROP TABLE #tempKhoiDT
DROP TABLE #resultAllKhoi
DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #tempRESULT
DROP TABLE #result
DROP TABLE #tempSumParent

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]    Script Date: 2/16/2024 2:56:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]	
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@IdChungTu nvarchar(4000),
	@Donvitinh int
	--@DotNhan int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
inner join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103) <= convert(varchar,@SNgayQuyetDinh,101)
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and ct.ID in (select * from f_split(@IdChungTu))
--and ct.iLoaiDotNhanPhanBo=@DotNhan

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, 0 fTienTroCapOmDau
	, 0 fTienTroCapThaiSan
	, 0 fTienTroCapTaiNan
	, 0 fTienTroCapHuuTri
	, 0 fTienTroCapPhucVien
	, 0 fTienTroCapXuatNgu
	, 0 fTienTroCapThoiViec
	, 0 fTienTroCapTuTuat
	, 1 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi

select
	null as STT
	, N'Khối dự toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 1 RowNumber
	into #temp2
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND(
ctct.sXauNoiMa like '9010001-010-011-0002%'
OR ctct.sXauNoiMa like '9010001-010-011-0001%'
OR ctct.sXauNoiMa like '9010001-010-011-0003%'
OR ctct.sXauNoiMa like '9010001-010-011-0004%'
OR ctct.sXauNoiMa like '9010001-010-011-0005%'
OR ctct.sXauNoiMa like '9010001-010-011-0006%'
OR ctct.sXauNoiMa like '9010001-010-011-0007%'
OR ctct.sXauNoiMa like '9010001-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi 

select
	null as STT
	, N'Khối hạch toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 2 RowNumber
	into #temp3
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND (

ctct.sXauNoiMa like '9010002-010-011-0001%'
OR ctct.sXauNoiMa like '9010002-010-011-0002%'
OR ctct.sXauNoiMa like '9010002-010-011-0003%'
OR ctct.sXauNoiMa like '9010002-010-011-0004%'
OR ctct.sXauNoiMa like '9010002-010-011-0005%'
OR ctct.sXauNoiMa like '9010002-010-011-0006%'
OR ctct.sXauNoiMa like '9010002-010-011-0007%'
OR ctct.sXauNoiMa like '9010002-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi

SELECT  NEWID() as Id,* INTO #tempRESULT
from
(
	SELECT * FROM #temp1
	UNION ALL 
	SELECT * FROM #temp2
	UNION ALL 
	SELECT * FROM #temp3
) tempRESULT;


With #tree(id,sTenDonVi,iID_MaDonVi, STT, IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat , RowNumber,position, iIdParent)
as
(
	SELECT  id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat,RowNumber,
						CAST(ROW_NUMBER() OVER(ORDER BY STT) AS NVARCHAR(MAX)) AS position, NEWID()
	FROM #tempRESULT
	WHERE  IsHangCha = 1 
	UNION ALL 
	SELECT chil.Id, chil.sTenDonVi, chil.iID_MaDonVi,   chil.STT, chil.IsHangCha,chil.fTienTroCapOmDau,chil.fTienTroCapThaiSan,chil.fTienTroCapTaiNan,chil.fTienTroCapHuuTri,chil.fTienTroCapPhucVien,chil.fTienTroCapXuatNgu, chil.fTienTroCapThoiViec, chil.fTienTroCapTuTuat,chil.RowNumber,
	CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.iID_MaDonVi) AS NVARCHAR(MAX))) AS position, pr.id
	FROM #tempRESULT chil
	INNER JOIN #tree pr ON pr.iID_MaDonVi = chil.iID_MaDonVi AND  pr.IsHangCha = 1
	WHERE chil.STT IS NULL AND chil.IsHangCha = 0 

	
)
select *, 
		cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
		INTO #result 
from #tree 
--temp table sum chill
SELECT  iIdParent
		,SUM(fTienTroCapOmDau) fTienTroCapOmDau
		,SUM(fTienTroCapThaiSan) fTienTroCapThaiSan
		,SUM(fTienTroCapTaiNan) fTienTroCapTaiNan
		,SUM(fTienTroCapHuuTri) fTienTroCapHuuTri
		,SUM(fTienTroCapPhucVien) fTienTroCapPhucVien
		,SUM(fTienTroCapXuatNgu) fTienTroCapXuatNgu
		,SUM(fTienTroCapThoiViec) fTienTroCapThoiViec
		,SUM(fTienTroCapTuTuat) fTienTroCapTuTuat
		INTO #tempSumParent
FROM #result
WHERE IsHangCha = 0
Group by iIdParent

Update #result
SET iIdParent = null
where IsHangCha = 1;

select id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapOmDau ELSE rs.fTienTroCapOmDau END) fTienTroCapOmDau,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThaiSan ELSE rs.fTienTroCapThaiSan END) fTienTroCapThaiSan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTaiNan ELSE rs.fTienTroCapTaiNan END) fTienTroCapTaiNan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapHuuTri ELSE rs.fTienTroCapHuuTri END) fTienTroCapHuuTri,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapPhucVien ELSE rs.fTienTroCapPhucVien END) fTienTroCapPhucVien,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapXuatNgu ELSE rs.fTienTroCapXuatNgu END) fTienTroCapXuatNgu,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThoiViec ELSE rs.fTienTroCapThoiViec END) fTienTroCapThoiViec,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTuTuat ELSE rs.fTienTroCapTuTuat END) fTienTroCapTuTuat,
			RowNumber,
			rs.position
			into #resultAllKhoi
from #result rs
LEFT JOIN #tempSumParent su ON rs.id = su.iIdParent
order by sort;


	--Mục A: Khối dự toán
	Select 
	newid() id,
	N'Đơn vị dự toán' sTenDonVi,
	'' iID_MaDonVi,
	N'A' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	 into #tempDonViDuToan

	--Mục B: Khối hạch toán
	Select 
	newid() id,
	N'Đơn vị hạch toán' sTenDonVi,
	'' iID_MaDonVi,
	N'B' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempDonViHachToan

	---Khoi du toán
	Select 
	newid() id,
	N'Khối dự toán' sTenDonVi,
	'' iID_MaDonVi,
	'' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempKhoiDuToan

	---Khoi hach toan
	Select 
	newid() id,
	N'Khối hạch toán' sTenDonVi,
	'' iID_MaDonVi,
	'' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempKhoiHachToan


--Mục B: Khối hạch toán --> Hiển thị các đơn vị có iKhoi = 1: Khối doanh nghiệp
	SELECT B.* into #tempDvKDN
	from DonVi A
LEFT JOIN #resultAllKhoi B ON A.iID_MaDonVi=B.iID_MaDonVi
where A.iNamLamViec=@INamLamViec
 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
 and A.iKhoi=1
 order by B.position


 -- Create bang tam stt cho Mục B: Khối hạch toán
 SELECT CAST(ROW_NUMBER() OVER (ORDER BY #tempDvKDN.sTenDonVi) AS VARCHAR(6))  STT,
 #tempDvKDN.sTenDonVi,
 #tempDvKDN.iID_MaDonVi
 INTO #tempSTTKDN
 FROM #tempDvKDN
 WHERE #tempDvKDN.IsHangCha=1


 --- update stt cho Mục B: Khối hạch toán
 UPDATE #tempDvKDN SET STT= A.STT
 FROM #tempSTTKDN A
 WHERE #tempDvKDN.sTenDonVi=A.sTenDonVi
	 and  #tempDvKDN.iID_MaDonVi=A.iID_MaDonVi

 --Mục A: Khối dự toán --> hiển thị các đơn vị có iKhoi = 2: Khối dự toán
	select B.* 
	into #tempDvKDT 
	from DonVi A
 LEFT JOIN #resultAllKhoi B ON A.iID_MaDonVi=B.iID_MaDonVi
where A.iNamLamViec=@INamLamViec
 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
 and A.iKhoi=2
 order by B.position

  -- Create bang tam stt cho Mục A: Khối dự toán
	SELECT CAST(ROW_NUMBER() OVER (ORDER BY #tempDvKDT.sTenDonVi) AS VARCHAR(6))  STT,
	#tempDvKDT.sTenDonVi,
	#tempDvKDT.iID_MaDonVi
	INTO #tempSTTKDT
	FROM #tempDvKDT
	WHERE #tempDvKDT.IsHangCha=1

 --- update stt cho Mục A: Khối dự toán
	UPDATE #tempDvKDT SET STT= A.STT
 FROM #tempSTTKDT A
 WHERE #tempDvKDT.sTenDonVi=A.sTenDonVi
	 and  #tempDvKDT.iID_MaDonVi=A.iID_MaDonVi


 --- Create data khoi du toan doanh nghiep ( Mục B: Khối hạch toán )
	SELECT 2 iLoai,* INTO #tempKhoiDN
from
(
	SELECT * FROM #tempDonViHachToan
	UNION ALL 
	SELECT * FROM #tempKhoiDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiHachToan
	UNION ALL 
	SELECT iD,
	sTenDonVi,
	iID_MaDonVi,
	convert(nvarchar(6), STT),
	IsHangCha,
	fTienTroCapOmDau,
	fTienTroCapThaiSan,
	fTienTroCapTaiNan,
	fTienTroCapHuuTri,
	fTienTroCapPhucVien,
	fTienTroCapXuatNgu,
	fTienTroCapThoiViec,
	fTienTroCapTuTuat, 
	RowNumber,
	position FROM #tempDvKDN
	
) tempRESULTKhoiDonViHoachToan order by  tempRESULTKhoiDonViHoachToan.position

 --- Create data khoi du toan  (Mục A: Khối dự toán)
	SELECT 1 iLoai, * INTO #tempKhoiDT
from
(
	SELECT * FROM #tempDonViDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiHachToan
	UNION ALL 
	SELECT iD,
	sTenDonVi,
	iID_MaDonVi,
	convert(nvarchar(6), STT),
	IsHangCha,
	fTienTroCapOmDau,
	fTienTroCapThaiSan,
	fTienTroCapTaiNan,
	fTienTroCapHuuTri,
	fTienTroCapPhucVien,
	fTienTroCapXuatNgu,
	fTienTroCapThoiViec,
	fTienTroCapTuTuat, 
	RowNumber,
	position FROM #tempDvKDT

)  tempKhoiDonViDuToan 	order by  tempKhoiDonViDuToan.position



--- Mục B: Khối hạch toán
--- Mục B: Sum khoi du toan cua khoi hach toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiDuToanOfKhoiDN 
		from  #tempKhoiDN
where sTenDonVi = N'Khối dự toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Mục B: Sum khoi du toan cua khoi hach toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiHachToanOfKhoiDN 
		from  #tempKhoiDN
where sTenDonVi = N'Khối hạch toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

	--- Mục B: Sum Don vi theo khoi hach toan(khoi doanh nghiep) 
	select 
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalForDonViKhoiDN 
		from  #tempKhoiDN
where
	iID_MaDonVi <>''
	and iID_MaDonVi is not null
	and STT is not null
	and IsHangCha <> 0

--- update Total Mục B: Đon vi khoi doanh nghiep
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalForDonViKhoiDN A
	where #tempKhoiDN.STT=N'B'
		and #tempKhoiDN.sTenDonVi=N'Đơn vị hạch toán'	
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is  null)


--- update Total Mục B: khoi du toan
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiDuToanOfKhoiDN A
	where #tempKhoiDN.sTenDonVi=N'Khối dự toán'	
		and #tempKhoiDN.STT=N''
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is null)

--- update Total Mục B:khoi hoach toan
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiHachToanOfKhoiDN A
	where #tempKhoiDN.sTenDonVi=N'Khối hạch toán'
		and #tempKhoiDN.STT=N''
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is null)


--- Mục A: Khối dự toán
--- Sum khoi du toan cua khoi du toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiDuToanOfKhoiDT
		from  #tempKhoiDT
where sTenDonVi = N'Khối dự toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Sum khoi hach toan cua Khoi du toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiHachToanOfKhoiDT 
		from  #tempKhoiDT
where sTenDonVi = N'Khối hạch toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Sum Don vi theo khoi du toan(khoi du toan) 
	select 
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalForDonViKhoiDT 
		from  #tempKhoiDT
where
	iID_MaDonVi <>''
	and iID_MaDonVi is not null
	and STT is not null
	and IsHangCha <> 0


--- update Total Mục B: Don vị khối dự toán
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalForDonViKhoiDT A
	where #tempKhoiDT.STT=N'A'
		and #tempKhoiDT.sTenDonVi=N'Đơn vị dự toán'	
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is  null)

--- update Total Mục B: khoi du toan
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiDuToanOfKhoiDT A
	where #tempKhoiDT.sTenDonVi=N'Khối dự toán'	
		and #tempKhoiDT.STT=N''
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is null)

--- update Total Mục B:khoi hoach toan
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiHachToanOfKhoiDT A
	where #tempKhoiDT.sTenDonVi=N'Khối hạch toán'
		and #tempKhoiDT.STT=N''
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is null)

SELECT  * 
from
(
	SELECT * FROM #tempKhoiDT
	UNION ALL 
	SELECT * FROM #tempKhoiDN
	
) tempRESULTALL order by iLoai, position;


DROP TABLE #tempDonViDuToan
DROP TABLE #tempDonViHachToan
DROP TABLE #tempKhoiDuToan
DROP TABLE #tempKhoiHachToan
DROP TABLE #tempDvKDN
DROP TABLE #tempDvKDT
DROP TABLE #tempSTTKDN
DROP TABLE #tempSTTKDT
DROP TABLE #SumTotalKhoiDuToanOfKhoiDN
DROP TABLE #SumTotalKhoiHachToanOfKhoiDN
DROP TABLE #SumTotalForDonViKhoiDN
DROP TABLE #SumTotalKhoiDuToanOfKhoiDT
DROP TABLE #SumTotalKhoiHachToanOfKhoiDT
DROP TABLE #SumTotalForDonViKhoiDT
DROP TABLE #tempKhoiDN
DROP TABLE #tempKhoiDT
DROP TABLE #resultAllKhoi
DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #tempRESULT
DROP TABLE #result
DROP TABLE #tempSumParent

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]    Script Date: 2/16/2024 2:56:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@IdChungTu nvarchar(Max),
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int
	--@DotNhan int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
and ct.ID in (select * from f_split(@IdChungTu))
--and ct.iLoaiDotNhanPhanBo=@DotNhan
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(  @IdMaDonVi));

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, SUM(ctct.fTongTien) as fTongTienDuToan
	, 0 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi 

select * from #temp1;

DROP TABLE #tempall
DROP TABLE #temp1
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]    Script Date: 2/16/2024 2:56:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@IdChungTu nvarchar(Max),
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int,
	@DotNhan int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
and ct.ID in (select * from f_split(@IdChungTu))
and ct.iLoaiDotNhanPhanBo=@DotNhan
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(  @IdMaDonVi));

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, SUM(ctct.fTongTien) as fTongTienDuToan
	, 0 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi 

select * from #temp1;

DROP TABLE #tempall
DROP TABLE #temp1
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]    Script Date: 2/16/2024 2:56:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@IdChungTu nvarchar(Max),
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int,
	@DotNhan int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
and ct.iLoaiDotNhanPhanBo=@DotNhan
and ct.ID in (select * from f_split(@IdChungTu))
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(  @IdMaDonVi));

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, SUM(ctct.fTongTien) as fTongTienDuToan
	, 0 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
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
 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
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
	 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
	 and A.iKhoi=1

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
	SELECT * 
	FROM
	(
		SELECT * FROM #tempDataDVDT
		UNION ALL 
		SELECT * FROM #tempDataDVHT
	)tempDataAll



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
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]    Script Date: 2/16/2024 2:56:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@IdChungTu nvarchar(Max),
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int,
	@DotNhan int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
and ct.iLoaiDotNhanPhanBo=@DotNhan
and ct.ID in (select * from f_split(@IdChungTu))
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(  @IdMaDonVi));

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, SUM(ctct.fTongTien) as fTongTienDuToan
	, 0 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
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
 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
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
	 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
	 and A.iKhoi=1

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
	SELECT * 
	FROM
	(
		SELECT * FROM #tempDataDVDT
		UNION ALL 
		SELECT * FROM #tempDataDVHT
	)tempDataAll



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
END
;
;
;
;
;
;
GO
