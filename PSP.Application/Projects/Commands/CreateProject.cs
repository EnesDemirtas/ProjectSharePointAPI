using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.ProjectAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Application.Projects.Commands {

    public class CreateProject : IRequest<OperationResult<Project>> {
        public Guid UserProfileId { get; set; }
        public string TextContent { get; set; }
    }
}