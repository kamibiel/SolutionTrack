using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SolutionTrack.Entities;

namespace SolutionTrack.Context
{
    public class SolutionTrackContext : DbContext
    {
        public SolutionTrackContext(DbContextOptions<SolutionTrackContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios{get; set;}
    }
}