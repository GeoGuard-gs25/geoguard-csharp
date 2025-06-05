# 🌪️ GeoGuard - Sistema de Notificações de Desastres Naturais

Este projeto foi desenvolvido como parte do Global Solution da **FIAP**, com foco na disciplina de **Desenvolvimento Web com ASP.NET Core**.

O sistema **GeoGuard** tem como objetivo notificar **usuários cadastrados** sobre **possíveis catástrofes naturais**, como enchentes, deslizamentos, terremotos ou incêndios, permitindo ações preventivas e informadas.

---

## 📚 Tecnologias Utilizadas

- ASP.NET Core Web API  
- Entity Framework Core  
- Banco de Dados Oracle  
- C#  
- Visual Studio  
- Swagger (documentação e testes de endpoints)  
- Postman (testes manuais de API)

---

## 🎯 Funcionalidades

### 👤 Usuários

- ✅ Cadastro de novos usuários  
- ✅ Listagem de todos os usuários  
- ✅ Consulta de usuário por ID  
- ✅ Atualização de dados cadastrais  
- ✅ Exclusão de usuários  

### 📢 Notificações

- ✅ Envio de novas notificações  
- ✅ Listagem de todas as notificações  
- ✅ Consulta de notificação por ID  
- ✅ Atualização de notificações existentes  
- ✅ Remoção de notificações  

---

**## 📦 Entidades do Banco de Dados

### 🧑‍💻 Usuario

**Atributos:**

- **Id**: Identificador único do usuário.
- **Nome**: Nome completo do usuário. Obrigatório e limitado a 100 caracteres.
- **Email**: Endereço de e-mail válido. Obrigatório.
- **Senha**: Senha do usuário. Obrigatória, com no mínimo 6 e no máximo 100 caracteres.
- **Localizacao**: Localização atual do usuário. Opcional, com até 150 caracteres.
- **Notificacoes**: Lista de notificações relacionadas ao usuário (relacionamento 1:N com a entidade Notificacao).

---

### 🔔 Notificacao

**Atributos:**

- **Id**: Identificador único da notificação.
- **Titulo**: Título da notificação. Obrigatório, com até 100 caracteres.
- **Mensagem**: Texto da mensagem da notificação. Obrigatório, com até 500 caracteres.
- **TipoMensagem**: Tipo da mensagem (ex: alerta, aviso, informativo). Opcional, com até 50 caracteres.
- **DataEnvio**: Data e hora do envio da notificação. Por padrão, é preenchido com a data/hora atual no formato UTC.
- **UsuarioId**: Chave estrangeira que referencia o usuário que recebeu a notificação.
- **Usuario**: Relacionamento com a entidade Usuario (propriedade de navegação).


## 📁 Estrutura do Projeto

```bash
GeoGuard-GS/
├── Controllers/
│   ├── NotificacaoController.cs
│   └── UsuarioController.cs
│
├── Data/
│   ├── AppDbContext.cs
│   └── Mappings/
│       ├── NotificacaoMapping.cs
│       └── UsuarioMapping.cs
│
├── Exceptions/
│   ├── NotificacaoException.cs
│   └── UsuarioException.cs
│
├── Migrations/
│   (arquivos de migração do Entity Framework)
│
├── Model/
│   ├── DTO/
│   │   ├── NotificacaoCreateDto.cs
│   │   ├── NotificacaoUpdateDto.cs
│   │   ├── UsuarioCreateDto.cs
│   │   └── UsuarioUpdateDto.cs
│   ├── Notificacao.cs
│   └── Usuario.cs
│
├── Services/
│   ├── Abstractions/
│   │   ├── INotificacaoService.cs
│   │   └── IUsuarioService.cs
│   ├── NotificacaoService.cs
│   └── UsuarioService.cs
│
├── appsettings.json
├── GeoGuard-GS.csproj
├── GeoGuard-GS.http
└── Program.cs
````

## 🔗 Rotas da API

### 👤 Usuários

| Método | Endpoint             | Descrição                        |
|--------|----------------------|----------------------------------|
| GET    | `/usuarios`          | Lista todos os usuários          |
| GET    | `/usuarios/{id}`     | Retorna um usuário específico    |
| GET    | `/usuarios/buscar`     | Retorna um usuário pelo email    |
| POST   | `/usuarios`          | Cadastra um novo usuário         |
| PUT    | `/usuarios/{id}`     | Atualiza os dados de um usuário  |
| DELETE | `/usuarios/{id}`     | Remove um usuário                |

### 📢 Notificações

| Método | Endpoint                   | Descrição                             |
|--------|----------------------------|---------------------------------------|
| GET    | `/notificacoes`            | Lista todas as notificações           |
| GET    | `/notificacoes/{id}`       | Retorna uma notificação específica    |
| GET    | `/notificacoes/usuario/{usuarioId}`| Retorna uma notificaçã do user|
| POST   | `/notificacoes`            | Envia uma nova notificação            |
| PUT    | `/notificacoes/{id}`       | Atualiza os dados da notificação      |
| DELETE | `/notificacoes/{id}`       | Exclui uma notificação                |

---

## 📥 Exemplo de Testes das requisições - Notificações

### 🔸 GET `/usuarios`
```json
[
    {
        "id": 22,
        "nome": "Artur Silva Vaz",
        "email": "artur@email.com",
        "senha": "artur123",
        "localizacao": "São Paulo"
    },
    {
        "id": 23,
        "nome": "Gabriel Siqueira",
        "email": "gabrieç@email.com",
        "senha": "gabriel123",
        "localizacao": "Rio de Janeiro"
    },
    {
        "id": 21,
        "nome": "João da Silva",
        "email": "joao@email.com",
        "senha": "vaitimao",
        "localizacao": "São Paulo"
    }
]
````
![image](https://github.com/user-attachments/assets/f94159e1-adb7-4f6c-9bee-61e74b827c7a)

### 🔸 POST `/usuarios`

```json
 {
    "nome": "João da Silva",
    "email": "joao@email.com",
    "senha": "vaitimao",
    "localizacao": "São Paulo"
  }
````
![image](https://github.com/user-attachments/assets/c50d3c4c-68c0-4b27-a34a-eefa8f8a7f9a)

### 🔸 GET `/usuarios/21`

```json
 {
    "id": 21,
    "nome": "João da Silva",
    "email": "joao@email.com",
    "senha": "vaitimao",
    "localizacao": "São Paulo"
  }
````
![image](https://github.com/user-attachments/assets/191caa6d-2876-4e08-90da-8ea47148888a)

### 🔸 GET `/usuarios/buscar?email=artur@email.com`

```json
{
    "id": 22,
    "nome": "Artur Silva Vaz",
    "email": "artur@email.com",
    "senha": "artur123",
    "localizacao": "São Paulo"
}
````
![image](https://github.com/user-attachments/assets/e34dcb79-fc18-441e-ad3b-958829497a00)

### 🔸 PUT `/usuarios/22`

```json
{
    "id": 22,
    "nome": "Pedro Silva",
    "email": "pedro@email.com",
    "senha": "pedro123",
    "localizacao": "São Caetano"
}
````
![image](https://github.com/user-attachments/assets/a8d9144c-a8ee-47bc-80b4-50fd19f9d5af)

### 🔸 DELETE `/usuarios/22`
![image](https://github.com/user-attachments/assets/1c0fbd6f-100c-47b3-a6fd-d8495232e4d6)

Sem corpo. Retorna status 204 (No Content).

--------------------------------------------------------------

## 📥 Exemplo de Requisição - Notificações

### 🔸 GET `/notificacoes`
```json
[
  {
    "id": 21,
    "titulo": "Alerta de Enchente",
    "mensagem": "Chuva intensa nas próximas horas. Evite áreas de risco.",
    "tipoMensagem": "ALERTA",
    "dataEnvio": "2025-05-30T15:00:00",
    "usuarioId": 21
  }
]
````
![image](https://github.com/user-attachments/assets/d64c945b-0a90-4d75-9e8a-583851505241)

### 🔸 POST `/notificacoes`

```json
{
  "titulo": "Alerta de Enchente",
  "mensagem": "Chuva intensa nas próximas horas. Evite áreas de risco.",
  "tipoMensagem": "ALERTA",
  "dataEnvio": "2025-05-30T15:00:00",
  "usuarioId": 21
}
````
![image](https://github.com/user-attachments/assets/05c2549a-ece1-4fbd-b6c4-83006b51aded)

### 🔸 GET `/notificacoes/22`

```json
{
  "id": 22,
  "titulo": "Alerta de Enchente",
  "mensagem": "Chuva intensa nas próximas horas. Evite áreas de risco.",
  "tipoMensagem": "ALERTA",
  "dataEnvio": "2025-05-30T15:00:00",
  "usuarioId": 22
}
````
![image](https://github.com/user-attachments/assets/406bdb47-3b6b-40aa-830e-9c9f8798b3ca)

### 🔸 GET `/notificacoes/usuario/21`

```json
[
  {
    "id": 21,
    "titulo": "Alerta de Enchente",
    "mensagem": "Chuva intensa nas próximas horas. Evite áreas de risco.",
    "tipoMensagem": "ALERTA",
    "dataEnvio": "2025-05-30T15:00:00",
    "usuarioId": 21
  }
]
````
![image](https://github.com/user-attachments/assets/fdce53d5-7f42-44ca-9aa7-903fe0d74951)

### 🔸 PUT `/notificacoes/21`

```json
{
  "titulo": "Alerta de Enchente Atualizado",
  "mensagem": "Chuva intensa continua. Atenção redobrada.",
  "tipoMensagem": "ALERTA",
  "dataEnvio": "2025-05-30T16:00:00",
  "usuarioId": 21
}
````
![image](https://github.com/user-attachments/assets/389e7a9b-f44b-426a-bbb3-ad4b2f30079e)

### 🔸 DELETE `/notificacoes/21`

![image](https://github.com/user-attachments/assets/3af80bf4-4e4f-48d2-8101-1ff40592f6fb)

Sem corpo. Retorna status 204 (No Content).
