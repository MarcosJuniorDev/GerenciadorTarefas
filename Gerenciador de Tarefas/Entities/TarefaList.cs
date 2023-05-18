

namespace Gerenciador_de_Tarefas.Entities
{
    
    internal class TarefaList
    {        
        public string Name { get; set; }
        public bool Concluida { get; set; }
        public List<Tarefa> Tarefas { get; set; } = new List<Tarefa>();

        public TarefaList()
        {
        }

        public TarefaList(string name, bool concluida)
        {
            Name = name;
            Concluida = concluida;
        }

        public void AdicionarTarefa(Tarefa tarefa)
        {
            Tarefas.Add(tarefa);
        }

        public void RemoverTarefa(Tarefa tarefa)
        {
            Tarefas.Remove(tarefa);
        }

        


        public override string ToString()
        {
            return String.Format($"Nome da tarefa: {Name}\n Tarefa: {Tarefas}\n------------------------------------------------------");
        }

    }
}
