DROP TABLE IF EXISTS exintra1_infos;
CREATE TABLE exintra1_infos
(
  ID integer AUTO_INCREMENT PRIMARY KEY,
  Titre nvarchar(100),
  Image nvarchar(100),
  Info nvarchar(200)
);

DROP TABLE IF EXISTS exintra1_jeux;
CREATE TABLE exintra1_jeux
(
  ID integer AUTO_INCREMENT PRIMARY KEY,
  Titre nvarchar(100),
  PetiteImage nvarchar(100),
  GrandeImage nvarchar(100),
  Info nvarchar(200),
  Prix decimal(10,2),
  Description nvarchar(1000)
);

INSERT INTO exintra1_infos(Titre, Image, Info)
VALUES 
 ("Confidentialité", "exp_confidentialite.jpg", "Sélectionnez les informations personnelles que les autres peuvent voir et qui peut interagir avec vos enfants."),
 ("Gestion du temps", "exp_temps.jpg", "Configurez les limites de temps passé devant l'écran pour chaque enfant. Visualisez les rapports d'activité pour effectuer le suivi."),
 ("Filtres de contenu", "exp_contenu.jpg", "Placez un filtre selon l’âge de votre enfant afin qu’il puisse avoir accès uniquement à du contenu approprié."),
 ("Limites d'achat", "exp_achat.jpg", "Les options disponibles comprennent l’approbation d'achat avant de les effectuer, la réception d'alarmes après chaque achat.");
 
INSERT INTO exintra1_jeux(Titre, PetiteImage, GrandeImage, Info, Prix, Description)
VALUES 
 ("Age of Wonders: Planetfall", "ageofwonders_planetfall.jpg", "ageofwonders_planetfall_full.jpg", "Multijoueur en ligne (2-8)", 49.5, "The Star Union: Autrefois un vaste empire connectant des milliers de mondes, son peuple a été laissé isolé et échoué à la suite de l'Effondrement. Des centaines d'années plus tard, la séparation a transformé les frères et sœurs en espèces étrangères différentes - des factions indépendantes qui entreprennent de reconstruire le monde comme elles l'entendent."),
 ("Monster Jam Steel Titans", "monsterjam_steeltitans.jpg", "monsterjam_steeltitans_full.jpg", "Multijoueur local (2)", 39.5, "De véritables camions. Une action plus vraie que nature. Monster Jam ! Découvrez le monde du Monster Jam avec Monster Jam Steel Titans !"),
 ("Warhammer: Chaosbane", "warhammer_chaosbane.jpg", "warhammer_chaosbane_full.jpg", "Multijoueur en ligne (2-4)", 90.5, "Dans un monde ravagé par les guerres et dominé par la magie, vous êtes le dernier espoir de l’Empire des Hommes face aux hordes du Chaos. Seul ou jusqu’à 4 en coop locale ou en ligne, choisissez votre héros parmi 4 classes de personnages aux compétences spécifiques et complémentaires, et préparez-vous à des combats épiques équipé des artefacts les plus puissants du Vieux Monde."),
 ("Final Fantasy XII: The Zodiac Age", "finalfantasyxii_thezodiacage.jpg", "finalfantasyxii_thezodiacage_full.jpg", "", 59.5, "Cette remastérisation en HD reprend le 12e opus de la franchise FINAL FANTASY qui s'est écoulé à plus de 6 millions d'exemplaires, et propose désormais des mécaniques de jeu réinventées !"),
 ("Mortal Kombat 11", "mortalkombat11.jpg", "mortalkombat11_full.jpg", "Multijoueur en ligne (2-8)", 79.5, "Mortal Kombat est de retour ! Cette suite de la célèbre franchise est bien meilleure que les précédentes."),
 ("World War Z", "wordlwarz.jpg", "wordlwarz_full.jpg", "Multijoueur en ligne (2-8)", 53.5, "World War Z est un jeu de tir coopératif palpitant, où jusqu'à 4 joueurs unissent leurs forces pour survivre à de gigantesques hordes de zombies dans des scènes d’action à couper le souffle."),
 ("Phoenix Wright: Ace Attorney Trilogy", "phoenixwright_aceattornettrilogy.jpg", "phoenixwright_aceattornettrilogy_full.jpg", "", 39.5, "Incarnez Phoenix Wright et succombez au frisson du combat judiciaire alors que vous luttez pour prouver l'innocence de vos clients au tribunal. Cette collection somptueuse rassemble les 14 épisodes des trois premiers jeux."),
 ("AngerForce: Reloaded", "angerforce_relaoaded.jpg", "angerforce_relaoaded_full.jpg", "Multijoueur local Xbox live (2)", 9.5, "AngerForce: Reloaded est un shoot'em up défilant verticalement bourré d'action qui rend hommage aux classiques des arcades des années 90. Cette expérience à haut indice d'octane se déroule dans le contexte d'un monde humain du 19ème siècle qui a vu éclater une rébellion de robot."),
 ("Operencia: The Stolen Sun", "operencia_thestolensun.jpg", "operencia_thestolensun_full.jpg", "Joueur unique", 38.5, "Operencia : The Stolen Sun comprend tout ce dont vous appréciez des classiques Dungeon crawler vue à la première personne, avec une amélioration de l’expérience RPG tour par tour avec des sensibilités modernes typiques de la vieille école . Rassemblez votre équipe de personnages mémorables et guidez-les dans un monde inspiré de la mythologie d'Europe centrale."),
 ("Windscape", "windscape.jpg", "windscape_full.jpg", "Joueur unique", 25.5, "Windscape est un jeu d'action et d'aventure à la première personne, qui se déroule dans un univers composé d'îles flottantes dans le ciel. En allant à la découverte de cet univers, vous apprenez que quelque chose a mal tourné : les îles s'effondrent et tombent du ciel ! C'est à vous que revient la tâche de découvrir quel danger menace l'univers de Windscape."),
 ("Power Rangers: Battle for the Grid", "powerrangers_battleforthegrid.jpg", "powerrangers_battleforthegrid_full.jpg", "Multijoueur en ligne Xbox live (2)", 25.5, "Des générations de Power Rangers entrent en collision à travers les 25 années de l'histoire du multivers. Vivez comme jamais auparavant des scènes de combat authentiques et réinventées."),
 ("Aca Neogeo: Baseball Stars 2", "acaneogea_baseballstars2.jpg", "acaneogea_baseballstars2_full.jpg", "Multijoueur local Xbox live (2)", 10.5, "Obtenez l'expérience complète avec des graphismes intenses et des annonceurs passionnés! Il est temps de voir du baseball excitant! Utilisez le Power-bat pour envoyer la balle voler pour une course à domicile incroyable! Le système Auto-Fielding permet même aux débutants de profiter du gameplay intense.");

