using System.Collections.Generic;

namespace Todo_App_ASPNET_MVC.Models
{
    public class TodoViewModel
    {
        public List<Todo> Todos { get; set; }
        public Todo todo { get; set; }
    }
}
