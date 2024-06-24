-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Aug 08, 2017 at 10:21 AM
-- Server version: 5.7.15-log
-- PHP Version: 5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `qlmamnon`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `countBangThuTienGenHistoryByLopAndNgayTinh`(IN `lop` INT, IN `ngay` DATE)
    NO SQL
SELECT COUNT(*) FROM `bangthutiengenhistory` WHERE (lop IS NULL OR LopId = lop) AND NgayTinh = ngay$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteHocSinhLopByHocSinhIds`(IN `hocsinhids` TEXT, IN `deleteddate` DATE)
    NO SQL
BEGIN

SET @sql = CONCAT('UPDATE `hocsinh_lop` SET `DateLeft`="', DATE_FORMAT(deleteddate, '%Y-%m-%d'), '", `DateDeleted`="', DATE_FORMAT(NOW(), '%Y-%m-%d'), '"  WHERE HocSinhId IN (', hocsinhids, ')');

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteKhoiTruongByKhoiId`(IN `khoi` INT)
    MODIFIES SQL DATA
DELETE FROM khoi_truong WHERE KhoiId = khoi$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteLopKhoiByLopId`(IN `lop` INT)
    MODIFIES SQL DATA
DELETE FROM lop_khoi WHERE LopId = lop$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteViewBanGiaoTaiSan`(IN `taisanlopid` INT)
    MODIFIES SQL DATA
UPDATE `taisan_lop` SET `taisan_lop`.`NgayHetHan`=NOW() WHERE `taisan_lop`.`TaiSanLopId`=taisanlopid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteViewTaiSan`(IN `taisanid` INT)
    MODIFIES SQL DATA
BEGIN

DECLARE phieuchiid INT DEFAULT 0;

SET phieuchiid := (SELECT ts.PhieuChiId FROM TaiSan ts WHERE ts.TaiSanId=taisanid);


DELETE FROM `phieuchi` WHERE `phieuchi`.`PhieuChiId`=phieuchiid;

DELETE FROM `TaiSan` WHERE `TaiSan`.`TaiSanId`=taisanid;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getBangThuTienById`(IN `@bangthutienid` INT)
    NO SQL
SELECT * FROM `bangthutien` WHERE BangThuTienId = `@bangthutienid`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getBangThuTienKhoanThuByBangThuTienIds`(IN `bangthutienids` TEXT)
    NO SQL
BEGIN

SET @sql = CONCAT('SELECT * FROM `bangthutien_khoanthu` WHERE 
                  `BangThuTienId` IN (', bangthutienids, ')');

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getGiaoVienByParams`(IN `@tinhthanhphoid` INT, IN `@quanhuyenid` INT, IN `@phuongxaid` INT, IN `@ngaysinh` DATE)
    NO SQL
SELECT * FROM `giaovien` gv WHERE gv.TinhTPId=`@tinhthanhphoid` AND gv.QuanHuyenId = `@quanhuyenid` AND gv.PhuongXaId = `@phuongxaid` AND gv.NgaySinh = `@ngaysinh`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getHocSinhByLopAndNgay`(IN `lop` INT, IN `ngay` DATE)
    NO SQL
SELECT h.* FROM hocsinh h INNER JOIN hocsinh_lop hl ON h.HocSinhId = hl.HocSinhId WHERE h.THoiHoc=0 AND (lop IS NULL OR hl.LopId = lop) AND DateJoin <= ngay AND (DateLeft IS NULL OR DateLeft >= ngay)
ORDER BY hl.LopId$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getHocSinhByParams`(IN `ngay` DATE, IN `khoihoc` INT, IN `lophoc` INT, IN `ngayvaolop` DATE)
    READS SQL DATA
SELECT h.*
FROM hocsinh h LEFT JOIN hocsinh_lop hl ON h.HocSinhId = hl.HocSinhId
    LEFT JOIN lop_khoi lk ON hl.LopId = lk.LopId
WHERE (khoihoc IS NULL OR lk.KhoiId = khoihoc)
    AND (lophoc IS NULL OR hl.LopId = lophoc)
    AND (ngay IS NULL OR hl.DateJoin <= ngay)
    AND (ngay IS NULL OR hl.DateLeft IS NULL OR hl.DateLeft > ngay)
    AND (ngayvaolop IS NULL OR hl.DateJoin = ngayvaolop)
    AND (ngayvaolop IS NULL OR hl.DateLeft IS NULL OR hl.DateLeft > ngayvaolop)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getHocSinhForThongTinHocSinh`(IN `@quan` INT, IN `@phuong` INT, IN `@ngaysinh` DATE, IN `@thangsinh` INT, IN `@lop` INT, IN `@thoihoc` BIT, IN `@namsinh` INT)
    NO SQL
SELECT h.* 
FROM hocsinh h LEFT JOIN 
    (SELECT * FROM hocsinh_lop hl WHERE hl.DateDeleted IS NULL OR hl.DateLeft > Current_date()) hl ON h.HocSinhId = hl.HocSinhId 
    LEFT JOIN lop_khoi lk ON hl.LopId = lk.LopId
    
WHERE (`@quan` IS NULL OR h.QuanHuyenId = `@quan`) AND (`@phuong` IS NULL OR h.PhuongXaId = `@phuong`) AND (`@ngaysinh` IS NULL OR h.NgaySinh = `@ngaysinh`) AND (`@thangsinh` IS NULL OR MONTH(h.NgaySinh) = `@thangsinh`) AND (`@lop` IS NULL OR hl.LopId = `@lop`) AND (`@thoihoc` IS NULL OR h.ThoiHoc = `@thoihoc`)  AND (`@namsinh` IS NULL OR YEAR(h.NgaySinh) = `@namsinh`)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getHocSinhLopByHocSinhIdsAndNgay`(IN `hocsinhids` TEXT, IN `ngay` DATE)
    NO SQL
BEGIN

SET @sql = CONCAT('SELECT * FROM `hocsinh_lop` WHERE `HocSinhId` IN (',hocsinhids, ') AND DateJoin <="', DATE_FORMAT(ngay, '%Y-%m-%d'),'" AND (DateLeft IS NULL OR DateLeft > "', DATE_FORMAT(ngay, '%Y-%m-%d'),'")');

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getHocSinhNotAssignedToLop`()
    NO SQL
SELECT h.* FROM hocsinh h LEFT JOIN (SELECT * FROM hocsinh_lop hl WHERE hl.DateDeleted IS NULL) hl ON h.HocSinhId = hl.HocSinhId WHERE hl.LopId IS NULL$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getKhoanThuByIds`(IN `khoanthuids` TEXT)
    NO SQL
BEGIN

SET @sql = CONCAT('SELECT * FROM `khoanthu` WHERE 
                  `KhoanThuId` IN (', khoanthuids, ')');

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getKhoanThuHangNamByParams`(IN `khoanthuids` TEXT, IN `khoi` INT, IN `ngay` DATE)
    NO SQL
BEGIN

SET @sql = CONCAT('SELECT * FROM `khoanthuhangnam` WHERE `KhoiId` = ',khoi, ' AND `KhoanThuId` IN (', khoanthuids, ') AND NgayTinh <="', DATE_FORMAT(ngay, '%Y-%m-%d'),'" AND (NgayKetThuc IS NULL OR NgayKetThuc > "', DATE_FORMAT(ngay, '%Y-%m-%d'),'")');

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getKhoiTruongByKhoiId`(IN `khoi` INT)
    READS SQL DATA
SELECT *
FROM khoi_truong
WHERE KhoiId=khoi$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getLopKhoiByLopId`(IN `lop` INT)
    READS SQL DATA
SELECT *
FROM lop_khoi
WHERE LopId=lop$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPhieuChiByDateRange`(IN `@fromDate` DATE, IN `@toDate` DATE, IN `@phanloaichiids` TEXT)
    NO SQL
BEGIN

SET @sql = CONCAT('SELECT * FROM `phieuchi` pc WHERE pc.Ngay>="', `@fromDate`, '" AND pc.Ngay<="', `@toDate`, '" AND pc.PhanLoaiChiId IN (', `@phanloaichiids`,')');

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPhieuChiByDateRangeWithGroupPhanLoaiChi`(IN `@fromDate` DATE, IN `@toDate` DATE, IN `@phanloaichiids` TEXT)
    NO SQL
BEGIN

SET @sql = CONCAT('SELECT MAX(PhieuChiId) AS PhieuChiId, "9999-01-01" AS CreatedDate, 0 AS DonGia, "" AS GhiChu, "" AS MaPhieu, "9999-01-01" AS Ngay, plc.MaPhanLoai AS NoiDung, 0 AS PhanLoaiChiId, 0 AS SoLuong, SUM(SoTien) AS SoTien FROM `phieuchi` pc INNER JOIN phanloaichi plc ON pc.PhanLoaiChiId=plc.PhanLoaiChiId WHERE pc.Ngay>="', `@fromDate`, '" AND pc.Ngay<="', `@toDate`, '" AND pc.PhanLoaiChiId IN (', `@phanloaichiids`,') GROUP BY plc.MaPhanLoai');

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPhieuChiById`(IN `phieuchiid` INT)
    NO SQL
SELECT * FROM `phieuchi` pc WHERE pc.`PhieuChiId`=phieuchiid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPhieuChiListForReportBCHDTC`(IN `tungay` DATE, IN `denngay` DATE, IN `phanloaichiids` TEXT)
    NO SQL
BEGIN

IF (phanloaichiids = "") THEN

SET @sql = CONCAT('SELECT `pc`.`PhanLoaiChiId`, `plc`.`DienGiai`, `plc`.`MaPhanLoai`, SUM(`pc`.`SoTien`) AS `SoTien`
FROM `phanloaichi` plc INNER JOIN `phieuchi` pc ON plc.`PhanLoaiChiId`=pc.`PhanLoaiChiId`
WHERE pc.`Ngay`>="',DATE_FORMAT(tungay, '%Y-%m-%d'),'" AND pc.`Ngay`<="',DATE_FORMAT(denngay, '%Y-%m-%d'),'" GROUP BY `pc`.`PhanLoaiChiId`, `plc`.`DienGiai`');

ELSE

SET @sql = CONCAT('SELECT `pc`.`PhanLoaiChiId`, `plc`.`DienGiai`,`plc`.`MaPhanLoai`, SUM(`pc`.`SoTien`) AS `SoTien`
FROM `phanloaichi` plc INNER JOIN `phieuchi` pc ON plc.`PhanLoaiChiId`=pc.`PhanLoaiChiId`
WHERE pc.`Ngay`>="',DATE_FORMAT(tungay, '%Y-%m-%d'),'" AND pc.`Ngay`<="',DATE_FORMAT(denngay, '%Y-%m-%d'),'" AND  pc.`PhanLoaiChiId` IN(',phanloaichiids, ') GROUP BY `pc`.`PhanLoaiChiId`, `plc`.`DienGiai`');

END IF;


PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPhieuThuByHocSinhAndThangNam`(IN `hocsinhids` TEXT, IN `nam` INT, IN `thang` INT)
    NO SQL
BEGIN SET @sql = CONCAT('SELECT * FROM `phieuthu` pt WHERE `HocSinhId` IN (',hocsinhids, ') AND YEAR(pt.Ngay)=', nam,' AND MONTH(pt.Ngay)=', thang, ' ORDER BY `Ngay`'); PREPARE stmt FROM @sql; EXECUTE stmt; DEALLOCATE PREPARE stmt; END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPhieuThuByHocSinhIdAndNgayTinh`(IN `hocsinhid` INT, IN `ngaytinh` DATE)
    NO SQL
SELECT pt.*

FROM `phieuthu` pt

WHERE (hocsinhid < 0 OR pt.HocSinhId=hocsinhid) AND YEAR(pt.Ngay)=YEAR(ngaytinh) AND MONTH(pt.Ngay)=MONTH(ngaytinh)

ORDER BY pt.Ngay$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPhieuThuForReportBCHDTC`(IN `tungay` DATE, IN `denngay` DATE)
    NO SQL
SELECT  COALESCE(SUM(`pt`.`SoTien`), 0) AS `SoTien`
FROM `phieuthu` pt
WHERE pt.`Ngay`>=tungay AND pt.`Ngay`<=denngay$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPhieuThuForSoQuyTienMat`(IN `@fromDate` DATE, IN `@toDate` DATE)
    READS SQL DATA
Select MAX(pt.PhieuThuId) AS PhieuThuId, pt.Ngay, SUM(pt.SoTien) AS SoTien, '' AS MaPhieu, '' AS GhiChu, MAX(pt.HocSinhId) AS HocSinhId, MAX(pt.CreatedDate) AS CreatedDate
From PhieuThu pt
Where pt.Ngay>=`@fromDate` AND pt.Ngay<=`@toDate`
Group By pt.Ngay$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getSoNgayNghiThangByNgayAndHocSinhIds`(IN `hocsinhids` TEXT, IN `ngay` DATE)
    NO SQL
BEGIN

SET @sql = CONCAT('SELECT HocSinhId, SoNgayNghiThang FROM `viewhoctap` WHERE MONTH(NgayTinh)=', MONTH(ngay), ' AND YEAR(NgayTinh)=', YEAR(ngay), ' AND HocSinhId IN (', hocsinhids, ')');

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getSoTienTruyThuByHocSinhId`(IN `hocsinhid` INT)
    NO SQL
SELECT btt.`SoTienTruyThu` FROM `bangthutien` btt WHERE btt.HocSinhId=hocsinhid ORDER BY `NgayTinh` DESC LIMIT 1$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getSumSoTienChiByDateRange`(IN `@fromDate` DATE, IN `@toDate` DATE, IN `@phanloaichiids` TEXT)
    NO SQL
BEGIN

IF (`@phanloaichiids` IS NULL) THEN

SET @sql = CONCAT('SELECT COALESCE(SUM(`pc`.`SoTien`), 0) AS `SoTien` FROM `phieuchi` pc WHERE pc.Ngay>="', `@fromDate`, '" AND pc.Ngay<="', `@toDate`, '"');

ELSE

SET @sql = CONCAT('SELECT COALESCE(SUM(`pc`.`SoTien`), 0) AS `SoTien` FROM `phieuchi` pc WHERE pc.Ngay>="', `@fromDate`, '" AND pc.Ngay<="', `@toDate`, '" AND pc.PhanLoaiChiId IN (', `@phanloaichiids`,')');

END IF;

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getUser`(IN `@username` TEXT, IN `@password` TEXT)
    NO SQL
Select *
From user u
Where u.UserName=`@username` AND u.Password=`@password`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getUserPriviledgeByUserId`(IN `@userid` INT)
    NO SQL
SELECT * FROM `user_privilege` WHERE UserId=`@userid`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getViewBanGiaoTaiSanByLopId`(IN `lopid` INT)
    NO SQL
SELECT *
FROM ViewBanGiaoTaiSan vbgts
WHERE vbgts.LopId=lopid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getViewBanGiaoTaiSanByYear`(IN `year` INT)
    NO SQL
SELECT * FROM `viewbangiaotaisan` vbgts WHERE (vbgts.LopId IS NOT NULL) AND YEAR(vbgts.NgayBanGiao)=year
ORDER BY vbgts.Ten, vbgts.TaiSanId, vbgts.NgayBanGiao$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getViewBangThuTienByNgayTinhAndHocSinhId`(IN `@ngay` DATE, IN `@hocsinhid` INT)
    NO SQL
SELECT vbtt.* FROM viewbangthutien vbtt
WHERE vbtt.HocSinhId=@hocsinhid AND YEAR(vbtt.`NgayTinh`) = YEAR(@ngay) AND MONTH(vbtt.`NgayTinh`) = MONTH(@ngay)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getViewBangThuTienByNgayTinhAndLop`(IN `ngay` DATE, IN `lop` INT)
    NO SQL
SELECT DISTINCT vbtt.* FROM viewbangthutien vbtt INNER JOIN hocsinh_lop hl ON vbtt.HocSinhId = hl.HocSinhId INNER JOIN hocsinh h ON vbtt.HocSinhId=h.HocSinhId

WHERE YEAR(vbtt.`NgayTinh`) = YEAR(ngay) AND MONTH(vbtt.`NgayTinh`) = MONTH(ngay) AND (lop IS NULL OR vbtt.`LopId` = lop) AND hl.DateJoin <= ngay AND (hl.DateLeft IS NULL OR hl.DateLeft > ngay) AND h.ThoiHoc=0

ORDER BY vbtt.`LopId`, vbtt.STT$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getViewHocTapByLopAndNgay`(IN `lop` INT, IN `ngay` DATE)
    NO SQL
BEGIN


IF NOT EXISTS(SELECT * FROM `baocaothanggenhistory` bccgh WHERE bccgh.`NgayTinh`=ngay AND bccgh.`LopId`=lop) THEN

  INSERT INTO `baocaothanggenhistory`(`LopId`, `NgayTinh`) VALUES (lop, ngay);

  INSERT INTO `baocaothang`(`NgayTinh`, `HocTapId`, `SoNgayNghi`) (
    SELECT ngay AS `NgayTinh`, ht.HocTapId, 0 AS `SoNgayNghi`
    FROM `hoctap` ht INNER JOIN `hocsinh_lop` hsl ON ht.HocSinhLopId=hsl.HocSinhLopId
    WHERE hsl.LopId = lop AND ngay >= hsl.DateJoin AND (hsl.DateLeft IS NULL OR hsl.DateLeft > ngay)
  );

END IF;


SELECT * FROM `viewhoctap`
WHERE LopId = lop AND (ngay IS NULL OR (ngay >= DateJoin AND (DateLeft IS NULL OR DateLeft > ngay))) AND (`NgayTinh` IS NULL OR `NgayTinh` = ngay);

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getViewTaiSanForBanGiaoTaiSan`()
    NO SQL
SELECT `TaiSanId`, `Ten`, `NgayNhap`, `DonViTinh`, `DonGia`, `SoChungTu`, `NgayChungTu`, `ThanhTien`, `PhieuChiId`, `PhanLoaiChiId`, SUM(`SoLuong`) AS SoLuong

FROM `viewtaisanforbangiaotaisan`

GROUP BY `TaiSanId`

HAVING SoLuong > 0$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `insertHocSinhToLop`(IN `hocsinh` INT, IN `lop` INT, IN `ngayvaolop` DATE)
    MODIFIES SQL DATA
BEGIN

UPDATE `hocsinh_lop` SET `DateLeft` = ngayvaolop, `DateDeleted` = NOW() WHERE `HocSinhId` = hocsinh AND `DateLeft` IS NULL;

IF NOT EXISTS(
    SELECT * FROM `hocsinh_lop` WHERE `HocSinhId` = hocsinh AND `LopId` = lop AND `DateJoin` = ngayvaolop AND `DateLeft` IS NULL) THEN
    INSERT INTO `hocsinh_lop`(`HocSinhId`, `LopId`, `DateJoin`) VALUES (hocsinh, lop, ngayvaolop);
END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `insertViewBanGiaoTaiSai`(IN `taisanid` INT, IN `lopid` INT, IN `ngaybangiao` DATE, IN `ngayhethan` DATE, IN `ghichu` TEXT CHARSET utf8, IN `soluongbangiao` DOUBLE)
    MODIFIES SQL DATA
INSERT INTO `taisan_lop`(`taisan_lop`.`TaiSanId`, `taisan_lop`.`LopId`, `taisan_lop`.`NgayBanGiao`, `taisan_lop`.`NgayHetHan`, `taisan_lop`.`GhiChu`, `taisan_lop`.`SoLuong`) VALUES (taisanid, lopid, ngaybangiao, ngayhethan, ghichu,soluongbangiao)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `insertViewTaiSan`(IN `ten` TEXT, IN `ngaynhap` DATE, IN `donvitinh` TEXT, IN `soluong` INT, IN `dongia` BIGINT, IN `thanhtien` BIGINT, IN `sochungtu` TEXT, IN `ngaychungtu` DATE, IN `phanloaichi` INT)
    MODIFIES SQL DATA
BEGIN

DECLARE phieuchiid INT DEFAULT 0;

INSERT INTO `phieuchi`(`Ngay`, `SoTien`, `PhanLoaiChiId`) VALUES (ngaynhap, thanhtien, phanloaichi);

SET phieuchiid := LAST_INSERT_ID();

INSERT INTO `taisan`(`Ten`, `DonViTinh`, `SoLuong`, `DonGia`, `SoChungTu`, `NgayChungTu`, `PhieuChiId`) VALUES (ten, donvitinh, soluong, dongia, sochungtu, ngaychungtu, phieuchiid);

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `selectViewBanGiaoTaiSan`()
    NO SQL
SELECT `TaiSanId`, `Ten`, `DonViTinh`, `SoLuong`, `DonGia`, `SoChungTu`, `NgayChungTu`, `PhieuChiId`, `LopId`, `NgayBanGiao`, `NgayHetHan`, `GhiChu`, `TaiSanLopId`, `SoLuongBanGiao`, `NgayNhap` FROM `viewbangiaotaisan` WHERE `NgayHetHan` IS NULL OR `NgayHetHan`> NOW()$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateSoTienTruyThuByHocSinhAndNgayUpdated`(IN `hocsinhid` INT, IN `ngayupdated` DATE, IN `sotientruythuadded` BIGINT)
    MODIFIES SQL DATA
UPDATE `bangthutien` SET `bangthutien`.`SoTienTruyThu`=`bangthutien`.`SoTienTruyThu`+sotientruythuadded WHERE `bangthutien`.`HocSinhId`=hocsinhid AND `bangthutien`.`NgayTinh`>ngayupdated$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateViewBanGiaoTaiSan`(IN `taisanlopid` INT, IN `lopid` INT, IN `ngaybangiao` DATE, IN `ngayhethan` DATE, IN `ghichu` TEXT CHARSET utf8, IN `soluongbangiao` DOUBLE)
    MODIFIES SQL DATA
UPDATE `taisan_lop` SET `taisan_lop`.`LopId`=lopid,`taisan_lop`.`NgayBanGiao`=ngaybangiao,`taisan_lop`.`NgayHetHan`=ngayhethan,`taisan_lop`.`GhiChu`=ghichu, `taisan_lop`.`SoLuong`=soluongbangiao WHERE `taisan_lop`.`TaiSanLopId`=taisanlopid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateViewHocTap`(IN `hoctapid` INT, IN `hocsinhlopid` INT, IN `songaynghithang` INT, IN `ngay` DATE)
    NO SQL
BEGIN

DECLARE _hoctapid INT DEFAULT 0;

IF (hoctapid IS NULL) THEN
  INSERT INTO `hoctap`(`HocSinhLopId`) VALUES (hocsinhlopid);
  SET _hoctapid := LAST_INSERT_ID();

ELSE

  SET _hoctapid := hoctapid;

END IF;



IF NOT EXISTS(
  SELECT * FROM `baocaothang` WHERE `baocaothang`.`HocTapId`=_hoctapid AND `baocaothang`.`NgayTinh`=ngay ) THEN

  INSERT INTO `baocaothang`(`HocTapId`, `NgayTinh`, `SoNgayNghi`) VALUES (_hoctapid, ngay, songaynghithang);

ELSE

  UPDATE `baocaothang` SET `baocaothang`.`SoNgayNghi`=songaynghithang WHERE `baocaothang`.`HocTapId`=_hoctapid AND `baocaothang`.`NgayTinh`=ngay;

END IF;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateViewTaiSan`(IN `ten` TEXT, IN `ngaynhap` DATE, IN `donvitinh` TEXT, IN `soluong` INT, IN `dongia` BIGINT, IN `thanhtien` BIGINT, IN `sochungtu` TEXT, IN `ngaychungtu` DATE, IN `phanloaichi` INT, IN `taisanid` INT)
    MODIFIES SQL DATA
BEGIN

DECLARE phieuchiid INT DEFAULT 0;

SET phieuchiid := (SELECT ts.PhieuChiId FROM TaiSan ts WHERE ts.TaiSanId=taisanid);

UPDATE `phieuchi` SET `phieuchi`.`Ngay`=ngaynhap,`phieuchi`.`SoTien`=thanhtien,`phieuchi`.`PhanLoaiChiId`=phanloaichi WHERE `phieuchi`.`PhieuChiId`=phieuchiid;

UPDATE `taisan` SET `taisan`.`Ten`=ten,`taisan`.`DonViTinh`=donvitinh,`taisan`.`SoLuong`=soluong,`taisan`.`DonGia`=dongia,`taisan`.`SoChungTu`=sochungtu,`taisan`.`NgayChungTu`=ngaychungtu WHERE `taisan`.`TaiSanId`=taisanid;

END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `bangthutien`
--

CREATE TABLE IF NOT EXISTS `bangthutien` (
  `BangThuTienId` int(11) NOT NULL AUTO_INCREMENT,
  `HocSinhId` int(11) NOT NULL,
  `LopId` int(11) NOT NULL,
  `SXThangTruoc` int(11) DEFAULT NULL,
  `SoTienSXThangTruoc` bigint(20) DEFAULT NULL,
  `AnSangThangTruoc` int(11) DEFAULT NULL,
  `SoTienAnSangThangTruoc` bigint(20) DEFAULT NULL,
  `SoTienAnSangThangNay` bigint(20) NOT NULL,
  `SoTienAnToiThangTruoc` bigint(20) DEFAULT NULL,
  `AnToiThangTruoc` int(11) NOT NULL,
  `SoTienAnToiThangNay` bigint(20) NOT NULL,
  `SoTienDoDung` bigint(20) DEFAULT '0',
  `SoTienNangKhieu` bigint(20) DEFAULT '0',
  `SoTienTruyThu` bigint(20) DEFAULT '0',
  `SoTienDieuHoa` bigint(20) DEFAULT '0',
  `NgayTinh` date NOT NULL,
  `STT` int(11) NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `GhiChu` text COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`BangThuTienId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=420 ;

--
-- Dumping data for table `bangthutien`
--

INSERT INTO `bangthutien` (`BangThuTienId`, `HocSinhId`, `LopId`, `SXThangTruoc`, `SoTienSXThangTruoc`, `AnSangThangTruoc`, `SoTienAnSangThangTruoc`, `SoTienAnSangThangNay`, `SoTienAnToiThangTruoc`, `AnToiThangTruoc`, `SoTienAnToiThangNay`, `SoTienDoDung`, `SoTienNangKhieu`, `SoTienTruyThu`, `SoTienDieuHoa`, `NgayTinh`, `STT`, `IsDeleted`, `DateCreated`, `GhiChu`) VALUES
(166, 147, 1, 2, 40000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 25, 0, '2017-07-01 04:18:03', ''),
(167, 148, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 24, 0, '2017-07-01 04:18:04', ''),
(168, 149, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 27, 0, '2017-07-01 04:18:04', ''),
(169, 150, 1, 1, 20000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 26, 0, '2017-07-01 04:18:04', ''),
(170, 151, 1, 2, 40000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 1, 0, '2017-07-01 04:18:05', ''),
(171, 161, 1, 4, 80000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 28, 0, '2017-07-01 04:18:05', ''),
(172, 163, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 3, 0, '2017-07-01 04:18:05', ''),
(173, 164, 1, 0, 0, 0, 0, 0, 18000, 3, 310000, 0, 40000, 0, 30000, '2017-07-31', 23, 0, '2017-07-01 04:18:05', ''),
(174, 165, 1, 5, 100000, 3, 18000, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 2, 0, '2017-07-01 04:18:05', ''),
(175, 168, 1, 1, 20000, 0, 0, 200000, 18000, 3, 310000, 0, 0, 0, 30000, '2017-07-31', 17, 0, '2017-07-01 04:18:06', ''),
(176, 174, 1, 2, 40000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 33, 0, '2017-07-01 04:18:06', ''),
(177, 188, 1, 2, 40000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 15, 0, '2017-07-01 04:18:06', ''),
(178, 177, 1, 1, 20000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 32, 0, '2017-07-01 04:18:06', ''),
(179, 89, 1, 5, 100000, 3, 18000, 200000, 54000, 9, 310000, 0, 40000, 0, 30000, '2017-07-31', 18, 0, '2017-07-01 04:18:06', ''),
(180, 102, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 37, 0, '2017-07-01 04:18:07', ''),
(181, 111, 1, 5, 100000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 30, 0, '2017-07-01 04:18:07', ''),
(182, 112, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 31, 0, '2017-07-01 04:18:07', ''),
(183, 122, 1, 5, 100000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 29, 0, '2017-07-01 04:18:07', ''),
(184, 117, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 6, 0, '2017-07-01 04:18:08', ''),
(185, 118, 1, 7, 140000, 3, 18000, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 7, 0, '2017-07-01 04:18:08', ''),
(186, 123, 1, 5, 100000, 3, 18000, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 9, 0, '2017-07-01 04:18:08', ''),
(187, 126, 1, 1, 20000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 10, 0, '2017-07-01 04:18:08', ''),
(188, 127, 1, 8, 160000, 6, 36000, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 8, 0, '2017-07-01 04:18:08', ''),
(189, 128, 1, 5, 100000, 0, 0, 0, 0, 0, 310000, 0, 0, 0, 30000, '2017-07-31', 22, 0, '2017-07-01 04:18:09', ''),
(190, 129, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 35, 0, '2017-07-01 04:18:09', ''),
(191, 120, 1, 1, 20000, 0, 0, 200000, 18000, 3, 310000, 0, 40000, 0, 30000, '2017-07-31', 19, 0, '2017-07-01 04:18:09', ''),
(192, 94, 1, 1, 20000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 14, 0, '2017-07-01 04:18:09', ''),
(193, 96, 1, 10, 200000, 2, 12000, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 5, 0, '2017-07-01 04:18:10', ''),
(194, 109, 1, 1, 20000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 12, 0, '2017-07-01 04:18:10', ''),
(195, 90, 1, 2, 40000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 36, 0, '2017-07-01 04:18:10', ''),
(196, 64, 1, 6, 120000, 21, 126000, 200000, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 21, 0, '2017-07-01 04:18:10', ''),
(197, 92, 1, 0, 0, 0, 0, 0, 0, 0, 310000, 0, 0, 0, 30000, '2017-07-31', 34, 0, '2017-07-01 04:18:11', ''),
(198, 208, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 16, 0, '2017-07-01 04:18:11', ''),
(199, 231, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 4, 0, '2017-07-01 04:18:11', ''),
(200, 230, 1, 0, 0, 0, 0, 200000, 6000, 1, 310000, 0, 40000, 0, 30000, '2017-07-31', 20, 0, '2017-07-01 04:18:11', ''),
(201, 242, 1, 1, 20000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 11, 0, '2017-07-01 04:18:11', ''),
(202, 243, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 13, 0, '2017-07-01 04:18:11', ''),
(203, 28, 2, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 3, 0, '2017-07-01 04:18:12', ''),
(204, 32, 2, 1, 20000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 28, 0, '2017-07-01 04:18:12', ''),
(205, 33, 2, 1, 20000, 18, 108000, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 2, 0, '2017-07-01 04:18:12', ''),
(206, 34, 2, 1, 20000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 26, 0, '2017-07-01 04:18:12', ''),
(207, 35, 2, 1, 20000, 0, 0, 0, 18000, 3, 310000, 0, 0, 0, 0, '2017-07-31', 22, 0, '2017-07-01 04:18:13', ''),
(208, 46, 2, 1, 20000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 1, 0, '2017-07-01 04:18:13', ''),
(209, 47, 2, 2, 40000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 27, 0, '2017-07-01 04:18:13', ''),
(210, 48, 2, 0, 0, 0, 0, 200000, 18000, 3, 310000, 0, 40000, 0, 30000, '2017-07-31', 16, 0, '2017-07-01 04:18:13', ''),
(211, 51, 2, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 5, 0, '2017-07-01 04:18:13', ''),
(212, 54, 2, 6, 120000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 36, 0, '2017-07-01 04:18:14', ''),
(213, 55, 2, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 13, 0, '2017-07-01 04:18:14', ''),
(214, 58, 2, 1, 20000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 4, 0, '2017-07-01 04:18:14', ''),
(215, 101, 2, 2, 40000, 0, 0, 200000, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 20, 0, '2017-07-01 04:18:14', ''),
(216, 113, 2, 2, 40000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 10, 0, '2017-07-01 04:18:14', ''),
(217, 114, 2, 4, 80000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 30, 0, '2017-07-01 04:18:15', ''),
(218, 91, 2, 1, 20000, 0, 0, 0, 18000, 3, 310000, 0, 40000, 0, 30000, '2017-07-31', 23, 0, '2017-07-01 04:18:15', ''),
(219, 95, 2, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 21, 0, '2017-07-01 04:18:15', ''),
(220, 100, 2, 3, 60000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 31, 0, '2017-07-01 04:18:15', ''),
(221, 116, 2, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 19, 0, '2017-07-01 04:18:16', ''),
(222, 115, 2, 2, 40000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 9, 0, '2017-07-01 04:18:16', ''),
(223, 119, 2, 0, 0, 0, 0, 0, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 25, 0, '2017-07-01 04:18:16', ''),
(224, 124, 2, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 18, 0, '2017-07-01 04:18:16', ''),
(225, 125, 2, 7, 140000, 6, 36000, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 12, 0, '2017-07-01 04:18:17', ''),
(226, 97, 2, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 6, 0, '2017-07-01 04:18:17', ''),
(227, 98, 2, 0, 0, 4, 24000, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 7, 0, '2017-07-01 04:18:17', ''),
(228, 103, 2, 6, 120000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 32, 0, '2017-07-01 04:18:17', ''),
(229, 104, 2, 0, 0, 0, 0, 0, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 24, 0, '2017-07-01 04:18:18', ''),
(230, 99, 2, 4, 80000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 8, 0, '2017-07-01 04:18:18', ''),
(231, 106, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 29, 0, '2017-07-01 04:18:18', ''),
(232, 178, 2, 1, 20000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 14, 0, '2017-07-01 04:18:18', ''),
(233, 121, 2, 4, 80000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 33, 0, '2017-07-01 04:18:18', ''),
(234, 110, 2, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 11, 0, '2017-07-01 04:18:19', ''),
(235, 175, 2, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 15, 0, '2017-07-01 04:18:19', ''),
(236, 233, 2, 0, 0, 0, 0, 0, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 35, 0, '2017-07-01 04:18:19', ''),
(237, 232, 2, 0, 0, 0, 0, 200000, 18000, 3, 310000, 0, 40000, 0, 30000, '2017-07-31', 17, 0, '2017-07-01 04:18:19', ''),
(238, 234, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 34, 0, '2017-07-01 04:18:19', ''),
(239, 105, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 1, 0, '2017-07-01 04:18:19', ''),
(240, 107, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 2, 0, '2017-07-01 04:18:20', ''),
(241, 108, 3, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 3, 0, '2017-07-01 04:18:20', ''),
(242, 130, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 4, 0, '2017-07-01 04:18:20', ''),
(243, 131, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 5, 0, '2017-07-01 04:18:20', ''),
(244, 65, 4, 1, 20000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 25, 0, '2017-07-01 04:18:20', ''),
(245, 81, 4, 0, 0, 0, 0, 200000, 18000, 3, 310000, 0, 40000, 0, 30000, '2017-07-31', 18, 0, '2017-07-01 04:18:21', ''),
(246, 71, 4, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 13, 0, '2017-07-01 04:18:21', ''),
(247, 60, 4, 3, 60000, 0, 0, 0, 36000, 6, 310000, 0, 40000, 0, 30000, '2017-07-31', 21, 0, '2017-07-01 04:18:21', ''),
(248, 59, 4, 2, 40000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 23, 0, '2017-07-01 04:18:21', ''),
(249, 79, 4, 1, 20000, 3, 18000, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 2, 0, '2017-07-01 04:18:21', ''),
(250, 62, 4, 3, 60000, 20, 120000, 200000, 36000, 6, 310000, 0, 40000, 0, 30000, '2017-07-31', 11, 0, '2017-07-01 04:18:22', ''),
(251, 74, 4, 1, 20000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 10, 0, '2017-07-01 04:18:22', ''),
(252, 61, 4, 3, 60000, 0, 0, 0, 36000, 6, 310000, 0, 40000, 0, 30000, '2017-07-31', 19, 0, '2017-07-01 04:18:22', ''),
(253, 73, 4, 1, 20000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 24, 0, '2017-07-01 04:18:22', ''),
(254, 68, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 30, 0, '2017-07-01 04:18:23', ''),
(255, 184, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 27, 0, '2017-07-01 04:18:23', ''),
(256, 185, 4, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 8, 0, '2017-07-01 04:18:23', ''),
(257, 182, 4, 0, 0, 1, 6000, 200000, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 15, 0, '2017-07-01 04:18:23', ''),
(258, 187, 4, 1, 20000, 0, 0, 200000, 18000, 3, 310000, 0, 40000, 0, 30000, '2017-07-31', 16, 0, '2017-07-01 04:18:23', ''),
(259, 192, 4, 0, 0, 1, 6000, 200000, 0, 0, 310000, 0, 0, 0, 30000, '2017-07-31', 14, 0, '2017-07-01 04:18:24', ''),
(260, 194, 4, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 5, 0, '2017-07-01 04:18:24', ''),
(261, 197, 4, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 6, 0, '2017-07-01 04:18:24', ''),
(262, 173, 4, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 4, 0, '2017-07-01 04:18:24', ''),
(263, 176, 4, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 12, 0, '2017-07-01 04:18:24', ''),
(264, 172, 4, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 17, 0, '2017-07-01 04:18:25', ''),
(265, 186, 4, 1, 20000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 3, 0, '2017-07-01 04:18:25', ''),
(266, 179, 4, 6, 120000, 0, 0, 0, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 22, 0, '2017-07-01 04:18:25', ''),
(267, 183, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 26, 0, '2017-07-01 04:18:25', ''),
(268, 181, 4, 6, 120000, 3, 18000, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 7, 0, '2017-07-01 04:18:25', ''),
(269, 63, 4, 0, 0, 1, 6000, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 1, 0, '2017-07-01 04:18:26', ''),
(270, 229, 4, 3, 60000, 0, 0, 0, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 20, 0, '2017-07-01 04:18:26', ''),
(271, 228, 4, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 9, 0, '2017-07-01 04:18:26', ''),
(272, 227, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 29, 0, '2017-07-01 04:18:26', ''),
(273, 240, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 28, 0, '2017-07-01 04:18:26', ''),
(274, 76, 5, 1, 20000, 0, 0, 200000, 18000, 3, 310000, 0, 40000, 0, 30000, '2017-07-31', 10, 0, '2017-07-01 04:18:26', ''),
(275, 77, 5, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 9, 0, '2017-07-01 04:18:27', ''),
(276, 78, 5, 4, 80000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 18, 0, '2017-07-01 04:18:27', ''),
(277, 80, 5, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 12, 0, '2017-07-01 04:18:27', ''),
(278, 82, 5, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 4, 0, '2017-07-01 04:18:28', ''),
(279, 83, 5, 1, 20000, 0, 0, 0, 18000, 3, 310000, 0, 40000, 0, 30000, '2017-07-31', 17, 0, '2017-07-01 04:18:28', ''),
(280, 84, 5, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 11, 0, '2017-07-01 04:18:28', ''),
(281, 86, 5, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 0, 0, 30000, '2017-07-31', 8, 0, '2017-07-01 04:18:29', ''),
(282, 1, 5, 2, 40000, 1, 6000, 200000, 0, 0, 310000, 0, 0, 0, 30000, '2017-07-31', 13, 0, '2017-07-01 04:18:29', ''),
(283, 2, 5, 1, 20000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 1, 0, '2017-07-01 04:18:29', ''),
(284, 7, 5, 5, 100000, 3, 18000, 200000, 0, 0, 310000, 0, 0, 0, 30000, '2017-07-31', 16, 0, '2017-07-01 04:18:29', ''),
(285, 9, 5, 3, 60000, 2, 12000, 200000, 36000, 6, 310000, 0, 40000, 0, 30000, '2017-07-31', 14, 0, '2017-07-01 04:18:30', ''),
(286, 13, 5, 4, 80000, 2, 12000, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 3, 0, '2017-07-01 04:18:30', ''),
(287, 206, 5, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, -38000, 30000, '2017-07-31', 6, 0, '2017-07-01 04:18:30', ''),
(288, 226, 5, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 50000, 30000, '2017-07-31', 2, 0, '2017-07-01 04:18:31', ''),
(289, 225, 5, 1, 20000, 1, 6000, 200000, 0, 0, 310000, 0, 0, 0, 30000, '2017-07-31', 15, 0, '2017-07-01 04:18:31', ''),
(290, 224, 5, 5, 100000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 19, 0, '2017-07-01 04:18:31', ''),
(291, 222, 5, 2, 40000, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 7, 0, '2017-07-01 04:18:31', ''),
(292, 235, 5, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 5, 0, '2017-07-01 04:18:31', ''),
(293, 190, 6, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 0, 0, 30000, '2017-07-31', 7, 0, '2017-07-01 04:18:32', ''),
(294, 191, 6, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 3, 0, '2017-07-01 04:18:32', ''),
(295, 193, 6, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 4, 0, '2017-07-01 04:18:32', ''),
(296, 195, 6, 17, 340000, 16, 96000, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 2, 0, '2017-07-01 04:18:32', ''),
(297, 196, 6, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 5, 0, '2017-07-01 04:18:33', ''),
(298, 198, 6, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 40000, 0, 30000, '2017-07-31', 8, 0, '2017-07-01 04:18:33', ''),
(299, 200, 6, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 1, 0, '2017-07-01 04:18:33', ''),
(300, 3, 6, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 9, 0, '2017-07-01 04:18:33', ''),
(301, 4, 6, 1, 20000, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 14, 0, '2017-07-01 04:18:33', ''),
(302, 5, 6, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 10, 0, '2017-07-01 04:18:33', ''),
(303, 6, 6, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 15, 0, '2017-07-01 04:18:34', ''),
(304, 10, 6, 1, 20000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 11, 0, '2017-07-01 04:18:34', ''),
(305, 15, 6, 1, 20000, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 13, 0, '2017-07-01 04:18:34', ''),
(306, 75, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 19, 0, '2017-07-01 04:18:34', ''),
(307, 203, 6, 6, 120000, 0, 0, 0, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 18, 0, '2017-07-01 04:18:34', ''),
(308, 204, 6, 3, 60000, 2, 12000, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 6, 0, '2017-07-01 04:18:35', ''),
(309, 218, 6, 3, 60000, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 17, 0, '2017-07-01 04:18:35', ''),
(310, 238, 6, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 30000, '2017-07-31', 12, 0, '2017-07-01 04:18:35', ''),
(311, 239, 6, 2, 40000, 0, 0, 200000, 0, 0, 0, 0, 40000, 0, 30000, '2017-07-31', 16, 0, '2017-07-01 04:18:35', ''),
(312, 8, 7, 12, 240000, 12, 72000, 200000, 0, 0, 310000, 0, 0, 0, 0, '2017-07-31', 7, 0, '2017-07-01 04:18:35', ''),
(313, 12, 7, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 3, 0, '2017-07-01 04:18:36', ''),
(314, 14, 7, 1, 20000, 0, 0, 200000, 18000, 3, 310000, 0, 0, 0, 0, '2017-07-31', 6, 0, '2017-07-01 04:18:36', ''),
(315, 16, 7, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 1, 0, '2017-07-01 04:18:36', ''),
(316, 17, 7, 0, 0, 0, 0, 0, 0, 0, 310000, 0, 0, 0, 0, '2017-07-31', 13, 0, '2017-07-01 04:18:36', ''),
(317, 18, 7, 3, 60000, 0, 0, 0, 36000, 6, 310000, 0, 0, 0, 0, '2017-07-31', 12, 0, '2017-07-01 04:18:36', ''),
(318, 19, 7, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 2, 0, '2017-07-01 04:18:37', ''),
(319, 20, 7, 0, 0, 2, 12000, 200000, 18000, 3, 310000, 0, 0, 0, 0, '2017-07-31', 8, 0, '2017-07-01 04:18:37', ''),
(320, 217, 7, 7, 140000, 5, 30000, 200000, 72000, 12, 310000, 0, 0, 0, 0, '2017-07-31', 5, 0, '2017-07-01 04:18:37', ''),
(321, 211, 7, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 0, 0, 0, '2017-07-31', 9, 0, '2017-07-01 04:18:37', ''),
(322, 210, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 17, 0, '2017-07-01 04:18:38', ''),
(323, 215, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 19, 0, '2017-07-01 04:18:38', ''),
(324, 214, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 14, 0, '2017-07-01 04:18:38', ''),
(325, 213, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 16, 0, '2017-07-01 04:18:38', ''),
(326, 212, 7, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 0, 0, 0, '2017-07-31', 10, 0, '2017-07-01 04:18:38', ''),
(327, 216, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 18, 0, '2017-07-01 04:18:39', ''),
(328, 220, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 15, 0, '2017-07-01 04:18:39', ''),
(329, 219, 7, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-07-31', 4, 0, '2017-07-01 04:18:39', ''),
(330, 221, 7, 0, 0, 0, 0, 200000, 0, 0, 310000, 0, 0, 0, 0, '2017-07-31', 11, 0, '2017-07-01 04:18:39', ''),
(341, 105, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1250000, 0, '2017-08-31', 1, 0, '2017-08-04 08:13:43', ''),
(342, 107, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1250000, 0, '2017-08-31', 2, 0, '2017-08-04 08:13:44', ''),
(343, 108, 3, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 1450000, 0, '2017-08-31', 3, 0, '2017-08-04 08:13:45', ''),
(344, 130, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1250000, 0, '2017-08-31', 4, 0, '2017-08-04 08:13:46', ''),
(345, 131, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1250000, 0, '2017-08-31', 5, 0, '2017-08-04 08:13:46', ''),
(383, 147, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 25, 0, '2017-08-04 10:18:37', ''),
(384, 148, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 24, 0, '2017-08-04 10:18:38', ''),
(385, 149, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 27, 0, '2017-08-04 10:18:39', ''),
(386, 150, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 26, 0, '2017-08-04 10:18:40', ''),
(387, 151, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 1, 0, '2017-08-04 10:18:41', ''),
(388, 161, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 28, 0, '2017-08-04 10:18:42', ''),
(389, 163, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 3, 0, '2017-08-04 10:18:44', ''),
(390, 164, 1, 0, 0, 0, 0, 0, 0, 0, 200000, 0, 0, 0, 0, '2017-08-31', 23, 0, '2017-08-04 10:18:44', ''),
(391, 165, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 2, 0, '2017-08-04 10:18:45', ''),
(392, 168, 1, 0, 0, 0, 0, 200000, 0, 0, 200000, 0, 0, 0, 0, '2017-08-31', 17, 0, '2017-08-04 10:18:45', ''),
(393, 174, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 33, 0, '2017-08-04 10:18:47', ''),
(394, 188, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 15, 0, '2017-08-04 10:18:48', ''),
(395, 177, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 32, 0, '2017-08-04 10:18:50', ''),
(396, 89, 1, 0, 0, 0, 0, 200000, 0, 0, 200000, 0, 0, 0, 0, '2017-08-31', 18, 0, '2017-08-04 10:18:54', ''),
(397, 102, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 37, 0, '2017-08-04 10:18:55', ''),
(398, 111, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 30, 0, '2017-08-04 10:18:56', ''),
(399, 112, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 31, 0, '2017-08-04 10:18:58', ''),
(400, 122, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 29, 0, '2017-08-04 10:18:59', ''),
(401, 117, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 6, 0, '2017-08-04 10:19:00', ''),
(402, 118, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 7, 0, '2017-08-04 10:19:01', ''),
(403, 123, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 9, 0, '2017-08-04 10:19:03', ''),
(404, 126, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 10, 0, '2017-08-04 10:19:05', ''),
(405, 127, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 8, 0, '2017-08-04 10:19:07', ''),
(406, 128, 1, 0, 0, 0, 0, 0, 0, 0, 200000, 0, 0, 0, 0, '2017-08-31', 22, 0, '2017-08-04 10:19:09', ''),
(407, 129, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 35, 0, '2017-08-04 10:19:11', ''),
(408, 120, 1, 0, 0, 0, 0, 200000, 0, 0, 200000, 0, 0, 0, 0, '2017-08-31', 19, 0, '2017-08-04 10:19:13', ''),
(409, 94, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 14, 0, '2017-08-04 10:19:15', ''),
(410, 96, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 5, 0, '2017-08-04 10:19:17', ''),
(411, 109, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 12, 0, '2017-08-04 10:19:18', ''),
(412, 90, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 36, 0, '2017-08-04 10:19:20', ''),
(413, 64, 1, 0, 0, 0, 0, 200000, 0, 0, 200000, 0, 0, 0, 0, '2017-08-31', 21, 0, '2017-08-04 10:19:21', ''),
(414, 92, 1, 0, 0, 0, 0, 0, 0, 0, 200000, 0, 0, 0, 0, '2017-08-31', 34, 0, '2017-08-04 10:19:22', ''),
(415, 208, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 16, 0, '2017-08-04 10:19:24', ''),
(416, 231, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 4, 0, '2017-08-04 10:19:26', ''),
(417, 230, 1, 0, 0, 0, 0, 200000, 0, 0, 200000, 0, 0, 0, 0, '2017-08-31', 20, 0, '2017-08-04 10:19:27', ''),
(418, 242, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 11, 0, '2017-08-04 10:19:28', ''),
(419, 243, 1, 0, 0, 0, 0, 200000, 0, 0, 0, 0, 0, 0, 0, '2017-08-31', 13, 0, '2017-08-04 10:19:29', '');

-- --------------------------------------------------------

--
-- Table structure for table `bangthutiengenhistory`
--

CREATE TABLE IF NOT EXISTS `bangthutiengenhistory` (
  `BTTGenHistoryId` int(11) NOT NULL AUTO_INCREMENT,
  `NgayTinh` date NOT NULL,
  `LopId` int(11) DEFAULT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`BTTGenHistoryId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=15 ;

--
-- Dumping data for table `bangthutiengenhistory`
--

INSERT INTO `bangthutiengenhistory` (`BTTGenHistoryId`, `NgayTinh`, `LopId`, `DateCreated`) VALUES
(2, '2017-07-31', 1, '2017-07-01 04:18:39'),
(3, '2017-07-31', 2, '2017-07-01 04:18:39'),
(4, '2017-07-31', 3, '2017-07-01 04:18:39'),
(5, '2017-07-31', 4, '2017-07-01 04:18:39'),
(6, '2017-07-31', 5, '2017-07-01 04:18:39'),
(7, '2017-07-31', 6, '2017-07-01 04:18:39'),
(8, '2017-07-31', 7, '2017-07-01 04:18:39'),
(9, '2017-07-31', 10, '2017-07-01 04:18:39'),
(12, '2017-08-31', 3, '2017-08-04 08:13:47'),
(14, '2017-08-31', 1, '2017-08-04 10:19:30');

-- --------------------------------------------------------

--
-- Table structure for table `bangthutien_khoanthu`
--

CREATE TABLE IF NOT EXISTS `bangthutien_khoanthu` (
  `BTTKTId` int(11) NOT NULL AUTO_INCREMENT,
  `KhoanThuId` int(11) NOT NULL,
  `BangThuTienId` int(11) NOT NULL,
  `SoTien` bigint(20) NOT NULL,
  PRIMARY KEY (`BTTKTId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=2515 ;

--
-- Dumping data for table `bangthutien_khoanthu`
--

INSERT INTO `bangthutien_khoanthu` (`BTTKTId`, `KhoanThuId`, `BangThuTienId`, `SoTien`) VALUES
(991, 1, 166, 442000),
(992, 2, 166, 158000),
(993, 3, 166, 260000),
(994, 4, 166, 390000),
(995, 5, 166, 0),
(996, 6, 166, 0),
(997, 1, 167, 442000),
(998, 2, 167, 158000),
(999, 3, 167, 260000),
(1000, 4, 167, 390000),
(1001, 5, 167, 0),
(1002, 6, 167, 0),
(1003, 1, 168, 442000),
(1004, 2, 168, 158000),
(1005, 3, 168, 260000),
(1006, 4, 168, 390000),
(1007, 5, 168, 0),
(1008, 6, 168, 0),
(1009, 1, 169, 442000),
(1010, 2, 169, 158000),
(1011, 3, 169, 260000),
(1012, 4, 169, 390000),
(1013, 5, 169, 0),
(1014, 6, 169, 0),
(1015, 1, 170, 442000),
(1016, 2, 170, 158000),
(1017, 3, 170, 260000),
(1018, 4, 170, 390000),
(1019, 5, 170, 200000),
(1020, 6, 170, 0),
(1021, 1, 171, 442000),
(1022, 2, 171, 158000),
(1023, 3, 171, 260000),
(1024, 4, 171, 390000),
(1025, 5, 171, 0),
(1026, 6, 171, 0),
(1027, 1, 172, 442000),
(1028, 2, 172, 158000),
(1029, 3, 172, 260000),
(1030, 4, 172, 390000),
(1031, 5, 172, 0),
(1032, 6, 172, 0),
(1033, 1, 173, 442000),
(1034, 2, 173, 158000),
(1035, 3, 173, 260000),
(1036, 4, 173, 390000),
(1037, 5, 173, 0),
(1038, 6, 173, 0),
(1039, 1, 174, 442000),
(1040, 2, 174, 158000),
(1041, 3, 174, 260000),
(1042, 4, 174, 390000),
(1043, 5, 174, 0),
(1044, 6, 174, 0),
(1045, 1, 175, 442000),
(1046, 2, 175, 158000),
(1047, 3, 175, 260000),
(1048, 4, 175, 390000),
(1049, 5, 175, 0),
(1050, 6, 175, 0),
(1051, 1, 176, 442000),
(1052, 2, 176, 158000),
(1053, 3, 176, 260000),
(1054, 4, 176, 390000),
(1055, 5, 176, 0),
(1056, 6, 176, 0),
(1057, 1, 177, 442000),
(1058, 2, 177, 158000),
(1059, 3, 177, 260000),
(1060, 4, 177, 390000),
(1061, 5, 177, 0),
(1062, 6, 177, 0),
(1063, 1, 178, 442000),
(1064, 2, 178, 158000),
(1065, 3, 178, 260000),
(1066, 4, 178, 390000),
(1067, 5, 178, 0),
(1068, 6, 178, 0),
(1069, 1, 179, 442000),
(1070, 2, 179, 158000),
(1071, 3, 179, 260000),
(1072, 4, 179, 390000),
(1073, 5, 179, 0),
(1074, 6, 179, 0),
(1075, 1, 180, 390000),
(1076, 2, 180, 120000),
(1077, 3, 180, 240000),
(1078, 4, 180, 320000),
(1079, 5, 180, 0),
(1080, 6, 180, 0),
(1081, 1, 181, 442000),
(1082, 2, 181, 158000),
(1083, 3, 181, 260000),
(1084, 4, 181, 390000),
(1085, 5, 181, 0),
(1086, 6, 181, 0),
(1087, 1, 182, 442000),
(1088, 2, 182, 158000),
(1089, 3, 182, 260000),
(1090, 4, 182, 390000),
(1091, 5, 182, 0),
(1092, 6, 182, 0),
(1093, 1, 183, 442000),
(1094, 2, 183, 158000),
(1095, 3, 183, 260000),
(1096, 4, 183, 390000),
(1097, 5, 183, 0),
(1098, 6, 183, 0),
(1099, 1, 184, 442000),
(1100, 2, 184, 158000),
(1101, 3, 184, 260000),
(1102, 4, 184, 390000),
(1103, 5, 184, 0),
(1104, 6, 184, 0),
(1105, 1, 185, 442000),
(1106, 2, 185, 105000),
(1107, 3, 185, 173000),
(1108, 4, 185, 390000),
(1109, 5, 185, 0),
(1110, 6, 185, 0),
(1111, 1, 186, 442000),
(1112, 2, 186, 158000),
(1113, 3, 186, 260000),
(1114, 4, 186, 390000),
(1115, 5, 186, 0),
(1116, 6, 186, 0),
(1117, 1, 187, 442000),
(1118, 2, 187, 158000),
(1119, 3, 187, 260000),
(1120, 4, 187, 390000),
(1121, 5, 187, 0),
(1122, 6, 187, 0),
(1123, 1, 188, 442000),
(1124, 2, 188, 105000),
(1125, 3, 188, 173000),
(1126, 4, 188, 390000),
(1127, 5, 188, 0),
(1128, 6, 188, 0),
(1129, 1, 189, 442000),
(1130, 2, 189, 158000),
(1131, 3, 189, 260000),
(1132, 4, 189, 390000),
(1133, 5, 189, 0),
(1134, 6, 189, 0),
(1135, 1, 190, 442000),
(1136, 2, 190, 158000),
(1137, 3, 190, 260000),
(1138, 4, 190, 390000),
(1139, 5, 190, 0),
(1140, 6, 190, 0),
(1141, 1, 191, 442000),
(1142, 2, 191, 158000),
(1143, 3, 191, 260000),
(1144, 4, 191, 390000),
(1145, 5, 191, 0),
(1146, 6, 191, 0),
(1147, 1, 192, 442000),
(1148, 2, 192, 158000),
(1149, 3, 192, 260000),
(1150, 4, 192, 390000),
(1151, 5, 192, 0),
(1152, 6, 192, 0),
(1153, 1, 193, 442000),
(1154, 2, 193, 105000),
(1155, 3, 193, 173000),
(1156, 4, 193, 260000),
(1157, 5, 193, 0),
(1158, 6, 193, 0),
(1159, 1, 194, 442000),
(1160, 2, 194, 158000),
(1161, 3, 194, 260000),
(1162, 4, 194, 390000),
(1163, 5, 194, 0),
(1164, 6, 194, 0),
(1165, 1, 195, 442000),
(1166, 2, 195, 158000),
(1167, 3, 195, 260000),
(1168, 4, 195, 390000),
(1169, 5, 195, 0),
(1170, 6, 195, 0),
(1171, 1, 196, 442000),
(1172, 2, 196, 105000),
(1173, 3, 196, 173000),
(1174, 4, 196, 390000),
(1175, 5, 196, 0),
(1176, 6, 196, 0),
(1177, 1, 197, 442000),
(1178, 2, 197, 158000),
(1179, 3, 197, 260000),
(1180, 4, 197, 390000),
(1181, 5, 197, 0),
(1182, 6, 197, 0),
(1183, 1, 198, 442000),
(1184, 2, 198, 158000),
(1185, 3, 198, 260000),
(1186, 4, 198, 390000),
(1187, 5, 198, 0),
(1188, 6, 198, 0),
(1189, 1, 199, 442000),
(1190, 2, 199, 158000),
(1191, 3, 199, 260000),
(1192, 4, 199, 390000),
(1193, 5, 199, 0),
(1194, 6, 199, 0),
(1195, 1, 200, 442000),
(1196, 2, 200, 158000),
(1197, 3, 200, 260000),
(1198, 4, 200, 390000),
(1199, 5, 200, 0),
(1200, 6, 200, 0),
(1201, 1, 201, 442000),
(1202, 2, 201, 158000),
(1203, 3, 201, 260000),
(1204, 4, 201, 390000),
(1205, 5, 201, 0),
(1206, 6, 201, 0),
(1207, 1, 202, 442000),
(1208, 2, 202, 158000),
(1209, 3, 202, 260000),
(1210, 4, 202, 390000),
(1211, 5, 202, 0),
(1212, 6, 202, 0),
(1213, 1, 203, 442000),
(1214, 2, 203, 158000),
(1215, 3, 203, 260000),
(1216, 4, 203, 390000),
(1217, 5, 203, 0),
(1218, 6, 203, 0),
(1219, 1, 204, 442000),
(1220, 2, 204, 158000),
(1221, 3, 204, 260000),
(1222, 4, 204, 390000),
(1223, 5, 204, 0),
(1224, 6, 204, 0),
(1225, 1, 205, 442000),
(1226, 2, 205, 158000),
(1227, 3, 205, 260000),
(1228, 4, 205, 390000),
(1229, 5, 205, 0),
(1230, 6, 205, 0),
(1231, 1, 206, 442000),
(1232, 2, 206, 158000),
(1233, 3, 206, 260000),
(1234, 4, 206, 390000),
(1235, 5, 206, 0),
(1236, 6, 206, 0),
(1237, 1, 207, 442000),
(1238, 2, 207, 158000),
(1239, 3, 207, 260000),
(1240, 4, 207, 390000),
(1241, 5, 207, 0),
(1242, 6, 207, 0),
(1243, 1, 208, 442000),
(1244, 2, 208, 158000),
(1245, 3, 208, 260000),
(1246, 4, 208, 390000),
(1247, 5, 208, 0),
(1248, 6, 208, 0),
(1249, 1, 209, 442000),
(1250, 2, 209, 158000),
(1251, 3, 209, 260000),
(1252, 4, 209, 390000),
(1253, 5, 209, 0),
(1254, 6, 209, 0),
(1255, 1, 210, 442000),
(1256, 2, 210, 158000),
(1257, 3, 210, 260000),
(1258, 4, 210, 390000),
(1259, 5, 210, 0),
(1260, 6, 210, 0),
(1261, 1, 211, 442000),
(1262, 2, 211, 158000),
(1263, 3, 211, 260000),
(1264, 4, 211, 390000),
(1265, 5, 211, 0),
(1266, 6, 211, 0),
(1267, 1, 212, 442000),
(1268, 2, 212, 105000),
(1269, 3, 212, 173000),
(1270, 4, 212, 390000),
(1271, 5, 212, 0),
(1272, 6, 212, 0),
(1273, 1, 213, 442000),
(1274, 2, 213, 158000),
(1275, 3, 213, 260000),
(1276, 4, 213, 390000),
(1277, 5, 213, 0),
(1278, 6, 213, 0),
(1279, 1, 214, 442000),
(1280, 2, 214, 158000),
(1281, 3, 214, 260000),
(1282, 4, 214, 390000),
(1283, 5, 214, 0),
(1284, 6, 214, 0),
(1285, 1, 215, 442000),
(1286, 2, 215, 158000),
(1287, 3, 215, 260000),
(1288, 4, 215, 390000),
(1289, 5, 215, 0),
(1290, 6, 215, 0),
(1291, 1, 216, 442000),
(1292, 2, 216, 158000),
(1293, 3, 216, 260000),
(1294, 4, 216, 390000),
(1295, 5, 216, 0),
(1296, 6, 216, 0),
(1297, 1, 217, 442000),
(1298, 2, 217, 158000),
(1299, 3, 217, 260000),
(1300, 4, 217, 390000),
(1301, 5, 217, 0),
(1302, 6, 217, 0),
(1303, 1, 218, 442000),
(1304, 2, 218, 158000),
(1305, 3, 218, 260000),
(1306, 4, 218, 390000),
(1307, 5, 218, 0),
(1308, 6, 218, 0),
(1309, 1, 219, 442000),
(1310, 2, 219, 158000),
(1311, 3, 219, 260000),
(1312, 4, 219, 390000),
(1313, 5, 219, 0),
(1314, 6, 219, 0),
(1315, 1, 220, 442000),
(1316, 2, 220, 158000),
(1317, 3, 220, 260000),
(1318, 4, 220, 390000),
(1319, 5, 220, 0),
(1320, 6, 220, 0),
(1321, 1, 221, 442000),
(1322, 2, 221, 158000),
(1323, 3, 221, 260000),
(1324, 4, 221, 390000),
(1325, 5, 221, 0),
(1326, 6, 221, 0),
(1327, 1, 222, 442000),
(1328, 2, 222, 158000),
(1329, 3, 222, 260000),
(1330, 4, 222, 390000),
(1331, 5, 222, 0),
(1332, 6, 222, 0),
(1333, 1, 223, 442000),
(1334, 2, 223, 158000),
(1335, 3, 223, 260000),
(1336, 4, 223, 390000),
(1337, 5, 223, 0),
(1338, 6, 223, 0),
(1339, 1, 224, 442000),
(1340, 2, 224, 158000),
(1341, 3, 224, 260000),
(1342, 4, 224, 390000),
(1343, 5, 224, 0),
(1344, 6, 224, 0),
(1345, 1, 225, 442000),
(1346, 2, 225, 105000),
(1347, 3, 225, 173000),
(1348, 4, 225, 390000),
(1349, 5, 225, 0),
(1350, 6, 225, 0),
(1351, 1, 226, 442000),
(1352, 2, 226, 158000),
(1353, 3, 226, 260000),
(1354, 4, 226, 390000),
(1355, 5, 226, 0),
(1356, 6, 226, 0),
(1357, 1, 227, 442000),
(1358, 2, 227, 158000),
(1359, 3, 227, 260000),
(1360, 4, 227, 390000),
(1361, 5, 227, 0),
(1362, 6, 227, 0),
(1363, 1, 228, 442000),
(1364, 2, 228, 105000),
(1365, 3, 228, 173000),
(1366, 4, 228, 390000),
(1367, 5, 228, 0),
(1368, 6, 228, 0),
(1369, 1, 229, 442000),
(1370, 2, 229, 158000),
(1371, 3, 229, 260000),
(1372, 4, 229, 390000),
(1373, 5, 229, 0),
(1374, 6, 229, 0),
(1375, 1, 230, 442000),
(1376, 2, 230, 158000),
(1377, 3, 230, 260000),
(1378, 4, 230, 390000),
(1379, 5, 230, 0),
(1380, 6, 230, 0),
(1381, 1, 231, 442000),
(1382, 2, 231, 158000),
(1383, 3, 231, 260000),
(1384, 4, 231, 390000),
(1385, 5, 231, 0),
(1386, 6, 231, 0),
(1387, 1, 232, 442000),
(1388, 2, 232, 158000),
(1389, 3, 232, 260000),
(1390, 4, 232, 390000),
(1391, 5, 232, 0),
(1392, 6, 232, 0),
(1393, 1, 233, 442000),
(1394, 2, 233, 158000),
(1395, 3, 233, 260000),
(1396, 4, 233, 390000),
(1397, 5, 233, 0),
(1398, 6, 233, 0),
(1399, 1, 234, 442000),
(1400, 2, 234, 158000),
(1401, 3, 234, 260000),
(1402, 4, 234, 390000),
(1403, 5, 234, 0),
(1404, 6, 234, 0),
(1405, 1, 235, 442000),
(1406, 2, 235, 158000),
(1407, 3, 235, 260000),
(1408, 4, 235, 390000),
(1409, 5, 235, 0),
(1410, 6, 235, 0),
(1411, 1, 236, 442000),
(1412, 2, 236, 158000),
(1413, 3, 236, 260000),
(1414, 4, 236, 390000),
(1415, 5, 236, 0),
(1416, 6, 236, 0),
(1417, 1, 237, 442000),
(1418, 2, 237, 158000),
(1419, 3, 237, 260000),
(1420, 4, 237, 390000),
(1421, 5, 237, 0),
(1422, 6, 237, 0),
(1423, 1, 238, 442000),
(1424, 2, 238, 158000),
(1425, 3, 238, 260000),
(1426, 4, 238, 390000),
(1427, 5, 238, 0),
(1428, 6, 238, 0),
(1429, 1, 239, 442000),
(1430, 2, 239, 158000),
(1431, 3, 239, 260000),
(1432, 4, 239, 390000),
(1433, 5, 239, 0),
(1434, 6, 239, 0),
(1435, 1, 240, 442000),
(1436, 2, 240, 158000),
(1437, 3, 240, 260000),
(1438, 4, 240, 390000),
(1439, 5, 240, 0),
(1440, 6, 240, 0),
(1441, 1, 241, 442000),
(1442, 2, 241, 158000),
(1443, 3, 241, 260000),
(1444, 4, 241, 390000),
(1445, 5, 241, 0),
(1446, 6, 241, 0),
(1447, 1, 242, 442000),
(1448, 2, 242, 158000),
(1449, 3, 242, 260000),
(1450, 4, 242, 390000),
(1451, 5, 242, 0),
(1452, 6, 242, 0),
(1453, 1, 243, 442000),
(1454, 2, 243, 158000),
(1455, 3, 243, 260000),
(1456, 4, 243, 390000),
(1457, 5, 243, 0),
(1458, 6, 243, 0),
(1459, 1, 244, 442000),
(1460, 2, 244, 158000),
(1461, 3, 244, 260000),
(1462, 4, 244, 390000),
(1463, 5, 244, 0),
(1464, 6, 244, 0),
(1465, 1, 245, 442000),
(1466, 2, 245, 158000),
(1467, 3, 245, 260000),
(1468, 4, 245, 390000),
(1469, 5, 245, 0),
(1470, 6, 245, 0),
(1471, 1, 246, 442000),
(1472, 2, 246, 158000),
(1473, 3, 246, 260000),
(1474, 4, 246, 390000),
(1475, 5, 246, 0),
(1476, 6, 246, 0),
(1477, 1, 247, 442000),
(1478, 2, 247, 158000),
(1479, 3, 247, 260000),
(1480, 4, 247, 390000),
(1481, 5, 247, 0),
(1482, 6, 247, 0),
(1483, 1, 248, 442000),
(1484, 2, 248, 158000),
(1485, 3, 248, 260000),
(1486, 4, 248, 390000),
(1487, 5, 248, 0),
(1488, 6, 248, 0),
(1489, 1, 249, 442000),
(1490, 2, 249, 158000),
(1491, 3, 249, 260000),
(1492, 4, 249, 390000),
(1493, 5, 249, 0),
(1494, 6, 249, 0),
(1495, 1, 250, 442000),
(1496, 2, 250, 158000),
(1497, 3, 250, 260000),
(1498, 4, 250, 390000),
(1499, 5, 250, 0),
(1500, 6, 250, 0),
(1501, 1, 251, 442000),
(1502, 2, 251, 158000),
(1503, 3, 251, 260000),
(1504, 4, 251, 390000),
(1505, 5, 251, 0),
(1506, 6, 251, 0),
(1507, 1, 252, 442000),
(1508, 2, 252, 158000),
(1509, 3, 252, 260000),
(1510, 4, 252, 390000),
(1511, 5, 252, 0),
(1512, 6, 252, 0),
(1513, 1, 253, 442000),
(1514, 2, 253, 158000),
(1515, 3, 253, 260000),
(1516, 4, 253, 390000),
(1517, 5, 253, 0),
(1518, 6, 253, 0),
(1519, 1, 254, 442000),
(1520, 2, 254, 158000),
(1521, 3, 254, 260000),
(1522, 4, 254, 390000),
(1523, 5, 254, 0),
(1524, 6, 254, 0),
(1525, 1, 255, 442000),
(1526, 2, 255, 158000),
(1527, 3, 255, 260000),
(1528, 4, 255, 390000),
(1529, 5, 255, 0),
(1530, 6, 255, 0),
(1531, 1, 256, 442000),
(1532, 2, 256, 158000),
(1533, 3, 256, 260000),
(1534, 4, 256, 390000),
(1535, 5, 256, 0),
(1536, 6, 256, 0),
(1537, 1, 257, 442000),
(1538, 2, 257, 158000),
(1539, 3, 257, 260000),
(1540, 4, 257, 390000),
(1541, 5, 257, 0),
(1542, 6, 257, 0),
(1543, 1, 258, 442000),
(1544, 2, 258, 158000),
(1545, 3, 258, 260000),
(1546, 4, 258, 390000),
(1547, 5, 258, 0),
(1548, 6, 258, 0),
(1549, 1, 259, 442000),
(1550, 2, 259, 158000),
(1551, 3, 259, 260000),
(1552, 4, 259, 390000),
(1553, 5, 259, 0),
(1554, 6, 259, 0),
(1555, 1, 260, 442000),
(1556, 2, 260, 158000),
(1557, 3, 260, 260000),
(1558, 4, 260, 390000),
(1559, 5, 260, 0),
(1560, 6, 260, 0),
(1561, 1, 261, 442000),
(1562, 2, 261, 158000),
(1563, 3, 261, 260000),
(1564, 4, 261, 390000),
(1565, 5, 261, 0),
(1566, 6, 261, 0),
(1567, 1, 262, 442000),
(1568, 2, 262, 158000),
(1569, 3, 262, 260000),
(1570, 4, 262, 390000),
(1571, 5, 262, 0),
(1572, 6, 262, 0),
(1573, 1, 263, 442000),
(1574, 2, 263, 158000),
(1575, 3, 263, 260000),
(1576, 4, 263, 390000),
(1577, 5, 263, 0),
(1578, 6, 263, 0),
(1579, 1, 264, 442000),
(1580, 2, 264, 158000),
(1581, 3, 264, 260000),
(1582, 4, 264, 390000),
(1583, 5, 264, 0),
(1584, 6, 264, 0),
(1585, 1, 265, 442000),
(1586, 2, 265, 158000),
(1587, 3, 265, 260000),
(1588, 4, 265, 390000),
(1589, 5, 265, 0),
(1590, 6, 265, 0),
(1591, 1, 266, 442000),
(1592, 2, 266, 105000),
(1593, 3, 266, 173000),
(1594, 4, 266, 390000),
(1595, 5, 266, 0),
(1596, 6, 266, 0),
(1597, 1, 267, 442000),
(1598, 2, 267, 158000),
(1599, 3, 267, 260000),
(1600, 4, 267, 390000),
(1601, 5, 267, 0),
(1602, 6, 267, 0),
(1603, 1, 268, 442000),
(1604, 2, 268, 105000),
(1605, 3, 268, 173000),
(1606, 4, 268, 390000),
(1607, 5, 268, 0),
(1608, 6, 268, 0),
(1609, 1, 269, 442000),
(1610, 2, 269, 158000),
(1611, 3, 269, 260000),
(1612, 4, 269, 390000),
(1613, 5, 269, 0),
(1614, 6, 269, 0),
(1615, 1, 270, 442000),
(1616, 2, 270, 158000),
(1617, 3, 270, 260000),
(1618, 4, 270, 390000),
(1619, 5, 270, 0),
(1620, 6, 270, 0),
(1621, 1, 271, 442000),
(1622, 2, 271, 158000),
(1623, 3, 271, 260000),
(1624, 4, 271, 390000),
(1625, 5, 271, 0),
(1626, 6, 271, 0),
(1627, 1, 272, 442000),
(1628, 2, 272, 158000),
(1629, 3, 272, 260000),
(1630, 4, 272, 390000),
(1631, 5, 272, 0),
(1632, 6, 272, 0),
(1633, 1, 273, 442000),
(1634, 2, 273, 158000),
(1635, 3, 273, 260000),
(1636, 4, 273, 390000),
(1637, 5, 273, 0),
(1638, 6, 273, 0),
(1639, 1, 274, 442000),
(1640, 2, 274, 183000),
(1641, 3, 274, 265000),
(1642, 4, 274, 410000),
(1643, 5, 274, 0),
(1644, 6, 274, 0),
(1645, 1, 275, 442000),
(1646, 2, 275, 183000),
(1647, 3, 275, 265000),
(1648, 4, 275, 410000),
(1649, 5, 275, 0),
(1650, 6, 275, 0),
(1651, 1, 276, 442000),
(1652, 2, 276, 183000),
(1653, 3, 276, 265000),
(1654, 4, 276, 410000),
(1655, 5, 276, 0),
(1656, 6, 276, 0),
(1657, 1, 277, 442000),
(1658, 2, 277, 183000),
(1659, 3, 277, 265000),
(1660, 4, 277, 410000),
(1661, 5, 277, 0),
(1662, 6, 277, 0),
(1663, 1, 278, 442000),
(1664, 2, 278, 183000),
(1665, 3, 278, 265000),
(1666, 4, 278, 410000),
(1667, 5, 278, 0),
(1668, 6, 278, 0),
(1669, 1, 279, 442000),
(1670, 2, 279, 183000),
(1671, 3, 279, 265000),
(1672, 4, 279, 410000),
(1673, 5, 279, 0),
(1674, 6, 279, 0),
(1675, 1, 280, 442000),
(1676, 2, 280, 183000),
(1677, 3, 280, 265000),
(1678, 4, 280, 410000),
(1679, 5, 280, 0),
(1680, 6, 280, 0),
(1681, 1, 281, 442000),
(1682, 2, 281, 183000),
(1683, 3, 281, 265000),
(1684, 4, 281, 410000),
(1685, 5, 281, 0),
(1686, 6, 281, 0),
(1687, 1, 282, 442000),
(1688, 2, 282, 183000),
(1689, 3, 282, 265000),
(1690, 4, 282, 410000),
(1691, 5, 282, 0),
(1692, 6, 282, 0),
(1693, 1, 283, 442000),
(1694, 2, 283, 183000),
(1695, 3, 283, 265000),
(1696, 4, 283, 410000),
(1697, 5, 283, 0),
(1698, 6, 283, 0),
(1699, 1, 284, 442000),
(1700, 2, 284, 183000),
(1701, 3, 284, 265000),
(1702, 4, 284, 410000),
(1703, 5, 284, 0),
(1704, 6, 284, 0),
(1705, 1, 285, 442000),
(1706, 2, 285, 183000),
(1707, 3, 285, 265000),
(1708, 4, 285, 410000),
(1709, 5, 285, 0),
(1710, 6, 285, 0),
(1711, 1, 286, 442000),
(1712, 2, 286, 183000),
(1713, 3, 286, 265000),
(1714, 4, 286, 410000),
(1715, 5, 286, 0),
(1716, 6, 286, 0),
(1717, 1, 287, 442000),
(1718, 2, 287, 183000),
(1719, 3, 287, 265000),
(1720, 4, 287, 410000),
(1721, 5, 287, 0),
(1722, 6, 287, 0),
(1723, 1, 288, 442000),
(1724, 2, 288, 183000),
(1725, 3, 288, 265000),
(1726, 4, 288, 410000),
(1727, 5, 288, 0),
(1728, 6, 288, 0),
(1729, 1, 289, 442000),
(1730, 2, 289, 183000),
(1731, 3, 289, 265000),
(1732, 4, 289, 410000),
(1733, 5, 289, 0),
(1734, 6, 289, 0),
(1735, 1, 290, 442000),
(1736, 2, 290, 183000),
(1737, 3, 290, 265000),
(1738, 4, 290, 410000),
(1739, 5, 290, 0),
(1740, 6, 290, 0),
(1741, 1, 291, 442000),
(1742, 2, 291, 183000),
(1743, 3, 291, 265000),
(1744, 4, 291, 410000),
(1745, 5, 291, 0),
(1746, 6, 291, 0),
(1747, 1, 292, 442000),
(1748, 2, 292, 183000),
(1749, 3, 292, 265000),
(1750, 4, 292, 410000),
(1751, 5, 292, 0),
(1752, 6, 292, 0),
(1753, 1, 293, 442000),
(1754, 2, 293, 183000),
(1755, 3, 293, 265000),
(1756, 4, 293, 410000),
(1757, 5, 293, 0),
(1758, 6, 293, 0),
(1759, 1, 294, 442000),
(1760, 2, 294, 183000),
(1761, 3, 294, 265000),
(1762, 4, 294, 410000),
(1763, 5, 294, 0),
(1764, 6, 294, 0),
(1765, 1, 295, 442000),
(1766, 2, 295, 183000),
(1767, 3, 295, 265000),
(1768, 4, 295, 410000),
(1769, 5, 295, 0),
(1770, 6, 295, 0),
(1771, 1, 296, 442000),
(1772, 2, 296, 91500),
(1773, 3, 296, 132500),
(1774, 4, 296, 205000),
(1775, 5, 296, 0),
(1776, 6, 296, 0),
(1777, 1, 297, 442000),
(1778, 2, 297, 183000),
(1779, 3, 297, 265000),
(1780, 4, 297, 410000),
(1781, 5, 297, 0),
(1782, 6, 297, 0),
(1783, 1, 298, 442000),
(1784, 2, 298, 183000),
(1785, 3, 298, 265000),
(1786, 4, 298, 410000),
(1787, 5, 298, 0),
(1788, 6, 298, 0),
(1789, 1, 299, 442000),
(1790, 2, 299, 183000),
(1791, 3, 299, 265000),
(1792, 4, 299, 410000),
(1793, 5, 299, 0),
(1794, 6, 299, 0),
(1795, 1, 300, 442000),
(1796, 2, 300, 183000),
(1797, 3, 300, 265000),
(1798, 4, 300, 410000),
(1799, 5, 300, 0),
(1800, 6, 300, 0),
(1801, 1, 301, 442000),
(1802, 2, 301, 183000),
(1803, 3, 301, 265000),
(1804, 4, 301, 410000),
(1805, 5, 301, 0),
(1806, 6, 301, 0),
(1807, 1, 302, 442000),
(1808, 2, 302, 183000),
(1809, 3, 302, 265000),
(1810, 4, 302, 410000),
(1811, 5, 302, 0),
(1812, 6, 302, 0),
(1813, 1, 303, 442000),
(1814, 2, 303, 183000),
(1815, 3, 303, 265000),
(1816, 4, 303, 410000),
(1817, 5, 303, 0),
(1818, 6, 303, 0),
(1819, 1, 304, 442000),
(1820, 2, 304, 183000),
(1821, 3, 304, 265000),
(1822, 4, 304, 410000),
(1823, 5, 304, 0),
(1824, 6, 304, 0),
(1825, 1, 305, 442000),
(1826, 2, 305, 183000),
(1827, 3, 305, 265000),
(1828, 4, 305, 410000),
(1829, 5, 305, 0),
(1830, 6, 305, 0),
(1831, 1, 306, 442000),
(1832, 2, 306, 183000),
(1833, 3, 306, 265000),
(1834, 4, 306, 410000),
(1835, 5, 306, 0),
(1836, 6, 306, 0),
(1837, 1, 307, 442000),
(1838, 2, 307, 122000),
(1839, 3, 307, 177000),
(1840, 4, 307, 410000),
(1841, 5, 307, 0),
(1842, 6, 307, 0),
(1843, 1, 308, 442000),
(1844, 2, 308, 183000),
(1845, 3, 308, 265000),
(1846, 4, 308, 410000),
(1847, 5, 308, 0),
(1848, 6, 308, 0),
(1849, 1, 309, 442000),
(1850, 2, 309, 183000),
(1851, 3, 309, 265000),
(1852, 4, 309, 410000),
(1853, 5, 309, 0),
(1854, 6, 309, 0),
(1855, 1, 310, 442000),
(1856, 2, 310, 183000),
(1857, 3, 310, 265000),
(1858, 4, 310, 410000),
(1859, 5, 310, 0),
(1860, 6, 310, 0),
(1861, 1, 311, 442000),
(1862, 2, 311, 183000),
(1863, 3, 311, 265000),
(1864, 4, 311, 410000),
(1865, 5, 311, 0),
(1866, 6, 311, 0),
(1867, 1, 312, 442000),
(1868, 2, 312, 132000),
(1869, 3, 312, 187000),
(1870, 4, 312, 287000),
(1871, 5, 312, 0),
(1872, 6, 312, 0),
(1873, 1, 313, 442000),
(1874, 2, 313, 198000),
(1875, 3, 313, 280000),
(1876, 4, 313, 430000),
(1877, 5, 313, 0),
(1878, 6, 313, 0),
(1879, 1, 314, 442000),
(1880, 2, 314, 198000),
(1881, 3, 314, 280000),
(1882, 4, 314, 430000),
(1883, 5, 314, 0),
(1884, 6, 314, 0),
(1885, 1, 315, 442000),
(1886, 2, 315, 198000),
(1887, 3, 315, 280000),
(1888, 4, 315, 430000),
(1889, 5, 315, 0),
(1890, 6, 315, 0),
(1891, 1, 316, 442000),
(1892, 2, 316, 198000),
(1893, 3, 316, 280000),
(1894, 4, 316, 430000),
(1895, 5, 316, 0),
(1896, 6, 316, 0),
(1897, 1, 317, 442000),
(1898, 2, 317, 198000),
(1899, 3, 317, 280000),
(1900, 4, 317, 430000),
(1901, 5, 317, 0),
(1902, 6, 317, 0),
(1903, 1, 318, 442000),
(1904, 2, 318, 198000),
(1905, 3, 318, 280000),
(1906, 4, 318, 430000),
(1907, 5, 318, 0),
(1908, 6, 318, 0),
(1909, 1, 319, 442000),
(1910, 2, 319, 198000),
(1911, 3, 319, 280000),
(1912, 4, 319, 430000),
(1913, 5, 319, 0),
(1914, 6, 319, 0),
(1915, 1, 320, 442000),
(1916, 2, 320, 132000),
(1917, 3, 320, 187000),
(1918, 4, 320, 430000),
(1919, 5, 320, 0),
(1920, 6, 320, 0),
(1921, 1, 321, 442000),
(1922, 2, 321, 198000),
(1923, 3, 321, 280000),
(1924, 4, 321, 430000),
(1925, 5, 321, 0),
(1926, 6, 321, 0),
(1927, 1, 322, 442000),
(1928, 2, 322, 198000),
(1929, 3, 322, 280000),
(1930, 4, 322, 430000),
(1931, 5, 322, 0),
(1932, 6, 322, 0),
(1933, 1, 323, 442000),
(1934, 2, 323, 198000),
(1935, 3, 323, 280000),
(1936, 4, 323, 430000),
(1937, 5, 323, 0),
(1938, 6, 323, 0),
(1939, 1, 324, 442000),
(1940, 2, 324, 198000),
(1941, 3, 324, 280000),
(1942, 4, 324, 430000),
(1943, 5, 324, 0),
(1944, 6, 324, 0),
(1945, 1, 325, 442000),
(1946, 2, 325, 198000),
(1947, 3, 325, 280000),
(1948, 4, 325, 430000),
(1949, 5, 325, 0),
(1950, 6, 325, 0),
(1951, 1, 326, 442000),
(1952, 2, 326, 198000),
(1953, 3, 326, 280000),
(1954, 4, 326, 430000),
(1955, 5, 326, 0),
(1956, 6, 326, 0),
(1957, 1, 327, 442000),
(1958, 2, 327, 198000),
(1959, 3, 327, 280000),
(1960, 4, 327, 430000),
(1961, 5, 327, 0),
(1962, 6, 327, 0),
(1963, 1, 328, 442000),
(1964, 2, 328, 198000),
(1965, 3, 328, 280000),
(1966, 4, 328, 430000),
(1967, 5, 328, 0),
(1968, 6, 328, 0),
(1969, 1, 329, 442000),
(1970, 2, 329, 198000),
(1971, 3, 329, 280000),
(1972, 4, 329, 430000),
(1973, 5, 329, 0),
(1974, 6, 329, 0),
(1975, 1, 330, 442000),
(1976, 2, 330, 198000),
(1977, 3, 330, 280000),
(1978, 4, 330, 430000),
(1979, 5, 330, 0),
(1980, 6, 330, 0),
(1981, 1, 331, 442000),
(1982, 2, 331, 158000),
(1983, 3, 331, 260000),
(1984, 4, 331, 390000),
(1985, 5, 331, 0),
(1986, 6, 331, 0),
(1987, 1, 332, 442000),
(1988, 2, 332, 158000),
(1989, 3, 332, 260000),
(1990, 4, 332, 390000),
(1991, 5, 332, 0),
(1992, 6, 332, 0),
(1993, 1, 333, 442000),
(1994, 2, 333, 158000),
(1995, 3, 333, 260000),
(1996, 4, 333, 390000),
(1997, 5, 333, 200000),
(1998, 6, 333, 0),
(1999, 1, 334, 442000),
(2000, 2, 334, 158000),
(2001, 3, 334, 260000),
(2002, 4, 334, 390000),
(2003, 5, 334, 0),
(2004, 6, 334, 0),
(2005, 1, 335, 442000),
(2006, 2, 335, 158000),
(2007, 3, 335, 260000),
(2008, 4, 335, 390000),
(2009, 5, 335, 0),
(2010, 6, 335, 0),
(2041, 1, 341, 442000),
(2042, 2, 341, 158000),
(2043, 3, 341, 260000),
(2044, 4, 341, 390000),
(2045, 5, 341, 0),
(2046, 6, 341, 0),
(2047, 1, 342, 442000),
(2048, 2, 342, 158000),
(2049, 3, 342, 260000),
(2050, 4, 342, 390000),
(2051, 5, 342, 0),
(2052, 6, 342, 0),
(2053, 1, 343, 442000),
(2054, 2, 343, 158000),
(2055, 3, 343, 260000),
(2056, 4, 343, 390000),
(2057, 5, 343, 200000),
(2058, 6, 343, 0),
(2059, 1, 344, 442000),
(2060, 2, 344, 158000),
(2061, 3, 344, 260000),
(2062, 4, 344, 390000),
(2063, 5, 344, 0),
(2064, 6, 344, 0),
(2065, 1, 345, 442000),
(2066, 2, 345, 158000),
(2067, 3, 345, 260000),
(2068, 4, 345, 390000),
(2069, 5, 345, 0),
(2070, 6, 345, 0),
(2293, 1, 383, 442000),
(2294, 2, 383, 158000),
(2295, 3, 383, 260000),
(2296, 4, 383, 390000),
(2297, 5, 383, 0),
(2298, 6, 383, 0),
(2299, 1, 384, 442000),
(2300, 2, 384, 158000),
(2301, 3, 384, 260000),
(2302, 4, 384, 390000),
(2303, 5, 384, 0),
(2304, 6, 384, 0),
(2305, 1, 385, 442000),
(2306, 2, 385, 158000),
(2307, 3, 385, 260000),
(2308, 4, 385, 390000),
(2309, 5, 385, 0),
(2310, 6, 385, 0),
(2311, 1, 386, 442000),
(2312, 2, 386, 158000),
(2313, 3, 386, 260000),
(2314, 4, 386, 390000),
(2315, 5, 386, 0),
(2316, 6, 386, 0),
(2317, 1, 387, 442000),
(2318, 2, 387, 158000),
(2319, 3, 387, 260000),
(2320, 4, 387, 390000),
(2321, 5, 387, 200000),
(2322, 6, 387, 0),
(2323, 1, 388, 442000),
(2324, 2, 388, 158000),
(2325, 3, 388, 260000),
(2326, 4, 388, 390000),
(2327, 5, 388, 0),
(2328, 6, 388, 0),
(2329, 1, 389, 442000),
(2330, 2, 389, 158000),
(2331, 3, 389, 260000),
(2332, 4, 389, 390000),
(2333, 5, 389, 200000),
(2334, 6, 389, 0),
(2335, 1, 390, 442000),
(2336, 2, 390, 158000),
(2337, 3, 390, 260000),
(2338, 4, 390, 390000),
(2339, 5, 390, 0),
(2340, 6, 390, 200000),
(2341, 1, 391, 442000),
(2342, 2, 391, 158000),
(2343, 3, 391, 260000),
(2344, 4, 391, 390000),
(2345, 5, 391, 200000),
(2346, 6, 391, 0),
(2347, 1, 392, 442000),
(2348, 2, 392, 158000),
(2349, 3, 392, 260000),
(2350, 4, 392, 390000),
(2351, 5, 392, 200000),
(2352, 6, 392, 200000),
(2353, 1, 393, 442000),
(2354, 2, 393, 158000),
(2355, 3, 393, 260000),
(2356, 4, 393, 390000),
(2357, 5, 393, 0),
(2358, 6, 393, 0),
(2359, 1, 394, 442000),
(2360, 2, 394, 158000),
(2361, 3, 394, 260000),
(2362, 4, 394, 390000),
(2363, 5, 394, 200000),
(2364, 6, 394, 0),
(2365, 1, 395, 442000),
(2366, 2, 395, 158000),
(2367, 3, 395, 260000),
(2368, 4, 395, 390000),
(2369, 5, 395, 0),
(2370, 6, 395, 0),
(2371, 1, 396, 442000),
(2372, 2, 396, 158000),
(2373, 3, 396, 260000),
(2374, 4, 396, 390000),
(2375, 5, 396, 200000),
(2376, 6, 396, 200000),
(2377, 1, 397, 442000),
(2378, 2, 397, 158000),
(2379, 3, 397, 260000),
(2380, 4, 397, 390000),
(2381, 5, 397, 0),
(2382, 6, 397, 0),
(2383, 1, 398, 442000),
(2384, 2, 398, 158000),
(2385, 3, 398, 260000),
(2386, 4, 398, 390000),
(2387, 5, 398, 0),
(2388, 6, 398, 0),
(2389, 1, 399, 442000),
(2390, 2, 399, 158000),
(2391, 3, 399, 260000),
(2392, 4, 399, 390000),
(2393, 5, 399, 0),
(2394, 6, 399, 0),
(2395, 1, 400, 442000),
(2396, 2, 400, 158000),
(2397, 3, 400, 260000),
(2398, 4, 400, 390000),
(2399, 5, 400, 0),
(2400, 6, 400, 0),
(2401, 1, 401, 442000),
(2402, 2, 401, 158000),
(2403, 3, 401, 260000),
(2404, 4, 401, 390000),
(2405, 5, 401, 200000),
(2406, 6, 401, 0),
(2407, 1, 402, 442000),
(2408, 2, 402, 158000),
(2409, 3, 402, 260000),
(2410, 4, 402, 390000),
(2411, 5, 402, 200000),
(2412, 6, 402, 0),
(2413, 1, 403, 442000),
(2414, 2, 403, 158000),
(2415, 3, 403, 260000),
(2416, 4, 403, 390000),
(2417, 5, 403, 200000),
(2418, 6, 403, 0),
(2419, 1, 404, 442000),
(2420, 2, 404, 158000),
(2421, 3, 404, 260000),
(2422, 4, 404, 390000),
(2423, 5, 404, 200000),
(2424, 6, 404, 0),
(2425, 1, 405, 442000),
(2426, 2, 405, 158000),
(2427, 3, 405, 260000),
(2428, 4, 405, 390000),
(2429, 5, 405, 200000),
(2430, 6, 405, 0),
(2431, 1, 406, 442000),
(2432, 2, 406, 158000),
(2433, 3, 406, 260000),
(2434, 4, 406, 390000),
(2435, 5, 406, 0),
(2436, 6, 406, 200000),
(2437, 1, 407, 442000),
(2438, 2, 407, 158000),
(2439, 3, 407, 260000),
(2440, 4, 407, 390000),
(2441, 5, 407, 0),
(2442, 6, 407, 0),
(2443, 1, 408, 442000),
(2444, 2, 408, 158000),
(2445, 3, 408, 260000),
(2446, 4, 408, 390000),
(2447, 5, 408, 200000),
(2448, 6, 408, 200000),
(2449, 1, 409, 442000),
(2450, 2, 409, 158000),
(2451, 3, 409, 260000),
(2452, 4, 409, 390000),
(2453, 5, 409, 200000),
(2454, 6, 409, 0),
(2455, 1, 410, 442000),
(2456, 2, 410, 158000),
(2457, 3, 410, 260000),
(2458, 4, 410, 390000),
(2459, 5, 410, 200000),
(2460, 6, 410, 0),
(2461, 1, 411, 442000),
(2462, 2, 411, 158000),
(2463, 3, 411, 260000),
(2464, 4, 411, 390000),
(2465, 5, 411, 200000),
(2466, 6, 411, 0),
(2467, 1, 412, 442000),
(2468, 2, 412, 158000),
(2469, 3, 412, 260000),
(2470, 4, 412, 390000),
(2471, 5, 412, 0),
(2472, 6, 412, 0),
(2473, 1, 413, 442000),
(2474, 2, 413, 158000),
(2475, 3, 413, 260000),
(2476, 4, 413, 390000),
(2477, 5, 413, 200000),
(2478, 6, 413, 200000),
(2479, 1, 414, 442000),
(2480, 2, 414, 158000),
(2481, 3, 414, 260000),
(2482, 4, 414, 390000),
(2483, 5, 414, 0),
(2484, 6, 414, 200000),
(2485, 1, 415, 442000),
(2486, 2, 415, 158000),
(2487, 3, 415, 260000),
(2488, 4, 415, 390000),
(2489, 5, 415, 200000),
(2490, 6, 415, 0),
(2491, 1, 416, 442000),
(2492, 2, 416, 158000),
(2493, 3, 416, 260000),
(2494, 4, 416, 390000),
(2495, 5, 416, 200000),
(2496, 6, 416, 0),
(2497, 1, 417, 442000),
(2498, 2, 417, 158000),
(2499, 3, 417, 260000),
(2500, 4, 417, 390000),
(2501, 5, 417, 200000),
(2502, 6, 417, 200000),
(2503, 1, 418, 442000),
(2504, 2, 418, 158000),
(2505, 3, 418, 260000),
(2506, 4, 418, 390000),
(2507, 5, 418, 200000),
(2508, 6, 418, 0),
(2509, 1, 419, 442000),
(2510, 2, 419, 158000),
(2511, 3, 419, 260000),
(2512, 4, 419, 390000),
(2513, 5, 419, 200000),
(2514, 6, 419, 0);

-- --------------------------------------------------------

--
-- Table structure for table `bangtinhphi`
--

CREATE TABLE IF NOT EXISTS `bangtinhphi` (
  `BangTinhPhiId` int(11) NOT NULL AUTO_INCREMENT,
  `SoNgayNghiMin` int(11) NOT NULL,
  `SoNgayNghiMax` int(11) NOT NULL,
  `SoTien` bigint(20) NOT NULL,
  `KhoanThuId` int(11) NOT NULL,
  `KhoiId` int(11) NOT NULL,
  PRIMARY KEY (`BangTinhPhiId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=37 ;

--
-- Dumping data for table `bangtinhphi`
--

INSERT INTO `bangtinhphi` (`BangTinhPhiId`, `SoNgayNghiMin`, `SoNgayNghiMax`, `SoTien`, `KhoanThuId`, `KhoiId`) VALUES
(1, 6, 8, 105000, 2, 1),
(2, 9, 12, 105000, 2, 1),
(3, 13, 18, 79000, 2, 1),
(4, 6, 8, 173000, 3, 1),
(5, 9, 12, 173000, 3, 1),
(6, 13, 18, 130000, 3, 1),
(7, 6, 8, 390000, 4, 1),
(8, 9, 12, 260000, 4, 1),
(9, 13, 18, 195000, 4, 1),
(10, 6, 8, 105000, 2, 2),
(11, 9, 12, 105000, 2, 2),
(12, 13, 18, 79000, 2, 2),
(13, 6, 8, 173000, 3, 2),
(14, 9, 12, 173000, 3, 2),
(15, 13, 18, 130000, 3, 2),
(16, 6, 8, 390000, 4, 2),
(17, 9, 12, 260000, 4, 2),
(18, 13, 18, 195000, 4, 2),
(19, 6, 8, 122000, 2, 3),
(20, 9, 12, 122000, 2, 3),
(21, 13, 18, 91500, 2, 3),
(22, 6, 8, 177000, 3, 3),
(23, 9, 12, 177000, 3, 3),
(24, 13, 18, 132500, 3, 3),
(25, 6, 8, 410000, 4, 3),
(26, 9, 12, 273000, 4, 3),
(27, 13, 18, 205000, 4, 3),
(28, 6, 8, 132000, 2, 4),
(29, 9, 12, 132000, 2, 4),
(30, 13, 18, 9900, 2, 4),
(31, 6, 8, 187000, 3, 4),
(32, 9, 12, 187000, 3, 4),
(33, 13, 18, 140000, 3, 4),
(34, 6, 8, 430000, 4, 4),
(35, 9, 12, 287000, 4, 4),
(36, 13, 18, 215000, 4, 4);

-- --------------------------------------------------------

--
-- Table structure for table `baocaothang`
--

CREATE TABLE IF NOT EXISTS `baocaothang` (
  `BaoCaoThangId` int(11) NOT NULL AUTO_INCREMENT,
  `NgayTinh` date NOT NULL,
  `HocTapId` int(11) NOT NULL,
  `SoNgayNghi` int(11) NOT NULL,
  PRIMARY KEY (`BaoCaoThangId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `baocaothanggenhistory`
--

CREATE TABLE IF NOT EXISTS `baocaothanggenhistory` (
  `BCTGenHistoryId` int(11) NOT NULL AUTO_INCREMENT,
  `LopId` int(11) NOT NULL,
  `NgayTinh` date NOT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`BCTGenHistoryId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `giaovien`
--

CREATE TABLE IF NOT EXISTS `giaovien` (
  `GiaoVienId` int(11) NOT NULL,
  `HoTen` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `GioiTinh` smallint(6) NOT NULL,
  `NgaySinh` date NOT NULL,
  `TinhTPId` int(11) NOT NULL,
  `QuanHuyenId` int(11) NOT NULL,
  `PhuongXaId` int(11) NOT NULL,
  `DiaChi` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `DienThoai` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Email` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `NgayVaoLam` date NOT NULL,
  `ChucVu` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`GiaoVienId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `hocsinh`
--

CREATE TABLE IF NOT EXISTS `hocsinh` (
  `HocSinhId` int(11) NOT NULL AUTO_INCREMENT,
  `HoDem` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `Ten` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `GioiTinh` tinyint(4) NOT NULL,
  `NgaySinh` date DEFAULT NULL,
  `NgayVaoLop` date DEFAULT NULL,
  `DanToc` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DienCuTru` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DienThoai` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `HoTenCha` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `HoTenMe` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DienThoaiMe` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TinhThanhPhoId` int(11) DEFAULT NULL,
  `QuanHuyenId` int(11) DEFAULT NULL,
  `PhuongXaId` int(11) DEFAULT NULL,
  `DiaChi` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ThoiHoc` tinyint(1) NOT NULL DEFAULT '0',
  `CreatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`HocSinhId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=245 ;

--
-- Dumping data for table `hocsinh`
--

INSERT INTO `hocsinh` (`HocSinhId`, `HoDem`, `Ten`, `GioiTinh`, `NgaySinh`, `NgayVaoLop`, `DanToc`, `DienCuTru`, `DienThoai`, `HoTenCha`, `HoTenMe`, `DienThoaiMe`, `TinhThanhPhoId`, `QuanHuyenId`, `PhuongXaId`, `DiaChi`, `ThoiHoc`, `CreatedDate`) VALUES
(1, 'Đặng Minh', 'Huy', 1, '2014-02-07', '2017-01-01', '', '', '0906481753', 'Đặng Văn Dũng', 'Dg T Kim Nhanh', '0905319793', 1, 3, 26, '46 Nguyễn Thị Định', 0, '2017-06-05 00:00:00'),
(2, 'Lê Hồ Bảo', 'Quyên', 0, '2014-06-10', '2017-01-01', '', '', '0935810231', 'Lê Thanh Hải', 'Hồ Thị Hằng', '0905535663', 1, 3, 28, '21 Hồ Sỉ Tân', 0, '2017-06-05 00:00:00'),
(3, 'Nguyễn Tuấn', 'Kiệt', 1, '2014-05-26', '2017-01-01', '', '', '0905144055', 'Nguyễn Duy Mạnh', 'Nguyễn Thị Oanh', '0905594908', 1, 3, 26, '864 Ngô Quyền', 0, '2017-06-05 00:00:00'),
(4, 'Mai Xuân', 'Phúc', 1, '2014-03-22', '2017-01-01', '', '', '01234258477', 'Mai Xuân Ước', 'Ngô Thị Sa', '0935144005', 1, 3, 26, '69 An Tân', 0, '2017-06-05 00:00:00'),
(5, 'Bùi Quỳnh', 'Hân', 0, '2014-06-17', '2017-01-01', '', '', '0935987897', 'Bùi Văn Vũ', 'Đặng Thị Lai', '0935709735', 1, 3, 26, '22 Lê Phụng Hiểu', 0, '2017-06-05 00:00:00'),
(6, 'Đặng Hồng', 'Ngân', 0, '2014-05-14', '2017-01-01', '', '', '0905044288', 'Đặng  Văn Xin', 'Mai Thị thành', '0914083434', 1, 3, 26, '12 Nguyễn Tuân', 0, '2017-06-05 00:00:00'),
(7, 'Ngô Thị Ngọc', 'Thy', 0, '2014-11-11', '2017-01-01', '', '', '0905877616', 'Ngô Văn Thương', 'Hà H.T Việt Trân', '0935303611', 1, 3, 26, '23 Nguyễn Thế Lộc', 0, '2017-06-05 00:00:00'),
(8, 'Đoàn Huy', 'Hoàng', 1, '2015-05-27', '2017-01-01', '', '', '0935345036', 'Đoàn Văn Trí', 'Nguyễn Thị Hường', '01214261982', 1, 3, 28, '48 Hoàng Quốc Việt', 0, '2017-06-05 00:00:00'),
(9, 'Lê Phạm Công', 'Hiếu', 1, '2014-02-03', '2017-01-01', '', '', '01227473223', 'Lê Vũ', 'Võ T Kim Hoa', '01224581359', 1, 3, 28, 'Võ Trường Toản', 0, '2017-06-05 00:00:00'),
(10, 'Ngô Minh', 'Huy', 1, '2014-09-07', '2017-01-01', '', '', '0906153612', 'Ngô Xuân Phúc', 'Huỳnh Thi Lan', '0935804811', 1, 3, 26, 'Tổ 65 An Tân', 0, '2017-06-05 00:00:00'),
(11, 'Bùi Ng Duy', 'Độ', 1, '2015-04-27', '2017-01-01', '', '', '0905198175', 'Bùi Duy Độ', 'Nguyễn Thị Em', '01289160789', 1, 3, 26, '50 An Hải 18', 1, '2017-06-05 00:00:00'),
(12, 'Lê Thảo', 'Vy', 0, '2015-08-05', '2017-01-01', '', '', '', 'Lê Tiến Phong', 'Huỳnh T K Tuyết', '0905162827', 1, 3, 28, 'Tổ 1 Nại Tú 1', 0, '2017-06-05 00:00:00'),
(13, 'Mai Xuân', 'Phú', 1, '2015-04-16', '2017-01-01', '', '', '0905336090', 'Mai Xuân Tuấn', 'Võ T Mỹ Phương', '01202334489', 1, 3, 26, '103 Nguyễn Trung Trực', 0, '2017-06-05 00:00:00'),
(14, 'Phùng Minh', 'Phúc', 1, '2015-04-16', '2017-01-01', '', '', '0986997975', 'Phùng Ngọc Minh', 'Ng Bá M Phương', '01202334489', 1, 3, 26, '06 Nguyễn Tuân', 0, '2017-06-05 00:00:00'),
(15, 'Ng Lê Uyên', 'Nhi', 0, '2014-12-18', '2017-01-01', '', '', '0905339750', 'Nguyễn Mạnh Việt', 'Lê T Bích Trâm', '0905385581', 1, 3, 24, '03 Nại Thịnh 3', 0, '2017-06-05 00:00:00'),
(16, 'Phan H. Khánh', 'Phong', 1, '2015-06-16', '2017-01-01', '', '', '0935631647', 'Phan Thanh Hải', 'Ngô Thị T Hồng', '0906446543', 1, 3, 28, '81 Nghĩa Địa Lê', 0, '2017-06-05 00:00:00'),
(17, 'Trần Ngô Minh', 'Nhật', 1, '2015-07-09', '2017-01-01', '', '', '', 'Trần Văn Lợi', 'Lê T P Thanh', '0905487175', 1, 3, 28, '146 Lý Đạo Thành', 0, '2017-06-05 00:00:00'),
(18, 'Lê Nguyễn Hà', 'My', 0, '2015-07-23', '2017-01-01', '', '', '', 'Lê Văn Tốt', 'Nguyễn Thị Nguyệt', '0924056980', 1, 3, 26, '88 Lê Phụng Hiểu', 0, '2017-06-05 00:00:00'),
(19, 'Trần Mộc', 'Miên', 0, '2015-11-10', '2017-01-01', '', '', '0901151525', 'Trần Trung Hậu', 'Huỳnh T B Trâm', '0905936529', 1, 3, 28, 'Tổ 2 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(20, 'Nguyễn Công', 'Khanh', 1, '2015-10-22', '2017-01-01', '', '', '0905041016', 'Nguyễn Minh Trung', 'Hoàng Thị Thu', '01262654894', 1, 3, 28, '81 Nguyễn Trung Trực', 0, '2017-06-05 00:00:00'),
(21, 'Hồ H. Thanh', 'Yến', 0, '2011-04-09', '2017-01-01', '', '', '905787034', 'Hồ Mã Hoàng', 'Hoàng Thị', '1264618619', 1, 3, 26, 'Tổ 10', 1, '2017-06-05 00:00:00'),
(22, 'Trần Ng Tuấn', 'Huy', 1, '2011-09-22', '2017-01-01', '', '', '906446281', 'Trần Văn Quy', 'Nguyễn Thị Hồng', '932483380', 1, 3, 28, 'Chung Cư A1', 1, '2017-06-05 00:00:00'),
(23, 'Lê Ng. Ngọc', 'Ly', 0, '2011-09-14', '2017-01-01', '', '', '9635692269', 'Lê Toàn', 'Ng T.Bích Hòa', '905459827', 1, 3, 28, 'Tổ 7', 1, '2017-06-05 00:00:00'),
(24, 'Dương Thiên', 'Bảo', 1, '2011-01-01', '2017-01-01', '', '', '0935721043', 'Dg  Đình Thanh', 'Lê T.Bích Tuân', '01283670788', 1, 3, 28, 'Tô 2 Trần Q. Toản', 1, '2017-06-05 00:00:00'),
(25, 'Bùi Văn', 'Thiện', 1, '2011-06-25', '2017-01-01', '', '', '0905344347', 'Bùi Văn Nở', 'Lê Thị Tơ', '012569886077', 1, 3, 28, 'Địa Bảo', 1, '2017-06-05 00:00:00'),
(26, 'Ng. T Anh', 'Dương', 1, '2011-01-11', '2017-01-01', '', '', '0905989860', 'Ng Văn Thiện', 'Võ Thị Kim Huệ', '01222475655', 1, 3, 28, 'Trần Hưng Đạo', 1, '2017-06-05 00:00:00'),
(27, 'Vũ Đăng', 'Khoa', 1, '2011-07-10', '2017-01-01', '', '', '', '', 'Võ T.Kim Hiếu', '0905324488', 1, 3, 28, 'Nại Hiên Đông', 1, '2017-06-05 00:00:00'),
(28, 'Đỗ Thu', 'Phương', 0, '2012-01-28', '2017-01-01', '', '', '983770444', 'Đỗ Văn Tiến', 'Trần T Hải Yên', '922911592', 1, 3, 26, '42 Ng Trung Trực', 0, '2017-06-05 00:00:00'),
(29, 'Phạm Ng Kỳ', 'Thư', 0, '2011-06-21', '2017-01-01', '', '', '935690234', 'Phạm Duy Kỳ', 'nguyễn Thi Vân', '936702123', 1, 3, 28, 'P510 Kcc Sunhome', 1, '2017-06-05 00:00:00'),
(30, 'Hồ Thanh', 'Thảo', 0, '2012-03-21', '2017-01-01', '', '', '935762068', 'Hồ Vinh', 'Đặng T Hồng Ánh', '905849601', 1, 3, 26, '336 Ngô Quyền', 1, '2017-06-05 00:00:00'),
(31, 'Ngô Bảo', 'Trân', 0, '2011-08-12', '2017-01-01', '', '', '0902205245', 'Ngô Văn Mạnh', 'Đỗ Thị Mỹ Dung', '0905859287', 1, 3, 26, 'Tổ 29 An Hải 10', 1, '2017-06-05 00:00:00'),
(32, 'Ngô Anh', 'Tiến', 1, '2012-08-16', '2017-01-01', '', '', '', 'Ngô V Th Tuấn', 'Võ Thi Kim Anh', '934940410', 1, 3, 28, 'Tổ 24 Dg Văn Nga', 0, '2017-06-05 00:00:00'),
(33, 'Đặng Trần Nhật', 'Minh', 1, '2012-07-07', '2017-01-01', '', '', '', 'Đg Ngọc Thoại', 'Trần T Hồng Hạnh', '964547077', 1, 3, 26, 'Tổ 21 An Đồn', 0, '2017-06-05 00:00:00'),
(34, 'Nguyễn T Thùy', 'Trâm', 0, '2012-11-15', '2017-01-01', '', '', '932490442', 'Ng Thành Thái', 'Lê Thị Hòa', '934812254', 1, 3, 26, '34 Ng Thị Định', 0, '2017-06-05 00:00:00'),
(35, 'Nguyễn Huỳnh', 'Giao', 0, '2012-08-19', '2017-01-01', '', '', '0905626994', 'Nguyễn Tấn', 'Huỳnh Thị Đào', '0908983904', 1, 3, 30, 'Tổ 10 Thọ Quang', 0, '2017-06-05 00:00:00'),
(36, 'Mai Hoàng', 'Vân', 0, '2011-05-18', '2017-01-01', '', '', '01262614303', 'Mai Văn Dũng', 'Huỳnh Thị Lệ', '', 1, 3, 28, 'Đại Địa Bảo- vũng thùng', 1, '2017-06-05 00:00:00'),
(37, 'Ngô Tr.Phương', 'Uyên', 0, '2011-09-07', '2017-01-01', '', '', '982910673', 'Ngô Minh Thân', 'Trịnh Thị Bình', '988788126', 1, 3, 28, 'Tổ 2', 1, '2017-06-05 00:00:00'),
(38, 'Ng . T. Hoàng', 'Ngân', 0, '2011-04-04', '2017-01-01', '', '', '912132191', 'Ng. Văn Thương', 'Ng.T.Thanh Thanh', '918504314', 1, 3, 28, '72 Nguyễn Chí Diễu', 1, '2017-06-05 00:00:00'),
(39, 'Lê Văn', 'Hiếu', 1, '2011-07-27', '2017-01-01', '', '', '0989437137', 'Lê Văn Hải', 'Đặng Thị Tuyết', '', 1, 3, 28, 'Tổ 135 Kc cư làng cá', 1, '2017-06-05 00:00:00'),
(40, 'Nguyễn Anh', 'Kiệt', 1, '2011-02-09', '2017-01-01', '', '', '1289160355', 'Nguyễn Văn Lên', 'Nguyễn Thị Ny Na', '01227993036', 1, 3, 28, 'Tổ 145 Kc cư làng cá', 1, '2017-06-05 00:00:00'),
(41, 'Lê T Thanh', 'Hằng', 0, '2011-10-02', '2017-01-01', '', '', '0946927853', 'Lê Văn Giang', 'Nguyễn Thị Bé', '', 1, 3, 28, 'Tổ 145 Kc cư làng cá', 1, '2017-06-05 00:00:00'),
(42, 'Đặng Ngọc Gia', 'Bảo', 1, '2011-02-23', '2017-01-01', '', '', '01205969490', 'Đặng Văn Mỹ', 'Trần Thị Hương', '0905291643', 1, 3, 26, 'Số 20 An Hải 11', 1, '2017-06-05 00:00:00'),
(43, 'Phan Lê Gia', 'Vũ', 1, '2011-07-31', '2017-01-01', '', '', '0975285488', 'Phan Văn Phúc', 'Lê Thị Kim Hạnh', '0925989148', 1, 3, 28, '157 Hoa Lư', 1, '2017-06-05 00:00:00'),
(44, 'Nguyễn Trung', 'Kiên', 1, '2011-09-04', '2017-01-01', '', '', '905072447', 'Ng. Mạnh Tiến', 'Ng.Thị Tho', '986884927', 1, 3, 26, '42 Ng Trung Trực', 1, '2017-06-05 00:00:00'),
(45, 'Nguyễn Nhất', 'Win', 1, '2011-07-01', '2017-01-01', '', '', '01638421713', 'Ng Đức Nam', 'Phạm Thị Chín', '0905755784', 1, 3, 28, 'Tổ 37', 1, '2017-06-05 00:00:00'),
(46, 'Mai Lê Thanh', 'Nam', 1, '2012-01-29', '2017-01-01', '', '', '0905678783', 'Mai Thanh Việt', 'Lê Thị Đào', '0905016591', 1, 3, 28, 'Tổ 19 Bùi Lam', 0, '2017-06-05 00:00:00'),
(47, 'Lê Văn Khải', 'Minh', 1, '2012-06-16', '2017-01-01', '', '', '0935747412', 'Lê Văn Thương', 'Ng T Kim Huệ', '01276589683', 1, 3, 28, 'Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(48, 'Lê Thị Như', 'Quỳnh', 0, '2012-04-17', '2017-01-01', '', '', '932521932', 'Lê Văn Sử', 'Ngô T Thu Thanh', '', 1, 3, 28, 'K ccu làng Cá', 0, '2017-06-05 00:00:00'),
(49, 'Huỳnh Ngọc Anh', 'Thư', 0, '2020-12-11', '2017-01-01', '', '', '982220620', 'H Ngọc Trung', '', '1265381048', 1, 3, 28, '40 Đào Duy Kỳ', 1, '2017-06-05 00:00:00'),
(50, 'Nguyễn', 'Lợi', 1, '2011-05-19', '2017-01-01', '', '', '1655461466', 'Nguyễn Mỹ', 'Lưu T.Hồng Thắm', '905300212', 1, 3, 28, '56 Nguyễn Hiền', 1, '2017-06-05 00:00:00'),
(51, 'Đặng Ngọc', 'Vĩ', 1, '2012-07-28', '2017-01-01', '', '', '905339113', 'Đặng Ngọc Trí', 'Trần Thị Trí', '905954550', 1, 3, 28, '05 Nại Thịnh 2', 0, '2017-06-05 00:00:00'),
(52, 'Ng Đức Anh', 'Khoa', 1, '2011-03-09', '2017-01-01', '', '', '', 'Nguyễn A Tuấn', 'Hồ Thị Mùi', '935158882', 1, 1, 22, 'Tổ 22 Hải Châu 2', 1, '2017-06-05 00:00:00'),
(53, 'Nguyễn Quý', 'Lâm', 1, '2011-02-17', '2017-01-01', '', '', '935282515', 'Nguyễn Văn Nở', 'Huỳnh Thị Mười', '932452153', 1, 3, 28, '48 Võ Trường Toản', 1, '2017-06-05 00:00:00'),
(54, 'Lê Đặng Gia', 'Hân', 0, '2012-08-05', '2017-01-01', '', '', '1225427077', 'Lê Văn Núi', 'ĐặngTThúy Cương', '1219342630', 1, 3, 26, '11 Bùi Lâm', 0, '2017-06-05 00:00:00'),
(55, 'Lê Ng Hạnh', 'Nguyên', 1, '2012-06-18', '2017-01-01', '', '', '935100790', 'Lê Thiện Tín', 'Nguyễn Thị Thúy', '935620668', 1, 3, 26, 'K 37/41', 0, '2017-06-05 00:00:00'),
(56, 'Trần Minh', 'Khôi', 1, '2011-01-12', '2017-01-01', '', '', '', 'Trần Sương', 'Trần Thị Hồng', '', 1, 3, 26, 'Tổ 35 Nại Hiên Đông', 1, '2017-06-05 00:00:00'),
(57, 'Ng Thạc Hoàng', 'Anh', 0, '2011-01-27', '2017-01-01', '', '', '978324455', 'Ng Thạc Văn', 'Nguyễn Thị Biên', '978315455', 1, 3, 26, 'P 512 CC sunhome', 1, '2017-06-05 00:00:00'),
(58, 'Võ Kim Khánh', 'Quỳnh', 0, '2012-02-09', '2017-01-01', '', '', '', 'Võ Bá Phụng', 'Dương Thị Ân', '1266645091', 1, 3, 28, 'Tổ 10 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(59, 'Nguyễn Lê', 'Nguyên', 1, '2013-09-09', '2017-06-20', '', '', '0905542272', 'Nguyễn Thanh Hải', 'Lê Thị Kim Thoa', '01216642626', 1, 3, 28, '54 Võ Trường toản', 0, '2017-06-05 00:00:00'),
(60, 'Trần T Thúy', 'Kiều', 0, '2013-04-07', '2017-06-20', '', '', '01227907388', 'Điền Nam', 'Trần T .Bé Hà', '01228595919', 1, 3, 28, 'Tổ 144Kcc làng cá', 0, '2017-06-05 00:00:00'),
(61, 'Đỗ Trần L Nhật', 'Minh', 1, '2013-03-04', '2017-06-20', '', '', '0901126299', 'Đỗ Văn Út', 'Lê Thị Ái', '0932451547', 1, 3, 28, '54 Đường Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(62, 'Ng Dương Linh', 'Đan', 0, '2013-11-18', '2017-06-20', '', '', '0905126948', 'Nguyễn Tấn Bình', 'Dương T Mỹ Hạnh', '0905381058', 1, 3, 28, 'Tổ 07 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(63, 'Lê Quốc', 'Phước', 1, '2013-05-31', '2017-01-01', '', '', '', 'Lê Quốc Bảo', 'Trần Thị Lê Thủy', '0977882978', 1, 3, 26, '23 Võ  Trường Toản', 0, '2017-06-05 00:00:00'),
(64, 'Nguyễn Thị Anh', 'Thư', 0, '2013-12-23', '2017-01-01', '', '', '0905135757', 'Nguyễn Tuấn Anh', 'Ngô T Kim Bằng', '0975707573', 1, 3, 26, '75 Ngô Trí Hòa', 0, '2017-06-05 00:00:00'),
(65, 'Trần Lê Long', 'Nhật', 1, '2013-01-11', '2017-06-20', '', '', '0905434212', 'Lê Thành', 'Trần Thị Hồng', '0905447024', 1, 3, 28, 'Tổ 152 N Khắc cần', 0, '2017-06-05 00:00:00'),
(69, 'Hoàng Phúc', 'Nguyên', 1, '2013-04-04', '2017-06-20', '', '', '0934792815', '', '', '0914288856', 1, 3, 28, '301Kccư A1Vũng Thùng', 1, '2017-06-05 00:00:00'),
(70, 'Trần Ng Anh', 'Thư', 0, '2013-08-08', '2017-01-01', '', '', '905629466', 'Trần Hữu Nghĩa', 'Ng T Cẩm Dung', '122858318', 1, 3, 28, 'P510 Ccu so 4', 1, '2017-06-05 00:00:00'),
(71, 'Trần Thị Khởi', 'My', 0, '2013-11-08', '2017-06-20', '', '', '935760963', 'Trần Phó', 'Ng T N Phương', '1202768917', 1, 3, 28, '10 Nại Nghĩa 4', 0, '2017-06-05 00:00:00'),
(72, 'Ng Huỳnh Linh', 'Đan', 0, '2013-01-27', '2017-06-20', '', '', '0909509579', 'Ng Hữu Ngọc', 'Huỳnh T Tuyết', '0984524480', 1, 3, 28, 'P101KCCA2 Ng Tri Hòa', 1, '2017-06-05 00:00:00'),
(73, 'Nguyễn Bảo', 'Duy', 1, '2013-10-13', '2017-06-20', '', '', '', 'Ng Văn Phước', 'Đặng H Linh', '1863143177', 1, 3, 28, '16 Hoành Quốc Việt', 0, '2017-06-05 00:00:00'),
(74, 'Ngô Như', 'Quỳnh', 0, '2013-12-04', '2017-06-20', '', '', '1222528982', 'Nguyễn Văn Trông', 'Ng Phước AThư', '905582548', 1, 3, 26, '95 Nguyễn Tuân', 0, '2017-06-05 00:00:00'),
(75, 'Nguyễn Chí', 'Bảo', 1, '2014-11-11', '2017-01-01', '', '', '913132191', 'Nguyễn V Thương', 'Ng T T Thanh', '918504314', 1, 3, 26, '72 Nguyễn Chí Diễu', 0, '2017-06-05 00:00:00'),
(76, 'Trịnh Công', 'Danh', 1, '2015-05-14', '2017-01-01', '', '', '', 'Trịnh Cao Công', 'Huỳnh Thị Bé', '', 1, 3, 28, 'Nguyễn Khắc Cần', 0, '2017-06-05 00:00:00'),
(77, 'Hồ Hoàng', 'Huy', 1, '2014-04-08', '2017-01-01', '', '', '934120467', 'Hồ Văn Qúy', 'Hoàng Thị Lưu', '', 1, 3, 28, '20 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(78, 'Trần H Bảo', 'Trâm', 0, '2015-01-10', '2017-01-01', '', '', '905349623', 'Nguyễn Xuân Tình', 'Lê T Thu Thủy', '935209371', 1, 3, 26, 'K187/08 Ngô Quyền', 0, '2017-06-05 00:00:00'),
(79, 'Vũ Lê Bảo', 'An', 0, '2013-08-04', '2017-06-20', '', '', '978009861', 'Vũ Lê Trung', 'Nguyễn Thị Đào', '1655893650', 1, 3, 29, '23A Trương Định', 0, '2017-06-05 00:00:00'),
(80, 'Huỳnh T Diệu', 'Anh', 0, '2014-03-07', '2017-01-01', '', '', '906119748', 'Huỳnh Văn Mỹ', 'Trần Thị Kỹ', '905258912', 1, 3, 26, '01 An Hải 5', 0, '2017-06-05 00:00:00'),
(81, 'Nguyễn Xuân', 'Pháp', 1, '2013-10-07', '2017-06-20', '', '', '905349623', 'Nguyễn Xuân Tình', 'Lê Thị Thu Thủy', '935209371', 1, 3, 26, '478 Ngô Quyền', 0, '2017-06-05 00:00:00'),
(82, 'Lê Ng Minh', 'Trí', 1, '2014-04-01', '2017-01-01', '', '', '935100790', 'Lê Thiện Tín', 'Nguyễn Thị Thủy', '935620668', 1, 3, 24, 'K37/41 Lương Thế Vinh', 0, '2017-06-05 00:00:00'),
(83, 'Đặng N Quỳnh', 'Nhi', 0, '2014-02-07', '2017-01-01', '', '', '905877687', 'Đặng Duy Tường', 'Đỗ Thị Quỳnh', '905630526', 1, 3, 26, '27 Võ Trường Toản', 0, '2017-06-05 00:00:00'),
(84, 'Hồ Ng Ngọc', 'Anh', 0, '2014-01-22', '2017-01-01', '', '', '', '', 'Hồ Thị Vui', '935158882', 1, 3, NULL, 'Lô 37KDC Phan Bá Phiến', 0, '2017-06-05 00:00:00'),
(85, 'Hà Nguyễn Gia', 'Hân', 0, '2015-01-31', '2017-01-01', '', '', '', 'Hà Trung Kiên', 'Nguyễn T K Lai', '905191660', 1, 1, 6, '88 Lê Duẫn', 1, '2017-06-05 00:00:00'),
(86, 'Nguyễn Gia', 'Hân', 0, '2015-01-31', '2017-01-01', '', '', '', 'Nguyễn Văn Lạc', 'Ng T Kiều Diễm', '905111250', 1, 3, 26, 'Tổ 11 An Hải Bắc', 0, '2017-06-05 00:00:00'),
(87, 'Ng H Bảo', 'Phương', 0, '2014-04-27', '2017-01-01', '', '', '', 'Nguyễn Công Linh', 'Huỳnh Thị Mẫn', '984010984', 1, 3, 26, 'Tổ 94 An Hải Bắc', 1, '2017-06-05 00:00:00'),
(88, 'Đỗ Ngọc Hồng', 'Phúc', 0, '2012-02-29', '2017-01-01', '', '', '0905269314', 'Đỗ Văn Lưu', 'Đinh T.Ng. Trâm', '0935018212', 1, 3, 28, 'Lô 23 Nại Nghĩa', 1, '2017-06-05 00:00:00'),
(89, 'Phan Minh', 'Tú', 1, '2012-06-06', '2017-01-01', '', '', '0905159010', 'Phan Minh Đông', 'Đỗ T Lan Nhi', '0905811797', 1, 3, 26, '22 Đỗ Xung Hợp', 0, '2017-06-05 00:00:00'),
(90, 'Ng Trần Gia', 'Bảo', 1, '2012-10-06', '2017-01-01', '', '', '0905456332', 'Ng Văn Cúc', 'Đoàn Thị Thu', '0935999710', 1, 3, 28, '03 Lê Cảnh Tân', 0, '2017-06-05 00:00:00'),
(91, 'Phạm Thanh B', 'Châu', 0, '2012-03-17', '2017-01-01', '', '', '0905026672', 'Phạm T . Long', 'Đinh T T Hằng', '0932471531', 1, 3, 26, 'Tổ 58 An Đồn', 0, '2017-06-05 00:00:00'),
(92, 'Lê Hoàng', 'Phước', 1, '2012-03-11', '2017-01-01', '', '', '', 'Ng Hoàng Linh', 'Lê Thị Như  Ngọc', '0932577047', 1, 3, 26, '24 Hoàng Việt', 0, '2017-06-05 00:00:00'),
(94, 'Trần Trí', 'Diễn', 1, '2012-03-30', '2017-01-01', '', '', '0905738860', 'Trần Văn Cường', 'Phan Thị Kim Liên', '01282664911', 1, 3, 28, 'Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(95, 'Trần Văn Phi', 'Long', 1, '2012-04-01', '2017-01-01', '', '', '0905909614', 'Trần Văn Hà', 'Huỳnh Thị Sẻ', '01225634651', 1, 3, 28, 'Tổ 63 Dương V Nga', 0, '2017-06-05 00:00:00'),
(96, 'Đoàn Đỗ Phương', 'Vy', 0, '2012-03-05', '2017-01-01', '', '', '01664586186', 'Đoàn Công Hiền', 'Đỗ T Thu Hồng', '0905447024', 1, 3, 26, 'Tổ 206 An cư', 0, '2017-06-05 00:00:00'),
(97, 'Huỳnh Phan T.', 'Hiếu', 1, '2012-07-20', '2017-01-01', '', '', '', 'Huỳnh Văn Dưỡng', 'Tô Thị Hạnh', '01686209234', 1, 3, 30, 'T160 VịnhMânQuan', 0, '2017-06-05 00:00:00'),
(98, 'Nguyễn Quốc', 'Thiên', 1, '2012-06-16', '2017-01-01', '', '', '0905983565', 'Nguyễn Thuận', 'Trần Mỹ Hòa', '0905758553', 1, 3, 28, 'Khu C Cư liền kề', 0, '2017-06-05 00:00:00'),
(99, 'Lê Nhật', 'Hoàng', 1, '2013-10-08', '2017-01-01', '', '', '', 'Lê Tú Vũ', 'Ng T Kim Thảo', '0905709088', 1, 3, 26, '107 NgTrung Trực', 0, '2017-06-05 00:00:00'),
(100, 'Lê Tấn', 'Kiệt', 1, '2013-02-11', '2017-01-01', '', '', '0905737126', 'Lê Tấn Đạt', 'Ng T Thanh thúy', '01289442282', 1, 3, 28, '132 Khu C cư', 0, '2017-06-05 00:00:00'),
(101, 'Đinh T.Thanh', 'Ngân', 0, '2012-05-13', '2017-01-01', '', '', '0982495429', 'Đinh Văn Lý', 'Nguyễn Thị Mến', '0982818821', 1, 3, 28, 'Tổ 51 KC Cư', 0, '2017-06-05 00:00:00'),
(102, 'Mai Ng Phượng', 'Nhi', 0, '2012-09-03', '2017-01-01', '', '', '0905688877', 'Mai Văn Hùng', 'Ng T Thu Sương', '01222434914', 1, 3, 28, '2B-711 khu c cư', 0, '2017-06-05 00:00:00'),
(103, 'Lê Ng Ngọc', 'Yến', 0, '2012-10-14', '2017-01-01', '', '', '01633109838', 'Lê Viết Trình', 'Nguyễn T. Nguyệt', '0905587293', 1, 3, 26, 'Số 2 Nguyễn Tuân', 0, '2017-06-05 00:00:00'),
(104, 'Phan Thủy', 'Tiên', 0, '2012-12-02', '2017-01-01', '', '', '0905258449', 'Phan Phước Chương', 'Nguyễn Thị Thủy', '0905731776', 1, 3, 26, '07 Trương Hán Siêu', 0, '2017-06-05 00:00:00'),
(105, 'Hoàng Đ Khánh', 'Nhi', 0, '2012-10-16', '2017-01-01', '', '', '0905051206', 'Hoàng Thế Thân', 'Đỗ Hoàng Phú', '0913988253', 1, 3, 24, '105 Ngô Quyền', 0, '2017-06-05 00:00:00'),
(106, 'Cao Ng Tuyết', 'Hạnh', 0, '2012-09-04', '2017-01-01', '', '', '', 'Cao Văn Nam', 'Nguyễn Thị Hồng', '1228340654', 1, 3, 28, '137 Kcc làng Cá', 0, '2017-06-05 00:00:00'),
(107, 'Trương Ng M', 'Hằng', 0, '2012-10-08', '2017-01-01', '', '', '0909570290', 'Trương Ngọc Sơn', 'Ng T Phương Thảo', '0905434391', 1, 3, 26, '145 Ng Trung Trực', 0, '2017-06-05 00:00:00'),
(108, 'Nguyễn Bảo', 'Trung', 1, '2012-09-29', '2017-01-01', '', '', '1226273251', 'Ng Thanh Tùng', '', '', 1, 3, 26, '75 Ngô Chí Diễu', 0, '2017-06-05 00:00:00'),
(109, 'Hồ Đỗ Kim', 'Ánh', 0, '2012-12-09', '2017-01-01', '', '', '1216670716', 'Hồ Mã Long', 'Đỗ T Lệ Trang', '932650584', 1, 3, 26, 'K28 Số 12 Ng T Lộc', 0, '2017-06-05 00:00:00'),
(110, 'Mai Quang', 'Bảo', 1, '2012-07-31', '2017-01-01', '', '', '0906271932', 'Mai Quang Nhân', 'Ng T Bích Ngọc', '0996738759', 1, 3, 28, 'Tổ 146/p207 KCC 2c', 0, '2017-06-05 00:00:00'),
(111, 'Ng Nhật Minh', 'Thư', 0, '2012-05-19', '2017-01-01', '', '', '0906448912', 'Ng Văn Cường', 'Đinh T D Hương', '', 1, 3, 26, 'Tổ 45 B', 0, '2017-06-05 00:00:00'),
(112, 'Ng Nhật Minh', 'Anh', 0, '2012-05-19', '2017-01-01', '', '', '0906448912', 'Ng Văn  Cường', 'Đinh T D Hương', '', 1, 3, 26, 'Tổ 45 B', 0, '2017-06-05 00:00:00'),
(113, 'Phạm Ng Minh', 'Thư', 0, '2012-07-18', '2017-01-01', '', '', '988205552', 'Phạm Tấn Tài', 'Ng Thị Lê Hân', '1289471928', 1, 3, 28, 'Số 46 Lê Hữu Kiều', 0, '2017-06-05 00:00:00'),
(114, 'Phạm Nhã', 'Trúc', 0, '2012-01-28', '2017-01-01', '', '', '', '', 'Phạm Thị SaLem', '905098755', 1, 3, 28, 'D5 Lô 25 Vân Đồn', 0, '2017-06-05 00:00:00'),
(115, 'Đặng T Khánh', 'Ngọc', 0, '2012-08-15', '2017-01-01', '', '', '982936637', 'Đăng Ngọc Hiếu', 'Lê Thị Biên', '1224440227', 1, 3, 26, 'Tổ 58 An Đồn', 0, '2017-06-05 00:00:00'),
(116, 'Mai Võ Mạnh', 'Quân', 1, '2012-01-01', '2017-01-01', '', '', '906434228', 'Mai Ngọc Phương', 'Võ Thị Quanh', '905888235', 1, 3, 28, '29 Ng Trung Trực', 0, '2017-06-05 00:00:00'),
(117, 'Nguyễn Văn', 'Bình', 1, '2012-06-29', '2017-01-01', '', '', '935572223', 'Ng Văn Thanh', 'Mai Thị Phương', '935932880', 1, 3, 26, '41 Lê Văn Thêm', 0, '2017-06-05 00:00:00'),
(118, 'Nguyễn Anh', 'Thư', 0, '2012-11-21', '2017-01-01', '', '', '906446179', 'Ng Quang Ánh', 'Nguyễn Thùy Hương', '905523637', 1, 3, 28, '16 Ng Trung Trực', 0, '2017-06-05 00:00:00'),
(119, 'Lê Văn', 'Việt', 1, '2012-07-30', '2017-01-01', '', '', '905908800', 'Lê Văn Vinh', 'Bùi Thị Kim Anh', '905633034', 1, 3, 28, '41 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(120, 'Phạm Thảo', 'Nguyên', 0, '2013-01-14', '2017-01-01', '', '', '0936001751', 'Phạm Phú Viên', 'Đặng Thị Lệ', '0905669896', 1, 3, 28, '08 Nguyễn Khắc cần', 0, '2017-06-05 00:00:00'),
(121, 'Trần Gia', 'Bảo', 1, '2012-10-10', '2017-01-01', '', '', '906426516', 'Trần Văn Dự', 'Trần Thị Lai', '', 1, 3, 28, 'Tổ 33 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(122, 'Nguyễn Thái', 'Bình', 1, '2012-01-01', '2017-01-01', '', '', '', 'Ng Ngọc Dũng', 'Phạm T Mỹ Lành', '935497223', 1, 3, 26, '39 Ng Tuân', 0, '2017-06-05 00:00:00'),
(123, 'Phạm Quốc', 'Long', 1, '2012-09-10', '2017-01-01', '', '', '', 'Phạm Q Dương', 'Lê Thị H Nhung', '1213982130', 1, 3, 26, 'Tổ 24 An Tân', 0, '2017-06-05 00:00:00'),
(124, 'Nguyễn Duy', 'Thái', 1, '2012-10-20', '2017-01-01', '', '', '', 'Nguyễn Duy Tâm', 'Đoàn Thi Ngọc', '905016278', 1, 3, 30, '102 Ngô Quyền', 0, '2017-06-05 00:00:00'),
(125, 'Nguyễn Tấn', 'Thành', 1, '2012-12-21', '2017-01-01', '', '', '935756711', 'Ng Tấn Sỹ', 'Nguyên Thị Hoa', '905790626', 1, 3, 26, '15 An Hải 5', 0, '2017-06-05 00:00:00'),
(126, 'Nguyễn H Khánh', 'An', 0, '2012-10-16', '2017-01-01', '', '', '1268677493', 'Ng Thành Hưng', 'NguyễnThị K Hòa', '1289467751', 1, 3, 28, '02 Võ Trường Toản', 0, '2017-06-05 00:00:00'),
(127, 'Nguyễn Gia', 'Phong', 1, '2013-01-19', '2017-01-01', '', '', '934966154', 'Nguyễn Văn Tây', 'Ng Thị Thu Lòng', '935885725', 1, 3, 28, '145 Ng Trung Trực', 0, '2017-06-05 00:00:00'),
(128, 'Trần Đặng Tấn', 'Phát', 1, '2013-10-17', '2017-01-01', '', '', '', 'Đặng Hồng Nam', 'Trần Thị Thủy', '905080300', 1, 3, 26, 'Tổ 59 An Đồn', 0, '2017-06-05 00:00:00'),
(129, 'Nguyễn Anh', 'Quân', 1, '2012-11-29', '2017-01-01', '', '', '1268677493', 'Nguyễn Công Hưng', 'Đặng Thị Yến', '905915184', 1, 3, 26, '50 An Hải 18', 0, '2017-06-05 00:00:00'),
(130, 'Nguyễn T Ngọc', 'Anh', 0, '2013-09-08', '2017-01-01', '', '', '', 'Nguyễn Xuân', 'Lê Thị Ut', '932145373', 1, 3, 28, 'CC làng cá', 0, '2017-06-05 00:00:00'),
(131, 'Đỗ Duy', 'Hoàng', 1, '2012-03-20', '2017-01-01', '', '', '', 'Đỗ Duy Đức', 'Lê Thị Thắm', '935625095', 1, 3, 28, '37 Nguyễn Thị Ba', 0, '2017-06-05 00:00:00'),
(132, 'Nguyễn Ngọc', 'Duy', 1, '2011-01-02', '2017-01-01', '', '', '01266607878', 'Ng. Ngọc Trí', 'Trần Thị Vân', '0904730442', 1, 3, 28, 'KCC L/cá Khúc Hạo', 1, '2017-06-05 00:00:00'),
(133, 'Lê Quang', 'Minh', 1, '2011-11-06', '2017-01-01', '', '', '935793532', 'Lê Quang Lợi', 'TrầnTThúy Hằng', '1202102016', 1, 3, 26, 'Tổ 12 An Tân', 1, '2017-06-05 00:00:00'),
(134, 'Nguyễn Vũ', 'Khoa', 1, '2011-05-10', '2017-01-01', '', '', '905479530', 'Nguyễn Văn Vũ', 'Phạm Thị Thúy', '905184116', 1, 3, 28, '141 Hoa Lư - v.Thùng', 1, '2017-06-05 00:00:00'),
(135, 'Phan Minh', 'Kiệt', 1, '2011-09-27', '2017-01-01', '', '', '935335533', 'Phan Minh Toàn', 'Ng Hà Bảo Vy', '905534343', 1, 3, 26, 'Lê Phụng Hiểu', 1, '2017-06-05 00:00:00'),
(136, 'Ng Lê Mai', 'Dương', 1, '2011-10-08', '2017-01-01', '', '', '905287847', 'Ng. Thành Tuấn', 'Lê T. Hồng Hạnh', '905761561', 1, 3, 26, '188-AHB', 1, '2017-06-05 00:00:00'),
(137, 'Trần Ng. Bảo', 'Ngọc', 0, '2011-09-30', '2017-01-01', '', '', '01222576800', 'Trần V Trường', '', '', 1, 3, 28, '26 Dg Thừa Lâm', 1, '2017-06-05 00:00:00'),
(138, 'Nguyễn Tuấn', 'Huy', 1, '2011-09-22', '2017-01-01', '', '', '0905652886', 'Ng Thanh Tuấn', 'Huỳnh Thị Bước', '', 1, 3, 28, 'Cc Nại Hiên Đông', 1, '2017-06-05 00:00:00'),
(139, 'Dương Hải', 'Yến', 0, '2011-10-29', '2017-01-01', '', '', '905599406', 'Dương Văn Sáu', 'Phan Thị Thu', '935922414', 1, 3, 26, '137 Ng Trung Trực', 1, '2017-06-05 00:00:00'),
(140, 'Ng Kiều Nhã', 'Phương', 0, '2011-07-23', '2017-01-01', '', '', '1269943208', 'Ng Văn Phước', 'Lê Thị Thảo', '935456391', 1, 3, 28, 'L68- Bùi Huy Bích', 1, '2017-06-05 00:00:00'),
(141, 'Phan M .Ngọc', 'Phúc', 1, '2011-10-11', '2017-01-01', '', '', '935854866', 'Phan Minh Phát', 'Võ T.Nguyệt Nga', '935888967', 1, 3, 26, '6 Lê Phụng Hiểu', 1, '2017-06-05 00:00:00'),
(142, 'Phan M.Ngọc', 'Trân', 0, '2011-09-05', '2017-01-01', '', '', '1684207809', 'Phan Minh Khoa', 'Nguyễn T. Huệ', '932441122', 1, 3, 26, 'Tổ 28', 1, '2017-06-05 00:00:00'),
(143, 'Ng Mai Thục', 'Nhi', 0, '2012-07-05', '2017-01-01', '', '', '905132132', 'Ng. Trọng Bình', 'Mai T.Xuân Anh', '902177132', 1, 3, 26, '01 Nguyến Thế Lộc', 1, '2017-06-05 00:00:00'),
(144, 'Ng.Như Thùy', 'Linh', 0, '2011-04-01', '2017-01-01', '', '', '01678799115', 'Nguyễn Tâm', 'Lê Thị  Nguyệt', '01654549141', 1, 3, 28, 'CC Nại Hiên Đông', 1, '2017-06-05 00:00:00'),
(145, 'Lê Quốc', 'Nhân', 1, '2011-08-06', '2017-01-01', '', '', '01208185593', 'Lê Văn Lực', 'Lê Thị Sen', '', 1, 3, 28, 'Tô 2 Nại Tú', 1, '2017-06-05 00:00:00'),
(146, 'Lê Hữu Trung', 'Kiên', 1, '2011-02-21', '2017-01-01', '', '', '0934839463', 'Lê Hữu Thắng', 'Phú Thị Minh', '0906494803', 1, 3, 26, 'An Đồn', 1, '2017-06-05 00:00:00'),
(147, 'Trần Tuấn', 'Tú', 1, '2012-04-05', '2017-01-01', '', '', '1287555330', 'Trần tuấn Thiện', 'phan Thị Thạnh', '905749330', 1, 3, 28, '68 Nguyễn Hiền', 0, '2017-06-05 00:00:00'),
(148, 'Phan H Khánh', 'Như', 0, '2012-03-04', '2017-01-01', '', '', '935631647', 'Phan Thanh Hải', 'Ngô T Thu Hồng', '906446543', 1, 3, 28, 'Số 5 Nguyễn Địa Lô', 0, '2017-06-05 00:00:00'),
(149, 'Nguyễn Bảo Lam', 'Vi', 0, '2012-03-26', '2017-01-01', '', '', '906414688', 'Ng Huy Hoàng', 'Ng Thị Thành', '935686927', 1, 3, 28, '49 Hoàng Quốc Việt', 0, '2017-06-05 00:00:00'),
(150, 'Trần Phước', 'Nguyên', 1, '2012-09-28', '2017-01-01', '', '', '903531541', 'Trần Phước Tuân', 'Võ Thị Thảo', '905900923', 1, 3, 28, 'KC Cư NHĐ', 0, '2017-06-05 00:00:00'),
(151, 'Huỳnh Tr Xuân', 'Ngọc', 1, '2012-10-30', '2017-01-01', '', '', '', 'Lê Văn Cường', 'Kiều Thị Hồ Sự', '1216852303', 1, 3, 28, 'Tổ 09 Nại Nghĩa', 0, '2017-06-05 00:00:00'),
(152, 'Lê Kiều Bảo', 'Ngọc', 0, '2012-10-29', '2017-01-01', '', '', '932564640', 'Huỳnh Bá Linh', 'TrầnT Thúy Trinh', '932578385', 1, 3, 28, 'KCcu làng cá', 1, '2017-06-05 00:00:00'),
(153, 'Ng Trần Vũ', 'Nguyên', 1, '2011-09-26', '2017-01-01', '', '', '0905674223', 'Nguyễn V Thêm', 'Trần T Ánh Hồng', '', 1, 3, 29, '43 Lê Phụ Trần', 1, '2017-06-05 00:00:00'),
(154, 'Võ Như', 'Tài', 1, '2011-07-05', '2017-01-01', '', '', '', 'Võ Văn Vang', 'Huỳnh Thúy Liên', '1214460446', 1, 3, 28, '04 Nại Nghĩa7', 1, '2017-06-05 00:00:00'),
(155, 'Nguyễn Huyền', 'Châu', 0, '2011-11-05', '2017-01-01', '', '', '', 'Nguyễn Văn Sáng', 'Lê Thị Ngân', '0932441648', 1, 3, 28, 'Tổ 29 - Bùi Huy Bich', 1, '2017-06-05 00:00:00'),
(156, 'Đặng Hoàng', 'Mỹ', 1, '2011-12-12', '2017-01-01', '', '', '12215601399', 'Đặng Văn Thị', 'Cao Thị Thu', '935419652', 1, 3, 28, 'K Ccu làng cá', 1, '2017-06-05 00:00:00'),
(157, 'Mai Tuấn', 'Anh', 1, '2011-11-09', '2017-01-01', '', '', '987334996', 'Mai Văn Dũng', 'Ngô Thị Hải', '987334351', 1, 3, 26, 'Tổ 90', 1, '2017-06-05 00:00:00'),
(158, 'Lê Ng Phúc', 'Luân', 1, '2011-10-06', '2017-01-01', '', '', '905635272', 'Lê Văn Xin', 'Ng Thi Thu vân', '1228583177', 1, 3, 28, '06 Lê Hữu Kiều', 1, '2017-06-05 00:00:00'),
(159, 'Nguyễn T. Bảo', 'Châu', 0, '2011-02-26', '2017-01-01', '', '', '983811509', 'Ng Đình Ninh', 'Ng Thị Hương', '', 1, 3, 28, '77 Ngô Trí Hòa', 1, '2017-06-05 00:00:00'),
(160, 'Nguyễn Hoàng', 'Long', 1, '2011-05-17', '2017-01-01', '', '', '', 'Ng Hữu Thà', 'Ng T Thùy Trang', '905134905', 1, 3, 26, 'NguyễnChí Diễu', 1, '2017-06-05 00:00:00'),
(161, 'Ng Lê Diễm', 'Quỳnh', 0, '2012-10-06', '2017-01-01', '', '', '', 'Nguyễn Linh Sơn', 'Lê Thị Nhơn', '905111874', 1, 3, 28, 'Tô 6 Nại Tú 3', 0, '2017-06-05 00:00:00'),
(162, 'Đoàn Nhật', 'Tài', 1, '2011-11-13', '2017-01-01', '', '', '905476828', 'Đoàn Văn Thiên', 'PhTThùy Phương', '', 1, 3, 28, '309 Khúc Hạo', 1, '2017-06-05 00:00:00'),
(163, 'Mai Dương', 'Phong', 1, '2012-09-30', '2017-01-01', '', '', '905760027', 'Mai Văn Dưỡng', 'Nguyễn T Tuyết', '908870978', 1, 3, 26, '101 Ng Trung Trực', 0, '2017-06-05 00:00:00'),
(164, 'Đặng N Quỳnh', 'Giao', 0, '2012-04-12', '2017-01-01', '', '', '905877687', 'Đặng Duy Tường', 'Đỗ Thị Quỳnh', '905603527', 1, 3, 26, '27 Võ Trường Toản', 0, '2017-06-05 00:00:00'),
(165, 'Đỗ Thị Bảo', 'Ngọc', 0, '2012-09-14', '2017-01-01', '', '', '905760027', 'Đỗ Huy Nhơn', 'Trương Thị Tám', '932455604', 1, 3, 28, 'Tổ 70 Lê Phụ Trần', 0, '2017-06-05 00:00:00'),
(166, 'Đặng T.Khánh', 'Huyền', 0, '2011-04-08', '2017-01-01', '', '', '', 'Đặng Văn Cu', 'Ng Thị Thu Hà', '935107637', 1, 3, 28, 'Tổ 9 Nại Hiên Đông', 1, '2017-06-05 00:00:00'),
(167, 'PhạmL.Khánh', 'Quỳnh', 0, '2011-07-13', '2017-01-01', '', '', '1223450373', 'Phạm Văn Giàu', 'Lê Thị Thắng', '', 1, 3, 28, 'Tổ 20 Nại Hiên Đông', 1, '2017-06-05 00:00:00'),
(168, 'Hoàng Lê Gia', 'Huy', 1, '2012-09-23', '2017-01-01', '', '', '', 'Hoàng Sinh Tùng', 'Lê T B Phượng', '935135215', 1, 3, 29, '122 Trần Nhân Tông', 0, '2017-06-05 00:00:00'),
(169, 'Võ Như', 'Phát', 1, '2011-11-01', '2017-01-01', '', '', '913141206', 'Võ Như Khương', 'Phan Thị Thu', '935922414', 1, 3, 29, 'K 229 Ngô Quyền', 1, '2017-06-05 00:00:00'),
(170, 'Đỗ Thanh', 'Phong', 1, '2011-10-19', '2017-01-01', '', '', '', 'Đoỗ Trọng Nguyên', 'Đặng T T Tâm', '909199503', 1, 3, 26, '24 Đỗ Xuân Hợp', 1, '2017-06-05 00:00:00'),
(171, 'Lê Quốc', 'Nhân', 1, '2011-08-06', '2017-01-01', '', '', '', 'Lê Văn Lực', 'Nguyễn T Thu Hà', '935107637', 1, 3, 28, 'Tổ 2 Nại Tú', 1, '2017-06-05 00:00:00'),
(172, 'Nguyễn Minh', 'Quân', 1, '2013-04-21', '2017-06-21', '', '', '01212275771', 'Ng Minh Hải', 'Từ Thị Phượng', '0938225160', 1, 3, 28, 'Tổ 09 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(173, 'Nguyễn T Bảo', 'Ngọc', 0, '2013-06-15', '2017-06-21', '', '', '0977701119', 'Ng Quốc Phương', 'Dương Ánh Loan', '0938225160', 1, 3, 28, '04 Nguyễn Trung Trực', 0, '2017-06-05 00:00:00'),
(174, 'Phạm Ngô T T.', 'Ngân', 0, '2013-03-25', '2017-01-01', '', '', '0905570541', 'Phạm Văn Đăng', 'Ngô Thị Lợi', '01227428481', 1, 3, 28, '29 Đỗ Hoành', 0, '2017-06-05 00:00:00'),
(175, 'Bùi Văn Trọng', 'Nghĩa', 1, '2013-01-12', '2017-01-01', '', '', '0935987897', 'Bùi Văn Vũ', 'Đặng Thị  Lai', '0935709735', 1, 3, 26, '22 Lê Phụng Hiểu', 0, '2017-06-05 00:00:00'),
(176, 'Ng Phùng Thảo', 'My', 0, '2013-10-17', '2017-06-21', '', '', '0905449759', 'Ng Hữu Thiệt', 'Phùng TThu Hương', '093507765', 1, 3, 28, 'Khu C cư Bluehuose', 0, '2017-06-05 00:00:00'),
(177, 'Nguyễn Quang', 'Minh', 1, '2013-03-21', '2017-01-01', '', '', '935492144', 'Ng Đăng Huy', 'Bùi Thị Thanh Lan', '905492144', 1, 3, 26, '21 Đỗ Xuân Hợp', 0, '2017-06-05 00:00:00'),
(178, 'Lê Pham Bảo', 'Ngọc', 0, '2013-04-16', '2017-01-01', '', '', '1266675717', 'Lê Văn Tài', 'Phạm T Phương Phú', '905361644', 1, 3, 28, '46 Lê Hữu Kiều', 0, '2017-06-05 00:00:00'),
(179, 'Lê Văn Duy', 'Khang', 1, '2013-01-08', '2017-06-21', '', '', '', 'Lê Văn Tốt', '', '', 1, 3, 26, '88  Lê Phụng Hiểu', 0, '2017-06-05 00:00:00'),
(181, 'Ng Trần Bảo', 'Trân', 0, '2013-11-24', '2017-01-01', '', '', '905674223', 'Ng Văn Thêm', 'Trần T Ánh Hồng', '0935756030', 1, 3, 29, '43 Lê Phụ Trần', 0, '2017-06-05 00:00:00'),
(182, 'Trần Tuấn', 'Kiệt', 1, '2013-05-01', '2017-06-21', '', '', '905630100', 'Trần Văn Giang', 'Đặng Thị Hồng', '1223479114', 1, 3, 28, '26 Dương Lâm', 0, '2017-06-05 00:00:00'),
(183, 'Võ Nguyễn Bảo', 'Anh', 0, '2013-12-12', '2017-06-21', '', '', '3931507', 'Võ Tấn Đông', 'Võ Thị  Nhât Uyên', '1204052149', 1, 3, 26, '24 Trương Hán Siêu', 0, '2017-06-05 00:00:00'),
(184, 'Trần Anh', 'Khôi', 1, '2013-06-11', '2017-06-21', '', '', '906478505', 'Trần Bá Toản', 'Võ Thị Tuyết Mận', '1267172521', 1, 3, 28, '51 Nại hiên đông 15', 0, '2017-06-05 00:00:00'),
(185, 'Nguyễn Lê', 'Vy', 0, '2014-01-20', '2017-06-21', '', '', '905323757', 'Nguyễn T Chiến', 'Lê Thị T. Tuyền', '', 1, 3, 30, 'K 49/14 Võ Duy Ninh', 0, '2017-06-05 00:00:00'),
(186, 'Bùi Phương', 'Linh', 0, '2013-01-06', '2017-06-21', '', '', '', 'Bùi Phg Hoàng', 'Phạm Thị Mỹ Thọ', '1227493336', 1, 3, 28, 'Khu C cư làng cá', 0, '2017-06-05 00:00:00'),
(187, 'Hà Hoài', 'An', 0, '2013-10-20', '2017-06-21', '', '', '906111542', 'Hà Văn Dũng', 'Ngô Thị H Thương', '935480241', 1, 3, 26, 'Tổ 53 An Đồn', 0, '2017-06-05 00:00:00'),
(188, 'Lê Hồng', 'Phước', 1, '2013-02-04', '2017-01-01', '', '', '', 'Lê Hồng Khai', 'Lê Thị Hiển', '943627567', 1, 4, 36, '80 Lý Nhân Tông', 0, '2017-06-05 00:00:00'),
(189, 'Võ NgNguyên', 'Khang', 1, '2014-01-24', '2017-01-01', '', '', '986131311', 'Võ Ngọc Tuyền', 'Nguyễn Thị Dung', '915820206', 1, 3, 28, 'Tổ 43 Nại Hiên Đông', 1, '2017-06-05 00:00:00'),
(190, 'Lê Kỳ', 'Nam', 1, '2014-04-25', '2017-01-01', '', '', '', 'Lê Văn Thắng', 'Ngô Thị Lài', '905287118', 1, 3, 28, 'Tổ 39 Tôn Quang Phiệt', 0, '2017-06-05 00:00:00'),
(191, 'Ng Hoàng Lan', 'Uyên', 0, '2014-07-17', '2017-01-01', '', '', '908720972', 'Nguyễn H Quốc', 'Nguyễn Thị Bé Lan', '905899411', 1, 3, 28, '16 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(192, 'Phan ĐHoàng', 'Dương', 1, '2013-12-03', '2017-06-21', '', '', '905877848', 'Phan Đình Hoàng', 'Lê Thị Quyên', '905552509', 1, 3, 26, '102 Nguyễn Thị Định', 0, '2017-06-05 00:00:00'),
(193, 'Huỳnh Thị Tố', 'Tâm', 0, '2014-11-04', '2017-01-01', '', '', '934721700', 'Huỳnh Văn Nhớ', 'Nguyễn Thị Bình', '1677143131', 1, 3, 28, 'Tổ 11 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(194, 'Phan Th Nhã', 'Trúc', 0, '2013-12-22', '2017-06-21', '', '', '', 'P Thanh Cảnh', 'Nguyễn Thị Thị', '906549780', 1, 3, 28, '68 Nguyễn Hiền', 0, '2017-06-05 00:00:00'),
(195, 'Nguyễn Tâm', 'Như', 0, '2014-07-05', '2017-01-01', '', '', '1282699789', 'Ng Đình Bảo', 'Phạm Thị Thủy', '935101700', 1, 3, 26, '415 Ngô Quyền', 0, '2017-06-05 00:00:00'),
(196, 'Từ Tuấn', 'Tú', 1, '2015-04-09', '2017-01-01', '', '', '', 'Từ Văn Tây', 'Lê T Việt Trinh', '905251941', 1, 3, 28, '55 Nại Nghĩa 7', 0, '2017-06-05 00:00:00'),
(197, 'Đặng Ngọc Bảo', 'Ân', 1, '2013-06-06', '2017-06-21', '', '', '905542431', 'Đặng Ngọc Vũ', 'Ngô T Thu Uyên', '905492952', 1, 3, 28, '26 Nguyễn Trung Trực', 0, '2017-06-05 00:00:00'),
(198, 'Trần Thị Bảo', 'Ngọc', 0, '2014-07-28', '2017-01-01', '', '', '1649840904', 'Trần Văn Bảo', 'Nguyễn T Phi Yến', '1664982723', 1, 3, 28, '47 Nại Nghĩa', 0, '2017-06-05 00:00:00'),
(199, 'Lê Gia', 'Bảo', 1, '2014-02-10', '2017-01-01', '', '', '906702758', 'Lê Hồ Vĩnh', 'Dđòa Thị Ngọc Loan', '906045510', 1, 3, 30, '133 Phan Bá Phiến', 1, '2017-06-05 00:00:00'),
(200, 'Mai Ngọc', 'Khang', 1, '2014-04-07', '2017-01-01', '', '', '1202433242', 'Mai Văn Ngọc', 'Trần Thị Trịnh', '936221443', 1, 3, 28, 'Tổ 43 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(203, 'Hồ Minh', 'Huy', 1, '2014-12-18', NULL, NULL, NULL, '0934805058', 'Hồ Văn Kỳ', 'Kiều Thị Thúy Hồng', '0905782065', 1, 3, 28, '104 Dương Văn Nga', 0, '2017-06-26 12:55:14'),
(204, 'Nguyễn Phương ', 'Trinh', 0, '2014-04-16', NULL, NULL, NULL, '0905669882', 'Nguyễn Ngọc Bình', 'Trần Thị Vi', '0935553137', 1, 3, 26, '28/03-K24 An đồn', 0, '2017-06-26 12:55:14'),
(206, 'Lê Minh Như', 'Ý', 0, '2015-04-18', '2017-06-27', NULL, NULL, '0905737676', 'Lê Minh Thương', 'Lê Ngọc Thúy', '0906534348', 1, 3, 28, '40 Dương Lâm', 0, '2017-06-26 13:19:14'),
(208, 'Vi Hoàng Bảo', 'Trâm', 0, '2013-01-16', NULL, NULL, NULL, '0987838778', 'Vi Văn Chung', 'Hoàng Thị Cường', '0989159648', 1, 3, 28, 'Tổ 7', 0, '2017-06-27 09:34:47'),
(210, 'Trần Quốc Tùng', 'Anh', 1, '2015-08-28', NULL, NULL, NULL, '0905453315', 'Trần Quốc Hưng', 'Trần Thị Nhung', '0904071122', 1, 3, 28, '30 Hoàng Quốc Việt', 0, '2017-06-27 10:03:21'),
(211, 'Trương Ngọc Hạo', 'Thiên', 1, '2015-06-16', NULL, NULL, NULL, '0903547687', 'Trương Ngọc Dũng', 'Mai Thị Quỳnh Nhi', '0906547517', 1, 3, 28, 'Số 22 Nại Hiên Đông 5', 0, '2017-06-27 10:03:21'),
(212, 'Trương Ngọc Thiên', 'Kim', 0, '2015-06-16', NULL, NULL, NULL, '0903547687', 'Trương Ngọc Dũng', 'Mai Thị Quỳnh Nhi', '0906547517', 1, 3, 28, 'Số 22 Nại Hiên Đông 5', 0, '2017-06-27 10:03:21'),
(213, 'Đỗ Thanh', 'Duy', 1, '2015-10-20', '2017-06-27', NULL, NULL, '01212121273', 'Đỗ Trọng Nguyên', 'Đặng Thị Thanh Tâm', '0935481049', 1, 3, 26, '33 Nguyễn Chí Diễu', 0, '2017-06-27 10:03:21'),
(214, 'Trương Lê Thanh', 'Trúc', 0, '2015-09-16', NULL, NULL, NULL, '0934999458', 'Trương Minh Nhân', 'Lê Thị Xuân Diệu', '01208073343', 1, 3, 28, '63 Nguyễn Trung Trực', 0, '2017-06-27 10:03:21'),
(215, 'Võ Như Minh', 'Khánh', 0, '2015-06-30', NULL, NULL, NULL, NULL, 'Võ Như Đông', 'Phan Thị Minh', '0909154225', 1, 3, 26, '105 Nguyễn Trung Trực', 0, '2017-06-27 10:03:21'),
(216, 'Ngô Phước', 'Chung', 1, '2015-07-08', NULL, NULL, NULL, '01213530757', 'Ngô Văn Bé', 'Trương Thị Minh Tâm', '0905277709', 1, 3, 26, '8/19 K257 An Đồn', 0, '2017-06-27 10:03:21'),
(217, 'Phan Đăng', 'Kiệt', 1, '2015-09-04', '2017-06-27', NULL, NULL, '0966568199', 'Phan Văn Trung', 'Trần Thị Thương', '01649742123', 1, 3, 30, 'P608-C5 Nhà công vụ vùng 3 HQ', 0, '2017-06-27 12:14:16'),
(218, 'Nguyễn Thùy Vân', 'Lam', 0, '2015-01-28', '2017-06-27', NULL, NULL, '0932489885', 'Nguyễn Văn Vân', 'Nguyễn Thị Thùy Trang', '0905600067', 1, 3, 28, '01 Nại Nghĩa 7', 0, '2017-06-27 12:17:03'),
(219, 'Lê Bảo', 'Châu', 0, '2015-10-12', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, '2 ', 0, '2017-06-27 16:05:19'),
(220, 'Nhiên', 'Tiêu', 0, '2015-09-02', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '8', 0, '2017-06-27 16:05:19'),
(221, 'Quỳnh', 'Tiên', 0, '2015-09-12', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '4', 0, '2017-06-27 16:05:19'),
(222, 'Hứa Gia', 'Ân', 0, '2015-03-10', NULL, NULL, NULL, '0975253350', 'Hưa Nam Phương', 'Lê Thị Bông Sen', '0935549354', 1, 3, 28, '40 Dương Lâm', 0, '2017-06-27 16:12:23'),
(224, 'Nguyễn Trần Gia', 'Khang', 1, '2014-10-25', NULL, NULL, NULL, '0905308407', 'Nguyễn Tiến Dũng', 'Trần Thị Mộng Hải', '0982831818', 1, 3, 26, '23 Đỗ Xuân Hợp', 0, '2017-06-27 16:12:23'),
(225, 'Lê Hoàng Khánh', 'Như', 0, '2014-08-12', '2017-06-27', NULL, NULL, '0905833437', 'Hoàng Thiên Lương', 'Lê Thị Thu Thu', '0905715211', 1, 3, 26, 'Tổ 52 An Đồn', 0, '2017-06-27 16:12:23'),
(226, 'Lê Minh', 'Quân', 1, '2014-01-10', NULL, NULL, NULL, '01205969450', 'Lê Văn Vương', 'Huỳnh Thị Miêu', '01262715184', 1, 3, 28, 'Tổ 128', 0, '2017-06-27 16:12:23'),
(227, 'Nguyễn Ngọc Bảo', 'Quỳnh', 0, '2014-05-04', NULL, NULL, NULL, '0915551883', 'Nguyễn Thanh Ngọc', 'Trương Lê Thanh Tú', '0935403881', 1, 3, NULL, '65 Hoàng Quốc Việt', 0, '2017-06-27 16:31:36'),
(228, 'Huỳnh Trần Gia', 'Phúc', 1, '2013-03-08', NULL, NULL, NULL, NULL, NULL, 'Huỳnh Thị Sang', '0908578934', 1, 3, 26, 'Tổ 65', 0, '2017-06-27 16:31:36'),
(229, 'Lê Nhật', 'Phong', 1, '2013-02-20', '2017-06-28', NULL, NULL, NULL, 'Lê Thương', 'Nguyễn Thị Ni', '0935004911', 1, 3, 28, 'Tổ 107 Số 6 NHĐ5', 0, '2017-06-27 16:31:36'),
(230, 'Lê Minh', 'Vũ', 1, '2012-11-11', '2017-06-28', NULL, NULL, NULL, 'Lê Văn Cảnh', 'Đặng Thị Phương Lan', '0935446002', 1, 3, 28, 'Tổ 70 Nại Hiên Đông', 0, '2017-06-27 16:44:24'),
(231, 'Đoàn Ngọc Gia', 'Hân', 0, '2012-06-02', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '6', 0, '2017-06-27 16:44:24'),
(232, 'Lê', 'Hưng', 1, '2013-05-25', NULL, NULL, NULL, '0905148945', 'Lê Dũng', 'Hứa Thị Bích', 'Lựu', NULL, 3, 28, 'Tổ 124, 65 Hồ Sỹ Tân', 0, '2017-06-27 16:51:12'),
(233, 'Bùi Đoàn Phúc', 'Khang', 1, '2012-05-11', NULL, NULL, NULL, '0901943953', 'Bùi Văn Huy', 'Đoàn Thị Bình', '0905560739', 1, 3, 28, '102 Nguyễn Thị Ba', 0, '2017-06-27 16:51:12'),
(234, 'Đinh Ngọc', 'Minh', 0, '2013-01-24', NULL, NULL, NULL, '0988795516', 'Lê Đức Vượng', 'Nguyễn Thị Nga', '0983630392', 1, 3, 26, '26 Nguyễn Thế Lộc', 0, '2017-06-27 16:51:12'),
(235, 'Phạm Đăng', 'Duy', 1, '2014-12-10', '2017-06-28', NULL, NULL, '0935256656', 'Phạm Ngọc Linh', 'Bùi Thị Thơm', '0935312356', 1, 3, 26, '08 Trương Hán Siêu', 0, '2017-06-28 07:10:17'),
(238, 'Ngô Minh', 'Duy', 1, '2015-06-20', NULL, NULL, NULL, '0906153612', 'Ngô Xuân Phúc', 'Huỳnh Thị Lan', '0935804811', 1, 3, 26, 'Tổ 64 An Tân', 0, '2017-06-28 09:50:18'),
(239, 'Nguyễn Trần Bảo', 'Ngọc', 0, '2015-04-12', '2017-06-28', NULL, NULL, NULL, 'Nguyễn Đức Hoàng', 'Trần P Song Loan', '0902957753', 1, 3, 28, '18 Nại Thịnh 7', 0, '2017-06-28 09:50:18'),
(240, 'Nguyễn Hoàng', 'Nam', 1, '2013-05-06', NULL, NULL, NULL, '01202567099', 'Nguyễn Quang Minh', 'Trần Thị Kim Hoa', '0905431443', 1, 3, 26, '39 Đông Du', 0, '2017-06-28 09:55:30'),
(242, 'Trần Thái Bảo', 'Trân', 0, '2013-03-27', '2017-06-28', NULL, NULL, NULL, 'Trần Ngọc Thịnh', 'Thái Thị Kim Anh', '0935595189', 1, 3, NULL, 'Tổ 73 Chung Cư', 0, '2017-06-28 09:58:23'),
(243, 'Huỳnh Nguyễn Bảo', 'Ngọc', 0, '2012-07-21', NULL, NULL, NULL, '01216043806', 'Huỳnh Minh Thương', 'Nguyễn Thị Bông', '0905719396', 1, 3, 28, '05 Nại Tú I', 0, '2017-06-28 09:58:23'),
(244, 'Nguyễn', 'Lợi', 1, '2012-05-19', NULL, NULL, NULL, '01655461466', 'Nguyễn Mỹ', 'Lưu Thị Hồng Thắm', '0905300212', 1, 3, 28, '56 Nguyễn Hiền', 0, '2017-07-03 09:07:19');

-- --------------------------------------------------------

--
-- Table structure for table `hocsinh_lop`
--

CREATE TABLE IF NOT EXISTS `hocsinh_lop` (
  `HocSinhLopId` int(11) NOT NULL AUTO_INCREMENT,
  `HocSinhId` int(11) NOT NULL,
  `LopId` int(11) NOT NULL,
  `DateJoin` date NOT NULL,
  `DateLeft` date DEFAULT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateDeleted` date DEFAULT NULL,
  PRIMARY KEY (`HocSinhLopId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=326 ;

--
-- Dumping data for table `hocsinh_lop`
--

INSERT INTO `hocsinh_lop` (`HocSinhLopId`, `HocSinhId`, `LopId`, `DateJoin`, `DateLeft`, `DateCreated`, `DateDeleted`) VALUES
(1, 1, 7, '2017-01-01', '2017-06-21', '2017-06-05 04:44:00', '2017-06-21'),
(2, 2, 7, '2017-01-01', '2017-06-21', '2017-06-05 04:44:00', '2017-06-21'),
(3, 3, 7, '2017-01-01', '2017-06-21', '2017-06-05 04:44:00', '2017-06-21'),
(4, 4, 7, '2017-01-01', '2017-06-21', '2017-06-05 04:44:00', '2017-06-21'),
(5, 5, 7, '2017-01-01', '2017-06-21', '2017-06-05 04:44:00', '2017-06-21'),
(6, 6, 7, '2017-01-01', '2017-06-21', '2017-06-05 04:44:00', '2017-06-21'),
(7, 7, 7, '2017-01-01', '2017-06-21', '2017-06-05 04:44:00', '2017-06-21'),
(8, 8, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(9, 9, 7, '2017-01-01', '2017-06-21', '2017-06-05 04:44:00', '2017-06-21'),
(10, 10, 7, '2017-01-01', '2017-06-21', '2017-06-05 04:44:00', '2017-06-21'),
(11, 11, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(12, 12, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(13, 13, 7, '2017-01-01', '2017-06-21', '2017-06-05 04:44:00', '2017-06-21'),
(14, 14, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(15, 15, 7, '2017-01-01', '2017-06-21', '2017-06-05 04:44:00', '2017-06-21'),
(16, 16, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(17, 17, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(18, 18, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(19, 19, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(20, 20, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(21, 21, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(22, 22, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(23, 23, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(24, 24, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(25, 25, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(26, 26, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(27, 27, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(28, 28, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(29, 29, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(30, 30, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(31, 31, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(32, 32, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(33, 33, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(34, 34, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(35, 35, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(36, 36, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(37, 37, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(38, 38, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(39, 39, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(40, 40, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(41, 41, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(42, 42, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(43, 43, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(44, 44, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(45, 45, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(46, 46, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(47, 47, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(48, 48, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(49, 49, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(50, 50, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(51, 51, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(52, 52, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(53, 53, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(54, 54, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(55, 55, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(56, 56, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(57, 57, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(58, 58, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(59, 59, 5, '2017-01-01', '2017-06-20', '2017-06-05 07:35:49', '2017-06-20'),
(60, 60, 5, '2017-01-01', '2017-06-20', '2017-06-05 07:35:49', '2017-06-20'),
(61, 61, 5, '2017-01-01', '2017-06-20', '2017-06-05 07:35:49', '2017-06-20'),
(62, 62, 5, '2017-01-01', '2017-06-20', '2017-06-05 07:35:49', '2017-06-20'),
(63, 63, 5, '2017-01-01', '2017-06-21', '2017-06-05 07:35:49', '2017-06-21'),
(64, 64, 5, '2017-01-01', '2017-06-21', '2017-06-05 07:35:49', '2017-06-21'),
(65, 65, 5, '2017-01-01', '2017-06-20', '2017-06-05 07:35:49', '2017-06-20'),
(66, 66, 5, '2017-01-01', '2017-06-21', '2017-06-05 07:35:49', '2017-06-21'),
(67, 67, 5, '2017-01-01', '2017-06-21', '2017-06-05 07:35:49', '2017-06-21'),
(68, 68, 5, '2017-01-01', '2017-06-20', '2017-06-05 07:35:49', '2017-06-20'),
(69, 69, 5, '2017-01-01', '2017-06-20', '2017-06-05 07:35:49', '2017-06-20'),
(70, 70, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(71, 71, 5, '2017-01-01', '2017-06-20', '2017-06-05 07:35:49', '2017-06-20'),
(72, 72, 5, '2017-01-01', '2017-06-20', '2017-06-05 07:35:49', '2017-06-20'),
(73, 73, 5, '2017-01-01', '2017-06-20', '2017-06-05 07:35:49', '2017-06-20'),
(74, 74, 5, '2017-01-01', '2017-06-20', '2017-06-05 07:35:49', '2017-06-20'),
(75, 75, 5, '2017-01-01', '2017-06-21', '2017-06-05 07:35:49', '2017-06-21'),
(76, 76, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(77, 77, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(78, 78, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(79, 79, 5, '2017-01-01', '2017-06-20', '2017-06-05 07:35:49', '2017-06-20'),
(80, 80, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(81, 81, 5, '2017-01-01', '2017-06-20', '2017-06-05 07:35:49', '2017-06-20'),
(82, 82, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(83, 83, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(84, 84, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(85, 85, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(86, 86, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(87, 87, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(88, 88, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(89, 89, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(90, 90, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(91, 91, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(92, 92, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(93, 93, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(94, 94, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(95, 95, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(96, 96, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(97, 97, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(98, 98, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(99, 99, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(100, 100, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(101, 101, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(102, 102, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(103, 103, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(104, 104, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(105, 105, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(106, 106, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(107, 107, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(108, 108, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(109, 109, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(110, 110, 3, '2017-01-01', '2017-06-23', '2017-06-05 07:50:44', '2017-06-23'),
(111, 111, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(112, 112, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(113, 113, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(114, 114, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(115, 115, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(116, 116, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(117, 117, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(118, 118, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(119, 119, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(120, 120, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(121, 121, 3, '2017-01-01', '2017-06-23', '2017-06-05 07:50:44', '2017-06-23'),
(122, 122, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(123, 123, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(124, 124, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(125, 125, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(126, 126, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(127, 127, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(128, 128, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(129, 129, 3, '2017-01-01', '2017-06-21', '2017-06-05 07:50:44', '2017-06-21'),
(130, 130, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(131, 131, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(132, 132, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(133, 133, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(134, 134, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(135, 135, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(136, 136, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(137, 137, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(138, 138, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(139, 139, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(140, 140, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(141, 141, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(142, 142, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(143, 143, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(144, 144, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(145, 145, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(146, 146, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(147, 147, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(148, 148, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(149, 149, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(150, 150, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(151, 151, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(152, 152, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(153, 153, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(154, 154, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(155, 155, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(156, 156, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(157, 157, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(158, 158, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(159, 159, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(160, 160, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(161, 161, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(162, 162, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(163, 163, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(164, 164, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(165, 165, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(166, 166, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(167, 167, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(168, 168, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(169, 169, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(170, 170, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(171, 171, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(172, 172, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(173, 173, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(174, 174, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(175, 175, 6, '2017-01-01', '2017-06-27', '2017-06-05 08:19:48', '2017-06-27'),
(176, 176, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(177, 177, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(178, 178, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(179, 179, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(180, 180, 6, '2017-01-01', '2017-06-27', '2017-06-05 08:19:48', '2017-06-27'),
(181, 181, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(182, 182, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(183, 183, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(184, 184, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(185, 185, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(186, 186, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(187, 187, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(188, 188, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(189, 189, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(190, 190, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(191, 191, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(192, 192, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(193, 193, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(194, 194, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(195, 195, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(196, 196, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(197, 197, 6, '2017-01-01', '2017-06-21', '2017-06-05 08:19:48', '2017-06-21'),
(198, 198, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(199, 199, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(200, 200, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(201, 65, 4, '2017-06-20', NULL, '2017-06-20 08:25:40', NULL),
(202, 69, 4, '2017-06-20', NULL, '2017-06-20 08:25:41', NULL),
(203, 81, 4, '2017-06-20', NULL, '2017-06-20 08:25:41', NULL),
(204, 71, 4, '2017-06-20', NULL, '2017-06-20 08:25:41', NULL),
(205, 60, 4, '2017-06-20', NULL, '2017-06-20 08:25:41', NULL),
(206, 59, 4, '2017-06-20', NULL, '2017-06-20 08:25:41', NULL),
(207, 72, 4, '2017-06-20', NULL, '2017-06-20 08:25:41', NULL),
(208, 79, 4, '2017-06-20', NULL, '2017-06-20 08:28:47', NULL),
(209, 62, 4, '2017-06-20', NULL, '2017-06-20 08:28:47', NULL),
(210, 74, 4, '2017-06-20', NULL, '2017-06-20 08:28:47', NULL),
(211, 61, 4, '2017-06-20', NULL, '2017-06-20 08:36:19', NULL),
(212, 73, 4, '2017-06-20', NULL, '2017-06-20 08:36:19', NULL),
(213, 68, 4, '2017-06-20', NULL, '2017-06-20 08:36:19', NULL),
(214, 184, 4, '2017-06-21', NULL, '2017-06-21 00:12:31', NULL),
(215, 183, 4, '2017-06-21', '2017-06-21', '2017-06-21 00:12:31', '2017-06-21'),
(216, 185, 4, '2017-06-21', NULL, '2017-06-21 00:12:31', NULL),
(217, 182, 4, '2017-06-21', NULL, '2017-06-21 00:12:31', NULL),
(218, 187, 4, '2017-06-21', NULL, '2017-06-21 00:12:31', NULL),
(219, 192, 4, '2017-06-21', NULL, '2017-06-21 00:12:31', NULL),
(220, 194, 4, '2017-06-21', NULL, '2017-06-21 00:12:31', NULL),
(221, 197, 4, '2017-06-21', NULL, '2017-06-21 00:12:31', NULL),
(222, 173, 4, '2017-06-21', NULL, '2017-06-21 00:12:31', NULL),
(223, 176, 4, '2017-06-21', NULL, '2017-06-21 00:12:31', NULL),
(224, 172, 4, '2017-06-21', NULL, '2017-06-21 00:12:32', NULL),
(225, 186, 4, '2017-06-21', NULL, '2017-06-21 00:12:32', NULL),
(226, 179, 4, '2017-06-21', NULL, '2017-06-21 00:12:32', NULL),
(227, 183, 4, '2017-06-21', NULL, '2017-06-21 00:14:55', NULL),
(228, 1, 5, '2017-06-21', NULL, '2017-06-21 00:20:54', NULL),
(229, 2, 5, '2017-06-21', NULL, '2017-06-21 00:21:13', NULL),
(230, 7, 5, '2017-06-21', NULL, '2017-06-21 00:21:13', NULL),
(231, 9, 5, '2017-06-21', NULL, '2017-06-21 00:21:23', NULL),
(232, 13, 5, '2017-06-21', NULL, '2017-06-21 00:21:30', NULL),
(233, 3, 6, '2017-06-21', NULL, '2017-06-21 00:23:50', NULL),
(234, 4, 6, '2017-06-21', NULL, '2017-06-21 00:24:34', NULL),
(235, 5, 6, '2017-06-21', NULL, '2017-06-21 00:24:35', NULL),
(236, 6, 6, '2017-06-21', NULL, '2017-06-21 00:24:35', NULL),
(237, 10, 6, '2017-06-21', NULL, '2017-06-21 00:24:35', NULL),
(238, 15, 6, '2017-06-21', NULL, '2017-06-21 00:24:44', NULL),
(239, 181, 4, '2017-06-21', NULL, '2017-06-21 00:31:48', NULL),
(240, 174, 1, '2017-06-21', NULL, '2017-06-21 00:33:06', NULL),
(241, 188, 1, '2017-06-21', NULL, '2017-06-21 00:33:14', NULL),
(242, 177, 1, '2017-06-21', NULL, '2017-06-21 00:33:27', NULL),
(243, 89, 1, '2017-06-21', NULL, '2017-06-21 00:34:00', NULL),
(244, 102, 1, '2017-06-21', NULL, '2017-06-21 00:34:07', NULL),
(245, 111, 1, '2017-06-21', NULL, '2017-06-21 00:34:34', NULL),
(246, 112, 1, '2017-06-21', NULL, '2017-06-21 00:34:39', NULL),
(247, 122, 1, '2017-06-21', NULL, '2017-06-21 00:34:55', NULL),
(248, 117, 1, '2017-06-21', NULL, '2017-06-21 00:35:11', NULL),
(249, 118, 1, '2017-06-21', NULL, '2017-06-21 00:35:20', NULL),
(250, 123, 1, '2017-06-21', NULL, '2017-06-21 00:35:42', NULL),
(251, 126, 1, '2017-06-21', NULL, '2017-06-21 00:36:05', NULL),
(252, 127, 1, '2017-06-21', NULL, '2017-06-21 00:36:13', NULL),
(253, 128, 1, '2017-06-21', NULL, '2017-06-21 00:36:26', NULL),
(254, 129, 1, '2017-06-21', NULL, '2017-06-21 00:36:33', NULL),
(255, 120, 1, '2017-06-21', NULL, '2017-06-21 00:36:48', NULL),
(256, 94, 1, '2017-06-21', NULL, '2017-06-21 00:37:01', NULL),
(257, 96, 1, '2017-06-21', NULL, '2017-06-21 00:37:10', NULL),
(258, 109, 1, '2017-06-21', NULL, '2017-06-21 00:37:31', NULL),
(259, 90, 1, '2017-06-21', NULL, '2017-06-21 00:38:42', NULL),
(260, 101, 2, '2017-06-21', NULL, '2017-06-21 01:57:14', NULL),
(261, 113, 2, '2017-06-21', NULL, '2017-06-21 01:57:37', NULL),
(262, 114, 2, '2017-06-21', NULL, '2017-06-21 01:57:37', NULL),
(263, 91, 2, '2017-06-21', NULL, '2017-06-21 01:57:50', NULL),
(264, 95, 2, '2017-06-21', NULL, '2017-06-21 01:58:27', NULL),
(265, 100, 2, '2017-06-21', NULL, '2017-06-21 01:58:50', NULL),
(266, 116, 2, '2017-06-21', NULL, '2017-06-21 01:59:39', NULL),
(267, 115, 2, '2017-06-21', NULL, '2017-06-21 01:59:39', NULL),
(268, 119, 2, '2017-06-21', NULL, '2017-06-21 02:01:54', NULL),
(269, 124, 2, '2017-06-21', NULL, '2017-06-21 02:01:54', NULL),
(270, 125, 2, '2017-06-21', NULL, '2017-06-21 02:01:54', NULL),
(271, 92, 2, '2017-06-21', '2017-06-27', '2017-06-21 02:01:54', '2017-06-27'),
(272, 97, 2, '2017-06-21', NULL, '2017-06-21 02:01:54', NULL),
(273, 98, 2, '2017-06-21', NULL, '2017-06-21 02:02:23', NULL),
(274, 103, 2, '2017-06-21', NULL, '2017-06-21 02:02:23', NULL),
(275, 104, 2, '2017-06-21', NULL, '2017-06-21 02:02:23', NULL),
(276, 99, 2, '2017-06-21', NULL, '2017-06-21 02:02:46', NULL),
(277, 106, 2, '2017-06-21', NULL, '2017-06-21 02:02:47', NULL),
(278, 93, 1, '2017-06-21', NULL, '2017-06-21 02:04:07', NULL),
(279, 178, 2, '2017-06-21', NULL, '2017-06-21 02:06:27', NULL),
(280, 121, 2, '2017-06-23', NULL, '2017-06-23 09:41:13', NULL),
(281, 110, 2, '2017-06-23', NULL, '2017-06-23 09:41:40', NULL),
(282, 205, 1, '2017-06-25', '2017-06-27', '2017-06-27 02:08:58', '2017-06-27'),
(283, 206, 5, '2017-06-27', NULL, '2017-06-27 02:24:20', NULL),
(284, 64, 1, '2017-06-27', NULL, '2017-06-27 02:24:40', NULL),
(285, 63, 4, '2017-06-27', NULL, '2017-06-27 02:24:54', NULL),
(286, 75, 6, '2017-06-27', NULL, '2017-06-27 02:25:07', NULL),
(287, 203, 6, '2017-06-27', NULL, '2017-06-27 02:25:39', NULL),
(288, 204, 6, '2017-06-27', NULL, '2017-06-27 02:25:47', NULL),
(289, 92, 1, '2017-06-27', NULL, '2017-06-27 02:49:51', NULL),
(290, 218, 6, '2017-06-27', NULL, '2017-06-27 08:55:50', NULL),
(291, 217, 7, '2017-06-27', NULL, '2017-06-27 08:56:13', NULL),
(292, 208, 1, '2017-06-27', NULL, '2017-06-27 08:57:46', NULL),
(293, 209, 7, '2017-06-27', NULL, '2017-06-27 09:01:27', NULL),
(294, 211, 7, '2017-06-27', NULL, '2017-06-27 09:04:06', NULL),
(295, 210, 7, '2017-06-27', NULL, '2017-06-27 09:04:16', NULL),
(296, 215, 7, '2017-06-27', NULL, '2017-06-27 09:04:24', NULL),
(297, 214, 7, '2017-06-27', NULL, '2017-06-27 09:04:31', NULL),
(298, 213, 7, '2017-06-27', NULL, '2017-06-27 09:04:36', NULL),
(299, 212, 7, '2017-06-27', NULL, '2017-06-27 09:04:43', NULL),
(300, 216, 7, '2017-06-27', NULL, '2017-06-27 09:04:52', NULL),
(301, 180, 4, '2017-06-27', NULL, '2017-06-27 09:40:38', NULL),
(302, 175, 2, '2017-06-27', NULL, '2017-06-27 09:46:18', NULL),
(303, 226, 5, '2017-06-27', NULL, '2017-06-27 10:05:16', NULL),
(304, 225, 5, '2017-06-27', NULL, '2017-06-27 10:05:17', NULL),
(305, 224, 5, '2017-06-27', NULL, '2017-06-27 10:06:10', NULL),
(306, 222, 5, '2017-06-27', NULL, '2017-06-27 10:06:11', NULL),
(307, 231, 1, '2017-06-28', NULL, '2017-06-28 02:46:45', NULL),
(308, 230, 1, '2017-06-28', NULL, '2017-06-28 02:47:23', NULL),
(309, 233, 2, '2017-06-28', NULL, '2017-06-28 02:47:47', NULL),
(310, 232, 2, '2017-06-28', NULL, '2017-06-28 02:47:59', NULL),
(311, 234, 2, '2017-06-28', NULL, '2017-06-28 02:48:06', NULL),
(312, 229, 4, '2017-06-28', NULL, '2017-06-28 02:48:21', NULL),
(313, 228, 4, '2017-06-28', NULL, '2017-06-28 02:48:32', NULL),
(314, 227, 4, '2017-06-28', NULL, '2017-06-28 02:48:37', NULL),
(315, 235, 5, '2017-06-28', NULL, '2017-06-28 02:49:04', NULL),
(316, 220, 7, '2017-06-28', NULL, '2017-06-28 02:49:36', NULL),
(317, 219, 7, '2017-06-28', NULL, '2017-06-28 02:49:42', NULL),
(318, 221, 7, '2017-06-28', NULL, '2017-06-28 02:49:47', NULL),
(319, 238, 6, '2017-06-28', NULL, '2017-06-28 05:23:35', NULL),
(320, 239, 6, '2017-06-28', NULL, '2017-06-28 05:23:42', NULL),
(321, 240, 4, '2017-06-28', NULL, '2017-06-28 05:23:57', NULL),
(322, 241, 1, '2017-06-28', NULL, '2017-06-28 05:24:25', NULL),
(323, 242, 1, '2017-06-28', NULL, '2017-06-28 05:24:34', NULL),
(324, 243, 1, '2017-06-28', NULL, '2017-06-28 05:25:04', NULL),
(325, 244, 2, '2017-06-28', NULL, '2017-06-28 05:25:14', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `hoctap`
--

CREATE TABLE IF NOT EXISTS `hoctap` (
  `HocTapId` int(11) NOT NULL AUTO_INCREMENT,
  `HocSinhLopId` int(11) NOT NULL,
  `Hoc2Buoi` tinyint(1) DEFAULT NULL,
  `ChuyenCan` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AnTaiTruong` tinyint(1) DEFAULT NULL,
  `HocLienTuc` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TheoDoiCanNang` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SDDCanNang` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TheoDoiChieuCao` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SDDChieuCao` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `HoanThanhCTGDMN` tinyint(1) DEFAULT NULL,
  `DanhGia` text COLLATE utf8_unicode_ci,
  PRIMARY KEY (`HocTapId`),
  KEY `HocSinhId` (`HocSinhLopId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `khoanthu`
--

CREATE TABLE IF NOT EXISTS `khoanthu` (
  `KhoanThuId` int(11) NOT NULL AUTO_INCREMENT,
  `Ten` varchar(1000) COLLATE utf8_unicode_ci NOT NULL,
  `SoTien` bigint(20) DEFAULT NULL,
  `IsBatBuoc` tinyint(1) NOT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateDeleted` date DEFAULT NULL,
  PRIMARY KEY (`KhoanThuId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=7 ;

--
-- Dumping data for table `khoanthu`
--

INSERT INTO `khoanthu` (`KhoanThuId`, `Ten`, `SoTien`, `IsBatBuoc`, `DateCreated`, `DateDeleted`) VALUES
(1, 'Tiền ăn & sữa', NULL, 1, '2017-03-07 07:57:23', NULL),
(2, 'Phụ phí', NULL, 1, '2017-03-07 07:57:23', NULL),
(3, 'Bán trú', NULL, 1, '2017-03-07 07:57:23', NULL),
(4, 'Học phí', NULL, 1, '2017-03-07 07:57:23', NULL),
(5, 'Ăn sáng', NULL, 1, '2017-03-09 04:03:36', NULL),
(6, 'Ăn tối', NULL, 1, '2017-03-21 09:14:37', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `khoanthuhangnam`
--

CREATE TABLE IF NOT EXISTS `khoanthuhangnam` (
  `KhoanThuHangNamId` int(11) NOT NULL AUTO_INCREMENT,
  `KhoanThuId` int(11) NOT NULL,
  `KhoiId` int(11) NOT NULL,
  `NgayTinh` date DEFAULT NULL,
  `NgayKetThuc` date DEFAULT NULL,
  `IsBatBuoc` tinyint(1) NOT NULL,
  `SoTien` bigint(20) NOT NULL,
  PRIMARY KEY (`KhoanThuHangNamId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=25 ;

--
-- Dumping data for table `khoanthuhangnam`
--

INSERT INTO `khoanthuhangnam` (`KhoanThuHangNamId`, `KhoanThuId`, `KhoiId`, `NgayTinh`, `NgayKetThuc`, `IsBatBuoc`, `SoTien`) VALUES
(1, 1, 1, '2017-07-01', NULL, 1, 442000),
(2, 2, 1, '2015-01-01', NULL, 1, 158000),
(3, 3, 1, '2015-01-01', NULL, 1, 260000),
(4, 4, 1, '2015-01-01', NULL, 1, 390000),
(5, 1, 2, '2017-07-01', NULL, 1, 442000),
(6, 2, 2, '2015-01-01', NULL, 1, 158000),
(7, 3, 2, '2015-01-01', NULL, 1, 260000),
(8, 4, 2, '2015-01-01', NULL, 1, 390000),
(9, 1, 3, '2017-07-01', NULL, 1, 442000),
(10, 2, 3, '2015-01-01', NULL, 1, 183000),
(11, 3, 3, '2015-01-01', NULL, 1, 265000),
(12, 4, 3, '2015-01-01', NULL, 1, 410000),
(13, 1, 4, '2017-07-01', NULL, 1, 442000),
(14, 2, 4, '2015-01-01', NULL, 1, 198000),
(15, 3, 4, '2015-01-01', NULL, 1, 280000),
(16, 4, 4, '2015-01-01', NULL, 1, 430000),
(17, 5, 1, '2015-01-01', NULL, 1, 200000),
(18, 5, 2, '2015-01-01', NULL, 1, 200000),
(19, 5, 3, '2015-01-01', NULL, 1, 200000),
(20, 5, 4, '2015-01-01', NULL, 1, 200000),
(21, 6, 1, '2015-01-01', NULL, 1, 200000),
(22, 6, 2, '2015-01-01', NULL, 1, 200000),
(23, 6, 3, '2015-01-01', NULL, 1, 200000),
(24, 6, 4, '2015-01-01', NULL, 1, 200000);

-- --------------------------------------------------------

--
-- Table structure for table `khoi`
--

CREATE TABLE IF NOT EXISTS `khoi` (
  `KhoiId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Description` text COLLATE utf8_unicode_ci,
  PRIMARY KEY (`KhoiId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=5 ;

--
-- Dumping data for table `khoi`
--

INSERT INTO `khoi` (`KhoiId`, `Name`, `Description`) VALUES
(1, 'Lớn', 'Khối lớn'),
(2, 'Nhỡ', 'Khối nhỡ'),
(3, 'Bé', 'Khối bé'),
(4, 'Trẻ', 'Khối trẻ');

-- --------------------------------------------------------

--
-- Table structure for table `khoi_truong`
--

CREATE TABLE IF NOT EXISTS `khoi_truong` (
  `KhoiTruongId` int(11) NOT NULL AUTO_INCREMENT,
  `KhoiId` int(11) NOT NULL,
  `TruongId` int(11) NOT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateDeleted` date DEFAULT NULL,
  PRIMARY KEY (`KhoiTruongId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=10 ;

--
-- Dumping data for table `khoi_truong`
--

INSERT INTO `khoi_truong` (`KhoiTruongId`, `KhoiId`, `TruongId`, `DateCreated`, `DateDeleted`) VALUES
(6, 1, 1, '2017-03-01 07:21:15', NULL),
(7, 2, 1, '2017-03-01 07:21:15', NULL),
(8, 3, 1, '2017-03-01 07:21:15', NULL),
(9, 4, 1, '2017-03-01 07:21:15', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `lop`
--

CREATE TABLE IF NOT EXISTS `lop` (
  `LopId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Description` text COLLATE utf8_unicode_ci,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateDeleted` date DEFAULT NULL,
  PRIMARY KEY (`LopId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=11 ;

--
-- Dumping data for table `lop`
--

INSERT INTO `lop` (`LopId`, `Name`, `Description`, `DateCreated`, `DateDeleted`) VALUES
(1, 'Lớn 1', 'Lớp lớn 1', '2016-12-31 17:00:00', NULL),
(2, 'Lớn 2', 'Lớp lớn 2', '2016-12-31 17:00:00', NULL),
(3, 'Nhỡ 1', 'Lớp nhỡ 1', '2016-12-31 17:00:00', NULL),
(4, 'Nhỡ 2', 'Lớp nhỡ 2', '2016-12-31 17:00:00', NULL),
(5, 'Bé 1', 'Lớp bé 1', '2016-12-31 17:00:00', NULL),
(6, 'Bé 2', 'Lớp bé 2', '2016-12-31 17:00:00', NULL),
(7, 'Trẻ 1', 'Lớp trẻ 1', '2016-12-31 17:00:00', NULL),
(10, 'Trẻ 2', 'Lớp trẻ 2', '2017-03-15 08:31:04', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `lop_khoi`
--

CREATE TABLE IF NOT EXISTS `lop_khoi` (
  `LopKhoiId` int(11) NOT NULL AUTO_INCREMENT,
  `LopId` int(11) NOT NULL,
  `KhoiId` int(11) NOT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateDeleted` date DEFAULT NULL,
  PRIMARY KEY (`LopKhoiId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=58 ;

--
-- Dumping data for table `lop_khoi`
--

INSERT INTO `lop_khoi` (`LopKhoiId`, `LopId`, `KhoiId`, `DateCreated`, `DateDeleted`) VALUES
(48, 1, 1, '2017-03-15 08:36:05', NULL),
(49, 2, 1, '2017-03-15 08:36:05', NULL),
(50, 3, 2, '2017-03-15 08:36:05', NULL),
(51, 4, 2, '2017-03-15 08:36:05', NULL),
(52, 5, 3, '2017-03-15 08:36:05', NULL),
(53, 6, 3, '2017-03-15 08:36:05', NULL),
(54, 7, 4, '2017-03-15 08:36:06', NULL),
(57, 10, 4, '2017-03-15 08:36:33', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `phanloaichi`
--

CREATE TABLE IF NOT EXISTS `phanloaichi` (
  `PhanLoaiChiId` int(11) NOT NULL AUTO_INCREMENT,
  `MaPhanLoai` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `DienGiai` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`PhanLoaiChiId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=9 ;

--
-- Dumping data for table `phanloaichi`
--

INSERT INTO `phanloaichi` (`PhanLoaiChiId`, `MaPhanLoai`, `DienGiai`) VALUES
(1, 'TP', 'Thực phẩm'),
(3, 'DCHT', 'Dụng cụ học tập'),
(4, 'TP', 'Sữa'),
(5, 'PP', 'Giấy VS'),
(6, 'Phụ phí', 'Nước xả vải'),
(7, 'Phụ phí', 'Nước lau sàn'),
(8, 'Phụ phí', 'Nước sinh hoạt');

-- --------------------------------------------------------

--
-- Table structure for table `phanloaithu`
--

CREATE TABLE IF NOT EXISTS `phanloaithu` (
  `PhanLoaiThuId` int(11) NOT NULL AUTO_INCREMENT,
  `DienGiai` text COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`PhanLoaiThuId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=3 ;

--
-- Dumping data for table `phanloaithu`
--

INSERT INTO `phanloaithu` (`PhanLoaiThuId`, `DienGiai`) VALUES
(1, 'Thu tiền học phí'),
(2, 'Thu thử');

-- --------------------------------------------------------

--
-- Table structure for table `phieuchi`
--

CREATE TABLE IF NOT EXISTS `phieuchi` (
  `PhieuChiId` int(11) NOT NULL AUTO_INCREMENT,
  `MaPhieu` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Ngay` date NOT NULL,
  `SoTien` bigint(20) NOT NULL,
  `GhiChu` text COLLATE utf8_unicode_ci,
  `PhanLoaiChiId` int(11) NOT NULL,
  `NoiDung` text COLLATE utf8_unicode_ci,
  `SoLuong` double DEFAULT NULL,
  `DonGia` double DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`PhieuChiId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=6 ;

--
-- Dumping data for table `phieuchi`
--

INSERT INTO `phieuchi` (`PhieuChiId`, `MaPhieu`, `Ngay`, `SoTien`, `GhiChu`, `PhanLoaiChiId`, `NoiDung`, `SoLuong`, `DonGia`, `CreatedDate`) VALUES
(2, '1234', '2017-07-17', 40000, 'test', 3, 'Dụng cụ học tập', 1, 40000, '2017-07-25 07:14:48'),
(3, '12', '2017-07-17', 100000, '', 8, 'Nước sinh hoạt', 1, 100000, '2017-07-24 10:34:53'),
(4, '1233', '2017-07-01', 15000, '', 6, 'Nước xả vải 123', 1, 15000, '2017-07-26 03:24:01'),
(5, '121', '2017-07-01', 2323000, '', 4, 'Sữa Ensure', 1, 2323000, '2017-07-26 03:39:15');

-- --------------------------------------------------------

--
-- Table structure for table `phieuthu`
--

CREATE TABLE IF NOT EXISTS `phieuthu` (
  `PhieuThuId` int(11) NOT NULL AUTO_INCREMENT,
  `Ngay` date NOT NULL,
  `SoTien` bigint(20) NOT NULL,
  `MaPhieu` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `GhiChu` text COLLATE utf8_unicode_ci,
  `PhanLoaiThuId` int(11) DEFAULT NULL,
  `HocSinhId` int(11) DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`PhieuThuId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=8 ;

--
-- Dumping data for table `phieuthu`
--

INSERT INTO `phieuthu` (`PhieuThuId`, `Ngay`, `SoTien`, `MaPhieu`, `GhiChu`, `PhanLoaiThuId`, `HocSinhId`, `CreatedDate`) VALUES
(1, '2017-07-05', 1550000, '12', '', 1, 2, '2017-07-13 04:51:40'),
(2, '2017-07-05', 1000000, '121', '', 1, 4, '2017-07-19 08:13:34'),
(3, '2017-07-05', 1250000, '14', '', 1, 6, '2017-07-10 02:38:03'),
(4, '2017-07-31', 1000000, '15', '', 1, 1, '2017-07-26 08:18:21'),
(5, '2017-07-05', 1200000, '16', '', 1, 3, '2017-07-10 02:38:34'),
(6, '2017-07-15', 1200000, '17', '', 1, 5, '2017-07-26 08:18:16'),
(7, '2017-07-05', 255000, '1200', '', 2, NULL, '2017-08-08 08:18:00');

-- --------------------------------------------------------

--
-- Table structure for table `phuongxa`
--

CREATE TABLE IF NOT EXISTS `phuongxa` (
  `PhuongXaId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `QuanHuyenId` int(11) NOT NULL,
  PRIMARY KEY (`PhuongXaId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=52 ;

--
-- Dumping data for table `phuongxa`
--

INSERT INTO `phuongxa` (`PhuongXaId`, `Name`, `QuanHuyenId`) VALUES
(1, 'Hải Châu 1', 1),
(2, 'Hải Châu 2', 1),
(3, 'Bình Hiên', 1),
(4, 'Thanh Bình', 1),
(5, 'Thuận Phước', 1),
(6, 'Thạch Thang', 1),
(7, 'Phước Ninh', 1),
(8, 'Hòa Thuận Tây', 1),
(9, 'Hòa Thuận Đông', 1),
(10, 'Nam Dương', 1),
(11, 'Bình Thuận', 1),
(12, 'Hòa Cường Bắc', 1),
(13, 'Hòa Cường Nam', 1),
(14, 'An Khê', 2),
(15, 'Thanh Khê Đông', 2),
(16, 'Xuân Hà', 2),
(17, 'Tam Thuận', 2),
(18, 'Tân Chính', 2),
(19, 'Thạc Gián', 2),
(20, 'Chính Gián', 2),
(21, 'Hòa Khê', 2),
(22, 'Vĩnh Trung', 2),
(23, 'Thanh Khê Tây', 2),
(24, 'AN HẢI ĐÔNG', 3),
(25, 'An Hải Tây', 3),
(26, 'An Hải Băc', 3),
(27, 'Phước Mỹ', 3),
(28, 'Nại Hiên Đông', 3),
(29, 'Mân Thái', 3),
(30, 'Thọ Quang', 3),
(31, 'Hòa An', 4),
(32, 'Hòa Phát', 4),
(33, 'Hòa Thọ Đông', 4),
(34, 'Hòa Thọ Tây', 4),
(35, 'Hòa Xuân', 4),
(36, 'Khuê Trung', 4),
(37, 'Hòa Hải', 5),
(38, 'Mỹ An', 5),
(39, 'Khuê Mỹ', 5),
(40, 'Hòa Quý', 5),
(41, 'Hòa Bắc', 6),
(42, 'Hòa Châu', 6),
(43, 'Hòa Khương', 6),
(44, 'Hòa Liên', 6),
(45, 'Hòa Nhơn', 6),
(46, 'Hòa Ninh', 6),
(47, 'Hòa Phong', 6),
(48, 'Hòa Phú', 6),
(49, 'Hòa Phước', 6),
(50, 'Hòa Sơn', 6),
(51, 'Hòa Tiến', 6);

-- --------------------------------------------------------

--
-- Table structure for table `privilege`
--

CREATE TABLE IF NOT EXISTS `privilege` (
  `PrivilegeId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`PrivilegeId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=5 ;

--
-- Dumping data for table `privilege`
--

INSERT INTO `privilege` (`PrivilegeId`, `Name`) VALUES
(1, 'Xem danh sách phiếu chi'),
(2, 'Xem danh sách phiếu thu'),
(3, 'Xem sổ quỹ tiền mặt'),
(4, 'Xem báo cáo thu chi');

-- --------------------------------------------------------

--
-- Table structure for table `quanhuyen`
--

CREATE TABLE IF NOT EXISTS `quanhuyen` (
  `QuanHuyenId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `ThanhPhoId` int(11) NOT NULL,
  PRIMARY KEY (`QuanHuyenId`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=7 ;

--
-- Dumping data for table `quanhuyen`
--

INSERT INTO `quanhuyen` (`QuanHuyenId`, `Name`, `ThanhPhoId`) VALUES
(1, 'Hải Châu', 1),
(2, 'Thanh Khê', 1),
(3, 'Sơn Trà', 1),
(4, 'Cẩm Lệ', 1),
(5, 'Ngũ Hành Sơn', 1),
(6, 'Hòa Vang', 1);

-- --------------------------------------------------------

--
-- Table structure for table `schoolsetting`
--

CREATE TABLE IF NOT EXISTS `schoolsetting` (
  `SchoolSettingId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(1000) COLLATE utf8_unicode_ci NOT NULL,
  `Value` text COLLATE utf8_unicode_ci NOT NULL,
  `DateCreated` date NOT NULL,
  `DateDeleted` date NOT NULL,
  PRIMARY KEY (`SchoolSettingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `taisan`
--

CREATE TABLE IF NOT EXISTS `taisan` (
  `TaiSanId` int(11) NOT NULL AUTO_INCREMENT,
  `Ten` text COLLATE utf8_unicode_ci NOT NULL,
  `DonViTinh` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `SoLuong` double NOT NULL,
  `DonGia` bigint(20) NOT NULL,
  `SoChungTu` text COLLATE utf8_unicode_ci NOT NULL,
  `NgayChungTu` date NOT NULL,
  `PhieuChiId` int(11) NOT NULL,
  PRIMARY KEY (`TaiSanId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `taisan_lop`
--

CREATE TABLE IF NOT EXISTS `taisan_lop` (
  `TaiSanLopId` int(11) NOT NULL AUTO_INCREMENT,
  `TaiSanId` int(11) NOT NULL,
  `LopId` int(11) NOT NULL,
  `SoLuong` double DEFAULT NULL,
  `NgayBanGiao` date DEFAULT NULL,
  `NgayHetHan` date DEFAULT NULL,
  `GhiChu` text COLLATE utf8_unicode_ci,
  PRIMARY KEY (`TaiSanLopId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `thanhpho`
--

CREATE TABLE IF NOT EXISTS `thanhpho` (
  `ThanhPhoId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`ThanhPhoId`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=2 ;

--
-- Dumping data for table `thanhpho`
--

INSERT INTO `thanhpho` (`ThanhPhoId`, `Name`) VALUES
(1, 'Đà Nẵng');

-- --------------------------------------------------------

--
-- Table structure for table `truong`
--

CREATE TABLE IF NOT EXISTS `truong` (
  `TruongId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Description` text COLLATE utf8_unicode_ci,
  PRIMARY KEY (`TruongId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=2 ;

--
-- Dumping data for table `truong`
--

INSERT INTO `truong` (`TruongId`, `Name`, `Description`) VALUES
(1, 'MN Hồng Minh', 'Trường mầm non Hồng Minh');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE IF NOT EXISTS `user` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Password` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=2 ;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`UserId`, `UserName`, `Password`, `CreatedDate`) VALUES
(1, 'admin', '827ccb0eea8a706c4c34a16891f84e7b', '2017-07-21 08:22:26');

-- --------------------------------------------------------

--
-- Table structure for table `user_privilege`
--

CREATE TABLE IF NOT EXISTS `user_privilege` (
  `UserPrivilegeId` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `PrivilegeId` int(11) NOT NULL,
  `Value` tinyint(1) NOT NULL,
  PRIMARY KEY (`UserPrivilegeId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=5 ;

--
-- Dumping data for table `user_privilege`
--

INSERT INTO `user_privilege` (`UserPrivilegeId`, `UserId`, `PrivilegeId`, `Value`) VALUES
(1, 1, 1, 1),
(2, 1, 2, 1),
(3, 1, 3, 1),
(4, 1, 4, 1);

-- --------------------------------------------------------

--
-- Stand-in structure for view `viewbangiaotaisan`
--
CREATE TABLE IF NOT EXISTS `viewbangiaotaisan` (
`TaiSanId` int(11)
,`Ten` text
,`DonViTinh` varchar(20)
,`SoLuong` double
,`DonGia` bigint(20)
,`SoChungTu` text
,`NgayChungTu` date
,`PhieuChiId` int(11)
,`LopId` int(11)
,`NgayBanGiao` date
,`NgayHetHan` date
,`GhiChu` text
,`TaiSanLopId` int(11)
,`SoLuongBanGiao` double
,`NgayNhap` date
);
-- --------------------------------------------------------

--
-- Stand-in structure for view `viewbangthutien`
--
CREATE TABLE IF NOT EXISTS `viewbangthutien` (
`BangThuTienId` int(11)
,`HocSinhId` int(11)
,`LopId` int(11)
,`SXThangTruoc` int(11)
,`SoTienSXThangTruoc` bigint(20)
,`AnSangThangTruoc` int(11)
,`SoTienAnSangThangTruoc` bigint(20)
,`SoTienAnSangThangNay` bigint(20)
,`AnToiThangTruoc` int(11)
,`SoTienAnToiThangTruoc` bigint(20)
,`SoTienAnToiThangNay` bigint(20)
,`SoTienNangKhieu` bigint(20)
,`SoTienTruyThu` bigint(20)
,`SoTienDoDung` bigint(20)
,`SoTienDieuHoa` bigint(20)
,`NgayTinh` date
,`STT` int(11)
,`IsDeleted` tinyint(1)
,`DateCreated` timestamp
,`SoTienAnSangConLai` bigint(15)
,`SoTienAnToiConLai` bigint(15)
,`ThanhTien` bigint(15)
,`TienAnSua` bigint(15)
,`PhuPhi` bigint(15)
,`BanTru` bigint(15)
,`HocPhi` bigint(15)
,`KhoanThuChinh` bigint(15)
,`HoTen` char(0)
,`NgayNopLan1` binary(0)
,`SoTienNopLan1` bigint(15)
,`NgayNopLan2` binary(0)
,`SoTienNopLan2` bigint(15)
,`GhiChu` text
,`Lop` varchar(1)
,`SoBienLai` varchar(1)
,`TongThu` varchar(1)
,`TienAn` bigint(15)
);
-- --------------------------------------------------------

--
-- Stand-in structure for view `viewhoctap`
--
CREATE TABLE IF NOT EXISTS `viewhoctap` (
`HocSinhId` int(11)
,`HoDem` varchar(50)
,`Ten` varchar(50)
,`GioiTinh` tinyint(4)
,`NgaySinh` date
,`NgayVaoLop` date
,`DanToc` varchar(50)
,`DienCuTru` varchar(50)
,`DienThoai` varchar(50)
,`HoTenCha` varchar(255)
,`HoTenMe` varchar(255)
,`TinhThanhPhoId` int(11)
,`QuanHuyenId` int(11)
,`PhuongXaId` int(11)
,`DiaChi` varchar(255)
,`ThoiHoc` tinyint(1)
,`CreatedDate` datetime
,`SoNgayNghiThang` int(11)
,`NgayTinh` date
,`HocTapId` int(11)
,`LopId` int(11)
,`LopName` varchar(255)
,`HocSinhLopId` int(11)
,`DateJoin` date
,`DateLeft` date
);
-- --------------------------------------------------------

--
-- Stand-in structure for view `viewtaisan`
--
CREATE TABLE IF NOT EXISTS `viewtaisan` (
`TaiSanId` int(11)
,`Ten` text
,`NgayNhap` date
,`DonViTinh` varchar(20)
,`SoLuong` double
,`DonGia` bigint(20)
,`SoChungTu` text
,`NgayChungTu` date
,`ThanhTien` bigint(20)
,`PhieuChiId` int(11)
,`PhanLoaiChiId` int(11)
);
-- --------------------------------------------------------

--
-- Stand-in structure for view `viewtaisanforbangiaotaisan`
--
CREATE TABLE IF NOT EXISTS `viewtaisanforbangiaotaisan` (
`TaiSanId` int(11)
,`Ten` mediumtext
,`NgayNhap` date
,`DonViTinh` varchar(20)
,`SoLuong` double
,`DonGia` bigint(20)
,`SoChungTu` mediumtext
,`NgayChungTu` date
,`ThanhTien` bigint(20)
,`PhieuChiId` int(11)
,`PhanLoaiChiId` bigint(20)
);
-- --------------------------------------------------------

--
-- Structure for view `viewbangiaotaisan`
--
DROP TABLE IF EXISTS `viewbangiaotaisan`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `viewbangiaotaisan` AS select `ts`.`TaiSanId` AS `TaiSanId`,`ts`.`Ten` AS `Ten`,`ts`.`DonViTinh` AS `DonViTinh`,`ts`.`SoLuong` AS `SoLuong`,`ts`.`DonGia` AS `DonGia`,`ts`.`SoChungTu` AS `SoChungTu`,`ts`.`NgayChungTu` AS `NgayChungTu`,`ts`.`PhieuChiId` AS `PhieuChiId`,`tsl`.`LopId` AS `LopId`,`tsl`.`NgayBanGiao` AS `NgayBanGiao`,`tsl`.`NgayHetHan` AS `NgayHetHan`,`tsl`.`GhiChu` AS `GhiChu`,`tsl`.`TaiSanLopId` AS `TaiSanLopId`,`tsl`.`SoLuong` AS `SoLuongBanGiao`,`pc`.`Ngay` AS `NgayNhap` from ((`taisan` `ts` join `phieuchi` `pc` on((`ts`.`PhieuChiId` = `pc`.`PhieuChiId`))) left join `taisan_lop` `tsl` on((`ts`.`TaiSanId` = `tsl`.`TaiSanId`)));

-- --------------------------------------------------------

--
-- Structure for view `viewbangthutien`
--
DROP TABLE IF EXISTS `viewbangthutien`;

CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `viewbangthutien` AS select `bangthutien`.`BangThuTienId` AS `BangThuTienId`,`bangthutien`.`HocSinhId` AS `HocSinhId`,`bangthutien`.`LopId` AS `LopId`,`bangthutien`.`SXThangTruoc` AS `SXThangTruoc`,`bangthutien`.`SoTienSXThangTruoc` AS `SoTienSXThangTruoc`,`bangthutien`.`AnSangThangTruoc` AS `AnSangThangTruoc`,`bangthutien`.`SoTienAnSangThangTruoc` AS `SoTienAnSangThangTruoc`,`bangthutien`.`SoTienAnSangThangNay` AS `SoTienAnSangThangNay`,`bangthutien`.`AnToiThangTruoc` AS `AnToiThangTruoc`,`bangthutien`.`SoTienAnToiThangTruoc` AS `SoTienAnToiThangTruoc`,`bangthutien`.`SoTienAnToiThangNay` AS `SoTienAnToiThangNay`,`bangthutien`.`SoTienNangKhieu` AS `SoTienNangKhieu`,`bangthutien`.`SoTienTruyThu` AS `SoTienTruyThu`,`bangthutien`.`SoTienDoDung` AS `SoTienDoDung`,`bangthutien`.`SoTienDieuHoa` AS `SoTienDieuHoa`,`bangthutien`.`NgayTinh` AS `NgayTinh`,`bangthutien`.`STT` AS `STT`,`bangthutien`.`IsDeleted` AS `IsDeleted`,`bangthutien`.`DateCreated` AS `DateCreated`,100000000000000 AS `SoTienAnSangConLai`,100000000000000 AS `SoTienAnToiConLai`,100000000000000 AS `ThanhTien`,100000000000000 AS `TienAnSua`,100000000000000 AS `PhuPhi`,100000000000000 AS `BanTru`,100000000000000 AS `HocPhi`,100000000000000 AS `KhoanThuChinh`,'' AS `HoTen`,NULL AS `NgayNopLan1`,100000000000000 AS `SoTienNopLan1`,NULL AS `NgayNopLan2`,100000000000000 AS `SoTienNopLan2`,`bangthutien`.`GhiChu` AS `GhiChu`,' ' AS `Lop`,' ' AS `SoBienLai`,' ' AS `TongThu`,100000000000000 AS `TienAn` from `bangthutien`;

-- --------------------------------------------------------

--
-- Structure for view `viewhoctap`
--
DROP TABLE IF EXISTS `viewhoctap`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `viewhoctap` AS select `hs`.`HocSinhId` AS `HocSinhId`,`hs`.`HoDem` AS `HoDem`,`hs`.`Ten` AS `Ten`,`hs`.`GioiTinh` AS `GioiTinh`,`hs`.`NgaySinh` AS `NgaySinh`,`hs`.`NgayVaoLop` AS `NgayVaoLop`,`hs`.`DanToc` AS `DanToc`,`hs`.`DienCuTru` AS `DienCuTru`,`hs`.`DienThoai` AS `DienThoai`,`hs`.`HoTenCha` AS `HoTenCha`,`hs`.`HoTenMe` AS `HoTenMe`,`hs`.`TinhThanhPhoId` AS `TinhThanhPhoId`,`hs`.`QuanHuyenId` AS `QuanHuyenId`,`hs`.`PhuongXaId` AS `PhuongXaId`,`hs`.`DiaChi` AS `DiaChi`,`hs`.`ThoiHoc` AS `ThoiHoc`,`hs`.`CreatedDate` AS `CreatedDate`,`bct`.`SoNgayNghi` AS `SoNgayNghiThang`,`bct`.`NgayTinh` AS `NgayTinh`,`ht`.`HocTapId` AS `HocTapId`,`l`.`LopId` AS `LopId`,`l`.`Name` AS `LopName`,`hsl`.`HocSinhLopId` AS `HocSinhLopId`,`hsl`.`DateJoin` AS `DateJoin`,`hsl`.`DateLeft` AS `DateLeft` from ((((`hocsinh` `hs` left join `hocsinh_lop` `hsl` on((`hs`.`HocSinhId` = `hsl`.`HocSinhId`))) left join `hoctap` `ht` on((`hsl`.`HocSinhLopId` = `ht`.`HocSinhLopId`))) join `lop` `l` on((`hsl`.`LopId` = `l`.`LopId`))) left join `baocaothang` `bct` on((`ht`.`HocTapId` = `bct`.`HocTapId`)));

-- --------------------------------------------------------

--
-- Structure for view `viewtaisan`
--
DROP TABLE IF EXISTS `viewtaisan`;

CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `viewtaisan` AS select `ts`.`TaiSanId` AS `TaiSanId`,`ts`.`Ten` AS `Ten`,`pc`.`Ngay` AS `NgayNhap`,`ts`.`DonViTinh` AS `DonViTinh`,`ts`.`SoLuong` AS `SoLuong`,`ts`.`DonGia` AS `DonGia`,`ts`.`SoChungTu` AS `SoChungTu`,`ts`.`NgayChungTu` AS `NgayChungTu`,`pc`.`SoTien` AS `ThanhTien`,`pc`.`PhieuChiId` AS `PhieuChiId`,`pc`.`PhanLoaiChiId` AS `PhanLoaiChiId` from (`taisan` `ts` join `phieuchi` `pc` on((`ts`.`PhieuChiId` = `pc`.`PhieuChiId`))) WITH CASCADED CHECK OPTION;

-- --------------------------------------------------------

--
-- Structure for view `viewtaisanforbangiaotaisan`
--
DROP TABLE IF EXISTS `viewtaisanforbangiaotaisan`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `viewtaisanforbangiaotaisan` AS (select `viewtaisan`.`TaiSanId` AS `TaiSanId`,`viewtaisan`.`Ten` AS `Ten`,`viewtaisan`.`NgayNhap` AS `NgayNhap`,`viewtaisan`.`DonViTinh` AS `DonViTinh`,`viewtaisan`.`SoLuong` AS `SoLuong`,`viewtaisan`.`DonGia` AS `DonGia`,`viewtaisan`.`SoChungTu` AS `SoChungTu`,`viewtaisan`.`NgayChungTu` AS `NgayChungTu`,`viewtaisan`.`ThanhTien` AS `ThanhTien`,`viewtaisan`.`PhieuChiId` AS `PhieuChiId`,`viewtaisan`.`PhanLoaiChiId` AS `PhanLoaiChiId` from `viewtaisan`) union (select `viewbangiaotaisan`.`TaiSanId` AS `TaiSanId`,`viewbangiaotaisan`.`Ten` AS `Ten`,`viewbangiaotaisan`.`NgayNhap` AS `NgayNhap`,`viewbangiaotaisan`.`DonViTinh` AS `DonViTinh`,(`viewbangiaotaisan`.`SoLuongBanGiao` * -(1)) AS `SoLuong`,`viewbangiaotaisan`.`DonGia` AS `DonGia`,`viewbangiaotaisan`.`SoChungTu` AS `SoChungTu`,`viewbangiaotaisan`.`NgayChungTu` AS `NgayChungTu`,0 AS `ThanhTien`,`viewbangiaotaisan`.`PhieuChiId` AS `PhieuChiId`,0 AS `PhanLoaiChiId` from `viewbangiaotaisan` where (isnull(`viewbangiaotaisan`.`NgayHetHan`) or (`viewbangiaotaisan`.`NgayHetHan` >= now())));

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
