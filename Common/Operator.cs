namespace Common
{
    public class Operator
    {
        public static Operator Equal = new Operator("Equals", "==");
        public static Operator NotEqual = new Operator("Equals", "!=");
        public static Operator Contains = new Operator("Contains", "in");
        public static Operator DoesNotContain = new Operator("Contains", "!in");
        public static Operator LessThan = new Operator("Less Than", "<");
        public static Operator LessThanEqual = new Operator("Less Than or Equal To", "<=");
        public static Operator GreaterThan = new Operator("Greater Than", ">");
        public static Operator GreaterThanEqual = new Operator("Greater Than or Equal To", ">=");
        public static Operator IsNull = new Operator("Is Null", "@");
        public static Operator IsNotNull = new Operator("Is Not Null", "!@");

        public string Name { get; }
        public string Token { get; }

        private Operator(string name, string token)
        {
            Name = name;
            Token = token;
        }

        public override int GetHashCode()
        {
            return (Name + ":" + Token).GetHashCode();
        }
    }
}
