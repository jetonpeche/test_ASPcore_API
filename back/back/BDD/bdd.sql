create table pain
(
    idPain int AUTO_INCREMENT,
    nomPain varchar(50),

    primary key (idPain)
);

create table utilisateur
(
    idUtilisateur int AUTO_INCREMENT,
    nomUtilisateur varchar(100),
    telUtilisateur char(10),
    mailUtilisateur varchar(100),

    primary key (idUtilisateur)
);

create table commande
(
    idCommande int AUTO_INCREMENT,
    idUtilisateur int not null,

    dateCommande datetime,

    primary key(idCommande),

    foreign key (idUtilisateur) references utilisateur (idUtilisateur)
);

create table pain_commande
(
    idCommande int not null,
    idPain int not null,

    qte int not null,

    primary key (idCommande, idPain),

    foreign key (idCommande) references commande (idCommande),
    foreign key (idPain) references pain (idPain)
);