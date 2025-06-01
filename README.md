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
| GET    | `/notificacoes/usuario/{usuarioId}` | Retorna uma notificação específica    |
| POST   | `/notificacoes`            | Envia uma nova notificação            |
| POST   | `/notificacoes/notificar`  | Envia uma nova notificação            |
| PUT    | `/notificacoes/{id}`       | Atualiza os dados da notificação      |
| DELETE | `/notificacoes/{id}`       | Exclui uma notificação                |

---

## 📥 Exemplo de Requisição

### 🔸 POST `/notificacoes`

```json
{
  "titulo": "Alerta de Enchente",
  "mensagem": "Chuva intensa nas próximas horas. Evite áreas de risco.",
  "tipoMensagem": "ALERTA",
  "dataEnvio": "2025-05-30T15:00:00",
  "usuarioId": 1
}
