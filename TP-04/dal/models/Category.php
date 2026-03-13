<?php

class Category
{
    public $id;
    public $name;
    public $description;
    public $image;

    public function __construct($sql_row)
    {
        if (isset($sql_row)) {
            $this->id = $sql_row["Id"];
            $this->name = $sql_row["Name"];
            $this->description = $sql_row["Description"];
            $this->image = $sql_row["Image"];
        }
    }
}
