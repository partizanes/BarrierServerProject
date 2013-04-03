/*
SQLyog Ultimate v8.62 
MySQL - 5.5.18 : Database - barrierserver2
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`barrierserver2` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `barrierserver2`;

/*Table structure for table `operations` */

DROP TABLE IF EXISTS `operations`;

CREATE TABLE `operations` (
  `id` bigint(20) unsigned NOT NULL,
  `operation` tinyint(2) unsigned NOT NULL,
  `count` float(7,3) unsigned NOT NULL,
  `price` mediumint(7) unsigned NOT NULL,
  `inactive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `operations` */

insert  into `operations`(`id`,`operation`,`count`,`price`,`inactive`) values (1,51,51.670,65050,0),(2,51,7.192,64090,0),(2,51,128.204,64650,0),(2,51,41.514,64660,0),(2,51,70.610,65050,0),(3,72,2.160,64650,0),(3,51,7.192,64090,0),(3,51,128.204,64650,0),(3,51,41.514,64660,0),(3,51,70.610,65050,0);

/*Table structure for table `priority` */

DROP TABLE IF EXISTS `priority`;

CREATE TABLE `priority` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `bar` tinytext NOT NULL,
  `name` tinytext NOT NULL,
  `turn_price` mediumint(7) unsigned NOT NULL,
  `count` float(7,3) NOT NULL,
  `sailed` float(7,3) DEFAULT NULL,
  `status` tinyint(2) NOT NULL,
  `status_text` tinytext NOT NULL,
  `current_price_ukm` mediumint(7) NOT NULL,
  `date` datetime NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*Data for the table `priority` */

insert  into `priority`(`id`,`bar`,`name`,`turn_price`,`count`,`sailed`,`status`,`status_text`,`current_price_ukm`,`date`) values (1,'07376','П/Ф ШЕЙНАЯ ЧАСТЬ СВИНАЯ ВИТЕБСКАЯ ОХЛ.',65050,15.000,249.680,0,'',65050,'2013-02-03 14:23:31'),(2,'07376','П/Ф ШЕЙНАЯ ЧАСТЬ СВИНАЯ ВИТЕБСКАЯ ОХЛ.',65050,88.000,249.680,0,'',65050,'2013-01-01 14:23:31'),(3,'07376','П/Ф ШЕЙНАЯ ЧАСТЬ СВИНАЯ ВИТЕБСКАЯ ОХЛ.',65050,15.000,249.680,0,'',65050,'2013-01-01 14:27:08');

/*Table structure for table `sendpos` */

DROP TABLE IF EXISTS `sendpos`;

CREATE TABLE `sendpos` (
  `id` tinyint(2) unsigned NOT NULL,
  `price` smallint(5) unsigned NOT NULL,
  `kod_isp` tinyint(2) unsigned NOT NULL,
  `datetime` datetime NOT NULL,
  `action` char(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sendpos` */

/*Table structure for table `tasks` */

DROP TABLE IF EXISTS `tasks`;

CREATE TABLE `tasks` (
  `tasks_id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `priority_id` bigint(20) unsigned NOT NULL,
  `user_group` int(2) NOT NULL,
  `task_text` longtext NOT NULL,
  `user_id` tinyint(4) NOT NULL,
  `priority` tinyint(4) NOT NULL,
  `date` datetime NOT NULL,
  `inactive` tinyint(1) unsigned NOT NULL,
  UNIQUE KEY `tasks_id` (`tasks_id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

/*Data for the table `tasks` */

insert  into `tasks`(`tasks_id`,`priority_id`,`user_group`,`task_text`,`user_id`,`priority`,`date`,`inactive`) values (1,1,1,' Нужна прогрузка цены на касску',0,3,'2013-04-03 14:23:42',0),(2,1,2,' Продано больше чем нужно',0,3,'2013-04-03 14:23:42',0),(3,2,2,' Продажи дешевле цены очередности [64090]',0,5,'2013-04-03 14:25:26',0),(4,2,2,' Продажи дешевле цены очередности [64650]',0,5,'2013-04-03 14:25:26',0),(5,2,2,' Продажи дешевле цены очередности [64660]',0,5,'2013-04-03 14:25:26',0),(6,2,1,' Нужна прогрузка цены на касску',0,3,'2013-04-03 14:25:27',0),(7,2,2,' Продано больше чем нужно',0,3,'2013-04-03 14:25:27',0),(8,3,2,' Продажи дешевле цены очередности [64650]',0,5,'2013-04-03 14:27:23',0),(9,3,2,' Продажи дешевле цены очередности [64090]',0,5,'2013-04-03 14:27:23',0),(10,3,2,' Продажи дешевле цены очередности [64660]',0,5,'2013-04-03 14:27:23',0),(11,3,1,' Нужна прогрузка цены на касску',0,3,'2013-04-03 14:27:24',0),(12,3,2,' Продано больше чем нужно',0,3,'2013-04-03 14:27:24',0);

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `id` tinyint(4) NOT NULL AUTO_INCREMENT,
  `username` tinytext NOT NULL,
  `hash` tinytext NOT NULL,
  `group` tinyint(2) DEFAULT NULL,
  `online` tinyint(1) DEFAULT NULL,
  `ip` tinytext,
  `tasks_count` int(11) DEFAULT NULL,
  `tasks_failed` int(11) DEFAULT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `users` */

insert  into `users`(`id`,`username`,`hash`,`group`,`online`,`ip`,`tasks_count`,`tasks_failed`) values (1,'partizanes','1ed6ad31cf5f6e0d10e18f8db3f08c9f',2,1,'127.0.0.1',0,0),(4,'user','91ff7d7746319a2d34f93537c3e54173',1,0,'127.0.0.1',0,0);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
