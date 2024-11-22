using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gato : Animal
{
   public Gato(string nombre) : base(nombre) { }

    public override void HacerSonido()
    {
        Debug.Log($"{Nombre} dice: GRR!");
    }
}
