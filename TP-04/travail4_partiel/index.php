<?php
ob_start();

// charge la classe CategoryFactory
require_once("dal/factories/categoryfactory.php");
require_once("dal/DAL.php");


?>

<?php
include("includes/index-carousel.php");
?>

<div class="container mt-4">

  <div class="row">
    <!-- Texte + image (formatté) -->
    <div class="col-lg-8 col-md-12">
      <h2 style="font-family: 'Comic Sans MS', cursive, sans-serif; color: #ff4500; margin-bottom: 20px;">
        🚗 Fun Fact About Cars Figurines!
      </h2>
    <p>
        Did you know that Disney <strong>Cars</strong> figurines are more than just toys? Each die-cast figure is crafted with incredible attention to detail—down to tiny decals, paint jobs, and even facial expressions that match the movie characters perfectly!
      </p>
      <p>
        Fans of Lightning McQueen, Mater, and the whole Radiator Springs gang often discover hidden Easter eggs on limited edition models. Some McQueen figures, for example, have secret racing numbers or tiny logos referencing Pixar movies or real-world racing legends. 
      </p>
      <p>
        Collectors especially hunt for "chase" versions—rare variants that make the treasure hunt feel almost magical. Some original 2006 figurines now sell for hundreds of dollars online! So next time you grab a Cars toy, remember: it might have secrets and stories of its own waiting to be discovered. 🏁
      </p>
    </div>

    <div class="col-lg-4 col-md-12 text-center">
      <img src="img/cars.jpg" class="img-fluid rounded" style="max-width:450px;">
    </div>

  </div>

</div>

<?php

$dal = new DAL();
$categories = $dal->CategoryFact()->getAll(); // ✅ instance call

include("includes/index-cards.php");

?>

<?php
//chatgpt a fait le texte
$jumbotron_text = "
<p>Every <strong>Disney Cars</strong> figurine is crafted with incredible detail. From Lightning McQueen’s signature decals to Mater’s quirky expressions, each model is a miniature work of art. 🎨</p>
<p>Rare 'chase' versions and limited editions add thrill to every collector's hunt. Some early 2006 figurines are now valued at hundreds of dollars online! 💰</p>
<p>Whether you're a casual fan or a dedicated collector, our gallery showcases the best of Radiator Springs and beyond. Dive into our collection and find your favorite hero today! 🏁</p>
<p>Stay tuned for new arrivals, exclusive editions, and fun collector tips!</p>
";
?>

<!-- Single-column Jumbotron -->
<div class="container-fluid bg-primary text-white py-5 my-4 rounded">
  <div class="container text-center">
    <h1 class="display-4 fw-bold mb-4">Welcome to the Collectible Cars!</h1>

    <!-- Continuous text -->
   <div class="lead text-center" style="max-width: 900px; margin: 0 auto;">
      <?= $jumbotron_text ?>
    </div>
    
    <!-- Call-to-action button -->
    <a href="products.php" class="btn btn-light btn-lg mt-4">
      <i class="fa-solid fa-car"></i> Browse Products
    </a>
  </div>
</div>
    
  </div>
</div>

<?php

$region_content = ob_get_clean();

require('includes/template.php');

?>