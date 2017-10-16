using System;
using System.Globalization;

namespace Modern_laba4
{
    static class HelperIO
    {
        internal static string stringInput(string msg)
        {
            string str;
            do
            {
                labelMessage(msg);
                str = Console.ReadLine();
                Console.Clear();
            } while (str == "");
            return str;
        }

        internal static string stringInput(string prev, string msg)
        {
            string str;
            labelMessage(prev, msg);
            str = Console.ReadLine();
            return str != "" ? str : prev;
        }

        internal static decimal decimalAbsInput(string msg)
        {
            decimal result;
            do
            {
                labelMessage(msg);
                if (!Decimal.TryParse(Console.ReadLine(), out result))
                {
                    result = -1;
                    displayError();
                }
                Console.Clear();
            } while (result == -1);
            return Math.Abs(result);
        }
        internal static decimal decimalAbsInput(decimal prev, string msg)
        {
            decimal result;
            do
            {
                labelMessage(prev.ToString(), msg);
                string str = Console.ReadLine();
                if (!Decimal.TryParse(str, out result))
                {
                    if (str == "")
                    {
                        return prev;
                    }
                    result = -1;
                    displayError();
                }
                Console.Clear();
            } while (result == -1);
            return Math.Abs(result);
        }
        internal static int intAbsInput(string msg)
        {
            int result;
            do
            {
                labelMessage(msg);
                if (!Int32.TryParse(Console.ReadLine(), out result))
                {
                    result = -1;
                    displayError();
                }
                Console.Clear();
            } while (result == -1);
            return Math.Abs(result);
        }

        internal static int intAbsInput(int prev, string msg)
        {
            int result;
            do
            {
                labelMessage(prev.ToString(), msg);
                string str = Console.ReadLine();
                if (!Int32.TryParse(str, out result))
                {
                    if (str == "")
                    {
                        return prev;
                    }
                    result = -1;
                    displayError();
                }
                Console.Clear();
            } while (result == -1);
            return Math.Abs(result);
        }
        internal static int intAbsInput(string msg, int limit)
        {
            int result;
            do
            {
                labelMessage(msg);
                if (!Int32.TryParse(Console.ReadLine(), out result) || result >= limit)
                {
                    result = -1;
                    displayError();
                }
                Console.Clear();
            } while (result == -1);
            return Math.Abs(result);
        }
        internal static int intAbsInput(int prev, string msg, int limit)
        {
            int result;
            do
            {
                labelMessage(prev.ToString(), msg);
                string str = Console.ReadLine();
                if (!Int32.TryParse(str, out result) || result >= limit)
                {
                    if (str == "")
                    {
                        return prev;
                    }
                    result = -1;
                    displayError();
                }
                Console.Clear();
            } while (result == -1);
            return Math.Abs(result);
        }
        internal static int selectId(string prev, string msg, int limit)
        {
            bool success = false;
            int result;
            do
            {
                labelMessage(prev, msg);
                string str = Console.ReadLine();
                if (!Int32.TryParse(str, out result) || result >= limit)
                {
                    if (str == "")
                    {
                        return -1;
                    }
                    displayError();
                }
                else
                {
                    success = true;
                }
                Console.Clear();
            } while (!success);
            return Math.Abs(result);
        }
        internal static DateTime dateInput(string msg)
        {
            bool success = false;
            DateTime result;
            do
            {
                labelMessage(msg);
                if (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    success = false;
                    displayError();
                }
                else
                {
                    success = true;
                }
                Console.Clear();
            } while (!success);
            return result;
        }
        internal static DateTime dateInput(DateTime prev, string msg)
        {
            bool success = false;
            DateTime result;
            do
            {
                labelMessage(prev.ToString("dd.MM.yyyy"), msg);
                string str = Console.ReadLine();
                if (!DateTime.TryParseExact(str, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    if (str == "")
                    {
                        return prev;
                    }
                    success = false;
                    displayError();
                }
                else
                {
                    success = true;
                }
                Console.Clear();
            } while (!success);
            return result;
        }
        internal static void welcomeMessage(string msg)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        private static void labelMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(msg + ": ");
            Console.ResetColor();
        }

        private static void labelMessage(string prev, string msg)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Старе значення: ");
            Console.ResetColor();
            Console.WriteLine(prev);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(msg + ": ");
            Console.ResetColor();
        }

        internal static void displayError()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Помилка, натиснiть 'enter' для повторної спроби");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }

        internal static void successMessage(String msg)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        internal static bool confirmDialog(object obj)
        {
            int result;
            Console.WriteLine("Видаляємо " + obj.ToString());
            String msg = "Ви впевнені? 1 - Так";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
            if (Int32.TryParse(Console.ReadLine(), out result) && result == 1)
            {
                return true;
            }
            return false;
        }
    }
}