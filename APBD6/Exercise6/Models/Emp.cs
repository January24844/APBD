namespace Exercise6.Models
{
    public class Emp
    {
        public int Empno { get; set; }
        public string Ename { get; set; } = string.Empty;
        public string Job { get; set; } = string.Empty;
        public int Salary { get; set; }
        public DateTime? HireDate { get; set; }
        public int? Deptno { get; set; }
        public Emp? Mgr { get; set; }

        public override string ToString()
        {
            return $"{Ename} ({Empno})";
        }
    }
}
