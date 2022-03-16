using System.Data.SqlClient;

public class PessoaDao
{
    private SqlConnection conexao;
    private SqlCommand comando;
    private SqlDataReader leitor;
    private string strSQL;

    public PessoaDao()
    {
    }


    public bool Insira(Pessoa p)
    {
        conexao = new SqlConnection(@"Data Source=DESKTOP-TEL7CEP\SQLEXPRESS;Initial Catalog=PIMVIII;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        strSQL = "insert into	ENDERECO (logradouro,numero,cep,bairro,cidade,estado) values (@logradouro,@numero,@cep,@bairro,@cidade,@estado) " +
                 "insert into PESSOA(endereco, nome, cpf) VALUES((SELECT MAX(id) FROM ENDERECO), @nome, @cpf) " +
                 "insert into TELEFONE_TIPO(tipo) values(@tipo) " +
                 "insert into TELEFONE(numero, ddd, tipo) values(@telefone, @ddd, (SELECT MAX(id) FROM TELEFONE_TIPO)) " +
                 "insert into PESSOA_TELEFONE(id_pessoa) values((SELECT MAX(id) FROM PESSOA)) ";

        if (p.telefone[1] != null)
        {
            strSQL += "insert into TELEFONE_TIPO(tipo) values(@tipo2) " +
                      "insert into TELEFONE(numero, ddd, tipo) values(@telefone2, @ddd2, (SELECT MAX(id) FROM TELEFONE_TIPO)) " +
                      "insert into PESSOA_TELEFONE(id_pessoa) values((SELECT MAX(id) FROM PESSOA)) ";
        }

        comando = new SqlCommand(strSQL, conexao);
        comando.Parameters.AddWithValue("@logradouro", p.endereco.logradouro);
        comando.Parameters.AddWithValue("@numero", p.endereco.numero);
        comando.Parameters.AddWithValue("@cep", p.endereco.cep);
        comando.Parameters.AddWithValue("@bairro", p.endereco.bairro);
        comando.Parameters.AddWithValue("@cidade", p.endereco.cidade);
        comando.Parameters.AddWithValue("@estado", p.endereco.estado);
        comando.Parameters.AddWithValue("@nome", p.nome);
        comando.Parameters.AddWithValue("@cpf", p.cpf);
        comando.Parameters.AddWithValue("@tipo", p.telefone[0].tipo.tipo);
        comando.Parameters.AddWithValue("@telefone", p.telefone[0].numero);
        comando.Parameters.AddWithValue("@ddd", p.telefone[0].ddd);
        if (p.telefone[1] != null)
        {
            comando.Parameters.AddWithValue("@tipo2", p.telefone[1].tipo.tipo);
            comando.Parameters.AddWithValue("@telefone2", p.telefone[1].numero);
            comando.Parameters.AddWithValue("@ddd2", p.telefone[1].ddd);
        }

        conexao.Open();
        comando.ExecuteNonQuery();
        conexao.Close();
        conexao = null;
        comando = null;

        return true;
    }

    public Pessoa Consulte(long cpf)
    {
        conexao = new SqlConnection(@"Data Source=DESKTOP-TEL7CEP\SQLEXPRESS;Initial Catalog=PIMVIII;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        Pessoa p = null;
        string logradouro = null, bairro = null, cidade = null, estado = null, nome = null;
        int numero = 0, cep = 0, PESSOAid = 0;
        string[] tipo = new string[2];
        int[] tel = new int[2], ddd = new int[2], TELEFONEid = new int[2];
        int[] TELELEFONE_TIPOid = new int[2];

        strSQL = "SELECT ENDERECO.logradouro, ENDERECO.numero, ENDERECO.cep, ENDERECO.bairro, ENDERECO.cidade, ENDERECO.estado, " +
                 "PESSOA.nome, PESSOA.cpf,PESSOA.id as PESSOAid, " +
                 "TELEFONE.numero as numeroTel,TELEFONE.ddd,TELEFONE.id as TELEFONEid, " +
                 "TELEFONE_TIPO.tipo as TELEFONEtipo,TELEFONE_TIPO.id as TELEFONETIPOid " +
                 "FROM PESSOA " +
                 "JOIN ENDERECO ON endereco.id = pessoa.endereco " +
                 "JOIN PESSOA_TELEFONE ON PESSOA.id = PESSOA_TELEFONE.id_pessoa " +
                 "JOIN TELEFONE ON PESSOA_TELEFONE.id_telefone = TELEFONE.id " +
                 "JOIN TELEFONE_TIPO ON TELEFONE.tipo = TELEFONE_TIPO.id " +
                 "where cpf = @cpf";

        comando = new SqlCommand(strSQL, conexao);
        comando.Parameters.AddWithValue("@cpf", cpf);
        conexao.Open();
        leitor = comando.ExecuteReader();

        int i = 0;
        cpf = 0;

        while (leitor.Read())
        {
            logradouro = (string)leitor["logradouro"];
            numero = (int)leitor["numero"];
            cep = (int)leitor["cep"];
            bairro = (string)leitor["bairro"];
            cidade = (string)leitor["cidade"];
            estado = (string)leitor["estado"];
            nome = (string)leitor["nome"];
            cpf = (long)leitor["cpf"];
            PESSOAid = (int)leitor["PESSOAid"];
            tel[i] = (int)leitor["numeroTel"];
            ddd[i] = (int)leitor["ddd"];
            TELEFONEid[i] = (int)leitor["TELEFONEid"];
            tipo[i] = (string)leitor["TELEFONEtipo"];
            TELELEFONE_TIPOid[i] = (int)leitor["TELEFONETIPOid"];

            i += 1;
        }

        Telefone[] telefone = new Telefone[2];
        Endereco endereco = new Endereco(logradouro, numero, cep, bairro, cidade, estado);
        TipoTelefone tipoTelefone01 = new(TELELEFONE_TIPOid[0], tipo[0]);
        TipoTelefone tipoTelefone02 = new(TELELEFONE_TIPOid[1], tipo[1]);
        telefone[0] = new Telefone(TELEFONEid[0], tel[0], ddd[0], tipoTelefone01);
        telefone[1] = new Telefone(TELEFONEid[1], tel[1], ddd[1], tipoTelefone02);
        p = new Pessoa(PESSOAid, nome, cpf, endereco, telefone);

        conexao.Close();
        conexao = null;
        comando = null;

        return p;
    }

    public bool Altere(Pessoa p)
    {
        conexao = new SqlConnection(@"Data Source=DESKTOP-TEL7CEP\SQLEXPRESS;Initial Catalog=PIMVIII;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        strSQL = "update ENDERECO set logradouro = @logradouro,numero = @numero, cep = @cep, bairro = @bairro, cidade = @cidade, estado = @estado  where id = @pessoaid " +
                 "update PESSOA set nome = @nome, cpf = @cpf where id = @pessoaid " +
                 "update TELEFONE_TIPO set tipo = @tipo where id = @idtipo " +
                 "update TELEFONE set numero = @telefone, ddd = @ddd where id = @telefoneid ";

        if (p.telefone[1].numero != 0)
        {
            strSQL += "update TELEFONE_TIPO set tipo = @tipo2 where id = @idtipo2 " +
                      "update TELEFONE set numero = @telefone2, ddd = @ddd2 where id = @telefoneid2 ";
        }

        comando = new SqlCommand(strSQL, conexao);
        comando.Parameters.AddWithValue("@logradouro", p.endereco.logradouro);
        comando.Parameters.AddWithValue("@numero", p.endereco.numero);
        comando.Parameters.AddWithValue("@cep", p.endereco.cep);
        comando.Parameters.AddWithValue("@bairro", p.endereco.bairro);
        comando.Parameters.AddWithValue("@cidade", p.endereco.cidade);
        comando.Parameters.AddWithValue("@estado", p.endereco.estado);
        comando.Parameters.AddWithValue("@pessoaid", p.Id);
        comando.Parameters.AddWithValue("@nome", p.nome);
        comando.Parameters.AddWithValue("@cpf", p.cpf);
        comando.Parameters.AddWithValue("@tipo", p.telefone[0].tipo.tipo);
        comando.Parameters.AddWithValue("@idtipo", p.telefone[0].tipo.Id);
        comando.Parameters.AddWithValue("@telefone", p.telefone[0].numero);
        comando.Parameters.AddWithValue("@ddd", p.telefone[0].ddd);
        comando.Parameters.AddWithValue("@telefoneid", p.telefone[0].Id);
        if (p.telefone[1].numero != 0)
        {
            comando.Parameters.AddWithValue("@tipo2", p.telefone[1].tipo.tipo);
            comando.Parameters.AddWithValue("@idtipo2", p.telefone[1].tipo.Id);
            comando.Parameters.AddWithValue("@telefone2", p.telefone[1].numero);
            comando.Parameters.AddWithValue("@ddd2", p.telefone[1].ddd);
            comando.Parameters.AddWithValue("@telefoneid2", p.telefone[1].Id);
        }

        conexao.Open();
        comando.ExecuteNonQuery();
        conexao.Close();
        conexao = null;
        comando = null;

        return true;
    }

    public bool Exclua(Pessoa p)
    {
        conexao = new SqlConnection(@"Data Source=DESKTOP-TEL7CEP\SQLEXPRESS;Initial Catalog=PIMVIII;Integrated Security=True;Connect " +
                                    "Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        strSQL = "delete from PESSOA_TELEFONE where id_pessoa = @idpessoa " +
                 "delete from TELEFONE where id = @telefoneid " +
                 "delete from TELEFONE_TIPO where id = @idtipo " +
                 "delete from PESSOA where id = @idpessoa " +
                 "delete from ENDERECO where id = @idpessoa ";

        if (p.telefone[1].numero != 0)
        {
            strSQL += "delete from TELEFONE where id = @telefoneid2 " +
                      "delete from TELEFONE_TIPO where id = @idtipo2";
        }

        comando = new SqlCommand(strSQL, conexao);
        comando.Parameters.AddWithValue("@idpessoa", p.Id);
        comando.Parameters.AddWithValue("@idtipo", p.telefone[0].tipo.Id);
        comando.Parameters.AddWithValue("@telefoneid", p.telefone[0].Id);

        if (p.telefone[1].numero != 0)
        {
            comando.Parameters.AddWithValue("@idtipo2", p.telefone[1].tipo.Id);
            comando.Parameters.AddWithValue("@telefoneid2", p.telefone[1].Id);
        }

        conexao.Open();
        comando.ExecuteNonQuery();
        conexao.Close();
        conexao = null;
        comando = null;

        return true;
    }
}