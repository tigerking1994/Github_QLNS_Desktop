/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]    Script Date: 18/07/2022 9:17:48 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]    Script Date: 18/07/2022 9:17:48 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]    Script Date: 18/07/2022 9:38:28 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet]    Script Date: 18/07/2022 9:38:28 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_qt_chutuchitiet]    Script Date: 18/07/2022 9:38:28 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_qt_chutuchitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_qt_chutuchitiet]
GO

/****** Object:  StoredProcedure [dbo].[sp_qt_qt_chutuchitiet]    Script Date: 18/07/2022 9:38:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_qt_qt_chutuchitiet]
  @strIdDonVi NVARCHAR (2000),
  @strThang NVARCHAR (50),
  @strNam int,
  @strThangTruoc NVARCHAR (50),
  @strNamTruoc int
AS
BEGIN

if (SELECT count (*)  FROM f_split(@strThang)) = 1
begin 

with Thang as (
	select  XauNoiMa, MoTa, NamLamViec, 
		sum(ctct.TongSo ) TongSo, sum(ctct.ThieuUy) ThieuUy, sum(ctct.TrungUy) TrungUy, sum(ctct.ThuongUy) ThuongUy, sum(ctct.DaiUy) DaiUy,sum(ctct.ThieuTa) ThieuTa, sum(ctct.TrungTa) TrungTa, sum(ctct.ThuongTa) ThuongTa, sum(ctct.DaiTa) DaiTa, sum(ctct.Tuong) Tuong,
		sum(ctct.BinhNhi) BinhNhi, sum(ctct.BinhNhat) BinhNhat, sum(ctct.HaSi) HaSi, sum(ctct.TrungSi) TrungSi, sum(ctct.ThuongSi ) ThuongSi,
		sum(ctct.ThieuUyCn) ThieuUyCn, sum(ctct.TrungUycn) TrungUycn, sum(ctct.ThuongUyCn) ThuongUyCn, sum(ctct.DaiUyCn) DaiUyCn,sum(ctct.ThieuTaCn) ThieuTaCn, sum(ctct.TrungTaCn) TrungTaCn, sum(ctct.ThuongTaCn) ThuongTaCn,
		sum(ctct.CNQP) CNQP, sum(ctct.LDHD) LDHD, sum(ctct.VCQP) VCQP
from  tl_qs_chungtuchitiet ctct 
where 1=1  
and ctct.Thang in  (SELECT * FROM f_split(@strThang))
and ctct.NamLamViec = @strNam
and ctct.Id_DonVi in  (SELECT * FROM f_split(@strIdDonVi))
group by  xaunoima, mota , NamLamViec
)
select * from  thang  order by  xaunoima;
end 

else
WITH ThoiGianTruoc as (
select  XauNoiMa, 
		case 
			when (select count(*) from f_split(@strThangTruoc)) = 1 then MoTa
			when (select count(*) from f_split(@strThangTruoc)) = 3 then replace(MoTa, N'tháng', N'quý')
			when (select count(*) from f_split(@strThangTruoc)) = 12 then replace(MoTa, N'tháng', N'năm')
		end as MoTa,
		NamLamViec, sum(ctct.TongSo ) TongSo, sum(ctct.ThieuUy) ThieuUy, sum(ctct.TrungUy) TrungUy, sum(ctct.ThuongUy) ThuongUy, sum(ctct.DaiUy) DaiUy,sum(ctct.ThieuTa) ThieuTa, sum(ctct.TrungTa) TrungTa, sum(ctct.ThuongTa) ThuongTa, sum(ctct.DaiTa) DaiTa, sum(ctct.Tuong) Tuong,
		sum(ctct.BinhNhi) BinhNhi, sum(ctct.BinhNhat) BinhNhat, sum(ctct.HaSi) HaSi, sum(ctct.TrungSi) TrungSi, sum(ctct.ThuongSi ) ThuongSi,
		sum(ctct.ThieuUyCn) ThieuUyCn, sum(ctct.TrungUycn) TrungUycn, sum(ctct.ThuongUyCn) ThuongUyCn, sum(ctct.DaiUyCn) DaiUyCn,sum(ctct.ThieuTaCn) ThieuTaCn, sum(ctct.TrungTaCn) TrungTaCn, sum(ctct.ThuongTaCn) ThuongTaCn,
		sum(ctct.CNQP) CNQP, sum(ctct.LDHD) LDHD, sum(ctct.VCQP) VCQP
from  tl_qs_chungtuchitiet ctct 
where 1=1  
and ctct.Thang in (SELECT * FROM f_split(@strThangTruoc))
and ctct.NamLamViec = @strNamTruoc
and ctct.Id_DonVi in  (SELECT * FROM f_split(@strIdDonVi))
and XauNoiMa in ('100','500')
group by  xaunoima, mota , NamLamViec
),
ThoiGianNay  as(
select  XauNoiMa, 
		case 
			when (select count(*) from f_split(@strThang)) = 1 then MoTa
			when (select count(*) from f_split(@strThang)) = 3 then replace(MoTa, N'tháng', N'quý')
			when (select count(*) from f_split(@strThang)) = 12 then replace(MoTa, N'tháng', N'năm')
		end as MoTa,
		NamLamViec, sum(ctct.TongSo ) TongSo, sum(ctct.ThieuUy) ThieuUy, sum(ctct.TrungUy) TrungUy, sum(ctct.ThuongUy) ThuongUy, sum(ctct.DaiUy) DaiUy,sum(ctct.ThieuTa) ThieuTa, sum(ctct.TrungTa) TrungTa, sum(ctct.ThuongTa) ThuongTa, sum(ctct.DaiTa) DaiTa, sum(ctct.Tuong) Tuong,
		sum(ctct.BinhNhi) BinhNhi, sum(ctct.BinhNhat) BinhNhat, sum(ctct.HaSi) HaSi, sum(ctct.TrungSi) TrungSi, sum(ctct.ThuongSi ) ThuongSi,
		sum(ctct.ThieuUyCn) ThieuUyCn, sum(ctct.TrungUycn) TrungUycn, sum(ctct.ThuongUyCn) ThuongUyCn, sum(ctct.DaiUyCn) DaiUyCn,sum(ctct.ThieuTaCn) ThieuTaCn, sum(ctct.TrungTaCn) TrungTaCn, sum(ctct.ThuongTaCn) ThuongTaCn,
		sum(ctct.CNQP) CNQP, sum(ctct.LDHD) LDHD, sum(ctct.VCQP) VCQP
from  tl_qs_chungtuchitiet ctct 
where 1=1  
and ctct.Thang in  (SELECT * FROM f_split(@strThang))
and ctct.NamLamViec = @strNam
and ctct.Id_DonVi in  (SELECT * FROM f_split(@strIdDonVi))
and  XauNoiMa not in ('100','500')
group by  xaunoima, mota , NamLamViec
)
select * from ThoiGianTruoc  
union all 
select * from ThoiGianNay 
order by xaunoima;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet]    Script Date: 18/07/2022 9:38:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet] @idChungTu nvarchar(MAX),
                                                       @nam int, @maCachTl nvarchar(50) AS BEGIN
SELECT 
	   --ctct.Id as Id,
	   iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS Mota,
	   MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       Sum(TongCong) AS TongCong,
       convert(decimal,Sum(SoNguoi)) as SoNguoi,
       Sum(DieuChinh) AS DieuChinh,
	   Sum(DDuToan) As DDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN TL_QT_ChungTuChiTiet ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
AND ctct.Id_ChungTu in (SELECT *
   FROM f_split(@idChungTu))
AND ctct.MaCachTl in
  (SELECT *
   FROM f_split(@maCachTl))
WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam
GROUP BY 
		 --ctct.Id,
		 iID_MLNS,
         iID_MLNS_Cha,
         sXauNoiMa,
         sLNS,
         sL,
         sK,
         sM,
         sTM,
         sTTM,
         sNG,
         sTNG,
         sTNG1,
         sTNG2,
         sTNG3,
         sMoTa,
         sChiTietToi,
         mlns.bHangCha,
         iNamLamViec,
		 MaCachTl
ORDER BY sXauNoiMa END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]    Script Date: 18/07/2022 9:38:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 09/12/2021
-- Description:	Lấy dữ liệu báo cáo bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]
	@MaDonVi NVARCHAR(max), 
	@Thang int,
	@Nam AS int,
	@IsOrderChucVu AS bit = 0,
	@IsGiaTriAm AS bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
	SET @Query =
	'
	WITH BangLuongThang AS (
		SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
			SELECT
				dsCapNhapBangLuong.Thang	AS Thang,
				dsCapNhapBangLuong.Nam		AS Nam,
				bangLuong.Ma_CBo			AS MaCanBo,
				bangLuong.MA_PHUCAP			AS MaPhuCap,
				bangLuong.GIA_TRI			AS GiaTri
			FROM TL_BangLuong_Thang bangLuong
			JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
				ON bangLuong.parent = dsCapNhapBangLuong.Id
			WHERE
				bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
				AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
				AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
				AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
				AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
				AND dsCapNhapBangLuong.Status=1
		) x
		PIVOT
		(
			SUM(GiaTri)
			FOR MaPhuCap IN (' + @Cols + ')
		) pvt
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi		AS MaDonVi,
			donVi.Ten_Donvi		AS TenDonvi,
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
			canBo.Thang_TNN		AS Tnn,
			ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			chucVu.Ma_Cv AS MaChucVu,
			chucVu.Ten_Cv AS TenChucVu,
			CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) AS Stk,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB=capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu
			ON canBo.Ma_CV=chucVu.Ma_Cv
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
	)
	
	SELECT
		bangLuong.*,
		canBo.MaDonVi,
		canBo.TenDonVi,
		canBo.TenCanBo,
		canBo.Ten,
		canBo.MaCapBac,
		canBo.CapBac,
		canBo.HSChucVu,
		canBo.MaChucVu,
		canBo.TenChucVu,
		canBo.Stk,
		canBo.NgayNhapNgu,
		canBo.NgayXuatNgu,
		canBo.NgayTaiNgu,
		canBo.Tnn
	FROM BangLuongThang bangLuong
	INNER JOIN ThongTinCanBo canBo
		ON bangLuong.MaCanBo = canBo.MaCanBo'
	If @IsGiaTriAm = 1 
	SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
	execute(@Query)
END
GO

/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]    Script Date: 18/07/2022 9:17:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]
	@YearOfWork int,
	@YearOrBudget nvarchar(20),
	@ListLNS nvarchar(200),
	@ListUnitId nvarchar(200),
	@DataType int,
	@BudgetSource int,
	@Type nvarchar(10)
as
begin

select 
	a.iID_MLNS,
	a.iID_MLNS_Cha as MLNS_Id_Parent,
	a.sLNS as LNS,
	a.sL as L,
	a.sK as K,
	a.sM as M,
	a.sTM as TM,
	a.sTTM as TTM,
	a.sNG as NG,
	a.sTNG1 as TNG1,
	a.sTNG2 as TNG2,
	a.sTNG3 as TNG3,
	a.sXauNoiMa as XauNoiMa,
	a.sMoTa as MoTa,
	cast (0 as bit) as IsHangCha,
	case when @DataType = 1 then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / 1000
		when @DataType = 2 then (a.fHienVat / 1000) end as DuToanSoBaoCao,
	case when @DataType = 1 then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / 1000
		when @DataType = 2 then (a.fHienVat)/1000 end as DuToanSoXetDuyet,
	0 as QuyetToanSoBaoCao,
	0 as QuyetToanSoXetDuyet,
	0 as XetDuyetDuToanConDuChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi
	from NS_DT_ChungTuChiTiet a
	inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi 
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach in (select * from f_split(@YearOrBudget))
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
		--and a.iID_MaNguonNganSach = @BudgetSource

	union all

	select
		a.iID_MLNS,
		a.iID_MLNS_Cha as MLNS_Id_Parent,
		a.sLNS as LNS,
		a.sL as L,
		a.sK as K,
		a.sM as M,
		a.sTM as TM,
		a.sTTM as TTM,
		a.sNG as NG,
		a.sTNG1 as TNG1,
		a.sTNG2 as TNG2,
		a.sTNG3 as TNG3,
		a.sXauNoiMa as XauNoiMa,
		a.sMoTa as MoTa,
		cast (0 as bit) as IsHangCha,
		0 as DuToanSoBaoCao,
		0 as DuToanSoXetDuyet,
		case when @DataType = 1 then a.fTuChi_DeNghi / 1000
		when @DataType = 2 then 0 end as QuyetToanSoBoCao,
		fTuChi_PheDuyet / 1000 as QuyetToanSoXetDuyet,
		(ISNULL(a.fChuyenNamSau_DaCap,0) + ISNULL(a.fChuyenNamSau_ChuaCap,0)) / 1000 as XetDuyetDuToanConDuChuyenNamSau,
		a.iID_MaDonVi as IdDonVi,
		b.sTenDonVi as TenDonVi
	from NS_QT_ChungTuChiTiet a
		inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach in (select * from f_split(@YearOrBudget))
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
		--and a.iID_MaNguonNganSach = @BudgetSource
		
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]    Script Date: 18/07/2022 9:17:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]
	@YearOfWork int,
	@YearOfBudget nvarchar(20),
	@ListLNS nvarchar(200),
	@ListUnitId nvarchar(200),
	@DataType int,
	@BudgetSource int
as
begin

--Dự toán năm trước chuyển sang
select 
	a.iID_MLNS,
	a.iID_MLNS_Cha as MLNS_Id_Parent,
	a.sLNS as LNS,
	a.sL as L,
	a.sK as K,
	a.sM as M,
	a.sTM as TM,
	a.sTTM as TTM,
	a.sNG as NG,
	a.sTNG1 as TNG1,
	a.sTNG2 as TNG2,
	a.sTNG3 as TNG3,
	a.sXauNoiMa as XauNoiMa,
	a.sMoTa as MoTa,
	cast (0 as bit) as IsHangCha,
	case when @DataType = 1 then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / 1000
		when @DataType = 2 then a.fHienVat / 1000 end as DuToanNamTruocChuyenSang,
	0 as DuToanTongSo,
	0 as DuToanBoSungSau,
	0 as SoDeNghiQuyetToanNam,
	0 as DuToanChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi,
	a.iNamNganSach as NamNganSach
	from NS_DT_ChungTuChiTiet a
	inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi 
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach in (1,4)
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
-- dự toán tổng số
	union all
	select 
	a.iID_MLNS,
	a.iID_MLNS_Cha as MLNS_Id_Parent,
	a.sLNS as LNS,
	a.sL as L,
	a.sK as K,
	a.sM as M,
	a.sTM as TM,
	a.sTTM as TTM,
	a.sNG as NG,
	a.sTNG1 as TNG1,
	a.sTNG2 as TNG2,
	a.sTNG3 as TNG3,
	a.sXauNoiMa as XauNoiMa,
	a.sMoTa as MoTa,
	cast (0 as bit) as IsHangCha,
	0 as DuToanNamTruocChuyenSang,
	case when @DataType = 1 then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / 1000 when @DataType = 2 then fHienVat / 1000 end as DuToanTongSo,
	0 as DuToanBoSungSau,
	0 as SoDeNghiQuyetToanNam,
	0 as DuToanChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi,
	a.iNamNganSach as NamNganSach
	from NS_DT_ChungTuChiTiet a
	inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi 
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach = 2
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
		--and a.iLoaiDuToan in (1,3,4,5)

-- bổ sung sau 30/09
	union all
	select 
	a.iID_MLNS,
	a.iID_MLNS_Cha as MLNS_Id_Parent,
	a.sLNS as LNS,
	a.sL as L,
	a.sK as K,
	a.sM as M,
	a.sTM as TM,
	a.sTTM as TTM,
	a.sNG as NG,
	a.sTNG1 as TNG1,
	a.sTNG2 as TNG2,
	a.sTNG3 as TNG3,
	a.sXauNoiMa as XauNoiMa,
	a.sMoTa as MoTa,
	cast (0 as bit) as IsHangCha,
	0 as DuToanNamTruocChuyenSang,
	0 as DuToanTongSo,
	case when @DataType = 1 then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / 1000 when @DataType = 2 then fHienVat / 1000 end as DuToanBoSungSau,
	0 as SoDeNghiQuyetToanNam,
	0 as DuToanChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi,
	a.iNamNganSach as NamNganSach
	from NS_DT_ChungTuChiTiet a
	inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi 
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach = 2
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
		--and a.iLoaiDuToan = 5

-- số đề nghị quyết toán năm & số chuyển năm sau
	union all

	select
		a.iID_MLNS,
		a.iID_MLNS_Cha as MLNS_Id_Parent,
		a.sLNS as LNS,
		a.sL as L,
		a.sK as K,
		a.sM as M,
		a.sTM as TM,
		a.sTTM as TTM,
		a.sNG as NG,
		a.sTNG1 as TNG1,
		a.sTNG2 as TNG2,
		a.sTNG3 as TNG3,
		a.sXauNoiMa as XauNoiMa,
		a.sMoTa as MoTa,
		cast (0 as bit) as IsHangCha,
		0 as DuToanNamTruocChuyenSang,
		0 as DuToanTongSo,
		0 as DuToanBoSungSau,
		-- thiếu cột fHienVat trong bảng NS_QT_ChungTuChiTiet
		case when @DataType = 1 then a.fTuChi_PheDuyet when @DataType = 2 then 0 end as SoDeNghiQuyetToanNam,
		(ISNULL(fChuyenNamSau_ChuaCap,0) + ISNULL(fChuyenNamSau_DaCap,0)) / 1000 as DuToanChuyenNamSau,
		a.iID_MaDonVi as IdDonVi,
		b.sTenDonVi as TenDonVi,
		a.iNamNganSach as NamNganSach
	from NS_QT_ChungTuChiTiet a
		inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach in (select * from f_split(@YearOfBudget))
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
end
GO
