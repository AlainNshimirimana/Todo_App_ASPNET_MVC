using System.Collections.Generic;
using Todo_App_ASPNET_MVC.Models;

namespace Todo_App_ASPNET_MVC.ViewModel
{
    public class TodoViewModel
    {
        public string Name { get; set; }
        public List<Todo> Todos { get; set; }
        public Todo Todo { get; set; }
    }
}
