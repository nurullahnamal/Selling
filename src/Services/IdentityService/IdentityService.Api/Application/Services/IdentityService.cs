﻿using IdentityService.Api.Application.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Api.Application.Services
{
	public class IdentityService : IIdentityService
	{
		public Task<LoginResponseModel> Login(LoginRequestModel requestModel)
		{
			//db

			var claims = new Claim[]
			{
				new Claim(ClaimTypes.NameIdentifier,requestModel.UserName),
				new Claim(ClaimTypes.Name,"Nurullah Namal"),
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKeyShouldBeLong"));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expiry = DateTime.Now.AddDays(10);

			var token = new JwtSecurityToken(claims: claims, expires: expiry, signingCredentials: creds, notBefore: DateTime.Now);

			var encodedJwt=new JwtSecurityTokenHandler().WriteToken(token);
			LoginResponseModel response = new()
			{
				UserToken = encodedJwt,
				UserName = requestModel.UserName,
			};

			return Task.FromResult(response);	
		}
	}
}
