namespace Notes.Models
{
    class Note
    {
        private string name;
        private DateTime lastEdited;

        private string content;

        private string tempContent;

        public Note()
        {
            name = "New Note";
            lastEdited = DateTime.Now;
            content = "";
            tempContent = content;
        }

        // name specified
        public Note(string name) : this()
        {
            this.name = name;
        }

        // copying prexisting Note
        public Note(string name, string content) : this()
        {
            this.name = $"{name} Copy";
            this.content = content;
            tempContent = content;
        }
    }
}
