namespace FarmaciaCRUD.model
{
    public class ItemCarrinho
    {
        public int RemedioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal => PrecoUnitario * Quantidade;
    }
}
