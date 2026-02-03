ALTER TABLE `phieuthu` ADD COLUMN `PaymentType` ENUM('CASH','TRANSFER') DEFAULT 'CASH' NULL AFTER `SoTien`; 

DELIMITER $$

USE `qlmamnon_20250919`$$

DROP PROCEDURE IF EXISTS `getPhieuThuForSoQuyTienMat`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPhieuThuForSoQuyTienMat`(IN `@fromDate` DATE, IN `@toDate` DATE, IN `@phanloaithuids` TEXT)
    READS SQL DATA
BEGIN
IF (`@phanloaithuids` = "") THEN
SET @sql = CONCAT('Select MAX(pt.PhieuThuId) AS PhieuThuId, "" AS MaPhieu, pt.PhanLoaiThuId, "" AS PaymentType, pt.Ngay, SUM(pt.SoTien) AS SoTien, '' AS MaPhieu, '' AS GhiChu, MAX(pt.HocSinhId) AS HocSinhId, MAX(pt.CreatedDate) AS CreatedDate
From PhieuThu pt
Where pt.Ngay>="',DATE_FORMAT(`@fromDate`, '%Y-%m-%d'),'" AND pt.`Ngay`<="',DATE_FORMAT(`@toDate`, '%Y-%m-%d'),'" GROUP BY pt.Ngay, `pt`.`PhanLoaiThuId`');
ELSE
SET @sql = CONCAT('Select MAX(pt.PhieuThuId) AS PhieuThuId, "" AS MaPhieu, pt.PhanLoaiThuId, "" AS PaymentType, pt.Ngay, SUM(pt.SoTien) AS SoTien, '' AS MaPhieu, '' AS GhiChu, MAX(pt.HocSinhId) AS HocSinhId, MAX(pt.CreatedDate) AS CreatedDate
From PhieuThu pt
Where pt.Ngay>="',DATE_FORMAT(`@fromDate`, '%Y-%m-%d'),'" AND pt.`Ngay`<="',DATE_FORMAT(`@toDate`, '%Y-%m-%d'),'" AND  pt.`PhanLoaiThuId` IN(', `@phanloaithuids`, ') GROUP BY pt.Ngay, `pt`.`PhanLoaiThuId`');
END IF;
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;
END$$

DELIMITER ;