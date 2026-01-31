<?php
// Je n'ai pas trouvé de solution de mes propres capacités pour déconnecter l'utilisateur facilement sans passer
//  par un fichier php comme celui-ci, alors je le fais ainsi même si je suppose qu'il y a meilleur moyen de le faire.

if (session_status() == PHP_SESSION_NONE) {
    session_start();
}

// Détruire la session completement
session_unset();
session_destroy();

// Rediriger
header("Location: login.php");
exit();
?>