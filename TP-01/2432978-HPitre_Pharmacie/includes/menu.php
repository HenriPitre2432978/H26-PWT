        <link rel="stylesheet" href="assets/site.css" />
<nav>
    <span id="nav-links">
        <a href="index.php">Accueil</a> &nbsp;|&nbsp;
        <a href="orders.php">Commandes</a>

 <span id="nav-username" style="float:right;">
        <?php if (!empty($_SESSION['userId'])): ?>
            <?= htmlspecialchars($template_nomcomplet); ?>  <!-- Écrire nom de l'utilisateur connecté (voir header.php)-->
            <a href="logout.php">[Déconnexion]</a>

        <!-- Si pas connecté encore -->
        <?php else: ?> 
            <a href="login.php">[Connexion]</a>
        <?php endif; ?>
    </span>
</nav>
<nav>

    </nav>