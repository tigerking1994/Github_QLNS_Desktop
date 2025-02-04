/****** Object:  StoredProcedure [dbo].[sp_qs_capnhat_chitiet_year_begin]    Script Date: 11/07/2023 5:21:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_capnhat_chitiet_year_begin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_capnhat_chitiet_year_begin]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_capnhat_chitiet]    Script Date: 11/07/2023 5:21:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_capnhat_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_capnhat_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bk_tonghop]    Script Date: 11/07/2023 5:21:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bk_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bk_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bk_tonghop]    Script Date: 11/07/2023 5:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bk_tonghop]
	@YearOfWork int,
	@QuarterMonth int,
	@LNS nvarchar(max),
	@AgencyId nvarchar(100),
	@DataType int,
	@Dvt int,
    @Loai nvarchar(100)
AS
BEGIN
	SELECT mlns.sLNS,
       mlns.sL,
       mlns.sK,
       mlns.sM,
       mlns.sTM,
       mlns.sTTM,
       mlns.sNG,
       mlns.sTNG,
       mlns.sXauNoiMa,
       mlns.iID_MLNS,
       mlns.iID_MLNS_Cha,
       mlns.sMoTa,
       NoiDung,
       sSoChungTu,
       dNgayChungTu,
       sSoQuyetDinh,
	   sLoai,
       sTenDonVi,
       fTongTuChi / @Dvt AS TuChi,
       fTongHienVat / @Dvt AS HienVat INTO #tblBkTongHop
	FROM
	  (SELECT sLNS,
			  sL,
			  sK,
			  sM,
			  sTM,
			  sTTM,
			  sNG,
			  sTNG,
			  sXauNoiMa,
			  iID_MLNS,
			  iID_MLNS_Cha,
			  ctct.sMoTa AS NoiDung,
			  iID_BKChungTu,
			  sSoChungTu,
			  sLoai,
			  dNgayChungTu,
			  dv.iID_MaDonVi,
			  dv.sTenDonVi,
			  fTongTuChi,
			  fTongHienVat
	   FROM NS_BK_ChungTuChiTiet ctct
	   LEFT JOIN DonVi dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi AND dv.iNamLamViec = ctct.iNamLamViec
	   WHERE iTrangThai=1
		 AND iThangQuy=@QuarterMonth
		 AND (@LNS IS NULL
			  OR sLNS in
				(SELECT *
				 FROM f_split(@LNS)))
		 AND (@AgencyId IS NULL
			  OR ctct.iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))
		 AND ctct.iNamLamViec = @YearOfWork 
	     AND (ISNULL(@Loai, '0') = '0' OR ctct.sLoai = @Loai)
		 AND (@DataType IS NULL
			  OR (@DataType=1
				  AND fTongTuChi<>0)
			  OR (@DataType=2
				  AND fTongHienVat<>0)) ) AS ctct -- lay so ghi so

	LEFT JOIN
	  (SELECT iID_BKChungTu,
			  sSoQuyetDinh,
			  dNgayQuyetDinh
	   FROM NS_BK_ChungTu
	   WHERE iNamLamViec=@YearOfWork) AS ct ON ct.iID_BKChungTu = ctct.iID_BKChungTu
	LEFT JOIN
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
		 AND iTrangThai = 1) AS mlns ON mlns.iID_MLNS = ctct.iID_MLNS;

	WITH LNSTreeParent AS
	  (SELECT sLNS,
			  sL,
			  sK,
			  sM,
			  sTM,
			  sTTM,
			  sNG,
			  sTNG,
			  sXauNoiMa,
			  iID_MLNS,
			  iID_MLNS_Cha,
			  sMoTa ,
			  cast(NoiDung AS nvarchar(MAX)) AS NoiDung ,
			  cast(sSoChungTu AS nvarchar(500)) AS SoChungTu ,
			  CONVERT(NVARCHAR(100), dNgayChungTu, 103) AS NgayChungTu ,
			  cast(sSoQuyetDinh AS nvarchar(500)) AS SoQuyetDinh ,
			  cast(sTenDonVi AS nvarchar(500)) AS TenDonVi ,
			  cast(sLoai AS nvarchar(500)) AS SLoai ,
			  cast(TuChi AS float) AS TuChi ,
			  cast(HienVat AS float) AS HienVat ,
			  cast(0 AS bit) AS IsHangCha
	   FROM #tblBkTongHop
	   UNION ALL SELECT mlnsParent.sLNS,
						mlnsParent.sL,
						mlnsParent.sK,
						mlnsParent.sM,
						mlnsParent.sTM,
						mlnsParent.sTTM,
						mlnsParent.sNG,
						mlnsParent.sTNG,
						mlnsParent.sXauNoiMa,
						mlnsParent.iID_MLNS,
						mlnsParent.iID_MLNS_Cha,
						mlnsParent.sMoTa ,
						cast('' AS nvarchar(MAX)) AS NoiDung ,
						cast('' AS nvarchar(500)) AS SoChungTu ,
						cast('' AS nvarchar(100)) AS NgayChungTu ,
						cast('' AS nvarchar(500)) AS SoQuyetDinh ,
						cast('' AS nvarchar(500)) AS TenDonVi ,
						cast('' AS nvarchar(500)) AS SLoai ,
						cast(0 AS float) AS TuChi ,
						cast(0 AS float) AS HienVat ,
						cast(1 AS bit) AS IsHangCha
	   FROM NS_MucLucNganSach mlnsParent
	   INNER JOIN LNSTreeParent ON mlnsParent.iID_MLNS = LNSTreeParent.iID_MLNS_Cha
	   WHERE mlnsParent.iNamLamViec = @YearOfWork )
	SELECT DISTINCT *
	FROM LNSTreeParent
	ORDER BY sLNS,
			 sL,
			 sK,
			 sM,
			 sTM,
			 sTTM,
			 sNG;


	DROP TABLE #tblBkTongHop;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_capnhat_chitiet]    Script Date: 11/07/2023 5:21:21 PM ******/
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
		ct1.fSoQNCN  = ct2.fSoQNCN ,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_capnhat_chitiet_year_begin]    Script Date: 11/07/2023 5:21:21 PM ******/
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
		ct1.fSoQNCN  = ct2.fSoQNCN ,
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
	) as ct2 on ct1.iID_MaDonVi = ct2.iID_MaDonVi and ct1.sKyHieu = ct2.sKyHieu
	where ct1.iThangQuy = 0;

END
;
;
GO
