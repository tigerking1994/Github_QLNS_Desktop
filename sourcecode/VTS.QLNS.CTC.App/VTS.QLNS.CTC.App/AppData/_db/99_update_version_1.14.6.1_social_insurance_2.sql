
update BH_DM_LoaiChi
set sLNS='901,9010001,9010002'
where sMaLoaiChi='01'

/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_data_summary]    Script Date: 6/11/2024 8:18:22 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]    Script Date: 6/11/2024 4:56:44 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]    Script Date: 6/11/2024 4:56:44 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]
GO

/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]    Script Date: 6/11/2024 6:39:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]    Script Date: 6/11/2024 6:39:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]    Script Date: 6/12/2024 10:24:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_data_summary]    Script Date: 6/11/2024 8:18:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX),
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Quy_CheDoBHXH_ChiTiet(
		ID_QTC_Quy_CheDoBHXH_ChiTiet,
		iID_QTC_Quy_CheDoBHXH,
		iID_MucLucNganSach,
		sLoaiTroCap,
		dNgaySua,
		dNgayTao,
		sNguoiSua,
		sNguoiTao,
		fTienDuToanDuyet,
		iSoLuyKeCuoiQuyNay,
		fTienLuyKeCuoiQuyNay,
		iSoSQ_DeNghi,
		fTienSQ_DeNghi,
		iSoQNCN_DeNghi,
		fTienQNCN_DeNghi,
		iSoCNVCQP_DeNghi,
		fTienCNVCQP_DeNghi,
		iSoHSQBS_DeNghi,
		fTienHSQBS_DeNghi,
		iSoLDHD_DeNghi,
		fTienLDHD_DeNghi,
		iTongSo_DeNghi,
		fTongTien_DeNghi,
		fTongTien_PheDuyet,
		iNamLamViec,
		sXauNoiMa,
		iIDMaDonVi
	)
SELECT 
	   NEWID(),
	   @IdChungTuSummary,
       iID_MucLucNganSach,
       sLoaiTroCap,
	   GETDATE(),
	   GETDATE(),
	   @NguoiTao,
	   '',
	   SUM(fTienDuToanDuyet),
	   SUM(iSoLuyKeCuoiQuyNay),
	   SUM(fTienLuyKeCuoiQuyNay),
	   SUM(iSoSQ_DeNghi),
	   SUM(fTienSQ_DeNghi),
	   SUM(iSoQNCN_DeNghi),
	   SUM(fTienQNCN_DeNghi),
	   SUM(iSoCNVCQP_DeNghi),
	   SUM(fTienCNVCQP_DeNghi),
	   SUM(iSoHSQBS_DeNghi),
	   SUM(fTienHSQBS_DeNghi),
	   SUM(iSoLDHD_DeNghi),
	   SUM(fTienLDHD_DeNghi),
	   SUM(iTongSo_DeNghi),
	   SUM(fTongTien_DeNghi),
	   SUM(fTongTien_PheDuyet),
	   @YearOfWork,
	   sXauNoiMa,
	   @MaDonVi

FROM BH_QTC_Quy_CheDoBHXH_ChiTiet
WHERE  iID_QTC_Quy_CheDoBHXH IN
    (SELECT *
     FROM f_split(@IdChungTu))
group by iID_MucLucNganSach, sLoaiTroCap,sXauNoiMa

UPDATE BH_QTC_Quy_CheDoBHXH SET iLoaiTongHop=2 , bDaTongHop=1  WHERE ID_QTC_Quy_CheDoBHXH IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]    Script Date: 6/11/2024 4:56:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]
@IdChungTu uniqueidentifier,
@IdMaDonVi nvarchar(50),
@INamLamViec int,
@User  nvarchar(50),
@IsTongHop bit
as
begin
insert into BH_QTC_Nam_CheDoBHXH_ChiTiet (
  ID_QTC_Nam_CheDoBHXH_ChiTiet, iID_QTC_Nam_CheDoBHXH, 
  iID_MucLucNganSach, sLoaiTroCap, 
  sXauNoiMa, iID_MaDonVi, iNamLamViec, 
  dNgaySua, dNgayTao, sNguoiSua, sNguoiTao, 
  fTienDuToanDuyet, iSoSQ_ThucChi, 
  fTienSQ_ThucChi, iSoQNCN_ThucChi, 
  fTienQNCN_ThucChi, iSoCNVCQP_ThucChi, 
  fTienCNVCQP_ThucChi, iSoHSQBS_ThucChi, 
  fTienHSQBS_ThucChi, iSoLDHD_ThucChi, 
  fTienLDHD_ThucChi, iTongSo_ThucChi, 
  fTongTien_ThucChi
) 
select 
  NEWID(), 
  @IdChungTu, 
  tb_qtcy.iID_MucLucNganSach, 
  tb_qtcy.sLoaiTroCap, 
  tb_qtcy.sXauNoiMa, 
  tb_qtcy.iIDMaDonVi, 
  tb_qtcy.iNamLamViec, 
  null, 
  null, 
  null, 
  @User, 
  tb_qtcy.fTienDuToanDuyet, 
  tb_qtcy.iSoSQ_ThucChi, 
  tb_qtcy.fTienSQ_ThucChi, 
  tb_qtcy.iSoQNCN_ThucChi, 
  tb_qtcy.fTienQNCN_ThucChi, 
  tb_qtcy.iSoCNVCQP_ThucChi, 
  tb_qtcy.fTienCNVCQP_ThucChi, 
  tb_qtcy.iSoHSQBS_ThucChi, 
  tb_qtcy.fTienHSQBS_ThucChi, 
  tb_qtcy.iSoLDHD_ThucChi, 
  tb_qtcy.fTienLDHD_ThucChi, 
  tb_qtcy.iTongSo_ThucChi, 
  tb_qtcy.fTongTien_ThucChi 
from 
  (
    select 
      ctqt_quy_chitiet.iID_MucLucNganSach, 
      ctqt_quy_chitiet.sLoaiTroCap, 
      ctqt_quy_chitiet.sXauNoiMa, 
      ctqt_quy_chitiet.iNamLamViec, 
      ctqt_quy_chitiet.iIDMaDonVi, 
      null as fTienDuToanDuyet, 
      null as iSoSQ_ThucChi, 
      null as fTienSQ_ThucChi, 
      null as iSoQNCN_ThucChi, 
      null as fTienQNCN_ThucChi, 
      null as iSoCNVCQP_ThucChi, 
      null as fTienCNVCQP_ThucChi, 
      null as iSoHSQBS_ThucChi, 
      null as fTienHSQBS_ThucChi, 
      null as iSoLDHD_ThucChi, 
      null as fTienLDHD_ThucChi, 
      null as iTongSo_ThucChi, 
      SUM(
        ctqt_quy_chitiet.fTongTien_DeNghi
      ) as fTongTien_ThucChi 
    from 
      BH_QTC_Quy_CheDoBHXH as ctqt_quy 
      inner join BH_QTC_Quy_CheDoBHXH_ChiTiet as ctqt_quy_chitiet on ctqt_quy.ID_QTC_Quy_CheDoBHXH = ctqt_quy_chitiet.iID_QTC_Quy_CheDoBHXH 
    where 
      ctqt_quy.iID_MaDonVi = @IdMaDonVi 
      and ctqt_quy.iNamChungTu = @INamLamViec 
      and ctqt_quy_chitiet.fTongTien_DeNghi > 0 --and ((@IsTongHop = 0 and ctqt_quy.bDaTongHop = 0 and ctqt_quy.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  ctqt_quy.sDSSoChungTuTongHop is not null))
    group by 
      ctqt_quy_chitiet.iID_MucLucNganSach, 
      ctqt_quy_chitiet.sLoaiTroCap, 
      ctqt_quy_chitiet.sXauNoiMa, 
      ctqt_quy_chitiet.iNamLamViec, 
      ctqt_quy_chitiet.iIDMaDonVi
  ) as tb_qtcy

end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]    Script Date: 6/11/2024 4:56:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]
@INamLamViec int,
@SMaLoaiChi nvarchar(50),
@SMaDonVi nvarchar(50),
@SLNS nvarchar(50),
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
				and danhmuc.bHangChaDuToan is not null
		
	
	-- lay dữ liệu bên phân bổ chi
		select 
			pbdt_ct.sXauNoiMa,
			pbdt_ct.iID_MaDonVi,
			Sum(pbdt_ct.fTienTuChi) as fTienDuToanDuyet
		into #tblQuyetToanPhanBoChiTiet
		from BH_DTC_PhanBoDuToanChi_ChiTiet as pbdt_ct
		where pbdt_ct.iID_DTC_PhanBoDuToanChi in
		( SELECT ID FROM  BH_DTC_PhanBoDuToanChi
					WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamChungTu = @INamLamViec
				  AND bIsKhoa=1)
				  --AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date))
				AND iID_MaDonVi in  (SELECT * FROM splitstring(@SMaDonVi))
				And pbdt_ct.sMaLoaiChi=@SMaLoaiChi
			group by pbdt_ct.sXauNoiMa,
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
		mucluc.sDuToanChiTietToi,
		mucluc.iID_MLNS as iID_MucLucNganSach,
		mucluc.sMoTa as sLoaiTroCap,

		chi_tiet.iID_MaDonVi,
		chi_tiet.fTienDuToanDuyet

	from #tblMucLucNganSach as mucluc
	left join #tblQuyetToanPhanBoChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
	order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanPhanBoChiTiet;
end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]    Script Date: 6/11/2024 6:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy]
@IdChungTu uniqueidentifier,
@IdMaDonVi nvarchar(max),
@INamLamViec int,
@User  nvarchar(50)
as
begin
INSERT INTO BH_QTC_Nam_KinhPhiQuanLy_ChiTiet
(
	ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
	iID_QTC_Nam_KinhPhiQuanLy,
	iID_MucLucNganSach,
	sM,
	sTM,
	sNoiDung,
	sXauNoiMa,
	iID_MaDonVi,
	iNamLamViec,
	dNgaySua,
	dNgayTao,
	sNguoiSua,
	sNguoiTao,
	fTien_ThucChi
)
SELECT 
	NEWID(),
	@IdChungTu,
	tb_qtcy.iID_MucLucNganSach,
	tb_qtcy.sM,
	tb_qtcy.sTM,
	tb_qtcy.sNoiDung,
	tb_qtcy.sXauNoiMa,
	tb_qtcy.iIDMaDonVi,
	tb_qtcy.iNamLamViec,
	NULL,
	GETDATE(),
	NULL,
	@user,
	tb_qtcy.fTienThucChi
	FROM 
	(
		SELECT 
			CTCT.iID_MucLucNganSach,
			CTCT.sM,
			CTCT.sTM,
			CTCT.sNoiDung,
			CTCT.sXauNoiMa,
			CTCT.iIDMaDonVi,
			CTCT.iNamLamViec,
			sum(CTCT.fTienDeNghiQuyetToanQuyNay) fTienThucChi
		FROM
		BH_QTC_Quy_KinhPhiQuanLy AS CT
		LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy_ChiTiet CTCT ON  CT.ID_QTC_Quy_KinhPhiQuanLy=CTCT.iID_QTC_Quy_KinhPhiQuanLy
		WHERE CT.iID_MaDonVi=@IdMaDonVi
		and CTCT.fTienDeNghiQuyetToanQuyNay>0
		AND CTCT.iNamLamViec=@INamLamViec
			AND CT.iNamChungTu=@INamLamViec
			GROUP BY CTCT.iID_MucLucNganSach,
			CTCT.sM,
			CTCT.sTM,
			CTCT.sNoiDung,
			CTCT.sXauNoiMa,
			CTCT.iIDMaDonVi,
			CTCT.iNamLamViec
	) as tb_qtcy;

end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]    Script Date: 6/11/2024 6:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet_clone]
	@IdChungTu uniqueidentifier,
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@SMaLoaiChi nvarchar(50),
	@IIdMaDonVi nvarchar(500),
	@DNgayChungTu datetime,
	@iQuyChungTu int,
	@INamLamViec int,
	@Loai int
AS
BEGIN
	DECLARE @iD uniqueidentifier='00000000-0000-0000-0000-000000000000';
	DECLARE @CountIndex INT;
	SELECT * into #tempLNS from f_split(@SLNS);
	SELECT * into #tempAgency from  f_split(@IIdMaDonVi);
	SELECT @CountIndex = COUNT(*) FROM 
									BH_QTC_QUY_KCB_Chitiet 
									WHERE iID_QTC_Quy_KCB =@IdChungTu
	DECLARE @fSoThamDinh INT;
	SELECT @fSoThamDinh = fSoThamDinh
	FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
	WHERE iNamLamViec = @INamLamViec - 1 and iMa = 225  and iID_MaDonVi IN (SELECT * FROM f_split(@IIdMaDonVi))

	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha,sDuToanChiTietToi
			into #tblNsMlns
	FROM BH_DM_MucLucNganSach 
	where iNamLamViec = @INamLamViec 

		-- lấy ra dữ liệu dự toán
	SELECT 
		  SUM(fTienTuChi) AS fTien_DuToanGiaoNamNay,
          iID_MaDonVi,
          iID_MucLucNganSach,
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
          AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date))
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IIdMaDonVi))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach,
   sXauNoiMa

   	SELECT 
		  SUM(fTienTuChi) AS fTien_DuToanGiaoNamNay,
          iID_MaDonVi,
          iID_MucLucNganSach,
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
		  )
     AND iID_MaDonVi in  (SELECT * FROM f_split(@IIdMaDonVi))-- donvi
   GROUP BY iID_MaDonVi, 
   iID_MucLucNganSach,
   sXauNoiMa


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
		INamLamViec = @INamLamViec 
		AND iID_MaDonVi IN (SELECT * FROM #tempAgency)

	-- Tao bang tam luu chu mlng con
	SELECT * INTO #tblMlnsByPhanCapExistDonVi
	FROM #tblMlnsByPhanCap tbl, #tblDonVi dv
	WHERE  tbl.bHangCha = 0 OR (tbl.bHangCha = 1 and (IsNull(tbl.sM, '') != '' OR tbl.sM <> '' OR IsNull(tbl.sTM, '') != '')  )
	AND tbl.sTM !=''  


	-- Tao bang tam luu chu mlng cha
	SELECT *, '' AS iID_MaDonVi, '' AS sTenDonVi  INTO #tblMlnsByPhanCapNotDonVi
	FROM #tblMlnsByPhanCap 
		WHERE bHangCha = 1 AND ( (IsNull(sM, '') = '' or sM = '')) AND ( (IsNull(sTM, '') = '' or sTM = ''))
		OR ((ISNULL(sTM,'')='' OR sTM='')) OR sTM='0'

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
		--WHERE bHangCha = 0
		--UNION ALL
		--SELECT *, null AS iID_MaDonVi, null AS sTenDonVi  
		--FROM #tblNsMlns 
		--WHERE bHangCha = 1
	) mlns

	SELECT CTCT.* 
		INTO #TempChungTuQuyTruoc
	FROM BH_QTC_QUY_KCB_Chitiet CTCT
		WHERE CTCT.iID_QTC_Quy_KCB IN
	(
		SELECT ID_QTC_Quy_KCB FROM  BH_QTC_QUY_KCB 
		WHERE iID_MaDonVi IN ( SELECT * FROM  f_split(@IIdMaDonVi))
					AND iNamChungTu=@INamLamViec
					AND iQuyChungTu=1
	)

	-- lay du lieu da quyet toan 
	SELECT  
		SUM(FTienDeNghiQuyetToanQuyNay) AS fTienQuyetToanDaDuyet,
		CT.iID_MaDonVi,
		CTCT.sXauNoiMa
		into #TemptblTienDaDuyet
	FROM
	BH_QTC_QUY_KCB CT
	INNER JOIN BH_QTC_QUY_KCB_Chitiet CTCT
	ON CT.ID_QTC_Quy_KCB=CTCT.iID_QTC_Quy_KCB
	WHERE CT.iNamChungTu = @INamLamViec
		  --AND CAST(CT.dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)
		  AND CT.iID_MaDonVi IN (SELECT * FROM f_split(@IIdMaDonVi))
		  --AND CT.bIsKhoa=1
		  AND CT.iQuyChungTu<@iQuyChungTu
	GROUP BY  CT.iID_MaDonVi,CTCT.sXauNoiMa

	--- chung tu thuong
	if 	@Loai=1	
	-- Get data
	SELECT
		isnull(ctct.ID_QTC_QUY_KCB_Chitiet, @iD) AS ID_QTC_QUY_KCB_Chitiet,
		@IdChungTu AS iID_QTC_QUY_KCB,
		mlnsPhanBo.iID_MLNS,
		mlnsPhanBo.iID_MLNS_Cha,
		mlnsPhanBo.iID_MLNS as iID_MucLucNganSach,
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
		mlnsPhanBo.bHangCha ,
		mlnsPhanBo.sDuToanChiTietToi,
		@INamLamViec AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		CASE WHEN mlnsPhanBo.sXauNoiMa = @SLNS THEN @fSoThamDinh ELSE 0 END as FTienDuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as FTienDuToanGiaoNamNay,
		isnull(ctct.fTien_TongDuToanDuocGiao, 0) as FTienTongDuToanDuocGiao,
		isnull(ctct.fTienThucChi, 0) as FTienThucChi,
		isnull(tblDaDuyet.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_QUY_KCB_Chitiet
			WHERE 
		 		iID_QTC_QUY_KCB = @IdChungTu
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_QUY_KCB ct ON ctct.iID_QTC_QUY_KCB = ct.ID_QTC_QUY_KCB
	LEFT JOIN #tblPhanBoDuToan daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa
	LEFT JOIN #TempChungTuQuyTruoc tblQuyTruoc on mlnsPhanBo.sXauNoiMa=tblQuyTruoc.sXauNoiMa
	LEFT JOIN #TemptblTienDaDuyet tblDaDuyet on mlnsPhanBo.sXauNoiMa=tblDaDuyet.sXauNoiMa
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	------ Chứng từ tong hop
	ELSE 
	---- Get data
	SELECT
		isnull(ctct.ID_QTC_QUY_KCB_Chitiet, @iD) AS ID_QTC_QUY_KCB_Chitiet,
		@IdChungTu AS iID_QTC_QUY_KCB,
		mlnsPhanBo.iID_MLNS ,
		mlnsPhanBo.iID_MLNS_Cha ,
		mlnsPhanBo.iID_MLNS as iID_MucLucNganSach,
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
		mlnsPhanBo.bHangCha ,
		mlnsPhanBo.sDuToanChiTietToi,
		@INamLamViec AS INamLamViec,
		mlnsPhanBo.iID_MaDonVi AS IID_MaDonVi,
		mlnsPhanBo.sTenDonVi AS STenDonVi,
		CASE WHEN mlnsPhanBo.sXauNoiMa = @SLNS THEN @fSoThamDinh ELSE 0 END as FTienDuToanNamTruocChuyenSang,
		isnull(daCapDuToan.fTien_DuToanGiaoNamNay, 0) as FTienDuToanGiaoNamNay,
		isnull(ctct.fTien_TongDuToanDuocGiao, 0) as FTienTongDuToanDuocGiao,
		isnull(ctct.fTienThucChi, 0) as FTienThucChi,
		isnull(tblDaDuyet.fTienQuyetToanDaDuyet, 0) as FTienQuyetToanDaDuyet,
		isnull(ctct.fTienDeNghiQuyetToanQuyNay, 0) as FTienDeNghiQuyetToanQuyNay,
		isnull(ctct.fTienXacNhanQuyetToanQuyNay, 0) as FTienXacNhanQuyetToanQuyNay,
		isnull(ctct.dNgayTao, getdate()) AS DNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS DNgaySua,
		ctct.sNguoiTao AS SNguoiTao,
		ctct.sNguoiSua AS SNguoiSua
	FROM #tblMlnsRoot AS mlnsPhanBo
	LEFT JOIN
		(SELECT *
			FROM 
				BH_QTC_QUY_KCB_Chitiet
			WHERE 
		 		iID_QTC_QUY_KCB = @IdChungTu
		) ctct
	ON mlnsPhanBo.iID_MLNS = ctct.iID_MucLucNganSach --and mlnsPhanBo.iID_MaDonVi = ctct.iID_MaDonVi
	LEFT JOIN BH_QTC_QUY_KCB ct ON ctct.iID_QTC_QUY_KCB = ct.ID_QTC_QUY_KCB
	LEFT JOIN #tblNhanPhanBoTrenGiao daCapDuToan
	ON mlnsPhanBo.sXauNoiMa = daCapDuToan.sXauNoiMa 
	LEFT JOIN #TempChungTuQuyTruoc tblQuyTruoc on mlnsPhanBo.sXauNoiMa=tblQuyTruoc.sXauNoiMa
	LEFT JOIN #TemptblTienDaDuyet tblDaDuyet on mlnsPhanBo.sXauNoiMa=tblDaDuyet.sXauNoiMa
	WHERE mlnsPhanBo.sLNS IN (SELECT * FROM #tempLNS)
	ORDER BY mlnsPhanBo.sXauNoiMa, mlnsPhanBo.iID_MaDonVi;
	--END
END
;
;
;
;
;
;
;
;
;;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]    Script Date: 6/12/2024 10:24:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy]
@IdChungTu uniqueidentifier,
@IdMaDonVi nvarchar(max),
@INamLamViec int,
@User  nvarchar(50),
@IDLoaiCap uniqueidentifier
as
begin
INSERT INTO BH_QTC_Nam_KPK_ChiTiet
(
	ID_QTC_Nam_KPK_ChiTiet,
	iID_QTC_Nam_KPK,
	iID_MucLucNganSach,
	sNoiDung,
	sXauNoiMa,
	iID_MaDonVi,
	iNamLamViec,
	dNgaySua,
	dNgayTao,
	sNguoiSua,
	sNguoiTao,
	fTien_ThucChi
)
SELECT 
	NEWID(),
	@IdChungTu,
	tb_qtcy.iID_MucLucNganSach,
	tb_qtcy.sNoiDung,
	tb_qtcy.sXauNoiMa,
	tb_qtcy.iIDMaDonVi,
	tb_qtcy.iNamLamViec,
	NULL,
	GETDATE(),
	NULL,
	@User,
	tb_qtcy.fTienThucChi
	FROM 
	(
		SELECT 
			CTCT.iID_MucLucNganSach,
			CTCT.sNoiDung,
			CTCT.sXauNoiMa,
			CTCT.iIDMaDonVi,
			CTCT.iNamLamViec,
			ISNULL(CTCT.fTienDeNghiQuyetToanQuyNay, 0) fTienThucChi
		FROM
		BH_QTC_Quy_KPK AS CT
		LEFT JOIN BH_QTC_Quy_KPK_ChiTiet CTCT ON  CT.ID_QTC_Quy_KPK=CTCT.iID_QTC_Quy_KPK
		WHERE CT.iID_MaDonVi=@IdMaDonVi
			AND CTCT.iNamLamViec=@INamLamViec
			--AND CT.iQuyChungTu=4
			--AND CT.bIsKhoa=1
			AND CT.iNamChungTu=@INamLamViec
			AND  CT.iID_LoaiChi=@IDLoaiCap
	) as tb_qtcy;

end
;
;
GO
