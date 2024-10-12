using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granja : MonoBehaviour
{
    private List<Animal> animales = new List<Animal>();

    public void AgregarAnimal(Animal animal)
    {
        animales.Add(animal);
        Debug.Log($"{animal.ObtenerNombre()} ha sido agregado a la granja.");
    }

    public void HacerSonidos()
    {
        foreach (var animal in animales)
        {
            animal.HacerSonido();
        }
    }

    public List<Animal> Animales => animales; 
}
