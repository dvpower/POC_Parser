using System.ComponentModel.DataAnnotations;

namespace TextParserApi.Models
{
    public class TextSubmission
    {
        /// <summary>
        /// The text to be parsed and analyzed.
        /// </summary>
        /// <example>ENTER_THE_CONTENTS_OF_THE_837_HERE</example>
        [Required(ErrorMessage = "The text of the 837 file is required.")]
        public string Text { get; set; }
    }
}