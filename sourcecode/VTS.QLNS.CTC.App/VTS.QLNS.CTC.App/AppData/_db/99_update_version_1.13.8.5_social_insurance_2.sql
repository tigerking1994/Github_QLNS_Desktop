/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]    Script Date: 1/13/2024 5:51:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quyKhac_getTienDuToanDuocGiao_chi_tiet]    Script Date: 1/13/2024 5:51:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_quyKhac_getTienDuToanDuocGiao_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_quyKhac_getTienDuToanDuocGiao_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_Create_fTienDuToanDuyet_forQTCNBHXH_pbdtchi]    Script Date: 1/13/2024 5:51:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_Create_fTienDuToanDuyet_forQTCNBHXH_pbdtchi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_Create_fTienDuToanDuyet_forQTCNBHXH_pbdtchi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]    Script Date: 1/13/2024 5:51:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]    Script Date: 1/13/2024 5:51:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]
@IdChungTu uniqueidentifier,
@INamLamViec int,
@IsTongHop4Quy bit
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
			danhmuc.bHangCha
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
		chi_tiet.ID_QTC_Nam_CheDoBHXH_ChiTiet,
		mucluc.iID_MLNS as iID_MucLucNganSach,
		mucluc.sMoTa as sLoaiTroCap,
		chi_tiet.iNamLamViec,
		chi_tiet.iID_MaDonVi,
		chi_tiet.fTienDuToanDuyet, 
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
	left join #tblQuyetToanNamChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
	order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanNamChiTiet;
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_Create_fTienDuToanDuyet_forQTCNBHXH_pbdtchi]    Script Date: 1/13/2024 5:51:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_bh_quyet_toan_Create_fTienDuToanDuyet_forQTCNBHXH_pbdtchi]
@INamLamViec int,
@SMaLoaiChi nvarchar(50),
@IDLoaiChi nvarchar(max),
@SMaDonVi nvarchar(50),
@SLNS nvarchar(50),
@IDChungTu nvarchar(100),
@DNgayChungTu datetime

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
			danhmuc.bHangCha,
			danhmuc.sDuToanChiTietToi
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (SELECT * FROM splitstring(@SLNS))
				and danhmuc.iTrangThai=1
		
	
	-- lay dữ liệu bên phân bổ chi
		select 
			pbdt_ct.iID_MucLucNganSach,
			pbdt_ct.iID_MaDonVi,
			Sum(pbdt_ct.fTienTuChi) as fTienDuToanDuyet
			--Sum(pbdt_ct.fTienTuChi) as fTienDuToanDuyet, ---3
			--0 iSoDuToanDuocDuyet, --2

			--0 iTongSo_ThucChi,
			--0 fTongTien_ThucChi, ---5

			--0 iSoSQ_ThucChi, ---6
			--0 fTienSQ_ThucChi, ---7

			--0 iSoQNCN_ThucChi, ----8
			--0 fTienQNCN_ThucChi,---9

			--0 iSoCNVCQP_ThucChi,---10
			--0 fTienCNVCQP_ThucChi, ----11

			--0 iSoLDHD_ThucChi, ----13
			--0 fTienLDHD_ThucChi, ---14

			--0 iSoHSQBS_ThucChi, ----15

			--0  as fTienThua,
			--0 as  fTienThieu

		into #tblQuyetToanPhanBoChiTiet
		from BH_DTC_PhanBoDuToanChi_ChiTiet as pbdt_ct
		where pbdt_ct.iID_DTC_PhanBoDuToanChi in
		( SELECT ID FROM  BH_DTC_PhanBoDuToanChi
					WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamChungTu = @INamLamViec
				  AND iID_LoaiDanhMucChi = @IDLoaiChi
				  AND sMaLoaiChi=@SMaLoaiChi
				  AND bIsKhoa=1
				  AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date))
				AND iID_MaDonVi in  (SELECT * FROM splitstring(@SMaDonVi))
			group by pbdt_ct.iID_MucLucNganSach,
					pbdt_ct.iID_MaDonVi

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
		mucluc.iID_MLNS as iID_MucLucNganSach,
		mucluc.sMoTa as sLoaiTroCap,
		mucluc.sDuToanChiTietToi,
		chi_tiet.iID_MaDonVi,
		chi_tiet.fTienDuToanDuyet
		INTO #tblChungTuChiTiet
		--chi_tiet.iSoDuToanDuocDuyet,

		--chi_tiet.iTongSo_ThucChi, 
		--chi_tiet.fTongTien_ThucChi,

		--chi_tiet.iSoSQ_ThucChi,
		--chi_tiet.fTienSQ_ThucChi,

		--chi_tiet.iSoQNCN_ThucChi,
		--chi_tiet.fTienQNCN_ThucChi,

		--chi_tiet.iSoCNVCQP_ThucChi,
		--chi_tiet.fTienCNVCQP_ThucChi,

		--chi_tiet.iSoLDHD_ThucChi,
		--chi_tiet.fTienLDHD_ThucChi,

		--chi_tiet.iSoHSQBS_ThucChi,
		--chi_tiet.fTienHSQBS_ThucChi,

		--chi_tiet.fTienThua,
		--chi_tiet.fTienThieu
	from #tblMucLucNganSach as mucluc
	left join #tblQuyetToanPhanBoChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
	order by mucluc.sXauNoiMa
	
	INSERT INTO BH_QTC_Nam_CheDoBHXH_ChiTiet (ID_QTC_Nam_CheDoBHXH_ChiTiet, iID_QTC_Nam_CheDoBHXH, iID_MaDonVi, iNamLamViec,sXauNoiMa,iID_MucLucNganSach,fTienDuToanDuyet)
	SELECT
	NEWID() AS  ID_QTC_Nam_CheDoBHXH_ChiTiet,
	@IDChungTu,
	@SMaDonVi,
	@INamLamViec,
	#tblChungTuChiTiet.sXauNoiMa,
	#tblChungTuChiTiet.iID_MucLucNganSach,
	#tblChungTuChiTiet.fTienDuToanDuyet
	FROM #tblChungTuChiTiet

	
	drop table #tblMucLucNganSach;
	drop table #tblChungTuChiTiet;
	drop table #tblQuyetToanPhanBoChiTiet;
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quyKhac_getTienDuToanDuocGiao_chi_tiet]    Script Date: 1/13/2024 5:51:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 1/06/2024
-- Description:	Lấy danh sách hiển thị chứng từ 

-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_quyKhac_getTienDuToanDuocGiao_chi_tiet]
	@YearOfWork int,
	@LNS nvarchar(max),
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi nvarchar(500),
	@Quy int
AS
BEGIN
		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS FTien_DuToanGiaoNamNay,
          iID_MaDonVi,
          iID_MucLucNganSach
		  into #Temp
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

	   SELECT * FROM #Temp
	--   SELECT 
	--	qtcn_ct.iID_MucLucNganSach,
	--	qtcn_ct.sNoiDung,
	--	SUM(qtcn_ct.fTienXacNhanQuyetToanQuyNay) fTienQuyetToanDaDuyet
	--	into #Temp1
	--FROM BH_QTC_Quy_KCB_ChiTiet qtcn_ct
	--	inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
	--WHERE qtcn.iQuyChungTu < @Quy
	--		AND qtcn.iID_MaDonVi in (SELECT * FROM f_split(@AgencyId))
	--		AND qtcn.iNamChungTu = @YearOfWork
	--		--AND qtcn.bIsKhoa=1
	--	GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sNoiDung


	--SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
	--		into #tblNsMlns
	--FROM BH_DM_MucLucNganSach 
	--WHERE iNamLamViec = @YearOfWork 
	--	and	sLNS in (SELECT * FROM f_split(@LNS))

	--SELECT TBL.iID_MLNS AS iID_MucLucNganSach,
	--		T.FTienDuToanDuocGiao,
	--		T1.fTienQuyetToanDaDuyet
	--FROM #tblNsMlns TBL
	--LEFT JOIN #Temp T ON TBL.iID_MLNS=T.iID_MucLucNganSach
	--LEFT JOIN #Temp1 T1 ON TBL.iID_MLNS=T1.iID_MucLucNganSach

		DROP TABLE  #Temp
	--DROP TABLE  #Temp1
	--DROP TABLE  #tblNsMlns
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]    Script Date: 1/13/2024 5:51:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_qtc_quykinhphi_khac_chungtu_chi_tiet]
	@VoucherId uniqueidentifier,
	@LNS nvarchar(max),
	@YearOfWork int,
	@AgencyId nvarchar(500),
	@VoucherDate datetime,
	@iID_LoaiDanhMucChi uniqueidentifier,
	@iQuyChungTu int
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@LNS);
	SELECT * into #tempAgency from  f_split(@AgencyId);
	SELECT @CountIndex = COUNT(*) FROM 
									BH_QTC_Quy_KPK_ChiTiet 
									WHERE iID_QTC_Quy_KPK =@VoucherId

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sDuToanChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @YearOfWork
	and iTrangThai=1;

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTien_DuToanGiaoNamNay,
          0 AS fTien_DuToanNamTruocChuyenSang,
          0 AS fTien_TongDuToanDuocGiao,
		  0 AS fTienThucChi,
		  0 AS fTienQuyetToanDaDuyet,
		  0 AS fTienDeNghiQuyetToanQuyNay,
		  0 AS fTienXacNhanQuyetToanQuyNay,
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

	SELECT DISTINCT iID_MucLucNganSach
	--, iID_MLNS_Cha 
	into #tblPhanBoDuToanGroupbyiID_MLNS from #tblPhanBoDuToan

	SELECT T.* INTO #tblNsMlns1
	FROM #tblNsMlns T
	INNER JOIN #tblPhanBoDuToanGroupbyiID_MLNS E ON T.iID_MLNS = E.iID_MucLucNganSach 


	;WITH C AS  
	(  
	  SELECT *
	  FROM #tblNsMlns1
	  UNION ALL 
	  SELECT T.*
	  FROM #tblNsMlns T
	  INNER JOIN C
	  ON T.iID_MLNS = C.iID_MLNS_Cha
	)

	SELECT DISTINCT * into #mlnsPhanBo from C;

	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa,
			CASE
				WHEN (sNG is null OR sNG = '') THEN cast(1 as bit)
			ELSE cast(0 as bit)
			END AS bHangCha	
			into #tblMlnsByPhanCap
	FROM #mlnsPhanBo 
	WHERE 
		sLNS IN (SELECT * FROM #tempLNS)

	-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @YearOfWork 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = '')) OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

	-- map bảng mlnsPhanCap và đơn vị
	SELECT * INTO #tblMLNS FROM (
		SELECT *
		FROM #tblMlnsByPhanCapExistDonVi tbl
		WHERE sTM !='0'
		UNION ALL
		SELECT *
		FROM #tblMlnsByPhanCapNotDonVi 
	) mlns

	-- map bảng mlns ns và đơn vị
	SELECT * INTO #tblMlnsRoot FROM (
		SELECT * FROM #tblNsMlns, #tblDonVi 
		WHERE bHangCha = 0
		UNION ALL
		SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		FROM #tblNsMlns 
		WHERE bHangCha = 1
	) mlns

	IF @CountIndex=0
	BEGIN
	-- lấy dữ liệu đã cấp
	SELECT  
		0 AS fTien_DuToanGiaoNamNay,
		SUM(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang,
		SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
	    CTCT.iID_MucLucNganSach
		into #tblDataDaCap
	FROM
	BH_QTC_Quy_KPK CT
	INNER JOIN BH_QTC_Quy_KPK_ChiTiet CTCT
	ON CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.ID_QTC_Quy_KPK=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu<@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach

	--SELECT * into #tempAgency from  f_split(N'001'); 


	--SELECT 0 AS fTienDuToanDuocGiao,
	--	  SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
	--	  SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
	--	  SUM(fTienThucChi) AS fTienThucChi,
	--	  SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
 --         --iID_MaDonVi,
 --         --iID_MucLucNganSach
	--	  --into #tblDataDaCap
	--FROM BH_QTC_Quy_KinhPhiQuanLy_chiTiet
	--WHERE iID_QTC_Quy_KinhPhiQuanLy IN
 --      (
	--	SELECT iID_QTC_Quy_KinhPhiQuanLy
 --       FROM BH_QTC_Quy_KinhPhiQuanLy
 --       WHERE iNamChungTu = 2023
	--	  --AND i = @iID_LoaiDanhMucChi
 --         AND CAST(dNgayChungTu AS DATE) <= CAST('09-20-2023' AS DATE)
	--	  AND EXISTS(SELECT * FROM #tempAgency INTERSECT SELECT * FROM f_split(N'001'))
	--	  AND iID_QTC_Quy_KinhPhiQuanLy='4033EB9C-ECDE-4D24-A41C-FB5B303B5883'
	--	  AND iID_MaDonVi IN (SELECT * FROM  f_split(N'001'))
	--	)
		
	--GROUP BY iID_MucLucNganSach
	SELECT * into #tblDataDuToan FROM #tblPhanBoDuToan

	SELECT sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, sum(fTien_TongDuToanDuocGiao) fTien_TongDuToanDuocGiao
	, SUM(fTienThucChi) AS fTienThucChi
	,SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	,SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
	,iID_MaDonVi
	, iID_MucLucNganSach into #tblDataDaCapDuToan FROM (
		SELECT * FROM #tblDataDaCap
		UNION ALL
		SELECT * FROM #tblDataDuToan
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTien_DuToanGiaoNamNay,
		   fTien_DuToanNamTruocChuyenSang,
		   fTien_TongDuToanDuocGiao,
		   fTienThucChi,
		   fTienQuyetToanDaDuyet,
		   fTienDeNghiQuyetToanQuyNay,
		   fTienXacNhanQuyetToanQuyNay,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToan
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToan daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi, sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang, SUM(fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao,SUM(fTienThucChi) as fTienThucChi,SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResult FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTien_DuToanGiaoNamNay,
			   T.fTien_DuToanNamTruocChuyenSang,
			   T.fTien_TongDuToanDuocGiao,
			   T.fTienThucChi,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienXacNhanQuyetToanQuyNay
		FROM #tblDaCapDuToan T 
		WHERE T.fTien_DuToanGiaoNamNay <> 0 OR  T.fTien_DuToanNamTruocChuyenSang <> 0 OR  T.fTien_TongDuToanDuocGiao <> 0 OR  T.fTienThucChi <> 0 OR  T.fTienQuyetToanDaDuyet <> 0 OR  T.fTienDeNghiQuyetToanQuyNay <> 0 OR  T.fTienXacNhanQuyetToanQuyNay <> 0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.ID_QTC_Quy_KPK_ChiTiet, @iD) AS ID_QTC_Quy_KPK_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KPK,
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
		mlnsPhanBo.sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as fTien_DuToanGiaoNamNay,
		isnull(daCapDuToan.fTien_DuToanNamTruocChuyenSang, 0) as fTien_DuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_TongDuToanDuocGiao, 0) as fTien_TongDuToanDuocGiao,
		isnull(daCapDuToan.fTienThucChi, 0) as fTienThucChi,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,

		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KPK_ChiTiet
			WHERE 
		 		iID_QTC_Quy_KPK = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KPK ct ON ctct.iID_QTC_Quy_KPK = ct.ID_QTC_Quy_KPK
	LEFT JOIN #tblDaCapDuToanResult daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS -- and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
	ELSE 
		BEGIN
	-- lấy dữ liệu đã cấp
	SELECT  
		SUM(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay,
		SUM(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang,
		SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao,
		SUM(fTienThucChi) AS fTienThucChi,
		SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet,
		SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay,
		SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay,
		CT.iID_MaDonVi,
		CTCT.iID_MucLucNganSach
		into #tblDataDaCapExist
	FROM
	BH_QTC_Quy_KPK CT
	INNER JOIN BH_QTC_Quy_KPK_ChiTiet CTCT
	ON CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
	WHERE CT.iNamChungTu = @YearOfWork
		  --AND i = @iID_LoaiDanhMucChi
          AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.ID_QTC_Quy_KPK=@VoucherId
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		  AND CT.iQuyChungTu=@iQuyChungTu

	GROUP BY  CT.iID_MaDonVi,CTCT.iID_MucLucNganSach


	SELECT sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, SUM(fTien_TongDuToanDuocGiao) AS fTien_TongDuToanDuocGiao
	,SUM(fTienThucChi) AS fTienThucChi
	,SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	,SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	,SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay
	, iID_MaDonVi, iID_MucLucNganSach into #tblDataDaCapDuToanExist FROM (
		SELECT * FROM #tblDataDaCapExist
		) data
	GROUP BY iID_MaDonVi, iID_MucLucNganSach;
	--select * from  #tblMlnsRoot
	SELECT mlns.iID_MLNS,
		   mlns.iID_MLNS_Cha,
		   mlns.sXauNoiMa,
		   fTien_DuToanGiaoNamNay,
		   fTien_DuToanNamTruocChuyenSang,
		   fTien_TongDuToanDuocGiao,
		   fTienThucChi,
		   fTienQuyetToanDaDuyet,
		   fTienDeNghiQuyetToanQuyNay,
		   fTienXacNhanQuyetToanQuyNay,
		   isnull(mlns.iID_MaDonVi, daCapDuToan.iID_MaDonVi) as iID_MaDonVi
		   INTO #tblDaCapDuToanExist
	FROM #tblMlnsRoot mlns
	LEFT JOIN
	  #tblDataDaCapDuToanExist daCapDuToan
	ON mlns.iID_MLNS = daCapDuToan.iID_MucLucNganSach and ((mlns.iID_MaDonVi is not null and mlns.iID_MaDonVi = daCapDuToan.iID_MaDonVi) or mlns.iID_MaDonVi is null)

	SELECT iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	, sum(fTien_DuToanGiaoNamNay) AS fTien_DuToanGiaoNamNay
	, sum(fTien_DuToanNamTruocChuyenSang) AS fTien_DuToanNamTruocChuyenSang
	, SUM(fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao
	, SUM(fTienThucChi) AS fTienThucChi
	, SUM(fTienQuyetToanDaDuyet) AS fTienQuyetToanDaDuyet
	, SUM(fTienDeNghiQuyetToanQuyNay) AS fTienDeNghiQuyetToanQuyNay
	, SUM(fTienXacNhanQuyetToanQuyNay) AS fTienXacNhanQuyetToanQuyNay INTO #tblDaCapDuToanResultExist FROM (
		SELECT distinct T.iID_MLNS,  
			   T.iID_MLNS_Cha,
			   --isnull(T.iID_MaDonVi, T.iID_MaDonVi) as iID_MaDonVi,
			   T.iID_MaDonVi,
			   T.fTien_DuToanGiaoNamNay,
			   T.fTien_DuToanNamTruocChuyenSang,
			   T.fTien_TongDuToanDuocGiao,
			   T.fTienThucChi,
			   T.fTienQuyetToanDaDuyet,
			   T.fTienDeNghiQuyetToanQuyNay,
			   T.fTienXacNhanQuyetToanQuyNay
		FROM #tblDaCapDuToanExist T 
		WHERE  T.fTien_DuToanGiaoNamNay <> 0 OR T.fTien_DuToanNamTruocChuyenSang<>0 
				OR   T.fTien_TongDuToanDuocGiao<>0 OR T.fTienThucChi <>0 
				OR T.fTienQuyetToanDaDuyet <>0 OR T.fTienDeNghiQuyetToanQuyNay <>0 
				OR T.fTienXacNhanQuyetToanQuyNay<>0) data
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	OPTION (maxrecursion 0)

	-- Get data
	SELECT
		isnull(ctct.ID_QTC_Quy_KPK_ChiTiet, @iD) AS ID_QTC_Quy_KPK_ChiTiet,
		@VoucherId AS iID_QTC_Quy_KPK,
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
		mlnsPhanBo.sDuToanChiTietToi,
		@YearOfWork AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as fTien_DuToanGiaoNamNay,
		isnull(daCapDuToan.fTien_DuToanNamTruocChuyenSang, 0) as fTien_DuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_TongDuToanDuocGiao, 0) as fTien_TongDuToanDuocGiao,
		isnull(daCapDuToan.fTienThucChi, 0) as fTienThucChi,
		isnull(daCapDuToan.fTienQuyetToanDaDuyet, 0) as fTienQuyetToanDaDuyet,
		isnull(daCapDuToan.fTienDeNghiQuyetToanQuyNay, 0) as fTienDeNghiQuyetToanQuyNay,
		isnull(daCapDuToan.fTienXacNhanQuyetToanQuyNay, 0) as fTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_Quy_KPK_chiTiet
			WHERE 
		 		iID_QTC_Quy_KPK = @VoucherId
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_Quy_KPK ct ON ctct.iID_QTC_Quy_KPK = ct.ID_QTC_Quy_KPK
	LEFT JOIN #tblDaCapDuToanResultExist daCapDuToan
	ON mlnsPhanBo.iID_MLNS = daCapDuToan.iID_MLNS --and daCapDuToan.iID_MaDonVi LIKE '%' + mlnsPhanBo.iID_MaDonVi + '%' 
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	END
END
;
;
;
;
;
;
GO
