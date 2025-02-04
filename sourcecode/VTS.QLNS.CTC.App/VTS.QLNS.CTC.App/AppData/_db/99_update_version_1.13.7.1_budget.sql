UPDATE NS_DT_ChungTu 
SET iNamNganSach = 3
WHERE iNamNganSach IN (1, 4)
GO

UPDATE NS_DT_ChungTuChiTiet 
SET iNamNganSach = 3
WHERE iNamNganSach IN (1, 4)
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]    Script Date: 12/12/2023 10:06:44 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tong_hop_du_toan_thu_chi]    Script Date: 12/12/2023 10:06:44 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_tong_hop_du_toan_thu_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_tong_hop_du_toan_thu_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tong_hop_du_toan_thu_chi]    Script Date: 12/12/2023 10:06:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_tong_hop_du_toan_thu_chi]
	@namLamViec int,
	@lstSelectedUnit ntext,
	@dvt int
AS
BEGIN
	
SELECT
	--ml.sLNS,
	--ml.sM,
	--ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002')
			THEN 
				SUM(ISNULL(ctct.fThu_BHXH_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHXH_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHXH,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002')
			THEN 
				SUM(ISNULL(ctct.fThu_BHTN_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHTN_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHTN,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002') AND ml.sM = '1'
			THEN 
				SUM(ISNULL(ctct.fThu_BHYT_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHYT_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_QN,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002') AND ml.sM = '2'
			THEN 
				SUM(ISNULL(ctct.fThu_BHYT_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHYT_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_NLD
	INTO #temp1
FROM BH_KHT_BHXH_ChiTiet ctct
LEFT JOIN BH_KHT_BHXH ct 
ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS = '9030001'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_TNQN,
	CASE 
		WHEN ml.sLNS = '9030002'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_CNVQP,
	CASE 
		WHEN ml.sLNS = '9030003'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_HVQS,
	CASE 
		WHEN ml.sLNS = '9030004'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_HSSV,
	CASE 
		WHEN ml.sLNS = '9030005'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_SQDB,
	CASE 
		WHEN ml.sLNS = '9030006'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_LUU_HS
	INTO #temp2
FROM BH_KHTM_BHYT_ChiTiet ctct
LEFT JOIN BH_KHTM_BHYT ct 
ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_NoiDung = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010001', '9010002')
		THEN 
			SUM(ISNULL(ctct.fTienCNVQP, 0)) +
			SUM(ISNULL(ctct.fTienHSQBS, 0)) +
			SUM(ISNULL(ctct.fTienLDHD, 0)) +
			SUM(ISNULL(ctct.fTienQNCN, 0)) +
			SUM(ISNULL(ctct.fTienSQ, 0))
		ELSE 0 
	END AS fSoTienChiCheDo
	INTO #temp3
FROM BH_KHC_CheDoBHXH_ChiTiet ctct
LEFT JOIN BH_KHC_CheDoBHXH ct 
ON ct.ID = ctct.iID_KHC_CheDoBHXH
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010003')
		THEN 
			SUM(ISNULL(ctct.fTienCanBo, 0)) +
			SUM(ISNULL(ctct.fTienQuanLuc, 0)) +
			SUM(ISNULL(ctct.fTienTaiChinh, 0)) +
			SUM(ISNULL(ctct.fTienQuanY, 0))
		ELSE 0 
	END AS fSoTienChiKinhPhiQuanLy
	INTO #temp4
FROM BH_KHC_KinhPhiQuanLy_ChiTiet ctct
LEFT JOIN BH_KHC_KinhPhiQuanLy ct 
ON ct.iID_BH_KHC_KinhPhiQuanLy = ctct.iID_KHC_KinhPhiQuanLy
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010006', '9010007')
		THEN 
			SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiQuanYDonVi
	INTO #temp5
FROM BH_KHC_KCB_ChiTiet ctct
LEFT JOIN BH_KHC_KCB ct 
ON ct.iID_BH_KHC_KCB = ctct.iID_KHC_KCB
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010009')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiTTB,
	CASE 
		WHEN ml.sLNS IN ('9010004', '9010005')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiTSDK,
	CASE 
		WHEN ml.sLNS IN ('9050001')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiNLD,
	CASE 
		WHEN ml.sLNS IN ('9050002')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiHSSV,
	CASE 
		WHEN ml.sLNS IN ('9040001', '9040002')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiCSYT
	INTO #temp6
FROM BH_KHC_K_ChiTiet ctct
LEFT JOIN BH_KHC_K ct 
ON ct.iID_BH_KHC_K = ctct.iID_KHC_K
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

IF NOT EXISTS(SELECT 1 FROM #temp1) INSERT INTO #temp1 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp2) INSERT INTO #temp2 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp3) INSERT INTO #temp3 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp4) INSERT INTO #temp4 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp5) INSERT INTO #temp5 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp6) INSERT INTO #temp6 DEFAULT VALUES 


SELECT NoiDung, SoTien  
INTO #temp7
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienThuBHXH, 0)) / @dvt AS N'1.1.0,1,Dự toán thu BHXH',
		SUM(ISNULL(fSoTienThuBHTN, 0))/ @dvt AS N'1.2.0,2,Dự toán thu BHTN',
		SUM(ISNULL(fSoTienThuBHYT_QN, 0)) / @dvt AS N'1.3.0,3,Dự toán thu BHYT quân nhân',
		SUM(ISNULL(fSoTienThuBHYT_NLD, 0)) / @dvt AS N'1.4.0,4,Dự toán thu BHYT người lao động'
   FROM #temp1) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([1.1.0,1,Dự toán thu BHXH], 
	  [1.2.0,2,Dự toán thu BHTN], 
	  [1.3.0,3,Dự toán thu BHYT quân nhân], 
	  [1.4.0,4,Dự toán thu BHYT người lao động])  
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienThuBHYT_TNQN, 0)) / @dvt AS N'1.5.1,-,Thân nhân quân nhân',
		SUM(ISNULL(fSoTienThuBHYT_CNVQP, 0)) / @dvt AS N'1.5.2,-,Thân nhân người lao động'
		--SUM(ISNULL(fSoTienThuBHYT_HVQS, 0)) AS N'1.3.7,-,BHYT HV QS xã phường (Phụ lục VII)',
		--SUM(ISNULL(fSoTienThuBHYT_HSSV, 0)) AS N'1.3.5,-,BHYT học sinh, sinh viên (Phụ lục VII)',
		--SUM(ISNULL(fSoTienThuBHYT_SQDB, 0)) AS N'1.3.8,-,BHYT SQ dự bị (Phụ lục VII)',
		--SUM(ISNULL(fSoTienThuBHYT_LUU_HS, 0)) AS N'1.3.6,-,BHYT lưu học sinh (Phụ lục VII)'
   FROM #temp2) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([1.5.1,-,Thân nhân quân nhân], 
	  [1.5.2,-,Thân nhân người lao động])
	  --[1.3.7,-,BHYT HV QS xã phường (Phụ lục VII)], 
	  --[1.3.5,-,BHYT học sinh, sinh viên (Phụ lục VII)], 
	  --[1.3.8,-,BHYT SQ dự bị (Phụ lục VII)], 
	  --[1.3.6,-,BHYT lưu học sinh (Phụ lục VII)])  
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiCheDo, 0)) / @dvt AS N'2.1.0,1,Dự toán chi các chế độ BHXH'
   FROM #temp3) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.1.0,1,Dự toán chi các chế độ BHXH])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiKinhPhiQuanLy, 0)) / @dvt AS N'2.2.0,2,Dự toán chi kinh phí quản lý BHXH, BHYT'
   FROM #temp4) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.2.0,2,Dự toán chi kinh phí quản lý BHXH, BHYT])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiQuanYDonVi, 0)) / @dvt AS N'2.3.0,3,Dự toán chi kinh phí KCB tại quân y đơn vị'
   FROM #temp5) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.3.0,3,Dự toán chi kinh phí KCB tại quân y đơn vị])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		--SUM(ISNULL(fSoTienChiTTB, 0)) AS N'2.3.0,3,Chi mua sắm TTB y tế (Phụ lục X)',
		SUM(ISNULL(fSoTienChiTSDK, 0)) AS N'2.4.0,4,Dự toán chi kinh phí KCB Trường Sa - DK'
		--SUM(ISNULL(fSoTienChiNLD, 0)) AS N'2.4.3,-,Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)',
		--SUM(ISNULL(fSoTienChiHSSV, 0)) AS N'2.4.4,-,Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)',
		--SUM(ISNULL(fSoTienChiCSYT, 0)) AS N'2.4.5,-,Chi KCB tại các cơ sở y tế (Phụ lục XV)'
   FROM #temp6) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      --([2.3.0,3,Chi mua sắm TTB y tế (Phụ lục X)], 
	  ([2.4.0,4,Dự toán chi kinh phí KCB Trường Sa - DK]) 
	  --[2.4.3,-,Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)], 
	  --[2.4.4,-,Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)], 
	  --[2.4.5,-,Chi KCB tại các cơ sở y tế (Phụ lục XV)])
) AS unpvt



SELECT * FROM #temp7
UNION SELECT N'1.0.0,A,Dự toán thu' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '1.%')
UNION SELECT N'2.0.0,B,Dự toán chi' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '2.%')
UNION SELECT N'1.5.0,5,Dự toán thu BHYT TN' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '1.5%')
--UNION SELECT N'2.4.0,4,Chi KCB BHYT' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '2.4%')


DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #temp4
DROP TABLE #temp5
DROP TABLE #temp6
DROP TABLE #temp7

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]    Script Date: 12/12/2023 10:06:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]
@NamLamViec int,
@IdDonVi NVARCHAR(MAX),
@DonViTinh int,
@IsTongHop int
AS
BEGIN
	
SELECT
	--ml.sLNS,
	--ml.sM,
	--ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002')
			THEN 
				SUM(ISNULL(ctct.fThu_BHXH_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHXH_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHXH,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002')
			THEN 
				SUM(ISNULL(ctct.fThu_BHTN_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHTN_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHTN,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002') AND ml.sM = '1'
			THEN 
				SUM(ISNULL(ctct.fThu_BHYT_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHYT_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_QN,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002') AND ml.sM = '2'
			THEN 
				SUM(ISNULL(ctct.fThu_BHYT_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHYT_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_NLD
	INTO #temp1
FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
LEFT JOIN BH_QTT_BHXH_ChungTu ct 
ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MLNS = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS = '9030001'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_TNQN,
	CASE 
		WHEN ml.sLNS = '9030002'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_CNVQP,
	CASE 
		WHEN ml.sLNS = '9030003'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_HVQS,
	CASE 
		WHEN ml.sLNS = '9030004'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_HSSV,
	CASE 
		WHEN ml.sLNS = '9030005'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_SQDB,
	CASE 
		WHEN ml.sLNS = '9030006'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_LUU_HS
	INTO #temp2
FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MLNS = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010001', '9010002')
		THEN 
			SUM(ISNULL(ctct.fTienCNVCQP_ThucChi, 0)) +
			SUM(ISNULL(ctct.fTienHSQBS_ThucChi, 0)) +
			SUM(ISNULL(ctct.fTienLDHD_ThucChi, 0)) +
			SUM(ISNULL(ctct.fTienQNCN_ThucChi, 0)) +
			SUM(ISNULL(ctct.fTienSQ_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiCheDo
	INTO #temp3
FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010003')
		THEN 
			SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiKinhPhiQuanLy
	INTO #temp4
FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
LEFT JOIN BH_QTC_Nam_KinhPhiQuanLy ct 
ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010006', '9010007')
		THEN 
			SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiQuanYDonVi
	INTO #temp5
FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet ctct
LEFT JOIN BH_QTC_Nam_KCB_QuanYDonVi ct 
ON ct.ID_QTC_Nam_KCB_QuanYDonVi = ctct.iID_QTC_Nam_KCB_QuanYDonVi
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010009')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiTTB,
	CASE 
		WHEN ml.sLNS IN ('9010004', '9010005')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiTSDK,
	CASE 
		WHEN ml.sLNS IN ('9050001')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiNLD,
	CASE 
		WHEN ml.sLNS IN ('9050002')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiHSSV,
	CASE 
		WHEN ml.sLNS IN ('9040001', '9040002')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiCSYT
	INTO #temp6
FROM BH_QTC_Nam_KPK_ChiTiet ctct
LEFT JOIN BH_QTC_Nam_KPK ct 
ON ct.ID_QTC_Nam_KPK = ctct.ID_QTC_Nam_KPK_ChiTiet
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

IF NOT EXISTS(SELECT 1 FROM #temp1) INSERT INTO #temp1 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp2) INSERT INTO #temp2 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp3) INSERT INTO #temp3 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp4) INSERT INTO #temp4 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp5) INSERT INTO #temp5 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp6) INSERT INTO #temp6 DEFAULT VALUES 




SELECT NoiDung, SoTien  
INTO #temp7
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienThuBHXH, 0)) / @DonViTinh AS N'1.1.0,1,Thu bảo hiểm xã hội (Phụ lục II)',
		SUM(ISNULL(fSoTienThuBHTN, 0)) / @DonViTinh AS N'1.2.0,2,Thu bảo hiểm thất nghiệp (Phụ lục III)',
		SUM(ISNULL(fSoTienThuBHYT_QN, 0)) / @DonViTinh AS N'1.3.1,-,BHYT quân nhân (Phụ lục IV)',
		SUM(ISNULL(fSoTienThuBHYT_NLD, 0)) / @DonViTinh AS N'1.3.2,-,BHYT người lao động (Phụ lục V)'
   FROM #temp1) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([1.1.0,1,Thu bảo hiểm xã hội (Phụ lục II)], 
	  [1.2.0,2,Thu bảo hiểm thất nghiệp (Phụ lục III)], 
	  [1.3.1,-,BHYT quân nhân (Phụ lục IV)], 
	  [1.3.2,-,BHYT người lao động (Phụ lục V)])  
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienThuBHYT_TNQN, 0)) / @DonViTinh AS N'1.3.3,-,BHYT thân nhân quân nhân (Phụ lục VI)',
		SUM(ISNULL(fSoTienThuBHYT_CNVQP, 0)) / @DonViTinh AS N'1.3.4,-,BHYT thân nhân CN, viên chức QP (Phụ lục VI)',
		SUM(ISNULL(fSoTienThuBHYT_HVQS, 0)) / @DonViTinh AS N'1.3.7,-,BHYT HV QS xã phường (Phụ lục VII)',
		SUM(ISNULL(fSoTienThuBHYT_HSSV, 0)) / @DonViTinh AS N'1.3.5,-,BHYT học sinh, sinh viên (Phụ lục VII)',
		SUM(ISNULL(fSoTienThuBHYT_SQDB, 0)) / @DonViTinh AS N'1.3.8,-,BHYT SQ dự bị (Phụ lục VII)',
		SUM(ISNULL(fSoTienThuBHYT_LUU_HS, 0)) / @DonViTinh AS N'1.3.6,-,BHYT lưu học sinh (Phụ lục VII)'
   FROM #temp2) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([1.3.3,-,BHYT thân nhân quân nhân (Phụ lục VI)], 
	  [1.3.4,-,BHYT thân nhân CN, viên chức QP (Phụ lục VI)], 
	  [1.3.7,-,BHYT HV QS xã phường (Phụ lục VII)], 
	  [1.3.5,-,BHYT học sinh, sinh viên (Phụ lục VII)], 
	  [1.3.8,-,BHYT SQ dự bị (Phụ lục VII)], 
	  [1.3.6,-,BHYT lưu học sinh (Phụ lục VII)])  
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiCheDo, 0)) / @DonViTinh AS N'2.1.0,1,Chi các chế độ BHXH (Phụ lục VIII)'
   FROM #temp3) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.1.0,1,Chi các chế độ BHXH (Phụ lục VIII)])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiKinhPhiQuanLy, 0)) / @DonViTinh AS N'2.2.0,2,Chi KP quản lý BHXH, BHYT (Phụ lục IX)'
   FROM #temp4) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.2.0,2,Chi KP quản lý BHXH, BHYT (Phụ lục IX)])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiQuanYDonVi, 0)) / @DonViTinh AS N'2.4.2,-,Chi KCB tại quân y đơn vị (Phụ lục XII)'
   FROM #temp5) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.4.2,-,Chi KCB tại quân y đơn vị (Phụ lục XII)])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiTTB, 0)) / @DonViTinh AS N'2.3.0,3,Chi mua sắm TTB y tế (Phụ lục X)',
		SUM(ISNULL(fSoTienChiTSDK, 0)) / @DonViTinh AS N'2.4.1,-,Chi KCB cho quân nhân tại TS-DK (Phụ lục XI)',
		SUM(ISNULL(fSoTienChiNLD, 0)) / @DonViTinh AS N'2.4.3,-,Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)',
		SUM(ISNULL(fSoTienChiHSSV, 0)) / @DonViTinh AS N'2.4.4,-,Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)',
		SUM(ISNULL(fSoTienChiCSYT, 0)) / @DonViTinh AS N'2.4.5,-,Chi KCB tại các cơ sở y tế (Phụ lục XV)'
   FROM #temp6) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.3.0,3,Chi mua sắm TTB y tế (Phụ lục X)], 
	  [2.4.1,-,Chi KCB cho quân nhân tại TS-DK (Phụ lục XI)], 
	  [2.4.3,-,Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)], 
	  [2.4.4,-,Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)], 
	  [2.4.5,-,Chi KCB tại các cơ sở y tế (Phụ lục XV)])
) AS unpvt



SELECT * FROM #temp7
UNION SELECT N'1.0.0,I,Quyết toán thu' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '1.%')
UNION SELECT N'2.0.0,II,Quyết toán chi' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '2.%')
UNION SELECT N'1.3.0,3,Thu bảo hiểm y tế' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '1.3%')
UNION SELECT N'2.4.0,4,Chi KCB BHYT' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '2.4%')


DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #temp4
DROP TABLE #temp5
DROP TABLE #temp6
DROP TABLE #temp7

END
GO
