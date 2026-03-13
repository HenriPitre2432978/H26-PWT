<?php

class Jeu
{
    public $id;
    public $titre;
    public $petiteimage;
    public $grandeimage;
    public $info;
    public $prix;
    public $description;

    public function __construct($sql_row)
    {
        if (isset($sql_row)) {
            $this->id = $sql_row["ID"];
            $this->titre = $sql_row["Titre"];
            $this->petiteimage = $sql_row["PetiteImage"];
            $this->grandeimage = $sql_row["GrandeImage"];
            $this->info = $sql_row["Info"];
            $this->prix = $sql_row["Prix"];
            $this->description = $sql_row["Description"];
        }
    }
}
