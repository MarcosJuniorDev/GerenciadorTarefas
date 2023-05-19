using Gerenciador_de_Tarefas.Entities;
using Gerenciador_de_Tarefas.Enums;

namespace Gerenciador_de_Tarefas
{
    internal class Program
    {
        private const string FILE_NAME = "Save.data";
        static void Main(string[] args)
        {
            DateTime hora = DateTime.Now;
            bool encerrarPrograma = false;
            bool listaNaTela = false;
            MostrarDadosNaTela();
            while (!encerrarPrograma)
            {

                int opcao = int.Parse(Console.ReadLine());
                Console.Clear();
                if (opcao == 1)
                {
                    Console.Write("Nome da tarefa:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Descrição da tarefa: ");
                    string desc = Console.ReadLine();
                    Console.Write("Prioridade da tarefa (Baixa, Media e Alta): ");
                    Prioridade prio = Enum.Parse<Prioridade>(Console.ReadLine());
                    TarefaList listaDeTarefa = new TarefaList(name, false);
                    listaDeTarefa.AdicionarTarefa(new Tarefa(desc, prio));

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
                                tw.WriteLine(listaDeTarefa.Hora);

                            }
                        }
                        Console.WriteLine("Lista Criada pressione ENTER para voltar ao menu");
                        Console.ReadLine();
                        MostrarDadosNaTela();

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
                            sw.WriteLine(listaDeTarefa.Hora);
                        }
                        Console.WriteLine("Lista criada pressione ENTER para voltar ao menu");
                        Console.ReadLine();
                        MostrarDadosNaTela();
                        ;
                    }
                }
                else if (opcao == 2)
                {
                    if (!File.Exists(FILE_NAME))
                    {
                        Console.WriteLine("ainda não existe lista salva");
                        break;
                    }
                    List<TarefaList> listaTarefa = LerDados();
                    if (listaTarefa == null)
                    {
                        Console.WriteLine("ainda não existe lista salva");
                        break;
                    }

                    foreach (TarefaList tarefa in listaTarefa)
                    {
                        Console.WriteLine($"Nome da lista:          {tarefa.Name}");
                        Console.WriteLine();
                        foreach (Tarefa tarefas in tarefa.Tarefas)
                        {
                            Console.WriteLine($"Descrição: \n{tarefas.Descricao} \n\nPrioridade: {tarefas.Prioridade}");
                        }
                        Console.WriteLine();
                        Console.WriteLine($"{tarefa.Hora}");
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine();

                    }
                    listaNaTela = true;
                    Console.ReadLine();
                    Console.WriteLine("Aperte ENTER para voltar ao menu");
                    MostrarDadosNaTela();
                }
                else if (opcao == 3)
                {
                    if (!listaNaTela)
                    {
                        Console.WriteLine("Selecione a opcao de mostrar lista na tela antes de excluir uma lista");
                        return;
                    }
                    List<TarefaList> listaTarefa = LerDados();
                    Console.Write("Insira o nome da lista que voce deseja excluir: ");
                    string name = Console.ReadLine();
                    RemoverListaArquivo(name);
                    Console.WriteLine();
                    Console.WriteLine("Voce removeu a lista de tarefa, aperte ENTER para voltar ao menu");
                    MostrarDadosNaTela();
                }
                else
                {
                    break;
                }

            }
        }
        static void RemoverListaArquivo(string nomeLista)
        {
            List<TarefaList> tarefas = LerDados(); // Ler as listas existentes do arquivo

            // Procurar a lista com base no nome
            TarefaList listaParaRemover = tarefas.Find(t => t.Name == nomeLista);

            if (listaParaRemover != null)
            {
                tarefas.Remove(listaParaRemover); // Remover a lista da lista principal

                // Sobrescrever o arquivo com as listas atualizadas
                using (StreamWriter sw = new StreamWriter(FILE_NAME))
                {
                    foreach (TarefaList tarefaList in tarefas)
                    {
                        sw.WriteLine(tarefaList.Name);

                        foreach (Tarefa tarefa in tarefaList.Tarefas)
                        {
                            sw.WriteLine(tarefa.Descricao);
                            sw.WriteLine(tarefa.Prioridade.ToString());
                        }
                    }
                }

                Console.WriteLine("Lista removida do arquivo.");
            }
            else
            {
                Console.WriteLine("Lista não encontrada no arquivo.");
            }
        }


        static void MostrarDadosNaTela()
        {
            Console.Clear();
            Console.WriteLine("BEM VINDO AO GERENCIADOR DE TAREFAS!");
            Console.WriteLine("Selecione umas das opcoes abaixo:");
            Console.WriteLine("1 - Adionar uma lista de tarefa.");
            Console.WriteLine("2 - Listar as lista de tarefas");
            Console.WriteLine("3 - Remover uma lista");
            Console.WriteLine("Qualquer outro botao para sair do programa");
            Console.WriteLine();
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
                    DateTime hora = DateTime.Parse(sr.ReadLine());
                    Tarefa tarefa = new Tarefa(descricao, prio);
                    tarefaList1.Hora = hora;

                    tarefaList1.Tarefas.Add(tarefa);
                    tarefaList.Add(tarefaList1);

                }
            }
            return tarefaList;
        }



    }


}