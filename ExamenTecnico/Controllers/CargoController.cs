using ExamenTecnico.BLL.Cargos;
using ExamenTecnico.DTOs;
using ExamenTecnico.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamenTecnico.Controllers
{
    public class CargoController : Controller
    {
        private readonly ICargoService _cargoService;

        public CargoController(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        // Listar todos los cargos
        public IActionResult Index()
        {
            List<Cargo> listaCargos = _cargoService.GetAll();
            return View(listaCargos);
        }

        // Obtener un cargo por ID (para el modal de edición)
        [HttpGet]
        public IActionResult GetCargo(int id)
        {
            try
            {
                Cargo cargo = _cargoService.GetById(id);
                return Json(cargo);
            }
            catch (KeyNotFoundException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Crear un nuevo cargo
        [HttpPost]
        public IActionResult Create([FromBody] CargoDto cargo)
        {
            try
            {
                _cargoService.Create(cargo);
                return Json(new { success = true, message = "Cargo registrado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Actualizar un cargo existente
        [HttpPut]
        public IActionResult Edit(int id, [FromBody] CargoDto cargo)
        {
            try
            {
                _cargoService.Update(id, cargo);
                return Json(new { success = true, message = "Cargo actualizado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Eliminar un cargo
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _cargoService.Delete(id);
                return Json(new { success = true, message = "Cargo eliminado correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
