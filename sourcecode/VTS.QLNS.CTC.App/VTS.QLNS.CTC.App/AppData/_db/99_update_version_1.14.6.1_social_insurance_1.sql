
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khtm_tong_hop]    Script Date: 6/12/2024 10:24:39 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_khtm_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_khtm_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khtm]    Script Date: 6/12/2024 10:24:39 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_khtm]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_khtm]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_kht_tong_hop]    Script Date: 6/12/2024 10:24:39 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_kht_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_kht_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_kht]    Script Date: 6/12/2024 10:24:39 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_kht]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_kht]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_kht]    Script Date: 6/12/2024 10:24:39 AM ******/
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
		ctct.fThu_BHXH_NLD,
		ctct.fThu_BHXH_NSD,
		ctct.fThu_BHYT_NLD,
		ctct.fThu_BHYT_NSD,
		ctct.fThu_BHTN_NLD,
		ctct.fThu_BHTN_NSD
	from
	BH_KHT_BHXH_ChiTiet ctct 
	join
	(select * from BH_KHT_BHXH
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHT_BHXH = ct.iID_KHT_BHXH
	join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS and mlns.iNamLamViec = @NamLamViec

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_kht_tong_hop]    Script Date: 6/12/2024 10:24:39 AM ******/
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
		ctct.fThu_BHXH_NLD,
		ctct.fThu_BHXH_NSD,
		ctct.fThu_BHYT_NLD,
		ctct.fThu_BHYT_NSD,
		ctct.fThu_BHTN_NLD,
		ctct.fThu_BHTN_NSD
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khtm]    Script Date: 6/12/2024 10:24:39 AM ******/
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
		ctct.fThanhTien
	from
	BH_KHTM_BHYT_ChiTiet ctct 
	join
	(select * from BH_KHTM_BHYT
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHTM_BHYT = ct.iID_KHTM_BHYT
	join BH_DM_MucLucNganSach mlns on ctct.iID_NoiDung = mlns.iID_MLNS and mlns.iNamLamViec = @NamLamViec

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khtm_tong_hop]    Script Date: 6/12/2024 10:24:39 AM ******/
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
		ctct.fThanhTien
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
GO


DELETE FROM DM_ChuKy WHERE Id_Code = 'rptBHXH_KHT_ChiTiet'
GO
INSERT INTO DM_ChuKy (Id, bDanhSach, ChucDanh1, ChucDanh1_MoTa, ChucDanh2, ChucDanh2_MoTa, ChucDanh3, ChucDanh3_MoTa, ChucDanh4, ChucDanh4_MoTa, ChucDanh5, ChucDanh5_MoTa, ChucDanh6, ChucDanh6_MoTa, DateCreated, DateModified, INamLamViec, iTrangThai, Id_Code, Id_Old_Type, Id_Type, KyHieu, [Log], MoTa, sKinhGuiCQTTBQP, sKinhGuiKBNN, sLoai, Tag, Ten, Ten1, Ten1_MoTa, Ten2, Ten2_MoTa, Ten3, Ten3_MoTa, Ten4, Ten4_MoTa, Ten5, Ten5_MoTa, Ten6, Ten6_MoTa, ThuaLenh1, ThuaLenh1_MoTa, ThuaLenh2, ThuaLenh2_MoTa, ThuaLenh3, ThuaLenh3_MoTa, ThuaLenh4, ThuaLenh4_MoTa, ThuaLenh5, ThuaLenh5_MoTa, ThuaLenh6, ThuaLenh6_MoTa, TieuDe1, TieuDe1_MoTa, TieuDe2, TieuDe2_MoTa, TieuDe3_MoTa, UserCreator, UserModifier, LoaiDVBanHanh1, LoaiDVBanHanh2, TenDVBanHanh1, TenDVBanHanh2, ThuaUyQuyen1, ThuaUyQuyen1_MoTa, ThuaUyQuyen2, ThuaUyQuyen2_MoTa, ThuaUyQuyen3, ThuaUyQuyen3_MoTa) 
VALUES ( newid(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, GETDATE(), GETDATE(), NULL, 1, N'rptBHXH_KHT_ChiTiet', NULL, N'rptBHXH_KHT_ChiTiet', NULL, NULL, NULL, NULL, NULL, N'KHT_BHXH_BHYT_BHTN', NULL, N'Báo cáo kế hoạch thu BHXH, BHYT, BHTN', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'Dự toán thu BHXH, BHYT, BHTN', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL )

GO
UPDATE DM_ChuKy SET Ten = N'Báo cáo kế hoạch thu mua BHYT thân nhân'
WHERE sLoai = 'KHTM_BHYT_THANNHAN'
AND bDanhSach = 1
GO