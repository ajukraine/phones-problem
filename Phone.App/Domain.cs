using Dahomey.Json.Attributes;

namespace Phone.App
{
    public abstract class APhone
    {
         public string Name { get; set; }

         public string Company { get; set; }

         public decimal Price { get; set; }
    }

    [JsonDiscriminator(nameof(MobilePhone))]
    public class MobilePhone : APhone
    {
        // For example, "green" or "red"
        public string Color { get; set; }

        // In GB
        public short MemorySize { get; set; }
    }

    [JsonDiscriminator(nameof(RadioPhone))]
    public class RadioPhone : APhone
    {
        // In meters
        public short Range { get; set; }

        public bool SupportsAutoAnswer { get; set; }
    }
}