using System.ComponentModel.DataAnnotations;

namespace Todo_App_ASPNET_MVC.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
