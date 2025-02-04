/****** Object:  StoredProcedure [dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh]    Script Date: 10/23/2024 10:01:38 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh]    Script Date: 10/23/2024 10:01:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh] 
	@MaDonVi nvarchar(max),
	@NamLamViec int,
	@IidChungTuNhan nvarchar(max)
AS
BEGIN

	declare @sDieuChinhTongHop nvarchar(max) = (
	select distinct STUFF((
	SELECT distinct ',' + iID_ChungTuDieuChinh
	from NS_DT_ChungTu where iID_DTChungTu in (SELECT * FROM f_split(@IidChungTuNhan))
	FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	,1,1,'') iID_ChungTuDieuChinh)

	declare @sSoChungTu nvarchar(max) = (
	select distinct STUFF((
	SELECT distinct ',' + sTongHop
	from NS_DC_ChungTu where iID_DCChungTu in (SELECT * FROM f_split(@sDieuChinhTongHop))
	FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	,1,1,'') sSoChungTu)

	select
		ctct.iID_DCCTChiTiet,
		ctct.bHangCha,
		ctct.dNgaySua,
		ctct.dNgayTao,
		ctct.fDuKienQtCuoiNam DuKienQtCuoiNam,
		ctct.fDuKienQtDauNam DuKienQtDauNam,
		ctct.iID_DCChungTu,
		ctct.iID_MaDonVi,
		ctct.iID_MaNguonNganSach,
		ctct.iID_MLNS,
		ctct.iID_MLNS_Cha,
		ctct.iNamLamViec,
		ctct.iNamNganSach,
		ctct.sGhiChu,
		ctct.sK,
		ctct.sL,
		ctct.sLNS,
		ctct.sM,
		ctct.sMoTa,
		ctct.sNG,
		ctct.sNguoiSua,
		ctct.sNguoiTao,
		ctct.sTM,
		ctct.sTNG,
		ctct.sTNG1,
		ctct.sTNG2,
		ctct.sTNG3,
		ctct.sTTM,
		ctct.sXauNoiMa,
		ctct.fDuToan DuToanNganSachNam,
		ctct.fDuToanChuyenNamSau DuToanChuyenNamSau

	from NS_DC_ChungTuChiTiet ctct
	join NS_DC_ChungTu ct on ctct.iID_DCChungTu = ct.iID_DCChungTu
	where ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ct.bDaTongHop = 1
		and ct.sSoChungTu in (SELECT * FROM f_split(@sSoChungTu))
END
GO
