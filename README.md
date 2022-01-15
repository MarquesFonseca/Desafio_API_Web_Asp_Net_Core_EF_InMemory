# :cloud: API Asp NET Core 3.1 Entity Framework In Memory (na memória)

Tabelas usadas:
- [x] Cidade
- [x] Cliente 

O que a API faz:

- [x] Cadastrar cidade (nome, estado)
- [x] Exibir todas as cidades
- [x] Consulta cidade pelo Id
- [x] Consulta cidade pelo Nome ou parte do nome
- [x] Consulta cidade pelo Estado
- [x] Altera cidade
- [x] Remove cidade (se não existir cliente vinculada à ela)
- [x] Cadastrar cliente (Nome completo, Sexo, Data de nascimento, idade(retorna atualizado), Cidade e Data de cadastro)
- [x] Exibir todos os clientes
- [x] Consultar cliente pelo Id 
- [x] Consultar cliente pelo nome ou parte do nome
- [x] Exibir todos os clientes de uma cidade específica(pelo Id da cidade)
- [x] Altera cliente
- [x] Remover cliente

### 📋 Ferramentas utilizadas
 - Visual Studio 2019
 - Visual Studio Code
 - POSTMAN (para testar) ou
 - INSOMNIA (para testar)

### 🔧 Instalação

- Instalar [VisualStudio 2019 Community](https://visualstudio.microsoft.com/pt-br/thank-you-downloading-visual-studio/?sku=Community&rel=16) 
- Instalar Carga de Trabalho no Visual Studio Aplicações Web em .NET Core
- Instalar o pacote do [Swagger] na versão 5.6.3 na linha de comando [dotnet add package Swashbuckle.AspNetCore --version 5.6.3] - ou pelo pelo [Nuget] do próprio gerenciador de pacotes do Visual Studio.
- Instalar o pacore do [Microsoft.EntityFrameworkCore.InMemory] na versão 5.0.13 na linha de comando [dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 5.0.13] - ou pelo pelo [Nuget] do próprio gerenciador de pacotes do Visual Studio.
- Instalar o pacore do [Microsoft.EntityFrameworkCore] na versão 5.0.13 na linha de comando [dotnet add package Microsoft.EntityFrameworkCore --version 5.0.13] - ou pelo pelo [Nuget] do próprio gerenciador de pacotes do Visual Studio.
- Instalar [POSTMAN Cliente](https://www.postman.com/downloads/) ou 
- Insomnia [INSOMNIA](https://insomnia.rest/download)

## 🛠️ Construído com

 - C# na versão do Framework .NET Core 3.1 - Web application ASP NET Core
 
## ⚙️ Executando os testes

- Clone o repositório https://github.com/MarquesFonseca/Desafio_API_Web_Asp_Net_Core_EF_InMemory.git
- Abra o arquivo Desafio_API_Web_Asp_Net_Core_EF_InMemory.sln com o visual Studio 2019 ou superior.
- Aguarde a Inicialização do aplicativo. 
- Se usar o IIS Express do Visual Studio, irá exibir a página https://localhost:44364/index.html 🤓.
- Se usar a opção de depuração "Desafio_API_Web_Asp_Net_Core_EF_InMemory" do Visual Studio, irá exibir a página https://localhost:5001/index.html 🤓 já pré definida.
- Para acessar os exemplos em json. 
- Se preferir o POSTMAN - Abra o programa Postman e importe para o programa o arquivo de acordo a sua versão na página  https://github.com/MarquesFonseca/Desafio_API_Web_Asp_Net_Core_EF_InMemory/tree/main/Projeto%20teste%20Postman.
- Se preferir o INSOMNIA - Abra o programa Insomnia e importe para ele o arquivo disponível na pasta [Projeto teste Insomnia] no endereço: https://github.com/MarquesFonseca/Desafio_API_Web_Asp_Net_Core_EF_InMemory/tree/main/Projeto%20teste%20Insomniaa 
- Execute as requisições HTTP cadastradas. 
- Voce pode executar as requisições tanto no POSTMAN, no INSOMNIA ou mesmo na própria página do swagger.
- Abaixo a Descrição detalhada da coleção de exemplos JSON para o POSTMAN
[x] Pasta Cidade
- [x] Visualizar cidades
   - Lista todas as cidades já pré inseridas no banco.
   - https://localhost:5001/api/cidade

- [x] Visualiza cidade com Id 1
   - https://localhost:5001/api/cidade/1

- [x] Visualiza cidade com Id 2
   - https://localhost:5001/api/cidade/2

- [x] Visualiza cidade com Id 3
   - https://localhost:5001/api/cidade/3

- [x] Alterar cidade com Id 1
   - https://localhost:5001/api/cidade/alterar/1
   - Body - raw - Json
   ```
   {
    "nome" : "Cidade 1 - Com alteração",
    "estadouf" : "AC"
   }
   ```

- [x] Alterar cidade com Id 2
   - https://localhost:5001/api/cidade/alterar/2
   - Body - raw - Json
   ```
   {
    "nome" : "Cidade 2 - Com alteração",
    "estadouf" : "TO"
   }
   ```

- [x] Alterar cidade com Id 3
   - https://localhost:5001/api/cidade/alterar/3
   - Body - raw - Json
   ```
   {
    "nome" : "Cidade 3 - Com alteração",
    "estadouf" : "MA"
   }
   ```

- [x] Cadastrar nova cidade
   - https://localhost:5001/api/cidade/novo
   - Body - raw - Json
    ```
    {
     "nome" : "Cidade Nova Cidade 1",
     "estadouf" : "TO"
    }
    ```

- [x] Cadastrar nova cidade
   - https://localhost:5001/api/cidade/novo
   - Body - raw - Json
    ```
    {
     "nome" : "Cidade Novo Cidade 2",
     "estadouf" : "GO"
    }
    ```

- [x] Cadastrar nova cidade
   - https://localhost:5001/api/cidade/novo
   - Body - raw - Json
    ```
    {
     "nome" : "Cidade Nova Cidade 3",
     "estadouf" : "MG"
    }
    ```
   
- [x] Remover cidade Id 1
   - https://localhost:5001/api/cidade/remover/1

- [x] Remover cidade Id 2
   - https://localhost:5001/api/cidade/remover/2
   - 
- [x] Remover cidade Id 3
   - https://localhost:5001/api/cidade/remover/3

----------------------------------------------------------------------
   
[x] Pasta Cliente
- [x] Visualizar Clientes
   - Lista todas os clientes já pré inseridas no banco.
   - https://localhost:5001/api/clientes

- [x] Pesquisar cliente com Id 1
   - https://localhost:5001/api/cliente/1

- [x] Pesquisar cliente pelo Nome ou parte do Nome
   - ex: ...nome/'silva' ou ...nome/'marques'
   - https://localhost:5001/api/cliente/pesquisar/nome/silva

- [x] Pesquisar todos os clientes que pertencem à cidade id 1
   - https://localhost:5001/api/cliente/pesquisar/cidade/1
   
- [x] Alterar o cliente id 1
   - https://localhost:5001/api/cliente/alterar/2 
   - Body - raw - Json
    ```
   {
		"nomeCompleto": "Cliente depois da alteração",
		"sexo": "M",
		"dataNascimento": "1999-01-02T00:00:00",
		"idade": 0,
		"cidadeId":2		
   }
    ```   

- [x] Cadastrar um novo cliente para a cidade id 1
   - https://localhost:5001/api/cliente/novo 
   - Body - raw - Json
    ```
   {
      "nomecompleto" : "Lucirene Ferreira da Silva",
      "sexo" : "F",
      "datanascimento" : "1959-12-06",
      "idade" : 0,
      "cidadeid" : 1
   }
    ```   

- [x] Cadastrar um novo cliente para a cidade id 1
   - https://localhost:5001/api/cliente/novo 
   - Body - raw - Json
    ```
   {
      "nomecompleto" : "Roberto da Silva",
      "sexo" : "M",
      "datanascimento" : "1992-11-15",
      "idade" : 0,
      "cidadeid" : 1
   }
    ```   
- [x] Cadastrar um novo cliente para a cidade id 2
   - https://localhost:5001/api/cliente/novo 
   - Body - raw - Json
    ```
   {
      "nomecompleto" : "Francisca Silva Fonseca",
      "sexo" : "F",
      "datanascimento" : "1995-09-05",
      "idade" : 0,
      "cidadeid" : 2
   }
    ```   

- [x] Cadastrar um novo cliente para a cidade id 2
   - https://localhost:5001/api/cliente/novo 
   - Body - raw - Json
    ```
   {
      "nomecompleto" : "Rafael Suarez",
      "sexo" : "M",
      "datanascimento" : "1986-07-20",
      "idade" : 0,
      "cidadeid" : 2
   }
    ```       
- [x] Remover Cliente id 1
   - https://localhost:5001/api/cliente/remover/1


## 📌 Versão

Usei [GitKraken](https://www.gitkraken.com/git-client) para controle de versão. Para as versões disponíveis, observe as [tags neste repositório](https://github.com/MarquesFonseca/Desafio_API_Web_Asp_Net_Core_EF_InMemory/tags). 

## ✒️ Autores

* **Marques Silva Fonseca** - 
- [Link Github:](https://github.com/MarquesFonseca)
- [Email:](mailto:marques-fonseca@hotmail.com) marques-fonseca@hotmail.com
- [Email:](mailto:marquesfonseca@gmail.com) marquesfonseca@gmail.com
- Telefone: (63) 99208-2226


