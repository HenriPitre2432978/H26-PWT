<?php
ob_start();

// Include DAL
require_once("dal/DAL.php");

$dal = new DAL();

// Determine selected game (si aucune, goto accueil)
if (isset($_GET['jeuId'])) {
        $selectedJeuId = intval($_GET['jeuId']);
        }
      else
        {
            header("../index.php");
            exit();
        } 
$jeu = $dal->JeuFact()->get($selectedJeuId);
$titre = $jeu->titre;
?>

<!-- PAGE HEADER -->

<div class="container">
<div class="row">


<div class="container">
    <div class="row">
    <!-- LEFT COLUMN -->
    <div class="col-6">
      <div class="container mt-5 pt-4 mb-5">
        <img src="../img/jeux/<?= $jeu->grandeimage ?>"
            class="img-fluid w-100 h-100 object-fit-cover"/>
        </div>  
    </div> 

    <!-- RIGHT COLUMN -->
    <div class="col mt-5 pt-5" >
         <h1 class="mb-4 text-start"><?=$jeu->titre?></h1>
          <p class="mb-2 text-start"><?=$jeu->description?></p>  
          <p class="mb-4 text-start"><?=$jeu->info?></p>  

          <h3 class="mb-4 text-start"><?=$jeu->prix?>$ CAD</h3>  
            
          <?=require_once("js/produit.js");?>

          <a class="mb-5 text-start" href=../index.php>Retourner à la page d'accueil</a>  
    </div>

    <div class="container mt-5 pt-4 mb-5">
        <img src="../img/xboxone.jpg"
            class="img-fluid w-100 h-100 object-fit-cover"/>
        </div>

<?php
$region_content = ob_get_clean();
require('includes/template.php');
?>