/****** Object:  StoredProcedure [dbo].[sp_dc_chungtu_chitiet_all]    Script Date: 10/28/2024 6:11:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dc_chungtu_chitiet_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dc_chungtu_chitiet_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_lientham]    Script Date: 10/30/2024 7:22:39 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_lientham]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_lientham]
GO
/****** Object:  StoredProcedure [dbo].[sp_dc_chungtu_chitiet_all]    Script Date: 10/28/2024 6:11:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dc_chungtu_chitiet_all]
	@chungTuIds nvarchar(max),
	@namLamViec int,
	@namNganSach int,
	@nguonNganSach int,
	@lns nvarchar(max),
	@donVi nvarchar(max),
	@loaiDuKien int,
	--@loaiChungTu int,
	--@ngayChungTu datetime,
	@userName nvarchar(50)
AS
BEGIN

SELECT DISTINCT VALUE into #tempLNS
					  FROM
					   (SELECT CAST(LEFT(Item, 1) AS nvarchar(10)) LNS1,
							   CAST(LEFT(Item, 3) AS nvarchar(10)) LNS3,
							   CAST(LEFT(Item, 5) AS nvarchar(10)) LNS5,
							   CAST(Item AS nvarchar(10)) LNS
						FROM f_split(@LNS) ) LNS UNPIVOT (value
																FOR col in (LNS1, LNS3, LNS5, LNS)) un
SELECT DISTINCT VALUE into #tempNguoiDungLNS
						 FROM
						   (SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
								   CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
								   CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
								   CAST(sLNS AS nvarchar(10)) LNS
							FROM NS_NguoiDung_LNS
							WHERE sMaNguoiDung = @userName
							  AND iNamLamViec = @namLamViec
							  AND sLNS IN
								(SELECT *
								 FROM f_split(@LNS)) ) LNS UNPIVOT (value
																	FOR col in (LNS1, LNS3, LNS5, LNS)) un
	DECLARE @CountDonViCha int;

	SELECT @CountDonViCha = count(*)
	FROM
	  (SELECT *
	   FROM NguoiDung_DonVi
	   WHERE iID_MaNguoiDung = @userName
		 AND iNamLamViec = @namLamViec
		 AND iTrangThai = 1) nddv
	INNER JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @namLamViec
		 AND iTrangThai = 1
		 AND iLoai = 0) dv ON dv.iID_MaDonVi = nddv.iID_MaDonVi;



		select 
			iID_MLNS, 
			iID_MLNS_Cha, 
			sXauNoiMa, 
			--iID_MaDonVi, 
			iNamLamViec, 
			iNamNganSach, 
			iID_MaNguonNganSach, 
			sum(fTuChi) as TuChi, 
			sum(fHangNhap) as HangNhap, 
			sum(fHangMua) as HangMua INTO #tblDuToanNganSachNam
		from NS_DT_ChungTuChiTiet
		where iNamLamViec = @namLamViec 
		and iNamNganSach = @namNganSach 
		and iID_MaNguonNganSach = @nguonNganSach
		and iID_MaDonVi in (select * from f_split(@donVi))
		and iID_DTChungTu in 
			(select 
				iID_DTChungTu 
			from NS_DT_ChungTu 
			where iNamLamViec = @namLamViec 
			and iNamNganSach = @namNganSach 
			and iID_MaNguonNganSach = @nguonNganSach
			--and ((iLoai = 1 AND @CountDonViCon > 0) OR (iLoai = 0 AND @CountDonViCon = 0))
			--and iLoaiChungTu = @loaiChungTu
			and bKhoa = 1
			and ISNULL(sSoQuyetDinh, '') <> '')
			--and cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
		--group by iID_MLNS, iID_MLNS_Cha, sXauNoiMa, iID_MaDonVi, iNamLamViec, iNamNganSach, iID_MaNguonNganSach
		group by iID_MLNS, iID_MLNS_Cha, sXauNoiMa, iNamLamViec, iNamNganSach, iID_MaNguonNganSach;
	




		select 
			iID_MLNS, 
			iID_MLNS_Cha, 
			sLNS, 
			--iID_MaDonVi,
			sum(fTuChi_PheDuyet) as TuChi INTO #tblQuyetToanDauNam
		from NS_QT_ChungTuChiTiet
		where iNamLamViec = @namLamViec 
		and iNamNganSach = @namNganSach 
		and iID_MaNguonNganSach = @nguonNganSach
		and iID_MaDonVi in (select * from f_split(@donVi))
		and iID_QTChungTu in
			(select 
				iID_QTChungTu 
			from NS_QT_ChungTu 
			where iNamLamViec = @namLamViec 
			and iNamNganSach = @namNganSach 
			and iID_MaNguonNganSach = @nguonNganSach
			and ((@loaiDuKien = 1 and iThangQuy <= 6) 
				or
				(@loaiDuKien = 2 and iThangQuy <= 9)))
			--and cast(dNgayChungTu as date) <= @ngayChungTu)
			--group by iID_MLNS, iID_MLNS_Cha, sLNS, iID_MaDonVi
			group by iID_MLNS, iID_MLNS_Cha, sLNS;
	
		SELECT DISTINCT VALUE INTO #tblMlnsQuyetToanDauNam
		FROM 
		(
			SELECT 
				CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
				CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
				CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
				CAST(sLNS AS nvarchar(10)) LNS 
			FROM
				#tblQuyetToanDauNam
		) LNS
		UNPIVOT
		(
			value
			FOR col in (LNS1, LNS3, LNS5, LNS)
		) un;

		SELECT mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			quyetToan.TuChi INTO #tblQuyetToanDauNamWithMlns
			--quyetToan.iID_MaDonVi
		FROM (SELECT * FROM NS_MucLucNganSach where iNamLamViec = @namLamViec and sLNS in (SELECT * FROM #tblMlnsQuyetToanDauNam)) mlns
		LEFT JOIN
			#tblQuyetToanDauNam quyetToan
		ON mlns.iID_MLNS = quyetToan.iID_MLNS;
	WITH C AS  
	(  
		SELECT T.iID_MLNS,  
				T.TuChi, 
				T.iID_MLNS AS RootID 
				--T.iID_MaDonVi
		FROM #tblQuyetToanDauNamWithMlns T
		UNION ALL 
		SELECT T.iID_MLNS,  
				T.TuChi, 
				C.RootID
				--T.iID_MaDonVi
		FROM #tblQuyetToanDauNamWithMlns T
		INNER JOIN C
		on T.iID_MLNS_Cha = C.iID_MLNS 
	)
	select * INTO #C FROM C;

		SELECT iID_MLNS, 
			iID_MLNS_Cha, 
			sum(TuChi) AS TuChi INTO #tblQuyetToanDauNamData
			--iID_MaDonVi
		FROM 
		(
			SELECT T.iID_MLNS,  
				T.iID_MLNS_Cha,
				total.TuChi
				--T.iID_MaDonVi
			FROM #tblQuyetToanDauNamWithMlns T  
			LEFT JOIN 
				(  
					SELECT RootID, sum(TuChi) AS TuChi
					FROM #C
					GROUP BY RootID
				) AS total 
			ON T.iID_MLNS = total.RootID 
			WHERE total.TuChi <> 0
		) data
		--GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
		GROUP BY iID_MLNS, iID_MLNS_Cha
	

	SELECT dc.*, dv.sTenDonVi FROM (
		SELECT 
			dcctct.iID_DCCTChiTiet AS iID_DCCTChiTiet,
			dcctct.iID_DCChungTu,
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.sTNG,
			mlns.sTNG1,
			mlns.sTNG2,
			mlns.sTNG3,
			mlns.sMoTa,
			mlns.bHangCha,
			isnull(dcctct.iNamNganSach, @namNganSach) as iNamNganSach,
			isnull(dcctct.iID_MaNguonNganSach, @nguonNganSach) as iID_MaNguonNganSach,
			isnull(dcctct.iNamLamViec, @namLamViec) as iNamLamViec,
			--isnull(dcctct.iID_MaDonVi, isnull(dtNsNam.iID_MaDonVi, qtdn.iID_MaDonVi)) as iID_MaDonVi,
			dcctct.iID_MaDonVi as iID_MaDonVi,
			dcctct.sGhiChu,
			case 
				when dcctct.fDuToan is null then 
					case 
						when mlns.sLNS = '1040200' then isnull(dtNsNam.TuChi, 0) + isnull(dtNsNam.HangNhap, 0) 
						when mlns.sLNS = '1040300' then isnull(dtNsNam.TuChi, 0) + isnull(dtNsNam.HangMua, 0) 
						else isnull(dtNsNam.TuChi, 0)
					end
				else dcctct.fDuToan
				end as DuToanNganSachNam,
			isnull(dcctct.fDuKienQtDauNam, qtdn.TuChi) as DuKienQtDauNam,
			isnull(dcctct.fDuKienQtCuoiNam, 0) as DuKienQtCuoiNam,
			isnull(dcctct.fDuToanChuyenNamSau, 0) as DuToanChuyenNamSau,
			mlns.sChiTietToi,
			mlns.bHangChaDuToan,
			dcctct.dNgayTao,
			dcctct.dNgaySua,
			dcctct.sNguoiTao,
			dcctct.sNguoiSua
		FROM
		  (SELECT *
		   FROM NS_MucLucNganSach
		   WHERE iNamLamViec = @namLamViec
			 AND iTrangThai = 1
			 AND bHangChaDuToan is not null
			 AND ((@CountDonViCha <> 0
				   AND sLNS in (select * from #tempLNS))
				  OR (@CountDonViCha = 0
					  AND sLNS in
						(select * from #tempNguoiDungLNS)))) mlns

	LEFT JOIN
		#tblDuToanNganSachNam dtNsNam
	ON dtNsNam.sXauNoiMa = mlns.sXauNoiMa
	LEFT JOIN
		#tblQuyetToanDauNamData qtdn
	ON qtdn.iID_MLNS = mlns.iID_MLNS 
	LEFT JOIN
	(select * from NS_DC_ChungTuChiTiet 
		where iNamLamViec = @namLamViec
		and iNamNganSach = @namNganSach
		and iID_MaNguonNganSach = @nguonNganSach
		and iID_DCChungTu  IN (SELECT *
								 FROM f_split(@chungTuIds))) dcctct
	on dcctct.sXauNoiMa = mlns.sXauNoiMa
	) dc
	LEFT JOIN
		(SELECT * FROM DonVi where iNamLamViec = @namLamViec) dv
	ON dv.iID_MaDonVi = dc.iID_MaDonVi
	order by sXauNoiMa
	option (maxrecursion 0)

	DROP TABLE #tblDuToanNganSachNam;
	DROP TABLE #tblMlnsQuyetToanDauNam;
	DROP TABLE #tblQuyetToanDauNam;
	DROP TABLE #tblQuyetToanDauNamWithMlns;
	DROP TABLE #C;
	DROP TABLE #tblQuyetToanDauNamData;
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
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_lientham]    Script Date: 10/30/2024 7:22:39 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_lientham]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_lientham]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_lientham]    Script Date: 10/30/2024 7:22:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
-------------------------------------------------
			----QNCN
		,ThuongTa_QNCN_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongTa_QNCN ELSE 0 END)
		,ThuongTa_QNCN_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongTa_QNCN ELSE 0 END)
		,ThuongTa_QNCN_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongTa_QNCN ELSE 0 END)
		,ThuongTa_QNCN_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongTa_QNCN ELSE 0 END)

			----QNCN
		,TrungTa_QNCN_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungTa_QNCN ELSE 0 END)
		,TrungTa_QNCN_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungTa_QNCN ELSE 0 END)
		,TrungTa_QNCN_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungTa_QNCN ELSE 0 END)
		,TrungTa_QNCN_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungTa_QNCN ELSE 0 END)
			----QNCN
		,ThieuTa_QNCN_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThieuTa_QNCN ELSE 0 END)
		,ThieuTa_QNCN_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThieuTa_QNCN ELSE 0 END)
		,ThieuTa_QNCN_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThieuTa_QNCN ELSE 0 END)
		,ThieuTa_QNCN_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThieuTa_QNCN ELSE 0 END)
			----QNCN
		,DaiUy_QNCN_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoDaiUy_QNCN ELSE 0 END)
		,DaiUy_QNCN_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoDaiUy_QNCN ELSE 0 END)
		,DaiUy_QNCN_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoDaiUy_QNCN ELSE 0 END)
		,DaiUy_QNCN_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoDaiUy_QNCN ELSE 0 END)
			----QNCN
		,ThuongUy_QNCN_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongUy_QNCN ELSE 0 END)
		,ThuongUy_QNCN_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongUy_QNCN ELSE 0 END)
		,ThuongUy_QNCN_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThuongUy_QNCN ELSE 0 END)
		,ThuongUy_QNCN_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThuongUy_QNCN ELSE 0 END)
			----QNCN
		,TrungUy_QNCN_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungUy_QNCN ELSE 0 END)
		,TrungUy_QNCN_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungUy_QNCN ELSE 0 END)
		,TrungUy_QNCNN_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoTrungUy_QNCN ELSE 0 END)
		,TrungUy_QNCN_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoTrungUy_QNCN ELSE 0 END)
			----QNCN
		,ThieuUy_QNCN_NamTruoc = SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThieuUy_QNCN ELSE 0 END)
		,ThieuUy_QNCN_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThieuUy_QNCN ELSE 0 END)
		,ThieuUy_QNCN_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN fSoThieuUy_QNCN ELSE 0 END)
		,ThieuUy_QNCN_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN fSoThieuUy_QNCN ELSE 0 END)
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
;
GO
