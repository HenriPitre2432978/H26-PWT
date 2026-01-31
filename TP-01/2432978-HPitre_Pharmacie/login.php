

<?php 
//Constants
require_once('includes/functions.php');
$page_title = "Connexion";
$triesTreshold = 3;

// Vérifie si la session est active
    if (session_status() == PHP_SESSION_NONE) {
        session_start();
    }

//Set tryCount if not set (3 essais maximum)
if (!isset($_SESSION['tryCount'])){
	$_SESSION['tryCount'] = 0;
}

$exceedsTries = $_SESSION['tryCount'] > $triesTreshold; //Si + que 3 essais = true
?>

<!DOCTYPE html> 
<html lang="fr">
	<head>
		<title>Identification</title>
		<meta charset="utf-8" />
		<link type="text/css" rel="stylesheet" href="assets/site.css"/>
	</head>
	<body>
		<div id="login">
			<?php
				if ($exceedsTries) { //si bloqué car trop d'essais
					echo '<p> Accès bloqué : trop de tentatives infructueuses.</p>'; 
					echo 	'<form method="post" action="">
								<button type="submit" name="restart">Recommencer!</button>
							</form>';
				}
				else { //Sinon try login:?>
				<form method="post" action="login-validation.php">
				<p class="title">Identification</p>
				<table>
					<tr>
						<td>Votre courriel :</td>
						<td><input type="text" name="email" value=""></td>
					</tr>
					<tr>
						<td>Votre mot de passe :</td>
						<td><input type="password" name="pwd" value=""></td>
					</tr>
					<tr>
						<td colspan="2" ><input id="btnLogin" type="submit" value="Connexion"></td>
					</tr>
					<tr>
    					<td colspan="2" class="invalidTry">
        					<?php
            					echo (isset($_SESSION['tryCount']) && $_SESSION['tryCount'] > 0 ) ? "Nombre d'essais : " . $_SESSION['tryCount'] : "";
       						 ?>
    					</td>
					</tr>
				</table>
			</form>
	
			<p>
    Pas encore de compte ?
    <a href="register.php">S'inscrire</a> <!-- Redirection pour s'inscire (register.php)-->
</p>
			<?php
			}?> <!--Close else statement-->
		</div>
	</body>
</html>