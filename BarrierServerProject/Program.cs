using System;
using System.Threading;
using System.Windows.Forms;
using ConsoleFunctions;

namespace BarrierServerProject
{
    class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static IntPtr ConsoleHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;

        private const int SW_MINIMIZE = 6;
        private const int SW_MAXIMIZE = 3;

        public static bool debug = Boolean.Parse(Config.GetParametr("Debug"));

        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        private static bool Handler(CtrlType sig)
        {
            switch (sig)
            {
                case CtrlType.CTRL_C_EVENT:
                case CtrlType.CTRL_LOGOFF_EVENT:
                case CtrlType.CTRL_SHUTDOWN_EVENT:
                case CtrlType.CTRL_CLOSE_EVENT:
                default:
                     Packages.connector.ExecuteNonQuery("UPDATE `users` SET `online`='0' ");
                    return true;
            }
        }

        static void Main(string[] args)
        {
            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);

            if (System.Diagnostics.Process.GetProcessesByName(Application.ProductName).Length > 1)
            {
                Color.WriteLineColor("\n", ConsoleColor.Red);
                Color.WriteLineColor("Приложение уже запущено!", ConsoleColor.Red);
                Color.WriteLineColor("\n", ConsoleColor.Red);
                Color.WriteLineColor("Запускаю завершение работы...", ConsoleColor.Red);
                Color.WriteLineColor("\n", ConsoleColor.Red);

                another_run();
            }
            else
            {
                CheckDll();

                ShowWindow(ConsoleHandle, SW_MAXIMIZE);

                Logo LogoLoad = new Logo();

                LogoLoad.LogoLoad();

                Server.ServerStart();

                Thread th = new Thread(delegate()
                {
                    Thread.Sleep(3000);

                    while (true)
                    {
                        Color.WriteLineColor("[THREAD] CheckSailAndPriceUpdate запущен", ConsoleColor.DarkYellow);

                        CheckThisBar.CheckSailAndPriceUpdate();

                        Color.WriteLineColor("[THREAD] CheckSailAndPriceUpdate завершен", ConsoleColor.DarkYellow);
                        Thread.Sleep(1800000);
                    }
                });
                th.Name = "Проверка задач";
                th.Start();
            }

            Thread.Sleep(1000);

            while (true)
            {
                Color.WriteLineColor("\nВведите комманду:", ConsoleColor.Green);

                string com = Console.ReadLine().ToLower();

                Color.WriteLineColor(Command.SwitchCommand(com), ConsoleColor.Yellow);
            }
        }
        public static void another_run()
        {
            int i = 10;

            while (i != 0)
            {
                Code.RenderConsoleProgress(i * 10, '\u2592', ConsoleColor.Red, "Завершение работы сервера через [" + i.ToString() + " second]");
                Thread.Sleep(1000);
                i--;
            }
            Environment.Exit(0);
        }

        private static void CheckDll()
        {
            while (!System.IO.File.Exists(Environment.CurrentDirectory + "\\" + "Serialization.dll"))
            {
                MessageBox.Show("[" + DateTime.Now.ToLongTimeString() + "] " + "В папке с программой отсутствует нужная для работы библиотека Serialization.dll \n Скопируйте в папку с программой библиотеку и нажмите ок!");
            }
        }
    }
}
