/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khtm_tong_hop]    Script Date: 12/13/2024 3:09:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_khtm_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_khtm_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khtm]    Script Date: 12/13/2024 3:09:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_khtm]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_khtm]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_kht_tong_hop]    Script Date: 12/13/2024 3:09:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_kht_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_kht_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_kht]    Script Date: 12/13/2024 3:09:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_kht]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_kht]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_kht]    Script Date: 12/13/2024 3:09:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_get_data_kht]
	@NamLamViec int,
	@SoChungTu nvarchar(100)
AS
BEGIN
	select
		ct.iID_MaDonVi,
		mlns.sXauNoiMa XauNoiMa,
		ctct.iID_MucLucNganSach,
		(ROUND(ctct.fThu_BHXH_NLD / 1000000, 0) * 1000000) fThu_BHXH_NLD,
		(ROUND(ctct.fThu_BHXH_NSD / 1000000, 0) * 1000000) fThu_BHXH_NSD,
		(ROUND(ctct.fThu_BHYT_NLD / 1000000, 0) * 1000000) fThu_BHYT_NLD,
		(ROUND(ctct.fThu_BHYT_NSD / 1000000, 0) * 1000000) fThu_BHYT_NSD,
		(ROUND(ctct.fThu_BHTN_NLD / 1000000, 0) * 1000000) fThu_BHTN_NLD,
		(ROUND(ctct.fThu_BHTN_NSD / 1000000, 0) * 1000000) fThu_BHTN_NSD
	from
	BH_KHT_BHXH_ChiTiet ctct 
	join
	(select * from BH_KHT_BHXH
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHT_BHXH = ct.iID_KHT_BHXH
	join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS and mlns.iNamLamViec = @NamLamViec

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_kht_tong_hop]    Script Date: 12/13/2024 3:09:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_get_data_kht_tong_hop] 
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	
	select
		ct.iID_MaDonVi,
		mlns.sXauNoiMa XauNoiMa,
		(ROUND(ctct.fThu_BHXH_NLD / 1000000, 0) * 1000000) fThu_BHXH_NLD,
		(ROUND(ctct.fThu_BHXH_NSD / 1000000, 0) * 1000000) fThu_BHXH_NSD,
		(ROUND(ctct.fThu_BHYT_NLD / 1000000, 0) * 1000000) fThu_BHYT_NLD,
		(ROUND(ctct.fThu_BHYT_NSD / 1000000, 0) * 1000000) fThu_BHYT_NSD,
		(ROUND(ctct.fThu_BHTN_NLD / 1000000, 0) * 1000000) fThu_BHTN_NLD,
		(ROUND(ctct.fThu_BHTN_NSD / 1000000, 0) * 1000000) fThu_BHTN_NSD
	from BH_KHT_BHXH_ChiTiet ctct
	join
	(select top 1 * from BH_KHT_BHXH
		where iNamLamViec = @NamLamViec
		and iID_MaDonVi = @MaDonVi
		--and sTongHop is not null
		and iLoaiTongHop = 2
		and bIsKhoa = 1) ct on ctct.iID_KHT_BHXH = ct.iID_KHT_BHXH
	join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS and mlns.iNamLamViec = @NamLamViec

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khtm]    Script Date: 12/13/2024 3:09:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_get_data_khtm] 
	@NamLamViec int,
	@SoChungTu nvarchar(100)
AS
BEGIN
	
	select
		ct.iID_MaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS,
		(ROUND(ctct.fThanhTien / 1000000, 0) * 1000000) fThanhTien
	from
	BH_KHTM_BHYT_ChiTiet ctct 
	join
	(select * from BH_KHTM_BHYT
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHTM_BHYT = ct.iID_KHTM_BHYT
	join BH_DM_MucLucNganSach mlns on ctct.iID_NoiDung = mlns.iID_MLNS and mlns.iNamLamViec = @NamLamViec

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khtm_tong_hop]    Script Date: 12/13/2024 3:09:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_get_data_khtm_tong_hop]
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	select
		ct.iID_MaDonVi,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		(ROUND(ctct.fThanhTien / 1000000, 0) * 1000000) fThanhTien -- lam tron hang trieu
	from BH_KHTM_BHYT_ChiTiet ctct
	join
	(select top 1 * from BH_KHTM_BHYT
		where iNamLamViec = @NamLamViec
		and iID_MaDonVi = @MaDonVi
		and sTongHop is not null
		and bKhoa = 1) ct on ctct.iID_KHTM_BHYT = ct.iID_KHTM_BHYT
	join BH_DM_MucLucNganSach mlns on ctct.iID_NoiDung = mlns.iID_MLNS and mlns.iNamLamViec = @NamLamViec

END
;
;
GO
