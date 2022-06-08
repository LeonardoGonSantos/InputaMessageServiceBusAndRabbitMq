namespace ReinputaNaFila
{
    public static class ServiceBusData
    {
        public class Ambiente
        {
            public TipoDeBus TipoDeBus { get; set; }
            public string Host { get; set; }
            public string KeyName { get; set; }
            public string SharedAccessKey { get; set; }
            public string Usuario { get; set; }
            public string Senha { get; set; }
            public string [] ClusterMembers { get; set; }
        }

        public enum TipoDeBus
        {
            RabbitMQ = 1,
            ServiceBus
        }

        public readonly static Ambiente Homolog = new Ambiente
        {
            Host = "linkHost",
            KeyName = "KeyName",
            SharedAccessKey = "chaveAcesso=",
            TipoDeBus = TipoDeBus.ServiceBus
        };

        public readonly static Ambiente Prod = new Ambiente
        {
            Host = "linkHost",
            KeyName = "KeyName",
            SharedAccessKey = "chaveAcesso=",
            TipoDeBus = TipoDeBus.ServiceBus
        };

        public readonly static Ambiente Local = new Ambiente
        {
            Host = "linkHost",
            KeyName = "KeyName",
            SharedAccessKey = "chaveAcesso=",
            TipoDeBus = TipoDeBus.ServiceBus
        };

        public static Ambiente RabbitHomolog => new Ambiente
        {
            Host = "linkHost",
            Usuario = "guest",
            Senha = "guest",
            TipoDeBus = TipoDeBus.RabbitMQ
        };

        public static Ambiente RabbitProd => new Ambiente
        {
            Host = "linkHost",
            Usuario = "guest",
            Senha = "guest",
            TipoDeBus = TipoDeBus.RabbitMQ
        };
    }
}
