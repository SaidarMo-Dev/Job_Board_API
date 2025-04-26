using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Models
{
	public class DeleteUserCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }

	}
}
