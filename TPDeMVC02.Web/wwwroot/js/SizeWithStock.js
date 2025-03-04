function updateStock() {
    const sizeSelect = document.getElementById("SizeId");
    const selectedOption = sizeSelect.options[sizeSelect.selectedIndex]; 
    const availableStock = selectedOption.getAttribute("data-stock") || "0"; 

    const stockContainer = document.getElementById("StockContainer"); 
    const stockInput = document.getElementById("Stock"); 
    const countContainer = document.getElementById("CountContainer");
    const countInput = document.getElementById("Quantity"); 
    const stockMessage = document.getElementById("StockMessage"); 

    stockInput.value = availableStock;
    countInput.max = availableStock;

    if (+countInput.value > +countInput.max) {
        countInput.value = countInput.max;
    }

    if (sizeSelect.selectedIndex === 0) {
        stockContainer.style.display = "block";
        countContainer.style.display = "block";
        stockMessage.style.display = "none";
    }
    else if (+availableStock === 0) {
        stockContainer.style.display = "none";
        countContainer.style.display = "none";
        stockMessage.style.display = "block";
    }
    else {
        stockContainer.style.display = "block";
        countContainer.style.display = "block";
        stockMessage.style.display = "none";
    }
}

window.onload = function () {
    const sizeSelect = document.getElementById("SizeId");
    if (!sizeSelect.value) sizeSelect.selectedIndex = 0;
    updateStock();
};