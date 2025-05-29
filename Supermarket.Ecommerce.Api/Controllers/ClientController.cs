using Microsoft.AspNetCore.Mvc;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Entities;
using Supermarket.Ecommerce.Core.Http;

namespace Supermarket.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientRepository _clientRepository;
    
    public ClientController(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<ClientDto>>>> GetAll()
    {
        var response = new Response<List<ClientDto>>();
        var clients = await _clientRepository.GetAllAsync();
        response.Data = clients.Select(c => new ClientDto(c)).ToList();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<ClientDto>>> Post([FromBody] ClientDto.ClientCreateDto clientDto)
    {
        var client = new Client()
        {
            Username = clientDto.Username, // Añadido
            FirstName = clientDto.FirstName,
            LastName = clientDto.LastName,
            Phone = clientDto.Phone
        };
    
        client = await _clientRepository.SaveAsync(client);
        var response = new Response<ClientDto> { 
            Data = new ClientDto(client) 
        };
    
        return Created($"/api/client/{client.Id}", response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<ClientDto>>> GetById(int id)
    {
        var response = new Response<ClientDto>();
        var client = await _clientRepository.GetById(id);

        if (client == null)
        {
            response.Errors.Add("Client Not Found");
            return NotFound(response);
        }
        
        response.Data = new ClientDto(client);
        return Ok(response);
    }

    // Cambiado de byauthuser a byusername
    [HttpGet("byusername/{username}")]
    public async Task<ActionResult<Response<ClientDto>>> GetByUsername(string username)
    {
        var response = new Response<ClientDto>();
        var client = await _clientRepository.GetByUsername(username);

        if (client == null)
        {
            response.Errors.Add("Client Not Found");
            return NotFound(response);
        }
        
        response.Data = new ClientDto(client);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<ClientDto>>> Update([FromBody] ClientDto clientDto)
    {
        var response = new Response<ClientDto>();
        var client = await _clientRepository.GetById(clientDto.Id);
        
        if (client == null)
        {
            response.Errors.Add("Client not found");
            return NotFound(response);
        }
        
        client.Username = clientDto.Username; // Añadido
        client.FirstName = clientDto.FirstName;
        client.LastName = clientDto.LastName;
        client.Phone = clientDto.Phone;
        
        await _clientRepository.UpdateAsync(client);
        response.Data = new ClientDto(client);
        
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _clientRepository.DeleteAsync(id);
        response.Data = result;
        return Ok(response);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<Response<LoginResponseDto>>> Login([FromBody] LoginRequestDto request)
    {
        var response = new Response<LoginResponseDto>();
        var client = await _clientRepository.GetByUsername(request.Username);

        if (client == null || client.Phone != request.Phone)
        {
            response.Data = new LoginResponseDto { Success = false, Message = "Credenciales inválidas" };
            return Unauthorized(response);
        }

        response.Data = new LoginResponseDto { Success = true, Message = "Inicio sesión", Client = new ClientDto(client) };
        return Ok(response);
    }
}