document.addEventListener("DOMContentLoaded", function () {
    const colorSelect = document.getElementById("ColorId");
    if (colorSelect) {
        colorSelect.addEventListener("change", () => updateColorSelection(colorSelect));
    }
});

function updateColorSelection(selectElement) {
    const selectedColorId = selectElement.value;
    const shoeId = document.getElementById("ShoeIdHidden")?.value;

    if (!shoeId) return console.error("Error: No se encontro el elemento ShoeIdHidden");

    fetch(`/Customer/Home/GetPriceByColor?shoeId=${shoeId}&colorId=${selectedColorId}`)
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                updatePriceDisplay(data.price);
            } else {
                console.error("Error al obteniendo el precio:", data.message);
                setPriceFieldsToNA();
            }
        })
        .catch(error => {
            console.error("Error en la petición AJAX:", error);
            setPriceFieldsToNA();
        });
}

function updatePriceDisplay(price) {
    const formattedPrice = formatPrice(price);
    document.getElementById("ShoePrice").textContent = formattedPrice;
    document.getElementById("ShoeCashPriceDisplay").textContent = formatPrice(price * 0.9);
    document.getElementById("ShoePriceDisplay").textContent = formattedPrice;
}

function formatPrice(price) {
    return new Intl.NumberFormat('es-AR', { style: 'decimal', minimumFractionDigits: 2, maximumFractionDigits: 2 })
        .format(parseFloat(price));
}

function setPriceFieldsToNA() {
    document.getElementById("ShoePrice").textContent = "Precio no disponible";
    document.getElementById("ShoePriceDisplay").textContent = "Precio no disponible";
    document.getElementById("ShoeCashPriceDisplay").textContent = "Precio no disponible";
}