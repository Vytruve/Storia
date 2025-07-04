namespace Storia.Core.Enums
{
    /// <summary>
    /// Enumera��o para as unidades de medida dos produtos.
    /// Isso garante consist�ncia e evita erros de entrada de dados.
    /// </summary>
    public enum UnitOfMeasure
    {
        /// <summary>
        /// Representa um item individual, cont�vel.
        /// Ex: 1 lata de refrigerante, 1 parafuso.
        /// </summary>
        und,

        /// <summary>
        /// Representa uma medida de peso em quilogramas.
        /// Ex: 2.5 Kg de carne.
        /// </summary>
        Kg,

        /// <summary>
        /// Representa uma medida de peso em gramas.
        /// Pode ser �til para itens de baixo peso.
        /// </summary>
        Gr,

        /// <summary>
        /// Representa uma medida de volume em litros.
        /// Ex: 1 L de leite.
        /// </summary>
        Lt,

        /// <summary>
        /// Representa uma medida de volume em mililitros.
        /// </summary>
        Ml,

        /// <summary>
        /// Representa uma medida de comprimento em metros.
        /// Ex: 10 m de fio.
        /// </summary>
        M,

        /// <summary>
        /// Representa uma medida de comprimento em cent�metros.
        /// </summary>
        cm
    }
}