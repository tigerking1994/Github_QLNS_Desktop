/****** Object:  StoredProcedure [dbo].[sp_dt_phanbochi_getdieuchinhthu_BHYTQN_clone]    Script Date: 4/22/2024 5:23:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_phanbochi_getdieuchinhthu_BHYTQN_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_phanbochi_getdieuchinhthu_BHYTQN_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN_clone2]    Script Date: 4/22/2024 5:23:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN_clone2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN_clone2]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN_clone2]    Script Date: 4/22/2024 5:23:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN_clone2]
	@NamLamViec int,
	@IdDonVi nvarchar(200)
AS
BEGIN
	select
	'9010004' sXauNoiMa,
	sum(0.1*(a.fThu_BHYT_NLD+ a.fThu_BHYT_NSD)) as fTienTuChi
	from BH_DTT_BHXH_ChungTu_ChiTiet a join BH_DTT_BHXH_ChungTu b
	on a.iID_DTT_BHXH = b.iID_DTT_BHXH
	where
	(a.sXauNoiMa like '9020001-010-011-0001%' or a.sXauNoiMa like '9020002-010-011-0001%')
	and a.iNamLamViec = @NamLamViec
	and b.iLoaiDuToan = 2
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
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbochi_getdieuchinhthu_BHYTQN_clone]    Script Date: 4/22/2024 5:23:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_phanbochi_getdieuchinhthu_BHYTQN_clone]
	@NamLamViec int,
	@IdDonVi nvarchar(200)
AS
BEGIN
	select
	'9010004' sXauNoiMa,
	a.iID_MaDonVi,
	sum(0.1*(a.fBHYT_NLD+ a.fBHYT_NSD)) as fTienTuChi
	from BH_DTT_BHXH_PhanBo_ChungTuChiTiet a join BH_DTT_BHXH_PhanBo_ChungTu b
	on a.iID_DTT_BHXH_ChungTu = b.iID_DTT_BHXH_PhanBo_ChungTu
	where
	(a.sXauNoiMa like '9020001-010-011-0001%' or a.sXauNoiMa like '9020002-010-011-0001%')
	and a.iNamLamViec = @NamLamViec
	and a.iID_MaDonVi in (select * from splitstring(@IdDonVi))
	and b.iLoaiDuToan = 2
	group by  a.iID_MaDonVi
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
GO
