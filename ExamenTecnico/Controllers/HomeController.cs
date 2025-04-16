using ExamenTecnico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace ExamenTecnico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Calculadora()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calcular(string expresion)
        {
            if (string.IsNullOrWhiteSpace(expresion))
            {
                return Json(new { success = false, message = "Por favor ingresa una expresión válida." });
            }

            try
            {
                // Evaluar la expresión (usando DataTable.Compute para evaluar expresiones matemáticas)
                //var resultado = new DataTable().Compute(expresion.Replace("(", "*("), "");

                //// Crear el valor completo para almacenar en la base de datos
                //var valorCompleto = $"{expresion}={resultado}";

                //// Guardar el resultado en la tabla tbResultado
                //var nuevoResultado = new Resultado
                //{
                //    ValorResultado = valorCompleto
                //};
                //_context.TbResultados.Add(nuevoResultado);
                //_context.SaveChanges();

                return Json(new { success = true, resultado = "valorCompleto" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error en la expresión. Por favor revisa la sintaxis." });
            }
        }

        [HttpPost]
        public IActionResult GuardarValores([FromBody] string valores)
        {
            try
            {
                // Validar que los valores no sean nulos o vacíos
                if (string.IsNullOrWhiteSpace(valores))
                {
                    return Json(new { success = false, message = "No se recibieron valores válidos." });
                }

                // Obtener la cadena de conexión desde appsettings.json
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                // Usa ADO.NET para ejecutar el procedimiento almacenado
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Crea el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("resultado_insert", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agrega el parámetro al procedimiento almacenado
                        command.Parameters.AddWithValue("@ValorResultado", valores);

                        // Ejecuta el procedimiento almacenado y obtener el ID generado
                        int idGenerado = (int)command.ExecuteScalar();

                        // Devuelve el ID generado al frontend
                        return Json(new { success = true, id = idGenerado });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
