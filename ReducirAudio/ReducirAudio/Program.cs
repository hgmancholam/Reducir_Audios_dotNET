using NAudio.Lame;
using NAudio.Wave;
using System;
using System.IO;
using System.Reflection.PortableExecutable;

class Program
{
    static void Main(string[] args)
    {
        string directorioEntrada = "C:\\Users\\hgman\\Downloads\\audios-corregidos\\originales"; // Ruta del directorio donde se encuentran los archivos MP3
        string directorioSalida = "C:\\Users\\hgman\\Downloads\\audios-corregidos\\reducidos"; // Ruta del directorio donde se guardarán los archivos MP3 reducidos

        string[] archivosMp3 = Directory.GetFiles(directorioEntrada, "*.mp3");

        foreach (string archivoMp3 in archivosMp3)
        {
            string nombreArchivo = Path.GetFileName(archivoMp3);
            string archivoSalida = Path.Combine(directorioSalida, nombreArchivo);

            ReducirTamanoMP3(archivoMp3, archivoSalida);
        }

        Console.WriteLine("Proceso completado.");
        Console.ReadLine();
    }

    static void ReducirTamanoMP3(string archivoEntrada, string archivoSalida)
    {

        using (var reader = new Mp3FileReader(archivoEntrada))
        {
            WaveFormat formatoAudio = reader.WaveFormat;
            Console.WriteLine($"Formato de audio: {formatoAudio.SampleRate} Hz, {formatoAudio.BitsPerSample} bits, {formatoAudio.Channels} canales");

            using (var writer = new LameMP3FileWriter(archivoSalida, formatoAudio, LAMEPreset.ABR_32))
            {
                reader.CopyTo(writer);
            }
        }
    }
}
