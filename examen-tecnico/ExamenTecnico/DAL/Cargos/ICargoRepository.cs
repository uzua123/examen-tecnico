using ExamenTecnico.DTOs;
using ExamenTecnico.Models;

namespace ExamenTecnico.DAL.Cargos
{
    public interface ICargoRepository
    {
        List<Cargo> GetAll();
        Cargo GetById(int id);
        void Create(CargoDto cargo);
        void Update(int id, CargoDto cargo);
        int Delete(int id);
    }
}
