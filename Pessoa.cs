using System;

public class Pessoa
{
    protected int id;
    public int Id { get { return id; } }
    public string nome;
    public long cpf;
    public Endereco endereco;
    public Telefone[] telefone = new Telefone[2];

    public Pessoa(string nome, long cpf, Endereco endereco, Telefone[] telefone)
    {
        this.nome = nome;
        this.cpf = cpf;
        this.endereco = endereco;
        this.telefone = telefone;
    }

    public Pessoa(int id, string nome, long cpf, Endereco endereco, Telefone[] telefone)
    {
        this.nome = nome;
        this.cpf = cpf;
        this.endereco = endereco;
        this.telefone = telefone;
        this.id = id;
    }
}