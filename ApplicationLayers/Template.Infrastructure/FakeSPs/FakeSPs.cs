using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Infrastructure.FakeSPs
{
    public static class FakeSP
    {
        public static async Task<dynamic> GetFakeResultFromSP(string storedProcedure)
        {
            try
            {
                if (storedProcedure.Equals("DBO.GET_AAA_BY_ID"))
                {
                    string Nombre = "PruebaEduA";
                    string Apellido = "PruebaEduB";
                    string Cedula = "12345678";

                    dynamic result = new ExpandoObject();
                    result.Nombre = Nombre;
                    result.Apellido = Apellido;
                    result.Cedula = Cedula;

                    return await Task.FromResult(result);
                }
                else if (storedProcedure.Equals("DBO.GET_AAAS"))
                {
                    List<dynamic> result = new List<dynamic>();

                    dynamic AAA = new ExpandoObject();
                    AAA.Nombre = "NombreA";
                    AAA.Apellido = "ApellidoA";
                    AAA.Cedula = "12345678";
                    result.Add(AAA);

                    dynamic AAA2 = new ExpandoObject();
                    AAA2.Nombre = "NombreB";
                    AAA2.Apellido = "ApellidoB";
                    AAA2.Cedula = "12345677";
                    result.Add(AAA2);

                    return await Task.FromResult(result);
                }
                else
                {
                    throw new Exception("No se encontró el SP en la lista de FakeSPs.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
