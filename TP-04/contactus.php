<?php
session_start();

ob_start();
?>
<!-- banniere en haut-->
<div class="d-flex justify-content-center my-4">
        <img src="img/teamicon.png" class="img-fluid w-30">
</div>

<div class="container mt-5 mb-5">

<?php
//Retour de contactus-send si message sent succesffully
if(isset($_SESSION['error'])){
    echo '<div class="alert alert-warning">'.$_SESSION['error'].'</div>';
    unset($_SESSION['error']);
}

if(isset($_SESSION['success'])){
    echo '<div class="alert alert-success">'.$_SESSION['success'].'</div>';
    unset($_SESSION['success']);
}
?>

<div class="row justify-content-center">

<div class="col-lg-6 col-md-8 col-sm-12">

<h2 class="mb-4 text-center">
<i class="fa-solid fa-envelope"></i> Nous joindre
</h2>

<form method="POST" action="contactus-send.php">

<div class="mb-3">
<label class="form-label">Nom</label>
<input type="text" 
class="form-control"
name="name"
value="<?php echo $_SESSION['name'] ?? ''; ?>">
</div>

<div class="mb-3">
<label class="form-label">
<i class="fa-solid fa-envelope"></i> Courriel
</label>
<input type="email"
class="form-control"
name="email"
value="<?php echo $_SESSION['email'] ?? ''; ?>">
</div>

<div class="mb-3">
<label class="form-label">Message</label>
<textarea class="form-control"
rows="5"
name="message"><?php echo $_SESSION['message'] ?? ''; ?></textarea>
</div>

<button class="btn btn-primary">
<i class="fa-solid fa-paper-plane"></i> Envoyer
</button>

</form>

</div>
</div>
</div>

<?php
$region_content = ob_get_clean();

require('includes/template.php');
?>
