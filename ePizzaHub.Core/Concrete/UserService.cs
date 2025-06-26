using AutoMapper;
using ePizzaHub.Core.Contracts;
using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Models.ApiModels.Request;
using Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.Concrete
{
    public class UserService : IUserService

    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;
        public UserService(IRoleRepository roleRepository , IUserRepository userRepository,IMapper mapper) {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<bool> CreateUserRequestAsync(CreateUserRequest createUserRequest)
        {
            // 1. Insert records in user table and role table
            // 2. Hash password sending my end user 
            var rolesDetails = _roleRepository.GetAll().Where(x=>x.Name == "User").FirstOrDefault();
            if (rolesDetails != null)
            {
                var user = _mapper.Map<User>(createUserRequest);
                user.Roles.Add(rolesDetails);
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                await _userRepository.AddAsync(user);
                int rowsInserted = await _userRepository.CommitAsync();
                return rowsInserted > 0;
            }

            return false;
        }
    }
}
