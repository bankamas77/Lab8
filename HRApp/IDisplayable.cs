namespace HRLib.Interfaces
{
    /// Інтерфейс для об'єктів, що підтримують виведення інформації.
    public interface IDisplayable
    {
        /// Повертає повну інформацію про об'єкт у вигляді рядка.
        string GetInfo();
    }
}
