using System;

public class Telefone
{
    protected int id;
    public int Id { get { return id; } }
    public int numero;
    public int ddd;
    public TipoTelefone tipo;

    public Telefone(int numero, int ddd, TipoTelefone tipo)
    {
        this.numero = numero;
        this.ddd = ddd;
        this.tipo = tipo;
    }

    public Telefone(int id, int numero, int ddd, TipoTelefone tipo)
    {
        this.id = id;
        this.numero = numero;
        this.ddd = ddd;
        this.tipo = tipo;
    }
}