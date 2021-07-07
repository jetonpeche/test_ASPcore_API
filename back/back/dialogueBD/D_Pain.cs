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
