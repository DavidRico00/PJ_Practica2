using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using TMPro;
using System;


[Serializable]
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
        return $"{nombreJugador,-15}{puntuacion,-10}{fecha,-15}\n";
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
        if (textoRanking != null)
            ActualizarTextoRanking();
    }

    public void AnadirPuntuacion(int p, string nombre)
    {
        Puntuaciones puntacion = new Puntuaciones(p, nombre, DateTime.Now.ToString("dd/MM/yyyy"));

        listaPuntuaciones.Add(puntacion);
        listaPuntuaciones.Sort((x, y) => y.puntuacion.CompareTo(x.puntuacion));
        if (listaPuntuaciones.Count > 5)
        {
            listaPuntuaciones.RemoveRange(5, listaPuntuaciones.Count - 5);
        }
        GuardarDatos();
    }

    public void GuardarDatos()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(dataPath));

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream file = File.Create(dataPath))
        {
            formatter.Serialize(file, listaPuntuaciones);
        }
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

    public string obtenerPuntuaciones()
    {
        string texto = "";
    texto += $"{"NOMBRE",-15}{"PUNTOS",-10}{"FECHA",-15}\n";
    texto += "---------------------------------------------\n\n";

    foreach (var p in listaPuntuaciones)
    {
        texto += p;
    }

        return texto;
    }

    public void EliminarTodasLasPuntuaciones()
    {
        listaPuntuaciones.Clear();
        GuardarDatos();
    }

    public void ActualizarTextoRanking()
    {
        if (textoRanking != null)
        {
            textoRanking.text = obtenerPuntuaciones();
        }
    }
}
