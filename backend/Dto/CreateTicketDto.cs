namespace backend.Dto
{
    public class CreateTicketDto
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int Prioridade { get; set; }
        public int ProdutoId { get; set; }
    }
}