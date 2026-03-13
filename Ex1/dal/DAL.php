<?php
require_once(realpath(__DIR__ . '/factories/JeuFactory.php'));
require_once(realpath(__DIR__ . '/factories/InfoFactory.php'));

class DAL
{
    private $jeuFact = null;
    private $infoFact = null;

    public function JeuFact()
    {
        if ($this->jeuFact == null) {
            $this->jeuFact = new JeuFactory();
        }

        return $this->jeuFact;
    }

    public function InfoFact()
    {
        if ($this->infoFact == null) {
            $this->infoFact = new InfoFactory();
        }

        return $this->infoFact;
    }
}
