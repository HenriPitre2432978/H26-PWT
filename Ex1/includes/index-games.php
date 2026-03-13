<div class="container mt-4">
  <div class="row">
    <?php foreach($jeux as $jeu) { ?>
      <div class="col-lg-4 col-md-6 col-sm-12 mb-4">
        <div class="card h-100">          
          <img src="img/jeux/<?= $jeu->petiteimage ?>" class="card-img-top">

          <div class="card-body col-md-12 text-center">
            <h5 class="card-title"><?= $jeu->prix ?></h5>

            <a href="produit.php/?jeuId=<?= $jeu->id ?>">
            <button type="button" class="btn btn-primary">
              Plus de détails
              <i class="fa-solid fa-circle-info"></i> <!--TODO: Changer pour -> -->
            </button> 
            </a>
          </div>
        </div>
      </div>
      
    <?php } ?>
  </div>
</div>