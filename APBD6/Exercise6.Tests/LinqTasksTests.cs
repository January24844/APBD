using Exercise6.Models;
using Xunit;

namespace Exercise6.Tests
{
    public class LinqTasksTests
    {
        #region Init
        public static IEnumerable<Emp> EmpsTest { get; set; } = new List<Emp>();
        public static IEnumerable<Dept> DeptsTest { get; set; } = new List<Dept>();

        public static int[] Array = { 1, 1, 1, 1, 1, 1, 10, 1, 1, 1, 1, 10, 2, 10, 10 };

        public LinqTasksTests()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();


            #region Load depts

            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            var d4 = new Dept
            {
                Deptno = 5,
                Dname = "Accounting",
                Loc = "Radom"
            };

            var d5 = new Dept
            {
                Deptno = 2137,
                Dname = "Testing",
                Loc = "Wadowice"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            deptsCol.Add(d4);
            deptsCol.Add(d5);

            DeptsTest = deptsCol;

            #endregion

            #region Load emps

            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-1),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            var e12 = new Emp
            {
                Deptno = 5,
                Empno = 100,
                Ename = "Marcin Marcinowski",
                HireDate = DateTime.Now.AddMonths(-10),
                Job = "KING",
                Mgr = null,
                Salary = 50000
            };

            var e11 = new Emp
            {
                Deptno = 5,
                Empno = 10,
                Ename = "Kunta Kinte",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Backend programmer",
                Mgr = e12,
                Salary = 2000
            };


            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            empsCol.Add(e11);
            empsCol.Add(e12);

            EmpsTest = empsCol;

            #endregion

            LinqTasks.Emps = EmpsTest;
            LinqTasks.Depts = DeptsTest;
        }

        #endregion

        /// <summary>
        ///     SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        [Fact]
        public void Task1()
        {
            //Act
            var res = LinqTasks.Task1().ToList();

            //Assert
            Assert.NotNull(res);
            Assert.NotEmpty(res);
            Assert.Equal(3, res.Count);
        }

        /// <summary>
        ///     SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        [Fact]
        public void Task2()
        {
            //Act
            var res = LinqTasks.Task2().ToList();

            //Assert
            Assert.NotNull(res);
            Assert.NotEmpty(res);
            Assert.Equal(3, res.Count);
            Assert.Equal("Anna Malewska", res.Last().Ename);
        }

        /// <summary>
        ///     SELECT MAX(Salary) FROM Emps;
        /// </summary>
        [Fact]
        public void Task3()
        {
            //Act
            var res = LinqTasks.Task3();

            //Assert
            Assert.True(res > 0);
            Assert.Equal(50000, res);
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        [Fact]
        public void Task4()
        {
            //Act
            var res = LinqTasks.Task4().ToList();

            //Assert
            Assert.NotNull(res);
            Assert.Single(res);
            Assert.Equal("KING", res.First().Job);
        }

        /// <summary>
        ///    SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        [Fact]
        public void Task5()
        {
            //Act
            var res = LinqTasks.Task5().ToList();

            //Assert
            Assert.NotNull(res);
            Assert.NotEmpty(res);
            Assert.Equal(12, res.Count);
            Assert.Equal(res.First().ToString(), new { Nazwisko = "Jan Kowalski", Praca = "Frontend programmer" }.ToString());
        }

        /// <summary>
        ///     SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        ///     INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        ///     Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        [Fact]
        public void Task6()
        {
            //Act
            var res = LinqTasks.Task6().ToList();

            //Assert
            Assert.NotNull(res);
            Assert.NotEmpty(res);
            Assert.Equal(res.Last().ToString(), new { Ename = "Marcin Marcinowski", Job = "KING", Dname = "Accounting" }.ToString());
        }

        /// <summary>
        ///     SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        [Fact]
        public void Task7()
        {
            //Act
            var res = LinqTasks.Task7().ToList();

            //Assert
            Assert.NotNull(res);
            Assert.NotEmpty(res);
            Assert.Equal(8, res.Count);
            Assert.Equal(res.First().ToString(), new { Praca = "Frontend programmer", LiczbaPracownikow = 3 }.ToString());
            Assert.Equal(res.Last().ToString(), new { Praca = "KING", LiczbaPracownikow = 1 }.ToString());
        }

        /// <summary>
        ///     Zwróć wartość "true" jeśli choć jeden
        ///     z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        [Fact]
        public void Task8()
        {
            //Act
            var res = LinqTasks.Task8();

            //Assert
            Assert.True(res);
        }

        /// <summary>
        ///     SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        ///     ORDER BY HireDate DESC;
        /// </summary>
        [Fact]
        public void Task9()
        {
            //Act
            var res = LinqTasks.Task9();

            //Assert
            Assert.NotNull(res);
            Assert.Equal("Jan Kowalski", res.Ename);
            Assert.Equal(1, res.Empno);
        }

        /// <summary>
        ///     SELECT Ename, Job, Hiredate FROM Emps
        ///     UNION
        ///     SELECT "Brak wartości", null, null;
        /// </summary>
        [Fact]
        public void Task10()
        {
            //Act
            var res = LinqTasks.Task10().ToList();

            //Assert
            Assert.NotNull(res);
            Assert.NotEmpty(res);
            Assert.Equal(13, res.Count());
            Assert.Equal(res.ElementAt(11).ToString(), new { Ename = "Marcin Marcinowski", Job = "KING", HireDate = EmpsTest.First(x => x.Job == "KING").HireDate}.ToString());
            Assert.Equal(res.Last().ToString(), new { Ename = "Brak wartości", Job = string.Empty, HireDate = (DateTime?)null }.ToString());
        }

        /// <summary>
        /// Wykorzystując LINQ pobierz pracowników podzielony na departamenty pamiętając, że:
        /// 1. Interesują nas tylko departamenty z liczbą pracowników powyżej 1
        /// 2. Chcemy zwrócić listę obiektów o następującej srukturze:
        ///    [
        ///      {name: "RESEARCH", numOfEmployees: 3},
        ///      {name: "SALES", numOfEmployees: 5},
        ///      ...
        ///    ]
        /// 3. Wykorzystaj typy anonimowe
        /// </summary>
        [Fact]
        public void Task11()
        {
            //Act
            var res = LinqTasks.Task11().ToList();

            //Assert
            Assert.NotNull(res);
            Assert.NotEmpty(res);
            Assert.Equal(res.Last().ToString()?.ToLower(), new { Name = "Accounting", NumOfEmployees = 2 }.ToString()?.ToLower());
        }

        /// <summary>
        /// Napisz własną metodę rozszerzeń, która pozwoli skompilować się poniższemu fragmentowi kodu.
        /// Metodę dodaj do klasy CustomExtensionMethods, która zdefiniowana jest poniżej.
        /// 
        /// Metoda powinna zwrócić tylko tych pracowników, którzy mają min. 1 bezpośredniego podwładnego.
        /// Pracownicy powinny w ramach kolekcji być posortowani po nazwisku (rosnąco) i pensji (malejąco).
        /// </summary>
        [Fact]
        public void Task12()
        {
            //Act
            var res = LinqTasks.Task12().ToList();

            //Assert
            Assert.NotNull(res);
            Assert.NotEmpty(res);

            Assert.Contains(res, x => x.Ename == "Marcin Marcinowski" && x.Empno == 100);

            Assert.Equal("Anna Malewska", res.First().Ename);
            Assert.Equal("Marcin Marcinowski", res.Last().Ename);
        }

        /// <summary>
        /// Poniższa metoda powinna zwracać pojedyczną liczbę int.
        /// Na wejściu przyjmujemy listę liczb całkowitych.
        /// Spróbuj z pomocą LINQ'a odnaleźć tę liczbę, które występuja w tablicy int'ów nieparzystą liczbę razy.
        /// Zakładamy, że zawsze będzie jedna taka liczba.
        /// Np: {1,1,1,1,1,1,10,1,1,1,1} => 10
        /// </summary>
        [Fact]
        public void Task13()
        {
            //Act
            var res = LinqTasks.Task13(Array);

            //Assert
            Assert.Equal(2, res);
        }

        /// <summary>
        /// Zwróć tylko te departamenty, które mają 5 pracowników lub nie mają pracowników w ogóle.
        /// Posortuj rezultat po nazwie departament rosnąco.
        /// </summary>
        [Fact]
        public void Task14()
        {
            var res = LinqTasks.Task14().ToList();

            Assert.NotNull(res);
            Assert.NotEmpty(res);
            Assert.Equal(2, res.Count());

            Assert.Contains(res, x => x.Dname == "Testing" && x.Deptno == 2137);
            Assert.Equal("Testing", res.Last().Dname);
        }
    }
}