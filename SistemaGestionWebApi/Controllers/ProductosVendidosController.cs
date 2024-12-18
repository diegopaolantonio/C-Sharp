﻿using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness.Services;
using SistemaGestionEntities;

namespace SistemaGestionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosVendidosController : ControllerBase
    {
        private readonly ILogger<ProductosVendidosController> _logger;
        private readonly ProductosVendidosService _productoVendidoService;
        private readonly VentasService _ventasService;
        private readonly UsuariosService _usuariosService;
        private readonly ProductosService _productosService;

        public ProductosVendidosController(
            ILogger<ProductosVendidosController> logger,
            ProductosVendidosService productoVendidoService,
            VentasService ventasService,
            UsuariosService usuariosService,
            ProductosService productosService
            )
        {
            _logger = logger;
            _productoVendidoService = productoVendidoService;
            _ventasService = ventasService;
            _usuariosService = usuariosService;
            _productosService = productosService;

        }

        [HttpGet(Name = "Get ProductoVendido")]
        public async Task<ActionResult<List<ProductoVendido>>> GetProductoVendido()
        {
            ProductoVendido productoVendido = new ProductoVendido();
            return await _productoVendidoService.GetProductoVendido();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoVendido>> GetOneProductoVendido(int id)
        {
            _logger.LogInformation("Consultando por el Producto Vendido con id {id}", id);
            var productoVendido = await _productoVendidoService.GetOneProductoVendido(id);
            if (productoVendido is null)
            {
                return NotFound();
            }
            return productoVendido;
        }

        [HttpPost(Name = "Post ProductoVendido")]
        public async Task<ActionResult<ProductoVendido>> CrearProductoVendido([FromBody] ProductoVendido productoVendido)
        {
            Producto producto = await _productosService.GetOneProducto(productoVendido.Producto.Id);
            if (producto != null)
            {
                producto.Stock = producto.Stock - productoVendido.Cantidad;
                await _productosService.UpdateProducto(producto.Id, producto);
            }
            var productoVendidoCreado = await _productoVendidoService.InsertProductoVendido(productoVendido);
            return CreatedAtAction(nameof(GetOneProductoVendido), new { id = productoVendidoCreado.Id }, productoVendidoCreado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductoVendido([FromRoute(Name = "id")] int id)
        {
            await _productoVendidoService.DeleteProductoVendido(id);
            return NoContent();
        }

    }
}