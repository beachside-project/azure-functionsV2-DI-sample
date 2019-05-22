namespace FunctionApp31
{
    public class SampleConfig
    {
        public string EnvironmentName { get; set; }
        public string Name { get; set; }
        public Item[] Items { get; set; }
    }

    public class Item
    {
        public string Key { get; set; }
        public string Description { get; set; }
    }
}