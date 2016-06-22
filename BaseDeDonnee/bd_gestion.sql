-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Client :  127.0.0.1
-- Généré le :  Mer 22 Juin 2016 à 16:07
-- Version du serveur :  5.6.17
-- Version de PHP :  5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de données :  `bd_gestion`
--

-- --------------------------------------------------------

--
-- Structure de la table `administrer`
--

CREATE TABLE IF NOT EXISTS `administrer` (
  `IDCOLLABORATEUR` int(11) NOT NULL,
  `IDPROJET` int(11) NOT NULL,
  PRIMARY KEY (`IDCOLLABORATEUR`,`IDPROJET`),
  KEY `FK_ADMINISTRER2` (`IDPROJET`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Contenu de la table `administrer`
--



-- --------------------------------------------------------

--
-- Structure de la table `aspnetroles`
--

CREATE TABLE IF NOT EXISTS `aspnetroles` (
  `Id` varchar(128) NOT NULL,
  `Name` varchar(256) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `aspnetroles`
--



-- --------------------------------------------------------

--
-- Structure de la table `aspnetuserclaims`
--

CREATE TABLE IF NOT EXISTS `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(128) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id` (`Id`),
  KEY `UserId` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Structure de la table `aspnetuserlogins`
--

CREATE TABLE IF NOT EXISTS `aspnetuserlogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `UserId` varchar(128) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`,`UserId`),
  KEY `ApplicationUser_Logins` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `aspnetuserroles`
--

CREATE TABLE IF NOT EXISTS `aspnetuserroles` (
  `UserId` varchar(128) NOT NULL,
  `RoleId` varchar(128) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IdentityRole_Users` (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `aspnetuserroles`
--


-- --------------------------------------------------------

--
-- Structure de la table `aspnetusers`
--

CREATE TABLE IF NOT EXISTS `aspnetusers` (
  `Id` varchar(128) NOT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEndDateUtc` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `UserName` varchar(256) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;



CREATE TABLE IF NOT EXISTS `caracteriser` (
  `IDEVALUATION` int(11) NOT NULL,
  `IDCRITERE` int(11) NOT NULL,
  PRIMARY KEY (`IDEVALUATION`,`IDCRITERE`),
  KEY `FK_CARACTERISER2` (`IDCRITERE`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;


--
-- Structure de la table `client`
--

CREATE TABLE IF NOT EXISTS `client` (
  `IDCLIENT` int(11) NOT NULL AUTO_INCREMENT,
  `ABREVIATION` char(25) COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`IDCLIENT`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_bin AUTO_INCREMENT=2 ;


--
-- Structure de la table `collaborateur`
--

CREATE TABLE IF NOT EXISTS `collaborateur` (
  `IDCOLLABORATEUR` int(11) NOT NULL AUTO_INCREMENT,
  `IDFONCTION` int(11) NOT NULL,
  `NOM` char(45) COLLATE utf8_bin NOT NULL,
  `PRENOM` char(45) COLLATE utf8_bin NOT NULL,
  `IMAGE` longblob,
  `FLAGEVAL` tinyint(1) NOT NULL,
  `TYPECOLLABORATEUR` char(35) COLLATE utf8_bin DEFAULT NULL,
  `DATEEMBAUCHE` date NOT NULL,
  `DATESORTIE` date DEFAULT NULL,
  `IdUser` varchar(128) CHARACTER SET latin1 NOT NULL,
  `Evaluer` tinyint(1) NOT NULL,
  PRIMARY KEY (`IDCOLLABORATEUR`),
  KEY `FK_AVOIR` (`IDFONCTION`),
  KEY `IdUser` (`IdUser`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_bin AUTO_INCREMENT=11 ;


--
-- Structure de la table `critere`
--

CREATE TABLE IF NOT EXISTS `critere` (
  `IDCRITERE` int(11) NOT NULL AUTO_INCREMENT,
  `IDFAMILLECRITERE` int(11) NOT NULL,
  `NOMCRITERE` char(254) COLLATE utf8_bin NOT NULL,
  `NoteCritere` varchar(20) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`IDCRITERE`),
  KEY `FK_SELECTIONNER` (`IDFAMILLECRITERE`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_bin AUTO_INCREMENT=10 ;


--
-- Structure de la table `evaluation`
--

CREATE TABLE IF NOT EXISTS `evaluation` (
  `IDEVALUATION` int(11) NOT NULL AUTO_INCREMENT,
  `IDEVALUATEUR` int(11) NOT NULL,
  `IDCOLLABORATEUR` int(11) NOT NULL,
  `DATEEVALUATION` char(45) COLLATE utf8_bin DEFAULT NULL,
  `DATENEXTEVALUATION` char(45) COLLATE utf8_bin DEFAULT NULL,
  `APPRECIATION` char(254) COLLATE utf8_bin DEFAULT NULL,
  `STATUT` char(35) COLLATE utf8_bin DEFAULT NULL,
  `Memo` varchar(254) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`IDEVALUATION`),
  KEY `FK_EXAMINER` (`IDCOLLABORATEUR`),
  KEY `FK_QUIEVALUE` (`IDEVALUATEUR`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_bin AUTO_INCREMENT=4 ;


--
-- Structure de la table `famillecritere`
--

CREATE TABLE IF NOT EXISTS `famillecritere` (
  `IDFAMILLECRITERE` int(11) NOT NULL AUTO_INCREMENT,
  `NOMFAMILLECRITERE` char(45) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`IDFAMILLECRITERE`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_bin AUTO_INCREMENT=5 ;


--
-- Structure de la table `fonction`
--

CREATE TABLE IF NOT EXISTS `fonction` (
  `IDFONCTION` int(11) NOT NULL AUTO_INCREMENT,
  `NOMFONCTION` char(45) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`IDFONCTION`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_bin AUTO_INCREMENT=3 ;


--
-- Structure de la table `projet`
--

CREATE TABLE IF NOT EXISTS `projet` (
  `IDPROJET` int(11) NOT NULL AUTO_INCREMENT,
  `IDCLIENT` int(11) NOT NULL,
  `NOMPROJET` char(45) COLLATE utf8_bin DEFAULT NULL,
  `DATEDEBUT` date DEFAULT NULL,
  `DATEFIN` date DEFAULT NULL,
  `TYPE` int(11) DEFAULT NULL,
  `FLAGTYPE` tinyint(1) NOT NULL,
  PRIMARY KEY (`IDPROJET`),
  KEY `FK_APPARTENIR` (`IDCLIENT`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_bin AUTO_INCREMENT=6 ;


--
-- Structure de la table `tache`
--

CREATE TABLE IF NOT EXISTS `tache` (
  `IDTACHE` int(11) NOT NULL AUTO_INCREMENT,
  `IDCOLLABORATEUR` int(11) NOT NULL,
  `IDPROJET` int(11) NOT NULL,
  `NOMTACHE` char(45) COLLATE utf8_bin DEFAULT NULL,
  `DATEDEBUTTACHE` date DEFAULT NULL,
  `DATEFINTACHE` date DEFAULT NULL,
  `NoteTache` varchar(20) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`IDTACHE`),
  KEY `FK_CONTIENT` (`IDPROJET`),
  KEY `FK_MANIER` (`IDCOLLABORATEUR`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_bin AUTO_INCREMENT=12 ;


--
-- Structure de la table `variable`
--

CREATE TABLE IF NOT EXISTS `variable` (
  `IDVARIABLE` int(11) NOT NULL AUTO_INCREMENT,
  `IDEVALUATION` int(11) NOT NULL,
  `TAUX` int(11) DEFAULT NULL,
  `PERIODE` int(11) DEFAULT NULL,
  PRIMARY KEY (`IDVARIABLE`),
  KEY `FK_POSSEDER` (`IDEVALUATION`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin AUTO_INCREMENT=1 ;

--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `administrer`
--
ALTER TABLE `administrer`
  ADD CONSTRAINT `FK_ADMINISTRER` FOREIGN KEY (`IDCOLLABORATEUR`) REFERENCES `collaborateur` (`IDCOLLABORATEUR`),
  ADD CONSTRAINT `FK_ADMINISTRER2` FOREIGN KEY (`IDPROJET`) REFERENCES `projet` (`IDPROJET`);

--
-- Contraintes pour la table `aspnetuserclaims`
--
ALTER TABLE `aspnetuserclaims`
  ADD CONSTRAINT `ApplicationUser_Claims` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Contraintes pour la table `aspnetuserlogins`
--
ALTER TABLE `aspnetuserlogins`
  ADD CONSTRAINT `ApplicationUser_Logins` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Contraintes pour la table `aspnetuserroles`
--
ALTER TABLE `aspnetuserroles`
  ADD CONSTRAINT `ApplicationUser_Roles` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `IdentityRole_Users` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Contraintes pour la table `caracteriser`
--
ALTER TABLE `caracteriser`
  ADD CONSTRAINT `FK_CARACTERISER` FOREIGN KEY (`IDEVALUATION`) REFERENCES `evaluation` (`IDEVALUATION`),
  ADD CONSTRAINT `FK_CARACTERISER2` FOREIGN KEY (`IDCRITERE`) REFERENCES `critere` (`IDCRITERE`);

--
-- Contraintes pour la table `collaborateur`
--
ALTER TABLE `collaborateur`
  ADD CONSTRAINT `FK_AVOIR` FOREIGN KEY (`IDFONCTION`) REFERENCES `fonction` (`IDFONCTION`),
  ADD CONSTRAINT `fk_IdUser` FOREIGN KEY (`IdUser`) REFERENCES `aspnetusers` (`Id`);

--
-- Contraintes pour la table `critere`
--
ALTER TABLE `critere`
  ADD CONSTRAINT `FK_SELECTIONNER` FOREIGN KEY (`IDFAMILLECRITERE`) REFERENCES `famillecritere` (`IDFAMILLECRITERE`);

--
-- Contraintes pour la table `evaluation`
--
ALTER TABLE `evaluation`
  ADD CONSTRAINT `FK_EXAMINER` FOREIGN KEY (`IDCOLLABORATEUR`) REFERENCES `collaborateur` (`IDCOLLABORATEUR`),
  ADD CONSTRAINT `FK_QUIEVALUE` FOREIGN KEY (`IDEVALUATEUR`) REFERENCES `collaborateur` (`IDCOLLABORATEUR`);

--
-- Contraintes pour la table `projet`
--
ALTER TABLE `projet`
  ADD CONSTRAINT `FK_APPARTENIR` FOREIGN KEY (`IDCLIENT`) REFERENCES `client` (`IDCLIENT`);

--
-- Contraintes pour la table `tache`
--
ALTER TABLE `tache`
  ADD CONSTRAINT `FK_CONTIENT` FOREIGN KEY (`IDPROJET`) REFERENCES `projet` (`IDPROJET`),
  ADD CONSTRAINT `FK_MANIER` FOREIGN KEY (`IDCOLLABORATEUR`) REFERENCES `collaborateur` (`IDCOLLABORATEUR`);

--
-- Contraintes pour la table `variable`
--
ALTER TABLE `variable`
  ADD CONSTRAINT `FK_POSSEDER` FOREIGN KEY (`IDEVALUATION`) REFERENCES `evaluation` (`IDEVALUATION`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
