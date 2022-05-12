using System.ComponentModel.DataAnnotations;

namespace PSP.Api.Contracts.Projects.Requests {

    public class ProjectCommentUpdate {

        [Required]
        public string Text { get; set; }
    }
}