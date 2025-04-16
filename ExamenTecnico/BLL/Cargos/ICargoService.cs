using ExamenTecnico.DTOs;
using ExamenTecnico.Models;

namespace ExamenTecnico.BLL.Cargos
{
    public interface ICargoService
    {
        List<Cargo> GetAll();
        Cargo GetById(int id);
        void Create(CargoDto cargo);
        void Update(int id, CargoDto cargo);
        void Delete(int id);
    }
}
