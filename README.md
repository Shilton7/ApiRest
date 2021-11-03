<h1 align="center">REST com ASP.NET Core WebAPI</h1>

<p align="center">
  <a href="#-tecnologias">Tecnologias</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#-projeto">Projeto</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#-instalação">Instalação</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#-testes">Testes</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#-deploy">Deploy</a>&nbsp;&nbsp;&nbsp;
</p>

## ✨ Tecnologias

Esse projeto foi desenvolvido com as seguintes tecnologias:


- [x] [JWT](https://jwt.io/)
- [x] [Docker](https://www.docker.com/)
- [x] [Swagger](https://swagger.io/)
- [x] [Postman](https://www.postman.com/)
- [x] [Elmah.io](https://elmah.io/)
- [x] [AutoMapper](https://automapper.org/)
- [x] [HealthChecks](https://docs.microsoft.com/pt-br/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.1)
- [x] [API versioning](https://github.com/dotnet/aspnet-api-versioning)
- [x] [Identity Server](https://docs.microsoft.com/pt-br/aspnet/identity/overview/getting-started/introduction-to-aspnet-identity)
- [x] [Fluent Validation](https://fluentvalidation.net/)
- [x] [ASP.NET Core 3.1](https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-3.1)
- [x] [Microsoft SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-2019)
- [x] [Entity Framework Core](https://docs.microsoft.com/pt-br/ef/core/)



## 💻 Projeto

Este projeto é uma aplicação REST usando ASP.NET Core WebApi 3.1, que simula um fluxo de CRUD de produtos, fornecedores e endereços para uma empresa. </br></br>
O estilo arquitetural aplicado foi o Tier Architecture, onde temos a distribuição de responsabilidades dívidas em 3 camadas: apresentação UI (API), aplicação (Business) e acesso a dados (Data). Pensando em evitar duplicidade de código, diminuir o acoplamento (IoC) e evitar a complexidade os princípios SOLID, DRY, KISS e YAGNI foram aplicados.</br></br>
No quesito segurança, implementei o JWT juntamente com a autenticação/autorização utilizando IdentityServer e seus recursos de Authorize, AllowAnonymous e Claims.
Toda parte de monitoramento de log foi desenhada para armazenar as exceções na ferramenta Elmah.io, a fim de conseguir administrar e atuar nos logs ali presentes.
Para documentação e versionamento visando atualizações futuras, foi usado as ferramentas Versioning ApiExplorer e Swagger.
Como ponte de monitoramento da saúde dos endpoints e do ambiente foi implantando o uso de HealthChecks;


## 🚀 Instalação

Essas instruções fornecerão uma cópia do projeto instalado e funcional para fins de desenvolvimento e testes.

### 1º Passo - Clonar o respositório
Comece clonando esse projeto para sua máquina local.
```sh
git clone https://github.com/Shilton7/ApiRest.git
```

### 2º Passo - Banco de dados (Migrations)
```sh
- CRUD (Solution Data): update-database -Context
- IdentityServer (Solution Api): update-database -Context ApplicationDbContext
```

![](https://i.imgur.com/JMw272S.png)
![](https://i.imgur.com/FBKlx1x.png)

### 3º Passo - Configurar o ambiente
Para configurar o ambiente é necessário alterar as informações dos arquivos `appsettings` para as informações correspondentes a conexão do seu banco de dados, chaves do JWT e Logger.</br>
```sh
- Ambiente localhost/desenvolvimento: appsettings.Development.json
- Ambiente homologação/testes: appsettings.Staging.json
- Ambiente produção: appsettings.Production.json
```

### 4º Passo - Executando a aplicação
Depois de tudo configurado basta setar a Solution Api como o principal (Set as Startup project). </br>
Agora você pode acessar [`localhost:5000`](http://localhost:5000) do seu navegador.

## 💻 Testes

Para os testes via postman importe as collections e environments presentes na pasta postman desse projeto:
```sh
- Collections: https://github.com/Shilton7/ApiRest/tree/main/postman/collections
- Environments: https://github.com/Shilton7/ApiRest/tree/main/postman/environments
```
<strong>Postman </strong>
![](https://i.imgur.com/1GEOoS4.png)

<strong> Swagger </strong>

![](https://i.imgur.com/h7aosJs.png)

## 💻 Deploy

Para fazer o up do ambiente usando containers é necessário alterar as informações dos arquivos do docker-compose para as de sua preferência:</br>
```sh
cd .\ApiRest\deploy\
- Ambiente localhost/desenvolvimento: docker-compose -f nome-do-arquivo.yml up
- Ambiente produção: docker-compose -f api_rest_production.yml up
```


![](https://i.imgur.com/D4dD8Bg.png)

