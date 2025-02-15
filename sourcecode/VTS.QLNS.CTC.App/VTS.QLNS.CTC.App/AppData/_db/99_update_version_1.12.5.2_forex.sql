/****** Object:  StoredProcedure [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]    Script Date: 1/16/2023 11:21:24 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoitaocapphat_chitiet]    Script Date: 1/16/2023 11:21:24 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_kt_khoitaocapphat_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_kt_khoitaocapphat_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hangmuc_bygoithauid]    Script Date: 1/16/2023 11:21:24 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_hangmuc_bygoithauid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_hangmuc_bygoithauid]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]    Script Date: 1/16/2023 11:21:24 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_duan_export_ctc]    Script Date: 1/16/2023 11:21:24 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_get_duan_export_ctc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_get_duan_export_ctc]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chuyendulieu_quyettoan_index]    Script Date: 1/16/2023 11:21:24 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_chuyendulieu_quyettoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_chuyendulieu_quyettoan_index]
GO
/****** Object:  UserDefinedFunction [dbo].[ToRoman]    Script Date: 17/01/2023 9:58:19 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ToRoman]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[ToRoman]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chuyendulieu_quyettoan_index]    Script Date: 1/16/2023 11:21:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_chuyendulieu_quyettoan_index]
AS
BEGIN
	SELECT 
		cqt.ID AS Id,
		cqt.sSoChungTu,
		cqt.dNgayChungTu,
		cqt.iID_DonViID,
		cqt.iLoaiThoiGian,
		cqt.iThoiGian,
		cqt.sMoTa,
		cqt.iID_MaDonVi,
		cqt.sNguoiTao,
		cqt.dNgayTao,
		cqt.sNguoiSua,
		cqt.dNgaySua,
		CONCAT(DonVi.iID_MaDonVi, ' - ', ISNULL(DonVi.sTenDonVi,'')) AS STenDonVi,
		(CASE WHEN cqt.iLoaiThoiGian = 1 THEN 'Tháng'
					WHEN cqt.iLoaiThoiGian = 2 THEN 'Quý'
					ELSE ''
		END) AS STenLoaiThoiGian,
		(CASE WHEN cqt.iLoaiThoiGian = 1 THEN (CASE WHEN cqt.iThoiGian IS NOT NULL OR cqt.iThoiGian <> 0 THEN CONCAT(N'Tháng ', cqt.iThoiGian) ELSE '' END)
					WHEN cqt.iLoaiThoiGian = 2 THEN (CASE WHEN cqt.iThoiGian IS NOT NULL OR cqt.iThoiGian <> 0 THEN CONCAT(N'Quý ', dbo.ToRoman(cqt.iThoiGian)) ELSE '' END)
					ELSE ''
		END) AS STenThoiGian
	FROM NH_QT_ChuyenQuyetToan cqt
	LEFT JOIN DonVi ON cqt.iID_DonViID = DonVi.iID_DonVi
	ORDER BY cqt.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_duan_export_ctc]    Script Date: 1/16/2023 11:21:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_get_duan_export_ctc]
	@iLoai INT = NULL
AS
BEGIN
	SELECT da.Id, da.sMaDuAn as SMaDuAn, da.sTenDuAn as STenDuAn
	       , ct.sSoQuyetDinh as SSoDauTu, CONVERT(varchar, ct.dNgayQuyetDinh, 103) as DNgayDauTu
		   , qd.sSoQuyetDinh as SSoQuyetDinh, CONVERT(varchar, qd.dNgayQuyetDinh, 103) as DNgayQuyetDinh
		   , da.iID_MaChuDauTu as IIdMaChuDauTu, pc.sMa as SMaPhanCapPheDuyet
		   , da.sKhoiCong as SKhoiCong, da.sKetThuc as SKetThuc
	FROM NH_DA_DuAn da
	LEFT JOIN NH_DA_ChuTruongDauTu ct ON ct.iID_DuAnID = da.ID
	LEFT JOIN NH_DA_QDDauTu qd ON qd.iID_DuAnID = da.ID
	LEFT JOIN NH_DM_PhanCapPheDuyet pc ON pc.ID = da.iID_CapPheDuyetID
	WHERE da.iLoai = @iLoai;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]    Script Date: 1/16/2023 11:21:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]
@IdGoiThau uniqueidentifier
AS
BEGIN
	select hd.ID as Id,
	hd.sSoHopDong as SSoHopDong,
	hd.dNgayHopDong as DNgayHopDong,
	hd.iID_LoaiHopDongID as IID_LoaiHopDongID,
	gtnt.iID_NhaThauID as IID_NhaThauID,
	gtntt.fGiaTRiHopDong_USD AS FGiaTriUsd,
	gtntt.fGiaTRiHopDong_VND AS FGiaTriVnd,
	lhd.sTenLoaiHopDong AS STenLoaiHopDong,
	nt.sTenNhaThau as STenNhaThau,
	gtntt.fGiaTRiHopDong_EUR AS FGiaTriEur,
	gtntt.fGiaTriHopDong_NgoaiTeKhac AS FGiaTriNgoaiTeKhac
	FROM NH_DA_HopDong hd
	LEFT JOIN NH_DA_HopDong_GoiThau_NhaThau gtnt on gtnt.iID_HopDongID=hd.ID 
	LEFT JOIN nh_da_goithau gt on gt.iID_GoiThauID = gtnt.iID_GoiThauID
	LEFT JOIN (select iID_HopDongID, sum(fGiaTRiHopDong_USD) as fGiaTRiHopDong_USD
				, sum(fGiaTRiHopDong_VND) as fGiaTRiHopDong_VND
				, sum(fGiaTRiHopDong_EUR) as fGiaTRiHopDong_EUR
				, sum(fGiaTriHopDong_NgoaiTeKhac) as fGiaTriHopDong_NgoaiTeKhac
				from NH_DA_HopDong_GoiThau_NhaThau 
				where NH_DA_HopDong_GoiThau_NhaThau.isCheck=1
				group by iID_HopDongID
	      ) gtntt on hd.ID = gtntt.iID_HopDongID
	LEFT JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
	LEFT JOIN NH_DM_NhaThau nt  on hd.iID_NhaThauThucHienID= nt.Id
	WHERE gtnt.iID_GoiThauID = @idGoiThau

	order by hd.sTenHopDong
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hangmuc_bygoithauid]    Script Date: 1/16/2023 11:21:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[sp_nh_hangmuc_bygoithauid]
	@idGoiThau uniqueidentifier
	
AS BEGIN
	SELECT
		HangMuc.iID_GoiThau_HangMucID as Id,
		HangMuc.isCheck as IsCheck,
		HangMuc.iID_GoiThau_ChiPhiID as IIdGoiThauChiPhiId,
		HangMuc.iIDGoiThauCheck as IIDGoiThauCheck,
		HangMuc.iID_QDDauTu_HangMucID as IIdQDDauTuHangMucId,
		HangMuc.iID_DuToan_HangMucID as IIdDuToanChiPhiId,
		HangMuc.fTienGoiThau_USD as FTienGoiThauUsd,
		HangMuc.fTienGoiThau_VND as FTienGoiThauVnd,
		HangMuc.fTienGoiThau_EUR as FTienGoiThauEur,
		HangMuc.fTienGoiThau_NgoaiTeKhac as FTienGoiThauNgoaiTeKhac,
		QDDT.sTenHangMuc as STenHangMucQDDT,
		DuToan.sTenHangMuc as STenHangMucDT,
		HangMuc.iID_ParentID as IIdParentId,
		DuToanChiPhi.sTenChiPhi as STenChiPhiDT
	FROM NH_DA_GoiThau_HangMuc HangMuc
	LEFT JOIN NH_DA_QDDauTu_HangMuc QDDT
		ON HangMuc.iID_QDDauTu_HangMucID = QDDT.ID
	LEFT JOIN NH_DA_DuToan_HangMuc DuToan
		ON HangMuc.iID_DuToan_HangMucID = DuToan.ID
	LEFT JOIN NH_DA_GoiThau_ChiPhi ChiPhi
	    ON HangMuc.iID_GoiThau_ChiPhiID = ChiPhi.ID
	LEFT JOIN NH_DA_QDDauTu_ChiPhi QDDTChiPhi
		ON ChiPhi.iID_QDDauTu_ChiPhiID = QDDTChiPhi.ID
	LEFT JOIN NH_DA_DuToan_ChiPhi DuToanChiPhi
		ON ChiPhi.iID_DuToan_ChiPhiID = DuToanChiPhi.ID
	WHERE 
		1=1
		AND ChiPhi.iID_GoiThauID = @idGoiThau
	ORDER BY DuToanChiPhi.sTenChiPhi,
	         case 
				when DuToan.iID_ParentID is null
				then DuToan.ID 
				else    (
						select  ID 
						from    NH_DA_DuToan_HangMuc parent 
						where   parent.ID = DuToan.iID_ParentID
						) 
				end
			,case when DuToan.iID_ParentID is null then 1 end desc
			,DuToan.ID

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kt_khoitaocapphat_chitiet]    Script Date: 1/16/2023 11:21:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_kt_khoitaocapphat_chitiet] @KhoiTaoCapPhatId NVARCHAR(2000)
AS
BEGIN
    SELECT
		khoiTaoCP_CT.ID									AS Id,
		khoiTaoCP_CT.iID_KhoiTaoCapPhatID				AS IIdKhoiTaoCapPhatID,
		khoiTaoCP_CT.iID_DuAnID							AS IIdDuAnID,
		khoiTaoCP_CT.iID_HopDongID						AS IIdHopDongID,
		khoiTaoCP_CT.fQTKinhPhiDuyetCacNamTruoc_USD		AS FQTKinhPhiDuyetCacNamTruocUSD,
		khoiTaoCP_CT.fQTKinhPhiDuyetCacNamTruoc_VND		AS FQTKinhPhiDuyetCacNamTruocVND,
		khoiTaoCP_CT.fDeNghiQTNamNay_USD				AS FDeNghiQTNamNayUSD,
		khoiTaoCP_CT.fDeNghiQTNamNay_VND				AS FDeNghiQTNamNayVND,
		khoiTaoCP_CT.fLuyKeKinhPhiDuocCap_USD			AS FLuyKeKinhPhiDuocCapUSD,
		khoiTaoCP_CT.fLuyKeKinhPhiDuocCap_VND			AS FLuyKeKinhPhiDuocCapVND,
		khoiTaoCP_CT.iID_ParentID						AS IIdParentID,
		duAn.sMaDuAn									AS SMaDuAn,
		duAn.sTenDuAn									AS STenDuAn,
		hopDong.sSoHopDong								AS SMaHopDong,
		hopDong.sTenHopDong								AS STenHopDong
		
	FROM NH_KT_KhoiTaoCapPhat_ChiTiet khoiTaoCP_CT
	LEFT JOIN NH_DA_DuAn duAn
		ON khoiTaoCP_CT.iID_DuAnID = duAn.Id
	LEFT JOIN NH_DA_HopDong hopDong
		ON khoiTaoCP_CT.iID_HopDongID = hopDong.Id
	WHERE khoiTaoCP_CT.iID_KhoiTaoCapPhatID = @KhoiTaoCapPhatId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]    Script Date: 1/16/2023 11:21:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]
AS
BEGIN
	SELECT 
		thanhtoan.ID AS IIdPheDuyetThanhToanId,
		chitiet.ID AS Id,
		chitiet.iID_ThongTriCapPhatID AS IIdThongTriCapPhatId,
		chitiet.sMaOrder AS SMaOrder,
		chitiet.iTrangThai AS ITrangThai,
		CASE 
			WHEN thanhtoan.iTrangThai = 2  THEN thanhtoan.sSoDeNghi 
		END AS SSoDeNghi,
		nhiemvuchi.sTenNhiemVuChi AS STenNhiemVuChi,
		hopdong.sTenHopDong AS STenHopDong,
		CASE
			WHEN thanhtoan.iLoaiDeNghi = 1 THEN N'Cấp kinh phí'
			WHEN thanhtoan.iLoaiDeNghi = 2 THEN N'Tạm ứng'
			WHEN thanhtoan.iLoaiDeNghi = 3 THEN N'Thanh toán'
		END AS SLoaiDeNghi,
		SUM(TTchitiet.fPheDuyetCapKyNay_USD) AS FPheDuyetUsd,
		SUM(TTchitiet.fPheDuyetCapKyNay_VND) AS FPheDuyetVnd,
		SUM(TTchitiet.fPheDuyetCapKyNay_EUR) AS FPheDuyetEur,
		SUM(TTchitiet.fPheDuyetCapKyNay_NgoaiTeKhac) AS FPheDuyetNgoaiTeKhac,
		thanhtoan.iNamKeHoach AS iNamKeHoach,
		thanhtoan.iID_DonVi AS iID_DonVi,
		thanhtoan.iID_NguonVonID AS iID_NguonVonID
	FROM NH_TT_ThanhToan thanhtoan 
	LEFT JOIN NH_TT_ThongTriCapPhat_ChiTiet chitiet ON thanhtoan.ID = chitiet.iID_PheDuyetThanhToanID
	LEFT JOIN NH_DM_NhiemVuChi nhiemvuchi ON thanhtoan.iID_NhiemVuChiID = nhiemvuchi.ID
	LEFT JOIN NH_DA_HopDong hopdong ON thanhtoan.iID_HopDongID = hopdong.Id
	LEFT JOIN NH_TT_ThanhToan_ChiTiet TTchitiet ON thanhtoan.ID = TTchitiet.iID_DeNghiThanhToanID
	WHERE thanhtoan.iTrangThai = 2
	GROUP BY thanhtoan.ID, chitiet.ID, chitiet.iID_ThongTriCapPhatID, chitiet.sMaOrder, chitiet.iTrangThai, thanhtoan.iLoaiDeNghi, thanhtoan.iNamKeHoach, thanhtoan.iID_DonVi, thanhtoan.iID_NguonVonID,
	nhiemvuchi.sTenNhiemVuChi, hopdong.sTenHopDong, thanhtoan.iTrangThai, thanhtoan.sSoDeNghi
END
;
GO
/****** Object:  UserDefinedFunction [dbo].[ToRoman]    Script Date: 17/01/2023 9:58:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ToRoman] (@Number INT)
  /**
 summary:   >
 This is a simple routine for converting a decimal integer into a roman numeral.
 Author: Phil Factor
 Revision: 1.2
 date: 3rd Feb 2014
 Why: converted to run on SQL Server 2008-12
 example:
      - code: Select dbo.ToRomanNumerals(187)
      - code: Select dbo.ToRomanNumerals(2011)
 returns:   >
 The Mediaeval-style 'roman' numeral as a string.
 **/   
 RETURNS NVARCHAR(100)
 AS
 BEGIN
  IF @Number<0
     BEGIN
     RETURN 'De romanorum non numero negative'
     end                          
   IF @Number> 200000
     BEGIN
     RETURN 'O Juppiter, magnus numerus'
     end                          
   DECLARE @RomanNumeral AS NVARCHAR(100)
   DECLARE @RomanSystem TABLE (symbol NVARCHAR(20) 
                                   COLLATE SQL_Latin1_General_Cp437_BIN ,
                               DecimalValue INT PRIMARY key)
    INSERT  INTO @RomanSystem (symbol, DecimalValue)
     VALUES('I', 1),
           ('IV', 4),
           ('V', 5),
           ('IX', 9),
           ('X', 10),
           ('XL', 40),
           ('L', 50),
           ('XC', 90),
           ('C', 100),
           ('CD', 400),
           ('D', 500),
           ('CM', 900),
           ('M', 1000),
           (N'|ↄↄ', 5000),
           (N'cc|ↄↄ', 10000),
           (N'|ↄↄↄ', 50000),
           (N'ccc|ↄↄↄ', 100000),
           (N'ccc|ↄↄↄↄↄↄ', 150000)
  
   WHILE @Number > 0
     SELECT  @RomanNumeral = COALESCE(@RomanNumeral, '') + symbol,
             @Number = @Number - DecimalValue
     FROM    @RomanSystem
     WHERE   DecimalValue = (SELECT  MAX(DecimalValue)
                             FROM    @RomanSystem
                             WHERE   DecimalValue <= @number)
   RETURN COALESCE(@RomanNumeral,'nulla');
   End ;
GO

