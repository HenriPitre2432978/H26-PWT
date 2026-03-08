<?php

require_once(realpath(__DIR__ . '/base/FactoryBase.php'));
require_once(realpath(__DIR__ . '/../models/Product.php'));

class ProductFactory extends FactoryBase
{
    // Get all products by category ID
    public function getByCategory($categoryId)
    {
        $db = $this->dbConnect();

        // Prepare SQL query
        $stmt = $db->prepare("SELECT * FROM tp4_products WHERE CategoryId = :catid ORDER BY Name ASC");
        $stmt->bindParam(':catid', $categoryId, PDO::PARAM_INT);

        // Execute and fetch
        $stmt->execute();
        $results = $stmt->fetchAll(PDO::FETCH_ASSOC);

        // Convert each row to a Product object
        $products = [];
        foreach ($results as $row) {
            $products[] = new Product($row);
        }

        return $products;
    }
}
