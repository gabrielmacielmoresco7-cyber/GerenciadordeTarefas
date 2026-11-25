using System;
using System.Collections.Generic;

class Tarefa
{
    public int Id;
    public string Descricao = "";
    public bool Concluida;
}

class Program
{
    static List<Tarefa> tarefas = new List<Tarefa>();
    static Stack<int> concluidas = new Stack<int>();
    static int proximoId = 1;

    static void Main()
    {
        string opcao = "";

        while (opcao != "6")
        {
            Console.WriteLine("\n===== GERENCIADOR DE TAREFAS =====");
            Console.WriteLine("1 - Cadastrar tarefa");
            Console.WriteLine("2 - Listar tarefas");
            Console.WriteLine("3 - Buscar tarefa");
            Console.WriteLine("4 - Concluir tarefa");
            Console.WriteLine("5 - Desfazer última conclusão");
            Console.WriteLine("6 - Sair");
            Console.Write("Escolha uma opção: ");

            opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    CadastrarTarefa();
                    break;

                case "2":
                    ListarTarefas();
                    break;

                case "3":
                    BuscarTarefa();
                    break;

                case "4":
                    ConcluirTarefa();
                    break;

                case "5":
                    DesfazerConclusao();
                    break;

                case "6":
                    Console.WriteLine("Programa encerrado!");
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    static void CadastrarTarefa()
    {
        Console.Write("Digite a descrição da tarefa: ");
        string descricao = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(descricao))
        {
            Console.WriteLine("Descrição inválida!");
            return;
        }

        Tarefa tarefa = new Tarefa();

        tarefa.Id = proximoId;
        tarefa.Descricao = descricao;
        tarefa.Concluida = false;

        tarefas.Add(tarefa);
        proximoId++;

        Console.WriteLine("Tarefa cadastrada com sucesso!");
        Console.WriteLine("ID: " + tarefa.Id);
    }

    static void ListarTarefas()
    {
        if (tarefas.Count == 0)
        {
            Console.WriteLine("Nenhuma tarefa cadastrada!");
            return;
        }

        foreach (Tarefa tarefa in tarefas)
        {
            ExibirTarefa(tarefa);
        }
    }

    static void BuscarTarefa()
    {
        Console.Write("Digite o ID da tarefa: ");

        int id;

        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        foreach (Tarefa tarefa in tarefas)
        {
            if (tarefa.Id == id)
            {
                ExibirTarefa(tarefa);
                return;
            }
        }

        Console.WriteLine("Tarefa não encontrada!");
    }

    static void ConcluirTarefa()
    {
        Console.Write("Digite o ID da tarefa: ");

        int id;

        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        foreach (Tarefa tarefa in tarefas)
        {
            if (tarefa.Id == id)
            {
                if (tarefa.Concluida == true)
                {
                    Console.WriteLine("Tarefa já concluída!");
                    return;
                }

                tarefa.Concluida = true;
                concluidas.Push(id);

                Console.WriteLine("Tarefa concluída com sucesso!");
                return;
            }
        }

        Console.WriteLine("Tarefa não encontrada!");
    }

    static void DesfazerConclusao()
    {
        if (concluidas.Count == 0)
        {
            Console.WriteLine("Nenhuma conclusão para desfazer!");
            return;
        }

        int id = concluidas.Pop();

        foreach (Tarefa tarefa in tarefas)
        {
            if (tarefa.Id == id)
            {
                tarefa.Concluida = false;

                Console.WriteLine("Conclusão desfeita!");
                Console.WriteLine("Tarefa " + id + " voltou a ficar pendente.");
                return;
            }
        }
    }

    static void ExibirTarefa(Tarefa tarefa)
    {
        Console.WriteLine("----------------------------");
        Console.WriteLine("ID: " + tarefa.Id);
        Console.WriteLine("Descrição: " + tarefa.Descricao);

        if (tarefa.Concluida == true)
        {
            Console.WriteLine("Situação: Concluída");
        }
        else
        {
            Console.WriteLine("Situação: Pendente");
        }
    }
}

