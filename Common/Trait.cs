namespace Common
{
    public class Trait
    {
        public Definition Definition { get; }
        public string Value { get; set; }

        public Trait(Definition definition, string value = null)
        {
            Definition = definition;
            Value = value;
        }
    }
}
