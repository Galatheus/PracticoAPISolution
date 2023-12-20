﻿using Microsoft.EntityFrameworkCore;
using PracticoAPI.Models;

namespace Practico.Data
{
	public class ApplicationDbContext :DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

	    public DbSet<Instancia> Instancias { get; set; }
    }
    
}
