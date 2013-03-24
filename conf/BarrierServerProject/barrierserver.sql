/*
SQLyog Ultimate v8.62 
MySQL - 5.5.18 : Database - barrierserver
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
  `barcode` tinytext NOT NULL,
  `name` tinytext NOT NULL,
  `price` mediumint(7) unsigned NOT NULL,
  `count` float(7,3) NOT NULL,
  `sailed` float(7,3) NOT NULL,
  `status` tinyint(1) unsigned NOT NULL,
  `date` datetime NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

/*Data for the table `state` */

insert  into `state`(`barcode`,`name`,`price`,`count`,`sailed`,`status`,`date`) values ('481353800045 ','НЕКТАР СОЧНЫЙ ФРУКТ МУЛЬТИФРУКТОВЫЙ 2Л            ',16350,9.000,0.000,1,'2013-03-14 22:12:19'),('481007400015 ','МИН.ВОДА МИНСК-4 2Л                               ',5440,18.000,32.000,5,'2013-01-14 22:12:19'),('544900000028 ','НАПИТОК Б/А КОКА-КОЛА РЕТ 2Л                      ',9900,9.000,88.000,5,'2013-01-11 22:12:19'),('481353800045 ','НЕКТАР СОЧНЫЙ ФРУКТ МУЛЬТИФРУКТОВЫЙ 2Л            ',16350,1.111,8.000,5,'2013-01-11 22:12:19'),('481012801085 ','ПЕЧЕНЬЕ РВСЯНОЕ ЗАБОТА НА ФРУКТОЗЕ 350Г.          ',10800,19.000,0.000,0,'2013-03-24 14:06:51');

/*Table structure for table `tasks` */

DROP TABLE IF EXISTS `tasks`;

CREATE TABLE `tasks` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `barcode` text NOT NULL,
  `group` int(2) NOT NULL,
  `text` longtext NOT NULL,
  `user_id` tinyint(4) NOT NULL,
  `priority` tinyint(4) NOT NULL,
  `date` datetime DEFAULT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=104 DEFAULT CHARSET=utf8;

/*Data for the table `tasks` */

insert  into `tasks`(`id`,`barcode`,`group`,`text`,`user_id`,`priority`,`date`) values (100,'481353800045',1,'Товар долго не продается.Число дней: 9',0,1,'2013-03-14 22:12:19'),(101,'481007400015',1,'Товар продается дороже цены очерёдности!',0,5,'2013-01-14 22:12:19'),(102,'544900000028',1,'Товар продается дороже цены очерёдности!',0,5,'2013-01-11 22:12:19'),(103,'481353800045',1,'Товар продается дороже цены очерёдности!',0,5,'2013-01-11 22:12:19');

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `id` tinyint(4) NOT NULL AUTO_INCREMENT,
  `username` tinytext NOT NULL,
  `hash` tinytext NOT NULL,
  `group` tinyint(2) DEFAULT NULL,
  `status` tinyint(1) DEFAULT NULL,
  `ip` tinytext,
  `tasks_count` int(11) DEFAULT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `users` */

insert  into `users`(`id`,`username`,`hash`,`group`,`status`,`ip`,`tasks_count`) values (1,'partizanes','1ed6ad31cf5f6e0d10e18f8db3f08c9f',2,0,'127.0.0.1',0),(4,'user','91ff7d7746319a2d34f93537c3e54173',1,0,'192.168.1.3',0);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
