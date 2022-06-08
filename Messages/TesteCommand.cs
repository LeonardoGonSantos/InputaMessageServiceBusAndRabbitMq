using System.Diagnostics.CodeAnalysis;

namespace ReinputaNaFila.ComuncacaoOferta
{
    [ExcludeFromCodeCoverage]
    public class TesteCommand
    {
        public TesteCommand(int testeId)
        {
            TesteId = testeId;
        }

        public int TesteId { get; set; }
    }
}
