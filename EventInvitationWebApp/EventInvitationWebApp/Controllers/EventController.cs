using EventInvitationWebApp.Models;
using EventInvitationWebApp.Models.DTO;
using EventInvitationWebApp.Models.ViewModel;
using EventInvitationWebApp.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EventInvitationWebApp.Controllers
{
    [Route("api/events")]
    [ApiController]
   
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public EventController(IEventRepository eventRepository, IUserRepository userRepository)
        {
            this._eventRepository = eventRepository;
            this._userRepository = userRepository;
        }


        
        [HttpPost]
        [Route("CreateEvent")]
        [Authorize]
        public async Task<IActionResult> CreateEvent([FromBody] EventViewModel eventView)
        {
            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //var userName = identity.FindFirst(ClaimTypes.Name).Value;
            var user = await _userRepository.GetUserById(eventView.CreatorId);
            if(user ==  null)
            {
                return BadRequest("User Not Found");
            }

            try
            {
                //DTO to Domain
                var newEvent = new Event
                {
                    Id = Guid.NewGuid().ToString(),
                    //Id = user.Id,
                    Name = eventView.Name,
                    StartDate = eventView.StartDate,
                    EndDate = eventView.EndDate,
                    Creator = user
                };

                await _eventRepository.CreateEventAsync(newEvent);
                //Domain to Dto
                var response = new EventDto
                {
                    Name = newEvent.Name,
                    StartDate = newEvent.StartDate,
                    EndDate = newEvent.EndDate,
                    Creator = new UserDto
                    {
                        Id = newEvent.Id,
                        Name = newEvent.Creator.Name,
                        Email = newEvent.Creator.Email
                    }
                };
               return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

       
        [HttpGet]
        [Route("GetAllEvents")]
       
        public async Task<IActionResult> GetAllEvents()
        {
            try
            {
                var _events = await _eventRepository.GetAllEventsAsync();

                if (_events == null || !_events.Any())
                {
                    return BadRequest("No Events In Database!");
                }

                var response = _events.Select(e => new EventDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Creator = e.Creator != null ? new UserDto
                    {
                        Id = e.Creator.Id,
                        Name = e.Creator.Name,
                        Email = e.Creator.Email
                    } : null,
                   
                }).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("GetEventById/{eventId}")]
        public async Task<IActionResult> GetEventById(string eventId)
        {
            try
            {
                var _event = await _eventRepository.GetEventById(eventId);

                if (_event == null)
                {
                    return NotFound($"Event with ID {eventId} not found");
                }

                var response = new EventDto
                {
                    Id = _event.Id,
                    Name = _event.Name,
                    StartDate = _event.StartDate,
                    EndDate = _event.EndDate,
                    Creator = _event.Creator != null ? new UserDto
                    {
                        Id = _event.Creator.Id,
                        Name = _event.Creator.Name,
                        Email = _event.Creator.Email
                    } : null,
                    Invitation = _event.Invitations != null ? _event.Invitations.Select(invitation => new InviteResponseDto(
                        eventId: invitation.EventId,
                        respondingUserName: invitation.UserId,
                        response: invitation.Response
                    )).ToList() : null
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("GetCreatedEventsByUserId/{userId}")]

        public async Task<IActionResult>GetCreatedEventsById(string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if(user == null ) 
            { 
                return BadRequest("User Not Found!");
            }

            try
            {
                var createdEventsByUser = await _eventRepository.GetEventsByUserAsync(userId);
                var response = createdEventsByUser.Select(e => new EventDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Creator = new UserDto
                    {
                        Id = e.Creator.Id,
                        Name = e.Creator.Name,
                        Email = e.Creator.Email
                    }
                }).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("InviteUsers")]
        public async Task<IActionResult> InviteUsers([FromBody] InviteUserDto inviteUserDto)
        {
            var _event = await _eventRepository.GetEventById(inviteUserDto.EventId);
            var invitedUser = await _userRepository.GetUserByName(inviteUserDto.InvitedUserName);

            if(_event==null || invitedUser == null)
            {
                return BadRequest("User or Event not found!");
            }

            if(_event.Invitations.Any(i => i.UserId == invitedUser.Id))
            {
                return BadRequest("Can't Send Invitation, User is already invited!");
            }

            try
            {
                var invitation = new Invitation
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = invitedUser.Id,
                    InvitedUser = invitedUser,
                    Response = Utilities.Enums.InvitationStatus.Pending
                };

                _event.Invitations.Add(invitation);
                await _eventRepository.UpdateEventAsync(_event);
                return Ok(new { message = "Invitation Sent Successfully!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong while sending the invitation.");
            }
           
        }

        //[HttpPost]
        //[Route("AcceptInvitation")]
        //public async Task<IActionResult> AcceptInvitation([FromBody] InviteResponseDto inviteResponseDto)
        //{

        //    var user = await _userRepository.GetUserByName(inviteResponseDto.RespondingUserName);
        //    var _event = await _eventRepository.GetEventById(inviteResponseDto.EventId);
        //    if(user == null || _event == null)
        //    {
        //        return BadRequest("User Or Evnet Not Found!");
        //    }

        //    var invitation = _event.Invitations.FirstOrDefault(i=>i.UserId ==  user.Id);
        //    if(invitation == null)
        //    {
        //        return BadRequest("Invitation Not Found For This User");
        //    }

        //    try
        //    {
        //        invitation.Response = Utilities.Enums.InvitationStatus.Accept;
        //        await _eventRepository.UpdateEventAsync(_event);
        //        return Ok("Invitation Successfully Accepted!");
        //    }
        //    catch(Exception ex)
        //    {
        //        return NotFound("Something Went Wrong!");
        //    }
        //}

        //[HttpPost]
        //[Route("RejectInvitation")]
        //public async Task<IActionResult> RejectInvitation([FromBody] InviteResponseDto inviteResponseDto)
        //{

        //    var user = await _userRepository.GetUserByName(inviteResponseDto.RespondingUserName);
        //    var _event = await _eventRepository.GetEventById(inviteResponseDto.EventId);
        //    if (user == null || _event == null)
        //    {
        //        return BadRequest("User Or Evnet Not Found!");
        //    }

        //    var invitation = _event.Invitations.FirstOrDefault(i => i.UserId == user.Id);
        //    if (invitation == null)
        //    {
        //        return BadRequest("Invitation Not Found For This User");
        //    }

        //    try
        //    {
        //        invitation.Response = Utilities.Enums.InvitationStatus.Reject;
        //        await _eventRepository.UpdateEventAsync(_event);
        //        return Ok("Invitation Successfully Rejected!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound("Something Went Wrong!");
        //    }
        //}

        [HttpPost]
        [Route("InviteResponse")]
        public async Task<IActionResult> InviteResponse([FromBody] InviteResponseDto inviteResponseDto)
        {
            var user = await _userRepository.GetUserByName(inviteResponseDto.RespondingUserName);
            var _event = await _eventRepository.GetEventById(inviteResponseDto.EventId);
            if (user == null || _event == null)
            {
                return BadRequest("User Or Evnet Not Found!");
            }

            var invitation = _event.Invitations.FirstOrDefault(i => i.UserId == user.Id);
            if (invitation == null)
            {
                return BadRequest("Invitation Not Found For This User");
            }

            try
            {
                if(inviteResponseDto.Status == "accept")
                {
                    invitation.Response = Utilities.Enums.InvitationStatus.Accept;
                }
                else if (inviteResponseDto.Status == "reject")
                {
                    invitation.Response = Utilities.Enums.InvitationStatus.Reject;
                }
                else
                {
                    return BadRequest("Invalid Action");
                }
                await _eventRepository.UpdateEventAsync(_event);
                return Ok($"Invitation Successfully {inviteResponseDto.Status}ed!");
            }
            catch (Exception ex) {
                return NotFound("Something Went Wrong!");
            }
        }

        [HttpGet]
        [Route("GetInvitedEventsOfUser/{userId}")]

        public async Task<IActionResult> GetInvitedEvents(string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if(user == null)
            {
                return BadRequest("User Not Found");
            }

            try
            {
                var invitedEvents = await _eventRepository.GetEventsUserIsInvitedTo(userId);
                if (invitedEvents == null)
                {
                    return BadRequest("User Not Invited In Any Event");
                }
                //var response = invitedEvents.Select(e => new EventDto
                //{
                //    Id = e.Id,
                //    Name = e.Name,
                //    StartDate = e.StartDate,
                //    EndDate = e.EndDate,
                //    Creator = e.Creator != null ? new UserDto
                //    {
                //        Id = e.Creator.Id,
                //        Name = e.Creator.Name,
                //        Email = e.Creator.Email
                //    } : null,
                //    InviteResponse = await GetInviteResponse(userId, e.Id)
                //}).ToList();
                var response = new List<EventDto>();
                foreach (var e in invitedEvents)
                {
                    var eventDto = new EventDto
                    {
                        Id = e.Id,
                        Name = e.Name,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        Creator = e.Creator != null ? new UserDto
                        {
                            Id = e.Creator.Id,
                            Name = e.Creator.Name,
                            Email = e.Creator.Email
                        } : null,
                        InviteResponse = await GetInviteResponse(userId, e.Id)
                    };
                    response.Add(eventDto);
                }
                    string json = JsonConvert.SerializeObject(response, Formatting.Indented);
                Console.WriteLine(json);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<InviteResponseDto> GetInviteResponse(string userId, string eventId)
        {
            var invitation = await _eventRepository.GetInviteResponse(userId, eventId);
            if (invitation != null)
            {
                return new InviteResponseDto(eventId, userId, invitation.Response);
                //return new InviteResponseDto
                //{
                //    EventId = eventId,
                //    RespondingUserName = userId,
                //    Status = invitation.Response.ToString() 
                //};
            }
            return null;
        }

    }
}
