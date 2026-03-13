<?php

class Info
{
    public $id;
    public $titre;
    public $image;
    public $info;

    public function __construct($sql_row)
    {
        if (isset($sql_row)) {
            $this->id = $sql_row["ID"];
            $this->titre = $sql_row["Titre"];
            $this->image = $sql_row["Image"];
            $this->info = $sql_row["Info"];
        }
    }
}
