<?php
ob_start();

// charge la classe CategoryFactory
require_once("dal/factories/InfoFactory.php");
require_once("dal/DAL.php");
$dal = new DAL();


?>

<div class="container mt-4">

<!-- Single-column Jumbotron -->
<div class="container-fluid bg-white text-black py-5 my-4 rounded">
  <div class="container text-center">
    <h1 class="display-4 fw-bold mb-4">Une expérience de jeux qui convient à l'ensemble de votre famille </h1>

    <!-- Continuous text -->
   <div class="lead text-center" style="max-width: 900px; margin: 0 auto;">
       <p>Que vous vouliez passer du temps à jouer avec vos enfants ou pour leur montrer comment jouer, les Paramètres famille sur Xbox One vous permettront de passer du temps en famille et de jouer en toute sécurité. </p>
    </div>
  
  </div>
</div>

<?php
// Get all categories alphabetically
$infos = $dal->InfoFact()->getAll();
include("includes/index-infosect.php");

$jeux = $dal->JeuFact()->getAll("titre"); //todo rendre dynamique titre/prix

?>

        <!--TODO:  ADD Linebreak-->


  <div class="row">

      <h1 class="display-10 mb-4 text-center">Les jeux populaires </h1>
      <p class="display-10 mb-4 text-center">Triés par : 
        <span>
        <a href=#>Titre</a>
        <!--TODO:  ADD Separator-->
        <a href=#>Prix</a> 
        </span>
        </p>

  </div>

</div>

<?php

$dal = new DAL();
include("includes/index-games.php");

?>


    
  </div>
</div>

<?php

$region_content = ob_get_clean();

require('includes/template.php');

?>