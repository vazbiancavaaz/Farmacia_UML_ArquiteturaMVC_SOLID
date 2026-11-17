[README_Farmacia (3).md](https://github.com/user-attachments/files/34987397/README_Farmacia.3.md)
# Sistema de Gerenciamento de Farmácia

Aplicação desktop desenvolvida em **C#**, utilizando **Windows Forms**, **.NET 8** e **SQLite**, com organização baseada no padrão arquitetural **MVC (Model-View-Controller)** e separação das responsabilidades por meio de repositórios e interfaces.

O sistema permite cadastrar e manter um catálogo de medicamentos, adicionar produtos a um carrinho, alterar quantidades e registrar a finalização de uma compra em um banco de dados local.

## Sumário

- [Sobre o projeto](#sobre-o-projeto)
- [Funcionalidades](#funcionalidades)
- [Tecnologias utilizadas](#tecnologias-utilizadas)
- [Organização do projeto](#organização-do-projeto)
- [Arquitetura e responsabilidades](#arquitetura-e-responsabilidades)
- [Banco de dados](#banco-de-dados)
- [Requisitos](#requisitos)
- [Como executar](#como-executar)
- [Regras e validações](#regras-e-validações)
- [Observações](#observações)

## Sobre o projeto

O projeto tem como objetivo aplicar conceitos de desenvolvimento de software em um sistema de gerenciamento de farmácia. A estrutura foi organizada para separar a interface, as regras de controle e o acesso aos dados, facilitando a leitura e a manutenção do código.

A aplicação utiliza um banco SQLite local. Na primeira inicialização, a classe de conexão verifica e cria as tabelas necessárias caso ainda não existam.

## Funcionalidades

### Catálogo de medicamentos

- Listagem dos medicamentos cadastrados.
- Cadastro de um novo medicamento com nome, descrição e preço.
- Edição das informações de um medicamento.
- Exclusão de um medicamento, mediante confirmação.
- Atualização da listagem após as operações.

### Carrinho de compras

- Adição de medicamentos ao carrinho.
- Exibição dos itens selecionados.
- Alteração da quantidade de cada item.
- Remoção de itens do carrinho.
- Cálculo do subtotal por item e do total da compra.
- Finalização da compra e gravação dos dados no SQLite.
- Limpeza do carrinho após a conclusão da compra.

### Persistência dos dados

- Armazenamento local em SQLite.
- Criação automática das tabelas na inicialização.
- Uso de comandos SQL parametrizados.
- Uso de transação para registrar a compra, seus itens e a limpeza do carrinho como uma única operação.

## Tecnologias utilizadas

| Tecnologia | Utilização |
|---|---|
| C# | Linguagem de programação |
| .NET 8 | Plataforma de desenvolvimento |
| Windows Forms | Construção da interface desktop |
| SQLite | Banco de dados local |
| Microsoft.Data.Sqlite | Integração entre a aplicação e o SQLite |
| Visual Studio | Ambiente recomendado para abrir e executar a solução |

## Organização do projeto

A estrutura principal está dividida da seguinte forma:

```text
Farmacia/
├── Controller/
│   ├── CarrinhoController.cs
│   └── RemedioController.cs
│
├── Dados/
│   ├── CarrinhoRepository.cs
│   ├── Conexao.cs
│   ├── ICarrinhoRepository.cs
│   ├── IRepository.cs
│   └── RemedioRepository.cs
│
├── Model/
│   ├── Compra.cs
│   ├── ItemCarrinho.cs
│   └── Remedio.cs
│
├── View/
│   ├── CadastroRemedioForm.cs
│   ├── CadastroRemedioForm.Designer.cs
│   ├── CadastroRemedioForm.resx
│   ├── CarrinhoForm.cs
│   ├── EditarRemedioForm.cs
│   ├── EditarRemedioForm.Designer.cs
│   ├── EditarRemedioForm.resx
│   ├── Form1.cs
│   ├── Form1.Designer.cs
│   └── Form1.resx
│
├── Properties/
├── Program.cs
├── Farmacia.csproj
├── Farmacia.sln
└── farmacia.db
```

### Descrição das pastas

- **Controller:** recebe as solicitações feitas pela interface, aplica validações e encaminha as operações aos repositórios.
- **Dados:** concentra a conexão com o banco e as operações de consulta, inclusão, alteração e exclusão. Também contém as interfaces dos repositórios.
- **Model:** contém as classes que representam os dados utilizados pelo sistema.
- **View:** reúne as telas e os elementos de interface com os quais o usuário interage.
- **Properties:** contém recursos e configurações do projeto.
- **Program.cs:** inicializa a aplicação e abre a tela principal.

## Arquitetura e responsabilidades

### MVC (Model-View-Controller)

O projeto utiliza a separação de responsabilidades do MVC:

**Model**
- `Remedio`: representa um medicamento, com identificador, nome, descrição e preço.
- `ItemCarrinho`: representa um medicamento no carrinho, incluindo quantidade, preço unitário e subtotal.
- `Compra`: representa o resultado de uma compra, com identificador e valor total.

**View**
- `Form1`: tela principal, com a listagem e as ações do catálogo.
- `CadastroRemedioForm`: formulário de cadastro de medicamentos.
- `EditarRemedioForm`: formulário de edição.
- `CarrinhoForm`: tela de gerenciamento do carrinho e finalização da compra.

**Controller**
- `RemedioController`: coordena as operações do catálogo e valida os dados do medicamento.
- `CarrinhoController`: coordena as operações do carrinho e valida identificadores, quantidades e a identificação da operação de compra.

### Repositórios e interfaces

O acesso ao banco fica separado das telas e dos controllers:

- `IRepository<T>` define operações comuns de listagem, inclusão, atualização e exclusão.
- `ICarrinhoRepository` define as operações específicas do carrinho e da finalização da compra.
- `RemedioRepository` implementa o acesso aos dados dos medicamentos.
- `CarrinhoRepository` implementa o acesso aos dados do carrinho e das compras.
- `Conexao` configura e abre conexões SQLite e garante a criação das tabelas.

Os controllers recebem os repositórios por construtor. Essa organização reduz o acoplamento entre as regras de controle e a implementação concreta do banco de dados.

### Relação com os princípios SOLID

A estrutura aplica conceitos relacionados aos princípios SOLID, principalmente:

- **Responsabilidade Única (SRP):** telas, controllers, modelos e repositórios possuem funções separadas.
- **Aberto/Fechado (OCP):** o uso de interfaces permite criar outras implementações de repositório sem precisar reescrever a lógica dos controllers.
- **Substituição de Liskov (LSP):** implementações que respeitem os contratos das interfaces podem ser utilizadas no lugar das abstrações correspondentes.
- **Segregação de Interfaces (ISP):** as operações do catálogo e do carrinho estão separadas em interfaces diferentes.
- **Inversão de Dependência (DIP):** os controllers dependem de interfaces de repositório, e não diretamente das classes concretas de persistência.

## Banco de dados

O banco utilizado é o **SQLite**, armazenado no arquivo `farmacia.db`.

As tabelas criadas pela aplicação são:

| Tabela | Finalidade |
|---|---|
| `Remedios` | Armazena o catálogo de medicamentos. |
| `CarrinhoItens` | Armazena os itens que estão no carrinho e suas quantidades. |
| `Compras` | Registra as compras finalizadas, com data, identificador da operação e total. |
| `CompraItens` | Armazena os itens correspondentes a cada compra. |

### Estrutura geral dos dados

- `Remedios`: `Id`, `Nome`, `Descricao` e `PrecoCentavos`.
- `CarrinhoItens`: `RemedioId`, `Quantidade` e `PrecoUnitarioCentavos`.
- `Compras`: `Id`, `Operacao`, `DataUtc` e `TotalCentavos`.
- `CompraItens`: `Id`, `CompraId`, `RemedioId`, `NomeRemedio`, `Quantidade` e `PrecoUnitarioCentavos`.

Os valores monetários são armazenados em centavos como números inteiros, evitando depender de valores decimais no armazenamento. A aplicação converte esses valores para o formato monetário ao exibi-los.

A conexão ativa as chaves estrangeiras do SQLite. A tabela `CarrinhoItens` possui relacionamento com `Remedios`, e `CompraItens` possui relacionamento com `Compras`.

## Requisitos

Para executar o projeto, é necessário:

- Windows, pois a aplicação utiliza Windows Forms.
- Visual Studio 2022 com suporte ao desenvolvimento para desktop com .NET.
- SDK do .NET 8.
- Acesso aos pacotes NuGet para restaurar a dependência `Microsoft.Data.Sqlite`.

## Como executar

### Pelo Visual Studio

1. Baixe ou clone este repositório.
2. Extraia o arquivo ZIP, caso tenha baixado o projeto compactado.
3. Abra o arquivo `Farmacia.sln` no Visual Studio.
4. Aguarde a restauração dos pacotes NuGet.
5. Selecione a configuração `Debug` ou `Release`.
6. Execute o projeto com **F5** ou **Ctrl + F5**.

### Pelo terminal

Abra o terminal na pasta que contém o arquivo `Farmacia.csproj` e execute:

```bash
dotnet restore
dotnet build
dotnet run
```

Como o projeto utiliza Windows Forms e tem como destino `net8.0-windows`, a execução deve ser feita em um ambiente Windows com o SDK correspondente instalado.

## Regras e validações

O sistema possui algumas validações para evitar dados inválidos:

- O nome do medicamento é obrigatório.
- O preço deve ser maior que zero e ter, no máximo, duas casas decimais.
- O preço é informado e apresentado no padrão monetário brasileiro, como `12,90`.
- A quantidade de cada medicamento no carrinho deve ficar entre 1 e 999 unidades.
- Não é permitido finalizar uma compra com o carrinho vazio.
- A identificação da operação de compra deve ser um GUID válido.
- A finalização registra a compra e seus itens dentro de uma transação. O carrinho é limpo somente junto com a confirmação dessa operação.

## Observações

- O projeto é uma aplicação desktop local; não é uma aplicação web.
- O banco SQLite é mantido em arquivo no ambiente em que o programa é executado.
- O projeto registra as compras e seus itens no banco. A estrutura atual não apresenta uma tela específica de consulta ao histórico de compras.
- Não há, na implementação apresentada, integração com serviços de pagamento, controle de usuários ou autenticação.
- Para manter o repositório organizado, arquivos temporários e de compilação do Visual Studio, como `.vs/`, `bin/` e `obj/`, normalmente não precisam ser enviados ao GitHub. O arquivo `.gitignore` pode ser configurado para ignorá-los.

---

Desenvolvido como projeto de estudo para aplicação de conceitos de C#, Windows Forms, banco de dados SQLite, arquitetura MVC e princípios SOLID.
