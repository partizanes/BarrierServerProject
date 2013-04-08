using System;
using System.Data.OleDb;

namespace LsTradeAgent
{
    class Dbf
    {
        private static string LsTradeDir = Config.GetParametr("LsTradeDir");
        public static string ConnectingString = "Provider=vfpoledb.1;Data Source=" + LsTradeDir + ";Mode=Read;Collating Sequence=MACHINE;CODEPAGE=1251";
    }
}
