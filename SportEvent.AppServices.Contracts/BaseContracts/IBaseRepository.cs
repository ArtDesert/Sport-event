namespace SportEvent.AppServices.Contracts.BaseContracts;

/// <summary>
/// Базовый обобщённый интерфейс для доступа к хранилищу данных. Является контрактом основных CRUD операций: вызывает методы 
/// непосредственно у объекта контекста данных.
/// </summary>
/// <typeparam name="TEntity">Тип сущности, с которой взаимодействует репозиторий</typeparam>
/// <typeparam name="TId">Тип первичного ключа сущности</typeparam>
public interface IBaseRepository<TEntity, TId>
{
	/// <summary>
	/// Create
	/// </summary>
	/// <param name="entity">Добавляемая сущность</param>
	/// <param name="token"></param>
	/// <returns></returns>
	Task<TId> CreateAsync(TEntity entity, CancellationToken token = default);

	/// <summary>
	/// Read
	/// </summary>
	/// <param name="id">id необходимой сущности</param>
	/// <param name="token"></param> 
	/// <returns></returns>
	Task<TEntity?> GetByIdAsync(TId id, CancellationToken token = default);

	/// <summary>
	/// Update
	/// </summary>
	/// <param name="entity">Обновлённая сущность</param>
	/// <param name="token"></param>
	/// <returns></returns>
	Task UpdateAsync(TEntity entity, CancellationToken token = default);

	/// <summary>
	/// Delete
	/// </summary>
	/// <param name="id">id удаляемой сущности</param>
	/// <param name="token"></param>
	/// <returns></returns>
	Task DeleteAsync(TId id, CancellationToken token = default);
}

