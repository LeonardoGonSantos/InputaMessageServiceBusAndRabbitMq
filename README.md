# Reinputa Mensagem em fila RabbitMq e ServiceBus

E um projeto com a intencao de ajudar nos trabalhos manuais fazendo um input de mensagens nas filas de rabbitMq e ServiceBus

## Como Utilizar

No arquivo ServiceBusData.cs insira sua credencial de acesso ao Rabbit ou serviceBus

```
        public readonly static Ambiente Homolog = new Ambiente
        {
            Host = "linkHost",
            KeyName = "KeyName",
            SharedAccessKey = "chaveAcesso=",
            TipoDeBus = TipoDeBus.ServiceBus
        };
```

no arquivo Program.cs mude na ServiceBusData.Homolog para a env que tenha cadastrado

```
       public static async Task Main()
        {
            Console.WriteLine("Enviando");

            //await RabbitMqSender.Send(RabbitProd, NomeFilas.AtualizacaoStatusPortabilidade, Marreta.ToArray());

            await SendMessage(
                ServiceBusData.Homolog,
                NomeFilas.TesteFila,
                new TesteCommand(1));

            Console.WriteLine("Press any key to exit");
            await Task.Run(() => Console.ReadKey());
        }
```

e substituia na pasta Messages o arquivo Messages/TesteCommand.cs pela sua classe de messagem e na Main da program.cs o seu new agora só executar que as mensagens devem ser enviadas com sucesso !!!!


# Caso queira contribuir abra um PR !!! TMJ
