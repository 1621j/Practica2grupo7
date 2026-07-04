namespace Practica2Grupo7.BLL.Dtos
{
    public class ProductoDto
    {
        public int ProductoId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public int CategoriaId { get; set; }

        public string NombreCategoria { get; set; } = string.Empty;
    }
}