/****** Object:  StoredProcedure [dbo].[sp_rpt_qt_chungtu_chitiet_hd4554_all]    Script Date: 11/8/2024 2:33:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qt_chungtu_chitiet_hd4554_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qt_chungtu_chitiet_hd4554_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qt_chungtu_chitiet_hd4554_all]    Script Date: 11/8/2024 2:33:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[sp_rpt_qt_chungtu_chitiet_hd4554_all]
	@chungTuIds nvarchar(max),
	@namLamViec int,
	@namNganSach int,
	@nguonNganSach int,
	@lns nvarchar(max),
	@donVi nvarchar(max),
	@dvt int,
	@thangQuyLoai int,
	@thangQuy nvarchar(20),

	@userName nvarchar(50)
AS
BEGIN


		select 
			sLNS, 
			ctct.iID_MaDonVi as IIDMaDonVi,
			Sum(fSoTien) as fSoTien ,
			dv.sTenDonVi,
			--ctct.iID_TN_QTChungTu,
			ctct.sM,
			ctct.sL,
			ctct.sK,
			ctct.sM,
			ctct.sTM,
			ctct.sTNG,
			ctct.sTNG1,
			ctct.sTNG2,
			ctct.sTNG3,
			ctct.sTTM,
			ctct.sXauNoiMa
		from TN_QuyetToan_ChungTuChiTiet_HD4554 ctct
		left join DonVi dv on ctct.iID_MaDonVi=dv.iID_MaDonVi
		where ctct.iNamLamViec = @namLamViec 
		and ctct.iNamNganSach = @namNganSach 
		and ctct.iNguonNganSach = @nguonNganSach
		and ctct.iID_MaDonVi in (select * from f_split(@donVi))
		and iID_TN_QTChungTu in  (select * from f_split(@chungTuIds))
		and dv.iNamLamViec=@namLamViec
		and dv.iTrangThai=1
		and ctct.iThangQuyLoai =@thangQuyLoai
		and ctct.iThangQuy in (select * from splitstring(@thangQuy))
		group by sLNS,ctct.iID_MaDonVi,dv.sTenDonVi,ctct.sM,
			ctct.sL,
			ctct.sK,
			ctct.sM,
			ctct.sTM,
			ctct.sTNG,
			ctct.sTNG1,
			ctct.sTNG2,
			ctct.sTNG3,
			ctct.sTTM,
			ctct.sXauNoiMa
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
;
;
;
;
;
GO
