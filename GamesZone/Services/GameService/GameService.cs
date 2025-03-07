namespace GamesZone.Services.GameService
{
	public class GameService : IGameService
	{
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string _imagesPath;

		public GameService(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
		{
			_context = context;
			_hostingEnvironment = hostingEnvironment;

			// دمج المسار باستخدام Path.Combine لضمان إضافة الفاصل الصحيح
			_imagesPath = Path.Combine(_hostingEnvironment.WebRootPath, FileSetting.ImagesPath);

			// التأكد من وجود مجلد الصور، وإن لم يكن موجوداً يتم إنشاؤه
			if (!Directory.Exists(_imagesPath))
			{
				Directory.CreateDirectory(_imagesPath);
			}
		}

		public IEnumerable<Games> GetAll()
		{
			var games = _context.Games
				.Include(g => g.Category)
				.Include(g => g.Devices)
				.ThenInclude(gd => gd.Device)
				.AsNoTracking()
				.ToList();
			return games;
		}

		public async Task Add(CreateFromViewModel model)
		{
			var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
			var path = Path.Combine(_imagesPath, CoverName);

			using var stream = File.Create(path);
			await model.Cover.CopyToAsync(stream);

			Games newGame = new()
			{
				Name = model.Name,
				Description = model.Description,
				Cover = CoverName,
				CategoryId = model.CategoryId,
				Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
			};
			_context.Add(newGame);
			_context.SaveChanges();
		}

        public Games? GetById(int id)
        {
			var GameData=_context.Games
				.Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(gd => gd.Device)
                .AsNoTracking()
				.SingleOrDefault(g=>g.Id == id);
            return GameData;
        }

		public async Task<Games?> Edit(EditGameFromViewModel model)
		{
			var game=_context.Games.Include(g=>g.Devices).SingleOrDefault(g=>g.Id==model.Id);
			if (game == null)
			{
				return null;
			}
			
			var hasNewCover = model.Cover != null;
			var oldCover = game.Cover;

			game.Name = model.Name;
			game.Description = model.Description;
			game.CategoryId = model.CategoryId;
			game.Devices = model.SelectedDevices
				.Select(d => new GameDevice { DeviceId = d }).ToList();
			if (hasNewCover)
			{
				var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
				var path = Path.Combine(_imagesPath, CoverName);

				using var stream = File.Create(path);
				await model.Cover.CopyToAsync(stream);
			}
			var result=_context.SaveChanges();
			if (result > 0)
			{
				if (hasNewCover)
				{
					File.Delete(Path.Combine(_imagesPath, oldCover));
				}
				return game;
			}
			else 
			{
				File.Delete(Path.Combine(_imagesPath, game.Cover));
				return null;
			}
		}

        public bool Delete(int id)
        {
            var isDeleted = false;

            var game = _context.Games.Find(id);

            if (game is null)
                return isDeleted;

            _context.Remove(game);
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted = true;

                var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
            }

            return isDeleted;
        }

    }
}
