
using IMedXModels;
using IMedXModels.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMedXUtilities
{
    public class IMedXDBContext : DbContext
    {
        public IMedXDBContext()
        {

        }
        public IMedXDBContext(DbContextOptions<IMedXDBContext> options):base(options)
        {
            //ChangeTracker.LazyLoadingEnabled = true;
        }

       // public DbSet<Patient> Patients { get; set; }
        //public DbSet<PatientICD> PatientICDs { get; set; }
        //public DbSet<PatientNDC> PatientNDCs { get; set; }

        public DbSet<InputPatientICD> InputPatientICDs { get; set; }
        public DbSet<InputPatientNDC> InputPatientNDCs { get; set; }
        public DbSet<IMedXPatientData> IMedXPatientDatas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DBConnectify.DbConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Patient>().ToTable("Patients", "dbo").HasKey(p=>p.PAID);
            //modelBuilder.Entity<PatientICD>().ToTable("PatientICDs", "dbo").HasKey(ic=>ic.PAICDId);
            //modelBuilder.Entity<PatientNDC>().ToTable("PatientNDCs", "dbo").HasKey(nd=>nd.PANDCId);
            // modelBuilder.Entity<IMedXPatientData>().ToTable("IMedXPatientData", "dbo").HasKey(pd => pd.RowId);
            modelBuilder.Entity<IMedXPatientData>().ToTable("IMedXPatientData", "dbo").HasNoKey();
             modelBuilder.Entity<InputPatientICD>().ToTable("InputPatientICD", "dbo").HasNoKey(); ;
            modelBuilder.Entity<InputPatientNDC>().ToTable("InputPatientNDC", "dbo").HasNoKey();
        }
    }
}
