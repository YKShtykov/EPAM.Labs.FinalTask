namespace Proxy.Domain.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Proxy.Domain.Concrete.TaskContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Proxy.Domain.Concrete.TaskContext context)
        {
            User user1 = new User();
            user1.UserId = 1;
            context.Users.AddOrUpdate(user1);
            User user2 = new User();
            user2.UserId = 32;
            context.Users.AddOrUpdate(user2);
            ToDoTask task = new ToDoTask();
            task.Id = 1;
            task.IsCompleted = false;
            task.Create = true;
            task.Name = "Finish Lab";
            task.UserId = 1;
            context.Tasks.AddOrUpdate(task);
            context.SaveChanges();
        }
    }
}
