<?php
//Factorybase.php: retourne les détails la connexion à la database SQL
abstract class FactoryBase
{
    protected function dbConnect()
    {
        $db = new \PDO('mysql:host=sql.decinfo-cchic.ca;port=33306;dbname=h26_web_2432978;charset=utf8', 'dev-2432978', 'DONNEZ-MOI 100% SVP');
        return $db;
    }
}
