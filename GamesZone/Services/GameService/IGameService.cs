namespace GamesZone.Services.Game
{
	public interface IGameService
	{
		IEnumerable<Games> GetAll();

		Games? GetById(int id);

        Task Add(CreateFromViewModel game);

		Task<Games?> Edit(EditGameFromViewModel game);

		bool Delete(int id);

	}
}
