using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.IO;

namespace Modern_laba4
{
    class Worker
    {
        string xmlFilePath = "crewList.xml";
        string binaryPath = "crewList.dat";
        private List<Crew> crewList = new List<Crew>();
        private List<Route> RouteList = new List<Route>();
        private List<City> cityList = new List<City>();
        private Crew CrewMaxLoadCapacity;
        private List<Crew> tmpCrewList;

        public Worker()
        {
            Console.OutputEncoding = Encoding.GetEncoding(UTF8Encoding.UTF8.WebName);
            Console.InputEncoding = Encoding.GetEncoding(1251);
        }
        public void menu()
        {

            string header = "Виберіть дію";
            IDictionary<string, string> menu = new Dictionary<string, string>()
            {
                {"1", "Керування містами"},
                {"2", "Керування екіпажами"},
                {"3", "Керування маршрутами"},

                {"4", "Вивести інформацію"},
                {"5", "Пошук найдовшого за часом маршруту"},
                {"6", "Серіалізація - десеріалізація екіпажів"},
                {"0", "Вихід"}
            };

            IDictionary<string, string> methtods = new Dictionary<string, string>()
            {
                {"1", "cityMenu"},
                {"2", "crewMenu"},
                {"3", "routeMenu"},

                {"4", "display"},
                {"5", "search"},
                {"6", "serilize"},
                {"0", "exit"}
            };

            string active = System.Reflection.MethodInfo.GetCurrentMethod().Name;

            execMethod(display(header, menu, methtods, active));
        }

        public void serilize()
        {
            if (crewList.Count() > 0)
            {
                Console.WriteLine(new string('_', 7) + " XML " + new string('_', 7));
                Serialize(new XmlSerializer(typeof(List<Crew>)), xmlFilePath, crewList);
                Deserialize(new XmlSerializer(typeof(List<Crew>)), xmlFilePath);
                //Console.WriteLine(new string('_', 7) + " BINARY " + new string('_', 7));
                //Serialize(new BinaryFormatter(), binaryPath, crewList);
                //Deserialize(new BinaryFormatter(), binaryPath);
                displayInfoEntity(CrewMaxLoadCapacity, "Максимальна вантажопідйомність");
                Console.ReadKey();
            } 
            menu();
        }

        public void routeMenu()
        {

            string header = "Виберіть дію";
            IDictionary<string, string> menu = new Dictionary<string, string>()
            {
                {"1", "Додати ланку маршруту"},
                {"0", "Головне меню"}
            };

            IDictionary<string, string> methtods = new Dictionary<string, string>()
            {
                {"1", "addRoute"},
                {"0", "menu"}
            };

            string active = System.Reflection.MethodInfo.GetCurrentMethod().Name;

            execMethod(display(header, menu, methtods, active));
        }

        public void cityMenu()
        {

            string header = "Виберіть дію";
            IDictionary<string, string> menu = new Dictionary<string, string>()
            {
                {"1", "Додати місто"},
                {"0", "Головне меню"}
            };

            IDictionary<string, string> methtods = new Dictionary<string, string>()
            {
                {"1", "addCity"},
                {"0", "menu"}
            };

            string active = System.Reflection.MethodInfo.GetCurrentMethod().Name;

            execMethod(display(header, menu, methtods, active));
        }

        public void crewMenu()
        {

            string header = "Виберіть дію";
            IDictionary<string, string> menu = new Dictionary<string, string>()
            {
                {"1", "Додати екіпаж"},
                {"0", "Головне меню"}
            };

            IDictionary<string, string> methtods = new Dictionary<string, string>()
            {
                {"1", "addCrew"},
                {"0", "menu"}
            };

            string active = System.Reflection.MethodInfo.GetCurrentMethod().Name;

            execMethod(display(header, menu, methtods, active));
        }

        private void execMethod(string method)
        {
            Type thisType = this.GetType();
            MethodInfo theMethod = thisType.GetMethod(method);
            theMethod.Invoke(this, null);
        }

        private string display(string header, IDictionary<string, string> menu, IDictionary<string, string> methtods, string active)
        {
            HelperIO.welcomeMessage(header);
            Console.WriteLine("Будь ласка виберіть один з пунктів:");
            foreach (KeyValuePair<string, string> item in menu)
            {
                Console.WriteLine("{0} - {1}", item.Key, item.Value);
            }
            string choice = Console.ReadLine();
            if (methtods.ContainsKey(choice))
            {
                Console.Clear();
                string value;
                methtods.TryGetValue(choice, out value);
                return value;
            }
            else
            {
                HelperIO.displayError();
                return active;
            }
        }

        public void addCrew()
        {
            displayInfoEntities(crewList, "Список екіпажів");
            crewList.Add(new Crew());

            crewList.Last().CarNumber = HelperIO.stringInput("Введіть номер авто");
            crewList.Last().DriverName = HelperIO.stringInput("Введіть ПІБ водія");
            crewList.Last().LoadCapacity = HelperIO.intAbsInput("Введіть вантажопідйомність авто");
            addRoute(crewList.Last().Id);
            menu();
        }

        public void addCity()
        {
            displayInfoEntities(cityList, "Список міст");
            cityList.Add(new City());

            cityList.Last().Name = HelperIO.stringInput("Введіть назву міста");
            cityMenu();
        }

        public void addRoute()
        {
            displayInfoEntities(crewList, "Список екіпажів");
            addRoute(crewList[HelperIO.intAbsInput("Введіть номер екіпажу", crewList.Count)].Id);
            routeMenu();
        }
        private void addRoute(int routeId)
        {
            displayInfoRoutes(routeId, "Список маршрутів");

            RouteList.Add(new Route(routeId));

            displayInfoEntities(cityList, "Список міст");
            RouteList.Last().CityFrom = cityList[HelperIO.intAbsInput("Введіть номер міста початку маршруту", cityList.Count)];
            displayInfoEntities(cityList, "Список міст");
            RouteList.Last().CityTo = cityList[HelperIO.intAbsInput("Введіть номер міста кінця маршруту", cityList.Count)];
            RouteList.Last().ArrivalTime = HelperIO.intAbsInput("Введіть тривалість поїздки, хв");
        }

        private void displayInfoRoutes(int routeId, string header)
        {
            if (RouteList.Count > 0)
            {
                HelperIO.welcomeMessage(header);
                foreach (Route element in RouteList)
                {
                    if (element.RouteId == routeId)
                    {
                        System.Console.WriteLine(element.ToString());
                    }

                }
            } 
        }

        private void displayInfoEntities(dynamic one, string header)
        {
            if (one.Count > 0)
            {
                HelperIO.welcomeMessage(header);
            }
            int i = 0;
            foreach (Entity element in one)
            {
                System.Console.WriteLine((i++) + " -> " + element.ToString());
            }
        }

        private void displayInfoEntity(dynamic one, string header)
        {
            if (header != "")
            {
                HelperIO.welcomeMessage(header);
            }
            System.Console.WriteLine(one.ToString());
        }

        public void display()
        {
            displayInfoEntities(crewList, "Список екіпажів");
            HelperIO.successMessage("-------------------------------------------------------------------------------");
            menu();
        }

        public void search()
        {
            if (crewList.Count > 0)
            {
                int[] maxTime = new int[crewList.Count];
                foreach (Crew tmp in crewList)
                {
                    int tmpMax = 0;
                    foreach (Route tmp2 in RouteList)
                    {
                        if (tmp2.RouteId == tmp.Id)
                        {
                            tmpMax += tmp2.ArrivalTime;
                            maxTime[crewList.IndexOf(tmp)] = tmpMax;
                        }
                    }
                }
                int max = 0;
                int num = 0;
                for (int i = 0; i < maxTime.Length; i++)
                {
                    if (maxTime[i] > max)
                    {
                        max = maxTime[i];
                        num = i;
                    }
                }
                displayInfoEntity(crewList[num], "Найдовший маршрут");
            }
            else
            {
                Console.WriteLine("Результатів не знайдено. Ще немає екіпажів");
            }
            menu();
        }

        public void exit()
        {

        }

        protected void Serialize(dynamic serializer, string path, List<Crew> crewList)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(fs, crewList);
            }
        }

        protected void Deserialize(dynamic serializer, string path)
        {
            this.CrewMaxLoadCapacity = null;
            TestDelegate del = new TestDelegate(this.UpdateCarIdMax);
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                tmpCrewList = (List<Crew>)serializer.Deserialize(fs);
                foreach (Crew tmp in tmpCrewList)
                {
                    Console.WriteLine(tmp.ToString() + Environment.NewLine);
                    if(tmp.Id != 0)
                    {
                        del(tmp.Id);
                    }
                }
            }
        }

        public delegate void TestDelegate(int id);

        protected void UpdateCarIdMax(int id)
        {
            foreach (Crew tmp in tmpCrewList)
            {
                if (this.CrewMaxLoadCapacity == null)
                {
                    this.CrewMaxLoadCapacity = tmp;
                } else
                {
                    if (tmp.LoadCapacity > this.CrewMaxLoadCapacity.LoadCapacity)
                    {
                        this.CrewMaxLoadCapacity = tmp;
                    }
                }
            }
        }
    }
}
