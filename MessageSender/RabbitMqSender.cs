using System;
using System.Threading.Tasks;
using MassTransit;

namespace ReinputaNaFila.MessageSender
{
    public class RabbitMqSender
    {
        public static async Task<bool> Send<T>(ServiceBusData.Ambiente ambiente, string fila, params T[] messages)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(ambiente.Host), h =>
                {
                    h.Username(ambiente.Usuario);
                    h.Password(ambiente.Senha);

                    if (ambiente.ClusterMembers != null)
                    {
                        h.UseCluster(cluster =>
                        {
                            foreach (var member in ambiente.ClusterMembers)
                            {
                                cluster.Node(member);
                            }
                        });
                    }
                });
            });

            await busControl.StartAsync();

            var endpoint = await busControl.GetSendEndpoint(ConstruirEnderecoDestino(busControl, NomeFilas.AtualizacaoStatusPortabilidade));

            var count = 0;
            var total = messages.Length;

            foreach (var message in messages)
            {
                try
                {
                    await endpoint.Send(message);
                    Console.WriteLine($"{total}/{count++}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
            await busControl.StopAsync();
            return true;
        }

        private static Uri ConstruirEnderecoDestino(IBusControl bus, string queueName)
        {
            var index = bus.Address.AbsoluteUri.LastIndexOf("/");
            var urlBase = bus.Address.AbsoluteUri.Substring(0, index);

            return new Uri($"{urlBase}/{queueName}");
        }
    }
}