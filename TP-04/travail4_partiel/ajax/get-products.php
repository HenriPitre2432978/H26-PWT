<?php
require_once("../dal/DAL.php");

$dal = new DAL();
$categories = $dal->CategoryFact()->getAll();

// Determine selected category
$catid = isset($_GET['catid']) ? intval($_GET['catid']) : $categories[0]->id;

// Get products for this category
$products = $dal->ProductFact()->getByCategory($catid);

// Map category IDs to image filenames
$categoryImages = [];
foreach ($categories as $cat) {
    switch (strtolower($cat->name)) {
        case 'classic cars':
            $categoryImages[$cat->id] = 'classic_cars.jpg';
            break;
        case 'motorcycles':
            $categoryImages[$cat->id] = 'motorcycles.jpg';
            break;
        case 'planes':
            $categoryImages[$cat->id] = 'planes.jpg';
            break;
        case 'ships':
            $categoryImages[$cat->id] = 'ships.jpg';
            break;
        case 'trains':
            $categoryImages[$cat->id] = 'trains.jpg';
            break;
        case 'trucks':
            $categoryImages[$cat->id] = 'trucks.jpg';
            break;
        case 'vintage cars':
            $categoryImages[$cat->id] = 'vintage_cars.jpg';
            break;
        default:
            $categoryImages[$cat->id] = 'cars.jpg';
    }
}

// Get banner image for this category
$bannerImage = isset($categoryImages[$catid]) ? 'img-upload/' . $categoryImages[$catid] : 'img/cars.jpg';

// Start output buffering for products HTML
ob_start();

// --- Desktop Table ---
echo '<div class="table-responsive d-none d-sm-block">';
echo '<table class="table table-striped table-bordered">';
echo '<thead><tr><th>Code</th><th>Produit</th><th class="text-end">Qté</th><th class="text-end">Prix</th></tr></thead>';
echo '<tbody>';
foreach ($products as $p) {
    echo '<tr>';
    echo '<td>' . htmlspecialchars($p->code) . '</td>';
    echo '<td>' . htmlspecialchars($p->name) . '</td>';
    echo '<td class="text-end">' . intval($p->quantityInStock) . '</td>';
    echo '<td class="text-end">' . number_format($p->buyPrice, 2) . '$</td>';
    echo '</tr>';
}
echo '</tbody>';
echo '</table>';
echo '</div>';

// --- Mobile Cards ---
echo '<div class="d-block d-sm-none">';
foreach ($products as $p) {
    echo '<div class="card mb-2 p-2">';
    echo '<strong>' . htmlspecialchars($p->name) . '</strong><br>';
    echo 'Code: ' . htmlspecialchars($p->code) . '<br>';
    echo 'Qté: ' . intval($p->quantityInStock) . '<br>';
    echo 'Prix: ' . number_format($p->buyPrice, 2) . '$';
    echo '</div>';
}
echo '</div>';

// Return JSON for AJAX
$response = [
    'bannerImage' => $bannerImage,
    'productsHtml' => ob_get_clean()
];

header('Content-Type: application/json');
echo json_encode($response);
?>