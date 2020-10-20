using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infra.Interface
{
    public interface IInstituicaoDao
    {
        IQueryable<Instituicao> ObterInstituicoesClassificadaPorNome();
    }
}
