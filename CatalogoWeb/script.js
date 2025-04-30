async function obtenerProductos() {
    try {
        const respuesta = await fetch("https://tudominio.com/api/productos"); // Cambia la URL por la real
        const productos = await respuesta.json();
        renderizarProductos(productos);
    } catch (error) {
        console.error("Error al obtener productos:", error);
    }
}

function renderizarProductos(productosFiltrados) {
    const catalog = document.querySelector('.catalog');
    catalog.innerHTML = productosFiltrados.map(producto => `
        <div class="product" data-nombre="${producto.nombre}" data-marca="${producto.marca}" data-año="${producto.año}" data-modelo="${producto.modelo}">
            <img src="${producto.imagen}" alt="${producto.nombre}">
            <div class="product-info">
                <h3>${producto.nombre}</h3>
                <p class="price">Q${producto.precio}</p>
                <button class="btn-details">Ver Detalles</button>
            </div>
        </div>
    `).join('');
}

async function filtrarProductos() {
    const searchTerm = document.getElementById('search').value.toLowerCase();
    const marca = document.getElementById('marca').value;
    const año = document.getElementById('año').value;
    const modelo = document.getElementById('modelo').value;

    try {
        const respuesta = await fetch("https://tudominio.com/api/productos"); // misma URL
        const productos = await respuesta.json();

        const productosFiltrados = productos.filter(producto => {
            return (
                producto.nombre.toLowerCase().includes(searchTerm) &&
                (marca === "" || producto.marca === marca) &&
                (año === "" || producto.año === año) &&
                (modelo === "" || producto.modelo === modelo)
            );
        });

        renderizarProductos(productosFiltrados);
    } catch (error) {
        console.error("Error al filtrar productos:", error);
    }
}

document.addEventListener('DOMContentLoaded', obtenerProductos);
