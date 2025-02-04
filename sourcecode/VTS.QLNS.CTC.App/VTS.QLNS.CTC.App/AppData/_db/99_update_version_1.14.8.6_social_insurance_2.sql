/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 10/1/2024 4:58:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]    Script Date: 10/1/2024 4:58:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_nkp_ql_chitiet]    Script Date: 10/1/2024 4:58:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_nkp_ql_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_nkp_ql_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_nkp_ql_chitiet]    Script Date: 10/1/2024 4:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
CREATE PROCEDURE [dbo].[sp_bh_qtc_nkp_ql_chitiet]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @Loai int 
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';

	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = fSoThamDinh
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @NamLamViec - 1 and iMa = 254 and iID_MaDonVi=@IdDonVi

	-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @NamLamViec 
		AND iTrangThai=1
		AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	-- lấy ra mlns theo phân cấp
	SELECT danhmuc.iID_MLNS, danhmuc.iID_MLNS_Cha, danhmuc.sXauNoiMa, danhmuc.sLNS, danhmuc.sL, danhmuc.sK, danhmuc.sM, danhmuc.sTM, danhmuc.sTTM, danhmuc.sNG, danhmuc.sTNG, danhmuc.sTNG1, danhmuc.sTNG2, danhmuc.sTNG3, danhmuc.sMoTa, danhmuc.bHangCha, danhmuc.sDuToanChiTietToi,danhmuc.bHangChaDuToan, dv.iID_MaDonVi,
		dv.sTenDonVi
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach AS danhmuc ,#tblDonVi dv
	WHERE 
		danhmuc.iNamLamViec = @NamLamViec 
		AND danhmuc.iTrangThai = 1
		AND danhmuc.sLNS IN (SELECT * FROM f_split(@LNS))
	-- Get data chung tu thuong
	IF @Loai=1
	SELECT   isnull(ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet, @iDCT) as ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
				@iD as IID_QTC_Nam_KinhPhiQuanLy,
				mlns.iID_MLNS as iID_MucLucNganSach,
				mlns.iID_MLNS_Cha as IdParent,
				mlns.sXauNoiMa as sXauNoiMa,
				mlns.sLNS as SLNS,
				mlns.sL as SL,
				mlns.sK as SK,
				mlns.sM as SM,
				mlns.sTM as STM,
				mlns.sTTM as STTM,
				mlns.sNG as SNG,
				mlns.sTNG as STNG,
				mlns.sMoTa as SNoiDung,
				mlns.sDuToanChiTietToi as SDuToanChiTietToi,
				mlns.bHangChaDuToan,
				mlns.iID_MaDonVi as IIdMaDonVi,
				mlns.sTenDonVi as STenDonVi,
				@NamLamViec AS INamLamViec,
				mlns.bHangCha as IsHangCha,
				isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
				isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
				ctct.sNguoiTao AS sNguoiTao,
				ctct.sNguoiSua AS sNguoiSua,
				CASE WHEN mlns.sXauNoiMa = @LNS THEN @fSoThamDinh ELSE 0 END as fDuToanNamTruocChuyenSang,
				--ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0)  as fTien_DuToanNamTruocChuyenSang, 
				ISNULL(daDuToan.fTienDuToan, 0)  as fTien_DuToanGiaoNamNay,
				ISNULL(ctct.fTien_TongDuToanDuocGiao, 0)  as fTien_TongDuToanDuocGiao, 
				ISNULL(ctct.fTien_ThucChi, 0)  as fTien_ThucChi, 
				ISNULL(ctct.fTienThua, 0)  as fTienThua, 
				ISNULL(ctct.fTienThieu, 0)  as fTienThieu, 
				ISNULL(ctct.fTiLeThucHienTrenDuToan, 0)  as fTiLeThucHienTrenDuToan
			FROM 
				#tblMlnsByPhanCap mlns
			LEFT JOIN 
				(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
						WHERE iID_QTC_Nam_KinhPhiQuanLy in
							( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
										WHERE iNamLamViec=@NamLamViec
										AND ID_QTC_Nam_KinhPhiQuanLy=@iD
										AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
										)) ctct on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ctct.iID_MaDonVi = ddv.iID_MaDonVi
			LEFT JOIN (
					-- lấy ra dữ liệu dự toán
					SELECT 
						  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
						  sXauNoiMa,
						  iID_MaDonVi
				   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
				   WHERE iID_DTC_PhanBoDuToanChi IN
					   (SELECT ID
						FROM BH_DTC_PhanBoDuToanChi
						WHERE sSoQuyetDinh <> ''
						  AND sSoQuyetDinh IS NOT NULL
						  AND iNamChungTu = @NamLamViec
						  AND bIsKhoa=1)
					 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
				   GROUP BY iID_MaDonVi, 
				   sXauNoiMa
				) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
			Order by mlns.sXauNoiMa
	-- Get data chung tu tong hop
	ELSE
	SELECT   isnull(ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet, @iDCT) as ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
		@iD as IID_QTC_Nam_KinhPhiQuanLy,
		mlns.iID_MLNS as iID_MucLucNganSach,
		mlns.iID_MLNS_Cha as IdParent,
		mlns.sXauNoiMa as sXauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		mlns.sDuToanChiTietToi as SDuToanChiTietToi,
		mlns.bHangChaDuToan,
		mlns.iID_MaDonVi as IIdMaDonVi,
		mlns.sTenDonVi as STenDonVi,
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS sNguoiTao,
		ctct.sNguoiSua AS sNguoiSua,
		CASE WHEN mlns.sXauNoiMa = @LNS THEN @fSoThamDinh ELSE 0 END as fDuToanNamTruocChuyenSang,
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0)  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(daDuToan.fTienDuToan, 0)  as fTien_DuToanGiaoNamNay,
		ISNULL(ctct.fTien_TongDuToanDuocGiao, 0)  as fTien_TongDuToanDuocGiao, 
		ISNULL(ctct.fTien_ThucChi, 0)  as fTien_ThucChi, 
		ISNULL(ctct.fTienThua, 0)  as fTienThua, 
		ISNULL(ctct.fTienThieu, 0)  as fTienThieu, 
		ISNULL(ctct.fTiLeThucHienTrenDuToan, 0)  as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Nam_KinhPhiQuanLy in
					( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
								WHERE iNamLamViec=@NamLamViec
								AND ID_QTC_Nam_KinhPhiQuanLy=@iD
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	LEFT JOIN 
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ctct.iID_MaDonVi = ddv.iID_MaDonVi
	LEFT JOIN (
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa,
				  iID_MaDonVi
		   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
		   WHERE iID_DTC_DuToanChiTrenGiao IN
			   (SELECT ID
				FROM BH_DTC_DuToanChiTrenGiao
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamLamViec = @NamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
		   GROUP BY iID_MaDonVi, 
		   sXauNoiMa
		) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
	Order by mlns.sXauNoiMa
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
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]    Script Date: 10/1/2024 4:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]
@IdChungTu uniqueidentifier,
@INamLamViec int,
@IsTongHop4Quy bit,
@Loai int,
@MaDonVi nvarchar(100)
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.sDuToanChiTietToi,
			danhmuc.bHangCha,
			danhmuc.bHangChaDuToan
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in ('9010001', '9010002')
				and danhmuc.iTrangThai=1
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Nam_CheDoBHXH_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,
			qtcn_ct.iID_MaDonVi,
			qtcn_ct.iNamLamViec,
			qtcn_ct.sXauNoiMa,
			qtcn_ct.fTienDuToanDuyet, ---3
			qtcn_ct.iSoDuToanDuocDuyet, --2

			qtcn_ct.iTongSo_ThucChi,
			qtcn_ct.fTongTien_ThucChi, ---5

			qtcn_ct.iSoSQ_ThucChi, ---6
			qtcn_ct.fTienSQ_ThucChi, ---7

			qtcn_ct.iSoQNCN_ThucChi, ----8
			qtcn_ct.fTienQNCN_ThucChi,---9

			qtcn_ct.iSoCNVCQP_ThucChi,---10
			qtcn_ct.fTienCNVCQP_ThucChi, ----11

			qtcn_ct.iSoLDHD_ThucChi, ----13
			qtcn_ct.fTienLDHD_ThucChi, ---14

			qtcn_ct.iSoHSQBS_ThucChi, ----15
			qtcn_ct.fTienHSQBS_ThucChi, ---16

			Case when isnull(qtcn_ct.fTienDuToanDuyet,0) > isnull(qtcn_ct.fTongTien_ThucChi,0) then isnull(qtcn_ct.fTienDuToanDuyet,0) - isnull(qtcn_ct.fTongTien_ThucChi,0)  ELSE  0 end fTienThua,
			Case when isnull(qtcn_ct.fTongTien_ThucChi,0) > isnull(qtcn_ct.fTienDuToanDuyet,0) then isnull(qtcn_ct.fTongTien_ThucChi,0) - isnull(qtcn_ct.fTienDuToanDuyet,0)  ELSE  0 end fTienThieu,
			Case when isnull(qtcn_ct.fTienDuToanDuyet,0)>0 then isnull(qtcn_ct.fTongTien_ThucChi,0)/ isnull(qtcn_ct.fTienDuToanDuyet,0)  ELSE  0 end fTiLeThucHienTrenDuToan
		into #tblQuyetToanNamChiTiet
		from BH_QTC_Nam_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Nam_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Nam_CheDoBHXH = qtcn.ID_QTC_Nam_CheDoBHXH
		where qtcn_ct.iID_QTC_Nam_CheDoBHXH = @IdChungTu;

IF @Loai=1
	---Kết quả hiển thị trả về
	select 
		mucluc.iID_MLNS,
		mucluc.iID_MLNS_Cha,
		mucluc.sLNS,
		mucluc.sL,
		mucluc.sK,
		mucluc.sM,
		mucluc.sTM,
		mucluc.sTTM,
		mucluc.sNG,
		mucluc.sTNG,
		mucluc.sXauNoiMa,
		mucluc.bHangCha,
		mucluc.bHangChaDuToan,
		chi_tiet.ID_QTC_Nam_CheDoBHXH_ChiTiet,
		mucluc.iID_MLNS as iID_MucLucNganSach,
		mucluc.sMoTa as sLoaiTroCap,
		mucluc.sDuToanChiTietToi,
		chi_tiet.iNamLamViec,
		chi_tiet.iID_MaDonVi,
		ddv.sTenDonVi,
		daDuToan.fTienDuToan as fTienDuToanDuyet, 
		chi_tiet.iSoDuToanDuocDuyet,

		chi_tiet.iTongSo_ThucChi, 
		chi_tiet.fTongTien_ThucChi,

		chi_tiet.iSoSQ_ThucChi,
		chi_tiet.fTienSQ_ThucChi,

		chi_tiet.iSoQNCN_ThucChi,
		chi_tiet.fTienQNCN_ThucChi,

		chi_tiet.iSoCNVCQP_ThucChi,
		chi_tiet.fTienCNVCQP_ThucChi,

		chi_tiet.iSoLDHD_ThucChi,
		chi_tiet.fTienLDHD_ThucChi,

		chi_tiet.iSoHSQBS_ThucChi,
		chi_tiet.fTienHSQBS_ThucChi,

		chi_tiet.fTienThua,
		chi_tiet.fTienThieu,
		chi_tiet.fTiLeThucHienTrenDuToan
	from #tblMucLucNganSach as mucluc
	left join #tblQuyetToanNamChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
	LEFT JOIN 
		(SELECT * FROM DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1 ) ddv ON chi_tiet.iID_MaDonVi = ddv.iID_MaDonVi
	LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa,
				  iID_MaDonVi
		   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
		   WHERE iID_DTC_PhanBoDuToanChi IN
			   (SELECT ID
				FROM BH_DTC_PhanBoDuToanChi
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamChungTu = @INamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@MaDonVi))-- donvi
		   GROUP BY iID_MaDonVi, 
		   sXauNoiMa
		) daDuToan  on mucluc.sXauNoiMa=daDuToan.sXauNoiMa
	order by mucluc.sXauNoiMa
ELSE
	select 
		mucluc.iID_MLNS,
		mucluc.iID_MLNS_Cha,
		mucluc.sLNS,
		mucluc.sL,
		mucluc.sK,
		mucluc.sM,
		mucluc.sTM,
		mucluc.sTTM,
		mucluc.sNG,
		mucluc.sTNG,
		mucluc.sXauNoiMa,
		mucluc.bHangCha,
		chi_tiet.ID_QTC_Nam_CheDoBHXH_ChiTiet,
		mucluc.iID_MLNS as iID_MucLucNganSach,
		mucluc.sMoTa as sLoaiTroCap,
		mucluc.sDuToanChiTietToi,
		mucluc.bHangChaDuToan,
		chi_tiet.iNamLamViec,
		chi_tiet.iID_MaDonVi,
		ddv.sTenDonVi,
		daDuToan.fTienDuToan as fTienDuToanDuyet, 
		chi_tiet.iSoDuToanDuocDuyet,

		chi_tiet.iTongSo_ThucChi, 
		chi_tiet.fTongTien_ThucChi,

		chi_tiet.iSoSQ_ThucChi,
		chi_tiet.fTienSQ_ThucChi,

		chi_tiet.iSoQNCN_ThucChi,
		chi_tiet.fTienQNCN_ThucChi,

		chi_tiet.iSoCNVCQP_ThucChi,
		chi_tiet.fTienCNVCQP_ThucChi,

		chi_tiet.iSoLDHD_ThucChi,
		chi_tiet.fTienLDHD_ThucChi,

		chi_tiet.iSoHSQBS_ThucChi,
		chi_tiet.fTienHSQBS_ThucChi,

		chi_tiet.fTienThua,
		chi_tiet.fTienThieu,
		chi_tiet.fTiLeThucHienTrenDuToan
	from #tblMucLucNganSach as mucluc
	LEFT JOIN #tblQuyetToanNamChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
	LEFT JOIN 
		(SELECT * FROM DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1 ) ddv ON chi_tiet.iID_MaDonVi = ddv.iID_MaDonVi
	LEFT JOIN (
		-- lấy ra dữ liệu dự toán tren giao
		SELECT 
			  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
			  sXauNoiMa,
			  iID_MaDonVi
	   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
	   WHERE iID_DTC_DuToanChiTrenGiao IN
		   (SELECT ID
			FROM BH_DTC_DuToanChiTrenGiao
			WHERE sSoQuyetDinh <> ''
			  AND sSoQuyetDinh IS NOT NULL
			  AND iNamLamViec = @INamLamViec
			  AND bIsKhoa=1)
		 AND iID_MaDonVi in  (SELECT * FROM f_split(@MaDonVi))-- donvi
	   GROUP BY iID_MaDonVi, 
	   sXauNoiMa
	) daDuToan  on mucluc.sXauNoiMa=daDuToan.sXauNoiMa
	order by mucluc.sXauNoiMa

	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanNamChiTiet;
end
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 10/1/2024 4:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max),
	 @DonViTnh int,
	 @Loai int
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = Sum(fSoThamDinh)
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @NamLamViec - 1 and iMa = 254 and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi))
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, sDuToanChiTietToi,bHangChaDuToan
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		AND sLNS IN (SELECT * FROM f_split(@LNS))

IF @Loai=1
	SELECT   isnull(ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet, @iDCT) as ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
			ctct.IID_QTC_Nam_KinhPhiQuanLy,
			mlns.iID_MLNS as iID_MucLucNganSach,
			mlns.iID_MLNS_Cha as IdParent,
			mlns.sXauNoiMa as sXauNoiMa,
			mlns.sLNS as SLNS,
			mlns.sL as SL,
			mlns.sK as SK,
			mlns.sM as SM,
			mlns.sTM as STM,
			mlns.sTTM as STTM,
			mlns.sNG as SNG,
			mlns.sTNG as STNG,
			mlns.sMoTa as SNoiDung,
			mlns.sDuToanChiTietToi,
			mlns.bHangChaDuToan,
			--ctct.iID_MaDonVi,
			@NamLamViec AS INamLamViec,
			mlns.bHangCha as IsHangCha,
			isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
			isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
			ctct.sNguoiTao AS sNguoiTao,
			ctct.sNguoiSua AS sNguoiSua,
			CASE WHEN mlns.sXauNoiMa = @LNS THEN ROUND(@fSoThamDinh / @DonViTnh,0) ELSE 0 END as fTien_DuToanNamTruocChuyenSang,
			CASE WHEN mlns.sXauNoiMa = @LNS THEN ROUND(@fSoThamDinh / @DonViTnh,0) ELSE 0 END as FDuToanNamTruocChuyenSang,
			ROUND(ISNULL((daDuToan.fTienDuToan), 0) / @DonViTnh,0)  as fTien_DuToanGiaoNamNay,
			ROUND(ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0) / @DonViTnh + ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0)/ @DonViTnh,0)  as fTien_TongDuToanDuocGiao, 
			ROUND(ISNULL(Sum(ctct.fTien_ThucChi), 0) / @DonViTnh,0)  as fTien_ThucChi, 
			ROUND(ISNULL(Sum(ctct.fTienThua), 0) / @DonViTnh,0)  as fTienThua, 
			ROUND(ISNULL(Sum(ctct.fTienThieu), 0) / @DonViTnh,0)  as fTienThieu, 
			--ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0) / @DonViTnh  as fTiLeThucHienTrenDuToan
			ROUND(ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0),0)   as fTiLeThucHienTrenDuToan
		FROM 
			#tblMlnsByPhanCap mlns
		LEFT JOIN 
			(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
					WHERE iID_QTC_Nam_KinhPhiQuanLy in
						( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
									WHERE iNamLamViec=@NamLamViec
									AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
									)) ctct
		on mlns.iID_MLNS = ctct.iID_MucLucNganSach
		LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa
		   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
		   WHERE iID_DTC_PhanBoDuToanChi IN
			   (SELECT ID
				FROM BH_DTC_PhanBoDuToanChi
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamChungTu = @NamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
		   GROUP BY sXauNoiMa
		) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
		Group by ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
			ctct.IID_QTC_Nam_KinhPhiQuanLy,
			--ctct.iID_MaDonVi,
			mlns.iID_MLNS ,
			mlns.iID_MLNS_Cha ,
			mlns.sXauNoiMa ,
			mlns.sLNS ,
			mlns.sL ,
			mlns.sK ,
			mlns.sM ,
			mlns.sTM ,
			mlns.sTTM ,
			mlns.sNG ,
			mlns.sTNG ,
			mlns.sMoTa ,
			mlns.bHangCha ,
			ctct.sNguoiTao ,
			ctct.sNguoiSua, 
			ctct.dNgayTao,
			mlns.sDuToanChiTietToi,
			ctct.dNgaySua,
			mlns.bHangChaDuToan,
			daDuToan.fTienDuToan
	Order by mlns.sXauNoiMa
	ELSE

		SELECT   isnull(ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet, @iDCT) as ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
			ctct.IID_QTC_Nam_KinhPhiQuanLy,
			mlns.iID_MLNS as iID_MucLucNganSach,
			mlns.iID_MLNS_Cha as IdParent,
			mlns.sXauNoiMa as sXauNoiMa,
			mlns.sLNS as SLNS,
			mlns.sL as SL,
			mlns.sK as SK,
			mlns.sM as SM,
			mlns.sTM as STM,
			mlns.sTTM as STTM,
			mlns.sNG as SNG,
			mlns.sTNG as STNG,
			mlns.sMoTa as SNoiDung,
			mlns.sDuToanChiTietToi,
			mlns.bHangChaDuToan,
			--ctct.iID_MaDonVi,
			@NamLamViec AS INamLamViec,
			mlns.bHangCha as IsHangCha,
			isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
			isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
			ctct.sNguoiTao AS sNguoiTao,
			ctct.sNguoiSua AS sNguoiSua,
			CASE WHEN mlns.sXauNoiMa = @LNS THEN ROUND(@fSoThamDinh / @DonViTnh,0) ELSE 0 END as fTien_DuToanNamTruocChuyenSang,
			CASE WHEN mlns.sXauNoiMa = @LNS THEN ROUND(@fSoThamDinh / @DonViTnh,0) ELSE 0 END as FDuToanNamTruocChuyenSang,
			ROUND(ISNULL((daDuToan.fTienDuToan), 0) / @DonViTnh,0)  as fTien_DuToanGiaoNamNay,
			ROUND(ISNULL(Sum(ctct.fTien_DuToanNamTruocChuyenSang), 0) / @DonViTnh + ISNULL(Sum(ctct.fTien_DuToanGiaoNamNay), 0)/ @DonViTnh,0)  as fTien_TongDuToanDuocGiao, 
			ROUND(ISNULL(Sum(ctct.fTien_ThucChi), 0) / @DonViTnh,0)  as fTien_ThucChi, 
			ROUND(ISNULL(Sum(ctct.fTienThua), 0) / @DonViTnh,0)  as fTienThua, 
			ROUND(ISNULL(Sum(ctct.fTienThieu), 0) / @DonViTnh,0)  as fTienThieu, 
			--ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0) / @DonViTnh  as fTiLeThucHienTrenDuToan
			ROUND(ISNULL(Sum(ctct.fTiLeThucHienTrenDuToan), 0),0)   as fTiLeThucHienTrenDuToan
		FROM 
			#tblMlnsByPhanCap mlns
		LEFT JOIN 
			(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
					WHERE iID_QTC_Nam_KinhPhiQuanLy in
						( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
									WHERE iNamLamViec=@NamLamViec
									AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
									)) ctct
		on mlns.iID_MLNS = ctct.iID_MucLucNganSach
		LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa
		   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
		   WHERE iID_DTC_DuToanChiTrenGiao IN
			   (SELECT ID
				FROM BH_DTC_DuToanChiTrenGiao
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamLamViec = @NamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdDonVi))-- donvi
		   GROUP BY sXauNoiMa
		) daDuToan  on mlns.sXauNoiMa=daDuToan.sXauNoiMa
		Group by ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
			ctct.IID_QTC_Nam_KinhPhiQuanLy,
			--ctct.iID_MaDonVi,
			mlns.iID_MLNS ,
			mlns.iID_MLNS_Cha ,
			mlns.sXauNoiMa ,
			mlns.sLNS ,
			mlns.sL ,
			mlns.sK ,
			mlns.sM ,
			mlns.sTM ,
			mlns.sTTM ,
			mlns.sNG ,
			mlns.sTNG ,
			mlns.sMoTa ,
			mlns.bHangCha ,
			ctct.sNguoiTao ,
			ctct.sNguoiSua, 
			ctct.dNgayTao,
			mlns.sDuToanChiTietToi,
			mlns.bHangChaDuToan,
			ctct.dNgaySua,
			daDuToan.fTienDuToan
	Order by mlns.sXauNoiMa

	drop table #tblMlnsByPhanCap;

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
GO
