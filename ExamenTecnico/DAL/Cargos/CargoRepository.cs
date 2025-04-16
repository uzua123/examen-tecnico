using ExamenTecnico.DTOs;
using ExamenTecnico.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ExamenTecnico.DAL.Cargos
{
    public class CargoRepository : ICargoRepository
    {
        private readonly string _connectionString;
        public CargoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Cargo> GetAll()
        {
            List<Cargo> listaCargos = new List<Cargo>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("cargo_select_all", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaCargos.Add(new Cargo
                            {
                                IdCargo = (int)reader["idCargo"],
                                ValorCargo = reader["valorCargo"].ToString()
                            });
                        }
                    }
                }
            }

            return listaCargos;
        }

        public Cargo GetById(int id)
        {
            Cargo cargo = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("cargo_select_by_id", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idCargo", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cargo = new Cargo
                            {
                                IdCargo = (int)reader["idCargo"],
                                ValorCargo = reader["valorCargo"].ToString()
                            };
                        }
                    }
                }
            }

            return cargo;
        }

        public void Create(CargoDto cargo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("cargo_insert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ValorCargo", cargo.ValorCargo);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(int id, CargoDto cargo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("cargo_update", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idCargo", id);
                    command.Parameters.AddWithValue("@ValorCargo", cargo.ValorCargo);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("cargo_delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idCargo", id);
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
