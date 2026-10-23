using LHPets.Data;
using LHPets.Models;
using Microsoft.AspNetCore.Mvc;

namespace LHPets.Controllers;

[ApiController]
[Route("api/pets")]
public class PetsController : ControllerBase
{
    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(MemoriaDb.Pets);
    }

    // Este endpoint facilita a resposta 201 do cadastro.
    [HttpGet("{id:int}")]
    public IActionResult BuscarPorId(int id)
    {
        var pet = MemoriaDb.Pets.FirstOrDefault(p => p.Id == id);

        return pet is null ? NotFound() : Ok(pet);
    }

    [HttpPost]
    public IActionResult Cadastrar(Pet pet)
    {
        var clienteExiste = MemoriaDb.Clientes.Any(c => c.Id == pet.ClienteId);

        if (!clienteExiste)
        {
            return NotFound(new { mensagem = "Cliente não encontrado." });
        }

        pet.Id = ProximoId();
        MemoriaDb.Pets.Add(pet);

        return CreatedAtAction(nameof(BuscarPorId), new { id = pet.Id }, pet);
    }

    [HttpGet("cliente/{clienteId:int}")]
    public IActionResult ListarPorCliente(int clienteId)
    {
        var clienteExiste = MemoriaDb.Clientes.Any(c => c.Id == clienteId);

        if (!clienteExiste)
        {
            return NotFound(new { mensagem = "Cliente não encontrado." });
        }

        var pets = MemoriaDb.Pets.Where(p => p.ClienteId == clienteId).ToList();
        return Ok(pets);
    }

    private static int ProximoId()
    {
        return MemoriaDb.Pets.Count == 0
            ? 1
            : MemoriaDb.Pets.Max(p => p.Id) + 1;
    }
}
