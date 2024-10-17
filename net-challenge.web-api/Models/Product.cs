using net_challenge.web_api.Models.Base;

namespace net_challenge.web_api.Models
{
    public class Product : Audit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
