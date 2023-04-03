﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloc_de_notas_app.Models
{
    internal class AllNotes
    {
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        public AllNotes() => LoadNotes();

        public void LoadNotes()
        {
            Notes.Clear();

            string appDataPath = FileSystem.AppDataDirectory;

            IEnumerable<Note> notes = Directory
                .EnumerateFiles(appDataPath, "*.notes.txt")
                .Select(fileName => new Note()
                {
                    FileName = fileName,
                    Text = File.ReadAllText(fileName),
                    Date = File.GetCreationTime(fileName)
                })
                .OrderBy(note => note.Date);

            foreach (Note note in notes)
            {
                Notes.Add(note);
            }
        }
    }
}
