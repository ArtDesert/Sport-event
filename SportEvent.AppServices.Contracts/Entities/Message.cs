namespace SportEvent.AppServices.Contracts.Entities;

/// <summary>
/// Сообщение о произошедшем в матче событии 
/// </summary>
public class Message
{
    /// <summary>
    /// Идентификатор сообщения
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Время события
    /// </summary>
    public TimeOnly Time { get; set; }

    /// <summary>
    /// Действие
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// Значение (если гол)
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// Основной субъект
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// Основная иформация о событии
    /// </summary>
    public string Info { get; set; }

    /// <summary>
    /// Идентификатор трансляции, к которой относится данное сообщение
    /// </summary>
    public long BroadcastId { get; set; }

    /// <summary>
    /// Навигационное свойство для трансляции
    /// </summary>
    public virtual Broadcast Broadcast { get; set; }
}
