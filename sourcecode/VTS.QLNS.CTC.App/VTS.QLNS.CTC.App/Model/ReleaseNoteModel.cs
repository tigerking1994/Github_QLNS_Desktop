using System;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.App.Model
{
    public class AppRelease
    {
        public string Version { get; set; }
        public List<ReleaseNoteModel> Notes { get; set; } = new List<ReleaseNoteModel>();
    }

    public class ReleaseNoteModel
    {
        public DateTime ReleaseDate { get; set; }
        public List<ReleaseNoteDetailModel> NoteDetails = new List<ReleaseNoteDetailModel>();
    }

    public class ReleaseNoteDetailModel
    {
        public string Module { get; set; }
        public List<string> Features { get; set; } = new List<string>();
        public List<string> BugFixes { get; set; } = new List<string>();
        public List<string> Improvements { get; set; } = new List<string>();
    }
}
