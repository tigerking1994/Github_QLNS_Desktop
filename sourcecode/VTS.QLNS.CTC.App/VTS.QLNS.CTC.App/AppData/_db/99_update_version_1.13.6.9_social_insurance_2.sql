/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kpql_slns]    Script Date: 12/19/2023 5:02:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kpql_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kpql_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_khac_slns]    Script Date: 12/19/2023 5:02:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_khac_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_khac_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]    Script Date: 12/19/2023 5:02:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_Bhxh_slns]    Script Date: 12/19/2023 5:02:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_Bhxh_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_Bhxh_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiao_index]    Script Date: 12/19/2023 5:02:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiao_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiao_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikhac_index]    Script Date: 12/19/2023 5:02:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_kehoach_chikhac_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_kehoach_chikhac_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikhac_index]    Script Date: 12/19/2023 5:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị lâp kế hoạch chi các chế dộ BHXH

-- =============================================
CREATE PROCEDURE [dbo].[bh_kehoach_chikhac_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 kcb.iID_BH_KHC_K
		, kcb.sSoChungTu
		, kcb.dNgayChungTu
		, kcb.sSoQuyetDinh
		, kcb.dNgayQuyetDinh
		, kcb.iNamLamViec
		, kcb.iID_DonVi
		, kcb.iID_MaDonVi
		, kcb.sMoTa
		, kcb.iIDLoaiChi
		, kcb.fTongTienDaThucHienNamTruoc
		, kcb.fTongTienUocThucHienNamTruoc
		, kcb.fTongTienKeHoachThucHienNamNay
		, kcb.sTongHop
		, kcb.iID_TongHopID
		, kcb.iLoaiTongHop
		, kcb.bIsKhoa
		, kcb.dNgaySua
		, kcb.dNgayTao
		, kcb.sNguoiSua
		, kcb.sNguoiTao
		, kcb.dNgayTao
		, kcb.bDaTongHop
		, kcb.sMaLoaiChi
		, donVi.sTenDonVi
		, dm.sTenDanhMucLoaiChi
		-- Tong dự toán todo
	FROM BH_KHC_K kcb
	LEFT JOIN DonVi donVi
		ON kcb.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = kcb.iID_DonVi
	LEFT JOIN BH_DM_LoaiChi dm ON kcb.iIDLoaiChi=dm.iID and kcb.iNamLamViec=dm.iNamLamViec
	ORDER BY kcb.sSoChungTu
END
;

GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiao_index]    Script Date: 12/19/2023 5:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[sp_bhxh_nhandutoanchitrengiao_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	DTC.ID,
	DTC.sSoChungTu,
	DTC.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	DTC.dNgayChungTu,
	DTC.sSoQuyetDinh,
	DTC.dNgayQuyetDinh,
	DTC.sLNS,
	DTC.fTongTienTuChi,
	DTC.fTongTienHienVat,
	DTC.fTongTien,
	DTC.iLoaiDotNhanPhanBo,
	DTC.sMoTa,
	DTC.iNamLamViec,
	DTC.sNguoiTao,
	DTC.bIsKhoa,
	BH_DM_LoaiChi.sTenDanhMucLoaiChi AS sLoaiChi,
	DTC.sMaLoaiChi
	FROM BH_DTC_DuToanChiTrenGiao DTC
	LEFT JOIN DonVi DV ON DTC.iID_MaDonVi = DV.iID_MaDonVi
	INNER JOIN BH_DM_LoaiChi ON DTC.iID_LoaiDanhMucChi = BH_DM_LoaiChi.iID
	WHERE DV.iNamLamViec = @YearOfWork and DTC.iNamLamViec = @YearOfWork
	ORDER BY DTC.dNgayQuyetDinh DESC
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_Bhxh_slns]    Script Date: 12/19/2023 5:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_Bhxh_slns]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

SELECT 
	A.sLNS,
	A.sTM,
	A.sTTM,
	A.sM,
	A.sNG,
	A.sMoTa AS sNoiDung,
	A.sXauNoiMa,
	A.iID_MLNS,
	A.iID_MLNS_Cha,
	A.bHangCha,
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTienKeHoachThucHienNamNay as fTongTien,
	A.iNamlamViec,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN (select	
		@iDNdtctg as iID_DTC_DuToanChiTrenGiao
		, ctct.iID_MucLucNganSach
		, ctct.fTienKeHoachThucHienNamNay
		from BH_KHC_CheDoBHXH_ChiTiet ctct
left join BH_KHC_CheDoBHXH ct on ctct.iID_KHC_CheDoBHXH=ct.ID
where (ct.iLoaiTongHop=2 or ct.bDaTongHop=1) and ct.iID_MaDonVi=@IIDDonVi
		and ct.iNamLamViec=@NamLamViec
		and ct.bIsKhoa=1) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
	order by A.sXauNoiMa
END



GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]    Script Date: 12/19/2023 5:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kcbqy_slns]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

SELECT 
	A.sLNS,
	A.sTM,
	A.sTTM,
	A.sM,
	A.sNG,
	A.sMoTa AS sNoiDung,
	A.sXauNoiMa,
	A.iID_MLNS,
	A.iID_MLNS_Cha,
	A.bHangCha,
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTienKeHoachThucHienNamNay as fTongTien,
	A.iNamlamViec,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN (select	
		@iDNdtctg as iID_DTC_DuToanChiTrenGiao
		, ctct.iID_MucLucNganSach
		, ctct.fTienKeHoachThucHienNamNay
		from BH_KHC_KCB_ChiTiet ctct
left join BH_KHC_KCB ct on ctct.iID_KHC_KCB=ct.iID_BH_KHC_KCB
where (ct.iLoaiTongHop=2 or ct.bDaTongHop=1) and ct.iID_MaDonVi=@IIDDonVi
		and ct.iNamLamViec=@NamLamViec
		and ct.bIsKhoa=1) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
	order by A.sXauNoiMa
END


GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_khac_slns]    Script Date: 12/19/2023 5:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_khac_slns]
@iDNdtctg uniqueidentifier,
@IDLoaiChi uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

SELECT 
	A.sLNS,
	A.sTM,
	A.sTTM,
	A.sM,
	A.sNG,
	A.sMoTa AS sNoiDung,
	A.sXauNoiMa,
	A.iID_MLNS,
	A.iID_MLNS_Cha,
	A.bHangCha,
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTienKeHoachThucHienNamNay as fTongTien,
	A.iNamlamViec,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN (select	
		@iDNdtctg as iID_DTC_DuToanChiTrenGiao
		, ctct.iID_MucLucNganSach
		, ctct.fTienKeHoachThucHienNamNay
		from BH_KHC_K_ChiTiet ctct
left join BH_KHC_K ct on ctct.iID_KHC_K=ct.iID_BH_KHC_K
where (ct.iLoaiTongHop=2 or ct.bDaTongHop=1) and ct.iID_MaDonVi=@IIDDonVi
		and ct.iIDLoaiChi=@IDLoaiChi
		and ct.iNamLamViec=@NamLamViec
		and ct.bIsKhoa=1) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
	order by A.sXauNoiMa
END


GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kpql_slns]    Script Date: 12/19/2023 5:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_kpql_slns]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

SELECT 
	A.sLNS,
	A.sTM,
	A.sTTM,
	A.sM,
	A.sNG,
	A.sMoTa AS sNoiDung,
	A.sXauNoiMa,
	A.iID_MLNS,
	A.iID_MLNS_Cha,
	A.bHangCha,
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTienKeHoachThucHienNamNay as fTongTien,
	A.iNamlamViec,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN (select	
		@iDNdtctg as iID_DTC_DuToanChiTrenGiao
		, ctct.iID_MucLucNganSach
		, ctct.fTienKeHoachThucHienNamNay
		from BH_KHC_KinhPhiQuanLy_ChiTiet ctct
left join BH_KHC_KinhPhiQuanLy ct on ctct.iID_KHC_KinhPhiQuanLy=ct.iID_BH_KHC_KinhPhiQuanLy
where (ct.iLoaiTongHop=2 or ct.bDaTongHop=1) and ct.iID_MaDonVi=@IIDDonVi
		and ct.iNamLamViec=@NamLamViec
		and ct.bIsKhoa=1) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
	order by A.sXauNoiMa
END


GO
