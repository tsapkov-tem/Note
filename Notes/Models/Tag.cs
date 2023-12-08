using Microsoft.EntityFrameworkCore;
using Notes.Repository.InterfacesOfStorage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Models
{
    /// <summary>
    /// Тэг для заметок.
    /// </summary>
    [Index("Name")]
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Название тэга.
        /// </summary>
        private string name;
        [Required]
        [MaxLength(50)]
        public string Name 
        {
            get 
            {
                return name;
            }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Имя тега не может быть Null или пустым.");
                }
                if (value.Length > 50)
                {
                    throw new ArgumentException("Имя тэга не может быть длинее 50 символов.");
                }
                name = value;
            } 
        }
        /// <summary>
        /// Ссылки на заметки, к которые отмечены тэгом.
        /// </summary>
        public List<Note> Notes { get; set; } = new();

        /// <summary>
        /// Создать новый тэг.
        /// </summary>
        /// <param name="name"> Название тэга. </param>
        /// <exception cref="ArgumentException"> Имя тэга Null или пустое. </exception>
        public Tag(string name)
        {
            Name = name;
        }
    }
}
