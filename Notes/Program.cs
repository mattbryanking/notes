using Notes.UI;
using Notes.Models;

namespace Notes
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {

                List<Note> notes = await Note.GetNotes();

                MenuViewer.DisplayMenu(notes);

                string? input = Convert.ToString(Console.ReadKey(intercept: true).KeyChar).ToLower();

                if (input == "\u001b" || input == "\b")
                {
                    break;
                }
                else if (input == null)
                {
                    continue;
                }
                else if (input == "c")
                {
                    while (true)
                    {
                        MenuViewer.DisplayCopyMenu(notes);

                        string? copyInput = Convert.ToString(Console.ReadKey(intercept: true).KeyChar).ToLower();

                        if (copyInput == "\u001b" || copyInput == "\b")
                        {
                            break;
                        }

                        if (int.TryParse(copyInput, out int selectedCopyId))
                        {
                            Note? selectedCopyNote = notes.FirstOrDefault(n => n.Id == selectedCopyId);

                            if (selectedCopyNote != null)
                            {
                                await selectedCopyNote.Copy();
                            }
                        }
                    }
                }
                else if (input == "n")
                {

                    MenuViewer.DisplayNameMenu(notes);

                    string? name = Console.ReadLine();

                    if (name == null)
                    {
                        continue;
                    }

                    int newId = await Note.GetNextIdAsync();

                    Note newNote = new Note(newId, name, "");

                    await newNote.Save();
                }

                else if (int.TryParse(input, out int selectedId))
                {
                    Note? selectedNote = notes.FirstOrDefault(n => n.Id == selectedId);

                    if (selectedNote != null)
                    {
                        // show note viewer
                    }
                }

            }
        }
    }
}
