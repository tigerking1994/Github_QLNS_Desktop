/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]    Script Date: 2/21/2024 11:08:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]    Script Date: 2/21/2024 11:08:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]    Script Date: 2/21/2024 11:08:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_getdieuchinhthu_BHYTQN]
	@NamLamViec int,
	@IdDonVi nvarchar(200),
	@DNgayChungTu Datetime
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
							AND CONVERT(DATE,dNgayChungTu)<= CONVERT(DATE,@DNgayChungTu)

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
							AND CONVERT(DATE,dNgayChungTu)<= CONVERT(DATE,@DNgayChungTu)

					)
					and
                    (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0001%')
                    AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
                    AND iNamLamViec = @NamLamViec

			SELECT 
                    ( sum
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
/****** Object:  StoredProcedure [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]    Script Date: 2/21/2024 11:08:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_nhanphanbo_getdieuchinhthu_BHYTQN]
	@NamLamViec int,
	@IdDonVi nvarchar(200),
	@DNgayChungTu Datetime
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
							AND CONVERT(DATE,dNgayChungTu)<= CONVERT(DATE,@DNgayChungTu)

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
							AND CONVERT(DATE,dNgayChungTu)<= CONVERT(DATE,@DNgayChungTu)

					)
					and
                    (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0001%')
                    AND iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
                    AND iNamLamViec = @NamLamViec

			SELECT 
                    (sum
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
;
GO
