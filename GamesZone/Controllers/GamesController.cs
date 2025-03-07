using GamesZone.Models;
using GamesZone.Services.Category;
using GamesZone.Services.Device;
using GamesZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamesZone.Controllers
{

    public class GamesController : Controller
    {
		private readonly ApplicationDbContext _context;
        private readonly ICategoryService _category;
        private readonly IDeviceService _device;
		private readonly IGameService _game;

		public GamesController(ApplicationDbContext context
            ,ICategoryService category
            ,IDeviceService device
            ,IGameService game)
        {
			_context = context;
            _category = category;
            _device = device;
            _game = game;
		}

        public IActionResult Index()
        {
            var games = _game.GetAll();
            return View(games);
        }
        public IActionResult Details(int id)
        {
            var game = _game.GetById(id);
            if(game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {

            CreateFromViewModel viewModel = new()
            {
                Categories = _category.GetSelectList(),

				Devices = _device.GetSelectList(),
            };
 
			return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(CreateFromViewModel model)
        {
            if (!ModelState.IsValid)
            { 
                model.Categories = _category.GetSelectList();
                model.Devices = _device.GetSelectList();
				return View(model);

			}
             await _game.Add(model);
			return RedirectToAction("Index");
		}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game= _game.GetById(id);
            if (game == null)
            {
                return NotFound();
            }
            EditGameFromViewModel newgame = new()
            {
                Id=id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                CurrentCover = game.Cover,
                Categories = _category.GetSelectList(),
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Devices = _device.GetSelectList()
            };
            return View(newgame);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit(EditGameFromViewModel model)
        {
			if (!ModelState.IsValid)
			{
				model.Categories = _category.GetSelectList();
				model.Devices = _device.GetSelectList();
				return View(model);

			}
            var game = await _game.Edit(model);

            if (game == null) 
            {
                return BadRequest();
            }

			return RedirectToAction("Index");
		}
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _game.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
