namespace HRLib.Interfaces
{
    /// Інтерфейс для об'єктів, що підтримують пошук по ключовому слову.
    public interface ISearchable
    {
        /// Перевіряє чи містить об'єкт задане ключове слово.
        bool ContainsKeyword(string keyword);
    }
}
