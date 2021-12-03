using System.Collections.Generic;

namespace RestWithASPNETUdemy.Data.Converter.Contract
{
    public interface IParser<O, D> //Receber dois tipos Origin e Destino
    {
        D Parse(O origin);
        List<D> Parse(List<O> origin);
    }
}
