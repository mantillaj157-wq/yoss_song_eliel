namespace SpotifyApp.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string? FilePath { get; set; } // Nueva ruta para el archivo .mp3 real
    }
}