namespace BUnit.Moq.Setups
{
    /// <summary>
    /// Перечисление описывает тип аргумента в методе настроек Mock.Setup.
    /// </summary>
    public enum SetupArgumentType
    {
        /// <summary>
        /// Аргумент является константой.
        /// </summary>
        /// <example>
        /// x.Write(4);
        /// </example>
        Constant,
        /// <summary>
        /// Аргумент авляется ссылкой на внешнию переменную.
        /// По сути захватывает переменную в текущем контексте.
        /// </summary>
        /// <example>
        /// var i = 5;
        /// x.Write(i);
        ///
        /// var r = new StringBuilder();
        /// x.Write(r);
        /// </example>
        MemberAccess,
        /// <summary>
        /// Аргумент создается прямо в expression.
        /// </summary>
        /// <example>
        /// x.Write(new StringBuilder());
        /// </example>
        AnyValue,
    }
}
