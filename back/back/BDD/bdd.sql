DELETE FROM painCommande
DELETE FROM commande
DELETE FROM utilisateur
DELETE FROM pain

-- autoriser difinir la clé primaire
SET IDENTITY_INSERT utilisateur ON; 
SET IDENTITY_INSERT pain ON; 
SET IDENTITY_INSERT commande ON; 

INSERT INTO utilisateur (idUtilisateur, nomUtilisateur, prenomUtilisateur, adresseUtilisateur, mailUtilisateur, mdpUtilisateur) 
VALUES (1, 'Nicolas', 'Peyrachon', 'lyon', 'a@.com', 'mdp'), (2, 'Toto', 'Radis', 'Paris', 'b@b.com', 'mdp');

INSERT INTO pain (idPain, nomPain) VALUES (1, 'pain blanc'), (2, 'pain au seigle'), (3, 'pain aux noix');

-- commande utilisateur 1
INSERT INTO commande (idCommande, idUtilisateur, dateLivraisonCommande) VALUES (1, 1, convert(datetime, '2020-06-25 00:00:00', 20));
INSERT INTO painCommande (idCommande, idPain, qte) VALUES (1, 1, 20), (1, 2, 45);

-- commande utilisateur 2
INSERT INTO commande (idCommande, idUtilisateur, dateLivraisonCommande) VALUES (2, 2, convert(datetime, '2020-06-25 00:00:00', 20));
INSERT INTO painCommande (idCommande, idPain, qte) VALUES (2, 3, 1), (2, 1, 5);

SET IDENTITY_INSERT utilisateur OFF; 
SET IDENTITY_INSERT pain OFF; 
SET IDENTITY_INSERT commande OFF; 