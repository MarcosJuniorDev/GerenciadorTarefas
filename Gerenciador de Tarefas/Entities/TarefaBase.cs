namespace Gerenciador_de_Tarefas.Entities
{
    abstract class TarefaBase : ITarefas
    {
        public string Descricao { get; set; }
        public int Prioridade { get; set; }
        public bool Concluida { get; set; } = false;

        public TarefaBase(string descricao, int prioridade, bool concluida)
        {
            Descricao = descricao;
            Prioridade = prioridade;
            Concluida = concluida;
        }

        public abstract int DefinirPrioridade();
        public abstract void EditarDescricao();
        public abstract bool MarcarConcluida();
    }
}
