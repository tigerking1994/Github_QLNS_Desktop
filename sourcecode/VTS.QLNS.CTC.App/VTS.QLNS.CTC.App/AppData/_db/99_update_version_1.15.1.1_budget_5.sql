/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_lientham]    Script Date: 11/14/2024 1:32:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_lientham]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_lientham]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_lientham]    Script Date: 11/14/2024 1:32:09 PM ******/
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

		----VCQP
		,VCQP_NamTruoc = SUM(
		CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12) or (iNamLamViec = @YearOfWork and iThangQuy = 0))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
		THEN FSoVcqp ELSE 0 END)
		,VCQP_Tang=SUM(CASe WHEN sKyHieu=2 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN FSoVcqp ELSE 0 END)
		,VCQP_Giam=SUM(CASe WHEN sKyHieu=3 AND iNamLamViec = @YearOfWork AND iThangQuy = @Month THEN FSoVcqp ELSE 0 END)
		,VCQP_QuyetToan=SUM(
			CASE WHEN sKyHieu = '700' and ((@Month = 1 and ((iNamLamViec <= @YearOfWorkBefore and iThangQuy = 1) or (iNamLamViec = @YearOfWork and iThangQuy <= 12))) or (@Month <> 1 and ((iNamLamViec = @YearOfWork and iThangQuy < @Month) or (iNamLamViec <= @YearOfWorkBefore and iThangQuy <= 12))))
			THEN FSoVcqp ELSE 0 END)


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
