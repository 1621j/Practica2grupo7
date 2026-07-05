(() => {

    const Producto = {

        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
        },

        inicializarTabla() {
            this.tabla = $('#tblProducto').DataTable({
                ajax: {
                    url: '/Productos/ObtenerTodos',
                    type: 'GET',
                    dataSrc: 'dato'
                },

                columns: [
                    { data: 'nombre' },
                    { data: 'descripcion' },
                    { data: 'precio' },
                    { data: 'stock' },
                    { data: 'nombreCategoria' },
                    {
                        data: null,
                        orderable: false,
                        searchable: false,
                        className: 'text-center',
                        render: function (data, type, row) {
                            return `
                                <button class="btn btn-sm btn-outline-primary btn-editar me-2" data-id="${row.productoId}">
                                    <i class="bi bi-pencil"></i>
                                </button>

                                <button class="btn btn-sm btn-outline-danger btn-eliminar" data-id="${row.productoId}">
                                    <i class="bi bi-trash"></i>
                                </button>`;
                        }
                    }
                ],

                pageLength: 10,

                language: {
                    search: "Buscar:",
                    lengthMenu: "Mostrar _MENU_ registros",
                    info: "Mostrando _START_ a _END_ de _TOTAL_",
                    zeroRecords: "No hay registros",
                    paginate: {
                        next: "Siguiente",
                        previous: "Anterior"
                    }
                }
            });
        },

        registrarEventos() {

            $('#btnGuardarProducto').on('click', function () {
                Producto.guardarProducto();
            });

            $('#btnEditarProducto').on('click', function () {
                Producto.editarProducto();
            });

            $('#tblProducto').on('click', '.btn-editar', function () {
                let id = $(this).data('id');
                Producto.cargarProducto(id);
            });

            $('#tblProducto').on('click', '.btn-eliminar', function () {
                let id = $(this).data('id');
                Producto.eliminarProducto(id);
            });

            $('#modalNuevoProducto').on('hidden.bs.modal', function () {
                $('#formCrearProducto')[0].reset();
            });
        },

        guardarProducto() {

            let form = $('#formCrearProducto');

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),

                success: function (respuesta) {
                    if (respuesta.esCorrecto) {
                        $('#modalNuevoProducto').modal('hide');
                        form[0].reset();
                        Producto.tabla.ajax.reload();

                        Swal.fire({
                            icon: 'success',
                            title: 'Correcto',
                            text: respuesta.mensaje
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: respuesta.mensaje
                        });
                    }
                }
            });
        },

        cargarProducto(id) {

            $.get(`/Productos/ObtenerPorId?id=${id}`, function (resultado) {

                if (resultado.esCorrecto) {

                    let data = resultado.dato;

                    $('#formEditarProducto #ProductoId').val(data.productoId);
                    $('#formEditarProducto #Nombre').val(data.nombre);
                    $('#formEditarProducto #Descripcion').val(data.descripcion);
                    $('#formEditarProducto #Precio').val(data.precio);
                    $('#formEditarProducto #Stock').val(data.stock);
                    $('#formEditarProducto #CategoriaId').val(data.categoriaId);

                    $('#modalEditarProducto').modal('show');
                }
            });
        },

        editarProducto() {

            let form = $('#formEditarProducto');

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),

                success: function (respuesta) {
                    if (respuesta.esCorrecto) {
                        $('#modalEditarProducto').modal('hide');
                        Producto.tabla.ajax.reload();

                        Swal.fire({
                            icon: 'success',
                            title: 'Correcto',
                            text: respuesta.mensaje
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: respuesta.mensaje
                        });
                    }
                }
            });
        },

        eliminarProducto(id) {

            Swal.fire({
                title: '¿Esta seguro?',
                text: 'Esta accion no se puede revertir',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Eliminar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {

                if (result.isConfirmed) {

                    $.ajax({
                        url: '/Productos/Delete',
                        type: 'POST',
                        data: { id: id },

                        success: function (respuesta) {
                            if (respuesta.esCorrecto) {
                                Producto.tabla.ajax.reload();

                                Swal.fire({
                                    icon: 'success',
                                    title: 'Eliminado',
                                    text: respuesta.mensaje
                                });
                            }
                        }
                    });
                }
            });
        }
    };

    $(document).ready(() => Producto.init());

})();