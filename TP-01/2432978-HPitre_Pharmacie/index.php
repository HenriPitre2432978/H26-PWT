<head>
    <link rel="stylesheet" href="assets/site.css" /> <!-- Appliquer css-->
</head>
<?php 
$page_title = "Accueil";
require_once('includes/functions.php');

// Vérifie si la session est active
if (session_status() == PHP_SESSION_NONE) {
    session_start();
}

$pdo = GetMySQLinstance();

//  get all drugs available depuis la table de bd 
try {
    $sql = "SELECT id, name, link, price, qty FROM pwt_tp1_drugs ORDER BY qty DESC";
    $stmt = $pdo->query($sql);
    $products = $stmt->fetchAll();
} catch (PDOException $e) { //si erreur
    die("Erreur SQL : " . $e->getMessage());
}

//Start capturer le html
ob_start(); 
?>

<h2>Bienvenue sur notre pharmacie !</h2>
<p>Découvrez nos meilleurs médicaments et offres spéciales.</p>

<section id="catalogue">
    <?php foreach ($products as $p) : ?>
        <article class="product-card">

            <h3><?= htmlspecialchars($p['name']); ?></h3>

            <!-- Image(lien html) (si vider/errer, afficher asset sans image) -->
            <?php
                $imgSrc = !empty($p['link']) ? $p['link'] : 'assets/no-image.png';
            ?>
            <div> <!-- texte alternatif pour les aveugle sur le site-->
                <img src="<?= htmlspecialchars($imgSrc); ?>" alt="<?= htmlspecialchars($p['name']); ?>">
            </div>

            <!-- Prix et stock -->
            <p>
                <strong>Prix :</strong> <?= number_format($p['price'], 2, ',', ' '); ?> $<br>
                <strong>En stock :</strong> <?= (int)$p['qty']; ?>
            </p>

            <?php if (!empty($_SESSION['userId'])): ?>
                <!-- Formulaire de commande (juste si connecté) -->
                <form method="post" action="commander.php">
                    <input type="hidden" name="product_id" value="<?= (int)$p['id']; ?>">
                    <label for="qty_<?= (int)$p['id']; ?>">Quantité :</label>
                    <input
                        id="qty_<?= (int)$p['id']; ?>"
                        type="number"
                        name="qty"
                        value="1"
                        min="1"
                        max="<?= (int)$p['qty']; ?>"
                        style="width:72px;"
                        required>
                    <div style="margin-top:8px;">
                        <button type="submit" <?= ((int)$p['qty'] <= 0) ? 'disabled' : ''; ?>>Commander</button>
                    </div>
                </form>
            <?php else: ?>
                <!-- Message si non connecté -->
                <p>Connectez-vous pour commander.</p>
            <?php endif; ?>
        </article>
    <?php endforeach; ?>
</section>

<?php
$content = ob_get_clean(); 
RenderTemplate($content, $page_title);
?>
