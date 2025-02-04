/****** Object:  StoredProcedure [dbo].[sp_luong_bhxh_import_qtc_bhxh]    Script Date: 12/12/2023 10:06:28 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_luong_bhxh_import_qtc_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_luong_bhxh_import_qtc_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_luong_get_che_do]    Script Date: 12/12/2023 10:06:28 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_luong_get_che_do]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_luong_get_che_do]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_luong_get_che_do]    Script Date: 12/12/2023 10:06:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_luong_get_che_do]
	@MaCanBo nvarchar(100)
AS
BEGIN
	select
		chedo.sMaCheDo,
		chedo.sMaCheDoCha,
		chedo.sTenCheDo,
		chedo.sXauNoiMa,
		case when (chedo.sMaCheDoCha = '' 
					or chedo.sMaCheDoCha is null 
					or chedo.sMaCheDo in (select distinct sMaCheDoCha from TL_DM_CheDoBHXH)) then 1 
				else 0 
		end as IsHangCha,
		canbo.*
	from 
	TL_CanBo_CheDoBHXH canbo
	right join TL_DM_CheDoBHXH chedo on canbo.sMaCheDo = chedo.sMaCheDo and canbo.sMaCanBo = @MaCanBo
	where chedo.sMaCheDo <> 'SONGAYHUONG'
	and chedo.bDisplay = 1
	order by chedo.iLoaiCheDo, chedo.sXauNoiMa

	--select
	--	chedo.sMaCheDo,
	--	chedo.sMaCheDoCha,
	--	chedo.sTenCheDo,
	--	CONCAT(chedocha.sMaCheDo, '-', chedocha.sTenCheDo) AS ParentName,
	--	canbo.*
	--from 
	--TL_DM_CheDoBHXH chedo 
	--left join TL_CanBo_CheDoBHXH canbo on canbo.sMaCheDo = chedo.sMaCheDo and canbo.sMaCanBo = @MaCanBo
	--left join TL_DM_CheDoBHXH chedocha on chedo.sMaCheDoCha = chedocha.sMaCheDo 
	--and chedo.sMaCheDoCha IN (select sMaCheDo from TL_DM_CheDoBHXH where sMaCheDoCha = '' or sMaCheDoCha is null)
	--where chedo.sMaCheDo <> 'SONGAYHUONG'
	--Order By ParentName
END
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_bhxh_import_qtc_bhxh]    Script Date: 12/12/2023 10:06:28 AM ******/
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
	left join luong_temp_qncn qncn on sq.sMaCheDo = qncn.sMaCheDo
	left join luong_temp_hsq_bs hsq  on sq.sMaCheDo = hsq.sMaCheDo
	left join luong_temp_vcqp vcqp on sq.sMaCheDo = vcqp.sMaCheDo
	left join luong_temp_hdld hdld on sq.sMaCheDo = hdld.sMaCheDo

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
GO
