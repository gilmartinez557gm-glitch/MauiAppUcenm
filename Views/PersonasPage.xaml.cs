namespace MauiAppUcenm.Views;

using MauiAppUcenm.Data;
using MauiAppUcenm.Models;

public partial class PersonasPage : ContentPage
{
    private readonly SQLiteDatabase _database;

    public PersonasPage()
    {
        InitializeComponent();

        string dbPath = Path.Combine(
            FileSystem.AppDataDirectory,
            "personas.db3");

        _database = new SQLiteDatabase(dbPath);
    }

    private async void GuardarButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            Personas personas = new Personas
            {
                Nombres = NombresEntry.Text,
                Apellidos = ApellidosEntry.Text,
                FechaNacimiento = FechaNacimientoPicker.Date ?? DateTime.Now,
                Direccion = DireccionEntry.Text,
                Telefono = TelefonoEntry.Text
            };

            await _database.AddPersonaAsync(personas);

            await DisplayAlertAsync(
                "Éxito",
                "Persona guardada correctamente.",
                "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "Error",
                $"Ocurrió un error al guardar la persona: {ex.Message}",
                "OK");
        }
    }
}