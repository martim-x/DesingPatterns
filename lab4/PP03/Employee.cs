using Lec03LibN;

namespace PP03
{
    public class Employee
    {
        public IBonus bonus { get; private set; }

        public Employee(IBonus bonus)
        {
            this.bonus = bonus;
        }

        public float calcBonus(float number_hours)
        {
            return bonus.calc(number_hours);
        }
    }
}


// ➜  lab4 git:(main) ✗ cd PP03
// ➜  PP03 git:(main) ✗ dotnet remove reference ../Lec03LibN/Lec03LibN.csproj
// ➜  PP03 git:(main) ✗ dotnet add reference ../Lec03LibN/Lec03LibN.csproj
