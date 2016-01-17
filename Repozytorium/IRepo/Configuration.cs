namespace Repozytorium.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Repozytorium.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repozytorium.Models.OglContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            
            
        }

        protected override void Seed(Repozytorium.Models.OglContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // Do debugowania metody seed
            // Zastosowanie tego kodu podczas startu metody Seed() uruchomi druga instancje VisualStudio,
            // w ktorej bedzie mozliwosc debugowania i sprawdzenia, jakie bledy zawiera metoda.

            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

             SeeedRoles(context);
             SeedUsers(context);
             SeedOgloszenia(context);
           // SeedKategorie(context);
           // SeedOgloszenie_Kategoria(context);
           // SeedAtrybut(context);
           // SeedAtrybutWartosc(context);
            //SeedKategoriaAtrybut(context);
        }

        private void SeedUsers(Models.OglContext context)
        {
            var store = new UserStore<Uzytkownik>(context);
            var manager = new UserManager<Uzytkownik>(store);
            if (!context.Users.Any(u => u.UserName == "Administrator"))
            {
                var user = new Uzytkownik { UserName = "Admin@asp.pl", Wiek=21 };
                var adminresult = manager.Create(user, "12345678");
                if (adminresult.Succeeded)
                {
                    manager.AddToRole(user.Id, "Admin");
                }
            }
            if(!context.Users.Any(u=>u.UserName=="Marek")){
                var user = new Uzytkownik {UserName="marek@asp.pl"};
                var adminresult = manager.Create(user,"1234Abc");
                if(adminresult.Succeeded){
                    manager.AddToRole(user.Id, "Pracownik");
                }

            }
            if(!context.Users.Any(u=>u.UserName=="Prezes")){
                var user = new Uzytkownik {UserName="prezes@asp.pl"};
                var adminresult = manager.Create(user,"1234Abc");
                if(adminresult.Succeeded){
                    manager.AddToRole(user.Id, "Admin");
                }
            }


        }
         //AddOrUpdate nie bedzie duplikowac danych przy kazdym wywolaniu metody Seed()
        private void SeedOgloszenia(Models.OglContext context)
        {
            var idUzytkownika = context.Set<Uzytkownik>().Where(u => u.UserName == "Admin").FirstOrDefault().Id;
            for (int i = 1; i < 10; i++)
            {
                var ogl = new Ogloszenie(){
                    Id = i,
                    UzytkownikId = idUzytkownika,
                    Tresc = "Treœæ og³oszenia" + i.ToString(),
                    Tytul = "Tytu³ og³oszenia" + i.ToString(),
                    DataDodania = DateTime.Now.AddDays(-i)
                };
                context.Set<Ogloszenie>().AddOrUpdate(ogl);
            }
            context.SaveChanges();            
        }

        private void SeedKategorie(Models.OglContext context)
        {
            for (int i = 1; i < 10; i++)
            {
                var kat = new Kategoria()
                {
                    Id = i,
                    Nazwa = "Nazwa kategorii" + i.ToString(),
                    Tresc = "Treœæ og³oszenia" + i.ToString(),
                    MetaTytul = "Tytu³ kategorii" + i.ToString(),
                    MetaOpis = "Opis kategorii" + i.ToString(),
                    MetaSlowa = "S³owa kluczowe do kategorii" + i.ToString(),
                    ParentId = i
                };
                context.Set<Kategoria>().AddOrUpdate(kat);
            }
            context.SaveChanges();
        }

        private void SeedOgloszenie_Kategoria(Models.OglContext context)
        {
            for (int i = 1; i < 10; i++)
            {
                var okat = new Ogloszenie_Kategoria()
                {
                    Id = i,
                    OgloszenieId = i ,
                    KategoriaId = i 
                };
                context.Set<Ogloszenie_Kategoria>().AddOrUpdate(okat);
            }
            context.SaveChanges();
        }

        private void SeeedRoles(Models.OglContext context)
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>
            (new RoleStore<IdentityRole>());

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Pracownik"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Pracownik";
                roleManager.Create(role);
            }
            
        }

        private void SeedAtrybut(Models.OglContext context)
        {
            for (int i = 1; i < 10; i++)
            {
                var atr = new Atrybut()
                {
                    Nazwa = "A"
                };
                context.Set<Atrybut>().AddOrUpdate(atr);
            }
            context.SaveChanges();
        }

        private void SeedKategoriaAtrybut(Models.OglContext context)
        {
            for (int i = 1; i < 10; i++)
            {
                var kat = new Kategoria_Atrybut()
                {
                    IdAtrybut = i,
                    IdKategoria = i

                };
                context.Set<Kategoria_Atrybut>().AddOrUpdate(kat);
            }
            context.SaveChanges();
        }

        private void SeedAtrybutWartosc(Models.OglContext context)
        {
            for (int i = 1; i < 10; i++)
            {
                var kat = new AtrybutWartosc()
                {
                    IdAtrybut = i,
                    Wartosc = "wartosc " + i.ToString()
                };
                context.Set<AtrybutWartosc>().AddOrUpdate(kat);
            }
            context.SaveChanges();
        }
    }
}
