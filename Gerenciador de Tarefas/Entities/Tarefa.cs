using Gerenciador_de_Tarefas.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_de_Tarefas.Entities
{
    internal class Tarefa
    {
        public string Descricao { get; set; }
        public Prioridade Prioridade { get; set; }

        public Tarefa()
        {
        }

        public Tarefa(string descricao, Prioridade prioridade)
        {
            Descricao = descricao;
            Prioridade = prioridade;
        }

        public override string ToString()
        {
            return $"{Descricao}\n{Prioridade}";
        }


    }
}
