/****** Object:  StoredProcedure [dbo].[sp_qt_qt_chutuchitiet_nq104]    Script Date: 4/10/2024 8:16:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_qt_chutuchitiet_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_qt_chutuchitiet_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_dieu_chinh_dtt_don_vi_tong_hop]    Script Date: 4/10/2024 8:16:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_dieu_chinh_dtt_don_vi_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_dieu_chinh_dtt_don_vi_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_dieu_chinh_dtt_don_vi_tong_hop]    Script Date: 4/10/2024 8:16:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_get_data_dieu_chinh_dtt_don_vi_tong_hop] 
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	select distinct
		ct.iID_MaDonVi,
		mlns.sXauNoiMa,
		ctct.fThuBHXH_NLD_Tang,
		ctct.fThuBHXH_NLD_Giam,
		ctct.fThuBHXH_NSD_Tang,
		ctct.fThuBHXH_NSD_Giam,
		ctct.fThuBHYT_NLD_Tang,
		ctct.fThuBHYT_NLD_Giam,
		ctct.fThuBHYT_NSD_Tang,
		ctct.fThuBHYT_NSD_Giam,
		ctct.fThuBHTN_NLD_Tang,
		ctct.fThuBHTN_NLD_Giam,
		ctct.fThuBHTN_NSD_Tang,
		ctct.fThuBHTN_NSD_Giam
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join
	(select * from BH_DTT_BHXH_DieuChinh
		where iNamLamViec = @NamLamViec
		and iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		--and sTongHop is not null
		and iLoaiTongHop = 1
		and bDaTongHop = 1) ct on ctct.iID_DTT_BHXH_DieuChinh = ct.iID_DTT_BHXH_DieuChinh
	join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_qt_chutuchitiet_nq104]    Script Date: 4/10/2024 8:16:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_qt_qt_chutuchitiet_nq104]
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
		select ctct.MLNS_Id_Parent MlnsIdParent, XauNoiMa, MoTa, NamLamViec, 
		sum(ctct.TongSo ) TongSo, sum(ctct.ThieuUy) ThieuUy, sum(ctct.TrungUy) TrungUy, sum(ctct.ThuongUy) ThuongUy, sum(ctct.DaiUy) DaiUy,sum(ctct.ThieuTa) ThieuTa, sum(ctct.TrungTa) TrungTa, sum(ctct.ThuongTa) ThuongTa, sum(ctct.DaiTa) DaiTa, sum(ctct.Tuong) Tuong,
		sum(ctct.BinhNhi) BinhNhi, sum(ctct.BinhNhat) BinhNhat, sum(ctct.HaSi) HaSi, sum(ctct.TrungSi) TrungSi, sum(ctct.ThuongSi ) ThuongSi,
		sum(ctct.ThieuUyCn) ThieuUyCn, sum(ctct.TrungUycn) TrungUycn, sum(ctct.ThuongUyCn) ThuongUyCn, sum(ctct.DaiUyCn) DaiUyCn,sum(ctct.ThieuTaCn) ThieuTaCn, sum(ctct.TrungTaCn) TrungTaCn, sum(ctct.ThuongTaCn) ThuongTaCn,
		sum(ctct.CNQP) CNQP, sum(ctct.LDHD) LDHD, sum(ctct.VCQP) VCQP, sum(ctct.QNCN) Qncn, sum(ctct.CCQP) Ccqp
from  tl_qs_chungtuchitiet_nq104 ctct 
where 1=1  
and ctct.Thang in  (SELECT * FROM f_split(@strThang))
and ctct.NamLamViec = @strNam
and ctct.Id_DonVi in  (SELECT * FROM f_split(@strIdDonVi))
group by  xaunoima, mota , NamLamViec, MLNS_Id_Parent
)
select * from  thang  order by  xaunoima;
end 

else
WITH ThoiGianTruoc as (
select  ctct.MLNS_Id_Parent MlnsIdParent, XauNoiMa, 
		case 
			when (select count(*) from f_split(@strThangTruoc)) = 1 then MoTa
			when (select count(*) from f_split(@strThangTruoc)) = 3 then replace(MoTa, N'tháng', N'quý')
			when (select count(*) from f_split(@strThangTruoc)) = 12 then replace(MoTa, N'tháng', N'năm')
		end as MoTa,
		NamLamViec, sum(ctct.TongSo ) TongSo, sum(ctct.ThieuUy) ThieuUy, sum(ctct.TrungUy) TrungUy, sum(ctct.ThuongUy) ThuongUy, sum(ctct.DaiUy) DaiUy,sum(ctct.ThieuTa) ThieuTa, sum(ctct.TrungTa) TrungTa, sum(ctct.ThuongTa) ThuongTa, sum(ctct.DaiTa) DaiTa, sum(ctct.Tuong) Tuong,
		sum(ctct.BinhNhi) BinhNhi, sum(ctct.BinhNhat) BinhNhat, sum(ctct.HaSi) HaSi, sum(ctct.TrungSi) TrungSi, sum(ctct.ThuongSi ) ThuongSi,
		sum(ctct.ThieuUyCn) ThieuUyCn, sum(ctct.TrungUycn) TrungUycn, sum(ctct.ThuongUyCn) ThuongUyCn, sum(ctct.DaiUyCn) DaiUyCn,sum(ctct.ThieuTaCn) ThieuTaCn, sum(ctct.TrungTaCn) TrungTaCn, sum(ctct.ThuongTaCn) ThuongTaCn,
		sum(ctct.CNQP) CNQP, sum(ctct.LDHD) LDHD, sum(ctct.VCQP) VCQP, sum(ctct.QNCN) Qncn, sum(ctct.CCQP) Ccqp
from  tl_qs_chungtuchitiet_nq104 ctct 
where 1=1  
and ctct.Thang in (SELECT * FROM f_split(@strThangTruoc))
and ctct.NamLamViec = @strNamTruoc
and ctct.Id_DonVi in  (SELECT * FROM f_split(@strIdDonVi))
and XauNoiMa in ('100','500')
group by  xaunoima, mota , NamLamViec, MLNS_Id_Parent
),
ThoiGianNay  as(
select  ctct.MLNS_Id_Parent MlnsIdParent, XauNoiMa, 
		case 
			when (select count(*) from f_split(@strThang)) = 1 then MoTa
			when (select count(*) from f_split(@strThang)) = 3 then replace(MoTa, N'tháng', N'quý')
			when (select count(*) from f_split(@strThang)) = 12 then replace(MoTa, N'tháng', N'năm')
		end as MoTa,
		NamLamViec, sum(ctct.TongSo ) TongSo, sum(ctct.ThieuUy) ThieuUy, sum(ctct.TrungUy) TrungUy, sum(ctct.ThuongUy) ThuongUy, sum(ctct.DaiUy) DaiUy,sum(ctct.ThieuTa) ThieuTa, sum(ctct.TrungTa) TrungTa, sum(ctct.ThuongTa) ThuongTa, sum(ctct.DaiTa) DaiTa, sum(ctct.Tuong) Tuong,
		sum(ctct.BinhNhi) BinhNhi, sum(ctct.BinhNhat) BinhNhat, sum(ctct.HaSi) HaSi, sum(ctct.TrungSi) TrungSi, sum(ctct.ThuongSi ) ThuongSi,
		sum(ctct.ThieuUyCn) ThieuUyCn, sum(ctct.TrungUycn) TrungUycn, sum(ctct.ThuongUyCn) ThuongUyCn, sum(ctct.DaiUyCn) DaiUyCn,sum(ctct.ThieuTaCn) ThieuTaCn, sum(ctct.TrungTaCn) TrungTaCn, sum(ctct.ThuongTaCn) ThuongTaCn,
		sum(ctct.CNQP) CNQP, sum(ctct.LDHD) LDHD, sum(ctct.VCQP) VCQP, sum(ctct.QNCN) Qncn, sum(ctct.CCQP) Ccqp
from  tl_qs_chungtuchitiet_nq104 ctct 
where 1=1  
and ctct.Thang in  (SELECT * FROM f_split(@strThang))
and ctct.NamLamViec = @strNam
and ctct.Id_DonVi in  (SELECT * FROM f_split(@strIdDonVi))
and  XauNoiMa not in ('100','500')
group by  xaunoima, mota , NamLamViec, MLNS_Id_Parent
)
select * from ThoiGianTruoc  
union all 
select * from ThoiGianNay 
order by xaunoima;
END
;
;
GO
