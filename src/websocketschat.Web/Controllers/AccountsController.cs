using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreModels = websocketschat.Core.Models;
using websocketschat.Core.Services.Interfaces;
using websocketschat.Datatransfer.HttpDto;

namespace websocketschat.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountsController> _logger;
        public AccountsController(IUserService userService, 
                                  IMapper mapper, 
                                  ILogger<AccountsController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        //POST api/accounts/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterUser(PostUserDto userDto)
        {
            CoreModels.User coreUser = _mapper.Map<CoreModels.User>(userDto);
            bool isCreated = await _userService.AddUserAsync(coreUser, userDto.Password);

            if (isCreated)
                return StatusCode(201);

            return StatusCode(400);
        }
    }
}
