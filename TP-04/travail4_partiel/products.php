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
$bannerImage = 'img-upload/' . $categoryImages[$selectedCategory->id];

// Optional: fallback if the file doesn't exist
if (!file_exists($bannerImage)) {
    $bannerImage = 'img/cars.jpg'; // default banner image
}
?>

<div class="container">
    <div class="row">
        <!-- Categories buttons -->
        <div class="col-lg-3 col-md-12 mb-3">

            <!-- Category banner image atop buttons -->
             <div class="mb-3">
        <img src="img-upload/<?= htmlspecialchars($categoryImages[$selectedCatId] ?? 'cars.jpg') ?>" 
             class="img-fluid rounded sidebar-banner" 
             alt="Category Banner">
    </div>


            <?php foreach ($categories as $cat) : ?>
                <button 
                    class="btn btn-outline-primary w-100 mb-2 category-btn <?= ($cat->id == $selectedCatId) ? 'active' : '' ?>" 
                    data-catid="<?= $cat->id ?>">
                    <?= htmlspecialchars($cat->name) ?>
                </button>
            <?php endforeach; ?>
        </div>

        <!-- Products table -->
        <div class="col-lg-9 col-md-12">
            <div id="products-container">
                <!-- AJAX will load products here -->
            </div>
        </div>
    </div>
</div>

<?php
$region_content = ob_get_clean();
require('includes/template.php');
?>