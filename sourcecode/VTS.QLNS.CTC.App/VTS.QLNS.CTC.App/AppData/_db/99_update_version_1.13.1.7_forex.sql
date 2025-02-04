/****** Object:  StoredProcedure [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]    Script Date: 9/18/2023 8:48:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtri_capphat_index]    Script Date: 9/18/2023 8:48:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtri_capphat_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtri_capphat_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_NH_TH_NguonNgoaiHoi_tang]    Script Date: 9/18/2023 8:48:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_insert_NH_TH_NguonNgoaiHoi_tang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_insert_NH_TH_NguonNgoaiHoi_tang]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_NH_TH_NguonNgoaiHoi_tang]    Script Date: 9/18/2023 8:48:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_insert_NH_TH_NguonNgoaiHoi_tang]
	@sLoai nvarchar(100),
	@iTypeExecute int,   -- 1: add, 2: update, 3: delete, 4: điều chỉnh
	@uIdQuyetDinh uniqueidentifier,
	@iIDQuyetDinhOld uniqueidentifier
AS
BEGIN
	CREATE TABLE #lstMaNguon(sMaNguon nvarchar(100))
	CREATE TABLE #lstMaTienTrinhRv(sMaTienTrinh nvarchar(100))
	INSERT INTO #lstMaTienTrinhRv(sMaTienTrinh)
	VALUES('100'),('200');

	IF(@sLoai = 'TTCP')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('TTCP'), ('101'), ('102'),('111'),('112'),('000'),('122'),('121'), ('132'), ('131')
	END
	ELSE IF(@sLoai = 'QUYET_TOAN')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('QUYET_TOAN'), ('301'), ('302'),('303'),('304'),('305'),('306'),('307'),('308'),('309'),('310'),('000')
	END
	ELSE IF(@sLoai = 'KHOI_TAO')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('KHOI_TAO'), ('303'), ('132'), ('211c'), ('212c'), ('301'), ('302'), ('321a'), ('322a')
			, ('000'), ('321b'), ('322b')
	END

	IF(@iTypeExecute in (2,3,4))
	BEGIN 
		IF (@iTypeExecute in (2,3))
		BEGIN
			-- dao nguoc but toan
			INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iID_ChungTu,
										sMaNguon, sMaDich, sMaNguonCha, fGiaTriUsd, fGiaTriVnd, bIsLog, iStatus, sMaTienTrinh,iID_MucLucNganSach,iQuyKeHoach)
			-->Output									
			SELECT iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iID_ChungTu,
							sMaDich, tbl.sMaNguon, tbl.sMaNguon, fGiaTriUsd, fGiaTriVnd, 1, 2, '300',iID_MucLucNganSach,iQuyKeHoach												
			FROM NH_TH_TongHop as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @uIdQuyetDinh 
				AND bIsLog = 0 AND sMaTienTrinh IN (SELECT sMaTienTrinh FROM #lstMaTienTrinhRv)

			-- khoa but toan da update
			UPDATE tbl 
			SET 
				bIsLog = 1
			FROM NH_TH_TongHop as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @uIdQuyetDinh
				AND bIsLog = 0 AND sMaTienTrinh IN (SELECT sMaTienTrinh FROM #lstMaTienTrinhRv)
		END
		ELSE IF (@iTypeExecute = 4)
		BEGIN
			-- dao nguoc but toan
			INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iID_ChungTu,
										sMaNguon, sMaDich, sMaNguonCha, fGiaTriUsd, fGiaTriVnd, bIsLog, iStatus, sMaTienTrinh,iID_MucLucNganSach,iQuyKeHoach)
			--> OutPut
			SELECT iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iID_ChungTu,
							sMaDich, tbl.sMaNguon, tbl.sMaNguon, fGiaTriUsd, fGiaTriVnd, 1, 2, '300',iID_MucLucNganSach,iQuyKeHoach												
			FROM NH_TH_TongHop as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @iIDQuyetDinhOld 
				AND bIsLog = 0 AND sMaTienTrinh IN (SELECT sMaTienTrinh FROM #lstMaTienTrinhRv)

			-- khoa but toan da update
			UPDATE tbl 
			SET 
				bIsLog = 1
			FROM NH_TH_TongHop as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @iIDQuyetDinhOld
				AND bIsLog = 0 AND sMaTienTrinh IN (SELECT sMaTienTrinh FROM #lstMaTienTrinhRv)
		END

	END

	IF(@sLoai = 'TTCP')
	BEGIN

		-- deleted thi khong xu ly nua
		IF(@iTypeExecute = 3)
		BEGIN
			RETURN
		END
		BEGIN
		--Insert bút toán mới vào
		--Insert Cấp kinh phí
		INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, dNgayDeNghi, iNamKeHoach, iQuyKeHoach, iID_ChungTu, 
								sMaNguon, sMaDich, fGiaTriUsd, fGiaTriVnd, iStatus, sMaTienTrinh, iID_MucLucNganSach, bIsLog)
		SELECT tbl.iID_DuAnID, ktt.iID_NhiemVuChiID, tbl.iID_HopDongId, tbl.iID_DonVi, tbl.iID_MaDonVi, tbl.dNgayDeNghi, tbl.iNamKeHoach, tbl.iQuyKeHoach, tbl.ID,
		(CASE WHEN tbl.iNamNganSach = 1 THEN '101' ELSE '102' END), '000', 
		(CASE WHEN tbl.iLoaiNoiDungChi = 1 THEN dt.fPheDuyetCapKyNay_USD ELSE 0 END),
		(CASE WHEN tbl.iLoaiNoiDungChi = 1 THEN 0 ELSE dt.fPheDuyetCapKyNay_VND END),  
		 0, '200', dt.iID_MucLucNganSachID, 0
		FROM NH_TT_ThanhToan as tbl
		INNER JOIN NH_KHTongThe_NhiemVuChi as ktt on ktt.iID_KHTongTheID = tbl.iID_KHTongTheID and ktt.iID_NhiemVuChiID = tbl.iID_NhiemVuChiID
		INNER JOIN NH_TT_ThanhToan_ChiTiet as dt on tbl.ID = dt.iID_DeNghiThanhToanID
		WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDeNghi = 1 ;		
		--Insert bút toán mới vào
		--Insert Tạm ứng kinh phí
		INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, dNgayDeNghi, iNamKeHoach, iQuyKeHoach, iID_ChungTu, 
								sMaNguon, sMaDich,sMaNguonCha , fGiaTriUsd, fGiaTriVnd, iStatus, sMaTienTrinh, iID_MucLucNganSach, bIsLog)
		SELECT tbl.iID_DuAnID, ktt.iID_NhiemVuChiID, tbl.iID_HopDongId, tbl.iID_DonVi, tbl.iID_MaDonVi, tbl.dNgayDeNghi, tbl.iNamKeHoach, tbl.iQuyKeHoach, tbl.ID,
		'000',(CASE WHEN tbl.iNamNganSach = 1 THEN '141' ELSE '142' END), 
		(CASE WHEN tbl.iNamNganSach = 1 THEN '101' ELSE '102' END),
		(CASE WHEN tbl.iLoaiNoiDungChi = 1 THEN dt.fPheDuyetCapKyNay_USD ELSE 0 END),
		(CASE WHEN tbl.iLoaiNoiDungChi = 1 THEN 0 ELSE dt.fPheDuyetCapKyNay_VND END),  
		 0, '200', dt.iID_MucLucNganSachID, 0
		FROM NH_TT_ThanhToan as tbl
		INNER JOIN NH_KHTongThe_NhiemVuChi as ktt on ktt.iID_KHTongTheID = tbl.iID_KHTongTheID and ktt.iID_NhiemVuChiID = tbl.iID_NhiemVuChiID
		INNER JOIN NH_TT_ThanhToan_ChiTiet as dt on tbl.ID = dt.iID_DeNghiThanhToanID
		WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDeNghi = 2 ;	
			
		--Insert bút toán mới vào
		--Insert Thanh toán sử dụng kinh phí 
		INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, dNgayDeNghi, iNamKeHoach, iQuyKeHoach, iID_ChungTu, 
								sMaNguon, sMaDich, fGiaTriUsd, fGiaTriVnd, iStatus, sMaTienTrinh, iID_MucLucNganSach, bIsLog)
		SELECT tbl.iID_DuAnID, ktt.iID_NhiemVuChiID, tbl.iID_HopDongId, tbl.iID_DonVi, tbl.iID_MaDonVi, tbl.dNgayDeNghi, tbl.iNamKeHoach, tbl.iQuyKeHoach, tbl.ID,
		(CASE WHEN tbl.iNamNganSach = 1 THEN '111' ELSE '112' END), '000',
		(CASE WHEN tbl.iLoaiNoiDungChi = 1 THEN dt.fPheDuyetCapKyNay_USD ELSE 0 END),
		(CASE WHEN tbl.iLoaiNoiDungChi = 1 THEN 0 ELSE dt.fPheDuyetCapKyNay_VND END),  
		 0, '200', dt.iID_MucLucNganSachID, 0
		FROM NH_TT_ThanhToan as tbl
		INNER JOIN NH_KHTongThe_NhiemVuChi as ktt on ktt.iID_KHTongTheID = tbl.iID_KHTongTheID and ktt.iID_NhiemVuChiID = tbl.iID_NhiemVuChiID
		INNER JOIN NH_TT_ThanhToan_ChiTiet as dt on tbl.ID = dt.iID_DeNghiThanhToanID
		WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDeNghi = 3 ;		

		--Insert bút toán mới vào
		--Insert Tạm ứng theo chế độ kinh phí
		INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, dNgayDeNghi, iNamKeHoach, iQuyKeHoach, iID_ChungTu, 
								sMaNguon, sMaDich, fGiaTriUsd, fGiaTriVnd, iStatus, sMaTienTrinh, iID_MucLucNganSach, bIsLog)
		SELECT tbl.iID_DuAnID, ktt.iID_NhiemVuChiID, tbl.iID_HopDongId, tbl.iID_DonVi, tbl.iID_MaDonVi, tbl.dNgayDeNghi, tbl.iNamKeHoach, tbl.iQuyKeHoach, tbl.ID,
		(CASE WHEN tbl.iNamNganSach = 1 THEN '121' ELSE '122' END), '000',
		(CASE WHEN tbl.iLoaiNoiDungChi = 1 THEN dt.fPheDuyetCapKyNay_USD ELSE 0 END),
		(CASE WHEN tbl.iLoaiNoiDungChi = 1 THEN 0 ELSE dt.fPheDuyetCapKyNay_VND END),  
		 0, '200', dt.iID_MucLucNganSachID, 0
		FROM NH_TT_ThanhToan as tbl
		INNER JOIN NH_KHTongThe_NhiemVuChi as ktt on ktt.iID_KHTongTheID = tbl.iID_KHTongTheID and ktt.iID_NhiemVuChiID = tbl.iID_NhiemVuChiID
		INNER JOIN NH_TT_ThanhToan_ChiTiet as dt on tbl.ID = dt.iID_DeNghiThanhToanID
		WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDeNghi = 4 ;		

		-- insert but toan moi vao: thu hồi nợ 
		INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, dNgayDeNghi, iNamKeHoach,
		iQuyKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTriUsd, fGiaTriVnd, iStatus, sMaTienTrinh, bIsLog)
		SELECT tbl.iID_DuAnID, ktt.iID_NhiemVuChiID, tbl.iID_HopDongId, tbl.iID_DonVi, tbl.iID_MaDonVi, tbl.dNgayDeNghi,
		tbl.iNamKeHoach, tbl.iQuyKeHoach, tbl.ID,
		(CASE WHEN tbl.iNamNganSach = 1 THEN '131' ELSE '132' END),
		'000',
		(CASE WHEN tbl.iLoaiNoiDungChi =1 THEN tbl.fThuHoiTamUngPheDuyet_BangSo_USD ELSE 0 END), 
		(CASE WHEN tbl.iLoaiNoiDungChi =1 THEN 0 ELSE tbl.fThuHoiTamUngPheDuyet_BangSo_VND END), 
		 0, '200', 0
		FROM NH_TT_ThanhToan as tbl
		INNER JOIN NH_KHTongThe_NhiemVuChi as ktt on ktt.iID_KHTongTheID = tbl.iID_KHTongTheID and ktt.iID_NhiemVuChiID = tbl.iID_NhiemVuChiID
		WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDeNghi = 3 
			AND (tbl.fThuHoiTamUngPheDuyet_BangSo_USD <> 0 OR tbl.fThuHoiTamUngPheDuyet_BangSo_VND <> 0)
			--RETURN
		END
	END
	ELSE IF(@sLoai = 'QUYET_TOAN')
	BEGIN   
			-- Đảo ngược bút toán
			INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iID_ChungTu,
										sMaNguon, sMaDich, sMaNguonCha, fGiaTriUsd, fGiaTriVnd, bIsLog, iStatus, sMaTienTrinh,iID_MucLucNganSach,iQuyKeHoach)
			-->Output									
			SELECT iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, iCoQuanThanhToan, dNgayDeNghi, iNamKeHoach, iID_ChungTu,
							sMaDich, tbl.sMaNguon, tbl.sMaNguon, fGiaTriUsd, fGiaTriVnd, 1, 2, '300',iID_MucLucNganSach,iQuyKeHoach												
			FROM NH_TH_TongHop as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @uIdQuyetDinh 
				AND bIsLog = 0 AND sMaTienTrinh IN (SELECT sMaTienTrinh FROM #lstMaTienTrinhRv)

			-- Khóa bút toán đã update
			UPDATE tbl 
			SET 
				bIsLog = 1
			FROM NH_TH_TongHop as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @uIdQuyetDinh
				AND bIsLog = 0 AND sMaTienTrinh IN (SELECT sMaTienTrinh FROM #lstMaTienTrinhRv)
			
			--> Insert bút toán mới vào khi khóa chứng từ QTND
			INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, dNgayDeNghi, iNamKeHoach,
									iID_ChungTu, sMaNguon, sMaDich,sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iStatus, sMaTienTrinh, iID_MucLucNganSach,bIsLog)
			SELECT dt.iID_DuAnID, dt.iID_KHTT_NhiemVuChiID, dt.iID_HopDongId,tbl.iID_DonViID, tbl.iID_MaDonVi, tbl.dNgayDeNghi,
				tbl.iNamKeHoach, tbl.Id , 
				dt.sMaNguon, 
				(CASE dt.sMaNguon WHEN '000' THEN '311' ELSE '000' END) , -- sMaDich
				(CASE dt.sMaNguon WHEN '000' THEN '303' ELSE NULL END) , --sMaNguonCha
				dt.fGiaTriUSD, dt.fGiaTriVND,
				0, --status
				(CASE dt.sMaNguon WHEN '000' THEN '200' 
								  WHEN '304' THEN '200'
								  WHEN '305' THEN '200'
								  ELSE '100' END), -- sMaTienTrinh
				dt.iID_MucLucNganSachID, 0 --bIsBlog
			From  NH_QT_QuyetToanNienDo as tbl
			INNER JOIN(
				SELECT Id, iID_QuyetToanNienDoID, iID_KHTT_NhiemVuChiID, iID_DuAnID, iID_HopDongId, iID_MucLucNganSachID, dt.fGiaTri as fGiaTriUSD, 0 as fGiaTriVND,
						(CASE colName WHEN 'fQTKinhPhiDuyetCacNamTruoc_USD' THEN '301' 
									  WHEN 'fQTKinhPhiDuocCap_NamTruocChuyenSang_USD' THEN '302'
									  WHEN 'fQTKinhPhiDuocCap_NamNay_USD' THEN '303'
									  WHEN 'fDeNghiQTNamNay_USD' THEN '304'
									  WHEN 'fDeNghiChuyenNamSau_USD' THEN '305'
									  WHEN 'fThuaNopNSNN_USD' THEN '000'
									  WHEN 'fLuyKeKinhPhiDuocCap_USD' THEN '306'
									  WHEN 'fLuyKeKinhPhiDaGiaiNganTrongNamNay_USD' THEN '307'
									  WHEN 'fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_USD' THEN '308'
									  WHEN 'fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_USD' THEN '309'
									  WHEN 'fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_USD' THEN '310'
									  END) as sMaNguon				
				From
				( SELECT Id, iID_QuyetToanNienDoID, iID_KHTT_NhiemVuChiID, iID_DuAnID, iID_HopDongId, iID_MucLucNganSachID,
						fQTKinhPhiDuyetCacNamTruoc_USD, --col 5
						fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,--col 9
						fQTKinhPhiDuocCap_NamNay_USD, --11
						fDeNghiQTNamNay_USD, --13
						fDeNghiChuyenNamSau_USD, -- 15
						fThuaNopNSNN_USD, --19
						fLuyKeKinhPhiDuocCap_USD, --21
						fLuyKeKinhPhiDaGiaiNganTrongNamNay_USD, -- 24
						fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_USD, --26
						fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_USD, -- 28
						fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_USD --30
					FROM NH_QT_QuyetToanNienDo_ChiTiet) as tbl
					UNPIVOT
					(fGiaTri FOR colName IN 
						(
						  fQTKinhPhiDuyetCacNamTruoc_USD, 
						  fQTKinhPhiDuocCap_NamTruocChuyenSang_USD, 
						  fQTKinhPhiDuocCap_NamNay_USD,
						  fDeNghiQTNamNay_USD, fDeNghiChuyenNamSau_USD, 
						  fThuaNopNSNN_USD, fLuyKeKinhPhiDuocCap_USD, 
						  fLuyKeKinhPhiDaGiaiNganTrongNamNay_USD,
						  fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_USD, 
						  fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_USD, 
						  fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_USD
						)
					) as dt
				) as dt on dt.iID_QuyetToanNienDoID = tbl.Id AND dt.fGiaTriUSD <> 0
			WHERE tbl.ID = @uIdQuyetDinh
			return
	END
	ELSE IF(@sLoai = 'KHOI_TAO')
	BEGIN			
			--> Insert bút toán mới vào khi khóa chứng từ KTCP
			INSERT INTO NH_TH_TongHop(iID_DuAnID, iID_KHTT_NhiemVuChiID, iID_HopDongId, iID_DonVi, iID_MaDonViQuanLy, dNgayDeNghi, iNamKeHoach,
									iID_ChungTu, sMaNguon, sMaDich,sMaNguonCha, fGiaTriUsd, fGiaTriVnd, iStatus, sMaTienTrinh,bIsLog)
			SELECT dt.iID_DuAnID, dt.iID_KHTT_NhiemVuChiID, dt.iID_HopDongId,tbl.iID_DonViID, tbl.iID_MaDonVi, tbl.dNgayKhoiTao,
				tbl.iNamKhoiTao, tbl.Id , 
				dt.sMaNguon, 
				'000' , -- sMaDich
				NULL , --sMaNguonCha
				dt.fGiaTriUSD, dt.fGiaTriVND,
				0, --status
				(CASE dt.sMaNguon  
								  WHEN '304' THEN '200'
								  WHEN '305' THEN '200'
								  ELSE '100' END), -- sMaTienTrinh
				 0 --bIsBlog
			From  NH_KT_KhoiTaoCapPhat as tbl
			INNER JOIN(
				SELECT Id, iID_KhoiTaoCapPhatID, iID_KHTT_NhiemVuChiID, iID_DuAnID, iID_HopDongId, dt.fGiaTri as fGiaTriUSD, 0 as fGiaTriVND,
						(CASE colName WHEN 'fQTKinhPhiDuyetCacNamTruoc_USD' THEN '301' 
									  WHEN 'fDeNghiQTNamNay_USD' THEN '304'
									  WHEN 'fDeNghiChuyenNamSau_USD' THEN '305'
									  WHEN 'fLuyKeKinhPhiDuocCap_USD' THEN '306'
									  WHEN 'fLuyKeKinhPhiDaGiaiNganTrongNamNay_USD' THEN '307'
									  WHEN 'fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_USD' THEN '308'
									  WHEN 'fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_USD' THEN '309'
									  WHEN 'fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_USD' THEN '310'
									  END) as sMaNguon				
				From
				( SELECT Id, iID_KhoiTaoCapPhatID, iID_KHTT_NhiemVuChiID, iID_DuAnID, iID_HopDongId ,
						fQTKinhPhiDuyetCacNamTruoc_USD, --col 5
						fDeNghiQTNamNay_USD, --13
						fDeNghiChuyenNamSau_USD, -- 15
						fLuyKeKinhPhiDuocCap_USD, --21
						fLuyKeKinhPhiDaGiaiNganTrongNamNay_USD, -- 24
						fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_USD, --26
						fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_USD, -- 28
						fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_USD --30
					FROM NH_KT_KhoiTaoCapPhat_ChiTiet) as tbl
					UNPIVOT
					(fGiaTri FOR colName IN 
					   (
					    fQTKinhPhiDuyetCacNamTruoc_USD, 
						fDeNghiQTNamNay_USD, 
						fDeNghiChuyenNamSau_USD, 
						fLuyKeKinhPhiDuocCap_USD, 
						fLuyKeKinhPhiDaGiaiNganTrongNamNay_USD,
						fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_USD, 
						fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_USD, 
						fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_USD
						)
					) as dt
				) as dt on dt.iID_KhoiTaoCapPhatID = tbl.Id AND dt.fGiaTriUSD <> 0
			WHERE tbl.ID = @uIdQuyetDinh
		RETURN
	END
END
;

GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtri_capphat_index]    Script Date: 9/18/2023 8:48:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thongtri_capphat_index]
AS
BEGIN
	SELECT 
		thongtri.ID AS Id,
		thongtri.iID_MaDonViID AS IIdMaDonViId,
		thongtri.iID_DonViID AS IIdDonViId,
		thongtri.iID_NguonVonID AS IIdNguonVonId,
		thongtri.sMaThongTri AS SMaThongTri,
		thongtri.dNgayLapThongTri AS DNgayLapThongTri,
		thongtri.iNamThucHien AS INamThucHien,
		thongtri.iID_DonViTienTeID AS IIdDonViTienTeId,
		thongtri.dNgayGhiSo AS DNgayGhiSo,
		thongtri.sTK1 AS STk1,
		thongtri.sSoCT1 AS SSoCt1,
		thongtri.sTK2 AS STk2,
		thongtri.sSoCT2 AS SSoCt2,
		thongtri.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndid,
		thongtri.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		thongtri.fTongGiaTriNgoaiTeKhac AS FTongGiaTriNgoaiTeKhac,
		thongtri.fTongGiaTriUSD AS FTongGiaTriUsd,
		thongtri.fTongGiaTriVND AS FTongGiaTriVnd,
		thongtri.sTongGiaTri_BangChu AS STongGiaTriBangChu,
		thongtri.sNguoiTao AS SNguoiTao,
		thongtri.dNgayTao AS DNgayTao,
		thongtri.sNguoiSua AS SNguoiSua,
		thongtri.dNgaySua AS DNgaySua,
		thongtri.sNguoiXoa AS SNguoiXoa,
		thongtri.dNgayXoa AS DNgayXoa,
		thongtri.bIsActive AS BIsActive,
		thongtri.bIsGoc AS BIsGoc,
		thongtri.bIsKhoa AS BIsKhoa,
		thongtri.iLanDieuChinh AS ILanDieuChinh,
		thongtri.iID_TiGiaID AS IIdTiGiaId,
		thongtri.bIsXoa AS BIsXoa,
		thongtri.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		CONCAT(DonVi.iID_MaDonVi, ' - ', DonVi.sTenDonVi) AS STenDonVi,
		NguonNganSach.sTen AS STenNguonVon,
		tiente.sMaTienTe AS STenTienTe
	FROM NH_TT_ThongTriCapPhat thongtri
	LEFT JOIN DonVi ON thongtri.iID_DonViID = DonVi.iID_DonVi
	LEFT JOIN NguonNganSach ON thongtri.iID_NguonVonID = NguonNganSach.iID_MaNguonNganSach
	LEFT JOIN NH_DM_LoaiTienTe tiente ON thongtri.iID_DonViTienTeID = tiente.ID
	ORDER BY DNgayTao DESC
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]    Script Date: 9/18/2023 8:48:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thongtricapphat_dspheduyetthanhtoan]
AS
BEGIN
	SELECT 
		thanhtoan.ID AS IIdPheDuyetThanhToanId,
		chitiet.ID AS Id,
		chitiet.iID_ThongTriCapPhatID AS IIdThongTriCapPhatId,
		chitiet.sMaOrder AS SMaOrder,
		chitiet.iTrangThai AS ITrangThai,
		CASE 
			WHEN thanhtoan.iTrangThai = 2  THEN thanhtoan.sSoDeNghi 
		END AS SSoDeNghi,
		nhiemvuchi.sTenNhiemVuChi AS STenNhiemVuChi,
		hopdong.sTenHopDong AS STenHopDong,
		CASE
			WHEN thanhtoan.iLoaiDeNghi = 1 THEN N'Cấp kinh phí'
			WHEN thanhtoan.iLoaiDeNghi = 2 THEN N'Tạm ứng kinh phí'
			WHEN thanhtoan.iLoaiDeNghi = 3 THEN N'Thanh toán theo khối lượng'
			WHEN thanhtoan.iLoaiDeNghi = 4 THEN N'Tạm ứng theo chế độ'
		END AS SLoaiDeNghi,
		SUM(TTchitiet.fPheDuyetCapKyNay_USD) AS FPheDuyetUsd,
		SUM(TTchitiet.fPheDuyetCapKyNay_VND) AS FPheDuyetVnd,
		SUM(TTchitiet.fPheDuyetCapKyNay_EUR) AS FPheDuyetEur,
		SUM(TTchitiet.fPheDuyetCapKyNay_NgoaiTeKhac) AS FPheDuyetNgoaiTeKhac,
		thanhtoan.iNamKeHoach AS iNamKeHoach,
		thanhtoan.iID_DonVi AS iID_DonVi,
		thanhtoan.iID_NguonVonID AS iID_NguonVonID
	FROM NH_TT_ThanhToan thanhtoan 
	LEFT JOIN NH_TT_ThongTriCapPhat_ChiTiet chitiet ON thanhtoan.ID = chitiet.iID_PheDuyetThanhToanID
	LEFT JOIN NH_DM_NhiemVuChi nhiemvuchi ON thanhtoan.iID_NhiemVuChiID = nhiemvuchi.ID
	LEFT JOIN NH_DA_HopDong hopdong ON thanhtoan.iID_HopDongID = hopdong.Id
	LEFT JOIN NH_TT_ThanhToan_ChiTiet TTchitiet ON thanhtoan.ID = TTchitiet.iID_DeNghiThanhToanID
	WHERE thanhtoan.iTrangThai = 2
	GROUP BY thanhtoan.ID, chitiet.ID, chitiet.iID_ThongTriCapPhatID, chitiet.sMaOrder, chitiet.iTrangThai, thanhtoan.iLoaiDeNghi, thanhtoan.iNamKeHoach, thanhtoan.iID_DonVi, thanhtoan.iID_NguonVonID,
	nhiemvuchi.sTenNhiemVuChi, hopdong.sTenHopDong, thanhtoan.iTrangThai, thanhtoan.sSoDeNghi
END
;
;
GO
