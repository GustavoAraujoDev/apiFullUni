namespace apiFullUni.Application.Validators
{
    public class ProdutoValidator
    {
        public static void ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome do produto não pode ser vazio.");
        }

        public static void ValidarPreco(decimal preco)
        {
            if (preco <= 0)
                throw new ArgumentException("Preço do produto deve ser maior que zero.");
        }
    }
}
