<div class="container mt-4">
  <div class="row">
    <?php foreach($infos as $info) { ?>
      <div class="col-lg-3 col-md-6 col-sm-12 mb-4">
        <div class="card h-100">          
          <img src="img/<?= $info->image ?>" class="card-img-top">

          <div class="card-body">
            
            <h5 class="card-title"><?= $info->titre ?></h5>
            <p class="card-text"><?= $info->info ?></p>
          </div>
        </div>
      </div>
      
    <?php } ?>
  </div>
</div>