# Projeto Loja do Seu Manoel

O projeto é composto por uma aplicação WebAPI .Net que recebe uma lista de pedidos, cada pedido possui uma relação de produtos e as dimensões respectivas.
O objetivo da API é determinar a melhor forma de separar os produtos dentro dos modelos de caixas disponíveis. O retorno é uma lista com o tipo da caixa e os produtos organizados nela.

## Sumário

- [Instalação](#instalação)
- [Execução](#execução)
- [Operação](#operação)
- [Testes](#testes)

## Instalação

Para instalar a aplicação, siga estes passos:

1. Pré requisitos para aplicação:

- .Net SDK 8.0

2. Clone o repositório:

```bash
git clone https://github.com/joseantonio-neto/ManoelStore.git
```

3. Navegue para o diretório da Solution:

```bash
cd ManoelStore
```

4. Instalar as dependências:

```bash
dotnet restore
```

5. Compile o projeto:

```bash
dotnet build
```

## Execução

Para executar a aplicação, siga estes passos:

### Executando na Máquina Local

1. No diretório da Solution, execute o comando abaixo para iniciar a aplicação:

```bash
dotnet run --project ./ManoelStore/ManoelStore.csproj
```

2. O swagger da API pode ser acessado no endereço http://localhost:5020/swagger/index.html. Os endpoints da API podem ser consumidos pelos endereços http://localhost:5020/api/.

### Executando no Docker

1. No diretório da Solution, execute o comando abaixo para iniciar a aplicação:

```bash
docker compose up
```

2. O swagger da API pode ser acessado no endereço http://localhost:8080/swagger/index.html. Os endpoints da API podem ser consumidos pelo endereço http://localhost:8080/api/.

## Operação

Na pasta do projeto tem 2 arquivos que com extensão `.http` com exemplos de requisições. _ManoelStore.http_ para execução e _ManoelStore - Docker.http_ para execução via Docker. Após gerar o token, atualiza a variável `@token` no arquivo.

1. Realizar uma requisição para o endpoint `/api/auth` para geração do Token JWT. O token será utilizado nas demais requisições para API.

Usuário para teste

- E-mail: admin@admin.com
- Senha: 12345

#### Request

```json
POST http://localhost:8080/api/auth/
Content-Type: application/json

{
  "email": "admin@admin.com",
  "password": "12345"
}
```

#### Response

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbiIsImp0aSI6ImZhMmQ4ZjkzLWFkNmItNDkyMS1iYmZlLTJmOTdjZTViNmU0MCIsImV4cCI6MTcyOTIyNDk4MSwiaXNzIjoiTWFub2VsU3RvcmUiLCJhdWQiOiJNYW5vZWxTdG9yZSJ9.kEW0Wm7P2bWTNxOU_RyC-bFfaA13eSa53z7k7U9lrzY",
  "expiration": "2024-10-18T04:16:21Z"
}
```

2. Realizar uma requisição para o endpoint `/api/packing`. Informar o token gerado no endpoint anterior na item Authorization de headers.

#### Request

```json
POST http://localhost:8080/api/packing/
Authorization: Bearer {{token}}
Content-Type: application/json

{
  "pedidos": [
    {
      "pedido_id": 1,
      "produtos": [
        {"produto_id": "PS5", "dimensoes": {"altura": 40, "largura": 10, "comprimento": 25}},
        {"produto_id": "Volante", "dimensoes": {"altura": 40, "largura": 30, "comprimento": 30}}
      ]
    }
  ]
}
```

#### Response

```json
{
  "pedidos": [
    {
      "pedido_id": 1,
      "caixas": [
        {
          "caixa_id": "Caixa 2",
          "produtos": ["PS5", "Volante"]
        }
      ]
    }
  ]
}
```

## Testes

Para rodar os testes da aplicação, siga estes passos:

1. Acessa o diretório da Solution:

```bash
cd ManoelStore
```

2. Rodar o comando:

```bash
dotnet test
```
