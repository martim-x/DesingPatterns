public interface I1
{
    string PrintName();
}

public interface I2
{
    string PrintName();
}

public class Human : I1, I2
{
    public Human() { }

    // Явная реализация I1
    string I1.PrintName()
    {
        return "Implementation for I1";
    }

    // Явная реализация I2
    string I2.PrintName()
    {
        return "Implementation for I2";
    }
}
