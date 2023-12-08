using Notes.Repository.InterfacesOfStorage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Models
{
    /// <summary>
    /// Заметка
    /// </summary>
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Заголовок заметки.
        /// </summary>
        private string title;
        [Required]
        [MaxLength(50)]
        public string Title 
        { 
            get 
            {
                return title;
            } 
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Заголовок заметки не может быть Null или пустым.");
                }
                if (value.Length > 50)
                {
                    throw new ArgumentException("Заголовок заметки не может быть длинее 50 символов");
                }
                title = value;
            } 
        }
        /// <summary>
        /// Текст заметки.
        /// </summary>
        private string text = "";
        [MaxLength(300)]
        public string Text 
        { 
            get
            {
                return text;
            }
            set
            {
                if (value.Length > 300)
                {
                    throw new ArgumentException("Текст заметки не может быть длинее 300 символов");
                }

                if (string.IsNullOrWhiteSpace(value))
                {
                    text = "";
                }
                else
                {
                    text = value;
                }
            } 
        }

        /// <summary>
        /// Дата создания заметки.
        /// </summary>
        public DateTime DateCreate { get; set; } = DateTime.Now;
        /// <summary>
        /// Дата закрепления заметки.
        /// </summary>
        private DateTime? datePin = null;
        public DateTime? DatePin 
        {
            get
            {
                return datePin;
            }
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ArgumentException("Дата прикрепления заметки не может быть раньше нынешней даты.");
                }
                datePin = value;
            }
        }

        /// <summary>
        /// Ссылки на тэги для заметки.
        /// </summary>
        public List<Tag> Tags { get; set; } = new();
        public List<Push> Pushes { get; set; } = new();

        /// <summary>
        /// Создать заметку.
        /// </summary>
        /// <param name="title"> Заголовок заметки. </param>
        public Note(string title)
        {
            Title = title;
            DateCreate = DateTime.Now;
        }
        /// <summary>
        /// Создать заметку.
        /// </summary>
        /// <param name="title"> Заголовок заметки. </param>
        /// <param name="text"> Текст заметки. </param>
        public Note(string title,string text) : this(title)
        {
            Text = text;
        }

        /// <summary>
        /// Создать заметку.
        /// </summary>
        /// <param name="title"> Заголовок заметки. </param>
        /// <param name="text"> Текст заметки. </param>
        /// <param name="date"> Дата прикрепления заметки. </param>
        public Note(string title, string text, DateTime date) : this(title, text)
        {
            DatePin = date;
        }
    }
}
