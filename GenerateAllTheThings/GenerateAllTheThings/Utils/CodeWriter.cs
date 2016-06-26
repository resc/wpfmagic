using System;
using System.IO;
using System.Linq;
using System.Text;

namespace GenerateAllTheThings.Utils
{
    /// <summary> CodeWriter keeps track of indentation and blocks, to simplify writing code. </summary>
    public class CodeWriter : TextWriter
    {

        public static CodeWriter CreateFile(string filepath)
        {
            return new CodeWriter(File.Open(filepath, FileMode.Create, FileAccess.Write, FileShare.Read));
        }

        private readonly TextWriter _w;
        private char _newline;
        private char[] _ignored
            ;
        private bool _writeIndent;
        private int _indentLevel;

        public CodeWriter() : this(new StringWriter())
        {
        }

        public CodeWriter(Stream s) : this(new StreamWriter(s))
        {
        }

        public CodeWriter(TextWriter w)
        {
            _w = w;
            SetNewLine(w.NewLine);
            IndentText = "    ";
            BlockStart = "{";
            BlockEnd = "}";
            BlockCommentStart = "/*";
            BlockCommentEnd = "*/";
            CommentStart = "//";
            CommentEnd = w.NewLine;
        }

        public string IndentText { get; set; }

        public string BlockStart { get; set; }
        public string BlockEnd { get; set; }

        public string BlockCommentStart { get; set; }
        public string BlockCommentEnd { get; set; }

        public string CommentStart { get; set; }
        public string CommentEnd { get; set; }

        public override Encoding Encoding
        {
            get { return _w.Encoding; }
        }

        public override string NewLine
        {
            get { return _w.NewLine; }
            set { SetNewLine(value); }
        }

        public override void Write(char value)
        {
            WriteIndent();
            if (value == _newline)
                _writeIndent = true;

            // normalize line endings...
            if (value == _newline)
                _w.Write(NewLine);
            else if (!_ignored.Contains(value))
                _w.Write(value);
        }

        /// <summary> Indents the code a level </summary>
        public IDisposable Indent()
        {
            _indentLevel++;
            return Disposable.Create(Dedent);
        }

        /// <summary> Writes a block </summary>
        /// <param name="trailer">text to immediatly follow the closing brace, before the newline. usually a ;</param>
        public IDisposable Block(string trailer = null)
        {
            WriteLine(BlockStart);
            var d = Indent();
            return Disposable.Create(() =>
            {
                d.Dispose();
                WriteLine($"{BlockEnd}{trailer}");
            });
        }

        /// <summary> Writes a block comment</summary>
        public IDisposable BlockComment()
        {
            WriteLine(BlockCommentStart);
            var d = Indent();
            return Disposable.Create(() =>
            {
                d.Dispose();
                WriteLine($"{BlockCommentEnd}");
            });
        }

        public void WriteComment(string comment)
        {
            Write(CommentStart);
            Write(comment.Replace(CommentEnd, $"{NewLine}{CommentStart}"));
            Write(CommentEnd);
        }

        public override void Flush()
        {
            _w.Flush();
        }

        public override string ToString()
        {
            return _w.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Flush();
                _w.Dispose();
            }

            base.Dispose(disposing);
        }

        private void SetNewLine(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentOutOfRangeException(nameof(value), "Newline cannot be null or empty");
            _w.NewLine = value;
            _newline = value.ToCharArray().Last();
            _ignored = value.ToCharArray().Except(new[] {_newline}).ToArray();
        }

        private void Dedent()
        {
            _indentLevel--;
        }

        private void WriteIndent()
        {
            if (_writeIndent)
            {
                for (int i = 0; i < _indentLevel; i++)
                    _w.Write(IndentText);

                _writeIndent = false;
            }
        }
    }
}