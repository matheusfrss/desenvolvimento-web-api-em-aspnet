using LHPets.Models;

namespace LHPets.Data;

/// <summary>
/// Armazenamento temporário em memória usado nesta etapa de prototipagem.
/// Os dados são perdidos quando a aplicação é encerrada.
/// </summary>
public static class MemoriaDb
{
    public static List<Cliente> Clientes { get; } = new();
    public static List<Pet> Pets { get; } = new();
}
