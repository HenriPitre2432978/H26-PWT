<head>
    <link rel="stylesheet" href="assets/site.css" />
</head>
<?php 
//Page pour afficher les commandes en cours et passées
$page_title = "Mes commandes";
require_once('includes/functions.php');

// Vérifie si la session est active
if (session_status() == PHP_SESSION_NONE) {
    session_start();
}

// Vérifier que l'utilisateur est connecté, sinon rediriger to login
if (empty($_SESSION['userId'])) {
    header("Location: login.php");
    exit();
}

try {
    $pdo = GetMySQLinstance();

    // get commandes en cours (isActive = 1)
    $sqlActive = "SELECT o.id, o.userId, o.orderDate, o.price, o.isActive, o.productId,
                         d.name AS drugName, d.link AS drugLink
                  FROM pwt_tp1_orders o
                  JOIN pwt_tp1_drugs d ON d.id = o.productId
                  WHERE o.userId = ? AND o.isActive = 1
                  ORDER BY o.orderDate DESC";
    $stmtActive = $pdo->prepare($sqlActive);
    $stmtActive->execute([$_SESSION['userId']]);
    $activeOrders = $stmtActive->fetchAll();

    // get commandes passées (isActive = 0)
    $sqlPast = "SELECT o.id, o.userId, o.orderDate, o.price, o.isActive, o.productId,
                       d.name AS drugName, d.link AS drugLink
                FROM pwt_tp1_orders o
                JOIN pwt_tp1_drugs d ON d.id = o.productId
                WHERE o.userId = ? AND o.isActive = 0
                ORDER BY o.orderDate DESC";
    $stmtPast = $pdo->prepare($sqlPast);
    $stmtPast->execute([$_SESSION['userId']]);
    $pastOrders = $stmtPast->fetchAll();

} catch (PDOException $e) {
    die("Erreur SQL : " . $e->getMessage());
}

//Start capturer le html
ob_start(); 
?>

<h2>Vos commandes</h2>

<h3>Commandes en cours</h3>

<?php if (empty($activeOrders)) : ?>
    <p>Aucune commande en cours.</p>
<?php else : ?>
    <table cellpadding="8" >
        <thead>
            <tr>
                <th>ID</th>
                <th>Produit</th>
                <th>Image</th>
                <th>Prix</th>
                <th>Date</th>
            </tr>
        </thead>
        <tbody>
        <?php foreach ($activeOrders as $order) : //Show le meme template de "carte" pour chaque medicament?>
            <tr>
                <td><?= (int)$order['id']; ?></td>
                <td><?= htmlspecialchars($order['drugName']); ?></td>
                <td>
                    <?php if (!empty($order['drugLink'])) : ?>
                        <img src="<?= htmlspecialchars($order['drugLink']); ?>" alt="<?= htmlspecialchars($order['drugName']); ?>" style="height:60px;">
                    <?php else : ?>
                        <span>Aucune image</span>
                    <?php endif; ?>
                </td>
                <td><?= number_format($order['price'], 2, ',', ' '); ?> $</td>
                <td><?= htmlspecialchars($order['orderDate']); ?></td>
            </tr>
        <?php endforeach; ?>
        </tbody>
    </table>
<?php endif; ?>

<hr>

<h3>Commandes passées</h3>

<?php if (empty($pastOrders)) : ?>
    <p>Aucune commande passée.</p>
<?php else : ?>
    <table cellpadding="8">
        <thead>
            <tr>
                <th>ID</th>
                <th>Produit</th>
                <th>Image</th>
                <th>Prix</th>
                <th>Date</th>
            </tr>
        </thead>
        <tbody>
        <?php foreach ($pastOrders as $order) : //Show le meme template de "carte" pour chaque medicament?>
            <tr>
                <td><?= (int)$order['id']; ?></td>
                <td><?= htmlspecialchars($order['drugName']); ?></td>
                <td>
                    <?php if (!empty($order['drugLink'])) : ?>
                        <img src="<?= htmlspecialchars($order['drugLink']); ?>" alt="<?= htmlspecialchars($order['drugName']); ?>" style="height:60px;">
                    <?php else : ?>
                        <span>Aucune image</span>
                    <?php endif; ?>
                </td>
                <td><?= number_format($order['price'], 2, ',', ' '); ?> $</td>
                <td><?= htmlspecialchars($order['orderDate']); ?></td>
            </tr>
        <?php endforeach; ?>
        </tbody>
    </table>
<?php endif; ?>

<?php
//stop la capture de html
$content = ob_get_clean(); 
RenderTemplate($content, $page_title);
?>
