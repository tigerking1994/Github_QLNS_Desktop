/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan_chitiet]    Script Date: 10/10/2024 4:12:48 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_thamdinhquyettoan_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_thamdinhquyettoan_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 10/10/2024 4:12:48 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 10/10/2024 4:12:48 PM ******/
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
		and danhmuc.iTrangThai = 1
		
		
		
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
		where qtcn.iID_MaDonVi in (select * from dbo.f_split(@IdMaDonVi)) 
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
			ROUND(tienDuyet.fTienQuyetToanDaDuyet /@Donvitinh,0)  fTienQuyetToanDaDuyet,
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
			ROUND(tienDuyet.fTienQuyetToanDaDuyet/@Donvitinh,0) fTienQuyetToanDaDuyet,
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan_chitiet]    Script Date: 10/10/2024 4:12:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_thamdinhquyettoan_chitiet]
 @IdChungTu uniqueidentifier,
@INamLamViec int ,
@IdDonVi nvarchar(max) 
AS
BEGIN
	
	declare @LoaiDonVi nvarchar(50) = (select iLoai from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi = @IdDonVi);

			-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @INamLamViec 
		AND iTrangThai=1
		AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))

	SELECT * INTO #dmtdqt
	FROM BH_DM_ThamDinhQuyetToan WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1
	ORDER BY iSTT

	SELECT 
		iID,
		ctct.iID_BH_TDQT_ChungTuChiTiet,
		dmtdqp.iMa,
		dmtdqp.iSTT,
		iMaCha,
		sSTT,
		sNoiDung,
		sXauNoiMa,
		iKieuChu,
		dmtdqp.iTrangThai,
		dmtdqp.iNamLamViec,
		dmtdqp.sNguoiTao, dmtdqp.sNguoiSua,
		CASE WHEN ctct.fSoBaoCao is null or ctct.fSoBaoCao = 0 THEN temp.fSoBaoCao
			 ELSE ctct.fSoBaoCao
		END AS fSoBaoCao,
		ctct.fSoThamDinh,
		CASE WHEN ctct.fQuanNhan is null or ctct.fQuanNhan = 0 THEN trocapnuoicon.fQuanNhan
			ELSE ctct.fQuanNhan 
		END AS fQuanNhan,
		CASE WHEN ctct.fCNVLDHD is null or ctct.fCNVLDHD = 0 THEN trocapnuoicon.fCNVLDHD
			ELSE ctct.fCNVLDHD 
		END AS fCNVLDHD,
		ctct.sGhiChu
		
	INTO #dmtdqtResult
	FROM #dmtdqt dmtdqp
	LEFT JOIN
	(SELECT 257 iMa, (ISNULL(SUM(ctct.fTienSQ_ThucChi), 0) + ISNULL(SUM(ctct.fTienQNCN_ThucChi), 0)) fQuanNhan,
		(ISNULL(SUM(ctct.fTienCNVCQP_ThucChi), 0) + ISNULL(SUM(ctct.fTienLDHD_ThucChi), 0)) fCNVLDHD
		FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
		LEFT JOIN BH_QTC_Nam_CheDoBHXH ct ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
		WHERE (ctct.sXauNoiMa LIKE '9010001-010-011-0002-0001-0001-00%' OR ctct.sXauNoiMa LIKE '9010002-010-011-0002-0001-0001-00%')
		AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ct.iNamLamViec = @INamLamViec
		AND ct.bIsKhoa = 1) trocapnuoicon ON dmtdqp.iMa = trocapnuoicon.iMa
	LEFT JOIN
	(
	SELECT 7 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2


	UNION

	SELECT 8 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION
	--phu nhan phu quan khoi du toan
	--SELECT 259 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	--LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	--ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa = '9020001-010-011-0001-0003'
	--AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ct.iNamLamViec = @INamLamViec
	--AND ct.bIsKhoa = 1
	SELECT 259 iMa, 0 as fSoBaoCao

	UNION

	SELECT 9 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 10 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 12 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 13 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 14 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 15 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 19 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 20 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 21 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2


	UNION

	SELECT 22 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 24 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 25 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 26 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 27 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 29 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 30 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 34 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 35 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2


UNION
 --phu nhan phu quan khoi hach toan
	--SELECT 260 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	--LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	--ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa = '9020002-010-011-0001-0003'
	--AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ct.iNamLamViec = @INamLamViec
	--AND ct.bIsKhoa = 1
	SELECT 260 iMa, 0 as fSoBaoCao

	UNION

	SELECT 36 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 37 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 39 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 40 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 41 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 42 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 46 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 47 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 48 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 49 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 51 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 52 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 53 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 54 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 56 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 57 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 70 iMa, SUM(ctct.fThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
--	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020001-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 71 iMa, SUM(ctct.fTruyThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020001-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	--AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iID_MaDonVi = '002'
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 73 iMa, SUM(ctct.fThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020001-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 74 iMa, SUM(ctct.fTruyThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020001-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 77 iMa, SUM(ctct.fThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020002-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 78 iMa, SUM(ctct.fTruyThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020002-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 80 iMa, SUM(ctct.fThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020002-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 81 iMa, SUM(ctct.fTruyThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	WHERE sXauNoiMa IN ('9020002-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2
	
	UNION

	SELECT 95 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 96 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 98 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 99 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 102 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 103 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 105 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 106 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 110 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 111 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 113 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 114 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 17 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 118 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 120 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 121 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 133 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 134 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 135 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 136 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 138 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 139 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 140 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	AND ct.iQuyNam = @INamLamViec
	AND ct.iQuyNamLoai = 2

	UNION

	SELECT 141 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 151 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa LIKE '9030001-010-011-0001%' OR sXauNoiMa LIKE '9030001-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 155 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa LIKE '9030002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 159 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 163 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 167 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030006'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 171 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030004'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 178 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 179 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 180 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	--WHERE (ctct.sXauNoiMa LIKE '9010001%' OR ctct.sXauNoiMa LIKE '9010002%')
	WHERE (ctct.sXauNoiMa LIKE '901%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 183 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 184 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 185 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 186 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 187 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 188 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 189 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 190 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0008%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 193 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 194 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 195 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 196 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 197 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 198 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 199 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 200 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0008%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 183 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	--UNION

	--SELECT 207 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(fTien_ThucChi), 0) FROM BH_QTC_Nam_KPK_ChiTiet WHERE sXauNoiMa like '9050001-010%' AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND iNamLamViec = @INamLamViec - 1) fSoBaoCao 
	--FROM BH_CP_ChungTu_ChiTiet ctct
	--LEFT JOIN BH_CP_ChungTu ct 
	--ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	--WHERE sXauNoiMa like '9050001%'
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ct.iNamChungTu = @INamLamViec - 1
	--AND ct.bIsKhoa = 1
	
	UNION

	SELECT 208 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 209 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	--UNION

	--SELECT 213 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(fTien_ThucChi), 0) FROM BH_QTC_Nam_KPK_ChiTiet WHERE sXauNoiMa LIKE '9050002-010%' AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND iNamLamViec = @INamLamViec - 1) fSoBaoCao 
	--FROM BH_CP_ChungTu_ChiTiet ctct
	--LEFT JOIN BH_CP_ChungTu ct 
	--ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	--WHERE sXauNoiMa LIKE '9050002%'
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ct.iNamChungTu = @INamLamViec - 1
	--AND ct.bIsKhoa = 1
	
	UNION

	SELECT 214 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 215 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa like '9050002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 218 iMa, 0.1 * (ISNULL(SUM(ctct.fBHYT_NLD), 0) + ISNULL(SUM(ctct.fBHYT_NSD), 0)) fSoBaoCao 
	FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	LEFT JOIN  BH_DTT_BHXH_PhanBo_ChungTu ct 
	ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	WHERE (ctct.sXauNoiMa like '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ct.iNamLamViec = @INamLamViec
		AND ct.bKhoa = 1

	UNION

	--SELECT 220 iMa, 0.1 * (ISNULL(SUM(ctct.fBHYT_NLD), 0) + ISNULL(SUM(ctct.fBHYT_NSD), 0) - (SELECT ISNULL(SUM(ctct.fThu_BHYT_NLD), 0) + ISNULL(SUM(ctct.fThu_BHYT_NSD), 0)
	--	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	--	JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%') 
	--		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
	--		AND ctct.iNamLamViec = @INamLamViec - 1
	--		AND ct.iQuyNam = @INamLamViec - 1
	--		AND ct.bIsKhoa = 1)) fSoBaoCao
	--FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	--JOIN BH_DTT_BHXH_PhanBo_ChungTu ct on ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	--WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ctct.iNamLamViec = @INamLamViec - 1
	--AND ct.bKhoa = 1

	--UNION

	SELECT 221 iMa, (ISNULL(SUM(ctct.fThu_BHYT_NLD), 0) + ISNULL(SUM(ctct.fThu_BHYT_NSD), 0)) * 0.1 fSoBaoCao
	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	JOIN BH_QTT_BHXH_ChungTu ct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa LIKE '9020002-010-011-0001%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 222 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010004%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 223 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KCB_QuanYDonVi ct 
	ON ct.ID_QTC_Nam_KCB_QuanYDonVi = ctct.iID_QTC_Nam_KCB_QuanYDonVi
	WHERE ctct.sXauNoiMa LIKE '9010004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 228 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct on ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010009-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE ctct.sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 229 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 230 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 231 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010009-010%' 
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 235 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct on ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010006-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN  BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 236 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 237 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 238 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 242 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010010%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 243 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010010-010%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 247 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct on ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 and sM is not null and sM <> '' AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 248 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 250 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct on ID_QTC_Nam_KinhPhiQuanLy = ID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 251 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 252 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KinhPhiQuanLy ct 
	ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 261 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0003-0001'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 262 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0003-0002'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 263 iMa, 0 fSoBaoCao 

	UNION

	SELECT 264 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0004-0001'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 265 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0004-0002'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 266 iMa, 0 fSoBaoCao 

	UNION

	SELECT 267 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0004-0001'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 268 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0004-0002'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 269 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020002-010-011-0001-0003-0001'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 270 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020002-010-011-0001-0003-0002'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 271 iMa, 0 fSoBaoCao 

	UNION

	SELECT 272 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0005-0001'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	UNION

	SELECT 273 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu 
	where ctct.sXauNoiMa = '9020001-010-011-0001-0005-0002'
	and ctct.iNamLamViec = @INamLamViec
	and ctct.iID_MaDonVi IN  (SELECT * FROM f_split(@IdDonVi))
	and ct.iQuyNamLoai = 2
	and ct.iQuyNam = @INamLamViec
	and ct.bIsKhoa = 1

	) AS temp ON temp.iMa = dmtdqp.iMa
	
	LEFT JOIN BH_ThamDinhQuyetToan_ChungTuChiTiet ctct ON dmtdqp.iMa = ctct.iMa 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec
	LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct ON ctct.iID_BH_TDQT_ChungTu = ct.iID_BH_TDQT_ChungTu 
	AND ct.iID_BH_TDQT_ChungTu = @IdChungTu AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ct.iNamLamViec = @INamLamViec
	LEFT JOIN (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi

	--ORDER BY iMa

	--Lấy dữ liệu chứng từ đơn vị cha
	SELECT dvc.iMa, dvc.fSoBaoCao INTO #dmtdqtCha FROM
	(
	SELECT 178 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 179 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010002%'
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ctct.iNamLamViec = @INamLamViec
		AND ct.bIsKhoa = 1

	UNION

	SELECT 218 iMa, 0.1 * (ISNULL(SUM(ctct.fThu_BHYT_NLD), 0) + ISNULL(SUM(ctct.fThu_BHYT_NSD), 0)) fSoBaoCao 
	FROM BH_DTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN  BH_DTT_BHXH_ChungTu ct 
	ON ct.iID_DTT_BHXH = ctct.iID_DTT_BHXH
	WHERE (ctct.sXauNoiMa like '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		AND ct.iNamLamViec = @INamLamViec
		AND ct.bIsKhoa = 1

	UNION

	--SELECT 220 iMa, 0.1 * (ISNULL(SUM(fThu_BHYT_NLD + fThu_BHYT_NSD), 0) - (SELECT ISNULL(SUM(ctct.fThu_BHYT_NLD + ctct.fThu_BHYT_NSD), 0) 
	--	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	--	JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	--	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%') 
	--		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
	--		AND ctct.iNamLamViec = @INamLamViec - 1
	--		AND ct.iQuyNam = @INamLamViec - 1
	--		AND ct.bIsKhoa = 1)) fSoBaoCao
	--FROM BH_DTT_BHXH_ChungTu_ChiTiet ctct
	--JOIN BH_DTT_BHXH_ChungTu ct ON ct.iID_DTT_BHXH = ctct.iID_DTT_BHXH
	--WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
	--AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	--AND ctct.iNamLamViec = @INamLamViec - 1
	--AND ct.bIsKhoa = 1 

	--UNION

	SELECT 228 iMa, ISNULL(SUM(fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010009-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 229 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	LEFT JOIN BH_DTC_DuToanChiTrenGiao ct 
	ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE 
	sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 235 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010006-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 236 iMa, SUM(ctct.fTienTuChi) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 247 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 and sM is not null and sM <> '' AND ct.BIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 248 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	) dvc

	IF @LoaiDonVi = 0
		UPDATE dmtdqtResult
		SET dmtdqtResult.fSoBaoCao = dmtdqtCha.fSoBaoCao
		FROM #dmtdqtResult AS dmtdqtResult
		JOIN #dmtdqtCha AS dmtdqtCha ON dmtdqtResult.iMa = dmtdqtCha.iMa;

	--Result
	SELECT #dmtdqtResult.*,
	CASE WHEN #dmtdqtResult.iKieuChu <> 1 THEN #tblDonVi.iID_MaDonVi  ELSE '' END as iID_MaDonVi,
	CASE WHEN #dmtdqtResult.iKieuChu <> 1 THEN #tblDonVi.sTenDonVi  ELSE '' END as sTenDonVi
	FROM #dmtdqtResult,#tblDonVi
	ORDER BY iSTT;

	DROP TABLE #dmtdqt;
	DROP TABLE #dmtdqtCha;
	DROP TABLE #dmtdqtResult;
	DROP TABLE #tblDonVi;
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
