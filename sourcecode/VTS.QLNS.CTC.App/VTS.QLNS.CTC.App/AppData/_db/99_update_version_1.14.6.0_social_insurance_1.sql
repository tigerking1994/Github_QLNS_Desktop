/****** Object:  StoredProcedure [dbo].[sp_tl_bhxh_export_can_bo_che_do]    Script Date: 6/7/2024 4:25:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bhxh_export_can_bo_che_do]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bhxh_export_can_bo_che_do]
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_bhxh_import_qtc_bhxh]    Script Date: 6/7/2024 4:25:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_luong_bhxh_import_qtc_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_luong_bhxh_import_qtc_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_bhxh_import_qtc_bhxh]    Script Date: 6/7/2024 4:25:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_luong_bhxh_import_qtc_bhxh]
	@YearOfWork int,
	@Months nvarchar(20)
AS
BEGIN
	select chedo.sXauNoiMaMlnsBHXH, luong.*
	into luong_temp
	from TL_BangLuong_ThangBHXH luong
	left join TL_DM_CheDoBHXH chedo
		on luong.sMaCheDo = chedo.sMaCheDo
	where 
		luong.iNam = @YearOfWork
		and luong.iThang in (SELECT * FROM f_split(@Months))
		and luong.sMaCheDo in 
		(select distinct sMaCheDo from TL_DM_CheDoBHXH
		where sMaCheDoCha is not null and sMaCheDoCha <> ''
		and sMaCheDo not in (select distinct sMaCheDoCha from TL_DM_CheDoBHXH)
		and (upper(sMaCheDo) not like '%_HS%' and upper(sMaCheDo) not like '%_HESO%'))

	--Thong tin luong Si quan
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_sq
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '1%'
			group by
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong QNCN
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_qncn
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '2%'
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong HSQ_BS
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_hsq_bs
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '0%'
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong VCQP
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_vcqp
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB in ('3.1', '3.2', '3.3')
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong hdld
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_hdld
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB = '43'
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Ket qua
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo MaCheDo,
		sq.SoNguoi SoNguoiSQ,
		sq.SoTien SoTienSQ,
		qncn.SoTien SoTienQNCN,
		qncn.SoNguoi SoNguoiQNCN,
		hsq.SoTien SoTienHSQ,
		hsq.SoNguoi SoNguoiHSQ,
		vcqp.SoTien SoTienVCQP,
		vcqp.SoNguoi SoNguoiVCQP,
		hdld.SoTien SoTienHDLD,
		hdld.SoNguoi SoNguoiHDLD
	from luong_temp temp
	left join luong_temp_sq sq on temp.sMaCheDo = sq.sMaCheDo
	left join luong_temp_qncn qncn on temp.sMaCheDo = qncn.sMaCheDo
	left join luong_temp_hsq_bs hsq on temp.sMaCheDo = hsq.sMaCheDo
	left join luong_temp_vcqp vcqp on temp.sMaCheDo = vcqp.sMaCheDo
	left join luong_temp_hdld hdld on temp.sMaCheDo = hdld.sMaCheDo
	where isnull(temp.sXauNoiMaMlnsBHXH, '') <> ''

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp]') AND type in (N'U'))
	drop table luong_temp;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_sq]') AND type in (N'U'))
	drop table luong_temp_sq;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_qncn]') AND type in (N'U'))
	drop table luong_temp_qncn;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_hsq_bs]') AND type in (N'U'))
	drop table luong_temp_hsq_bs;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_vcqp]') AND type in (N'U'))
	drop table luong_temp_vcqp;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_hdld]') AND type in (N'U'))
	drop table luong_temp_hdld;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bhxh_export_can_bo_che_do]    Script Date: 6/7/2024 4:25:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bhxh_export_can_bo_che_do]
	@YearOfWork int,
	@Months nvarchar(20)
AS
BEGIN
	select distinct
		chedo.sXauNoiMaMlnsBHXH,
		canbo.Ma_Hieu_Canbo sMaHieuCanBo,
		canbo.Ten_CanBo sTenCanBo,
		canbo.Ten_DonVi sTenDonVi,
		canbo.Parent sMaDonVi,
		cbcd.sMaCanBo,
		cb.Ma_Cb SMaCapBac,
		cb.Ten_Cb STenCapBac,
		cbcd.sMaCheDo sMaCheDo,
		cbcd.fSoNgayHuongBHXH,
		cbcd.sSoQuyetDinh,
		cbcd.dNgayQuyetDinh,
		cbcd.iThangLuongCanCuDong,
		isnull(cbcd.fSoTien, 0) fSoTien,
		isnull(cbcd.fGiaTriCanCu, 0) fGiaTriCanCu
	from TL_CanBo_CheDoBHXH cbcd
		join TL_BangLuong_ThangBHXH luong on cbcd.sMaCanBo = luong.sMaCBo and cbcd.iNam = luong.iNam and cbcd.iThang = luong.iThang
		left join TL_DM_CanBo canbo on cbcd.sMaCanBo = canbo.Ma_CanBo
		join (
			select canbo.Ma_CanBo,
				capbac.Ma_Cb,
				capbac.Note Ten_Cb
			from TL_DM_CanBo canbo
			join TL_DM_CapBac capbac
			on canbo.Ma_CB = capbac.Ma_Cb
		) cb on cbcd.sMaCanBo = cb.Ma_CanBo
		left join TL_DM_CheDoBHXH chedo
			on cbcd.sMaCheDo = chedo.sMaCheDo
	where cbcd.iNam = @YearOfWork
		and cbcd.iThang in (SELECT * FROM f_split(@Months))
		and isnull(chedo.sXauNoiMaMlnsBHXH, '') <> ''
	order by canbo.Ma_Hieu_Canbo desc

END
;
;
GO


update BH_DM_MucLucNganSach
set sNS_LuongChinh = '1010000-010-011-6000-6001-10-00'
where sXauNoiMa in ('9020001-010-011-0001-0000', '9020002-010-011-0001-0000')
and isnull(sNS_LuongChinh, '') = ''
GO

update BH_DM_MucLucNganSach
set sNS_PCCV = '1010000-010-011-6100-6101-10-00'
where sXauNoiMa in ('9020001-010-011-0001-0000', '9020002-010-011-0001-0000')
and isnull(sNS_PCCV, '') = ''
GO

update BH_DM_MucLucNganSach
set sNS_LuongChinh = '1010000-010-011-6000-6001-20-00'
where sXauNoiMa in ('9020001-010-011-0001-0001', '9020002-010-011-0001-0001')
and isnull(sNS_LuongChinh, '') = ''
GO

update BH_DM_MucLucNganSach
set sNS_PCCV = '1010000-010-011-6100-6101-20-00'
where sXauNoiMa in ('9020001-010-011-0001-0001', '9020002-010-011-0001-0001')
and isnull(sNS_PCCV, '') = ''
GO
update BH_DM_MucLucNganSach
set sNS_LuongChinh = '1010000-010-011-6400-6449-10-00'
where sXauNoiMa in ('9020001-010-011-0001-0002', '9020002-010-011-0001-0002')
and isnull(sNS_LuongChinh, '') = ''
GO

update BH_DM_MucLucNganSach
set sNS_PCCV = '1010000-010-011-6400-6449-30-00'
where sXauNoiMa in ('9020001-010-011-0001-0002', '9020002-010-011-0001-0002')
and isnull(sNS_PCCV, '') = ''
GO

update BH_DM_MucLucNganSach
set sNS_LuongChinh = '1010000-010-011-6000-6001-30-00,1010000-010-011-6000-6001-40-00,1010000-010-011-6000-6001-70-00'
where sXauNoiMa in ('9020001-010-011-0002-0000', '9020002-010-011-0002-0000')
and isnull(sNS_LuongChinh, '') = ''
GO

update BH_DM_MucLucNganSach
set sNS_PCCV = '1010000-010-011-6100-6101-30-00,1010000-010-011-6100-6101-40-00,1010000-010-011-6100-6101-70-00'
where sXauNoiMa in ('9020001-010-011-0002-0000', '9020002-010-011-0002-0000')
and isnull(sNS_PCCV, '') = ''
GO

