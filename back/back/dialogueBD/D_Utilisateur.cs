using back.Classe;
using back.dbContext;
using back.table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace back.dialogueBD
{
    public static class D_Utilisateur
    {
        public static DataContext context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_utilisateur"></param>
        /// <returns>true si on est incript. false si le mail existe déjà</returns>
        public static bool Incription(Utilisateur _utilisateur)
        {
            int nbMail = (from uti in context.utilisateur
                        where uti.mailUtilisateur == _utilisateur.mailUtilisateur
                        select uti.mailUtilisateur).Count();

            // le compte existe pas
            if(nbMail == 0)
            {
                _utilisateur.nomUtilisateur = Protection.XSS(_utilisateur.nomUtilisateur);
                _utilisateur.prenomUtilisateur = Protection.XSS(_utilisateur.prenomUtilisateur);
                _utilisateur.mailUtilisateur = Protection.XSS(_utilisateur.mailUtilisateur);
                _utilisateur.adresseUtilisateur = Protection.XSS(_utilisateur.adresseUtilisateur);

                _utilisateur.mdpUtilisateur = BC.HashPassword(_utilisateur.mdpUtilisateur);

                context.utilisateur.Add(_utilisateur);
                context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Connexion
        /// </summary>
        /// <param name="_utilisateur"></param>
        /// <returns>false si pas bon. Sinon renvoie des infos de l'utilisateur</returns>
        public static dynamic Connexion(Utilisateur _utilisateur)
        {
            var infos = (from uti in context.utilisateur
                        where uti.mailUtilisateur == _utilisateur.mailUtilisateur
                        select uti.mdpUtilisateur).FirstOrDefault();

            // compte existe
            if(infos != null)
            {
                // mdp OK
                if(BC.Verify(_utilisateur.mdpUtilisateur, infos))
                {
                    var infoReturn = (from uti in context.utilisateur
                                     where uti.mailUtilisateur == _utilisateur.mailUtilisateur
                                     select new { uti.idUtilisateur, uti.nomUtilisateur, uti.prenomUtilisateur }).FirstOrDefault();

                    return infoReturn;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  Supprime toutes les commandes de l'utilisateur et l'utilisateur
        /// </summary>
        /// <param name="_idUtilisateur"></param>
        public static void Supprimer(int _idUtilisateur)
        {
            List<int> listeIdCommande = (from c in context.commande
                                   where c.idUtilisateur == _idUtilisateur
                                   select c.idCommande).ToList<int>();

            foreach (int item in listeIdCommande)
            {
                PainCommande painCommande = (from pc in context.painCommande
                                         where pc.idCommande == item
                                         select pc).FirstOrDefault();

                if(painCommande != null)
                    context.painCommande.Remove(painCommande);
            }

            Utilisateur utilisateur = (from uti in context.utilisateur
                               where uti.idUtilisateur == _idUtilisateur
                               select uti).FirstOrDefault();

            context.utilisateur.Remove(utilisateur);

            context.SaveChanges();
        }
    }
}
