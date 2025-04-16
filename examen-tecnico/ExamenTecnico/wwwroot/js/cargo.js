// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let currentId = null; // Variable para almacenar el ID del cargo en edición

// Abrir el modal para crear un nuevo cargo
function openCreateModal() {
    currentId = null; // Limpiar el ID para indicar que es una creación
    document.getElementById('cargoForm').reset(); // Limpiar el formulario
    document.getElementById('cargoModalLabel').innerText = 'Nuevo Cargo';
}

// Abrir el modal para editar un cargo existente
function openEditModal(id) {
    currentId = id; // Guardar el ID del cargo en edición
    document.getElementById('cargoModalLabel').innerText = 'Editar Cargo';

    // Obtener los datos del cargo mediante una llamada AJAX
    fetch(`/Cargo/GetCargo/${id}`)
        .then(response => response.json())
        .then(data => {
            document.getElementById('idCargo').value = data.idCargo;
            document.getElementById('valorCargo').value = data.valorCargo;
        })
        .catch(error => console.error('Error:', error));
}

// Guardar el cargo (nuevo o edición)
document.getElementById('saveCargo').addEventListener('click', function () {
    debugger;
    // Obtener los valores directamente de los campos del formulario
    const idCargo = document.getElementById('idCargo').value;
    const valorCargo = document.getElementById('valorCargo').value;

    // Validar que el campo "Valor del Cargo" no esté vacío
    if (!valorCargo.trim()) {
        alert('El campo "Valor del Cargo" es obligatorio.');
        return;
    }

    const url = currentId ? `/Cargo/Edit/${currentId}` : '/Cargo/Create';
    const method = currentId ? 'PUT' : 'POST';

    fetch(url, {
        method: method,
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            idCargo: 1, // Incluye el ID solo en caso de edición
            valorCargo: valorCargo
        })
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert(data.message);
                location.reload(); // Recargar la página para reflejar los cambios
            } else {
                alert('Error: ' + data.message);
            }
        })
        .catch(error => console.error('Error:', error));
});

// Eliminar un cargo
document.querySelectorAll('.btn-delete').forEach(button => {
    button.addEventListener('click', function (event) {
        event.preventDefault(); // Evitar que el enlace redirija inmediatamente

        const idCargo = this.getAttribute('data-id'); // Obtener el ID del cargo
        const url = `/Cargo/Delete/${idCargo}`; // URL para eliminar el cargo

        // Mostrar confirmación
        if (confirm('¿Estás seguro de que deseas eliminar este cargo?')) {
            fetch(url, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        location.reload(); // Recargar la página si la eliminación fue exitosa
                    } else {
                        alert('Error: ' + data.message);
                    }
                })
                .catch(error => console.error('Error:', error));
        }
    });
});