/****** Object:  StoredProcedure [dbo].[sp_qs_tao_chitiet]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_tao_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_tao_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_tonghop]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_thuongxuyen]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_thuongxuyen]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_thuongxuyen]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_raquan_thangtruoc]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_raquan_thangtruoc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_raquan_thangtruoc]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_raquan]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_raquan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_raquan]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_lientham]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_lientham]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_lientham]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_donvi]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_chitiet_donvi]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_binhquan_thang]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_binhquan_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_binhquan_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_binhquan_donvi]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_binhquan_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_binhquan_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_get_donvi]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_chungtu_chitiet_tonghop]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_chungtu_chitiet_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_chungtu_chitiet_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_chungtu_chitiet]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_capnhat_chitiet_year_begin]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_capnhat_chitiet_year_begin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_capnhat_chitiet_year_begin]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_capnhat_chitiet]    Script Date: 15/12/2023 5:30:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_capnhat_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_capnhat_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_capnhat_chitiet]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_capnhat_chitiet]
	@YearOfWork int,
	@Month int,
	@IdMaDonVi nvarchar(50)
AS
BEGIN
	update ct1 set 
		ct1.fSoThieuUy  = ct2.fSoThieuUy ,
		ct1.fSoTrungUy  = ct2.fSoTrungUy ,
		ct1.fSoThuongUy  = ct2.fSoThuongUy ,
		ct1.fSoDaiUy  = ct2.fSoDaiUy ,
		ct1.fSoThieuTa  = ct2.fSoThieuTa ,
		ct1.fSoTrungTa  = ct2.fSoTrungTa ,
		ct1.fSoThuongTa  = ct2.fSoThuongTa ,
		ct1.fSoDaiTa  = ct2.fSoDaiTa ,
		ct1.fSoTuong = ct2.fSoTuong,
		ct1.fSoTSQ  = ct2.fSoTSQ ,
		ct1.fSoBinhNhi  = ct2.fSoBinhNhi ,
		ct1.fSoBinhNhat  = ct2.fSoBinhNhat ,
		ct1.fSoHaSi  = ct2.fSoHaSi ,
		ct1.fSoTrungSi  = ct2.fSoTrungSi ,
		ct1.fSoThuongSi  = ct2.fSoThuongSi ,
		ct1.fSoThuongTa_QNCN  = ct2.fSoThuongTa_QNCN ,
		ct1.fSoTrungTa_QNCN  = ct2.fSoTrungTa_QNCN ,
		ct1.fSoThieuTa_QNCN  = ct2.fSoThieuTa_QNCN ,
		ct1.fSoDaiUy_QNCN  = ct2.fSoDaiUy_QNCN ,
		ct1.fSoThuongUy_QNCN  = ct2.fSoThuongUy_QNCN ,
		ct1.fSoTrungUy_QNCN  = ct2.fSoTrungUy_QNCN ,
		ct1.fSoThieuUy_QNCN  = ct2.fSoThieuUy_QNCN ,
		ct1.fSoCNVQP  = ct2.fSoCNVQP ,
		ct1.fSoLDHD  = ct2.fSoLDHD ,
		ct1.fSoCNVQPCT  = ct2.fSoCNVQPCT ,
		ct1.fSoQNVQPHD  = ct2.fSoQNVQPHD ,
		ct1.fTongSo  = ct2.fTongSo ,
		ct1.fSoSQ_KH  = ct2.fSoSQ_KH ,
		ct1.fSoHSQBS_KH  = ct2.fSoHSQBS_KH ,
		ct1.fSoCNVQP_KH  = ct2.fSoCNVQP_KH ,
		ct1.fSoLDHD_KH  = ct2.fSoLDHD_KH ,
		ct1.fSoQNCN_KH  = ct2.fSoQNCN_KH ,
		ct1.fSoVCQP  = ct2.fSoVCQP ,
		ct1.fSoCY_H  = ct2.fSoCY_H ,
		ct1.fSoCY_KT = ct2.fSoCY_KT,
		ct1.fSoCcqp = ct2.fSoCcqp
	from 
	NS_QS_ChungTuChiTiet as ct1
	INNER JOIN (
		select * from NS_QS_ChungTuChiTiet 
		where iThangQuy = (@Month - 1) and iNamLamViec = @YearOfWork 
			and sKyHieu = '700' 
			and ((@IdMaDonVi <> '' and iID_MaDonVi = @IdMaDonVi) or @IdMaDonVi = '')
	) as ct2 on ct1.iID_MaDonVi = ct2.iID_MaDonVi and ct1.iNamLamViec = ct2.iNamLamViec
	where ct1.iThangQuy = @Month and ct1.sKyHieu in ('100', '700');

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_capnhat_chitiet_year_begin]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_capnhat_chitiet_year_begin]
	@YearOfWork int,
	@IdMaDonVi nvarchar(50)
AS
BEGIN
	update ct1 set 
		ct1.fSoThieuUy  = ct2.fSoThieuUy ,
		ct1.fSoTrungUy  = ct2.fSoTrungUy ,
		ct1.fSoThuongUy  = ct2.fSoThuongUy ,
		ct1.fSoDaiUy  = ct2.fSoDaiUy ,
		ct1.fSoThieuTa  = ct2.fSoThieuTa ,
		ct1.fSoTrungTa  = ct2.fSoTrungTa ,
		ct1.fSoThuongTa  = ct2.fSoThuongTa ,
		ct1.fSoDaiTa  = ct2.fSoDaiTa ,
		ct1.fSoTuong = ct2.fSoTuong,
		ct1.fSoTSQ  = ct2.fSoTSQ ,
		ct1.fSoBinhNhi  = ct2.fSoBinhNhi ,
		ct1.fSoBinhNhat  = ct2.fSoBinhNhat ,
		ct1.fSoHaSi  = ct2.fSoHaSi ,
		ct1.fSoTrungSi  = ct2.fSoTrungSi ,
		ct1.fSoThuongSi  = ct2.fSoThuongSi ,
		ct1.fSoThuongTa_QNCN  = ct2.fSoThuongTa_QNCN ,
		ct1.fSoTrungTa_QNCN  = ct2.fSoTrungTa_QNCN ,
		ct1.fSoThieuTa_QNCN  = ct2.fSoThieuTa_QNCN ,
		ct1.fSoDaiUy_QNCN  = ct2.fSoDaiUy_QNCN ,
		ct1.fSoThuongUy_QNCN  = ct2.fSoThuongUy_QNCN ,
		ct1.fSoTrungUy_QNCN  = ct2.fSoTrungUy_QNCN ,
		ct1.fSoThieuUy_QNCN  = ct2.fSoThieuUy_QNCN ,
		ct1.fSoCNVQP  = ct2.fSoCNVQP ,
		ct1.fSoLDHD  = ct2.fSoLDHD ,
		ct1.fSoCNVQPCT  = ct2.fSoCNVQPCT ,
		ct1.fSoQNVQPHD  = ct2.fSoQNVQPHD ,
		ct1.fTongSo  = ct2.fTongSo ,
		ct1.fSoSQ_KH  = ct2.fSoSQ_KH ,
		ct1.fSoHSQBS_KH  = ct2.fSoHSQBS_KH ,
		ct1.fSoCNVQP_KH  = ct2.fSoCNVQP_KH ,
		ct1.fSoLDHD_KH  = ct2.fSoLDHD_KH ,
		ct1.fSoQNCN_KH  = ct2.fSoQNCN_KH ,
		ct1.fSoVCQP  = ct2.fSoVCQP ,
		ct1.fSoCY_H  = ct2.fSoCY_H ,
		ct1.fSoCY_KT = ct2.fSoCY_KT,
		ct1.fSoCcqp = ct2.fSoCcqp
	from 
	NS_QS_ChungTuChiTiet as ct1
	INNER JOIN (
		select * from NS_QS_ChungTuChiTiet 
		where iThangQuy = 12 and iNamLamViec = @YearOfWork - 1
			and ((@IdMaDonVi <> '' and iID_MaDonVi = @IdMaDonVi) or @IdMaDonVi = '')
	) as ct2 on ct1.iID_MaDonVi = ct2.iID_MaDonVi 
	and ct1.sKyHieu in ('100', '700')	
	and ct2.sKyHieu = '700'
	--and ct1.sKyHieu = ct2.sKyHieu
	where ct1.iThangQuy = 0;

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_chungtu_chitiet]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qs_chungtu_chitiet]
	@YearOfWork int,
	@VoucherId nvarchar(100),
	@AgencyId nvarchar(20)
AS
BEGIN
	SELECT iID_QSCTChiTiet,
		   sM,
		   ml.sKyHieu,
		   sMoTa,
		   bHangCha,
		   iID_MaDonVi,
		   fSoThieuUy,
		   fSoTrungUy,
		   fSoThuongUy,
		   fSoDaiUy,
		   fSoThieuTa,
		   fSoTrungTa,
		   fSoThuongTa,
		   fSoDaiTa,
		   fSoTuong,
		   fSoTSQ,
		   fSoBinhNhi,
		   fSoBinhNhat,
		   fSoHaSi,
		   fSoTrungSi,
		   fSoThuongSi,
		   fSoThuongTa_QNCN,
		   fSoTrungTa_QNCN,
		   fSoThieuTa_QNCN,
		   fSoDaiUy_QNCN,
		   fSoThuongUy_QNCN,
		   fSoTrungUy_QNCN,
		   fSoThieuUy_QNCN,
		   fSoVCQP,
		   fSoCNVQP,
		   fSoCY_H,
		   fSoCY_KT,
		   fSoLDHD,
		   fSoCcqp,
		   sGhiChu
	FROM
	  (SELECT iID_QSCTChiTiet,
			  sKyHieu,
			  iThangQuy,
			  iID_MaDonVi,
			  fSoThieuUy,
			  fSoTrungUy,
			  fSoThuongUy,
			  fSoDaiUy,
			  fSoThieuTa,
			  fSoTrungTa,
			  fSoThuongTa,
			  fSoDaiTa,
			  fSoTuong,
			  fSoTSQ,
			  fSoBinhNhi,
			  fSoBinhNhat,
			  fSoHaSi,
			  fSoTrungSi,
			  fSoThuongSi,
			  fSoThuongTa_QNCN,
			  fSoTrungTa_QNCN,
			  fSoThieuTa_QNCN,
			  fSoDaiUy_QNCN,
			  fSoThuongUy_QNCN,
			  fSoTrungUy_QNCN,
			  fSoThieuUy_QNCN,
			  fSoVCQP,
			  fSoCNVQP,
			  fSoCY_H,
			  fSoCY_KT,
			  fSoLDHD,
			  fSoCcqp,
			  sGhiChu
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iID_QSChungTu = @VoucherId
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi = @AgencyId) ) ctct -- lay mucluc quan so
	RIGHT JOIN
	  (SELECT sKyHieu,
			  sM,
			  sMoTa,
			  bHangCha
	   FROM NS_QS_MucLuc
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork
		 AND sHienThi <> '2') AS ml ON ctct.sKyHieu = ml.sKyHieu
	ORDER BY ml.sKyHieu;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_chungtu_chitiet_tonghop]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_chungtu_chitiet_tonghop]
	@YearOfWork int,
	@VoucherId nvarchar(100),
	@AgencyId nvarchar(MAX)
AS
BEGIN
	SELECT NULL AS iID_QSCTChiTiet,
	 ml.sKyHieu,
	 sM,
	 sMoTa,
	 bHangCha,
	 NULL AS iID_MaDonVi,
	 fSoThieuUy,
	 fSoTrungUy,
	 fSoThuongUy,
	 fSoDaiUy,
	 fSoThieuTa,
	 fSoTrungTa,
	 fSoThuongTa,
	 fSoDaiTa,
	 fSoThuongTa_QNCN,
	 fSoTrungTa_QNCN,
	 fSoThieuTa_QNCN,
	 fSoDaiUy_QNCN,
	 fSoThuongUy_QNCN,
	 fSoTrungUy_QNCN,
	 fSoThieuUy_QNCN,
	 fSoTuong,
	 fSoTSQ,
	 fSoBinhNhi,
	 fSoBinhNhat,
	 fSoHaSi,
	 fSoTrungSi,
	 fSoThuongSi,
	 fSoVCQP,
	 fSoCNVQP,
	 fSoCY_H,
	 fSoCY_KT,
	 fSoLDHD,
	 fSoCcqp,
	 '' AS SGhiChu
	FROM
	  (SELECT sKyHieu,
			  iThangQuy,
			  fSoThieuUy = sum(fSoThieuUy),
			  fSoTrungUy = sum(fSoTrungUy),
			  fSoThuongUy = sum(fSoThuongUy),
			  fSoDaiUy = sum(fSoDaiUy),
			  fSoThieuTa = sum(fSoThieuTa),
			  fSoTrungTa = sum(fSoTrungTa),
			  fSoThuongTa = sum(fSoThuongTa),
			  fSoDaiTa = sum(fSoDaiTa),
			  fSoTuong = sum(fSoTuong),
			  fSoTSQ = sum(fSoTSQ),
			  fSoBinhNhi = sum(fSoBinhNhi),
			  fSoBinhNhat = sum(fSoBinhNhat),
			  fSoHaSi = sum(fSoHaSi),
			  fSoTrungSi = sum(fSoTrungSi),
			  fSoThuongSi = sum(fSoThuongSi),
			  fSoThuongTa_QNCN = sum(fSoThuongTa_QNCN),
			  fSoTrungTa_QNCN = sum(fSoTrungTa_QNCN),
			  fSoThieuTa_QNCN = sum(fSoThieuTa_QNCN),
			  fSoDaiUy_QNCN = sum(fSoDaiUy_QNCN),
			  fSoThuongUy_QNCN = sum(fSoThuongUy_QNCN),
			  fSoTrungUy_QNCN = sum(fSoTrungUy_QNCN),
			  fSoThieuUy_QNCN = sum(fSoThieuUy_QNCN),
			  fSoVCQP = sum(fSoVCQP),
			  fSoCNVQP = sum(fSoCNVQP),
			  fSoCY_H = sum(fSoCY_H),
			  fSoCY_KT = sum(fSoCY_KT),
			  fSoLDHD = sum(fSoLDHD),
			  fSoCcqp = sum(fSoCcqp)
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iID_QSChungTu = @VoucherId
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM splitstring(@AgencyId)))
	   GROUP BY sKyHieu,
				iThangQuy)ctct -- lay mucluc quan so
	RIGHT JOIN
	  (SELECT sKyHieu,
			  sM,
			  sMoTa,
			  bHangCha
	   FROM NS_QS_MucLuc
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork
		 AND sHienThi <> '2') AS ml ON ctct.sKyHieu = ml.sKyHieu
	ORDER BY ml.sKyHieu

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_get_donvi]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_get_donvi]
	@YearOfWork int,
	@Months nvarchar(100)
AS
BEGIN
	SELECT dv.*
	FROM (SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork) dv
	INNER JOIN
		(select distinct iID_MaDonVi from NS_QS_ChungTuChiTiet
		where iNamLamViec = @YearOfWork 
		and (@Months <> '' and iThangQuy in (select * from f_split(@Months)) or @Months = '') 
		and (fSoThieuUy <> 0 or fSoTrungUy <> 0 OR fSoThuongUy <> 0 or fSoDaiUy <> 0
		or fSoThieuTa <> 0 or fSoTrungTa <> 0 or fSoThuongTa <> 0 or fSoDaiTa <> 0
		or fSoTuong <> 0 or fSoTSQ <> 0 or fSoBinhNhi <> 0 or fSoBinhNhat <> 0
		or fSoHaSi <> 0 or fSoTrungSi <> 0 or fSoThuongSi <> 0 
		or fSoThuongTa_QNCN <> 0
		or fSoTrungTa_QNCN <> 0
		or fSoThieuTa_QNCN <> 0
		or fSoDaiUy_QNCN <> 0
		or fSoThuongUy_QNCN <> 0
		or fSoTrungUy_QNCN <> 0
		or fSoThieuUy_QNCN <> 0
		or fSoCNVQP <> 0 or fSoLDHD <> 0 or fSoCNVQPCT <> 0 or fSoQNVQPHD <> 0
		or fTongSo <> 0 or  fSoSQ_KH <> 0 or fSoHSQBS_KH <> 0 or fSoCNVQP_KH <> 0
		or fSoLDHD_KH <> 0 or fSoQNCN_KH <> 0 or fSoVCQP <> 0 or fSoCY_H <> 0 or fSoCY_KT <> 0)) qs
	ON qs.iID_MaDonVi = dv.iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_binhquan_donvi]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_binhquan_donvi] 
	@YearOfWork int,
	@AgencyId nvarchar(max),
	@Period nvarchar(100),
	@TotalMonth int
AS
BEGIN
	SELECT dv.iID_MaDonVi ,
		   sTenDonVi ,
		   MoTa ,
		   fSoThieuUy/@TotalMonth as fSoThieuUy,
		   fSoTrungUy/@TotalMonth as fSoTrungUy,
		   fSoThuongUy/@TotalMonth as fSoThuongUy,
		   fSoDaiUy/@TotalMonth as fSoDaiUy,
		   fSoThieuTa/@TotalMonth as fSoThieuTa,
		   fSoTrungTa/@TotalMonth as fSoTrungTa,
		   fSoThuongTa/@TotalMonth as fSoThuongTa,
		   fSoDaiTa/@TotalMonth as fSoDaiTa,
		   fSoTuong/@TotalMonth as fSoTuong,
		   fSoTSQ/@TotalMonth as fSoTSQ,
		   fSoBinhNhi/@TotalMonth as fSoBinhNhi,
		   fSoBinhNhat/@TotalMonth as fSoBinhNhat,
		   fSoHaSi/@TotalMonth as fSoHaSi,
		   fSoTrungSi/@TotalMonth as fSoTrungSi,
		   fSoThuongSi/@TotalMonth as fSoThuongSi,
		   fSoThuongTa_QNCN/@TotalMonth as fSoThuongTa_QNCN,
		   fSoTrungTa_QNCN/@TotalMonth as fSoTrungTa_QNCN,
		   fSoThieuTa_QNCN/@TotalMonth as fSoThieuTa_QNCN,
		   fSoDaiUy_QNCN/@TotalMonth as fSoDaiUy_QNCN,
		   fSoThuongUy_QNCN/@TotalMonth as fSoThuongUy_QNCN,
		   fSoTrungUy_QNCN/@TotalMonth as fSoTrungUy_QNCN,
		   fSoThieuUy_QNCN/@TotalMonth as fSoThieuUy_QNCN,
		   fSoVCQP/@TotalMonth as fSoVCQP,
		   fSoCNVQP/@TotalMonth as fSoCNVQP,
		   fSoLDHD/@TotalMonth as fSoLDHD
	FROM
	  (SELECT iID_MaDonVi ,
			  fSoThieuUy = sum(fSoThieuUy) ,
			  fSoTrungUy = sum(fSoTrungUy) ,
			  fSoThuongUy = sum(fSoThuongUy) ,
			  fSoDaiUy = sum(fSoDaiUy) ,
			  fSoThieuTa = sum(fSoThieuTa) ,
			  fSoTrungTa = sum(fSoTrungTa) ,
			  fSoThuongTa = sum(fSoThuongTa) ,
			  fSoDaiTa = sum(fSoDaiTa) ,
			  fSoTuong = sum(fSoTuong) ,
			  fSoTSQ = sum(fSoTSQ) ,
			  fSoBinhNhi = sum(fSoBinhNhi) ,
			  fSoBinhNhat = sum(fSoBinhNhat) ,
			  fSoHaSi = sum(fSoHaSi) ,
			  fSoTrungSi = sum(fSoTrungSi) ,
			  fSoThuongSi = sum(fSoThuongSi) ,
			  fSoThuongTa_QNCN = sum(fSoThuongTa_QNCN) ,
			  fSoTrungTa_QNCN = sum(fSoTrungTa_QNCN) ,
			  fSoThieuTa_QNCN = sum(fSoThieuTa_QNCN) ,
			  fSoDaiUy_QNCN = sum(fSoDaiUy_QNCN) ,
			  fSoThuongUy_QNCN = sum(fSoThuongUy_QNCN) ,
			  fSoTrungUy_QNCN = sum(fSoTrungUy_QNCN) ,
			  fSoThieuUy_QNCN = sum(fSoThieuUy_QNCN) ,
			  fSoVCQP = sum(fSoVCQP) ,
			  fSoCNVQP = sum(fSoCNVQP) ,
			  fSoLDHD = sum(fSoLDHD)
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))
		 AND (@Period IS NULL
			  OR iThangQuy in
				(SELECT *
				 FROM f_split(@Period)))
		 AND sKyHieu = '700'
	   GROUP BY iID_MaDonVi)ctct -- lay ten don vi
	RIGHT JOIN
	  (SELECT iID_MaDonVi,
			  sTenDonVi,
			  MoTa = iID_MaDonVi + ' - '+ sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork
		 AND iLoai=1
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId))) ) AS dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY ctct.iID_MaDonVi
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_binhquan_thang]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_binhquan_thang]
	@YearOfWork int,
	@AgencyId nvarchar(max),
	@Period nvarchar(100)
AS
BEGIN

	SELECT '' AS iID_MaDonVi ,
		   '' AS sTenDonVi ,
		   CASE
			WHEN t.iThangQuy = 0 THEN N'Đầu năm' 
			ELSE CONCAT('Tháng ', t.iThangQuy) 
		   END AS MoTa ,
		   fSoThieuUy ,
		   fSoTrungUy ,
		   fSoThuongUy ,
		   fSoDaiUy ,
		   fSoThieuTa ,
		   fSoTrungTa ,
		   fSoThuongTa ,
		   fSoDaiTa ,
		   fSoTuong ,
		   fSoTSQ ,
		   fSoBinhNhi ,
		   fSoBinhNhat ,
		   fSoHaSi ,
		   fSoTrungSi ,
		   fSoThuongSi ,
		   fSoThuongTa_QNCN ,
		   fSoTrungTa_QNCN ,
		   fSoThieuTa_QNCN ,
		   fSoDaiUy_QNCN ,
		   fSoThuongUy_QNCN ,
		   fSoTrungUy_QNCN ,
		   fSoThieuUy_QNCN ,
		   fSoVCQP ,
		   fSoCNVQP ,
		   fSoLDHD
	FROM
	  (SELECT iThangQuy ,
			  fSoThieuUy = sum(fSoThieuUy) ,
			  fSoTrungUy = sum(fSoTrungUy) ,
			  fSoThuongUy = sum(fSoThuongUy) ,
			  fSoDaiUy = sum(fSoDaiUy) ,
			  fSoThieuTa = sum(fSoThieuTa) ,
			  fSoTrungTa = sum(fSoTrungTa) ,
			  fSoThuongTa = sum(fSoThuongTa) ,
			  fSoDaiTa = sum(fSoDaiTa) ,
			  fSoTuong = sum(fSoTuong) ,
			  fSoTSQ = sum(fSoTSQ) ,
			  fSoBinhNhi = sum(fSoBinhNhi) ,
			  fSoBinhNhat = sum(fSoBinhNhat) ,
			  fSoHaSi = sum(fSoHaSi) ,
			  fSoTrungSi = sum(fSoTrungSi) ,
			  fSoThuongSi = sum(fSoThuongSi) ,
			  fSoThuongTa_QNCN = sum(fSoThuongTa_QNCN) ,
			  fSoTrungTa_QNCN = sum(fSoTrungTa_QNCN) ,
			  fSoThieuTa_QNCN = sum(fSoThieuTa_QNCN) ,
			  fSoDaiUy_QNCN = sum(fSoDaiUy_QNCN) ,
			  fSoThuongUy_QNCN = sum(fSoThuongUy_QNCN) ,
			  fSoTrungUy_QNCN = sum(fSoTrungUy_QNCN) ,
			  fSoThieuUy_QNCN = sum(fSoThieuUy_QNCN) ,
			  fSoVCQP = sum(fSoVCQP) ,
			  fSoCNVQP = sum(fSoCNVQP) ,
			  fSoLDHD = sum(fSoLDHD)
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))
		 AND (@Period IS NULL
			  OR iThangQuy in
				(SELECT *
				 FROM f_split(@Period)))
		 AND sKyHieu = '700'
	   GROUP BY iThangQuy) AS ctct
	RIGHT JOIN
	  (SELECT Item AS iThangQuy
	   FROM f_split(@Period)) AS t ON t.iThangQuy=ctct.iThangQuy
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_chitiet_donvi]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_chitiet_donvi]
	@YearOfWork int,
	@AgencyId nvarchar(max),
	@Period nvarchar(100)
AS
BEGIN
	SELECT mlns.sKyHieu ,
		   dv.iID_MaDonVi ,
		   mlns.sMoTa ,
		   dv.iID_MaDonVi + ' - ' + dv.sTenDonVi as TenDonVi,
		   bHangCha ,
		   isnull(fSoThieuUy , 0) as fSoThieuUy , 
			isnull(fSoTrungUy , 0) as fSoTrungUy , 
			isnull(fSoThuongUy , 0) as fSoThuongUy , 
			isnull(fSoDaiUy , 0) as fSoDaiUy , 
			isnull(fSoThieuTa , 0) as fSoThieuTa , 
			isnull(fSoTrungTa , 0) as fSoTrungTa , 
			isnull(fSoThuongTa , 0) as fSoThuongTa , 
			isnull(fSoDaiTa , 0) as fSoDaiTa , 
			isnull(fSoTuong , 0) as fSoTuong , 
			isnull(fSoTSQ , 0) as fSoTSQ , 
			isnull(fSoBinhNhi , 0) as fSoBinhNhi , 
			isnull(fSoBinhNhat , 0) as fSoBinhNhat , 
			isnull(fSoHaSi , 0) as fSoHaSi , 
			isnull(fSoTrungSi , 0) as fSoTrungSi , 
			isnull(fSoThuongSi , 0) as fSoThuongSi , 
			isnull(fSoThuongTa_QNCN , 0) as fSoThuongTa_QNCN , 
			isnull(fSoTrungTa_QNCN , 0) as fSoTrungTa_QNCN , 
			isnull(fSoThieuTa_QNCN , 0) as fSoThieuTa_QNCN , 
			isnull(fSoDaiUy_QNCN , 0) as fSoDaiUy_QNCN , 
			isnull(fSoThuongUy_QNCN , 0) as fSoThuongUy_QNCN , 
			isnull(fSoTrungUy_QNCN , 0) as fSoTrungUy_QNCN , 
			isnull(fSoThieuUy_QNCN , 0) as fSoThieuUy_QNCN , 
			isnull(fSoVCQP , 0) as fSoVCQP , 
			isnull(fSoCNVQP , 0) as fSoCNVQP , 
			isnull(fSoLDHD, 0) as fSoLDHD ,
			isnull(fSoCcqp, 0) as fSoCcqp 

	FROM
	  (SELECT sKyHieu ,
			  iID_MaDonVi ,
			  fSoThieuUy = sum(fSoThieuUy) ,
			  fSoTrungUy = sum(fSoTrungUy) ,
			  fSoThuongUy = sum(fSoThuongUy) ,
			  fSoDaiUy = sum(fSoDaiUy) ,
			  fSoThieuTa = sum(fSoThieuTa) ,
			  fSoTrungTa = sum(fSoTrungTa) ,
			  fSoThuongTa = sum(fSoThuongTa) ,
			  fSoDaiTa = sum(fSoDaiTa) ,
			  fSoTuong = sum(fSoTuong) ,
			  fSoTSQ = sum(fSoTSQ) ,
			  fSoBinhNhi = sum(fSoBinhNhi) ,
			  fSoBinhNhat = sum(fSoBinhNhat) ,
			  fSoHaSi = sum(fSoHaSi) ,
			  fSoTrungSi = sum(fSoTrungSi) ,
			  fSoThuongSi = sum(fSoThuongSi) ,
			  fSoThuongTa_QNCN = sum(fSoThuongTa_QNCN) ,
			  fSoTrungTa_QNCN = sum(fSoTrungTa_QNCN) ,
			  fSoThieuTa_QNCN = sum(fSoThieuTa_QNCN) ,
			  fSoDaiUy_QNCN = sum(fSoDaiUy_QNCN) ,
			  fSoThuongUy_QNCN = sum(fSoThuongUy_QNCN) ,
			  fSoTrungUy_QNCN = sum(fSoTrungUy_QNCN) ,
			  fSoThieuUy_QNCN = sum(fSoThieuUy_QNCN) ,
			  fSoVCQP = sum(fSoVCQP) ,
			  fSoCNVQP = sum(fSoCNVQP) ,
			  fSoLDHD = sum(fSoLDHD),
			  fSoCcqp = sum(fSoCcqp)
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))
		 AND iThangQuy in
		   (SELECT *
			FROM f_split(@Period))
	   GROUP BY sKyHieu, iID_MaDonVi)ctct -- lay mucluc quan so

	RIGHT JOIN
	  (SELECT sKyHieu,
			  sMoTa,
			  bHangCha
	   FROM NS_QS_MucLuc
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork
		 AND sHienThi <> '2') AS mlns ON ctct.sKyHieu = mlns.sKyHieu
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	UNION 
	SELECT sKyHieu ,
		   null as iID_MaDonVi ,
		   sMoTa ,
		   null as TenDonVi,
		   bHangCha ,
		   0 as fSoThieuUy ,
		   0 as fSoTrungUy ,
		   0 as fSoThuongUy ,
		   0 as fSoDaiUy ,
		   0 as fSoThieuTa ,
		   0 as fSoTrungTa ,
		   0 as fSoThuongTa ,
		   0 as fSoDaiTa ,
		   0 as fSoTuong ,
		   0 as fSoTSQ ,
		   0 as fSoBinhNhi ,
		   0 as fSoBinhNhat ,
		   0 as fSoHaSi ,
		   0 as fSoTrungSi ,
		   0 as fSoThuongSi ,
		   0 as fSoThuongTa_QNCN ,
		   0 as fSoTrungTa_QNCN ,
		   0 as fSoThieuTa_QNCN ,
		   0 as fSoDaiUy_QNCN ,
		   0 as fSoThuongUy_QNCN ,
		   0 as fSoTrungUy_QNCN ,
		   0 as fSoThieuUy_QNCN ,
		   0 as fSoVCQP ,
		   0 as fSoCNVQP ,
		   0 as fSoLDHD ,
		   0 as fSoCcqp
	FROM NS_QS_MucLuc
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork
		 AND sHienThi <> '2'
	ORDER BY sKyHieu, iID_MaDonVi
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_donvi]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_donvi]
	@YearOfWork int,
	@AgencyId nvarchar(max),
	@Period nvarchar(100)
AS
BEGIN
	SELECT mlns.sKyHieu ,
		   sMoTa ,
		   bHangCha ,
		   fSoThieuUy ,
		   fSoTrungUy ,
		   fSoThuongUy ,
		   fSoDaiUy ,
		   fSoThieuTa ,
		   fSoTrungTa ,
		   fSoThuongTa ,
		   fSoDaiTa ,
		   fSoTuong ,
		   fSoTSQ ,
		   fSoBinhNhi ,
		   fSoBinhNhat ,
		   fSoHaSi ,
		   fSoTrungSi ,
		   fSoThuongSi ,
		   fSoThuongTa_QNCN ,
		   fSoTrungTa_QNCN ,
		   fSoThieuTa_QNCN ,
		   fSoDaiUy_QNCN ,
		   fSoThuongUy_QNCN ,
		   fSoTrungUy_QNCN ,
		   fSoThieuUy_QNCN ,
		   fSoVCQP ,
		   fSoCNVQP ,
		   fSoLDHD,
		   fSoCcqp
	FROM
	  (SELECT sKyHieu ,
			  fSoThieuUy = sum(fSoThieuUy) ,
			  fSoTrungUy = sum(fSoTrungUy) ,
			  fSoThuongUy = sum(fSoThuongUy) ,
			  fSoDaiUy = sum(fSoDaiUy) ,
			  fSoThieuTa = sum(fSoThieuTa) ,
			  fSoTrungTa = sum(fSoTrungTa) ,
			  fSoThuongTa = sum(fSoThuongTa) ,
			  fSoDaiTa = sum(fSoDaiTa) ,
			  fSoTuong = sum(fSoTuong) ,
			  fSoTSQ = sum(fSoTSQ) ,
			  fSoBinhNhi = sum(fSoBinhNhi) ,
			  fSoBinhNhat = sum(fSoBinhNhat) ,
			  fSoHaSi = sum(fSoHaSi) ,
			  fSoTrungSi = sum(fSoTrungSi) ,
			  fSoThuongSi = sum(fSoThuongSi) ,
			  fSoThuongTa_QNCN = sum(fSoThuongTa_QNCN) ,
			  fSoTrungTa_QNCN = sum(fSoTrungTa_QNCN) ,
			  fSoThieuTa_QNCN = sum(fSoThieuTa_QNCN) ,
			  fSoDaiUy_QNCN = sum(fSoDaiUy_QNCN) ,
			  fSoThuongUy_QNCN = sum(fSoThuongUy_QNCN) ,
			  fSoTrungUy_QNCN = sum(fSoTrungUy_QNCN) ,
			  fSoThieuUy_QNCN = sum(fSoThieuUy_QNCN) ,
			  fSoVCQP = sum(fSoVCQP) ,
			  fSoCNVQP = sum(fSoCNVQP) ,
			  fSoLDHD = sum(fSoLDHD),
			  fSoCcqp = sum(fSoCcqp)
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))
		 AND iThangQuy in
		   (SELECT *
			FROM f_split(@Period))
	   GROUP BY sKyHieu)ctct -- lay mucluc quan so

	RIGHT JOIN
	  (SELECT sKyHieu,
			  sMoTa,
			  bHangCha
	   FROM NS_QS_MucLuc
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork
		 AND sHienThi <> '2') AS mlns ON ctct.sKyHieu = mlns.sKyHieu
	ORDER BY sKyHieu
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_lientham]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_lientham]
	@Month int,
	@YearOfWork int,
	@YearOfWorkBefore int,
	@AgencyId nvarchar(max)
AS
BEGIN
	
	SELECT 
		ctct.*,
		dv.sTenDonVi,
		dv.MoTa
	FROM(
		SELECT iID_MaDonVi
		--thieu uy
		,ThieuUy_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThieuUy ELSE 0 END)
		,ThieuUy_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThieuUy ELSE 0 END)
		,ThieuUy_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThieuUy ELSE 0 END)
		,ThieuUy_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThieuUy ELSE 0 END)

		--trung uy
		,TrungUy_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungUy ELSE 0 END)
		,TrungUy_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungUy ELSE 0 END)
		,TrungUy_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungUy ELSE 0 END)
		,TrungUy_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungUy ELSE 0 END)

		----thuong uy
		,ThuongUy_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongUy ELSE 0 END)
		,ThuongUy_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongUy ELSE 0 END)
		,ThuongUy_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongUy ELSE 0 END)
		,ThuongUy_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongUy ELSE 0 END)

		----Dai uy
		,DaiUy_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoDaiUy ELSE 0 END)
		,DaiUy_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoDaiUy ELSE 0 END)
		,DaiUy_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoDaiUy ELSE 0 END)
		,DaiUy_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoDaiUy ELSE 0 END)

		----thieu ta
		,ThieuTa_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThieuTa ELSE 0 END)
		,ThieuTa_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThieuTa ELSE 0 END)
		,ThieuTa_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThieuTa ELSE 0 END)
		,ThieuTa_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThieuTa ELSE 0 END)

		----trungta
		,TrungTa_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungTa ELSE 0 END)
		,TrungTa_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungTa ELSE 0 END)
		,TrungTa_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungTa ELSE 0 END)
		,TrungTa_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungTa ELSE 0 END)

		----thuong ta
		,ThuongTa_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongTa ELSE 0 END)
		,ThuongTa_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongTa ELSE 0 END)
		,ThuongTa_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongTa ELSE 0 END)
		,ThuongTa_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongTa ELSE 0 END)

		----Dai ta
		,DaiTa_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoDaiTa ELSE 0 END)
		,DaiTa_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoDaiTa ELSE 0 END)
		,DaiTa_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoDaiTa ELSE 0 END)
		,DaiTa_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoDaiTa ELSE 0 END)

		----thieu tuong
		,Tuong_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTuong ELSE 0 END)
		,Tuong_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTuong ELSE 0 END)
		,Tuong_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTuong ELSE 0 END)
		,Tuong_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTuong ELSE 0 END)

		----TSQ
		,TSQ_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTSQ ELSE 0 END)
		,TSQ_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTSQ ELSE 0 END)
		,TSQ_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTSQ ELSE 0 END)
		,TSQ_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTSQ ELSE 0 END)

		----binh nhi
		,BinhNhi_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoBinhNhi ELSE 0 END)
		,BinhNhi_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoBinhNhi ELSE 0 END)
		,BinhNhi_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoBinhNhi ELSE 0 END)
		,BinhNhi_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoBinhNhi ELSE 0 END)

		----binh nhat
		,BinhNhat_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoBinhNhat ELSE 0 END)
		,BinhNhat_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoBinhNhat ELSE 0 END)
		,BinhNhat_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoBinhNhat ELSE 0 END)
		,BinhNhat_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoBinhNhat ELSE 0 END)

		----ha si
		,HaSi_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoHaSi ELSE 0 END)
		,HaSi_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoHaSi ELSE 0 END)
		,HaSi_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoHaSi ELSE 0 END)
		,HaSi_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoHaSi ELSE 0 END)

		----trung si
		,TrungSi_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungSi ELSE 0 END)
		,TrungSi_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungSi ELSE 0 END)
		,TrungSi_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungSi ELSE 0 END)
		,TrungSi_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungSi ELSE 0 END)

		----thuong si
		,ThuongSi_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongSi ELSE 0 END)
		,ThuongSi_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongSi ELSE 0 END)
		,ThuongSi_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongSi ELSE 0 END)
		,ThuongSi_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongSi ELSE 0 END)

		----QNCN
		,QNCN_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) ELSE 0 END)
		,QNCN_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) ELSE 0 END)
		,QNCN_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) ELSE 0 END)
		,QNCN_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) ELSE 0 END)

		----CNVQP
		,CNVQP_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoCNVQP ELSE 0 END)
		,CNVQP_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoCNVQP ELSE 0 END)
		,CNVQP_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoCNVQP ELSE 0 END)
		,CNVQP_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoCNVQP ELSE 0 END)

		----LDHD
		,LDHD_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoLDHD ELSE 0 END)
		,LDHD_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoLDHD ELSE 0 END)
		,LDHD_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoLDHD ELSE 0 END)
		,LDHD_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoLDHD ELSE 0 END)

		----CCQP
		,CCQP_NamTruoc = SUM(
		CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
		THEN fSoCcqp ELSE 0 END)
		,CCQP_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoCcqp ELSE 0 END)
		,CCQP_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoCcqp ELSE 0 END)
		,CCQP_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoCcqp ELSE 0 END)


		FROM NS_QS_ChungTuChiTiet
		WHERE iNamLamViec = @YearOfWork
					and (@AgencyId is null or iID_MaDonVi in (select * from f_split(@AgencyId)))
		GROUP BY iID_MaDonVi) as ctct
		RIGHT JOIN 
			(
				SELECT 
					iID_MaDonVi, 
					sTenDonVi,	
					MoTa = iID_MaDonVi + ' - ' + sTenDonVi from DonVi 
				WHERE 
					iTrangThai=1 
					and iNamLamViec = @yearOfWork 
					and iLoai=1 
					and (@AgencyId is null or iID_MaDonVi in (select * from f_split(@AgencyId)))
			) as dv
			on dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY dv.iID_MaDonVi
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_raquan]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_raquan]
	@YearOfWork int,
	@AgencyId nvarchar(max),
	@Period nvarchar(50)
AS
BEGIN
	SELECT 
		Huu,
		PhucVien,
		XuatNgu,
		ThoiViec,
		dv.iID_MaDonVi as Id_DonVi,
		dv.sTenDonVi as TenDonVi,
		dv.MoTa
	FROM 
	(
		SELECT 
			iID_MaDonVi
			,Huu = SUM(CASE WHEN sKyHieu = '310' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END)
			,PhucVien = SUM(CASE WHEN sKyHieu = '331' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END)
			,XuatNgu = SUM(CASE WHEN sKyHieu = '320' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END)
			,ThoiViec = SUM(CASE WHEN sKyHieu = '330' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END)
		FROM 
			NS_QS_ChungTuChiTiet
		WHERE  
			iNamLamViec=@YearOfWork
			AND (@AgencyId is null or iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId)))
			AND bHangCha=0
			AND iThangQuy in (select * from f_split(@Period))
		GROUP BY
			iID_MaDonVi
		HAVING SUM(CASE WHEN sKyHieu = '310' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END) <>0
			OR SUM(CASE WHEN sKyHieu = '331' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END)<>0
			OR SUM(CASE WHEN sKyHieu = '320' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END)<>0
			OR SUM(CASE WHEN sKyHieu = '330' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoVCQP + fSoCNVQP + fSoLDHD) ELSE 0 END)<>0
	) AS ctct
	RIGHT JOIN 
	(
		SELECT 
			iID_MaDonVi, 
			sTenDonVi,	
			MoTa = iID_MaDonVi + ' - ' + sTenDonVi 
		FROM DonVi 
		WHERE 
			iTrangThai=1 
			AND iNamLamViec = @yearOfWork 
			AND iLoai=1 
			AND (@AgencyId IS NULL OR iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId)))
	) AS dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY Id_DonVi
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_raquan_thangtruoc]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_raquan_thangtruoc]
	@Month int,
	@YearOfWork int,
	@YearOfWorkBefore int,
	@AgencyId nvarchar(max)
AS
BEGIN
	SELECT 
		Huu = SUM(CASE WHEN sKyHieu = '310' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoCNVQP + fSoLDHD) ELSE 0 END)
		,PhucVien = SUM(CASE WHEN sKyHieu = '331' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoCNVQP + fSoLDHD) ELSE 0 END)
		,XuatNgu = SUM(CASE WHEN sKyHieu = '320' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoCNVQP + fSoLDHD) ELSE 0 END)
		,ThoiViec = SUM(CASE WHEN sKyHieu = '330' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoCNVQP + fSoLDHD) ELSE 0 END)
		,'' AS Id_DonVi
		,'' AS TenDonVi
		,'' AS MoTa
	FROM 
		NS_QS_ChungTuChiTiet
	WHERE  
		(
			(@Month = 1 and iNamLamViec = @YearOfWork and iThangQuy = 0) 
			OR 
			( 
				@Month <> 1 
				AND 
				(
					(iNamLamViec = @YearOfWork and iThangQuy < @Month) 
					OR (iNamLamViec < @YearOfWorkBefore and iThangQuy <= 12)
				)
			)
		)
		AND (@AgencyId is null OR iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId)))
	HAVING SUM(CASE WHEN sKyHieu = '310' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoCNVQP + fSoLDHD) ELSE 0 END) <>0
		OR SUM(CASE WHEN sKyHieu = '331' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoCNVQP + fSoLDHD) ELSE 0 END)<>0
		OR SUM(CASE WHEN sKyHieu = '320' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoCNVQP + fSoLDHD) ELSE 0 END)<>0
		OR SUM(CASE WHEN sKyHieu = '330' THEN (fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa + fSoTrungTa + fSoThuongTa + fSoDaiTa + fSoTuong + fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi + (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) + fSoCNVQP + fSoLDHD) ELSE 0 END)<>0
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_thuongxuyen]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_thuongxuyen]
	@month1 int,
	@month2 int,
	@month3 int,
	@month4 int,
	@yearOfWork int,
	@agencyId nvarchar(max)
AS
BEGIN
	SELECT 
		rSQ = SUM(fSoSQ),
		rQNCN = SUM(fSoQNCN) ,
		rCNVHD = SUM(fSoCNVHD),
		rHSQCS = SUM(fSoHSQCS),

		rSQ1 = SUM(CASE WHEN ((iThangQuy =@month1 AND iNamLamViec=@yearOfWork))  THEN fSoSQ ELSE 0 END),
		rSQ2 = SUM(CASE WHEN ((iThangQuy =@month2 AND iNamLamViec=@yearOfWork))   THEN fSoSQ ELSE 0 END),
		rSQ3 = SUM(CASE WHEN ((iThangQuy =@month3 AND iNamLamViec=@yearOfWork))   THEN fSoSQ ELSE 0 END),
		rSQ4 = SUM(CASE WHEN ((iThangQuy =@month4 AND iNamLamViec=@yearOfWork))  THEN fSoSQ ELSE 0 END),
				
		rQNCN1 = SUM(CASE WHEN ((iThangQuy =@month1 AND iNamLamViec=@yearOfWork))   THEN fSoQNCN ELSE 0 END),
		rQNCN2 = SUM(CASE WHEN ((iThangQuy =@month2 AND iNamLamViec=@yearOfWork))   THEN fSoQNCN ELSE 0 END),
		rQNCN3 = SUM(CASE WHEN ((iThangQuy =@month3 AND iNamLamViec=@yearOfWork))   THEN fSoQNCN ELSE 0 END),
		rQNCN4 = SUM(CASE WHEN ((iThangQuy =@month4 AND iNamLamViec=@yearOfWork))  THEN fSoQNCN ELSE 0 END),
				
		rCNVHD1 = SUM(CASE WHEN ((iThangQuy =@month1 AND iNamLamViec=@yearOfWork))   THEN fSoCNVHD ELSE 0 END),
		rCNVHD2 = SUM(CASE WHEN ((iThangQuy =@month2 AND iNamLamViec=@yearOfWork))   THEN fSoCNVHD ELSE 0 END),
		rCNVHD3 = SUM(CASE WHEN ((iThangQuy =@month3 AND iNamLamViec=@yearOfWork))   THEN fSoCNVHD ELSE 0 END),
		rCNVHD4 = SUM(CASE WHEN ((iThangQuy =@month4 AND iNamLamViec=@yearOfWork))  THEN fSoCNVHD ELSE 0 END),
				
		rHSQCS1 = SUM(CASE WHEN ((iThangQuy =@month1 AND iNamLamViec=@yearOfWork))   THEN fSoHSQCS ELSE 0 END),
		rHSQCS2 = SUM(CASE WHEN ((iThangQuy =@month2 AND iNamLamViec=@yearOfWork))   THEN fSoHSQCS ELSE 0 END),
		rHSQCS3 = SUM(CASE WHEN ((iThangQuy =@month3 AND iNamLamViec=@yearOfWork))   THEN fSoHSQCS ELSE 0 END),
		rHSQCS4 = SUM(CASE WHEN ((iThangQuy =@month4 AND iNamLamViec=@yearOfWork))  THEN fSoHSQCS ELSE 0 END),
		dv.iID_MaDonVi as Id_DonVi,
		dv.sTenDonVi as TenDonVi,
		dv.MoTa,
		iNamLamViec,
		iThangQuy
	FROM (
		SELECT
			fSoSQ = SUM(CASE WHEN sKyHieu = '700' THEN fSoThieuUy + fSoTrungUy + fSoThuongUy + fSoDaiUy + fSoThieuTa+ fSoTrungTa + fSoThuongTa + fSoDaiTa+ fSoTuong ELSE 0 END),
			fSoQNCN = SUM(CASE WHEN sKyHieu = '700' THEN (fSoThuongTa_QNCN + fSoTrungTa_QNCN + fSoThieuTa_QNCN + fSoDaiUy_QNCN + fSoThuongUy_QNCN + fSoTrungUy_QNCN + fSoThieuUy_QNCN) ELSE 0 END),
			fSoCNVHD = SUM(CASE WHEN sKyHieu = '700' THEN fSoCNVQP + fSoLDHD ELSE 0 END),
			fSoHSQCS = SUM(CASE WHEN sKyHieu = '700' THEN fSoTSQ + fSoBinhNhi + fSoBinhNhat + fSoHaSi + fSoTrungSi + fSoThuongSi ELSE 0 END),
			iID_MaDonVi,
			iThangQuy,
			iNamLamViec
		from  
			NS_QS_ChungTuChiTiet 
		WHERE 
			iNamLamViec = @yearOfWork
			and (@agencyId is null or iID_MaDonVi in (select * from f_split(@agencyId)))
		group by  
			iID_MaDonVi, iThangQuy, iNamLamViec 
	) as qs
	RIGHT JOIN 
		(
			SELECT iID_MaDonVi, sTenDonVi, MoTa = iID_MaDonVi + ' - ' + sTenDonVi from DonVi 
			WHERE iTrangThai=1 and iNamLamViec = @yearOfWork and iLoai=1 and (@AgencyId is null or iID_MaDonVi in (select * from f_split(@AgencyId)))) as dv
	on dv.iID_MaDonVi = qs.iID_MaDonVi
	
	group by dv.iID_MaDonVi, dv.sTenDonVi, dv.MoTa, iNamLamViec, iThangQuy
	ORDER BY dv.iID_MaDonVi		
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_tonghop]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_tonghop]
	@YearOfWork int,
	@AgencyId nvarchar(max),
	@Period nvarchar(100)
AS
BEGIN
	SELECT dv.iID_MaDonVi ,
		   sTenDonVi ,
		   MoTa ,
		   fSoThieuUy ,
		   fSoTrungUy ,
		   fSoThuongUy ,
		   fSoDaiUy ,
		   fSoThieuTa ,
		   fSoTrungTa ,
		   fSoThuongTa ,
		   fSoDaiTa ,
		   fSoTuong ,
		   fSoTSQ ,
		   fSoBinhNhi ,
		   fSoBinhNhat ,
		   fSoHaSi ,
		   fSoTrungSi ,
		   fSoThuongSi ,
		   fSoThuongTa_QNCN,
		   fSoTrungTa_QNCN,
		   fSoThieuTa_QNCN,
		   fSoDaiUy_QNCN,
		   fSoThuongUy_QNCN,
		   fSoTrungUy_QNCN,
		   fSoThieuUy_QNCN,
		   fSoVCQP ,
		   fSoCNVQP ,
		   fSoLDHD ,
		   fSoCCQP
	FROM
	  (SELECT iID_MaDonVi ,
			  fSoThieuUy = SUM(fSoThieuUy) ,
			  fSoTrungUy = SUM(fSoTrungUy) ,
			  fSoThuongUy = SUM(fSoThuongUy) ,
			  fSoDaiUy = sum(fSoDaiUy) ,
			  fSoThieuTa = SUM(fSoThieuTa) ,
			  fSoTrungTa = SUM(fSoTrungTa) ,
			  fSoThuongTa = SUM(fSoThuongTa) ,
			  fSoDaiTa = SUM(fSoDaiTa) ,
			  fSoTuong = SUM(fSoTuong) ,
			  fSoTSQ = SUM(fSoTSQ) ,
			  fSoBinhNhi = SUM(fSoBinhNhi) ,
			  fSoBinhNhat = SUM(fSoBinhNhat) ,
			  fSoHaSi = SUM(fSoHaSi) ,
			  fSoTrungSi = SUM(fSoTrungSi) ,
			  fSoThuongSi = SUM(fSoThuongSi) ,
			  fSoThuongTa_QNCN = sum(fSoThuongTa_QNCN) ,
			  fSoTrungTa_QNCN = sum(fSoTrungTa_QNCN) ,
			  fSoThieuTa_QNCN = sum(fSoThieuTa_QNCN) ,
			  fSoDaiUy_QNCN = sum(fSoDaiUy_QNCN) ,
			  fSoThuongUy_QNCN = sum(fSoThuongUy_QNCN) ,
			  fSoTrungUy_QNCN = sum(fSoTrungUy_QNCN) ,
			  fSoThieuUy_QNCN = sum(fSoThieuUy_QNCN) ,
			  fSoVCQP = SUM(fSoVCQP) ,
			  fSoCNVQP = SUM(fSoCNVQP) ,
			  fSoLDHD = SUM(fSoLDHD) ,
			  fSoCCQP = SUM(fSoCcqp)
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec=@YearOfWork
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))
		 AND (@Period IS NULL
			  OR iThangQuy in
				(SELECT *
				 FROM f_split(@Period)))
		 AND sKyHieu = '700'
	   GROUP BY iID_MaDonVi)ctct -- lay ten don vi
	RIGHT JOIN
	  (SELECT iID_MaDonVi,
			  sTenDonVi,
			  MoTa = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai = 1
		 AND iNamLamViec = @YearOfWork
		 AND iLoai = 1
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId))) ) AS dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY iID_MaDonVi
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_tao_chitiet]    Script Date: 15/12/2023 5:30:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_tao_chitiet]
	@IdChungTu nvarchar(100),
	@YearOfWork int,
	@Thang int,
	@User nvarchar(100)
AS
BEGIN

	select iID_MaDonVi into #dv 
	from DonVi 
	where iLoai = 1
	and iNamLamViec = @YearOfWork

	IF EXISTS (SELECT * FROM #dv) AND @Thang <> 0
		BEGIN
			select * into #qs1 
			from NS_QS_MucLuc 
			where sKyHieu in ('100', '700') and iNamLamViec = @YearOfWork

			select * into #data1 from #qs1, #dv
			insert into NS_QS_ChungTuChiTiet
				(iID_QSChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, iThangQuy, iID_MaDonVi, iNamLamViec, 
				fSoThieuUy, fSoTrungUy, fSoThuongUy, fSoDaiUy, fSoThieuTa, fSoTrungTa, fSoThuongTa, fSoDaiTa, fSoTuong,
				fSoTSQ, fSoBinhNhi, fSoBinhNhat, fSoHaSi, fSoTrungSi, fSoThuongSi, 
				fSoThuongTa_QNCN, 
				fSoTrungTa_QNCN, 
				fSoThieuTa_QNCN, 
				fSoDaiUy_QNCN, 
				fSoThuongUy_QNCN, 
				fSoTrungUy_QNCN, 
				fSoThieuUy_QNCN, 
				fSoCNVQP, fSoLDHD, 
				fSoCNVQPCT, fSoQNVQPHD, fTongSo, fSoSQ_KH, fSoHSQBS_KH, fSoCNVQP_KH, fSoLDHD_KH, fSoQNCN_KH, fSoVCQP, 
				fSoCY_H, fSoCY_KT, sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNgaySua)

				select @IdChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, @Thang, iID_MaDonVi, @YearOfWork, 
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 
					0, 0, 0, 0, 0, 0, 0,
					0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 
					0, 0, null, getdate(), @User, null, null
				from #data1
			drop table #qs1, #dv, #data1
		END

	ELSE IF EXISTS (SELECT * FROM #dv) AND @Thang = 0
		BEGIN
			select * into #qs2 
			from NS_QS_MucLuc 
			where iNamLamViec = @YearOfWork
			select * into #data2 from #qs2, #dv

			insert into NS_QS_ChungTuChiTiet
				(iID_QSChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, iThangQuy, iID_MaDonVi, iNamLamViec, 
				fSoThieuUy, fSoTrungUy, fSoThuongUy, fSoDaiUy, fSoThieuTa, fSoTrungTa, fSoThuongTa, fSoDaiTa, fSoTuong,
				fSoTSQ, fSoBinhNhi, fSoBinhNhat, fSoHaSi, fSoTrungSi, fSoThuongSi, 
				fSoThuongTa_QNCN, 
				fSoTrungTa_QNCN, 
				fSoThieuTa_QNCN, 
				fSoDaiUy_QNCN, 
				fSoThuongUy_QNCN, 
				fSoTrungUy_QNCN, 
				fSoThieuUy_QNCN, 				
				fSoCNVQP, fSoLDHD, 
				fSoCNVQPCT, fSoQNVQPHD, fTongSo, fSoSQ_KH, fSoHSQBS_KH, fSoCNVQP_KH, fSoLDHD_KH, fSoQNCN_KH, fSoVCQP, 
				fSoCY_H, fSoCY_KT, sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNgaySua)

				select @IdChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, @Thang, iID_MaDonVi, @YearOfWork, 
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0,
					0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 
					0, 0, null, getdate(), @User, null, null
				from #data2
			drop table #qs2, #dv, #data2
		END
	ELSE IF @Thang <> 0
		BEGIN
			select iID_MaDonVi into #dv2 
			from DonVi 
			where iLoai = 0
			and iNamLamViec = @YearOfWork

			select * into #qs3 
			from NS_QS_MucLuc 
			where sKyHieu in ('100', '700') and iNamLamViec = @YearOfWork

			select * into #data3 from #qs3, #dv2
			insert into NS_QS_ChungTuChiTiet
				(iID_QSChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, iThangQuy, iID_MaDonVi, iNamLamViec, 
				fSoThieuUy, fSoTrungUy, fSoThuongUy, fSoDaiUy, fSoThieuTa, fSoTrungTa, fSoThuongTa, fSoDaiTa, fSoTuong,
				fSoTSQ, fSoBinhNhi, fSoBinhNhat, fSoHaSi, fSoTrungSi, fSoThuongSi, 
				fSoThuongTa_QNCN, 
				fSoTrungTa_QNCN, 
				fSoThieuTa_QNCN, 
				fSoDaiUy_QNCN, 
				fSoThuongUy_QNCN, 
				fSoTrungUy_QNCN, 
				fSoThieuUy_QNCN, 				
				fSoCNVQP, fSoLDHD, 
				fSoCNVQPCT, fSoQNVQPHD, fTongSo, fSoSQ_KH, fSoHSQBS_KH, fSoCNVQP_KH, fSoLDHD_KH, fSoQNCN_KH, fSoVCQP, 
				fSoCY_H, fSoCY_KT, sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNgaySua)

				select @IdChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, @Thang, iID_MaDonVi, @YearOfWork, 
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0,
					0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 
					0, 0, null, getdate(), @User, null, null
				from #data3
			drop table #qs3, #dv2, #data3
		END
	ELSE 
		BEGIN
			select iID_MaDonVi into #dv4 
			from DonVi 
			where iLoai = 0
			and iNamLamViec = @YearOfWork
		
			select * into #qs4 
			from NS_QS_MucLuc 
			where iNamLamViec = @YearOfWork

			select * into #data4 from #qs4, #dv4
			insert into NS_QS_ChungTuChiTiet
				(iID_QSChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, iThangQuy, iID_MaDonVi, iNamLamViec, 
				fSoThieuUy, fSoTrungUy, fSoThuongUy, fSoDaiUy, fSoThieuTa, fSoTrungTa, fSoThuongTa, fSoDaiTa, fSoTuong,
				fSoTSQ, fSoBinhNhi, fSoBinhNhat, fSoHaSi, fSoTrungSi, fSoThuongSi,
				fSoThuongTa_QNCN, 
				fSoTrungTa_QNCN, 
				fSoThieuTa_QNCN, 
				fSoDaiUy_QNCN, 
				fSoThuongUy_QNCN, 
				fSoTrungUy_QNCN, 
				fSoThieuUy_QNCN, 				
				fSoCNVQP, fSoLDHD, 
				fSoCNVQPCT, fSoQNVQPHD, fTongSo, fSoSQ_KH, fSoHSQBS_KH, fSoCNVQP_KH, fSoLDHD_KH, fSoQNCN_KH, fSoVCQP, 
				fSoCY_H, fSoCY_KT, sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNgaySua)

				select @IdChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, @Thang, iID_MaDonVi, @YearOfWork, 
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0,
					0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 
					0, 0, null, getdate(), @User, null, null
				from #data4
			drop table #qs4, #dv4, #data4
		END

	
END
;
;
;
;
;
GO
