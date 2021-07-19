using back.Classe;
using back.dbContext;
using back.table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.dialogueBD
{
    public static class D_Pain
    {
        public static DataContext context;

        public static IQueryable Lister()
        {
            var listeReturn = from pain in context.pain
                              orderby pain.nomPain descending
                              select pain;

            return listeReturn;
        }

        public static IQueryable ListerParPage(int _numPage)
        {
            int idMin = 1;
            int idMax = 2;

            if (_numPage > 1)
                idMin = (idMin * _numPage) + 1;

            idMax *= _numPage;
            

            var liste = (from pain in context.pain
                         where pain.idPain >= idMin
                         orderby pain.idPain ascending
                         select new { pain.idPain, pain.nomPain }).Take(idMax);

            return liste;
        }

        public static IQueryable Rechercher(string _nomPainRechercher)
        {
            var liste = from pain in context.pain
                        where pain.nomPain.Contains(_nomPainRechercher)
                        orderby pain.nomPain descending
                        select new { pain.idPain, pain.nomPain };

            return liste;
        }

        public static void Modifier(Pain _pain)
        {
            _pain.nomPain = Protection.XSS(_pain.nomPain);
            context.pain.Update(_pain);
            context.SaveChanges();
        }

        public static void Supprimer(int _id)
        {
            var painDelete = (from pain in context.pain
                              where pain.idPain == _id
                              select pain).FirstOrDefault();

            context.pain.Remove(painDelete);
            context.SaveChanges();
        }

        public static void Ajouter(Pain[] _listePain)
        {
            foreach (Pain item in _listePain)
            {
                item.nomPain = Protection.XSS(item.nomPain);

                context.pain.Add(item);
            }

            context.SaveChanges();
        }
    }
}
