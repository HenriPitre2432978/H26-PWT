<head>
    <link rel="stylesheet" href="assets/site.css" />
</head>
<?php
require_once('includes/functions.php');
$message = "";

// Vérifier si le formulaire est soumis
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $nom = htmlspecialchars($_POST['nom']);
    $email = htmlspecialchars($_POST['email']);
    $motdepasse = $_POST['motdepasse'];
    $motdepasse2 = $_POST['motdepasse2'];

    // Vérifier que les mots de passe correspondent
    if ($motdepasse === $motdepasse2) {
        $message = "<span style='color:green;'>Inscription réussie !</span><br>Nom : $nom<br>Email : $email";
       
        // Connexion à la base de données
try {
    $pdo = GetMySQLinstance();

    // Vérifier si existe deja
    $check = $pdo->prepare("SELECT id FROM pwt_tp1_users WHERE email = ?");
    $check->execute([$email]);
    if ($check->fetch()) {
        die("Erreur : Ce courriel est déjà utilisé. <a href='register.php'>Retour</a>");
    }

    // inserer nouvel utilisateur
    $sql = "INSERT INTO pwt_tp1_users (name, email, pwd)
            VALUES (:name, :email, :pwd)";
    $stmt = $pdo->prepare($sql);
    $stmt->execute([
        ':name' => $nom,
        ':email' => $email,
        ':pwd' => $motdepasse //todo password_hash... 
    ]);

    // Redirection après succès
    header("Location: login.php?registered=1");
    exit();

} catch (PDOException $e) {
    die("Erreur SQL : " . $e->getMessage());
}
        
    } else {
        $message = "<span style='color:red;'>Erreur : Les mots de passe ne correspondent pas !</span>";
    }
}
?>

<!DOCTYPE html>
<html lang="fr">
<head>
    <meta charset="UTF-8">
    <title>Formulaire Inscription</title>
</head>
<body>

<div class="form-container">
    <h2>Inscription</h2>

    <?php if($message != "") echo "<div class='message'>$message</div>"; ?>

    <form action="" method="post">
        <label for="nom">Nom :</label>
        <input type="text" id="nom" name="nom" required>

        <label for="email">Email :</label>
        <input type="email" id="email" name="email" required>

        <label for="motdepasse">Mot de passe :</label>
        <input type="password" id="motdepasse" name="motdepasse" required>

        <label for="motdepasse2">Vérification mot de passe :</label>
        <input type="password" id="motdepasse2" name="motdepasse2" required>

        <button type="submit">S'inscrire</button>
    </form>
</div>

</body>
</html>
