using System;

public class Endereco
{
    protected int id;
    public int Id{ get { return id; } }
    public string logradouro;
	public int numero;
	public int cep;
	public string bairro;
	public string cidade;
	public string estado;

    public Endereco(string logradouro, int numero, int cep, string bairro, string cidade, string estado)
    {
        this.logradouro = logradouro;
        this.numero = numero;
        this.cep = cep;
        this.bairro = bairro;
        this.cidade = cidade;
        this.estado = estado;
    }

    public Endereco(int id, string logradouro, int numero, int cep, string bairro, string cidade, string estado)
    {
        this.id = id;
        this.logradouro = logradouro;
        this.numero = numero;
        this.cep = cep;
        this.bairro = bairro;
        this.cidade = cidade;
        this.estado = estado;
    }
}