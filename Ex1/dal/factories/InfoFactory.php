<?php

require_once(realpath(__DIR__ . '/base/FactoryBase.php'));
require_once(realpath(__DIR__ . '/../models/Info.php'));

class InfoFactory extends FactoryBase
{
    public function getAll()
    {
        $db = $this->dbConnect(); //connect using factorybase
        $stmt = $db->query("SELECT * FROM exintra1_infos");

        $infos = [];

        while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
            $infos[] = new Info($row);
        }

        return $infos;
    }
}
