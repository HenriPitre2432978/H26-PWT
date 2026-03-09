<?php
require_once("../dal/DAL.php");

$dal = new DAL();

// Get categories
$categories = $dal->CategoryFact()->getAll();

// Determine category actuelle
$catid = isset($_GET['catid']) ? intval($_GET['catid']) : $categories[0]->id;

// Get products from factory
$products = $dal->ProductFact()->getByCategory($catid);

// id to url converter array
$categoryImages = [
    1 => 'classic_cars.jpg',
    2 => 'motorcycles.jpg',
    3 => 'planes.jpg',
    4 => 'ships.jpg',
    5 => 'trains.jpg',
    6 => 'trucks.jpg',
    7 => 'vintage_cars.jpg'
];

// Banner image
$bannerImage = 'img-upload/' . ($categoryImages[$catid] ?? 'cars.jpg');

// if image doesn't exist
if (!file_exists('../' . $bannerImage)) {
    $bannerImage = 'img/cars.jpg';
}

ob_start();
?>

<!-- Desktop tableau -->
<div class="table-responsive d-none d-sm-block">
<table class="table table-striped table-bordered">
<thead>
<tr>
<th>Code</th>
<th>Produit</th>
<th class="text-end">Qté</th>
<th class="text-end">Prix</th>
</tr>
</thead>
<tbody>

<?php foreach ($products as $p) : ?>

<tr>
<td><?= htmlspecialchars($p->code) ?></td>
<td><?= htmlspecialchars($p->name) ?></td>
<td class="text-end"><?= intval($p->quantityInStock) ?></td>
<td class="text-end"><?= number_format($p->buyPrice,2) ?>$</td>
</tr>

<?php endforeach; ?>

</tbody>
</table>
</div>

<!-- Cards(sur mobile) -->
<div class="d-block d-sm-none">

<?php foreach ($products as $p) : ?>

<div class="card mb-2 p-2">
<strong><?= htmlspecialchars($p->name) ?></strong><br>
Code: <?= htmlspecialchars($p->code) ?><br>
Qté: <?= intval($p->quantityInStock) ?><br>
Prix: <?= number_format($p->buyPrice,2) ?>$
</div>

<?php endforeach; ?>

</div>

<?php

$response = [
    "bannerImage" => $bannerImage,
    "productsHtml" => ob_get_clean()
];

header("Content-Type: application/json"); //set response type as json 
echo json_encode($response); //convert to json 

?>