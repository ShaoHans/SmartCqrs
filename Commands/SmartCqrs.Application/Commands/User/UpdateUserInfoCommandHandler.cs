﻿using AutoMapper;
using SmartCqrs.Application.Dtos;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.Repositories;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Infrastructure.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCqrs.Application.Commands
{
    public class UpdateUserInfoCommandHandler : BaseCommandHandler<UpdateUserInfoCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserInfoCommandHandler(IUnitOfWork uow, 
            IUserRepository userRepository) : base(uow)
        {
            _userRepository = userRepository;
        }

        public override async Task<CommandResult> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUserIdAsync(request.LoginUserId);
            if(user == null)
            {
                return new CommandResult(ResultCode.FAIL, "不存在此用户");
            }

            UpdateUserInfoDto userDto = Mapper.Map<UpdateUserInfoDto>(user);
            request.UserPatch.ApplyTo(userDto);
            Mapper.Map(userDto, user);
            await _userRepository.UpdateAsync(user);
            await UnitOfWork.SaveChangesAsync();

            return new CommandResult();
        }
    }
}
