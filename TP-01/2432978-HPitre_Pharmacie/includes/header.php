<?php 
if (isset($_SESSION['name'])){
$template_nomcomplet = $_SESSION['name'];
}
else {
    $template_nomcomplet = "Inconnu";
}
?>
<!DOCTYPE html>
<html lang="fr">
<head>
    <link rel="stylesheet" href="assets/site.css" />
    <meta charset="UTF-8">
    <title>SUPER-DROGUE</title>
</head>
<body>
<header>
    <h1>Super-Drogue | Pharmacie en ligne</h1>
</header>
<main>
