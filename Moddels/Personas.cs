namespace MauiAppUcenm.Models;

using SQLite;

public class Personas
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Nombres { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Apellidos { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    [MaxLength(200)]
    public string Direccion { get; set; } = string.Empty;

    [Unique]
    public string Telefono { get; set; } = string.Empty;
}