using LHPets.Data;
using LHPets.Models;
using Microsoft.AspNetCore.Mvc;

namespace LHPets.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(MemoriaDb.Clientes);
    }

    // Este endpoint facilita a resposta 201 do cadastro.
    [HttpGet("{id:int}")]
    public IActionResult BuscarPorId(int id)
    {
        var cliente = MemoriaDb.Clientes.FirstOrDefault(c => c.Id == id);

        return cliente is null ? NotFound() : Ok(cliente);
    }

    [HttpPost]
    public IActionResult Cadastrar(Cliente cliente)
    {
        if (MemoriaDb.Clientes.Any(c => c.Cpf == cliente.Cpf))
        {
            return Conflict(new { mensagem = "Este CPF já está cadastrado." });
        }

        cliente.Id = ProximoId();
        MemoriaDb.Clientes.Add(cliente);

        return CreatedAtAction(nameof(BuscarPorId), new { id = cliente.Id }, cliente);
    }

    private static int ProximoId()
    {
        return MemoriaDb.Clientes.Count == 0
            ? 1
            : MemoriaDb.Clientes.Max(c => c.Id) + 1;
    }
}
