// Variable para almacenar el último cálculo realizado
let ultimoCalculo = null;

// Cargar el último valor al iniciar la página
document.addEventListener('DOMContentLoaded', function () {
    const ultimoValorGuardado = localStorage.getItem('ultimoValor');
    const ultimoValorDiv = document.getElementById('ultimoValor');
    if (ultimoValorGuardado) {
        ultimoValorDiv.textContent = `Último cálculo: ${ultimoValorGuardado}`;
    } else {
        ultimoValorDiv.textContent = 'No hay cálculos previos.';
    }
});

// Función para manejar el botón "Ejecutar"
document.getElementById('btnEjecutar').addEventListener('click', function () {
    const expressionInput = document.getElementById('expression');
    let expression = expressionInput.value.trim();

    if (!expression) {
        alert('Por favor ingresa una expresión válida.');
        return;
    }

    try {
        // Reemplazar multiplicaciones implícitas con explícitas
        expression = expression.replace(/(\d)\(/g, '$1*('); // Ejemplo: 1( -> 1*(
        expression = expression.replace(/\)(\d)/g, ')*$1'); // Ejemplo: )1 -> )*1

        // Evaluar la expresión corregida
        const result = eval(expression);

        // Agregar el resultado al historial
        const resultsList = document.getElementById('resultados');
        const listItem = document.createElement('li');
        listItem.className = 'list-group-item';
        listItem.textContent = result;
        resultsList.appendChild(listItem);

        // Guardar el último cálculo en la variable
        ultimoCalculo = result;

        // Limpiar el campo de entrada
        expressionInput.value = '';
    } catch (error) {
        alert('Error en la expresión. Por favor revisa la sintaxis.');
    }
});

// Guardar el último cálculo en localStorage al cerrar la página
window.addEventListener('beforeunload', function () {
    if (ultimoCalculo !== null) {
        localStorage.setItem('ultimoValor', ultimoCalculo);
    }
});

document.getElementById('btnGuardar').addEventListener('click', function () {
    // Obtener todos los elementos <li> del historial
    const resultadosList = document.querySelectorAll('#resultados li');

    if (resultadosList.length === 0) {
        alert('No hay valores calculados para guardar.');
        return;
    }

    // Extraer el texto de cada <li> y almacenarlo en un array
    const valoresArray = Array.from(resultadosList).map(li => {
        const texto = li.textContent.trim();
        return texto.split('=')[1]?.trim() || texto;
    });

    // Convertir el array en una cadena separada por comas
    const valoresCalculados = valoresArray.join(',');

    // Enviar los valores al backend
    fetch('/Home/GuardarValores', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(valoresCalculados)
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert('Valores guardados correctamente en la BD.');
                // Limpiar la lista de resultados
                const resultadosUl = document.getElementById('resultados');
                resultadosUl.innerHTML = ''; // Vaciar el contenido de la lista

            } else {
                alert('Error al guardar los valores: ' + data.message);
            }
        })
        .catch(error => console.error('Error:', error));
});