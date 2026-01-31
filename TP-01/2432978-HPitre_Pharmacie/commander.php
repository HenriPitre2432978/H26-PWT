<?php
//Script PHP qui fait l'action de commander un produit sur le site.

require_once('includes/functions.php');

if (session_status() == PHP_SESSION_NONE) {
    session_start();
}

// Vérifier que l'utilisateur est connecté
if (empty($_SESSION['userId'])) {
    header("Location: login.php");
    exit();
}

// Récupérer et valider les données POST
$productId = isset($_POST['product_id']) ? (int)$_POST['product_id'] : 0;
$qty = isset($_POST['qty']) ? (int)$_POST['qty'] : 0;

if ($productId <= 0 || $qty <= 0) {
    // Revenir accueil si commande invalide
    header("Location: index.php");
    exit();
}

try {
    $pdo = GetMySQLinstance();
    $pdo->beginTransaction();

    // get details du medicament
    $stmt = $pdo->prepare("SELECT id, price, qty FROM pwt_tp1_drugs WHERE id = ?");
    $stmt->execute([$productId]);
    $product = $stmt->fetch();

    if (!$product) {
        //si introuvable
        $pdo->rollBack(); // annuler la requête
        die("Produit introuvable.");
    }

    //si stock invalide/incohérent (genre une commande en même temps ?? TODO tester )
    if ($qty > (int)$product['qty']) {
        $pdo->rollBack(); // annuler la requête
        die("Quantité demandée indisponible.");
    }

    $unitPrice = $product['price'];
    $totalPrice = $unitPrice * $qty;

    // INSERT dans la bd
    // id, userId, orderDate, price, isActive, productId
    $insertSql = "INSERT INTO pwt_tp1_orders (userId, orderDate, price, isActive, productId)
                  VALUES (:userId, NOW(), :price, 1, :productId)"; //isActive toujours = 1 car une commande qui vient d'être passée est toujours en cours

    $insertStmt = $pdo->prepare($insertSql);
    $insertStmt->execute([
        ':userId' => $_SESSION['userId'],
        ':price' => $totalPrice,
        ':productId' => $productId
    ]);

    // update la qté pour la drogue commandée
    $newQty = (int)$product['qty'] - $qty;
    $updateStmt = $pdo->prepare("UPDATE pwt_tp1_drugs SET qty = ? WHERE id = ?");
    $updateStmt->execute([$newQty, $productId]);

    // confirmer transweb
    $pdo->commit();

    // Redirection 
    header("Location: orders.php");
    exit();

} catch (PDOException $e) {
    if ($pdo && $pdo->inTransaction()) {
        $pdo->rollBack();
    }
    die("Erreur SQL : " . $e->getMessage());
}
?>
