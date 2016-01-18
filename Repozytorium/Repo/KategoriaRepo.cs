using Repozytorium.IRepo;
using Repozytorium.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Repozytorium.Repo
{
    public class KategoriaRepo : IKategoriaRepo
    {
        private readonly IOglContext _db;

        public KategoriaRepo(IOglContext db)
        {
        _db = db;
        }

        public IQueryable<Kategoria> PobierzKategorie()
        {
            int k = 0;
            foreach (var item in _db.Kategorie)
	        {
                if (k == 0) { item.ParentId = 0; k++;  continue; }
                else item.MainParent=1;
                   if (item.Id < 4)
                   {
                       item.ParentId = 1;
                   }
                   else
                       item.ParentId = 2;


                   if (k == 3) { item.ParentId = 0; k++; continue; }

                   k++;

                   
                 
	        }

            _db.SaveChanges();

            _db.Database.Log = message => Trace.WriteLine(message);
            var kategorie = _db.Kategorie.AsNoTracking();
            return kategorie;
        }
        public IQueryable<Ogloszenie> PobierzOgloszeniaZKategorii(int id)
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            var ogloszenia = from o in _db.Ogloszenia
                             join k in _db.Ogloszenie_Kategoria on o.Id equals k.Id
                             where k.KategoriaId == id
                             select o;
            return ogloszenia;
        }

    
        public string NazwaDlaKategorii(int id)
        {
            var nazwa = _db.Kategorie.Find(id).Nazwa;
            return nazwa;
        }
    }
}