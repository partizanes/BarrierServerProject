/*
SQLyog Ultimate v8.62 
MySQL - 5.0.84-community-nt : Database - barrierserver
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`barrierserver` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `barrierserver`;

/*Table structure for table `state` */

DROP TABLE IF EXISTS `state`;

CREATE TABLE `state` (
  `barcode` longtext NOT NULL,
  `name` longtext NOT NULL,
  `price` bigint(20) NOT NULL,
  `count` double NOT NULL,
  `sailed` double NOT NULL,
  `status` int(11) NOT NULL,
  `date` datetime NOT NULL,
  `flag` int(11) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

/*Data for the table `state` */

insert  into `state`(`barcode`,`name`,`price`,`count`,`sailed`,`status`,`date`,`flag`) values ('05306        ','колбаса                                           ',45000,3,3,0,'2013-03-04 19:30:24',0),('481006400201 ','КРЕКЕР ОРИГИНАЛЬНЫЙ С АРОМ.СЫРА 180Г              ',5250,5,0,0,'2013-03-09 12:59:28',0),('481041003696 ','КОНФЕТЫ АССОРТИ 190Г                              ',28950,8,0,0,'2013-03-09 12:59:28',0),('05306        ','КОНФЕТЫ ТУРОНЧИКИ С ЦУКАТАМИ ВЕС                  ',45000,55,0,0,'2013-03-09 14:40:43',0),('481016802940 ','ЗАКУСКА РЫБНАЯ ИЗ ТРЕСКИ И ЛОСОСЯ ОРИГИН          ',12200,3,0,0,'2013-03-09 14:00:20',0),('40490        ','ГОРБУША ПОДКОПЧ. ДАЛЬНЕВОСТОЧНАЯ (СПИНКА          ',88450,981,0,0,'2013-03-09 14:40:43',0),('07985        ','ПОЛЕНДВИЦА ФУРШЕТНАЯ С/К                          ',119400,5,0,0,'2013-03-09 14:00:20',0),('481004700249 ','МАК.ИЗД.УЛИТКИ 800Г                               ',7600,6,0,0,'2013-03-09 14:00:20',0),('482301223942 ','ДЕСЕРТ БОНЖУР ТРЮФЕЛЬ 200Г                        ',21150,6,0,0,'2013-03-09 14:00:20',0),('05306        ','КОНФЕТЫ ТУРОНЧИКИ С ЦУКАТАМИ ВЕС                  ',45000,55,0,0,'2013-03-09 14:40:43',0),('40490        ','ГОРБУША ПОДКОПЧ. ДАЛЬНЕВОСТОЧНАЯ (СПИНКА          ',88450,981,0,0,'2013-03-09 14:40:43',0),('460298400279 ','ЧИСТЯЩИЙ ПОРОШОК ЧИСТИН ЛИМОН.ВСПЛЕСК 40          ',7200,155,0,0,'2013-03-09 14:40:43',0);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
