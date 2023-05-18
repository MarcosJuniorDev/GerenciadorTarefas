using Gerenciador_de_Tarefas.Entities;
using Gerenciador_de_Tarefas.Enums;

namespace Gerenciador_de_Tarefas
{
    internal class Program
    {
        private const string FILE_NAME = "Test.txt";
        static void Main(string[] args)
        {
            bool listaNaTela = false;

            Console.WriteLine("BEM VINDO AO GERENCIADOR DE TAREFAS!");
            Console.WriteLine();
            Console.WriteLine("Selecione umas das opcoes abaixo:");
            Console.WriteLine("1 - Adionar uma lista de tarefa.");
            Console.WriteLine("2 - Listar as lista de tarefas");
            Console.WriteLine("3 - Remover uma lista");
            int opcao = int.Parse(Console.ReadLine());
            if (opcao == 1)
            {
                Console.Write("Nome da tarefa:");
                string name = Console.ReadLine();
                Console.WriteLine("Descrição da tarefa: ");
                string desc = Console.ReadLine();
                Console.Write("Prioridade da tarefa (Baixa, Media e Alta): ");
                Prioridade prio = Enum.Parse<Prioridade>(Console.ReadLine());
                TarefaList listaDeTarefa = new TarefaList(name, false);
                listaDeTarefa.AdicionarTarefa(new Tarefa(desc, prio, false));

                if (!File.Exists(FILE_NAME))
                {
                    using (FileStream fs = new FileStream(FILE_NAME, FileMode.CreateNew))
                    {
                        using (StreamWriter tw = new StreamWriter(fs))
                        {
                            tw.WriteLine(listaDeTarefa.Name);
                            foreach (var tarefa in listaDeTarefa.Tarefas)
                            {

                                tw.WriteLine(tarefa);
                            }

                        }
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(FILE_NAME, true))
                    {
                        sw.WriteLine(listaDeTarefa.Name);
                        foreach (var tarefa in listaDeTarefa.Tarefas)
                        {
                            sw.WriteLine(tarefa);
                        }

                    }
                }
            }
            else if (opcao == 2)
            {
                List<TarefaList> listaTarefa = LerDados();

                foreach (TarefaList tarefa in listaTarefa)
                {
                    Console.WriteLine($"Nome da lista: {tarefa.Name}");
                    foreach (Tarefa tarefas in tarefa.Tarefas)
                    {
                        Console.WriteLine($"Descrição: \n{tarefas.Descricao} \nPrioridade: {tarefas.Prioridade}");
                    }
                    Console.WriteLine("--------------------------------------------------------");
                    Console.WriteLine();

                }
                listaNaTela = true;
            }
            else if (opcao == 3)
            {
                if(!listaNaTela)
                {
                    Console.WriteLine("Selecione a opcao de mostrar lista na tela antes de excluir uma lista");
                    return;
                }
                List<TarefaList> listaTarefa = LerDados();
                Console.Write("Insira o nome da lista que voce deseja excluir: ");
                string name = Console.ReadLine();
                listaTarefa.RemoveAll(t => t.Name == name);

            }


        }

        public void MostrarDadosNaTela()
        {

        }

        static List<TarefaList> LerDados()
        {
            List<TarefaList> tarefaList = new List<TarefaList>();
            using (StreamReader sr = new StreamReader(FILE_NAME))
            {

                while (!sr.EndOfStream)
                {
                    TarefaList tarefaList1 = new TarefaList();
                    string nome = sr.ReadLine();
                    tarefaList1.Name = nome;
                    string descricao = sr.ReadLine();
                    Prioridade prio = Enum.Parse<Prioridade>(sr.ReadLine());
                    Tarefa tarefa = new Tarefa(descricao, prio, false);

                    tarefaList1.Tarefas.Add(tarefa);
                    tarefaList.Add(tarefaList1);

                }
            }
            return tarefaList;
        }



    }


}