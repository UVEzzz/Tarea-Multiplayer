using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Granja miGranja;

    void Start()
    {
        miGranja = new Granja();
        miGranja.AgregarAnimal(new Perro("Spyke"));
        miGranja.AgregarAnimal(new Gato("Wilson"));
        miGranja.AgregarAnimal(new Vaca("Gloria"));
        miGranja.HacerSonidos();
    }
}
