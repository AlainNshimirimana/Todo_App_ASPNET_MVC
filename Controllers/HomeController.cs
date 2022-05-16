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
        // ADD new Task
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
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return Redirect("http://localhost:5000/");
        }
        // UPDATE
        [HttpGet]
        public JsonResult PopulateForm(int id){
            var todo = GetById(id);
            return Json(todo);
        }
        internal Todo GetById(int id){
            Todo todo = new();
            using (var con = new SqliteConnection("Data Source=tododb.sqlite")){
                using (var cmd = con.CreateCommand()){
                    con.Open();
                    cmd.CommandText = $"SELECT * FROM todo WHERE Id = '{id}'";
                    
                    using (var reader = cmd.ExecuteReader()){
                        if (reader.HasRows){
                            reader.Read();
                            todo.Id = reader.GetInt32(0);
                            todo.Name = reader.GetString(1);
                        }
                        else{
                            return todo;
                        }
                    };
                }
            }
            return todo;
        }

        public RedirectResult Update(Todo todo)
        {
            using (SqliteConnection con = new SqliteConnection("Data Source=tododb.sqlite"))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = $"UPDATE todo SET Name = '{todo.Name}' WHERE Id = '{todo.Id}'";
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return Redirect("http://localhost:5000/");
            }
        }

        // DELETE
        [HttpPost]
        public JsonResult Delete(int id){
            using (SqliteConnection con = new SqliteConnection("Data Source=tododb.sqlite")){
                using (var cmd = con.CreateCommand()){
                    con.Open();
                    cmd.CommandText = $"DELETE FROM todo WHERE Id = '{id}'";
                    cmd.ExecuteNonQuery();
                }
            }
            return Json(new{});
        }
    }
}
