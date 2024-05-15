﻿using AutoMapper;
using ERP.TimeTable.DataService.Repositories.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace ERP.TimeTable.Api.Controllers;
[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]

public class BaseController : ControllerBase
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public BaseController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
}

