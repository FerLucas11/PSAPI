namespace ProjetoStefaniniESGreen.Models.Dtos
{
    public class ItemPedidoDto
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public double ValorUnitario { get; set; }
        public int Quantidade { get; set; }
    }
}
