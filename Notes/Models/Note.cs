using System;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Notes.Models
{
    class Note
    {
        private int Id;
        private string Name;
        private DateTime LastEdited;

        private string Content;

        public Note()
        {
            Name = "New Note";
            LastEdited = DateTime.Now;
            Content = "";
        }

        // Name specified
        public Note(string Name) : this()
        {
            this.Name = Name;
        }

        // copying prexisting Note
        public Note(string Name, string Content) : this()
        {
            this.Name = $"{Name} Copy";
            this.Content = Content;
        }

        public async Task<bool> Save()
        {
            NoteFile _note = new NoteFile
            {
                Id = this.Id,
                Name = this.Name,
                LastEdited = this.LastEdited,
                Content = this.Content
            };

            string baseDir = AppContext.BaseDirectory;
            string projectDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..\.."));
            string notePath = Path.Combine(projectDir, "Saved", Id + ".json");

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
    }
    class NoteFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastEdited { get; set; }

        public string Content { get; set; }
    }


}
