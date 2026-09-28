namespace MauiAppUcenm
{
    public partial class MainPage : ContentPage
    {
        private static readonly System.Text.RegularExpressions.Regex IdRegex = new(
            @"^[1-9]\d{0,8}$",
            System.Text.RegularExpressions.RegexOptions.Compiled);

        private static readonly System.Text.RegularExpressions.Regex NombreRegex = new(
            @"^[\p{L}]+(?:[ '\-][\p{L}]+)*$",
            System.Text.RegularExpressions.RegexOptions.Compiled);

        private static readonly System.Text.RegularExpressions.Regex DireccionRegex = new(
            @"^(?=.{5,150}$)[\p{L}\p{N} .,#°º/'\-]+$",
            System.Text.RegularExpressions.RegexOptions.Compiled);

        private static readonly System.Text.RegularExpressions.Regex TelefonoRegex = new(
            @"^\+?(?:\d[ -]?){7,14}\d$",
            System.Text.RegularExpressions.RegexOptions.Compiled);

        public MainPage()
        {
            InitializeComponent();

            FechaNacimientoPicker.MaximumDate = DateTime.Today;
            FechaNacimientoPicker.Date = DateTime.Today.AddYears(-18);
        }

        private async void OnMostrarDatosClicked(object? sender, EventArgs e)
        {
            var idTexto = IdEntry.Text?.Trim() ?? string.Empty;
            var nombres = NombresEntry.Text?.Trim() ?? string.Empty;
            var apellidos = ApellidosEntry.Text?.Trim() ?? string.Empty;
            var direccion = DireccionEntry.Text?.Trim() ?? string.Empty;
            var telefono = TelefonoEntry.Text?.Trim() ?? string.Empty;

            var id = 0;

            var idValido = IdRegex.IsMatch(idTexto)
                && int.TryParse(idTexto, out id);

            var nombresValidos = NombreRegex.IsMatch(nombres);
            var apellidosValidos = NombreRegex.IsMatch(apellidos);
            var direccionValida = DireccionRegex.IsMatch(direccion);
            var telefonoValido = TelefonoRegex.IsMatch(telefono);

            var fechaNacimiento = FechaNacimientoPicker.Date.GetValueOrDefault();

            var fechaValida = FechaNacimientoPicker.Date.HasValue
                && fechaNacimiento.Date <= DateTime.Today;

            IdErrorLabel.IsVisible = !idValido;
            NombresErrorLabel.IsVisible = !nombresValidos;
            ApellidosErrorLabel.IsVisible = !apellidosValidos;
            FechaNacimientoErrorLabel.IsVisible = !fechaValida;
            DireccionErrorLabel.IsVisible = !direccionValida;
            TelefonoErrorLabel.IsVisible = !telefonoValido;

            if (!idValido || !nombresValidos || !apellidosValidos
                || !fechaValida || !direccionValida || !telefonoValido)
            {
                await DisplayAlertAsync(
                    "Datos inválidos",
                    "Revise los campos marcados en rojo.",
                    "Aceptar");

                return;
            }

            var persona = new MauiAppUcenm.Models.Personas
            {
                Id = id,
                Nombres = nombres,
                Apellidos = apellidos,
                FechaNacimiento = fechaNacimiento,
                Direccion = direccion,
                Telefono = telefono
            };

            var mensaje = $"ID: {persona.Id}\n"
                + $"Nombres: {persona.Nombres}\n"
                + $"Apellidos: {persona.Apellidos}\n"
                + $"Fecha de nacimiento: {persona.FechaNacimiento.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)}\n"
                + $"Dirección: {persona.Direccion}\n"
                + $"Teléfono: {persona.Telefono}";

            await DisplayAlertAsync(
                "Datos capturados",
                mensaje,
                "Aceptar");
        }
    }
}