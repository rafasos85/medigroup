$(document).ready(function () {
    const apiUrl = "https://medigroup.runasp.net/api/medicamentos"; // Adjust port if necessary
    let deleteId = null;

    //Cargar Datos iniciales
    loadMedicamentos();

    // Evento para que al buscar o filtrar se cargue la tabla
    $('#searchInput, #categoryFilter, #expirationFilter').on('input change', function () {
        loadMedicamentos();
    });

    function loadMedicamentos() {
        const search = $('#searchInput').val();
        const category = $('#categoryFilter').val();
        const expiration = $('#expirationFilter').val();

        let url = apiUrl + "?";
        if (search) url += `search=${search}&`;
        if (category) url += `categoria=${category}&`;
        if (expiration) url += `fechaExpiracion=${expiration}`;
        //hace el request a la api
        $.ajax({
            url: url,
            method: 'GET',
            success: function (data) {
                const tbody = $('#medicationTableBody');
                tbody.empty();
                data.forEach(med => {
                    const row = `
                        <tr>
                            <td>${med.nombre}</td>
                            <td>${med.categoria}</td>
                            <td>${med.cantidad}</td>
                            <td>${new Date(med.fechaExpiracion).toLocaleDateString()}</td>
                            <td>
                                <button class="btn btn-sm btn-warning btn-edit" data-id="${med.id}">Editar</button>
                                <button class="btn btn-sm btn-danger btn-delete" data-id="${med.id}">Eliminar</button>
                            </td>
                        </tr>
                    `;
                    tbody.append(row);
                });
            },
            error: function (err) {
                console.error("Error loading data", err);
            }
        });
    }

    // Abrir Modal para agregar
    $('#btnAdd').click(function () {
        $('#medicationForm')[0].reset();
        $('#medicationId').val('');
        $('#modalTitle').text('Agregar Medicamento');
    });

    // Abrir ventana Modal para editar
    $(document).on('click', '.btn-edit', function () {
        const id = $(this).data('id');
        $.get(`${apiUrl}/${id}`, function (med) {
            $('#medicationId').val(med.id);
            $('#nombre').val(med.nombre);
            $('#categoria').val(med.categoria);
            $('#cantidad').val(med.cantidad);
            $('#fechaExpiracion').val(med.fechaExpiracion.split('T')[0]);
            $('#modalTitle').text('Editar Medicamento');
            $('#medicationModal').modal('show');
        });
    });

    // Guardar cuando se edita o agrega
    $('#btnSave').click(function () {
        const id = $('#medicationId').val();
        const med = {
            id: id ? parseInt(id) : 0,
            nombre: $('#nombre').val(),
            categoria: $('#categoria').val(),
            cantidad: parseInt($('#cantidad').val()),
            fechaExpiracion: $('#fechaExpiracion').val()
        };
        //Validar que todos los campos esten completos
        if (!med.nombre || !med.categoria || !med.cantidad || !med.fechaExpiracion) {
            alert("Por favor complete todos los campos.");
            return;
        }
        //Validar que la cantidad sea un numero
        if (isNaN(med.cantidad)) {
            alert("La cantidad debe ser un numero");
            return;
        }

        if (med.cantidad < 0) {
            alert("La cantidad debe ser mayor a 0");
            return;
        }
        //Validar que la fecha de expiracion sea una fecha valida
        if (!med.fechaExpiracion) {
            alert("Por favor seleccione una fecha de expiracion.");
            return;
        }
        const method = id ? 'PUT' : 'POST';
        const url = id ? `${apiUrl}/${id}` : apiUrl;

        $.ajax({
            url: url,
            method: method,
            contentType: 'application/json',
            data: JSON.stringify(med),
            success: function () {
                $('#medicationModal').modal('hide');
                loadMedicamentos();
            },
            error: function (err) {
                console.error("Error saving data", err);
                alert("Error al guardar. Verifique la consola.");
            }
        });
    });

    // Confirmar eliminación
    $(document).on('click', '.btn-delete', function () {
        deleteId = $(this).data('id');
        $('#deleteModal').modal('show');
    });

    $('#btnConfirmDelete').click(function () {
        if (deleteId) {
            $.ajax({
                url: `${apiUrl}/${deleteId}`,
                method: 'DELETE',
                success: function () {
                    $('#deleteModal').modal('hide');
                    loadMedicamentos();
                },
                error: function (err) {
                    console.error("Error deleting data", err);
                }
            });
        }
    });
});
