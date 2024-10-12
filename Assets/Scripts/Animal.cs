using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : IAnimal
{
    public string Nombre { get; set; }

    protected Animal(string nombre)
    {
        Nombre = nombre;
    }

    public abstract void HacerSonido();

    public string ObtenerNombre()
    {
        return Nombre;
    }
}
