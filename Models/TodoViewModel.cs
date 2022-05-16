using System.Collections.Generic;

namespace Todo_App_ASPNET_MVC.Models
{
    public class TodoViewModel
    {
        public List<Todo> TodoList { get; set; }
        public Todo todo { get; set; }
    }
}
