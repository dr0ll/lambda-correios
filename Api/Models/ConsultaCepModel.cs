using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WSCorreios;

namespace ewcorreios
{
    public class ConsultaCepModel
    {
        public static (string, bool) ConsultaCorreios(string cep)
        {
            try
            {
                AtendeClienteClient ws = new AtendeClienteClient();
                enderecoERP response = ws.consultaCEPAsync(cep).GetAwaiter().GetResult().@return;

                object retorno = new
                {
                    cep = response.cep,
                    logradouro = response.end,
                    complemento = response.complemento2,
                    bairro = response.bairro,
                    localidade = response.cidade,
                    uf = response.uf,
                    unidade = response.unidadesPostagem,
                };

                return (JsonConvert.SerializeObject(retorno), true);

            }
            catch (System.Exception)
            {
                object msg = new { msg = "CEP Inválido" };
                return (JsonConvert.SerializeObject(msg), false);
            }

        }


        public static (string, bool) ConsultaViaCep(string cep)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"https://viacep.com.br/ws/{cep}/json");
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
                dynamic bodyJson = JsonConvert.DeserializeObject(body);
                if (bodyJson["erro"]) throw new System.Exception("CEP Inválido");




                return (body, true);
            }
            catch (System.Exception)
            {
                object msg = new { msg = "CEP Inválido" };
                return (JsonConvert.SerializeObject(msg), false);
            }
        }
    }
}
