using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perro : Animal
{
      public Perro(string nombre) : base(nombre) { }

      public override void HacerSonido()

      {
          Debug.Log($"{Nombre} dice: Guau!");
      }
    
}
