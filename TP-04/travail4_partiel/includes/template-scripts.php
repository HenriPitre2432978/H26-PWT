<!-- Placed at the end of the document so the pages load faster -->
<!-- jQuery first, then Popper.js, then Bootstrap JS -->

<!-- Insérer vos balises <script> ici -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<script>
$(document).ready(function() {
    function loadProducts(catid) {
        $("#products-container").html("<p>Loading...</p>");
        $.ajax({
            url: "ajax/get-products.php",
            method: "GET",
            data: { catid: catid },
            success: function(data) {
                // Update products HTML
                $("#products-container").html(data.productsHtml);

                // Update sidebar banner image
                $(".sidebar-banner").attr("src", data.bannerImage);
            },
            error: function() {
                $("#products-container").html("<p class='text-danger'>Error loading products.</p>");
            }
        });
    }

    // Initial load
    loadProducts(<?= $selectedCatId ?>);

    // Category button click
    $(".category-btn").click(function() {
        var catid = $(this).data("catid");

        // Update active button
        $(".category-btn").removeClass("active");
        $(this).addClass("active");

        // Load products + banner
        loadProducts(catid);
        history.replaceState(null, null, "?catid=" + catid);
    });
});
</script>

<!--BootstrapJS & POPPER-->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>