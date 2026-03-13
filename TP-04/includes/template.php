<?php
//$current_page = basename($_SERVER["PHP_SELF"]);
?>

<!doctype html>
<html lang="fr">
    <head>
        <title>Modèles réduits</title>
        <?php
        include("includes/template-meta.php");

        if (isset($region_links)) {
            echo $region_links;
        }
        ?>
    </head>

    <body>
        <?php
        include("includes/template-nav.php");
        ?>

        <main>
            <?= $region_content; ?>
        </main>

        <?php
        include("includes/template-footer.php");
        include("includes/template-scripts.php");

        if (isset($region_scripts)) {
            echo $region_scripts;
        }
        ?>
    </body>
</html>