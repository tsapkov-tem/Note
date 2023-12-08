using Notes.Repository.InterfacesOfStorage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Models
{
    /// <summary>
    /// Оповещения для заметок.
    /// </summary>
    public class Push
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Дата оповещения.
        /// </summary>
        [Required]
        public DateTime Date { get;set; }
        /// <summary>
        /// Айди заметки, к которой относится оповещение.
        /// </summary>
        [Required]
        public int IdNote { get; set; }
        /// <summary>
        /// Ссылка на заметку, к которой относится оповещение.
        /// </summary>
        public Note Note { get; set; }
        /// <summary>
        /// Создать новое оповещение.
        /// </summary>
        /// <param name="note"> Заметка, для которой создается оповещение. </param>
        /// <exception cref="ArgumentException"> Передаваемая заметка не имеет даты прикрепления. </exception>
        /// /// <exception cref="ArgumentException"> Передаваемая заметка имеет дату прикрепления раньше, чем сейчас. </exception>
        public Push(Note note)
        {
            if(note.DatePin is null)
            {
                throw new ArgumentException("Создание пуш-уведомления невозможно для заметки без даты напоминаниия.");
            }
            if (note.DatePin < DateTime.Now)
            {
                throw new ArgumentException("Создание пуш-уведомления невозможно для заметки c датой напоминания раньше, чем сейчас.");
            }
            Note = note;
            Date = (DateTime)note.DatePin;
        }

        /// <summary>
        /// Пустой конструктор для EF.
        /// Должен не использоваться самостоятельно.
        /// </summary>
        public Push() { }
    }
}
