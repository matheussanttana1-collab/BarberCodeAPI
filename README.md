# ✂️ BarberCode API

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2CA5E0?style=for-the-badge&logo=docker&logoColor=white)
![Status](https://img.shields.io/badge/Status-Em_Produ%C3%A7%C3%A3o-success?style=for-the-badge)

Uma API REST completa e escalável desenvolvida em **ASP.NET Core 8** para gestão de agendamentos, clientes e serviços de barbearias. O projeto foi construído focando em **Clean Architecture**, **Desacoplamento** e **Arquitetura Orientada a Eventos**.

🌐 **A API ESTÁ ONLINE!**  
Você pode testar e interagir com os endpoints agora mesmo através do Swagger:  
👉 **https://barbercodex.duckdns.org/swagger/index.html**

---

## 💡 Como testar na prática (Sem precisar criar conta)

Para facilitar a avaliação técnica, o banco de dados em produção já possui um *Data Seeding* (dados iniciais pré-cadastrados). Você pode testar o fluxo de agendamento de ponta a ponta sem precisar de login, Ou se Quiser Crie sua propria Barbearia, e cadastre e logue seu WhatsApp!

Barbearia Teste Login:

email: barbeariaTeste@gmail.com

senha:Senha123@Teste

**Siga este passo a passo rápido no Swagger:**

1. **Buscando as referências (IDs):**
   * Chame o endpoint `GET /api/barbearias` para ver a barbearia de demonstração e copie o `id` dela.
   * Chame `GET /api/barbeiros` e copie o `id` do barbeiro disponível.
   * Chame `GET /api/servicos` e copie o `id` do serviço que deseja agendar.
Execute! Se tudo der certo, você receberá um 200 OK e o nosso sistema de eventos fará o processamento das notificações em segundo plano!

🚀 Tecnologias Utilizadas
C# & .NET 8 (ASP.NET Core Web API)

Entity Framework Core (Code-First)

MySQL (Banco de Dados Relacional)

Docker & Docker Compose (Containerização)

ASP.NET Core Identity & JWT (Autenticação e Autorização)

Evolution API (Integração para disparos de WhatsApp)

Arquitetura Orientada a Eventos (EventBus customizado e Domain Events)


🐳 Como rodar o projeto localmente (Docker)
O projeto está totalmente containerizado, o que significa que você só precisa do Docker instalado na sua máquina para rodar a API e o Banco de Dados simultaneamente.

Clone o repositório:

Bash
git clone [https://github.com/matheussanttana1-collab/BarberCodeAPI.git](https://github.com/matheussanttana1-collab/BarberCodeAPI.git)
cd BarberCode
Configure as variáveis de ambiente:
Renomeie o arquivo appsettings.Development.example.json para appsettings.Development.json e insira suas chaves (caso queira testar o fluxo de WhatsApp real com a Evolution API).

Suba os containers:

Bash
docker-compose up -d
Acesse localmente:
A API estará rodando e disponível em: http://localhost:8080/swagger (ajuste a porta conforme o seu ambiente). O banco de dados MySQL também já estará rodando na porta 3306 (ou 3308 externamente, dependendo do seu compose).

### 📱 Configurando o WhatsApp (Rodando Localmente)

Se você estiver rodando o projeto localmente e quiser testar o disparo de mensagens, precisará conectar o seu WhatsApp à instância da Evolution API que subiu no Docker:

1. Com os containers rodando, acesse a documentação local ou o gerenciador da Evolution API (geralmente em `http://localhost:8081`).
2. Crie uma nova instância (ex: `BarberCodeInstance`).
3. Gere o QR Code e escaneie com o WhatsApp que fará os disparos (recomenda-se usar um número de testes).
4. Copie a `ApiKey` e a `BaseUrl` da sua instância e atualize o seu arquivo `appsettings.Development.json` no projeto da API.
5. Pronto! Os eventos de agendamento já vão disparar mensagens reais.

📂 Estrutura do projeto
/src
 ├── Domain
 ├── Application
 ├── Infrastructure
 └── API

 ⚙️ Decisões técnicas
✔️ Uso de Clean Architecture
Separar responsabilidades e facilitar manutenção e testes.

✔️ Uso de DDD
Organizar o domínio de forma clara, com foco nas regras de negócio.

✔️ Uso de Result Pattern
Evitar o uso excessivo de exceções para controle de fluxo.

✔️ Integrações externas isoladas
Serviços externos são abstraídos na camada de infraestrutura.

 
🗺️ Roadmap e Próximos Passos
O backend core está finalizado e em produção, mas o projeto continua evoluindo:

[x] CRUD de Barbearias, Barbeiros e Serviços

[x] Sistema de Agendamento de Horários

[x] Autenticação segura via JWT e Identity

[x] Fluxo seguro de recuperação de senha (Base64UrlEncoding)

[x] Notificações via E-mail e WhatsApp (Event Driven)

[x] Containerização com Docker

[x] Deploy em Nuvem

[ ] Testes Unitários: Implementação de testes automatizados (Em Progresso ⏳)

[ ] Front-end: Desenvolvimento da interface de usuário para consumir esta API (Em Progresso ⏳)

[ ] Mensageria: Substituição do EventBus em memória por RabbitMQ.


## 👨‍💻 Autor

**Matheus Santana**  

Desenvolvedor .NET focado em construção de APIs escaláveis, boas práticas e arquitetura limpa.

- 💼 [LinkedIn](https://www.linkedin.com/in/matheus-santana-871348148)  
- 💻 [GitHub](https://github.com/matheussanttana1-collab)

---
