﻿namespace ProjetoStefaniniESGreen.Models.Dtos
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public bool Pago { get; set; }
        public double ValorTotal { get; set; }
        public List<ItemPedidoDto> ItensPedido { get; set; }
    }
}
