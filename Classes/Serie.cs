using System;
//Perdão, está um pouco bagunçado por querer jogar todos os métodos e implementações em uma classe.
//Será corrigido e melhor dividido em novos commits futuros.
namespace SeriesDIO
{
    public class Serie : EntidadeBase
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        private Genero Genero { get; set; }
        public string Titulo { get; set; }
        private string Descricao { get; set; }
        public int Ano { get; set; }
        private bool Excluido { get; set; }
        public Serie(int id, Genero genero, string titulo, string descricao, int ano)
        {
            this.Id = id;
            this.Genero = genero;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Ano = ano;
            this.Excluido = false;
        }
        public Serie()
        {

        }

        public override string ToString()
        {
            return "Gênero: "
                   + this.Genero
                   + Environment.NewLine
                   + "Titulo: "
                   + this.Titulo
                   + Environment.NewLine
                   + "Descricao: "
                   + this.Descricao
                   + Environment.NewLine
                   + "Ano de inicio: "
                   + this.Ano
                   + Environment.NewLine
                   +"Excluído: "
                   + this.Excluido;
        }
        public string RetornaTitulo()
        {
            return this.Titulo;
        }
        public int RetornaId()
        {
            return this.Id;
        }

        public void Excluir()
        {
            Excluido = true;
        }
        public string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Dio Séries - Seja Bem Vindo !");
            Console.WriteLine("Informe uma opção:");

            Console.WriteLine("1 - Listar séries.");
            Console.WriteLine("2 - Inserir nova série.");
            Console.WriteLine("3 - Atualizar série.");
            Console.WriteLine("4 - Excluir série.");
            Console.WriteLine("5 - Visualizar série.");
            Console.WriteLine("C - Limpar tela.");
            Console.WriteLine("X - Sair.");
            Console.WriteLine("");

            string opcaoUsuario = Console.ReadLine();
            Console.WriteLine("");
            return opcaoUsuario.ToUpper();


        }

        public void AcionarOpcao()
        {
            string opcao = ObterOpcaoUsuario();
            while (opcao.ToUpper() != "X")
            {
                switch (opcao)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("Opção digitada inválida");
                }
                Console.WriteLine();
                opcao = ObterOpcaoUsuario();
            }
            Console.WriteLine("Certo, até mais !");
            Console.ReadKey();
        }

        private void VisualizarSerie()
        {
            Console.WriteLine("Digite o id da série:");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);
            Console.WriteLine(serie);
        }

        private void ExcluirSerie()
        {
           Console.WriteLine("Digite o id da série:");
           int indiceSerie = int.Parse(Console.ReadLine());

           repositorio.Exclui(indiceSerie);
        }

        private static void ListarSeries()
        {
            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }
            foreach (var serie in lista)
            {
                Console.WriteLine($"#ID {serie.RetornaId()}: {serie.RetornaTitulo()}");
            }
        }
        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série:");

            foreach (int i in Enum.GetValues(typeof(Genero))) //Verificar documentação GetValues
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i)); // Verificar documentaçao GetName
            }
            Console.Write("Digite o gênero entre as opções a cima: ");
            int entradaGen = int.Parse(Console.ReadLine());

            Console.Write("Digite o titulo da série:");
            string entradaTit = Console.ReadLine();

            Console.Write("Digite o ano de início da série:");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a descrição da série");
            string entradaDescr = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGen,
                                        titulo: entradaTit,
                                        ano: entradaAno,
                                        descricao: entradaDescr);

            repositorio.Insere(novaSerie);
        }
        private void AtualizarSerie()
        {
            Console.WriteLine("Atualizar série, digite o id:");
            int indiceSerie = int.Parse(Console.ReadLine());

            foreach (int i in Enum.GetValues(typeof(Genero))) //Verificar documentação GetValues
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i)); // Verificar documentaçao GetName
            }
            Console.Write("Digite o gênero entre as opções a cima: ");
            int entradaGen = int.Parse(Console.ReadLine());

            Console.Write("Digite o titulo da série:");
            string entradaTit = Console.ReadLine();

            Console.Write("Digite o ano de início da série:");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a descrição da série");
            string entradaDescr = Console.ReadLine();

            Serie atualizaSerie = new Serie(id: indiceSerie,
                                        genero: (Genero)entradaGen,
                                        titulo: entradaTit,
                                        ano: entradaAno,
                                        descricao: entradaDescr);

            repositorio.Atualiza(indiceSerie, atualizaSerie);
        }

    }
}