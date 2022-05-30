﻿global using AutoMapper;
global using MediatR;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.ApiExplorer;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Mvc.Versioning;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using PSP.Api.Contracts.Common;
global using PSP.Api.Contracts.Identity;
global using PSP.Api.Contracts.Projects.Requests;
global using PSP.Api.Contracts.Projects.Responses;
global using PSP.Api.Contracts.UserProfile.Requests;
global using PSP.Api.Contracts.UserProfile.Responses;
global using PSP.Api.Filters;
global using PSP.Api.Options;
global using PSP.Application.Enums;
global using PSP.Application.Identity.Commands;
global using PSP.Application.Models;
global using PSP.Application.Options;
global using PSP.Application.Projects.Commands;
global using PSP.Application.Projects.Queries;
global using PSP.Application.Users.Commands;
global using PSP.Application.Users.Queries;
global using PSP.Dal;
global using PSP.Domain.Aggregates.ProjectAggregate;
global using PSP.Domain.Aggregates.UserAggregate;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using System.ComponentModel.DataAnnotations;
global using System.Text;
