using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using TMPro;


[System.Serializable]
public class Puntuaciones
{
    public int puntuacion;
    public string nombreJugador;
    public string fecha;

    public Puntuaciones(int puntuacion, string nombreJugador, string fecha)
    {
        this.puntuacion = puntuacion;
        this.nombreJugador = nombreJugador;
        this.fecha = fecha;
    }

    public override string ToString()
    {
        return $"{nombreJugador}\t\t {puntuacion} puntos\t\t {fecha}\n";
    }
}


public class ControladorPuntaciones : MonoBehaviour
{
    private List<Puntuaciones> listaPuntuaciones = new List<Puntuaciones>();
    private string dataPath = Application.dataPath + "/Datos/puntuaciones.dat";

    public TMP_Text textoRanking;

    public void Start()
    {
        CargarDatos();
        ActualizarTextoRanking();
    }
    public void AñadirPuntuacion(Puntuaciones puntacion)
    {
        listaPuntuaciones.Add(puntacion);
        listaPuntuaciones.Sort((x, y) => y.puntuacion.CompareTo(x.puntuacion));
        if (listaPuntuaciones.Count > 5)
        {
            listaPuntuaciones.RemoveRange(5, listaPuntuaciones.Count - 5);
        }
        ActualizarTextoRanking();
    }

    public void GuardarDatos()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(dataPath));

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream file = File.Create(dataPath))
        {
            formatter.Serialize(file, listaPuntuaciones);
        }
        ActualizarTextoRanking();
    }

    public void CargarDatos()
    {
        if (File.Exists(dataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = File.Open(dataPath, FileMode.Open))
            {
                listaPuntuaciones = (List<Puntuaciones>)formatter.Deserialize(file);
            }
        }
    }

    public string obtenerPunt()
    {
        string texto = "NOMBRE\t\tPUNTOS\t\tFECHA\n";
        texto += "-----------------------------------------------------------\n\n"; 
        foreach (var p in listaPuntuaciones)
        {
            texto += p;
        }
        return texto;
    }

    public void EliminarTodasLasPuntuaciones()
    {
        listaPuntuaciones.Clear();
    }

    public void ActualizarTextoRanking()
    {
        if (textoRanking != null)
        {
            textoRanking.text = obtenerPunt();
        }
    }
}
