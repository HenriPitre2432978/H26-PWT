<?php 
//page secondaire qui confirme la validité des informations entrées poiur se connecter au site

require_once('includes/functions.php');

if (session_status() == PHP_SESSION_NONE) {
    session_start();
}

try {
    $pdo = GetMySQLinstance();

    //Get values de lutilisateur from login.php
    $email = $_POST['email'];
    $pwd = $_POST['pwd'];

    //Get data from db
    $sql = "SELECT id, name, email FROM pwt_tp1_users WHERE email = ? AND pwd = ?";

    //envoyer requete
    $stmt = $pdo->prepare($sql);
    if ($stmt === false) {
        throw new Exception("Erreur Prepare ($sql)");
    }

    $stmt->execute([$email, $pwd]);

    //get la row correspondante
    $row = $stmt->fetch();

    //SI utilisateur trouvé, toute sacrer dans session et goto index.php
    if ($row) {
        $_SESSION['userId'] = $row['id'];
        $_SESSION['name'] = $row['name'];
        $_SESSION['email'] = $row['email'];
        $_SESSION['tryCount'] = 0;

        header("Location: index.php"); 
        exit();
    } 
    //Sinon, augmenter trycount et goto login once again
    else {
        $_SESSION['tryCount'] = ($_SESSION['tryCount'] ?? 0) + 1;
        header("Location: login.php");
        exit();
    }
}
catch (PDOException $e) {
    die("Erreur SQL : " . $e->getMessage());
}
