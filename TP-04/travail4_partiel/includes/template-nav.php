<!-- 
navbar = active le composant navbar de Bootstrap.
navbar-expand-md = navbar expand lorsque >= 768px (md).
                           si <= :hamburger).
data-bs-theme="dark" = adapte ppour fond foncé. equivalent de .navbar-dark en 5.3 (deprecated)
bg-dark = https://getbootstrap.com/docs/5.3/utilities/background/
class="fixed-top" = fixe la navbar en haut de l’écran en permanence.
-->
<nav class="navbar navbar-expand-md bg-dark border-bottom border-body  fixed-top" data-bs-theme='dark'>

<!--Container qui prend toute la place possible (fluid)-->
  <div class="container-fluid">

    <!-- 
    <a> = lien.
    navbar-brand = style spécial Bootstrap pour le titre/logo du site.
    fw-bold = font-weight bold (texte en gras).
    href="#" = lien temporaire (ne mène nulle part).
    -->
    <a class="navbar-brand fw-bold" href="index.php">
      MODÈLES RÉDUITS
    </a>

    <!--     
    data-bs-toggle="collapse" 
        indique possibilité d'activer/désactiver un élément collapsed.
    
    data-bs-target="#mainNavbar"
        indique quel élément sera ouvert/fermé.
        doit match l’id du div collapsed plus bas.
    -->
    <button class="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#mainNavbar">
        <!--Icone de base du navbar lorsque toggled-->
      <span class="navbar-toggler-icon"></span>
    </button>

    <!-- 
    collapse =  contenu cachable.
    navbar-collapse = style spécial pour navbar.
    justify-content-end =  contenu à droite.
    
    id="mainNavbar"
        doit correspondre à data-bs-target cihaut.
    -->
    <div class="collapse navbar-collapse justify-content-end" id="mainNavbar">

      <!-- navbar-nav = active le style liste de navigation Bootstrap-->
      <ul class="navbar-nav">

        <li class="nav-item">

          <!--active = indique que c’est la page actuelle-->
          <a class="nav-link active" href="index.php">

            <!-- 
            fa-solid = style Font Awesome
            fa-house = icône 🏠
            me-1 = margin-end 0.25rem
            -->
            <i class="fa-solid fa-house me-1"></i>
            Accueil
          </a>
        </li>

        <li class="nav-item">
          <a class="nav-link" href="products.php">
            <i class="fa-solid fa-box me-1"></i>
            Nos produits
          </a>
        </li>

        <li class="nav-item">
          <a class="nav-link" href="contactus.php">
            <i class="fa-solid fa-envelope me-1"></i>
            Nous joindre
          </a>
        </li>

      </ul>
    </div>

  </div>
</nav>