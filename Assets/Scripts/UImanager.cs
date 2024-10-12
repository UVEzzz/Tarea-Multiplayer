using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UIManager : MonoBehaviour
{
    public Granja miGranja; 
    public TMP_Text infoText;
    public TMP_InputField animalNameInput; 
    void Start()
    {
        if (miGranja == null)
        {
            miGranja = new Granja();
        }
    }

    public void AgregarPerro()
    {
        string nombre = animalNameInput.text;
        miGranja.AgregarAnimal(new Perro(nombre));
        infoText.text = $"{nombre} ha sido agregado a la granja como perro.";
        animalNameInput.text = ""; 
    }

    public void AgregarGato()
    {
        string nombre = animalNameInput.text;
        miGranja.AgregarAnimal(new Gato(nombre));
        infoText.text = $"{nombre} ha sido agregado a la granja como gato.";
        animalNameInput.text = ""; 
    }

    public void AgregarVaca()
    {
        string nombre = animalNameInput.text;
        miGranja.AgregarAnimal(new Vaca(nombre));
        infoText.text = $"{nombre} ha sido agregado a la granja como vaca.";
        animalNameInput.text = ""; 
    }

    public void MostrarSonidos()
    {
        infoText.text = "Sonidos de los animales:\n";
        foreach (var animal in miGranja.Animales)
        {
            
            string sonido = $"{animal.ObtenerNombre()} dice: ";

            if (animal is Perro)
            {
                sonido += "Guau!";
            }
            else if (animal is Gato)
            {
                sonido += "Miau!";
            }
            else if (animal is Vaca)
            {
                sonido += "Muu!";
            }

            infoText.text += sonido + "\n";

            animal.HacerSonido();
        }
    }
}
