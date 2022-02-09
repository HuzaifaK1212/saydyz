using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Platform.Data.Model.Configuration;
using Platform.Services.ConfigurationService;
using Platform.Utilities;
using Platform.Web.Api.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.Web.Api.Controllers.Configuration
{
    //test comment for checking git branch push
    public class ConfigurationController : Controller
    {

        private readonly IConfiguration Configuration;
        private readonly IMapper Mapper;

        private IConfigurationService ConfigurationService;
        
        public ConfigurationController(
            IConfiguration configuration, IMapper mapper,
            IConfigurationService configurationService)
        {
            Configuration = configuration;
            Mapper = mapper;

            this.ConfigurationService = configurationService;
        }


        //Audit
        
        [HttpGet]
        [Route("api/configuration/all/{profileKey}")]
        public async Task<IActionResult> GetAllConfigurations(string profileKey)
        {
            try
            {

                var query = await ConfigurationService.GetAllConfigurations(profileKey);
                if (query.Success)
                {
                    return Ok(new Response<ConfigurationProfileDto>()
                    {
                        Success = query.Success,
                        Message = query.Message,
                        Data = Mapper.Map<ConfigurationProfile, ConfigurationProfileDto>(query.Data)
                    });
                }
                else
                {
                    return Ok(query);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<int>()
                {
                    Success = false,
                    Message = $"Configurations fetching failed due to [{ex.Message}]",
                    Error = new Error()
                    {
                        Code = ErrorCodes.UNKNOWN_ERROR,
                        Cause = ex.Message
                    }
                });
            }
        }

        
        [HttpGet]
        [Route("api/configuration/profile/{profileId}")]
        public async Task<IActionResult> GetConfigurationProfileById(int profileId)
        {
            try
            {

                var query = await ConfigurationService.GetConfigurationProfileById(profileId);
                if (query.Success)
                {
                    return Ok(new Response<ConfigurationProfileDto>()
                    {
                        Success = query.Success,
                        Message = query.Message,
                        Data = Mapper.Map<ConfigurationProfile, ConfigurationProfileDto>(query.Data)
                    });
                }
                else
                {
                    return Ok(query);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<int>()
                {
                    Success = false,
                    Message = $"Configuration profile fetching failed due to [{ex.Message}]",
                    Error = new Error()
                    {
                        Code = ErrorCodes.UNKNOWN_ERROR,
                        Cause = ex.Message
                    }
                });
            }
        }

        
        [HttpGet]
        [Route("api/configuration/profile/all")]
        public async Task<IActionResult> GetAllConfigurationProfiles(int profileId)
        {
            try
            {

                var query = await ConfigurationService.GetAllConfigurationProfiles();
                if (query.Success)
                {
                    return Ok(new Response<List<ConfigurationProfileDto>>()
                    {
                        Success = query.Success,
                        Message = query.Message,
                        Data = Mapper.Map<List<ConfigurationProfile>, List<ConfigurationProfileDto>>(query.Data)
                    });
                }
                else
                {
                    return Ok(query);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<int>()
                {
                    Success = false,
                    Message = $"Configuration profiles fetching failed due to [{ex.Message}]",
                    Error = new Error()
                    {
                        Code = ErrorCodes.UNKNOWN_ERROR,
                        Cause = ex.Message
                    }
                });
            }
        }

        //Configuration
        
        [HttpPost]
        [Route("api/configuration/profile/add")]
        public async Task<IActionResult> AddConfigurationProfile([FromBody] ConfigurationProfileDto configurationProfileRequest)
        {
            try
            {
                var query = await ConfigurationService.AddConfigurationProfile(Mapper.Map<ConfigurationProfile>(configurationProfileRequest));
                if (query.Success)
                {
                    return Ok(query);
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is DbUpdateException)
                    return BadRequest(new Response<int>()
                    {
                        Success = false,
                        Message = $"Invalid data provided.",
                        Error = new Error()
                        {
                            Code = ErrorCodes.DB_ERROR,
                            Cause = ex.Message + (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message) ? $"INNEX [{ex.InnerException.Message}]" : "")
                        }
                    });
                return BadRequest(new Response<int>()
                {
                    Success = false,
                    Message = $"ConfigurationProfile addition failed due to [{ex.Message}]",
                    Error = new Error()
                    {
                        Code = ErrorCodes.UNKNOWN_ERROR,
                        Cause = ex.Message
                    }
                });
            }
        }

        
        [HttpPut]
        [Route("api/configuration/profile/update")]
        public async Task<IActionResult> UpdateConfigurationProfile([FromBody] ConfigurationProfileDto configurationProfileDto)
        {
            try
            {
                var query = await ConfigurationService.UpdateConfigurationProfile(Mapper.Map<ConfigurationProfile>(configurationProfileDto));
                if (query.Success)
                {
                    return Ok(new Response<ConfigurationProfileDto>()
                    {
                        Success = query.Success,
                        Message = query.Message,
                        Data = Mapper.Map<ConfigurationProfileDto>(query.Data)
                    });
                }
                else
                    return BadRequest(query);

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is DbUpdateException)
                    return BadRequest(new Response<int>()
                    {
                        Success = false,
                        Message = $"Invalid data provided.",
                        Error = new Error()
                        {
                            Code = ErrorCodes.DB_ERROR,
                            Cause = ex.Message + (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message) ? $"INNEX [{ex.InnerException.Message}]" : "")
                        }
                    });
                return BadRequest(new Response<int>()
                {
                    Success = false,
                    Message = $"ConfigurationProfile updated failed due to [{ex.Message}]",
                    Error = new Error()
                    {
                        Code = ErrorCodes.UNKNOWN_ERROR,
                        Cause = ex.Message
                    }
                });
            }
        }

        
        [HttpGet]
        [Route("api/configuration/profile/all/{pageNo}/{limit}")]
        public async Task<IActionResult> GetAllConfigurationProfiles(int pageNo, int limit)
        {
            try
            {

                var query = await ConfigurationService.GetAllConfigurationProfiles(pageNo, limit);
                if (query.Success)
                {
                    return Ok(new Response<dynamic>()
                    {
                        Success = query.Success,
                        Message = query.Message,
                        Data = Mapper.Map<List<ConfigurationProfile>, List<ConfigurationProfileDto>>(query.Data)
                    });
                }
                else
                {
                    return BadRequest(query);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<int>()
                {
                    Success = false,
                    Message = $"ConfigurationProfiles fetching failed due to [{ex.Message}]",
                    Error = new Error()
                    {
                        Code = ErrorCodes.UNKNOWN_ERROR,
                        Cause = ex.Message
                    }
                });
            }
        }

        
        [HttpPut]
        [Route("api/configuration/profile/parameters/update")]
        public async Task<IActionResult> AddOrUpdateAllConfigurationParameters([FromBody] List<ConfigurationParameterDto> configurationProfileDto)
        {
            try
            {
                var query = await ConfigurationService.AddOrUpdateAllConfigurationParameters(Mapper.Map<List<ConfigurationParameter>>(configurationProfileDto));
                if (query.Success)
                {
                    return Ok(new Response<ConfigurationProfileDto>()
                    {
                        Success = query.Success,
                        Message = query.Message,
                        Data = Mapper.Map<ConfigurationProfileDto>(query.Data)
                    });
                }
                else
                    return BadRequest(query);

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is DbUpdateException)
                    return BadRequest(new Response<int>()
                    {
                        Success = false,
                        Message = $"Invalid data provided.",
                        Error = new Error()
                        {
                            Code = ErrorCodes.DB_ERROR,
                            Cause = ex.Message + (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message) ? $"INNEX [{ex.InnerException.Message}]" : "")
                        }
                    });
                return BadRequest(new Response<int>()
                {
                    Success = false,
                    Message = $"ConfigurationProfile updated failed due to [{ex.Message}]",
                    Error = new Error()
                    {
                        Code = ErrorCodes.UNKNOWN_ERROR,
                        Cause = ex.Message
                    }
                });
            }
        }


        
        [HttpGet]
        [Route("api/configuration/profile/{profile}/parameter/{parameterId}")]
        public async Task<IActionResult> GetAllConfigurationProfiles(string profile, int parameterId)
        {
            try
            {
                var query = await ConfigurationService.GetConfigurationParameterByProfileAndKey(profile, parameterId);
                return Ok(query);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is DbUpdateException)
                    return BadRequest(new Response<int>()
                    {
                        Success = false,
                        Message = $"Invalid data provided.",
                        Error = new Error()
                        {
                            Code = ErrorCodes.DB_ERROR,
                            Cause = ex.Message + (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message) ? $"INNEX [{ex.InnerException.Message}]" : "")
                        }
                    });
                return BadRequest(new Response<int>()
                {
                    Success = false,
                    Message = $"Configuration parameter fetching failed due to [{ex.Message}]",
                    Error = new Error()
                    {
                        Code = ErrorCodes.UNKNOWN_ERROR,
                        Cause = ex.Message
                    }
                });
            }  
        }
        
        
        [HttpGet]
        [Route("api/configuration/parameter/{parameterId}")]
        public async Task<IActionResult> GetConfigurationParameterById( int parameterId)
        {
            try
            {
                var query = await ConfigurationService.GetConfigurationParameterById(parameterId);
                return Ok(query);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is DbUpdateException)
                    return BadRequest(new Response<int>()
                    {
                        Success = false,
                        Message = $"Invalid data provided.",
                        Error = new Error()
                        {
                            Code = ErrorCodes.DB_ERROR,
                            Cause = ex.Message + (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message) ? $"INNEX [{ex.InnerException.Message}]" : "")
                        }
                    });
                return BadRequest(new Response<int>()
                {
                    Success = false,
                    Message = $"Configuration parameter fetching failed due to [{ex.Message}]",
                    Error = new Error()
                    {
                        Code = ErrorCodes.UNKNOWN_ERROR,
                        Cause = ex.Message
                    }
                });
            }  
        }

    }
}
