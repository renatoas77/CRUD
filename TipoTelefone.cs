using System;

public class TipoTelefone
{
    protected int id;
    public int Id { get { return id; } }
    public string tipo;

    public TipoTelefone(string tipo)
    {
        this.tipo = tipo;
    }

    public TipoTelefone(int id, string tipo)
    {
        this.id = id;
        this.tipo = tipo;
    }
}