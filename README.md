# Guadalupe.Conexão

### MISSÃO
Integrar e engajar a toda a comunidade da missão guadalupe por meio da tecnologia a todos os projetos da missão.

### PROPOSTA
entregar um feed de notícias interativo, atualizado dinamicamente com as informações atualizadas da missão, informando a toda a comunidade sempre as ultimas noticias por push de notificação, deixando assim a todos atualizados em tempo real e possibilitando a interação da comunidade com o tema apresentado.
Habitar um formulário para captar algumas informações da comunidade.

## Arquitetura
![](docs/architetura.png)

## Development
- Nesse primeiro momento iremos manter o APP, API e WebSite no mesmo repositório/solução, para utilizar apenas um VS para codificação.
- Este projeto utiliza ef-core com migrations, para criar o banco de dados basta configurar a connection-string e o migration irá criá-lo ou atualiza-lo, quando executar a API. (*Para servidor de banco de dados é indicado utilizar o sql-server-express com localDB*)

### Portas de saídas:
|  APP | https   | http  | Permite IP Local |
| ------------ | ------------ | ------------ |
|  API | https://localhost:4001   | http://localhost:4000  | Sim |
|  BackOffice | https://localhost:5001   | http://localhost:5000  | Não |


### Icones
Para gerar e manter os icónes está sendo utilizando o portal do: [Fontello](https://fontello.com/)