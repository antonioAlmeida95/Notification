# Tech Challenge - Fase 3

Continuando a evolução do Tech Challenge, no qual foi desenvolvido um aplicativo .NET para cadastro de contatos regionais com práticas de CI e monitoramento, esta fase irá introduzir conceitos avançados de arquitetura de microsserviços e mensageria.

## Objetivos do Desafio
- Dividir o aplicativo monolítico em microsserviços menores, cada um responsável por uma parte da funcionalidade (por exemplo: cadastro, consulta, atualização, exclusão de contatos).
- Implementar padrões de comunicação entre microsserviços, considerando padrões como Circuit Breaker (caso necessário).
- Configurar o RabbitMQ para gerenciar a fila de mensagens.
- Desenvolver produtores (producers) e consumidores (consumers) de mensagens para os eventos de criação, atualização e exclusão de contatos.


## Composição do Projeto
- ArquivosCompose: possui os arquivos para build e deploy dos demais;
- Person: repositório contendo a API em dotnet core a ser monitorada e produtora de mensagens para o RabbitMQ;
- Notification: repositório contendo worker responsável pelo consumo de mensagens noo RabbitMQ;
- Prometheus: responsável por conter os arquivos básicos para configuração e execução via docker do servidor de monitoramento Prometheus;
- Grafana: contém os arquivos básicos para configuração e execução via docker do servidor Grafana;

## Documentação da API

O projeto seguiu como padrão de desenvolvimento arquitetural, visando as boas práticas de desenvolvimento, o modelo hexagonal, a seguir encontra-se ilustrado de forma ampla a solução proposta para o desafio, juntamente com a definição arquitetural do projeto.

### Arquitetura Geral
<p align="center"><img src="/docs/arquitetura_geral.png" alt="Arquitetura Geral"></p>

### Arquitetura
<p align="center"><img src="/docs/arquitetura_notification.png" alt="Arquitetura Notification Wprker Service"></p>