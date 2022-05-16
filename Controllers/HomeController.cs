using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Todo_App_ASPNET_MVC.Models;
using Microsoft.Data.Sqlite;

namespace Todo_App_ASPNET_MVC.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            var todoViewModel = GetAllTodos();
            return View(todoViewModel);
        }
        // Get Todo items from the database
        internal TodoViewModel GetAllTodos()
        {
            List<Todo> todos = new();
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
                                Todos = todos
                            };
                        }
                    };
                }
            }
            return new TodoViewModel
            {
                Todos = todos
            };

        }
        // Add a Task
        public IActionResult CreateTask(Todo newTask)
        {
            using (SqliteConnection con = new SqliteConnection("Data Source=tododb.sqlite"))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = $"INSERT INTO todo (name) VALUES ('{newTask.Name}')";
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
        // Delete Task
        /*[HttpGet]
        public IActionResult DeleteTask(int id)
        {
            var task = _db.Todos.Find(id);
            _db.Todos.Remove(task);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditTask(int id)
        {
            Todo todo = _db.Todos.Find(id);
            return View("UpdateTask", todo);
        }
        public IActionResult UpdateTasks(Todo newTask)
        {
            var task = _db.Todos.Where(t =>t.Id == newTask.Id).FirstOrDefault(); 
            task.Name = newTask.Name;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }*/
    }
}
