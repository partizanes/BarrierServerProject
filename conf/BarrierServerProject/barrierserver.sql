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

insert  into `operations`(`id`,`operation`,`count`,`price`,`inactive`) values (4,51,13.000,10150,0),(5,51,4.000,2750,0),(7,51,20.000,7050,0),(7,51,30.000,7400,0);

/*Table structure for table `priority` */

DROP TABLE IF EXISTS `priority`;

CREATE TABLE `priority` (
  `id` bigint(20) NOT NULL auto_increment,
  `bar` tinytext NOT NULL,
  `name` tinytext NOT NULL,
  `turn_price` mediumint(7) unsigned NOT NULL,
  `count` float(7,3) NOT NULL,
  `sailed` float(7,3) default NULL,
  `status` tinyint(2) NOT NULL,
  `status_text` tinytext NOT NULL,
  `current_price_ukm` mediumint(7) NOT NULL,
  `date` datetime NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

/*Data for the table `priority` */

insert  into `priority`(`id`,`bar`,`name`,`turn_price`,`count`,`sailed`,`status`,`status_text`,`current_price_ukm`,`date`) values (4,'481027200011','САХАР РАФИНАД 1КГ',10150,15.000,13.000,0,'',10150,'2013-03-25 17:43:17'),(5,'481076700458','ЙОГУРТ ШАЛУН 1.5% АНАНАС 150Г',2750,12.000,4.000,0,'',2750,'2013-04-01 17:43:17'),(6,'481059100337','ПОВИДЛО ЯБЛОЧНОЕ 0.5Л',8600,15.000,NULL,0,'',8600,'2013-03-03 18:12:31'),(7,'481059100001','РАССОЛЬНИК 0.5Л',7400,12.000,50.000,0,'',7400,'2013-03-03 19:41:15');

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

insert  into `sendpos`(`id`,`price`,`kod_isp`,`datetime`,`action`) values (1,8600,30,'2013-04-02 20:32:26','К3'),(1,8600,30,'2013-04-02 20:32:26','К3'),(2,7400,24,'2013-03-18 11:25:10','К3'),(2,7400,24,'2013-03-18 11:27:23','Ц2'),(2,7400,30,'2013-04-02 20:32:26','К3'),(3,7400,24,'2013-03-18 11:25:10','К3'),(3,7400,24,'2013-03-18 11:27:23','Ц2'),(3,7400,30,'2013-04-02 20:32:26','К3'),(4,7400,24,'2013-03-18 11:25:10','К3'),(4,7400,24,'2013-03-18 11:27:23','Ц2'),(4,7400,30,'2013-04-02 20:32:26','К3'),(1,8600,30,'2013-04-02 20:32:26','К3'),(5,8600,30,'2013-04-02 20:32:26','К3'),(2,7400,24,'2013-03-18 11:25:10','К3'),(2,7400,24,'2013-03-18 11:27:23','Ц2'),(2,7400,30,'2013-04-02 20:32:26','К3'),(3,7400,24,'2013-03-18 11:25:10','К3'),(3,7400,24,'2013-03-18 11:27:23','Ц2'),(3,7400,30,'2013-04-02 20:32:26','К3'),(4,7400,24,'2013-03-18 11:25:10','К3'),(4,7400,24,'2013-03-18 11:27:23','Ц2'),(4,7400,30,'2013-04-02 20:32:26','К3'),(1,8600,30,'2013-04-02 20:32:26','К3'),(5,8600,30,'2013-04-02 20:32:26','К3'),(6,8600,30,'2013-04-02 20:32:26','К3'),(2,7400,24,'2013-03-18 11:25:10','К3'),(2,7400,24,'2013-03-18 11:27:23','Ц2'),(2,7400,30,'2013-04-02 20:32:26','К3'),(3,7400,24,'2013-03-18 11:25:10','К3'),(3,7400,24,'2013-03-18 11:27:23','Ц2'),(3,7400,30,'2013-04-02 20:32:26','К3'),(4,7400,24,'2013-03-18 11:25:10','К3'),(4,7400,24,'2013-03-18 11:27:23','Ц2'),(4,7400,30,'2013-04-02 20:32:26','К3'),(1,8600,30,'2013-04-02 20:32:26','К3'),(5,8600,30,'2013-04-02 20:32:26','К3'),(6,8600,30,'2013-04-02 20:32:26','К3'),(7,8600,30,'2013-04-02 20:32:26','К3'),(2,7400,24,'2013-03-18 11:25:10','К3'),(2,7400,24,'2013-03-18 11:27:23','Ц2'),(2,7400,30,'2013-04-02 20:32:26','К3'),(3,7400,24,'2013-03-18 11:25:10','К3'),(3,7400,24,'2013-03-18 11:27:23','Ц2'),(3,7400,30,'2013-04-02 20:32:26','К3'),(4,7400,24,'2013-03-18 11:25:10','К3'),(4,7400,24,'2013-03-18 11:27:23','Ц2'),(4,7400,30,'2013-04-02 20:32:26','К3'),(1,8600,30,'2013-04-02 20:32:26','К3'),(8,8600,30,'2013-04-02 20:32:26','К3'),(2,7400,24,'2013-03-18 11:25:10','К3'),(2,7400,24,'2013-03-18 11:27:23','Ц2'),(2,7400,30,'2013-04-02 20:32:26','К3'),(3,7400,24,'2013-03-18 11:25:10','К3'),(3,7400,24,'2013-03-18 11:27:23','Ц2'),(3,7400,30,'2013-04-02 20:32:26','К3'),(4,7400,24,'2013-03-18 11:25:10','К3'),(4,7400,24,'2013-03-18 11:27:23','Ц2'),(4,7400,30,'2013-04-02 20:32:26','К3'),(1,8600,30,'2013-04-02 20:32:26','К3'),(8,8600,30,'2013-04-02 20:32:26','К3'),(9,8600,30,'2013-04-02 20:32:26','К3'),(2,7400,24,'2013-03-18 11:25:10','К3'),(2,7400,24,'2013-03-18 11:27:23','Ц2'),(2,7400,30,'2013-04-02 20:32:26','К3'),(3,7400,24,'2013-03-18 11:25:10','К3'),(3,7400,24,'2013-03-18 11:27:23','Ц2'),(3,7400,30,'2013-04-02 20:32:26','К3'),(4,7400,24,'2013-03-18 11:25:10','К3'),(4,7400,24,'2013-03-18 11:27:23','Ц2'),(4,7400,30,'2013-04-02 20:32:26','К3'),(9,8600,30,'2013-04-02 20:32:26','К3'),(2,7400,24,'2013-03-18 11:25:10','К3'),(2,7400,24,'2013-03-18 11:27:23','Ц2'),(2,7400,30,'2013-04-02 20:32:26','К3'),(3,7400,24,'2013-03-18 11:25:10','К3'),(3,7400,24,'2013-03-18 11:27:23','Ц2'),(3,7400,30,'2013-04-02 20:32:26','К3'),(4,7400,24,'2013-03-18 11:25:10','К3'),(4,7400,24,'2013-03-18 11:27:23','Ц2'),(4,7400,30,'2013-04-02 20:32:26','К3'),(9,8600,30,'2013-04-02 20:32:26','К3'),(2,7400,24,'2013-03-18 11:25:10','К3'),(2,7400,24,'2013-03-18 11:27:23','Ц2'),(2,7400,30,'2013-04-02 20:32:26','К3'),(3,7400,24,'2013-03-18 11:25:10','К3'),(3,7400,24,'2013-03-18 11:27:23','Ц2'),(3,7400,30,'2013-04-02 20:32:26','К3'),(4,7400,24,'2013-03-18 11:25:10','К3'),(4,7400,24,'2013-03-18 11:27:23','Ц2'),(4,7400,30,'2013-04-02 20:32:26','К3'),(9,8600,30,'2013-04-02 20:32:26','К3'),(2,7400,24,'2013-03-18 11:25:10','К3'),(2,7400,24,'2013-03-18 11:27:23','Ц2'),(2,7400,30,'2013-04-02 20:32:26','К3'),(3,7400,24,'2013-03-18 11:25:10','К3'),(3,7400,24,'2013-03-18 11:27:23','Ц2'),(3,7400,30,'2013-04-02 20:32:26','К3'),(4,7400,24,'2013-03-18 11:25:10','К3'),(4,7400,24,'2013-03-18 11:27:23','Ц2'),(4,7400,30,'2013-04-02 20:32:26','К3');

/*Table structure for table `tasks` */

DROP TABLE IF EXISTS `tasks`;

CREATE TABLE `tasks` (
  `tasks_id` bigint(20) unsigned NOT NULL auto_increment,
  `priority_id` bigint(20) unsigned NOT NULL,
  `user_group` int(2) NOT NULL,
  `task_text` longtext NOT NULL,
  `user_id` tinyint(4) NOT NULL,
  `priority` tinyint(4) NOT NULL,
  `date` datetime NOT NULL,
  `inactive` tinyint(1) unsigned NOT NULL,
  UNIQUE KEY `tasks_id` (`tasks_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

/*Data for the table `tasks` */

insert  into `tasks`(`tasks_id`,`priority_id`,`user_group`,`task_text`,`user_id`,`priority`,`date`,`inactive`) values (2,7,2,' Продажи дешевле цены очередности [7050]',0,5,'2013-04-03 19:42:12',0),(3,7,1,' Нужна прогрузка цены на кассу',0,3,'2013-04-03 19:42:12',0),(4,7,2,' Продано больше чем нужно',0,3,'2013-04-03 19:42:12',0),(9,6,1,'481059100337 Нет продаж.Число дней: [31] ',0,5,'2013-04-03 20:20:17',0);

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `id` tinyint(4) NOT NULL auto_increment,
  `username` tinytext NOT NULL,
  `hash` tinytext NOT NULL,
  `group` tinyint(2) default NULL,
  `online` tinyint(1) default NULL,
  `ip` tinytext,
  `tasks_count` int(11) default NULL,
  `tasks_failed` int(11) default NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `users` */

insert  into `users`(`id`,`username`,`hash`,`group`,`online`,`ip`,`tasks_count`,`tasks_failed`) values (1,'partizanes','1ed6ad31cf5f6e0d10e18f8db3f08c9f',2,0,'127.0.0.1',NULL,NULL),(2,'user','91ff7d7746319a2d34f93537c3e54173',1,0,'127.0.0.1',NULL,NULL);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
