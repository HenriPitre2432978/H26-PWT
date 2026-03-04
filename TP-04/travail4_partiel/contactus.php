<?php
//session_start();

ob_start();
?>



<?php
$region_content = ob_get_clean();

require('includes/template.php');
?>
