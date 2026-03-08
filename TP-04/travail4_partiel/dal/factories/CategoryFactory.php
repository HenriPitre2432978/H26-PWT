<?php
require_once(realpath(__DIR__ . '/base/FactoryBase.php'));
require_once(realpath(__DIR__ . '/../models/Category.php'));

class CategoryFactory extends FactoryBase
{
    public function getAll()
    {
        $db = $this->dbConnect();
        $stmt = $db->query("SELECT * FROM tp4_categories"); // adjust table name

        $categories = [];

        while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
            $categories[] = new Category($row);
        }

        return $categories;
    }
}