// Datos de ejemplo (pueden ser reemplazados por una API)
const productos = [
    { nombre: "Sistema de Dirección", marca: "Toyota", año: "2020", modelo: "Corolla", precio: "150.00", imagen: "direccion.jpeg" },
    { nombre: "Sistema de Frenos", marca: "Honda", año: "2021", modelo: "Civic", precio: "200.00", imagen: "frenos.jpeg" },
    { nombre: "Sistema de Motor", marca: "Ford", año: "2022", modelo: "Mustang", precio: "500.00", imagen: "motor.jpeg" },
    { nombre: "Sistema de Suspensión", marca: "Chevrolet", año: "2023", modelo: "Silverado", precio: "300.00", imagen: "suspension.jpeg" },
];

// Función para renderizar productos
function renderizarProductos(productosFiltrados) {
    const catalog = document.querySelector('.catalog');
    catalog.innerHTML = productosFiltrados.map(producto => `
        <div class="product" data-nombre="${producto.nombre}" data-marca="${producto.marca}" data-año="${producto.año}" data-modelo="${producto.modelo}">
            <img src="${producto.imagen}" alt="${producto.nombre}">
            <div class="product-info">
                <h3>${producto.nombre}</h3>
                <p class="price">${producto.precio}</p>
                <button class="btn-details">Ver Detalles</button>
            </div>
        </div>
    `).join('');
}

// Función para filtrar productos
function filtrarProductos() {
    const searchTerm = document.getElementById('search').value.toLowerCase();
    const marca = document.getElementById('marca').value;
    const año = document.getElementById('año').value;
    const modelo = document.getElementById('modelo').value;

    const productosFiltrados = productos.filter(producto => {
        return (
            producto.nombre.toLowerCase().includes(searchTerm) &&
            (marca === "" || producto.marca === marca) &&
            (año === "" || producto.año === año) &&
            (modelo === "" || producto.modelo === modelo)
        );
    });

    renderizarProductos(productosFiltrados);
}

// Inicializar con todos los productos
document.addEventListener('DOMContentLoaded', () => {
    renderizarProductos(productos);
});