# LH-Pets — Web API em ASP.NET Core

Projeto avaliativo de Web API em C# usando ASP.NET Core MVC com Controllers, Swagger e listas em memória (`List<T>`).

## Estrutura

- `Models/Cliente.cs`: modelo de cliente com `Id`, `Nome`, `Cpf` e `Email`.
- `Models/Pet.cs`: modelo de pet com `Id`, `Nome`, `Especie`, `Raca` e `ClienteId`.
- `Controllers/ClientesController.cs`: cadastro e listagem de clientes.
- `Controllers/PetsController.cs`: cadastro, listagem e filtro de pets por cliente.
- `Data/MemoriaDb.cs`: listas em memória compartilhadas pelos controllers.

Os dados são perdidos quando a aplicação é encerrada, pois esta etapa não utiliza banco de dados.

## Como executar

Pré-requisito: SDK do .NET 8 instalado.

No terminal, dentro desta pasta:

```bash
dotnet restore
dotnet run --launch-profile http
```

Abra o Swagger em [http://localhost:5100/swagger](http://localhost:5100/swagger).

## Endpoints

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/clientes` | Lista todos os clientes |
| `POST` | `/api/clientes` | Cadastra um cliente |
| `GET` | `/api/clientes/{id}` | Consulta um cliente por Id |
| `GET` | `/api/pets` | Lista todos os pets |
| `POST` | `/api/pets` | Cadastra um pet vinculado a um cliente |
| `GET` | `/api/pets/cliente/{clienteId}` | Lista os pets de um cliente |
| `GET` | `/api/pets/{id}` | Consulta um pet por Id |

## Ordem sugerida para o teste no Swagger

1. Execute `POST /api/clientes` com um cliente.
2. Execute `GET /api/clientes` e anote o `id` retornado.
3. Execute `POST /api/pets`, usando esse valor em `clienteId`.
4. Execute `GET /api/pets`.
5. Execute `GET /api/pets/cliente/{clienteId}`.

As validações retornam `400 Bad Request` para dados obrigatórios inválidos, `404 Not Found` quando o cliente informado não existe e `409 Conflict` quando o CPF já está cadastrado.
