using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoStefaniniESGreen.Data;
using ProjetoStefaniniESGreen.Models;
using ProjetoStefaniniESGreen.Models.Dtos;

namespace ProjetoStefaniniESGreen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.ItensPedido)
                .ThenInclude(i => i.Produto) 
                .ToListAsync();

            var pedidosDto = pedidos.Select(p => new PedidoDto
            {
                Id = p.Id,
                NomeCliente = p.NomeCliente,
                EmailCliente = p.EmailCliente,
                Pago = p.Pago,
                ValorTotal = p.ItensPedido.Sum(i => i.Produto.Valor * i.Quantidade),
                ItensPedido = p.ItensPedido.Select(i => new ItemPedidoDto
                {
                    Id = i.Id,
                    IdProduto = i.Produto.Id, 
                    NomeProduto = i.Produto.NomeProduto,
                    ValorUnitario = i.Produto.Valor,
                    Quantidade = i.Quantidade
                }).ToList()
            }).ToList();


            return Ok(pedidosDto);
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // POST: api/Pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
        }

        // PUT: api/Pedidos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
