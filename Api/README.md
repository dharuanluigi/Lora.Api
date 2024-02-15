# Challange
Nesse repositório contém os arquivos necessários para a entrega do desafio técnico. O desafio era necessário criar um contato inteligente que falasse sobre os 6 princípios da empresa e mais uma api-gateway para fazer a busca pelos últimos 5 repositórios da linguagem C#.

## Arquivos
Dentro do repositório contém uma pasta chamada: `Api` e uma chamada `Flow`.

1. `Api`: Contém todo o código fonte da api-gateway usada pelo contato inteligente.
2. `Flow`: Contém o arquivo JSON do contato inteligente.

### Reprodução

* Flow - Para reproduzir o fluxo do chatbot por conta própria:
    1. Faça o download do arquivo que está dentro da pasta `Flow/takevalues1.json`;
    2. Crie um chatbot do zero na plataforma BLIP;
    3. No menu vertical a esquerda, clique no icone de chave(configurações);
    4. Ao abrir uma janela vertical a direita, selecione na aba a opção `VERSIONS`;
    5. Em seguida, selecione a opção `Upload flow`; 
    6. Ao abrir a janela para selecionar o arquivo, selecione o arquivo: `takevalues1.json` que foi baixado no passo 1.

* API - Para acessar a documentação da API basta acessar: [AQUI](https://lora-api.fly.dev/swagger/index.html). Caso queira fazer o teste local, siga os passos:
    1. Clone o repositório como preferir;
    2. Após clonar o repositório, dentro da pasta root do projeto: `/Api`, abra o terminal e digite o comando: `dotnet restore`. Para isso é necessário ter instalado o ***.NET 6.0 LTS***. Mais informações de como instalar: [AQUI](https://dotnet.microsoft.com/pt-br/download/dotnet/6.0);
    3. Apos isso, será necessário criar seu token pessoal do Github, para conseguir fazer adequadamente as requisições para a APi do Github. Para isso siga essa documentação aqui: [Criando Personal Token](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens#creating-a-fine-grained-personal-access-token)
    4. Ao criar seu token pessoal, abra o arquivo na pasta root do projeto chamado: `appsettings.Development.json`, e substitua o valor de `${TOKEN_VALUE}` pelo valor do seu token pessoal do Github.
        * Obs: Em um cenário produtivo, ao invés de editar o arquivo `appsettings.Development.json`, faça a alteração em `appsettings.json` no momento de build com o valor desejado do token. Caso use Github actions existe a variável global com esse valor, chamada: `GITHUB_TOKEN`;
    5. Após substituir o valor, execute o comando no terminal dentro da pasta root do projeto: `dotnet run`;
        * Obs: Informações de porta serão exibidas no console do app;
