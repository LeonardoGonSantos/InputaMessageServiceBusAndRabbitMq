using MassTransit;
using Microsoft.Azure.ServiceBus.Primitives;
using PrbOctopusBack.ServiceBus.Models.Commands.PropostaRefinanciamento;
using PrbOctopusBack.ServiceBus.Models.Commands.ProvaDeVida;
using ReinputaNaFila.MessageSender;
using System;
using System.Threading.Tasks;
using ReinputaNaFila.ComuncacaoOferta;

namespace ReinputaNaFila
{
    class Program
    {
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

        private static async Task SendMessage<T>(ServiceBusData.Ambiente ambiente, string fila, params T[] messages)
        {
            var bus = Bus.Factory.CreateUsingAzureServiceBus(sbc =>
            {
                sbc.Host(new Uri(ambiente.Host), h =>
                {
                    h.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(ambiente.KeyName, ambiente.SharedAccessKey, TokenScope.Namespace);

                    h.TransportType = Microsoft.Azure.ServiceBus.TransportType.AmqpWebSockets;
                });
            });

            await bus.StartAsync();

            var enderecoUri = ConstruirEnderecoDestino(bus, fila);
            var endpoint = await bus.GetSendEndpoint(enderecoUri);

            var count = 0;
            var total = messages.Length;
            foreach (var message in messages)
            {
                await endpoint.Send(message);
                Console.WriteLine($"{total}/{count++}");
            }

            await bus.StopAsync();
        }

        private static Uri ConstruirEnderecoDestino(IBusControl bus, string queueName)
        {
            var index = bus.Address.AbsoluteUri.LastIndexOf("/");
            var urlBase = bus.Address.AbsoluteUri.Substring(0, index);

            return new Uri($"{urlBase}/{queueName}");
        }

    }
}
