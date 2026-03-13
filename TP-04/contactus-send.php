<?php

//La classe PHPMAILER ne fonctionne pas pour une raison inconnue. Voici donc le résultat théorique sans test, 
// en me basant sur ma logique et la documentation phpmailer sur gituhb

session_start();

require __DIR__ . '/vendor/autoload.php';

use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\Exception;

$name = trim($_POST['name'] ?? '');
$email = trim($_POST['email'] ?? '');
$message = trim($_POST['message'] ?? '');

// Save détails dans session
$_SESSION['name'] = $name;
$_SESSION['email'] = $email;
$_SESSION['message'] = $message;

if ($name === '' || $email === '' || $message === '') {
    $_SESSION['error'] = "Tous les champs doivent être remplis.";
    header("Location: contactus.php");
    exit;
}

$mail = new PHPMailer(true);

//• Serveur SMTP : smtp.gmail.com
//• Chiffrement : TLS
//• Port : 587
//• Utilisateur : votreCompte@gmail.com
//• Mot de passe : Votre mot de passe
try {
    $mail->isSMTP();
    $mail->Host = 'smtp.gmail.com';
    $mail->SMTPAuth = true;
    $mail->Username = 'henripitre2006@gmail.com'; // Gmail
    $mail->Password = 'ytua wmcj shdz srag';
    $mail->SMTPSecure = 'tls';
    $mail->Port = 587;

    $mail->setFrom('henripitre2006@gmail.com', $name);
    $mail->addAddress('henripitre2006@gmail.com');

    $mail->isHTML(true);
    //Contenu du mail
    $mail->Subject = "Message reçu le site";
    $mail->Body = "
        <h3>Nouveau message</h3>
        <p><b>Nom:</b> {$name}</p>
        <p><b>Email:</b> {$email}</p>
        <p><b>Message:</b><br>{$message}</p>
    ";

    $mail->send();

    unset($_SESSION['name'], $_SESSION['email'], $_SESSION['message']);
    $_SESSION['success'] = "Votre message a été envoyé.";

} catch (Exception $e) {
    $_SESSION['error'] = "Erreur lors de l'envoi du message: " . $mail->ErrorInfo;
}

header("Location: contactus.php");
exit;