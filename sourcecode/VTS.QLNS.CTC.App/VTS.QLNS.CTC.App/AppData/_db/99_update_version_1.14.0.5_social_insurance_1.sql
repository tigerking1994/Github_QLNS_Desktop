/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]    Script Date: 2/28/2024 11:25:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]    Script Date: 2/28/2024 11:25:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_quy]    Script Date: 2/28/2024 11:25:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_can_cu_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_quy]    Script Date: 2/28/2024 11:25:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_quy] 
	@Year int,
	@Months nvarchar(20),
	@LoaiQuyNam int
AS
BEGIN
	
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu]') AND type in (N'U')) drop table tbl_cancu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result]') AND type in (N'U')) drop table tbl_cancu_result;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu]') AND type in (N'U')) drop table tbl_luong_can_cu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final]') AND type in (N'U')) drop table tbl_cancu_result_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final]') AND type in (N'U')) drop table tbl_luong_can_cu_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi]') AND type in (N'U')) drop table tbl_cancu_so_nguoi;
	
	select
	 pc.Ma_PhuCap,
	 pc.Ma_Cb,
	 sum(ctct.DieuChinh) fGiaTri
	into tbl_cancu
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.Ma_Cb = ctct.MaCb and pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
	and pc.Ma_Cb in ('1', '2', '3.1', '3.2', '3.3', '43', '4')
	and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
	and ct.Thang in (SELECT * FROM f_split(@Months))
	and ct.Nam = @Year
	and pc.Nam = @Year
	and ct.bKhoa = 1
	group by
	 pc.Ma_PhuCap,
	 pc.Ma_Cb
	 
	--get so nguoi
	select
	 pc.Ma_Cb,
	 sum(ctct.SoNguoi) IQSBQNam
	 into tbl_cancu_so_nguoi
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.Ma_Cb = ctct.MaCb and pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
	and pc.Ma_Cb in ('1', '2', '3.1', '3.2', '3.3', '43', '4')
	and pc.Ma_PhuCap = 'LHT_TT'
	and ct.Thang in (SELECT * FROM f_split(@Months))
	and ct.Nam = @Year
	and pc.Nam = @Year
	and ct.bKhoa = 1
	group by
	 pc.Ma_Cb
	 ------------------------------------------
	 select distinct
	  cancu.Ma_Cb SMaCapBac,
	  case
		when @LoaiQuyNam = 0 then songuoi.IQSBQNam
		else songuoi.IQSBQNam/3
	  end as IQSBQNam,
	  luongchinh.fGiaTri FGiaTriLuongChinh,
	  pccv.fGiaTri FGiaTriPCCV,
	  pctn.fGiaTri FGiaTriPCTN,
	  pctnvk.fGiaTri FGiaTriPCTNVK,
	  hsbl.fGiaTri FGiaTriHSBL
	 into tbl_cancu_result
	 from tbl_cancu cancu
	 left join tbl_cancu luongchinh on cancu.Ma_Cb = luongchinh.Ma_Cb and luongchinh.Ma_PhuCap = 'LHT_TT'
	 left join tbl_cancu pccv on cancu.Ma_Cb = pccv.Ma_Cb and pccv.Ma_PhuCap = 'PCCV_TT'
	 left join tbl_cancu pctn on cancu.Ma_Cb = pctn.Ma_Cb and pctn.Ma_PhuCap = 'PCTN_TT'
	 left join tbl_cancu pctnvk on cancu.Ma_Cb = pctnvk.Ma_Cb and pctnvk.Ma_PhuCap = 'PCTNVK_TT'
	 left join tbl_cancu hsbl on cancu.Ma_Cb = hsbl.Ma_Cb and hsbl.Ma_PhuCap = 'HSBL_TT'
	 left join tbl_cancu_so_nguoi songuoi on cancu.Ma_Cb = songuoi.Ma_Cb

	 update tbl_cancu_result set SMaCapBac = '3' where SMaCapBac in ('3.1','3.2','3.3')

	 select distinct
	  cancu.SMaCapBac,
	  sum(cancu.IQSBQNam) IQSBQNam,
	  sum(cancu.FGiaTriLuongChinh) FGiaTriLuongChinh,
	  sum(cancu.FGiaTriPCCV) FGiaTriPCCV,
	  sum(cancu.FGiaTriPCTN) FGiaTriPCTN,
	  sum(cancu.FGiaTriPCTNVK) FGiaTriPCTNVK,
	  sum(cancu.FGiaTriHSBL) FGiaTriHSBL
	 into tbl_cancu_result_final
	 from tbl_cancu_result cancu
	 group by cancu.SMaCapBac

	 --Luong BHXH
	 select
	  sMaCB SMaCapBac,
	  sum(nGiaTri) FNghiOm
	 into tbl_luong_can_cu
	 from TL_BangLuong_ThangBHXH
	 where iNam = @Year
	  and iThang in (SELECT * FROM f_split(@Months))
	  and sMaCheDo in ('BENHDAINGAY_D14NGAY', 'OMKHAC_D14NGAY')
	  and (sMaCB like '1%' or sMaCB like '2%' or sMaCB like '0%' or sMaCB = '43' or sMaCB in ('3.1', '3.2', '3.3'))
	 group by sMaCB

	update tbl_luong_can_cu set SMaCapBac = '1' where SMaCapBac like '1%'
	update tbl_luong_can_cu set SMaCapBac = '2' where SMaCapBac like '2%'
	update tbl_luong_can_cu set SMaCapBac = '4' where SMaCapBac like '0%'
	update tbl_luong_can_cu set SMaCapBac = '3' where SMaCapBac in ('3.1','3.2','3.3')

	select
	  SMaCapBac,
	  sum(FNghiOm) FNghiOm
	 into tbl_luong_can_cu_final
	 from tbl_luong_can_cu
	 group by SMaCapBac

	 --result
	 select
	  luong.SMaCapBac,
	  luong.IQSBQNam,
	  luong.FGiaTriLuongChinh,
	  luong.FGiaTriPCCV,
	  luong.FGiaTriPCTN,
	  luong.FGiaTriPCTNVK,
	  luong.FGiaTriHSBL,
	  CAST(bhxh.FNghiOm AS FLOAT) FNghiOm
	 from tbl_cancu_result_final luong
	 left join tbl_luong_can_cu_final bhxh on luong.SMaCapBac = bhxh.SMaCapBac
	 -----------------------------------------------
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu]') AND type in (N'U')) drop table tbl_cancu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result]') AND type in (N'U')) drop table tbl_cancu_result;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu]') AND type in (N'U')) drop table tbl_luong_can_cu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final]') AND type in (N'U')) drop table tbl_cancu_result_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final]') AND type in (N'U')) drop table tbl_luong_can_cu_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi]') AND type in (N'U')) drop table tbl_cancu_so_nguoi;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]    Script Date: 2/28/2024 11:25:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]
	@ListIdChungTuTongHop ntext, 
	@Nguoitao nvarchar(50), 
	@IdChungTu nvarchar(100), 
	@NamLamViec int 
AS 
BEGIN 
	INSERT INTO [dbo].BH_DTT_BHXH_DieuChinh_ChiTiet (
    iID_DTT_BHXH_DieuChinh_ChiTiet, iID_DTT_BHXH_DieuChinh, iID_MucLucNganSach, sLNS, sNoiDung, sXauNoiMa,
	fThuBHXH_NLD, fThuBHXH_NSD, fThuBHYT_NLD, fThuBHYT_NSD, fThuBHTN_NLD, fThuBHTN_NSD,
	fThuBHXH_NLD_QTDauNam, fThuBHXH_NSD_QTDauNam, fThuBHYT_NLD_QTDauNam, fThuBHYT_NSD_QTDauNam, fThuBHTN_NLD_QTDauNam, fThuBHTN_NSD_QTDauNam,
	fThuBHXH_NLD_QTCuoiNam, fThuBHXH_NSD_QTCuoiNam, fThuBHYT_NLD_QTCuoiNam, fThuBHYT_NSD_QTCuoiNam, fThuBHTN_NLD_QTCuoiNam, fThuBHTN_NSD_QTCuoiNam,
	fTongThuBHXH_NLD, fTongThuBHXH_NSD, fTongThuBHYT_NLD, fTongThuBHYT_NSD, fTongThuBHTN_NLD, fTongThuBHTN_NSD,
	fThuBHXH_NLD_Tang, fThuBHXH_NSD_Tang, fThuBHXH_Tang, fThuBHYT_NLD_Tang, fThuBHYT_NSD_Tang, fThuBHYT_Tang, fThuBHTN_NLD_Tang, fThuBHTN_NSD_Tang, fThuBHTN_Tang,
	fThuBHXH_NLD_Giam, fThuBHXH_NSD_Giam, fThuBHXH_Giam, fThuBHYT_NLD_Giam, fThuBHYT_NSD_Giam, fThuBHYT_Giam, fThuBHTN_NLD_Giam, fThuBHTN_NSD_Giam, fThuBHTN_Giam, fTongCong,
    dNgaySua, dNgayTao, sNguoiSua, sNguoiTao)

	SELECT 
	DISTINCT NEWID(), @idChungTu, iID_MucLucNganSach, sLNS, sNoiDung, sXauNoiMa,
	sum(isnull(fThuBHXH_NLD, 0)), sum(isnull(fThuBHXH_NSD, 0)), sum(isnull(fThuBHYT_NLD, 0)), sum(isnull(fThuBHYT_NSD, 0)), sum(isnull(fThuBHTN_NLD, 0)), sum(isnull(fThuBHTN_NSD, 0)),
	sum(isnull(fThuBHXH_NLD_QTDauNam, 0)), sum(isnull(fThuBHXH_NSD_QTDauNam, 0)), sum(isnull(fThuBHYT_NLD_QTDauNam, 0)), sum(isnull(fThuBHYT_NSD_QTDauNam, 0)), sum(isnull(fThuBHTN_NLD_QTDauNam, 0)), sum(isnull(fThuBHTN_NSD_QTDauNam, 0)),
	sum(isnull(fThuBHXH_NLD_QTCuoiNam, 0)), sum(isnull(fThuBHXH_NSD_QTCuoiNam, 0)), sum(isnull(fThuBHYT_NLD_QTCuoiNam, 0)), sum(isnull(fThuBHYT_NSD_QTCuoiNam, 0)), sum(isnull(fThuBHTN_NLD_QTCuoiNam, 0)), sum(isnull(fThuBHTN_NSD_QTCuoiNam, 0)),
	sum(isnull(fTongThuBHXH_NLD, 0)), sum(isnull(fTongThuBHXH_NSD, 0)), sum(isnull(fTongThuBHYT_NLD, 0)), sum(isnull(fTongThuBHYT_NSD, 0)), sum(isnull(fTongThuBHTN_NLD, 0)), sum(isnull(fTongThuBHTN_NSD, 0)),
	sum(isnull(fThuBHXH_NLD_Tang, 0)), sum(isnull(fThuBHXH_NSD_Tang, 0)), sum(isnull(fThuBHXH_Tang, 0)), sum(isnull(fThuBHYT_NLD_Tang, 0)), sum(isnull(fThuBHYT_NSD_Tang, 0)), sum(isnull(fThuBHYT_Tang, 0)), sum(isnull(fThuBHTN_NLD_Tang, 0)), sum(isnull(fThuBHTN_NSD_Tang, 0)), sum(isnull(fThuBHTN_Tang, 0)),
	sum(isnull(fThuBHXH_NLD_Giam, 0)), sum(isnull(fThuBHXH_NSD_Giam, 0)), sum(isnull(fThuBHXH_Giam, 0)), sum(isnull(fThuBHYT_NLD_Giam, 0)), sum(isnull(fThuBHYT_NSD_Giam, 0)), sum(isnull(fThuBHYT_Giam, 0)), sum(isnull(fThuBHTN_NLD_Giam, 0)), sum(isnull(fThuBHTN_NSD_Giam, 0)), sum(isnull(fThuBHTN_Giam, 0)), sum(isnull(fTongCong, 0)),
	Null, GETDATE(), Null, @Nguoitao 
	FROM 
	  BH_DTT_BHXH_DieuChinh_ChiTiet 
	WHERE 
	  iID_DTT_BHXH_DieuChinh in (SELECT * FROM f_split(@ListIdChungTuTongHop)) 
	GROUP BY 
	  sLNS,
	  iID_MucLucNganSach, 
	  sNoiDung,
	  sXauNoiMa

	  --danh dau chung tu da tong hop
		update 
		  BH_DTT_BHXH_DieuChinh 
		set 
		  bDaTongHop = 1
		where 
		  iID_DTT_BHXH_DieuChinh in (SELECT * FROM f_split(@ListIdChungTuTongHop))
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]    Script Date: 2/28/2024 11:25:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@NamLamViec int
AS
BEGIN
	INSERT INTO BH_KHTM_BHYT_ChiTiet (iID_KHTM_BHYT, iID_NoiDung, sTenNoiDung, iSoNguoi, iSoThang, fDinhMuc, fThanhTien, sGhiChu, dNgayTao, dNgaySua, sNguoiTao, sNguoiSua, iID_MaDonVi, sTenDonVi)
SELECT @idChungTu,
       iID_NoiDung,
	   sTenNoiDung,
       sum(isnull(iSoNguoi, 0)) iSoNguoi,
       sum(isnull(iSoThang, 0)) iSoThang,
	   fDinhMuc,
	   sum(isnull(fThanhTien, 0)) fThanhTien,
	   NULL,
       NULL,
       NULL,
       NULL,
	   NULL,
	   NULL,
	   NULL
FROM BH_KHTM_BHYT_ChiTiet
WHERE iID_KHTM_BHYT in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY iID_NoiDung,
	   sTenNoiDung,
	   fDinhMuc

--danh dau chung tu da tong hop
update BH_KHTM_BHYT set bDaTongHop = 1 
where iID_KHTM_BHYT in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop));
END
;
;
GO



/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]    Script Date: 2/29/2024 10:53:39 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]    Script Date: 2/29/2024 10:53:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0,
@IsNew AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)
DECLARE @ThangTruoc AS int
DECLARE @NamTruoc AS int
IF @Thang = 1 
BEGIN
SET @ThangTruoc = 12;
SET @NamTruoc = @Nam - 1;
END
ELSE 
BEGIN
SET @ThangTruoc = @Thang - 1;
SET @NamTruoc = @Nam;
END
SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCNU_TT,PCTHD_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH CanBoThangTruoc as (
Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
From TL_DM_CanBo canbo
Where 
canbo.Thang = ' + CAST(@ThangTruoc AS VARCHAR(2)) + '
And canbo.Nam = ' + CAST(@NamTruoc AS VARCHAR(4)) + '
AND canbo.Parent IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
blt AS (
SELECT * FROM TL_BangLuong_Thang
WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (Ma_Hieu_CanBo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = Ma_DonVi)))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
FROM blt bangLuong
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
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
canBo.Nam_TN AS NTN,
dbo.f_luong_ntn(canBo.Ngay_NN, canBo.Ngay_XN, canBo.Ngay_TN, canBo.Thang_TNN, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') AS NamTn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
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
canBo.NamTN,
canBo.Tnn,
canBo.NTN,
canBo.XauNoiMa
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
;
;
;
;
;
GO
