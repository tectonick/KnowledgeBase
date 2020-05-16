using KnowledgeBase.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.Repositories
{
    public class MyDbContext: IdentityDbContext<User>
    {


        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<DateModel> DateModels { get; set; }


    }
}
