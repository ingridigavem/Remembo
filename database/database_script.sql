-- -----------------------------------------------------
-- Schema Remembo
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `Remembo` DEFAULT CHARACTER SET utf8 ;
USE `Remembo` ;

-- -----------------------------------------------------
-- Table `Remembo`.`Users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Remembo`.`Users` (
  `Id` CHAR(36) NOT NULL ,
  `Name` NVARCHAR(150) NOT NULL,
  `Email` NVARCHAR(150) NOT NULL,
  `PasswordHash` NVARCHAR(250) NOT NULL,
  PRIMARY KEY (`Id`));

-- -----------------------------------------------------
-- Table `Remembo`.`Subjects`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Remembo`.`Subjects` (
  `Id` CHAR(36) NOT NULL ,
  `UserId` CHAR(36) NOT NULL,
  `Name` NVARCHAR(50) NOT NULL,
  PRIMARY KEY (`Id`, `UserId`),
  CONSTRAINT `fk_Subject_User`
    FOREIGN KEY (`UserId`)
    REFERENCES `Remembo`.`Users` (`Id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

-- -----------------------------------------------------
-- Table `Remembo`.`Contents`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Remembo`.`Contents` (
  `Id` CHAR(36) NOT NULL ,
  `SubjectId` CHAR(36) NOT NULL,
  `Name` NVARCHAR(250) NOT NULL,
  `Note` TEXT NULL,
  `ReviewNumber` SMALLINT NOT NULL DEFAULT 1,
  `IsCompleted` BOOLEAN NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`, `SubjectId`),
  CONSTRAINT `fk_Content_Subject1`
    FOREIGN KEY (`SubjectId`)
    REFERENCES `Remembo`.`Subjects` (`Id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

-- -----------------------------------------------------
-- Table `Remembo`.`Reviews`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Remembo`.`Reviews` (
  `Id` CHAR(36) NOT NULL ,
  `ContentId` CHAR(36) NOT NULL,
  `ScheduleReviewDate` DATETIME NOT NULL,
  `IsReviewed` BOOLEAN NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`, `ContentId`),
  CONSTRAINT `fk_Review_Content1`
    FOREIGN KEY (`ContentId`)
    REFERENCES `Remembo`.`Contents` (`Id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);
