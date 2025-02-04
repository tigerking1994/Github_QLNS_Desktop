
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]    Script Date: 7/10/2024 4:44:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 7/10/2024 4:44:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_giaithich_trocap]    Script Date: 7/10/2024 4:44:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_giaithich_trocap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_giaithich_trocap]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_giaithich_trocap]    Script Date: 7/10/2024 4:44:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_giaithich_trocap]
	-- Add the parameters for the stored procedure here
	@lstmaCanbo nvarchar(max) ,
	@Thang int ,
	@Nam int ,
	@DonViTinh int ,
	@TypeOutPut int  -- 2: Đơn vị; 1: theo đối tượng
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	--Declare ma phu cap --
	DECLARE @LstMaPhuCapBDN_D14N nvarchar(1000) = 'BDN_D14N_PCCVBH_TT,BDN_D14N_PCTNBH_TT,BDN_D14N_HSBLBH_TT,BDN_D14N_LBH_TT,BDN_D14N_PCTNVKBH_TT,BENHDAINGAY_D14NGAY';
	DECLARE @LstMaPhuCapBDN_T14N nvarchar(1000) = 'BDN_T14N_PCCVBH_TT,BDN_T14N_PCTNBH_TT,BDN_T14N_HSBLBH_TT,BDN_T14N_LBH_TT,BDN_T14N_PCTNVKBH_TT,BENHDAINGAY_T14NGAY';
	DECLARE @LstMaPhuCapOK_D14N nvarchar(1000) = 'OK_D14N_PCCVBH_TT,OK_D14N_PCTNBH_TT,OK_D14N_HSBLBH_TT,OK_D14N_LBH_TT,OK_D14N_PCTNVKBH_TT,OMKHAC_D14NGAY';
	DECLARE @LstMaPhuCapOK_T14N nvarchar(1000) = 'OK_T14N_PCCVBH_TT,OK_T14N_PCTNBH_TT,OK_T14N_HSBLBH_TT,OK_T14N_LBH_TT,OK_T14N_PCTNVKBH_TT,OMKHAC_T14NGAY';
	DECLARE @LstMaPhuCap_CON_OM nvarchar(1000) = 'CONOM_PCCVBH_TT,CONOM_PCTNBH_TT,CONOM_HSBLBH_TT,CONOM_LBH_TT,CONOM_PCTNVKBH_TT,CONOM';

	DECLARE @NameBDN_D14 nvarchar(1000) = N'Bệnh dài ngày - Dưới 14 ngày';
	DECLARE @NameBDN_T14 nvarchar(1000)=N'Bệnh dài ngày - Từ 14 ngày trở lên';
	DECLARE @NameOK_D14 nvarchar(1000)=N'Ốm khác - Dưới 14 ngày';
	DECLARE @NameOK_T14 nvarchar(1000)=N'Ốm khác - Từ 14 ngày trở lên';
	DECLARE @NameCON_OM nvarchar(1000)=N'Con ốm';


	CREATE TABLE #tempResult(STT nvarchar(6) ,TenChiTieu nvarchar(1000), MaCapBac nvarchar(50),MaCanBo nvarchar(50), TenCanBo  nvarchar(500), MaDonVi nvarchar(50), PCCVBH_TT numeric, PCTNBH_TT numeric, HSBLBH_TT numeric,  LBH_TT numeric, PCTNVKBH_TT numeric, Total numeric, LoaiDoiTuong nvarchar(50), rowNumber int)
    -- Insert statements for procedure here
	-- Bệnh dài ngày dứoi 14 ngày
	INSERT INTO #tempResult(STT, TenChiTieu, MaCapBac,MaCanBo, TenCanBo, MaDonVi, PCCVBH_TT, PCTNBH_TT, HSBLBH_TT, LBH_TT, PCTNVKBH_TT, Total, LoaiDoiTuong,rowNumber)
	SELECT CAST('1' as nvarchar(6)), @NameBDN_D14 , sMaCB, sMaCBo, sTenCbo,sMaDonVi,
	  BDN_D14N_PCCVBH_TT,BDN_D14N_PCTNBH_TT,BDN_D14N_HSBLBH_TT,BDN_D14N_LBH_TT,BDN_D14N_PCTNVKBH_TT,BENHDAINGAY_D14NGAY,
	  CASE
		WHEN sMaCB LIKE '1%' THEN 'SQ'
		WHEN sMaCB LIKE '2%' THEN 'QNCN'
		WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
		WHEN sMaCB = '43' THEN 'LDHD'
		ELSE
			NULL
	 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB = '43' THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapBDN_D14N)) AND iThang =@Thang and iNam = @Nam
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (BDN_D14N_PCCVBH_TT,BDN_D14N_PCTNBH_TT,BDN_D14N_HSBLBH_TT,BDN_D14N_LBH_TT,BDN_D14N_PCTNVKBH_TT,BENHDAINGAY_D14NGAY)  
	) AS PivotTable
	UNION
	-- Bệnh dài ngày trên 14 ngày
	SELECT  '2',@NameBDN_T14, sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			BDN_T14N_PCCVBH_TT,BDN_T14N_PCTNBH_TT,BDN_T14N_HSBLBH_TT,BDN_T14N_LBH_TT,BDN_T14N_PCTNVKBH_TT,BENHDAINGAY_T14NGAY,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB = '43' THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB = '43' THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapBDN_T14N)) AND iThang =@Thang and iNam = @Nam
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (BDN_T14N_PCCVBH_TT,BDN_T14N_PCTNBH_TT,BDN_T14N_HSBLBH_TT,BDN_T14N_LBH_TT,BDN_T14N_PCTNVKBH_TT,BENHDAINGAY_T14NGAY)  
	) AS PivotTable
		UNION

	--Ốm khác dưới 14 ngày
	SELECT  '3',@NameOK_D14,  sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			OK_D14N_PCCVBH_TT,OK_D14N_PCTNBH_TT,OK_D14N_HSBLBH_TT,OK_D14N_LBH_TT,OK_D14N_PCTNVKBH_TT,OMKHAC_D14NGAY,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB = '43' THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB = '43' THEN 5
		ELSE
			NULL
	 END
	 FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapOK_D14N)) AND iThang =@Thang and iNam = @Nam
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (OK_D14N_PCCVBH_TT,OK_D14N_PCTNBH_TT,OK_D14N_HSBLBH_TT,OK_D14N_LBH_TT,OK_D14N_PCTNVKBH_TT,OMKHAC_D14NGAY)  
	) AS PivotTable
		UNION

	-- Ốm khác trên 14 ngày
	SELECT '4', @NameOK_T14, sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			OK_T14N_PCCVBH_TT,OK_T14N_PCTNBH_TT,OK_T14N_HSBLBH_TT,OK_T14N_LBH_TT,OK_T14N_PCTNVKBH_TT,OMKHAC_T14NGAY,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB = '43' THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB = '43' THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapOK_T14N)) AND iThang =@Thang and iNam = @Nam
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (OK_T14N_PCCVBH_TT,OK_T14N_PCTNBH_TT,OK_T14N_HSBLBH_TT,OK_T14N_LBH_TT,OK_T14N_PCTNVKBH_TT,OMKHAC_T14NGAY)  
	) AS PivotTable

	UNION
	-- con ốm
	SELECT '5', @NameCON_OM, sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			CONOM_PCCVBH_TT,CONOM_PCTNBH_TT,CONOM_HSBLBH_TT,CONOM_LBH_TT,CONOM_PCTNVKBH_TT,CONOM,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB = '43' THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB = '43' THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCap_CON_OM)) AND iThang =@Thang and iNam = @Nam
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (CONOM_PCCVBH_TT,CONOM_PCTNBH_TT,CONOM_HSBLBH_TT,CONOM_LBH_TT,CONOM_PCTNVKBH_TT,CONOM)  
	) AS PivotTable;  


	INSERT INTO #tempResult(STT, TenChiTieu, MaCapBac,MaCanBo, TenCanBo, MaDonVi, PCCVBH_TT, PCTNBH_TT, HSBLBH_TT, LBH_TT, PCTNVKBH_TT, Total,LoaiDoiTuong,rowNumber)
	SELECT '0',TenCanBo,MaCapBac, MaCanBo,TenCanBo, MaDonVi, 
			SUM(ISNULL(PCCVBH_TT,0)) PCCVBH_TT,
			SUM(ISNULL(PCTNBH_TT,0)) PCTNBH_TT, 
			SUM(ISNULL(HSBLBH_TT,0)) HSBLBH_TT, 
			SUM(ISNULL(LBH_TT,0)) LBH_TT, 
			SUM(ISNULL(PCTNVKBH_TT,0)) PCTNVKBH_TT,
			SUM(ISNULL(Total,0)) Total,
			LoaiDoiTuong,
			rowNumber
	FROM #tempResult group by MaCanBo, TenCanBo, MaCapBac,MaDonVi, LoaiDoiTuong,rowNumber;
	--SELECT LEFT(MaCapBac, 1),* FROM #tempResult ORDER BY MaCanBo , STT;
	IF(@TypeOutPut = 2)
		BEGIN
		--Lấy Đơn vị
			SELECT 
				0 as Level,
				CAST (NULL as nvarchar(6)) STT,
				CAST (NULL as nvarchar(50)) LoaiDoiTuong,
				donvi.Ten_DonVi as TenChiTieu,
				CAST (NULL as nvarchar(50)) MaCapBac,
				CAST (NULL as nvarchar(50)) MaCanBo,
				CAST (NULL as nvarchar(50)) TenCanBo,
				rs.MaDonVi MaDonVi, 
				donvi.Ten_DonVi as TenDonVi,
				SUM(ISNULL(PCCVBH_TT,0))/@DonViTinh PCCVBH_TT,
				SUM(ISNULL(PCTNBH_TT,0))/@DonViTinh PCTNBH_TT, 
				SUM(ISNULL(HSBLBH_TT,0))/@DonViTinh HSBLBH_TT, 
				SUM(ISNULL(LBH_TT,0))/@DonViTinh LBH_TT, 
				SUM(ISNULL(PCTNVKBH_TT,0))/@DonViTinh PCTNVKBH_TT,
				SUM(ISNULL(Total,0))/@DonViTinh Total,
				0 as rowNumber
				INTO #tempDonVi
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
				WHERE rs.STT <> 0
				GROUP BY rs.MaDonVi, donvi.Ten_DonVi
				ORDER BY rs.MaDonVi;
			-- Lấy Loại đối tượng
			SELECT 
				1 Level,
	  			CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 'I'
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 'II'
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 'III'
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 'IV'
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 'V'
					ELSE
						NULL
				END AS nvarchar(6))  STT,
				rs.LoaiDoiTuong,
				rs.LoaiDoiTuong as TenChiTieu,
				CAST (NULL as nvarchar(50)) MaCapBac,
				CAST (NULL as nvarchar(50)) MaCanBo,
				CAST (NULL as nvarchar(50)) TenCanBo,
				rs.MaDonVi MaDonVi, 
				donvi.Ten_DonVi as TenDonVi,
				SUM(ISNULL(PCCVBH_TT,0))/@DonViTinh PCCVBH_TT,
				SUM(ISNULL(PCTNBH_TT,0))/@DonViTinh PCTNBH_TT, 
				SUM(ISNULL(HSBLBH_TT,0))/@DonViTinh HSBLBH_TT, 
				SUM(ISNULL(LBH_TT,0))/@DonViTinh LBH_TT, 
				SUM(ISNULL(PCTNVKBH_TT,0))/@DonViTinh PCTNVKBH_TT,
				SUM(ISNULL(Total,0))/@DonViTinh Total,
				CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 1
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 2
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 3
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 4
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 5
					ELSE
						NULL
				END AS int)  rowNumber
				INTO #tempLoaiDoiTuong
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
				WHERE rs.STT <> '0'
				GROUP BY rs.MaDonVi,rs.LoaiDoiTuong,donvi.Ten_DonVi

			-- lấy chi tiết từng đối tượng
			SELECT 
				CASE
					WHEN STT = '0' THEN 2 
					ELSE 3
				END AS Level,
				STT,
				LoaiDoiTuong,
				TenChiTieu,
				MaCapBac, 
				MaCanBo,
				TenCanBo,
				rs.MaDonVi MaDonVi, 
				donvi.Ten_DonVi as TenDonVi,
				ISNULL(PCCVBH_TT,0)/@DonViTinh PCCVBH_TT,
				ISNULL(PCTNBH_TT,0)/@DonViTinh PCTNBH_TT, 
				ISNULL(HSBLBH_TT,0)/@DonViTinh HSBLBH_TT, 
				ISNULL(LBH_TT,0)/@DonViTinh LBH_TT, 
				ISNULL(PCTNVKBH_TT,0)/@DonViTinh PCTNVKBH_TT,
				ISNULL(Total,0)/@DonViTinh Total,
				rs.rowNumber
				INTO #tempCanBo 
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi;

				--OUTPUT---
			SELECT rs.*  FROM
			(
			SELECT * FROM #tempDonVi
			UNION 
			SELECT * FROM #tempLoaiDoiTuong
			UNION
			SELECT * FROM #tempCanBo
			) rs
			ORDER BY rs.MaDonVi,rs.rowNumber,rs.MaCanBo

			DROP TABLE #tempDonVi;
			DROP TABLE #tempLoaiDoiTuong;
			DROP TABLE #tempCanBo;
		END
	ELSE
		BEGIN 
-- Lấy Loại đối tượng
			SELECT 
				1 Level,
	  			CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 'I'
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 'II'
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 'III'
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 'IV'
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 'V'
					ELSE
						NULL
				END AS nvarchar(6))  STT,
				rs.LoaiDoiTuong,
				rs.LoaiDoiTuong as TenChiTieu,
				CAST (NULL as nvarchar(50)) MaCapBac,
				CAST (NULL as nvarchar(50)) MaCanBo,
				CAST (NULL as nvarchar(50)) TenCanBo,
				CAST (NULL as nvarchar(50)) MaDonVi,
				SUM(ISNULL(PCCVBH_TT,0))/@DonViTinh PCCVBH_TT,
				SUM(ISNULL(PCTNBH_TT,0))/@DonViTinh PCTNBH_TT, 
				SUM(ISNULL(HSBLBH_TT,0))/@DonViTinh HSBLBH_TT, 
				SUM(ISNULL(LBH_TT,0))/@DonViTinh LBH_TT, 
				SUM(ISNULL(PCTNVKBH_TT,0))/@DonViTinh PCTNVKBH_TT,
				SUM(ISNULL(Total,0))/@DonViTinh Total,
				CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 1
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 2
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 3
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 4
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 5
					ELSE
						NULL
				END AS int)  rowNumber
				INTO #tempLoaiDoiTuong2
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
				WHERE rs.STT <> '0'
				GROUP BY rs.LoaiDoiTuong

			-- lấy chi tiết từng đối tượng
			SELECT 
				CASE
					WHEN STT = '0' THEN 2 
					ELSE 3
				END AS Level,
				STT,
				LoaiDoiTuong,
				TenChiTieu,
				MaCapBac, 
				MaCanBo,
				TenCanBo,
				CASE
					WHEN STT = '0' THEN rs.MaDonVi 
					ELSE NULL
				END AS MaDonVi,
				ISNULL(PCCVBH_TT,0)/@DonViTinh PCCVBH_TT,
				ISNULL(PCTNBH_TT,0)/@DonViTinh PCTNBH_TT, 
				ISNULL(HSBLBH_TT,0)/@DonViTinh HSBLBH_TT, 
				ISNULL(LBH_TT,0)/@DonViTinh LBH_TT, 
				ISNULL(PCTNVKBH_TT,0)/@DonViTinh PCTNVKBH_TT,
				ISNULL(Total,0)/@DonViTinh Total,
				rs.rowNumber
				INTO #tempCanBo2 
				FROM #tempResult rs

				--OUTPUT---
			SELECT rs.*,donvi.Ten_DonVi as TenDonVi  FROM
			(
			SELECT * FROM #tempLoaiDoiTuong2
			UNION
			SELECT * FROM #tempCanBo2
			) rs
			LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
			ORDER BY rs.rowNumber,rs.MaCanBo

			DROP TABLE #tempLoaiDoiTuong2;
			DROP TABLE #tempCanBo2;		
		END
	DROP TABLE #tempResult;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 7/10/2024 4:44:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0,
@IsNew AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)
DECLARE @ThangTruoc AS int
DECLARE @NamTruoc AS int

IF @Thang = 1 
BEGIN
	SET @ThangTruoc = 12;
	SET @NamTruoc = @Nam - 1;
END
ELSE 
BEGIN
	SET @ThangTruoc =  @Thang - 1;
	SET @NamTruoc = @Nam;
END

SET @Cols = 'PCNU_TT,PCCOV_HS,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo canbo
			Where 
				canbo.Thang = ' + CAST(@ThangTruoc AS VARCHAR(2)) + '
				And canbo.Nam = ' + CAST(@NamTruoc AS VARCHAR(4)) + '
				AND canbo.Parent IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		),
blt AS (
	SELECT * FROM TL_BangLuong_Thang
	WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	AND ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (Ma_Hieu_CanBo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = Ma_DonVi)))
),
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT,
		PCTNVK_TT.GiaTri_PCTNVKBH_TT,
		HSBL_TT.GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'', ''KT_LBH_TT'', ''NAMNGHIVIEC_LBH_TT'', ''KHHGD_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_PCCVBH_TT'', ''KT_PCCVBH_TT'', ''NAMNGHIVIEC_PCCVBH_TT'', ''KHHGD_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_PCTNBH_TT'', ''KT_PCTNBH_TT'', ''NAMNGHIVIEC_PCTNBH_TT'', ''KHHGD_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNVKBH_TT'', ''OK_T14N_PCTNVKBH_TT'', ''BDN_D14N_PCTNVKBH_TT'', ''BDN_T14N_PCTNVKBH_TT'', ''CONOM_PCTNVKBH_TT'', ''KT_PCTNVKBH_TT'', ''NAMNGHIVIEC_PCTNVKBH_TT'', ''KHHGD_PCTNVKBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_HSBLBH_TT'', ''OK_T14N_HSBLBH_TT'', ''BDN_D14N_HSBLBH_TT'', ''BDN_T14N_HSBLBH_TT'', ''CONOM_HSBLBH_TT'', ''KT_HSBLBH_TT'', ''NAMNGHIVIEC_HSBLBH_TT'', ''KHHGD_HSBLBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_PhuCap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTNVK_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''HSBL_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM blt bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi AND bangLuong.NAM = bhxh.iNam AND bangLuong.THANG = bhxh.iThang
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
canBo.Nam_TN AS NTN,
dbo.f_luong_ntn(canBo.Ngay_NN, canBo.Ngay_XN, canBo.Ngay_TN, canBo.Thang_TNN, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') AS NamTn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.NamTN,
canBo.Tnn,
--canBo.NTN,
canBo.XauNoiMa
into tbl_luong_bhxh
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)

UPDATE tbl_luong_bhxh SET 
PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) * isnull(PCCOV_HS, 0)

UPDATE tbl_luong_bhxh SET
LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0) + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)

-- Update so phai tru BHXH, BHYT, BHTN khi hưởng chế dộ khác BENHDAINGAY_TD14NGAY và OMKHAC_D14NGAY
UPDATE tbl_luong_bhxh SET BHXHCN_TT = 0, BHYTCN_TT = 0, BHTNCN_TT = 0
WHERE MaCanBo in (
select sMaCBo from TL_BangLuong_ThangBHXH
where iNam = @Nam
and iThang = @Thang
and sMaCheDo in ('BENHDAINGAY_T14NGAY', 'OMKHAC_T14NGAY', 'CONOM', 'OMDAU_DUONGSUCPHSK', 'TAINANLD_DUONGSUCPHSK', 'KHAMTHAI', 'KHHGD', 'NAMNGHIKHIVOSINHCON', 'THAISAN_DUONGSUCPHSK'))

UPDATE tbl_luong_bhxh SET
BHCN_TT = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0)

UPDATE tbl_luong_bhxh SET
PHAITRU_SUM = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0) + isnull(THUETNCN_TT, 0) + isnull(TA_TONG, 0) + isnull(PHAITRUKHAC_SUM, 0)

UPDATE tbl_luong_bhxh SET
THANHTIEN = isnull(LUONGTHANG_SUM, 0) - isnull(PHAITRU_SUM, 0)

select * from tbl_luong_bhxh;

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_bhxh]') AND type in (N'U')) drop table tbl_luong_bhxh;

END
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]    Script Date: 7/10/2024 4:44:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'PCNU_TT,PCCOV_HS,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH 
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT,
		PCTNVK_TT.GiaTri_PCTNVKBH_TT,
		HSBL_TT.GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'', ''KT_LBH_TT'', ''NAMNGHIVIEC_LBH_TT'', ''KHHGD_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_PCCVBH_TT'', ''KT_PCCVBH_TT'', ''NAMNGHIVIEC_PCCVBH_TT'', ''KHHGD_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_PCTNBH_TT'', ''KT_PCTNBH_TT'', ''NAMNGHIVIEC_PCTNBH_TT'', ''KHHGD_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_LBH_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNVKBH_TT'', ''OK_T14N_PCTNVKBH_TT'', ''BDN_D14N_PCTNVKBH_TT'', ''BDN_T14N_PCTNVKBH_TT'', ''CONOM_PCTNVKBH_TT'', ''KT_PCTNVKBH_TT'', ''NAMNGHIVIEC_PCTNVKBH_TT'', ''KHHGD_PCTNVKBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_HSBLBH_TT'', ''OK_T14N_HSBLBH_TT'', ''BDN_D14N_HSBLBH_TT'', ''BDN_T14N_HSBLBH_TT'', ''CONOM_HSBLBH_TT'', ''KT_HSBLBH_TT'', ''NAMNGHIVIEC_HSBLBH_TT'', ''KHHGD_HSBLBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_PhuCap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTNVK_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''HSBL_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM TL_BangLuong_Thang bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi AND bangLuong.NAM = bhxh.iNam AND bangLuong.THANG = bhxh.iThang
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.Tnn,
canBo.XauNoiMa
into tbl_luong_bhxh_2
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)

UPDATE tbl_luong_bhxh_2 SET 
PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) * isnull(PCCOV_HS, 0)

UPDATE tbl_luong_bhxh_2 SET
LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0) + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)

-- Update so phai tru BHXH, BHYT, BHTN khi hưởng chế dộ khác BENHDAINGAY_TD14NGAY và OMKHAC_D14NGAY
UPDATE tbl_luong_bhxh_2 SET BHXHCN_TT = 0, BHYTCN_TT = 0, BHTNCN_TT = 0
WHERE MaCanBo in (
select sMaCBo from TL_BangLuong_ThangBHXH
where iNam = @Nam
and iThang = @Thang
and sMaCheDo in ('BENHDAINGAY_T14NGAY', 'OMKHAC_T14NGAY', 'CONOM', 'OMDAU_DUONGSUCPHSK', 'TAINANLD_DUONGSUCPHSK', 'KHAMTHAI', 'KHHGD', 'NAMNGHIKHIVOSINHCON', 'THAISAN_DUONGSUCPHSK'))

UPDATE tbl_luong_bhxh_2 SET
BHCN_TT = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0)

UPDATE tbl_luong_bhxh_2 SET
PHAITRU_SUM = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0) + isnull(THUETNCN_TT, 0) + isnull(TA_TONG, 0) + isnull(PHAITRUKHAC_SUM, 0)

UPDATE tbl_luong_bhxh_2 SET
THANHTIEN = isnull(LUONGTHANG_SUM, 0) - isnull(PHAITRU_SUM, 0)

select * from tbl_luong_bhxh_2;

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_bhxh_2]') AND type in (N'U')) drop table tbl_luong_bhxh_2;

END
;
;
;
;
;
GO


---2025---
--TH1: Mục lục thêm mục con
--Mục cha đã có trong master data: 9020001-010-011-0001-0003: Phu nhân phu quân - Khối dự toán
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0003-0001' and iNamLamViec = 2025;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:35:25.1095754' AS DateTime2), CAST(N'2024-06-26T11:31:44.2307383' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0003' and iNamLamViec = 2025)
, NULL, NULL, N'STTM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'Sĩ quan', N'', N'admin', N'admin', NULL, N'', N'0003', N'', N'', N'', N'', N'0001', NULL, N'9020001-010-011-0001-0003-0001', NULL, 0, NULL, NULL, NULL, NULL, 0.22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0003-0002' and iNamLamViec = 2025;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:35:25.1095754' AS DateTime2), CAST(N'2024-06-26T11:31:44.2307383' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0003' and iNamLamViec = 2025)
, NULL, NULL, N'STTM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'QNCN', N'', N'admin', N'admin', NULL, N'', N'0003', N'', N'', N'', N'', N'0002', NULL, N'9020001-010-011-0001-0003-0002', NULL, 0, NULL, NULL, NULL, NULL, 0.22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020002-010-011-0001-0003-0001' and iNamLamViec = 2025;
GO
--Mục cha đã có trong master data: 9020002-010-011-0001-0003: Phu nhân phu quân - Khối hạch toán
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:38:16.8013839' AS DateTime2), CAST(N'2024-06-26T11:33:35.8546786' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020002-010-011-0001-0003' and iNamLamViec = 2025)
, NULL, NULL, N'STTM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020002', N'0001', NULL, N'Sĩ quan', N'', N'admin', N'admin', NULL, N'', N'0003', N'', N'', N'', N'', N'0001', NULL, N'9020002-010-011-0001-0003-0001', NULL, 0, NULL, NULL, NULL, NULL, 0.22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020002-010-011-0001-0003-0002' and iNamLamViec = 2025;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:38:16.8013839' AS DateTime2), CAST(N'2024-06-26T11:33:35.8546786' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020002-010-011-0001-0003' and iNamLamViec = 2025)
, NULL, NULL, N'STTM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020002', N'0001', NULL, N'QNCN', N'', N'admin', N'admin', NULL, N'', N'0003', N'', N'', N'', N'', N'0002', NULL, N'9020002-010-011-0001-0003-0002', NULL, 0, NULL, NULL, NULL, NULL, 0.22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

--TH2: Mục lục thâm cha và con
--9020001-010-011-0001-0004: Tùy viên QP
--9020001-010-011-0001-0004-0001: Sĩ quan
--9020001-010-011-0001-0004-0002: QNCN
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0004' and iNamLamViec = 2025;
go
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-06-26T11:31:18.8342156' AS DateTime2),NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'5. Tùy viên QP', N'', N'admin', N'admin', NULL, N'', N'0004', N'', N'', N'', N'', N'', NULL, N'9020001-010-011-0001-0004', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0004-0001' and iNamLamViec = 2025;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:35:25.1095754' AS DateTime2), CAST(N'2024-06-26T11:32:29.5989902' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0004' and iNamLamViec = 2025)
, NULL, NULL, N'STTM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'Sĩ quan', N'', N'admin', N'admin', NULL, N'', N'0004', N'', N'', N'', N'', N'0001', NULL, N'9020001-010-011-0001-0004-0001', NULL, 0, NULL, NULL, NULL, NULL, 0.08, 0.175, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0004-0002' and iNamLamViec = 2025;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:35:25.1095754' AS DateTime2), CAST(N'2024-06-26T11:32:29.5989902' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0004' and iNamLamViec = 2025)
, NULL, NULL, N'STTM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'QNCN', N'', N'admin', N'admin', NULL, N'', N'0004', N'', N'', N'', N'', N'0002', NULL, N'9020001-010-011-0001-0004-0002', NULL, 0, NULL, NULL, NULL, NULL, 0.08, 0.175, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

--9020001-010-011-0001-0005: Tạm giam, tạm giữ
--9020001-010-011-0001-0005-0001: Sĩ quan
--9020001-010-011-0001-0005-0002: QNCN
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0005' and iNamLamViec = 2025;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:32:53.4676917' AS DateTime2), CAST(N'2024-06-26T11:31:18.8342156' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001' and iNamLamViec = 2025)
, NULL, NULL, N'STM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'6. Tạm giam tạm giữ', N'', N'admin', N'admin', NULL, N'', N'0005', N'', N'', N'', N'', N'', NULL, N'9020001-010-011-0001-0005', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0005-0001' and iNamLamViec = 2025;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:40:01.8090483' AS DateTime2), CAST(N'2024-06-26T11:32:53.4676917' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0005' and iNamLamViec = 2025)
, NULL, NULL, N'STTM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'Sĩ quan', N'', N'admin', N'admin', NULL, N'', N'0005', N'', N'', N'', N'', N'0001', NULL, N'9020001-010-011-0001-0005-0001', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.045, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0005-0002' and iNamLamViec = 2025;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:40:01.8090483' AS DateTime2), CAST(N'2024-06-26T11:32:53.4676917' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0005' and iNamLamViec = 2025)
, NULL, NULL, N'STTM', NULL, 0, 2025, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'QNCN', N'', N'admin', N'admin', NULL, N'', N'0005', N'', N'', N'', N'', N'0002', NULL, N'9020001-010-011-0001-0005-0002', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.045, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO

update BH_DM_MucLucNganSach set bHangCha = 1 where sxaunoima in ('9020001-010-011-0001-0003','9020002-010-011-0001-0003') and iNamLamViec = 2025

GO

---2024---
--TH1: Mục lục thêm mục con
--Mục cha đã có trong master data: 9020001-010-011-0001-0003: Phu nhân phu quân - Khối dự toán
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0003-0001' and iNamLamViec = 2024;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:35:25.1095754' AS DateTime2), CAST(N'2024-06-26T11:31:44.2307383' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0003' and iNamLamViec = 2024)
, NULL, NULL, N'STTM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'Sĩ quan', N'', N'admin', N'admin', NULL, N'', N'0003', N'', N'', N'', N'', N'0001', NULL, N'9020001-010-011-0001-0003-0001', NULL, 0, NULL, NULL, NULL, NULL, 0.22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0003-0002' and iNamLamViec = 2024;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:35:25.1095754' AS DateTime2), CAST(N'2024-06-26T11:31:44.2307383' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0003' and iNamLamViec = 2024)
, NULL, NULL, N'STTM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'QNCN', N'', N'admin', N'admin', NULL, N'', N'0003', N'', N'', N'', N'', N'0002', NULL, N'9020001-010-011-0001-0003-0002', NULL, 0, NULL, NULL, NULL, NULL, 0.22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020002-010-011-0001-0003-0001' and iNamLamViec = 2024;
GO
--Mục cha đã có trong master data: 9020002-010-011-0001-0003: Phu nhân phu quân - Khối hạch toán
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:38:16.8013839' AS DateTime2), CAST(N'2024-06-26T11:33:35.8546786' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020002-010-011-0001-0003' and iNamLamViec = 2024)
, NULL, NULL, N'STTM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020002', N'0001', NULL, N'Sĩ quan', N'', N'admin', N'admin', NULL, N'', N'0003', N'', N'', N'', N'', N'0001', NULL, N'9020002-010-011-0001-0003-0001', NULL, 0, NULL, NULL, NULL, NULL, 0.22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020002-010-011-0001-0003-0002' and iNamLamViec = 2024;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:38:16.8013839' AS DateTime2), CAST(N'2024-06-26T11:33:35.8546786' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020002-010-011-0001-0003' and iNamLamViec = 2024)
, NULL, NULL, N'STTM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020002', N'0001', NULL, N'QNCN', N'', N'admin', N'admin', NULL, N'', N'0003', N'', N'', N'', N'', N'0002', NULL, N'9020002-010-011-0001-0003-0002', NULL, 0, NULL, NULL, NULL, NULL, 0.22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

--TH2: Mục lục thâm cha và con
--9020001-010-011-0001-0004: Tùy viên QP
--9020001-010-011-0001-0004-0001: Sĩ quan
--9020001-010-011-0001-0004-0002: QNCN
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0004' and iNamLamViec = 2024;
go
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:32:29.5989902' AS DateTime2), CAST(N'2024-06-26T11:31:18.8342156' AS DateTime2),NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'5. Tùy viên QP', N'', N'admin', N'admin', NULL, N'', N'0004', N'', N'', N'', N'', N'', NULL, N'9020001-010-011-0001-0004', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0004-0001' and iNamLamViec = 2024;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:35:25.1095754' AS DateTime2), CAST(N'2024-06-26T11:32:29.5989902' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0004' and iNamLamViec = 2024)
, NULL, NULL, N'STTM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'Sĩ quan', N'', N'admin', N'admin', NULL, N'', N'0004', N'', N'', N'', N'', N'0001', NULL, N'9020001-010-011-0001-0004-0001', NULL, 0, NULL, NULL, NULL, NULL, 0.08, 0.175, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0004-0002' and iNamLamViec = 2024;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:35:25.1095754' AS DateTime2), CAST(N'2024-06-26T11:32:29.5989902' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0004' and iNamLamViec = 2024)
, NULL, NULL, N'STTM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'QNCN', N'', N'admin', N'admin', NULL, N'', N'0004', N'', N'', N'', N'', N'0002', NULL, N'9020001-010-011-0001-0004-0002', NULL, 0, NULL, NULL, NULL, NULL, 0.08, 0.175, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

--9020001-010-011-0001-0005: Tạm giam, tạm giữ
--9020001-010-011-0001-0005-0001: Sĩ quan
--9020001-010-011-0001-0005-0002: QNCN
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0005' and iNamLamViec = 2024;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:32:53.4676917' AS DateTime2), CAST(N'2024-06-26T11:31:18.8342156' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001' and iNamLamViec = 2024)
, NULL, NULL, N'STM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'6. Tạm giam tạm giữ', N'', N'admin', N'admin', NULL, N'', N'0005', N'', N'', N'', N'', N'', NULL, N'9020001-010-011-0001-0005', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0005-0001' and iNamLamViec = 2024;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:40:01.8090483' AS DateTime2), CAST(N'2024-06-26T11:32:53.4676917' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0005' and iNamLamViec = 2024)
, NULL, NULL, N'STTM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'Sĩ quan', N'', N'admin', N'admin', NULL, N'', N'0005', N'', N'', N'', N'', N'0001', NULL, N'9020001-010-011-0001-0005-0001', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.045, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0005-0002' and iNamLamViec = 2024;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-06-26T11:40:01.8090483' AS DateTime2), CAST(N'2024-06-26T11:32:53.4676917' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0005' and iNamLamViec = 2024)
, NULL, NULL, N'STTM', NULL, 0, 2024, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'QNCN', N'', N'admin', N'admin', NULL, N'', N'0005', N'', N'', N'', N'', N'0002', NULL, N'9020001-010-011-0001-0005-0002', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.045, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO

update BH_DM_MucLucNganSach set bHangCha = 1 where sxaunoima in ('9020001-010-011-0001-0003','9020002-010-011-0001-0003') and iNamLamViec = 2024

GO

---2023---
--TH1: Mục lục thêm mục con
--Mục cha đã có trong master data: 9020001-010-011-0001-0003: Phu nhân phu quân - Khối dự toán
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0003-0001' and iNamLamViec = 2023;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-06-26T11:35:25.1095754' AS DateTime2), CAST(N'2023-06-26T11:31:44.2307383' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0003' and iNamLamViec = 2023)
, NULL, NULL, N'STTM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'Sĩ quan', N'', N'admin', N'admin', NULL, N'', N'0003', N'', N'', N'', N'', N'0001', NULL, N'9020001-010-011-0001-0003-0001', NULL, 0, NULL, NULL, NULL, NULL, 0.22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0003-0002' and iNamLamViec = 2023;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-06-26T11:35:25.1095754' AS DateTime2), CAST(N'2023-06-26T11:31:44.2307383' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0003' and iNamLamViec = 2023)
, NULL, NULL, N'STTM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'QNCN', N'', N'admin', N'admin', NULL, N'', N'0003', N'', N'', N'', N'', N'0002', NULL, N'9020001-010-011-0001-0003-0002', NULL, 0, NULL, NULL, NULL, NULL, 0.22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020002-010-011-0001-0003-0001' and iNamLamViec = 2023;
GO
--Mục cha đã có trong master data: 9020002-010-011-0001-0003: Phu nhân phu quân - Khối hạch toán
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-06-26T11:38:16.8013839' AS DateTime2), CAST(N'2023-06-26T11:33:35.8546786' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020002-010-011-0001-0003' and iNamLamViec = 2023)
, NULL, NULL, N'STTM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020002', N'0001', NULL, N'Sĩ quan', N'', N'admin', N'admin', NULL, N'', N'0003', N'', N'', N'', N'', N'0001', NULL, N'9020002-010-011-0001-0003-0001', NULL, 0, NULL, NULL, NULL, NULL, 0.22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020002-010-011-0001-0003-0002' and iNamLamViec = 2023;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-06-26T11:38:16.8013839' AS DateTime2), CAST(N'2023-06-26T11:33:35.8546786' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020002-010-011-0001-0003' and iNamLamViec = 2023)
, NULL, NULL, N'STTM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020002', N'0001', NULL, N'QNCN', N'', N'admin', N'admin', NULL, N'', N'0003', N'', N'', N'', N'', N'0002', NULL, N'9020002-010-011-0001-0003-0002', NULL, 0, NULL, NULL, NULL, NULL, 0.22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

--TH2: Mục lục thâm cha và con
--9020001-010-011-0001-0004: Tùy viên QP
--9020001-010-011-0001-0004-0001: Sĩ quan
--9020001-010-011-0001-0004-0002: QNCN
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0004' and iNamLamViec = 2023;
go
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-06-26T11:32:29.5989902' AS DateTime2), CAST(N'2023-06-26T11:31:18.8342156' AS DateTime2),NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'5. Tùy viên QP', N'', N'admin', N'admin', NULL, N'', N'0004', N'', N'', N'', N'', N'', NULL, N'9020001-010-011-0001-0004', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0004-0001' and iNamLamViec = 2023;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-06-26T11:35:25.1095754' AS DateTime2), CAST(N'2023-06-26T11:32:29.5989902' AS DateTime2), NEWID(), 
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0004' and iNamLamViec = 2023)
, NULL, NULL, N'STTM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'Sĩ quan', N'', N'admin', N'admin', NULL, N'', N'0004', N'', N'', N'', N'', N'0001', NULL, N'9020001-010-011-0001-0004-0001', NULL, 0, NULL, NULL, NULL, NULL, 0.08, 0.175, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0004-0002' and iNamLamViec = 2023;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-06-26T11:35:25.1095754' AS DateTime2), CAST(N'2023-06-26T11:32:29.5989902' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0004' and iNamLamViec = 2023)
, NULL, NULL, N'STTM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'QNCN', N'', N'admin', N'admin', NULL, N'', N'0004', N'', N'', N'', N'', N'0002', NULL, N'9020001-010-011-0001-0004-0002', NULL, 0, NULL, NULL, NULL, NULL, 0.08, 0.175, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

--9020001-010-011-0001-0005: Tạm giam, tạm giữ
--9020001-010-011-0001-0005-0001: Sĩ quan
--9020001-010-011-0001-0005-0002: QNCN
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0005' and iNamLamViec = 2023;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 1, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-06-26T11:32:53.4676917' AS DateTime2), CAST(N'2023-06-26T11:31:18.8342156' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001' and iNamLamViec = 2023)
, NULL, NULL, N'STM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'6. Tạm giam tạm giữ', N'', N'admin', N'admin', NULL, N'', N'0005', N'', N'', N'', N'', N'', NULL, N'9020001-010-011-0001-0005', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0005-0001' and iNamLamViec = 2023;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-06-26T11:40:01.8090483' AS DateTime2), CAST(N'2023-06-26T11:32:53.4676917' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0005' and iNamLamViec = 2023)
, NULL, NULL, N'STTM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'Sĩ quan', N'', N'admin', N'admin', NULL, N'', N'0005', N'', N'', N'', N'', N'0001', NULL, N'9020001-010-011-0001-0005-0001', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.045, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
DELETE FROM BH_DM_MucLucNganSach WHERE sxaunoima = '9020001-010-011-0001-0005-0002' and iNamLamViec = 2023;
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [bDuPhong], [bHangCha], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [dNgaySua], [dNgayTao], [iID_MLNS], [iID_MLNS_Cha], [iID_MaBQuanLy], [iID_MaDonVi], [iLoai], [iLoaiNganSach], [iLock], [iNamLamViec], [iTrangThai], [Log], [sCPChiTietToi], [sChiTietToi], [sDuToanChiTietToi], [sK], [sL], [sLNS], [sM], [sMaCB], [sMoTa], [sNG], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [sTM], [sTNG], [sTNG1], [sTNG2], [sTNG3], [sTTM], [Tag], [sXauNoiMa], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHTN_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHXH_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHYT_NSD], [sLuongChinh], [sNS_LuongChinh], [sNS_PCCV], [sNS_PCTN], [sNS_PCTNVK], [sPCCV], [sPCTN], [sPCTNVK], [sNS_HSBL]) 
VALUES (NEWID(), 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2023-06-26T11:40:01.8090483' AS DateTime2), CAST(N'2023-06-26T11:32:53.4676917' AS DateTime2), NEWID(),
(select iID_MLNS from BH_DM_MucLucNganSach where sxaunoima = '9020001-010-011-0001-0005' and iNamLamViec = 2023)
, NULL, NULL, N'STTM', NULL, 0, 2023, 1, NULL, NULL, N'NG', N'', N'011', N'010', N'9020001', N'0001', NULL, N'QNCN', N'', N'admin', N'admin', NULL, N'', N'0005', N'', N'', N'', N'', N'0002', NULL, N'9020001-010-011-0001-0005-0002', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.045, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

GO

update BH_DM_MucLucNganSach set bHangCha = 1 where sxaunoima in ('9020001-010-011-0001-0003','9020002-010-011-0001-0003') and iNamLamViec = 2023

GO


/****** Object:  StoredProcedure [dbo].[sp_luong_bhxh_import_qtc_bhxh]    Script Date: 7/11/2024 10:25:24 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_luong_bhxh_import_qtc_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_luong_bhxh_import_qtc_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_bhxh_import_qtc_bhxh]    Script Date: 7/11/2024 10:25:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_luong_bhxh_import_qtc_bhxh]
	@YearOfWork int,
	@Months nvarchar(20)
AS
BEGIN
	select chedo.sXauNoiMaMlnsBHXH, luong.*
	into luong_temp
	from TL_BangLuong_ThangBHXH luong
	left join TL_DM_CheDoBHXH chedo
		on luong.sMaCheDo = chedo.sMaCheDo
	where 
		luong.iNam = @YearOfWork
		and luong.iThang in (SELECT * FROM f_split(@Months))
		and luong.sMaCheDo in 
		(select distinct sMaCheDo from TL_DM_CheDoBHXH
		where sMaCheDoCha is not null and sMaCheDoCha <> ''
		--and sMaCheDo not in (select distinct sMaCheDoCha from TL_DM_CheDoBHXH)
		and (upper(sMaCheDo) not like '%_HS%' and upper(sMaCheDo) not like '%_HESO%'))

	--Thong tin luong Si quan
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_sq
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '1%'
			group by
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong QNCN
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_qncn
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '2%'
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong HSQ_BS
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_hsq_bs
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '0%'
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong VCQP
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_vcqp
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB in ('3.1', '3.2', '3.3')
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong hdld
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_hdld
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB = '43'
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Ket qua
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo MaCheDo,
		sq.SoNguoi SoNguoiSQ,
		sq.SoTien SoTienSQ,
		qncn.SoTien SoTienQNCN,
		qncn.SoNguoi SoNguoiQNCN,
		hsq.SoTien SoTienHSQ,
		hsq.SoNguoi SoNguoiHSQ,
		vcqp.SoTien SoTienVCQP,
		vcqp.SoNguoi SoNguoiVCQP,
		hdld.SoTien SoTienHDLD,
		hdld.SoNguoi SoNguoiHDLD
	from luong_temp temp
	left join luong_temp_sq sq on temp.sMaCheDo = sq.sMaCheDo
	left join luong_temp_qncn qncn on temp.sMaCheDo = qncn.sMaCheDo
	left join luong_temp_hsq_bs hsq on temp.sMaCheDo = hsq.sMaCheDo
	left join luong_temp_vcqp vcqp on temp.sMaCheDo = vcqp.sMaCheDo
	left join luong_temp_hdld hdld on temp.sMaCheDo = hdld.sMaCheDo
	where isnull(temp.sXauNoiMaMlnsBHXH, '') <> ''

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp]') AND type in (N'U'))
	drop table luong_temp;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_sq]') AND type in (N'U'))
	drop table luong_temp_sq;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_qncn]') AND type in (N'U'))
	drop table luong_temp_qncn;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_hsq_bs]') AND type in (N'U'))
	drop table luong_temp_hsq_bs;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_vcqp]') AND type in (N'U'))
	drop table luong_temp_vcqp;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_hdld]') AND type in (N'U'))
	drop table luong_temp_hdld;

END
;
;
;
GO
