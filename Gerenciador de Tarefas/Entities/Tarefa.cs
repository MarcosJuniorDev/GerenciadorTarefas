using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_de_Tarefas.Entities
{
    internal class Tarefa : TarefaBase
    {
        public Tarefa(string descricao, int prioridade, bool concluida) : base(descricao, prioridade, concluida)
        {
        }

        public override int DefinirPrioridade()
        {
            throw new NotImplementedException();
        }

        public override void EditarDescricao()
        {
            throw new NotImplementedException();
        }

        public override bool MarcarConcluida()
        {
            throw new NotImplementedException();
        }
    }
}
