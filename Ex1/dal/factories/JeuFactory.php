<?php
require_once(realpath(__DIR__ . '/base/FactoryBase.php'));
require_once(realpath(__DIR__ . '/../models/Jeu.php'));

class JeuFactory extends FactoryBase
{
    public function getAll($order)
    {
        $db = $this->dbConnect(); //connect using factorybase
        $stmt = $db->query("SELECT * FROM exintra1_jeux ORDER BY '$order' ASC");

        $jeux = [];

        while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
            $jeux[] = new Jeu($row);
        }

        return $jeux;
    }

    public function get($jeuId)
    {
        $db = $this->dbConnect(); //connect using factorybase

        // Prepare SQL 
        $stmt = $db->prepare("SELECT * FROM exintra1_jeux WHERE ID = :jeuid"); //TODO make orderby dynamic titre/prix
        $stmt->bindParam(':jeuid', $jeuId, PDO::PARAM_INT);

        // Execute and fetch
        $stmt->execute();
        $results = $stmt->fetchAll(PDO::FETCH_ASSOC);

        // Convert each row to a Product object
        $jeu;
        foreach ($results as $row) {
            $jeu = new Jeu($row);
        }

        return $jeu;
    }
}