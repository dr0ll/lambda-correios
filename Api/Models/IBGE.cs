using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ewcorreios.Models
{
    class MunicipioIBGE
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("nome")]
        public string nome { get; set; }
        [JsonProperty("microrregiao")]
        public MicrorregiaoIBGE microrregiao { get; set; }
    }

    class MicrorregiaoIBGE
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("nome")]
        public string nome { get; set; }
        [JsonProperty("mesorregiao")]
        public MesorregiaoIBGE mesorregiao { get; set; }
    }
    class MesorregiaoIBGE
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("nome")]
        public string nome { get; set; }
        [JsonProperty("UF")]
        public UF uf { get; set; }
    }

    class UF
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("sigla")]
        public string sigla { get; set; }
        [JsonProperty("nome")]
        public string nome { get; set; }
        [JsonProperty("regiao")]
        public Regiao regiao { get; set; }
    }

    class Regiao
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("sigla")]
        public string sigla { get; set; }
        [JsonProperty("nome")]
        public string nome { get; set; }
    }

    public class IBGE
    {
        public static string RetornaIdMunicipioIBGE(string nom_cidade, string sgl_estado)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                byte[] bytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(nom_cidade);
                string cidadeAscii = Encoding.UTF8.GetString(bytes);
                cidadeAscii = cidadeAscii.Replace(' ', '-');




                HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"https://servicodados.ibge.gov.br/api/v1/localidades/municipios/{cidadeAscii}");
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();


                byte[] buffer = new byte[1000];
                StringBuilder sb = new StringBuilder();
                string temp;
                Stream stream = res.GetResponseStream();
                int cont;
                do
                {
                    cont = stream.Read(buffer, 0, buffer.Length);
                    temp = Encoding.UTF8.GetString(buffer, 0, cont).Trim();
                    sb.Append(temp);
                }

                while (cont > 0);
                string body = sb.ToString();

                if (body.StartsWith("["))
                {
                    List<MunicipioIBGE> municipios = JsonConvert.DeserializeObject<List<MunicipioIBGE>>(body);
                    return municipios
                            .Where(m => m.microrregiao.mesorregiao.uf.sigla == sgl_estado)
                            .Select(m => m.id)
                            .FirstOrDefault();
                }
                else
                {
                    MunicipioIBGE municipio = JsonConvert.DeserializeObject<MunicipioIBGE>(body);
                    return municipio.id;
                }
            } catch (System.Exception)
            {
                return "";
            }
        }

    }
}
