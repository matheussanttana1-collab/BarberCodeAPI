# Refatoração - DependencyInjections

## Resumo das Mudanças

### 1. **BarberCode.Infra/DependencyInjections.cs** - Destrinchado em Métodos Menores

A classe foi refatorada para seguir o princípio de **Single Responsibility**, separando as responsabilidades por categorias:

#### Métodos Criados:

- **`AddDatabase(IConfiguration)`**
  - Configura o `DbContext` com MySql
  - Registra `Identity` com `AppUser` e `IdentityRole`
  - Válida se a string de conexão existe (throws se não encontrada)

- **`AddRepositories()`**
  - Registra todos os repositórios em Scoped
  - Agrupa: `AgendamentoRepository`, `ServicoRepository`, `BarbeiroRepository`, `BarbeariaRepository`, `ClienteRepository`

- **`AddServices()`**
  - Registra todos os serviços em Scoped
  - Agrupa: `AppUserService`, `TokenService`, `EmailService`, `EmailTemplateService`, `WhatsAppService` (com HttpClient)

- **`AddAutoMapperConfiguration()`**
  - Configura AutoMapper com profiles
  - Isolado para facilitar testes e manutenção

- **`AddEmailSettings(IConfiguration)`**
  - Configura `EmailSettings` via `Configuration`
  - Usa `Options Pattern` do ASP.NET Core

#### Método Principal:

```csharp
public static IServiceCollection addInfra(this IServiceCollection services, IConfiguration configuration)
```

Agora recebe `IConfiguration` como parâmetro e chama os métodos em sequência, ficando responsável apenas por orquestrar o registro.

---

### 2. **BarberCode.API/Program.cs** - Simplificado e Limpo

**Antes:**
```csharp
builder.Services.AddHttpClient();
var connectionString = builder.Configuration.GetConnectionString("BarberCodeConnection");
builder.Services.AddDbContext<BarberCodeContext>(...);
builder.Services.AddAutoMapper(...);
builder.Services.addInfra();
builder.Services.Configure<EmailSettings>(...);
```

**Depois:**
```csharp
builder.Services.AddHttpClient();
builder.Services.AddApplication();
builder.Services.addInfra(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<CriarBarbeariaValidator>();
```

- Removidos 5 linhas de configuração desnecessárias do `Program.cs`
- Removidos imports desnecessários: `BarbeariaProfile`, `BarberCodeContext`, `EntityFrameworkCore`
- Toda a configuração de banco agora está centralizada em `DependencyInjections`

---

### 3. **appsettings.json** - Secrets Adicionadas

**Antes:**
```json
"ConnectionStrings": {
    "BarberCodeConnection": "server=localhost; database=BarberCode; user=root; password=Fm020406@"
}
```

**Depois:**
```json
"ConnectionStrings": {
    "BarberCodeConnection": "${DATABASE_CONNECTION_STRING}"
}
```

- Placeholder para environment variable
- **Segredo real deve ser definido em**:
  - **Desenvolvimento**: `appsettings.Development.json` (já adicionado)
  - **Produção**: Environment variables ou Azure Key Vault
  - **User Secrets** (para desenvolvimento local): `dotnet user-secrets set "ConnectionStrings:BarberCodeConnection" "..."`

---

### 4. **appsettings.Development.json** - Configuração Local

**Adicionado:**
```json
{
  "Logging": { ... },
  "ConnectionStrings": {
    "BarberCodeConnection": "server=localhost; database=BarberCode; user=root; password=Fm020406@"
  }
}
```

Agora contém a string de conexão local para desenvolvimento.

---

## Benefícios

✅ **Separação de Responsabilidades**: Cada método tem uma função clara  
✅ **Testabilidade**: Métodos menores são mais fáceis de testar  
✅ **Manutenibilidade**: Adicionar nova categoria é trivial  
✅ **Segurança**: Secrets fora do arquivo padrão  
✅ **Documentação**: Cada método tem um resumo XML  
✅ **Limpeza do Program.cs**: Configuração centralizada na Infra  

---

## Como Usar Environment Variables

**Windows (PowerShell):**
```powershell
$env:DATABASE_CONNECTION_STRING = "server=localhost; database=BarberCode; user=root; password=senha@"
dotnet run
```

**Linux/Mac:**
```bash
export DATABASE_CONNECTION_STRING="server=localhost; database=BarberCode; user=root; password=senha@"
dotnet run
```

**Docker (.env file):**
```
DATABASE_CONNECTION_STRING=server=db-service; database=BarberCode; user=root; password=senha@
```

---

## Build Status

✅ Compilação bem-sucedida sem erros.
