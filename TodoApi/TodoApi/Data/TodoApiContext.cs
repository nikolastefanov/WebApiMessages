﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class TodoApiContext : DbContext
    {

        public DbSet<TodoItem> TodoItem { get; set; }

        public TodoApiContext (DbContextOptions<TodoApiContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }



    }
}
