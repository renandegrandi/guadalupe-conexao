# Guadalupe.Conexão

### MISSÃO
Integrar e engajar a toda a comunidade da missão guadalupe por meio da tecnologia a todos os projetos da missão.

### PROPOSTA
entregar um feed de notícias interativo, atualizado dinamicamente com as informações atualizadas da missão, informando a toda a comunidade sempre as ultimas noticias por push de notificação, deixando assim a todos atualizados em tempo real e possibilitando a interação da comunidade com o tema apresentado.
Habitar um formulário para captar algumas informações da comunidade.

## Development server
Nesse primeiro momento iremos manter o APP e API no mesmo repositório/solução, para utilizar apenas um VS para codificação.

### Icones
Para gerar e manter os icónes está sendo utilizando o portal do: [Fontello](https://fontello.com/)


### Pendências

-- Icones de notificações:
- Se tiver push configurado usaremos os icones sem o corte.
- Se não aceitou notificações iremos utilizar o sino com corte.
- se tiver notificações iremos utilizar o preto.
- se não tiver notificações iremos utilizar o branco.

-- última atualização.
-- se for nulla irei trazer todos os 100 últimos registros.
-- se tiver uma data irei análisar o isnull de (modified, inclusion).

-- 18/10/2020 15:01 - null               -- deverá retornar o registro (1, 2).
-- 18/10/2020 15:03 - 18/10/2020 15:01   -- deverá retornar o registro (2, 3).

-- id, message, image, posted_by, inclued, modified, removed
-- 1, '', '', 1, 18/10/2020 15:00, null, null
-- 2, '', '', 1, 18/10/2020 15:00, 18/10/2020 15:02, null
-- 3, '', '', 1, 18/10/2020 15:02, null, null

-- No json por registro irei devolver uma propriedade (state) que irá detectar o que aconteceu no server.
-- inclued, modified, removed

-- na UI teremos que detectar se será necessário, incluir, atualizar ou remover de acordo com as atualizações do usuário.
