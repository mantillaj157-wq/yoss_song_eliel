using SpotifyApp_ElielMantilla.Models;

namespace SpotifyApp.Models
{
    public class SongArray
    {
        private Song?[] songs;
        private int count;

        public SongArray(int capacity = 50)
        {
            songs = new Song?[capacity];
            count = 0;
        }

        public int Count => count;
        public int Capacity => songs.Length;

        public Song[] GetAll()
        {
            // Contamos solo las canciones válidas para evitar nulos
            int validCount = 0;
            for (int i = 0; i < count; i++)
            {
                if (songs[i] != null) validCount++;
            }

            Song[] currentSongs = new Song[validCount];
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                if (songs[i] != null)
                {
                    currentSongs[index++] = songs[i]!;
                }
            }
            return currentSongs;
        }

        public bool Add(Song song)
        {
            if (count >= songs.Length) return false; // Arreglo lleno (Overflow)

            // Generación segura del ID basada en el elemento anterior o máximo existente
            int maxId = 0;
            for (int i = 0; i < count; i++)
            {
                if (songs[i] != null && songs[i]!.Id > maxId)
                {
                    maxId = songs[i]!.Id;
                }
            }
            song.Id = maxId + 1;

            songs[count] = song;
            count++;
            return true;
        }

        public bool Delete(int id)
        {
            int index = -1;
            for (int i = 0; i < count; i++)
            {
                if (songs[i] != null && songs[i]!.Id == id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1) return false;

            // Algoritmo de desplazamiento (Shift Left) para arreglos estáticos
            for (int i = index; i < count - 1; i++)
            {
                songs[i] = songs[i + 1];
            }

            songs[count - 1] = null;
            count--;
            return true;
        }

        // Algoritmo de Ordenamiento (BubbleSort) seguro contra nulos
        public void SortByTitle()
        {
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (songs[j] != null && songs[j + 1] != null)
                    {
                        string title1 = songs[j]?.Title ?? string.Empty;
                        string title2 = songs[j + 1]?.Title ?? string.Empty;

                        if (string.Compare(title1, title2, StringComparison.OrdinalIgnoreCase) > 0)
                        {
                            Song? temp = songs[j];
                            songs[j] = songs[j + 1];
                            songs[j + 1] = temp;
                        }
                    }
                }
            }
        }
    }
}