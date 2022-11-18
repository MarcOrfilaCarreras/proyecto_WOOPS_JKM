using UnityEngine;
using UnityEngine.UI;
using System;

public class linternaController : MonoBehaviour
{
    //variables publicas
    public GameObject luz;
    public float opacidad = 0.5f;
    public Button boton;
    public float tiempoEncendida = 1;

    //variables privadas
    private bool encendida = false;
    private DateTime contador;

    //se ejecuta al principio de la escena
    void Start()
    {
        //desactivamos la luz
        luz.SetActive(false);

        //añadimos un listener
        boton.onClick.AddListener(TaskOnClick);
    }

    //se ejecuta cada frame
    void Update()
    {   
        //si la luz esta encendida
        if (encendida)  {
            //cambiamos la opacidad
            ChangeAlpha(luz.GetComponent<Renderer>().material, opacidad);
            
            //activamos la luz
            luz.SetActive(true);
        
            //si no ha pasado el tiempo de encendido
            if ((DateTime.Now - contador).TotalSeconds > tiempoEncendida){
                encendida = false;
            }
     } else {
        encendida = false;

        //ocultamos la luz
        luz.SetActive(false);
     }
    }

    //https://stackoverflow.com/a/62022162
    void ChangeAlpha(Material mat, float alphaVal) {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    }

    void TaskOnClick()
    {
        encendida = true;
        contador = System.DateTime.Now;
    }
}
