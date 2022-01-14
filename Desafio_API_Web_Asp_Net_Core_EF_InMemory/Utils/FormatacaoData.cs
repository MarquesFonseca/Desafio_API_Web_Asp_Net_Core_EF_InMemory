using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Utils
{
    public class FormatacaoData
    {
        public static int RetornaIdade(DateTime dataNascimento)
        {
            return new DateTime((DateTime.Now - dataNascimento).Ticks).Year - 1;
        }

        public static bool VerificaDataSeValida(DateTime data)
        {
            bool retorno = false;
            DateTime dataValida;
            if(DateTime.TryParse(data.ToString(), out dataValida)) 
                return true;
            return retorno;
        }
    }
}
