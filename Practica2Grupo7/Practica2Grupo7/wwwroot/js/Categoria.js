(() => {

    const Categoria = {

        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
        },

        inicializarTabla() {

            this.tabla = $('#tblCategoria').DataTable({

                ajax: {
                    url: '/Categorias/ObtenerTodas',
                    type: 'GET',
                    dataSrc: 'dato'
                },

                columns: [
                    { data: 'nombre' },
                    { data: 'descripcion' },
                    {
                        data: null,
                        orderable: false,
                        searchable: false,
                        className: 'text-center',
                        render: (data, type, row) => {

                            return `
                            <button class="btn btn-sm btn-outline-primary btn-editar me-2"
                                    data-id="${row.categoriaId}">
                                <i class="bi bi-pencil"></i>
                            </button>

                            <button class="btn btn-sm btn-outline-danger btn-eliminar"
                                    data-id="${row.categoriaId}">
                                <i class="bi bi-trash"></i>
                            </button>`;
                        }
                    }
                ],

                scrollX: false,
                responsive: true,
                pageLength: 10,
                searching: true,
                ordering: true,
                paging: true,

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


            
            $('#tblCategoria').on('click', '.btn-editar', function () {

                let id = $(this).data('id');

                Categoria.cargarCategoria(id);

            });


            
            $('#tblCategoria').on('click', '.btn-eliminar', function () {

                let id = $(this).data('id');

                Categoria.eliminarCategoria(id);

            });
            
            $('#btnGuardarCategoria').on('click', function () {

                Categoria.guardarCategoria();

            });

            $('#btnEditarCategoria').on('click', function () {
                Categoria.editarCategoria();
            });



            $('#modalNuevaCategoria').on('hidden.bs.modal', function () {

                $('#formCrearCategoria')[0].reset();

            });

        },


        guardarCategoria() {


            let form = $('#formCrearCategoria');

            console.log(form.attr('action'));
            if (!form.valid()) {
                return;
            }


            $.ajax({

                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),

                success: function (respuesta) {


                    if (respuesta.esCorrecto) {


                        $('#modalNuevaCategoria').modal('hide');


                        form[0].reset();


                        Categoria.tabla.ajax.reload();


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
        editarCategoria() {

            let form = $('#formEditarCategoria');

            if (!form.valid()) {
                return;
            }

            $.ajax({

                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),

                beforeSend: function () {
                    console.log("ENVIANDO:", form.serialize());
                },

                success: function (respuesta) {

                    if (respuesta.esCorrecto) {

                        $('#modalEditarCategoria').modal('hide');

                        form[0].reset();

                        Categoria.tabla.ajax.reload();

                        Swal.fire({
                            title: 'Correcto',
                            text: respuesta.mensaje,
                            icon: 'success'
                        });

                    } else {

                        Swal.fire({
                            title: 'Incorrecto',
                            text: respuesta.mensaje,
                            icon: 'error'
                        });
                    }
                },

            });
        },

        eliminarCategoria(id) {


            Swal.fire({

                title: '¿Está seguro?',
                text: "Esta acción no se puede revertir",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Eliminar',
                cancelButtonText: 'Cancelar'

            }).then((result) => {


                if (result.isConfirmed) {


                    $.ajax({

                        url: `/Categorias/Delete`,
                        type: 'POST',
                        data:{id: id},

                        success: function (respuesta) {


                            if (respuesta.esCorrecto) {


                                Categoria.tabla.ajax.reload(null, true);


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

        },


        cargarCategoria(id) {

            $.get(`/Categorias/ObtenerPorId?id=${id}`, function (resultado) {

                if (resultado.esCorrecto) {

                    let data = resultado.dato;

                    $('#CategoriaId').val(data.categoriaId);
                    $('#Nombre').val(data.nombre);
                    $('#Descripcion').val(data.descripcion);

                    $('#modalEditarCategoria').modal('show');

                }

            });

        }


    };


    $(document).ready(() => Categoria.init());


})();