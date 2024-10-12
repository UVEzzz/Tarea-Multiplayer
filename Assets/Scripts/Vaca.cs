using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaca : Animal
{
    public Vaca(string nombre) : base(nombre) { }

    public override void HacerSonido()
    {
        Debug.Log($"{Nombre} dice: Muu!");
    }
}
