<head>
    <link rel="stylesheet" href="assets/site.css" />
</head>
<?php
//•	Centralisation des fonctions vers un fichier « functions.php » : 
// Ce fichier contiendra les fonctions qui se réutilisent d’une page à l’autre.
//  Ce qui peut permettre de diminuer la quantité de code PHP dans vos différentes pages.

function GetUsernameSession() {
	if (isset($_SESSION["userId"])) {
		return $_SESSION["userId"];
	}
	else {
		return "ERREUR: SESSION USERNAME NOT FOUND";
		exit();
	}
}

//Formatter et "instancier" le gabarit de page issu de template-site.php  
function RenderTemplate($content, $page_title)
{
    // Get header et le menu de navigation
    require 'includes/header.php';
    require 'includes/menu.php';

    // Print le main
    echo "<main>";
    echo $content;
    echo "</main>";

    //Get Footer
    require 'includes/footer.php';
}

function GetMySQLinstance() {
//Ce code crée la connexion avec SQL en façon PDO, et la retourne pour l'utiliser

// Informations de connexion à la base de données
$host = "sql.decinfo-cchic.ca";      // Adresse du serveur MySQL
$port = "33306";
$dbname = "a25_c50_2432978";      // Nom de ta base de données
$username = "dev-2432978";       // Nom d'utilisateur MySQL
$password = "DONNEZ-MOI 100% SVP";           // Mot de passe MySQL

$dsn = "mysql:host=$host;port=$port;dbname=$dbname;charset=utf8"; 
//requis pour sécurité ?? (SQLSTATE[HY000] [3159])

try {
    // Connexion avec PDO (SSL requis par le serveur MySQL)
    $conn = new PDO(
        $dsn,
        $username,
        $password,
        [
            // Force l'utilisation d'une connexion sécurisée (SSL)
            PDO::MYSQL_ATTR_SSL_CA => null,

            // Enlever verification du certificat du site par mysql
            PDO::MYSQL_ATTR_SSL_VERIFY_SERVER_CERT => false
        ]
    );
    
    // Définir le mode d'erreur de PDO sur Exception
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    
    // Optionnel : activer le mode FETCH_ASSOC par défaut
    $conn->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_ASSOC);
    
    // Connexion réussie (tu peux commenter cette ligne en prod)
    // echo "Connexion réussie !";

    return $conn;

} catch (PDOException $e) {
    // En cas d'erreur, afficher le message
    die("Erreur de connexion : " . $e->getMessage());
}}?>
