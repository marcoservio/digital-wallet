# API de Carteiras Digitais

Este projeto é uma API RESTful desenvolvida em **C# com ASP.NET Core**, que gerencia carteiras digitais e transações financeiras entre usuários.

## Funcionalidades

- Autenticação com JWT (Bearer Token)
- Cadastro de usuários
- Consulta de saldo da carteira
- Adição de saldo
- Transferência entre carteiras
- Listagem de transferências com filtro opcional por período

## Tecnologias

- **.NET 9**
- **PostgreSQL**
- **Docker**
- **Entity Framework Core**
- **Redis**
- **xUnit + FluentAssertions** (testes)
- **JWT** para autenticação
- **Swagger** para documentação da API

## Como executar o projeto com Docker

1. Clone o repositório:

   ```bash
   git clone https://github.com/marcoservio/digital-wallet.git
   cd digital-wallet
   ```

2. Execute os containers:

   ```bash
   docker-compose up -d
   ```

3. Acesse a aplicação:
   - API: [http://localhost:12345](http://localhost:12345)
   - Swagger: [http://localhost:12345/swagger](http://localhost:12345/swagger)

## Script de popular o banco de dados

Um seed automático é executado ao subir o container da aplicação. Ele insere:

- 3 usuários fictícios
- Saldos iniciais em suas carteiras
- Algumas transferências entre eles

## Autenticação

Use o endpoint de login para obter um token JWT:

```http
POST /login

"email": "admin@gmail.com"
"password": "root"
```

Inclua o token como Bearer em todas as requisições autenticadas:

```
Authorization: Bearer {seu_token}
```

## Testes

Para rodar os testes automatizados:

```bash
dotnet test
```

## Linter

Usamos `dotnet format` para manter o código limpo. Execute:

```bash
dotnet format
```

---

## Contato

- Em caso de dúvidas, envie um email para: marcoservio22@hotmail.com
- Linkedin: [https://www.linkedin.com/in/marcoservio/](https://www.linkedin.com/in/marcoservio/)
