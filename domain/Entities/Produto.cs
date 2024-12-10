namespace apiFullUni.domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        // Construtores, validações, etc.
        public Produto(string nome, decimal preco, int id)
        {
            if (string.IsNullOrEmpty(nome))
                throw new Exception("Nome do produto não pode ser vazio.");

            if (preco <= 0)
                throw new Exception("Preço do produto deve ser maior que zero.");
            Id = id;
            Nome = nome;
            Preco = preco;
        }
    }
}
