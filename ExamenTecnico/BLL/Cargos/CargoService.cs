using ExamenTecnico.DAL.Cargos;
using ExamenTecnico.DTOs;
using ExamenTecnico.Models;

namespace ExamenTecnico.BLL.Cargos
{
    public class CargoService : ICargoService
    {
        private readonly ICargoRepository _repository;

        public CargoService(ICargoRepository repository)
        {
            _repository = repository;
        }

        public List<Cargo> GetAll()
        {
            return _repository.GetAll();
        }

        public Cargo GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(CargoDto cargo)
        {
            if (string.IsNullOrWhiteSpace(cargo.ValorCargo))
            {
                throw new ArgumentException("El valor del cargo no puede estar vacío.");
            }

            _repository.Create(cargo);
        }

        public void Update(int id, CargoDto cargo)
        {
            if (string.IsNullOrWhiteSpace(cargo.ValorCargo))
            {
                throw new ArgumentException("El valor del cargo no puede estar vacío.");
            }

            _repository.Update(id, cargo);
        }

        public void Delete(int id)
        {
            int rowsAffected = _repository.Delete(id);

            if (rowsAffected == 0)
            {
                throw new KeyNotFoundException("El cargo no existe.");
            }
        }
    }
}
