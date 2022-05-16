using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Todo_App_ASPNET_MVC.Models;
using Microsoft.Data.Sqlite;

namespace Todo_App_ASPNET_MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var todoViewModel = GetAllTodos();
            return View(todoViewModel);
        }
        // Get Todo items from the database
        internal TodoViewModel GetAllTodos()
        {
            List<Todo> todos = new List<Todo>();
            using (SqliteConnection con = new SqliteConnection("Data Source=tododb.sqlite"))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = $"SELECT * FROM todo";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                todos.Add(new Todo
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                });
                            }
                        }
                        else
                        {
                            return new TodoViewModel
                            {
                                TodoList = todos
                            };
                        }
                    };
                }
            }
            return new TodoViewModel
            {
                TodoList = todos
            };

        }
        // Add a Task
        public RedirectResult CreateTask(Todo todo)
        {
            using (SqliteConnection con = new SqliteConnection("Data Source=tododb.sqlite"))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = $"INSERT INTO todo (name) VALUES ('{todo.Name}')";
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            return Redirect("Index");
        }
    }
}
