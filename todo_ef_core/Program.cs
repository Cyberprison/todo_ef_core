using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_ef_core
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (AppContext context = new AppContext())
            //{
            //    context.Add(new TodoItem(){TodoBody = "My First Todo"});

            //    context.SaveChanges();
            //};

            //using (AppContext context = new AppContext())
            //{
            //    var list = context.TodoItems.ToList();

            //    foreach (var item in list)
            //    {
            //        Console.WriteLine($"{item.Id} | {item.TodoBody}");
            //    }
            //}

            bool flagInfiniteCycle = true;
            int choiceVar;

            while(flagInfiniteCycle)
            {
                Console.Clear();

                Console.WriteLine("1 - добавить запись в начало");
                Console.WriteLine("2 - вывести все записи");
                Console.WriteLine("3 - обновить первую запись");
                Console.WriteLine("4 - удалить первую запись");
                Console.WriteLine("5 - выход");
                Console.WriteLine();

                Console.Write("Выберите пункт меню: ");
                choiceVar = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                switch (choiceVar)
                {
                    case 1:
                        {
                            using (AppContext db = new AppContext())
                            {
                                Console.WriteLine("Введите заметку:");
                                string todoBody = Console.ReadLine();

                                TodoItem todoItem = new TodoItem()
                                {
                                    TodoBody = todoBody
                                };

                                db.TodoItems.Add(todoItem);
                                db.SaveChanges();
                            }
                            
                            Console.ReadLine();
                            break;
                        }
                    case 2:
                        {
                            using (AppContext db = new AppContext())
                            {
                                var todoItems = db.TodoItems.ToList();

                                foreach (var item in todoItems)
                                {
                                    Console.WriteLine($"{item.Id} | {item.TodoBody}");
                                }
                            }

                            Console.ReadLine();
                            break;
                        }
                    case 3:
                        {
                            using (AppContext db = new AppContext())
                            {
                                TodoItem todoItem = db.TodoItems.FirstOrDefault();

                                if (todoItem != null)
                                {
                                    Console.WriteLine("Измените текст заметки:");
                                    string todoBody = Console.ReadLine();

                                    todoItem.TodoBody = todoBody;

                                    db.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine("Список заметок пуст");
                                }
                            }

                            Console.ReadLine();
                            break;
                        }
                    case 4:
                        {
                            using (AppContext db = new AppContext())
                            {
                                TodoItem todoItem = db.TodoItems.FirstOrDefault();

                                if (todoItem != null)
                                {
                                    db.TodoItems.Remove(todoItem);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine("Список заметок пуст");
                                }
                            }

                            Console.ReadLine();
                            break;
                        }
                    case 5:
                        {
                            flagInfiniteCycle = false;
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Нету такого пункта меню");
                            Console.ReadLine();
                            break;
                        }
                }
            }

            Console.ReadLine();
        }
    }

    class TodoItem
    {
        public int Id { get; set; }
        public string TodoBody { get; set; }
    }

    class AppContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=TodoDB; Trusted_Connection=True;");
        }
    }
}
