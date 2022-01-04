using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            First();
            Second();
            Third();
            Four();
            Fifth();
            Sixth();
            Seventh();
            Eight();
            Ninth();
            Tenth();
            Eleven();
            Twelve();
            Console.ReadKey();
        }

        /// <summary>
        /// first
        /// </summary>
        static void First() {
            string[] teams = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };
            Console.WriteLine("First result");
            Console.WriteLine(new string('=', 30));
            FirstMyVersion(teams);
            Console.WriteLine(new string('-', 30));
            FirstWithoutLinq(teams);
            Console.WriteLine(new string('-', 30));
            FirstWithLinq(teams);
            Console.WriteLine(new string('-', 30));
            FirstAlternative(teams);
            Console.WriteLine(new string('=', 30));
        }
        static void FirstMyVersion(string[] teams)
        {
            var selectedTeams = teams.Where(t => t.FirstOrDefault().ToString().ToUpper().Equals("Б")).OrderBy(t => t);

            foreach (string s in selectedTeams)
                Console.WriteLine(s);
        }
        static void FirstWithoutLinq(string[] teams)
        {
            var selectedTeams = new List<string>();
            foreach (string s in teams)
            {
                if (s.ToUpper().StartsWith("Б"))
                    selectedTeams.Add(s);
            }
            selectedTeams.Sort();

            foreach (string s in selectedTeams)
                Console.WriteLine(s);
        }
        static void FirstWithLinq(string[] teams)
        {
            var selectedTeams = from t in teams // определяем каждый объект из teams как t
                                where t.ToUpper().StartsWith("Б") //фильтрация по критерию
                                orderby t  // упорядочиваем по возрастанию
                                select t; // выбираем объект

            foreach (string s in selectedTeams)
                Console.WriteLine(s);
        }
        static void FirstAlternative(string[] teams)
        {
            var selectedTeams = teams.Where(t => t.ToUpper().StartsWith("Б")).OrderBy(t => t);

            foreach (string s in selectedTeams)
                Console.WriteLine(s);
        }

        
        /// <summary>
        /// Second
        /// </summary>
        static void Second() {
            int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
            Console.WriteLine("Second result");
            Console.WriteLine(new string('=', 30));
            SecondMyVersion(numbers);
            Console.WriteLine(new string('-', 30));
            SecondWithoutLinq(numbers);
            Console.WriteLine(new string('-', 30));
            SecondWithLinq(numbers);
            Console.WriteLine(new string('-', 30));
            SecondAlternative(numbers);
            Console.WriteLine(new string('=', 30));
        }
        static void SecondMyVersion(int[] numbers) {
            var evens = from i in numbers where i % 2 == 0 && i > 10 orderby i select i;

            foreach (int i in evens)
                Console.WriteLine(i);
        }
        static void SecondWithoutLinq(int[] numbers) {
            List<int> evens = new List<int>();

            foreach (int i in numbers)
            {
                if (i % 2 == 0 && i > 10)
                    evens.Add(i);
            }

            foreach (int i in evens)
                Console.WriteLine(i);
        }
        static void SecondWithLinq(int[] numbers)
        {
            IEnumerable<int> evens = from i in numbers
                                     where i % 2 == 0 && i > 10
                                     select i;

            foreach (int i in evens)
                Console.WriteLine(i);
        }
        static void SecondAlternative(int[] numbers)
        {
            IEnumerable<int> evens = numbers.Where(i => i % 2 == 0 && i > 10);

            foreach (int i in evens)
                Console.WriteLine(i);
        }


        /// <summary>
        /// Third
        /// </summary>
        /// 
        class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public List<string> Languages { get; set; }
            public User()
            {
                Languages = new List<string>();
            }
        }
        static void Third() {
            List<User> users = new List<User>
            {
                new User {Name="Том", Age=23, Languages = new List<string> {"английский", "немецкий" }},
                new User {Name="Боб", Age=27, Languages = new List<string> {"английский", "французский" }},
                new User {Name="Джон", Age=29, Languages = new List<string> {"английский", "испанский" }},
                new User {Name="Элис", Age=24, Languages = new List<string> {"испанский", "немецкий" }}
            }; 
            Console.WriteLine("Third result");
            Console.WriteLine(new string('=', 30));
            ThirdWithLinq(users);
            Console.WriteLine(new string('=', 30));
            ThirdWithLinqHard(users);
            Console.WriteLine(new string('-', 30));
            ThirdAlternativeHard(users);
            Console.WriteLine(new string('=', 30));
            ThirdWithLinqNew(users);
            Console.WriteLine(new string('=', 30)); 
            ThirdWithLinqLet(users);
            Console.WriteLine(new string('=', 30));
        }
        static void ThirdWithLinq(List<User> users) {
            var selectedUsers = from user in users
                                where user.Age > 25
                                select user;

            foreach (User user in selectedUsers)
                Console.WriteLine("{0} - {1}", user.Name, user.Age);
        }
        static void ThirdWithLinqHard(List<User> users)
        {
            var selectedUsers = from user in users
                                from lang in user.Languages
                                where user.Age < 28
                                where lang == "английский"
                                select user;

            foreach (User user in selectedUsers)
                Console.WriteLine("{0} - {1}", user.Name, user.Age);
        }
        static void ThirdAlternativeHard(List<User> users) {
            var selectedUsers = users.SelectMany(u => u.Languages,
                                (u, l) => new { User = u, Lang = l })
                              .Where(u => u.Lang == "английский" && u.User.Age < 28)
                              .Select(u => u.User);

            foreach (User user in selectedUsers)
                Console.WriteLine("{0} - {1}", user.Name, user.Age);
        }
        static void ThirdWithLinqNew(List<User> users)
        {
            var items = from u in users
                        select new
                        {
                            FirstName = u.Name,
                            DateOfBirth = DateTime.Now.Year - u.Age
                        };

            foreach (var n in items)
                Console.WriteLine("{0} - {1}", n.FirstName, n.DateOfBirth);
        }
        static void ThirdWithLinqLet(List<User> users)
        {
            var people = from u in users
                         let name = "Mr. " + u.Name
                         select new
                         {
                             Name = name,
                             Age = u.Age
                         };

            foreach (var p in people)
                Console.WriteLine("{0} - {1}", p.Name, p.Age);
        }

        /// <summary>
        /// Four
        /// </summary>
        class Phone
        {
            public string Name { get; set; }
            public string Company { get; set; }
        }

        static void Four() {
            List<User> users = new List<User>()
            {
                new User { Name = "Sam", Age = 43 },
                new User { Name = "Tom", Age = 33 }
            }; 
            List<Phone> phones = new List<Phone>()
            {
                new Phone {Name="Lumia 630", Company="Microsoft" },
                new Phone {Name="iPhone 6", Company="Apple"},
            };
            Console.WriteLine("Four result");
            Console.WriteLine(new string('=', 30));
            FourWithLinq(users, phones);


            Console.WriteLine(new string('=', 30));
        }
        static void FourWithLinq(List<User> users, List<Phone> phones) {
            var people = from user in users
                         from phone in phones
                         select new { Name = user.Name, Phone = phone.Name };

            foreach (var p in people)
                Console.WriteLine("{0} - {1}", p.Name, p.Phone);
        }

        /// <summary>
        /// Fifth
        /// </summary>
        static void Fifth()
        {
            List<User> users = new List<User>()
            {
                new User { Name = "Tom", Age = 33 },
                new User { Name = "Bob", Age = 30 },
                new User { Name = "Tom", Age = 21 },
                new User { Name = "Sam", Age = 43 }
            };

            Console.WriteLine("Fifth result");
            Console.WriteLine(new string('=', 30));
            FifthWithLinq(users);
            Console.WriteLine(new string('-', 30));
            FifthWithoutLinq(users);
            Console.WriteLine(new string('=', 30));
        }

        static void FifthWithLinq(List<User> users){
        var sortedUsers = from u in users
                  orderby u.Name descending, u.Age descending
                  select u;
            
        foreach (User u in sortedUsers)
            Console.WriteLine(u.Name);
        }
        static void FifthWithoutLinq(List<User> users){
            var sortedUsers = users.OrderByDescending(u => u.Name).ThenByDescending(u => u.Age);
            
        foreach (User u in sortedUsers)
            Console.WriteLine(u.Name +" - "+ u.Age);
        }

        static void Sixth() {
            string[] soft = { "Microsoft", "Google", "Apple" };
            string[] hard = { "Apple", "IBM", "Samsung" };

            // разность множеств
            var result = soft.Except(hard);
            foreach (string s in result)
                Console.Write(s + "; ");
            Console.WriteLine();

            // пересечение множеств
            result = soft.Intersect(hard);
            foreach (string s in result)
                Console.Write(s + "; ");
            Console.WriteLine();

            // объединение множеств
            result = soft.Union(hard);

            foreach (string s in result)
                Console.Write(s + "; ");
            Console.WriteLine();

            // конкатенация множеств
            result = soft.Concat(hard);
            foreach (string s in result)
                Console.Write(s + "; ");
            Console.WriteLine();

            // удаление дупликатов
            result = soft.Concat(hard).Distinct();
            foreach (string s in result)
                Console.Write(s + "; ");
            Console.WriteLine();


            Console.WriteLine(new string('=', 30));
        }

        static void Seventh() {
            Console.WriteLine("Seventh result");
            Console.WriteLine(new string('=', 30));

            int[] numbers = { 1, 2, 3, 4, 5 };
            //агрегация
            int query = numbers.Aggregate((x, y) => x - y); // аналогично (((1 - 2) - 3) - 4) - 5
            Console.WriteLine(query);
            query = numbers.Aggregate((x, y) => x + y); // аналогично (((1 + 2) + 3) + 4) + 5
            Console.WriteLine(query);

            //count
            numbers = new int[] { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
            int size = (from i in numbers where i % 2 == 0 && i > 10 select i).Count();
            Console.WriteLine(size);
            size = numbers.Count(i => i % 2 == 0 && i > 10);
            Console.WriteLine(size);

            //Сумма
            List<User> users = new List<User>()
            {
                new User { Name = "Tom", Age = 23 },
                new User { Name = "Sam", Age = 43 },
                new User { Name = "Bill", Age = 35 }
            };

            int sum1 = numbers.Sum();
            Console.WriteLine(sum1);
            decimal sum2 = users.Sum(n => n.Age);
            Console.WriteLine(sum2);

            int min1 = numbers.Min();
            int min2 = users.Min(n => n.Age); // минимальный возраст
            Console.WriteLine(min1 + " - " + min2);

            int max1 = numbers.Max();
            int max2 = users.Max(n => n.Age); // максимальный возраст
            Console.WriteLine(max1 + " - " + max2);

            double avr1 = numbers.Average();
            double avr2 = users.Average(n => n.Age); //средний возраст
            Console.WriteLine(avr1 + " - " + avr2);

            numbers = new int[] { -3, -2, -1, 0, 1, 2, 3 };
            Console.Write("Numbers: ");
            foreach (int i in numbers)
                Console.Write(i + "; ");
            Console.WriteLine();

            //take
            Console.Write("Take 3: ");
            var result = numbers.Take(3);
            foreach (int i in result)
                Console.Write(i + "; ");
            Console.WriteLine();

            //skip
            Console.Write("Skip 3: ");
            result = numbers.Skip(3);
            foreach (int i in result)
                Console.Write(i + "; ");
            Console.WriteLine();

            //skip take
            Console.Write("Skip 4: ");
            result = numbers.Skip(4).Take(3);
            foreach (int i in result)
                Console.Write(i + "; ");
            Console.WriteLine();


            //takewhile
            string[] teams = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };
            foreach (var t in teams.TakeWhile(x => x.StartsWith("Б")))
                Console.Write(t + "; ");
            Console.WriteLine();

            //skipwhile
            teams = new string[] { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };
            foreach (var t in teams.SkipWhile(x => x.StartsWith("Б")))
                Console.Write(t + "; ");
            Console.WriteLine();


            Console.WriteLine(new string('=', 30));
        }

        static void Eight()
        {
            Console.WriteLine("Eight result");
            Console.WriteLine(new string('=', 30));
            List<Phone> phones = new List<Phone>
            {
                new Phone {Name="Lumia 430", Company="Microsoft" },
                new Phone {Name="Mi 5", Company="Xiaomi" },
                new Phone {Name="LG G 3", Company="LG" },
                new Phone {Name="iPhone 5", Company="Apple" },
                new Phone {Name="Lumia 930", Company="Microsoft" },
                new Phone {Name="iPhone 6", Company="Apple" },
                new Phone {Name="Lumia 630", Company="Microsoft" },
                new Phone {Name="LG G 4", Company="LG" }
            };

            var phoneGroups = from phone in phones
                              group phone by phone.Company;

            foreach (IGrouping<string, Phone> g in phoneGroups)
            {
                Console.Write(g.Key + ": ");
                foreach (var t in g)
                    Console.Write(t.Name + "; ");
                Console.WriteLine();
            }

            var phoneGroups2 = from phone in phones
                               group phone by phone.Company into g
                               select new { Name = g.Key, Count = g.Count() };
            foreach (var group in phoneGroups2)
                Console.WriteLine("{0} : {1}", group.Name, group.Count);

            Console.WriteLine(new string('=', 30));
        }


        /// <summary>
        /// Ninth
        /// </summary>
        class Player
        {
            public string Name { get; set; }
            public string Team { get; set; }
        }
        class Team
        {
            public string Name { get; set; }
            public string Country { get; set; }
        }

        static void Ninth()
        {
            Console.WriteLine("Ninth result");
            Console.WriteLine(new string('=', 30));

            List<Team> teams = new List<Team>()
            {
                new Team { Name = "Бавария", Country ="Германия" },
                new Team { Name = "Барселона", Country ="Испания" },
                new Team { Name = "Бавария", Country ="Италия" },
                new Team { Name = "Барселона", Country ="Италия" }
            };
                        List<Player> players = new List<Player>()
            {
                new Player {Name="Месси", Team="Барселона"},
                new Player {Name="Неймар", Team="Барселона"},
                new Player {Name="Роббен", Team="Бавария"}
            };

            var result = from pl in players
                         join t in teams on pl.Team equals t.Name
                         select new { Name = pl.Name, Team = pl.Team, Country = t.Country };

            foreach (var item in result)
                Console.WriteLine("{0} - {1} ({2})", item.Name, item.Team, item.Country);

            Console.WriteLine(new string('-', 30));

            result = players.Join(teams, // второй набор
             p => p.Team, // свойство-селектор объекта из первого набора
             t => t.Name, // свойство-селектор объекта из второго набора
             (p, t) => new { Name = p.Name, Team = p.Team, Country = t.Country }); // результат

            foreach (var item in result)
                Console.WriteLine("{0} - {1} ({2})", item.Name, item.Team, item.Country);

            Console.WriteLine(new string('-', 30));


            var result2 = teams.GroupJoin(
                        players, // второй набор
                        t => t.Name, // свойство-селектор объекта из первого набора
                        pl => pl.Team, // свойство-селектор объекта из второго набора
                        (team, pls) => new  // результирующий объект
                        {
                            Name = team.Name,
                            Country = team.Country,
                            Players = pls.Select(p => p.Name)
                        });

            foreach (var team in result2)
            {
                Console.Write(team.Name + ":");
                foreach (string player in team.Players)
                {
                    Console.Write(player + "; ");
                }
                Console.WriteLine();
            }

            Console.WriteLine(new string('-', 30));

            var result3 = players.Zip(teams,
                          (player, team) => new
                          {
                              Name = player.Name,
                              Team = team.Name,
                              Country = team.Country
                          });
            foreach (var player in result3)
            {
                Console.WriteLine("{0} - {1} ({2})", player.Name, player.Team, player.Country);

                Console.WriteLine();
            }

            Console.WriteLine(new string('=', 30));
        }

        static void Tenth()
        {
            Console.WriteLine("Tenth result");
            Console.WriteLine(new string('=', 30));
            List<User> users = new List<User>()
                {
                    new User { Name = "Tom", Age = 23 },
                    new User { Name = "Sam", Age = 43 },
                    new User { Name = "Bill", Age = 35 }
                };

            bool result1 = users.All(u => u.Age > 20); // true
            if (result1)
                Console.WriteLine("У всех пользователей возраст больше 20");
            else
                Console.WriteLine("Есть пользователи с возрастом меньше 20");

            bool result2 = users.All(u => u.Name.StartsWith("T")); //false
            if (result2)
                Console.WriteLine("У всех пользователей имя начинается с T");
            else
                Console.WriteLine("Не у всех пользователей имя начинается с T");

            Console.WriteLine(new string('-', 30));

            result1 = users.Any(u => u.Age < 20); //false
            if (result1)
                Console.WriteLine("Есть пользователи с возрастом меньше 20");
            else
                Console.WriteLine("У всех пользователей возраст больше 20");

            result2 = users.Any(u => u.Name.StartsWith("T")); //true
            if (result2)
                Console.WriteLine("Есть пользователи, у которых имя начинается с T");
            else
                Console.WriteLine("Отсутствуют пользователи, у которых имя начинается с T");

            Console.WriteLine(new string('=', 30));
        }

        static void Eleven()
        {
            Console.WriteLine("Tenth result");
            Console.WriteLine(new string('=', 30));
            string[] teams = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };
            // определение LINQ-запроса
            var selectedTeams = from t in teams
                                where t.ToUpper().StartsWith("Б")
                                orderby t
                                select t;
            // выполнение запроса
            Console.WriteLine(selectedTeams.Count()); //3
            teams[1] = "Ювентус";
            // выполнение запроса
            Console.WriteLine(selectedTeams.Count()); //2

            Console.WriteLine(new string('=', 30));
        }

        static void Twelve()
        {
            Console.WriteLine("Tenth result");
            Console.WriteLine(new string('=', 30));
            int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };

            Func<int, bool> MoreThanTen = delegate(int i) { return i > 10; };

            var result = numbers.Where(MoreThanTen);

            foreach (int i in result)
                Console.WriteLine(i);


            Console.WriteLine(new string('-', 30));
            numbers = new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 };

            result = numbers.Where(i => i > 0).Select(Factorial);

            foreach (int i in result)
                Console.WriteLine(i);
            Console.WriteLine(new string('=', 30));
        }
        //alternative
        private static bool MoreThanTen(int i)
        {
            return i > 10;
        }
        static int Factorial(int x)
        {
            int result = 1;
            for (int i = 1; i <= x; i++)
                result *= i;
            return result;
        }
    }
}
