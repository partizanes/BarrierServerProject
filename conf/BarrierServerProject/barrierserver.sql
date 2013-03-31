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

/*Table structure for table `sendprice` */

DROP TABLE IF EXISTS `sendprice`;

CREATE TABLE `sendprice` (
  `id` tinyint(2) unsigned NOT NULL,
  `price` smallint(5) unsigned NOT NULL,
  `kod_isp` tinyint(2) unsigned NOT NULL,
  `datetime` datetime NOT NULL,
  `action` char(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sendprice` */

insert  into `sendprice`(`id`,`price`,`kod_isp`,`datetime`,`action`) values (125,2000,24,'2013-03-23 20:12:40','Ц2'),(125,2000,24,'2013-03-23 22:06:56','К3'),(125,2000,24,'2013-03-23 22:07:07','Ц2'),(125,2000,20,'2013-03-24 21:20:51','К3'),(125,2350,20,'2013-03-24 21:23:06','К3'),(126,33600,24,'2013-03-05 17:10:10','К3'),(126,33600,24,'2013-03-05 17:11:32','Ц2'),(126,33600,30,'2013-03-19 12:48:20','Ц2'),(126,33600,30,'2013-03-19 12:48:51','К3'),(127,33600,24,'2013-03-05 17:10:10','К3'),(127,33600,24,'2013-03-05 17:11:32','Ц2'),(127,33600,30,'2013-03-19 12:48:20','Ц2'),(127,33600,30,'2013-03-19 12:48:51','К3');

/*Table structure for table `state` */

DROP TABLE IF EXISTS `state`;

CREATE TABLE `state` (
  `barcode` tinytext NOT NULL,
  `name` tinytext NOT NULL,
  `price` mediumint(7) unsigned NOT NULL,
  `sailprice` mediumint(7) NOT NULL,
  `count` float(7,3) NOT NULL,
  `sailed` float(7,3) NOT NULL,
  `status` tinyint(1) unsigned NOT NULL,
  `date` datetime NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

/*Data for the table `state` */

insert  into `state`(`barcode`,`name`,`price`,`sailprice`,`count`,`sailed`,`status`,`date`) values ('481006705664 ','ШОКОЛАД ДРАКОША ГОША  СЛИВОЧНЫЙ 48Г               ',4050,4050,5.000,10.000,3,'2013-03-18 22:47:30'),('481006704959 ','ШОКОЛАД ЛЮКС 20Г                                  ',2350,2000,17.000,1.000,5,'2013-03-18 22:47:30'),('481006704959 ','ШОКОЛАД ЛЮКС 20Г                                  ',2350,2350,17.000,15.000,0,'2013-03-18 22:47:30'),('481385200267 ','ВИНО САПЕРАВИ КРАСНОЕ П/СЛ 0.7Л                   ',33600,30950,5.000,7.000,5,'2013-02-28 14:39:24'),('481385200267 ','ВИНО САПЕРАВИ КРАСНОЕ П/СЛ 0.7Л                   ',33600,33600,5.000,24.000,3,'2013-02-28 14:39:24');

/*Table structure for table `tasks` */

DROP TABLE IF EXISTS `tasks`;

CREATE TABLE `tasks` (
  `id` bigint(20) unsigned NOT NULL auto_increment,
  `barcode` text NOT NULL,
  `sailprice` mediumint(7) NOT NULL,
  `group` int(2) NOT NULL,
  `text` longtext NOT NULL,
  `user_id` tinyint(4) NOT NULL,
  `priority` tinyint(4) NOT NULL,
  `date` datetime NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=128 DEFAULT CHARSET=utf8;

/*Data for the table `tasks` */

insert  into `tasks`(`id`,`barcode`,`sailprice`,`group`,`text`,`user_id`,`priority`,`date`) values (124,'481006705664',4050,2,'Продано больше чем было в очередности,необходима прогрузка цены на кассу',1,3,'2013-03-18 22:47:30'),(125,'481006704959',2000,1,'были продажи дешевле цены очерёдности!',0,5,'2013-03-18 22:47:30'),(126,'481385200267',30950,1,'были продажи дешевле цены очерёдности!',0,5,'2013-02-28 14:39:24'),(127,'481385200267',33600,2,'Продано больше чем было в очередности,необходима прогрузка цены на кассу',0,3,'2013-02-28 14:39:24');

/*Table structure for table `tmp_tasks` */

DROP TABLE IF EXISTS `tmp_tasks`;

CREATE TABLE `tmp_tasks` (
  `barcode` text NOT NULL,
  `sailprice` mediumint(7) NOT NULL,
  `group` int(2) NOT NULL,
  `user_id` tinyint(4) NOT NULL,
  `priority` tinyint(4) NOT NULL,
  `date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tmp_tasks` */

insert  into `tmp_tasks`(`barcode`,`sailprice`,`group`,`user_id`,`priority`,`date`) values ('481385200267',30950,1,1,5,'2013-02-28 14:39:24'),('481385200267',30950,1,1,5,'2013-02-28 14:39:24');

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `id` tinyint(4) NOT NULL auto_increment,
  `username` tinytext NOT NULL,
  `hash` tinytext NOT NULL,
  `group` tinyint(2) default NULL,
  `status` tinyint(1) default NULL,
  `ip` tinytext,
  `tasks_count` int(11) default NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `users` */

insert  into `users`(`id`,`username`,`hash`,`group`,`status`,`ip`,`tasks_count`) values (1,'partizanes','1ed6ad31cf5f6e0d10e18f8db3f08c9f',2,0,'127.0.0.1',0),(4,'user','91ff7d7746319a2d34f93537c3e54173',1,0,'127.0.0.1',0);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
