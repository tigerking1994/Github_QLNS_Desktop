/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_dutoan_index]    Script Date: 1/2/2024 11:19:44 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_dutoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_dutoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]    Script Date: 1/2/2024 11:19:44 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chi_tiet]    Script Date: 1/2/2024 11:19:44 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]    Script Date: 1/2/2024 11:19:44 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]    Script Date: 1/2/2024 11:19:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]
@INamLamViec int,
@SMaLoaiChi nvarchar(50),
@IDLoaiChi nvarchar(max),
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
			danhmuc.bHangCha
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

		chi_tiet.iID_MaDonVi,
		chi_tiet.fTienDuToanDuyet
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
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanPhanBoChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chi_tiet]    Script Date: 1/2/2024 11:19:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100)
AS
BEGIN
SELECT 
		ct.iID_BH_DTC_ChiTiet ,
		ct.iID_BH_DTC ,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sM,
		ct.sTM,
		ct.sNoiDung,
		ct.sXauNoiMa,
		ct.iNamLamViec,
		ct.fTienDuToanDuocGiao,
		ct.fTienThucHien06ThangDauNam,
		ct.fTienUocThucHien06ThangCuoiNam,
		(isnull(ct.fTienThucHien06ThangDauNam,0) + isnull(ct.fTienUocThucHien06ThangCuoiNam,0)) fTienUocThucHienCaNam,
		(CASE  WHEN isnull(ct.fTienDuToanDuocGiao,0) > isnull(ct.fTienUocThucHienCaNam,0) THEN isnull(ct.fTienDuToanDuocGiao,0) - isnull(ct.fTienUocThucHienCaNam,0) ELSE 0 END ) as fTienSoSanhTang,
		(CASE  WHEN isnull(ct.fTienUocThucHienCaNam,0) > isnull(ct.fTienDuToanDuocGiao,0) THEN isnull(ct.fTienUocThucHienCaNam,0) - isnull(ct.fTienDuToanDuocGiao,0) ELSE 0 END ) as fTienSoSanhGiam,
		ct.sTenDonVi,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sGhiChu
	FROM
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi in (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]    Script Date: 1/2/2024 11:19:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  -- Create date: <Create Date,,>
  -- Description:  <Description,,>
  -- =============================================
  CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop] @ListIdChungTuTongHop ntext, 
  @Nguoitao nvarchar(50), 
  @IdChungTu nvarchar(100), 
  @NamLamViec int AS BEGIN INSERT INTO [dbo].BH_DTC_DieuChinhDuToanChi_ChiTiet (
    iID_BH_DTC_ChiTiet,
	iID_BH_DTC, 
    iID_MucLucNganSach,
	sM,
	sTM,
	sNoiDung,
	sXauNoiMa,
	iNamLamViec,
	iID_MaDonVi,
    fTienDuToanDuocGiao,
	fTienThucHien06ThangDauNam,
    fTienUocThucHien06ThangCuoiNam,
    fTienUocThucHienCaNam,
	fTienSoSanhTang, 
    fTienSoSanhGiam,
	dNgaySua,
	dNgayTao, 
    sNguoiSua,
	sNguoiTao
  ) 
SELECT 
  DISTINCT NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sM, 
  sTM, 
  sNoiDung,
  sXauNoiMa,
  iNamLamViec,
  iID_MaDonVi,
  sum(fTienDuToanDuocGiao), 
  sum(fTienThucHien06ThangDauNam), 
  sum(fTienUocThucHien06ThangCuoiNam), 
  sum(fTienUocThucHienCaNam), 
  sum(fTienSoSanhTang), 
  sum(fTienSoSanhGiam), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao 
FROM 
  BH_DTC_DieuChinhDuToanChi_ChiTiet 
WHERE 
  iID_BH_DTC in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  sM, 
  sTM, 
  iID_MucLucNganSach, 
  sNoiDung,
  sXauNoiMa,
  iNamLamViec,
  iID_MaDonVi;
--danh dau chung tu da tong hop
update 
  BH_DTC_DieuChinhDuToanChi 
set 
  bDaTongHop = 1 
where 
  iID_BH_DTC in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) END;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_dutoan_index]    Script Date: 1/2/2024 11:19:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị dự toán điều chỉnh dự toán

-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_dutoan_index]
@NamLamViec int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 dcdt.iID_BH_DTC 
		, dcdt.sSoChungTu
		, dcdt.dNgayChungTu
		, dcdt.sSoQuyetDinh
		, dcdt.dNgayQuyetDinh
		, dcdt.iNamLamViec
		, dcdt.iID_DonVi
		, dcdt.iID_MaDonVi
		, dcdt.sMoTa
		, dcdt.sLNS
		, dcdt.iID_LoaiCap
		, dcdt.fTienDuToanDuocGiao
		, dcdt.fTienThucHien06ThangDauNam
		, dcdt.fTienUocThucHien06ThangCuoiNam
		, dcdt.fTienUocThucHienCaNam
		, dcdt.fTienSoSanhTang
		, dcdt.fTienSoSanhGiam
		, dcdt.sTongHop
		, dcdt.iID_TongHopID
		, dcdt.iLoaiTongHop
		, dcdt.dNgaySua
		, dcdt.dNgayTao
		, dcdt.sNguoiSua
		, dcdt.sNguoiTao
		, dcdt.dNgayTao
		, donVi.sTenDonVi
		, dcdt.iID_LoaiCap
		, dcdt.bIsKhoa
		, dcdt.bDaTongHop
		, dcdt.sMaLoaiChi
		, lc.sTenDanhMucLoaiChi
		-- Tong dự toán todo
	FROM BH_DTC_DieuChinhDuToanChi dcdt
	LEFT JOIN DonVi donVi
		ON dcdt.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = dcdt.iID_DonVi
	LEFT JOIN BH_DM_LoaiChi lc on dcdt.iID_LoaiCap=lc.iID and dcdt.iNamLamViec=lc.iNamLamViec
	where dcdt.iNamLamViec=@NamLamViec
	order by dcdt.dNgayChungTu
END


GO
