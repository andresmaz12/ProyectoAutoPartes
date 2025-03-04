function agregarProducto() {
    // Obtener el valor del input
    const nombreProducto = document.getElementById('producto').value;

    // Verificar que el campo no esté vacío
    if (nombreProducto.trim() === '') {
        alert('Por favor, ingrese un nombre de producto.');
        return;
    }

    // Crear un nuevo elemento de lista
    const nuevoProducto = document.createElement('li');
    nuevoProducto.textContent = nombreProducto;

    // Botón para eliminar el producto
    const botonEliminar = document.createElement('button');
    botonEliminar.textContent = 'Eliminar';
    botonEliminar.onclick = function() {
        nuevoProducto.remove();
    };

    // Agregar el botón de eliminar al producto
    nuevoProducto.appendChild(botonEliminar);

    // Agregar el nuevo producto a la lista
    document.getElementById('listaProductos').appendChild(nuevoProducto);

    // Limpiar el campo de entrada
    document.getElementById('producto').value = '';
}