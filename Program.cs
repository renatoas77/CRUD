using System;

namespace PIMVIIIDAO
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcao, numTelefone01, numTelefone02, ddd01, ddd02, numero, cep;
            long cpf;
            string nome, logradouro, bairro, cidade, estado, tipo01, tipo02;
            Telefone[] telefone = new Telefone[2];
            PessoaDao dao = new PessoaDao();
            Pessoa p;

            do
            {
                Console.WriteLine("Selecione uma funcão: ");

                Console.WriteLine("1-Insert");
                Console.WriteLine("2-Read");
                Console.WriteLine("0-Sair");

                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        nome = null; cpf = 0; logradouro = null; numero = 0; cep = 0; bairro = null; cidade = null; estado = null;
                        numTelefone01 = 0; ddd01 = 0; tipo01 = null; numTelefone02 = 0; ddd02 = 0; tipo02 = null; telefone = new Telefone[2];

                        Console.Write("Nome: ");
                        nome = Console.ReadLine();
                        Console.Write("CPF: ");
                        cpf = long.Parse(Console.ReadLine());
                        Console.Write("Logradouro: ");
                        logradouro = Console.ReadLine();
                        Console.Write("Numero: ");
                        numero = int.Parse(Console.ReadLine());
                        Console.Write("CEP: ");
                        cep = int.Parse(Console.ReadLine());
                        Console.Write("Bairro: ");
                        bairro = Console.ReadLine();
                        Console.Write("Cidade: ");
                        cidade = Console.ReadLine();
                        Console.Write("Estado: ");
                        estado = Console.ReadLine();
                        Console.Write("Telefone: ");
                        numTelefone01 = int.Parse(Console.ReadLine());
                        Console.Write("DDD: ");
                        ddd01 = int.Parse(Console.ReadLine());
                        Console.Write("Tipo: ");
                        tipo01 = Console.ReadLine();
                        Console.WriteLine("Possui mais um numero de telefone S/N ?");
                        char resp = char.Parse(Console.ReadLine());
                        if (resp == 'S' || resp == 's')
                        {
                            Console.Write("Telefone: ");
                            numTelefone02 = int.Parse(Console.ReadLine());
                            Console.Write("DDD: ");
                            ddd02 = int.Parse(Console.ReadLine());
                            Console.Write("Tipo: ");
                            tipo02 = Console.ReadLine();
                            TipoTelefone tipoTelefone02 = new TipoTelefone(tipo02);
                            telefone[1] = new Telefone(numTelefone02, ddd02, tipoTelefone02);
                        }

                        Endereco endereco = new Endereco(logradouro, numero, cep, bairro, cidade, estado);
                        TipoTelefone tipoTelefone01 = new TipoTelefone(tipo01);
                        telefone[0] = new Telefone(numTelefone01, ddd01, tipoTelefone01);
                        p = new Pessoa(nome, cpf, endereco, telefone);

                        dao.Insira(p);
                        Console.Clear();
                        Console.WriteLine("Dados inseridos com sucesso!");
                        break;
                    case 2:
                        Console.Write("Digite o cpf: ");
                        cpf = long.Parse(Console.ReadLine());
                        p = dao.Consulte(cpf);

                        Console.Clear();
                        Console.WriteLine("Nome: " + p.nome);
                        Console.WriteLine("CPF: " + p.cpf);
                        Console.WriteLine("Logradouro: " + p.endereco.logradouro);
                        Console.WriteLine("Numero: " + p.endereco.numero);
                        Console.WriteLine("CEP: " + p.endereco.cep.ToString("d8"));
                        Console.WriteLine("Bairro: " + p.endereco.bairro);
                        Console.WriteLine("Cidade: " + p.endereco.cidade);
                        Console.WriteLine("Estado: " + p.endereco.estado);
                        Console.WriteLine("Telefone : " + p.telefone[0].numero);
                        Console.WriteLine("DDD: " + p.telefone[0].ddd);
                        Console.WriteLine("Tipo: " + p.telefone[0].tipo.tipo);
                        if (p.telefone[1].numero != 0)
                        {
                            Console.WriteLine("Telefone : " + p.telefone[1].numero);
                            Console.WriteLine("DDD: " + p.telefone[1].ddd);
                            Console.WriteLine("Tipo: " + p.telefone[1].tipo.tipo);
                        }


                        Console.WriteLine("");
                        Console.WriteLine("Selecione uma opção:");
                        Console.WriteLine("1-Alterar");
                        Console.WriteLine("2-Excluir");
                        Console.WriteLine("3-Menu principal");
                        opcao = int.Parse(Console.ReadLine());
                        switch (opcao)
                        {
                            case 1:
                                Console.Write("Nome: ");
                                p.nome = Console.ReadLine();
                                Console.Write("CPF: ");
                                p.cpf = long.Parse(Console.ReadLine());
                                Console.Write("Logradouro: ");
                                p.endereco.logradouro = Console.ReadLine();
                                Console.Write("Numero: ");
                                p.endereco.numero = int.Parse(Console.ReadLine());
                                Console.Write("CEP: ");
                                p.endereco.cep = int.Parse(Console.ReadLine());
                                Console.Write("Bairro: ");
                                p.endereco.bairro = Console.ReadLine();
                                Console.Write("Cidade: ");
                                p.endereco.cidade = Console.ReadLine();
                                Console.Write("Estado: ");
                                p.endereco.estado = Console.ReadLine();
                                Console.Write("Telefone: ");
                                p.telefone[0].numero = int.Parse(Console.ReadLine());
                                Console.Write("DDD: ");
                                p.telefone[0].ddd = int.Parse(Console.ReadLine());
                                Console.Write("Tipo: ");
                                p.telefone[0].tipo.tipo = Console.ReadLine();
                                if (p.telefone[1].numero != 0)
                                {
                                    Console.Write("Telefone: ");    
                                    p.telefone[1].numero = int.Parse(Console.ReadLine());
                                    Console.Write("DDD: ");
                                    p.telefone[1].ddd = int.Parse(Console.ReadLine());
                                    Console.Write("Tipo: ");
                                    p.telefone[1].tipo.tipo = Console.ReadLine();
                                }

                                dao.Altere(p);
                                Console.Clear();
                                Console.WriteLine("Cadastro alterado! ");
                                break;
                            case 2:
                                dao.Exclua(p);
                                Console.Clear();
                                Console.WriteLine("Cadastro excluido! ");
                                break;
                            case 3:
                                Console.Clear();
                                break;
                        }

                        break;
                    case 0:
                        Console.WriteLine("Sair");
                        break;
                    default:
                        Console.WriteLine("Opção invalida");
                        break;
                }
            } while (opcao != 0);
        }
    }
}
