using Newtonsoft.Json;
using NUnit.Framework;

namespace Common.Tests
{
    public class Test_Traits
    {
        [Test]
        public void Create1()
        {
            var definitionManager = new DefinitionManager();
            var traitManager = new TraitManager(definitionManager);

            var system = traitManager.Add("System", "Test");
            var definition = definitionManager.Add("Class");
            definition.AddRequirement(new Comparison(system.Definition, Operator.Equal, "Test"));
            var @class = traitManager.Add("Class", "Foo", definition);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None
            };
            var traitsJson = JsonConvert.SerializeObject(traitManager, Formatting.Indented, settings);
        }
    }
}
