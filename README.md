# SolutionTrack

**SolutionTrack** é uma plataforma web para gestão de erros e soluções de sistemas.

SolutionTrack é uma plataforma desenvolvida em C# com ASP.NET Core que permite o registro, organização e busca de erros e suas soluções em sistemas de software. Ele também permite feedback dos usuários sobre as soluções implementadas e oferece relatórios detalhados sobre os problemas mais recorrentes.

<!-- [![Demo](link-para-demo)](link-para-demo) -->

## Funcionalidades
- Cadastro e gerenciamento de erros e suas soluções.
- Controle de usuários com diferentes níveis de acesso (administrador, usuário comum, cliente).
- Feedback dos usuários sobre as soluções implementadas.
- Sistema de busca avançada para encontrar rapidamente soluções relacionadas a erros específicos.
- Relatórios detalhados sobre os erros mais recorrentes e a eficácia das soluções.


## Tecnologias Utilizadas
- **ASP.NET Core**: Framework para o desenvolvimento do back-end.
- **Entity Framework Core**: ORM para gerenciamento do banco de dados.
- **MySQL**: Banco de dados utilizado.
- **Bootstrap**: Estilização e design responsivo.
<!--- **Identity/OAuth**: Gerenciamento de autenticação e autorização.-->

## Instalação

### Pré-requisitos
- .NET SDK (versão 6.0)
- MySQL (ou outro banco de dados compatível)

### Passos para instalar
1. Clone o repositório:
    ```bash
    git clone https://github.com/kamibiel/SolutionTrack.git
    cd SolutionTrack
    ```
2. Restaure as dependências:
    ```bash
    dotnet restore
    ```
3. Configure o banco de dados:
    - Abra o arquivo `appsettings.json` e atualize a string de conexão:
      ```json
      {
        "ConnectionStrings": {
          "DefaultConnection": "Server=seu-servidor;Database=seu-db;User Id=seu-usuario;Password=sua-senha;"
        }
      }
      ```
4. Aplique as migrações do banco de dados:
    ```bash
    dotnet ef database update
    ```
5. Execute a aplicação:
    ```bash
    dotnet run
    ```

A aplicação estará disponível em `http://localhost:5000`.

## Uso

Após iniciar a aplicação, você pode acessá-la em `http://localhost:5000`. O sistema possui as seguintes funcionalidades principais:

- **Cadastro de Erros**: Acesse a seção de "Cadastro de Erros" para registrar novos problemas.
- **Busca de Soluções**: Utilize a barra de busca para encontrar soluções para erros específicos.
- **Feedback**: Forneça feedback sobre as soluções aplicadas para melhorar continuamente o sistema.
- **Relatórios**: Visualize relatórios detalhados sobre os erros mais comuns e a eficácia das soluções implementadas.

## Contribuição

Contribuições são bem-vindas! Siga os passos abaixo para contribuir:

1. Faça um fork deste repositório.
2. Crie uma nova branch para a sua feature ou correção:
    ```bash
    git checkout -b minha-nova-feature
    ```
3. Faça o commit das suas alterações:
    ```bash
    git commit -m 'Adicionando nova feature'
    ```
4. Envie as alterações para o repositório remoto:
    ```bash
    git push origin minha-nova-feature
    ```
5. Abra um pull request e descreva suas alterações.

### Regras de Contribuição
- **Respeito**: Trate todos os contribuidores com respeito.
- **Qualidade do Código**: Mantenha o código limpo e bem documentado.
- **Testes**: Inclua testes para suas alterações, se aplicável.
- **Descrição Clara**: Descreva claramente as mudanças feitas no pull request.

## Licença

Este projeto está licenciado sob a MIT License - veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## Contato

Para dúvidas, envie um email para [gabriel.o.bonifacio@gmail.com](mailto:gabriel.o.bonifacio@gmail.com).

Siga-me no [LinkedIn](https://www.linkedin.com/in/gabriel-bonif%C3%A1cio-oliveira-403298138/) para mais atualizações.

<!--## Capturas de Tela


![Tela de cadastro de erro](./screenshots/cadastro-erro.png)
![Tela de busca de soluções](./screenshots/busca-solucoes.png)
![Tela de feedback](./screenshots/feedback.png)
-->

<!--## Roadmap

- [ ] Implementar sistema de notificações em tempo real.
- [ ] Adicionar suporte a múltiplos idiomas.
- [ ] Melhorar a interface de usuário com novos temas.

## FAQs

**P:** Como resetar minha senha?  
**R:** Clique em "Esqueci minha senha" na página de login e siga as instruções.

**P:** Posso integrar o SolutionTrack com outras ferramentas?  
**R:** Sim, estamos planejando adicionar APIs para integração com ferramentas populares.

## Autores

- **Gabriel Bonifácio** - *Desenvolvedor Principal* - [kamibiel](https://github.com/kamibiel)

## Referências

- [Documentação do ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core) -->




