using back.dbContext;
using back.model;
using back.table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace back.dialogueBD
{
    public static class D_Commande
    {
        public static DataContext context;

        public static IQueryable ListerTouteCommandes()
        {
            var listeCommande = from commande in context.commande

                                join uti in context.utilisateur
                                on commande.idUtilisateur equals uti.idUtilisateur

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
                                             where p.commande.idUtilisateur == uti.idUtilisateur
                                             select new { pain.idPain, pain.nomPain, p.qte }) as object
                                };

            return listeCommande;
        }

        public static IQueryable ListerCommandeUtilisateur(int _idUtilisateur)
        {
            IQueryable liste =  from commande in context.commande
                                join uti in context.utilisateur
                                on commande.idUtilisateur equals uti.idUtilisateur

                                where commande.idUtilisateur == _idUtilisateur
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
                                             where p.commande.idUtilisateur == uti.idUtilisateur
                                             select new { pain.idPain, pain.nomPain, p.qte }) as object
                                };

            return liste;
        }

        /// <summary>
        /// Liste toutes les commandes d'aujourd'hui
        /// </summary>
        /// <returns></returns>
        public static IQueryable ListerCommandeJour()
        {
            IQueryable liste = from commande in context.commande
                               join uti in context.utilisateur
                               on commande.idUtilisateur equals uti.idUtilisateur

                               where commande.dateLivraisonCommande == DateTime.Now
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
                                            where p.commande.idUtilisateur == uti.idUtilisateur
                                            select new { pain.idPain, pain.nomPain, p.qte }) as object
                               };

            return liste;
        }

        public static void AjouterCommande(CommandeModel _nouvelleCommande)
        {
            Commande _commande = new Commande();

            _commande.idUtilisateur = _nouvelleCommande.idUtilisateur;
            _commande.dateLivraisonCommande = _nouvelleCommande.dateLivraisonCommande;        

            context.commande.Add(_commande);

            context.SaveChanges();

            int _idCommande = (from commande in context.commande
                               orderby commande.idCommande ascending
                               select commande.idCommande).LastOrDefault();

            foreach (PainCommande item in _nouvelleCommande.listePain)
            {
                PainCommande painCommande = new PainCommande();

                painCommande.idCommande = _idCommande;
                painCommande.idPain = item.idPain;
                painCommande.qte = item.qte;

                context.painCommande.Add(painCommande);
            }

            context.SaveChanges();
        }

        public static void SupprimerCommande(int _id)
        {
            PainCommande[] painCommandeDelete = (from pc in context.painCommande
                                                  where pc.idCommande == _id
                                                  select pc).ToArray();

            Commande commandeDelete = (from c in context.commande
                                      where c.idCommande == _id
                                      select c).FirstOrDefault();

            context.painCommande.RemoveRange(painCommandeDelete);
            context.commande.Remove(commandeDelete);

            context.SaveChanges();
        }
    }
}
