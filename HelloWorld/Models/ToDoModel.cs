using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.Models
{
    public class ToDoModel
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; private set; }
        public string Text { get; private set; }
        public bool Completed { get; private set; }

        public ToDoModel(string text)
        {
            if(text == "")
            {
                throw new ArgumentOutOfRangeException("text must be filled!", nameof(text));
            }
            Text = text;
        }

        public  bool Update(string text)
        {
            if (text == "")
            {
                return false;
            }
            Text = text;
            return true;
        }

        /// <summary>
        /// Completed non si potrà mettere a false secondo la mutazione 
        /// </summary>
        public void Complete()
        {
            Completed = true;
        }
        
        
    }
}
