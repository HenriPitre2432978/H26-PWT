<?php
ob_start();

// Include DAL
require_once("dal/DAL.php");

$dal = new DAL();

// Get all categories alphabetically
$categories = $dal->CategoryFact()->getAll();

// Determine selected category (GET parameter or default to first)
$selectedCatId = isset($_GET['catid']) ? intval($_GET['catid']) : $categories[0]->id;

// Find the selected category object
$selectedCategory = null;
foreach ($categories as $cat) {
    if ($cat->id == $selectedCatId) {
        $selectedCategory = $cat;
        break;
    }
}

// Map category IDs to image filenames
$categoryImages = [
    1 => 'classic_cars.jpg',
    2 => 'motorcycles.jpg',
    3 => 'planes.jpg',
    4 => 'ships.jpg',
    5 => 'trains.jpg',
    6 => 'trucks.jpg',
    7 => 'vintage_cars.jpg'
];

// Get image for selected category by ID
$bannerImage = 'img-upload/'. $categoryImages[$selectedCategory->id];

// Optional: fallback if the file doesn't exist
if (!file_exists($bannerImage)) {
    $bannerImage = 'img/cars.jpg'; // default banner image
}
?>

<!-- PAGE HEADER -->
<div class="container mt-5 pt-4">

    <div class="bg-light text-center p-5 rounded mb-4">

        <img src='img/cars.jpg'
             class="img-fluid rounded mb-3"
             style="max-height:250px; object-fit:cover; width:100%;">

    </div>

    <h4 class="text-center mb-4">
        Nos produits classés par catégorie
    </h4>

</div>

<div class="container">
<div class="row">

<!-- LEFT COLUMN -->
<div class="col-lg-3 col-md-12 mb-3">

<!-- Category image -->
<div class="mb-3">
<img src="<?= $bannerImage ?>"
class="img-fluid rounded sidebar-banner">
</div>

<!-- Category buttons -->
<?php foreach ($categories as $cat) : ?>

<button 
class="btn btn-outline-primary w-100 mb-2 category-btn <?= ($cat->id == $selectedCatId) ? 'active' : '' ?>"
data-catid="<?= $cat->id ?>">

<?= htmlspecialchars($cat->name) ?>

</button>

<?php endforeach; ?>

</div>


<!-- RIGHT COLUMN -->
<div class="col-lg-9 col-md-12">

<div id="products-container">
<!-- AJAX loads products  -->
</div>

</div>

</div>
</div>

<?php
$region_content = ob_get_clean();
require('includes/template.php');
?>