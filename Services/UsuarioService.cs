﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _sigInManager;
        private TokenService _tokenService;

        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> sigInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _sigInManager = sigInManager;
            _tokenService = tokenService;
        }

        public async Task Cadastra(CreateUsuarioDto dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);

            IdentityResult resultado = await
                _userManager.CreateAsync(usuario, dto.Password);
            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuário");
            };

        }

        public async Task<string> Login(LoginUsuarioDto dto)
        {
            var resultado = await _sigInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);
            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            };
            var usuario = _sigInManager
                .UserManager
                .Users
                .FirstOrDefault(user => user.NormalizedUserName ==
                dto.Username.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }



    }
}
