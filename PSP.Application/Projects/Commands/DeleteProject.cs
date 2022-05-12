using MediatR;
using PSP.Application.Models;
using PSP.Domain.Aggregates.ProjectAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Application.Projects.Commands {

    public class DeleteProject : IRequest<OperationResult<Project>> {
        public Guid ProjectId { get; set; }
    }
}