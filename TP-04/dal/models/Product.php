<?php

class Product
{
    public $id;
    public $code;
    public $name;
    public $categoryId;
    public $scale;
    public $vendor;
    public $description;
    public $quantityInStock;
    public $buyPrice;
    public $msrp;

    public function __construct($sql_row)
    {
        if (isset($sql_row)) {
            $this->id = $sql_row["Id"];
            $this->code = $sql_row["Code"];
            $this->name = $sql_row["Name"];
            $this->categoryId = $sql_row["CategoryId"];
            $this->scale = $sql_row["Scale"];
            $this->vendor = $sql_row["Vendor"];
            $this->description = $sql_row["Description"];
            $this->quantityInStock = $sql_row["QuantityInStock"];
            $this->buyPrice = $sql_row["BuyPrice"];
            $this->msrp = $sql_row["MSRP"];
        }
    }
}
