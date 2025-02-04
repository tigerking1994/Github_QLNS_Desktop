/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]    Script Date: 6/24/2024 4:26:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 6/24/2024 4:26:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 6/25/2024 9:09:44 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 6/24/2024 4:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@SLN nvarchar(max),
@IsTongHop bit,
@IQuy int,
@Donvitinh int
as
begin
	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = sum(fSoThamDinh)
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @INamLamViec - 1 and iMa = 225 and  iID_MaDonVi IN (SELECT * FROM f_split(@IdMaDonVi))
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
			danhmuc.bHangChaDuToan
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 
		and iTrangThai=1
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.sXauNoiMa,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang) as FTienDuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay) as FTienDuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as FTienTongDuToanDuocGiao,
			sum(qtcn_ct.fTienThucChi) as FTienThucChi ,
			sum(qtcn_ct.fTienQuyetToanDaDuyet) as FTienQuyetToanDaDuyet ,
			sum(qtcn_ct.fTienDeNghiQuyetToanQuyNay) as FTienDeNghiQuyetToanQuyNay,
			sum(qtcn_ct.fTienXacNhanQuyetToanQuyNay) as FTienXacNhanQuyetToanQuyNay
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_KCB_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		and qtcn.iQuyChungTu = @IQuy
		group by qtcn_ct.sXauNoiMa,qtcn_ct.sNoiDung

   		-- lấy ra dữ liệu dự toán
	SELECT 
		  ROUND(SUM(fTienTuChi),0) AS FTienDuToanGiaoNamNay,
		  sXauNoiMa
		  into #tblPhanBoDuToan
   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
   WHERE iID_DTC_PhanBoDuToanChi IN
       (SELECT ID
        FROM BH_DTC_PhanBoDuToanChi
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamChungTu = @INamLamViec
		  AND bIsKhoa=1
          --AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date)) 
		  )
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
   GROUP BY sXauNoiMa
   --- Get nhan phan bo tren giao
   	SELECT 
		  ROUND(SUM(fTienTuChi),0) AS FTienDuToanGiaoNamNay,
		  sXauNoiMa
		  into #tblNhanPhanBoTrenGiao
   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
   WHERE iID_DTC_DuToanChiTrenGiao IN
       (SELECT ID
        FROM BH_DTC_DuToanChiTrenGiao
        WHERE sSoQuyetDinh <> ''
          AND sSoQuyetDinh IS NOT NULL
          AND iNamLamViec = @INamLamViec
		  AND bIsKhoa=1
          --AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date)) 
		  )
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
   GROUP BY sXauNoiMa

   SELECT SUM(CTCT.fTien_DuToanNamTruocChuyenSang)  fTien_DuToanNamTruocChuyenSang,
					CTCT.sXauNoiMa
		INTO #TempChungTuQuyTruoc
	FROM BH_QTC_QUY_KCB_Chitiet CTCT
		WHERE CTCT.iID_QTC_Quy_KCB IN
	(
		SELECT ID_QTC_Quy_KCB FROM  BH_QTC_QUY_KCB 
		WHERE iID_MaDonVi IN ( SELECT * FROM  f_split(@IdMaDonVi))
					AND iNamChungTu=@INamLamViec
					AND iQuyChungTu=1
	)
	group by CTCT.sXauNoiMa

	-- lay du lieu da quyet toan 
	SELECT  
		SUM(FTienDeNghiQuyetToanQuyNay) AS fTienQuyetToanDaDuyet,
		CTCT.sXauNoiMa
		into #TemptblTienDaDuyet
	FROM
	BH_QTC_QUY_KCB CT
	INNER JOIN BH_QTC_QUY_KCB_Chitiet CTCT
	ON CT.ID_QTC_Quy_KCB=CTCT.iID_QTC_Quy_KCB
	WHERE CT.iNamChungTu = @INamLamViec
		  --AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@IdMaDonVi))
		  --AND CT.bIsKhoa=1
		  AND CT.iQuyChungTu<@IQuy
	GROUP BY  CTCT.sXauNoiMa

   -- chung tu thuong
		if @IsTongHop=1
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
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sNoiDung,
			CASE WHEN mucluc.sXauNoiMa = @SLN THEN ROUND(@fSoThamDinh/@Donvitinh,0)  ELSE 0 END as FTienDuToanNamTruocChuyenSang,
			ROUND(dt.FTienDuToanGiaoNamNay /@Donvitinh,0) FTienDuToanGiaoNamNay,
			ROUND(chi_tiet.FTienTongDuToanDuocGiao /@Donvitinh,0) FTienTongDuToanDuocGiao,
			ROUND((isnull(chi_tiet.fTienThucChi,0) + isnull(tienDuyet.fTienQuyetToanDaDuyet,0)) /@Donvitinh,0) as fTienThucChi,
			ROUND(chi_tiet.fTienQuyetToanDaDuyet /@Donvitinh,0)  fTienQuyetToanDaDuyet,
			ROUND(chi_tiet.fTienDeNghiQuyetToanQuyNay/ @Donvitinh,0) fTienDeNghiQuyetToanQuyNay,
			ROUND(chi_tiet.fTienXacNhanQuyetToanQuyNay/ @Donvitinh,0) fTienXacNhanQuyetToanQuyNay
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		left join #tblPhanBoDuToan as dt on mucluc.sXauNoiMa=dt.sXauNoiMa
		left join #TempChungTuQuyTruoc as quy_truoc on mucluc.sXauNoiMa=quy_truoc.sXauNoiMa
		left join #TemptblTienDaDuyet tienDuyet on mucluc.sXauNoiMa=tienDuyet.sXauNoiMa
		order by mucluc.sXauNoiMa
	else
		---- chung tu tong hop
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
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sNoiDung,
			CASE WHEN mucluc.sXauNoiMa = @SLN THEN ROUND(@fSoThamDinh/@Donvitinh,0)  ELSE 0 END as FTienDuToanNamTruocChuyenSang,
			--quy_truoc.fTien_DuToanNamTruocChuyenSang as FTienDuToanNamTruocChuyenSang,
			ROUND(dt.FTienDuToanGiaoNamNay/@Donvitinh,0) FTienDuToanGiaoNamNay,
			ROUND(chi_tiet.FTienTongDuToanDuocGiao /@Donvitinh,0) FTienTongDuToanDuocGiao,
			ROUND((isnull(chi_tiet.fTienThucChi,0) + isnull(tienDuyet.fTienQuyetToanDaDuyet,0))/@Donvitinh,0) fTienThucChi,
			ROUND(chi_tiet.fTienQuyetToanDaDuyet/@Donvitinh,0) fTienQuyetToanDaDuyet,
			ROUND(chi_tiet.fTienDeNghiQuyetToanQuyNay/@Donvitinh,0) fTienDeNghiQuyetToanQuyNay,
			ROUND(chi_tiet.fTienXacNhanQuyetToanQuyNay/@Donvitinh,0) fTienXacNhanQuyetToanQuyNay
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		left join #tblNhanPhanBoTrenGiao as dt on mucluc.sXauNoiMa=dt.sXauNoiMa
		left join #TempChungTuQuyTruoc as quy_truoc on mucluc.sXauNoiMa=quy_truoc.sXauNoiMa
		left join #TemptblTienDaDuyet tienDuyet on mucluc.sXauNoiMa=tienDuyet.sXauNoiMa
		order by mucluc.sXauNoiMa
end
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]    Script Date: 6/24/2024 4:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@IQuy int,
@Donvitinh int
as
begin
	---Lấy thông tin chi tiết giai thich tro cap
	SELECT gt.* into #TBL_TroCapTaiNan 
			FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
			LEFT JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_ChungTu=ct.ID_QTC_Quy_CheDoBHXH
			LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
			WHERE (gt.sXauNoiMa like '9010001-010-011-0003%' or gt.sXauNoiMa like '9010002-010-011-0003%')
					AND gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy 
					AND dv.iNamLamViec=@INamLamViec
					AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 

		SELECT
			ROW_NUMBER() OVER(ORDER BY tbltctn.sTenCanBo ASC) AS STT
			,tbltctn.sTenCanBo
			,tbltctn.sTenPhanHo
			, tbltctn.sMaCapBac
			, tbltctn.sSoQuyetDinh
			, CONVERT(varchar, (tbltctn.dNgayQuyetDinh), 101) As SNgayQuyetDinh

			---- Chi giám định mức suy giảm KNLĐ (người)1
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) /@Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienGiamDinh
			
			---- Trợ cấp 1 lần (người)2
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0002-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTroCap1Lan


			--- - Chi hỗ Trợ phòng người (người)
			-- - Chi h.trợ chuyển đổi n.nghiệp (người)
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0007-00'  or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0008-00'  THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 END) FTienTCTP
			
			---- Trợ cấp hàng tháng (người)
			, (CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0003-00' THEN Sum(tbltctn.fSoTien)/ @Donvitinh ELSE 0 end )FTienTCHangThang
			
			--- - Trợ cấp phục hồi chức năng (người)--Trợ cấp phục vụ (người)
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0004-00' or tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0005-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END ) FTienTCPHCNvPV
			--- - Trợ cấp chết do TNLD. BNN (người)
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0006-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh  ELSE 0 END )FTienTCCDTNLD
			--- - DS, PHSK sau TNLĐ, BNN (người)
			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.iSoNgayHuong)  ELSE 0 END ) ISoNgayDSPHSK

			,( CASE WHEN tbltctn.sXauNoiMa = '9010001-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltctn.sXauNoiMa = '9010002-010-011-0003-0001-0009-00' THEN Sum(tbltctn.fSoTien) / @Donvitinh ELSE 0 END) FTienDSPHSK
		FROM #TBL_TroCapTaiNan tbltctn
		GROUP BY tbltctn.sTenCanBo,
			tbltctn.sTenPhanHo,
			tbltctn.sMaCapBac
			, tbltctn.sSoQuyetDinh
			, tbltctn.dNgayQuyetDinh
			,tbltctn.sXauNoiMa 
		

		DROP TABLE #TBL_TroCapTaiNan


end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qtc_nqlkp_kehoach_bh]    Script Date: 6/25/2024 9:09:44 AM ******/
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
	WHERE iNamLamViec = @NamLamViec - 1 and iMa = 240 and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi))
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
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]    Script Date: 7/1/2024 10:10:00 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]    Script Date: 7/1/2024 10:10:00 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]    Script Date: 7/1/2024 10:10:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị chứng từ quyết toán chi nam kinh phí khac

-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_namkinh_phikhac_chungtu_index_bh]
	@YearOfWork int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		 ct.ID_QTC_Nam_KPK
		, ct.iID_DonVi
		, ct.iID_MaDonVi
		, ct.sSoChungTu
		, ct.dNgayChungTu
		, ct.sSoQuyetDinh
		, ct.dNgayQuyetDinh
		, ct.bThucChiTheo4Quy
		, ct.iNamLamViec
		, ct.sMoTa
		, ct.dNgaySua
		, ct.dNgayTao
		, ct.sNguoiSua
		, ct.sNguoiTao
		, ct.sTongHop
		, ct.iID_TongHopID
		, ct.iLoaiTongHop
		, ct.bIsKhoa
		, ct.fTongTien_DuToanNamTruocChuyenSang
		, ct.fTongTien_DuToanGiaoNamNay
		, ct.fTongTien_TongDuToanDuocGiao
		, ct.fTongTien_ThucChi
		, ct.fTongTienThua
		, ct.fTongTienThieu
		, ct.fTiLeThucHienTrenDuToan
		, ct.iID_LoaiChi
		, ct.sDSLNS
		, ct.bDaTongHop
		, lc.sTenDanhMucLoaiChi
		, dv.sTenDonVi
		-- Tong dự toán todo
	FROM BH_QTC_Nam_KPK ct
	LEFT JOIN DonVi dv on ct.iID_DonVi=dv.iID_DonVi 
	and ct.iID_MaDonVi=dv.iID_MaDonVi 
	and dv.iNamLamViec=ct.iNamLamViec
	LEFT JOIN BH_DM_LoaiChi lc on lc.iID=ct.iID_LoaiChi
	WHERE ct.iNamLamViec=@YearOfWork 
	and dv.iTrangThai=1
	Order by lc.sMaLoaiChi,ct.sSoChungTu
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]    Script Date: 7/1/2024 10:10:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IQuy int,
	@Donvitinh int
AS
BEGIN
SELECT gt.* INTO #tblTroCap FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	WHERE (--- Du toan
			gt.sXauNoiMa like '9010001-010-011-0004%'
			or gt.sXauNoiMa like '9010001-010-011-0005%'
			or gt.sXauNoiMa like '9010001-010-011-0006%'
			or gt.sXauNoiMa like '9010001-010-011-0007%'
			or gt.sXauNoiMa like '9010001-010-011-0008%'
			--- Hoach toan
			or gt.sXauNoiMa like '9010002-010-011-0004%'
			or gt.sXauNoiMa like '9010002-010-011-0005%'
			or gt.sXauNoiMa like '9010002-010-011-0006%'
			or gt.sXauNoiMa like '9010002-010-011-0007%'
			or gt.sXauNoiMa like '9010002-010-011-0008%')
		and gt.iNamLamViec = @INamLamViec and gt.iQuy = @IQuy and gt.iiD_MaDonVi in (SELECT * FROM f_split(@IdMaDonVi))

	--- 9010001-010-011-0004 Tro cap Huu tri
	SELECT * INTO #tempHuuTri FROM
	(
	SELECT 1 bHangCha
			, N'I' STT
			, N'TC Hưu Trí'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0001-0' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0004-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, 0 as FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0004%' or tbltc.sXauNoiMa like '9010002-010-011-0004%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	) tblHuutri

	 if (SELECT COUNT(1) FROM #tempHuuTri) > 2
		UPDATE #tempHuuTri set bHasData = 1

	--- 9010001-010-011-0005 Tro cap phuc vien
	SELECT * INTO #tempPhucVien FROM
	(
	SELECT 1 bHangCha
			, N'II' STT
			, N'TC Phục viên'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0001-000' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, 0 as FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0005%' or tbltc.sXauNoiMa like '9010002-010-011-0005%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac	
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	) tblPhucVien

	 if (SELECT COUNT(1) FROM #tempPhucVien) > 2
		UPDATE #tempPhucVien SET bHasData = 1

	--- 9010001-010-011-0006 Tro cap xuat ngu
	SELECT * INTO #tempXuatNgu FROM
	(
	SELECT 1 bHangCha
			, N'III' STT
			, N'TC Xuất ngũ'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac		
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sSoQuyetDinh) AS VARCHAR(6)) STT
			, Convert(varchar(10),(COUNT(tbltc.sSoQuyetDinh))) + N' Đồng chi' as sTenCanBo
			, tbltc.sTenPhanHo
			, '' as SMaCapBac		
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0006-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCap1Lan
			--, 0 AS  FTienTroCap1Lan
			, 0 AS  FTienTroCapKV
			, 0 AS  FTienTroCapMT
			--, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0005-0001-0002-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
			, 0 as FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0006%' or tbltc.sXauNoiMa like '9010002-010-011-00065%'
	GROUP BY 
			 tbltc.sTenPhanHo
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	) tblXuatNgu

	 if (SELECT COUNT(1) from #tempXuatNgu) > 2
		UPDATE #tempXuatNgu set bHasData = 1

	--- 9010001-010-011-0007 tro cap thoi viec
	SELECT * INTO #tempThoiViec FROM
	(
	SELECT 1 bHangCha
			, N'IV' STT
			, N'TC Thôi việc'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0007-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, 0 AS FTienTroCapMT
			, 0 as FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0007%' or tbltc.sXauNoiMa like '9010002-010-011-0007%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	) tblThoiViec

	 if (SELECT COUNT(1) FROM #tempThoiViec) > 2
		UPDATE #tempThoiViec SET bHasData = 1

	--- 9010001-010-011-0008 tro cap tu tuat
	SELECT * INTO #tempTuTuat FROM
	(
	SELECT 1 bHangCha
			, N'IV' STT
			, N'TC Tử tuất'as STenCanBo 
			, '' as STenPhanHo
			, '' as SMaCapBac 
			, null as SSoQuyetDinh
			, null as  DNgayQuyetDinh
			, null as FTienTroCap1Lan
			, null as FTienTroCapKV
			, null as FTienTroCapMT
			, null as FTienTroCap1LanTL
			, null as FTienTroCapKVTL
			, null as FTienTroCapMTTL
			, 0 bHasData
	UNION ALL
	SELECT  0 bHangCha 
			, CAST(ROW_NUMBER() OVER (ORDER BY tbltc.sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0001-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCap1Lan
			,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0002-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  FTienTroCapKV
			, (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END 
			+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0008-0001-0003-00' THEN Sum(tbltc.fSoTien) / @Donvitinh ELSE 0 END)  AS FTienTroCapMT
			, 0 as FTienTroCap1LanTL
			, 0 as FTienTroCapKVTL
			, 0 as FTienTroCapMTTL
			, 0 bHasData
			--,  (CASE WHEN tbltc.sXauNoiMa = '9010001-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) /1 ELSE 0 END 
			--+ CASE WHEN tbltc.sXauNoiMa = '9010002-010-011-0003-0001-0001-00' THEN Sum(tbltc.fSoTien) / 1 ELSE 0 END)  FTienTroCapMT
	FROM  #tblTroCap tbltc
	WHERE tbltc.sXauNoiMa like '9010001-010-011-0008%' or tbltc.sXauNoiMa like '9010002-010-011-0008%'
	GROUP BY  tbltc.sMa_Hieu_Can_Bo
			, tbltc.sTenCanBo
			, tbltc.sTenPhanHo
			, tbltc.sMaCapBac
			, tbltc.sSoQuyetDinh
			, tbltc.dNgayQuyetDinh
			, tbltc.sXauNoiMa
	) tblTuTuat

	 if (SELECT COUNT(1) FROM #tempTuTuat) > 2
		UPDATE #tempTuTuat SET bHasData = 1

		-- ket qua
	SELECT * INTO #tempRESULT  
	FROM
	(
		SELECT * FROM #tempHuuTri
		UNION ALL 
		SELECT * FROM #tempPhucVien
		UNION ALL 
		SELECT * FROM #tempXuatNgu
		UNION ALL
		SELECT * FROM #tempThoiViec
		UNION ALL
		SELECT * FROM #tempTuTuat
	) TBLRESULT

	SELECT * FROM #tempRESULT

	 DROP TABLE #tempHuuTri
	 DROP TABLE #tempPhucVien
	 DROP TABLE #tempXuatNgu
	 DROP TABLE #tempThoiViec
	 DROP TABLE #tempTuTuat
	 DROP TABLE #tempRESULT

END
;
;
;
;
;
;
;
GO



UPDATE BH_DTC_PhanBoDuToanChi
set sSoChungTu=STUFF(sSoChungTu,1,2,'PB')
where CHARINDEX('CT',sSoChungTu)>0