using Capitulo001.Models;
using Modelo.Cadastros;
using System.Linq;

namespace Capitulo001.Data
{
    public class IESDbInitializer
    {
        public static void Initialize(IESContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //if (!context.Departamentos.Any() )
            //{
            //    var departamentos = new Departamento[]
            //    {
            //            new Departamento {Nome="Ciência da Computação" },
            //            new Departamento {Nome="Ciência de Alimentação" }
            //    };

            //    foreach (Departamento d in departamentos)
            //    {
            //        context.Departamentos.Add(d);
            //    }

            //    context.SaveChanges();
            //}

            //if (!context.Instituicoes.Any())
            //{
            //    var instituicoes = new Instituicao[]
            //    {
            //        new Instituicao{ Nome = "UniParaná", Endereco = "Paraná" },
            //        new Instituicao{ Nome = "UniSanta", Endereco = "Santa Catarina" },
            //        new Instituicao{ Nome = "UniSãoPaulo", Endereco = "São Paulo" },
            //        new Instituicao{ Nome = "UniSulgrandense", Endereco = "Rio Grande do Sul" },
            //        new Instituicao{ Nome = "UniCarioca", Endereco = "Rio de Janeiro" },
            //    };

            //    foreach (Instituicao i in instituicoes)
            //    {  
            //        context.Instituicoes.Add(i);
            //    }
            //    context.SaveChanges();
            //}

            //return;



            if (context.Departamentos.Any())
            {
                return;
            }
                var instituicoes = new Instituicao[]
            {
                    new Instituicao{ Nome = "UniParaná", Endereco = "Paraná" },
                    new Instituicao{ Nome = "UniSanta", Endereco = "Santa Catarina" },
                    new Instituicao{ Nome = "UniSãoPaulo", Endereco = "São Paulo" },
                    new Instituicao{ Nome = "UniSulgrandense", Endereco = "Rio Grande do Sul" },
                    new Instituicao{ Nome = "UniCarioca", Endereco = "Rio de Janeiro" },
            };

            foreach (Instituicao i in instituicoes)
            {
                context.Instituicoes.Add(i);
            }
            context.SaveChanges();

            var departamentos = new Departamento[]
            {
                        new Departamento {Nome="Ciência da Computação" , InstituicaoID = 1},
                        new Departamento {Nome="Ciência de Alimentação", InstituicaoID = 2 }
            };

            foreach (Departamento d in departamentos)
            {
                context.Departamentos.Add(d);
            }

            context.SaveChanges();

        }
    }    
}
