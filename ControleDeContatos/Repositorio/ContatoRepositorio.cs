using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;


        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ContatoModel ListarPorId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(b => b.Id == id);
        }

        public List<ContatoModel> BuscarTodos()
        {
            return _bancoContext.Contatos.ToList();
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
           // gravar no banco de dados
           _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();

            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoBd = ListarPorId(contato.Id);

            if (contatoBd == null) throw new Exception("Houve um erro na atualização do contato");

            contatoBd.Nome = contato.Nome;
            contatoBd.Email = contato.Email;
            contatoBd.Celular = contato.Celular;

            _bancoContext.Contatos.Update(contatoBd);
            _bancoContext.SaveChanges();
            return contatoBd;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoBd = ListarPorId(id);

            if (contatoBd == null) throw new Exception("Houve um erro na deleção do contato");

            _bancoContext.Contatos.Remove(contatoBd);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
