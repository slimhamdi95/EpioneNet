
using Epione.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epione.Data
{
   public class EpioneContext : IdentityDbContext<User>
    {
        public EpioneContext():base("DefaultConnection", throwIfV1Schema:false)//nom du bdd MaBase
        {
            Database.SetInitializer<EpioneContext>(new EpioneContextInitializer());
        }
        public static EpioneContext Create()
        {
            return new EpioneContext();
        }
        //les dbsets
        //public DbSet<User>user { get; set; }

        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Reclamation> Reclamation { get; set; }
    }
    public class EpioneContextInitializer : DropCreateDatabaseIfModelChanges<EpioneContext> //changement 
    {
        protected override void Seed(EpioneContext context)
        {
           


        }
    }

}
