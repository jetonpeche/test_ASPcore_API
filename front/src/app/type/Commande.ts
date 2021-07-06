export type Commande =
{
    idCommande: string,
    dateLivraisonCommande: Date,
    adresseUtilisateur: string,
    utilisateur: 
    {
        nomUtilisateur: string,
        prenomUtilisateur: string,
        mailUtilisateur: string
    },
    liste:
    [
        {
            idPain: string,
            nomPain: string,
            qte: string
        }
    ]
}