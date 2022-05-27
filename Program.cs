using CrudJson.Dados;
using System;
using System.IO;
using static System.Console;
namespace CrudJson
{
    class Program
    {
        static void Main(string[] args)
        {
            string arquivoJson = Environment.CurrentDirectory;
            //using (StreamReader stream = new StreamReader(@"C:\Desktop\revisaoLabenu\CrudJson\Dados"))
            //arquivoJson = arquivoJson.Replace("\\bin\\Debug", "");
            arquivoJson = arquivoJson + "\\Dados\\usuario.json";
            JsonCrud jscrud = new JsonCrud();
            WriteLine("-- Operaçoes CRUD com arquivo JSON --");
            WriteLine("");
            while (true)
            {
                WriteLine("Opções : 1 - Incluir, 2 - Atualizar, 3 - Deletar, 4 - Selecionar \n");
                try
                {
                    var option = ReadLine();
                    switch (option)
                    {
                        case "1":
                            jscrud.AdicionarEmpresa(arquivoJson);
                            break;
                        case "2":
                            jscrud.AtualizarEmpresa(arquivoJson);
                            break;
                        case "3":
                            jscrud.DeletarEmpresa(arquivoJson);
                            break;
                        case "4":
                            jscrud.DetalhesdoUsuario(arquivoJson);
                            break;
                        default:
                            Main(null);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    WriteLine("Erro : " + ex.Message);
                }
            }
        }
    }
}