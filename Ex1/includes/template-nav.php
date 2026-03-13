<?php
$currentPage = basename($_SERVER['PHP_SELF']);
?>

<!-- 
navbar-expand-md =
 navbar expand lorsque >= 768px (md), si <= alors hamburger).
data-bs-theme="dark" = adapte ppour fond foncé. equivalent de .navbar-dark en 5.3 (deprecated)
class="fixed-top" = fixe la navbar en haut de lecrna
-->
<nav class="navbar navbar-expand-md bg-dark border-bottom border-body fixed-top" data-bs-theme='dark'>

  <div class="container-fluid">

  <a class="navbar-brand" href="https://web.decinfo-cchic.ca/dev-2432978/s4/ex1/index.php">
        <span style="display: flex">
    <img src="img/favicon.ico" width="30" height="30" class="d-inline-block align-top" alt="">
      <h2 style="font-family: 'Arial', cursive, sans-serif; color: #094d03; margin-bottom: 20px;">XBOX</h2>
      <h2 style="font-family: 'Arial', cursive, sans-serif; color: #ffffff; margin-bottom: 20px;">&nbspONE</h2>
    </span>
  </a>
  </div>
</nav>