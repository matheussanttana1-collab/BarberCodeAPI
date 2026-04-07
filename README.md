# 💈 BarbeCode API

Sistema de agendamento para barbearias desenvolvido com foco em boas práticas de arquitetura, escalabilidade e organização de código.

---

## 📌 Sobre o projeto

O **BarbeCode** é uma API REST que permite o gerenciamento de usuários, autenticação e agendamentos de serviços em barbearias.

O projeto foi construído com foco em:

* Separação de responsabilidades
* Regras de negócio bem definidas
* Integração com serviços externos
* Código limpo e manutenível

---

## 🚀 Tecnologias utilizadas

* C#
* .NET / ASP.NET Core
* Entity Framework Core
* SQL Server (ou outro banco que você estiver usando)
* JWT (autenticação)
* Clean Architecture
* DDD (Domain-Driven Design)
* Result Pattern
* Integração com APIs externas (Email e WhatsApp)

---

## 🧠 Arquitetura

O projeto segue os princípios de **Clean Architecture**, organizado nas seguintes camadas:

* **Domain**
  Contém entidades, regras de negócio e contratos.

* **Application**
  Contém os casos de uso da aplicação.

* **Infrastructure**
  Implementações de acesso a dados e integrações externas.

* **API**
  Camada de entrada (controllers, configuração, middlewares).

### 🔄 Regra de dependência:

As dependências sempre apontam para o **Domain**, garantindo baixo acoplamento.

---

## 📦 Funcionalidades

* Cadastro de usuários
* Autenticação com JWT
* Agendamento de serviços
* Validação de regras de negócio
* Envio de notificações (Email e WhatsApp)

---

## 🔌 Integrações externas

O sistema integra com serviços externos para:

* 📧 Envio de e-mails
* 📱 Notificações via WhatsApp

---

## 🧭 Fluxo básico do sistema

1. Usuário se cadastra
2. Realiza login
3. Agenda um serviço
4. Sistema valida disponibilidade
5. Notificação é enviada ao usuário

---

## ▶️ Como executar o projeto

### Pré-requisitos

* .NET SDK instalado
* Banco de dados configurado

---

### Passos

```bash
# Clonar o repositório
git clone https://github.com/seu-usuario/barbecode.git

# Entrar na pasta
cd barbecode

# Restaurar dependências
dotnet restore

# Rodar a aplicação
dotnet run
```

---

## 🔐 Variáveis de ambiente

Configure as variáveis abaixo antes de executar:

```
JWT_SECRET=your_secret_key
DATABASE_CONNECTION=your_connection_string
EMAIL_API_KEY=your_email_key
WHATSAPP_API_URL=your_whatsapp_url
```

---

## ⚙️ Decisões técnicas

### ✔️ Uso de Clean Architecture

Separar responsabilidades e facilitar manutenção e testes.

### ✔️ Uso de DDD

Organizar o domínio de forma clara, com foco nas regras de negócio.

### ✔️ Uso de Result Pattern

Evitar o uso excessivo de exceções para controle de fluxo.

### ✔️ Integrações externas isoladas

Serviços externos são abstraídos na camada de infraestrutura.

---

## 📖 Documentação da API

A API pode ser explorada via Swagger após iniciar o projeto:

```
/swagger
```

---

## 📂 Estrutura do projeto

```
/src
 ├── Domain
 ├── Application
 ├── Infrastructure
 └── API
```

---

## 🧩 Possíveis melhorias futuras

* Adição de testes automatizados
* Implementação de filas para processamento assíncrono
* Pipeline CI/CD
* Logs estruturados
* Monitoramento

---

## 👨‍💻 Autor

Desenvolvido por você 🙂
(adicione aqui seu LinkedIn e GitHub)

---
