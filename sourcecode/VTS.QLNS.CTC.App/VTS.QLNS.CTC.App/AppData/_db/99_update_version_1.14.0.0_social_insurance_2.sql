/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]    Script Date: 2/20/2024 11:18:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]    Script Date: 2/20/2024 11:18:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]    Script Date: 2/20/2024 11:18:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]    Script Date: 2/20/2024 4:45:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]    Script Date: 2/20/2024 11:18:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_chedo_bhxh]
	@VoucherId uniqueidentifier ,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi nvarchar(255)
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) 
	FROM BH_CP_ChungTu_ChiTiet
	WHERE iID_CP_ChungTu = @VoucherId

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sCPChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and iTrangThai=1
	and sLNS in (select * from splitstring(@LNS))
	--and  bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToan,
		  --fTongTien AS fTienDuToan,
           0 AS fTienKeHoachCap,
          0 AS fTienDaCap,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach

	-- lấy dữ liệu đã cấp
	SELECT ISNULL(SUM(fTienDuToan),0) AS fTienDuToan,
		  ISNULL(SUM(fTienKeHoachCap),0)  AS fTienKeHoachCap,
		  ISNULL(SUM(fTienDaCap),0)  AS fTienDaCap,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblDataDaCap
	FROM BH_CP_ChungTu_ChiTiet
	WHERE iID_CP_ChungTu IN
       (
		SELECT iID_CP_ChungTu
        FROM BH_CP_ChungTu
        WHERE iNamChungTu = @YearOfWork
		  AND iID_LoaiCap = @iID_LoaiDanhMucChi
          AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(@AgencyId))
		  AND iID_CP_ChungTu=@VoucherId
		)
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	GROUP BY iID_MaDonVi, iID_MucLucNganSach

	-- tao bang tam chua don vi
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
	
	
	-- Tao bang tam luu chu mlns cha co don vi
	SELECT * INTO #tblMlnsExistDonVi
	FROM #tblNsMlns ,#tblDonVi dv
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'
	
	-- Tao bang tam luu chu mlns va tien du toan chi che do bhxh
	SELECT ml.sXauNoiMa,
		SUM(pb.fTienDuToan) fTienDuToan ,
		ml.iID_MaDonVi 
		INTO #tempMlnsbhxh 
		FROM #tblMlnsExistDonVi ml
   LEFT JOIN #tblPhanBoDuToan pb ON pb.iID_MucLucNganSach=ml.iID_MLNS and ml.iID_MaDonVi=pb.iID_MaDonVi
   GROUP BY ml.sXauNoiMa,ml.iID_MaDonVi
		ORDER BY ml.sXauNoiMa

	SELECT SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa,
		A.iID_MaDonVi, 
		SUM(A.fTienDuToan) fTienDuToan
		INTO #tblDaCapDuToanResult
		FROM #tempMlnsbhxh  A
		GROUP BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi
		ORDER BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi

	-- Get data
	SELECT
		isnull(ctct.iID_CP_ChungTu_ChiTiet, @iD) AS iID_CP_ChungTu_ChiTiet,
		@VoucherId AS iID_CP_ChungTu,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha As IsHangCha,
		mlnsPhanBo.sCPChiTietToi sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(ctct.fTienDaCap, 0) as FTienDaCap,
		isnull(daCapDuToan.fTienDuToan, 0) as FTienDuToan,
		isnull(ctct.fTienKeHoachCap, 0) as FTienKeHoachCap,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsExistDonVi AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_CP_ChungTu_ChiTiet
			WHERE 
		 		iID_CP_ChungTu = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
	LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	on mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa and daCapDuToan.iID_MaDonVi= mlnsPhanBo.iID_MaDonVi
	where mlnsPhanBo.sLNS ='901'
	order by mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;

END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]    Script Date: 2/20/2024 11:18:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]
	@NamLamViec int,
	@IdDonVi nvarchar(200)
AS
BEGIN
DECLARE @CountIndex INT;
	
					SELECT 
					fBHYT_NLD,
					fBHYT_NSD,
					iID_MaDonVi,
					sXauNoiMa,
					iNamLamViec
					into #temp1
					FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet 
					WHERE iID_DTT_BHXH_ChungTu IN
					(	
							SELECT iID_DTT_BHXH_PhanBo_ChungTu FROM BH_DTT_BHXH_PhanBo_ChungTu
							WHERE iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
							AND iNamLamViec=@NamLamViec
							AND bKhoa=1

					)
					and
                    (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0001%')
                    AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
                    AND iNamLamViec = @NamLamViec


					SELECT 
					fThuBHYT_NSD_QTCuoiNam,
					fThuBHYT_NLD_QTCuoiNam,
					fThuBHYT_NSD_QTDauNam,
					fThuBHYT_NLD_QTDauNam,
					iID_MaDonVi,
					sXauNoiMa,
					iNamLamViec
					into #temp2
					 FROM BH_DTT_BHXH_DieuChinh_ChiTiet 
					WHERE iID_DTT_BHXH_DieuChinh IN
					(	
							SELECT iID_DTT_BHXH_DieuChinh FROM BH_DTT_BHXH_DieuChinh
							WHERE iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
							AND bIsKhoa=1
							AND iNamLamViec=@NamLamViec

					)
					and
                    (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0001%')
                    AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
                    AND iNamLamViec = @NamLamViec

			SELECT 
                    (
						(
						(	
						((ISNULL(B.fThuBHYT_NSD_QTCuoiNam,0))  +  (ISNULL(B.fThuBHYT_NLD_QTCuoiNam,0)) 
						+ (ISNULL(B.fThuBHYT_NSD_QTDauNam ,0))   + (ISNULL(B.fThuBHYT_NLD_QTDauNam,0))
						)
						- (A.fBHYT_NLD+A.fBHYT_NSD)
							)
					*10)
					)
					/100 as FTienTangGiam,
					A.iID_MaDonVi
					into #tempTotal
				 FROM #temp1 A
				 LEFT JOIN #temp2 B ON A.iID_MaDonVi=B.iID_MaDonVi and a.sXauNoiMa=B.sXauNoiMa
				  group by B.fThuBHYT_NSD_QTCuoiNam,B.fThuBHYT_NLD_QTCuoiNam
				, B.fThuBHYT_NSD_QTDauNam,B.fThuBHYT_NLD_QTDauNam,A.fBHYT_NLD,A.fBHYT_NSD,A.iID_MaDonVi

			SELECT @CountIndex = COUNT(*) 
				FROM #tempTotal
		IF @CountIndex>0
		BEGIN 
			SELECT 
                    NEWID() as IID_MucLucNganSach,
                    N'10 % số điều chỉnh thu BHYT quân nhân' as SNoiDung,
                    Sum(A.FTienTangGiam) as FTienTangGiam,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    1 as IRemainRow,
					'SLNS' SDuToanChiTietToi,
					null SM
				 FROM #tempTotal A
				 group by A.iID_MaDonVi

					
              UNION
              SELECT 
                    NEWID() as IID_MucLucNganSach,
                    N'Số còn lại' as SNoiDung,
                    0 as FTienTangGiam,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    2 as IRemainRow,
					'SLNS' SDuToanChiTietToi,
					null SM
					;
		END
		ELSE
		BEGIN
		SELECT 
                    NEWID() as IID_MucLucNganSach,
                    N'10 % số điều chỉnh thu BHYT quân nhân' as SNoiDung,
                    0 as FTienTangGiam,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    1 as IRemainRow,
					'SLNS' SDuToanChiTietToi,
					null SM
              UNION
              SELECT 
                    NEWID() as IID_MucLucNganSach,
                    N'Số còn lại' as SNoiDung,
                    0 as FTienTangGiam,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    2 as IRemainRow,
					'SLNS' SDuToanChiTietToi,
					null SM
					;
		END

			drop table #temp1
			drop table #temp2
			drop table #tempTotal
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]    Script Date: 2/20/2024 11:18:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]
	@NamLamViec int,
	@IdDonVi nvarchar(200)
AS
BEGIN
	DECLARE @CountIndex INT;
	
					SELECT 
					fBHYT_NLD,
					fBHYT_NSD,
					iID_MaDonVi,
					sXauNoiMa,
					iNamLamViec
					into #temp1
					FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet 
					WHERE iID_DTT_BHXH_ChungTu IN
					(	
							SELECT iID_DTT_BHXH_PhanBo_ChungTu FROM BH_DTT_BHXH_PhanBo_ChungTu
							WHERE iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
							AND iNamLamViec=@NamLamViec
							AND bKhoa=1

					)
					and
                    (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0001%')
                    AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
                    AND iNamLamViec = @NamLamViec


					SELECT 
					fThuBHYT_NSD_QTCuoiNam,
					fThuBHYT_NLD_QTCuoiNam,
					fThuBHYT_NSD_QTDauNam,
					fThuBHYT_NLD_QTDauNam,
					iID_MaDonVi,
					sXauNoiMa,
					iNamLamViec
					into #temp2
					 FROM BH_DTT_BHXH_DieuChinh_ChiTiet 
					WHERE iID_DTT_BHXH_DieuChinh IN
					(	
							SELECT iID_DTT_BHXH_DieuChinh FROM BH_DTT_BHXH_DieuChinh
							WHERE iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
							AND bIsKhoa=1
							AND iNamLamViec=@NamLamViec

					)
					and
                    (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0001%')
                    AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
                    AND iNamLamViec = @NamLamViec

			SELECT 
                    (
						(
						(	
						((ISNULL(B.fThuBHYT_NSD_QTCuoiNam,0))  +  (ISNULL(B.fThuBHYT_NLD_QTCuoiNam,0)) 
						+ (ISNULL(B.fThuBHYT_NSD_QTDauNam ,0))   + (ISNULL(B.fThuBHYT_NLD_QTDauNam,0))
						)
						- (A.fBHYT_NLD+A.fBHYT_NSD)
							)
					*10)
					)
					/100 as fTienTuChi,
					A.iID_MaDonVi
					into #tempTotal
				 FROM #temp1 A
				 LEFT JOIN #temp2 B ON A.iID_MaDonVi=B.iID_MaDonVi and a.sXauNoiMa=B.sXauNoiMa
				  group by B.fThuBHYT_NSD_QTCuoiNam,B.fThuBHYT_NLD_QTCuoiNam
				, B.fThuBHYT_NSD_QTDauNam,B.fThuBHYT_NLD_QTDauNam,A.fBHYT_NLD,A.fBHYT_NSD,A.iID_MaDonVi
			
			SELECT @CountIndex = COUNT(*) 
				FROM #tempTotal
		IF @CountIndex>0
		BEGIN
		SELECT 
                    NEWID() as iID_MLNS,
                    N'10 % số điều chỉnh thu BHYT quân nhân' as SNoiDung,
                    Sum(ISNULL(A.fTienTuChi,0))   as fTienTuChi,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    1 as IRemainRow,
					'LNS' SDuToanChiTietToi,
					null SM

				 FROM #tempTotal A
					 group by A.iID_MaDonVi
              UNION
              SELECT 
                    NEWID() as iID_MLNS,
                    N'Số còn lại' as SNoiDung,
                    0 as fTienTuChi,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    2 as IRemainRow,
					'LNS' SDuToanChiTietToi,
					null SM
					;
	END
	ELSE
	BEGIN 
		SELECT 
                    NEWID() as iID_MLNS,
                    N'10 % số điều chỉnh thu BHYT quân nhân' as SNoiDung,
                    0   as fTienTuChi,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    1 as IRemainRow,
					'LNS' SDuToanChiTietToi,
					null SM
              UNION
              SELECT 
                    NEWID() as iID_MLNS,
                    N'Số còn lại' as SNoiDung,
                    0 as fTienTuChi,
                    1 as IsRemainRow,
                    1 as BHangCha,
					1 as IsHangCha,
                    2 as IRemainRow,
					'LNS' SDuToanChiTietToi,
					null SM
					;
	END
			drop table #temp1
			drop table #temp2
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]    Script Date: 2/20/2024 4:45:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_chungtu_chi_tiet_cssk_hssv_nld]
	@VoucherId uniqueidentifier ,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi nvarchar(255)
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) 
	FROM BH_CP_ChungTu_ChiTiet
	WHERE iID_CP_ChungTu = @VoucherId

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sCPChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and iTrangThai=1
	and sLNS in (select * from splitstring(@LNS))
	--and  bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTienDuToan,
		  --fTongTien AS fTienDuToan,
           0 AS fTienKeHoachCap,
          0 AS fTienDaCap,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @YearOfWork
          AND iID_LoaiDanhMucChi = @iID_LoaiDanhMucChi
		  AND bIsKhoa=1
          AND cast(dNgayQuyetDinh AS date) <= cast(@VoucherDate AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@AgencyId))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach

	-- lấy dữ liệu đã cấp
	SELECT ISNULL(SUM(fTienDuToan),0) AS fTienDuToan,
		  ISNULL(SUM(fTienKeHoachCap),0)  AS fTienKeHoachCap,
		  ISNULL(SUM(fTienDaCap),0)  AS fTienDaCap,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #tblDataDaCap
	FROM BH_CP_ChungTu_ChiTiet
	WHERE iID_CP_ChungTu IN
       (
		SELECT iID_CP_ChungTu
        FROM BH_CP_ChungTu
        WHERE iNamChungTu = @YearOfWork
		  AND iID_LoaiCap = @iID_LoaiDanhMucChi
          AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(@AgencyId))
		  AND iID_CP_ChungTu=@VoucherId
		)
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)
	GROUP BY iID_MaDonVi, iID_MucLucNganSach

	-- tao bang tam chua don vi
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
	
	
	
	-- Tao bang tam luu chu mlns cha co don vi
	SELECT *,'' AS iID_MaDonVi, '' AS sTenDonVi   INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblNsMlns 
	WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) 
	
		-- Tao bang tam luu chu mlns cha co don vi
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblNsMlns ,#tblDonVi dv
		WHERE  bHangCha=0

		-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT *
		FROM #tblMlnsByPhanCapExistDonVi tbl
		UNION ALL
		SELECT *
		FROM #tblMlnsByPhanCapNotDonVi 
	) mlns

	
	---- Tao bang tam luu chu mlns va tien du toan chi che do bhxh
	--SELECT ml.sXauNoiMa,
	--	SUM(pb.fTienDuToan) fTienDuToan ,
	--	ml.iID_MaDonVi 
	--	INTO #tempMlnsbhxh 
	--	FROM #tblMlnsExistDonVi ml
 --  LEFT JOIN #tblPhanBoDuToan pb ON pb.iID_MucLucNganSach=ml.iID_MLNS and ml.iID_MaDonVi=pb.iID_MaDonVi
 --  GROUP BY ml.sXauNoiMa,ml.iID_MaDonVi
	--	ORDER BY ml.sXauNoiMa

	--SELECT SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa,
	--	A.iID_MaDonVi, 
	--	SUM(A.fTienDuToan) fTienDuToan
	--	INTO #tblDaCapDuToanResult
	--	FROM #tempMlnsbhxh  A
	--	GROUP BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi
	--	ORDER BY SUBSTRING(A.sXauNoiMa,1,3),A.iID_MaDonVi

	-- Get data
	SELECT
		isnull(ctct.iID_CP_ChungTu_ChiTiet, @iD) AS iID_CP_ChungTu_ChiTiet,
		@VoucherId AS iID_CP_ChungTu,
		mlnsPhanBo.iID_MLNS AS iID_MucLucNganSach,
		mlnsPhanBo.iID_MLNS_Cha AS IdParent,
		mlnsPhanBo.sXauNoiMa AS sXauNoiMa,
		mlnsPhanBo.sLNS AS SLNS,
		mlnsPhanBo.sL AS SL,
		mlnsPhanBo.sK AS SK,
		mlnsPhanBo.sM AS SM,
		mlnsPhanBo.sTM AS STM,
		mlnsPhanBo.sTTM AS STTM,
		mlnsPhanBo.sNG AS SNG,
		mlnsPhanBo.sTNG AS STNG,
		mlnsPhanBo.sTNG1 AS STNG1,
		mlnsPhanBo.sTNG2 AS STNG2,
		mlnsPhanBo.sTNG3 AS STNG3,
		mlnsPhanBo.sMoTa AS SNoiDung,
		mlnsPhanBo.bHangCha As IsHangCha,
		mlnsPhanBo.sCPChiTietToi sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(ctct.fTienDaCap, 0) as FTienDaCap,
		0 as FTienDuToan,
		isnull(ctct.fTienKeHoachCap, 0) as FTienKeHoachCap,
		ctct.sGhiChu AS SGhiChu,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMLNS AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_CP_ChungTu_ChiTiet
			WHERE 
		 		iID_CP_ChungTu = @VoucherId
				
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_CP_ChungTu ct ON ctct.iID_CP_ChungTu = ct.iID_CP_ChungTu
	--LEFT JOIN #tempMlnsbhxh daCapDuToan
	--on mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa and daCapDuToan.iID_MaDonVi= mlnsPhanBo.iID_MaDonVi
	
	order by mlnsPhanBo.sXauNoiMa ,mlnsPhanBo.iID_MaDonVi;

END
;
;
;
;
;
;
;
GO
