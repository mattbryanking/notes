using System;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Notes.Models
{
    class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastEdited { get; set; }
        public string Content { get; set; }

        public Note()
        {
            // MAX ID + 1
            Name = "New Note";
            Content = "";
        }

        // copying prexisting note
        public Note(string Name, string Content) : this()
        {
            this.Name = $"{Name} Copy";
            this.Content = Content;
        }
        public Note(int Id, string Name, string Content)
        {
            this.Name = Name;
            this.Content = Content;
            this.Id = Id;
        }

        public string GetPath(int id)
        {
            string baseDir = AppContext.BaseDirectory;
            string projectDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..\.."));
            return Path.Combine(projectDir, "Saved", Id + ".json");
        }

        public static async Task<List<Note>> GetNotes()
        {
            string baseDir = AppContext.BaseDirectory;
            string projectDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..\.."));
            string fileDir = Path.Combine(projectDir, "Saved");
            string[] filePaths = Directory.GetFiles(fileDir, "*.json", SearchOption.TopDirectoryOnly);

            List<Note> notes = new List<Note>();

            foreach (string path in filePaths)
            {
                Note note = new Note();
                await note.Load(path);
                notes.Add(note);
            }

            return notes;
        }

        public static async Task<int> GetNextIdAsync()
        {
            List<Note> notes = await GetNotes();
            return notes.Count != 0 ? notes.Max(n => n.Id) + 1 : 1;
        }


        public async Task<bool> Load(string path)
        {
            await using FileStream openStream = File.OpenRead(path);
            NoteFile? loadedNote = await JsonSerializer.DeserializeAsync<NoteFile>(openStream);

            if (loadedNote == null)
            {
                return false;
            }

            Id = loadedNote.Id;
            Name = loadedNote.Name;
            LastEdited = loadedNote.LastEdited;
            Content = loadedNote.Content;

            return true;
        }

        public async Task<bool> Save()
        {
            NoteFile _note = new NoteFile
            {
                Id = this.Id,
                Name = this.Name,
                LastEdited = DateTime.Now,
                Content = this.Content
            };

            string notePath = GetPath(Id);

            try
            {
                await using FileStream createStream = File.Create(notePath);
                await JsonSerializer.SerializeAsync(createStream, _note);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public async Task<bool> Copy()
        {
            int newId = await GetNextIdAsync();
            Note copiedNote = new Note(newId, this.Name + " Copy", this.Content);
            await copiedNote.Save();
            return true;
        }

    }
    class NoteFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastEdited { get; set; }

        public string Content { get; set; }
    }


}
