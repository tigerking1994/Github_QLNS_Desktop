/****** Object:  StoredProcedure [dbo].[sp_skt_plan_begin_year_2]    Script Date: 12/13/2024 4:11:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_plan_begin_year_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_plan_begin_year_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qt_congkhai_thuchi_donvi]    Script Date: 12/13/2024 4:11:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qt_congkhai_thuchi_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qt_congkhai_thuchi_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qt_congkhai_thuchi]    Script Date: 12/13/2024 4:11:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qt_congkhai_thuchi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qt_congkhai_thuchi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_nhan_quyettoankinhphi_donvi]    Script Date: 12/13/2024 4:11:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_nhan_quyettoankinhphi_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_nhan_quyettoankinhphi_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_thongke_soquyetdinh]    Script Date: 12/13/2024 4:11:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_thongke_soquyetdinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_thongke_soquyetdinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 12/13/2024 4:11:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_all]    Script Date: 12/13/2024 4:11:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chitiet_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chitiet_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chitiet_all]    Script Date: 12/13/2024 4:11:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[sp_cp_chungtu_chitiet_all]
	@VoucherId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@UserName nvarchar(100),
	@PhanCap nvarchar(10),
	@IsCapPhatToanDonVi bit
AS
BEGIN
	-- Khai báo chỉ mục cấp phát
	declare @IndexCP int;
	select @IndexCP = iSoChungTuIndex from NS_CP_ChungTu where iID_CTCapPhat = @VoucherId

	-- Cắt chuỗi LNS và ID đơn vị
	select * into #lns from f_split(@LNS);
	select * into #donvis from f_split(@AgencyId);
	select distinct value into #fullLns 
	from 
	(
		select 
			cast(left(sLNS, 1) AS nvarchar(10)) LNS1, 
			cast(left(sLNS, 3) AS nvarchar(10)) LNS3, 
			cast(left(sLNS, 5) AS nvarchar(10)) LNS5, 
			cast(sLNS AS nvarchar(10)) LNS 
		from
			NS_NguoiDung_LNS 
		where 
			sMaNguoiDung = @UserName
			AND INamLamViec = @YearOfWork
			AND sLNS IN (SELECT * FROM #lns)
	) LNS
	UNPIVOT
	(
		value
		FOR col in (LNS1, LNS3, LNS5, LNS)
	) un;

	-- Mục lục ngân sách đầy đủ theo năm
	select *
	into #muclucs	
	from NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork
	and sLNS in (select value from #fullLns)

	-- Lấy ra đơn vị từ đơn vị của chứng từ
	select iID_MaDonVi, sTenDonVi into #tblDonVi
	from DonVi 
	where INamLamViec = @YearOfWork and iID_MaDonVi in (select * from #donvis)

	-- Map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMlnsDonVi FROM (
		SELECT mucluc.sXauNoiMa, b.Item iID_MaDonVi FROM 
			(SELECT sXauNoiMa FROM #muclucs
			 WHERE bHangCha = 0 OR (bHangCha = 1 AND @PhanCap = 'M' AND ISNULL(sM, '') <> '') 
								OR (bHangCha = 1 AND @PhanCap = 'TM' AND IsNull(sTM, '') <> '')) mucluc
	    INNER JOIN #donvis b ON 1 = 1
	) mlns

	-- Lấy dữ liệu đã cấp
	SELECT SUM(ISNULL(fTuChi, 0)) AS DaCap,
		   0 AS DuToan,
           iID_MaDonVi,
           sXauNoiMa,
		   sLNS
	INTO #tblDataDaCap
	FROM NS_CP_ChungTuChiTiet
	WHERE iID_CTCapPhat IN
       (
		SELECT iID_CTCapPhat
        FROM NS_CP_ChungTu
        WHERE iNamLamViec = @YearOfWork
		  AND iID_MaNguonNganSach = @BudgetSource
		  AND iNamNganSach = @YearOfBudget
          AND iID_CTCapPhat <> @VoucherId
		  AND bKhoa = 1
		  AND iSoChungTuIndex < @IndexCP
          AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND EXISTS(SELECT * FROM #donvis INTERSECT SELECT * FROM f_split(sDSID_MaDonVi))
		)
		AND iID_MaDonVi IN (SELECT * FROM #donvis)
	GROUP BY iID_MaDonVi, sXauNoiMa, sLNS;

	-- Lấy ra dữ liệu dự toán
	SELECT SUM(ISNULL(fTuChi, 0)) AS TuChi,
           SUM(ISNULL(fHangNhap, 0)) AS HangNhap,
           SUM(ISNULL(fHangMua, 0)) AS HangMua,
           iID_MaDonVi,
           sXauNoiMa,
		   sLNS
		   INTO #tblPhanBoDuToan
	FROM NS_DT_ChungTuChiTiet
	WHERE iID_DTChungTu IN
       (SELECT iID_DTChungTu
        FROM NS_DT_ChungTu
        WHERE ISNULL(sSoQuyetDinh, '') <> ''
          AND iLoai = 1 
          AND iNamLamViec = @YearOfWork
          AND ((@YearOfBudget = 2 AND (iNamNganSach = 2 OR iNamNganSach = 4))
            OR (@YearOfBudget <> 2 AND iNamNganSach = @YearOfBudget))
          AND iID_MaNguonNganSach = @BudgetSource
          AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@VoucherDate AS DATE))
    AND iID_MaDonVi IN (SELECT * FROM #donvis)
	GROUP BY iID_MaDonVi, sXauNoiMa, sLNS

   SELECT * into #tblDataDuToan FROM (
   SELECT 0 AS DaCap,
		ISNULL(CASE
			WHEN mlns.sLNS = '1040200' THEN (dtctct.TuChi + dtctct.HangNhap)
			WHEN mlns.sLNS = '1040300' THEN (dtctct.TuChi + dtctct.HangMua)
			ELSE dtctct.TuChi
		END, 0)  AS DuToan,
		isnull(dtctct.iID_MaDonVi, @AgencyId) AS iID_MaDonVi,
		mlns.sXauNoiMa,
		mlns.sLNS
	FROM (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @YearOfWork) mlns
	INNER JOIN (SELECT * FROM #tblPhanBoDuToan WHERE sLNS <> '1040100') dtctct
	ON mlns.sXauNoiMa = dtctct.sXauNoiMa
	UNION ALL
	SELECT 0 AS DaCap,
		ISNULL(CASE
			WHEN mlns.sLNS = '1040200' THEN (dtctct.TuChi + dtctct.HangNhap)
			WHEN mlns.sLNS = '1040300' THEN (dtctct.TuChi + dtctct.HangMua)
			ELSE dtctct.TuChi
		END, 0)  AS DuToan,
		isnull(dtctct.iID_MaDonVi, @AgencyId) AS iID_MaDonVi,
		mlns.sXauNoiMa,
		mlns.sLNS
	FROM (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @YearOfWork) mlns
	INNER JOIN (SELECT * FROM #tblPhanBoDuToan WHERE sLNS = '1040100') dtctct
	ON  REPLACE(dtctct.sXauNoiMa, '1040100', '1040200') = mlns.sXauNoiMa
	UNION ALL
	SELECT 0 AS DaCap,
		ISNULL(CASE
			WHEN mlns.sLNS = '1040200' THEN (dtctct.TuChi + dtctct.HangNhap)
			WHEN mlns.sLNS = '1040300' THEN (dtctct.TuChi + dtctct.HangMua)
			ELSE dtctct.TuChi
		END, 0)  AS DuToan,
		isnull(dtctct.iID_MaDonVi, @AgencyId) AS iID_MaDonVi,
		mlns.sXauNoiMa,
		mlns.sLNS
	FROM (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @YearOfWork) mlns
	INNER JOIN(SELECT * FROM #tblPhanBoDuToan WHERE sLNS = '1040100') dtctct
	on REPLACE(dtctct.sXauNoiMa, '1040100', '1040300') = mlns.sXauNoiMa
	) dt


	SELECT sum(isnull(DaCap, 0)) AS DaCap, sum(isnull(DuToan, 0)) AS DuToan, iID_MaDonVi, sXauNoiMa, sLNS into #tblDataDaCapDuToan FROM (
		SELECT * FROM #tblDataDaCap
		UNION ALL
		SELECT * FROM #tblDataDuToan
		) data
	GROUP BY iID_MaDonVi, sXauNoiMa, sLNS

	SELECT
		isnull(ctct.iID_CTCapPhatChiTiet, NEWID()) AS Id,
		ctct.iID_CTCapPhat AS IdChungTu,
		mlnsPhanBo.iID_MLNS AS MlnsId,
		mlnsPhanBo.iID_MLNS_Cha AS MlnsIdParent,
		mlnsPhanBo.sXauNoiMa AS XauNoiMa,
		mlnsPhanBo.sLNS AS LNS,
		mlnsPhanBo.sL AS L,
		mlnsPhanBo.sK AS K,
		mlnsPhanBo.sM AS M,
		mlnsPhanBo.sTM AS TM,
		mlnsPhanBo.sTTM AS TTM,
		mlnsPhanBo.sNG AS NG,
		mlnsPhanBo.sTNG AS TNG,
		mlnsPhanBo.sTNG1 AS TNG1,
		mlnsPhanBo.sTNG2 AS TNG2,
		mlnsPhanBo.sTNG3 AS TNG3,
		mlnsPhanBo.sMoTa AS MoTa,
		'' Chuong, 
		ct.sSoChungTu AS SoChungTu,
		mlnsPhanBo.bHangCha,
		@YearOfWork AS NamLamViec,
		@YearOfBudget AS NamNganSach,
		@BudgetSource AS NguonNganSach,
		ctct.iLoai,
		dv.iID_MaDonVi AS IdDonVi,
		dv.sTenDonVi AS TenDonVi,
		isnull(ctct.fTuChi, 0) AS TuChi,
		isnuLL(ctct.fDeNghiDonVi, 0) AS DeNghiDonVi,
		isnull(ctct.fHienVat, 0) AS HienVat,
		isnull(daCapDuToan.DaCap, 0) as DaCap,
		isnull(daCapDuToan.DuToan, 0) as DuToan,
		ctct.sGhiChu AS GhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DateCreated,
		isnull(ctct.dNgaySua, getdate()) AS DateModified,
		ctct.sNguoiTao AS UserCreator,
		ctct.sNguoiSua AS UserModifier
	FROM #muclucs mlnsPhanBo
	LEFT JOIN #tblMlnsDonVi AS mlnsPhanBo1 on mlnsPhanBo1.sXauNoiMa = mlnsPhanBo.sXauNoiMa
	LEFT JOIN
		(SELECT *
			FROM 
				NS_CP_ChungTuChiTiet
			WHERE 
		 		iID_CTCapPhat = @VoucherId
		 		AND INamLamViec = @YearOfWork
		 		AND iID_MaNguonNganSach = @BudgetSource
				AND iNamNganSach = @YearOfBudget
		) ctct
	ON mlnsPhanBo1.sXauNoiMa = ctct.sXauNoiMa and mlnsPhanBo1.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN NS_CP_ChungTu ct ON ctct.iID_CTCapPhat = ct.iID_CTCapPhat
	LEFT JOIN #tblDataDaCapDuToan daCapDuToan ON mlnsPhanBo1.sXauNoiMa = daCapDuToan.sXauNoiMa 
	and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo1.iID_MaDonVi + '%' 
	LEFT JOIN #tblDonVi dv ON dv.iID_MaDonVi = mlnsPhanBo1.iID_MaDonVi
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 12/13/2024 4:11:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
	@VoucherId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@AgencyId NVARCHAR(MAX),
	@YearOfWork INT,
	@BudgetOfYear INT,
	@SourceOfBudget INT,
	@UserName NVARCHAR(100),
	@IsGetAll BIT
AS
BEGIN

declare @CountRoot int;
declare @NgayQuyetDinh datetime;
declare @NgayChungTu datetime;
declare @SoChungTuIndex int;

select 
@NgayQuyetDinh = cast(dNgayQuyetdinh as date),
@NgayChungTu = cast(dNgayChungTu as date),
@SoChungTuIndex = iSoChungTuIndex
from NS_DT_ChungTu 
where iID_DTChungTu = @VoucherId;

select @CountRoot = count(*) from DonVi dv
join NguoiDung_DonVi nd 
on nd.iID_MaDonVi = dv.iID_MaDonVi and nd.iNamLamViec = dv.iNamLamViec
where dv.iNamLamViec = @YearOfWork
and iID_MaNguoiDung = @UserName;

with lns1 as 
(select distinct value
from
(select cast(left(Item, 1) as nvarchar(10)) LNS1,
		cast(left(Item, 3) as nvarchar(10)) LNS3,
		cast(left(Item, 5) as nvarchar(10)) LNS5,
		cast(Item as nvarchar(10)) LNS
from f_split(@LNS)) 
LNS unpivot (value for col in (LNS1, LNS3, LNS5, LNS)) un
),
lns2 as 
(select distinct value
from
(select cast(left(sLNS, 1) as nvarchar(10)) LNS1,
		cast(left(sLNS, 3) as nvarchar(10)) LNS3,
		cast(left(sLNS, 5) as nvarchar(10)) LNS5,
		cast(sLNS as nvarchar(10)) LNS
from NS_NguoiDung_LNS
where sMaNguoiDung = @UserName
AND iNamLamViec = @YearOfWork
AND sLNS IN (SELECT * FROM f_split('')))
LNS unpivot (value for col in (LNS1, LNS3, LNS5, LNS)) un
)

select * into #muclucngansachs from NS_MucLucNganSach
where iNamLamViec = @YearOfWork
and iTrangThai = 1
and bHangChaDuToan is not null
and ((@CountRoot <> 0 and sLNS in (select * from lns1)) or (@CountRoot = 0 and sLNS in (select * from lns2)));

with 

settings as
(
select sLNS, bTuChi, bHienVat, bHangNhap, bHangMua, bPhanCap, bDuPhong, bTonKho from #muclucngansachs where isnull(sL, '') = ''
),

iddonvis as
(
select * from f_split(@AgencyId)
),

donvis as 
(
select dv.iID_MaDonVi, dv.sTenDonVi from DonVi dv
join NguoiDung_DonVi nd on nd.iID_MaDonVi = dv.iID_MaDonVi and nd.iNamLamViec = dv.iNamLamViec
where dv.iNamLamViec = @YearOfWork
and dv.iID_MaDonVi in (select * from iddonvis)
),

xaunoimas as
(
select distinct sXauNoiMa from #muclucngansachs where bHangChaDuToan = 0 and @IsGetAll = 1
union all
select distinct sXauNoiMa from NS_DT_ChungTuChiTiet where iID_DTChungTu in (select iID_CTDuToan_Nhan from NS_DT_Nhan_PhanBo_Map where iID_CTDuToan_PhanBo = @VoucherId)
union all 
select distinct sXauNoiMa from NS_DT_ChungTuChiTiet where iID_DTChungTu  = @VoucherId
),

soquyetdinh as
(
select iiD_DTChungTu, sSoQuyetDinh from NS_DT_ChungTu where 
iID_DTChungTu in (select iID_CTDuToan_Nhan from NS_DT_Nhan_PhanBo_Map where iID_CTDuToan_PhanBo = @VoucherId)
),

idnhanphanbo as
(
select iID_CTDuToan_Nhan from NS_DT_Nhan_PhanBo_Map where iID_CTDuToan_PhanBo = @VoucherId
),

mucluc_sqd as (
select * from xaunoimas, idnhanphanbo
),

mucluc_sqd_donvi as (
select * from xaunoimas, idnhanphanbo, iddonvis
),

nhanphanbo as
(
select sXauNoiMa, iID_DTChungTu, 
sum(isnull(fTuChi, 0)) fTuChi,
sum(isnull(fRutKBNN, 0)) fRutKBNN,
sum(isnull(fHienVat, 0)) fHienVat,
sum(isnull(fDuPhong, 0)) fDuPhong,
sum(isnull(fHangMua, 0)) fHangMua,
sum(isnull(fHangNhap, 0)) fHangNhap,
sum(isnull(fPhanCap, 0)) fPhanCap,
sum(isnull(fTonKho, 0)) fTonKho
from NS_DT_ChungTuChiTiet where iID_DTChungTu in (select iID_CTDuToan_Nhan from NS_DT_Nhan_PhanBo_Map where iID_CTDuToan_PhanBo = @VoucherId) 
group by sXauNoiMa, iID_DTChungTu
),

daphanbo as
(
select sXauNoiMa, iID_CTDuToan_Nhan, 
0 - sum(isnull(fTuChi, 0)) fTuChi,
0 - sum(isnull(fRutKBNN, 0)) fRutKBNN,
0 - sum(isnull(fHienVat, 0)) fHienVat,
0 - sum(isnull(fDuPhong, 0)) fDuPhong,
0 - sum(isnull(fHangMua, 0)) fHangMua,
0 - sum(isnull(fHangNhap, 0)) fHangNhap,
0 - sum(isnull(fPhanCap, 0)) fPhanCap,
0 - sum(isnull(fTonKho, 0)) fTonKho 
from NS_DT_ChungTuChiTiet ctct
left join NS_DT_ChungTu ct on ct.iID_DTChungTu = ctct.iID_CTDuToan_Nhan
left join NS_DT_ChungTu dpb on dpb.iID_DTChungTu = ctct.iID_DTChungTu
where iID_CTDuToan_Nhan in (select iID_CTDuToan_Nhan from NS_DT_Nhan_PhanBo_Map where iID_CTDuToan_PhanBo = @VoucherId) 
and 
(
	(
		(
		cast(isnull(dpb.dNgayQuyetDinh, dpb.dNgayChungTu) as date)
		= cast(isnull(@NgayQuyetDinh, @NgayChungTu) as date)
		) and (dpb.iSoChungTuIndex < @SoChungTuIndex)
	) or 
	(
		cast(isnull(dpb.dNgayQuyetDinh, dpb.dNgayChungTu) as date) 
		< cast(isnull(@NgayQuyetDinh, @NgayChungTu) as date)
	)
)
group by sXauNoiMa, iID_CTDuToan_Nhan
),

nhanphanbo_current as
(
select sXauNoiMa, iID_DTChungTu, 
sum(isnull(fTuChi, 0)) fTuChi,
sum(isnull(fRutKBNN, 0)) fRutKBNN,
sum(isnull(fHienVat, 0)) fHienVat,
sum(isnull(fDuPhong, 0)) fDuPhong,
sum(isnull(fHangMua, 0)) fHangMua,
sum(isnull(fHangNhap, 0)) fHangNhap,
sum(isnull(fPhanCap, 0)) fPhanCap,
sum(isnull(fTonKho, 0)) fTonKho
from 
(select * from nhanphanbo
union all
select * from daphanbo) npb_current
group by sXauNoiMa, iID_DTChungTu
),

phanbo as
(
select sXauNoiMa, iID_CTDuToan_Nhan, 
sum(isnull(fTuChi, 0)) fTuChi,
sum(isnull(fRutKBNN, 0)) fRutKBNN,
sum(isnull(fHienVat, 0)) fHienVat,
sum(isnull(fDuPhong, 0)) fDuPhong,
sum(isnull(fHangMua, 0)) fHangMua,
sum(isnull(fHangNhap, 0)) fHangNhap,
sum(isnull(fPhanCap, 0)) fPhanCap,
sum(isnull(fTonKho, 0)) fTonKho
from NS_DT_ChungTuChiTiet where iID_DTChungTu  = @VoucherId
group by sXauNoiMa, iID_CTDuToan_Nhan
),

phanbo_donvi as
(
select iID_DTCTChiTiet, sXauNoiMa, 
iID_CTDuToan_Nhan, iID_MaDonVi, sGhiChu,
sum(isnull(fTuChi, 0)) fTuChi,
sum(isnull(fRutKBNN, 0)) fRutKBNN,
sum(isnull(fHienVat, 0)) fHienVat,
sum(isnull(fDuPhong, 0)) fDuPhong,
sum(isnull(fHangMua, 0)) fHangMua,
sum(isnull(fHangNhap, 0)) fHangNhap,
sum(isnull(fPhanCap, 0)) fPhanCap,
sum(isnull(fTonKho, 0)) fTonKho
from NS_DT_ChungTuChiTiet where iID_DTChungTu  = @VoucherId
group by iID_DTCTChiTiet, sXauNoiMa, iID_CTDuToan_Nhan, iID_MaDonVi, sGhiChu
),

conlai_mucluc as
(
select 
iID_DTCTChiTiet = null,
iID_DTChungTu = null,
mucluc_sqd.sXauNoiMa, 
mucluc_sqd.iID_CTDuToan_Nhan,
iID_MaDonVi = null,
sTenDonVi = null,
soquyetdinh.iID_DTChungTu idSoQuyetDinh,
soquyetdinh.sSoQuyetDinh,
iRowType = 2,
sGhiChu = null,
(isnull(nhanphanbo.fTuChi, 0) - isnull(phanbo.fTuChi, 0)) fTuChi, 
(isnull(nhanphanbo.fRutKBNN, 0) - isnull(phanbo.fRutKBNN, 0)) fRutKBNN, 
(isnull(nhanphanbo.fHienVat, 0) - isnull(phanbo.fHienVat, 0)) fHienVat, 
(isnull(nhanphanbo.fDuPhong, 0) - isnull(phanbo.fDuPhong, 0)) fDuPhong, 
(isnull(nhanphanbo.fHangMua, 0) - isnull(phanbo.fHangMua, 0)) fHangMua, 
(isnull(nhanphanbo.fHangNhap, 0) - isnull(phanbo.fHangNhap, 0)) fHangNhap, 
(isnull(nhanphanbo.fPhanCap, 0) - isnull(phanbo.fPhanCap, 0)) fPhanCap, 
(isnull(nhanphanbo.fTonKho, 0) - isnull(phanbo.fTonKho, 0)) fTonKho
from mucluc_sqd
left join nhanphanbo_current nhanphanbo on nhanphanbo.sXauNoiMa = mucluc_sqd.sXauNoiMa and nhanphanbo.iID_DTChungTu = mucluc_sqd.iID_CTDuToan_Nhan
left join phanbo on phanbo.sXauNoiMa = mucluc_sqd.sXauNoiMa and phanbo.iID_CTDuToan_Nhan = mucluc_sqd.iID_CTDuToan_Nhan
left join soquyetdinh on soquyetdinh.iID_DTChungTu = mucluc_sqd.iID_CTDuToan_Nhan
),

nhanphanbo_mucluc as
(
select 
iID_DTCTChiTiet = null,
iID_DTChungTu = null,
mucluc_sqd.sXauNoiMa, 
mucluc_sqd.iID_CTDuToan_Nhan iID_CTDuToan_Nhan,
iID_MaDonVi = null,
sTenDonVi = null,
soquyetdinh.iID_DTChungTu idSoQuyetDinh,
soquyetdinh.sSoQuyetDinh,
iRowType = 1,
sGhiChu = null,
isnull(nhanphanbo.fTuChi, 0) fTuChi,
isnull(nhanphanbo.fRutKBNN, 0) fRutKBNN,
isnull(nhanphanbo.fHienVat, 0) fHienVat,
isnull(nhanphanbo.fDuPhong, 0) fDuPhong,
isnull(nhanphanbo.fHangMua, 0) fHangMua,
isnull(nhanphanbo.fHangNhap, 0) fHangNhap,
isnull(nhanphanbo.fPhanCap, 0) fPhanCap,
isnull(nhanphanbo.fTonKho, 0) fTonKho
from mucluc_sqd
left join nhanphanbo_current nhanphanbo on nhanphanbo.sXauNoiMa = mucluc_sqd.sXauNoiMa and nhanphanbo.iID_DTChungTu = mucluc_sqd.iID_CTDuToan_Nhan
left join soquyetdinh on soquyetdinh.iID_DTChungTu = mucluc_sqd.iID_CTDuToan_Nhan
),

phanbo_mucluc as
(
select 
phanbo_donvi.iID_DTCTChiTiet,
iID_DTChungTu  = cast(@VoucherId as uniqueidentifier),
mucluc_sqd_donvi.sXauNoiMa,
mucluc_sqd_donvi.iID_CTDuToan_Nhan,
mucluc_sqd_donvi.Item iID_MaDonVi,
donvis.iID_MaDonVi + ' - ' + donvis.sTenDonVi as sTenDonVi,
idSoQuyetDinh = soquyetdinh.iID_DTChungTu,
sSoQuyetDinh = soquyetdinh.sSoQuyetDinh,
iRowType = 3,
phanbo_donvi.sGhiChu,
isnull(phanbo_donvi.fTuChi, 0) fTuChi,
isnull(phanbo_donvi.fRutKBNN, 0) fRutKBNN,
isnull(phanbo_donvi.fHienVat, 0) fHienVat,
isnull(phanbo_donvi.fDuPhong, 0) fDuPhong,
isnull(phanbo_donvi.fHangMua, 0) fHangMua,
isnull(phanbo_donvi.fHangNhap, 0) fHangNhap,
isnull(phanbo_donvi.fPhanCap, 0) fPhanCap,
isnull(phanbo_donvi.fTonKho, 0) fTonKho
from mucluc_sqd_donvi
left join phanbo_donvi on 
phanbo_donvi.sXauNoiMa = mucluc_sqd_donvi.sXauNoiMa 
and phanbo_donvi.iID_CTDuToan_Nhan = mucluc_sqd_donvi.iID_CTDuToan_Nhan
and phanbo_donvi.iID_MaDonVi = mucluc_sqd_donvi.Item
left join donvis on donvis.iID_MaDonVi = mucluc_sqd_donvi.Item
left join soquyetdinh on soquyetdinh.iID_DTChungTu = mucluc_sqd_donvi.iID_CTDuToan_Nhan
),

muclucchas as
(
select 
iID_DTCTChiTiet = null,
iID_DTChungTu = null,
sXauNoiMa, 
iID_CTDuToan_Nhan = null,
iID_MaDonVi = null,
sTenDonVi = null,
idSoQuyetDinh = null,
sSoQuyetDinh = null,
iRowType = 0, 
sGhiChu = null,
fTuChi = 0,
fRutKBNN = 0,
fHienVat = 0,
fDuPhong = 0,
fHangMua = 0,
fHangNhap = 0,
fPhanCap = 0,
fTonKho = 0
from #muclucngansachs
where bHangChaDuToan = 1
),

datas as
(select * from muclucchas
union
select * from nhanphanbo_mucluc
union 
select * from conlai_mucluc
union 
select * from phanbo_mucluc)

select 
mucluc.iID_MLNS,
mucluc.iID_MLNS_Cha,
mucluc.sXauNoiMa,
mucluc.sLNS,
mucluc.sL,
mucluc.sK,
mucluc.sM,
mucluc.sTM,
mucluc.sTTM,
mucluc.sNG,
mucluc.sTNG,
mucluc.sTNG1,
mucluc.sTNG2,
mucluc.sTNG3,
case iRowType when 2 then N'Số chưa phân bổ'
else mucluc.sMoTa end as sMoTa,
case when iRowType < 3 then 1
else mucluc.bHangCha end as bHangCha,
case when iRowType < 3 then 1
else mucluc.bHangChaDuToan end as bHangChaDuToan,
datas.sGhiChu,
datas.iID_DTCTChiTiet,
datas.iID_CTDuToan_Nhan,
datas.iID_DTChungTu,
datas.iID_MaDonVi,
datas.sTenDonVi,
datas.fTuChi,
datas.fRutKBNN,
datas.fHienVat,
datas.fHangNhap,
datas.fHangMua,
datas.fPhanCap,
datas.fDuPhong,
datas.fTonKho,
datas.iRowType,
datas.idSoQuyetDinh,
datas.sSoQuyetDinh,
settings.bDuPhong,
settings.bHangMua,
settings.bHangNhap,
settings.bHienVat,
settings.bPhanCap,
settings.bTonKho,
settings.bTuChi,
isnull(settings.bTuChi, cast(0 as bit)) as IsEditTuChi,
isnull(settings.bTonKho, cast(0 as bit)) as IsEditTonKho,
isnull(settings.bHienVat, cast(0 as bit)) as IsEditHienVat,
isnull(settings.bHangNhap, cast(0 as bit)) as IsEditHangNhap,
isnull(settings.bHangMua, cast(0 as bit)) as IsEditHangMua,
isnull(settings.bPhanCap, cast(0 as bit)) as IsEditPhanCap,
isnull(settings.bDuPhong, cast(0 as bit)) as IsEditDuPhong
from #muclucngansachs mucluc
left join datas on datas.sXauNoiMa = mucluc.sXauNoiMa
left join settings on settings.sLNS = mucluc.sLNS
where datas.iRowType is not null
--order by sXauNoiMa, iRowType, iID_MaDonVi

drop table #muclucngansachs

END

;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_thongke_soquyetdinh]    Script Date: 12/13/2024 4:11:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dutoan_rpt_thongke_soquyetdinh]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@SoQuyetDinh nvarchar(100),
	@LNS nvarchar(max),
	@dvt int
AS
BEGIN
select * into #lns from f_split(@LNS);

with
nhanphanbo as
(
	select iID_DTChungTu from NS_DT_ChungTu
	where sSoQuyetDinh = @SoQuyetDinh
	and iNamNganSach = @YearOfBudget
	and iNamLamViec = @YearOfWork
	and iID_MaNguonNganSach = @BudgetSource
	and iLoai = 0
),

nhanphanbos as
(
	select ctct.sXauNoiMa, 
	sum(isnull(fTuChi, 0)) 
	+ sum(isnull(fHienVat, 0)) 
	+ sum(isnull(fDuPhong, 0)) 
	+ sum(isnull(fHangNhap, 0)) 
	+ sum(isnull(fHangMua, 0)) 
	+ sum(isnull(fPhanCap, 0)) SoDuToan from NS_DT_ChungTuChiTiet ctct
	where ctct.iID_DTChungTu in (select * from nhanphanbo)
	group by ctct.sXauNoiMa
),

phanbodieuchinhs as
(
	select map.* from NS_DT_Nhan_PhanBo_Map map
	where map.iID_CTDuToan_Nhan in (select * from nhanphanbo)
	union all
	select map.* from NS_DT_Nhan_PhanBo_Map map
	join phanbodieuchinhs on phanbodieuchinhs.iID_CTDuToan_PhanBo = map.iID_CTDuToan_Nhan
),

phanbos as
(
	select ct.sSoQuyetDinh, ctct.sXauNoiMa, ctct.iID_MaDonVi, 
	sum(isnull(fTuChi, 0)) 
	+ sum(isnull(fHienVat, 0)) 
	+ sum(isnull(fDuPhong, 0)) 
	+ sum(isnull(fHangNhap, 0)) 
	+ sum(isnull(fHangMua, 0)) 
	+ sum(isnull(fPhanCap, 0)) SoPhanBo from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
	where ctct.iID_DTChungTu in (select iID_CTDuToan_PhanBo from phanbodieuchinhs)
	group by ct.sSoQuyetDinh, ctct.sXauNoiMa, ctct.iID_MaDonVi
	having sum(fTuChi) <> 0
	or sum(fHienVat) <> 0
	or sum(fDuPhong) <> 0
	or sum(fHangNhap) <> 0
	or sum(fHangMua) <> 0
	or sum(fPhanCap) <> 0
),

phanboalls as
(
	select ct.sSoQuyetDinh, ctct.sXauNoiMa, 
	sum(isnull(fTuChi, 0)) 
	+ sum(isnull(fHienVat, 0)) 
	+ sum(isnull(fDuPhong, 0)) 
	+ sum(isnull(fHangNhap, 0)) 
	+ sum(isnull(fHangMua, 0)) 
	+ sum(isnull(fPhanCap, 0)) TongSoPhanBo from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
	where ctct.iID_DTChungTu in (select iID_CTDuToan_PhanBo from phanbodieuchinhs)
	group by ct.sSoQuyetDinh, ctct.sXauNoiMa
	having sum(fTuChi) <> 0
	or sum(fHienVat) <> 0
	or sum(fDuPhong) <> 0
	or sum(fHangNhap) <> 0
	or sum(fHangMua) <> 0
	or sum(fPhanCap) <> 0
),

mucluc_parents as
(
	select * from NS_MucLucNganSach where iNamLamViec = @YearOfWork
),

muclucs as
(
	select * from NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and sLNS in (select * from #lns)
	and sXauNoiMa in (select sXauNoiMa from phanboalls)
	union all
	select alls.* from mucluc_parents alls
	join muclucs on muclucs.iID_MLNS_Cha = alls.iID_MLNS
)

select distinct 
null SoQuyetDinh,
muclucs.iID_MLNS MlnsId,  
muclucs.iID_MLNS_Cha MlnsIdCha, 
muclucs.sLNS LNS,
muclucs.sL L, 
muclucs.sK K, 
muclucs.sM M, 
muclucs.sTM TM, 
muclucs.sTTM TTM,
muclucs.sNG NG,
muclucs.sTNG TNG,
muclucs.sTNG1 TNG1,
muclucs.sTNG2 TNG2,
muclucs.sTNG3 TNG3,
muclucs.sXauNoiMa XauNoiMa,
muclucs.sMoTa MoTa,
muclucs.bHangCha BHangCha,
0  SoDuToan, 
0 SoPhanBo,
0 TongSoPhanBo,
--isnull(nhanphanbos.SoDuToan, 0) - isnull(phanbos.SoPhanBo, 0) ConLai,
null MaDonVi,
null TenDonVi
from NS_MucLucNganSach muclucs
where iNamLamViec = @YearOfWork 
and sLNS in (select * from #lns)
and sXauNoiMa in (select sXauNoiMa from phanboalls)
union all
select distinct 
phanbos.sSoQuyetDinh SoQuyetDinh,
muclucs.iID_MLNS MlnsId, 
muclucs.iID_MLNS_Cha MlnsIdCha, 
muclucs.sLNS LNS,
muclucs.sL L, 
muclucs.sK K, 
muclucs.sM M, 
muclucs.sTM TM, 
muclucs.sTTM TTM,
muclucs.sNG NG,
muclucs.sTNG TNG,
muclucs.sTNG1 TNG1,
muclucs.sTNG2 TNG2,
muclucs.sTNG3 TNG3,
muclucs.sXauNoiMa XauNoiMa,
muclucs.sMoTa MoTa,
muclucs.bHangCha BHangCha,
isnull(nhanphanbos.SoDuToan, 0)/@dvt SoDuToan, 
isnull(phanbos.SoPhanBo, 0)/@dvt SoPhanBo,
isnull(phanboalls.TongSoPhanBo, 0)/@dvt TongSoPhanBo,
--isnull(nhanphanbos.SoDuToan, 0) - isnull(phanbos.SoPhanBo, 0) ConLai,
phanbos.iID_MaDonVi MaDonVi,
donvi.sTenDonVi TenDonVi
from muclucs
left join nhanphanbos on nhanphanbos.sXauNoiMa = muclucs.sXauNoiMa
left join phanboalls on phanboalls.sXauNoiMa = muclucs.sXauNoiMa
left join phanbos on phanbos.sXauNoiMa = muclucs.sXauNoiMa and phanbos.sSoQuyetDinh = phanboalls.sSoQuyetDinh
left join DonVi donvi on donvi.iID_MaDonVi = phanbos.iID_MaDonVi

order by XauNoiMa, MaDonVi

drop table #lns



END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_nhan_quyettoankinhphi_donvi]    Script Date: 12/13/2024 4:11:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qt_nhan_quyettoankinhphi_donvi]
@YearOfWork int,
@YearOfBudget nvarchar(50),
@BudgetSource int,
@LNS nvarchar(max)
AS
BEGIN


with 

xauNoiMa as (
	select map.sXauNoiMa from NS_MucLucQuyetToanNam_MLNS map
	join NS_MucLucQuyetToanNam mucluc on mucluc.sMa = map.sMaMLQT
	and mucluc.iNamLamViec = map.iNamLamViec
	where map.iNamLamViec = @YearOfWork
	and (isnull(@LNS, '') <> '' and mucluc.sMa in (select * from f_split(@LNS)))
	--and mucluc.iTrangThai = 1
),

donViDuToan as (
	select distinct donvi.* from DonVi donvi
	join NS_DT_ChungTuChiTiet chitiet on chitiet.iID_MaDonVi = donvi.iID_MaDonVi
	and chitiet.iNamLamViec = donvi.iNamLamViec
	where chitiet.iNamLamViec = @YearOfWork and chitiet.sXauNoiMa in (select * from xauNoiMa)
	and iNamNganSach in (select * from f_split(@YearOfBudget))
	and iID_MaNguonNganSach = @BudgetSource
	and isnull(fTuChi, 0) <> 0
),


donViQuyetToan as (
	select distinct donvi.* from DonVi donvi
	join NS_QT_ChungTuChiTiet chitiet on chitiet.iID_MaDonVi = donvi.iID_MaDonVi
	and chitiet.iNamLamViec = donvi.iNamLamViec
	where chitiet.iNamLamViec = @YearOfWork and chitiet.sXauNoiMa in (select * from xauNoiMa)
	and iNamNganSach in (select * from f_split(@YearOfBudget))
	and iID_MaNguonNganSach = @BudgetSource
	and (isnull(fTuChi_DeNghi, 0) <> 0 or isnull(fDeNghi_ChuyenNamSau, 0) <> 0 or isnull(fChuyenNamSau_DaCap, 0) <> 0)
),


donViCapPhat as (
	select distinct donvi.* from DonVi donvi
	join NS_CP_ChungTuChiTiet chitiet on chitiet.iID_MaDonVi = donvi.iID_MaDonVi
	and chitiet.iNamLamViec = donvi.iNamLamViec
	where chitiet.iNamLamViec = @YearOfWork and chitiet.sXauNoiMa in (select * from xauNoiMa)
	and iNamNganSach in (select * from f_split(@YearOfBudget))
	and iID_MaNguonNganSach = @BudgetSource
	and isnull(fTuChi, 0) <> 0
)

select * from donViDuToan
union 
select * from donViQuyetToan
union 
select * from donviCapPhat
where iLoai = 1

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qt_congkhai_thuchi]    Script Date: 12/13/2024 4:11:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_qt_congkhai_thuchi]
	@MaMucLucs nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Time int,
	@DonViTinh int
AS
BEGIN

with 
DonViCha AS
(
	select iID_MaDonVi from DonVi 
	where iLoai = 0
	and iNamLamViec = @YearOfWork
	and iTrangThai = 1
),
MucLucCongKhaiCTE AS
(
	select *, case sMa when '01' then 1
			  else 0 end as isThu
	from NS_DanhMucCongKhai 
	where isnull(sMaCha, '') = '' and iNamLamViec = @YearOfWork
	union all
	select child.*, parent.isThu
	from NS_DanhMucCongKhai child
	JOIN MucLucCongKhaiCTE parent on parent.sMa = child.sMaCha
	where child.iNamLamViec = @YearOfWork
),
MucLucCongKhaiMap as 
(
	select * from NS_DMCongKhai_MLNS
	where iNamLamViec = @YearOfWork
),
MucLucNganSach as 
(
	select * from NS_MucLucNganSach
	where iNamLamViec = @YearOfWork
),
ChiTietDuToan as 
(
	select * from NS_DT_ChungTuChiTiet
	where iNamLamViec = @YearOfWork
	and iNamNganSach = @YearOfBudget
	and iID_MaNguonNganSach = @BudgetSource
),
ChiTietQuyetToan as 
(
	select * from NS_QT_ChungTuChiTiet
	where iNamLamViec = @YearOfWork
	and iNamNganSach = @YearOfBudget
	and iID_MaNguonNganSach = @BudgetSource
),
ChiTietDuToanThuNop as 
(
	select * from TN_DT_ChungTuChiTiet
	where NamLamViec = @YearOfWork
	and NamNganSach = @YearOfBudget
	and NguonNganSach = @BudgetSource
),
ChiTietQuyetToanThuNop as 
(
	select * from TN_QuyetToan_ChungTuChiTiet_HD4554
	where iNamLamViec = @YearOfWork
	and iNamNganSach = @YearOfBudget
	and iNguonNganSach = @BudgetSource
)

select 
mucluc.STT,
mucluc.sMoTa NoiDung,
mucluc.Id Id,
mucluc.iID_DMCongKhai_Cha ParentId,
mucluc.bHangCha IsHangCha,
mucluc.sMa MaMucLuc,
mucluc.sMaCha MaMucLucCha,
chitiet.DuToanDuocGiao / @DonViTinh DuToanDuocGiao, 
chitiet.SoBaoCaoQuyetToan / @DonViTinh SoBaoCaoQuyetToan,
chitiet.SoQuyetToanDuocDuyet / @DonViTinh SoQuyetToanDuocDuyet
from MucLucCongKhaiCTE mucluc

left join

(select 
mlck.Id,
(sum(isnull(dt.ftuchi, 0)) + sum(isnull(dt.fHangNhap, 0)) + sum(isnull(dt.fHangMua, 0)) + sum(isnull(dttn.TuChi, 0))) DuToanDuocGiao,
(sum(isnull(qt.fTuChi_DeNghi, 0)) + sum(isnull(qttn.fSoTien_DeNghi, 0))) SoBaoCaoQuyetToan,
(sum(isnull(qt.fTuChi_PheDuyet, 0)) + sum(isnull(qttn.fSoTien, 0))) SoQuyetToanDuocDuyet
from MucLucCongKhaiCTE mlck
left join MucLucCongKhaiMap map on map.iID_DMCongKhai = mlck.Id
left join 
(
select sXauNoiMa, sum(isnull(fTuChi, 0)) fTuChi, sum(isnull(fHangNhap, 0)) fHangNhap, sum(isnull(fHangMua, 0)) fHangMua from ChiTietDuToan
where iPhanCap = 0 and iID_MaDonVi in (select * from DonViCha)
group by sXauNoiMa
) dt on (dt.sXauNoiMa = map.sNS_XauNoiMa and mlck.isThu = 0)
left join 
(
select XauNoiMa, sum(isnull(TuChi, 0)) TuChi from ChiTietDuToanThuNop
where iPhanCap = 0 and Id_DonVi in (select * from DonViCha)
group by XauNoiMa
) dttn on (dttn.XauNoiMa = map.sNS_XauNoiMa and mlck.isThu = 1)
left join 
(
select sXauNoiMa, sum(isnull(fTuChi_DeNghi, 0)) fTuChi_DeNghi, sum(isnull(fTuChi_PheDuyet, 0)) fTuChi_PheDuyet from ChiTietQuyetToan
where iID_MaDonVi in (select * from DonViCha)
group by sXauNoiMa
) qt on (qt.sXauNoiMa = map.sNS_XauNoiMa and mlck.isThu = 0)
left join
(
select sXauNoiMa, sum(isnull(fSoTien, 0)) fSoTien, sum(isnull(fSoTien_DeNghi, 0)) fSoTien_DeNghi from ChiTietQuyetToanThuNop
where iID_MaDonVi in (select * from DonViCha)
group by sXauNoiMa
) qttn on (qttn.sXauNoiMa = map.sNS_XauNoiMa and mlck.isThu = 1)
where isnull(@MaMucLucs, '') = '' or mlck.sMa in (select * from f_split(@MaMucLucs))
group by mlck.Id
) chitiet on chitiet.Id = mucluc.Id
order by sMa

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qt_congkhai_thuchi_donvi]    Script Date: 12/13/2024 4:11:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_qt_congkhai_thuchi_donvi]
	@MaMucLucs nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@MaDonVis nvarchar(max),
	@DonViTinh int
AS
BEGIN
	

with 
MucLucCongKhaiCTE AS
(
	select *, case sMa when '01' then 1
			  else 0 end as isThu
	from NS_DanhMucCongKhai 
	where isnull(sMaCha, '') = '' and iNamLamViec = @YearOfWork
	union all
	select child.*, parent.isThu
	from NS_DanhMucCongKhai child
	JOIN MucLucCongKhaiCTE parent on parent.sMa = child.sMaCha
	where child.iNamLamViec = @YearOfWork
),
MucLucCongKhaiMap as 
(
	select * from NS_DMCongKhai_MLNS
	where iNamLamViec = @YearOfWork
),
MucLucNganSach as 
(
	select * from NS_MucLucNganSach
	where iNamLamViec = @YearOfWork
),
ChiTietDuToan as 
(
	select * from NS_DT_ChungTuChiTiet
	where iNamLamViec = @YearOfWork
	and iNamNganSach = @YearOfBudget
	and iID_MaNguonNganSach = @BudgetSource
),
ChiTietQuyetToan as 
(
	select * from NS_QT_ChungTuChiTiet
	where iNamLamViec = @YearOfWork
	and iNamNganSach = @YearOfBudget
	and iID_MaNguonNganSach = @BudgetSource
),
ChiTietDuToanThuNop as 
(
	select * from TN_DT_ChungTuChiTiet
	where NamLamViec = @YearOfWork
	and NamNganSach = @YearOfBudget
	and NguonNganSach = @BudgetSource
),
ChiTietQuyetToanThuNop as 
(
	select * from TN_QuyetToan_ChungTuChiTiet_HD4554
	where iNamLamViec = @YearOfWork
	and iNamNganSach = @YearOfBudget
	and iNguonNganSach = @BudgetSource
)

(select 
mlck.Id,
isnull(qt.iID_MaDonVi, qttn.iID_MaDonVi) MaDonVi,
(sum(isnull(qt.fTuChi_PheDuyet, 0)) + sum(isnull(qttn.fSoTien, 0))) / @DonViTinh SoTien
from MucLucCongKhaiCTE mlck
left join MucLucCongKhaiMap map on map.iID_DMCongKhai = mlck.Id
left join 
(
select sXauNoiMa, iID_MaDonVi, sum(isnull(fTuChi_PheDuyet, 0)) / @DonViTinh fTuChi_PheDuyet from ChiTietQuyetToan
where isnull(@MaDonVis, '') = '' or iID_MaDonVi in (select * from f_split(@MaDonVis))
group by sXauNoiMa, iID_MaDonVi
) qt on (qt.sXauNoiMa = map.sNS_XauNoiMa and mlck.isThu = 0)
left join
(
select sXauNoiMa, iID_MaDonVi, sum(isnull(fSoTien, 0)) / @DonViTinh fSoTien from ChiTietQuyetToanThuNop
where isnull(@MaDonVis, '') = '' or iID_MaDonVi in (select * from f_split(@MaDonVis))
group by sXauNoiMa, iID_MaDonVi
) qttn on (qttn.sXauNoiMa = map.sNS_XauNoiMa and mlck.isThu = 1)
where (coalesce(qt.iID_MaDonVi, qttn.iID_MaDonVi, '') <> '')
and (isnull(@MaMucLucs, '') = '' or mlck.sMa in (select * from f_split(@MaMucLucs)))
group by mlck.Id, mlck.sMa, qt.iID_MaDonVi, qttn.iID_MaDonVi) 

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_plan_begin_year_2]    Script Date: 12/13/2024 4:11:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_skt_plan_begin_year_2] 
	@YearOfWork INT,
	@YearOfBudget INT,
	@BudgetSource INT,
	@Loai NVARCHAR(50),
	@iLoaiNNS INT,
	@UserName NVARCHAR(100)
AS
BEGIN
	declare @CountRoot int;
select @CountRoot = count(nddv.iID_MaDonVi) from NguoiDung_DonVi nddv
join DonVi dv on dv.iID_MaDonVi = nddv.iID_MaDonVi
and dv.iNamLamViec = nddv.iNamLamViec
where iID_MaNguoiDung = @UserName 
and nddv.iTrangThai = 1
and dv.iTrangThai = 1
and nddv.iNamLamViec = @YearOfWork
and iLoai = 0;

with
donvis as
(
select iID_MaDonVi, sTenDonVi, iLoai
from DonVi
where iNamLamViec = @YearOfWork
and ((@Loai <> '2') or (@Loai = '2' and bCoNSNganh = 1))
and iTrangThai = 1
),

dutoandaunams as
(
select * from NS_DTDauNam_ChungTu
where iLoaiChungTu = 1
and iNamLamViec = @YearOfWork
and iNamNganSach = @YearOfBudget
and iID_MaNguonNganSach = @BudgetSource
and ((@CountRoot > 0 and bKhoa = 1) or ((isnull(sDSLNS, '') = '' or (exists (
select * from f_split(sDSLNS)
intersect
select sLNS from NS_NguoiDung_LNS
where sMaNguoiDung = @UserName
and iNamLamViec = @YearOfWork
)))
and exists (
select * from f_split(iID_MaDonVi)
intersect
select iID_MaDonVi from NguoiDung_DonVi
where iID_MaNguoiDung = @UserName
and iNamLamViec = @YearOfWork
)))
and (@iLoaiNNS = 0 OR iLoaiNguonNganSach = @iLoaiNNS)
),

sokiemtras as
(
select 
ctct.iID_MaDonVi, 
ct.iLoaiNguonNganSach,
case when (@Loai = 2) 
then sum(isnull(fPhanCap, 0)) + sum(isnull(fMuaHangCapHienVat, 0))
else sum(isnull(fTuChi, 0)) end as SoKiemTra
from NS_SKT_ChungTuChiTiet ctct
join NS_SKT_ChungTu ct on ct.iID_CTSoKiemTra = ctct.iID_CTSoKiemTra
where 
ct.iNamLamViec = @YearOfWork
and ct.iNamNganSach = @YearOfBudget
and ct.iID_MaNguonNganSach = @BudgetSource
and ct.bKhoa = 1
and (@iLoaiNNS = 0 OR iLoaiNguonNganSach = @iLoaiNNS)
and (ctct.iLoai = 4 or ctct.iLoai = 3 or ctct.iLoai = 2)
group by ctct.iID_MaDonVi, ctct.iLoai, ct.iLoaiNguonNganSach
)

select 
dv.iID_MaDonVi Id_DonVi,
dv.sTenDonVi TenDonVi,
dv.iLoai Loai,
isnull(skt.SoKiemTra, 0) SoKiemTra,
isnull(dtdn.fTongTuChi, 0) SoDuToan,
dtdn.iID_CTDTDauNam Id,
dtdn.sMoTa MoTa,
dtdn.iLoaiChungTu LoaiNganSach,
dtdn.iLoaiNguonNganSach ILoaiNguonNganSach,
dtdn.sSoChungTu,
dtdn.sDSDonViTongHop DSDonViTongHop,
dtdn.sDSSoChungTuTongHop DSSoChungTuTongHop,
dtdn.sNguoiTao NguoiTao,
dtdn.sDSLNS DsLNS,
dtdn.bDaTongHop BDaTongHop,
isnull(dtdn.bKhoa, 0) IsLocked,
dtdn.is_Sent IsSent
from dutoandaunams dtdn
left join sokiemtras skt 
on skt.iID_MaDonVi = dtdn.iID_MaDonVi
and skt.iLoaiNguonNganSach = dtdn.iLoaiNguonNganSach
left join donvis dv on dv.iID_MaDonVi = dtdn.iID_MaDonVi
order by Id_DonVi
END;
GO



IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_QuyetToan_CongKhai_ThuChi_04a')
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 
1, N'rptNS_QuyetToan_CongKhai_ThuChi_04a', NULL, N'rptNS_QuyetToan_CongKhai_ThuChi_04a', NULL, 
NULL, NULL, NULL, NULL, N'QUYET_TOAN', NULL, 
N'Báo cáo quyết toán công khai thu chi', NULL, NULL, 
NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 
NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 
NULL, N'QUYẾT TOÁN THU, CHI NGÂN SÁCH NHÀ NƯỚC NĂM 2024', NULL, N'(Kèm theo Quyết định số   /QĐ - Ngày.....tháng....năm.......của........)', NULL, NULL, NULL,
1, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
ELSE
UPDATE DM_ChuKy
set TieuDe1_MoTa = N'QUYẾT TOÁN THU, CHI NGÂN SÁCH NHÀ NƯỚC NĂM 2024',
TieuDe2_MoTa = N'(Kèm theo Quyết định số   /QĐ - Ngày.....tháng....năm.......của........)'
where Id_Type = 'rptNS_QuyetToan_CongKhai_ThuChi_04a'
GO

IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_QuyetToan_CongKhai_ThuChi_04b')
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 
1, N'rptNS_QuyetToan_CongKhai_ThuChi_04b', NULL, N'rptNS_QuyetToan_CongKhai_ThuChi_04b', NULL, 
NULL, NULL, NULL, NULL, N'QUYET_TOAN', NULL, 
N'Báo cáo quyết toán công khai thu chi', NULL, NULL, 
NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 
NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 
NULL, N'QUYẾT TOÁN THU, CHI NGÂN SÁCH NHÀ NƯỚC NĂM 2024', NULL, N'(Kèm theo Quyết định số   /QĐ - Ngày.....tháng....năm.......của........)', NULL, NULL, NULL,
1, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
ELSE
UPDATE DM_ChuKy
set TieuDe1_MoTa = N'QUYẾT TOÁN THU, CHI NGÂN SÁCH NHÀ NƯỚC NĂM 2024',
TieuDe2_MoTa = N'(Kèm theo Quyết định số   /QĐ - Ngày.....tháng....năm.......của........)'
where Id_Type = 'rptNS_QuyetToan_CongKhai_ThuChi_04b'
GO



