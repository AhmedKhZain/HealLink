using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Authentication.Commands.UpdateUserData
{
    public class UpdateUserDataCommandHandler
        (IUsersRepository _userRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<UpdateUserDataCommand, ErrorOr<User>>
        
    {
        public async Task<ErrorOr<User>> Handle(UpdateUserDataCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.Id);
            user.UpdateProfile(
                command.ShowName,
                command.FullName,
                command.PhotoPath,
                command.Email);

            var result = await _unitOfWork.ExecuteInTransactionAsync(() =>
            {
                _userRepository.Update(user);
                return Task.CompletedTask;
            });

            if (result.IsError)
            {
                return result.Errors;
            }
            return user;

        }
    }
}
