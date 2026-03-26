namespace Services;

public partial class Math
{
    public class Builder
    {
        // Начальная точка цепочки — просто возвращает a
        private IMathWrapper result = new Result();

        public static Builder Create() => new Builder();

        private Builder() { }

        public Builder Sum(int y)
        {
            this.result = new Sum(y, this.result); // оборачиваем текущую цепочку
            return this;
        }

        public Builder Diff(int y)
        {
            this.result = new Diff(y, this.result);
            return this;
        }

        public Builder Mul(int y)
        {
            this.result = new Mul(y, this.result);
            return this;
        }

        public Builder Div(int y)
        {
            this.result = new Div(y, this.result);
            return this;
        }

        public Builder Mod(int y)
        {
            this.result = new Mod(y, this.result);
            return this;
        }

        public IMathWrapper Build() => this.result;
    }
}
