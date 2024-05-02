using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTest.Domains
{
    [Table("OlegTable")]
    public class Oleg
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
