<div class="container mt-4">
  <div class="row">
    <?php foreach($categories as $cat) { ?>
      <div class="col-lg-4 col-md-6 col-sm-12 mb-4">
        <div class="card h-100">
          <img src="img-upload/<?= $cat->image ?>" class="card-img-top">
          <div class="card-body">
            <h5 class="card-title"><?= $cat->name ?></h5>
            <p class="card-text"><?= $cat->description ?></p>
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modal<?= $cat->id ?>">
  <i class="fa-solid fa-circle-info"></i> En savoir plus
</button>
          </div>
        </div>
      </div>

        <!-- Modal pour chaque catégorie -->
      <div class="modal fade" id="modal<?= $cat->id ?>" tabindex="-1" aria-labelledby="modalLabel<?= $cat->id ?>" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="modalLabel<?= $cat->id ?>"><?= $cat->name ?></h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Fermer"></button>
            </div>
            <div class="modal-body">
              <img src="img-upload/<?= $cat->image ?>" class="img-fluid mb-3" alt="<?= $cat->name ?>">
              <p><?= nl2br($cat->description) ?></p>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Fermer</button>
            </div>
          </div>
        </div>
      </div>
      
    <?php } ?>
  </div>
</div>