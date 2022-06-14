using System.ComponentModel.DataAnnotations;

namespace SanTsgProject.Domain.Cities
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [MinLength(3)]
        public string Name { get; set; }
    }
}
