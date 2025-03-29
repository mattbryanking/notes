using System.Threading.Tasks;
using Notes.Models;

namespace Notes.UI
{
    static class MenuViewer
    {
        public static void DisplayMenu(List<Note> notes)
        {
            Console.Clear();
            Console.WriteLine("-----NOTES-----\n");

            Console.WriteLine("N. New Note");
            Console.WriteLine("C. Copy Note\n");

            List(notes);
        }

        public static void DisplayCopyMenu(List<Note> notes)
        {
            Console.Clear();
            Console.WriteLine("-----COPY-----\n");

            Console.WriteLine("Please choose which note to copy.\n");

            List(notes);
        }

        public static void DisplayNameMenu(List<Note> notes)
        {
            Console.Clear();
            Console.WriteLine("-----NAME-----\n");

            Console.WriteLine("Please name your new note.\n");

            List(notes);
        }

        public static void List(List<Note> notes)
        {
            for (int i = 0; i < notes.Count; i++)
            {
                Console.WriteLine($"{i}. {notes[i].Name} - {notes[i].LastEdited}");
            }
        }
    }
}