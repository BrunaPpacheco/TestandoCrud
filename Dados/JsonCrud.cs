using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CrudJson.Dados
{
    public class JsonCrud
    {
        public void DetalhesdoUsuario(string arquivoJson)
        {
            var json = File.ReadAllText(arquivoJson);
try
{
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    Console.WriteLine("ID :" + jObject["id"].ToString());
                    Console.WriteLine("Nome :" + jObject["nome"].ToString());
                    var endereco = jObject["endereco"];
                    Console.WriteLine("Rua :" + endereco["rua"].ToString());
                    Console.WriteLine("Cidade :" + endereco["cidade"].ToString());
                    Console.WriteLine("Cep :" + endereco["cep"]);
                    JArray arrayExperiencias = (JArray)jObject["experiencias"];
                    if (arrayExperiencias != null)
                    {
                        Console.WriteLine("Empresas");
                        foreach (var item in arrayExperiencias)
                        {
                            Console.WriteLine("\tId :" + item["empresaid"]);
                            Console.WriteLine("\tEmpresa :" + item["empresanome"].ToString());
                        }
                    }
                    Console.WriteLine("Telefone :" + jObject["telefone"].ToString());
                    Console.WriteLine("Cargo :" + jObject["cargo"].ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //incluir empresa
        public void AdicionarEmpresa(string arquivoJson)
        {
            Console.WriteLine("Informe o Id da empresa : ");
            var empresaId = Console.ReadLine();
            Console.WriteLine("\nQual o nome da Empresa : ");
            var nomeEmpresa = Console.ReadLine();
            var novaEmpresaMembro = "{ 'empresaid': " + empresaId + ", 'empresanome': '" + nomeEmpresa + "'}";
            try
            {
                var json = File.ReadAllText(arquivoJson);
var jsonObj = JObject.Parse(json);
                var arrayExperiencias = jsonObj.GetValue("experiencias") as JArray;
                var novaEmpresa = JObject.Parse(novaEmpresaMembro);
                arrayExperiencias.Add(novaEmpresa);
                jsonObj["experiencias"] = arrayExperiencias;
                string novoJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj,
Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(arquivoJson, novoJsonResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar : " + ex.Message.ToString());
            }
        }
        //deletar empresa
        public void DeletarEmpresa(string arquivoJson)
        {
            var json = File.ReadAllText(arquivoJson);
            try
            {
                var jObject = JObject.Parse(json);
                JArray arrayExperiencias = (JArray)jObject["experiencias"];
                Console.Write("Informe o ID da Empresa a deletar : ");
                var empresaId = Convert.ToInt32(Console.ReadLine());
                if (empresaId > 0)
                {
                    var nomeEmpresa = string.Empty;
                    var empresaADeletar = arrayExperiencias.FirstOrDefault(obj =>
obj["empresaid"].Value<int>() == empresaId);
                    arrayExperiencias.Remove(empresaADeletar);
                    string saida = Newtonsoft.Json.JsonConvert.SerializeObject(jObject,
Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(arquivoJson, saida);
                }
                else
                {
                    Console.Write("O ID da empresa é inválido, tente novamente!");
                    AtualizarEmpresa(arquivoJson);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //atualizar empresa
        public void AtualizarEmpresa(string arquivoJson)
        {
            string json = File.ReadAllText(arquivoJson);
            try
            {
                var jObject = JObject.Parse(json);
                JArray arrayExperiencias = (JArray)jObject["experiencias"];
                Console.Write("Informe o ID da empresa a atualizar : ");
                var empresaId = Convert.ToInt32(Console.ReadLine());
                if (empresaId > 0)
                {
                    Console.Write("Informe o nome da empresa: ");
                    var nomeEmpresa = Convert.ToString(Console.ReadLine());
                    foreach (var empresa in arrayExperiencias.Where(obj =>
                                       obj["empresaid"].Value<int>() == empresaId))
                    {
                        empresa["empresanome"] = !string.IsNullOrEmpty(nomeEmpresa) ? nomeEmpresa : "";
                    }
                    jObject["experiencias"] = arrayExperiencias;
                    string saida = Newtonsoft.Json.JsonConvert.SerializeObject(jObject,
Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(arquivoJson, saida);
                }
                else
                {
                    Console.Write("O ID da empresa é inválido, tente novamente!");
                    AtualizarEmpresa(arquivoJson);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro de Atualização : " + ex.Message.ToString());
            }
        }
        //fim
    }
}
    

