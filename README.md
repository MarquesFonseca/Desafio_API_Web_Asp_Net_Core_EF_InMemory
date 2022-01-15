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
 - insomnia (para testar)

### 🔧 Instalação

- Instalar [VisualStudio 2019 Community](https://visualstudio.microsoft.com/pt-br/thank-you-downloading-visual-studio/?sku=Community&rel=16) 
- Instalar Carga de Trabalho no Visual Studio Aplicações Web em .NET Core
- Instalar [POSTMAN Cliente](https://www.postman.com/downloads/) ou 
- Insomnia [Insomnia](https://insomnia.rest/download)

## 🛠️ Construído com

 - C# na versão do Framework .NET Core 3.1 - Web application ASP NET Core

## 📷 Wiki e imagens do projeto rodando

- Acesse aqui a Wiki do projeto[Wiki](https://github.com/MarquesFonseca/Desafio_API_Web_Asp_Net_Core_EF_InMemory/wiki)
  
## ⚙️ Executando os testes

- Clone o repositório https://github.com/MarquesFonseca/Desafio_API_Web_Asp_Net_Core_EF_InMemory.git
- Abra o arquivo Desafio_API_Web_Asp_Net_Core_EF_InMemory.sln com o visual Studio 2019 ou superior.
- Aguarde a Inicialização do aplicativo. 
- Se usar o IIS Express do Visual Studio, irá exibir a página https://localhost:44364/index.html 🤓.
- Se usar a opção de depuração "Desafio_API_Web_Asp_Net_Core_EF_InMemory" do Visual Studio, irá exibir a página https://localhost:5001/index.html 🤓 já pré definida.
- Para acessar os exemplos em json. 
- Se preferir o POSTMAN - Abra o programa Postman e importe para o programa o arquivo de acordo a sua versão na página  https://github.com/MarquesFonseca/Desafio_API_Web_Asp_Net_Core_EF_InMemory/tree/main/Projeto%20teste%20Postman.
- Se preferir o INSOMNIA - Abra o programa Insomnia e importe para ele o arquivo disponível na pasta [Projeto teste Insomnia] no endereço: https://github.com/MarquesFonseca/Desafio_API_Web_Asp_Net_Core_EF_InMemory#:~:text=Projeto%20teste%20Insomnia 
- Execute as requisições HTTP cadastradas. 
- Voce pode executar as requisições tanto no POSTMAN, no INSOMNIA ou mesmo na própria página do swagger.
- Abaixo a Descrição detalhada da coleção de exemplos JSON para o POSTMAN
[x] Pasta Cidade
- [x] Visualizar cidades
   - Lista todas as cidades já pré inseridas no banco.
   - https://localhost:5001/api/cidade

- [x] Visualiza cidade com Id 1
   - Lista a cidade com id = 1
   - https://localhost:5001/api/cidade/1

- [x] Visualiza cidade com Id 2
   - Lista a cidade com id = 2
   - https://localhost:5001/api/cidade/2

- [x] Visualiza cidade com Id 3
   - Lista a cidade com id = 2
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
   - Body - raw - Json
    ```
    {
     "nome" : "Cidade Nova Cidade 1",
     "estadouf" : "TO"
    }
    ```
   - https://localhost:5001/api/cidade/novo

- [x] Cadastrar nova cidade
   - Body - raw - Json
    ```
    {
     "nome" : "Cidade Novo Cidade 2",
     "estadouf" : "GO"
    }
    ```
   - https://localhost:5001/api/cidade/novo

- [x] Cadastrar nova cidade
   Body - raw - Json
    ```
    {
     "nome" : "Cidade Nova Cidade 3",
     "estadouf" : "MG"
    }
    ```
   - https://localhost:5001/api/cidade/novo
   
- [x] Remover cidade Id 1
   - https://localhost:5001/api/cidade/remover/1

- [x] Remover cidade Id 2
   - https://localhost:5001/api/cidade/remover/2
   - 
- [x] Remover cidade Id 3
   - https://localhost:5001/api/cidade/remover/3
   
   
[x] Pasta Cliente
- [x] Visualizar cidades
   - Lista todas as cidades já pré inseridas no banco.
   - https://localhost:5001/api/cidade

   
   

## 📌 Versão

Usei [GitKraken](https://www.gitkraken.com/git-client) para controle de versão. Para as versões disponíveis, observe as [tags neste repositório](https://github.com/MarquesFonseca/Desafio_API_Web_Asp_Net_Core_EF_InMemory/tags). 

## ✒️ Autores

* **Marques Silva Fonseca** - 
- [marquesfonseca](https://github.com/suarezrafael)


