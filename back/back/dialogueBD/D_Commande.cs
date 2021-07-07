using back.dbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.dialogueBD
{
    public static class D_Commande
    {
        public static DataContext context;

        public static IQueryable ListerTouteCommandes()
        {
            var listeCommande = from commande in context.commande

                                join uti in context.utilisateur
                                on commande.utilisateur.idUtilisateur equals uti.idUtilisateur

                                orderby commande.dateLivraisonCommande ascending
                                select new
                                {
                                    commande.idCommande,
                                    commande.dateLivraisonCommande,
                                    uti.adresseUtilisateur,

                                    // selection des infos de l'utilisateur dans des { }
                                    utilisateur = new { uti.nomUtilisateur, uti.prenomUtilisateur, uti.mailUtilisateur },

                                    // sous requete liste de des pains de la commande
                                    liste = (from p in context.painCommande
                                             join pain in context.pain
                                             on p.idPain equals pain.idPain
                                             where p.commande.utilisateur.idUtilisateur == uti.idUtilisateur
                                             select new { pain.idPain, pain.nomPain, p.qte }) as object
                                };

            return listeCommande;
        }

        public static void SupprimerCommande(int _id)
        {
            var painCommandeDelete = (from pc in context.painCommande
                                      where pc.idCommande == _id
                                      select pc).ToArray();

            var commandeDelete = (from c in context.commande
                                  where c.idCommande == _id
                                  select c).FirstOrDefault();

            context.painCommande.RemoveRange(painCommandeDelete);
            context.commande.Remove(commandeDelete);

            context.SaveChanges();
        }
    }
}
